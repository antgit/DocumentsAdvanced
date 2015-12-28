using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>��������� ������� "������ �����"</summary>
    internal struct ProductStruct
    {
        /// <summary>�������������� �����</summary>
        public string Nomenclature;
        /// <summary>����������� �����</summary>
        public string Articul;
        /// <summary>���������� �����</summary>
        public string Catalogue;
        /// <summary>����� ���</summary>
        public string Barcode;
        /// <summary>������������� ������� ���������</summary>
        public int UnitId;
        /// <summary>���</summary>
        public decimal Weight;
        /// <summary>������</summary>
        public decimal Height;
        /// <summary> ������</summary>
        public decimal Width;
        /// <summary>�������</summary>
        public decimal Depth;
        /// <summary>����������</summary>
        public string Size;
        /// <summary>������ ��������</summary>
        public decimal? StoragePeriod;
        /// <summary>������������� ������������</summary>
        public int ManufactureId;
        /// <summary>������������� �������� �����</summary>
        public int TradeMarkId;
        /// <summary>������������� ������</summary>
        public int BrandId;
        /// <summary>������������� ��� ���������</summary>
        public int ProductTypeId;
        /// <summary>������������� ��������</summary>
        public int PakcTypeId;
        /// <summary>������������� �����</summary>
        public int ColorId;
    }
    /// <summary>
    /// ������ ����� - �����, �������� ��������
    /// </summary>
    public sealed class Product : BaseCore<Product>, IChains<Product>, IReportChainSupport, IEquatable<Product>, IFacts<Product>,
        ICodes<Product>,
        IChainsAdvancedList<Product, FileData>,
        IChainsAdvancedList<Product, Knowledge>,
        IChainsAdvancedList<Product, Note>,
        IChainsAdvancedList<Product, Analitic>,
        IHierarchySupport, ICompanyOwner
    {
        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>����������� ���, ������������� �������� 0</summary>
        public const int KINDVALUE_DEFAULT = 0;
        /// <summary>�����, ������������� �������� 1</summary>
        public const int KINDVALUE_PRODUCT = 1;
        /// <summary>��������, ������������� �������� 2</summary>
        public const int KINDVALUE_MONEY = 2;
        /// <summary>������, ������������� �������� 3</summary>
        public const int KINDVALUE_SERVICE = 3;
        /// <summary>�������� ��������, ������������� �������� 4</summary>
        public const int KINDVALUE_ASSETS = 4;
        /// <summary>����, ������������� �������� 5</summary>
        public const int KINDVALUE_PACK = 5;
        /// <summary>����������, ������������� �������� 6</summary>
        public const int KINDVALUE_AUTO = 6;
        /// <summary>���, ������������� �������� 7</summary>
        public const int KINDVALUE_MBP = 7;
        /// <summary>���������, ������������� �������� 8</summary>
        public const int KINDVALUE_COMPUTER = 8;
        /// <summary>����������, ������������� �������� 8</summary>
        public const int KINDVALUE_ORGTEX = 9;
        /// <summary>������� ���������, ������������� �������� 10</summary>
        public const int KINDVALUE_FINISHPRODUCTION = 10;
        /// <summary>�����(���������), ������������� �������� 11</summary>
        public const int KINDVALUE_RAWMATERIAL = 11;
        

        /// <summary>�����, ������������� �������� 65537</summary>
        public const int KINDID_PRODUCT = 65537;
        /// <summary>��������, ������������� �������� 65538</summary>
        public const int KINDID_MONEY = 65538;
        /// <summary>������, ������������� �������� 65539</summary>
        public const int KINDID_SERVICE = 65539;
        /// <summary>�������� ��������, ������������� �������� 65540</summary>
        public const int KINDID_ASSETS = 65540;
        /// <summary>����, ������������� �������� 65541</summary>
        public const int KINDID_PACK = 65541;
        /// <summary>����������, ������������� �������� 65542</summary>
        public const int KINDID_AUTO = 65542;
        /// <summary>���, ������������� �������� 65543</summary>
        public const int KINDID_MBP = 65543;
        /// <summary>���������, ������������� �������� 65544</summary>
        public const int KINDID_COMPUTER = 65544;
        /// <summary>����������, ������������� �������� 65545</summary>
        public const int KINDID_ORGTEX = 65545;
        /// <summary>������� ���������, ������������� �������� 65546</summary>
        public const int KINDID_FINISHPRODUCTION = 65546;
        /// <summary>�����(���������), ������������� �������� 65547</summary>
        public const int KINDID_RAWMATERIAL = 65547;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Product>.Equals(Product other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// ��������� �������� ����� (�������) 
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="kind"></param>
        /// <param name="likeName">��������� ������ ������������</param>
        /// <param name="count">����������� ������������� ���������� �������</param>
        /// <returns></returns>
        public List<Product> GetCollectionByNomenclature(Workarea wa, int kind, string likeName, int count)
        {
            List<Product> collection = new List<Product>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Product>().Entity.FindMethod("GetCollectionByNomenclature").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Count, SqlDbType.Int).Value = count;
                        cmd.Parameters.Add(GlobalSqlParamNames.Value, SqlDbType.NVarChar, 100).Value = likeName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Product item = new Product { Workarea = wa };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
        /// <summary>
        /// ��������� ������� ���������� ��� ������
        /// </summary>
        /// <returns></returns>
        public List<PriceRegion> GetPriceRegions()
        {
            List<PriceRegion> collection = new List<PriceRegion>();
            using (SqlConnection cnn =  Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Entity.FindMethod("GetPriceRegions").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.ProductId, SqlDbType.Int).Value = Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            PriceRegion item = new PriceRegion { Workarea = Workarea };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }

        /// <summary>�����������</summary>
        public Product()
        {
            EntityId = (short)WhellKnownDbEntity.Product;
        }
        protected override void CopyValue(Product template)
        {
            base.CopyValue(template);
            Articul = template.Articul;
            Nomenclature = template.Nomenclature;
            Cataloque = template.Cataloque;
            Depth = template.Depth;
            Height = template.Height;
            ManufacturerId = template.ManufacturerId;
            PakcTypeId = template.PakcTypeId;
            ProductTypeId = template.ProductTypeId;
            Size = template.Size;
            StoragePeriod = template.StoragePeriod;
            TradeMarkId = template.TradeMarkId;
            UnitId = template.UnitId;
            Weight = template.Weight;
            Width = template.Width;
            ColorId = template.ColorId;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override Product Clone(bool endInit)
        {
            Product obj = base.Clone(false);
            obj.Articul = Articul;
            obj.Nomenclature = Nomenclature;
            obj.Cataloque = Cataloque;
            obj.Depth = Depth;
            obj.Height = Height;
            obj.ManufacturerId = ManufacturerId;
            obj.PakcTypeId = PakcTypeId;
            obj.ProductTypeId = ProductTypeId;
            obj.Size = Size;
            obj.StoragePeriod = StoragePeriod;
            obj.TradeMarkId = TradeMarkId;
            obj.UnitId = UnitId;
            obj.Weight = Weight;
            obj.Width = Width;
            obj.ColorId = ColorId;
            if (endInit)
                OnEndInit();
            return obj;
        }

		protected override void OnCreated()
		{
			base.OnCreated();
			if (_auto != null && _auto.Id != Id)
			{
				_auto.Id = Id;
			}
		}
        #region ��������
        private string _nomenclature;
        /// <summary>
        /// �������������� �����
        /// </summary>
        public string Nomenclature
        {
            get { return _nomenclature; }
            set
            {
                if (value == _nomenclature) return;
                OnPropertyChanging(GlobalPropertyNames.Nomenclature);
                _nomenclature = value;
                OnPropertyChanged(GlobalPropertyNames.Nomenclature);
            }
        }

        private string _articul;
        /// <summary>
        /// ����������� �����
        /// </summary>
        public string Articul
        {
            get { return _articul; }
            set
            {
                if (value == _articul) return;
                OnPropertyChanging(GlobalPropertyNames.Articul);
                _articul = value;
                OnPropertyChanged(GlobalPropertyNames.Articul);
            }
        }

        private string _cataloque;
        /// <summary>
        /// ���������� �����
        /// </summary>
        public string Cataloque
        {
            get { return _cataloque; }
            set
            {
                if (value == _cataloque) return;
                OnPropertyChanging(GlobalPropertyNames.Cataloque);
                _cataloque = value;
                OnPropertyChanged(GlobalPropertyNames.Cataloque);
            }
        }

        private string _barcode;
        /// <summary>
        /// ����� ���
        /// </summary>
        public string Barcode
        {
            get { return _barcode; }
            set
            {
                if (value == _barcode) return;
                OnPropertyChanging(GlobalPropertyNames.Barcode);
                _barcode = value;
                OnPropertyChanged(GlobalPropertyNames.Barcode);
            }
        }

        private int _unitId;
        /// <summary>
        /// ������������� ������� ���������
        /// </summary>
        public int UnitId
        {
            get { return _unitId; }
            set
            {
                if (value == _unitId) return;
                OnPropertyChanging(GlobalPropertyNames.UnitId);
                _unitId = value;
                OnPropertyChanged(GlobalPropertyNames.UnitId);
            }
        }

        private Unit _unit;
        /// <summary>
        /// ������� ��������� 
        /// </summary>
        public Unit Unit
        {
            get
            {
                if (_unitId == 0)
                    return null;
                if (_unit == null)
                    _unit = Workarea.Cashe.GetCasheData<Unit>().Item(_unitId);
                else if (_unit.Id != _unitId)
                    _unit = Workarea.Cashe.GetCasheData<Unit>().Item(_unitId);
                return _unit;
            }
            set
            {
                if (_unit == value) return;
                OnPropertyChanging(GlobalPropertyNames.Unit);
                _unit = value;
                _unitId = _unit == null ? 0 : _unit.Id;
                OnPropertyChanged(GlobalPropertyNames.Unit);
            }
        }

        private decimal _weight;
        /// <summary>
        /// ���
        /// </summary>
        public decimal Weight
        {
            get { return _weight; }
            set
            {
                if (value == _weight) return;
                OnPropertyChanging(GlobalPropertyNames.Weight);
                _weight = value;
                OnPropertyChanged(GlobalPropertyNames.Weight);
            }
        }

        private decimal _height;
        /// <summary>
        /// ������
        /// </summary>
        public decimal Height
        {
            get { return _height; }
            set
            {
                if (value == _height) return;
                OnPropertyChanging(GlobalPropertyNames.Height);
                _height = value;
                OnPropertyChanged(GlobalPropertyNames.Height);
            }
        }

        private decimal _width;
        /// <summary>
        /// ������
        /// </summary>
        public decimal Width
        {
            get { return _width; }
            set
            {
                if (value == _width) return;
                OnPropertyChanging(GlobalPropertyNames.Width);
                _width = value;
                OnPropertyChanged(GlobalPropertyNames.Width);
            }
        }

        private decimal _depth;
        /// <summary>
        /// �������
        /// </summary>
        public decimal Depth
        {
            get { return _depth; }
            set
            {
                if (value == _depth) return;
                OnPropertyChanging(GlobalPropertyNames.Depth);
                _depth = value;
                OnPropertyChanged(GlobalPropertyNames.Depth);
            }
        }

        private string _size;
        /// <summary>
        /// ����������
        /// </summary>
        public string Size
        {
            get { return _size; }
            set
            {
                if (value == _size) return;
                OnPropertyChanging(GlobalPropertyNames.Size);
                _size = value;
                OnPropertyChanged(GlobalPropertyNames.Size);
            }
        }

        private decimal? _storagePeriod;
        /// <summary>
        /// ������ ��������
        /// </summary>
        public decimal? StoragePeriod
        {
            get { return _storagePeriod; }
            set
            {
                if (value == _storagePeriod) return;
                OnPropertyChanging(GlobalPropertyNames.StoragePeriod);
                _storagePeriod = value;
                OnPropertyChanged(GlobalPropertyNames.StoragePeriod);
            }
        }

        private int _manufacturerId;
        /// <summary>
        /// ������������� ������������
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� �������������� ��������������� ������ "�������������"</remarks>
        public int ManufacturerId
        {
            get { return _manufacturerId; }
            set
            {
                if (value == _manufacturerId) return;
                OnPropertyChanging(GlobalPropertyNames.ManufacturerId);
                _manufacturerId = value;
                OnPropertyChanged(GlobalPropertyNames.ManufacturerId);
            }
        }

        private Agent _manufacturer;
        /// <summary>
        /// ������������ 
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� �������������� ��������������� ������ "�������������"</remarks>
        public Agent Manufacturer
        {
            get
            {
                if (_manufacturerId == 0)
                    return null;
                if (_manufacturer == null)
                    _manufacturer = Workarea.Cashe.GetCasheData<Agent>().Item(_manufacturerId);
                else if (_manufacturer.Id != _manufacturerId)
                    _manufacturer = Workarea.Cashe.GetCasheData<Agent>().Item(_manufacturerId);
                return _manufacturer;
            }
            set
            {
                if (_manufacturer == value) return;
                OnPropertyChanging(GlobalPropertyNames.Manufacturer);
                _manufacturer = value;
                _manufacturerId = _manufacturer == null ? 0 : _manufacturer.Id;
                OnPropertyChanged(GlobalPropertyNames.Manufacturer);
            }
        }

        private int _tradeMarkId;
        /// <summary>
        /// ������������� �������� �����
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� �������������� ��������� ������ "�������� ������"</remarks>
        public int TradeMarkId
        {
            get { return _tradeMarkId; }
            set
            {
                if (value == _tradeMarkId) return;
                OnPropertyChanging(GlobalPropertyNames.TradeMarkId);
                _tradeMarkId = value;
                OnPropertyChanged(GlobalPropertyNames.TradeMarkId);
            }
        }

        private Analitic _tradeMark;
        /// <summary>
        /// �������� ����� ��� �������� ������
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� ��������� ������ "�������� ������"</remarks>
        public Analitic TradeMark
        {
            get
            {
                if (_tradeMarkId == 0)
                    return null;
                if (_tradeMark == null)
                    _tradeMark = Workarea.Cashe.GetCasheData<Analitic>().Item(_tradeMarkId);
                else if (_tradeMark.Id != _tradeMarkId)
                    _tradeMark = Workarea.Cashe.GetCasheData<Analitic>().Item(_tradeMarkId);
                return _tradeMark;
            }
            set
            {
                if (_tradeMark == value) return;
                OnPropertyChanging(GlobalPropertyNames.TradeMark);
                _tradeMark = value;
                _tradeMarkId = _tradeMark == null ? 0 : _tradeMark.Id;
                OnPropertyChanged(GlobalPropertyNames.TradeMark);
            }
        }

        private int _brandId;
        /// <summary>
        /// ������������� ������
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� �������������� ��������� ������ "�����"</remarks>
        public int BrandId
        {
            get { return _brandId; }
            set
            {
                if (value == _brandId) return;
                OnPropertyChanging(GlobalPropertyNames.BrandId);
                _brandId = value;
                OnPropertyChanged(GlobalPropertyNames.BrandId);
            }
        }
        private Analitic _brand;
        /// <summary>
        /// �����
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� ��������� ������ "�����"</remarks>
        public Analitic Brand
        {
            get
            {
                if (_brandId == 0)
                    return null;
                if (_brand == null)
                    _brand = Workarea.Cashe.GetCasheData<Analitic>().Item(_brandId);
                else if (_brand.Id != _brandId)
                    _brand = Workarea.Cashe.GetCasheData<Analitic>().Item(_brandId);
                return _brand;
            }
            set
            {
                if (_brand == value) return;
                OnPropertyChanging(GlobalPropertyNames.Brand);
                _brand = value;
                _brandId = _brand == null ? 0 : _brand.Id;
                OnPropertyChanged(GlobalPropertyNames.Brand);
            }
        }

        private int _productTypeId;
        /// <summary>
        /// ������������� ��� ���������
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� �������������� ��������� ������ "��� ���������"</remarks>
        public int ProductTypeId
        {
            get { return _productTypeId; }
            set
            {
                if (value == _productTypeId) return;
                OnPropertyChanging(GlobalPropertyNames.ProductTypeId);
                _productTypeId = value;
                OnPropertyChanged(GlobalPropertyNames.ProductTypeId);
            }
        }

        private Analitic _productType;
        /// <summary>
        /// ��� ���������
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� ��������� ������ "��� ���������"</remarks>
        public Analitic ProductType
        {
            get
            {
                if (_productTypeId == 0)
                    return null;
                if (_productType == null)
                    _productType = Workarea.Cashe.GetCasheData<Analitic>().Item(_productTypeId);
                else if (_productType.Id != _productTypeId)
                    _productType = Workarea.Cashe.GetCasheData<Analitic>().Item(_productTypeId);
                return _productType;
            }
            set
            {
                if (_productType == value) return;
                OnPropertyChanging(GlobalPropertyNames.ProductType);
                _productType = value;
                _productTypeId = _productType == null ? 0 : _productType.Id;
                OnPropertyChanged(GlobalPropertyNames.ProductType);
            }
        }

        private int _pakcTypeId;
        /// <summary>
        /// ������������� ��������
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� �������������� ��������� ������ "��� ��������"</remarks>
        public int PakcTypeId
        {
            get { return _pakcTypeId; }
            set
            {
                if (value == _pakcTypeId) return;
                OnPropertyChanging(GlobalPropertyNames.PakcTypeId);
                _pakcTypeId = value;
                OnPropertyChanged(GlobalPropertyNames.PakcTypeId);
            }
        }

        private Analitic _pakcType;
        /// <summary>
        /// ��������
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� ��������� ������ "��� ��������"</remarks>
        public Analitic PakcType
        {
            get
            {
                if (_pakcTypeId == 0)
                    return null;
                if (_pakcType == null)
                    _pakcType = Workarea.Cashe.GetCasheData<Analitic>().Item(_pakcTypeId);
                else if (_pakcType.Id != _pakcTypeId)
                    _pakcType = Workarea.Cashe.GetCasheData<Analitic>().Item(_pakcTypeId);
                return _pakcType;
            }
            set
            {
                if (_pakcType == value) return;
                OnPropertyChanging(GlobalPropertyNames.PakcType);
                _pakcType = value;
                _pakcTypeId = _pakcType == null ? 0 : _pakcType.Id;
                OnPropertyChanged(GlobalPropertyNames.PakcType);
            }
        }
        
        private int _colorId;
        /// <summary>
        /// ������������� �����
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� �������������� ��������� ������ "����"</remarks>
        public int ColorId
        {
            get { return _colorId; }
            set
            {
                if (value == _colorId) return;
                OnPropertyChanging(GlobalPropertyNames.ColorId);
                _colorId = value;
                OnPropertyChanged(GlobalPropertyNames.ColorId);
            }
        }


        private Analitic _color;
        /// <summary>
        /// ����
        /// </summary>
        /// <remarks>��������� ���������� ��� ������� �������� �������� ��������� ������ "����"</remarks>
        public Analitic Color
        {
            get
            {
                if (_colorId == 0)
                    return null;
                if (_color == null)
                    _color = Workarea.Cashe.GetCasheData<Analitic>().Item(_colorId);
                else if (_color.Id != _colorId)
                    _color = Workarea.Cashe.GetCasheData<Analitic>().Item(_colorId);
                return _color;
            }
            set
            {
                if (_color == value) return;
                OnPropertyChanging(GlobalPropertyNames.Color);
                _color = value;
                _colorId = _color == null ? 0 : _color.Id;
                OnPropertyChanged(GlobalPropertyNames.Color);
            }
        }

        private int _myCompanyId;
        /// <summary>
        /// ������������� �����������, �������� ����������� ������ �����
        /// </summary>
        public int MyCompanyId
        {
            get { return _myCompanyId; }
            set
            {
                if (value == _myCompanyId) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompanyId);
                _myCompanyId = value;
                OnPropertyChanged(GlobalPropertyNames.MyCompanyId);
            }
        }


        private Agent _myCompany;
        /// <summary>
        /// ��� ��������, ����������� �������� ����������� ������ �����
        /// </summary>
        public Agent MyCompany
        {
            get
            {
                if (_myCompanyId == 0)
                    return null;
                if (_myCompany == null)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                else if (_myCompany.Id != _myCompanyId)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                return _myCompany;
            }
            set
            {
                if (_myCompany == value) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompany);
                _myCompany = value;
                _myCompanyId = _myCompany == null ? 0 : _myCompany.Id;
                OnPropertyChanged(GlobalPropertyNames.MyCompany);
            }
        }
        #endregion

        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_nomenclature))
                writer.WriteAttributeString(GlobalPropertyNames.Nomenclature, _nomenclature);
            if (!string.IsNullOrEmpty(_articul))
                writer.WriteAttributeString(GlobalPropertyNames.Articul, _articul);
            if (!string.IsNullOrEmpty(_cataloque))
                writer.WriteAttributeString(GlobalPropertyNames.Catalogue, _cataloque);
            if (!string.IsNullOrEmpty(_barcode))
                writer.WriteAttributeString(GlobalPropertyNames.Barcode, _barcode);
            if (_unitId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.UnitId, XmlConvert.ToString(_unitId));
            if (_weight != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Weight, XmlConvert.ToString(_weight));
            if (_height != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Height, XmlConvert.ToString(_height));
            if (_width != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Width, XmlConvert.ToString(_width));
            if (_depth != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Depth, XmlConvert.ToString(_depth));
            if (!string.IsNullOrEmpty(_size))
                writer.WriteAttributeString(GlobalPropertyNames.Size, _size);
            if (_storagePeriod.HasValue && _storagePeriod.Value!=0)
                writer.WriteAttributeString(GlobalPropertyNames.StoragePeriod, XmlConvert.ToString(_storagePeriod.Value));
            if (_manufacturerId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ManufactureId, XmlConvert.ToString(_manufacturerId));
            if (_tradeMarkId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TradeMarkId, XmlConvert.ToString(_tradeMarkId));
            if (_brandId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.BrandId, XmlConvert.ToString(_brandId));
            if (_productTypeId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProductTypeId, XmlConvert.ToString(_productTypeId));
            if (_pakcTypeId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.PakcTypeId, XmlConvert.ToString(_pakcTypeId));
            if (_colorId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ColorId, XmlConvert.ToString(_colorId));

        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Nomenclature) != null)
                _nomenclature = reader.GetAttribute(GlobalPropertyNames.Nomenclature);
            if (reader.GetAttribute(GlobalPropertyNames.Articul) != null)
                _articul = reader.GetAttribute(GlobalPropertyNames.Articul);
            if (reader.GetAttribute(GlobalPropertyNames.Catalogue) != null)
                _cataloque = reader.GetAttribute(GlobalPropertyNames.Catalogue);
            if (reader.GetAttribute(GlobalPropertyNames.Barcode) != null)
                _barcode = reader.GetAttribute(GlobalPropertyNames.Barcode);
            if (reader.GetAttribute(GlobalPropertyNames.UnitId) != null)
                _unitId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UnitId));
            if (reader.GetAttribute(GlobalPropertyNames.Weight) != null)
                _weight = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Weight));
            if (reader.GetAttribute(GlobalPropertyNames.Height) != null)
                _height = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Height));
            if (reader.GetAttribute(GlobalPropertyNames.Width) != null)
                _width = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Width));
            if (reader.GetAttribute(GlobalPropertyNames.Depth) != null)
                _depth = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Depth));
            if (reader.GetAttribute(GlobalPropertyNames.Size) != null)
                _size = reader.GetAttribute(GlobalPropertyNames.Size);
            if (reader.GetAttribute(GlobalPropertyNames.StoragePeriod) != null)
                _storagePeriod = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.StoragePeriod));
            if (reader.GetAttribute(GlobalPropertyNames.ManufactureId) != null)
                _manufacturerId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ManufactureId));
            if (reader.GetAttribute(GlobalPropertyNames.TradeMarkId) != null)
                _tradeMarkId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TradeMarkId));
            if (reader.GetAttribute(GlobalPropertyNames.BrandId) != null)
                _brandId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.BrandId));
            if (reader.GetAttribute(GlobalPropertyNames.ProductTypeId) != null)
                _productTypeId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProductTypeId));
            if (reader.GetAttribute(GlobalPropertyNames.PakcTypeId) != null)
                _pakcTypeId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PakcTypeId));
            if (reader.GetAttribute(GlobalPropertyNames.ColorId) != null)
                _colorId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ColorId));
        }
        #endregion

		#region Relation
		private Auto _auto;
		/// <summary>������ ����������</summary>
		public Auto Auto
		{
			get
			{
				if (_auto == null)
					RefreshAuto();
				return _auto;
			}
		}
		/// <summary>�������� ���������� ����������</summary>
		private void RefreshAuto()
		{
			if (!IsNew)
			{
				RelationHelper<Product, Auto> hlp = new RelationHelper<Product, Auto>();
				_auto = hlp.GetObject(this);
				_auto._owner = this;
			}
			else
			{
				_auto = new Auto { Workarea = Workarea };
				_auto._owner = this;
			}
		}

		/// <summary>
		/// �������� �����������
		/// </summary>
		public bool IsAuto
		{
			get { return (KindValue == Product.KINDVALUE_AUTO); }
		}
		#endregion

		#region �������������� ��������
		private List<ProductUnit> _units;
        /// <summary>
        /// ��������� ����������� ������ ���������
        /// </summary>
        public List<ProductUnit> Units
        {
            get 
            {
                if (_units == null)
                {
                    _units = GetCollectionProductUnit(this);
                    return _units;
                }
                return _units; 
            }
        }

        /// <summary>
        /// ��������� ������ ��������� �������
        /// </summary>
        /// <param name="product">������ ����� <see cref="BusinessObjects.Product"/></param>
        /// <returns></returns>
        private List<ProductUnit> GetCollectionProductUnit(Product product)
        {
            List<ProductUnit> collection = new List<ProductUnit>();
            if (product == null)
                return collection;
            using (SqlConnection cnn = product.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.ProductId, SqlDbType.Int).Value = product.Id;
                        cmd.CommandText = product.Workarea.Empty<Product>().Entity.FindMethod("Product.ProductUnitsLoadAll").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ProductUnit item = new ProductUnit { Workarea = product.Workarea };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }

        private List<Series> _series;
        /// <summary>������ ������</summary>
        public List<Series> Series
        {
            get
            {
                if (_series == null)
                    RefreshSeries();
                return _series;
            }
        }
        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="all">��������� ��� ���������</param>
        /// <remarks>��� ���������� ���� ��������� ������ ����������� ������ � ������� ������ � ����������� �������� ���������</remarks>
        public override void Refresh(bool all)
        {
            base.Refresh(all);
            if (!all) return;
            RefreshSeries();
            RefreshFa�tView();
            _units = GetCollectionProductUnit(this);
        }
        private void RefreshSeries()
        {
            _series = new List<Series>();

            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        cmd.CommandText = FindProcedure("LoadSeries");
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Series item = new Series {Workarea = Workarea};
                            item.Load(reader);
                            _series.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        #endregion

        #region ���������
        ProductStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new ProductStruct
                                  {
                                      Articul = _articul,
                                      Barcode = _barcode,
                                      BrandId = _brandId,
                                      Catalogue = _cataloque,
                                      Depth = _depth,
                                      Height = _height,
                                      ManufactureId = _manufacturerId,
                                      Nomenclature = _nomenclature,
                                      Size = _size,
                                      StoragePeriod = _storagePeriod,
                                      TradeMarkId = _tradeMarkId,
                                      UnitId = _unitId,
                                      Weight = _weight,
                                      Width = _width,
                                      ProductTypeId = _productTypeId,
                                      PakcTypeId = _pakcTypeId,
                                      ColorId = _colorId
                                  };
                return true;
            }
            return false;
        }

        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            Articul = _baseStruct.Articul;
            Barcode = _baseStruct.Barcode;
            BrandId = _baseStruct.BrandId;
            Cataloque = _baseStruct.Catalogue;
            Depth = _baseStruct.Depth;
            Height = _baseStruct.Height;
            ManufacturerId = _baseStruct.ManufactureId;
            Nomenclature = _baseStruct.Nomenclature;
            Size = _baseStruct.Size;
            StoragePeriod = _baseStruct.StoragePeriod;
            TradeMarkId = _baseStruct.TradeMarkId;
            UnitId = _baseStruct.UnitId;
            Weight = _baseStruct.Weight;
            Width = _baseStruct.Width;
            ProductTypeId = _baseStruct.ProductTypeId;
            PakcTypeId = _baseStruct.PakcTypeId;
            ColorId = _baseStruct.ColorId;
            IsChanged = false;
        }
        #endregion

        #region ���� ������
        /// <summary>�������� ������</summary>
        /// <param name="reader">������ SqlDataReader</param>
        /// <param name="endInit">������� ��������� ��������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _nomenclature = reader.IsDBNull(17) ? null : reader.GetString(17);
                _articul = reader.IsDBNull(18) ? null : reader.GetString(18);
                _cataloque = reader.IsDBNull(19) ? null : reader.GetString(19);
                _barcode = reader.IsDBNull(20) ? null : reader.GetString(20);
                _unitId = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
                _weight = reader.IsDBNull(22) ? 0 : reader.GetDecimal(22);
                _height = reader.IsDBNull(23) ? 0 : reader.GetDecimal(23);
                _width = reader.IsDBNull(24) ? 0 : reader.GetDecimal(24);
                _depth = reader.IsDBNull(25) ? 0 : reader.GetDecimal(25);
                _size = reader.IsDBNull(26) ? null : reader.GetString(26);
                _storagePeriod = reader.IsDBNull(27) ? 0 : reader.GetDecimal(27);
                _manufacturerId = reader.IsDBNull(28) ? 0 : reader.GetInt32(28);
                _tradeMarkId = reader.IsDBNull(29) ? 0 : reader.GetInt32(29);
                _brandId = reader.IsDBNull(30) ? 0 : reader.GetInt32(30);
                _productTypeId = reader.IsDBNull(31) ? 0 : reader.GetInt32(31);
                _pakcTypeId = reader.IsDBNull(32) ? 0 : reader.GetInt32(32);
                _colorId = reader.IsDBNull(33) ? 0 : reader.GetInt32(33);
                _myCompanyId = reader.IsDBNull(34) ? 0 : reader.GetInt32(34);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>���������� �������� ���������� ��� �������� �������� ��� ����������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ����������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Nomenclature, SqlDbType.NVarChar, 50) {IsNullable = true};
            if (string.IsNullOrEmpty(_nomenclature))
                prm.Value = DBNull.Value;
            else
                prm.Value = _nomenclature;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Articul, SqlDbType.NVarChar, 50) {IsNullable = true};
            if (string.IsNullOrEmpty(_articul))
                prm.Value = DBNull.Value;
            else
                prm.Value = _articul;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Cataloque, SqlDbType.NVarChar, 50) {IsNullable = true};
            if (string.IsNullOrEmpty(_cataloque))
                prm.Value = DBNull.Value;
            else
                prm.Value = _cataloque;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Barcode, SqlDbType.NVarChar, 50) {IsNullable = true};
            if (string.IsNullOrEmpty(_barcode))
                prm.Value = DBNull.Value;
            else
                prm.Value = _barcode;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.UnitId, SqlDbType.Int) {IsNullable = true};
            if (_unitId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _unitId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Weight, SqlDbType.Money) {IsNullable = true};
            if (_weight == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _weight;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Height, SqlDbType.Money) {IsNullable = true};
            if (_height == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _height;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Width, SqlDbType.Money) {IsNullable = true};
            if (_width == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _width;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Depth, SqlDbType.Money) {IsNullable = true};
            if (_depth == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _depth;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Size, SqlDbType.NVarChar, 50) {IsNullable = true};
            if (string.IsNullOrEmpty(_size))
                prm.Value = DBNull.Value;
            else
                prm.Value = _size;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.StoragePeriod, SqlDbType.Money) {IsNullable = true};
            if (_storagePeriod == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _storagePeriod;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ManufacturerId, SqlDbType.Int) {IsNullable = true};
            if (_manufacturerId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _manufacturerId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.TradeMarkId, SqlDbType.Int) {IsNullable = true};
            if (_tradeMarkId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _tradeMarkId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.BrandId, SqlDbType.Int) {IsNullable = true};
            if (_brandId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _brandId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ProductTypeId, SqlDbType.Int) { IsNullable = true };
            if (_productTypeId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _productTypeId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PakcTypeId, SqlDbType.Int) { IsNullable = true };
            if (_pakcTypeId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _pakcTypeId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ColorId, SqlDbType.Int) { IsNullable = true };
            if (_colorId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _colorId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.NVarChar) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Product> Members
        /// <summary>
        /// ����� ������
        /// </summary>
        /// <returns></returns>
        public List<IChain<Product>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// ����� ������
        /// </summary>
        /// <param name="kind">��� �����</param>
        /// <returns></returns>
        public List<IChain<Product>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Product> IChains<Product>.SourceList(int chainKindId)
        {
            return Chain<Product>.GetChainSourceList(this, chainKindId);
        }
        List<Product> IChains<Product>.DestinationList(int chainKindId)
        {
            return Chain<Product>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Product>> GetValues(bool allKinds)
        {
            return CodeHelper<Product>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Product>.GetView(this, true);
        }
        #endregion

        #region IFacts<Product> Members

        private List<FactView> _factView;
        public List<FactView> GetCollectionFactView()
        {
            return _factView ?? (_factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId));
        }

        public void RefreshFa�tView()
        {
            _factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId);
        }
        /// <summary>
        /// ������������� �������� ������
        /// </summary>
        /// <param name="factCode">��� ������������ �����</param>
        /// <param name="columnCode">��� ������� �����</param>
        /// <returns></returns>
        public FactView GetFactViewValue(string factCode, string columnCode)
        {
            return GetCollectionFactView().FirstOrDefault(s => s.FactNameCode == factCode && s.ColumnCode == columnCode);
        }
        /// <summary>
        /// ��������� ������������ ������ ��� ������� �����
        /// </summary>
        /// <returns></returns>
        public List<FactName> GetFactNames()
        {
            return FactHelper.GetFactNames(Workarea, EntityId);
        }
        #endregion


        #region IChainsAdvancedList<Product,FileData> Members

        List<IChainAdvanced<Product, FileData>> IChainsAdvancedList<Product, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<Product, FileData>)this).GetLinks(48);
        }

        List<IChainAdvanced<Product, FileData>> IChainsAdvancedList<Product, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<Product, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Product, FileData>(this);
        }
        /// <summary>
        /// ��������� ��������� ������ � �������� �����
        /// </summary>
        /// <returns></returns>
        public List<IChainAdvanced<Product, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<Product, FileData>> collection = new List<IChainAdvanced<Product, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Product>().Entity.FindMethod("LoadFiles").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = Id;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainAdvanced<Product, FileData> item = new ChainAdvanced<Product, FileData> { Workarea = Workarea, Left = this };
                                item.Load(reader);
                                collection.Add(item);
                            }
                        }
                        reader.Close();

                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
            return collection;
        }

        #endregion
        #region IChainsAdvancedList<Product,Knowledge> Members

        List<IChainAdvanced<Product, Knowledge>> IChainsAdvancedList<Product, Knowledge>.GetLinks()
        {
            return ((IChainsAdvancedList<Product, Knowledge>)this).GetLinks(60);
        }

        List<IChainAdvanced<Product, Knowledge>> IChainsAdvancedList<Product, Knowledge>.GetLinks(int? kind)
        {
            return GetLinkedKnowledges();
        }
        /// <summary>
        /// ��������� ��������� ������ ���� ������ � �������� �����
        /// </summary>
        /// <returns></returns>
        public List<IChainAdvanced<Product, Knowledge>> GetLinkedKnowledges()
        {
            List<IChainAdvanced<Product, Knowledge>> collection = new List<IChainAdvanced<Product, Knowledge>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Product>().Entity.FindMethod("LoadKnowledges").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = Id;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainAdvanced<Product, Knowledge> item = new ChainAdvanced<Product, Knowledge> { Workarea = Workarea, Left = this };
                                item.Load(reader);
                                collection.Add(item);
                            }
                        }
                        reader.Close();

                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
            return collection;
        }
        List<ChainValueView> IChainsAdvancedList<Product, Knowledge>.GetChainView()
        {
            return ChainValueView.GetView<Product, Knowledge>(this);
        }
        #endregion
        #region IChainsAdvancedList<Product,Note> Members

        List<IChainAdvanced<Product, Note>> IChainsAdvancedList<Product, Note>.GetLinks()
        {
            return ChainAdvanced<Product, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Product, Note>> IChainsAdvancedList<Product, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Product, Note>.CollectionSource(this, kind);
        }
        /// <summary>
        /// ��������� ������� "������ �����-����������"
        /// </summary>
        /// <param name="kind">��� �����, ������������� �������� �������������� ���� �����</param>
        /// <returns></returns>
        public List<IChainAdvanced<Product, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Product, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Product, Note>.GetChainView()
        {
            return ChainValueView.GetView<Product, Note>(this);
        }
        #endregion
        #region IChainsAdvancedList<Product,Analitic> Members

        List<IChainAdvanced<Product, Analitic>> IChainsAdvancedList<Product, Analitic>.GetLinks()
        {
            return ChainAdvanced<Product, Analitic>.CollectionSource(this);
        }

        List<IChainAdvanced<Product, Analitic>> IChainsAdvancedList<Product, Analitic>.GetLinks(int? kind)
        {
            return ChainAdvanced<Product, Analitic>.CollectionSource(this, kind);
        }
        /// <summary>
        /// ��������� ������������� ������
        /// </summary>
        /// <returns></returns>
        public List<IChainAdvanced<Product, Analitic>> GetLinkedAnalitics()
        {
            return ChainAdvanced<Product, Analitic>.CollectionSource(this);
        }
        List<ChainValueView> IChainsAdvancedList<Product, Analitic>.GetChainView()
        {
            return ChainValueView.GetView<Product, Analitic>(this);
        }
        #endregion

        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }
        /// <summary>
        /// ����� ������� �����
        /// </summary>
        /// <param name="hierarchyId">������������� �������� � ������ ���������� ������������ �����</param>
        /// <param name="userName">��� ������������</param>
        /// <param name="flags">�����</param>
        /// <param name="stateId">������������� ���������</param>
        /// <param name="name">������������</param>
        /// <param name="kindId">������������� ���� ������� �����</param>
        /// <param name="code">���</param>
        /// <param name="memo">����������</param>
        /// <param name="flagString">��������� ����</param>
        /// <param name="templateId">������������� �������</param>
        /// <param name="count">����������, �� ��������� 100</param>
        /// <param name="filter">�������������� ������</param>
        /// <param name="nomenclature">������������</param>
        /// <param name="articul">�������</param>
        /// <param name="cataloque">���������� �����</param>
        /// <param name="barcode">����� ���</param>
        /// <param name="unitId">������������� ������� ���������</param>
        /// <param name="weight"></param>
        /// <param name="height">������</param>
        /// <param name="width">������</param>
        /// <param name="depth">�������</param>
        /// <param name="size">����������</param>
        /// <param name="storagePeriod">���� ��������</param>
        /// <param name="manufacturerId">������������� ������������</param>
        /// <param name="tradeMarkId">������������� �������� ����� ��� �������� ������</param>
        /// <param name="brandId">������������� ������</param>
        /// <param name="productTypeId">������������� ���� ���������</param>
        /// <param name="pakcTypeId">������������� ���� ��������</param>
        /// <param name="colorId">������������� �����</param>
        /// <param name="useAndFilter">������������ � ������</param>
        /// <returns></returns>
        public List<Product> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Product> filter = null,
            string nomenclature = null, string articul = null, string cataloque = null, string barcode = null,
            int? unitId = null, decimal? weight = null, decimal? height = null, decimal? width = null, decimal? depth = null,
            string size = null, decimal? storagePeriod = null, int? manufacturerId = null, int? tradeMarkId = null, int? brandId = null, int? productTypeId = null, int? pakcTypeId = null,
            int? colorId = null,
            bool useAndFilter = false)
        {
            Product item = new Product { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("�� ������ ����� {0}, ������ ��� ������ ������ �� ���������������", GlobalMethodAlias.LoadList));
            }
            List<Product> collection = new List<Product>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindProcedure(GlobalMethodAlias.FindBy);
                        cmd.Parameters.Add(GlobalSqlParamNames.Count, SqlDbType.Int).Value = count;
                        if (hierarchyId != null && hierarchyId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;
                        if (userName != null && !string.IsNullOrEmpty(userName))
                            cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;
                        if (flags.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.Flags, SqlDbType.Int).Value = flags;
                        if (stateId.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateId;
                        if (!string.IsNullOrWhiteSpace(name))
                            cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = name;
                        if (kindId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindId;
                        if (!string.IsNullOrWhiteSpace(code))
                            cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;
                        if (!string.IsNullOrWhiteSpace(memo))
                            cmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255).Value = memo;
                        if (!string.IsNullOrWhiteSpace(flagString))
                            cmd.Parameters.Add(GlobalSqlParamNames.FlagString, SqlDbType.NVarChar, 50).Value = flagString;
                        if (templateId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TemplateId, SqlDbType.Int).Value = templateId;
                        if (useAndFilter)
                            cmd.Parameters.Add(GlobalSqlParamNames.UseAndFilter, SqlDbType.Bit).Value = true;

                        if (!string.IsNullOrWhiteSpace(nomenclature))
                            cmd.Parameters.Add(GlobalSqlParamNames.Nomenclature, SqlDbType.NVarChar, 50).Value = nomenclature;
                        if (!string.IsNullOrWhiteSpace(articul))
                            cmd.Parameters.Add(GlobalSqlParamNames.Articul, SqlDbType.NVarChar, 50).Value = articul;
                        if (!string.IsNullOrWhiteSpace(cataloque))
                            cmd.Parameters.Add(GlobalSqlParamNames.Cataloque, SqlDbType.NVarChar, 50).Value = cataloque;
                        if (!string.IsNullOrWhiteSpace(barcode))
                            cmd.Parameters.Add(GlobalSqlParamNames.Barcode, SqlDbType.NVarChar, 50).Value = barcode;

                        if (unitId.HasValue && unitId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.UnitId, SqlDbType.Int).Value = unitId.Value;
                        if (weight.HasValue && weight.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Weight, SqlDbType.Money).Value = weight.Value;
                        if (height.HasValue && height.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Height, SqlDbType.Money).Value = height.Value;
                        if (width.HasValue && width.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Width, SqlDbType.Money).Value = width.Value;
                        if (depth.HasValue && depth.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Depth, SqlDbType.Money).Value = depth.Value;

                        if (!string.IsNullOrWhiteSpace(size))
                            cmd.Parameters.Add(GlobalSqlParamNames.Size, SqlDbType.NVarChar, 50).Value = size;
                        if (storagePeriod.HasValue && storagePeriod.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.StoragePeriod, SqlDbType.Money).Value = storagePeriod.Value;
                        if (manufacturerId.HasValue && manufacturerId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.ManufacturerId, SqlDbType.Int).Value = manufacturerId.Value;
                        if (tradeMarkId.HasValue && tradeMarkId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TradeMarkId, SqlDbType.Int).Value = tradeMarkId.Value;
                        if (brandId.HasValue && brandId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.BrandId, SqlDbType.Int).Value = brandId.Value;
                        if (productTypeId.HasValue && productTypeId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.ProductTypeId, SqlDbType.Int).Value = productTypeId.Value;
                        if (pakcTypeId.HasValue && pakcTypeId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.PakcTypeId, SqlDbType.Int).Value = pakcTypeId.Value;
                        if (colorId.HasValue && colorId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.ColorId, SqlDbType.Int).Value = colorId.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Product { Workarea = Workarea };
                            item.Load(reader);
                            Workarea.Cashe.SetCasheData(item);
                            if (filter != null && filter.Invoke(item))
                                collection.Add(item);
                            else if (filter == null)
                                collection.Add(item);

                        }
                        reader.Close();

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
    }

    /// <summary>
    /// ������������� �������
    /// </summary>
    public sealed class ProductView
    {
        /// <summary>
        /// ������� �������
        /// </summary>
        public Workarea Workarea { get; set; }
        /// <summary>
        /// ������������� ������
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ��� ������
        /// </summary>
        public int KindValue { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        public string Nomenclature { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        public string Brend { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// �������������
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// ��� ������ � ���� ������
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// ������������� ���������
        /// </summary>
        public int StateId { get; set; }
        
        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="reader">������ ������ ������</param>
        public void Load(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            KindValue = reader.GetInt32(1);
            Name = reader.GetString(2);
            Nomenclature = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
            Manufacturer = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
            ProductType = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
            Group = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
            Memo = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
            Date = reader.IsDBNull(8) ? (DateTime)SqlDateTime.MinValue : reader.GetDateTime(8);
            Price = reader.IsDBNull(9) ? 0 : reader.GetDecimal(9);
            Qty = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);
            StateId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
        }
        /// <summary>
        /// �������������� � �����
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Product ConvertToProduct(ProductView c)
        {
            Product val = new Product { Workarea = c.Workarea, Id=c.Id };
            val.Load(c.Id);
            return val;
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="hierarchyId">������������� ��������</param>
        /// <returns></returns>
        public static List<ProductView> GetView(Workarea wa, int hierarchyId=0)
        {
            List<ProductView> collection = new List<ProductView>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Product.GetViewSimple";

                        //cmd.CommandText = wa.Empty<Product>().Entity.FindMethod("GetViewSimple").FullName;
                        SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int);
                        if (hierarchyId == 0)
                            prm.Value = DBNull.Value;
                        else
                            prm.Value = hierarchyId;
                        //cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = chainKindId;
                        //if (stateId != 1)
                        //    cmd.Parameters.Add(GlobalSqlParamNames.ChainStateId, SqlDbType.Int).Value = stateId;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ProductView item = new ProductView { Workarea = wa };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
        /// <summary>
        /// ����������� ���� �� ������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="priceNameId">������������� ���� ����</param>
        /// <param name="username">��� ������������</param>
        /// <returns>��������� ���� �������� ����� � ������� � �����, � ������� ��������� ������������</returns>
        public static List<ProductView> GetViewCurrentPrices(Workarea wa, int priceNameId, string username)
        {
            List<ProductView> collection = new List<ProductView>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Sales.ViewGetProductAllPrices";
                        
                        cmd.Parameters.Add(GlobalSqlParamNames.PrcNameId, SqlDbType.Int).Value=priceNameId;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = username;
                        
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ProductView item = new ProductView { Workarea = wa };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
    }
}
