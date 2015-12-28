using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Служба"</summary>
    internal struct WebServiceStruct
    {
        /// <summary>Адрес</summary>
        public string UrlAddress;
        /// <summary>Пользователь</summary>
        public string Uid;
        /// <summary>Пароль</summary>
        public string Password;
        /// <summary>Тип авторизации</summary>
        public int AuthKind;
        /// <summary>Хранить пароль</summary>
        public bool StorePassword;
        /// <summary>Порт</summary>
        public int Port;
        public string BindingType;
        public string ServiceContract;
        /// <summary>Использовать HTTPS</summary>
        public bool UseHttps;
        public string InternalClientConfiguration;
        public string EndPointBehavior;
        public string ServiceBinding;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>Служба</summary>
    public sealed class WebService : BaseCore<WebService>, IChains<WebService>, IEquatable<WebService>,
                                     IComparable, IComparable<WebService>,
                                     IFacts<WebService>,
                                     ICodes<WebService>, ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>SQL Reporting Service 1</summary>
        public const int KINDVALUE_SSRS = 1;
        /// <summary>Служба обновления серверных отчетов, соответствует значению 2</summary>
        public const int KINDVALUE_REPUPDATE = 2;
        /// <summary>Служба обновления, соответствует значению 3</summary>
        public const int KINDVALUE_UPDATE = 3;
        /// <summary>Служба файлового обмена, соответствует значению 4</summary>
        public const int KINDVALUE_FILE = 4;
        /// <summary>Служба синхронизации, соответствует значению 5</summary>
        public const int KINDVALUE_EXC = 5;


        /// <summary>SQL Reporting Service, соответствует значению 5373953</summary>
        public const int KINDID_SSRS = 5373953;
        /// <summary>Служба обновления серверных отчетов, соответствует значению 5373954</summary>
        public const int KINDID_REPUPDATE = 5373954;
        /// <summary>Служба обновления, соответствует значению 5373955</summary>
        public const int KINDID_UPDATE = 5373955;
        /// <summary>Служба файлового обмена, соответствует значению 5373956</summary>
        public const int KINDID_FILE = 5373956;
        /// <summary>Служба синхронизации, соответствует значению 5373957</summary>
        public const int KINDID_EXC = 5373957;
        
        // ReSharper restore InconsistentNaming

        #endregion
        bool IEquatable<WebService>.Equals(WebService other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// Сравнение двух объектов по идентификатору
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            WebService otherPerson = (WebService)obj;
            return Id.CompareTo(otherPerson.Id);
        }
        /// <summary>
        /// Сравнение двух объектов по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(WebService other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>Конструктор</summary>
        public WebService()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.WebService;
        }
        protected override void CopyValue(WebService template)
        {
            base.CopyValue(template);
            UrlAddress = template.UrlAddress;
            Uid = template.Uid;
            Password = template.Password;
            AuthKind = template.AuthKind;
            StorePassword = template.StorePassword;
            Port = template.Port;
            BindingType = template.BindingType;
            ServiceContract = template.ServiceContract;
            UseHttps = template.UseHttps;
            InternalClientConfiguration = template.InternalClientConfiguration;
            EndPointBehavior = template.EndPointBehavior;
            ServiceBinding = template.ServiceBinding;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override WebService Clone(bool endInit)
        {
            WebService obj = base.Clone(false);
            obj.UrlAddress = UrlAddress;
            obj.Uid = Uid;
            obj.Password = Password;
            obj.AuthKind = AuthKind;
            obj.StorePassword = StorePassword;
            obj.Port = Port;
            obj.BindingType = BindingType;
            obj.ServiceContract = ServiceContract;
            obj.UseHttps = UseHttps;
            obj.InternalClientConfiguration = InternalClientConfiguration;
            obj.EndPointBehavior = EndPointBehavior;
            obj.ServiceBinding = ServiceBinding;
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства
        private string _urlAddress;
        /// <summary>
        /// Адрес службы
        /// </summary>
        public string UrlAddress
        {
            get { return _urlAddress; }
            set
            {
                if (value == _urlAddress) return;
                OnPropertyChanging(GlobalPropertyNames.UrlAddress);
                _urlAddress = value;
                OnPropertyChanged(GlobalPropertyNames.UrlAddress);
            }
        }


        private string _uid;
        /// <summary>
        /// Пользователь
        /// </summary>
        public string Uid
        {
            get { return _uid; }
            set
            {
                if (value == _uid) return;
                OnPropertyChanging(GlobalPropertyNames.Uid);
                _uid = value;
                OnPropertyChanged(GlobalPropertyNames.Uid);
            }
        }


        private string _password;
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                OnPropertyChanging(GlobalPropertyNames.Password);
                _password = value;
                OnPropertyChanged(GlobalPropertyNames.Password);
            }
        }


        private int _authKind;
        /// <summary>
        /// Тип авторизации
        /// </summary>
        /// <remarks>
        /// 0 - Нет авторизации
        /// 1 - Windows авторизация
        /// 2 - На основе форм
        /// </remarks>
        public int AuthKind
        {
            get { return _authKind; }
            set
            {
                if (value == _authKind) return;
                OnPropertyChanging(GlobalPropertyNames.AuthKind);
                _authKind = value;
                OnPropertyChanged(GlobalPropertyNames.AuthKind);
            }
        }

        private bool _storePassword;
        /// <summary>
        /// Хранить пароль
        /// </summary>
        public bool StorePassword
        {
            get { return _storePassword; }
            set
            {
                if (value == _storePassword) return;
                OnPropertyChanging(GlobalPropertyNames.StorePassword);
                _storePassword = value;
                OnPropertyChanged(GlobalPropertyNames.StorePassword);
            }
        }
        
        private int _port;
        /// <summary>Порт</summary>
        [DefaultValue(80)]
        public int Port
        {
            get { return _port; }
            set
            {
                if (value == _port) return;
                OnPropertyChanging(GlobalPropertyNames.Port);
                _port = value;
                OnPropertyChanged(GlobalPropertyNames.Port);
            }
        }


        private string _bindingType;
        public string BindingType
        {
            get { return _bindingType; }
            set
            {
                if (value == _bindingType) return;
                OnPropertyChanging(GlobalPropertyNames.BindingType);
                _bindingType = value;
                OnPropertyChanged(GlobalPropertyNames.BindingType);
            }
        }

        private string _serviceContract;
        public string ServiceContract
        {
            get { return _serviceContract; }
            set
            {
                if (value == _serviceContract) return;
                OnPropertyChanging(GlobalPropertyNames.ServiceContract);
                _serviceContract = value;
                OnPropertyChanged(GlobalPropertyNames.ServiceContract);
            }
        }


        private bool _useHttps;
        /// <summary>
        /// Использовать HTTPS
        /// </summary>
        public bool UseHttps
        {
            get { return _useHttps; }
            set
            {
                if (value == _useHttps) return;
                OnPropertyChanging(GlobalPropertyNames.UseHttps);
                _useHttps = value;
                OnPropertyChanged(GlobalPropertyNames.UseHttps);
            }
        }

        private string _internalClientConfiguration;
        public string InternalClientConfiguration
        {
            get { return _internalClientConfiguration; }
            set
            {
                if (value == _internalClientConfiguration) return;
                OnPropertyChanging(GlobalPropertyNames.InternalClientConfiguration);
                _internalClientConfiguration = value;
                OnPropertyChanged(GlobalPropertyNames.InternalClientConfiguration);
            }
        }

        private string _endPointBehavior;
        public string EndPointBehavior
        {
            get { return _endPointBehavior; }
            set
            {
                if (value == _endPointBehavior) return;
                OnPropertyChanging(GlobalPropertyNames.EndPointBehavior);
                _endPointBehavior = value;
                OnPropertyChanged(GlobalPropertyNames.EndPointBehavior);
            }
        }


        private string _serviceBinding;
        public string ServiceBinding
        {
            get { return _serviceBinding; }
            set
            {
                if (value == _serviceBinding) return;
                OnPropertyChanging(GlobalPropertyNames.ServiceBinding);
                _serviceBinding = value;
                OnPropertyChanged(GlobalPropertyNames.ServiceBinding);
            }
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
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_urlAddress))
                writer.WriteAttributeString(GlobalPropertyNames.UrlAddress, _urlAddress);
            if (!string.IsNullOrEmpty(_uid))
                writer.WriteAttributeString(GlobalPropertyNames.Uid, _uid);
            if (!string.IsNullOrEmpty(_password))
                writer.WriteAttributeString(GlobalPropertyNames.Password, _password);
            if (_authKind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AuthKind, XmlConvert.ToString(_authKind));
            if (_storePassword)
                writer.WriteAttributeString(GlobalPropertyNames.AuthKind, XmlConvert.ToString(_storePassword));

            writer.WriteAttributeString(GlobalPropertyNames.Port, XmlConvert.ToString(_port));

            if (!string.IsNullOrEmpty(_bindingType)) 
                writer.WriteAttributeString(GlobalPropertyNames.BindingType, _bindingType);
            if (!string.IsNullOrEmpty(_serviceContract))
                writer.WriteAttributeString(GlobalPropertyNames.ServiceContract, _serviceContract);
            if (_useHttps)
                writer.WriteAttributeString(GlobalPropertyNames.UseHttps, XmlConvert.ToString(_useHttps));
            if (!string.IsNullOrEmpty(_internalClientConfiguration))
                writer.WriteAttributeString(GlobalPropertyNames.InternalClientConfiguration, _internalClientConfiguration);
            if (!string.IsNullOrEmpty(_endPointBehavior))
                writer.WriteAttributeString(GlobalPropertyNames.EndPointBehavior, _endPointBehavior);
            if (!string.IsNullOrEmpty(_serviceBinding))
                writer.WriteAttributeString(GlobalPropertyNames.ServiceBinding, _serviceBinding);
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.UrlAddress) != null)
                _urlAddress = reader[GlobalPropertyNames.UrlAddress];
            if (reader.GetAttribute(GlobalPropertyNames.Uid) != null)
                _uid = reader[GlobalPropertyNames.Uid];
            if (reader.GetAttribute(GlobalPropertyNames.Password) != null)
                _password = reader[GlobalPropertyNames.Password];
            if (reader.GetAttribute(GlobalPropertyNames.AuthKind) != null)
                _authKind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AuthKind));
            if (reader.GetAttribute(GlobalPropertyNames.StorePassword) != null)
                _storePassword = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.StorePassword));
            if (reader.GetAttribute(GlobalPropertyNames.Port) != null)
                _port= XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Port));
            if (reader.GetAttribute(GlobalPropertyNames.BindingType) != null)
                _bindingType = reader[GlobalPropertyNames.BindingType];
            if (reader.GetAttribute(GlobalPropertyNames.ServiceContract) != null)
                _serviceContract = reader[GlobalPropertyNames.ServiceContract];
            if (reader.GetAttribute(GlobalPropertyNames.UseHttps) != null)
                _useHttps = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.UseHttps));
            if (reader.GetAttribute(GlobalPropertyNames.ServiceContract) != null)
                _serviceContract = reader[GlobalPropertyNames.ServiceContract];
            if (reader.GetAttribute(GlobalPropertyNames.InternalClientConfiguration) != null)
                _internalClientConfiguration = reader[GlobalPropertyNames.InternalClientConfiguration];
            if (reader.GetAttribute(GlobalPropertyNames.EndPointBehavior) != null)
                _endPointBehavior = reader[GlobalPropertyNames.EndPointBehavior];
            if (reader.GetAttribute(GlobalPropertyNames.ServiceBinding) != null)
                _serviceBinding = reader[GlobalPropertyNames.ServiceBinding];
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion

        #region Состояние
        WebServiceStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new WebServiceStruct { UrlAddress = _urlAddress, Uid = _uid, Password = _password, AuthKind = _authKind, StorePassword= _storePassword,
                                                     Port = _port, BindingType = _bindingType, ServiceContract = _serviceContract,
                                                     UseHttps = _useHttps, InternalClientConfiguration = _internalClientConfiguration,
                                                     EndPointBehavior = _endPointBehavior, ServiceBinding = _serviceBinding, MyCompanyId = _myCompanyId
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            UrlAddress = _baseStruct.UrlAddress;
            Uid = _baseStruct.Uid;
            Password = _baseStruct.Password;
            AuthKind = _baseStruct.AuthKind;
            StorePassword = _baseStruct.StorePassword;
            Port = _baseStruct.Port;
            BindingType = _baseStruct.BindingType;
            ServiceContract = _baseStruct.ServiceContract;
            UseHttps = _baseStruct.UseHttps;
            InternalClientConfiguration = _baseStruct.InternalClientConfiguration;
            EndPointBehavior = _baseStruct.EndPointBehavior;
            ServiceBinding = _baseStruct.ServiceBinding;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            
            if (string.IsNullOrEmpty(UrlAddress))
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_WEBSERVICEURL", 1049));
        }

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
                _urlAddress = reader.GetString(17);
                _password = reader.IsDBNull(18)? string.Empty: reader.GetString(18);
                _uid = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                _authKind = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _storePassword = reader.IsDBNull(21) ? false : reader.GetBoolean(21);
                _port = reader.IsDBNull(22) ? 0 : reader.GetInt32(22);
                _bindingType = reader.IsDBNull(23) ? string.Empty : reader.GetString(23);
                _serviceContract = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                _useHttps = reader.IsDBNull(25) ? false : reader.GetBoolean(25);
                _internalClientConfiguration = reader.IsDBNull(26) ? string.Empty : reader.GetString(26);
                _endPointBehavior = reader.IsDBNull(27) ? string.Empty : reader.GetString(27);
                _serviceBinding = reader.IsDBNull(28) ? string.Empty : reader.GetString(28);
                _myCompanyId = reader.IsDBNull(29) ? 0 : reader.GetInt32(29);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.UrlAddress, SqlDbType.NVarChar, 255) { IsNullable = false, Value = _urlAddress };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Uid, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_uid))
                prm.Value = DBNull.Value;
            else
                prm.Value = _uid;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Password, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_password))
                prm.Value = DBNull.Value;
            else
                prm.Value = _password;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.AuthKind, SqlDbType.Int) { IsNullable = false, Value = _authKind };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.StorePassword, SqlDbType.Bit) { IsNullable = false, Value = _storePassword };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Port, SqlDbType.Int) { IsNullable = false, Value = _port };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.BindingType, SqlDbType.NVarChar, 50) { IsNullable = true };
            if(string.IsNullOrEmpty(_bindingType))
                prm.Value = DBNull.Value;
            else
                prm.Value = _bindingType;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ServiceContract, SqlDbType.NVarChar, 255) { IsNullable = true };
            if(string.IsNullOrEmpty(_serviceContract))
                prm.Value = DBNull.Value;
            else
                prm.Value = _serviceContract;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UseHttps, SqlDbType.Bit) { IsNullable = false, Value = _useHttps};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.InternalClientConfiguration, SqlDbType.NVarChar, 255) { IsNullable = true };
            if(string.IsNullOrEmpty(_internalClientConfiguration))
                prm.Value = DBNull.Value;
            else
                prm.Value = _internalClientConfiguration;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EndPointBehavior, SqlDbType.NVarChar, 255) { IsNullable = true };
            if(string.IsNullOrEmpty(_endPointBehavior))
                prm.Value = DBNull.Value;
            else
                prm.Value = _endPointBehavior;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ServiceBinding, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_serviceBinding))
                prm.Value = DBNull.Value;
            else
                prm.Value = _serviceBinding;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
            
        }
        #endregion

        #region ILinks<WebService> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<WebService>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<WebService>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<WebService> IChains<WebService>.SourceList(int chainKindId)
        {
            return Chain<WebService>.GetChainSourceList(this, chainKindId);
        }
        List<WebService> IChains<WebService>.DestinationList(int chainKindId)
        {
            return Chain<WebService>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<WebService>> GetValues(bool allKinds)
        {
            return CodeHelper<WebService>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<WebService>.GetView(this, true);
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
    }
}