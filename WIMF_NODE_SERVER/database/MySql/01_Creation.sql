--source 01_Creation.sql
-- Création de la base de données
SHOW DATABASES;
-- Si la base de données 'db_wimf' existe, on l'a supprime pour qu'elle soit totalement neuve.
DROP DATABASE IF EXISTS db_wimf;
-- Création de la base de données
CREATE DATABASE db_wimf;
-- Utilise cette base de données
USE db_wimf;
SHOW TABLES;

--Table Gps_utilisateur
CREATE TABLE Gps_utilisateur
(
  idU int,
  gps_lat DECIMAL(7,7),
  gps_long DECIMAL(7,7),
  dateCrea TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
  CONSTRAINT pk_Message PRIMARY KEY (idMsg),
  CONSTRAINT fk_MessageU1 FOREIGN KEY (idU1)
  REFERENCES Utilisateur(idU),
  CONSTRAINT fk_MessageU2 FOREIGN KEY (idU2)
  REFERENCES Utilisateur(idU)
)
;
DESC Message
;

--Table Utilisateur
CREATE TABLE Utilisateur
(
  idU int AUTO_INCREMENT,
  nom VARCHAR(50),
  tel VARCHAR(50),
  gps_lat DECIMAL(7,7),
  gps_long DECIMAL(7,7),
  password VARCHAR(50) DEFAULT 'password',
  dateCrea TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
  dateMaj TIMESTAMP,
  CONSTRAINT pk_Utilisateur PRIMARY KEY (idU),
  CONSTRAINT uc_UtilisateurTel UNIQUE (tel)
)
;
DESC Utilisateur
;

--Table Amis
CREATE TABLE Amis
(
  idU_snd int,
  idU_rcv int,
  etat int DEFAULT 0,
  dateCrea TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
  dateMaj TIMESTAMP,
  CONSTRAINT pk_Amis PRIMARY KEY (idU1,idU2),
  CONSTRAINT fk_AmisU1 FOREIGN KEY (idU1)
  REFERENCES Utilisateur(idU),
  CONSTRAINT fk_AmisU2 FOREIGN KEY (idU2)
  REFERENCES Utilisateur(idU)
)
;
DESC Amis
;

--Table Message
CREATE TABLE Message
(
  idMsg int AUTO_INCREMENT,
  valeur VARCHAR(256),
  idU_snd int,
  idU_rcv int,
  etat int DEFAULT 0,
  dateCrea TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
  dateMaj TIMESTAMP,
  CONSTRAINT pk_Message PRIMARY KEY (idMsg),
  CONSTRAINT fk_MessageU1 FOREIGN KEY (idU1)
  REFERENCES Utilisateur(idU),
  CONSTRAINT fk_MessageU2 FOREIGN KEY (idU2)
  REFERENCES Utilisateur(idU)
)
;
DESC Message
;
