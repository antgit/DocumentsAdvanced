using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Корреспондент"</summary>
    internal struct AgentStruct
    {
        /// <summary>Налоговый номер</summary>
        public string CodeTax;
        /// <summary>Адрес юридический</summary>
        public string AddressLegal;
        /// <summary>Адрес фактический</summary>
        public string AddressPhysical;
        /// <summary>Адрес фактический</summary>
        public decimal AmmountTrust;
        /// <summary>Максимальное количество дней отсрочки</summary>
        public int TimeDelay;
        /// <summary>Телефон</summary>
        public string Phone;
        /// <summary>
        /// Идентификатор компании владельца
        /// </summary>
        public int MyCompanyId;
    }
    /// <summary>Корреспондент</summary>
    public sealed class Agent : BaseCore<Agent>, IChains<Agent>, IReportChainSupport, IEquatable<Agent>, 
        IFacts<Agent>, ICodes<Agent>,
        IChainsAdvancedList<Agent, FileData>,
        IChainsAdvancedList<Agent, Knowledge>,
        IChainsAdvancedList<Agent, Note>,
        IChainsAdvancedList<Agent, Analitic>,
        IHierarchySupport, ICompanyOwner
        
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Предприятие, соответствует значению 196609</summary>
        public const int KINDID_COMPANY = 196609;
        /// <summary>Физ лицо, соответствует значению 196610</summary>
        public const int KINDID_PEOPLE = 196610;
        /// <summary>Склад поставщика или клиента, соответствует значению 196611</summary>
        public const int KINDID_STORE = 196611;
        /// <summary>Мое предприятие, соответствует значению 196612</summary>
        public const int KINDID_MYCOMPANY = 196612;
        /// <summary>Наш склад, соответствует значению 196613</summary>
        public const int KINDID_MYSTORE = 196613;
        /// <summary>Банк, соответствует значению 196614</summary>
        public const int KINDID_BANK = 196614;

        /// <summary>Предприятие, соответствует значению 1</summary>
        public const int KINDVALUE_COMPANY = 1;
        /// <summary>Физ лицо, соответствует значению 2</summary>
        public const int KINDVALUE_PEOPLE = 2;
        /// <summary>Склад поставщика или клиента, соответствует значению 3</summary>
        public const int KINDVALUE_STORE = 3;
        /// <summary>Мое предприятие, соответствует значению 4</summary>
        public const int KINDVALUE_MYCOMPANY = 4;
        /// <summary>Наш склад, соответствует значению 5</summary>
        public const int KINDVALUE_MYSTORE = 5;
        /// <summary>Банк, соответствует значению 6</summary>
        public const int KINDVALUE_BANK = 6;

        /// <summary>
        /// Код системного корреспондента, используемого для создания "общих" областей видимости в разрезе предприятий.
        /// </summary>
        public const string SYSTEM_AGENT_SHAREDHOLDING = "SYSTEM_AGENT_SHAREDHOLDING";
        // ReSharper restore InconsistentNaming
        
        #endregion
        bool IEquatable<Agent>.Equals(Agent other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public Agent():base()
        {
            EntityId = (short)WhellKnownDbEntity.Agent;
        }
        protected override void CopyValue(Agent template)
        {
            base.CopyValue(template);
            CodeTax = template.CodeTax;
            AddressLegal = template.AddressLegal;
            AddressPhysical = template.AddressPhysical;
            AmmountTrust = template.AmmountTrust;
            TimeDelay = template.TimeDelay;
            Phone = template.Phone;
        }

        protected override void OnCreated()
        {
            base.OnCreated();
            if (_company != null && _company.Id != this.Id)
            {
                _company.Id = Id;
                if (_company._bank != null && _company._bank.Id != this.Id)
                    _company._bank.Id = Id;
            }
            if(_store!=null && _store.Id!=Id)
            {
                _store.Id = Id;
            }
            if (_people != null && _people.Id != this.Id)
                _people.Id = Id;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit"></param>
        /// <returns></returns>
        protected override Agent Clone(bool endInit)
        {
            Agent obj = base.Clone(false);
            obj.CodeTax = CodeTax;
            obj.AddressLegal = AddressLegal;
            obj.AddressPhysical = AddressPhysical;
            obj.AmmountTrust = AmmountTrust;
            obj.TimeDelay = TimeDelay;
            obj.Phone = Phone;
            if(IsCompany)
            {
                obj._company = Company.Clone(true);
                obj._company.Id = 0;
                obj._company.Guid = Guid.Empty;
                obj._company.IsNew = true;

                if(IsBank)
                {
                    obj._company._bank = Company.Bank.Clone(true);
                    obj._company._bank.Id = 0;
                    obj._company._bank.Guid = Guid.Empty;
                    obj._company._bank.IsNew = true;
                }
            }
            if(endInit)
                OnEndInit();
            return obj;
        }

        /// <summary>
        /// Сравнение объекта для службы обмена данными
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange(Agent value)
        {
            if (!base.CompareExchange(value))
            {
                return false;
            }

            if (value.TimeDelay != TimeDelay)
                return false;
            if (value.AmmountTrust != AmmountTrust)
                return false;
            if (!StringNullCompare(value.CodeTax, CodeTax))
                return false;
            if (!StringNullCompare(value.AddressLegal, AddressLegal))
                return false;
            if (!StringNullCompare(value.AddressPhysical, AddressPhysical))
                return false;
            if (!StringNullCompare(value.Phone, Phone))
                return false;
            

            return true;
        }
        #region Свойства
        private string _phone;
        /// <summary>
        /// Телефон
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
        
        private string _codeTax;
        /// <summary>
        /// Налоговый номер
        /// </summary>
        public string CodeTax
        {
            get { return _codeTax; }
            set 
            {
                if (value == _codeTax) return;
                OnPropertyChanging(GlobalPropertyNames.CodeTax);
                _codeTax = value;
                OnPropertyChanged(GlobalPropertyNames.CodeTax);
            }
        }

        private string _addressLegal;
        /// <summary>
        /// Адрес юридический
        /// </summary>
        public string AddressLegal
        {
            get { return _addressLegal; }
            set
            {
                if (value == _addressLegal) return;
                OnPropertyChanging(GlobalPropertyNames.AddressLegal);
                _addressLegal = value;
                OnPropertyChanged(GlobalPropertyNames.AddressLegal);
            }
        }

        private string _addressPhysical;
        /// <summary>
        /// Адрес фактический
        /// </summary>
        public string AddressPhysical
        {
            get { return _addressPhysical; }
            set
            {
                if (value == _addressPhysical) return;
                OnPropertyChanging(GlobalPropertyNames.AddressPhysical);
                _addressPhysical = value;
                OnPropertyChanged(GlobalPropertyNames.AddressPhysical);
            }
        }

        private decimal _ammountTrust;
        /// <summary>
        /// Максимальная сумма задолженности
        /// </summary>
        public decimal AmmountTrust
        {
            get { return _ammountTrust; }
            set
            {
                if (value == _ammountTrust) return;
                OnPropertyChanging(GlobalPropertyNames.AmmountTrust);
                _ammountTrust = value;
                OnPropertyChanged(GlobalPropertyNames.AmmountTrust);
            }
        }

        private int _timeDelay;
        /// <summary>
        /// Максимальное количество дней отсрочки
        /// </summary>
        public int TimeDelay
        {
            get { return _timeDelay; }
            set
            {
                if (value == _timeDelay) return;
                OnPropertyChanging(GlobalPropertyNames.TimeDelay);
                _timeDelay = value;
                OnPropertyChanged(GlobalPropertyNames.TimeDelay);
            }
        }


        private int _myCompanyId;
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит предприятие
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
        /// Моя компания, предприятие которому принадлежит предприятие
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
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_codeTax))
                writer.WriteAttributeString(GlobalPropertyNames.CodeTax, _codeTax);
            if (!string.IsNullOrEmpty(_addressLegal))
                writer.WriteAttributeString(GlobalPropertyNames.AddressLegal, _addressLegal);
            if (!string.IsNullOrEmpty(_addressPhysical))
                writer.WriteAttributeString(GlobalPropertyNames.AddressPhysical, _addressPhysical);
            if (_ammountTrust != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AmmountTrust, XmlConvert.ToString(_ammountTrust));
            if (_timeDelay != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TimeDelay, XmlConvert.ToString(_timeDelay));
            if (!string.IsNullOrEmpty(_phone))
                writer.WriteAttributeString(GlobalPropertyNames.Phone, _phone);
            if(MyCompanyId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.CodeTax) != null)
                _codeTax = reader[GlobalPropertyNames.CodeTax];
            if (reader.GetAttribute(GlobalPropertyNames.AddressLegal) != null)
                _addressLegal = reader[GlobalPropertyNames.AddressLegal];
            if (reader.GetAttribute(GlobalPropertyNames.AddressPhysical) != null)
                _addressPhysical = reader[GlobalPropertyNames.AddressPhysical];
            if (reader.GetAttribute(GlobalPropertyNames.AmmountTrust) != null)
                _ammountTrust = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.AmmountTrust));
            if (reader.GetAttribute(GlobalPropertyNames.TimeDelay) != null)
                _timeDelay = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TimeDelay));
            if (reader.GetAttribute(GlobalPropertyNames.Phone) != null)
                _phone = reader[GlobalPropertyNames.Phone];
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
        }
        #endregion

        private List<AgentBankAccount> _bankAccounts;
        /// <summary>Коллекция расчетных счетов</summary>
        /// <remarks>Коллекция является кешированной, для прямого доступа используйте метод 
        /// <see cref="BusinessObjects.Agent.GetBanks()"/></remarks>
        public List<AgentBankAccount> BankAccounts
        {
            get 
            {
                if (_bankAccounts == null)
                    GetBanks();
                return _bankAccounts; 
            }
        }
	
        /// <summary>Коллекция расчетных счетов</summary>
        /// <remarks>Только для внутреннего использования для получения коллекции 
        /// используйте свойство <see cref="BusinessObjects.Agent.BankAccounts"/></remarks>
        /// <returns></returns>
        internal List<AgentBankAccount> GetBanks()
        {
            _bankAccounts = new List<AgentBankAccount>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return _bankAccounts;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = string.Format("Contractor.AgentGetBanks");
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            AgentBankAccount item = new AgentBankAccount { Workarea = Workarea };
                            item.Load(reader);
                            _bankAccounts.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return _bankAccounts;
        }


        /// <summary>Коллекция адресов</summary>
        /// <remarks>Коллекция является кешированной, для прямого доступа используйте метод 
        /// <see cref="BusinessObjects.Agent.GetAddress()"/></remarks>
        public List<AgentAddress> AddressCollection
        {
            get 
            {
                if (_addresses == null)
                    GetAddress();
                return _addresses; 
            }
        }

        private List<AgentAddress> _addresses;
        /// <summary>
        /// Коллекция адресов корреспондента
        /// </summary>
        /// <remarks>Только для внутреннего использования для получения коллекции, используйте 
        /// свойство <see cref="BusinessObjects.Agent.AddressCollection"/></remarks>
        /// <returns></returns>
        internal List<AgentAddress> GetAddress()
        {
            _addresses = new List<AgentAddress>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return _addresses;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = string.Format("Contractor.AddresLoadByAgent");
                        cmd.Parameters.Add(GlobalSqlParamNames.OwnId, SqlDbType.Int).Value = Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            AgentAddress item = new AgentAddress { Workarea = Workarea };
                            item.Load(reader);
                            _addresses.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return _addresses;
        }
        /// <summary>
        /// Идентификатор первого подразделения для предприятия
        /// </summary>
        /// <returns></returns>
        public int FirstDepatment()
        {
            int value = 0;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return 0;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindProcedure("FirstDepatment");
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        object obj = cmd.ExecuteScalar();
                        if (obj != null)
                            value = (int)obj;
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return value;
        }

        /// <summary>
        /// Идентификатор первого склада для подразделения
        /// </summary>
        /// <returns></returns>
        public int FirstStore()
        {
            int value = 0;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return 0;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindProcedure("FirstStore");
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        object obj = cmd.ExecuteScalar();
                        if (obj != null)
                            value = (int)obj;
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return value;
        }

        #region Relation

        private Dictionary<Type, ICoreObject> _singleCache;
        // TODO: данные метод должен использоваться в RelationHelper, а не в текущем классе...
        internal TObject GetRelated<TObject>() where TObject : class, ICoreObject, IRelationSingle
        {
            if (_singleCache == null)
                _singleCache = new Dictionary<Type, ICoreObject>();
            Type t = typeof (TObject);
            if(!_singleCache.ContainsKey(t))
            {
                RelationHelper<Agent, TObject> hlp = new RelationHelper<Agent, TObject>();
                TObject obj = hlp.GetObject(this);
                _singleCache.Add(t, obj);
            }
            return _singleCache[t] as TObject;
        }

        private Company _company;
        /// <summary>Данные о компании</summary>
        public Company Company
        {
            get
            {
                if (_company == null)
                    RefreshCompany();
                return _company;
            }
        }
        /// <summary>Обновить информацию о компании</summary>
        private void RefreshCompany()
        {
            if (!IsNew)
            {
                RelationHelper<Agent, Company> hlp = new RelationHelper<Agent, Company>();
                _company = hlp.GetObject(this);
                _company._owner = this;
            }
            else
            {
                _company = new Company {Workarea = Workarea};
                _company._owner = this;
            }
        }
        private People _people;
        /// <summary>Данные о человеке</summary>
        public People People
        {
            get
            {
                if (_people == null)
                    RefreshPeople();
                return _people;
            }
        }
        /// <summary>Обновить информацию о человеке</summary>
        private void RefreshPeople()
        {
            if (!IsNew)
            {
                RelationHelper<Agent, People> hlp = new RelationHelper<Agent, People>();
                _people = hlp.GetObject(this);
                _people._owner = this;
            }
            else
            {
                _people = new People {Workarea = Workarea};
                _people._owner = this;
            }
        }
        private List<Contact> _contactInfo;
        /// <summary>Контактная информация</summary>
        /// <returns></returns>
        public List<Contact> ContactInfo()
        {
            if (_contactInfo == null)
                RefreshContactInfo();
            return _contactInfo;
        }
        /// <summary>Обновить информацию о контактах</summary>
        private void RefreshContactInfo()
        {
            RelationHelper<Agent, Contact> hlp = new RelationHelper<Agent, Contact>();
            _contactInfo = hlp.GetListObject(this);
        }
        private Store _store;
        /// <summary>Данные о складе</summary>
        public Store Store
        {
            get
            {
                if (_store == null)
                    RefreshStore();
                return _store;
            }
        }
        /// <summary>
        /// Является предприятием
        /// </summary>
        public bool IsCompany
        {
            get { return (KindValue == Agent.KINDVALUE_BANK || KindValue == Agent.KINDVALUE_COMPANY || KindValue== Agent.KINDVALUE_MYCOMPANY); }
        }
        /// <summary>
        /// Является моим предприятием
        /// </summary>
        public bool IsMyCompany
        {
            get { return (KindValue == Agent.KINDVALUE_MYCOMPANY); }
        }
        /// <summary>
        /// Является простым предприятием
        /// </summary>
        public bool IsCompanyOnly
        {
            get { return (KindValue == Agent.KINDVALUE_COMPANY); }
        }
        /// <summary>
        /// Является складом
        /// </summary>
        public bool IsStore
        {
            get { return (KindValue == Agent.KINDVALUE_STORE || KindValue == Agent.KINDVALUE_MYSTORE ); }
        }
        /// <summary>
        /// Является банком
        /// </summary>
        public bool IsBank
        {
            get { return (KindValue == Agent.KINDVALUE_BANK ); }
        }
        /// <summary>
        /// Является физ лицом
        /// </summary>
        public bool IsPeople
        {
            get { return (KindValue == Agent.KINDVALUE_PEOPLE ); }
        }
        
        /// <summary>Обновить информацию о складах</summary>
        private void RefreshStore()
        {
            if (!IsNew)
            {
                RelationHelper<Agent, Store> hlp = new RelationHelper<Agent, Store>();
                _store = hlp.GetObject(this);
                _store._owner = this;
            }
            else
            {
                _store = new Store { Workarea = Workarea };
                _store._owner = this;
            }
            
        }
        #endregion

        #region Состояние
        AgentStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new AgentStruct
                                  {
                                      CodeTax = _codeTax,
                                      AddressLegal = _addressLegal,
                                      AddressPhysical = _addressPhysical,
                                      AmmountTrust = _ammountTrust,
                                      TimeDelay = _timeDelay,
                                      Phone = _phone,
                                      MyCompanyId=_myCompanyId
                                  };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            CodeTax = _baseStruct.CodeTax;
            AddressLegal = _baseStruct.AddressLegal;
            AddressPhysical = _baseStruct.AddressPhysical;
            AmmountTrust = _baseStruct.AmmountTrust;
            TimeDelay = _baseStruct.TimeDelay;
            Phone = _baseStruct.Phone;
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
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _codeTax = reader.IsDBNull(17) ? null : reader.GetString(17);
                _addressLegal = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                _addressPhysical = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                _ammountTrust = reader.GetDecimal(20);
                _timeDelay = reader.GetInt32(21);
                _phone = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                _myCompanyId = reader.IsDBNull(23) ? 0: reader.GetInt32(23);
            }
            catch(Exception ex)
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
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.CodeTax, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_codeTax))
                prm.Value = DBNull.Value;
            else
                prm.Value = _codeTax;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AddressLegal, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_addressLegal))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _addressLegal.Length;
                prm.Value = _addressLegal;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AddressPhysical, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_addressPhysical))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _addressPhysical.Length;
                prm.Value = _addressPhysical;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AmmountTrust, SqlDbType.Money) {IsNullable = false, Value = _ammountTrust};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TimeDelay, SqlDbType.Int) {IsNullable = false, Value = _timeDelay};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Phone, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_phone))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _phone.Length;
                prm.Value = _phone;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.NVarChar) { IsNullable = true };
            if (_myCompanyId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);

        }
        #endregion

        #region ILinks
        /// <summary>Связи корреспондента</summary>
        /// <returns></returns>
        public List<IChain<Agent>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи корреспондента</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Agent>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        
        List<Agent> IChains<Agent>.SourceList(int chainKindId)
        {
            return Chain<Agent>.GetChainSourceList(this, chainKindId);
            
        }

        List<Agent> IChains<Agent>.DestinationList(int chainKindId)
        {
            return Chain<Agent>.DestinationList(this, chainKindId);
        }
        // TODO: использовать List<Agent> IChains<Agent>.SourceList(int chainKindId)
        [Obsolete]
        public static List<Agent> GetChainSourceList(Workarea workarea, int id, int chainKindId)
        {
            Agent ag = workarea.Cashe.GetCasheData<Agent>().Item(id);
            return Chain<Agent>.GetChainSourceList(ag, chainKindId);
            //Agent item;
            //List<Agent> collection = new List<Agent>();
            //using (SqlConnection cnn = workarea.GetDatabaseConnection())
            //{
            //    if (cnn == null) return collection;
            //    try
            //    {
            //        using (SqlCommand cmd = cnn.CreateCommand())
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            // TODO: Маппинг ХП
            //            cmd.CommandText = "[Contractor].[AgentsLoadByChainsId]";//FindMethod("Core.EntitiesHasBrowseTree").FullName;
            //            cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
            //            cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = chainKindId;
            //            SqlDataReader reader = cmd.ExecuteReader();
            //            while (reader.Read())
            //            {
            //                item = new Agent { Workarea = workarea };
            //                item.Load(reader);
            //                collection.Add(item);
            //            }
            //            reader.Close();
            //        }
            //    }
            //    finally
            //    {
            //        cnn.Close();
            //    }
            //}
            //return collection;
        }

        
        #endregion

        #region Дополнительные методы
        private List<Agent> _stores;
        /// <summary>Обновить склады</summary>
        private void RefreshStores()
        {
            if (_stores == null)
                _stores = new List<Agent>();
            else
                _stores.Clear();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Entity.FindMethod("GetStores").FullName; 
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
                                Agent item = new Agent { Workarea = Workarea };
                                item.Load(reader);
                                _stores.Add(item);
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
        }
        /// <summary>Склады ассоциированные с данным предприятием</summary>
        /// <returns></returns>
        public List<Agent> GetStores()
        {
            if (_stores == null)
                RefreshStores();
            return _stores;
        }
        /// <summary>
        /// Поиск корреспондентов
        /// </summary>
        /// <param name="hierarchyId">Идентификатор иеархии в которой необъодимо вести поиск</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="flags">Флаг</param>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <param name="name">Наименование</param>
        /// <param name="kindId">Подтип</param>
        /// <param name="code">Код</param>
        /// <param name="memo">Примечание</param>
        /// <param name="flagString">Пользовательский флаг</param>
        /// <param name="templateId">Идентификатор шаблона</param>
        /// <param name="count">Количество записей, по умолчанию 100</param>
        /// <param name="filter">Дополнительный фильтр</param>
        /// <param name="myCompanyId">Идентификатор компании владельца</param>
        /// <param name="useAndFilter">Использовать фильтр И (по умолчанию ИЛИ)</param>
        /// <returns></returns>
        public List<Agent> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
int? stateId = null, string name = null, int kindId = 0, string code = null,
string memo = null, string flagString = null, int templateId = 0,
int count = 100, Predicate<Agent> filter = null, int? myCompanyId = null, bool useAndFilter=false)
        {
            Agent item = new Agent { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Agent> collection = new List<Agent>();
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
                            item = new Agent { Workarea = Workarea };
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

        #region ICodes
        public List<CodeValue<Agent>> GetValues(bool allKinds)
        {
            return CodeHelper<Agent>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Agent>.GetView(this, true);
        }
        #endregion

        #region IChainsAdvancedList<Agent,FileData> Members

        List<IChainAdvanced<Agent, FileData>> IChainsAdvancedList<Agent, FileData>.GetLinks()
        {
            return ChainAdvanced<Agent, FileData>.CollectionSource(this);
            //return ((IChainsAdvancedList<Agent, FileData>)this).GetLinks(47);
        }

        List<IChainAdvanced<Agent, FileData>> IChainsAdvancedList<Agent, FileData>.GetLinks(int? kind)
        {
            return ChainAdvanced<Agent, FileData>.CollectionSource(this, kind);
            //return GetLinkedFiles();
        }
        public List<IChainAdvanced<Agent, FileData>> GetLinkedFiles()
        {
            return ChainAdvanced<Agent, FileData>.CollectionSource(this);
        }

        List<ChainValueView> IChainsAdvancedList<Agent, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Agent, FileData>(this);
        }
        #endregion

        #region IChainsAdvancedList<Agent,Knowledge> Members

        List<IChainAdvanced<Agent, Knowledge>> IChainsAdvancedList<Agent, Knowledge>.GetLinks()
        {
            return ChainAdvanced<Agent, Knowledge>.CollectionSource(this);
        }

        List<IChainAdvanced<Agent, Knowledge>> IChainsAdvancedList<Agent, Knowledge>.GetLinks(int? kind)
        {
            return ChainAdvanced<Agent, Knowledge>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Agent, Knowledge>> GetLinkedKnowledges()
        {
            return ChainAdvanced<Agent, Knowledge>.CollectionSource(this);
            List<IChainAdvanced<Agent, Knowledge>> collection = new List<IChainAdvanced<Agent, Knowledge>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Agent>().Entity.FindMethod("LoadKnowledges").FullName;
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
                                ChainAdvanced<Agent, Knowledge> item = new ChainAdvanced<Agent, Knowledge> { Workarea = Workarea, Left = this };
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
        List<ChainValueView> IChainsAdvancedList<Agent, Knowledge>.GetChainView()
        {
            return ChainValueView.GetView<Agent, Knowledge>(this);
        }
        #endregion

        #region IChainsAdvancedList<Agent,Analitic> Members

        List<IChainAdvanced<Agent, Analitic>> IChainsAdvancedList<Agent, Analitic>.GetLinks()
        {
            return ChainAdvanced<Agent, Analitic>.CollectionSource(this);
        }

        List<IChainAdvanced<Agent, Analitic>> IChainsAdvancedList<Agent, Analitic>.GetLinks(int? kind)
        {
            return ChainAdvanced<Agent, Analitic>.CollectionSource(this, kind);
        }
        /// <summary>
        /// Связанные аналитические данные
        /// </summary>
        /// <returns></returns>
        public List<IChainAdvanced<Agent, Analitic>> GetLinkedAnalitics()
        {
            return ChainAdvanced<Agent, Analitic>.CollectionSource(this);
        }
        List<ChainValueView> IChainsAdvancedList<Agent, Analitic>.GetChainView()
        {
            return ChainValueView.GetView<Agent, Analitic>(this);
        }
        #endregion

        #region IChainsAdvancedList<Agent,Note> Members

        List<IChainAdvanced<Agent, Note>> IChainsAdvancedList<Agent, Note>.GetLinks()
        {
            return ChainAdvanced<Agent, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Agent, Note>> IChainsAdvancedList<Agent, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Agent, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Agent, Note>> GetLinkedNotes(int? kind=null)
        {
            return ChainAdvanced<Agent, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Agent, Note>.GetChainView()
        {
            return ChainValueView.GetView<Agent, Note>(this);
        }
        #endregion


        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<Agent>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }
        /// <summary>
        /// Метод вовращает компанию являющуюся первым холдингом для текущего корреспондента
        /// </summary>
        /// <returns></returns>
        public Agent GetAgentHolding()
        {
            // если это мое предприятие, необходимо вернуть кореспондента по связи TREE в обратном направлении
            if(this.KindId== KINDID_MYCOMPANY || this.KindId== KINDID_COMPANY)
            {

                List<Agent> coll = Chain<Agent>.DestinationList(this, Workarea.CompanyTreeChainId());
                if (coll != null && coll.Count > 0)
                {
                    return coll[0];
                }
                else return this;
            }
            else if(this.KindId==KINDID_PEOPLE && this.MyCompanyId!=0)
            {
                Agent agcompany = Workarea.Cashe.GetCasheData<Agent>().Item(this.MyCompanyId);
                List<Agent> coll = Chain<Agent>.DestinationList(agcompany, Workarea.CompanyTreeChainId());
                if (coll != null && coll.Count > 0)
                {
                    return coll[0];
                }
                

            }
            return null;
        }
    }


    
}
