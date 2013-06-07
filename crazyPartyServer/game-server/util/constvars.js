var constvars = module.exports = {};

constvars.oneday = new Date("January 1, 2100 00:00:00");

constvars.random = function(number){
	return Math.floor((Math.random()*number));
};
