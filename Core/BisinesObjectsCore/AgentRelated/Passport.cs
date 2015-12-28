using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Паспорт физического лица</summary>
    public class Passport : BaseCoreObject, IRelationMany
    {
        /// <summary>Конструктор</summary>
        public Passport()
        {
            EntityId = (short) WhellKnownDbEntity.Passport;
        }
        #region Свойства
        private int _ownId;
        /// <summary>Идентификатор корреспондента</summary>
        public int OwnId
        {
            get { return _ownId; }
            set
            {
                if (_ownId == value) return;
                OnPropertyChanging(GlobalPropertyNames.OwnId);
                _ownId = value;
                OnPropertyChanged(GlobalPropertyNames.OwnId);
            }
        }
        private string _seriesNo;
        /// <summary>Номер серии</summary>
        public string SeriesNo
        {
            get { return _seriesNo; }
            set
            {
                if (_seriesNo == value) return;
                OnPropertyChanging(GlobalPropertyNames.SeriesNo);
                _seriesNo = value;
                OnPropertyChanged(GlobalPropertyNames.SeriesNo);
            }
        }

        private string _number;
        /// <summary>Номер</summary>
        public string Number
        {
            get { return _number; }
            set
            {
                if (_number == value) return;
                OnPropertyChanging(GlobalPropertyNames.Number);
                _number = value;
                OnPropertyChanged(GlobalPropertyNames.Number);
            }
        }

        private string _firstName;
        /// <summary>Имя</summary>
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
        /// <summary>Фамилия</summary>
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
        /// <summary>Отчетство</summary>
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

        private DateTime _birthday;
        /// <summary>День рождения</summary>
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                if (_birthday == value) return;
                OnPropertyChanging(GlobalPropertyNames.Birthday);
                _birthday = value;
                OnPropertyChanged(GlobalPropertyNames.Birthday);
            }
        }


        private string _birthTown;
        /// <summary>Где родился</summary>
        public string BirthTown
        {
            get { return _birthTown; }
            set
            {
                if (_birthTown == value) return;
                OnPropertyChanging(GlobalPropertyNames.BirthTown);
                _birthTown = value;
                OnPropertyChanged(GlobalPropertyNames.BirthTown);
            }
        }

        private string _whogives;
        /// <summary>Кем выдан паспорт</summary>
        public string Whogives
        {
            get { return _whogives; }
            set
            {
                if (_whogives == value) return;
                OnPropertyChanging(GlobalPropertyNames.Whogives);
                _whogives = value;
                OnPropertyChanged(GlobalPropertyNames.Whogives);
            }
        }

        private bool _male;
        /// <summary>Пол</summary>
        public bool Male
        {
            get { return _male; }
            set
            {
                if (_male == value) return;
                OnPropertyChanging(GlobalPropertyNames.Male);
                _male = value;
                OnPropertyChanged(GlobalPropertyNames.Male);
            }
        }

        /// <summary>
        /// Фотография
        /// </summary>
        public byte[] Signature;

        /// <summary>
        /// Роспись
        /// </summary>
        public byte[] SignatureOfficial;
        #endregion

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));
            if (!string.IsNullOrEmpty(_seriesNo))
                writer.WriteAttributeString(GlobalPropertyNames.SeriesNo, _seriesNo);
            if (!string.IsNullOrEmpty(_number))
                writer.WriteAttributeString(GlobalPropertyNames.Number, _number);
            if (!string.IsNullOrEmpty(_firstName))
                writer.WriteAttributeString(GlobalPropertyNames.FirstName, _firstName);
            if (!string.IsNullOrEmpty(_lastName))
                writer.WriteAttributeString(GlobalPropertyNames.LastName, _lastName);
            if (!string.IsNullOrEmpty(_midleName))
                writer.WriteAttributeString(GlobalPropertyNames.MidleName, _midleName);
            //if (_birthday)
                writer.WriteAttributeString(GlobalPropertyNames.Birthday, XmlConvert.ToString(_birthday));
            if (!string.IsNullOrEmpty(_birthTown))
                writer.WriteAttributeString(GlobalPropertyNames.BirthTown, _birthTown);
            if (!string.IsNullOrEmpty(_whogives))
                writer.WriteAttributeString(GlobalPropertyNames.Whogives, _whogives);
            if (_male)
                writer.WriteAttributeString(GlobalPropertyNames.Male, XmlConvert.ToString(_male));
            if (Signature!=null)
                writer.WriteAttributeString(GlobalPropertyNames.Signature, Convert.ToBase64String(Signature));
            if (SignatureOfficial != null)
                writer.WriteAttributeString(GlobalPropertyNames.SignatureOfficial, Convert.ToBase64String(SignatureOfficial));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnId));
            if (reader.GetAttribute(GlobalPropertyNames.SeriesNo) != null)
                _seriesNo = reader.GetAttribute(GlobalPropertyNames.SeriesNo);
            if (reader.GetAttribute(GlobalPropertyNames.Number) != null)
                _number = reader.GetAttribute(GlobalPropertyNames.Number);
            if (reader.GetAttribute(GlobalPropertyNames.FirstName) != null)
                _firstName = reader.GetAttribute(GlobalPropertyNames.FirstName);
            if (reader.GetAttribute(GlobalPropertyNames.LastName) != null)
                _lastName = reader.GetAttribute(GlobalPropertyNames.LastName);
            if (reader.GetAttribute(GlobalPropertyNames.MidleName) != null)
                _midleName = reader.GetAttribute(GlobalPropertyNames.MidleName);
            if (reader.GetAttribute(GlobalPropertyNames.Birthday) != null)
                _birthday = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.Birthday));
            if (reader.GetAttribute(GlobalPropertyNames.BirthTown) != null)
                _birthTown = reader.GetAttribute(GlobalPropertyNames.BirthTown);
            if (reader.GetAttribute(GlobalPropertyNames.Whogives) != null)
                _whogives = reader.GetAttribute(GlobalPropertyNames.Whogives);
            if (reader.GetAttribute(GlobalPropertyNames.Male) != null)
                _male = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Male));
            if (reader.GetAttribute(GlobalPropertyNames.Signature) != null)
                Signature = Convert.FromBase64String(reader.GetAttribute(GlobalPropertyNames.Signature));
            if (reader.GetAttribute(GlobalPropertyNames.SignatureOfficial) != null)
                SignatureOfficial = Convert.FromBase64String(reader.GetAttribute(GlobalPropertyNames.SignatureOfficial));
        }
        #endregion

        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(_seriesNo))
                throw new ValidateException("Не указана серия!");
            if (string.IsNullOrEmpty(_whogives))
                throw new ValidateException("Не указано кем выдан документ!");

            if (string.IsNullOrEmpty(_birthTown))
                throw new ValidateException("Не указано место рождения!");

            if (string.IsNullOrEmpty(_midleName))
                throw new ValidateException("Не указано отчество !");
            if (string.IsNullOrEmpty(_lastName))
                throw new ValidateException("Не указана фамилия!");
            if (string.IsNullOrEmpty(_firstName))
                throw new ValidateException("Не указано имя!");
            if (string.IsNullOrEmpty(_number))
                throw new ValidateException("Не указан номер!");

            
        }
        
        //TODO: Signature	 image
        //TODO: SignatureOfficial image
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                _seriesNo = reader.GetString(10);
                _number = reader.GetString(11);
                _firstName = reader.GetString(12);
                _lastName = reader.GetString(13);
                _midleName = reader.GetString(14);
                _birthday = reader.GetDateTime(15);
                _birthTown = reader.GetString(16);
                Signature = reader.IsDBNull(17) ? null : reader.GetSqlBinary(17).Value;
                _whogives = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                _male = reader.GetBoolean(19);
                SignatureOfficial = reader.IsDBNull(20) ? null : reader.GetSqlBinary(20).Value;
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.OwnId, SqlDbType.Int) {Value = _ownId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SeriesNo, SqlDbType.NVarChar, 50) {Value = _seriesNo};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Number, SqlDbType.NVarChar, 50) {Value = _number};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.FirstName, SqlDbType.NVarChar, 255) {Value = _firstName};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LastName, SqlDbType.NVarChar, 255) {Value = _lastName};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MidleName, SqlDbType.NVarChar, 255) {Value = _midleName};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Birthday, SqlDbType.DateTime) {Value = _birthday};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.BirthTown, SqlDbType.NVarChar, 255) {Value = _birthTown};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Signature, SqlDbType.VarBinary);
            if (Signature == null || Signature.All(v => v == 0))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = Signature.Length;
                prm.Value = Signature;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Whogives, SqlDbType.NVarChar, 255);
            if (string.IsNullOrEmpty(_whogives))
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _whogives;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Male, SqlDbType.Bit) {Value = _male};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SignatureOfficial, SqlDbType.VarBinary);
            if (SignatureOfficial == null || SignatureOfficial.All(v => v == 0))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = SignatureOfficial.Length;
                prm.Value = SignatureOfficial;
            }
            sqlCmd.Parameters.Add(prm);
        }

        #region IRelationMany Members

        string IRelationSingle.Schema
        {
            get { return GlobalSchemaNames.Contractor; }
        }

        #endregion
    }
}
