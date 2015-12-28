using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Типы документов
    /// </summary>
    public sealed class EntityDocument : BaseCoreObject, IBase, IEquatable<EntityDocument>, IFacts<EntityDocument>, IEntityType
    {
        bool IEquatable<EntityDocument>.Equals(EntityDocument other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        #region События сохранения
        [NonSerialized]
        private CancelEventHandler _savingHandlers;
        /// <summary>
        /// Событие начала сохранения
        /// </summary>
        public event CancelEventHandler Saving
        {
            add
            {
                _savingHandlers = (CancelEventHandler)
                      Delegate.Combine(_savingHandlers, value);
            }
            remove
            {
                _savingHandlers = (CancelEventHandler)
                      Delegate.Remove(_savingHandlers, value);
            }
        }

        [NonSerialized]
        private EventHandler _savedHandlers;
        /// <summary>
        /// Событие после сохранения
        /// </summary>
        public event EventHandler Saved
        {
            add
            {
                _savedHandlers = (EventHandler)
                      Delegate.Combine(_savedHandlers, value);
            }
            remove
            {
                _savedHandlers = (EventHandler)
                      Delegate.Remove(_savedHandlers, value);
            }
        }
        /// <summary>
        /// Метод вызываемый непосредственно после сохранения
        /// </summary>
        void OnSaved()
        {
            OnSaved(new EventArgs());
        }
        /// <summary>
        /// Метод вызываемый непосредственно после сохранения
        /// </summary>
        /// <param name="e"></param>
        void OnSaved(EventArgs e)
        {
            if (_savedHandlers != null)
                _savedHandlers.Invoke(this, e);
        }
        /// <summary>
        /// Метод вызываемый непосредственно перед сохранения
        /// </summary>
        /// <param name="e"></param>
        void OnSaving(CancelEventArgs e)
        {
            if (_savingHandlers != null)
                _savingHandlers.Invoke(this, e);
        }
        #endregion
        /// <summary>Конструктор</summary>
        public EntityDocument(): base()
        {
        }

        #region Свойства

        ////private const short _entityId = 14;
        //short ICoreObject.EntityId
        //{
        //    get { return _entityId; }
        //}
        short IBase.KindValue
        {
            get { return (Int16)Id; }
            set { }
        }

        //private EntityType _entity;
        //EntityType ICoreObject.Entity
        //{
        //    get
        //    {
        //        if (_entity == null)
        //            _entity = (this as ICoreObject).Workarea.CollectionDbEntities.Find(f => f.Id == _entityId);
        //        else if (_entity.Id != _entityId)
        //            _entity = (this as ICoreObject).Workarea.CollectionDbEntities.Find(f => f.Id == _entityId);
        //        return _entity;
        //    }
        //}
        EntityType ICoreObject.Entity
        {
            get
            {
                return Workarea.CollectionEntities.Find(s => s.Id == 14);
                //return this;
            }
        }
        protected override EntityType OnRequestEntity()
        {
            return Workarea.CollectionEntities.Find(s => s.Id == 14);
        }
        int IBase.KindId
        {
            get { return 0; }
            set { }
        }
        int IBase.TemplateId
        {
            get { return 0; }
            set { }
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
        private string _nameFull;
        /// <summary>
        /// Полное наименование
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string NameFull
        {
            get { return _nameFull; }
            set
            {
                if (value == _nameFull) return;
                OnPropertyChanging(GlobalPropertyNames.NameFull);
                _nameFull = value;
                OnPropertyChanged(GlobalPropertyNames.NameFull);
            }
        }
        private string _code;
        /// <summary>Код</summary>
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
        private string _codeFind;
        /// <summary>
        /// Код поиска
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string CodeFind
        {
            get { return _codeFind; }
            set
            {
                if (value == _codeFind) return;
                OnPropertyChanging(GlobalPropertyNames.CodeFind);
                _codeFind = value;
                OnPropertyChanged(GlobalPropertyNames.CodeFind);
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

        private string _nameSchema;
        /// <summary>
        /// Схема данных
        /// </summary>
        public string NameSchema
        {
            get { return _nameSchema; }
            set
            {
                if (value == _nameSchema) return;
                OnPropertyChanging(GlobalPropertyNames.NameSchema);
                _nameSchema = value;
                OnPropertyChanged(GlobalPropertyNames.NameSchema);
            }
        }


        private string _codeClass;
        /// <summary>
        /// Код класса
        /// </summary>
        public string CodeClass
        {
            get { return _codeClass; }
            set
            {
                if (value == _codeClass) return;
                OnPropertyChanging(GlobalPropertyNames.CodeClass);
                _codeClass = value;
                OnPropertyChanged(GlobalPropertyNames.CodeClass);
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

            if (!string.IsNullOrEmpty(_code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, _code);
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
            if (!string.IsNullOrEmpty(_nameFull))
                writer.WriteAttributeString(GlobalPropertyNames.NameFull, _nameFull);
            if (!string.IsNullOrEmpty(_codeFind))
                writer.WriteAttributeString(GlobalPropertyNames.CodeFind, _codeFind);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);


            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                _code = reader.GetAttribute(GlobalPropertyNames.Code);
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
            if (reader.GetAttribute(GlobalPropertyNames.NameFull) != null)
                _nameFull = reader.GetAttribute(GlobalPropertyNames.NameFull);
            if (reader.GetAttribute(GlobalPropertyNames.CodeFind) != null)
                _codeFind = reader.GetAttribute(GlobalPropertyNames.CodeFind);
        }
        #endregion

        #region IBase Members



        string IBase.Name
        {
            get { return Name; }
            set { Name = value; }
        }

        string IBase.Code
        {
            get { return Code; }
            set { Code = value; }
        }

        short ICoreObject.EntityId
        {
            get { return 14; }
        }

        #endregion
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(_name))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_NAMEISEMPTY, 1049));
            if (Id == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_IDREQUREMENT, 1049));
        }
        ///// <summary>Сохранить</summary>
        //public void Save()
        //{
        //    CancelEventArgs e = new CancelEventArgs();
        //    OnSaving(e);
        //    if (e.Cancel)
        //        return;
        //    Validate();
        //    if (IsNew)
        //        Create(Workarea.FindMethod("Core.DocumentTypeInsert").FullName);
        //    else
        //        Update(Workarea.FindMethod("Core.DocumentTypeUpdate").FullName, true);
                
        //    OnSaved();
        //}
        ///// <summary>
        ///// Удалить
        ///// </summary>
        //public void Delete()
        //{
        //    Workarea.DeleteById(Id, "Core.DocumentTypeDelete");
        //}

        #region База данных
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _name = reader.GetString(9);
                _code = reader.IsDBNull(10) ? string.Empty : reader.GetString(9);
                _memo = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                _nameSchema = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                _codeClass = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255)
                      {
                          IsNullable = false,
                          Value = _name
                      };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 255) {IsNullable = true};
            if (string.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _memo.Length;
                prm.Value = _memo;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.NameSchema, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_nameSchema))
                prm.Value = DBNull.Value;
            else
                prm.Value = _nameSchema;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CodeClass, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_codeClass))
                prm.Value = DBNull.Value;
            else
                prm.Value = _codeClass;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        private List<ProcedureMap> _methods;
        /// <summary>Коллекция методов</summary>
        public List<ProcedureMap> Methods
        {
            get
            {
                if (_methods == null)
                    RefreshMethods();
                return _methods;
            }
        }
        /// <summary>Коллекция методов</summary>
        /// <remarks>Только для внутреннего использования, используйте свойство <see cref="EntityDocument.Methods"/></remarks>
        /// <returns></returns>
        public List<ProcedureMap> RefreshMethods()
        {
            _methods = new List<ProcedureMap>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return _methods;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        cmd.CommandText = Workarea.FindMethod("Core.DocumentTypeMethodsByType").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ProcedureMap item = new ProcedureMap {Workarea = Workarea};
                            item.Load(reader);
                            _methods.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return _methods;
        }

        /// <summary>Поиск метода</summary>
        /// <param name="methodName">Псевдоним метода</param>
        /// <returns></returns>
        public ProcedureMap FindMethod(string methodName)
        {
            try
            {
                return Methods.FirstOrDefault(m => m.Method == methodName);
            }
            catch (Exception ex)
            {
                throw new MethodFindException(string.Format(Workarea.Cashe.ResourceString("EX_MSG_METHOTNOTFOUND", 1049), methodName), ex);
            }
        }
        /// <summary>Поиск метода</summary>
        /// <param name="methodName">Псевдоним</param>
        /// <param name="subKindId">Идентификатор вида</param>
        /// <returns></returns>
        public ProcedureMap FindMethod(string methodName, short subKindId)
        {
            return Methods.FirstOrDefault(m => (m.Method == methodName && m.SubKindId == subKindId));
        }

        private List<EntityDocumentKind> _kinds;
        /// <summary>Коллекция видов</summary>
        public List<EntityDocumentKind> Kinds
        {
            get 
            {
                if (_kinds == null)
                {
                    _kinds = new List<EntityDocumentKind>();
                    using (SqlConnection cnn = Workarea.GetDatabaseConnection())
                    {
                        if (cnn == null) return _kinds;
                        try
                        {
                            using (SqlCommand cmd = cnn.CreateCommand())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.SmallInt).Value = Id;
                                cmd.CommandText = Workarea.FindMethod("Core.DocumentKindsByType").FullName;
                                SqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    EntityDocumentKind item = new EntityDocumentKind { Workarea = Workarea };
                                    item.Load(reader);
                                    _kinds.Add(item);
                                }
                                reader.Close();
                            }
                        }
                        finally
                        {
                            cnn.Close();
                        }
                    }
                }
                return _kinds; 
            }
        }



        #region ILoadById Members
        ///// <summary>Згарузить объект из базы данных по идентификатору</summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.FindMethod("Core.DocumentTypeLoad").FullName);
        //}
        #endregion

        #region IFacts<EntityType> Members

        private List<FactView> _factView;
        public List<FactView> GetCollectionFactView()
        {
            return _factView ?? (_factView = FactHelper.GetCollectionFactView(Workarea, Id, 14));
        }

        public void RefreshFaсtView()
        {
            _factView = FactHelper.GetCollectionFactView(Workarea, Id, 14);
        }

        public FactView GetFactViewValue(string factCode, string columnCode)
        {
            return GetCollectionFactView().FirstOrDefault(s => s.FactNameCode == factCode && s.ColumnCode == columnCode);
        }
        public List<FactName> GetFactNames()
        {
            return FactHelper.GetFactNames(Workarea, 14);
        }
        #endregion
    }
}
