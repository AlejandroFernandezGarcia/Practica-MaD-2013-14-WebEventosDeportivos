USE [master]
GO

/*Drop database if already exists*/
IF EXISTS (SELECT name FROM sys.databases WHERE name='PracticaMaD')
DROP DATABASE [PracticaMaD]
GO

USE [master]
GO

/*Database creation*/
CREATE DATABASE [PracticaMaD] ON PRIMARY
(NAME = 'PracticaMaD', FILENAME = 'C:\DBMaD\Database\PracticaMaD.mdf')
LOG ON
(NAME = 'PracticaMaD_log', FILENAME = 'C:\DBMaD\Database\PracticaMaD_log.ldf')
GO

/*Delete User if already exists*/
IF EXISTS (SELECT * FROM sys.server_principals WHERE name = 'user')
DROP LOGIN [user]
GO

/*Create LoginUser*/
CREATE LOGIN [user]
WITH PASSWORD='password',
		DEFAULT_DATABASE=[PracticaMaD],
		DEFAULT_LANGUAGE=[Español],
		CHECK_EXPIRATION=OFF,
		CHECK_POLICY=OFF
GO

/*Set user as database dbo*/
USE practicaMaD
GO

SP_CHANGEDBOWNER 'user'
GO

USE [PracticaMaD]
GO


/*Drop table Category*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
GO

/*Category: Tabla creation*/
CREATE TABLE Category(
	id INTEGER NOT NULL,
	name VARCHAR(50) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (id)
)
GO


/*Drop table Event*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Event]') AND type in ('U'))
DROP TABLE [Event]
GO

/*Event: Tabla creation*/
CREATE TABLE Event(
	id INTEGER NOT NULL,
	name VARCHAR(50) NOT NULL,
	date TIMESTAMP NOT NULL,
	description VARCHAR(1000) NOT NULL,
	categoryId INTEGER NOT NULL,
	commentId INTEGER,

	CONSTRAINT [PK_Event] PRIMARY KEY (id)
)
GO

/*Drop table Comment*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
GO


/*Comment: Tabla creation*/
CREATE TABLE Comment(
	id INTEGER NOT NULL,
	date TIMESTAMP NOT NULL,
	text VARCHAR(1000) NOT NULL,
	eventId INTEGER NOT NULL,
	userProfileId VARCHAR(30) NOT NULL,

	CONSTRAINT [PK_Comment] PRIMARY KEY (id)
)
GO


/*Drop table UsersGroup*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UsersGroup]') AND type in ('U'))
DROP TABLE [UsersGroup]
GO

/*UsersGroup: Tabla creation*/
CREATE TABLE UsersGroup(
	id INTEGER NOT NULL,
	name VARCHAR(50) NOT NULL,
	description VARCHAR(1000) NOT NULL,
	userId INTEGER NOT NULL, /*Puede ser que haya grupo sin usuarios?*/
	recommendationId INTEGER,

	CONSTRAINT [PK_UsersGroup] PRIMARY KEY (id)
)
GO

/*Drop table Recommendation*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Recommendation]') AND type in ('U'))
DROP TABLE Recommendation
GO

/*Recommendation: Tabla creation*/
CREATE TABLE Recommendation(
	id INTEGER NOT NULL,
	text VARCHAR(1000) NOT NULL,
	eventId INTEGER NOT NULL,
	usersGroupId INTEGER NOT NULL,

	CONSTRAINT [PK_Recommendation] PRIMARY KEY (id)
)
GO

/*Drop table UserProfile*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO

/*UserProfile: Tabla creation*/
CREATE TABLE UserProfile(
	loginName VARCHAR(30) NOT NULL,
	enPassword VARCHAR(50) NOT NULL,
	firstName VARCHAR(30) NOT NULL,
	surname VARCHAR(40) NOT NULL,
	email VARCHAR(60) NOT NULL,
	language VARCHAR(2) NOT NULL,
	country VARCHAR(2) NOT NULL,
	usersGroupId INTEGER ,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (loginName)
)
GO

/*Drop table UserProfile_UserGroups*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile_UserGroups]') AND type in ('U'))
DROP TABLE [UserProfile]
GO

/*UserProfile_UserGroups: Tabla creation*/
CREATE TABLE UserProfile_UserGroups(
	loginName VARCHAR(30) NOT NULL,
	groupId INTEGER NOT NULL,

	CONSTRAINT [PK_UserProfile_UserGroups] PRIMARY KEY (loginName, groupId)
)
GO



/*Add constraints for all the tables*/
ALTER TABLE Event ADD CONSTRAINT [FK_Category_Event] FOREIGN KEY (categoryId) REFERENCES Category (id)

ALTER TABLE Comment ADD	CONSTRAINT [FK_Event_Comment] FOREIGN KEY (eventId) REFERENCES Event (id)
ALTER TABLE Comment ADD	CONSTRAINT [FK_UserProfile_Comment] FOREIGN KEY (userProfileId) REFERENCES UserProfile (loginName)

ALTER TABLE Recommendation ADD CONSTRAINT [FK_Event_Recommendation] FOREIGN KEY (eventId) REFERENCES Event (id)
ALTER TABLE Recommendation ADD CONSTRAINT [FK_UsersGroup_Recommendation] FOREIGN KEY (usersGroupId) REFERENCES UsersGroup (id)

ALTER TABLE UserProfile ADD CONSTRAINT [FK_UsersGroup_UserProfile] FOREIGN KEY (usersGroupId) REFERENCES UsersGroup (id)

ALTER TABLE UserProfile_UserGroups ADD CONSTRAINT [FK_UserProfile_UserGroups_UserProfile] FOREIGN KEY (loginName) REFERENCES UserProfile (loginName)
ALTER TABLE UserProfile_UserGroups ADD CONSTRAINT [FK_UserProfile_UsersGroup_UsersGroup] FOREIGN KEY (groupId) REFERENCES UsersGroup (id)


















