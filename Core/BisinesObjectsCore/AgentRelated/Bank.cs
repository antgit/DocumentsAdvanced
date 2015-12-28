using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct BankStruct
    {
        /// <summary>Мфо банка</summary>
        public string Mfo;
        /// <summary>Номер свидетельства регистрации НБУ</summary>
        public string SertificateNo;
        /// <summary>Дата свидетельства регистрации НБУ</summary>
        public DateTime? SertificateDate;
        /// <summary>Номер банковской лицензии</summary>
        public string LicenseNo;
        /// <summary>Дата банковской лицензии</summary>
        public DateTime? LicenseDate;
        /// <summary>S.W.I.F.T.</summary>
        public string Swift;
        /// <summary>Кореспондентский счет</summary>
        public string CorrBankAccount;
    }
    /// <summary>Банк</summary>
    public class Bank : BaseCoreObject, IRelationSingle, ICloneable
    {
        /// <summary>Конструктор</summary>
        public Bank()
            : base()
        {
            EntityId = (short) WhellKnownDbEntity.Bank;
        }
        #region ICloneable Members
        /// <summary>Копия объекта</summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return Clone(true);
        }
        /// <summary>Копия объекта</summary>
        /// <param name="endInit">Завершить инициализацию</param>
        /// <returns></returns>
        protected internal virtual Bank Clone(bool endInit)
        {
            Bank cloneObj = new Bank();

            cloneObj.Workarea = Workarea;
            cloneObj.OnBeginInit();
            cloneObj.Id = Id;
            cloneObj.Guid = Guid;
            cloneObj.DatabaseId = DatabaseId;
            cloneObj.FlagsValue = FlagsValue;
            cloneObj.DbSourceId = DbSourceId;
            cloneObj.StateId = StateId;
            cloneObj.Mfo = Mfo;
            cloneObj.SertificateNo = SertificateNo;
            cloneObj.SertificateDate = SertificateDate;
            cloneObj.LicenseNo = LicenseNo;
            cloneObj.LicenseDate = LicenseDate;
            cloneObj.Swift = Swift;
            cloneObj.CorrBankAccount = CorrBankAccount;   

            if (endInit)
                cloneObj.OnEndInit();

            return cloneObj;
        }
        #endregion
        #region Свойства
        internal Company _owner;
        /// <summary>
        /// Объект владелец (компания)
        /// </summary>
        public Company Owner
        {
            get { return _owner; }
        }
        private string _mfo;
        /// <summary>Мфо банка</summary>
        public string Mfo
        {
            get { return _mfo; }
            set
            {
                if (_mfo == value) return;
                OnPropertyChanging(GlobalPropertyNames.Mfo);
                _mfo = value;
                OnPropertyChanged(GlobalPropertyNames.Mfo);
            }
        }


        private string _sertificateNo;
        /// <summary>
        /// Номер свидетельства регистрации НБУ
        /// </summary>
        public string SertificateNo
        {
            get { return _sertificateNo; }
            set
            {
                if (value == _sertificateNo) return;
                OnPropertyChanging(GlobalPropertyNames.SertificateNo);
                _sertificateNo = value;
                OnPropertyChanged(GlobalPropertyNames.SertificateNo);
            }
        }
        
        private DateTime? _sertificateDate;
        /// <summary>
        /// Дата свидетельства регистрации НБУ
        /// </summary>
        public DateTime? SertificateDate
        {
            get { return _sertificateDate; }
            set
            {
                if (value == _sertificateDate) return;
                OnPropertyChanging(GlobalPropertyNames.SertificateDate);
                _sertificateDate = value;
                OnPropertyChanged(GlobalPropertyNames.SertificateDate);
            }
        }


        private string _licenseNo;
        /// <summary>
        /// Номер банковской лицензии
        /// </summary>
        public string LicenseNo
        {
            get { return _licenseNo; }
            set
            {
                if (value == _licenseNo) return;
                OnPropertyChanging(GlobalPropertyNames.LicenseNo);
                _licenseNo = value;
                OnPropertyChanged(GlobalPropertyNames.LicenseNo);
            }
        }

        private DateTime? _licenseDate;
        /// <summary>
        /// Дата банковской лицензии
        /// </summary>
        public DateTime? LicenseDate
        {
            get { return _licenseDate; }
            set
            {
                if (value == _licenseDate) return;
                OnPropertyChanging(GlobalPropertyNames.LicenseDate);
                _licenseDate = value;
                OnPropertyChanged(GlobalPropertyNames.LicenseDate);
            }
        }


        private string _Swift;
        /// <summary>
        /// S.W.I.F.T.
        /// </summary>
        public string Swift
        {
            get { return _Swift; }
            set
            {
                if (value == _Swift) return;
                OnPropertyChanging(GlobalPropertyNames.Swift);
                _Swift = value;
                OnPropertyChanged(GlobalPropertyNames.Swift);
            }
        }


        private string _corrBankAccount;
        /// <summary>
        /// Кореспондентский счет 
        /// </summary>
        public string CorrBankAccount
        {
            get { return _corrBankAccount; }
            set
            {
                if (value == _corrBankAccount) return;
                OnPropertyChanging(GlobalPropertyNames.CorrBankAccount);
                _corrBankAccount = value;
                OnPropertyChanged(GlobalPropertyNames.CorrBankAccount);
            }
        }
        
        
        
        /*
         	nvarchar(50)	Checked
SertificateDate	date	Checked
LicenseNo	nvarchar(50)	Checked
LicenseDate	date	Checked
Swift	nvarchar(50)	Checked
CorrBankAccount	nvarchar(50)	Checked
         */
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_mfo))
                writer.WriteAttributeString(GlobalPropertyNames.Mfo, _mfo);
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Mfo) != null)
                _mfo = reader.GetAttribute(GlobalPropertyNames.Mfo);
        }
        #endregion

        ///// <summary>Загрузить</summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.FindMethod(((IRelationSingle)this).Schema + GetType().Name).FullName);
        //}
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _mfo = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                _sertificateNo = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                if (reader.IsDBNull(11))
                    _sertificateDate = null;
                else
                    _sertificateDate = reader.GetDateTime(11);
                LicenseNo = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                if (reader.IsDBNull(13))
                    _licenseDate = null;
                else
                    _licenseDate = reader.GetDateTime(13);
                Swift = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                CorrBankAccount = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (endInit)
                OnEndInit();
        }
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            sqlCmd.Parameters[GlobalSqlParamNames.Id].Value = Id;

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Mfo, SqlDbType.NVarChar, 50);
            if (string.IsNullOrEmpty(_mfo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _mfo;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LicenseNo, SqlDbType.NVarChar, 50);
            if (string.IsNullOrEmpty(_licenseNo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _licenseNo;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LicenseDate, SqlDbType.Date);
            if (!_licenseDate.HasValue)
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _licenseDate;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SertificateNo, SqlDbType.NVarChar, 50);
            if (string.IsNullOrEmpty(_sertificateNo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _sertificateNo;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SertificateDate, SqlDbType.Date);
            if (!_sertificateDate.HasValue)
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _sertificateDate;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Swift, SqlDbType.NVarChar, 50);
            if (string.IsNullOrEmpty(_Swift))
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _Swift;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CorrBankAccount, SqlDbType.NVarChar, 50);
            if (string.IsNullOrEmpty(_corrBankAccount))
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _corrBankAccount;
            }
            sqlCmd.Parameters.Add(prm);
        }

        #region IRelationSingle Members

        string IRelationSingle.Schema
        {
            get {return GlobalSchemaNames.Contractor; }
        }

        #endregion
        #region Состояние
        BankStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new BankStruct
                {
                    Mfo = _mfo,
                    CorrBankAccount = _corrBankAccount, 
                    LicenseDate = _licenseDate, 
                    LicenseNo = _licenseNo, 
                    SertificateDate = _sertificateDate, 
                    SertificateNo=_sertificateNo, 
                    Swift=_Swift
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            Mfo = _baseStruct.Mfo;
            CorrBankAccount = _baseStruct.CorrBankAccount;
            LicenseDate = _baseStruct.LicenseDate;
            LicenseNo = _baseStruct.LicenseNo;
            SertificateDate = _baseStruct.SertificateDate;
            SertificateNo = _baseStruct.SertificateNo;
            Swift = _baseStruct.Swift;
            IsChanged = false;
        }
        #endregion
    }
}