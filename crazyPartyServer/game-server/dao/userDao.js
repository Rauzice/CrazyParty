var mysql = require('./mysql/mysql');
var userDao = module.exports;

/**
 * Get userInfo by userid
 * @param {unsigned int} userid
 * @param {funciton} cb(err, user) Call Back Function
*/
userDao.getUserInfoById = function(userid, cb){
	var sql = 'select * from User where id = ?';
	var args = [userid];
	mysql.query(sql, args, function(err, res){
		if(!!err){
			cb({code:err.code, msg:'服务器错误.'}, null);
		}
		else{
			if(!!res && res.length === 1){
				var rs = res[0];
				var user = {
						Username: rs.name, 
						Email: rs.email, From: rs.from, 
						State: rs.state, LoginCount: rs.loginCount, 
						LastLoginTime: rs.lastLoginTime, Moment: rs.moment,
						Birthday: rs.birthday, Gender: rs.gender, 
						Avatar: rs.avatar, 
						LiarDiceTotalCount: rs.LiarDiceTotalCount, LiarDiceWinCount: rs.LiarDiceWinCount, 
						DicingTotalCount: rs.DicingTotalCount, DicingWinCount: rs.DicingWinCount, 
						};
				cb(null, {userid: rs.id, userInfo: user});
			}
			else{
				cb({code: 10001, msg:'用户名或密码不正确.'}, null);
			}
		}
	});
};
/**
 * Get userInfo by username and password
 * @param {String} username
 * @param {string} password
 * @param {funciton} cb(err, user) Call Back Function
*/
userDao.getUserInfo = function(username, password, cb){
	var sql = 'select * from User where name = ? and password = ?';
	var args = [username, password];
	mysql.query(sql, args, function(err, res){
		if(!!err){
			cb({code:err.code, msg:'服务器错误.'}, null);
		}
		else{
			if(!!res && res.length === 1){
				var rs = res[0];
				var user = {
						Username: rs.name, 
						Email: rs.email, From: rs.from, 
						State: rs.state, LoginCount: rs.loginCount, 
						LastLoginTime: rs.lastLoginTime, Moment: rs.moment,
						Birthday: rs.birthday, Gender: rs.gender, 
						Avatar: rs.avatar, 
						LiarDiceTotalCount: rs.LiarDiceTotalCount, LiarDiceWinCount: rs.LiarDiceWinCount, 
						DicingTotalCount: rs.DicingTotalCount, DicingWinCount: rs.DicingWinCount, 
						};
				cb(null, {userid: rs.id, userInfo: user});
			}
			else{
				cb({code: 10001, msg:'用户名或密码不正确.'}, null);
			}
		}
	});
};

/**
 * update userInfo by userid
 * @param {unsigned int} userid
 * @param {funciton} cb(err, user) Call Back Function
*/
userDao.updateLiarDiceGameofUser = function(userid, winCount, totalCount, cb){
	var sql = 'select * from User where id = ?';
	var args = [userid];
	mysql.query(sql, args, function(err, res){
		if(!!err){
			cb({code: err.code, msg:'服务器错误.'}, null);
		}
		else{
			if(!!res && res.length === 1){
				var rs = res[0];
				var updatesql = 'update User set LiarDiceTotalCount = ?, LiarDiceWinCount = ? where id = ?';
				var updateArgs = [rs.LiarDiceTotalCount + totalCount, rs.LiarDiceWinCount + winCount, userid];
				mysql.query(updatesql, updateArgs, function(err, result){
					if(!!err)
						cb({code: err.code, msg: '服务器错误.'}, null);
					else
						cb(null, result);
				});
			}
		}
	});
};

/**
* Get userInfo by email
* @param {String} email
* @param {funciton} cb(err, userid) Call Back Function
*/
userDao.getUseridByEmail = function(email, cb){
	var sql = 'select * from User where email = ?';
	var args = [email];
	mysql.query(sql, args, function(err, res){
		if(err!=null){
			cb({code:err.code, msg:'服务器错误.'}, null);
		}
		else{
			if(!!res && res.length === 1){
				var rs = res[0];
				cb(null, rs.id);
			}
			else{
				cb({code:10001, msg:'用户不存在.'}, null);
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
 * @param {function} cb(err, userid) Call Back Function
 */
userDao.createUser = function(username, password, cb){
	var checkusernamesql = 'select * from User where name = ?';
	var checkusernameargs = [username];
	mysql.query(checkusernamesql, checkusernameargs, function(checkusernameerr, checkusernameres){
		if(!!checkusernameerr){
			cb({code: err.code, msg: "服务器错误."}, null);
		}else if(checkusernameres.length > 0){
			cb({code: 10002, msg: "用户名已存在."}, null);
		}else{
			var sql = 'insert into User (name, password, email, state, `from`, loginCount, lastLoginTime, birthday, gender, avatar, moment, LiarDiceTotalCount, LiarDiceWinCount, DicingTotalCount, DicingWinCount) ' +
			' value(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)';
			var loginTime = Date.now();
			//the first loginTime is the register time
			var args = [username, password, '', 0, '', 1, loginTime, '2010-01-01', 'male', '', '', 0, 0, 0, 0];
			mysql.insert(sql, args, function(err, res){
				if(err != null){
					cb({code: err.code, msg: "服务器错误."}, null);
				}
				else{
					var userId = res.insertId;
					cb(null, userId);
				}
			});
		}
	});
};
