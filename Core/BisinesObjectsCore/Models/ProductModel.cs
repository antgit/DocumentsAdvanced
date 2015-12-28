namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель объекта учета
    /// </summary>
    public class ProductModel : BaseModel<Product>
    {
        /// <summary>Конструктор</summary>
        public ProductModel()
        {

        }

        /// <summary>Конструктор</summary>
        public ProductModel(Product value): this()
        {
            GetData(value);
        }
        /// <summary>Заполнение данных</summary>
        /// <param name="value"></param>
        public override void GetData(Product value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
            Nomenclature = value.Nomenclature;
            Articul = value.Articul;
            Catalogue = value.Cataloque;
            Barcode = value.Barcode;
            UnitId = value.UnitId!=0? value.UnitId: (int?)null;
            UnitName = value.UnitId != 0 ? value.Unit.Name : string.Empty;
            UnitCode = value.UnitId != 0 ? value.Unit.Code : string.Empty;
            Weight = value.Weight;
            Height = value.Height;
            Width = value.Width;
            Depth = value.Depth;
            Size = value.Size;
            StoragePeriod = value.StoragePeriod;
            ManufactureId = value.ManufacturerId;
            ManufactureName = value.ManufacturerId != 0 ? value.Manufacturer.Name: string.Empty;
            TradeMarkId = value.TradeMarkId==0? (int?)null: value.TradeMarkId;
            TradeMarkName = value.TradeMarkId != 0 ? value.TradeMark.Name : string.Empty;
            BrandId = value.BrandId==0? (int?)null: value.BrandId;
            BrandName = value.BrandId != 0 ? value.Brand.Name : string.Empty;
            ProductTypeId = value.ProductTypeId==0? (int?)null: value.ProductTypeId;
            ProductTypeName = value.ProductTypeId != 0 ? value.ProductType.Name : string.Empty;
            PakcTypeId = value.PakcTypeId==0? (int?)null: value.PakcTypeId;
            PakcTypeName = value.PakcTypeId != 0 ? value.PakcType.Name : string.Empty;
            ColorId = value.ColorId;
            ColorName = value.ColorId != 0 ? value.Color.Name : string.Empty;
        }
        #region Свойства
        /// <summary>Основная група</summary>
        public string DefaultGroup { get; set; }
        /// <summary>Наименование единицы измерения</summary>
        public string UnitName { get; set; }
        /// <summary>Код единицы измерения</summary>
        public string UnitCode { get; set; }
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Номенклатурный номер</summary>
        public string Nomenclature { get; set; }
        /// <summary>Артикульный номер</summary>
        public string Articul { get; set; }
        /// <summary>Каталожный номер</summary>
        public string Catalogue { get; set; }
        /// <summary>Штрих код</summary>
        public string Barcode { get; set; }
        /// <summary>Идентификатор единицы измерения</summary>
        public int? UnitId { get; set; }
        /// <summary>Вес</summary>
        public decimal Weight { get; set; }
        /// <summary>Высота</summary>
        public decimal Height { get; set; }
        /// <summary> Ширина</summary>
        public decimal Width { get; set; }
        /// <summary>Глубина</summary>
        public decimal Depth { get; set; }
        /// <summary>Типоразмер</summary>
        public string Size { get; set; }
        /// <summary>Период хранения</summary>
        public decimal? StoragePeriod { get; set; }
        /// <summary>Идентификатор изготовителя</summary>
        public int ManufactureId { get; set; }
        /// <summary>Наименование изготовителя</summary>
        public string ManufactureName { get; set; }
        /// <summary>Идентификатор торговой марки</summary>
        public int? TradeMarkId { get; set; }
        /// <summary>Наименование торговой марки</summary>
        public string TradeMarkName { get; set; }
        /// <summary>Идентификатор бренда</summary>
        public int? BrandId { get; set; }
        /// <summary>Наименование бренда</summary>
        public string BrandName { get; set; }
        /// <summary>Идентификатор вид продукции</summary>
        public int? ProductTypeId { get; set; }
        /// <summary>Наименование вид продукции</summary>
        public string ProductTypeName { get; set; }
        /// <summary>Идентификатор упаковка</summary>
        public int? PakcTypeId { get; set; }
        /// <summary>Наименование упаковки</summary>
        public string PakcTypeName { get; set; }
        /// <summary>Идентификатор цвета</summary>
        public int ColorId { get; set; }
        /// <summary>Наименование цвета</summary>
        public string ColorName { get; set; } 
        #endregion
    }
}