/**
 * rooms : {userid1: room1, userid2: room1, userid3: room2, userid4: room2}
 * roomidArray : an array of roomids,
 * waitingUseridArray: an array of userids in waiting queue,
 * websockets: {userid1: websocket1, userid2, websocket2};
 */
var LiarDiceGameRoom = require('./LiarDiceGameRoom');
var binlog = require('../util/binlog');


var MAX_ROOM_COUNT = 50;

var LiarDiceGameLobby = function(){
	this.rooms = {};
	this.roomidArray = [];

	this.waitingUseridArray = [];
	this.websockets = {};
};


LiarDiceGameLobby.prototype.newRoom = function(cb){
	if(this.roomidArray.length >= MAX_ROOM_COUNT){
		cb({code: 10001, msg: '房间数量已达上限.'});
		return;
	}
	else{
		if(this.waitingUseridArray.length < 2){
			cb({code: 10002, msg:'人数不足, 请耐心等待.'});
			return;
		}
		var user1id = this.waitingUseridArray.shift(),
		    user2id = this.waitingUseridArray.shift(),
		    room = new LiarDiceGameRoom(user1id, user2id, this.websockets[user1id], this.websockets[user2id]);
		if(this.roomidArray.length > 0)
			room.id = (this.roomidArray[this.roomidArray.length - 1].id + 1) % 2823432427 + 1;
		else
			room.id = 1;
		this.roomidArray.push(room.id);
		this.rooms[user1id] = room;
		this.rooms[user2id] = room;

		var self = this;
		room.open(function(err){
			if(!!err){
				binlog.log(err.msg);
			}
			binlog.log('new room is created, total room count is ' + self.roomidArray.length);
		});
	}
};


LiarDiceGameLobby.prototype.removeRoom = function(room){
    this.waitingUseridArray.push(room.user1id);
    this.waitingUseridArray.push(room.user2id);

    this.rooms.slice(room, 1);
};

/**
 * get the userid of an websocket
 * @param {websocket} websocket
 * @param {function} cb(err, userid)
 */
LiarDiceGameLobby.prototype.getUseridByWebsocket = function(websocket, cb){
	for(var x in this.websockets){
		if(this.websockets[x] === websocket)
			cb(null, x);
	}
	cb({code: 10001, msg:'没有找到相应ws.'}, null);
};

LiarDiceGameLobby.prototype.addPlayer = function(userid, websocket){
	this.waitingUseridArray.push(userid);
	this.websockets[userid] = websocket;

	binlog.log('user ' + userid + ' joined Liar Dice Game.');

	this.newRoom(function(err){
        binlog.log(err.msg);
		;
	});
};

LiarDiceGameLobby.prototype.removePlayer = function(userid){
	for(var i=0; i<this.waitingUseridArray.length; i++){
		if(this.waitingUseridArray[i] == userid)
			this.waitingUseridArray.splice(i, 1);
	}

	delete this.websockets[userid];
	binlog.log('user ' + userid + ' left Liar Dice Game.');
};

/**
 * a player go back to this game after its dropping down.
 * @param {unsigned int} userid
 * @param {ws.WebSocket} websocket
 * @param {function} cb(err, data)
 */
LiarDiceGameLobby.prototype.reentryGame = function(userid, websocket, cb){
    this.websockets[userid] = websocket;
    if(userid == this.rooms[userid].user1id){
        this.rooms[userid].websocket1 = websocket;
    }else{
        this.rooms[userid].websocket2 = websocket;
    }
};

module.exports = LiarDiceGameLobby;
