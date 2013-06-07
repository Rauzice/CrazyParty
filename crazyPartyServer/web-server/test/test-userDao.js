var userDao = require('../dao/userDao'); 
var mysql = require('../dao/mysql/mysql'); 
mysql.init();

exports['getUserInfo'] = function(test) {
    test.ok(userDao.getUserInfo());
    test.done();
};
