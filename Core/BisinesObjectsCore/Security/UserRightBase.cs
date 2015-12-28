using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects.Security
{
    /// <summary>Разрешение пользователя или группы</summary>
    public abstract class UserRightBase : BaseCoreObject, IUserRight
    {
        /// <summary>Конструктор</summary>
        protected UserRightBase():base()
        {
        }
        #region Свойства
        private int _userId;
        /// <summary>Идентификатор пользователя</summary>
        public int DbUidId
        {
            get { return _userId; }
            set
            {
                if (_userId == value) return;
                OnPropertyChanging(GlobalPropertyNames.UserId);
                _userId = value;
                OnPropertyChanged(GlobalPropertyNames.UserId);
            }
        }

        private Uid _uid;
        /// <summary>Пользователь</summary>
        public Uid Uid
        {
            get
            {
                if (_userId == 0)
                    return null;
                if (_uid == null)
                    _uid = Workarea.Cashe.GetCasheData<Uid>().Item(_userId);
                else if (_right.Id != _userId)
                    _uid = Workarea.Cashe.GetCasheData<Uid>().Item(_userId);
                return _uid;
            }
            set
            {
                if (value == _uid) return;
                OnPropertyChanging(GlobalPropertyNames.Uid);
                _uid = value;
                _userId = _uid == null ? 0 : _uid.Id;
                OnPropertyChanged(GlobalPropertyNames.Uid);
            }
        }

        private int _rightId;
        /// <summary>Идентификатор разрешения</summary>
        public int RightId
        {
            get { return _rightId; }
            set
            {
                if (_rightId == value) return;
                OnPropertyChanging(GlobalPropertyNames.RightId);
                _rightId = value;
                OnPropertyChanged(GlobalPropertyNames.RightId);
            }
        }

        private Right _right;
        /// <summary>Разрешение</summary>
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
                if (value == _right) return;
                OnPropertyChanging(GlobalPropertyNames.Right);
                _right = value;
                _rightId = _right == null ? 0 : _right.Id;
                OnPropertyChanged(GlobalPropertyNames.Right);
            }
        }

        private short? _value;
        /// <summary>Значение</summary>
        public short? Value
        {
            get { return _value; }
            set
            {
                if (value == _value) return;
                OnPropertyChanging(GlobalPropertyNames.Value);
                _value = value;
                OnPropertyChanged(GlobalPropertyNames.Value);
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

            if (_userId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.DbUidId, XmlConvert.ToString(_userId));
            if (_rightId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.RightId, XmlConvert.ToString(_rightId));
            if (_value.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.Value, XmlConvert.ToString(_value.Value));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.DbUidId) != null)
                _userId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DbUidId));
            if (reader.GetAttribute(GlobalPropertyNames.RightId) != null)
                _rightId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RightId));
            if (reader.GetAttribute(GlobalPropertyNames.Value) != null)
                _value = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.Value));
        }
        #endregion

        /// <summary>Проверка</summary>
        /// <exception cref="ValidateException">Не указан пользователь</exception>
        /// <exception cref="ValidateException">Не указано разрешение</exception>
        public override void Validate()
        {
            base.Validate();
            if (_userId==0)
                throw new ValidateException("Не указан пользователь");
            if (_rightId==0)
                throw new ValidateException("Не указано разрешение");
        }

        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, endInit);
            try
            {
                _userId = reader.GetInt32(9);
                _rightId = reader.GetInt32(10);
                _value = reader.IsDBNull(11) ? (short?) null : reader.GetInt16(11);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }

        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "Load"</remarks>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            throw new NotSupportedException("Данный класс не поддерживает загрузку разрешений!");
        }
        /// <summary>
        /// Установить значения параметров для комманды создания
        /// </summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Sql комманда создания или обновления данных</param>
        /// <param name="validateVersion">Выполнять проверку версии. Параметр используется
        /// только для комманды обновления данных.</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.UserId, SqlDbType.Int) { IsNullable = false, Value = _userId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RightId, SqlDbType.Int) { IsNullable = false, Value = _rightId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Value, SqlDbType.SmallInt) {IsNullable = true, Value = _value};
            sqlCmd.Parameters.Add(prm);
        }
        ///// <summary>Удалить</summary>
        ///// <param name="procedureName">Наименование процедуры</param>
        ///// <returns></returns>
        //protected virtual int Delete(string procedureName)
        //{
        //    int code = 0;
        //    using (SqlConnection cnn = (this as ICoreObject).Workarea.GetDatabaseConnection())
        //    {
        //        if (cnn == null) return code;
        //        try
        //        {
        //            using (SqlCommand sqlCmd = cnn.CreateCommand())
        //            {
        //                sqlCmd.CommandType = CommandType.StoredProcedure;
        //                sqlCmd.CommandText = procedureName;
        //                sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
        //                SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int)
        //                                       {Direction = ParameterDirection.ReturnValue};
        //                sqlCmd.Parameters.Add(prm);
        //                sqlCmd.ExecuteNonQuery();
        //                code = (int)sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
        //            }
        //        }
        //        finally
        //        {
        //            if (cnn.State != ConnectionState.Closed)
        //                cnn.Close();
        //        }
        //    }
        //    return code;
        //}
    }
}
