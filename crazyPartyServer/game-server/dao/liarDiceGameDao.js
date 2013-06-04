var mysql = require('./mysql/mysql');

var liarDiceGameDao = module.exports;

/**
 * Get LiarDiceGame data by id
 * @param {unsigned int} id
 * @param {function} cb(err, LiarDiceGame)
 */
liarDiceGameDao.getLiarDiceGameById = function(id, cb){
	var sql = 'select * from LiarDiceGame where id = ?';
	var args = [id];
	mysql.query(sql, args, function(err, results){
		if(!!err)
			cb({code: err.code, msg:'服务器错误.'}, null);
		else if(!results)
			cb({code: 10002, msg:'未能找到相符结果.'}, null);
		else
			cb(null, results[0]);
	});
}

/**
 * Get LiarDiceGames data by playerid
 * @param {unsigned int} playerid
 * @param {function} cb(err, LiarDiceGame)
 */
liarDiceGameDao.getLiarDiceGamesByPlayerid = function(playerid, cb){
	var sql = 'select * from LiarDiceGame where player1id = ? or player2id = ? order by endTime DESC';
	var args = [palyerid, playerid];
	mysql.query(sql, args, function(err, results){
		if(!!err)
			cb({code: err.code, msg:'服务器错误.'}, null);
		else if(!results)
			cb({code: 10002, msg:'未能找到相符结果.'}, null);
		else
			cb(null, results);
	});
}

/**
 * Add a new LiarDiceGame two playerid
 * @param {unsigned int} userid
 * @param {function} cb(err, liarDiceGame)
 */
liarDiceGameDao.addGame = function(game, cb){
	var sql = 'insert into LiarDiceGame (playeri1id, player2id, first, winner, startTime, endTime) values(?, ?, ?, ?, ?, ?)';
	var args = [game.player1id, game.player2id, game.first, game.winner, game.startTime, game.endTime];
	
	mysql.insert(sql, args, function(err, result){
		if(!!err){
			cb({code: err.code, msg:'服务器错误.'}, null);
		}else{
			cb(null, result);
		}
	});
};