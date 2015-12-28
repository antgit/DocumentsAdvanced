using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct ChainKindStruct
    {
        /// <summary>Признак</summary>
        public string Code;
        /// Флаговое значение подтипов для которых действительна связь
        public int EntityContent;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>Наименование</summary>
        public string Name;
        /// Обратное наименование
        public string NameRight;
        /// <summary>Идентификатор типа</summary>
        public int ToEntityId;
        /// <summary>Идентификатор типа</summary>
        public int FromEntityId;
    }
    /// <summary>
    /// Вид цепочки
    /// </summary>
    public class ChainKind : BaseCoreObject, IComparable, IComparable<ChainKind>

    {
        /// <summary>Конструктор</summary>
        public ChainKind():base()
        {
            EntityId = (short)WhellKnownDbEntity.ChainKind;
        }


// ReSharper disable InconsistentNaming
        /// <summary>Уволенные сотрудники</summary>
        public const string DISMISSED = "DISMISSED";
        /// <summary>Файлы</summary>
        public const string PROCESS = "PROCESS";
        /// <summary>Файлы</summary>
        public const string FILE = "FILE";
        /// <summary>Склады</summary>
        public const string STORE = "STORE";
        /// <summary>Сотрудники</summary>
        public const string WORKERS = "WORKERS";
        public const string TREEUI = "TREEUI";
        /// <summary>Дерево</summary>
        public const string TREE = "TREE";
        /// <summary>Дети</summary>
        public const string CHILDREN = "CHILDREN";
        /// <summary>Аналоги</summary>
        public const string ANALOGS = "ANALOGS";
        /// <summary>Файлы договоров</summary>
        public const string FILE_CONTRACTS = "FILE_CONTRACTS";
        /// <summary>Печатные формы</summary>
        public const string PRINTFORM = "PRINTFORM";
        /// <summary>Отчеты</summary>
        public const string REPORT = "REPORT";
        /// <summary>Печатные отчеты</summary>
        public const string PRINTREPORT = "PRINTREPORT";
        /// <summary>Код типа связи "Торговые представители"</summary>
        public const string TRADERS = "TRADERS";
        /// <summary>Код типа связи "Перевозчик"</summary>
        public const string DELIVERY = "DELIVERY";

        /// <summary>Код типа связи "Примечания"</summary>
        public const string NOTES = "NOTES";
        /// <summary>Код типа связи "База знаний"</summary>
        public const string KNOWLEDGES = "KNOWLEDGES";
        /// <summary>Код типа связи "Сообщения"</summary>
        public const string MESSAGE = "MESSAGE";
        /// <summary>Код типа связи "Документы"</summary>
        public const string DOCS = "DOCS";
        /// <summary>Код типа связи "Событие"</summary>
        public const string EVENT = "EVENT";
        /// <summary>Код типа связи "Города"</summary>
        public const string TOWNS = "TOWNS";
        
        
        
        
        public const int PAGES_ID = 23;
        public const string PAGESDEFAULT = "PAGESDEFAULT";
// ReSharper restore InconsistentNaming
        public int CompareTo(object obj)
        {
            ChainKind otherPerson = (ChainKind)obj;
            return Id.CompareTo(otherPerson.Id);
        }

        public int CompareTo(ChainKind other)
        {
            return Id.CompareTo(other.Id);
        }

        #region Свойства
        private Int32 _fromEntityId;
        /// <summary>Идентификатор типа</summary>
        public Int32 FromEntityId
        {
            get { return _fromEntityId; }
            set
            {
                if (value == _fromEntityId) return;
                OnPropertyChanging(GlobalPropertyNames.FromEntityId);
                _fromEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.FromEntityId);
            }
        }
        private EntityType _fromEntityType;
        /// <summary>Тип элемента</summary>
        public EntityType FromEntity
        {
            get
            {
                if (_fromEntityId == 0)
                    return null;
                if (_fromEntityType == null)
                    _fromEntityType = Workarea.CollectionEntities.Find(f => f.Id == _fromEntityId);
                else if (_fromEntityType.Id != _fromEntityId)
                    _fromEntityType = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == _fromEntityId);
                return _fromEntityType;
            }
        }

        private Int32 _toEntityId;
        /// <summary>Идентификатор типа</summary>
        public Int32 ToEntityId
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
        private EntityType _toEntityType;
        /// <summary>Тип элемента</summary>
        public EntityType ToEntity
        {
            get
            {
                if (_toEntityId == 0)
                    return null;
                if (_toEntityType == null)
                    _toEntityType = Workarea.CollectionEntities.Find(f => f.Id == _toEntityId);
                else if (_toEntityType.Id != _toEntityId)
                    _toEntityType = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == _toEntityId);
                return _toEntityType;
            }
        }
        private string _code;
        /// <summary>Признак</summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (value == _code) return;
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
                if (value == _name) return;
                OnPropertyChanging(GlobalPropertyNames.Name);
                _name = value;
                OnPropertyChanged(GlobalPropertyNames.Name);
            }
        }

        private string _nameRight;
        /// <summary>
        /// Обратное наименование
        /// </summary>
        public string NameRight
        {
            get { return _nameRight; }
            set
            {
                if (value == _nameRight) return;
                OnPropertyChanging(GlobalPropertyNames.NameRight);
                _nameRight = value;
                OnPropertyChanged(GlobalPropertyNames.NameRight);
            }
        }

        private int _entityContent;
        /// <summary>
        /// Флаговое значение подтипов для которых действительна связь
        /// </summary>
        [Obsolete]
        public int EntityContent
        {
            get { return _entityContent; }
            set
            {
                if (value == _entityContent) return;
                OnPropertyChanging(GlobalPropertyNames.EntityContent);
                _entityContent = value;
                OnPropertyChanged(GlobalPropertyNames.EntityContent);
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
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, _code);
            if (_entityContent != 0)
                writer.WriteAttributeString(GlobalPropertyNames.EntityContent, XmlConvert.ToString(_entityContent));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
            if (!string.IsNullOrEmpty(_nameRight))
                writer.WriteAttributeString(GlobalPropertyNames.NameRight, _nameRight);
            if (_toEntityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ToEntityId, XmlConvert.ToString(_toEntityId));
            if (_fromEntityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FromEntityId, XmlConvert.ToString(_fromEntityId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                _code = reader.GetAttribute(GlobalPropertyNames.Code);
            if (reader.GetAttribute(GlobalPropertyNames.EntityContent) != null)
                _entityContent = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.EntityContent));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
            if (reader.GetAttribute(GlobalPropertyNames.NameRight) != null)
                _nameRight = reader.GetAttribute(GlobalPropertyNames.NameRight);
            if (reader.GetAttribute(GlobalPropertyNames.ToEntityId) != null)
                _toEntityId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ToEntityId));
            if (reader.GetAttribute(GlobalPropertyNames.FromEntityId) != null)
                _fromEntityId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FromEntityId));
        }
        #endregion

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        public override void Validate()
        {
            if (string.IsNullOrEmpty(_name))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_NAMEISEMPTY, 1049));
            if (!string.IsNullOrEmpty(_code) && _code.Length > 50)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_NAMELENTH", 1049));
            if (ToEntityId == 0)
                throw new ValidateException("Не указан первый тип");
            if (FromEntityId == 0)
                throw new ValidateException("Не указан второй тип");
            if (DatabaseId == 0)
                DatabaseId = Workarea.MyBranche.Id;
        }
        ///// <summary>Сохранить</summary>
        //public void Save()
        //{
        //    Validate();
        //    if (IsNew)
        //        Create(Workarea.FindMethod("Core.ChainKindInsert").FullName);
        //    else
        //        Update(Workarea.FindMethod("Core.ChainKindUpdate").FullName, true);
        //}
        ///// <summary>Удалить</summary>
        //public void Delete()
        //{
        //    Workarea.DeleteById(Id, Workarea.FindMethod("Core.ChainKindDelete").FullName);
        //}
        #region Состояние
        ChainKindStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new ChainKindStruct
                {
                    Code = _code,
                    EntityContent = _entityContent,
                    Memo = _memo,
                    Name = _name,
                    NameRight = _nameRight,
                    ToEntityId = _toEntityId,
                    FromEntityId=_fromEntityId
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
            Code = _baseStruct.Code;
            Memo = _baseStruct.Memo;
            Name = _baseStruct.Name;
            NameRight = _baseStruct.NameRight;
            FromEntityId = _baseStruct.FromEntityId;
            ToEntityId = _baseStruct.ToEntityId;
            IsChanged = false;
        }
        #endregion
        #region База данных
        ///// <summary>Загрузить объект из базы данных по идентификатору</summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.FindMethod("Сore.ChainKindLoad").FullName);
        //}
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _fromEntityId = reader.GetInt16(9);
                _toEntityId = reader.GetInt16(10);
                _code = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                _memo = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                _name = reader.GetString(13);
                _nameRight = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.FromEntityId, SqlDbType.SmallInt) { IsNullable = true, Value = _fromEntityId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ToEntityId, SqlDbType.SmallInt) {IsNullable = true, Value = _toEntityId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 255) {IsNullable = true};
            if (string.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255) {IsNullable = true};
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
                prm.Value = _memo;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255)
                      {
                          IsNullable = false,
                          Value = _name
                      };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.NameRight, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_nameRight))
                prm.Value = DBNull.Value;
            else
                prm.Value = _nameRight;
            sqlCmd.Parameters.Add(prm);

            sqlCmd.Parameters[GlobalSqlParamNames.Id].Value = Id;
        }
        #endregion

        /// <summary>Представление объекта в виде строки</summary>
        /// <rereturns>Соответствует наименованию объекта <see cref="Name"/></rereturns>
        public override string ToString()
        {
            return _name ?? string.Empty;
        }

        private List<ChainKindContentType> _contentTypes;
        /// <summary>
        /// Коллекция допустимых типов для связывания.
        /// </summary>
        /// <returns></returns>
        public List<ChainKindContentType> GetContentType()
        {
            if (_contentTypes == null)
                _contentTypes = ChainKindContentType.GetCollection(Workarea, Id);
            return _contentTypes;
        }
        /// <summary>
        /// Список допустимого назначения
        /// </summary>
        /// <returns></returns>
        public List<int> GetContentTypeKindId()
        {
            List<int> val = new List<int>();
            foreach (ChainKindContentType contentType in GetContentType())
            {
                val.Add(contentType.EntityKindId);
            }
            return val;
        }
        /// <summary>
        /// Список допустимых источников
        /// </summary>
        /// <returns></returns>
        public List<int> GetContentTypeKindIdFrom()
        {
            List<int> val = new List<int>();
            foreach (ChainKindContentType contentType in GetContentType())
            {
                val.Add(contentType.EntityKindIdFrom);
            }
            return val;
        }
    }
}