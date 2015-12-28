using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct FlagValueStruct
    {
        /// <summary>Код</summary>
        public string Code;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>Наименование</summary>
        public string Name;
        /// <summary>Числовое значение системного типа</summary>
        public short ToEntityId;
        /// <summary>Значение</summary>
        public int Value;
    }
    /// <summary>Значение флага</summary>
    /// <remarks>Объекты имеющие атрибут "Системный" запрещено удалять.
    /// Объекты имеющие атрибут "Только чтение" запрещено редактировать, можно только изменить атрибуты.
    /// Объекты имеющие атрибут "Скрытый" запрещено отображать в списках, кроме администраторов системы.
    /// Объекты имеющие атрибут "Шаблон" могут использоваться как шаблоны для создания других объектов.
    /// Объекты имеющие атрибут "Архивный" означают старые объекты использование которых крайне редко.
    /// Объекты имеющие атрибут "Необновляемый" означают объекты которые исключаются из системы обновления при синхронизации данных.
    /// Объекты имеющие атрибут "Обязательный" используются в правилах и процессах.
    /// Объекты имеющие атрибут "Рекомендуемый" используются в правилах и процессах.
    /// <para>
    /// Флаги со значением 16 и 32 являются собственными для различных объектов и имеют собственное значение
    /// </para>
    /// </remarks>
    public sealed class FlagValue : BaseCoreObject
    {
// ReSharper disable InconsistentNaming
        /// <summary>"Шаблон", соответствует значению 1</summary>
        public const int FLAGTEMPLATE = 1;
        /// <summary>"Только чтение", соответствует значению 2</summary>
        public const int FLAGREADONLY = 2;
        /// <summary>"Скрытый", соответствует значению 4</summary>
        public const int FLAGHIDEN = 4;
        /// <summary>"Системный", соответствует значению 8.</summary>
        public const int FLAGSYSTEM = 8;
        /// <summary>"Обязательный" - используются в правилах, процессах и дополнительных параметрах (например в системных параметрах означает возможность пользовательского значения), соответствует значению 16.</summary>
        public const int FLAGREQUIRED = 16;
        /// <summary>"Рекомендуемый" - используются в правилах и процессах, соответствует значению 32.</summary>
        public const int FLAGRECOMMENDED = 32;
        /// <summary>"Необновляемый" - используется для исключения из обновления данных, соответствует значению 64.</summary>
        public const int EXCNOUPDATE = 64;
        /// <summary>"Авхивный", соответствует значению 16.</summary>
        public const int FLAGARHIVE = 128;
        
// ReSharper restore InconsistentNaming        
        /// <summary>Конструктор</summary>
        public FlagValue(): base()
        {
            EntityId = (short)WhellKnownDbEntity.FlagValue;
        }

        #region Свойства
        private short _toEntityId;
        /// <summary>Числовое значение системного типа</summary>
        public short ToEntityId
        {
            get { return _toEntityId; }
            set
            {
                if (_toEntityId == value) return;
                OnPropertyChanging(GlobalPropertyNames.ToEntityId);
                _toEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.ToEntityId);
            }
        }

        private string _code;
        /// <summary>Код</summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code == value) return;
                OnPropertyChanging(GlobalPropertyNames.Code);
                _code = value;
                OnPropertyChanged(GlobalPropertyNames.Code);
            }
        }

        private string _name;
        /// <summary>Наименование</summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                OnPropertyChanging(GlobalPropertyNames.Name);
                _name = value;
                OnPropertyChanged(GlobalPropertyNames.Name);
            }
        }

        private string _memo;
        /// <summary>Примечание</summary>
        public string Memo
        {
            get { return _memo; }
            set
            {
                if (_memo == value) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }

        private int _value;
        /// <summary>Значение</summary>
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                OnPropertyChanging(GlobalPropertyNames.Value);
                _value = value;
                OnPropertyChanged(GlobalPropertyNames.Value);
            }
        }
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, _code);
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
            if(_toEntityId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.ToEntityId, XmlConvert.ToString(_toEntityId));
            if (_value != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Value, XmlConvert.ToString(_value));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                _code = reader.GetAttribute(GlobalPropertyNames.Code);
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
            if (reader.GetAttribute(GlobalPropertyNames.ToEntityId) != null)
                _toEntityId = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.ToEntityId));
            if (reader.GetAttribute(GlobalPropertyNames.Value) != null)
                _value = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Value));
        }
        #endregion

        #region Состояние
        FlagValueStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new FlagValueStruct
                {
                    Code = _code,
                    Memo = _memo,
                    Name = _name,
                    ToEntityId = _toEntityId,
                    Value = _value,
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            Code = _baseStruct.Code;
            Memo = _baseStruct.Memo;
            Name = _baseStruct.Name;
            ToEntityId = _baseStruct.ToEntityId;
            Value = _baseStruct.Value;

            IsChanged = false;
        }
        #endregion
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(_name))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_NAMEISEMPTY, 1049));
            if (!string.IsNullOrEmpty(_code) && _code.Length > 50)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_NAMELENTH", 1049));
        }
        /// <summary>Максимальный флаг для системного типа</summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="dbEntityId">Идентификатор системного типа</param>
        /// <returns></returns>
        public static int GetMaxValue(IWorkarea wa, int dbEntityId)
        {
            int res;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<FlagValue>().FindProcedure("MaxByDbEntity");

                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = dbEntityId;
                        res = (int)cmd.ExecuteScalar();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return res;
        }
        #region База данных
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _toEntityId = reader.GetInt16(9);
                _code = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                _name = reader.GetString(11);
                _memo = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                _value = reader.GetInt32(13);
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
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ToEntityId, SqlDbType.SmallInt) {IsNullable = false, Value = _toEntityId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 255) {IsNullable = true};
            if (string.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255)
                      {
                          IsNullable = false,
                          Value = _name
                      };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255) {IsNullable = true};
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _memo.Length;
                prm.Value = _memo;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.FlagValue, SqlDbType.Int) {IsNullable = false, Value = _value};
            sqlCmd.Parameters.Add(prm);
            
        }
        #endregion
    }
}
