--Table Amis
----
DELIMITER //
CREATE TRIGGER trigger_ami_delete_doublon
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

----
DELIMITER //
CREATE TRIGGER trigger_ami_updt_dateMaj
BEFORE UPDATE ON Amis
FOR EACH ROW
BEGIN
  SET new.dateMaj=CURRENT_TIMESTAMP
END;
//
DELIMITER ;
  WHERE idU1=new.idU1 AND idU2=new.idU2;
    UPDATE Amis
--DROP TRIGGER trigger_ami_updt_dateMaj


DELIMITER //
CREATE TRIGGER trigger_ami_insrt_dateMaj
AFTER INSERT
ON Amis
FOR EACH ROW
BEGIN
  UPDATE Amis
  SET dateMaj=CURRENT_TIMESTAMP
  WHERE idU1=new.idU1 AND idU2=new.idU2;
END;
//
DELIMITER ;

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
