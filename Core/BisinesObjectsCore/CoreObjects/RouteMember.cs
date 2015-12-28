using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Внутренняя структура объекта "Участник маршрута"</summary>
    internal struct RouteMemberStruct
    {
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;

        /// <summary>Идентификатор устройства</summary>
        public int DeviceId;

        /// <summary>Идентификатор торгового представителя</summary>
        public int ManagerId;

        /// <summary>Идентификатор супервайзера</summary>
        public int SupervisorId;

        /// <summary>Идентификатор перевозчика</summary>
        public int ShippingId;
    }
    /// <summary>Участник маршрута</summary>
    public sealed class RouteMember : BaseCore<RouteMember>, IChains<RouteMember>, IReportChainSupport, IEquatable<RouteMember>,
                                      IComparable, IComparable<RouteMember>,
                                      IFacts<RouteMember>,
                                      IChainsAdvancedList<RouteMember, Knowledge>,
                                      IChainsAdvancedList<RouteMember, Note>,
                                      ICodes<RouteMember>, IHierarchySupport, ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>Участник маршрута автомобиль, соответствует значению 1</summary>
        public const int KINDVALUE_AUTO = 1;

        /// <summary>Участник маршрута автомобиль, соответствует значению 7340033</summary>
        public const int KINDID_AUTO = 7340033;


        // ReSharper restore InconsistentNaming

        #endregion
        bool IEquatable<RouteMember>.Equals(RouteMember other)
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
            RouteMember otherObj = (RouteMember)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(RouteMember other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>Конструктор</summary>
        public RouteMember()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.RouteMember;
        }
        protected override void CopyValue(RouteMember template)
        {
            base.CopyValue(template);
            MyCompanyId = template.MyCompanyId;
            DeviceId = template.DeviceId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override RouteMember Clone(bool endInit)
        {
            RouteMember obj = base.Clone(false);
            obj.MyCompanyId = MyCompanyId;
            obj.DeviceId = DeviceId;
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


        private int _deviceId;
        /// <summary>
        /// Идентификатор устройства
        /// </summary>
        public int DeviceId
        {
            get { return _deviceId; }
            set
            {
                if (value == _deviceId) return;
                OnPropertyChanging(GlobalPropertyNames.DeviceId);
                _deviceId = value;
                OnPropertyChanged(GlobalPropertyNames.DeviceId);
            }
        }


        private Device _device;
        /// <summary>
        /// Устройство
        /// </summary>
        public Device Device
        {
            get
            {
                if (_deviceId == 0)
                    return null;
                if (_device == null)
                    _device = Workarea.Cashe.GetCasheData<Device>().Item(_deviceId);
                else if (_device.Id != _deviceId)
                    _device = Workarea.Cashe.GetCasheData<Device>().Item(_deviceId);
                return _device;
            }
            set
            {
                if (_device == value) return;
                OnPropertyChanging(GlobalPropertyNames.Device);
                _device = value;
                _deviceId = _device == null ? 0 : _device.Id;
                OnPropertyChanged(GlobalPropertyNames.Device);
            }
        }

        private int _managerId;
        /// <summary>
        /// Идентификатор торгового представителя
        /// </summary>
        public int ManagerId
        {
            get { return _managerId; }
            set
            {
                if (value == _managerId) return;
                OnPropertyChanging(GlobalPropertyNames.ManagerId);
                _managerId = value;
                OnPropertyChanged(GlobalPropertyNames.ManagerId);
            }
        }

        private Agent _manager;
        /// <summary>
        /// Торговый представитель
        /// </summary>
        public Agent Manager
        {
            get
            {
                if (_managerId == 0)
                    return null;
                if (_manager == null)
                    _manager = Workarea.Cashe.GetCasheData<Agent>().Item(_managerId);
                else if (_manager.Id != _managerId)
                    _manager = Workarea.Cashe.GetCasheData<Agent>().Item(_managerId);
                return _manager;
            }
            set
            {
                if (_manager == value) return;
                OnPropertyChanging(GlobalPropertyNames.Manager);
                _manager = value;
                _managerId = _manager == null ? 0 : _manager.Id;
                OnPropertyChanged(GlobalPropertyNames.Manager);
            }
        }

        private int _supervisorId;
        /// <summary>
        /// Идентификатор супервайзера
        /// </summary>
        public int SupervisorId
        {
            get { return _supervisorId; }
            set
            {
                if (value == _supervisorId) return;
                OnPropertyChanging(GlobalPropertyNames.SupervisorId);
                _supervisorId = value;
                OnPropertyChanged(GlobalPropertyNames.SupervisorId);
            }
        }

        private Agent _supervisor;
        /// <summary>
        /// Супервайзер
        /// </summary>
        public Agent Supervisor
        {
            get
            {
                if (_supervisorId == 0)
                    return null;
                if (_supervisor == null)
                    _supervisor = Workarea.Cashe.GetCasheData<Agent>().Item(_supervisorId);
                else if (_supervisor.Id != _supervisorId)
                    _supervisor = Workarea.Cashe.GetCasheData<Agent>().Item(_supervisorId);
                return _supervisor;
            }
            set
            {
                if (_supervisor == value) return;
                OnPropertyChanging(GlobalPropertyNames.Supervisor);
                _supervisor = value;
                _supervisorId = _supervisor == null ? 0 : _supervisor.Id;
                OnPropertyChanged(GlobalPropertyNames.Supervisor);
            }
        }

        private int _shippingId;
        /// <summary>
        /// Идентификатор перевозчика
        /// </summary>
        public int ShippingId
        {
            get { return _shippingId; }
            set
            {
                if (value == _shippingId) return;
                OnPropertyChanging(GlobalPropertyNames.ShippingId);
                _shippingId = value;
                OnPropertyChanged(GlobalPropertyNames.ShippingId);
            }
        }

        private Agent _shipping;
        /// <summary>
        /// Перевозчик
        /// </summary>
        public Agent Shipping
        {
            get
            {
                if (_shippingId == 0)
                    return null;
                if (_shipping == null)
                    _shipping = Workarea.Cashe.GetCasheData<Agent>().Item(_shippingId);
                else if (_shipping.Id != _shippingId)
                    _shipping = Workarea.Cashe.GetCasheData<Agent>().Item(_shippingId);
                return _shipping;
            }
            set
            {
                if (_shipping == value) return;
                OnPropertyChanging(GlobalPropertyNames.Shipping);
                _shipping = value;
                _shippingId = _shipping == null ? 0 : _shipping.Id;
                OnPropertyChanged(GlobalPropertyNames.Shipping);
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
            if (_deviceId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DeviceId, XmlConvert.ToString(_deviceId));
            if (_managerId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ManagerId, XmlConvert.ToString(_managerId));
            if (_supervisorId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SupervisorId, XmlConvert.ToString(_supervisorId));
            if (_shippingId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ShippingId, XmlConvert.ToString(_shippingId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
            if (reader.GetAttribute(GlobalPropertyNames.DeviceId) != null)
                _deviceId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DeviceId));
            if (reader.GetAttribute(GlobalPropertyNames.ManagerId) != null)
                _managerId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ManagerId));
            if (reader.GetAttribute(GlobalPropertyNames.SupervisorId) != null)
                _supervisorId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SupervisorId));
            if (reader.GetAttribute(GlobalPropertyNames.ShippingId) != null)
                _shippingId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ShippingId));
        }
        #endregion

        #region Состояние
        RouteMemberStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new RouteMemberStruct
                                  {
                                      MyCompanyId = _myCompanyId,
                                      DeviceId = _deviceId,
                                      ManagerId = _managerId,
                                      SupervisorId = _supervisorId,
                                      ShippingId = _shippingId
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
            DeviceId = _baseStruct.DeviceId;
            ManagerId = _baseStruct.ManagerId;
            SupervisorId = _baseStruct.SupervisorId;
            ShippingId = _baseStruct.ShippingId;
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
                _deviceId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _myCompanyId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _managerId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _supervisorId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _shippingId = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
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

            prm = new SqlParameter(GlobalSqlParamNames.DeviceId, SqlDbType.Int) { IsNullable = true, Value = (_deviceId == 0 ? DBNull.Value : (object)_deviceId )};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ManagerId, SqlDbType.Int) { IsNullable = true, Value = (_managerId == 0 ? DBNull.Value : (object)_managerId) };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SupervisorId, SqlDbType.Int) { IsNullable = true, Value = (_supervisorId == 0 ? DBNull.Value : (object)_supervisorId) };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ShippingId, SqlDbType.Int) { IsNullable = true, Value = (_shippingId == 0 ? DBNull.Value : (object)_shippingId) };
            sqlCmd.Parameters.Add(prm);

        }
        #endregion

        #region ILinks<RouteMember> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<RouteMember>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<RouteMember>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<RouteMember> IChains<RouteMember>.SourceList(int chainKindId)
        {
            return Chain<RouteMember>.GetChainSourceList(this, chainKindId);
        }
        List<RouteMember> IChains<RouteMember>.DestinationList(int chainKindId)
        {
            return Chain<RouteMember>.DestinationList(this, chainKindId);

        }
        #endregion

        #region IChainsAdvancedList<Agent,Knowledge> Members

        List<IChainAdvanced<RouteMember, Knowledge>> IChainsAdvancedList<RouteMember, Knowledge>.GetLinks()
        {
            return ((IChainsAdvancedList<RouteMember, Knowledge>)this).GetLinks(59);
        }

        List<IChainAdvanced<RouteMember, Knowledge>> IChainsAdvancedList<RouteMember, Knowledge>.GetLinks(int? kind)
        {
            return GetLinkedKnowledges();
        }
        public List<IChainAdvanced<RouteMember, Knowledge>> GetLinkedKnowledges()
        {
            List<IChainAdvanced<RouteMember, Knowledge>> collection = new List<IChainAdvanced<RouteMember, Knowledge>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<RouteMember>().Entity.FindMethod("LoadKnowledges").FullName;
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
                                ChainAdvanced<RouteMember, Knowledge> item = new ChainAdvanced<RouteMember, Knowledge> { Workarea = Workarea, Left = this };
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
        List<ChainValueView> IChainsAdvancedList<RouteMember, Knowledge>.GetChainView()
        {
            return ChainValueView.GetView<RouteMember, Knowledge>(this);
        }
        #endregion
        #region IChainsAdvancedList<RouteMember,Note> Members

        List<IChainAdvanced<RouteMember, Note>> IChainsAdvancedList<RouteMember, Note>.GetLinks()
        {
            return ChainAdvanced<RouteMember, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<RouteMember, Note>> IChainsAdvancedList<RouteMember, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<RouteMember, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<RouteMember, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<RouteMember, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<RouteMember, Note>.GetChainView()
        {
            return ChainValueView.GetView<RouteMember, Note>(this);
        }
        #endregion

        #region ICodes
        public List<CodeValue<RouteMember>> GetValues(bool allKinds)
        {
            return CodeHelper<RouteMember>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<RouteMember>.GetView(this, true);
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
            _firstHierarchy = Hierarchy.FirstHierarchy<RouteMember>(this);
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
        public List<RouteMember> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
                                   int? stateId = null, string name = null, int kindId = 0, string code = null,
                                   string memo = null, string flagString = null, int templateId = 0,
                                   int count = 100, Predicate<RouteMember> filter = null, bool useAndFilter = false)
        {
            RouteMember item = new RouteMember { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<RouteMember> collection = new List<RouteMember>();
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
                            item = new RouteMember { Workarea = Workarea };
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