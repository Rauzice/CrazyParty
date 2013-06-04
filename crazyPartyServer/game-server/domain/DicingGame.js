/**
 *Module dependencies
 */

var util = require('util');

/**
 * Initialize a new 'DicingGame' with the given 'opts'.
 *
 * @param {Object} opts
 * @api public
 */

var DicingGame = function(opts) {
	this.id = opts.id;
	this.player1id = opts.player1id;
	this.player2id = opts.player2id;
	this.state = opts.state;
	this.winnerid = opts.winnerid;
	this.startTime = opts.startTime;
	this.endTime = opts.endTime;
};

/**
 * Expose 'Entity' constructor
 */

module.exports = DicingGame;
