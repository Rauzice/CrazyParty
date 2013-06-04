/**
 *Module dependencies
 */

var util = require('util');
var constvars = require('../util/constvars');
var liarDiceGameDao = require('../dao/liarDiceGameDao');
var userDao = require('../dao/userDao');

/**
 * LiarDiceGame: player1 join --> player1 waiting
 * 		--> player2 join --> player2 waiting
 * 		--> new room
 * 		--> new game --> prepare --> start --> player1 --> player2
 * 					--> showDices --> game end -- > new game ...
 * 		-->leave game
 */

var USER_PLAY_TIME = 15000;
var INTERNET_TIMEOUT = 1000;

var GAME_ENDED_STATE = 9;
var GAME_SHOWED_STATE = 8;
var GAME_PLAYER1PLAY_STATE = 7;
var GAME_PLAYER0PLAY_STATE = 6;
//var GAME_TWODICED_STATE = 5;
var GAME_ONEDICED_STATE = 4;
var GAME_DICING_STATE   = 3;
//var GAME_TWOPREPARED_STATE = 2;
var GAME_ONEPREPARED_STATE = 1;
var GAME_PREPARING_STATE = 0;

var PLAYER1_FIRST = 0;
var PLAYER2_FIRST = 1;

var SENDTO_NOONE = 0;
var SENDTO_PLAYER0 = 1;
var SENDTO_PLAYER1 = 2;
var SENDTO_BOTH = 3;

/**
 * Initialize a new 'LiarDiceGame' with the given 'opts'.
 *
 * @param {Object} opts
 * @param user0id {unsigned int}
 * @param user1id {unsigned int}
 * @param room {LiarDiceGameRoom}
 */

var LiarDiceGame = function(user0id, user1id, room) {
    this.gameInfo = {};
    var self = this;
    userDao.getUserIndoById(user0id, function(err, data){
        if(!!err){
            throw err;
        }else{
            self.gameInfo.user0name = data.name;
        }
    });
    userDao.getUserIndoById(user1id, function(err, data){
        if(!!err){
            throw err;
        }else{
            self.gameInfo.user1name = data.name;
        }
    });
	this.first = constvars.random(2);
	this.gameInfo.player0id = user0id;
	this.gameInfo.player1id = user1id;
    this.room = room;
};

/**
 * game start
 */
LiarDiceGame.prototype.start = function(){
	this.startTime = new Date();
	this.endTime = constvars.oneday;
    this.gameInfo.state = GAME_PREPARING_STATE;
	this.gameInfo.player0Dices = [];
	this.gameInfo.player1Dices = [];

	this.gameInfo.player0State = {number: 0, point: 0};
	this.gameInfo.player1State = {number: 0, point: 0};
	this.gameInfo.liars = [];
	this.isOneLiared = false;
	this.gameInfo.liars.push({player: 1-this.first, number:0, point:0});
///   this.lastUpdateTime = new Date();
	this.room.send(SENDTO_BOTH, this.gameInfo);
    this.timeoutid = setTimeout(function(){
        this.endgame("玩家未准备。");
    }, USER_PLAY_TIME);
};

/**
 * one player prepared
 * @param {0 or 1} player
 */
LiarDiceGame.prototype.prepare = function(player){
	if(this.gameInfo.state === GAME_ONEPREPARED_STATE){
        clearTimeout(this.timeoutid);
		this.dicing();
	}else if(this.gameInfo.state === GAME_PREPARING_STATE || this.gameInfo.state === GAME_SHOWED_STATE){
        this.gameInfo.state= GAME_ONEPREPARED_STATE;
        this.room.send(SENDTO_BOTH, this.gameInfo);
	}else{
        this.endGame("服务器内部错误。");
    }
};

LiarDiceGame.prototype.dicing = function(){
	this.gameInfo.state = GAME_DICING_STATE;
    this.room.send(SENDTO_BOTH, this.gameInfo);
    this.timeoutid = setTimeout(function(){
        if(!this.player0Dices){
            this.state ++;
            for(var i=0; i< 5; i++)
                this.player0Dices.push(Math.random(constvars.random(6) + 1));
        }
        if(!this.player0Dices){
            this.state ++;
            for(var i=0; i< 5; i++)
                this.player0Dices.push(Math.random(constvars.random(6) + 1));
        }
        if(this.first === 0)
            this.gameInfo.state = GAME_PLAYER0PLAY_STATE;
        else
            this.gameInfo.state = GAME_PLAYER1PLAY_STATE;
        this.room.send(SENDTO_BOTH, this.gameInfo);
    }, USER_PLAY_TIME )
}

/**
 *  get dices of one player
 *  @param {0 or 1} player,
 *  @param {array} dices
 */
LiarDiceGame.prototype.diced = function(player, dices){
	if(player === 0){
		this.player0Dices.push(dices);
	}else{
		this.player1Dices.push(dices);
	}

	if(this.gameInfo.state === GAME_ONEDICED_STATE){
        clearTimeout(this.timeoutid);
        if(this.first === 0)
            this.gameInfo.state = GAME_PLAYER0PLAY_STATE;
        else
            this.gameInfo.state = GAME_PLAYER1PLAY_STATE;
        this.room.send(SENTO_BOTH,this.gameInfo);
    }
	else if(this.state === GAME_DICING_STATE){
        this.gameInfo.state = GAME_ONEDICED_STATE;
	}else
        this.endgame("服务器错误.");
};

LiarDiceGame.prototype.liar = function(player, liarState){
	this.gameInfo.liars.push({player: player, number: liarState.number, point: liarState.point});
	if(liarState.point === 1)
		this.isOneLiared = true;
    if(this.player === 0)
        this.gameInfo.state = GAME_PLAYER1PLAY_STATE;
    else
        this.gameInfo.state = GAME_PLAYER0PLAY_STATE;
    this.room.send(SENTO_BOTH, this.gameInfo);
};

/**
 * judge if one condition is straight.
 * @param {array} a,
 * @param {bool} isOneOkay,
 */
var isStraight = function(a, isOneOkay){
	var points = [];
	for(var i = 1; i<= 6; i++)
		point[i] = 0;
	if(isOneOkay){
		for(var x in a){
			if(point[x] > 0)
				return false;
			point[x]++;
		}
		return true;
	}
	for(var x in a){
		if(point[x] > 0 || x == 1)
			return false;
	}
	return true;
}

LiarDiceGame.prototype.showDices = function(player){
	var pointCount = 0;
	var point = this.liars[this.liars.length - 1].point;
	var liarCount = this.liars[this.liars.length -1].number;
	if(this.isOneLiared){
		if(isStraight(this.player0Dices, false)){
			if(isStraight(this.player1Dices, false))
				this.winner = 1 - player;
			else{
				for(var x in this.player1Dices){
					if(x == point)
						pointCount++;
				}
				if(pointCount == 5)
					pointCount = 7;
				if(pointCount >= liarCount)
					this.winner = 1 - player;
				else
					this.winner = player;
			}
		}else{
			if(isStraight(this.player1Dices, false)){
				for(var x in this.player0Dices){
					if(x == point)
						pointCount++;
				}
				if(pointCount == 5)
					pointCount = 7;
				if(pointCount >= liarCount)
					this.winner = 1 - player;
				else
					this.winner = player;
			}else{
				for(var x in this.player0dices){
					if(x == point)
						pointcount++;
				}
				if(pointcount == 5)
					pointcount = 7;
				var tmpcount = 0;
				for(var x in this.player1dices){
					if(x == point)
						tmpcount++;
				}
				if(tmpcount == 5)
					tmpcount = 7;
				pointcount += tmpcount;
				if(pointcount >= liarcount)
					this.winner = 1 - player;
				else
					this.winner = player;
			}
		}
	}else{
		if(isStraight(this.player0Dices, true)){
			if(isStraight(this.player1Dices, true))
				this.winner = 1 - player;
			else{
				var tmpCount1 = 0;
				var tmpCountPoint = 0;
				for(var x in this.player1Dices){
					if(x == point)
						tmpCountPoint++;
					else if (x == 1)
						tmpCount1 ++;
				}
				if(tmpCount1 == 5 || tmpCountPoint == 5)
					pointCount = 7;
				else if(tmpCountPoint + tmpCount1 == 5)
					pointCount = 6;
				else
					pointCount = tmpCount1 + tmpCountPoint;
				if(pointCount >= liarCount)
					this.winner = 1 - player;
				else
					this.winner = player;
			}
		}else{
			if(isStraight(this.player1Dices, false)){
				var tmpCount1 = 0;
				var tmpCountPoint = 0;
				for(var x in this.player0Dices){
					if(x == point)
						tmpCountPoint++;
					else if (x == 1)
						tmpCount1 ++;
				}
				if(tmpCount1 == 5 || tmpCountPoint == 5)
					pointCount = 7;
				else if(tmpCountPoint + tmpCount1 == 5)
					pointCount = 6;
				else
					pointCount = tmpCount1 + tmpCountPoint;
				if(pointCount >= liarCount)
					this.winner = 1 - player;
				else
					this.winner = player;
			}else{
				var tmpCount1 = 0;
				var tmpCountPoint = 0;
				for(var x in this.player0Dices){
					if(x == point)
						tmpCountPoint++;
					else if (x == 1)
						tmpCount1 ++;
				}
				if(tmpCount1 == 5 || tmpCountPoint == 5)
					pointCount = 7;
				else if(tmpCountPoint + tmpCount1 == 5)
					pointCount = 6;
				else
					pointCount = tmpCount1 + tmpCountPoint;
				tmpCount1 = 0;
				tmpCountPoint = 0;
				for(var x in this.player1Dices){
					if(x == point)
						tmpCountPoint++;
					else if (x == 1)
						tmpCount1 ++;
				}
				if(tmpCount1 == 5 || tmpCountPoint == 5)
					pointCount += 7;
				else if(tmpCountPoint + tmpCount1 == 5)
					pointCount += 6;
				else
					pointCount += (tmpCount1 + tmpCountPoint);
				if(pointCount >= liarCount)
					this.winner = 1 - player;
				else
					this.winner = player;
			}
		}
	}

    this.first = 1 - this.winner;
	this.gameInfo.state = GAME_SHOWED_STATE;
    this.gameInfo.winner = this.winner;
	if(this.winner === 0)
		userDao.updateLiarDiceGameofUser(this.player0id, 1, 1, function(err, data){
			if(!!err)
                throw err;
		});
	else
		userDao.updateLiarDiceGameofUser(this.player1id, 1, 1, function(err, data){
			if(!!err)
                throw err;
		});
    this.room.send(SENDTO_BOTH, this.gameInfo);
}

LiarDiceGame.prototype.endgame = function(){
	this.endTime = new Date();

	liarDiceGameDao.addGame(this.gameInfo);

    this.room.close();
};

LiarDiceGame.prototype.reentry = function(player){
    ;
}

module.exports = LiarDiceGame;
