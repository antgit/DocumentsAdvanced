USE [AccentWebOrders]
GO
/****** Object:  StoredProcedure [dbo].[ExportProductsFromAccent]    Script Date: 08/25/2011 11:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	DECLARE @ds DATE =GETDATE()
	EXEC ExportProductsFromAccent @ds,'Бренд','Товарная группа','Вид продукции'
*/
ALTER PROCEDURE [dbo].[ExportProductsFromAccent]
@ds DATE, @BrandParamName NVARCHAR(100), @ProductTypeName NVARCHAR(100), @TradeMarkParamName NVARCHAR(100)
AS
BEGIN
SET NOCOUNT ON;


SELECT e.ENT_ID AS Id, e.ENT_GUID AS Guid, e.ENT_NAME AS [Name], e.ENT_NOM AS Nomenclature, e.ENT_MEMO AS Memo,
       b.BrandId, b.BrandGuid, b.BrandName,
       p.ProductTypeId, p.ProductTypeGuid, p.ProductTypeName,
       t.TradeMarkId, t.TradeMarkGuid, t.TradeMarkName, ISNULL(pr.PRC_VALUE,0) AS Price
  FROM dbo.ENTITIES e
  LEFT JOIN (SELECT v.ENT_ID, v.PRM_LONG AS BrandId, m.MSC_GUID AS BrandGuid, m.MSC_NAME AS BrandName 
   FROM dbo.ENT_PARAM_NAMES n INNER JOIN dbo.ENT_PARAMS v ON v.PRM_ID=N.PRM_ID
   INNER JOIN dbo.MISC m ON m.MSC_ID=v.PRM_LONG
  WHERE n.PRM_NAME=@BrandParamName) AS b ON e.ENT_ID=b.ENT_ID
  
  LEFT JOIN (SELECT v.ENT_ID, v.PRM_LONG AS ProductTypeId, m.MSC_GUID AS ProductTypeGuid, m.MSC_NAME AS ProductTypeName
   FROM dbo.ENT_PARAM_NAMES n INNER JOIN dbo.ENT_PARAMS v ON v.PRM_ID=N.PRM_ID
   INNER JOIN dbo.MISC m ON m.MSC_ID=v.PRM_LONG
  WHERE n.PRM_NAME=@ProductTypeName) AS p ON e.ENT_ID=p.ENT_ID
  
  LEFT JOIN (SELECT v.ENT_ID, v.PRM_LONG AS TradeMarkId, m.MSC_GUID AS TradeMarkGuid, m.MSC_NAME AS TradeMarkName
   FROM dbo.ENT_PARAM_NAMES n INNER JOIN dbo.ENT_PARAMS v ON v.PRM_ID=N.PRM_ID
   INNER JOIN dbo.MISC m ON m.MSC_ID=v.PRM_LONG
  WHERE n.PRM_NAME=@TradeMarkParamName) AS t ON e.ENT_ID=t.ENT_ID
  
  LEFT JOIN (SELECT pc.ENT_ID, pc.PRC_VALUE 
  FROM dbo.PRC_CONTENTS pc
   INNER JOIN (SELECT m.ENT_ID, max(m.PRC_DATE)AS MaxDate
  FROM dbo.PRC_CONTENTS m
	WHERE m.PRC_ID = 21 AND m.PRL_ID = 21 AND m.PRC_DATE<@ds
	GROUP BY m.ENT_ID) AS v ON pc.ENT_ID=v.ENT_ID AND pc.PRC_DATE=v.MaxDate 
	WHERE pc.PRC_ID = 21 AND pc.PRL_ID = 21) pr ON pr.ENT_ID=e.ENT_ID
	 
WHERE e.ENT_TYPE=1004
END

