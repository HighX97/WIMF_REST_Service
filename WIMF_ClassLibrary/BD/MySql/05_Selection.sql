--Table Utilisateur

--Table Amis

SELECT  idU, nom, tel
FROM Utilisateur
;

SELECT  idU1, idU2
FROM Amis
;

SELECT  idU1, idU2 FROM Amis , Utilisateur WHERE idU1 = idU;

SELECT U1.idU,U1.nom,U1.tel,A.dateCrea,U2.idU,U2.nom,U2.tel,M.valeur,M.dateCrea FROM Amis A,Utilisateur U1,Utilisateur U2, Message M WHERE A.idU1 = U1.idU AND A.idU2 = U2.idU AND M.idU1 = A.idU1 AND M.idU2 = A.idU2 ;
--
SELECT *
FROM Amis
WHERE idU1 = 1 AND
  dateCrea <= (SELECT min(dateCrea)
  FROM Amis
  WHERE idU1 = 1)
;

--Table Message
