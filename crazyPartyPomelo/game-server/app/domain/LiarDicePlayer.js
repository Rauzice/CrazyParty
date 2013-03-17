/**
 *Module dependencies
 */

var util = require('util');

/**
 * Initialize a new 'LiarDicePlayer' with the given 'opts'.
 *
 * @param {Object} opts
 * @api public
 */

var LiarDicePlayer = function(opts) {
	this.id = opts.id;
	this.userId = opts.userId;
	this.totalGameCount = opts.totalGameCount;
	this.winGameCount = opts.winGameCount;
};

/**
 * Expose 'Entity' constructor
 */

module.exports = LiarDicePlayer;
