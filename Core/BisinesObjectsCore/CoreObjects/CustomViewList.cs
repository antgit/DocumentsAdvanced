using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Список"</summary>
    internal struct CustomViewListStruct
    {
        /// <summary>Является ли список основанным на коллекции</summary>
        public bool IsCollectionBased;
        /// <summary>Идентификатор системного типа которому принадлежит список</summary>
        public int SourceEntityTypeId;
        /// <summary>Наименование используемого представления, функции или процедуры</summary>
        public string SystemName;
        /// <summary>Наименование процедуры обновления данных в строке представления</summary>
        public string SystemNameRefresh;
        /// <summary>Отображать панель группировки</summary>
        public bool GroupPanelVisible;
        /// <summary>Показывать строку автофильтра</summary>
        public bool AutoFilterVisible;
        /// <summary>Дополнительные настройки</summary>
        public string Options;
        /// <summary>Идентификатор Layout используемый для построения списка</summary>
        public int LayoutId;
        /// <summary>Отображать индикатор</summary>
        public bool ShowIndicator;
    }
    /// <summary>
    /// Список
    /// </summary>
    public sealed class CustomViewList : BaseCore<CustomViewList>, IChains<CustomViewList>, IEquatable<CustomViewList>
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Список, соответствует значению 1</summary>
        public const int KINDVALUE_LIST = 1;
        /// <summary>Поиск (хранимая процедура), соответствует значению 2</summary>
        public const int KINDVALUE_STOREDPROC = 2;
        /// <summary>Представление, соответствует значению 4</summary>
        public const int KINDVALUE_VIEW = 4;
        /// <summary>Табличные отчеты, соответствует значению 8</summary>
        public const int KINDVALUE_TABLE = 8;

        /// <summary>Список, соответствует значению 1376257</summary>
        public const int KINDID_LIST = 1376257;
        /// <summary>Поиск (хранимая процедура), соответствует значению 1376258</summary>
        public const int KINDID_STOREDPROC = 1376258;
        /// <summary>Представление, соответствует значению 1376260</summary>
        public const int KINDID_VIEW = 1376260;
        /// <summary>Табличные отчеты, соответствует значению 1376264</summary>
        public const int KINDID_TABLE = 1376264;
        // ReSharper restore InconsistentNaming
        #endregion

        public const string DEFAULT_LOOKUP_NAME = "DEFAULT_LOOKUP_NAME";

        bool IEquatable<CustomViewList>.Equals(CustomViewList other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public CustomViewList()
            : base()
        {
            EntityId = 21;
        }
        protected override void CopyValue(CustomViewList template)
        {
            base.CopyValue(template);
            AutoFilterVisible = template.AutoFilterVisible;
            GroupPanelVisible = template.GroupPanelVisible;
            IsCollectionBased = template.IsCollectionBased;
            Options = template.Options;
            SourceEntityTypeId = template.SourceEntityTypeId;
            SystemName = template.SystemName;
            SystemNameRefresh = template.SystemNameRefresh;
            LayoutId = template.LayoutId;
            ShowIndicator = template.ShowIndicator;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override CustomViewList Clone(bool endInit)
        {
            CustomViewList obj = base.Clone(false);
            obj.AutoFilterVisible = AutoFilterVisible;
            obj.GroupPanelVisible = GroupPanelVisible;
            obj.IsCollectionBased = IsCollectionBased;
            obj.Options = Options;
            obj.SourceEntityTypeId = SourceEntityTypeId;
            obj.SystemName = SystemName;
            obj.SystemNameRefresh = SystemNameRefresh;
            obj.LayoutId = LayoutId;
            obj.ShowIndicator = ShowIndicator;
            if (endInit)
                OnEndInit();
            return obj;
        }
        /// <summary>
        /// Сравнение объекта для службы обмена данными
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange(CustomViewList value)
        {
            if (!base.CompareExchange(value))
            {
                return false;
            }

            if (value.AutoFilterVisible != AutoFilterVisible)
                return false;
            if (value.GroupPanelVisible != GroupPanelVisible)
                return false;
            if (value.IsCollectionBased != IsCollectionBased)
                return false;
            if (!StringNullCompare(value.Options, Options))
                return false;
            if (value.SourceEntityTypeId != SourceEntityTypeId)
                return false;
            if (!StringNullCompare(value.SystemNameRefresh, SystemNameRefresh))
                return false;
            if (!StringNullCompare(value.SystemName, SystemName))
                return false;

            if (value.LayoutId != LayoutId)
                return false;
            if (value.ShowIndicator != ShowIndicator)
                return false;
                
            return true;
        }


        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);
            if (!String.IsNullOrEmpty(SystemName))
                writer.WriteAttributeString(GlobalPropertyNames.SystemName, SystemName);
            if (IsCollectionBased)
                writer.WriteAttributeString(GlobalPropertyNames.IsCollectionBased, XmlConvert.ToString(IsCollectionBased));
            if (!String.IsNullOrEmpty(SystemNameRefresh))
                writer.WriteAttributeString(GlobalPropertyNames.SystemNameRefresh, SystemNameRefresh);
            if(SourceEntityTypeId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.SourceEntityTypeId, XmlConvert.ToString(SourceEntityTypeId));
            if(GroupPanelVisible)
                writer.WriteAttributeString(GlobalPropertyNames.GroupPanelVisible, XmlConvert.ToString(GroupPanelVisible));
            if(AutoFilterVisible)
                writer.WriteAttributeString(GlobalPropertyNames.AutoFilterVisible, XmlConvert.ToString(AutoFilterVisible));
            if (!String.IsNullOrEmpty(Options))
                writer.WriteAttributeString(GlobalPropertyNames.Options, Options);
            if(UseLayout)
                writer.WriteAttributeString(GlobalPropertyNames.UseLayout, XmlConvert.ToString(UseLayout));
            if(LayoutId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.LayoutId, XmlConvert.ToString(LayoutId));

            writer.WriteStartElement(GlobalPropertyNames.Columns);
            foreach (CustomViewColumn a in Columns)
            {
                a.WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);
            
            if (reader.GetAttribute(GlobalPropertyNames.SystemName) != null)
                SystemName = reader.GetAttribute(GlobalPropertyNames.SystemName);

            if (reader.GetAttribute(GlobalPropertyNames.SystemNameRefresh) != null)
                SystemNameRefresh = reader.GetAttribute(GlobalPropertyNames.SystemNameRefresh);

            if (reader.GetAttribute(GlobalPropertyNames.Options) != null)
                Options = reader.GetAttribute(GlobalPropertyNames.Options);

            if (reader.GetAttribute(GlobalPropertyNames.IsCollectionBased) != null)
                IsCollectionBased = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.IsCollectionBased));

            if (reader.GetAttribute(GlobalPropertyNames.SourceEntityTypeId) != null)
                _sourceEntityTypeId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SourceEntityTypeId));

            if (reader.GetAttribute(GlobalPropertyNames.GroupPanelVisible) != null)
                _groupPanelVisible = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.GroupPanelVisible));

            if (reader.GetAttribute(GlobalPropertyNames.AutoFilterVisible) != null)
                _autoFilterVisible = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.AutoFilterVisible));

            if (reader.GetAttribute(GlobalPropertyNames.UseLayout) != null)
                _useLayout = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.UseLayout));

            if (reader.GetAttribute(GlobalPropertyNames.LayoutId) != null)
                _layoutId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.LayoutId));
            reader.Read();
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == GlobalPropertyNames.Columns)
            {
               //reader.Read(); // Skip ahead to next node
                 while (reader.MoveToContent() == XmlNodeType.Element)// &&
                     //Type.GetType(reader.LocalName).IsSubclassOf(typeof (CustomViewColumn)))
                 {
                     if (_columns == null)
                         _columns = new List<CustomViewColumn>();
                     CustomViewColumn a = new CustomViewColumn();
                     a.ReadXml(reader);
                     _columns.Add(a);
                     //reader.Read();
                     //          AnimalType = (Animal) Type.GetType(reader.LocalName);
                     //        Animal a = AnimalType.Assembly.CreateInstance(reader.LocalName);
                     //        a.ReadXml(reader);
                     //        Animals.Add(a.Key, a);
                     //        reader.Read(); // Skip to next animal (if there is one)
                 }
            }

        }
        #endregion

        #region Свойства

        private bool _showIndicator;
        /// <summary>
        /// Отображать индикатор
        /// </summary>
        public bool ShowIndicator
        {
            get { return _showIndicator; }
            set
            {
                if (value == _showIndicator) return;
                OnPropertyChanging(GlobalPropertyNames.ShowIndicator);
                _showIndicator = value;
                OnPropertyChanged(GlobalPropertyNames.ShowIndicator);
            }
        }
        
        private bool _isCollectionBased;
        /// <summary>
        /// Является ли список основанным на коллекции
        /// </summary>
        public bool IsCollectionBased
        {
            get { return _isCollectionBased; }
            set
            {
                if (_isCollectionBased == value) return;
                OnPropertyChanging(GlobalPropertyNames.IsCollectionBased);
                _isCollectionBased = value;
                OnPropertyChanged(GlobalPropertyNames.IsCollectionBased);
            }
        }
        private string _systemName;
        /// <summary>Наименование используемого представления, функции или процедуры</summary>
        public string SystemName
        {
            get { return _systemName; }
            set
            {
                if (value == _systemName) return;
                OnPropertyChanging(GlobalPropertyNames.SystemName);
                _systemName = value;
                OnPropertyChanged(GlobalPropertyNames.SystemName);
            }
        }
        private string _systemNameRefresh;
        /// <summary>
        /// Наименование процедуры обновления данных в строке представления
        /// </summary>
        public string SystemNameRefresh
        {
            get { return _systemNameRefresh; }
            set
            {
                if (value == _systemNameRefresh) return;
                OnPropertyChanging(GlobalPropertyNames.SystemNameRefresh);
                _systemNameRefresh = value;
                OnPropertyChanged(GlobalPropertyNames.SystemNameRefresh);
            }
        }

        private int _sourceEntityTypeId;
        /// <summary>
        /// Идентификатор системного типа которому принадлежит список
        /// </summary>
        public int SourceEntityTypeId
        {
            get { return _sourceEntityTypeId; }
            set
            {
                if (value == _sourceEntityTypeId) return;
                OnPropertyChanging(GlobalPropertyNames.SourceEntityTypeId);
                _sourceEntityTypeId = value;
                OnPropertyChanged(GlobalPropertyNames.SourceEntityTypeId);
            }
        }

        private EntityType _sourceEntityType;
        /// <summary>
        /// Системный тип которому принадлежит список
        /// </summary>
        public EntityType SourceEntityType
        {
            get
            {
                if (_sourceEntityTypeId== 0)
                    return null;
                if (_sourceEntityType == null)
                    _sourceEntityType = Workarea.CollectionEntities.Find(f => f.Id == _sourceEntityTypeId);
                else if (_sourceEntityType.Id != _sourceEntityTypeId)
                    _sourceEntityType = Workarea.CollectionEntities.Find(f => f.Id == _sourceEntityTypeId);
                return _sourceEntityType;
            }
            set
            {
                if (_sourceEntityType == value) return;
                OnPropertyChanging(GlobalPropertyNames.SourceEntityType);
                _sourceEntityType = value;
                _sourceEntityTypeId = _sourceEntityType == null ? 0 : _sourceEntityType.Id;
                OnPropertyChanged(GlobalPropertyNames.SourceEntityType);
            }
        }

        internal List<CustomViewColumn> _columns;
        /// <summary>
        /// Список колонок
        /// </summary>
        public List<CustomViewColumn> Columns
        {
            get
            {
                if (_columns != null)
                    return _columns;
                _columns = new List<CustomViewColumn>();
                _columns = GetColums();
                return _columns;
            }
        }

        private bool _groupPanelVisible;
        /// <summary>
        /// Отображать панель группировки
        /// </summary>
        public bool GroupPanelVisible
        {
            get { return _groupPanelVisible; }
            set
            {
                if (value == _groupPanelVisible) return;
                OnPropertyChanging(GlobalPropertyNames.GroupPanelVisible);
                _groupPanelVisible = value;
                OnPropertyChanged(GlobalPropertyNames.GroupPanelVisible);
            }
        }

        private bool _autoFilterVisible;
        /// <summary>
        /// Показывать строку автофильтра
        /// </summary>
        public bool AutoFilterVisible
        {
            get { return _autoFilterVisible; }
            set
            {
                if (value == _autoFilterVisible) return;
                OnPropertyChanging(GlobalPropertyNames.AutoFilterVisible);
                _autoFilterVisible = value;
                OnPropertyChanged(GlobalPropertyNames.AutoFilterVisible);
            }
        }
        
        private string _options;
        /// <summary>
        /// Дополнительные настройки
        /// </summary>
        public string Options
        {
            get { return _options; }
            set
            {
                if (value == _options) return;
                OnPropertyChanging(GlobalPropertyNames.Options);
                _options = value;
                OnPropertyChanged(GlobalPropertyNames.Options);
            }
        }

        private bool _useLayout;
        /// <summary>
        /// Использовать описание Layout
        /// </summary>
        public bool UseLayout
        {
            get { return _useLayout; }
            set
            {
                if (value == _useLayout) return;
                OnPropertyChanging(GlobalPropertyNames.UseLayout);
                _useLayout = value;
                OnPropertyChanged(GlobalPropertyNames.UseLayout);
            }
        }


        private int _layoutId;
        /// <summary>
        /// Идентификатор Layout используемый для построения списка
        /// </summary>
        public int LayoutId
        {
            get { return _layoutId; }
            set
            {
                if (value == _layoutId) return;
                OnPropertyChanging(GlobalPropertyNames.LayoutId);
                _layoutId = value;
                OnPropertyChanged(GlobalPropertyNames.LayoutId);
            }
        }


        private XmlStorage _layout;
        /// <summary>
        /// Описатель Layout
        /// </summary>
        public XmlStorage Layout
        {
            get
            {
                if (_layoutId == 0)
                    return null;
                if (_layout == null)
                    _layout = Workarea.Cashe.GetCasheData<XmlStorage>().Item(_layoutId);
                else if (_layout.Id != _layoutId)
                    _layout = Workarea.Cashe.GetCasheData<XmlStorage>().Item(_layoutId);
                return _layout;
            }
            set
            {
                if (_layout == value) return;
                OnPropertyChanging(GlobalPropertyNames.Layout);
                _layout = value;
                _layoutId = _layout == null ? 0 : _layout.Id;
                OnPropertyChanged(GlobalPropertyNames.Layout);
            }
        }
        
        
        #endregion

        #region Состояние
        CustomViewListStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new CustomViewListStruct
                                  {
                                      IsCollectionBased = _isCollectionBased,
                                      SourceEntityTypeId = _sourceEntityTypeId,
                                      SystemName = _systemName,
                                      SystemNameRefresh = _systemNameRefresh,
                                      AutoFilterVisible = _autoFilterVisible,
                                      GroupPanelVisible = _groupPanelVisible,
                                      LayoutId = _layoutId,
                                      Options = _options,
                                      ShowIndicator = _showIndicator
                                  };
                return true;
            }
            return false;
        }
        /// <summary>
        /// Востановить состояние
        /// </summary>
        public override void RestoreState()
        {
            base.RestoreState();
            _isCollectionBased = _baseStruct.IsCollectionBased;
            _sourceEntityTypeId = _baseStruct.SourceEntityTypeId;
            _systemName = _baseStruct.SystemName;
            _systemNameRefresh = _baseStruct.SystemNameRefresh;
            _autoFilterVisible = _baseStruct.AutoFilterVisible;
            _groupPanelVisible = _baseStruct.GroupPanelVisible;
            _layoutId = _baseStruct.LayoutId;
            _options = _baseStruct.Options;
            _showIndicator = _baseStruct.ShowIndicator;
            IsChanged = false;
        }
        #endregion

        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if ((FlagsValue & 1) != 1)
            {
                if (string.IsNullOrEmpty(_systemName) && _isCollectionBased == false)
                    throw new ValidateException("Список не основанный на коллекции должен иметь иметь представление или хранимую процедуру");
            }
        }

        #region База данных
        /// <summary>Загрузить экземпляр из базы данных</summary>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _sourceEntityTypeId = reader.IsDBNull(17) ? 0 : reader.GetInt16(17);
                _isCollectionBased = reader.GetBoolean(18);
                _systemName = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                _systemNameRefresh = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                _groupPanelVisible = reader.GetBoolean(21);
                _options = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                _autoFilterVisible = reader.GetBoolean(23);
                _useLayout = reader.GetBoolean(24);
                _layoutId = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                _showIndicator = reader.GetBoolean(26);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
            //Workarea.Cashe.GetCasheData<CustomViewList>().Add(this);
        }
        /// <summary>
        /// Установить значения параметров для комманды создания
        /// </summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            // TODO: Убрать в параметрах хранимой процедуры параметр GlobalSqlParamNames.CustomViewId
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt) { IsNullable = true };
            if (_sourceEntityTypeId== 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _sourceEntityTypeId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.IsCollectionBased, SqlDbType.Bit) {IsNullable = false, Value = _isCollectionBased};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SystemName, SqlDbType.NVarChar, 128)
            {
                IsNullable = true
            };
            if (string.IsNullOrEmpty(_systemName))
                prm.Value = DBNull.Value;
            else
                prm.Value = _systemName;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SystemNameRefresh, SqlDbType.NVarChar, 255)
            {
                IsNullable = true
            };
            if (string.IsNullOrEmpty(_systemNameRefresh))
                prm.Value = DBNull.Value;
            else
                prm.Value = _systemNameRefresh;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.GroupPanelVisible, SqlDbType.Bit) { IsNullable = false, Value = _groupPanelVisible };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Options, SqlDbType.NVarChar, -1 ) { IsNullable = true, Value = _options };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AutoFilterVisible, SqlDbType.Bit) { IsNullable = false, Value = _autoFilterVisible };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UseLayout, SqlDbType.Bit) { IsNullable = false, Value = _useLayout };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LayoutId, SqlDbType.Int)
            {
                IsNullable = true
            };
            if (_layoutId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _layoutId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ShowIndicator, SqlDbType.Bit) { IsNullable = false, Value = _showIndicator };
            sqlCmd.Parameters.Add(prm);
        } 
        #endregion

        /// <summary>
        /// Коллекция колонок списка
        /// </summary>
        /// <returns></returns>
        private List<CustomViewColumn> GetColums()
        {
            List<CustomViewColumn> collection = new List<CustomViewColumn>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.CustomViewListId, SqlDbType.Int).Value = Id;
                        cmd.CommandText = Entity.FindMethod("Core.CustomViewColumnsLoadByViewId").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        CustomViewColumn item;
                        while (reader.Read())
                        {
                            item = new CustomViewColumn { Workarea = Workarea, CustomViewList = this };
                            _columns.Add(item);
                            item.Load(reader);
                            Workarea.Cashe.SetCasheData(item);
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

        #region ILinks<CustomViewList> Members
        /// <summary>
        /// Связи списка
        /// </summary>
        /// <returns></returns>
        public List<IChain<CustomViewList>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи списка
        /// </summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<CustomViewList>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<CustomViewList> IChains<CustomViewList>.SourceList(int chainKindId)
        {
            return Chain<CustomViewList>.GetChainSourceList(this, chainKindId);
        }
        List<CustomViewList> IChains<CustomViewList>.DestinationList(int chainKindId)
        {
            return Chain<CustomViewList>.DestinationList(this, chainKindId);
        }
        #endregion

        #region Дополнительные методы
        ///// <summary>
        ///// Создание копии в базе данных
        ///// </summary>
        ///// <param name="value">Копируемый объект</param>
        ///// <returns></returns>
        //public static CustomViewList CreateCopy(CustomViewList value)
        //{
        //    CustomViewList item=null;
        //    using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
        //    {
        //        if (cnn == null) return null;
        //        try
        //        {
        //            using (SqlCommand cmd = cnn.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
        //                cmd.CommandText = value.FindProcedure(GlobalMethodAlias.Copy); //"Core.CustomViewListCopy";
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    item = new CustomViewList { Workarea = value.Workarea };
        //                    item.Load(reader);
        //                }
        //                reader.Close();
        //            }
        //        }
        //        finally
        //        {
        //            cnn.Close();
        //        }
        //    }
        //    return item;
        //}
        #endregion

        public List<CustomViewList> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<CustomViewList> filter = null,
            Int16? toEntityId=null, bool? isCollectionBased=null, string systemName=null, string systemNameRefresh=null, bool? groupPanelVisible=null, string options=null,
            bool? autoFilterVisible=null, bool? useLayout=null, int? layoutId=null,
            bool useAndFilter = false)
        {
            CustomViewList item = new CustomViewList { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<CustomViewList> collection = new List<CustomViewList>();
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

                        if (toEntityId.HasValue && toEntityId .Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.ToEntityId, SqlDbType.Int).Value = toEntityId;
                        if (isCollectionBased.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.IsCollectionBased, SqlDbType.Bit).Value = isCollectionBased;
                        if (!string.IsNullOrEmpty(systemName))
                            cmd.Parameters.Add(GlobalSqlParamNames.SystemName, SqlDbType.NVarChar, 255).Value = systemName;
                        if (groupPanelVisible.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.GroupPanelVisible, SqlDbType.Bit).Value = groupPanelVisible;
                        if (!string.IsNullOrEmpty(options))
                            cmd.Parameters.Add(GlobalSqlParamNames.Options, SqlDbType.NVarChar, 255).Value = options;
                        if (autoFilterVisible.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.AutoFilterVisible, SqlDbType.Bit).Value = autoFilterVisible;
                        if (useLayout.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.UseLayout, SqlDbType.Bit).Value = useLayout;
                        if (layoutId.HasValue && layoutId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.LayoutId, SqlDbType.Int).Value = layoutId;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new CustomViewList { Workarea = Workarea };
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

}