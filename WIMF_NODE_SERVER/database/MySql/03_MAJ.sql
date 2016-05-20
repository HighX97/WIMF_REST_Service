
UPDATE Amis SET dateMaj=CURRENT_TIMESTAMP WHERE idU1 IS NOT NULL;
select * from Amis;

UPDATE Utilisateur SET dateMaj=CURRENT_TIMESTAMP WHERE idU IS NOT NULL;
select * from Utilisateur;

UPDATE Message SET dateMaj=CURRENT_TIMESTAMP WHERE idU1 IS NOT NULL;
select * from Message;
