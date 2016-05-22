
UPDATE Amis SET datetimeMaj=CURRENT_TIMESTAMP WHERE idU1 IS NOT NULL;
select * from Amis;

select * from Utilisateur;
select * from Gps_utilisateur;
UPDATE Utilisateur SET datetimeMaj=CURRENT_TIMESTAMP WHERE idU IS NOT NULL;
UPDATE Utilisateur SET gps_lat=-161, gps_long =161, datetimeMaj=CURRENT_TIMESTAMP WHERE idU IS NOT NULL;
select * from Utilisateur;

UPDATE Message SET datetimeMaj=CURRENT_TIMESTAMP WHERE idU1 IS NOT NULL;
select * from Message;
