using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessObjects.Developer;
using BusinessObjects.Documents;
using BusinessObjects.Security;
namespace BusinessObjects
{
    /// <summary>Рабочая область</summary>
    public partial class Workarea: IWorkarea
    {
        /// <summary>
        /// Констпуктор
        /// </summary>
        public Workarea()
        {
            _period = new Period();
            //_database = new Database(this);
            _access = new Secure(this);
            _cashe = new WorkareaCashe(this);
            SessionId = new Guid();
        }
        public void OnCreatedObject<T>(T value) where T : class, ICoreObject, new()
        {

                string keyCode = string.Format("Workarea.GetCollection_{0}_{1}", value.Entity.Name, 0);
                if (Cashe.GetListCodeCasheData<T>().Exists(keyCode))
                {
                    List<T> coll = Cashe.GetListCodeCasheData<T>().Get(keyCode);
                    if (!coll.Exists(f => f.Id == value.Id))
                    {
                        coll.Add(value);
                    }
                }

                if (value.IsTemplate)
                {
                    keyCode = string.Format("Workarea.GetTemplates_{0}", value.Entity.Name);
                    if (Cashe.GetListCodeCasheData<T>().Exists(keyCode))
                    {
                        List<T> coll = Cashe.GetListCodeCasheData<T>().Get(keyCode);
                        if (!coll.Exists(f => f.Id == value.Id))
                        {
                            coll.Add(value);
                        }
                    }
                }
            //    if (value is Analitic)
        //    {
        //        Analitic obj = value as Analitic;
        //        string keyCode = string.Format("Workarea.GetCollection_{0}_{1}", obj.Entity.Name, 0);
        //        if (Cashe.GetListCodeCasheData<Analitic>().Exists(keyCode))
        //        {
        //            List<Analitic> coll = Cashe.GetListCodeCasheData<Analitic>().Get(keyCode);
        //            if (!coll.Exists(f => f.Id == obj.Id))
        //            {
        //                coll.Add(obj);
        //            }
        //        }
        //    }
        //    if (value is Task)
        //    {
        //        Task obj = value as Task;
        //        string keyCode = string.Format("Workarea.GetCollection_{0}_{1}", obj.Entity.Name, 0);
        //        if (Cashe.GetListCodeCasheData<Analitic>().Exists(keyCode))
        //        {
        //            List<Task> coll = Cashe.GetListCodeCasheData<Task>().Get(keyCode);
        //            if (!coll.Exists(f => f.Id == obj.Id))
        //            {
        //                coll.Add(obj);
        //            }
        //        }
        //    } 
        }
        public void OnCreatedCoreObject(object value)
        {
           
        }
        public bool IsWebApplication { get; set; }
        /// <summary>
        /// Глобальное обновление
        /// </summary>
        public void Refresh()
        {
            _access.RefreshCompanyScopeEdit();
            _access.RefreshCompanyScopeView();
            _access.GetAllUsers(true);

            _cashe._cashedCollections = new Dictionary<Type, object>();
        }
        /// <summary>Код сессии</summary>
        public Guid SessionId { get; set; }
        private readonly WorkareaCashe _cashe;
        /// <summary>
        /// Управляемый кеш объектов
        /// </summary>
        public WorkareaCashe Cashe
        {
            get { return _cashe; }
        }
	

        internal readonly Secure _access;
        /// <summary>
        /// Управление правами и разрешениями
        /// </summary>
        public ISecurirty Access
        {
            get { return _access; }
        }

        private Period _period;
        /// <summary>Рабочий период</summary>
        public Period Period
        {
            get { return _period; }
            set { _period = value; }
        }
        internal Branche _myBranche;
        /// <summary>Владелец текущей базы данных</summary>
        public Branche MyBranche
        {
            get { return _myBranche; }
        }
        internal int _langId;
        /// <summary>Текущий язык</summary>
        public int LanguageId
        {
            get { return _langId; }
        }

        #region Кеширование

        private List<EntityKind> _collectionEntityKinds;
        /// <summary>
        /// Коллекция всех видов системных объектов
        /// </summary>
        /// <remarks>Данная коллекция является кешированной после открытия базы данных, 
        /// <see cref="BusinessObjects.WorkareaBase.GetCollectionDbEntityKind(int)"/></remarks>
        public List<EntityKind> CollectionEntityKinds
        {
            get { return _collectionEntityKinds; }
        }

        private List<EntityType> _collectionEntities;
        /// <summary>
        /// Коллекция всех системных объектов
        /// </summary>
        /// <remarks>Данная коллекция является кешированной после открытия базы данных, 
        /// <see cref="RefreshCollectionEntities"/></remarks>
        public List<EntityType> CollectionEntities
        {
            get { return _collectionEntities; }
        }

        private List<State> _collectionStates;
        /// <summary>
        /// Коллекция состояний
        /// </summary>
        /// <remarks>Данная коллекция является кешированной после открытия базы данных
        /// </remarks>
        public List<State> CollectionStates
        {
            get { return _collectionStates; }
        }

        #endregion
        private void MakeCasheData()
        {
            GetGlobalMethods();
            RefreshCollectionStates();
            RefreshCollectionEntities();
            Empty<SystemParameter>().FindBy(code: "DEFAULT_LISTVIEWDOCUMENT");
            Empty<SystemParameter>().FindBy(code: "UISTYLE");
            _collectionEntityKinds = GetCollectionEntityKind(0);

            List<SystemParameter> coll = Empty<SystemParameter>().FindBy(code: "DEFAULTPARTALRELOADTIME");
            if(coll.Count==1)
            {
                if (coll[0].ValueInt.HasValue && coll[0].ValueInt.Value > 5)
                    this.Cashe.DefaultPartalReloadTime = coll[0].ValueInt.Value;
            }
        }
        /// <summary>
        /// Действия выполняемые после открытия базы данных
        /// </summary>
        /// <remarks></remarks>
        protected void OnAfterLogOn()
        {
            MakeCasheData();
        }
        /// <summary>
        /// Коллекция всех системных объектов
        /// </summary>
        /// <returns></returns>
        private void RefreshCollectionEntities()
        {
            _collectionEntities = new List<EntityType>();
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindMethod("Core.EntitiesLoadAll").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            EntityType item = new EntityType {Workarea = this};
                            item.Load(reader);
                            _collectionEntities.Add(item);
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
        /// <summary>
        /// Коллекция всех состояний
        /// </summary>
        /// <returns></returns>
        private void RefreshCollectionStates()
        {
            _collectionStates = new List<State>();
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindMethod("Core.StatesLoadAll").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            State item = new State {Workarea = this};
                            item.Load(reader);
                            _collectionStates.Add(item);
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
        /// <summary>
        /// Коллекция видов системных объектов
        /// </summary>
        /// <param name="value">Идентификатор системного типа</param>
        /// <returns></returns>
        public virtual List<EntityKind> GetCollectionEntityKind(int value)
        {
            List<EntityKind> collection = new List<EntityKind>();
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindMethod("Core.EntityKindsLoadAll").FullName;
                        if (value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = value;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            EntityKind item = new EntityKind {Workarea = this};
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


        //private List<EntityDocument> _documentTypes;
        /// <summary>
        /// Коллекция типов документов
        /// </summary>
        /// <returns></returns>
        public List<EntityDocument> CollectionDocumentTypes()
        {
            EntityDocument item;
            List<EntityDocument> coll = new List<EntityDocument>();
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return coll;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindMethod("Core.DocumentTypesLoadAll").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new EntityDocument {Workarea = this};
                            item.Load(reader);
                            coll.Add(item);
                        }
                        reader.Close();

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return coll;
        }

        private List<EntityDocumentKind> _collectionDocumentKinds;
        public List<EntityDocumentKind> CollectionDocumentKinds
        {
            get
            {
                if (_collectionDocumentKinds == null)
                    RefreshCollectionDocumentKinds();
                return _collectionDocumentKinds;
            }
        }

        public void RefreshCollectionDocumentKinds()
        {
            EntityDocumentKind item;
            _collectionDocumentKinds = new List<EntityDocumentKind>();
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Core].[DocumentKindLoadAll]"; //FindMethod("[Core].[DocumentKindLoadAll]").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new EntityDocumentKind { Workarea = this };
                            item.Load(reader);
                            _collectionDocumentKinds.Add(item);
                        }
                        reader.Close();

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return;
        }
        /// <summary>
        /// Информация об ошибке
        /// </summary>
        /// <param name="value">Идентификатор записи в протоколе ошибок</param>
        /// <returns></returns>
        public virtual ErrorLog GetErrorLog(int value)
        {
            ErrorLog item = null;
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return item;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value;
                        cmd.CommandText = FindMethod("Core.ErrorLogLoad").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new ErrorLog();
                            Action<SqlDataReader> loader = item.Load;
                            loader(reader);
                        }
                        reader.Close();
                        
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return item;
        }
        /// <summary>
        /// Получение объекта из базы данных по идентификатору
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public T GetObject<T>(int id) where T : class, ICoreObject, new()
        {
            T item = new T { Workarea = this };
            item.Load(id);
            return item;
        }
        /// <summary>
        /// Получение объекта из базы данных по идентификатору
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="guid">Идентификатор</param>
        /// <returns></returns>
        public T GetObject<T>(Guid guid) where T : class, ICoreObject, new()
        {
            T item = new T { Workarea = this };
            item.Load(guid);
            return item;
        }
        /// <summary>
        /// Получение объекта из базы данных по идентификатору
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public T GetObjectProperty<T>(int id) where T : class, ICoreObject, new()
        {
            T item = new T { Workarea = this };
            item.Load(id);
            return item;
        }
        /// <summary>
        /// Возвращает пустой объект, для внутреннего использования
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns></returns>
        public T Empty<T>() where T : class, ICoreObject, new()
        {
            string key = typeof (T).FullName;
            if (!EmptyValues.ContainsKey(key))
                EmptyValues.Add(key, new T { Workarea = this });
            return EmptyValues[key] as T;
        }
        public IBase Empty(int id)
        {
            Int16 value = (Int16) id;
            if (Enum.IsDefined(typeof(WhellKnownDbEntity), value))
                return Empty((WhellKnownDbEntity) value);
            return null;
        }
        /// <summary>
        /// Возвращает пустой объект, для внутреннего исползования
        /// </summary>
        /// <param name="kind">Основная сущность</param>
        /// <returns></returns>
        public IBase Empty(WhellKnownDbEntity kind)
        {
            switch (kind)
            {
                case WhellKnownDbEntity.Account:
                    return Empty<Account>();
                case WhellKnownDbEntity.Acl:
                    return null;
                case WhellKnownDbEntity.Agent:
                    return Empty<Agent>();
                case WhellKnownDbEntity.Analitic:
                    return Empty<Analitic>();
                case WhellKnownDbEntity.BankAccount:
                    return Empty<AgentBankAccount>();
                case WhellKnownDbEntity.Branche:
                    return Empty<Branche>();
                case WhellKnownDbEntity.Chain:
                    return null;
                case WhellKnownDbEntity.Column:
                    return Empty<CustomViewColumn>();
                case WhellKnownDbEntity.Contact:
                    return Empty<Contact>();
                case WhellKnownDbEntity.Country:
                    return Empty<Country>();
                case WhellKnownDbEntity.Currency:
                    return Empty<Currency>();
                case WhellKnownDbEntity.CustomViewList:
                    return Empty<CustomViewList>();
                case WhellKnownDbEntity.DbObject:
                    return Empty<Developer.DbObject>();
                case WhellKnownDbEntity.Document:
                    return Empty<Documents.Document>();
                case WhellKnownDbEntity.FactColumn:
                    return Empty<FactColumn>();
                case WhellKnownDbEntity.FactName:
                    return Empty<FactName>();
                case WhellKnownDbEntity.FileData:
                    return Empty<FileData>();
                case WhellKnownDbEntity.Folder:
                    return Empty<Folder>();
                case WhellKnownDbEntity.Hierarchy:
                    return Empty<Hierarchy>();
                case WhellKnownDbEntity.Library:
                    return Empty<Library>();
                case WhellKnownDbEntity.Price:
                    return Empty<PriceValue>();
                case WhellKnownDbEntity.PriceName:
                    return Empty<PriceName>();
                case WhellKnownDbEntity.Product:
                    return Empty<Product>();
                case WhellKnownDbEntity.ProductUnit:
                    return Empty<ProductUnit>();
                case WhellKnownDbEntity.PropertyGroup:
                    return Empty<EntityPropertyGroup>();
                case WhellKnownDbEntity.Rate:
                    return Empty<Rate>();
                case WhellKnownDbEntity.ProductRecipeItem:
                    return Empty<Recipe>();
                case WhellKnownDbEntity.Ruleset:
                    return Empty<Ruleset>();
                case WhellKnownDbEntity.SystemParameter:
                    return Empty<SystemParameter>();
                case WhellKnownDbEntity.Territory:
                    return Empty<Territory>();
                case WhellKnownDbEntity.Town:
                    return Empty<Town>();
                case WhellKnownDbEntity.Unit:
                    return Empty<Unit>();
                case WhellKnownDbEntity.Users:
                    return Empty<Uid>();
                case WhellKnownDbEntity.XmlStorage:
                    return Empty<XmlStorage>();
            }
            return null;
        }
        private Dictionary<string, object> _emptyValues;
        /// <summary>
        /// Кешированные данные о "пустых" объектах
        /// </summary>
        private Dictionary<string, object> EmptyValues
        {
            get { return _emptyValues ?? (_emptyValues = new Dictionary<string, object>()); }
        }

        /// <summary>Создание объекта</summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="templateValue">Шаблон</param>
        /// <remarks>При создании нового объекта копируются свойства: 
        /// StateId,
        /// KindId,
        /// FlagsValue,
        /// Name,
        /// Memo,
        /// Code,
        /// TemplateId,
        /// KindValue
        /// </remarks>
        /// <returns>Объект указанного типа с установленным свойством Workarea</returns>
        public T CreateNewObject<T>(T templateValue) where T : class, IBase, new()
        {
            T item = new T {Workarea = this, StateId = templateValue.StateId, FlagsValue = templateValue.FlagsValue};
            if ((templateValue.FlagsValue & FlagValue.FLAGTEMPLATE) == FlagValue.FLAGTEMPLATE)
                item.FlagsValue = item.FlagsValue - FlagValue.FLAGTEMPLATE;
            if ((templateValue.FlagsValue & FlagValue.FLAGREADONLY) == FlagValue.FLAGREADONLY)
                item.FlagsValue = item.FlagsValue - FlagValue.FLAGREADONLY;
            if ((templateValue.FlagsValue & FlagValue.FLAGHIDEN) == FlagValue.FLAGHIDEN)
                item.FlagsValue = item.FlagsValue - FlagValue.FLAGHIDEN;
            if ((templateValue.FlagsValue & FlagValue.FLAGSYSTEM) == FlagValue.FLAGSYSTEM)
                item.FlagsValue = item.FlagsValue - FlagValue.FLAGSYSTEM;
            item.KindId = templateValue.KindId;
            item.Name = templateValue.Name;
            item.Memo = templateValue.Memo;
            //item.Code = templateValue.Code;
            item.TemplateId = templateValue.Id;
            item.KindValue = templateValue.KindValue;

            if (item is ICopyValue<T>)
            {
                (item as ICopyValue<T>).CopyValue(templateValue);
            }
            item.Code = string.Empty;
            return item;
        }
        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="templateCode">Код шаблона</param>
        /// <returns>Объект указанного типа с установленным свойством Workarea</returns>
        public T CreateNewObject<T>(string templateCode) where T : class, IBase, new()
        {
            T templateValue = GetTemplates<T>().Find(s => s.Code == templateCode);
            
            return CreateNewObject<T>(templateValue);
        }
        /// <summary>
        /// Коллекция объектов указанного типа
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <returns>Коллекция содержит все элементы</returns>
        public virtual List<T> GetCollection<T>(bool refresh = false) where T : class, ICoreObject, new()
        {
            return GetCollection<T>(0, refresh);
        }

        private object objectlockGetCollection = new object() ;
        /// <summary>
        /// Коллекция объектов указанного типа
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="kind">Фильтр подтипа элемента в виде флага, для элементов являющиеся шаблонами всегда присутствует флаг 1</param>
        /// <returns>Коллекция содержит элементы которые принадлежат указанному типу</returns>
        // <example>
        // Пример получения коллекции для бухгалтерских счетов:
        // List<Account> collectionTemplates = GetCollection<Account>(1);
        // </example>
        public virtual List<T> GetCollection<T>(int kind, bool refresh = false) where T : class, ICoreObject, new() 
        {
            lock (objectlockGetCollection)
            {
                T item = new T {Workarea = this};
                List<T> collection = new List<T>();
                string keyCode = string.Format("Workarea.GetCollection_{0}_{1}", item.Entity.Name, kind);
                if (!refresh && Cashe.GetListCodeCasheData<T>().Exists(keyCode))
                {
                    return Cashe.GetListCodeCasheData<T>().Get(keyCode);
                }

                using (SqlConnection cnn = GetDatabaseConnection())
                {
                    if (cnn == null) return collection;

                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            string procedureName = string.Empty;
                            if (item.EntityId != 0)
                            {
                                ProcedureMap procedureMap = item.Entity.FindMethod("LoadAll");
                                if (procedureMap != null)
                                {
                                    procedureName = procedureMap.FullName;
                                }
                            }
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = procedureName;
                            if (kind != 0)
                                cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = kind;
                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                item = new T {Workarea = this}; //CreateNewObject<T>();
                                item.Load(reader);
                                collection.Add(item);
                                Cashe.SetCasheData(item);
                            }
                            reader.Close();
                            Cashe.GetListCodeCasheData<T>().Add(keyCode, collection);
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
        /// <summary>
        /// Коллекция объектов по списку идентификаторов
        /// </summary>
        /// <typeparam name="T">Тип объектов</typeparam>
        /// <param name="values">Список идентификаторов объектов</param>
        /// <returns></returns>
        public List<T> GetCollection<T>(IEnumerable<int> values) where T : class, ICoreObject, new() 
        {
            T item = new T { Workarea = this };
            List<T> collection = new List<T>();

            if(item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList ));
            }

            //Countiong
            int count = 0;
            using (IEnumerator<int> enumerator = values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    count++;
            }

            if (count == 0)
                return collection;

            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = item.Entity.FindMethod(GlobalMethodAlias.LoadList).FullName;
                        DatabaseHelper.AddTvpParamKeyListId(cmd, values, "@TVP");
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new T { Workarea = this };//CreateNewObject<T>();
                            item.Load(reader);
                            collection.Add(item);
                            Cashe.SetCasheData(item);
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
        /// <summary>
        /// Коллекция объектов по типу
        /// </summary>
        /// <param name="kind">Числовое представление типа</param>
        /// <returns></returns>
        public List<object> GetCollection(int kind)
        {
            switch ((WhellKnownDbEntity)kind)
            {
                case 0:
                    return null;
                case WhellKnownDbEntity.Product:
                    return GetCollection<Product>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Account:
                    return GetCollection<Account>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Agent:
                    return GetCollection<Agent>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Analitic:
                    return GetCollection<Analitic>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Currency:
                    return GetCollection<Currency>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Recipe:
                    return GetCollection<Recipe>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Folder:
                    return GetCollection<Folder>().ConvertAll(v => v as object);
                //case WhellKnownDbEntity.StorageCellTurn:
                //    return GetCollection<StorageCellTurn>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.PriceName:
                    return GetCollection<PriceName>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Unit:
                    return GetCollection<Unit>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Branche:
                    return GetCollection<Branche>().ConvertAll(v => v as object);
                //case WhellKnownDbEntity.ProductRecipeItem:
                //    return GetCollection<ProductRecipeItem>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Rate:
                    return GetCollection<Rate>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.EntityDocument:
                    return GetCollection<EntityDocument>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Library:
                    return GetCollection<Library>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Price:
                    return GetCollection<PriceName>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.FactName:
                    return GetCollection<FactName>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.FactColumn:
                    return GetCollection<FactColumn>().ConvertAll(v => v as object);
                //case WhellKnownDbEntity.DbEntity:
                //    return GetCollection<DbEntity>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Document:
                    return GetCollection<Document>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.CustomViewList:
                    return GetCollection<CustomViewList>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Column:
                    return GetCollection<CustomViewColumn>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.FileData:
                    return GetCollection<FileData>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.BankAccount:
                    return GetCollection<Account>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.SystemParameter:
                    return GetCollection<SystemParameter>().ConvertAll(v => v as object);
                //case WhellKnownDbEntity.Users:
                //    return GetCollection<Users>().ConvertAll(v => v as object);
                //case WhellKnownDbEntity.Acl:
                //    return GetCollection<Acl>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Hierarchy:
                    return GetCollection<Hierarchy>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.HierarchyContent:
                    return GetCollection<HierarchyContent>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Series:
                    return GetCollection<Series>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Contact:
                    return GetCollection<Contact>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.ProductUnit:
                    return GetCollection<ProductUnit>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Country:
                    return GetCollection<Country>().ConvertAll(v => v as object);
                //case WhellKnownDbEntity.PropertyGroup:
                //    return GetCollection<PropertyGroup>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Ruleset:
                    return GetCollection<Ruleset>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.XmlStorage:
                    return GetCollection<XmlStorage>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Territory:
                    return GetCollection<Territory>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Town:
                    return GetCollection<Town>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.StorageCell:
                    return GetCollection<StorageCell>().ConvertAll(v => v as object);
                //case WhellKnownDbEntity.Chain:
                //    return GetCollection<Chain>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.DbObject:
                    return GetCollection<DbObject>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.DbObjectChild:
                    return GetCollection<DbObjectChild>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Passport:
                    return GetCollection<Passport>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.DrivingLicence:
                    return GetCollection<DrivingLicence>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Store:
                    return GetCollection<Store>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Company:
                    return GetCollection<Company>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Bank:
                    return GetCollection<Bank>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.SystemParameterUser:
                    return GetCollection<SystemParameterUser>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.LibraryContent:
                    return GetCollection<LibraryContent>().ConvertAll(v => v as object);
                //case WhellKnownDbEntity.ReportChain:
                //    return GetCollection<ReportChain>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.FlagValue:
                    return GetCollection<FlagValue>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.DocumentSign:
                    return GetCollection<DocumentSign>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Taxe:
                    return GetCollection<Taxe>().ConvertAll(v => v as object);
                //case WhellKnownDbEntity.ResourceImage:
                //    return GetCollection<ResourceImage>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.ResourceString:
                    return GetCollection<ResourceString>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.State:
                    return GetCollection<State>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.People:
                    return GetCollection<People>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.Employer:
                    return GetCollection<Employer>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.ChainKind:
                    return GetCollection<ChainKind>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.FactValue:
                    return GetCollection<FactValue>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.UserRightCommon:
                    return GetCollection<UserRightCommon>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.UserRightElement:
                    return GetCollection<UserRightElement>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.FactDate:
                    return GetCollection<FactDate>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.FactColumnEntityKind:
                    return GetCollection<FactColumnEntityKind>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.LogUserAction:
                    return GetCollection<LogUserAction>().ConvertAll(v => v as object);
                case WhellKnownDbEntity.AgentAddress:
                    return GetCollection<AgentAddress>().ConvertAll(v => v as object);
                default:
                    throw new Exception("Неизвестный тип");
            }
        }

        #region Удаление
        internal void TryRemoveFromCasheCollection<T>(T value)
        {
            if(value is EntityType)
            {
                EntityType obj = value as EntityType;
                if (CollectionEntities.Contains(obj))
                    CollectionEntities.Remove(obj);
            }
        }

        #endregion

        #region Цепочки
        /// <summary>
        /// Коллекция родительских цепочек
        /// </summary>
        /// <param name="source">Тип</param>
        /// <param name="kind">Числовое предлставление</param>
        /// <returns></returns>
        public List<IChain<T>> CollectionChainSources<T>(T source, int? kind) where T : class, IBase, new()
        {
            List<IChain<T>> collection = new List<IChain<T>>();
            using (SqlConnection cnn = source.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = source.Entity.FindMethod("ChainLoadSource").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = source.Id;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    if (kind.HasValue)
                        prm.Value = kind.Value;
                    else
                        prm.Value = DBNull.Value;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Chain<T> item = new Chain<T> {Workarea = source.Workarea, Left = source};
                                item.Load(reader);
                                collection.Add(item);
                            }
                        }
                        reader.Close();
                        
                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
            return collection;
        }

        public List<IChainAdvanced<T, T2>> CollectionChainSources<T, T2>(T source, int? kind) where T : class, IBase, new()
            where T2 : class, IBase, new()
        {
            List<IChainAdvanced<T, T2>> collection = new List<IChainAdvanced<T, T2>>();
            using (SqlConnection cnn = source.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    // TODO: Правильное определение имени процедуры
                    cmd.CommandText = source.Entity.FindMethod("ChainAdvancedLoadSource").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = source.Id;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    if (kind.HasValue)
                        prm.Value = kind.Value;
                    else
                        prm.Value = DBNull.Value;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                       
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainAdvanced<T, T2> item = new ChainAdvanced<T, T2> { Workarea = source.Workarea, Left = source };
                                item.Load(reader);
                                collection.Add(item);
                            }
                        }
                        reader.Close();
                        
                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
            return collection;
        } 
        #endregion
        #region Дополнительные коды

        #endregion
        #region Пользователи
        private Uid _currentUser;
        /// <summary>
        /// Текущий польователь системы
        /// </summary>
        public Uid CurrentUser
        {
            get { return _currentUser; }
            internal set { _currentUser = value; }
        }
        /// <summary>
        /// Альтернативное имя пользователя
        /// </summary>
        /// <remarks>Имя пользователя используемое как альтернативное, 
        /// в контексте которого происходит текщая обработка глобальных фильтров безопастности (область видимости). 
        /// Если значение не указано или соответствует пустой строке считается что используется основной контекст.
        /// Использование альтернативного контекста возможно только при работе системы от административной учетной записи.
        /// Основное применение алтернативного контекста - web приложения.
        /// <para>
        /// В Web приложении рекомендуется использовать пользователей без SQL логинов - повышая безопастность системы и упрощая административную нагрузку.
        /// </para>
        /// </remarks>
        public string CurrentUserContext { get; set; }
        #endregion

        /// <summary>
        /// Таблица данных, сожержащая информацию на основе списка
        /// </summary>
        /// <param name="list">Список</param>
        /// <param name="elementId">Идентификато объекта</param>
        /// <param name="kindType">Числовое представление типа объекта</param>
        /// <param name="hierarchyId">Идентификатор иерархии</param>
        /// <returns></returns>
        public virtual DataTable GetCollectionCustomViewList(CustomViewList list, int elementId, int kindType, int hierarchyId)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {

                        switch (list.KindValue)
                        {
                            case 2:
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = elementId;
                                cmd.Parameters.Add(GlobalSqlParamNames.KindType, SqlDbType.Int).Value = kindType;
                                cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;
                                cmd.CommandText = list.SystemName;
                                break;
                            case 4:
                                cmd.CommandText = "select * from " + list.SystemName;
                                break;
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tbl);
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return tbl;
        }
        /// <summary>
        /// Коллекция списков для объекта
        /// </summary>
        /// <param name="value">Числовой представление системного объекта</param>
        /// <returns></returns>
        public virtual List<CustomViewList> GetCustomViewList(int value)
        {
            List<CustomViewList> collection = new List<CustomViewList>();
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value;
                        cmd.CommandText = Empty<CustomViewList>().Entity.FindMethod("Core.CustomViewListLoadByDbEntityKind").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        CustomViewList item;
                        
                        while (reader.Read())
                        {
                            item = new CustomViewList { Workarea = this };
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
