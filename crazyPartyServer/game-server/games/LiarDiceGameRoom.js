var LiarDiceGame = require('./LiarDiceGame');

var SENDTO_NOONE = 0;
var SENDTO_PLAYER0 = 1;
var SENDTO_PLAYER1 = 2;
var SENDTO_BOTH = 3;

var LiarDiceGameRoom = function(user1id, user2id, websocket1, websocket2, lobby){
	this.user1id = user1id;
	this.user2id = user2id;
	this.websocket1 = websocket1;
	this.websocket2 = websocket2;
	this.id = 0;
    this.lobby = lobby;
};

LiarDiceGameRoom.prototype.reentry = function(userid, websocket, cb){
    if(userid === this.user1id){
        this.websocket1 = websocket;
        this.game.reentry(1);
    }else{
        this.websocket0 = websocket;
        this.game.reentry(0);
    }
};

LiarDiceGameRoom.prototype.open = function(cb){
	this.game = new LiarDiceGame(this.user1id, this.user2id, this);
    if(this.game !== null){
        this.game.start();
    }
};

LiarDiceGameRoom.prototype.close = function(cb){
    this.lobby.removeRoom(this);
};

LiarDiceGameRoom.prototype.send = function(sendto, data){
    if(sendto === SENDTO_NOONE){
        ;
    }else if(sendto === SENDTO_PLAYER0){
        this.websocket0.send(data);
    }else if(sendto === SENDTO_PLAYER1){
        this.websocket1.send(data);
    }else if(sendto === SENDTO_BOTH){
        this.websocket0.send(data);
        this.websocket1.send(data);
    }
};

module.exports = LiarDiceGameRoom;
