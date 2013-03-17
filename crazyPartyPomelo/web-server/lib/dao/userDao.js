var mysql = require('./mysql/mysql');
var userDao = module.exports;

/**
 * Get userInfo by username
 * @param {String} username
 * @param {funciton} cb Call Back Function
*/
userDao.getUserByName = function(username, cb){
	var sql = 'select * from User where name = ?';
	var args = [username];
	mysql.query(sql, args, function(err, res){
		if(err!=null){
			cb(err.message, null);
		}
		else{
			if(!!res && res.length === 1){
				var rs = res[0];
				var user = {id: rs.id, name: rs.name, password: rs.password, 
						email: rs.email, from: rs.from, state: rs.state, 
						loginCount: rs.loginCount, lastLoginTime: rs.lastLoginTime
						};
				cb(null, user);
			}
			else{
				cb('user not exist ', null);
			}
		}
	});
};

/**
 * Get userInfo by username and password
 * @param {String} username
 * @param {string} password
 * @param {funciton} cb Call Back Function
*/
userDao.getUserInfo = function(username, password, cb){
	var sql = 'select * from User where name = ? and password = ?';
	var args = [username, password];
	mysql.query(sql, args, function(err, res){
		if(err!=null){
			cb(err.message, null);
		}
		else{
			if(!!res && res.length === 1){
				var rs = res[0];
				var user = {
						UserId: rs.id, Username: rs.name, 
						Email: rs.email, From: rs.from, 
						State: rs.state, LoginCount: rs.loginCount, 
						LastLoginTime: rs.lastLoginTime, Moment: rs.moment,
						Birthday: rs.birthday, Gender: rs.gender, 
						Avatar: rs.avatar, 
						LiarDiceTotalCount: rs.LiarDiceTotalCount, LiarDiceWinCount: rs.LiarDiceWinCount, 
						DicingTotalCount: rs.DicingTotalCount, DicingWinCount: rs.DicingWinCount, 
						};
				cb(null, user);
			}
			else{
				cb('username or password is incorrect.', null);
			}
		}
	});
};

/**
* Get userInfo by email
* @param {String} email
* @param {funciton} cb Call Back Function
*/
userDao.getUserByEmail = function(email, cb){
	var sql = 'select * from User where email = ?';
	var args = [email];
	mysql.query(sql, args, function(err, res){
		if(err!=null){
			cb(err.message, null);
		}
		else{
			if(!!res && res.length === 1){
				var rs = res[0];
				var user = {id: rs.id, name: rs.name, password: rs.password, 
						email: rs.email, from: rs.from, state: rs.state, 
						loginCount: rs.loginCount, lastLoginTime: rs.lastLoginTime
						};
				cb(null, user);
			}
			else{
				cb('user not exist ', null);
			}
		}
	});
};

/**
 * Create a  new user
 * @param {string} username
 * @param {string} password
 * @param {string} email
 * @param {string} from Register source
 * @param {function} cb Call Back Function
 */
userDao.createUser = function(username, password, cb){
	var sql = 'insert into User (name, password, email, state, `from`, loginCount, lastLoginTime, birthday, gender, avatar, moment, LiarDiceTotalCount, LiarDiceWinCount, DicingTotalCount, DicingWinCount) ' +
	' value(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)';
	var loginTime = Date.now();
	//the first loginTime is the register time
	var args = [username, password, '', 0, '', 1, loginTime, '2010-01-01', 'male', '', '', 0, 0, 0, 0];
	mysql.insert(sql, args, function(err, res){
		if(err != null){
			cb({code: err.number, msg: err.message}, null);
		}
		else{
			var userId = res.insertId;
			var user = {id: userId, name: username}; 
			cb(null, user);
		}
	});
};