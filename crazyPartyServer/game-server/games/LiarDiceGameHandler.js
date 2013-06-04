var application = require('../application');

var joinLobby = function(ws, userid, cb){
};

var cancelJoinLobby = function(userid, cb){
};

var LiarDiceGameHandler = module.exports = {};

LiarDiceGameHandler.handle = function(ws, content){
    var lobbyCallback = function(err, data){
        if(!!err){
            binlog.log('error: ' + err.code + ', ' + err.msg);
            ws.send('服务器错误,请稍后再试.', function(err1){
                if(!!err1){
                    binlog.log('error: ' + err1.number + ', ' + err1.message);
                }
                ws.close();
            });
        }
        else{
            ws.send(JSON.stringify(data), function(err1){
                if(!!err1){
                    binlog.log('error: ' + err1.number + ', ' + err1.message);
                    ws.close();
                }
            });
        }
    }

    switch(content.code){
        /**join game**/
        case 100:{
            application.liarDiceGameLobby.addPlayer(content.userid, ws, lobbyCallback);
            break;
        }
        /**cancel join game**/
        case 101:{
            application.liarDiceGameLobby.removePlayer(content.userid, lobbyCallback);
        }
        /**reentry**/
        case 800:{
            application.liarDiceGameLobby.reentryGame(content.userid, ws, lobbyCallback);
        }
        /**default**/
        default:{
            break;
        }
    }

    if(content.code >= 200 && content.code < 300){
        var room = application.liarDiceGameLobby.rooms[content.userid];
        if(!room){
            binlog.log('unexcepted error occured, the room of user ' + userid + ' not exists.');
            return;
        }
        var game = room.game;
        var callback = function(err, data, sendto){
            if(!!err){
            }else{
                if(sendto === 1){
                    room.websocket1.send(data);
                }
                else if(sendto === 2){
                    room.websocket2.send(data);
                }
                else if(sendto === 3){
                    room.websocket1.send(data);
                    room.websocket2.send(data);
                }
            }
        };

        switch(content.code - 200){
        /**start**/
        case 0:{
            game.start(cb);
            break;
        }
        /**prepared**/
        case 1:{
            game.prepare(player, callback);
            break;
        }
        /**diced**/
        case 2:{
            game.diced(player, content.dices, callback);
            break;
        }
        /**liar**/
        case 3:{
            game.liar(player, content.liarState, callback);
            break;
        }
        /**show**/
        case 4:{
            game.showDices(player, callback);
            break;
        }
        default:{
            break;
        }
        }
    }
};
