using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects.Documents
{
    internal struct DocumentWorkFlowStruct
    {
        /// <summary>Идентификатор документа</summary>
        public int OwnId;
        /// <summary>Идентификатор Workflow</summary>
        public int WfId;
        /// <summary>Текущее состояние процесса</summary>
        public string CurrentState;
        /// <summary>Идентификатор пользователя получателя</summary>
        public int UserIdTo;
        /// <summary>Идентификатор пользователя получателя</summary>
        public int UserIdFrom;
        /// <summary>Дата инициализации, планового запуска</summary>
        public DateTime Date;
        /// <summary>Описание</summary>
        public string Memo;
    }
    /// <summary>
    /// Процессы документов
    /// </summary>
    public sealed class DocumentWorkFlow : BaseCoreObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentWorkFlow()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentWorkflow;
        }
        #region Свойства
        private int _ownId;
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public int OwnId
        {
            get { return _ownId; }
            set
            {
                if (value == _ownId) return;
                OnPropertyChanging(GlobalPropertyNames.OwnId);
                _ownId = value;
                OnPropertyChanged(GlobalPropertyNames.OwnId);
            }
        }

        private int _wfId;
        /// <summary>
        /// Идентификатор Workflow
        /// </summary>
        public int WfId
        {
            get { return _wfId; }
            set
            {
                if (value == _wfId) return;
                OnPropertyChanging(GlobalPropertyNames.WfId);
                _wfId = value;
                OnPropertyChanged(GlobalPropertyNames.WfId);
            }
        }

        private string _currentState;
        /// <summary>
        /// Текущее состояние процесса
        /// </summary>
        public string CurrentState
        {
            get { return _currentState; }
            set
            {
                if (value == _currentState) return;
                OnPropertyChanging(GlobalPropertyNames.CurrentState);
                _currentState = value;
                OnPropertyChanged(GlobalPropertyNames.CurrentState);
            }
        }


        private int _userIdTo;
        /// <summary>
        /// Идентификатор пользователя получателя
        /// </summary>
        public int UserIdTo
        {
            get { return _userIdTo; }
            set
            {
                if (value == _userIdTo) return;
                OnPropertyChanging(GlobalPropertyNames.UserIdTo);
                _userIdTo = value;
                OnPropertyChanged(GlobalPropertyNames.UserIdTo);
            }
        }


        private int _userIdFrom;
        /// <summary>
        /// Идентификатор пользователя получателя
        /// </summary>
        public int UserIdFrom
        {
            get { return _userIdFrom; }
            set
            {
                if (value == _userIdFrom) return;
                OnPropertyChanging(GlobalPropertyNames.UserIdFrom);
                _userIdFrom = value;
                OnPropertyChanged(GlobalPropertyNames.UserIdFrom);
            }
        }


        private string _memo;
        /// <summary>
        /// Описание
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

        private DateTime _date;
        /// <summary>
        /// Дата инициализации, планового запуска
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value == _date) return;
                OnPropertyChanging(GlobalPropertyNames.Date);
                _date = value;
                OnPropertyChanged(GlobalPropertyNames.Date);
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

            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));
            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.WfId, XmlConvert.ToString(_wfId));
            if (string.IsNullOrEmpty(_currentState))
                writer.WriteAttributeString(GlobalPropertyNames.CurrentState, _currentState);
            if (_userIdTo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserIdTo, XmlConvert.ToString(_userIdTo));
            if (_userIdFrom != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserIdFrom, XmlConvert.ToString(_userIdFrom));
            //if (_date != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Date, XmlConvert.ToString(_date));
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

            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnId));
            if (reader.GetAttribute(GlobalPropertyNames.WfId) != null)
                _wfId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.WfId));
            if (reader.GetAttribute(GlobalPropertyNames.CurrentState) != null)
                _currentState = reader.GetAttribute(GlobalPropertyNames.CurrentState);
            if (reader.GetAttribute(GlobalPropertyNames.UserIdTo) != null)
                _userIdTo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UserIdTo));
            if (reader.GetAttribute(GlobalPropertyNames.UserIdFrom) != null)
                _userIdFrom = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UserIdFrom));
            if (reader.GetAttribute(GlobalPropertyNames.Date) != null)
                _date = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.Date));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
        }
        #endregion

        #region Методы
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.GetInt32(9);
                _wfId = reader.GetInt32(10);
                _currentState = reader.GetString(11);
                _userIdTo = reader.GetInt32(12);
                _userIdFrom = reader.GetInt32(13);
                _date = reader.GetDateTime(14);
                _memo = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.OwnId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _ownId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.WfId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _wfId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.UserIdTo, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _userIdTo;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.UserIdFrom, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _userIdFrom;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.CurrentState, SqlDbType.NVarChar, 255);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _currentState;
        }

        
        #endregion

        #region Состояния
        private DocumentWorkFlowStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new DocumentWorkFlowStruct
                                  {
                                      CurrentState = CurrentState,
                                      Date = Date,
                                      OwnId = OwnId,
                                      UserIdFrom = UserIdFrom,
                                      UserIdTo = UserIdTo,
                                      Memo = Memo,
                                      WfId = WfId
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
            CurrentState = _baseStruct.CurrentState;
            Date = _baseStruct.Date;
            OwnId = _baseStruct.OwnId;
            UserIdFrom = _baseStruct.UserIdFrom;
            UserIdTo = _baseStruct.UserIdTo;
            WfId = _baseStruct.WfId;
            Memo = _baseStruct.Memo;
            IsChanged = false;
        } 
        #endregion
    }

    /// <summary>
    /// Представление рабочих прцессов документа
    /// </summary>
    public sealed class DocumentWorkFlowView
    {
        /// <summary>
        /// Рабочая область
        /// </summary>
        public Workarea Workarea { get; set; }
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор владельца
        /// </summary>
        public int OwnId { get; set; }
        /// <summary>
        /// Идентификатор процесса
        /// </summary>
        public int WfId { get; set; }
        /// <summary>
        /// Текущее состояние
        /// </summary>
        public string CurrentState { get; set; }
        /// <summary>
        /// Идентификатор кользователя получателя
        /// </summary>
        public int UserIdTo { get; set; }
        /// <summary>
        /// Идентификатор пользователя отправителе
        /// </summary>
        public int UserIdFrom { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// Наименование процесса
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// Загрузка из базы данных
        /// </summary>
        /// <param name="reader">Объект чтения данных</param>
        public void Load(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            OwnId = reader.GetInt32(1);
            WfId = reader.GetInt32(2);
            CurrentState = reader.GetString(3);
            UserIdFrom = reader.GetInt32(4);
            UserIdTo = reader.GetInt32(5);
            Date = reader.GetDateTime(6);
            Memo = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
            WorkflowName = reader.GetString(8);
        }
        /// <summary>
        /// Преобразование в процесс документа
        /// </summary>
        /// <returns></returns>
        public DocumentWorkFlow ConvertToObject()
        {
            return ConvertToObject(this);
        }
        /// <summary>
        ///  Преобразование в процесс документа
        /// </summary>
        /// <param name="c">Представление процесса документа</param>
        /// <returns></returns>
        public static DocumentWorkFlow ConvertToObject(DocumentWorkFlowView c)
        {
            DocumentWorkFlow val = new DocumentWorkFlow { Workarea = c.Workarea, Id= c.Id };
            val.Load(c.Id);
            return val;
        }
        /// <summary>
        /// Список представлений процессов для документа 
        /// </summary>
        /// <param name="value">Документ</param>
        /// <returns></returns>
        public static List<DocumentWorkFlowView> GetView(Document value)
        {
            DocumentWorkFlowView item;
            List<DocumentWorkFlowView> collection = new List<DocumentWorkFlowView>();
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = value.Workarea.Empty<Document>().Entity.FindMethod("GetWorkFlowView").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new DocumentWorkFlowView { Workarea = value.Workarea };
                            item.Load(reader);
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