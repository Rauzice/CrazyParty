var express = require('express');
var secret = require('../shared/config/session').secret;
var userDao = require('./dao/userDao');
var app = express.createServer();
var mysql = require('./dao/mysql/mysql');
//var everyauth = require('./lib/oauth');
var validator = require('./public/js/validator');
var binCommon = require('./public/js/binCommon');
var url = require('url');
var querystring = require('querystring');

var publicPath = __dirname + '/public';

app.configure(function(){
  app.use(express.methodOverride());
  app.use(express.bodyParser());
  app.use(express.cookieParser());
  app.use(express.session({secret: "keyboard cat"}));
  app.use(app.router);
  app.set('view engine', 'jade');
  app.set('views', __dirname + '/public');
  app.set('view options', {layout: false});
  app.set('basepath', publicPath);
});

app.configure('development', function(){
  app.use(express.static(publicPath));
  app.use(express.errorHandler({ dumpExceptions: true, showStack: true }));
});

app.configure('production', function(){
  var oneYear = 31557600000;
  app.use(express.static(publicPath, { maxAge: oneYear }));
  app.use(express.errorHandler());
});

app.get('/login', function(req, res){
	var postData = url.parse(req.url, true).query;
	console.log(postData);
	var username = "";
	var pwd = "";
	if(!!postData.username)
		username = postData.username.toLowerCase();
	if(!!postData.password)
		pwd = postData.password.toLowerCase();
	pwd = binCommon.MD5(pwd);
	
	res.contentType('json');
	
	if(!username || !pwd){
		var msg = '用户名或密码为空.';
		res.send(JSON.stringify({code: 100, content:{message: msg}}));
		return ;
	}
	userDao.getUserInfo(username, pwd, function(err, user){
		if(!!err){
			var msg = err;
			console.log(msg);
			res.send(JSON.stringify({code: 500, content:{message: msg}}));
			return ;
		}
		if(!user){
			var msg = '登陆时出现异常错误.';
			console.log(msg);
			res.send(JSON.stringify({code: 501, content:{message: msg}}));
			return ;
		}
		
		var msg = username + '登陆成功.';
		console.log(msg);
		res.send(JSON.stringify({code: 200, content: {message:msg, userid:user.userid, user:user.userInfo}}));
	});
});

app.post('/login', function(req, res){
	var postData = req.body;
	//var paras = querystring.parse(postData);
	
	var username = "";
	var password = "";
	if(!!postData.username)
		username = postData.username.toLowerCase();
	if(!!postData.password)
		password = postData.password.toLowerCase();
	
	res.contentType('json');
	
	if(!username || !password){
		var msg = '用户名或密码为空.';
		res.send(JSON.stringify({code: 100, content:{message: msg}}));
		return ;
	}
	
	userDao.getUserInfo(username, password, function(err, user){
		if(!!err){
			var msg = err.msg;
			console.log(msg);
			res.send(JSON.stringify({code: 500, content:{message: msg}}));
			return ;
		}
		if(!user){
			var msg = '获取用户信息时发生异常错误.';
			console.log(msg);
			res.send(JSON.stringify({code: 501, content:{message: msg}}));
			return ;
		}
		
		var msg = username + '登陆成功.';
		console.log(msg);
		res.send(JSON.stringify({code: 200, content:{message:msg, userid: user.userid, user: user.userInfo}}));
	});
});

app.post('/register', function(req, res){
	var postData = req.body;
	var username = "";
	var password = "";
	if(!!postData.username)
		username = postData.username.toLowerCase();
	if(!!postData.password)
		password = postData.password.toLowerCase();
	
	res.contentType('json');
	
	if(!validator.checkUsername(username)){
		var msg = '用户名格式不正确.'
		console.log(msg); 
		res.send(JSON.stringify({code: 500, content:{message:msg}}));
		return;
	}
	userDao.createUser(username, password, function(err, userid){
		if(!!err){
			var msg = '创建用户失败.' + ' 原因为' + err.msg;
			console.log(msg);
			res.send(JSON.stringify({code: 502, content:{message: msg}}));
			return;
		}
		if(!userid)
		{
			var msg = '创建用户时发生异常错误.';
			res.send(JSON.stringify({code: 502, content:{message: msg}}));
			return;
		}
		var msg = '创建用户成功.';
		console.log(msg);
		res.send(JSON.stringify({code: 200, content:{message: msg}}));
		return;
	});
});

mysql.init();

console.log("Web server has started.\nPlease log on http://127.0.0.1:3001/index.html");

app.listen(3009);
