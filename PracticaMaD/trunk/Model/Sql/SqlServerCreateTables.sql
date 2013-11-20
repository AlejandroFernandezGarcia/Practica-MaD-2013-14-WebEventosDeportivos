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

/*Create LoginUser*/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'user')
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


/*Drop table Category*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
GO

/*Drop table Event*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Event]') AND type in ('U'))
DROP TABLE [Event]
GO

/*Drop table Comment*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
GO

/*Drop table UsersGroup*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UsersGroup]') AND type in ('U'))
DROP TABLE [UsersGroup]
GO

/*Drop table Recommendation*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Recommendation]') AND type in ('U'))
DROP TABLE [Recommendation]
GO

/*Drop table UserProfile*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO

/*Drop table UserProfile_UserGroups*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfileUserGroups]') AND type in ('U'))
DROP TABLE [UserProfileUserGroups]
GO


/*Category: Tabla creation*/
CREATE TABLE Category(
	id BIGINT IDENTITY(1,1) NOT NULL,
	name VARCHAR(50) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (id)
)
GO

/*Event: Tabla creation*/
CREATE TABLE Event(
	id BIGINT IDENTITY(1,1) NOT NULL,
	name VARCHAR(50) NOT NULL,
	date TIMESTAMP NOT NULL,
	description VARCHAR(1000) NOT NULL,
	categoryId BIGINT NOT NULL,

	CONSTRAINT [PK_Event] PRIMARY KEY (id)
)
GO

/*Comment: Tabla creation*/
CREATE TABLE Comment(
	id BIGINT IDENTITY(1,1) NOT NULL,
	date TIMESTAMP NOT NULL,
	text VARCHAR(1000) NOT NULL,
	eventId BIGINT NOT NULL,
	userProfileId BIGINT NOT NULL,

	CONSTRAINT [PK_Comment] PRIMARY KEY (id)
)
GO

/*UsersGroup: Tabla creation*/
CREATE TABLE UsersGroup(
	id BIGINT IDENTITY(1,1) NOT NULL,
	name VARCHAR(50) NOT NULL,
	description VARCHAR(1000) NOT NULL,

	CONSTRAINT [PK_UsersGroup] PRIMARY KEY (id)
)
GO

/*Recommendation: Tabla creation*/
CREATE TABLE Recommendation(
	id BIGINT IDENTITY(1,1) NOT NULL,
	text VARCHAR(1000) NOT NULL,
	eventId BIGINT NOT NULL,
	usersGroupId BIGINT NOT NULL,

	CONSTRAINT [PK_Recommendation] PRIMARY KEY (id)
)
GO

/*UserProfile: Tabla creation*/
CREATE TABLE UserProfile(
	id BIGINT IDENTITY(1,1) NOT NULL,
	loginName VARCHAR(30) NOT NULL,
	enPassword VARCHAR(50) NOT NULL,
	firstName VARCHAR(30) NOT NULL,
	surname VARCHAR(40) NOT NULL,
	email VARCHAR(60) NOT NULL,
	language VARCHAR(2) NOT NULL,
	country VARCHAR(2) NOT NULL,
	
	CONSTRAINT [PK_UserProfile] PRIMARY KEY (id),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)
GO

/*UserProfile_UserGroups: Tabla creation*/
CREATE TABLE UserProfileUsersGroup(
	userId BIGINT NOT NULL,
	groupId BIGINT NOT NULL,

	CONSTRAINT [PK_UserProfile_UsersGroup] PRIMARY KEY (userId,groupId),
	CONSTRAINT [ROW_UNIQUE] UNIQUE (userId, groupId)
)
GO

/*Add constraints for all the tables*/
ALTER TABLE Event ADD CONSTRAINT [FK_Category_Event] FOREIGN KEY (categoryId) REFERENCES Category (id)
GO

ALTER TABLE Comment ADD	CONSTRAINT [FK_Event_Comment] FOREIGN KEY (eventId) REFERENCES Event (id)
GO
ALTER TABLE Comment ADD	CONSTRAINT [FK_UserProfile_Comment] FOREIGN KEY (userProfileId) REFERENCES UserProfile (id)
GO

ALTER TABLE Recommendation ADD CONSTRAINT [FK_Event_Recommendation] FOREIGN KEY (eventId) REFERENCES Event (id)

GO
ALTER TABLE Recommendation ADD CONSTRAINT [FK_UsersGroup_Recommendation] FOREIGN KEY (usersGroupId) REFERENCES UsersGroup (id)
GO


ALTER TABLE UserProfileUsersGroup ADD CONSTRAINT [FK_UserProfile_UsersGroup_UserProfile] FOREIGN KEY (userId) REFERENCES UserProfile (id)
GO

ALTER TABLE UserProfileUsersGroup ADD CONSTRAINT [FK_UserProfile_UsersGroup_UsersGroup] FOREIGN KEY (groupId) REFERENCES UsersGroup (id)
GO



















