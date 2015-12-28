using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
        /// <summary>Внутренняя структура объекта "Подразделение"</summary>
    internal struct DepatmentStruct
    {
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
        /// <summary>Идентификатор руководителя</summary>
        public int DepatmentHeadId;
        /// <summary>Идентификатор заместителя</summary>
        public int DepatmentSubHeadId;
        /// <summary>Основной телефон отдела</summary>
        public string Phone;    
    }

    /// <summary>Подразделение или отдел</summary>
    public sealed class Depatment : BaseCore<Depatment>, IChains<Depatment>, IReportChainSupport, IEquatable<Depatment>,
                                    IComparable, IComparable<Depatment>,
                                    IFacts<Depatment>,
                                    IChainsAdvancedList<Depatment, Note>,
                                    IChainsAdvancedList<Depatment, Agent>,
                                    IChainsAdvancedList<Depatment, Equipment>,
                                    ICodes<Depatment>, IHierarchySupport, ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>Отдел 1</summary>
		public const int KINDVALUE_DEPATMENT = 1;
        /// <summary>Цех 2</summary>
		public const int KINDVALUE_SHOP = 2;

		/// <summary>Отдел 7143425</summary>
		public const int KINDID_DEPATMENT = 7143425;
		/// <summary>Цех 7143426</summary>
		public const int KINDID_SHOP = 7143426;

        /// <summary>Резолюция подписи "Не установлено"</summary>
        public const string SYSTEM_SIGN_EMPTY = "SYSTEM_SIGN_EMPTY";
        /// <summary>Резолюция подписи "Отклоняю"</summary>
        public const string SYSTEM_SIGN_NO = "SYSTEM_SIGN_NO";
        /// <summary>Резолюция подписи "Согласен"</summary>
        public const string SYSTEM_SIGN_YES = "SYSTEM_SIGN_YES";

        /// <summary>Важность "Нормальная"</summary>
        public const string SYSTEM_PRIORITY_NORMAL = "SYSTEM_PRIORITY_NORMAL";

        // ReSharper restore InconsistentNaming

        #endregion
        bool IEquatable<Depatment>.Equals(Depatment other)
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
            Depatment otherObj = (Depatment)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(Depatment other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>Конструктор</summary>
        public Depatment()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Depatment;
        }
        protected override void CopyValue(Depatment template)
        {
            base.CopyValue(template);
            MyCompanyId = template.MyCompanyId;
            DepatmentHeadId = template.DepatmentHeadId;
            DepatmentSubHeadId = template.DepatmentSubHeadId;
            Phone = template.Phone;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Depatment Clone(bool endInit)
        {
            Depatment obj = base.Clone(false);
            obj.MyCompanyId = MyCompanyId;
            obj.DepatmentHeadId = DepatmentHeadId;
            obj.DepatmentSubHeadId = DepatmentSubHeadId;
            obj.Phone = Phone;
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

        private int _depatmentHeadId;
        /// <summary>
        /// Идентификатор руководителя
        /// </summary>
        public int DepatmentHeadId
        {
            get { return _depatmentHeadId; }
            set
            {
                if (value == _depatmentHeadId) return;
                OnPropertyChanging(GlobalPropertyNames.DepatmentHeadId);
                _depatmentHeadId = value;
                OnPropertyChanged(GlobalPropertyNames.DepatmentHeadId);
            }
        }


        private Agent _depatmentHead;
        /// <summary>
        /// Руководитель
        /// </summary>
        public Agent DepatmentHead
        {
            get
            {
                if (_depatmentHeadId == 0)
                    return null;
                if (_depatmentHead == null)
                    _depatmentHead = Workarea.Cashe.GetCasheData<Agent>().Item(_depatmentHeadId);
                else if (_depatmentHead.Id != _depatmentHeadId)
                    _depatmentHead = Workarea.Cashe.GetCasheData<Agent>().Item(_depatmentHeadId);
                return _depatmentHead;
            }
            set
            {
                if (_depatmentHead == value) return;
                OnPropertyChanging(GlobalPropertyNames.DepatmentHead);
                _depatmentHead = value;
                _depatmentHeadId = _depatmentHead == null ? 0 : _depatmentHead.Id;
                OnPropertyChanged(GlobalPropertyNames.DepatmentHead);
            }
        }
        

        private Agent _depatmentSubHead;
        /// <summary>
        /// Заместитель
        /// </summary>
        public Agent DepatmentSubHead
        {
            get
            {
                if (_depatmentSubHeadId == 0)
                    return null;
                if (_depatmentSubHead == null)
                    _depatmentSubHead = Workarea.Cashe.GetCasheData<Agent>().Item(_depatmentSubHeadId);
                else if (_depatmentSubHead.Id != _depatmentSubHeadId)
                    _depatmentSubHead = Workarea.Cashe.GetCasheData<Agent>().Item(_depatmentSubHeadId);
                return _depatmentSubHead;
            }
            set
            {
                if (_depatmentSubHead == value) return;
                OnPropertyChanging(GlobalPropertyNames.DepatmentSubHead);
                _depatmentSubHead = value;
                _depatmentSubHeadId = _depatmentSubHead == null ? 0 : _depatmentSubHead.Id;
                OnPropertyChanged(GlobalPropertyNames.DepatmentSubHead);
            }
        }
        

        private int _depatmentSubHeadId;
        /// <summary>
        /// Идентификатор заместителя
        /// </summary>
        public int DepatmentSubHeadId
        {
            get { return _depatmentSubHeadId; }
            set
            {
                if (value == _depatmentSubHeadId) return;
                OnPropertyChanging(GlobalPropertyNames.DepatmentSubHeadId);
                _depatmentSubHeadId = value;
                OnPropertyChanged(GlobalPropertyNames.DepatmentSubHeadId);
            }
        }


        private string _phone;
        /// <summary>
        /// Основной телефон отдела
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (value == _phone) return;
                OnPropertyChanging(GlobalPropertyNames.Phone);
                _phone = value;
                OnPropertyChanged(GlobalPropertyNames.Phone);
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
            if (_depatmentHeadId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DepatmentHeadId, _depatmentHeadId.ToString());
            if (_depatmentSubHeadId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DepatmentSubHeadId, _depatmentSubHeadId.ToString());
            if (!string.IsNullOrEmpty(_phone))
                writer.WriteAttributeString(GlobalPropertyNames.Phone, _phone);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
            if (reader.GetAttribute(GlobalPropertyNames.DepatmentHeadId) != null) _depatmentHeadId = Int32.Parse(reader[GlobalPropertyNames.DepatmentHeadId]);
            if (reader.GetAttribute(GlobalPropertyNames.DepatmentSubHeadId) != null) _depatmentSubHeadId = Int32.Parse(reader[GlobalPropertyNames.DepatmentSubHeadId]);
            if (reader.GetAttribute(GlobalPropertyNames.Phone) != null) _phone = reader[GlobalPropertyNames.Phone];
        }
        #endregion

        #region Состояние
        DepatmentStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new DepatmentStruct
                                  {
                                      MyCompanyId = _myCompanyId,
                                      DepatmentHeadId = _depatmentHeadId,
                                      DepatmentSubHeadId = _depatmentSubHeadId
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
            DepatmentHeadId = _baseStruct.DepatmentHeadId;
            DepatmentSubHeadId = _baseStruct.DepatmentSubHeadId;
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
                _depatmentHeadId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _depatmentSubHeadId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _phone = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
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


            prm = new SqlParameter(GlobalSqlParamNames.DepatmentHeadId, SqlDbType.Int) { IsNullable = true };
            if (_depatmentHeadId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _depatmentHeadId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DepatmentSubHeadId, SqlDbType.Int) { IsNullable = true };
            if (_depatmentSubHeadId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _depatmentSubHeadId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Phone, SqlDbType.NVarChar, 50) { IsNullable = true };
            if (string.IsNullOrEmpty(_phone))
                prm.Value = DBNull.Value;
            else
                prm.Value = _phone;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Depatment> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<Depatment>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Depatment>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Depatment> IChains<Depatment>.SourceList(int chainKindId)
        {
            return Chain<Depatment>.GetChainSourceList(this, chainKindId);
        }
        List<Depatment> IChains<Depatment>.DestinationList(int chainKindId)
        {
            return Chain<Depatment>.DestinationList(this, chainKindId);

        }
        #endregion

        #region IChainsAdvancedList<Depatment,Note> Members

        List<IChainAdvanced<Depatment, Note>> IChainsAdvancedList<Depatment, Note>.GetLinks()
        {
            return ChainAdvanced<Depatment, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Depatment, Note>> IChainsAdvancedList<Depatment, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Depatment, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Depatment, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Depatment, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Depatment, Note>.GetChainView()
        {
            return ChainValueView.GetView<Depatment, Note>(this);
        }
        #endregion

        #region IChainsAdvancedList<Depatment,Agent> Members

        List<IChainAdvanced<Depatment, Agent>> IChainsAdvancedList<Depatment, Agent>.GetLinks()
        {
            return ChainAdvanced<Depatment, Agent>.CollectionSource(this);
        }

        List<IChainAdvanced<Depatment, Agent>> IChainsAdvancedList<Depatment, Agent>.GetLinks(int? kind)
        {
            return ChainAdvanced<Depatment, Agent>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Depatment, Agent>> GetLinkedAgents(int? kind = null)
        {
            return ChainAdvanced<Depatment, Agent>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Depatment, Agent>.GetChainView()
        {
            return ChainValueView.GetView<Depatment, Agent>(this);
        }
        #endregion

        #region IChainsAdvancedList<Depatment,Equipment> Members

        List<IChainAdvanced<Depatment, Equipment>> IChainsAdvancedList<Depatment, Equipment>.GetLinks()
        {
            return ChainAdvanced<Depatment, Equipment>.CollectionSource(this);
        }

        List<IChainAdvanced<Depatment, Equipment>> IChainsAdvancedList<Depatment, Equipment>.GetLinks(int? kind)
        {
            return ChainAdvanced<Depatment, Equipment>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Depatment, Equipment>> GetLinkedEquipment(int? kind = null)
        {
            return ChainAdvanced<Depatment, Equipment>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Depatment, Equipment>.GetChainView()
        {
            return ChainValueView.GetView<Depatment, Equipment>(this);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Depatment>> GetValues(bool allKinds)
        {
            return CodeHelper<Depatment>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Depatment>.GetView(this, true);
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
            _firstHierarchy = Hierarchy.FirstHierarchy<Depatment>(this);
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
        public List<Depatment> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
                                     int? stateId = null, string name = null, int kindId = 0, string code = null,
                                     string memo = null, string flagString = null, int templateId = 0,
                                     int count = 100, Predicate<Depatment> filter = null, bool useAndFilter = false, int? myCompanyId = null)
        {
            Depatment item = new Depatment { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Depatment> collection = new List<Depatment>();
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
                        if (myCompanyId.HasValue && myCompanyId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = myCompanyId;


                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Depatment { Workarea = Workarea };
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