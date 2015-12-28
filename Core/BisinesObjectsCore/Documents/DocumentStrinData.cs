using System;
using System.Xml;
using System.Data.SqlClient;
using System.Data;

namespace BusinessObjects.Documents
{
    internal struct DocumentStringDataStruct
    {
        /// <summary>Описание</summary>
        public string Memo;

        /// <summary>Значение 1</summary>
        public string Value1;
        /// <summary>Значение 2</summary>
        public string Value2;
        /// <summary>Значение 3</summary>
        public string Value3;
        /// <summary>Значение 4</summary>
        public string Value4;
        /// <summary>Значение 5</summary>
        public string Value5;
        /// <summary>Значение 6</summary>
        public string Value6;
        /// <summary>Значение 7</summary>
        public string Value7;
        /// <summary>Значение 8</summary>
        public string Value8;
        /// <summary>Значение 9</summary>
        public string Value9;
        /// <summary>Значение 10</summary>
        public string Value10;
        /// <summary>Значение 11</summary>
        public string Value11;
        /// <summary>Значение 12</summary>
        public string Value12;
        /// <summary>Значение 13</summary>
        public string Value13;
        /// <summary>Значение 14</summary>
        public string Value14;
        /// <summary>Значение 15</summary>
        public string Value15;
    }
    /// <summary>
    /// Дополнительные строковые данные для документа
    /// </summary>
    public sealed class DocumentStringData: BaseCoreObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentStringData()
            : base()
        {
            EntityId = (short) WhellKnownDbEntity.DocumentStrinData;
        }

        #region Свойства
        private string _memo;
        /// <summary>Описание</summary>
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

        private string _value1;
        /// <summary>Значение 1</summary>
        public string Value1
        {
            get { return _value1; }
            set
            {
                if (value == _value1) return;
                OnPropertyChanging(GlobalPropertyNames.Value1);
                _value1 = value;
                OnPropertyChanged(GlobalPropertyNames.Value1);
            }
        }

        private string _value2;
        /// <summary>Значение 2</summary>
        public string Value2
        {
            get { return _value2; }
            set
            {
                if (value == _value2) return;
                OnPropertyChanging(GlobalPropertyNames.Value2);
                _value2 = value;
                OnPropertyChanged(GlobalPropertyNames.Value2);
            }
        }

        private string _value3;
        /// <summary>Значение 3</summary>
        public string Value3
        {
            get { return _value3; }
            set
            {
                if (value == _value3) return;
                OnPropertyChanging(GlobalPropertyNames.Value3);
                _value3 = value;
                OnPropertyChanged(GlobalPropertyNames.Value3);
            }
        }

        private string _value4;
        /// <summary>Значение 4</summary>
        public string Value4
        {
            get { return _value4; }
            set
            {
                if (value == _value4) return;
                OnPropertyChanging(GlobalPropertyNames.Value4);
                _value4 = value;
                OnPropertyChanged(GlobalPropertyNames.Value4);
            }
        }

        private string _value5;
        /// <summary>Значение 5</summary>
        public string Value5
        {
            get { return _value5; }
            set
            {
                if (value == _value5) return;
                OnPropertyChanging(GlobalPropertyNames.Value5);
                _value5 = value;
                OnPropertyChanged(GlobalPropertyNames.Value5);
            }
        }

        private string _Value6;
        /// <summary>Значение 6</summary>
        public string Value6
        {
            get { return _Value6; }
            set
            {
                if (value == _Value6) return;
                OnPropertyChanging(GlobalPropertyNames.Value6);
                _Value6 = value;
                OnPropertyChanged(GlobalPropertyNames.Value6);
            }
        }

        private string _Value7;
        /// <summary>Значение 7</summary>
        public string Value7
        {
            get { return _Value7; }
            set
            {
                if (value == _Value7) return;
                OnPropertyChanging(GlobalPropertyNames.Value7);
                _Value7 = value;
                OnPropertyChanged(GlobalPropertyNames.Value7);
            }
        }

        private string _Value8;
        /// <summary>Значение 8</summary>
        public string Value8
        {
            get { return _Value8; }
            set
            {
                if (value == _Value8) return;
                OnPropertyChanging(GlobalPropertyNames.Value8);
                _Value8 = value;
                OnPropertyChanged(GlobalPropertyNames.Value8);
            }
        }

        private string _value9;
        /// <summary>Значение 9</summary>
        public string Value9
        {
            get { return _value9; }
            set
            {
                if (value == _value9) return;
                OnPropertyChanging(GlobalPropertyNames.Value9);
                _value9 = value;
                OnPropertyChanged(GlobalPropertyNames.Value9);
            }
        }

        private string _value10;
        /// <summary>Значение 10</summary>
        public string Value10
        {
            get { return _value10; }
            set
            {
                if (value == _value10) return;
                OnPropertyChanging(GlobalPropertyNames.Value10);
                _value10 = value;
                OnPropertyChanged(GlobalPropertyNames.Value10);
            }
        }

        private string _value11;
        /// <summary>Значение 11</summary>
        public string Value11
        {
            get { return _value11; }
            set
            {
                if (value == _value11) return;
                OnPropertyChanging(GlobalPropertyNames.Value11);
                _value11 = value;
                OnPropertyChanged(GlobalPropertyNames.Value11);
            }
        }

        private string _value12;
        /// <summary>Значение 12</summary>
        public string Value12
        {
            get { return _value12; }
            set
            {
                if (value == _value12) return;
                OnPropertyChanging(GlobalPropertyNames.Value12);
                _value12 = value;
                OnPropertyChanged(GlobalPropertyNames.Value12);
            }
        }

        private string _value13;
        /// <summary>Значение 13</summary>
        public string Value13
        {
            get { return _value13; }
            set
            {
                if (value == _value13) return;
                OnPropertyChanging(GlobalPropertyNames.Value13);
                _value13 = value;
                OnPropertyChanged(GlobalPropertyNames.Value13);
            }
        }

        private string _value14;
        /// <summary>Значение 14</summary>
        public string Value14
        {
            get { return _value14; }
            set
            {
                if (value == _value14) return;
                OnPropertyChanging(GlobalPropertyNames.Value14);
                _value14 = value;
                OnPropertyChanged(GlobalPropertyNames.Value14);
            }
        }

        private string _value15;
        /// <summary>Значение 15</summary>
        public string Value15
        {
            get { return _value15; }
            set
            {
                if (value == _value15) return;
                OnPropertyChanging(GlobalPropertyNames.Value15);
                _value15 = value;
                OnPropertyChanged(GlobalPropertyNames.Value15);
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

            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (!string.IsNullOrEmpty(_value1))
                writer.WriteAttributeString(GlobalPropertyNames.Value1, _value1);
            if (!string.IsNullOrEmpty(_value2))
                writer.WriteAttributeString(GlobalPropertyNames.Value2, _value2);
            if (!string.IsNullOrEmpty(_value3))
                writer.WriteAttributeString(GlobalPropertyNames.Value3, _value3);
            if (!string.IsNullOrEmpty(_value4))
                writer.WriteAttributeString(GlobalPropertyNames.Value4, _value4);
            if (!string.IsNullOrEmpty(_value5))
                writer.WriteAttributeString(GlobalPropertyNames.Value5, _value5);
            if (!string.IsNullOrEmpty(_Value6))
                writer.WriteAttributeString(GlobalPropertyNames.Value6, _Value6);
            if (!string.IsNullOrEmpty(_Value7))
                writer.WriteAttributeString(GlobalPropertyNames.Value7, _Value7);
            if (!string.IsNullOrEmpty(_Value8))
                writer.WriteAttributeString(GlobalPropertyNames.Value8, _Value8);
            if (!string.IsNullOrEmpty(_value9))
                writer.WriteAttributeString(GlobalPropertyNames.Value9, _value9);
            if (!string.IsNullOrEmpty(_value10))
                writer.WriteAttributeString(GlobalPropertyNames.Value10, _value10);
            if (!string.IsNullOrEmpty(_value11))
                writer.WriteAttributeString(GlobalPropertyNames.Value11, _value11);
            if (!string.IsNullOrEmpty(_value12))
                writer.WriteAttributeString(GlobalPropertyNames.Value12, _value12);
            if (!string.IsNullOrEmpty(_value13))
                writer.WriteAttributeString(GlobalPropertyNames.Value13, _value13);
            if (!string.IsNullOrEmpty(_value14))
                writer.WriteAttributeString(GlobalPropertyNames.Value14, _value14);
            if (!string.IsNullOrEmpty(_value15))
                writer.WriteAttributeString(GlobalPropertyNames.Value15, _value15);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.Value1) != null)
                _value1 = reader.GetAttribute(GlobalPropertyNames.Value1);
            if (reader.GetAttribute(GlobalPropertyNames.Value2) != null)
                _value2 = reader.GetAttribute(GlobalPropertyNames.Value2);
            if (reader.GetAttribute(GlobalPropertyNames.Value3) != null)
                _value3 = reader.GetAttribute(GlobalPropertyNames.Value3);
            if (reader.GetAttribute(GlobalPropertyNames.Value4) != null)
                _value4 = reader.GetAttribute(GlobalPropertyNames.Value4);
            if (reader.GetAttribute(GlobalPropertyNames.Value5) != null)
                _value5 = reader.GetAttribute(GlobalPropertyNames.Value5);
            if (reader.GetAttribute(GlobalPropertyNames.Value6) != null)
                _Value6 = reader.GetAttribute(GlobalPropertyNames.Value6);
            if (reader.GetAttribute(GlobalPropertyNames.Value7) != null)
                _Value7 = reader.GetAttribute(GlobalPropertyNames.Value7);
            if (reader.GetAttribute(GlobalPropertyNames.Value8) != null)
                _Value8 = reader.GetAttribute(GlobalPropertyNames.Value8);
            if (reader.GetAttribute(GlobalPropertyNames.Value9) != null)
                _value9 = reader.GetAttribute(GlobalPropertyNames.Value9);
            if (reader.GetAttribute(GlobalPropertyNames.Value10) != null)
                _value10 = reader.GetAttribute(GlobalPropertyNames.Value10);
            if (reader.GetAttribute(GlobalPropertyNames.Value11) != null)
                _value11 = reader.GetAttribute(GlobalPropertyNames.Value11);
            if (reader.GetAttribute(GlobalPropertyNames.Value12) != null)
                _value12 = reader.GetAttribute(GlobalPropertyNames.Value12);
            if (reader.GetAttribute(GlobalPropertyNames.Value13) != null)
                _value13 = reader.GetAttribute(GlobalPropertyNames.Value13);
            if (reader.GetAttribute(GlobalPropertyNames.Value14) != null)
                _value14 = reader.GetAttribute(GlobalPropertyNames.Value14);
            if (reader.GetAttribute(GlobalPropertyNames.Value15) != null)
                _value15 = reader.GetAttribute(GlobalPropertyNames.Value15);
        }
        #endregion

        #region Методы
        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        public override void Validate()
        {
            base.Validate();
        }
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _memo = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                _value1 = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                _value2 = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                _value3 = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                _value4 = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                _value5 = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                _Value6 = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                _Value7 = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                _Value8 = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                _value9 = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                _value10 = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                _value11 = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                _value12 = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                _value13 = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                _value14 = reader.IsDBNull(23) ? string.Empty : reader.GetString(23);
                _value15 = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        ///// <summary>
        ///// Сохранить объект в базе данных
        ///// </summary>
        ///// <remarks>Если идентификатор объекта <see cref="Id"/> равен 0 
        ///// выполняется создание в противном случае обновление объекта</remarks>
        //public void Save()
        //{
        //    Validate();
        //    if (IsNew)
        //        Create(Workarea.Empty<Document>().Entity.FindMethod("SignatureInsert").FullName);
        //    else
        //        Update(Workarea.Empty<Document>().Entity.FindMethod("SignatureUpdate").FullName, true);
        //}

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, -1);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _memo;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value1, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value1))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value1;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value2, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value2))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value2;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value3, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value3))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value3;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value4, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value4))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value4;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value5, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value5))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value5;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value6, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_Value6))
                prm.Value = DBNull.Value;
            else
                prm.Value = _Value6;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value7, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_Value7))
                prm.Value = DBNull.Value;
            else
                prm.Value = _Value7;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value8, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_Value8))
                prm.Value = DBNull.Value;
            else
                prm.Value = _Value8;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value9, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value9))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value9;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value10, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value10))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value10;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value11, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value11))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value11;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value12, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value12))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value12;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value13, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value13))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value13;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value14, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value14))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value14;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Value15, SqlDbType.NVarChar, 150);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_value15))
                prm.Value = DBNull.Value;
            else
                prm.Value = _value15;
        }

        ///// <summary>
        ///// Удаление объекта из базы данных
        ///// </summary>
        //public void Delete()
        //{
        //    Workarea.DeleteById(Id, Workarea.Empty<Document>().Entity.FindMethod("SignatureDelete").FullName);    
        //}
        ///// <summary>
        ///// Загрузить данные объекта из базы данных по идентификатору
        ///// </summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.Empty<Document>().Entity.FindMethod("SignatureLoad").FullName);
        //}
        ///// <summary>
        ///// Загрузить текущий объект
        ///// </summary>
        ///// <remarks>Загрузка возможна только для объекта существующего в базе данных, чей идентификатор не равен 0</remarks>
        //public void Load()
        //{
        //    Load(Id);
        //}
        #endregion

        private DocumentStringDataStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new DocumentStringDataStruct
                {
                    Memo = Memo,
                    Value1 = Value1,
                    Value2 = Value2,
                    Value3 = Value3,
                    Value4 = Value4,
                    Value5 = Value5,
                    Value6 = Value6,
                    Value7 = Value7,
                    Value8 = Value8,
                    Value9 = Value9,
                    Value10 = Value10,
                    Value11 = Value11,
                    Value12 = Value12,
                    Value13 = Value13,
                    Value14 = Value14,
                    Value15 = Value15
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
            Memo = _baseStruct.Memo;
            Value1 = _baseStruct.Value1;
            Value2 = _baseStruct.Value2;
            Value3 = _baseStruct.Value3;
            Value4 = _baseStruct.Value4;
            Value5 = _baseStruct.Value5;
            Value6 = _baseStruct.Value6;
            Value7 = _baseStruct.Value7;
            Value8 = _baseStruct.Value8;
            Value9 = _baseStruct.Value9;
            Value10 = _baseStruct.Value10;
            Value11 = _baseStruct.Value11;
            Value12 = _baseStruct.Value12;
            Value13 = _baseStruct.Value13;
            Value14 = _baseStruct.Value14;
            Value15 = _baseStruct.Value15;
            IsChanged = false;
        }
    }
}