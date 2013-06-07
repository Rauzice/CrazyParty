var express = require('express');
var secret = require('../shared/config/session').secret;
var userDao = require('./dao/userDao');
var app = express.createServer();
var lobbies = require('./application');
var mysql = require('./dao/mysql/mysql');
var url = require('url');
var binlog = require('./util/binlog');
var querystring = require('querystring');

var publicPath = __dirname + '/public';

app.lobbies = lobbies;
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

app.get('/gameinfo/liardice', function(req, res){
	res.contentType('json');
    res.send(JSON.stringify({code: 200, content: {playing: 10, waiting: 0}}));
});

var WebSocketServer = require('ws').Server;
var wsServer = new WebSocketServer({'port' : 3456});
var wsh = require('./wsHandler');
wsh.lobbies = app.lobbies;

wsServer.on('connection', function(ws){
	//var wsh = new WsHandler();
	ws.on('message', function(message){
		wsh.message(this, message,{});
	});

	ws.on('close', function(code, message){
		wsh.close(this, code, message);
	});

	wsh.open(ws);
});

mysql.init();

binlog.log("game server has started.\nPlease log on http://127.0.0.1:3010/index.html");
binlog.log('game server on ws://127.0.0.1:3456 started.');

app.listen(3010);
