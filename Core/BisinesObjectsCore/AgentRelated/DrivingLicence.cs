using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct DrivingLicenceStruct
    {
        /// <summary>Идентификатор организации выдавшей документ</summary>
        public int AuthorityId;
        /// <summary>Категория</summary>
        public string Category;
        /// <summary>Дата открытия категории</summary>
        public DateTime CategoryDate;
        /// <summary>Дата окончания категории</summary>
        public DateTime? CategoryExpire;
        /// <summary>Дата выдачи</summary>
        public DateTime Date;
        /// <summary>Дата окончания</summary>
        public DateTime? Expire;
        /// <summary>Номер</summary>
        public string Number;
        /// <summary>Идентификатор корреспондента</summary>
        public int OwnId;
        /// <summary>Ограничения</summary>
        public string Restriction;
        /// <summary>Серия</summary>
        public string SeriesNo;
    }
    /// <summary>
    /// Водительское удостоверение
    /// </summary>
    public class DrivingLicence : BaseCoreObject, IRelationMany
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DrivingLicence(): base()
        {
            EntityId = (short) WhellKnownDbEntity.DrivingLicence;
        }
        #region Свойства
        private int _ownId;
        /// <summary>
        /// Идентификатор корреспондента
        /// </summary>
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

        private DateTime _date;
        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date == value) return;
                OnPropertyChanging(GlobalPropertyNames.Date);
                _date = value;
                OnPropertyChanged(GlobalPropertyNames.Date);
            }
        }

        private string _seriesNo;
        /// <summary>
        /// Серия
        /// </summary>
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
        /// <summary>
        /// Номер
        /// </summary>
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


        private DateTime? _expire;
        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? Expire
        {
            get { return _expire; }
            set
            {
                if (_expire == value) return;
                OnPropertyChanging(GlobalPropertyNames.Expire);
                _expire = value;
                OnPropertyChanged(GlobalPropertyNames.Expire);
            }
        }

        private int _authorityId;
        /// <summary>
        /// Идентификатор организации выдавшей документ
        /// </summary>
        public int AuthorityId
        {
            get { return _authorityId; }
            set
            {
                if (_authorityId == value) return;
                OnPropertyChanging(GlobalPropertyNames.AuthorityId);
                _authorityId = value;
                OnPropertyChanged(GlobalPropertyNames.AuthorityId);
            }
        }

        private Agent _agentAuthority;
        /// <summary>Корреспондент, организация, выдавшая документ</summary>
        public Agent AgentAuthority
        {
            get
            {
                if (_authorityId == 0)
                    return null;
                if (_agentAuthority == null)
                    _agentAuthority = Workarea.Cashe.GetCasheData<Agent>().Item(_authorityId);
                else if (_agentAuthority.Id != _authorityId)
                    _agentAuthority = Workarea.Cashe.GetCasheData<Agent>().Item(_authorityId);
                return _agentAuthority;
            }
            set
            {
                if (_agentAuthority == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentAuthority);
                _agentAuthority = value;
                _authorityId = _agentAuthority == null ? 0 : _agentAuthority.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentAuthority);
            }
        }

        private string _category;
        /// <summary>
        /// Категория
        /// </summary>
        public string Category
        {
            get { return _category; }
            set
            {
                if (_category == value) return;
                OnPropertyChanging(GlobalPropertyNames.Category);
                _category = value;
                OnPropertyChanged(GlobalPropertyNames.Category);
            }
        }

        private DateTime _categoryDate;
        /// <summary>
        /// Дата открытия категории
        /// </summary>
        public DateTime CategoryDate
        {
            get { return _categoryDate; }
            set
            {
                if (_categoryDate == value) return;
                OnPropertyChanging(GlobalPropertyNames.CategoryDate);
                _categoryDate = value;
                OnPropertyChanged(GlobalPropertyNames.CategoryDate);
            }
        }

        private DateTime? _categoryExpire;
        /// <summary>
        /// Дата окончания категории
        /// </summary>
        public DateTime? CategoryExpire
        {
            get { return _categoryExpire; }
            set
            {
                if (_categoryExpire == value) return;
                OnPropertyChanging(GlobalPropertyNames.CategoryExpire);
                _categoryExpire = value;
                OnPropertyChanged(GlobalPropertyNames.CategoryExpire);
            }
        }

        private string _restriction;
        /// <summary>
        /// Ограничения
        /// </summary>
        public string Restriction
        {
            get { return _restriction; }
            set
            {
                if (_restriction == value) return;
                OnPropertyChanging(GlobalPropertyNames.Restriction);
                _restriction = value;
                OnPropertyChanged(GlobalPropertyNames.Restriction);
            }
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

            if (_authorityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AuthorityId, XmlConvert.ToString(_authorityId));
            if (!string.IsNullOrEmpty(_category))
                writer.WriteAttributeString(GlobalPropertyNames.Category, _category);
            //if (_categoryDate.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.CategoryDate, XmlConvert.ToString(_categoryDate));
            if (_categoryExpire.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.CategoryExpire, XmlConvert.ToString(_categoryExpire.Value));
            //if (_date.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.Date, XmlConvert.ToString(_date));
            if (_expire.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.Expire, XmlConvert.ToString(_expire.Value));
            if (!string.IsNullOrEmpty(_number))
                writer.WriteAttributeString(GlobalPropertyNames.Number, _number);
            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));
            if (!string.IsNullOrEmpty(_restriction))
                writer.WriteAttributeString(GlobalPropertyNames.Restriction, _restriction);
            if (!string.IsNullOrEmpty(_seriesNo))
                writer.WriteAttributeString(GlobalPropertyNames.SeriesNo, _seriesNo);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.AuthorityId) != null)
                _authorityId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AuthorityId));
            if (reader.GetAttribute(GlobalPropertyNames.Category) != null)
                _category = reader.GetAttribute(GlobalPropertyNames.Category);
            if (reader.GetAttribute(GlobalPropertyNames.CategoryDate) != null)
                _categoryDate = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.CategoryDate));
            if (reader.GetAttribute(GlobalPropertyNames.CategoryExpire) != null)
                _categoryExpire = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.CategoryExpire));
            if (reader.GetAttribute(GlobalPropertyNames.Date) != null)
                _date = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.Date));
            if (reader.GetAttribute(GlobalPropertyNames.Expire) != null)
                _expire = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.Expire));
            if (reader.GetAttribute(GlobalPropertyNames.Number) != null)
                _number = reader.GetAttribute(GlobalPropertyNames.Number);
            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnId));
            if (reader.GetAttribute(GlobalPropertyNames.Restriction) != null)
                _restriction = reader.GetAttribute(GlobalPropertyNames.Restriction);
            if (reader.GetAttribute(GlobalPropertyNames.SeriesNo) != null)
                _seriesNo = reader.GetAttribute(GlobalPropertyNames.SeriesNo);
        }
        #endregion

        #region Состояние
        DrivingLicenceStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new DrivingLicenceStruct
                {
                    AuthorityId = _authorityId,
                    Category = _category,
                    CategoryDate = _categoryDate,
                    CategoryExpire = _categoryExpire,
                    Date = _date,
                    Expire = _expire,
                    Number = _number,
                    OwnId = _ownId,
                    Restriction = _restriction,
                    SeriesNo = _seriesNo,
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
            AuthorityId = _baseStruct.AuthorityId;
            Category = _baseStruct.Category;
            CategoryDate = _baseStruct.CategoryDate;
            CategoryExpire = _baseStruct.CategoryExpire;
            Date = _baseStruct.Date;
            Expire = _baseStruct.Expire;
            Number = _baseStruct.Number;
            OwnId = _baseStruct.OwnId;
            Restriction = _baseStruct.Restriction;
            SeriesNo = _baseStruct.SeriesNo;

            IsChanged = false;
        }
        #endregion
        ///// <summary>Загрузить</summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.FindMethod("Contractor.DrivingLicenceLoad").FullName);
        //}
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.GetInt32(9);
                _date = reader.GetDateTime(10);
                _seriesNo = reader.GetString(11);
                _number = reader.GetString(12);
                if (reader.IsDBNull(13))
                    _expire = null;
                else
                    _expire = reader.GetDateTime(13);
                _authorityId = reader.GetInt32(14);
                _category = reader.GetString(15);
                _categoryDate = reader.GetDateTime(16);
                if (reader.IsDBNull(17))
                    _categoryExpire = null;
                else
                    _categoryExpire = reader.GetDateTime(17);
                _restriction = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.OwnId, SqlDbType.Int) {Value = _ownId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Date, SqlDbType.DateTime) {Value = _date};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SeriesNo, SqlDbType.NVarChar, 50) {Value = _seriesNo};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Number, SqlDbType.NVarChar, 50) {Value = _number};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ExpireDate, SqlDbType.DateTime);
            if (!_expire.HasValue)
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _expire;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AuthorityId, SqlDbType.Int) {Value = _authorityId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Category, SqlDbType.NVarChar, 50) {Value = _category};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CategoryDate, SqlDbType.DateTime) {Value = _categoryDate};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CategoryExpire, SqlDbType.DateTime);
            if (!_categoryExpire.HasValue)
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _categoryExpire;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Restriction, SqlDbType.NVarChar, 255);
            if (string.IsNullOrEmpty(_restriction))
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _restriction;
            }
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
