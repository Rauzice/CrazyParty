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
	this.userId = opts.userId;
	this.totalGameCount = opts.totalGameCount;
	this.winGameCount = opts.winGameCount;
};

/**
 * Expose 'Entity' constructor
 */

module.exports = DicingPlayer;
