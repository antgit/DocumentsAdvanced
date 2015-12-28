namespace BusinessObjects.Models
{
    /// <summary>
    /// ������ ������� �����
    /// </summary>
    public class ProductModel : BaseModel<Product>
    {
        /// <summary>�����������</summary>
        public ProductModel()
        {

        }

        /// <summary>�����������</summary>
        public ProductModel(Product value): this()
        {
            GetData(value);
        }
        /// <summary>���������� ������</summary>
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
        #region ��������
        /// <summary>�������� �����</summary>
        public string DefaultGroup { get; set; }
        /// <summary>������������ ������� ���������</summary>
        public string UnitName { get; set; }
        /// <summary>��� ������� ���������</summary>
        public string UnitCode { get; set; }
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId { get; set; }
        /// <summary>������������ �����������, �������� ����������� ������</summary>
        public string MyCompanyName { get; set; }
        /// <summary>�������������� �����</summary>
        public string Nomenclature { get; set; }
        /// <summary>����������� �����</summary>
        public string Articul { get; set; }
        /// <summary>���������� �����</summary>
        public string Catalogue { get; set; }
        /// <summary>����� ���</summary>
        public string Barcode { get; set; }
        /// <summary>������������� ������� ���������</summary>
        public int? UnitId { get; set; }
        /// <summary>���</summary>
        public decimal Weight { get; set; }
        /// <summary>������</summary>
        public decimal Height { get; set; }
        /// <summary> ������</summary>
        public decimal Width { get; set; }
        /// <summary>�������</summary>
        public decimal Depth { get; set; }
        /// <summary>����������</summary>
        public string Size { get; set; }
        /// <summary>������ ��������</summary>
        public decimal? StoragePeriod { get; set; }
        /// <summary>������������� ������������</summary>
        public int ManufactureId { get; set; }
        /// <summary>������������ ������������</summary>
        public string ManufactureName { get; set; }
        /// <summary>������������� �������� �����</summary>
        public int? TradeMarkId { get; set; }
        /// <summary>������������ �������� �����</summary>
        public string TradeMarkName { get; set; }
        /// <summary>������������� ������</summary>
        public int? BrandId { get; set; }
        /// <summary>������������ ������</summary>
        public string BrandName { get; set; }
        /// <summary>������������� ��� ���������</summary>
        public int? ProductTypeId { get; set; }
        /// <summary>������������ ��� ���������</summary>
        public string ProductTypeName { get; set; }
        /// <summary>������������� ��������</summary>
        public int? PakcTypeId { get; set; }
        /// <summary>������������ ��������</summary>
        public string PakcTypeName { get; set; }
        /// <summary>������������� �����</summary>
        public int ColorId { get; set; }
        /// <summary>������������ �����</summary>
        public string ColorName { get; set; } 
        #endregion
    }
}