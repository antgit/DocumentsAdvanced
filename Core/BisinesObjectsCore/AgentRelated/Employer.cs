using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// /// <summary>Структура объекта "Сотрудник"</summary>
    /// </summary>
    internal struct EmployerStruct
    {
        /// <summary>Материально ответственное лицо</summary>
        public bool Mol;
        /// <summary>Табельный номер</summary>
        public string TabNo;
        /// <summary>
        /// Дата приема на работу
        /// </summary>
        /// <remarks>Соответствует дате последнего приема на работу</remarks>
        public DateTime? DateStart;
        /// <summary>
        /// Дата увольнения
        /// </summary>
        /// <remarks>Соответствует дате последнего увольнения</remarks>
        public DateTime? DateEnd;
        /// <summary>
        /// Идентификатор категории сотрудника
        /// </summary>
        /// <remarks>Ссылка на аналитику "Категория сотрудника", например: штатный, договорник и т.д.</remarks>
        public int CategoryId;
    }
    /// <summary>
    /// Расширение данных о кореспонденте относительно сотрудника
    /// </summary>
    public class Employer : BaseCoreObject, IRelationSingle
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Employer(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Employer;
        }
        #region Свойства
        internal People _owner;
        /// <summary>
        /// Объект владелец (корреспондент)
        /// </summary>
        public People Owner
        {
            get { return _owner; }
        }
        private string _tabNo;
        /// <summary>
        /// Табельный номер
        /// </summary>
        public string TabNo
        {
            get { return _tabNo; }
            set
            {
                if (_tabNo == value) return;
                OnPropertyChanging(GlobalPropertyNames.TabNo);
                _tabNo = value;
                OnPropertyChanged(GlobalPropertyNames.TabNo);
            }
        }

        private bool _mol;
        /// <summary>
        /// Материально ответственное лицо
        /// </summary>
        public bool Mol
        {
            get { return _mol; }
            set
            {
                if (_mol == value) return;
                OnPropertyChanging(GlobalPropertyNames.Mol);
                _mol = value;
                OnPropertyChanged(GlobalPropertyNames.Mol);
            }
        }

        private DateTime? _dateStart;
        /// <summary>
        /// Дата приема на работу
        /// </summary>
        /// <remarks>Соответствует дате последнего приема на работу</remarks>
        public DateTime? DateStart
        {
            get { return _dateStart; }
            set
            {
                if (value == _dateStart) return;
                OnPropertyChanging(GlobalPropertyNames.DateStart);
                _dateStart = value;
                OnPropertyChanged(GlobalPropertyNames.DateStart);
            }
        }


        private DateTime? _dateEnd;
        /// <summary>
        /// Дата увольнения
        /// </summary>
        /// <remarks>Соответствует дате последнего увольнения</remarks>
        public DateTime? DateEnd
        {
            get { return _dateEnd; }
            set
            {
                if (value == _dateEnd) return;
                OnPropertyChanging(GlobalPropertyNames.DateEnd);
                _dateEnd = value;
                OnPropertyChanged(GlobalPropertyNames.DateEnd);
            }
        }

        private int _categoryId;
        /// <summary>
        /// Идентификатор категории сотрудника
        /// </summary>
        /// <remarks>Ссылка на аналитику "Категория сотрудника", например: штатный, договорник и т.д.</remarks>
        public int CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (value == _categoryId) return;
                OnPropertyChanging(GlobalPropertyNames.CategoryId);
                _categoryId = value;
                OnPropertyChanged(GlobalPropertyNames.CategoryId);
            }
        }

        private Analitic _category;
        /// <summary>
        /// Категория сотрудника
        /// </summary>
        /// <remarks>Ссылка на аналитику "Категория сотрудника", например: штатный, договорник и т.д.</remarks>
        public Analitic Category
        {
            get
            {
                if (_categoryId == 0)
                    return null;
                if (_category == null)
                    _category = Workarea.Cashe.GetCasheData<Analitic>().Item(_categoryId);
                else if (_category.Id != _categoryId)
                    _category = Workarea.Cashe.GetCasheData<Analitic>().Item(_categoryId);
                return _category;
            }
            set
            {
                if (_category == value) return;
                OnPropertyChanging(GlobalPropertyNames.Category);
                _category = value;
                _categoryId = _category == null ? 0 : _category.Id;
                OnPropertyChanged(GlobalPropertyNames.Category);
            }
        }
        
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_mol)
                writer.WriteAttributeString(GlobalPropertyNames.Mol, XmlConvert.ToString(_mol));
            if (!string.IsNullOrEmpty(_tabNo))
                writer.WriteAttributeString(GlobalPropertyNames.TabNo, _tabNo);
            if (DateStart.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateStart, XmlConvert.ToString(_dateStart.Value));
            if (DateEnd.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateEnd, XmlConvert.ToString(_dateEnd.Value));
            if (CategoryId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.CategoryId, XmlConvert.ToString(CategoryId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Mol) != null)
                _mol = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Mol));
            if (reader.GetAttribute(GlobalPropertyNames.TabNo) != null)
                _tabNo = reader.GetAttribute(GlobalPropertyNames.TabNo);
            if (reader.GetAttribute(GlobalPropertyNames.DateStart) != null)
                _dateStart = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateStart));
            if (reader.GetAttribute(GlobalPropertyNames.DateEnd) != null)
                _dateEnd = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateEnd));
            if (reader.GetAttribute(GlobalPropertyNames.CategoryId) != null)
                _categoryId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CategoryId));
        }
        #endregion

        #region Состояние
        EmployerStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new EmployerStruct
                {
                    Mol = _mol,
                    TabNo = _tabNo,
                    DateStart = _dateStart,
                    DateEnd = _dateEnd,
                    CategoryId = _categoryId
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
            Mol = _baseStruct.Mol;
            TabNo = _baseStruct.TabNo;
            DateStart = _baseStruct.DateStart;
            DateEnd = _baseStruct.DateEnd;
            CategoryId = _baseStruct.CategoryId;

            IsChanged = false;
        }
        #endregion
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _tabNo = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                _mol = reader.GetInt32(10) == 1 ? true : false;
                _dateStart = reader.IsDBNull(11) ? (DateTime?) null : reader.GetDateTime(11);
                _dateEnd = reader.IsDBNull(12) ? (DateTime?)null : reader.GetDateTime(12);
                _categoryId = reader.IsDBNull(13) ? 0: reader.GetInt32(13);
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.TabNo, SqlDbType.NVarChar, 50) { IsNullable = true };
            if (string.IsNullOrEmpty(_tabNo))
                prm.Value = DBNull.Value;
            else
                prm.Value = _tabNo;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Mol, SqlDbType.Int) { IsNullable = false };
            prm.Value = _mol ? 1 : 0;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateStart, SqlDbType.DateTime) { IsNullable = true };
            prm.Value = _dateStart.HasValue ? (DateTime?) null : _dateStart.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateEnd, SqlDbType.DateTime) { IsNullable = true };
            prm.Value = _dateEnd.HasValue ? (DateTime?)null : _dateEnd.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CategoryId, SqlDbType.Int) { IsNullable = true };
            prm.Value = _categoryId == 0 ? (int?) null : _categoryId;
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
