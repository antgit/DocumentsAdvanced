using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using BusinessObjects.Documents.Person;

namespace BusinessObjects.Models
{
    /// <summary>Класс работы с моделями данных в базе</summary>
    public sealed class ModelCasheData
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ModelCasheData()
        {

        }

        /// <summary>Идентификатор модели</summary>
        public int Id { get; set; }

        /// <summary>Глобальный идентификатор модели</summary>
        public Guid Guid { get; set; }

        /// <summary>Собственый идентификатор модели</summary>
        public Guid ModelId { get; set; }

        /// <summary>Данные</summary>
        public string Value { get; set; }

        /// <summary>Дата создания или изменения модели</summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>Время жизни модели</summary>
        public int TimeLive { get; set; }

        /// <summary>Идентификатор пользователя владельца</summary>
        public int UserOwnerId { get; set; }

        /// <summary>Имя типа</summary>
        public string TypeNameValue { get; set; }

        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        public void Load(SqlDataReader reader)
        {

            try
            {
                Id = reader.GetInt32(0);
                Guid = reader.GetGuid(1);
                ModelId = reader.GetGuid(2);
                Value = reader.GetString(3);
                DateCreated = reader.GetDateTime(4);
                TimeLive = reader.GetInt32(5);
                UserOwnerId = reader.GetInt32(6);
                TypeNameValue = reader.GetString(7);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
        }

        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        private void SetParametersToInsertUpdate(SqlCommand sqlCmd)
        {
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Id, SqlDbType.Int) {IsNullable = false, Value = Id};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Guid, SqlDbType.UniqueIdentifier)
                      {IsNullable = false, Value = Guid};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ModelId, SqlDbType.UniqueIdentifier)
                      {IsNullable = false, Value = ModelId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Value, SqlDbType.NVarChar, Value.Length)
                      {IsNullable = false, Value = Value};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateCreated, SqlDbType.DateTime)
                      {IsNullable = false, Value = DateCreated};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TimeLive, SqlDbType.Int) {IsNullable = false, Value = TimeLive};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserOwnerId, SqlDbType.Int)
                      {IsNullable = false, Value = UserOwnerId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TypeNameValue, SqlDbType.NVarChar, 255) { IsNullable = false, Value = TypeNameValue };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            sqlCmd.Parameters.Add(prm);

        }

        /// <summary>
        /// Сохранение в базу данных
        /// </summary>
        /// <param name="wa"></param>
        public void Save(Workarea wa)
        {
            if (wa == null) return;
            if (UserOwnerId == 0) return;
            if (ModelId.Equals(Guid.Empty)) return;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "[Core].[ModelCasheDataInsertUpdate]";
                        SetParametersToInsertUpdate(sqlCmd);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Load(reader);
                        }
                        reader.Close();

                        object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(wa.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));
                        if ((Int32) retval != 0)
                            throw new DatabaseException(wa.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049),
                                                        (Int32) retval);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }

        /// <summary>
        /// Удаление из базы данных
        /// </summary>
        /// <param name="wa"></param>
        public void Delete(Workarea wa)
        {
            if (wa == null) return;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new DatabaseException("Отсутствует соединение");
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "Core.ModelCasheDataDelete";
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.ModelId, SqlDbType.UniqueIdentifier).Value = ModelId;
                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int)
                                               {Direction = ParameterDirection.ReturnValue};
                        sqlCmd.Parameters.Add(prm);
                        sqlCmd.ExecuteNonQuery();
                        object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(wa.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((int) retval != 0)
                            throw new DatabaseException(wa.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int) retval);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }

        /// <summary>
        /// Загрузить модель по идентификатору
        /// </summary>
        /// <param name="wa"></param>
        /// <param name="modelId"></param>
        public void Load(Workarea wa, Guid modelId)
        {
            if (wa == null) return;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "[Core].[ModelCasheDataLoad]";
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.ModelId, SqlDbType.UniqueIdentifier).Value = modelId;
                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int)
                                               {Direction = ParameterDirection.ReturnValue};
                        sqlCmd.Parameters.Add(prm);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Load(reader);
                        }
                        reader.Close();

                        object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(wa.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));
                        if ((Int32) retval != 0)
                            throw new DatabaseException(wa.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049),
                                                        (Int32) retval);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }

        /// <summary>
        /// Загрузить все данные
        /// </summary>
        /// <param name="wa"></param>
        public static List<ModelCasheData> LoadAll(Workarea wa)
        {
            if (wa == null) return null;
            List<ModelCasheData> coll = new List<ModelCasheData>();

            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return null;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "[Core].[ModelCasheDataLoadAll]";
                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int)
                                               {Direction = ParameterDirection.ReturnValue};
                        sqlCmd.Parameters.Add(prm);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ModelCasheData item = new ModelCasheData();
                                item.Load(reader);
                                coll.Add(item);
                            }
                        }
                        reader.Close();

                        object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(wa.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));
                        if ((Int32) retval != 0)
                            throw new DatabaseException(wa.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049),
                                                        (Int32) retval);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            return coll;
        }
    }

    /// <summary>
    /// Кеширование моделей
    /// </summary>
    public sealed class ModelCashe
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ModelCashe()
        {
            _values = new Dictionary<string, object>();
            _dbValues = new Dictionary<Guid, ModelCasheData>();
            DbAllowStore = true;
            DbTimeLive = 1440;
        }
        /// <summary>
        /// Рабочая область
        /// </summary>
        public Workarea Workarea { get; set; }

        private Dictionary<string, object> _values;
        private Dictionary<Guid, ModelCasheData> _dbValues;

        /// <summary>
        /// Использовать только для перебора!!!
        /// </summary>
        public Dictionary<string, object> Values
        {
            get
            {
                Dictionary<string, object> newvalue = new Dictionary<string, object>(_values);
                return newvalue;
            }
        }
        /// <summary>
        /// Проверка наличия модели в кеш данных
        /// </summary>
        public bool Exists(string modelId)
        {
            if (_values != null)
            {
                return _values.ContainsKey(modelId);
            }
            return false;
        }

        /// <summary>
        /// Проверка наличия модели в кеш данных
        /// </summary>
        public object Get(string modelId)
        {
            if (_values != null)
            {
                if (_values.ContainsKey(modelId))
                    return _values[modelId];
                else // пробуем востановить из БД
                {
                    RestoreFromDbByModelId(Guid.Parse(modelId));
                    if (_values.ContainsKey(modelId))
                        return _values[modelId];
                }
            }
            return null;
        }
        /// <summary>
        /// Добавить модель в кеш данных
        /// </summary>
        public void Add(string modelId, object value, bool addtoDb=true)
        {
            if(_values!=null)
            {
                if (_values.ContainsKey(modelId))
                    _values[modelId] = value;
                else
                    _values.Add(modelId, value);

                if (addtoDb)
                    AddDb(Guid.Parse(modelId), value);
            }
        }
        /// <summary>
        /// Удалить модель из кеша
        /// </summary>
        public void Remove(string modelId)
        {
            if (_values != null)
            {
                if (_values.ContainsKey(modelId))
                {
                    _values.Remove(modelId);
                    RemoveDb(Guid.Parse(modelId));
                }
            }
        }
        /// <summary>
        /// Провести полную очистику кешированных данных
        /// </summary>
        public void CrearAll()
        {
            if (_values != null)
                _values.Clear();
        }
        /// <summary>
        /// Использовать внутренний механизм кеширования данных в базе данных
        /// </summary>
        public bool DbAllowStore { get; set; }
        /// <summary>
        /// Время жизни в минутах модели в кеше базы данных, по умолчанию 1440 минут(24 часа)
        /// </summary>
        public int DbTimeLive { get; set; }
        
        /// <summary>
        /// Создание или обновление в базе данных
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="value"></param>
        private void AddDb(Guid modelId, object value)
        {
            if (!DbAllowStore) return;
            if (!(value is IModelData))
                return;
            if (_dbValues.ContainsKey(modelId))
            {
                _dbValues[modelId].DateCreated = DateTime.Now;
                _dbValues[modelId].Value = InternalSerialize(value);
                _dbValues[modelId].Save(Workarea);
            }
            else
            {
                ModelCasheData newData = new ModelCasheData();
                newData.ModelId = modelId;
                newData.TimeLive = DbTimeLive;
                newData.UserOwnerId = (value as IModelData).ModelUserOwnerId;
                newData.Value = InternalSerialize(value);
                newData.TypeNameValue = value.GetType().AssemblyQualifiedName;
                newData.Save(Workarea);
                _dbValues.Add(modelId, newData);
            }
        }
        /// <summary>
        /// Удаление из базы данных
        /// </summary>
        /// <param name="modelId"></param>
        private void RemoveDb(Guid modelId)
        {
            if (!DbAllowStore) return;

            if (_dbValues.ContainsKey(modelId))
            {
                _dbValues[modelId].Delete(Workarea);
                _dbValues.Remove(modelId);
            }
            
        }

        private void RestoreFromDb()
        {
            List<ModelCasheData> coll = ModelCasheData.LoadAll(Workarea);
            if (coll == null)
                return;
            foreach (ModelCasheData data in coll)
            {
                try
                {
                    Type type = Type.GetType(data.TypeNameValue);
                    object v = InternalDeserialize(data.Value, type);
                    Add(data.ModelId.ToString(), v, false);
                }
                catch (Exception)
                {
                    
                }
            }
        }

        private void RestoreFromDbByModelId(Guid modelId)
        {
            try
            {
                ModelCasheData item = new ModelCasheData();
                item.Load(Workarea, modelId);
                if(item.Id!=0)
                {
                    Type type = Type.GetType(item.TypeNameValue);
                    object v = InternalDeserialize(item.Value, type);
                    Add(modelId.ToString(), v, false);
                    if (!_dbValues.ContainsKey(modelId))
                        _dbValues.Add(modelId, item);
                    else
                        _dbValues[modelId] = item;
                }
            }
            catch (Exception)
            {
            }
        }
        private string InternalSerialize(object value)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(value.GetType());
                    serializer.Serialize(sw, value);
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {

                return "SERIALIZATION ERR " + ex.Message;
            }
        }

        private object InternalDeserialize(string xml, Type type)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(type);
                    return serializer.Deserialize(sr);
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
    }

}
