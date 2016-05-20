
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

SHOW TRIGGERS LIKE '%'\G
/*
@Author : lortole
@Besoin : Supprimer les doublons dans la table ami. (1,2) = (2,1)
*/
