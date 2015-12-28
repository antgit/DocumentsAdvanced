using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Внутренняя структура объекта "Устройство"</summary>
    internal struct DeviceStruct
    {
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>Устройство</summary>
    public sealed class Device : BaseCore<Device>, IChains<Device>, IReportChainSupport, IEquatable<Device>,
                                 IComparable, IComparable<Device>,
                                 IFacts<Device>,
                                 IChainsAdvancedList<Device, Knowledge>,
                                 IChainsAdvancedList<Device, Note>,
                                 ICodes<Device>, IHierarchySupport, ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>Устройство GPS, соответствует значению 1</summary>
        public const int KINDVALUE_DEVICE = 1;

        /// <summary>Устройство GPS, соответствует значению 7274497</summary>
        public const int KINDID_DEVICE = 7274497;
        
        
        // ReSharper restore InconsistentNaming

        #endregion
        bool IEquatable<Device>.Equals(Device other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Device otherObj = (Device)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(Device other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>Конструктор</summary>
        public Device()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Device;
        }
        protected override void CopyValue(Device template)
        {
            base.CopyValue(template);
            MyCompanyId = template.MyCompanyId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Device Clone(bool endInit)
        {
            Device obj = base.Clone(false);
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        private int _myCompanyId;
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит аналитика
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
        /// Моя компания, предприятие которому принадлежит аналитика
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

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion

        #region Состояние
        DeviceStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new DeviceStruct
                                  {
                                      MyCompanyId = _myCompanyId
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
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion
        #region База данных
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="reader">Объект <see cref="SqlDataReader"/> чтения данных</param>
        /// <param name="endInit">Закончить инициализацию</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _myCompanyId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);

        }
        #endregion

        #region ILinks<Device> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<Device>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Device>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Device> IChains<Device>.SourceList(int chainKindId)
        {
            return Chain<Device>.GetChainSourceList(this, chainKindId);
        }
        List<Device> IChains<Device>.DestinationList(int chainKindId)
        {
            return Chain<Device>.DestinationList(this, chainKindId);

        }
        #endregion

        #region IChainsAdvancedList<Agent,Knowledge> Members

        List<IChainAdvanced<Device, Knowledge>> IChainsAdvancedList<Device, Knowledge>.GetLinks()
        {
            return ((IChainsAdvancedList<Device, Knowledge>)this).GetLinks(59);
        }

        List<IChainAdvanced<Device, Knowledge>> IChainsAdvancedList<Device, Knowledge>.GetLinks(int? kind)
        {
            return GetLinkedKnowledges();
        }
        public List<IChainAdvanced<Device, Knowledge>> GetLinkedKnowledges()
        {
            List<IChainAdvanced<Device, Knowledge>> collection = new List<IChainAdvanced<Device, Knowledge>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Device>().Entity.FindMethod("LoadKnowledges").FullName;
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
                                ChainAdvanced<Device, Knowledge> item = new ChainAdvanced<Device, Knowledge> { Workarea = Workarea, Left = this };
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
        List<ChainValueView> IChainsAdvancedList<Device, Knowledge>.GetChainView()
        {
            return ChainValueView.GetView<Device, Knowledge>(this);
        }
        #endregion
        #region IChainsAdvancedList<Device,Note> Members

        List<IChainAdvanced<Device, Note>> IChainsAdvancedList<Device, Note>.GetLinks()
        {
            return ChainAdvanced<Device, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Device, Note>> IChainsAdvancedList<Device, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Device, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Device, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Device, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Device, Note>.GetChainView()
        {
            return ChainValueView.GetView<Device, Note>(this);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Device>> GetValues(bool allKinds)
        {
            return CodeHelper<Device>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Device>.GetView(this, true);
        }
        #endregion

        #region IFacts

        private List<FactView> _factView;
        public List<FactView> GetCollectionFactView()
        {
            return _factView ?? (_factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId));
        }

        public void RefreshFaсtView()
        {
            _factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId);
        }

        public FactView GetFactViewValue(string factCode, string columnCode)
        {
            return GetCollectionFactView().FirstOrDefault(s => s.FactNameCode == factCode && s.ColumnCode == columnCode);
        }

        public List<FactName> GetFactNames()
        {
            return FactHelper.GetFactNames(Workarea, EntityId);
        }
        #endregion

        private int? _firstHierarchy;
        /// <summary>
        /// Первая группа в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            return FirstHierarchy(false);
        }
        /// <summary>
        /// Первая группа в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy(bool refresh)
        {
            if (!refresh && (LastLoadPartial.HasValue && LastLoadPartial.Value.AddMinutes(10) > DateTime.Now))
            {
                if (!_firstHierarchy.HasValue) return null;
                return Workarea.Cashe.GetCasheData<Hierarchy>().Item(_firstHierarchy.Value);
            }
            _firstHierarchy = Hierarchy.FirstHierarchy<Device>(this);
            LastLoadPartial = DateTime.Now;
            if (!_firstHierarchy.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(_firstHierarchy.Value);
        }
        /// <summary>
        /// Поиск объекта
        /// </summary>
        /// <param name="hierarchyId">Идентификатор иерархии в которой осуществлять поиск</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="flags">Флаг</param>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <param name="name">Наименование</param>
        /// <param name="kindId">Идентификатор типа</param>
        /// <param name="code">Признак</param>
        /// <param name="memo">Наименование</param>
        /// <param name="flagString">Пользовательский флаг</param>
        /// <param name="templateId">Идентификатор шаблона</param>
        /// <param name="count">Количество, по умолчанию 100</param>
        /// <param name="filter">Дополнительный фильтр</param>
        /// <param name="useAndFilter">Использовать фильтр И</param>
        /// <returns></returns>
        public List<Device> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
                                     int? stateId = null, string name = null, int kindId = 0, string code = null,
                                     string memo = null, string flagString = null, int templateId = 0,
                                     int count = 100, Predicate<Device> filter = null, bool useAndFilter = false)
        {
            Device item = new Device { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Device> collection = new List<Device>();
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



                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Device { Workarea = Workarea };
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