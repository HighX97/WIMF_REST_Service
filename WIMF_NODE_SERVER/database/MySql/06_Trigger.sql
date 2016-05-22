
DELIMITER //
CREATE TRIGGER trigger_ami_delete_doublon
AFTER INSERT
ON Amis
FOR EACH ROW
BEGIN
  DECLARE x INT;
  SET x = (SELECT count(*)
  FROM Amis
  WHERE idU_rcv = new.idU_snd
        AND
        idU_snd = new.idU_rcv );
  IF (x > 0)
  THEN
    DELETE
    FROM Amis
    WHERE idU_rcv = new.idU_rcv
      AND
      idU_snd = new.idU_snd
    ;
  END IF;
END;
//
DELIMITER ;

DELIMITER //
CREATE TRIGGER trigger_utilisateur_gps
AFTER UPDATE
ON Utilisateur
FOR EACH ROW
BEGIN
  IF ((new.gps_lat != old.gps_lat OR new.gps_long != old.gps_long) AND new.gps_lat IS NOT NULL AND new.gps_long IS NOT NULL)
  THEN
  INSERT INTO Gps_utilisateur
  (idU,gps_lat,gps_long,datetimeCrea)
  VALUES
  (new.idU,new.gps_lat,new.gps_long,CURRENT_TIMESTAMP)
  ;
  END IF;
END;
//
DELIMITER ;

SHOW TRIGGERS LIKE '%'\G
/*
@Author : lortole
@Besoin : Supprimer les doublons dans la table ami. (1,2) = (2,1)
*/
