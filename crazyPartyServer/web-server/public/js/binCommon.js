var binCommon = module.exports;

binCommon.queryStringParse = function(query) {
    var vars = query.split('&');
    var paras = [];
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split('=');
        paras[pair[0]] = pair[1];
    }
    return paras;
}

binCommon.MD5 = function(input){
    var hash = require('crypto').createHash('md5');
    return hash.update(input+"").digest('hex');
}