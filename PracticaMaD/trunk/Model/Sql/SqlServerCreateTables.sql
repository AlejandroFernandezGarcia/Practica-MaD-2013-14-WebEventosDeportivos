USE [master]
GO

/*Drop database if already exists*/
IF EXISTS (SELECT name FROM sys.databases WHERE name='PracticaMaD')
DROP DATABASE [PracticaMad]
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
IF EXISTS (SELECT * FROM sys.objects WHERE object.id = OJECT_ID('[Category]') AND type in ('U'))
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
IF EXISTS (SELECT * FROM sys.objects WHERE object.id = OJECT_ID('[Event]') AND type in ('U'))
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

	CONSTRAINT [PK_Event] PRIMARY KEY (id),
	CONSTRAINT [FK_Category_Event] FOREIGN KEY (categoryId) REFERENCES Category (id),
	CONSTRAINT [FK_Comment_Event] FOREIGN KEY (commentId) REFERENCES Comment (id)
)
GO

/*Drop table Comment*/
IF EXISTS (SELECT * FROM sys.objects WHERE object.id = OJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
GO


/*Comment: Tabla creation*/
CREATE TABLE Comment(
	id INTEGER NOT NULL,
	date TIMESTAMP NOT NULL,
	text VARCHAR(1000) NOT NULL,
	eventId INTEGER NOT NULL,
	userProfileId INTEGER NOT NULL,

	CONSTRAINT [PK_Comment] PRIMARY KEY (id),
	CONSTRAINT [FK_Event_Comment] FOREIGN KEY (eventId) REFERENCES Event (id),
	CONSTRAINT [FK_UserProfile_Comment] FOREIGN KEY (userProfileId) REFERENCES UserProfileId (id)
)
GO


/*Drop table UsersGroup*/
IF EXISTS (SELECT * FROM sys.objects WHERE object.id = OJECT_ID('[UsersGroup]') AND type in ('U'))
DROP TABLE [UsersGroup]
GO

/*UsersGroup: Tabla creation*/
CREATE TABLE UsersGroup(
	id INTEGER NOT NULL,
	name VARCHAR(50) NOT NULL,
	description VARCHAR(1000) NOT NULL,
	userId INTEGER NOT NULL, /*Puede ser que haya grupo sin usuarios?*/
	recommendationId INTEGER,

	CONSTRAINT [PK_UsersGroup] PRIMARY KEY (id),
	CONSTRAINT [FK_UserProfile_UsersGroup] FOREIGN KEY (userId) REFERENCES UserProfile (id),
	CONSTRAINT [FK_Recommendation_UsersGroup] FOREIGN KEY (recommendationId) REFERENCES Recommendation (id)
)
GO

/*Drop table Recommendation*/
IF EXISTS (SELECT * FROM sys.objects WHERE object.id = OJECT_ID('[Recommendation]') AND type in ('U'))
DROP TABLE Recommendation
GO

/*Recommendation: Tabla creation*/
CREATE TABLE Recommendation(
	id INTEGER NOT NULL,
	text VARCHAR(1000) NOT NULL,
	eventId INTEGER NOT NULL,
	usersGroupId INTEGER NOT NULL,

	CONSTRAINT [PK_Recommendation] PRIMARY KEY (id),
	CONSTRAINT [FK_Event_Recommendation] FOREIGN KEY (eventId) REFERENCES Event (id),
	CONSTRAINT [FK_UsersGroup_Recommendation] FOREIGN KEY (usersGroupId) REFERENCES UsersGroup (id)
)
GO

/*Drop table UserProfile*/
IF EXISTS (SELECT * FROM sys.objects WHERE object.id = OJECT_ID('[UserProfile]') AND type in ('U'))
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

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (loginName),
	CONSTRAINT [FK_UsersGroup_UserProfile] FOREIGN KEY (usersGroupId) REFERENCES UsersGroup (id)
)
GO




















