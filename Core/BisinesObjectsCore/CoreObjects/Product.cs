using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Объект учета"</summary>
    internal struct ProductStruct
    {
        /// <summary>Номенклатурный номер</summary>
        public string Nomenclature;
        /// <summary>Артикульный номер</summary>
        public string Articul;
        /// <summary>Каталожный номер</summary>
        public string Catalogue;
        /// <summary>Штрих код</summary>
        public string Barcode;
        /// <summary>Идентификатор единицы измерения</summary>
        public int UnitId;
        /// <summary>Вес</summary>
        public decimal Weight;
        /// <summary>Высота</summary>
        public decimal Height;
        /// <summary> Ширина</summary>
        public decimal Width;
        /// <summary>Глубина</summary>
        public decimal Depth;
        /// <summary>Типоразмер</summary>
        public string Size;
        /// <summary>Период хранения</summary>
        public decimal? StoragePeriod;
        /// <summary>Идентификатор изготовителя</summary>
        public int ManufactureId;
        /// <summary>Идентификатор торговой марки</summary>
        public int TradeMarkId;
        /// <summary>Идентификатор бренда</summary>
        public int BrandId;
        /// <summary>Идентификатор вид продукции</summary>
        public int ProductTypeId;
        /// <summary>Идентификатор упаковка</summary>
        public int PakcTypeId;
        /// <summary>Идентификатор цвета</summary>
        public int ColorId;
    }
    /// <summary>
    /// Объект учета - товар, основные средства
    /// </summary>
    public sealed class Product : BaseCore<Product>, IChains<Product>, IReportChainSupport, IEquatable<Product>, IFacts<Product>,
        ICodes<Product>,
        IChainsAdvancedList<Product, FileData>,
        IChainsAdvancedList<Product, Knowledge>,
        IChainsAdvancedList<Product, Note>,
        IChainsAdvancedList<Product, Analitic>,
        IHierarchySupport, ICompanyOwner
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Неизвестный тип, соответствует значению 0</summary>
        public const int KINDVALUE_DEFAULT = 0;
        /// <summary>Товар, соответствует значению 1</summary>
        public const int KINDVALUE_PRODUCT = 1;
        /// <summary>Денежный, соответствует значению 2</summary>
        public const int KINDVALUE_MONEY = 2;
        /// <summary>Услуги, соответствует значению 3</summary>
        public const int KINDVALUE_SERVICE = 3;
        /// <summary>Основные средства, соответствует значению 4</summary>
        public const int KINDVALUE_ASSETS = 4;
        /// <summary>Тара, соответствует значению 5</summary>
        public const int KINDVALUE_PACK = 5;
        /// <summary>Автомобиль, соответствует значению 6</summary>
        public const int KINDVALUE_AUTO = 6;
        /// <summary>МБП, соответствует значению 7</summary>
        public const int KINDVALUE_MBP = 7;
        /// <summary>Компьютер, соответствует значению 8</summary>
        public const int KINDVALUE_COMPUTER = 8;
        /// <summary>Оргтехника, соответствует значению 8</summary>
        public const int KINDVALUE_ORGTEX = 9;
        /// <summary>Готовая продукция, соответствует значению 10</summary>
        public const int KINDVALUE_FINISHPRODUCTION = 10;
        /// <summary>Сырье(материалы), соответствует значению 11</summary>
        public const int KINDVALUE_RAWMATERIAL = 11;
        

        /// <summary>Товар, соответствует значению 65537</summary>
        public const int KINDID_PRODUCT = 65537;
        /// <summary>Денежный, соответствует значению 65538</summary>
        public const int KINDID_MONEY = 65538;
        /// <summary>Услуги, соответствует значению 65539</summary>
        public const int KINDID_SERVICE = 65539;
        /// <summary>Основные средства, соответствует значению 65540</summary>
        public const int KINDID_ASSETS = 65540;
        /// <summary>Тара, соответствует значению 65541</summary>
        public const int KINDID_PACK = 65541;
        /// <summary>Автомобиль, соответствует значению 65542</summary>
        public const int KINDID_AUTO = 65542;
        /// <summary>МБП, соответствует значению 65543</summary>
        public const int KINDID_MBP = 65543;
        /// <summary>Компьютер, соответствует значению 65544</summary>
        public const int KINDID_COMPUTER = 65544;
        /// <summary>Оргтехника, соответствует значению 65545</summary>
        public const int KINDID_ORGTEX = 65545;
        /// <summary>Готовая продукция, соответствует значению 65546</summary>
        public const int KINDID_FINISHPRODUCTION = 65546;
        /// <summary>Сырье(материалы), соответствует значению 65547</summary>
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
        /// Коллекция объектов учета (товаров) 
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="kind"></param>
        /// <param name="likeName">Подстрока поиска номенклатуры</param>
        /// <param name="count">Ограничение максимального количества записей</param>
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
        /// Коллекция ценовых диапазонов для товара
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

        /// <summary>Конструктор</summary>
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
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
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
        #region Свойства
        private string _nomenclature;
        /// <summary>
        /// Номенклатурный номер
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
        /// Артикульный номер
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
        /// Каталожный номер
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
        /// Штрих код
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
        /// Идентификатор единицы измерения
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
        /// Единица измерения 
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
        /// Вес
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
        /// Высота
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
        /// Ширина
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
        /// Глубина
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
        /// Типоразмер
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
        /// Период хранения
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
        /// Идентификатор изготовителя
        /// </summary>
        /// <remarks>Основными значениями для данного свойства являются идентификаторы корреспондентов группы "Производители"</remarks>
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
        /// Изготовитель 
        /// </summary>
        /// <remarks>Основными значениями для данного свойства являются идентификаторы корреспондентов группы "Производители"</remarks>
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
        /// Идентификатор торговой марки
        /// </summary>
        /// <remarks>Основными значениями для данного свойства являются идентификаторы аналитики группы "Товарная группа"</remarks>
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
        /// Торговая марка или товарная группа
        /// </summary>
        /// <remarks>Основными значениями для данного свойства являются аналитика группы "Товарная группа"</remarks>
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
        /// Идентификатор бренда
        /// </summary>
        /// <remarks>Основными значениями для данного свойства являются идентификаторы аналитики группы "Бренд"</remarks>
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
        /// Бренд
        /// </summary>
        /// <remarks>Основными значениями для данного свойства является аналитика группы "Бренд"</remarks>
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
        /// Идентификатор вид продукции
        /// </summary>
        /// <remarks>Основными значениями для данного свойства являются идентификаторы аналитики группы "Вид продукции"</remarks>
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
        /// Вид продукции
        /// </summary>
        /// <remarks>Основными значениями для данного свойства является аналитика группы "Вид продукции"</remarks>
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
        /// Идентификатор упаковки
        /// </summary>
        /// <remarks>Основными значениями для данного свойства являются идентификаторы аналитики группы "Вид упаковки"</remarks>
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
        /// Упаковка
        /// </summary>
        /// <remarks>Основными значениями для данного свойства является аналитика группы "Вид упаковки"</remarks>
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
        /// Идентификатор цвета
        /// </summary>
        /// <remarks>Основными значениями для данного свойства являются идентификаторы аналитики группы "Цвет"</remarks>
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
        /// Цвет
        /// </summary>
        /// <remarks>Основными значениями для данного свойства является аналитика группы "Цвет"</remarks>
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
        /// Идентификатор предприятия, которому принадлежит объект учета
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
        /// Моя компания, предприятие которому принадлежит объект учета
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

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
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
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
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
		/// <summary>Данные автомобиля</summary>
		public Auto Auto
		{
			get
			{
				if (_auto == null)
					RefreshAuto();
				return _auto;
			}
		}
		/// <summary>Обновить информацию автомобиля</summary>
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
		/// Является автомобилем
		/// </summary>
		public bool IsAuto
		{
			get { return (KindValue == Product.KINDVALUE_AUTO); }
		}
		#endregion

		#region Дополнительные свойства
		private List<ProductUnit> _units;
        /// <summary>
        /// Коллекция производных единиц измерения
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
        /// Коллекция единиц измерения объекта
        /// </summary>
        /// <param name="product">Объект учета <see cref="BusinessObjects.Product"/></param>
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
        /// <summary>Партии товара</summary>
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
        /// Обновить данные
        /// </summary>
        /// <param name="all">Обновлять все связанное</param>
        /// <remarks>При обновлении всех связанных данных обновляются данные о партиях товара и производных единицах измерения</remarks>
        public override void Refresh(bool all)
        {
            base.Refresh(all);
            if (!all) return;
            RefreshSeries();
            RefreshFaсtView();
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

        #region Состояние
        ProductStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
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

        /// <summary>Востановить текущее состояние объекта</summary>
        /// <remarks>Востановление состояние возможно только после выполнения сосхранения состояния</remarks>
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

        #region База данных
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Признак окончания загрузки</param>
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
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
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
        /// Связи товара
        /// </summary>
        /// <returns></returns>
        public List<IChain<Product>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи товара
        /// </summary>
        /// <param name="kind">Тип связи</param>
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

        public void RefreshFaсtView()
        {
            _factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId);
        }
        /// <summary>
        /// Представление значений фактов
        /// </summary>
        /// <param name="factCode">Код наименования факта</param>
        /// <param name="columnCode">Код колонки факта</param>
        /// <returns></returns>
        public FactView GetFactViewValue(string factCode, string columnCode)
        {
            return GetCollectionFactView().FirstOrDefault(s => s.FactNameCode == factCode && s.ColumnCode == columnCode);
        }
        /// <summary>
        /// Коллекция наименований фактов для объекта учета
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
        /// Коллекция связанных файлов с объектом учета
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
        /// Коллекция связанных статей базы знаний с объектом учета
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
        /// Коллекция цепоцек "объект учета-примечания"
        /// </summary>
        /// <param name="kind">Тип связи, соответствует значению идентификатора типа связи</param>
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
        /// Связанные аналитические данные
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
        /// Поиск объекта учета
        /// </summary>
        /// <param name="hierarchyId">Идентификатор иерархии в котрой необходимо осуществлять поиск</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="flags">Флаги</param>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <param name="name">Наименование</param>
        /// <param name="kindId">Идентификатор типа объекта учета</param>
        /// <param name="code">Код</param>
        /// <param name="memo">Примечание</param>
        /// <param name="flagString">Строковый флаг</param>
        /// <param name="templateId">Идентификатор шаблона</param>
        /// <param name="count">Количество, по умолчанию 100</param>
        /// <param name="filter">Дополнительный фильтр</param>
        /// <param name="nomenclature">Номенклатура</param>
        /// <param name="articul">Артикул</param>
        /// <param name="cataloque">Каталожный номер</param>
        /// <param name="barcode">Штрих код</param>
        /// <param name="unitId">Идентификатор единицы измерения</param>
        /// <param name="weight"></param>
        /// <param name="height">Высота</param>
        /// <param name="width">Ширина</param>
        /// <param name="depth">Глубина</param>
        /// <param name="size">Типоразмер</param>
        /// <param name="storagePeriod">Срок хранения</param>
        /// <param name="manufacturerId">Идентификатор изготовителя</param>
        /// <param name="tradeMarkId">Идентификатор торговой марки или торговой группы</param>
        /// <param name="brandId">Идентификатор бренда</param>
        /// <param name="productTypeId">Идентификатор вида продукции</param>
        /// <param name="pakcTypeId">Идентификатор вида упаковки</param>
        /// <param name="colorId">Идентификатор цвета</param>
        /// <param name="useAndFilter">Использовать И фильтр</param>
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
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
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
    /// Представление товаров
    /// </summary>
    public sealed class ProductView
    {
        /// <summary>
        /// Рабочая область
        /// </summary>
        public Workarea Workarea { get; set; }
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Тип товара
        /// </summary>
        public int KindValue { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Номенклатура
        /// </summary>
        public string Nomenclature { get; set; }
        /// <summary>
        /// Бренд
        /// </summary>
        public string Brend { get; set; }
        /// <summary>
        /// Группа
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// Производитель
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// Тип товара в виде строки
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// Идентификатор состояния
        /// </summary>
        public int StateId { get; set; }
        
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="reader">Объект чтения данных</param>
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
        /// Преобразование в товар
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
        /// Представление
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="hierarchyId">Идентификатор иерархии</param>
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
        /// Действующие цены на товары
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="priceNameId">Идентификатор вида цены</param>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Коллекция всех объектов учета с данными о ценах, в области видимости пользователя</returns>
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
