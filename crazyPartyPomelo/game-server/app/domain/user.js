/**
 *Module dependencies
 */

var util = require('util');

/**
 * Initialize a new 'User' with the given 'opts'.
 *
 * @param {Object} opts
 * @api public
 */

var User = function(opts) {
	this.id = opts.id;
	this.name = opts.name;
	this.from = opts.from || '';
	this.password = opts.password;
	this.loginCount = opts.loginCount;
	this.lastLoginTime = opts.lastLoginTime;
	this.email = opts.email;
	this.state = opts.state;
	this.moment = opts.state;
};

/**
 * Expose 'Entity' constructor
 */

module.exports = User;
