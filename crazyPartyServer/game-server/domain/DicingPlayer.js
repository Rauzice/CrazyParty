/**
 *Module dependencies
 */

var util = require('util');

/**
 * Initialize a new 'DicingPlayer' with the given 'opts'.
 *
 * @param {Object} opts
 * @api public
 */

var DicingPlayer = function(opts) {
	this.id = opts.id;
	this.userid = opts.userid;
	this.dices = opts.dices;
	this.enterTime = opts.enterTime;
	this.leaveTime = opts.leaveTime;
};

/**
 * Expose 'Entity' constructor
 */

module.exports = DicingPlayer;
