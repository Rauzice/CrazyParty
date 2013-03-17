/**
 *Module dependencies
 */

var util = require('util');

/**
 * Initialize a new 'LiarDiceGame' with the given 'opts'.
 *
 * @param {Object} opts
 * @api public
 */

var LiarDiceGame = function(opts) {
	this.id = opts.id;
	this.name = opts.name;
	this.player1Id = opts.player1Id;
	this.player2Id = opts.player2Id;
	this.winner = opts.winner;
	this.startTime = opts.startTime;
	this.endTime = opts.endTime;
};

/**
 * Expose 'Entity' constructor
 */

module.exports = LiarDiceGame;
