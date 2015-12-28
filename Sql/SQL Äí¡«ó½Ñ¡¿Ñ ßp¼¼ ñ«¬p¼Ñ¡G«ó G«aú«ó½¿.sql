declare @ds date='20090101',@de date='20110101'
DECLARE @tblDocDetail TABLE (id INT, Summa MONEY)
DECLARE @tblDocs TABLE (id INT, Summa MONEY)
INSERT @tblDocDetail(id, Summa)
SELECT d.Id,sum(d2.Summa) AS Summa
  FROM DOCUMENT.Documents d 
       INNER JOIN Sales.DocumentDetails d2 ON d.Id=d2.OwnId
WHERE d.[Date] BETWEEN @ds AND @de  
      AND (d.Kind=131073 OR d.Kind=131074 OR d.Kind=131083 OR d.Kind=131084) 
      AND d.StateId = 1 AND d.IsTemplate=0
      AND d2.StateId = 1
GROUP BY d.Id

INSERT @tblDocs(id, Summa)      
SELECT d.Id,sum(d.Summa) AS Summs
      FROM DOCUMENT.Documents d 
WHERE d.[Date] BETWEEN @ds AND @de  
      AND (d.Kind=131073 OR d.Kind=131074 OR d.Kind=131083 OR d.Kind=131084) 
      AND d.StateId = 1 AND d.IsTemplate=0
GROUP BY d.Id

UPDATE [Document].Documents
SET Summa = d2.Summa 
FROM 
[Document].Documents m INNER JOIN @tblDocDetail d2 ON d2.id = m.id
WHERE m.Summa<>d2.Summa 

UPDATE [Document].Documents
SET SummaTax = Summa*.2
WHERE  SummaTax <> Summa*.2