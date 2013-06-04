var binlog = require('./util/binlog');
var liarDiceGame = require('./games/liarDiceGame');
var application = require('./application');
var liarDiceGameHandler = require('./games/LiarDiceGamehandler');

var WsHandler = module.exports = {};

WsHandler.open = function(ws){
	binlog.log('websocket opened successfully!');
};

WsHandler.close = function(ws, code, message){
    binlog.log('websocket closed with code ' + code.toString() + ' and message ' + message.toString());
};

WsHandler.message = function(ws, message, flags){
	//var msg =  'Websocket said ' + data + ' too';
	//ws.send(JSON.stringify({code: 100, content:{message: msg}}));
	binlog.log(message);
    var data = JSON.parse(message.toString());

	switch (data.gamename){
	case 100 :{
		joinLobby(ws, data.content);
		break;
	}
	case 101 :{
		cancelJoinQueue(ws, data.content);
		break;
	}
	case 200: {
		gameMessage(data.content);
	}
	case 500 :{
		exitGame(data.content);
		break;
	}
	}
};

WsHandler.error = function(ws, error){
	binlog.log('unexcepted websocket error occured with error: ' + error);
};

