--Table Utilisateur

--Table Amis

SELECT  idU, nom, tel
FROM Utilisateur
;

SELECT  idU_snd, idU_rcv
FROM Amis
;

SELECT  idU_snd, idU_rcv FROM Amis , Utilisateur WHERE idU_snd = idU;

SELECT  idU_snd, idU_rcv FROM Amis , Utilisateur WHERE idU_snd = idU;

SELECT DISTINCT(Ufind.idU),Ufind.nom,Ufind.tel,A.datetimeCrea,A.etat
FROM Message M ,Utilisateur Ufind ,Utilisateur Usearch
WHERE (Usearch.tel = "1234567890") and (M.idU_snd = Usearch.idU and Ufind.idu = M.idU_rcv ) or (M.idU_rcv = Usearch.idU and Ufind.idu = M.idU_snd )


SELECT DISTINCT(Ufind.idU),Ufind.nom,Ufind.tel,A.datetimeCrea,A.etat
FROM Amis A ,Utilisateur Ufind ,Utilisateur Usearch
WHERE (Usearch.tel = "1234567890") and (A.idU_snd = Usearch.idU and Ufind.idu = A.idU_rcv ) or (A.idU_rcv = Usearch.idU and Ufind.idu = A.idU_snd )

SELECT DISTINCT(U.idU),U.nom,U.tel,A.datetimeCrea,A.etat
FROM Amis A,Utilisateur U
WHERE (A.idU_snd = 1 and U.idu = A.idU_rcv ) or (A.idU_rcv = 1 and U.idu = A.idU_snd )
ORDER BY A.datetimeCrea ASC

SELECT U1.idU,U1.nom,U1.tel,A.datetimeCrea,U2.idU,U2.nom,U2.tel,M.valeur,M.datetimeCrea
FROM Amis A,Utilisateur U1,Utilisateur U2, Message M
WHERE A.idU_snd = U1.idU AND A.idU_rcv = U2.idU AND M.idU_snd = A.idU_snd AND M.idU_rcv = A.idU_rcv ;
--
SELECT *
FROM Amis
WHERE idU_snd = 1 AND
  datetimeCrea <= (SELECT min(datetimeCrea)
  FROM Amis
  WHERE idU_snd = 1)
;
--Table Message
SELECT U.idU, U.nom, U.tel, U.gps,A.idU_snd, A.etat, A.datetimeCrea, A.dateMaj
FROM Utilisateur U, Amis A
WHERE U.idU = 1 AND (A.idU_snd =U.idU OR A.idU_rcv = U.idU)
;

select idMsg,valeur,idU_snd,idU_rcv,etat,datetimeCrea,dateMaj from Message where idMsg = 4 limit 0,50
