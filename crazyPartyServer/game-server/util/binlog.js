var binlog = module.exports;

binlog.log = function(message){
	console.log(new Date().toLocaleString() + ' -- ' + message);
}