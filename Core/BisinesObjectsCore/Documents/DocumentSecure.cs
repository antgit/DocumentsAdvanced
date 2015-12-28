using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;
using BusinessObjects.Security;

namespace BusinessObjects.Documents
{
    /// <summary>Структура объекта "Дополнительные, персоналные разрешения документа"</summary>
    internal struct DocumentSecureStruct
    {
        /// <summary>Идентификатор документа владельца</summary>
        public int OwnId;
        /// <summary>Идентификатор разрешения</summary>
        public int RightId;
        /// <summary>Идентификатор пользователя или группы</summary>
        public int UserIdTo;
        /// <summary>Дата окончания действия разрешения</summary>
        public DateTime DateEnd;
        /// <summary>Дата начала действия разрешения</summary>
        public DateTime DateStart;
        /// <summary>Разрешено</summary>
        public bool IsAllow;
        /// <summary>Описание</summary>
        public string Memo;
    }
    /// <summary>Дополнительные, персоналные разрешения документа</summary>
    public sealed class DocumentSecure : BaseCoreObject
    {
        public static List<DocumentSecure> GetCollectionByDocumentId(Workarea wa, int id)
        {
            List<DocumentSecure> collection = new List<DocumentSecure>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<DocumentSecure>().Entity.FindMethod(GlobalMethodAlias.LoadByOwnerId).FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.OwnId, SqlDbType.Int).Value = id;
                        
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentSecure item = new DocumentSecure { Workarea = wa };
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

        // ReSharper disable InconsistentNaming
        /// <summary>Первая сторона, соответствует значению 7077889summary>
        public const int KINDID_DEFAULT = 7077889;
        /// <summary>Первая сторона, соответствует значению 1</summary>
        public const int KINDVALUE_DEFAULT = 1;
        // ReSharper restore InconsistentNaming

        /// <summary>Конструктор</summary>
        public DocumentSecure(): base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentSecure;
        }


        #region Свойства
        private int _ownId;
        /// <summary>Идентификатор документа владельца</summary>
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

        private int _rightId;
        /// <summary>Идентификатор разрешения</summary>
        public int RightId
        {
            get { return _rightId; }
            set
            {
                if (value == _rightId) return;
                OnPropertyChanging(GlobalPropertyNames.RightId);
                _rightId = value;
                OnPropertyChanged(GlobalPropertyNames.RightId);
            }
        }


        private Right _right;
        /// <summary>
        /// Разрешение
        /// </summary>
        public Right Right
        {
            get
            {
                if (_rightId == 0)
                    return null;
                if (_right == null)
                    _right = Workarea.Cashe.GetCasheData<Right>().Item(_rightId);
                else if (_right.Id != _rightId)
                    _right = Workarea.Cashe.GetCasheData<Right>().Item(_rightId);
                return _right;
            }
            set
            {
                if (_right == value) return;
                OnPropertyChanging(GlobalPropertyNames.Right);
                _right = value;
                _rightId = _right == null ? 0 : _right.Id;
                OnPropertyChanged(GlobalPropertyNames.Right);
            }
        }


        private int _userIdTo;
        /// <summary>Идентификатор пользователя или группы</summary>
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


        private Uid _userTo;
        /// <summary>
        /// Пользователь или группа которому даны разрешения
        /// </summary>
        public Uid UserTo
        {
            get
            {
                if (_userIdTo == 0)
                    return null;
                if (_userTo == null)
                    _userTo = Workarea.Cashe.GetCasheData<Uid>().Item(_userIdTo);
                else if (_userTo.Id != _userIdTo)
                    _userTo = Workarea.Cashe.GetCasheData<Uid>().Item(_userIdTo);
                return _userTo;
            }
            set
            {
                if (_userTo == value) return;
                OnPropertyChanging(GlobalPropertyNames.UserTo);
                _userTo = value;
                _userIdTo = _userTo == null ? 0 : _userTo.Id;
                OnPropertyChanged(GlobalPropertyNames.UserTo);
            }
        }

        private DateTime _dateEnd;
        /// <summary>Дата окончания действия разрешения</summary>
        public DateTime DateEnd
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


        private DateTime _dateStart;
        /// <summary>Дата начала действия разрешения</summary>
        public DateTime DateStart
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

        private bool _isAllow;
        /// <summary>Разрешено</summary>
        public bool IsAllow
        {
            get { return _isAllow; }
            set
            {
                if (value == _isAllow) return;
                OnPropertyChanging(GlobalPropertyNames.IsAllow);
                _isAllow = value;
                OnPropertyChanged(GlobalPropertyNames.IsAllow);
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
            if (_rightId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.RightId, XmlConvert.ToString(_rightId));
            if (_userIdTo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserToId, XmlConvert.ToString(_userIdTo));
            //if (_dateEnd != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DateEnd, XmlConvert.ToString(_dateEnd));
            //if (_dateStart != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DateStart, XmlConvert.ToString(_dateStart));
            if (_isAllow)
                writer.WriteAttributeString(GlobalPropertyNames.IsAllow, XmlConvert.ToString(_isAllow));
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
            if (reader.GetAttribute(GlobalPropertyNames.RightId) != null)
                _rightId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RightId));
            if (reader.GetAttribute(GlobalPropertyNames.UserToId) != null)
                _userIdTo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UserToId));
            if (reader.GetAttribute(GlobalPropertyNames.DateEnd) != null)
                _dateEnd = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateEnd));
            if (reader.GetAttribute(GlobalPropertyNames.DateStart) != null)
                _dateStart = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateStart));
            if (reader.GetAttribute(GlobalPropertyNames.IsAllow) != null)
                _isAllow = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.IsAllow));
            if (reader.GetAttribute(GlobalPropertyNames.StateId) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.StateId);
        }
        #endregion

        #region Состояние
        DocumentSecureStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new DocumentSecureStruct { OwnId=_ownId, DateEnd=_dateEnd, DateStart=_dateStart, IsAllow=_isAllow, Memo=_memo, RightId=_rightId, UserIdTo=_userIdTo };
                return true;
            }
            return false;
        }
        /// <summary>
        /// Востановить состояние объекта
        /// </summary>
        public override void RestoreState()
        {
            base.RestoreState();
            OwnId = _baseStruct.OwnId;
            DateEnd = _baseStruct.DateEnd;
            DateStart = _baseStruct.DateStart;
            IsAllow = _baseStruct.IsAllow;
            Memo = _baseStruct.Memo;
            RightId = _baseStruct.RightId;
            UserIdTo = _baseStruct.UserIdTo;
            IsChanged = false;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            if (_ownId == 0)
                throw new ValidateException("Не указан документ");
            if (_rightId == 0)
                throw new ValidateException("Не указано разрешение");
            if (_dateStart < SqlDateTime.MinValue || _dateStart > SqlDateTime.MaxValue)
                throw new ValidateException("Не корректно указана дата начала");
            if (_dateEnd < SqlDateTime.MinValue || _dateEnd > SqlDateTime.MaxValue)
                throw new ValidateException("Не корректно указана дата начала");
            if (_dateEnd < _dateStart )
                throw new ValidateException("Дата начала не может быть больше даты окончания");
        }
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.GetInt32(9);
                _rightId = reader.GetInt32(10);
                _userIdTo = reader.GetInt32(11);
                _dateEnd = reader.GetDateTime(12);
                _dateStart = reader.GetDateTime(13);
                _isAllow = reader.GetBoolean(14);
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

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.RightId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _rightId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.UserIdTo, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _userIdTo;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.DateEnd, SqlDbType.Date);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _dateEnd;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.DateStart, SqlDbType.Date);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _dateStart;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.IsAllow, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _isAllow;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _memo;
        }

        
        #endregion
        
        
    }
}