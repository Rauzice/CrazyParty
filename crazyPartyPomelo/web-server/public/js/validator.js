var checkEmail = function(email){
	var emailReg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	return emailReg.test(email);
};

var checkUsername = function(username){
	var nameReg = /^\w+$/;
	return nameReg.test(username);
}

var checkPassword = function(pwd){
	if(pwd.length < 6 || pwd.length > 20)
		return false;
	return true;
}

exports.checkEmail = checkEmail;
exports.checkUsername = checkUsername;
exports.checkPassword = checkPassword;
