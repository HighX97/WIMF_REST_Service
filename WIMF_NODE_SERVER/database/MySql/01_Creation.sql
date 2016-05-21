SHOW DATABASES;
DROP DATABASE IF EXISTS db_wimf;
CREATE DATABASE db_wimf;
USE db_wimf;
SHOW TABLES;




CREATE TABLE Utilisateur
(
  idU int AUTO_INCREMENT,
  nom VARCHAR(50),
  tel VARCHAR(50),
  gps_lat DECIMAL(11,8),
  gps_long DECIMAL(11,8),
  password VARCHAR(50) DEFAULT 'password',
  datetimeCrea TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
  datetimeMaj TIMESTAMP,
  CONSTRAINT pk_Utilisateur PRIMARY KEY (idU),
  CONSTRAINT uc_UtilisateurTel UNIQUE (tel)
)
;

DESC Utilisateur
;


CREATE TABLE Gps_utilisateur
(
  idU int,
  gps_lat DECIMAL(11,8),
  gps_long DECIMAL(11,8),
  datetimeCrea TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT pk_Gps_utilisateur PRIMARY KEY (idU,gps_lat,gps_long,datetimeCrea)
)
;

DESC Gps_utilisateur
;


CREATE TABLE Amis
(
  idU_snd int,
  idU_rcv int,
  etat int DEFAULT 0,
  datetimeCrea TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
  datetimeMaj TIMESTAMP,
  CONSTRAINT pk_Amis PRIMARY KEY (idU_snd,idU_rcv),
  CONSTRAINT fk_AmisU1 FOREIGN KEY (idU_snd)
  REFERENCES Utilisateur(idU),
  CONSTRAINT fk_AmisU2 FOREIGN KEY (idU_rcv)
  REFERENCES Utilisateur(idU)
)
;

DESC Amis
;

DROP TABLE Message;
CREATE TABLE Message
(
  idMsg int AUTO_INCREMENT,
  valeur VARCHAR(256),
  tel_snd VARCHAR(20),
  tel_rcv VARCHAR(20),
  etat int DEFAULT 0,
  datetimeCrea TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
  datetimeMaj TIMESTAMP,
  CONSTRAINT pk_Message PRIMARY KEY (idMsg),
  CONSTRAINT fk_MessageU1 FOREIGN KEY (idU_snd)
  REFERENCES Utilisateur(idU),
  CONSTRAINT fk_MessageU2 FOREIGN KEY (idU_rcv)
  REFERENCES Utilisateur(idU)
)
;

DESC Message
;

source 06_Trigger.sql
