using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.Security;
using System.Xml;

namespace BusinessObjects.Security
{
    // http://theintegrity.co.uk/2010/11/asp-net-mvc-2-custom-membership-provider-tutorial-part-1/
    /// <summary>Внутренняя структура объекта "Аналитика"</summary>
    internal struct UidStruct
    {
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
        /// <summary>Пароль</summary>
        public string Password;
        /// <summary>Тип авторизации</summary>
        public int AuthenticateKind;
        /// <summary>Идентификатор соответствия корреспонденту-сотруднику</summary>
        public int AgentId;
        /// <summary>Электронная почта</summary>
        public string Email;
        /// <summary>Дата последней блокировки</summary>
        public DateTime? LastLockedOutDate;
        /// <summary>Дата последнего изменения пароля</summary>
        public DateTime? LastPasswordChangedDate;
        /// <summary>Дата последнего входа</summary>
        public DateTime? LastLoginDate;
        /// <summary>Секретный вопрос</summary>
        public string PasswordQuestion;
        /// <summary>Новый адресс для отправки пароля</summary>
        public string NewEmailKey;
        /// <summary>Дата создания</summary>
        public DateTime? DateCreated;
        /// <summary>Дата последненей активности</summary>
        public DateTime? LastActivityDate;
        /// <summary>Реальный адрес пользователя (используется в Web)</summary>
        public string RemoteAddr;
        /// <summary>Разрешить смену пароля пользователем</summary>
        public bool AllowChangePassword;
        /// <summary>Рекомендуемая дата смены пароля</summary>
        public DateTime? RecommendedDateChangePassword;
        /// <summary>Автоматически генерировать следующий пароль</summary>
        public bool AutogenerateNextPassword;
        /// <summary>График разрешенного входа в систмему</summary>
        public int TimePeriodId;
    }

    /// <summary>
    /// Тип авторизации
    /// </summary>
    public enum AuthenticateKind
    {
        /// <summary>
        /// Sql Server
        /// </summary>
        SqlServer = 0,
        /// <summary>
        /// Windows
        /// </summary>
        Windows = 1,
        /// <summary>
        /// Без логина
        /// </summary>
        NoLogin = 2
    }
    /// <summary>Пользователь или группа</summary>
    public sealed class Uid : BaseCore<Uid>, IChains<Uid>,
        IChainsAdvancedList<Uid, Agent>, ICompanyOwner
    {

        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Пользователь, соответствует значению 1</summary>
        public const int KINDVALUE_USER = 1;
        /// <summary>Группа, соответствует значению 2</summary>
        public const int KINDVALUE_GROUP = 2;

        /// <summary>Пользователь, соответствует значению 1703937</summary>
        public const int KINDID_USER = 1703937;
        /// <summary>Группа, соответствует значению 1703938</summary>
        public const int KINDID_GROUP = 1703938;
        // ReSharper restore InconsistentNaming
        #endregion

        #region Константы системных групп
        // ReSharper disable InconsistentNaming
        /// <summary>Бухгалтерия</summary>
        public const string GROUP_GROUPBOOKKEEP = "Бухгалтерия";
        /// <summary>Корректоры</summary>
        public const string GROUP_GROUPEDITORS = "Корректоры";
	
        /// <summary>Администраторы</summary>
        public const string GROUP_GROUPLOCALADMIN = "Администраторы";
        /// <summary>Системные администраторы</summary>
        public const string GROUP_GROUPSYSTEMADMIN = "Системные администраторы";

	    /// <summary>Пользователи</summary>
        public const string GROUP_ALLUSERS = "Пользователи";
        /// <summary>Управление складом</summary>
        public const string GROUP_GROUPSTORE = "Управление складом";
	
        /// <summary>Управление торговлей</summary>
        public const string GROUP_GROUPSALES = "Управление торговлей";

        /// <summary>Управление торговлей с НДС</summary>
        public const string GROUP_GROUPSALESNDS = "Управление торговлей НДС";
        /// <summary>Учет договоров</summary>
        public const string GROUP_GROUPCONTRACTS = "Учет договоров";
	
        /// <summary>Управление финансами</summary>
        public const string GROUP_GROUPFINANCE = "Управление финансами";
        /// <summary>Управление финансами с НДС</summary>
        public const string GROUP_GROUPFINANCENDS = "Управление финансами НДС";
        /// <summary>Услуги</summary>
        public const string GROUP_GROUPSERVICES = "Услуги";

        /// <summary>Услуги с НДС</summary>
        public const string GROUP_GROUPSERVICESNDS = "Услуги НДС";
	
        /// <summary>Налоговые</summary>
        public const string GROUP_GROUPTAX = "Налоговые";
        /// <summary>Web Администраторы</summary>
        public const string GROUP_GROUPWEBADMIN = "Web Администраторы";
        /// <summary>Web пользователи</summary>
        public const string GROUP_GROUPWEBUSER = "Web пользователи";
        
        /// <summary>Управление персоналом</summary>
        public const string GROUP_GROUPPERSON = "Управление персоналом";
        /// <summary>Маркетинг</summary>
        public const string GROUP_GROUPMKTG = "Маркетинг";

        /// <summary>Управление ценами</summary>
        public const string GROUP_GROUPPRICES = "Управление ценами";

        /// <summary>Управление задачами</summary>
        public const string GROUP_GROUPTASK = "Управление задачами";
        
        /// <summary>Мой бизнес</summary>
        public const string GROUP_WEBMYBIZ = "WEB_MYBIZ";
        
        // ReSharper restore InconsistentNaming
        #endregion

        private static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
                FormsAuthentication.HashPasswordForStoringInConfigFile(
                    saltAndPwd, "sha1");
            return hashedPwd;
        }

        /// <summary>Конструктор</summary>
        public Uid(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Users;
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if(KindValue== Uid.KINDVALUE_USER && Workarea._access._allUsers!=null)
            {
                if(Workarea._access._allUsers.Exists(s=>s.Id==Id))
                {
                    int idx = Workarea._access._allUsers.FindIndex(s => s.Id == Id);
                    Workarea._access._allUsers[idx] = this;
                }
                else
                {
                    Workarea._access.GetAllUsers(true);
                }
            }

            if (KindValue == Uid.KINDVALUE_GROUP && Workarea._access._allGroups != null)
            {
                if (Workarea._access._allGroups.Exists(s => s.Id == Id))
                {
                    int idx = Workarea._access._allGroups.FindIndex(s => s.Id == Id);
                    Workarea._access._allGroups[idx] = this;
                }
                else
                {
                    Workarea._access.GetAllGroups(true);
                }
            }
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Uid Clone(bool endInit)
        {
            Uid obj = base.Clone(endInit);
            obj.AgentId = AgentId;
            obj.AuthenticateKind = AuthenticateKind;
            obj.Password = Password;
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства
        private string _password;
        /// <summary>
        /// Пароль
        /// </summary>
        /// <remarks>Пароль по умолчанию можно будет задать в системных параметрах.</remarks>
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;
                OnPropertyChanging(GlobalPropertyNames.Password);
                _password = value;
                OnPropertyChanged(GlobalPropertyNames.Password);
            }
        }
        private int _authenticateKind;
        /// <summary>
        /// Тип авторизации
        /// </summary>
        /// <remarks>
        /// Используемые значения: 
        /// <para></para>
        /// <list type="table">
        /// <listheader>
        /// <term>Значение</term>
        /// <description>Описание</description></listheader>
        /// <item>
        /// <term>0</term>
        /// <description>Sql Server</description></item>
        /// <item>
        /// <term>1</term>
        /// <description>Windows</description></item>
        /// <item>
        /// <term>2</term>
        /// <description>Не имеет логина, пользователь на основе группы безопастности
        /// домена</description></item></list>
        /// <para>Тип авторизации не должен меняться после создания!</para>
        /// </remarks>
        public int AuthenticateKind
        {
            get { return _authenticateKind; }
            set
            {
                if (_authenticateKind == value) return;
                OnPropertyChanging(GlobalPropertyNames.AuthenticateKind);
                _authenticateKind = value;
                OnPropertyChanged(GlobalPropertyNames.AuthenticateKind);
            }
        }

        /// <summary>Проверка соответствия объекта бизнес правилам</summary>
        /// <remarks>Метод выполняет проверку наименования объекта <see cref="BaseCore{T}.Name"/> на предмет null, <see cref="string.Empty"/> и максимальную длину не более 255 символов</remarks>
        /// <returns><c>true</c> - если объект соответствует бизнес правилам, <c>false</c> в противном случае</returns>
        /// <exception cref="ValidateException">Если объект не соответствует текущим правилам</exception>
        public override void Validate()
        {
            base.Validate();
            if (KindValue != 1) return;
            if (AuthenticateKind == 0 && String.IsNullOrEmpty(Password))
                throw new ValidateException("Не указан пароль!");
            if (AuthenticateKind == 0 && String.IsNullOrEmpty(Code))
                throw new ValidateException("Не указан логин!");

            _passwordSalt = CreateSalt();
            _passwordHash = CreatePasswordHash(_password, _passwordSalt);
        }
        private int _agentId;
        /// <summary>
        /// Идентификатор соответствия корреспонденту-сотруднику
        /// </summary>
        public int AgentId
        {
            get { return _agentId; }
            set
            {
                if (_agentId == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentId);
                _agentId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentId);
            }
        }

        private Agent _agent;
        /// <summary>
        /// Сотрудник
        /// </summary>
        public Agent Agent
        {
            get
            {
                if (_agentId == 0)
                    return null;
                if (_agent == null)
                    _agent = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId);
                else if (_agent.Id != _agentId)
                    _agent = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId);
                return _agent;
            }
            set
            {
                if (_agent == value) return;
                OnPropertyChanging(GlobalPropertyNames.Agent);
                _agent = value;
                _agentId = _agent == null ? 0 : _agent.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent);
            }
        }
        


        private string _email;
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                if (value == _email) return;
                OnPropertyChanging(GlobalPropertyNames.Email);
                _email = value;
                OnPropertyChanged(GlobalPropertyNames.Email);
            }
        }


        private bool _isActivated;
        /// <summary>
        /// Активен
        /// </summary>
        public bool IsActivated
        {
            get { return IsStateAllow; }
            set
            {
                if (value)
                    StateId = State.STATEACTIVE;
                else
                    StateId = State.STATEDENY;
            }
        }


        private bool _isLockedOut;
        /// <summary>
        /// Заблокирован
        /// </summary>
        public bool IsLockedOut
        {
            get { return IsStateDeny; }
            set
            {
                if (!value)
                    StateId = State.STATEACTIVE;
                else
                    StateId = State.STATEDENY;
            }
        }

        private DateTime? _lastLockedOutDate;
        /// <summary>
        /// Дата последней блокировки
        /// </summary>
        public DateTime? LastLockedOutDate
        {
            get { return _lastLockedOutDate; }
            set
            {
                if (value == _lastLockedOutDate) return;
                OnPropertyChanging(GlobalPropertyNames.LastLockedOutDate);
                _lastLockedOutDate = value;
                OnPropertyChanged(GlobalPropertyNames.LastLockedOutDate);
            }
        }


        private DateTime? _lastPasswordChangedDate;
        /// <summary>
        /// Дата последнего изменения пароля
        /// </summary>
        public DateTime? LastPasswordChangedDate
        {
            get { return _lastPasswordChangedDate; }
            set
            {
                if (value == _lastPasswordChangedDate) return;
                OnPropertyChanging(GlobalPropertyNames.LastPasswordChangedDate);
                _lastPasswordChangedDate = value;
                OnPropertyChanged(GlobalPropertyNames.LastPasswordChangedDate);
            }
        }
        
        private DateTime? _lastLoginDate;
        /// <summary>
        /// Дата последнего входа
        /// </summary>
        public DateTime? LastLoginDate
        {
            get { return _lastLoginDate; }
            set
            {
                if (value == _lastLoginDate) return;
                OnPropertyChanging(GlobalPropertyNames.LastLoginDate);
                _lastLoginDate = value;
                OnPropertyChanged(GlobalPropertyNames.LastLoginDate);
            }
        }


        private string _passwordQuestion;
        /// <summary>
        /// Секретный вопрос
        /// </summary>
        public string PasswordQuestion
        {
            get { return _passwordQuestion; }
            set
            {
                if (value == _passwordQuestion) return;
                OnPropertyChanging(GlobalPropertyNames.PasswordQuestion);
                _passwordQuestion = value;
                OnPropertyChanged(GlobalPropertyNames.PasswordQuestion);
            }
        }


        private string _passwordSalt;
        public string PasswordSalt
        {
            get { return _passwordSalt; }
            set
            {
                if (value == _passwordSalt) return;
                OnPropertyChanging(GlobalPropertyNames.PasswordSalt);
                _passwordSalt = value;
                OnPropertyChanged(GlobalPropertyNames.PasswordSalt);
            }
        }


        private string _passwordHash;
        public string PasswordHash
        {
            get { return _passwordHash; }
            set
            {
                if (value == _passwordHash) return;
                OnPropertyChanging(GlobalPropertyNames.PasswordHash);
                _passwordHash = value;
                OnPropertyChanged(GlobalPropertyNames.PasswordHash);
            }
        }

        private string _newEmailKey;
        public string NewEmailKey
        {
            get { return _newEmailKey; }
            set
            {
                if (value == _newEmailKey) return;
                OnPropertyChanging(GlobalPropertyNames.NewEmailKey);
                _newEmailKey = value;
                OnPropertyChanged(GlobalPropertyNames.NewEmailKey);
            }
        }
        
        private bool _isOnline;
        public bool IsOnline
        {
            get { return _isOnline; }
            set
            {
                if (value == _isOnline) return;
                OnPropertyChanging("IsOnline");
                _isOnline = value;
                OnPropertyChanged("IsOnline");
            }
        }

        private DateTime? _dateCreated;
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? DateCreated
        {
            get { return _dateCreated; }
            set
            {
                if (value == _dateCreated) return;
                OnPropertyChanging(GlobalPropertyNames.DateCreated);
                _dateCreated = value;
                OnPropertyChanged(GlobalPropertyNames.DateCreated);
            }
        }


        private DateTime? _lastActivityDate;
        /// <summary>
        /// Дата последненго входа
        /// </summary>
        public DateTime? LastActivityDate
        {
            get { return _lastActivityDate; }
            set
            {
                if (value == _lastActivityDate) return;
                OnPropertyChanging(GlobalPropertyNames.LastActivityDate);
                _lastActivityDate = value;
                OnPropertyChanged(GlobalPropertyNames.LastActivityDate);
            }
        }

        
        private bool _allowChangePassword;
        /// <summary>Разрешить смену пароля пользователем</summary>
        public bool AllowChangePassword 
        { 
            get{ return _allowChangePassword; } 
            set
            {
               if (value == _allowChangePassword) return;
                OnPropertyChanging(GlobalPropertyNames.AllowChangePassword);
                _allowChangePassword = value;
                OnPropertyChanged(GlobalPropertyNames.AllowChangePassword);
            } 
        }

        
        private DateTime? _recommendedDateChangePassword;
        /// <summary>Рекомендуемая дата смены пароля</summary>
        public DateTime? RecommendedDateChangePassword 
        { 
            get{ return _recommendedDateChangePassword; } 
            set
            {
               if (value == _recommendedDateChangePassword) return;
                OnPropertyChanging(GlobalPropertyNames.RecommendedDateChangePassword);
                _recommendedDateChangePassword = value;
                OnPropertyChanged(GlobalPropertyNames.RecommendedDateChangePassword);
            } 
        }
        
        private bool _autogenerateNextPassword;
        /// <summary>Автоматически генерировать следующий пароль</summary>
        public bool AutogenerateNextPassword
        {
            get { return _autogenerateNextPassword; }
            set
            {
                if (value == _autogenerateNextPassword) return;
                OnPropertyChanging(GlobalPropertyNames.AutogenerateNextPassword);
                _autogenerateNextPassword = value;
                OnPropertyChanged(GlobalPropertyNames.AutogenerateNextPassword);
            }
        }


        private int _timePeriodId;
        /// <summary>Идентификатор графика разрешенного входа в систему</summary>
        public int TimePeriodId
        {
            get { return _timePeriodId; }
            set
            {
                if (value == _timePeriodId) return;
                OnPropertyChanging(GlobalPropertyNames.TimePeriodId);
                _timePeriodId = value;
                OnPropertyChanged(GlobalPropertyNames.TimePeriodId);
            }
        }


        private TimePeriod _timePeriod;
        /// <summary>График разрешенного входа в систему</summary>
        public TimePeriod TimePeriod
        {
            get
            {
                if (_timePeriodId == 0)
                    return null;
                if (_timePeriod == null)
                    _timePeriod = Workarea.Cashe.GetCasheData<TimePeriod>().Item(_timePeriodId);
                else if (_timePeriod.Id != _timePeriodId)
                    _timePeriod = Workarea.Cashe.GetCasheData<TimePeriod>().Item(_timePeriodId);
                return _timePeriod;
            }
            set
            {
                if (_timePeriod == value) return;
                OnPropertyChanging(GlobalPropertyNames.TimePeriod);
                _timePeriod = value;
                _timePeriodId = _timePeriod == null ? 0 : _timePeriod.Id;
                OnPropertyChanged(GlobalPropertyNames.TimePeriod);
            }
        }
        
        
        #endregion	
        #region Состояние
        UidStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new UidStruct
                {
                    MyCompanyId = _myCompanyId,
                    Password = _password,
                    AuthenticateKind = _authenticateKind,
                    AgentId = _agentId,
                    Email = _email,
                    LastLockedOutDate = _lastLockedOutDate,
                    LastPasswordChangedDate = _lastPasswordChangedDate,
                    LastLoginDate = _lastLoginDate,
                    PasswordQuestion = _passwordQuestion,
                    NewEmailKey = _newEmailKey,
                    DateCreated = _dateCreated,
                    LastActivityDate = _lastActivityDate,
                    RemoteAddr = _remoteAddr,
                    AllowChangePassword = _allowChangePassword, 
                    RecommendedDateChangePassword = _recommendedDateChangePassword,
                    AutogenerateNextPassword = _autogenerateNextPassword,
                    TimePeriodId = _timePeriodId
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
            Password = _baseStruct.Password;
            AuthenticateKind = _baseStruct.AuthenticateKind;
            AgentId = _baseStruct.AgentId;
            Email = _baseStruct.Email;
            LastLockedOutDate = _baseStruct.LastLockedOutDate;
            LastPasswordChangedDate = _baseStruct.LastPasswordChangedDate;
            LastLoginDate = _baseStruct.LastLoginDate;
            PasswordQuestion = _baseStruct.PasswordQuestion;
            NewEmailKey = _baseStruct.NewEmailKey;
            DateCreated = _baseStruct.DateCreated;
            LastActivityDate = _baseStruct.LastActivityDate;
            RemoteAddr = _baseStruct.RemoteAddr;
            AllowChangePassword = _baseStruct.AllowChangePassword;
            RecommendedDateChangePassword = _baseStruct.RecommendedDateChangePassword;
            AutogenerateNextPassword = _baseStruct.AutogenerateNextPassword;
            TimePeriodId = _baseStruct.TimePeriodId;
            IsChanged = false;
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

            if (!String.IsNullOrEmpty(_password))
                writer.WriteAttributeString(GlobalPropertyNames.Password, _password);
            if (_authenticateKind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AuthenticateKind, XmlConvert.ToString(_authenticateKind));
            if (_agentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentId, XmlConvert.ToString(_agentId));
            if (!String.IsNullOrEmpty(_email))
                writer.WriteAttributeString(GlobalPropertyNames.Email, _email);
            if (_lastLockedOutDate.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.LastLockedOutDate, XmlConvert.ToString(_lastLockedOutDate.Value));
            if (_lastPasswordChangedDate.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.LastPasswordChangedDate, XmlConvert.ToString(_lastPasswordChangedDate.Value));
            if (_lastLoginDate.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.LastLoginDate, XmlConvert.ToString(_lastLoginDate.Value));
            if (!String.IsNullOrEmpty(_passwordQuestion))
                writer.WriteAttributeString(GlobalPropertyNames.PasswordQuestion, _passwordQuestion);
            if (!String.IsNullOrEmpty(_passwordSalt))
                writer.WriteAttributeString(GlobalPropertyNames.PasswordSalt, _passwordSalt);
            if (!String.IsNullOrEmpty(_passwordHash))
                writer.WriteAttributeString(GlobalPropertyNames.PasswordHash, _passwordHash);
            if (!String.IsNullOrEmpty(_newEmailKey))
                writer.WriteAttributeString(GlobalPropertyNames.NewEmailKey, _newEmailKey);
            if (_myCompanyId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
            if (_timePeriodId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TimePeriodId, XmlConvert.ToString(_timePeriodId));
            
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Password) != null)
                _password = reader.GetAttribute(GlobalPropertyNames.Password);
            if (reader.GetAttribute(GlobalPropertyNames.AuthenticateKind) != null)
                _authenticateKind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AuthenticateKind));
            if (reader.GetAttribute(GlobalPropertyNames.AgentId) != null)
                _agentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentId));
            if (reader.GetAttribute(GlobalPropertyNames.Email) != null)
                _email = reader.GetAttribute(GlobalPropertyNames.Email);
            if (reader.GetAttribute(GlobalPropertyNames.LastLockedOutDate) != null)
                _lastLockedOutDate = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.LastLockedOutDate));
            if (reader.GetAttribute(GlobalPropertyNames.LastPasswordChangedDate) != null)
                _lastPasswordChangedDate = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.LastPasswordChangedDate));
            if (reader.GetAttribute(GlobalPropertyNames.LastLoginDate) != null)
                _lastLoginDate = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.LastLoginDate));
            if (reader.GetAttribute(GlobalPropertyNames.PasswordSalt) != null)
                _passwordSalt = reader.GetAttribute(GlobalPropertyNames.PasswordSalt);
            if (reader.GetAttribute(GlobalPropertyNames.PasswordHash) != null)
                _passwordHash = reader.GetAttribute(GlobalPropertyNames.PasswordHash);
            if (reader.GetAttribute(GlobalPropertyNames.NewEmailKey) != null)
                _newEmailKey = reader.GetAttribute(GlobalPropertyNames.NewEmailKey);
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
            if (reader.GetAttribute(GlobalPropertyNames.TimePeriodId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TimePeriodId));
            
        }
        #endregion

        private int _myCompanyId;
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит объект
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
        /// Моя компания, предприятие которому принадлежит объект
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


        private string _remoteAddr;
        /// <summary>
        /// Реальный адрес пользователя (используется в Web)
        /// </summary>
        public string RemoteAddr
        {
            get { return _remoteAddr; }
            set
            {
                if (value == _remoteAddr) return;
                OnPropertyChanging(GlobalPropertyNames.RemoteAddr);
                _remoteAddr = value;
                OnPropertyChanged(GlobalPropertyNames.RemoteAddr);
            }
        }
        

        private List<Uid> _groups;
        /// <summary>Список групп в которые входит пользователь</summary>
        public List<Uid> Groups
        {
            get 
            {
                if (KindValue != 1)
                    return null;
                if (_groups == null)
                    RefreshUserGroups();
                return _groups; 
            }
        }
        /// <summary>
        /// Обновить данные из базы данных
        /// </summary>
        public override void Refresh(bool all=true)
        {
            base.Refresh(all);
            if(all)
                RefreshUserGroups();
        }
        /// <summary>Обновить список групп пользователя</summary>
        /// <returns></returns>
        private void RefreshUserGroups()
        {
            if (_groups == null)
                _groups = new List<Uid>();
            else
                _groups.Clear();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("UserGroupsLoadByUser").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = Name;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Uid item = new Uid { Workarea = Workarea };
                            item.Load(reader);
                            _groups.Add(item);
                        }
                        reader.Close();

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return;
        }
        /// <summary>Включить пользователя в группу</summary>
        /// <param name="group"></param>
        public void IncludeInGroup(Uid group)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                // TODO: Дополнительная проверка if (cnn == null) выдает false но идет в блок throw?
                //if (cnn == null)
                //    throw new ConnectionException(Workarea.Cashe.ResourceString("EX_MSG_CONNECTIONLOST", 1049));

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Entity.FindMethod("Uid.IncludeInGroup").FullName;// "sp_addrolemember";
                        cmd.Parameters.Add(GlobalSqlParamNames.Rolename, SqlDbType.NVarChar, 255).Value = group.Name;
                        cmd.Parameters.Add(GlobalSqlParamNames.Membername, SqlDbType.NVarChar, 255).Value = Name;
                        cmd.ExecuteNonQuery();

                        if (group.Name.ToUpper() == "АДМИНИСТРАТОРЫ")
                        {
                            cmd.CommandText =
                                String.Format("EXEC master..sp_addsrvrolemember @loginame = N'{0}', @rolename = N'securityadmin'", Code);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            cmd.CommandText =
                                String.Format("USE MASTER GO GRANT CONTROL SERVER TO {0}", Code);
                            
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /*
EXEC master..sp_dropsrvrolemember @loginame = N'LocalAdmin', @rolename = N'securityadmin'
GO

GRANT ALTER ANY LOGIN TO LocalAdmin
GRANT CREATE ROLE TO LocalAdmin
GRANT ALTER ANY ROLE TO LocalAdmin --db
GRANT ALTER ANY USER TO LocalAdmin --sb
GRANT CREATE ROLE TO LocalAdmin --db
GRANT BACKUP DATABASE TO LocalAdmin --db
GRANT CONTROL SERVER TO LocalAdmin --master

ALTER ANY SCHEMA --db



REVOKE ALTER ANY LOGIN TO LocalAdmin --master
REVOKE ALTER ANY ROLE TO LocalAdmin -- db
REVOKE ALTER ANY USER TO LocalAdmin --db
REVOKE CONTROL SERVER TO LocalAdmin --master
--GRANT ALTER ANY EVENT NOTIFICATION TO JanethEsteves

SELECT * FROM fn_my_permissions(NULL, 'SERVER');
EXECUTE AS LOGIN ='localAdmin'
GO
SELECT * FROM fn_my_permissions(NULL, 'SERVER');
GO
REVERT
GO


SELECT * FROM fn_my_permissions(NULL, 'DATABASE');
EXECUTE AS LOGIN ='localAdmin'
GO
SELECT * FROM fn_my_permissions('localAdmin', 'DATABASE');
GO
REVERT
        */
        /// <summary>
        /// Установить время последней активности пользователя
        /// </summary>
        /// <param name="value">Дата, которую необходимо установить или текущая</param>
        public void SetLastActivityDate(DateTime? value, string remoteAddr=null)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException(Workarea.Cashe.ResourceString("EX_MSG_CONNECTIONLOST", 1049));
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "[Secure].[UserUpdateLastActivityDate]";//Entity.FindMethod("DbUid.ExcludeFromGroup").FullName; //"sp_droprolemember";
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.LastActivityDate, SqlDbType.DateTime).Value = value.HasValue ? value.Value : DateTime.Now;
                        SqlParameter prmRA = new SqlParameter(GlobalSqlParamNames.RemoteAddr, SqlDbType.NVarChar, 100) { IsNullable = true };
                        if (String.IsNullOrEmpty(remoteAddr))
                            prmRA.Value = DBNull.Value;
                        else
                            prmRA.Value = remoteAddr;
                        sqlCmd.Parameters.Add(prmRA);

                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                        sqlCmd.Parameters.Add(prm);
                        if (sqlCmd.Connection.State != ConnectionState.Open)
                            sqlCmd.Connection.Open();
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                Load(reader);
                            }
                            reader.Close();

                            object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                            if (retval == null)
                                throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                            if ((int)retval != 0)
                                throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        }
                        
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            //
        }
        /// <summary>Исключить пользователя из группы</summary>
        /// <param name="group">Группа</param>
        public void ExcludeFromGroup(Uid group)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException(Workarea.Cashe.ResourceString("EX_MSG_CONNECTIONLOST", 1049));
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Entity.FindMethod("DbUid.ExcludeFromGroup").FullName; //"sp_droprolemember";
                        cmd.Parameters.Add(GlobalSqlParamNames.Rolename, SqlDbType.NVarChar, 255).Value = group.Name;
                        cmd.Parameters.Add(GlobalSqlParamNames.Membername, SqlDbType.NVarChar, 255).Value = Name;
                        cmd.ExecuteNonQuery();

                        if (group.Name.ToUpper() == "АДМИНИСТРАТОРЫ")
                        {
                            cmd.CommandText =
                                String.Format("EXEC master..sp_dropsrvrolemember @loginame = N'{0}', @rolename = N'securityadmin'", Code);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();

                            cmd.CommandText =
                                String.Format("USE MASTER GO REVOKE CONTROL SERVER TO {0}", Code);

                            cmd.ExecuteNonQuery();
                        }
                        //
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /// <summary>
        /// Проверка соответствия имени пользователя и пароля
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="userPass">Пароль</param>
        /// <returns></returns>
        public static bool IsUserPasswordValid(Workarea wa, string userName, string userPass)
        {
            return wa.Access.GetAllUsers().Exists(
                f => (f.Name.ToUpper() == userName.ToUpper()) && (f.Password.ToUpper() == userPass.ToUpper()));
        }
        /// <summary>
        /// Проверка соответствия пользователя и хеш пароля
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="userPassHash">Хеш пароля</param>
        /// <returns></returns>
        public static bool IsUserPasswordHashValid(Workarea wa, string userName, string userPassHash)
        {
            return wa.Access.GetAllUsers().Exists(
                f => (f.Name.ToUpper() == userName.ToUpper()) && (f.PasswordHash.ToUpper() == userPassHash.ToUpper()));
        }
        /// <summary>Загрузить данные из базы данных</summary>
        /// <param name="reader">Объект чтения данных<see cref="System.Data.SqlClient.SqlDataReader"/></param>
        /// <param name="endInit">Выполнять действия окончания инициализации</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _password = reader.IsDBNull(17) ? String.Empty : reader.GetString(17);
                _authenticateKind = reader.GetInt32(18);
                _agentId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _email = reader.IsDBNull(20) ? String.Empty : reader.GetString(20);
                _lastActivityDate = reader.IsDBNull(21) ? (DateTime?)null : reader.GetDateTime(21);
                _lastLockedOutDate = reader.IsDBNull(22) ? (DateTime?)null : reader.GetDateTime(22);
                _lastLoginDate = reader.IsDBNull(23) ? (DateTime?)null : reader.GetDateTime(23);
                _lastPasswordChangedDate = reader.IsDBNull(24) ? (DateTime?)null : reader.GetDateTime(24);
                _passwordQuestion = reader.IsDBNull(25) ? String.Empty : reader.GetString(25);
                _dateCreated = reader.IsDBNull(26) ? (DateTime?)null : reader.GetDateTime(26);
                _passwordSalt = reader.IsDBNull(27) ? String.Empty : reader.GetString(27);
                _passwordHash = reader.IsDBNull(28) ? String.Empty : reader.GetString(28);
                _newEmailKey = reader.IsDBNull(29) ? String.Empty : reader.GetString(29);
                _myCompanyId = reader.IsDBNull(30) ? 0 : reader.GetInt32(30);
                _remoteAddr = reader.IsDBNull(31) ? String.Empty : reader.GetString(31);
                _allowChangePassword = !reader.IsDBNull(32) && reader.GetBoolean(32);
                _recommendedDateChangePassword = reader.IsDBNull(33) ? (DateTime?)null : reader.GetDateTime(33);
                _autogenerateNextPassword = !reader.IsDBNull(34) && reader.GetBoolean(34);
                _timePeriodId = reader.IsDBNull(35) ? 0 : reader.GetInt32(35);
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
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, true, validateVersion);
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Password, SqlDbType.NVarChar, 255) {IsNullable = true};
            if (String.IsNullOrEmpty(_password))
                prm.Value = DBNull.Value;
            else
                prm.Value = _password;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.AuthenticateKind, SqlDbType.Int) {IsNullable = false, Value = _authenticateKind};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AgentId, SqlDbType.Int) {IsNullable = true};
            if (_agentId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _agentId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Email, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (String.IsNullOrEmpty(_email))
                prm.Value = DBNull.Value;
            else
                prm.Value = _email;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LastActivityDate, SqlDbType.Date) { IsNullable = true };
            if (_lastActivityDate.HasValue)
                prm.Value = _lastActivityDate;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LastLockedOutDate, SqlDbType.Date) { IsNullable = true };
            if (_lastLockedOutDate.HasValue)
                prm.Value = _lastLockedOutDate;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LastLoginDate, SqlDbType.Date) { IsNullable = true };
            if (_lastLoginDate.HasValue)
                prm.Value = _lastLoginDate;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LastPasswordChangedDate, SqlDbType.Date) { IsNullable = true };
            if (_lastPasswordChangedDate.HasValue)
                prm.Value = _lastPasswordChangedDate;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PasswordQuestion, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (String.IsNullOrEmpty(_passwordQuestion))
                prm.Value = DBNull.Value;
            else
                prm.Value = _passwordQuestion;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateCreated, SqlDbType.Date) { IsNullable = true };
            if (_dateCreated.HasValue)
                prm.Value = _dateCreated;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PasswordSalt, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (String.IsNullOrEmpty(_passwordSalt))
                prm.Value = DBNull.Value;
            else
                prm.Value = _passwordSalt;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PasswordHash, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (String.IsNullOrEmpty(_passwordHash))
                prm.Value = DBNull.Value;
            else
                prm.Value = _passwordHash;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.NewEmailKey, SqlDbType.NVarChar, 36) { IsNullable = true };
            if (String.IsNullOrEmpty(_newEmailKey))
                prm.Value = DBNull.Value;
            else
                prm.Value = _newEmailKey;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.NVarChar) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RemoteAddr, SqlDbType.NVarChar, 100) { IsNullable = true };
            if (String.IsNullOrEmpty(_remoteAddr))
                prm.Value = DBNull.Value;
            else
                prm.Value = _remoteAddr;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AllowChangePassword, SqlDbType.Bit) { IsNullable = false };
            prm.Value = _allowChangePassword;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RecommendedDateChangePassword, SqlDbType.Date) { IsNullable = true };
            if (!_recommendedDateChangePassword.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _recommendedDateChangePassword;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AutogenerateNextPassword, SqlDbType.Bit) { IsNullable = false };
            prm.Value = _autogenerateNextPassword;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TimePeriodId, SqlDbType.Int) { IsNullable = true };
            if (_timePeriodId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _timePeriodId;
            sqlCmd.Parameters.Add(prm);
        }

        #region ILinks<Library> Members
        /// <summary>
        /// Связи библиотеки
        /// </summary>
        /// <returns></returns>
        public List<IChain<Uid>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи библиотеки
        /// </summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Uid>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Uid> IChains<Uid>.SourceList(int chainKindId)
        {
            return Chain<Uid>.GetChainSourceList(this, chainKindId);
        }
        List<Uid> IChains<Uid>.DestinationList(int chainKindId)
        {
            return Chain<Uid>.DestinationList(this, chainKindId);
        }
        #endregion
        #region IChainsAdvancedList<Uid,Agent> Members

        List<IChainAdvanced<Uid, Agent>> IChainsAdvancedList<Uid, Agent>.GetLinks()
        {
            return ((IChainsAdvancedList<Uid, Agent>)this).GetLinks(18);
        }

        List<IChainAdvanced<Uid, Agent>> IChainsAdvancedList<Uid, Agent>.GetLinks(int? kind)
        {
            return GetLinkedAgents();
        }
        List<ChainValueView> IChainsAdvancedList<Uid, Agent>.GetChainView()
        {
            return ChainValueView.GetView<Uid, Agent>(this);
        }
        public List<IChainAdvanced<Uid, Agent>> GetLinkedAgents()
        {
            List<IChainAdvanced<Uid, Agent>> collection = new List<IChainAdvanced<Uid, Agent>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("LoadAgents").FullName;
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
                                ChainAdvanced<Uid, Agent> item = new ChainAdvanced<Uid, Agent> { Workarea = Workarea, Left = this };
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
    }
}
