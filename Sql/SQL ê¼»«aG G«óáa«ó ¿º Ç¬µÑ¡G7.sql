-- SELECT * FROM Product.Products p
/*
DELETE FROM Product.Products
WHERE Id NOT IN
(SELECT p.Id FROM Product.Products p
INNER JOIN Донецк7.dbo.ENTITIES e ON p.Guid = e.ENT_GUID)
AND IsTemplate= 0
*/


DECLARE @AccProducts TABLE (id INT, Guid UNIQUEIDENTIFIER, KindId INT, NAME NVARCHAR(255), Code NVARCHAR(50), Cataloque NVARCHAR(50),
                            Nomenclature NVARCHAR(50), Articul NVARCHAR(50), Barcode NVARCHAR(50), Memo NVARCHAR(255), UnitId INT, UnitName NVARCHAR(16))
INSERT @AccProducts(id, Guid, KindId, [NAME], Code, Cataloque, Nomenclature, Articul, Barcode, Memo,
       UnitId,UnitName)
SELECT e.ENT_ID, e.ENT_GUID, e.ENT_TYPE, e.ENT_NAME, e.ENT_TAG,
       e.ENT_CAT, e.ENT_NOM, e.ENT_ART, e.ENT_BAR, e.ENT_MEMO, e.UN_ID,u.UN_SHORT 
  FROM Донецк7.dbo.ENTITIES e 
LEFT JOIN Донецк7.dbo.UNITS u ON e.UN_ID = u.UN_ID
WHERE e.ENT_TYPE = 1004

SELECT * FROM @AccProducts
-- 1 Создание или обновление товаров
MERGE Product.Products AS T 
      USING @AccProducts AS S   
      ON T.Guid = S.Guid 
      WHEN NOT MATCHED BY TARGET 
         THEN INSERT(Guid,DatabaseId, Flags, StateId, KindId, Code, Name,Cataloque,Nomenclature,Articul,Barcode,Memo) 
         VALUES(Guid,1, 0, 1, 65537, S.Code, S.Name, S.Cataloque,S.Nomenclature,S.Articul,S.Barcode,S.Memo) 
      WHEN MATCHED 
         THEN UPDATE SET Code=S.Code,NAME=S.Name,Cataloque=S.Cataloque,Nomenclature=S.Nomenclature,Articul=S.Articul,Barcode=S.Barcode,Memo=S.Memo
      output $action, INSERTED.*;

-- 2 Обновление единиц измерения
-- Проверка несоответствия единиц измерений, если имеется результат - ошибка
SELECT 'ERR_UNIT', * FROM @AccProducts a LEFT JOIN Core.Units u ON a.UnitName = u.Code WHERE 
a.UnitId IS NOT NULL AND u.id IS NULL

UPDATE @AccProducts
SET UnitId = u.Id
FROM @AccProducts a INNER JOIN Core.Units u ON a.UnitName = u.Code

MERGE Product.Products AS T 
      USING @AccProducts AS S   
      ON T.Guid = S.Guid  
      WHEN MATCHED AND isnull(T.UnitId,0)<>isnull(S.UnitId,0)
         THEN UPDATE SET UnitId=S.UnitId
      output $action, INSERTED.*;

SELECT * FROM Product.Products