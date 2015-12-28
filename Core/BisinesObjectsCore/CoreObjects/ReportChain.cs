using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Связи отчетов и элементов
    /// </summary>
    /// <typeparam name="T">EntityType или EntityDocument</typeparam>
    /// <typeparam name="T2"></typeparam>
    public class ReportChain<T, T2> : BaseCoreObject where T : class, ICoreObject, IEntityType
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ReportChain()
        {
            EntityId = (short)WhellKnownDbEntity.ReportChain;   
        }
        
        #region Свойства
        private Int32 _toEntityId;
        /// <summary>
        /// Идентификатор типа
        /// </summary>
        public Int32 ToEntityId
        {
            get { return _toEntityId; }
            set
            {
                if (value == _toEntityId) return;
                OnPropertyChanging(GlobalPropertyNames.EntityId);
                _toEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.EntityId);
            }
        }
        private string _name;
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get { return _name; }
            internal set
            {
                if (value == _name) return;
                OnPropertyChanging(GlobalPropertyNames.Name);
                _name = value;
                OnPropertyChanged(GlobalPropertyNames.Name);
            }
        }
        private int _reportId;
        /// <summary>
        /// Идентификатор отчета
        /// </summary>
        public int ReportId
        {
            get { return _reportId; }
            set
            {
                if (value == _reportId) return;
                OnPropertyChanging(GlobalPropertyNames.ReportId);
                _reportId = value;
                OnPropertyChanged(GlobalPropertyNames.ReportId);
            }
        }

        private Library _library;
        /// <summary>
        /// Отчет
        /// </summary>
        public Library Library
        {
            get
            {
                if (_reportId == 0)
                    return null;
                if (_library == null)
                    _library = Workarea.Cashe.GetCasheData<Library>().Item(_reportId);
                else if (_library.Id != _reportId)
                    _library = Workarea.Cashe.GetCasheData<Library>().Item(_reportId);
                return _library;
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
        #endregion

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_toEntityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ToEntityId, XmlConvert.ToString(_toEntityId));
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
            if (_reportId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ReportId, XmlConvert.ToString(_reportId));
            if (_elementId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ElementId, XmlConvert.ToString(_elementId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ToEntityId) != null)
                _toEntityId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ToEntityId));
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
            if (reader.GetAttribute(GlobalPropertyNames.ReportId) != null)
                _reportId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ReportId));
            if (reader.GetAttribute(GlobalPropertyNames.ElementId) != null)
                _elementId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ElementId));
        }
        #endregion

        public bool IsEntityDocument
        {
            get { return (typeof(T) == typeof(EntityDocument)); }
            
        }

        private T _toEntity;

        /// <summary>
        /// Тип объекта
        /// </summary>
        public T ToEntity
        {
            get
            {
                if (_toEntityId == 0)
                    return null;
                if (!IsEntityDocument)
                {
                    if (_toEntity == null)
                        _toEntity = Workarea.CollectionEntities.Find(f => f.Id == ToEntityId) as T;
                    else if (_toEntity.Id != ToEntityId)
                        _toEntity = Workarea.CollectionEntities.Find(f => f.Id == ToEntityId) as T;
                    return _toEntity;
                }
                else if (IsEntityDocument)
                {
                    if (_toEntity == null)
                        _toEntity = Workarea.CollectionDocumentTypes().Find(f => f.Id == ToEntityId) as T;
                    else if (_toEntity.Id != ToEntityId)
                        _toEntity = Workarea.CollectionDocumentTypes().Find(f => f.Id == ToEntityId) as T;
                    return _toEntity;
                }
                return _toEntity;
            }
        }

        /// <summary>
        /// Связанный объект: Product, Analitic etc
        /// </summary>
        public T2 ToElement { get; set; }

        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            if (_elementId == 0)
                throw new ValidateException("Не указан идентификатор объекта");
            if (_reportId == 0)
                throw new ValidateException("Не указан идентификатор отчета");
        }

        /// <summary>Создание объекта в базе данных</summary>
        /// <remarks>Перед созданием объекта не выполняется проверка <see cref="BaseCoreObject.Validate"/>.
        /// Метод использует хранимую процедуру указанную в методах объекта по ключу "ReportChainInsert" для объекта ToEntity.
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
            if (!IsEntityDocument)
                Create(ToEntity.FindMethod(GlobalMethodAlias.ReportChainInsert).FullName);
            else
                Create(Workarea.CollectionEntities.Find(f => f.Id == 20).FindMethod(GlobalMethodAlias.ReportChainInsert).FullName);
            OnCreated();
            
        }

        /// <summary>Обновление объекта в базе данных</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "ReportChainUpdate" для объекта ToEntity</remarks>
        /// <seealso cref="BaseCoreObject.Create()"/>
        /// <seealso cref="BaseCoreObject.Load(int)"/>
        /// <seealso cref="BaseCoreObject.Validate"/>
        protected override void Update(bool versionControl = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnUpdating(e);
            if (e.Cancel)
                return;
            if (!IsEntityDocument)
                Update(ToEntity.FindMethod(GlobalMethodAlias.ReportChainUpdate).FullName, versionControl);
            else
                Update(Workarea.CollectionEntities.Find(f => f.Id == 20).FindMethod(GlobalMethodAlias.ReportChainUpdate).FullName, versionControl);
            OnUpdated();
        }
        /// <summary>
        /// Удалить
        /// </summary>
        public override void Delete(bool checkVersion = true)
        {
            if (!IsEntityDocument)
                Delete(ToEntity.FindMethod(GlobalMethodAlias.ReportChainDelete).FullName, checkVersion);
            else
                Delete(Workarea.CollectionEntities.Find(f => f.Id == 20).FindMethod(GlobalMethodAlias.ReportChainDelete).FullName, checkVersion);
        }

        /// <summary>Поиск метода</summary>
        /// <param name="metodAliasName">Псевдоним</param>
        /// <returns></returns>
        protected internal override string FindProcedure(string metodAliasName)
        {
            if (!IsEntityDocument)
                return metodAliasName.StartsWith("ReportChain") ? ToEntity.FindMethod(metodAliasName).FullName : ToEntity.FindMethod("ReportChain" + metodAliasName).FullName;
            else
                return metodAliasName.StartsWith("ReportChain") ? Workarea.CollectionEntities.Find(f => f.Id == 20).FindMethod(metodAliasName).FullName : Workarea.CollectionEntities.Find(f => f.Id == 20).FindMethod("ReportChain" + metodAliasName).FullName;
        }

        #region База данных
        /// <summary>
        /// Загрузить объект из базы данных по идентификатору
        /// </summary>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            //Load(value, Workarea.FindMethod("ReportChainLoad").FullName);
            CancelEventArgs e = new CancelEventArgs();
            OnLoading(e);
            if (e.Cancel)
                return;
            if (!IsEntityDocument)
                Load(value, ToEntity.FindMethod("ReportChainLoad").FullName);
        }
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="reader">Объект <see cref="SqlDataReader"/> чтения данных</param>
        /// <param name="endInit">Закончить инициализацию</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _reportId = reader.GetInt32(9);
                _elementId = reader.GetInt32(10);
                _toEntityId = BaseKind.ExtractEntityKind(reader.GetInt32(11));
                _name = reader.GetString(12);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>
        /// Установить значения параметров для комманды создания
        /// </summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ReportId, SqlDbType.Int) { IsNullable = false, Value = _reportId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ElementId, SqlDbType.Int) { IsNullable = false, Value = _elementId };
            sqlCmd.Parameters.Add(prm);
        }
        #endregion
    }

}
