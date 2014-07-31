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
	id BIGINT NOT NULL,
	name VARCHAR(50) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (id)
)
GO

/*Event: Tabla creation*/
CREATE TABLE Event(
	id BIGINT IDENTITY(1,1) NOT NULL,
	name VARCHAR(50) NOT NULL,
	date datetime2 NOT NULL,
	description VARCHAR(1000) NOT NULL,
	categoryId BIGINT NOT NULL,

	CONSTRAINT [PK_Event] PRIMARY KEY (id)
)
GO

/*Comment: Tabla creation*/
CREATE TABLE Comment(
	id BIGINT IDENTITY(1,1) NOT NULL,
	date datetime2 NOT NULL,
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

	CONSTRAINT [PK_UsersGroup] PRIMARY KEY (id),
	CONSTRAINT [UniqueKey_UsersGroup_Name] UNIQUE (name)
)
GO

/*Recommendation: Tabla creation*/
CREATE TABLE Recommendation(
	id BIGINT IDENTITY(1,1) NOT NULL,
	text VARCHAR(1000) NOT NULL,
	eventId BIGINT NOT NULL,
	usersGroupId BIGINT NOT NULL,
	date datetime2 NOT NULL,

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

	CONSTRAINT [PK_UserProfile_UsersGroup] PRIMARY KEY (userId,groupId)
)
GO

CREATE TABLE Tag(
	id BIGINT IDENTITY(1,1) NOT NULL,
	tagName VARCHAR(50) NOT NULL,

	CONSTRAINT [PK_Tag] PRIMARY KEY (id),
	CONSTRAINT [ROW_UNIQUE_TAG] UNIQUE (tagName)
)
GO


CREATE TABLE CommentTag(
	tagId BIGINT NOT NULL,
	commentId BIGINT NOt NULL,

	CONSTRAINT [PK_CommentTag] PRIMARY KEY (tagId, commentId)
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

ALTER TABLE CommentTag ADD CONSTRAINT [FK_Tag_CommentTag] FOREIGN KEY (tagId) REFERENCES Tag(id)
GO

ALTER TABLE CommentTag ADD CONSTRAINT [FK_Comment_CommentTag] FOREIGN KEY (commentId) REFERENCES Comment(id)
GO


INSERT INTO Category (id,name) VALUES (0,' '),(1,'Futbol'),(2,'Baloncesto'),(3,'Tenis'),(4,'Balonmano'),(5,'Petanca'),(6,'Volleyball'),(7,'Golf'),(8,'Lucha en el barro')
GO

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Final de petanca Gilberto-Tomás',CONVERT(datetime2,'18-10-13 10:34:09 PM',5),
				'Final del torneo Asserto de petanca. Lugar: Camponaraya .Entradas 3€',5)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Champions-Semifinal',CONVERT(datetime2,'18-09-16 10:05:45 PM',5),
				'Semifinal de la champions Levante - Betis. Lugar: Benito Villamarin',1)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Partido de liga',CONVERT(datetime2,'25-06-14 01:02:09 PM',5),
				'Jornada 21. Lakers - Boston. Lugar: Stapless Center',2)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Wimbledon',CONVERT(datetime2,'25-01-14 11:10:09 PM',5),
				'Vanesa Furlanetto - Maria Sharapova. Lugar: Yan Wanq',3)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Partido liga Asobal',CONVERT(datetime2,'30-03-13 11:40:16 PM',5),
				'Ademar León - Barcelona. Lugar: Barcelona',4)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Partido de Extraliga',CONVERT(datetime2,'09-06-14 08:36:25 PM',5),
				'SPPCZ Brno - FATRA Zlim. Lugar: Brno ',6)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Partido  Liga de Campeones',CONVERT(datetime2,'02-08-14 05:34:45 PM',5),
				'Lokomotiv Novosibirsk - Lube Banca Marche Macerata.  Lugar: Rusia',6)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Partido Northwestern Mutual World Challenge',CONVERT(datetime2,'07-08-14 10:25:09 PM',5),
				'Tigers Woods quiere revalidar su titulo. Entradas 10€',7)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Enfrentamiento de Sollheim Cup',CONVERT(datetime2,'26-02-14 09:34:12 PM',5),
				'Anja Rubik - Bridget Hall. Lugar: FIC. Entrada: 20€',8)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Enfrentamiento de Sollheim Cup',CONVERT(datetime2,'13-06-12 10:25:09 PM',5),
				'Katie Price - Abigail Clancy. Lugar: FIC. Entrada: 19€',8)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Copa del Rey',CONVERT(datetime2,'18-09-12 10:56:09 PM',5),
				'Ponferradina - Deportivo . Lugar: Toralin.',1)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Liga Calcio',CONVERT(datetime2,'18-06-15 06:12:09 PM',5),
				'Lazio - Napoli. Lugar: Lazio Stadium',1)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Champions',CONVERT(datetime2,'01-06-13 02:34:15 PM',5),
				'Bayern - Deportivo. Lugar: Riazor',1)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Baloncesto',CONVERT(datetime2,'02-11-14 09:25:09 PM',5),
				'EA7 Emporio Armani Milano - Real Madrid. Lugar: Vistalegre',2)

INSERT INTO Event (name, date, description, categoryId) 
		VALUES ('Final de petanca World Cup',CONVERT(datetime2,'21-09-13 09:25:09 PM',5),
				'Evaristo - Elias. Lugar: Playa Riazor',5)

GO














