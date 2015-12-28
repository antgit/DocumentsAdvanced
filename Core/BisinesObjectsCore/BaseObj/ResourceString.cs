using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Строковые ресурсы
    /// </summary>
    /// <remarks>Используется в системных сообщениях, локализации интерфейса.</remarks>
    public class ResourceString: KeyResource<string>
    {
        #region Константы
        // ReSharper disable InconsistentNaming
        public const string BTN_CAPTION_LINK = "BTN_CAPTION_LINK";
        public const string BTN_CAPTION_FIND = "BTN_CAPTION_FIND";
        public const string BTN_CAPTION_SETUP = "BTN_CAPTION_SETUP";
        public const string STR_REPORTSERVER = "STR_REPORTSERVER";
        public const string BTN_CAPTION_EDITRB = "BTN_CAPTION_EDITRB";
        public const string BTN_CAPTION_REFRESHRS = "BTN_CAPTION_REFRESHRS";
        public const string BTN_CAPTION_BUILD = "BTN_CAPTION_BUILD";
        public const string BTN_CAPTION_IMAGEEXPORT = "BTN_CAPTION_IMAGEEXPORT";
        public const string BTN_CAPTION_IMAGEIMPORT = "BTN_CAPTION_IMAGEIMPORT";
        public const string BTN_CAPTION_UP = "BTN_CAPTION_UP";
        public const string BTN_CAPTION_EXCLUDE = "BTN_CAPTION_EXCLUDE";
        public const string BTN_CAPTION_DOWN = "BTN_CAPTION_DOWN";
        public const string STR_CAPTION_VIEWLIST = "STR_CAPTION_VIEWLIST";
        public const string STR_CAPTION_VIEW = "STR_CAPTION_VIEW";
        public const string BTN_CAPTION_PROP = "BTN_CAPTION_PROP";
        public const string BTN_CAPTON_ADDS = "BTN_CAPTON_ADDS";
        public const string BTN_CAPTION_DELETE = "BTN_CAPTION_DELETE";
        public const string STR_STANDARTACTION = "STR_STANDARTACTION";
        public const string BTN_CAPTION_CREATE = "BTN_CAPTION_CREATE";
        public const string BTN_CAPTION_REFRESH = "BTN_CAPTION_REFRESH";
        public const string BTN_CAPTION_CREATECOPY = "BTN_CAPTION_CREATECOPY"; 
        public const string BTN_CAPTION_EDIT = "BTN_CAPTION_EDIT";
        /// <summary>Добавить</summary>
        public const string BTN_CAPTON_ADD = "BTN_CAPTON_ADD";
        /// <summary>Подразделение клиента</summary>
        public const string CAPTION_DOC_AGDEP = "CAPTION_DOC_AFDEP";
        /// <summary>Подразделение</summary>
        public const string CAPTION_DOC_AGMYDEP = "CAPTION_DOC_AFMYDEP";
        public const string STR_DOC_NOTREADONLY = "STR_DOC_NOTREADONLY";
        public const string CAPTION_DOC_AGSTORE = "CAPTION_DOC_AGSTORE";
        public const string CAPTION_DOC_AGMYSTORE = "CAPTION_DOC_AGMYSTORE";
        public const string CAPTION_DOC_AGENT = "CAPTION_DOC_AGENT";
        public const string CAPTION_DOC_AGMYCOMPANY = "CAPTION_DOC_AGMYCOMPANY";
        public const string STR_DOC_ACTIONGROUP = "STR_DOC_ACTIONGROUP";
        public const string STR_DOC_ACTIONS_TIP = "STR_DOC_ACTIONS_TIP";
        public const string STR_DOC_SAVECLOSE_TIP = "STR_DOC_SAVECLOSE_TIP";
        public const string STR_DOC_SAVECLOSE_INFO = "STR_DOC_SAVECLOSE_INFO";
        public const string STR_DOC_SAVE_TIP = "STR_DOC_SAVE_TIP";
        public const string STR_DOC_SAVE_INFO = "STR_DOC_SAVE_INFO";
        public const string STR_DOC_CLOSE_TIP = "STR_DOC_CLOSE_TIP";
        public const string STR_DOC_CLOSE_INFO = "STR_DOC_CLOSE_INFO";
        public const string STR_DOC_SETNOTDONE_TIP = "STR_DOC_SETNOTDONE_TIP";
        public const string STR_DOC_SETNOTDONE = "STR_DOC_SETNOTDONE";
        public const string STR_DOC_READONLY_INFO = "STR_DOC_READONLY_INFO";
        public const string STR_DOC_READONLY_TIPF = "STR_DOC_READONLY_TIPF";
        public const string STR_DOC_READONLY = "STR_DOC_READONLY";
        public const string STR_DOC_SETDONE_TIP = "STR_DOC_SETDONE_TIP";
        public const string STR_DOC_SETDONE = "STR_DOC_SETDONE";
        public const string STR_DOC_PREVIEW_TIP = "STR_DOC_PREVIEW_TIP";
        public const string STR_DOC_PREVIEW = "STR_DOC_PREVIEW";
        public const string STR_DOC_PRINT_TIP = "STR_DOC_PRINT_TIP";
        public const string STR_DOC_PRINT = "STR_DOC_PRINT";
        public const string STR_DOC_ADDPRODUCT_TIP = "STR_DOC_ADDPRODUCT_TIP";
        public const string STR_DOC_ADDPRODUCT = "STR_DOC_ADDPRODUCT";
        public const string STR_DOC_PRODUCTINFO_TIP = "STR_DOC_PRODUCTINFO_TIP";
        public const string STR_DOC_DELETEPRODUCT_TIP = "STR_DOC_DELETEPRODUCT_TIP";
        public const string STR_DOC_DELETEPRODUCT = "STR_DOC_DELETEPRODUCT";
        public const string STR_DOC_ACTIONS = "STR_DOC_ACTIONS";
        public const string STR_DOC_PRODUCTINFO = "STR_DOC_PRODUCTINFO";
        public const string BTN_CAPTION_SAVE = "BTN_CAPTION_SAVE";
        public const string MSG_CAPERROR = "MSG_CAPERROR";
        public const string EX_MSG_ERRORSAVE = "EX_MSG_ERRORSAVE";
        public const string EX_MSG_ERRORPRINT = "EX_MSG_ERRORPRINT";
        public const string MSG_CAPATTENTION = "MSG_CAPATTENTION";
        public const string MSG_VAL_IDREQUREMENT = "MSG_VAL_IDREQUREMENT";
        public const string MSG_VAL_NAMEISEMPTY = "MSG_VAL_NAMEISEMPTY";
        internal const string MSG_VAL_METHODISEMPTY = "MSG_VAL_METHODISEMPTY";
        internal const string MSG_VAL_PROCNAMEISEMPTY = "MSG_VAL_PROCNAMEISEMPTY";
        internal const string MSG_VAL_SHEMANAMEISEMPTY = "MSG_VAL_SHEMANAMEISEMPTY";
        // ReSharper restore InconsistentNaming 
        #endregion
        /// <summary>
        /// Конструктор
        /// </summary>
        public ResourceString():base()
        {
            EntityId = (short)WhellKnownDbEntity.ResourceString;
        }
        #region Свойства
        private int _cultureId;
        /// <summary>
        /// Идентификатор языка
        /// </summary>
        public int CultureId
        {
            get
            {
                return _cultureId;
            }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.CultureId);
                _cultureId = value;
                OnPropertyChanged(GlobalPropertyNames.CultureId);
            }
        }
        private string _memo;
        /// <summary>
        /// Примечание
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set
            {
                if (value == _memo) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }

        private CultureInfo _cultureInfo;
        /// <summary>
        /// Язык
        /// </summary>
        public CultureInfo CultureInfo
        {
            get
            {
                if (_cultureId == 0)
                    return null;
                if (_cultureInfo == null)
                    _cultureInfo = CultureInfo.GetCultureInfo(_cultureId);
                else if (_cultureInfo.LCID != _cultureId)
                    _cultureInfo = CultureInfo.GetCultureInfo(_cultureId);
                return _cultureInfo;
            }
            set
            {
                if (_cultureInfo == value) return;
                OnPropertyChanging(GlobalPropertyNames.CultureInfo);
                _cultureInfo = value;
                _cultureId = _cultureInfo == null ? 0 : _cultureInfo.LCID;
                OnPropertyChanged(GlobalPropertyNames.CultureInfo);
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

            if (_cultureId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CultureId, XmlConvert.ToString(_cultureId));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.CultureId) != null)
                _cultureId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CultureId));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
        }
        #endregion

        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _memo = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                Value = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                _cultureId = reader.GetInt32(13);
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
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Value, SqlDbType.NVarChar, Value.Length) { IsNullable = false, Value = Value };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(Memo))
                prm.Value = DBNull.Value;
            else
                prm.Value = Memo;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CultureId, SqlDbType.Int) { IsNullable = false, Value = CultureId };
            sqlCmd.Parameters.Add(prm);
            
        }
    }
}