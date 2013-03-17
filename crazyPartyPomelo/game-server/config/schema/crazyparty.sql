/*
Source Host: localhost
Source Database: crazyParty
date: 2013-03-12 18:03
*/


--  -----------------------  --
--   database for crazyparty --
--  -----------------------  ----
-- create database crazyParty --

--  -----------------------  --
--   table structure for User --
-- state, 0 as unchecked, 1 as checkedWith email --
-- gender should be 'male' or 'female' --
--  -----------------------  --
create table if not exists User(
  id bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  name varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  email varchar(50) COLLATE utf8_unicode_ci,
  state int unsigned default '0',
  `from` varchar(50) COLLATE utf8_unicode_ci DEFAULT	'',
  password varchar(50) COLLATE utf8_unicode_ci DEFAULT '',
  loginCount smallint(6) unsigned DEFAULT '0',
  lastLoginTime datetime,
  moment varchar(1023) COLLATE utf8_unicode_ci default '',
  birthday date not null default '2000-01-01',
  gender varchar(8) not null default 'male',
  avatar varchar(40) default '',
  liarDiceTotalCount int unsigned default '0',
  liarDiceWinCount int unsigned default '0',
  dicingTotalCount int unsigned default '0',
  dicingWinCount int unsigned default '0',
  PRIMARY KEY (id),
  UNIQUE KEY INDEX_ACCOUNT_NAME (name)
)ENGINE=InnoDB AUTO_INCREMENT=32209 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--  -----------------------  --
--   table structure for RegisterCode --
--  -----------------------  --
create table if not exists RegisterCode(
  id bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  userId bigint(20) unsigned NOT NULL,
  requestTime datetime not null,
  avaiableTime datetime not null,
  checkCode varchar(10) COLLATE utf8_unicode_ci not null,
  PRIMARY KEY (id),
  foreign key (userId) references User(id)
)ENGINE=InnoDB AUTO_INCREMENT=12307 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

create table if not exists ForgetPassword(
  id bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  userId bigint(20) unsigned NOT NULL,
  requestTime datetime not null,
  avaiableTime datetime not null,
  checkCode varchar(10) COLLATE utf8_unicode_ci not null,
  PRIMARY KEY (id),
  foreign key (userId) references User(id)
)ENGINE=InnoDB AUTO_INCREMENT=11101 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


--  -----------------------  --
--   table structure for  LiarDiceGame --
--  winner, 1 means player1 win and 2 player2 win --
--  state: 0 means waiting, 1 means started and Dicing, 2 means waiting player 1, 3 means waiting player2, 4 means ended --
--  -----------------------  --
create table if not exists LiarDiceGame(
	id bigint(20) unsigned not null AUTO_INCREMENT,
	state smallint(5) not null default '0',
	winner bigint(20) unsigned not null,
	startTime datetime not null,
	endTime datetime not null,
	primary key (id),
	foreign key (winner) references User(id)
)ENGINE=InnoDB AUTO_INCREMENT=10123 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


--  -----------------------  --
--   table structure for DicingGame --
--  state: 0 means waiting, 1 means started and Dicing, 4 means ended --
--  -----------------------  --
create table if not exists DicingGame(
	id bigint(20) unsigned not null AUTO_INCREMENT,
	state smallint(5) not null default '0',
	winner int unsigned,
	startTime datetime not null,
	endTime datetime not null,
	primary key (id)
)ENGINE=InnoDB AUTO_INCREMENT=20249 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


--  -----------------------  --
--   table structure for PlayLiarDice --
--  status: 0 means palyer1, 1 means palyer2 --
--  -----------------------  --
create table if not exists PlayLiarDice(
	id bigint(20) unsigned not null AUTO_INCREMENT,
	userId bigint(20) unsigned NOT NULL,
	gameId bigint(20) unsigned NOT NULL,
	dices char(10) not null default '00000',
	lastUpdateTime datetime not null,
	liarCount int not null default 0,
	liarPoint int not null default 0,
	status smallint not null,
	primary key (id),
	foreign key (userId) references User(id),
	foreign key (gameId) references LiarDice(id)
)ENGINE=InnoDB AUTO_INCREMENT=32209 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--  -----------------------  --
--   table structure for PlayDicing --
--  -----------------------  --
create table if not exists PlayDicing(
	id bigint(20) unsigned not null AUTO_INCREMENT,
	userId bigint(20) unsigned NOT NULL,
	gameId bigint(20) unsigned NOT NULL,
	dices char(3) not null default '000',
	lastUpdateTime datetime not null,
	status smallint not null,
	primary key (id),
	foreign key (userId) references User(id)
)ENGINE=InnoDB AUTO_INCREMENT=32232 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


