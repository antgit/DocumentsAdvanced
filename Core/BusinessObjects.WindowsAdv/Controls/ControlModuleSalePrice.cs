using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BusinessObjects.Windows.Controls
{
    internal partial class ControlModuleSalePrice : BusinessObjects.Windows.Controls.ControlBase
    {
        public ControlModuleSalePrice()
        {
            InitializeComponent();
            /*
             DECLARE @PrcNameId INT=3  -- идентификатор цены
DECLARE @Date date=null   -- дата для определения цены

IF @Date IS NULL
   SELECT @Date=GETDATE()
SELECT MAX([Date]) AS [Date], MAX([Id]) AS [Id], PrcNameId INTO #t1 FROM [Price].ActualPrices  a
		WHERE [Date]<=@date 
		AND a.PrcNameId=@PrcNameId
GROUP BY PrcNameId, a.ProductId

SELECT p.ProductId, p.[Value] INTO #tmp
  FROM #t1 a INNER JOIN [Price].ActualPrices p ON p.Id=a.Id 

SELECT p.Id, p.Nomenclature, p.Name , isnull(pv.Value, 0) AS Price
FROM Product.ProductView p
LEFT JOIN #tmp pv ON p.Id=pv.ProductId 
WHERE 
(p.KindValue=1 OR p.KindValue=3)
AND p.IsTemplate=0

DROP TABLE #t1
DROP TABLE #tmp
             
             */
        }
    }
}
