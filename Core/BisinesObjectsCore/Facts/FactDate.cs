using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;

namespace BusinessObjects
{
    internal struct FactDateStruct
    {
        //public DateTime ActualDate;
        /// <summary>Идентификатор колонки</summary>
        public int ColumnId;
        /// <summary>Идентификатор сущности</summary>
        public int ElementId;
        /// <summary>Имеются ли значения на дату</summary>
        public bool HasValue;
        /// <summary>Сортировка</summary>
        public int SortOrder;
        /// <summary>Тип сущности</summary>
        public int ToEntityId;
    }
    /// <summary>Дата факта</summary>
    public sealed class FactDate : BaseCoreObject
    {
        /// <summary>Конструктор</summary>
        public FactDate(): base()
        {
            EntityId = (short)WhellKnownDbEntity.FactDate;
        }
        #region Свойства

        private int _columnId;
        /// <summary>Идентификатор колонки</summary>
        public int ColumnId
        {
            get { return _columnId; }
            set 
            {
                if (value == _columnId) return;
                OnPropertyChanging(GlobalPropertyNames.ColumnId);
                _columnId = value;
                OnPropertyChanged(GlobalPropertyNames.ColumnId);
            }
        }

        private FactColumn _factColumn;
        /// <summary>Колонка факта</summary>
        public FactColumn FactColumn
        {
            get
            {
                if (_columnId == 0)
                    return null;
                if (_factColumn == null)
                    _factColumn = Workarea.Cashe.GetCasheData<FactColumn>().Item(_columnId);
                else if (_factColumn.Id != _columnId)
                    _factColumn = Workarea.Cashe.GetCasheData<FactColumn>().Item(_columnId);
                return _factColumn;
            }
            set
            {
                if (_factColumn == value) return;
                OnPropertyChanging(GlobalPropertyNames.FactColumn);
                _factColumn = value;
                _columnId = _factColumn == null ? 0 : _factColumn.Id;
                OnPropertyChanged(GlobalPropertyNames.FactColumn);
            }
        } 

        private int _elementId;
        /// <summary>Идентификатор сущности</summary>
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

        private int _toEntityId;
        /// <summary>Тип сущности</summary>
        public int ToEntityId
        {
            get { return _toEntityId; }
            set 
            {
                if (value == _toEntityId) return;
                OnPropertyChanging(GlobalPropertyNames.ToEntityId);
                _toEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.ToEntityId);
            }
        }

        private DateTime _actualDate;
        /// <summary>Дата</summary>
        public DateTime ActualDate
        {
            get { return _actualDate; }
            set 
            {
                if (value == _actualDate) return;
                OnPropertyChanging(GlobalPropertyNames.ActualDate);
                _actualDate = value;
                OnPropertyChanged(GlobalPropertyNames.ActualDate);
            }
        }

        private int _sortOrder;
        /// <summary>Сортировка</summary>
        public int SortOrder
        {
            get { return _sortOrder; }
            set 
            {
                if (value == _sortOrder) return;
                OnPropertyChanging(GlobalPropertyNames.SortOrder);
                _sortOrder = value;
                OnPropertyChanged(GlobalPropertyNames.SortOrder);
            }
        }

        private bool _hasValue;
        /// <summary>Имеются ли значения на дату</summary>
        public bool HasValue
        {
            get { return _hasValue; }
        }
	
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_columnId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ColumnId, XmlConvert.ToString(_columnId));
            if (_elementId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ElementId, XmlConvert.ToString(_elementId));
            if (_hasValue)
                writer.WriteAttributeString(GlobalPropertyNames.HasValue, XmlConvert.ToString(_hasValue));
            if (_sortOrder != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SortOrder, XmlConvert.ToString(_sortOrder));
            if (_toEntityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ToEntityId, XmlConvert.ToString(_toEntityId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ColumnId) != null)
                _columnId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ColumnId));
            if (reader.GetAttribute(GlobalPropertyNames.ElementId) != null)
                _elementId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ElementId));
            if (reader.GetAttribute(GlobalPropertyNames.HasValue) != null)
                _hasValue = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.HasValue));
            if (reader.GetAttribute(GlobalPropertyNames.SortOrder) != null)
                _sortOrder = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SortOrder));
            if (reader.GetAttribute(GlobalPropertyNames.ToEntityId) != null)
                _toEntityId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ToEntityId));
        }
        #endregion

        #region Состояние
        FactDateStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new FactDateStruct
                {
                    //ActualDate = _actualDate,
                    ColumnId = _columnId,
                    ElementId = _elementId,
                    HasValue = _hasValue,
                    SortOrder = _sortOrder,
                    ToEntityId = ToEntityId
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            //ActualDate = _baseStruct.ActualDate;
            ColumnId = _baseStruct.ColumnId;
            ElementId = _baseStruct.ElementId;
            _hasValue = _baseStruct.HasValue;
            SortOrder = _baseStruct.SortOrder;
            ToEntityId = _baseStruct.ToEntityId;

            IsChanged = false;
        }
        #endregion
        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <remarks>
        /// Длясистемных объектов существует разрешенный диапазон дат, который задается в
        /// параметрах системных объектов и запрещает создание фактов с датой ниже чем
        /// указана в параметрах.
        /// </remarks>
        public override void Validate()
        {
            base.Validate();
            DateTime val = Workarea.Access.GetMinFactDateAllow(ToEntityId);
            if(val>ActualDate)
            {
                throw new ValidateException(string.Format("Создание фактов с датой актуальности меньше чем {0} запрещено", val));
            }
            
        }

        #region База данных
        ///// <summary>Сохранить объект в базе данных</summary>
        ///// <remarks>В зависимости от состояния объекта <see cref="BusinessObjects.FactDate.IsNew"/> 
        ///// выполняется создание или обновление объекта</remarks>
        //public void Save()
        //{
        //    if (IsNew)
        //        Create(Workarea.Empty<FactName>().Entity.FindMethod("FactDateInsert").FullName);
        //    else
        //        Update(Workarea.Empty<FactName>().Entity.FindMethod("FactDateUpdate").FullName, true);
        //}
        ///// <summary>Удалить объект уз базы данных</summary>
        //public void Delete()
        //{
        //    Workarea.DeleteById(Id, Workarea.Empty<FactName>().Entity.FindMethod("FactDateDelete").FullName);
        //}
        // TODO: возможна реализация в базовом классе
        ///// <summary>Перемещение в корзину</summary>
        //public void Remove()
        //{
        //    
        //    Workarea.ChangeStateId(Id, State.STATEDELETED, "Fact.FactDateChangeState");
        //    StateId = State.STATEDELETED;
        //}

        /// <summary>Коллекция значений</summary>
        /// <param name="columnId">Идентификатор колонки</param>
        /// <param name="elementId">Идентификатор объекта</param>
        /// <param name="entityId">Идентификатор типа объекта</param>
        /// <returns></returns>
        public List<FactValue> GetFactValues(int columnId, int elementId, int entityId)
        {
            FactValue item;
            List<FactValue> collection = null;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    collection = new List<FactValue>();
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.Add(GlobalSqlParamNames.DateId, SqlDbType.Int).Value = Id;
                        //cmd.Parameters.Add(GlobalSqlParamNames.ColumnId, SqlDbType.Int).Value = columnId;
                        //cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = elementId;
                        //cmd.Parameters.Add(GlobalSqlParamNames.ToEntityId, SqlDbType.Int).Value = entityId;
                        cmd.CommandText = Workarea.Empty<FactName>().Entity.FindMethod("GetFactValues").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new FactValue {Workarea = Workarea, FactDate = this};
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

        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _columnId = reader.GetInt32(9);
                _elementId = reader.GetInt32(10);
                _toEntityId = reader.GetInt16(11);
                _actualDate = reader.GetDateTime(12);
                _sortOrder = reader.GetInt32(13);
                _hasValue = reader.GetBoolean(14);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        ///// <summary>
        ///// Загрузить объект из базы данных по идентификатору
        ///// </summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.Empty<FactName>().Entity.FindMethod("FactDateLoad").FullName);
        //}
        /// <summary>
        /// Установить значения параметров для комманды создания
        /// </summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ColumnId, SqlDbType.Int) {IsNullable = false, Value = _columnId};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.ElementId, SqlDbType.Int) {IsNullable = false, Value = _elementId};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.ToEntityId, SqlDbType.SmallInt) { IsNullable = false, Value = _toEntityId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.ActualDate, SqlDbType.DateTime) {IsNullable = false, Value = _actualDate};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.Int) {IsNullable = false, Value = _sortOrder};
            sqlCmd.Parameters.Add(prm);
        }
        #endregion
    }
}