using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct CodeValueStruct
    {
        /// <summary>Признак</summary>
        public string Value;
        /// <summary>Идентификатор объекта</summary>
        public int ElementId;
        /// <summary>Идентификатор кода</summary>
        public int CodeNameId;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>Номер в списке</summary>
        public int OrderNo;
    }
    /// <summary>
    /// Значение кода
    /// </summary>
    public sealed class CodeValue<T> : BaseCoreObject, IComparable, ICode<T> where T : class, IBase, new()
        
    {
        /// <summary>Конструктор</summary>
        public CodeValue()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.CodeValue;
        }


        public int CompareTo(object obj)
        {
            ICode<T> otherPerson = (ICode<T>)obj;
            return Id.CompareTo(otherPerson.Id);
        }


        #region Свойства

        private int _codeNameId;
        /// <summary>
        /// Идентификатор кода
        /// </summary>
        public int CodeNameId
        {
            get { return _codeNameId; }
            set
            {
                if (value == _codeNameId) return;
                OnPropertyChanging(GlobalPropertyNames.CodeNameId);
                _codeNameId = value;
                OnPropertyChanged(GlobalPropertyNames.CodeNameId);
            }
        }


        private CodeName _codeName;
        /// <summary>
        /// Наименование кода
        /// </summary>
        public CodeName CodeName
        {
            get
            {
                if (_codeNameId == 0)
                    return null;
                if (_codeName == null)
                    _codeName = Workarea.Cashe.GetCasheData<CodeName>().Item(_codeNameId);
                else if (_codeName.Id != _codeNameId)
                    _codeName = Workarea.Cashe.GetCasheData<CodeName>().Item(_codeNameId);
                return _codeName;
            }
            set
            {
                if (_codeName == value) return;
                OnPropertyChanging(GlobalPropertyNames.CodeName);
                _codeName = value;
                _codeNameId = _codeName == null ? 0 : _codeName.Id;
                OnPropertyChanged(GlobalPropertyNames.CodeName);
            }
        }
        

        private int _elementId;
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public int ElementId
        {
            get { return _elementId; }
            set
            {
                if (value == _elementId) return;
                OnPropertyChanging(GlobalPropertyNames.ElementId);
                _elementId = value;
                OnPropertyChanged(GlobalPropertyNames.ElementId);
            }
        }
        private T _element;
        /// <summary>Источник</summary>
        public T Element
        {
            get
            {
                if (_elementId == 0)
                    return default(T);
                if (_element == null)
                    _element = Workarea.Cashe.GetCasheData<T>().Item(_elementId);
                else if (_element.Id != _elementId)
                    _element = Workarea.Cashe.GetCasheData<T>().Item(_elementId);
                return _element;
            }
            set
            {
                _element = value;
                _elementId = _element != null ? _element.Id : 0;
            }
        }
        

        private string _value;
        /// <summary>Признак</summary>
        public string Value
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

        private string _memo;
        /// <summary>Примечание</summary>
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


        private int _orderNo;
        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderNo
        {
            get { return _orderNo; }
            set
            {
                if (value == _orderNo) return;
                OnPropertyChanging(GlobalPropertyNames.OrderNo);
                _orderNo = value;
                OnPropertyChanged(GlobalPropertyNames.OrderNo);
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

            if (!string.IsNullOrEmpty(_value))
                writer.WriteAttributeString(GlobalPropertyNames.Value, _value);
            if (_elementId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ElementId, XmlConvert.ToString(_elementId));
            if (_codeNameId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CodeNameId, XmlConvert.ToString(_codeNameId));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (_orderNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_orderNo));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Value) != null)
                _value = reader.GetAttribute(GlobalPropertyNames.Value);
            if (reader.GetAttribute(GlobalPropertyNames.ElementId) != null)
                _elementId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ElementId));
            if (reader.GetAttribute(GlobalPropertyNames.CodeNameId) != null)
                _codeNameId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CodeNameId));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _orderNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OrderNo));
        }
        #endregion
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        public override void Validate()
        {
            if (!string.IsNullOrEmpty(_value) && _value.Length > 255)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_NAMELENTH", 1049));
            if (ElementId == 0)
                // TODO:
                throw new ValidateException("Не указан элемент");
            if (CodeNameId == 0)
                // TODO:
                throw new ValidateException("Не указан тип кода");
            if (DatabaseId == 0)
                DatabaseId = Workarea.MyBranche.Id;
        }

        #region Состояние
        CodeValueStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new CodeValueStruct
                                  {
                                      Value = _value,
                                      ElementId = _elementId,
                                      CodeNameId = _codeNameId,
                                      Memo = _memo,
                                      OrderNo = _orderNo
                                  };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            Value = _baseStruct.Value;
            Memo = _baseStruct.Memo;
            ElementId = _baseStruct.ElementId;
            CodeNameId = _baseStruct.CodeNameId;
            OrderNo = _baseStruct.OrderNo;
            IsChanged = false;
        }
        #endregion
        #region База данных

        /// <summary>Создание объекта в базе данных</summary>
        /// <remarks>Перед созданием объекта не выполняется проверка <see cref="BaseCoreObject.Validate"/>.
        /// Метод использует хранимую процедуру указанную в методах объекта по ключу "CodeInsertUpdate".
        /// </remarks>
        /// <seealso cref="BaseCoreObject.Load(int)"/>
        /// <seealso cref="BaseCoreObject.Validate"/>
        /// <seealso cref="BaseCoreObject.Update(bool)"/>
        protected override void Create()
        {
            CancelEventArgs e = new CancelEventArgs();
            OnCreating(e);
            if (e.Cancel)
                return;
            Create(Element.Entity.FindMethod("CodeInsertUpdate").FullName);
            OnCreated();
        }

        /// <summary>Обновление объекта в базе данных</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "CodeInsertUpdate"</remarks>
        /// <seealso cref="BaseCoreObject.Create()"/>
        /// <seealso cref="BaseCoreObject.Load(int)"/>
        /// <seealso cref="BaseCoreObject.Validate"/>
        protected override void Update(bool versionControl = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnUpdating(e);
            if (e.Cancel)
                return;
            Update(Element.Entity.FindMethod("CodeInsertUpdate").FullName, versionControl);
            OnUpdated();
        }
        /// <summary>Удаление объекта из базы данных</summary>
        public override void Delete(bool checkVersion = true)
        {
            Delete(Element.Entity.FindMethod("CodeDelete").FullName, checkVersion);
        }
        /// <summary>Загрузить данные объекта из базы данных по идентификатору</summary>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnLoading(e);
            if (e.Cancel)
                return;
            Load(value, Element.Entity.FindMethod("CodeLoad").FullName);
        }
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _codeNameId = reader.GetInt32(9);
                _elementId = reader.GetInt32(10);
                _value = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                _memo = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                _orderNo = reader.GetInt32(13);
            }
            catch (Exception ex)
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.CodeNameId, SqlDbType.Int) { IsNullable = false, Value = _codeNameId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ElementId, SqlDbType.Int) { IsNullable = false, Value = _elementId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Value, SqlDbType.NVarChar, 255) { IsNullable = true };
            prm.Value = _value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
                prm.Value = _memo;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.Int) { IsNullable = false, Value = _orderNo };
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        /// <summary>Представление объекта в виде строки</summary>
        /// <rereturns>Соответствует значению кода <see cref="Value"/></rereturns>
        public override string ToString()
        {
            return _value ?? string.Empty;
        }
    }
    /// <summary>
    /// Представление значений кодов
    /// </summary>
    public sealed class CodeValueView
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
        /// Идентификатор наименования кода
        /// </summary>
        public int CodeNameId { get; set; }
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public int ElementId { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderNo { get; set; }
        /// <summary>
        /// Наименование элемента
        /// </summary>
        public string ElementName { get; set; }
        /// <summary>
        /// Наименование кода
        /// </summary>
        public string CodeName { get; set; }
        /// <summary>
        /// Приложение
        /// </summary>
        public string App { get; set; }
        /// <summary>
        /// Группа
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="reader">Объект чтения данных</param>
        public void Load(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            CodeNameId = reader.GetInt32(1);
            ElementId = reader.GetInt32(2);
            Value = reader.GetString(3);
            Memo = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
            OrderNo = reader.GetInt32(5);            
            ElementName = reader.GetString(6);
            CodeName = reader.GetString(7);
            App = reader.GetString(8);
            GroupName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
        }
        public CodeValue<T> ConvertToCodeValue<T>(T item) where T : class, IBase, new()
        {
            return ConvertToCodeValue<T>(item, this);
        }

        public static CodeValue<T> ConvertToCodeValue<T>(T item, CodeValueView c) where T : class, IBase, new()
        {
            CodeValue<T> val = new CodeValue<T> {Workarea = c.Workarea, Element = item};
            val.Load(c.Id);
            return val;
        }
    }

    internal static class CodeHelper<T>where T : class, IBase, new()
    {
        public static List<CodeValue<T>> GetValues(T value, bool allKinds)
        {
            CodeValue<T> item;
            List<CodeValue<T>> collection = new List<CodeValue<T>>();
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = value.Workarea.Empty<T>().Entity.FindMethod("CodeGetValues").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        //cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = chainKindId;
                        //if (stateId != 1)
                        //    cmd.Parameters.Add(GlobalSqlParamNames.ChainStateId, SqlDbType.Int).Value = stateId;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new CodeValue<T>  { Workarea = value.Workarea };
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

        public static List<CodeValueView> GetView(T value, bool allKinds)
        {
            CodeValueView item;
            List<CodeValueView> collection = new List<CodeValueView>();
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = value.Workarea.Empty<T>().Entity.FindMethod("CodeGetView").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        //cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = chainKindId;
                        //if (stateId != 1)
                        //    cmd.Parameters.Add(GlobalSqlParamNames.ChainStateId, SqlDbType.Int).Value = stateId;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new CodeValueView { Workarea = value.Workarea };
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