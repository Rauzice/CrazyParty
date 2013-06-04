var LiarDiceGame = require('../games/LiarDiceGame');
var mysql = require('../dao/mysql/mysql'); 
mysql.init();

function isFunction(object) {
    return typeof(object) == "function";
};

exports['constructor'] = function(test) {
    test.ok(isFunction(LiarDiceGame));

    test.throws(new LiarDiceGame(1, 2, null));
    var room = {id: 123}, game, user0id = 32209, user1id = 32210;
    game = new LiarDiceGame(user0id, user1id, room);
    test.equal(game.gameInfo.player0id, user0id);
    test.equal(game.gameInfo.player1id, user1id);
    test.equal(game.room, room);
    test.ok(game.first == 0 || game.first == 1);
    test.done();
};

exports['start'] = function(test) {
    test.ok(true);
    test.ok(true);
    test.ok(true);
    test.done();
};
exports['prepare'] = function(test) {
    test.ok(true);
    test.ok(true);
    test.ok(true);
    test.ok(true);
    test.ok(true);
    test.done();
};
exports['dicing'] = function(test) {
    test.ok(true);
    test.ok(true);
    test.ok(true);
    test.done();
};
exports['diced'] = function(test) {
    test.ok(true);
    test.ok(true);
    test.ok(true);
    test.done();
};
