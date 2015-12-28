UPDATE Product.Products
SET BrandId = a.Id
--SELECT i.ENT_GUID, m.MSC_GUID, i.BrandName, p.BrandId, a.Guid,a.Name
  FROM Донецк7.dbo.uv_GoodsInfo i 
inner JOIN Донецк7.dbo.MISC m ON m.MSC_ID = i.BrandID
INNER JOIN Product.Products p ON p.Guid = i.ENT_GUID
LEFT JOIN Analitic.Analitics a ON m.MSC_NAME = a.NAME 
WHERE i.BrandID IS NOT NULL AND (p.BrandId<> a.Id OR p.BrandId IS null)


UPDATE Product.Products
SET TradeMarkId = a.Id
--SELECT i.ENT_GUID, m.MSC_GUID, i.GoodGroupName, p.TradeMarkId, a.Guid,a.Name
  FROM Донецк7.dbo.uv_GoodsInfo i 
inner JOIN Донецк7.dbo.MISC m ON m.MSC_ID = i.GoodGroupID
INNER JOIN Product.Products p ON p.Guid = i.ENT_GUID
LEFT JOIN Analitic.Analitics a ON m.MSC_NAME = a.NAME 
WHERE i.BrandID IS NOT NULL AND (p.TradeMarkId<> a.Id OR p.TradeMarkId IS null)