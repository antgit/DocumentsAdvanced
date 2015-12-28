using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Базовый класс ресурсов
    /// </summary>
    /// <remarks>
    /// В классах наследниках обязательно необходимо переопределить методы <see
    /// cref="Load">Load</see> и <see
    /// cref="SetParametersToInsertUpdate">SetParametersToInsertUpdate</see>
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public abstract class KeyResource<T>: BaseCoreObject
    {
        protected KeyResource():base()
        {
            
        }
        #region Свойства
        private int _kindId;
        public int KindId
        {
            get
            {
                return _kindId;
            }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.KindId);
                _kindId = value;
                OnPropertyChanged(GlobalPropertyNames.KindId);
            }
        }
        private string _code;
        /// <summary>
        /// Код
        /// </summary>
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.Code);
                _code = value;
                OnPropertyChanged(GlobalPropertyNames.Code);
            }
        }
        private T _value;
        /// <summary>
        /// Значение
        /// </summary>
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
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

            if (_kindId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.KindId, XmlConvert.ToString(_kindId));
            if (!string.IsNullOrEmpty(_code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, _code);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.KindId) != null)
                _kindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.KindId));
            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                _code = reader.GetAttribute(GlobalPropertyNames.Code);
        }
        #endregion

        #region База данных
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _kindId = reader.GetInt32(9);
                _code = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.KindId, SqlDbType.Int) {IsNullable = false, Value = _kindId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 255) {IsNullable = true};
            if (string.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion
    }
}