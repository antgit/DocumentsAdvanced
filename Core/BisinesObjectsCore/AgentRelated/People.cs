using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Физическое лицо
    /// </summary>
    public class People : BaseCoreObject, IRelationSingle
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public People()
        {
            EntityId = (short)WhellKnownDbEntity.People;
        }

        protected override void OnCreated()
        {
            base.OnCreated();
            if (_employer != null && _employer.Id == 0)
                _employer.Id = Id;
        }
        
        #region Свойства

        internal Agent _owner;
        /// <summary>
        /// Объект владелец (корреспондент)
        /// </summary>
        public Agent Owner
        {
            get { return _owner; }
        }

        private bool _sex;
        /// <summary>
        /// Пол
        /// </summary>
        public bool Sex
        {
            get { return _sex; }
            set
            {
                if (_sex == value) return;
                OnPropertyChanging(GlobalPropertyNames.Sex);
                _sex = value;
                OnPropertyChanged(GlobalPropertyNames.Sex);
            }
        }
        /// <summary>
        /// Пол в виде строки
        /// </summary>
        public string SexName
        {
            get {return Sex ? "Женский" : "Мужской";}
        }

        private decimal _taxSocialPrivilege;
        /// <summary>
        /// Социальные привелегии
        /// </summary>
        public decimal TaxSocialPrivilege
        {
            get { return _taxSocialPrivilege; }
            set
            {
                if (_taxSocialPrivilege == value) return;
                OnPropertyChanging(GlobalPropertyNames.TaxSocialPrivilege);
                _taxSocialPrivilege = value;
                OnPropertyChanged(GlobalPropertyNames.TaxSocialPrivilege);
            }
        }

        private string _firstName;
        /// <summary>
        /// Первое имя (Имя)
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName == value) return;
                OnPropertyChanging(GlobalPropertyNames.FirstName);
                _firstName = value;
                OnPropertyChanged(GlobalPropertyNames.FirstName);
            }
        }

        private string _lastName;
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName == value) return;
                OnPropertyChanging(GlobalPropertyNames.LastName);
                _lastName = value;
                OnPropertyChanged(GlobalPropertyNames.LastName);
            }
        }

        private string _midleName;
        /// <summary>
        /// Отчество
        /// </summary>
        public string MidleName
        {
            get { return _midleName; }
            set
            {
                if (_midleName == value) return;
                OnPropertyChanging(GlobalPropertyNames.MidleName);
                _midleName = value;
                OnPropertyChanged(GlobalPropertyNames.MidleName);
            }
        }


        private string _insuranceNumber;
        /// <summary>
        /// Номер страхового свидетельства
        /// </summary>
        public string InsuranceNumber
        {
            get { return _insuranceNumber; }
            set
            {
                if (value == _insuranceNumber) return;
                OnPropertyChanging(GlobalPropertyNames.InsuranceNumber);
                _insuranceNumber = value;
                OnPropertyChanged(GlobalPropertyNames.InsuranceNumber);
            }
        }

        private string _insuranceSeries;
        /// <summary>
        /// Серия страхового свидетельства
        /// </summary>
        public string InsuranceSeries
        {
            get { return _insuranceSeries; }
            set
            {
                if (value == _insuranceSeries) return;
                OnPropertyChanging(GlobalPropertyNames.InsuranceSeries);
                _insuranceSeries = value;
                OnPropertyChanged(GlobalPropertyNames.InsuranceSeries);
            }
        }


        private int _lastPlaceWorkId;
        /// <summary>
        /// Идентификатор последнего места работы
        /// </summary>
        public int LastPlaceWorkId
        {
            get { return _lastPlaceWorkId; }
            set
            {
                if (value == _lastPlaceWorkId) return;
                OnPropertyChanging(GlobalPropertyNames.LastPlaceWorkId);
                _lastPlaceWorkId = value;
                OnPropertyChanged(GlobalPropertyNames.LastPlaceWorkId);
            }
        }


        private Agent _lastPlaceWork;
        /// <summary>
        /// Последнее место работы
        /// </summary>
        public Agent LastPlaceWork
        {
            get
            {
                if (_lastPlaceWorkId == 0)
                    return null;
                if (_lastPlaceWork == null)
                    _lastPlaceWork = Workarea.Cashe.GetCasheData<Agent>().Item(_lastPlaceWorkId);
                else if (_lastPlaceWork.Id != _lastPlaceWorkId)
                    _lastPlaceWork = Workarea.Cashe.GetCasheData<Agent>().Item(_lastPlaceWorkId);
                return _lastPlaceWork;
            }
            set
            {
                if (_lastPlaceWork == value) return;
                OnPropertyChanging(GlobalPropertyNames.LastPlaceWork);
                _lastPlaceWork = value;
                _lastPlaceWorkId = _lastPlaceWork == null ? 0 : _lastPlaceWork.Id;
                OnPropertyChanged(GlobalPropertyNames.LastPlaceWork);
            }
        }
        

        private bool _invalidity;
        /// <summary>
        /// Инвалидность
        /// </summary>
        public bool Invalidity
        {
            get { return _invalidity; }
            set
            {
                if (value == _invalidity) return;
                OnPropertyChanging(GlobalPropertyNames.Invalidity);
                _invalidity = value;
                OnPropertyChanged(GlobalPropertyNames.Invalidity);
            }
        }


        private bool _pension;
        /// <summary>
        /// Пенсионность
        /// </summary>
        public bool Pension
        {
            get { return _pension; }
            set
            {
                if (value == _pension) return;
                OnPropertyChanging(GlobalPropertyNames.Pension);
                _pension = value;
                OnPropertyChanged(GlobalPropertyNames.Pension);
            }
        }



        private bool _legalWorker;
        /// <summary>Оффициальный сотрудник</summary>
        public bool LegalWorker
        {
            get { return _legalWorker; }
            set
            {
                if (value == _legalWorker) return;
                OnPropertyChanging(GlobalPropertyNames.LegalWorker);
                _legalWorker = value;
                OnPropertyChanged(GlobalPropertyNames.LegalWorker);
            }
        }


        private int _placeEmploymentBookId;
        /// <summary>
        /// Идентификатор места хранения трудовой
        /// </summary>
        public int PlaceEmploymentBookId
        {
            get { return _placeEmploymentBookId; }
            set
            {
                if (value == _placeEmploymentBookId) return;
                OnPropertyChanging(GlobalPropertyNames.PlaceEmploymentBookId);
                _placeEmploymentBookId = value;
                OnPropertyChanged(GlobalPropertyNames.PlaceEmploymentBookId);
            }
        }



        private Analitic _placeEmploymentBook;
        /// <summary>
        /// Место хранения трудовой
        /// </summary>
        public Analitic PlaceEmploymentBook
        {
            get
            {
                if (_placeEmploymentBookId == 0)
                    return null;
                if (_placeEmploymentBook == null)
                    _placeEmploymentBook = Workarea.Cashe.GetCasheData<Analitic>().Item(_placeEmploymentBookId);
                else if (_placeEmploymentBook.Id != _placeEmploymentBookId)
                    _placeEmploymentBook = Workarea.Cashe.GetCasheData<Analitic>().Item(_placeEmploymentBookId);
                return _placeEmploymentBook;
            }
            set
            {
                if (_placeEmploymentBook == value) return;
                OnPropertyChanging(GlobalPropertyNames.PlaceEmploymentBook);
                _placeEmploymentBook = value;
                _placeEmploymentBookId = _placeEmploymentBook == null ? 0 : _placeEmploymentBook.Id;
                OnPropertyChanged(GlobalPropertyNames.PlaceEmploymentBook);
            }
        }
        

        private int _minorsId;
        /// <summary>
        /// Идентификатор несовершеннолетия
        /// </summary>
        public int MinorsId
        {
            get { return _minorsId; }
            set
            {
                if (value == _minorsId) return;
                OnPropertyChanging(GlobalPropertyNames.MinorsId);
                _minorsId = value;
                OnPropertyChanged(GlobalPropertyNames.MinorsId);
            }
        }
        
        private List<DrivingLicence> _drivingLicence;
        /// <summary>
        /// Водительское удостоверение
        /// </summary>
        public List<DrivingLicence> DrivingLicence
        {
            get
            {
                if (_drivingLicence == null)
                    RefreshDrivingLicence();
                return _drivingLicence;
            }
        }
        /// <summary>Обновить информацию о водительских правах</summary>
        private void RefreshDrivingLicence()
        {
            RelationHelper<Agent, DrivingLicence> hlp = new RelationHelper<Agent, DrivingLicence>();
            Agent ag = Workarea.Cashe.GetCasheData<Agent>().Item(Id);
            _drivingLicence = hlp.GetListObject(ag);
        }
        private List<Passport> _agentPassport;
        /// <summary>
        /// Паспорт
        /// </summary>
        public List<Passport> AgentPassport
        {
            get
            {
                if (_agentPassport == null)
                    RefreshAgentPassport();
                return _agentPassport;
            }
        }
        /// <summary>Обновить информацию о паспорте</summary>
        private void RefreshAgentPassport()
        {
            RelationHelper<Agent, Passport> hlp = new RelationHelper<Agent, Passport>();
            Agent ag = Workarea.Cashe.GetCasheData<Agent>().Item(Id);
            _agentPassport = hlp.GetListObject(ag);
        }
        private Employer _employer;
        /// <summary>
        /// Данные о сотруднике
        /// </summary>
        public Employer Employer
        {
            get
            {
                if (_employer == null)
                    RefreshEmployer();
                return _employer;
            }
        }
        /// <summary>Обновить информацию о сотруднике</summary>
        private void RefreshEmployer()
        {
            if (!IsNew)
            {
                RelationHelper<Agent, Employer> hlp = new RelationHelper<Agent, Employer>();

                Agent ag = Workarea.Cashe.GetCasheData<Agent>().Item(Id);
                _employer = hlp.GetObject(ag);
                _employer._owner = this;
            }
            else
            {
                _employer = new Employer{Workarea = Workarea};
                _employer._owner = this;
            }
        }
        #endregion

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(_firstName))
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_PEOPLEFIRSTNAME", 1049));
            if (string.IsNullOrEmpty(_lastName))
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_PEOPLELASTNAME", 1049));
            if (string.IsNullOrEmpty(_midleName))
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_PEOPLEMIDLENAME", 1049));
        }
        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_sex)
                writer.WriteAttributeString(GlobalPropertyNames.Sex, XmlConvert.ToString(_sex));
            if (_taxSocialPrivilege != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TaxSocialPrivilege, XmlConvert.ToString(_taxSocialPrivilege));
            if (!string.IsNullOrEmpty(_firstName))
                writer.WriteAttributeString(GlobalPropertyNames.FirstName, _firstName);
            if (!string.IsNullOrEmpty(_lastName))
                writer.WriteAttributeString(GlobalPropertyNames.LastName, _lastName);
            if (!string.IsNullOrEmpty(_midleName))
                writer.WriteAttributeString(GlobalPropertyNames.MidleName, _midleName);
            if (!string.IsNullOrEmpty(_insuranceNumber))
                writer.WriteAttributeString(GlobalPropertyNames.InsuranceNumber, _insuranceNumber);
            if (!string.IsNullOrEmpty(_insuranceSeries))
                writer.WriteAttributeString(GlobalPropertyNames.InsuranceSeries, _insuranceSeries);
            if (_lastPlaceWorkId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.LastPlaceWorkId, XmlConvert.ToString(_lastPlaceWorkId));
            if (_invalidity)
                writer.WriteAttributeString(GlobalPropertyNames.Invalidity, XmlConvert.ToString(_invalidity));
            if (_pension)
                writer.WriteAttributeString(GlobalPropertyNames.Pension, XmlConvert.ToString(_pension));
            if (_legalWorker)
                writer.WriteAttributeString(GlobalPropertyNames.LegalWorker, XmlConvert.ToString(_legalWorker));
            if (_placeEmploymentBookId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.PlaceEmploymentBookId, XmlConvert.ToString(_placeEmploymentBookId));
            if (_minorsId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.MinorsId, XmlConvert.ToString(_minorsId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Sex) != null)
                _sex = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Sex));
            if (reader.GetAttribute(GlobalPropertyNames.TaxSocialPrivilege) != null)
                _taxSocialPrivilege = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.TaxSocialPrivilege));
            if (reader.GetAttribute(GlobalPropertyNames.FirstName) != null)
                _firstName = reader.GetAttribute(GlobalPropertyNames.FirstName);
            if (reader.GetAttribute(GlobalPropertyNames.LastName) != null)
                _lastName = reader.GetAttribute(GlobalPropertyNames.LastName);
            if (reader.GetAttribute(GlobalPropertyNames.MidleName) != null)
                _midleName = reader.GetAttribute(GlobalPropertyNames.MidleName);
            if (reader.GetAttribute(GlobalPropertyNames.InsuranceNumber) != null)
                _insuranceNumber = reader.GetAttribute(GlobalPropertyNames.InsuranceNumber);
            if (reader.GetAttribute(GlobalPropertyNames.InsuranceSeries) != null)
                _insuranceSeries = reader.GetAttribute(GlobalPropertyNames.InsuranceSeries);
            if (reader.GetAttribute(GlobalPropertyNames.LastPlaceWorkId) != null)
                _lastPlaceWorkId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.LastPlaceWorkId));
            if (reader.GetAttribute(GlobalPropertyNames.Invalidity) != null)
                _invalidity = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Invalidity));
            if (reader.GetAttribute(GlobalPropertyNames.Pension) != null)
                _pension = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Pension));
            if (reader.GetAttribute(GlobalPropertyNames.LegalWorker) != null)
                _legalWorker = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.LegalWorker));
            if (reader.GetAttribute(GlobalPropertyNames.PlaceEmploymentBookId) != null)
                _placeEmploymentBookId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PlaceEmploymentBookId));
            if (reader.GetAttribute(GlobalPropertyNames.MinorsId) != null)
                _minorsId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MinorsId));
        }
        #endregion

        ///// <summary>Загрузить</summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.FindMethod("Contractor.PeopleLoad").FullName);
        //}
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _sex = reader.GetInt32(9) == 1 ? true : false;
                _taxSocialPrivilege = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);
                _firstName = reader.GetString(11);
                _lastName = reader.GetString(12);
                _midleName = reader.GetString(13);
                _insuranceNumber = reader.IsDBNull(14) ? null : reader.GetString(14);
                _insuranceSeries = reader.IsDBNull(15) ? null : reader.GetString(15);
                _lastPlaceWorkId = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                _invalidity = reader.GetBoolean(17);
                _pension = reader.GetBoolean(18);
                _legalWorker = reader.GetBoolean(19);
                _placeEmploymentBookId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _minorsId = reader.IsDBNull(20) ? 0 : reader.GetInt32(21);

            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (endInit)
                OnEndInit();
        }

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            sqlCmd.Parameters[GlobalSqlParamNames.Id].Value = Id;

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Sex, SqlDbType.Int) {Value = _sex ? 1 : 0};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TaxSocialPrivilege, SqlDbType.Money);
            if (_taxSocialPrivilege == 0)
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _taxSocialPrivilege;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.FirstName, SqlDbType.NVarChar, 255) {Value = _firstName};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LastName, SqlDbType.NVarChar, 255) {Value = _lastName};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MidleName, SqlDbType.NVarChar, 255) {Value = _midleName};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.InsuranceNumber, SqlDbType.NVarChar, 50) { Value = _insuranceNumber };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.InsuranceSeries, SqlDbType.NVarChar, 50) { Value = _insuranceSeries };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LastPlaceWorkId, SqlDbType.Int) { Value = _lastPlaceWorkId == 0 ? (object) DBNull.Value : _lastPlaceWorkId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Invalidity, SqlDbType.Bit) { Value = _invalidity };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Pension, SqlDbType.Bit) { Value = _pension };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LegalWorker, SqlDbType.Bit) { Value = _legalWorker };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PlaceEmploymentBookId, SqlDbType.Int) { Value = _placeEmploymentBookId == 0 ? (object) DBNull.Value : _placeEmploymentBookId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MinorsId, SqlDbType.Int) { Value = _minorsId == 0 ? (object)DBNull.Value : _minorsId };
            sqlCmd.Parameters.Add(prm);
        }

        #region IRelationSingle Members

        string IRelationSingle.Schema
        {
            get { return GlobalSchemaNames.Contractor; }
        }

        #endregion
    }
}