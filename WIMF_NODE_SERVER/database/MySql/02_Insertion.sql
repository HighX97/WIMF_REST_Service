--source 02_Insertion.sql
--Table Utilisateur
SELECT * FROM Utilisateur
;
INSERT INTO Utilisateur
(nom,tel)
VALUES
("loic","0695504940")
;
INSERT INTO Utilisateur
(nom,tel)
VALUES
("mariam","0753369827")
;
INSERT INTO Utilisateur
(nom,tel)
VALUES
("jimmy","0789654004")
;
INSERT INTO Utilisateur
(nom,tel)
VALUES
("chahinaz","0783573458")
;
SELECT * FROM Utilisateur
;

--Table Amis
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(1,2)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(1,3)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(1,4)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(2,1)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(2,3)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(2,4)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(3,1)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(3,2)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(3,4)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(4,1)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(4,2)
;
INSERT INTO Amis
(idU_snd,idU_rcv)
VALUES
(4,3)
;
SELECT * FROM Amis
;

--Table Message
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0695504940","0753369827")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0695504940","0789654004")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0695504940","0783573458")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0753369827","0695504940")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0753369827","0789654004")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0753369827","0783573458")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0789654004","0695504940")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0789654004","0753369827")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0789654004","0783573458")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0783573458","0695504940")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0783573458","0753369827")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0783573458","0789654004")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0695504940","0753369827")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0695504940","0789654004")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0695504940","0783573458")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0753369827","0695504940")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0753369827","0789654004")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0753369827","0783573458")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0789654004","0695504940")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0789654004","0753369827")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0789654004","0783573458")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0783573458","0695504940")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0783573458","0753369827")
;
INSERT INTO Message
(valeur,tel_snd,tel_rcv)
VALUES
("Message","0783573458","0789654004")
;
SELECT * from Message;
