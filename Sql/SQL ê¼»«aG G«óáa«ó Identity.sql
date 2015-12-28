SET IDENTITY_INSERT Product.Products ON
INSERT INTO Product.Products(Id, Guid, DatabaseId, DbSourceId, UserName,
            DateModified, Flags, StateId, [Name], KindId, Code, Memo, FlagString,
            TemplateId, Nomenclature, Articul, Cataloque, Barcode, UnitId,
            [Weight], Height, Width, Depth, [Size], StoragePeriod, ManufacturerId,
            TradeMarkId, BrandId)
select s.Id, s.Guid, s.DatabaseId, s.DbSourceId, s.UserName,
            s.DateModified, s.Flags, s.StateId, s.[Name], s.KindId, s.Code, s.Memo, s.FlagString,
            s.TemplateId, s.Nomenclature, s.Articul, s.Cataloque, s.Barcode, s.UnitId,
            s.[Weight], s.Height, s.Width, s.Depth, s.[Size], s.StoragePeriod, s.ManufacturerId,
            s.TradeMarkId, s.BrandId
 FROM [srv-devel].[Documents2009].Product.Products s LEFT JOIN
Product.Products d ON d.Guid = s.Guid
WHERE d.Guid IS NULL;

SET IDENTITY_INSERT Product.Products OFF