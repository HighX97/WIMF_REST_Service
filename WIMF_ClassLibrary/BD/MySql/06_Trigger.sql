--Table Amis
DELIMITER //
CREATE TRIGGER trigger_ami
AFTER INSERT
ON Amis
FOR EACH ROW
BEGIN
  DECLARE x INT;
  SET x = (SELECT count(*)
  FROM Amis
  WHERE idU1 = new.idU2
        AND
        idU2 = new.idU1 );
  IF (x > 0)
  THEN
    DELETE
    FROM Amis
    WHERE idU1 = new.idU1
      AND
      idU2 = new.idU2
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
