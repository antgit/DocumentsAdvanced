using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Serialization;

namespace BusinessObjects
{
    /// <summary>Структура используемая для сохранения объекта</summary>
    internal struct BaseCoreStruct
    {
        /// <summary>Идентификатор</summary>
        public int Id;
        /// <summary>Глобальный идентификатор</summary>
        public Guid Guid;
        /// <summary>Идентификатор в базе источнике</summary>
        public int DbSourceId;
        /// <summary>Идентификатор владельца</summary>
        public int DatabaseId;
        /// <summary>Идентификатор состояния</summary>
        public int StateId;
        /// <summary>Дата создания или последнего изменения записи</summary>
        public DateTime? DateModified;
        /// <summary>Идентификатор системного типа</summary>
        public short EntityId;
        /// <summary>Набор флагов</summary>
        public int FlagsValue;
        /// <summary>Имя пользователя создавшего или изменившего запись</summary>
        public string UserName;
    }

    /// <summary>Базовый класс бизнес объектов</summary>
    //[Serializable]
    [DataContract(Namespace="http://atlan.com.ua/BusinessObjects")]
    public abstract class BaseCoreObject : BasePropertyChangeSupport, ICoreObject, IDataErrorInfo, ISerializable, IXmlSerializable 
    {
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
        protected virtual void OnSaved()
        {
            OnSaved(new EventArgs());
        }
        /// <summary>
        /// Метод вызываемый непосредственно после сохранения
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSaved(EventArgs e)
        {
            if (_savedHandlers != null)
                _savedHandlers.Invoke(this, e);
        }
        /// <summary>
        /// Метод вызываемый непосредственно перед сохранения
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSaving(CancelEventArgs e)
        {
            if (_savingHandlers != null)
                _savingHandlers.Invoke(this, e);
        }
        #endregion
        #region События загрузки
        [NonSerialized]
        private CancelEventHandler _loadingHandlers;
        /// <summary>
        /// Событие начала загрузки
        /// </summary>
        public event CancelEventHandler Loading
        {
            add
            {
                _loadingHandlers = (CancelEventHandler)
                      Delegate.Combine(_loadingHandlers, value);
            }
            remove
            {
                _loadingHandlers = (CancelEventHandler)
                      Delegate.Remove(_loadingHandlers, value);
            }
        }

        [NonSerialized]
        private EventHandler _loadHandlers;
        /// <summary>
        /// Событие после загрузки
        /// </summary>
        public event EventHandler LoadEnd
        {
            add
            {
                _loadHandlers = (EventHandler)
                      Delegate.Combine(_loadHandlers, value);
            }
            remove
            {
                _loadHandlers = (EventHandler)
                      Delegate.Remove(_loadHandlers, value);
            }
        }
        /// <summary>
        /// Метод вызываемый несредственно после загрузки из базы данных
        /// </summary>
        protected virtual void OnLoadEnd()
        {
            OnLoadEnd(new EventArgs());
        }
        /// <summary>
        /// Метод вызываемый несредственно после загрузки из базы данных
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLoadEnd(EventArgs e)
        {
            if (_loadHandlers != null)
                _loadHandlers.Invoke(this, e);
        }
        /// <summary>
        /// Метод вызываемый несредственно перед загрузкой из базы данных
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLoading(CancelEventArgs e)
        {
            if (_loadingHandlers != null)
                _loadingHandlers.Invoke(this, e);
        }
        #endregion
        #region События удаления
        [NonSerialized]
        private CancelEventHandler _deletingHandlers;
        /// <summary>
        /// Событие начала удаления
        /// </summary>
        public event CancelEventHandler Deleting
        {
            add
            {
                _deletingHandlers = (CancelEventHandler)
                      Delegate.Combine(_deletingHandlers, value);
            }
            remove
            {
                _deletingHandlers = (CancelEventHandler)
                      Delegate.Remove(_deletingHandlers, value);
            }
        }

        [NonSerialized]
        private EventHandler _deletedHandlers;
        /// <summary>
        /// Событие после удаления
        /// </summary>
        public event EventHandler Deleted
        {
            add
            {
                _deletedHandlers = (EventHandler)
                      Delegate.Combine(_deletedHandlers, value);
            }
            remove
            {
                _deletedHandlers = (EventHandler)
                      Delegate.Remove(_deletedHandlers, value);
            }
        }

        /// <summary>
        /// Метод вызываемый непосредственно после удаления
        /// </summary>
        protected virtual void OnDeleted()
        {
            OnDeleted(new EventArgs());
        }
        /// <summary>
        /// Метод вызываемый непосредственно после удаления
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDeleted(EventArgs e)
        {
            if (_deletedHandlers != null)
                _deletedHandlers.Invoke(this, e);
        }
        /// <summary>
        /// Метод вызываемый непосредственно перед удаления
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDeleting(CancelEventArgs e)
        {
            if (_deletingHandlers != null)
                _deletingHandlers.Invoke(this, e);
        }
        #endregion

        #region События создания
        [NonSerialized]
        private CancelEventHandler _creatingHandlers;
        /// <summary>
        /// Событие начала создания
        /// </summary>
        public event CancelEventHandler Creating
        {
            add
            {
                _creatingHandlers = (CancelEventHandler)
                      Delegate.Combine(_creatingHandlers, value);
            }
            remove
            {
                _creatingHandlers = (CancelEventHandler)
                      Delegate.Remove(_creatingHandlers, value);
            }
        }

        [NonSerialized]
        private EventHandler _createdHandlers;
        /// <summary>
        /// Событие после создания
        /// </summary>
        public event EventHandler Created
        {
            add
            {
                _createdHandlers = (EventHandler)
                      Delegate.Combine(_createdHandlers, value);
            }
            remove
            {
                _createdHandlers = (EventHandler)
                      Delegate.Remove(_createdHandlers, value);
            }
        }
        /// <summary>
        /// Метод вызываемый несредственно после создания записи в базе данных
        /// </summary>
        protected virtual void OnCreated()
        {
            OnCreated(new EventArgs());
            this.Workarea.OnCreatedCoreObject(this);
        }
        /// <summary>
        /// Метод вызываемый несредственно после создания записи в базе данных
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCreated(EventArgs e)
        {
            if (_createdHandlers != null)
                _createdHandlers.Invoke(this, e);
        }
        /// <summary>
        /// Метод вызываемый несредственно перед создания записи в базе данных
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCreating(CancelEventArgs e)
        {
            if (_creatingHandlers != null)
                _creatingHandlers.Invoke(this, e);
        }
        #endregion

        #region События обновления
        [NonSerialized]
        private CancelEventHandler _updatingHandlers;
        /// <summary>
        /// Событие начала обновления
        /// </summary>
        public event CancelEventHandler Updating
        {
            add
            {
                _updatingHandlers = (CancelEventHandler)
                      Delegate.Combine(_updatingHandlers, value);
            }
            remove
            {
                _updatingHandlers = (CancelEventHandler)
                      Delegate.Remove(_updatingHandlers, value);
            }
        }

        [NonSerialized]
        private EventHandler _updatedHandlers;
        /// <summary>
        /// Событие после обновления
        /// </summary>
        public event EventHandler Updated
        {
            add
            {
                _updatedHandlers = (EventHandler)
                      Delegate.Combine(_updatedHandlers, value);
            }
            remove
            {
                _updatedHandlers = (EventHandler)
                      Delegate.Remove(_updatedHandlers, value);
            }
        }
        /// <summary>
        /// Метод вызываемый несредственно после обновления записи в базе данных
        /// </summary>
        protected virtual void OnUpdated()
        {
            OnUpdated(new EventArgs());
        }
        /// <summary>
        /// Метод вызываемый несредственно после обновления записи в базе данных
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUpdated(EventArgs e)
        {
            if (_updatedHandlers != null)
                _updatedHandlers.Invoke(this, e);
        }
        /// <summary>
        /// Метод вызываемый несредственно перед обновления записи в базе данных
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUpdating(CancelEventArgs e)
        {
            if (_updatingHandlers != null)
                _updatingHandlers.Invoke(this, e);
        }
        #endregion

        #region События проверки
        [NonSerialized]
        private CancelEventHandler _validatingHandlers;
        /// <summary>
        /// Событие начала проверки
        /// </summary>
        public event CancelEventHandler Validating
        {
            add
            {
                _validatingHandlers = (CancelEventHandler)
                      Delegate.Combine(_validatingHandlers, value);
            }
            remove
            {
                _validatingHandlers = (CancelEventHandler)
                      Delegate.Remove(_validatingHandlers, value);
            }
        }

        [NonSerialized]
        private EventHandler _validatedHandlers;
        /// <summary>
        /// Событие после проверки
        /// </summary>
        public event EventHandler Validated
        {
            add
            {
                _validatedHandlers = (EventHandler)
                      Delegate.Combine(_validatedHandlers, value);
            }
            remove
            {
                _validatedHandlers = (EventHandler)
                      Delegate.Remove(_validatedHandlers, value);
            }
        }
        protected virtual void OnValidated()
        {
            OnValidated(new EventArgs());
        }
        protected virtual void OnValidated(EventArgs e)
        {
            if (_validatedHandlers != null)
                _validatedHandlers.Invoke(this, e);
        }
        protected virtual void OnValidating(CancelEventArgs e)
        {
            if (_validatingHandlers != null)
                _validatingHandlers.Invoke(this, e);
        }
        #endregion

        #region Сериализация
        //http://blog.kowalczyk.info/article/Serialization-in-C.html
        //http://devolutions.net/articles/dot-net/Net-Serialization-FAQ.aspx
        //http://www.albahari.com/nutshell/ch15.aspx
        //http://insideaspnet.com/index/custom-serialization/
        //http://www.dotnetspider.com/resources/37599-JSON-Serialization-using-DataContract-Serializer.aspx
        //http://dotnet.itags.org/webservices/104985/
        //http://blogs.techrepublic.com.com/howdoi/?p=143
        //http://netcode.ru/dotnet/?lang=&katID=30&skatID=251&artID=6603
        // The following constructor is for deserialization
        protected BaseCoreObject(SerializationInfo info, StreamingContext context)
        {
            //productId = info.GetInt32("Product ID");
            //price = info.GetDecimal("Price");
            //quantity = info.GetInt32("Quantity");
            //total = price * quantity;
        }
        // The following method is called during serialization
        [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(GlobalPropertyNames.DatabaseId, DatabaseId);
            info.AddValue(GlobalPropertyNames.DateModified, DateModified);
            info.AddValue(GlobalPropertyNames.SourceId, DbSourceId);
            info.AddValue(GlobalPropertyNames.EntityId, EntityId);
            info.AddValue(GlobalPropertyNames.FlagsValue, FlagsValue);
            info.AddValue(GlobalPropertyNames.Guid, Guid);
            info.AddValue(GlobalPropertyNames.Id, Id);
            info.AddValue(GlobalPropertyNames.StateId, StateId);
            info.AddValue(GlobalPropertyNames.UserName, UserName);
            info.AddValue(GlobalPropertyNames.IsNew, IsNew);
        }

        #endregion
        #region IXmlSerializable Members
        //  http://www.albahari.com/nutshell/ch15.aspx
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }
        /// <summary>
        /// Чтение данных из XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        public virtual void ReadXml(XmlReader reader)
        {
            reader.Read(); // Skip <Pound>
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == GetType().Name.ToString())
            {
                ReadPartialXml(reader);

                //City = reader["City"];
                //reader.Read(); // Skip ahead to next node
                //if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Animals")
                //{
                //   reader.Read(); // Skip ahead to next node
                //    while (reader.MoveToContent() == XmlNodeType.Element &&\
                //    Type.GetType(reader.LocalName).IsSubclassOf(typeof (Animal)))
                //    {
                //        AnimalType = (Animal) Type.GetType(reader.LocalName);
                //        Animal a = AnimalType.Assembly.CreateInstance(reader.LocalName);
                //        a.ReadXml(reader);
                //        Animals.Add(a.Key, a);
                //        reader.Read(); // Skip to next animal (if there is one)
                //    }
                //}
            }
        }
        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected virtual void ReadPartialXml(XmlReader reader)
        {
            Id = XmlConvert.ToInt32(reader[GlobalPropertyNames.Id]);
            Guid = XmlConvert.ToGuid(reader[GlobalPropertyNames.Guid]);
            DatabaseId = XmlConvert.ToInt32(reader[GlobalPropertyNames.DatabaseId]);

            if (reader.GetAttribute(GlobalPropertyNames.DateModified) != null)
                DateModified = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateModified));

            if(reader.GetAttribute(GlobalPropertyNames.DbSourceId)!=null)
                DbSourceId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DbSourceId));

            if (reader.GetAttribute(GlobalPropertyNames.FlagsValue) != null)
                FlagsValue = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FlagsValue));

            StateId = XmlConvert.ToInt32(reader[GlobalPropertyNames.StateId]);

            if (reader.GetAttribute(GlobalPropertyNames.UserName) != null)
                UserName = reader.GetAttribute(GlobalPropertyNames.UserName);
        }
        /// <summary>
        /// Запись данных в XML
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(GetType().Name.ToString());
            WritePartialXml(writer);
            writer.WriteEndElement();
        }
        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected virtual void WritePartialXml(XmlWriter writer)
        {
            writer.WriteAttributeString(GlobalPropertyNames.Id, XmlConvert.ToString(Id));
            writer.WriteAttributeString(GlobalPropertyNames.Guid, XmlConvert.ToString(Guid));
            writer.WriteAttributeString(GlobalPropertyNames.DatabaseId, DatabaseId.ToString());

            if (DateModified.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateModified, XmlConvert.ToString(DateModified.Value));

            if (DbSourceId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SourceId, XmlConvert.ToString(DbSourceId));

            writer.WriteAttributeString(GlobalPropertyNames.EntityId, EntityId.ToString()); // TODO:  ?????

            if (FlagsValue != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FlagsValue, FlagsValue.ToString());

            writer.WriteAttributeString(GlobalPropertyNames.StateId, StateId.ToString());

            if (!String.IsNullOrEmpty(UserName))
                writer.WriteAttributeString(GlobalPropertyNames.UserName, UserName);

            //writer.WriteAttributeString(GlobalPropertyNames.IsNew, IsNew.ToString());

            //writer.WriteAttributeString("City", City);
            //writer.WriteStartElement("Animals");
            //foreach (Animal a in Animals.Values)
            //{
            //    a.WriteXml(writer);
            //}
            //writer.WriteEndElement();
            
        }

        //public virtual void ReadXml(XmlReader reader)
        //{
        //    //UserName = reader.GetAttribute("UserName");
        //    reader.MoveToContent();

        //    while (reader.Read())
        //    {
        //        if (reader.IsStartElement())
        //        {
        //            if (!reader.IsEmptyElement)
        //            {
        //                string elementName = reader.Name;
        //                reader.Read(); // Read the start tag.

        //                OnReadXml(reader, elementName);
        //            }
        //        }
        //    }
        //}

        protected virtual void OnReadXml(XmlReader reader, string elementName)
        {
            //if (elementName == GlobalPropertyNames.DatabaseId)
            //{
            //    _databaseId = Convert.ToInt32(reader.ReadString());
            //}
            //if (elementName == GlobalPropertyNames.DateModified)
            //{
            //    _dateModified = Convert.ToDateTime(reader.ReadString());
            //}
            //if (elementName == GlobalPropertyNames.SourceId)
            //{
            //    _dbSourceId = Convert.ToInt32(reader.ReadString());
            //}
            //if (elementName == GlobalPropertyNames.EntityId)
            //{
            //    _entityId = Convert.ToInt16(reader.ReadString());
            //}
            //if (elementName == GlobalPropertyNames.FlagsValue)
            //{
            //    _flagsValue = Convert.ToInt32(reader.ReadString());
            //}
            //if (elementName == GlobalPropertyNames.Guid)
            //{
            //    _guid = Guid.Parse(reader.ReadString());
            //}
            //if (elementName == GlobalPropertyNames.Id)
            //{
            //    _id = Convert.ToInt32(reader.ReadString());
            //}
            //if (elementName == GlobalPropertyNames.StateId)
            //{
            //    _stateId = Convert.ToInt32(reader.ReadString());
            //}
            //if (elementName == GlobalPropertyNames.UserName)
            //{
            //    _userName = reader.ReadString();
            //}
            //if (elementName == GlobalPropertyNames.IsNew)
            //{
            //    _isNew = Convert.ToBoolean(reader.ReadString());
            //}
            //else
            //{
            //    reader.Read();
            //}
        }

        //public virtual void WriteXml(XmlWriter writer)
        //{
        //    //only start document if needed
        //    //bool start = false;
        //    //if (writer.WriteState == WriteState.Start)
        //    //{
        //    //    start = true;
        //    //    writer.WriteStartDocument();
        //    //    writer.WriteStartElement("root", "http://example.com");
        //    //}

        //    writer.WriteStartElement(GetType().Name.ToString());
        //    //writer.WriteStartElement(GetType().ToString());
        //    writer.WriteAttributeString(GlobalPropertyNames.DatabaseId, DatabaseId.ToString());
        //    if (DateModified.HasValue)
        //        writer.WriteAttributeString(GlobalPropertyNames.DateModified, DateModified.ToString());
        //    if (DbSourceId!=0)
        //        writer.WriteAttributeString(GlobalPropertyNames.SourceId, DbSourceId.ToString());
        //    writer.WriteAttributeString(GlobalPropertyNames.EntityId, EntityId.ToString());
        //    if (FlagsValue!=0)
        //        writer.WriteAttributeString(GlobalPropertyNames.FlagsValue, FlagsValue.ToString());
        //    writer.WriteAttributeString(GlobalPropertyNames.Guid, Guid.ToString());
        //    writer.WriteAttributeString(GlobalPropertyNames.Id, Id.ToString());
        //    writer.WriteAttributeString(GlobalPropertyNames.StateId, StateId.ToString());
        //    if (!String.IsNullOrEmpty(UserName))
        //        writer.WriteAttributeString(GlobalPropertyNames.UserName, UserName);
        //    writer.WriteAttributeString(GlobalPropertyNames.IsNew, IsNew.ToString());
        //    /*
        //    writer.WriteAttributeString("UserName", UserName);
        //    writer.WriteElementString("MachineName", MachineName);
        //     */
        //}

        #endregion
        /// <summary>Конструктор</summary>
        protected BaseCoreObject(): base()
        {
            _isNew = true;
        }

        /// <summary>
        /// Сравнение объекта для службы обмена данными
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange(ICoreObject value)
        {
            if (value.DatabaseId != DatabaseId)
                return false;
            if(value.FlagsValue!=FlagsValue)
                return false;
            if (value.Guid != Guid)
                return false;
            if (value.StateId != StateId)
                return false;

            return true;
        }
        private EntityType _entity;
        /// <summary>Тип элемента</summary>
        public EntityType Entity
        {
            get {
                return OnRequestEntity();
            }
        }

        protected virtual EntityType OnRequestEntity()
        {
            if (EntityId == 0)
                return null;
            if (_entity == null)
                _entity = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == EntityId);
            else if (_entity.Id != EntityId)
                _entity = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == EntityId);
            return _entity;
        }
        [NonSerialized]
        private short _entityId;
        /// <summary>Идентификатор системного типа</summary>
        [XmlIgnore]
        public short EntityId
        {
            get { return _entityId; }
            set
            {
                if (_entityId != 0) return;
                if (value == _entityId) return;
                OnPropertyChanging(GlobalPropertyNames.EntityId);
                _entityId = value;
                OnPropertyChanged(GlobalPropertyNames.EntityId);
            }
        }
        
        #region Свойства
        [NonSerialized]
        private Workarea _iWorkarea;
        /// <summary>Рабочая область</summary>
        [XmlIgnore]
        public virtual Workarea Workarea
        {
            get { return _iWorkarea; }
            set
            {
                if (_iWorkarea == value) return;
                OnPropertyChanging(GlobalPropertyNames.Workarea);
                _iWorkarea = value;
                OnPropertyChanged(GlobalPropertyNames.Workarea);
            }
        }

        private int _id;
        /// <summary>Идентификатор</summary>
        [Description("Идентификатор"),
        DataMember]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != 0 && !IsNew) return;
                if (_id == value) return;
                OnPropertyChanging(GlobalPropertyNames.Id);
                _id = value;
                OnPropertyChanged(GlobalPropertyNames.Id);
            }
        }
        private int _dbSourceId;
        /// <summary>Идентификатор в базе источнике</summary>
        int ICoreObject.DbSourceId
        {
            get
            {
                return DbSourceId;
            }
            set
            {
                DbSourceId = value;
            }
        }
        /// <summary>Идентификатор в базе источнике</summary>
        [DataMember]
        public int DbSourceId
        {
            get
            {
                return _dbSourceId;
            }
            set
            {
                if (_dbSourceId == value) return;
                OnPropertyChanging(GlobalPropertyNames.SourceId);
                _dbSourceId = value;
                OnPropertyChanged(GlobalPropertyNames.SourceId);
            }
        }
        private int _databaseId;
        /// <summary>Идентификатор владельца</summary>
        int ICoreObject.DatabaseId
        {
            get
            {
                return DatabaseId;
            }
            set
            {
                DatabaseId = value;
            }
        }
        /// <summary>Идентификатор владельца</summary>
        [DataMember]
        public int DatabaseId
        {
            get
            {
                return _databaseId;
            }
            set
            {
                if (_databaseId == value) return;
                OnPropertyChanging(GlobalPropertyNames.DatabaseId);
                _databaseId = value;
                OnPropertyChanged(GlobalPropertyNames.DatabaseId);
            }
        }

        private Guid _guid;
        /// <summary>Глобальный идентификатор</summary>
        [Description("Глобальный идентификатор"), DataMember]
        public Guid Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                if (_guid.Equals(value)) return;
                OnPropertyChanging(GlobalPropertyNames.Guid);
                _guid = value;
                OnPropertyChanged(GlobalPropertyNames.Guid);
            }
        }
        Guid ICoreObject.Guid
        {
            get { return Guid; }
            set { Guid = value; }
        }
        /// <summary>Версия</summary>
        /// <returns></returns>
        public virtual byte[] GetVersion()
        {
            if (_objectVersion == null)
                return null;
            return (byte[])_objectVersion.Clone();
        }
        private byte[] _objectVersion;
        /// <summary>Версия</summary>
        [Description("Версия")]
        public byte[] ObjectVersion
        {
            get
            {
                return _objectVersion;
            }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.Version);
                _objectVersion = value;
                OnPropertyChanged(GlobalPropertyNames.Version);
            }
        }

        private int _flagsValue;
        /// <summary>Набор флагов</summary>
        [Description("Набор флагов"), 
        DataMember]
        public int FlagsValue
        {
            get
            {
                return _flagsValue;
            }
            set
            {
                if (_flagsValue == value) return;
                OnPropertyChanging(GlobalPropertyNames.FlagsValue);
                _flagsValue = value;
                OnPropertyChanged(GlobalPropertyNames.FlagsValue);
            }
        }

        /// <summary>
        /// Значение флагов в виде строки
        /// </summary>
        /// <returns></returns>
        public string FlagsValueString()
        {
            string res = string.Empty;
            foreach (FlagValue flagItem in Entity.FlagValues)
            {
                if ((flagItem.Value & FlagsValue) == flagItem.Value)
                    res = res + flagItem.Name + "; ";
                
            }
            return res;
        }

        private int _stateId;
        /// <summary>Идентификатор состояния</summary>
        [Description("Идентификатор состояния"),
        DataMember]
        public virtual int StateId
        {
            get { return _stateId; }
            set
            {
                if (_stateId == value) return;
                OnPropertyChanging(GlobalPropertyNames.StateId);
                _stateId = value;
                OnPropertyChanged(GlobalPropertyNames.StateId);
            }
        }
        /// <summary>Состояние по умолчанию</summary>
        public bool IsStateDefault { get { return _stateId == State.STATEDEFAULT; } }
        /// <summary>Состояние "Активное"</summary>
        public bool IsStateActive { get { return _stateId == State.STATEACTIVE; } }
        /// <summary>Состояние "Удален"</summary>
        public bool IsStateDeleted { get { return _stateId == State.STATEDELETED; } }
        /// <summary>Состояние "Запрещен"</summary>
        public bool IsStateDeny{ get { return _stateId == State.STATEDENY; } }
        /// <summary>Состояние "Разрешен"</summary>
        public bool IsStateAllow { get { return (_stateId == State.STATEACTIVE) || (_stateId == State.STATEDEFAULT); } }
        /// <summary>Флаг "Только чтение"</summary>
        public bool IsReadOnly { get { return (FlagsValue & FlagValue.FLAGREADONLY) == FlagValue.FLAGREADONLY; } }
        /// <summary>Флаг "Скрытый"</summary>
        public bool IsHiden { get { return (FlagsValue & FlagValue.FLAGHIDEN) == FlagValue.FLAGHIDEN; } }
        /// <summary>Флаг "Системный"</summary>
        public bool IsSystem { get { return (FlagsValue & FlagValue.FLAGSYSTEM) == FlagValue.FLAGSYSTEM; } }
        /// <summary>Флаг "Шаблон"</summary>
        public bool IsTemplate { get { return (FlagsValue & FlagValue.FLAGTEMPLATE) == FlagValue.FLAGTEMPLATE; } }
        /// <summary>Флаг "Необновляемый"</summary>
        public bool IsNoupdate { get { return (FlagsValue & FlagValue.EXCNOUPDATE) == FlagValue.EXCNOUPDATE; } }
        /// <summary>Флаг "Архивный"</summary>
        public bool IsArhive { get { return (FlagsValue & FlagValue.FLAGARHIVE) == FlagValue.FLAGARHIVE; } }

        /// <summary>Установить-добавить флаг для объекта</summary>
        /// <param name="value">Значение</param>
        public virtual void SetFlagValue(int value)
        {
            if (!((FlagsValue & value) == value))
                FlagsValue = FlagsValue + value;
        }
        /// <summary>Установить-добавить флаг для объекта</summary>
        /// <param name="value">Значение</param>
        public virtual void RemoveFlagValue(int value)
        {
            if (((FlagsValue & value) == value))
                FlagsValue = FlagsValue - value;
        }
        /// <summary>Добавить флаг "Только чтение"</summary>
        public void SetFlagReadOnly()
        {
            SetFlagValue(FlagValue.FLAGREADONLY);
        }
        /// <summary>Удалить флаг "Только чтение"</summary>
        public void RemoveFlagReadOnly()
        {
            RemoveFlagValue(FlagValue.FLAGREADONLY);
        }
        /// <summary>Добавить флаг "Скрытый"</summary>
        public void SetFlagHidden()
        {
            SetFlagValue(FlagValue.FLAGHIDEN);
        }
        /// <summary>Удалить флаг "Скрытый"</summary>
        public void RemoveFlagHidden()
        {
            RemoveFlagValue(FlagValue.FLAGHIDEN);
        }
        /// <summary>Добавить флаг "Системный"</summary>
        public void SetFlagSystem()
        {
            SetFlagValue(FlagValue.FLAGSYSTEM);
        }
        /// <summary>Удалить флаг "Системный"</summary>
        public void RemoveFlagSystem()
        {
            RemoveFlagValue(FlagValue.FLAGSYSTEM);
        }
        /// <summary>Добавить флаг "Шаблон"</summary>
        public void SetFlagTemplate()
        {
            SetFlagValue(FlagValue.FLAGTEMPLATE);
        }
        /// <summary>Удалить флаг "Шаблон"</summary>
        public void RemoveFlagTemplate()
        {
            RemoveFlagValue(FlagValue.FLAGTEMPLATE);
        }
        /// <summary>Добавить флаг "Необновляемый"</summary>
        public void SetFlagNoupdate()
        {
            SetFlagValue(FlagValue.EXCNOUPDATE);
        }
        /// <summary>Удалить флаг "Необновляемый"</summary>
        public void RemoveFlagNoupdate()
        {
            RemoveFlagValue(FlagValue.EXCNOUPDATE);
        }
        /// <summary>Добавить флаг "Архивный"</summary>
        public void SetFlagArhive()
        {
            SetFlagValue(FlagValue.FLAGARHIVE);
        }
        /// <summary>Удалить флаг "Архивный"</summary>
        public void RemoveFlagArhive()
        {
            RemoveFlagValue(FlagValue.FLAGARHIVE);
        }
        private State _state;
        /// <summary>Состояние</summary>
        public State State
        {
            get
            {
                if (_stateId < 0)
                    return null;
                if (_state == null)
                {
                    if ((this as ICoreObject).Workarea!=null)
                        _state = (this as ICoreObject).Workarea.CollectionStates.Find(f => f.Id == _stateId);
                }
                else if (_state.Id != _stateId)
                {
                    if ((this as ICoreObject).Workarea != null)
                        _state = (this as ICoreObject).Workarea.CollectionStates.Find(f => f.Id == _stateId);
                }
                return _state;
            }
            set
            {
                if (_state == value) return;
                OnPropertyChanging(GlobalPropertyNames.State);
                _state = value;
                _stateId = _state == null ? 0 : _state.Id;
                OnPropertyChanged(GlobalPropertyNames.State);
            }
        }
        
        private string _userName;
        /// <summary>
        /// Имя пользователя создавшего или изменившего запись
        /// </summary>
        [DataMember]
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value == _userName) return;
                OnPropertyChanging(GlobalPropertyNames.UserName);
                _userName = value;
                OnPropertyChanged(GlobalPropertyNames.UserName);
            }
        }

        private DateTime? _dateModified;
        /// <summary>
        /// Дата создания или последнего изменения записи
        /// </summary>
        [DataMember(EmitDefaultValue = false)] 
        public DateTime? DateModified
        {
            get { return _dateModified; }
            set
            {
                if (value == _dateModified) return;
                OnPropertyChanging(GlobalPropertyNames.DateModified);
                _dateModified = value;
                OnPropertyChanged(GlobalPropertyNames.DateModified);
            }
        }
        
        
        private bool _isNew;
        /// <summary>Является ли объект новым</summary>
        [Description("Является ли объект новым"), Browsable(false)]
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                if (_isNew == value) return;
                OnPropertyChanging(GlobalPropertyNames.IsNew);
                _isNew = value;
                OnPropertyChanged(GlobalPropertyNames.IsNew);
            }
        }


        private DateTime? _lastLoad;
        /// <summary>Дата последней загрузки объекта из базы данных</summary>
        /// <remarks>Используется для автоматического обновления данных объекта из базы данных</remarks>
        internal DateTime? LastLoad
        {
            get { return _lastLoad; }
            set
            {
                if (value == _lastLoad) return;
                OnPropertyChanging(GlobalPropertyNames.LastLoad);
                _lastLoad = value;
                OnPropertyChanged(GlobalPropertyNames.LastLoad);
            }
        }

        [NonSerialized]
        private DateTime? _lastLoadPartial;
        /// <summary>
        /// Дата и время последней частичной загрузки объекиа
        /// </summary>
        [XmlIgnore]
        internal DateTime? LastLoadPartial
        {
            get { return _lastLoadPartial; }
            set
            {
                if (value == _lastLoadPartial) return;
                OnPropertyChanging(GlobalPropertyNames.LastLoadPartial);
                _lastLoadPartial = value;
                OnPropertyChanged(GlobalPropertyNames.LastLoadPartial);
            }
        }
        
        

        #endregion

        #region Состояния
        BaseCoreStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (overwrite | _baseStruct.Id == 0)
            {
                _baseStruct = new BaseCoreStruct
                                  {
                                      Id = _id,
                                      Guid = _guid,
                                      DatabaseId = _databaseId,
                                      DbSourceId = _dbSourceId,
                                      StateId = StateId
                };
                return true;
            }
            return false;
        }
        /// <summary>Востановить текущее состояние объекта</summary>
        /// <remarks>Востановление состояние возможно только после выполнения сосхранения состояния</remarks>
        public override void RestoreState()
        {
            Id = _baseStruct.Id;
            Guid = _baseStruct.Guid;
            DatabaseId = _baseStruct.DatabaseId;
            DbSourceId = _baseStruct.DbSourceId;
            StateId = _baseStruct.StateId;
            
            IsChanged = false;
        }
        #endregion
        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public virtual void Validate()
        {
            if (_databaseId == 0)
                _databaseId = Workarea.MyBranche.Id;
        }
        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected virtual void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Id, SqlDbType.Int) { IsNullable = true };
            if (Id == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = Id;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Guid, SqlDbType.UniqueIdentifier) { IsNullable = true };
            if (Guid == Guid.Empty)
                prm.Value = DBNull.Value;
            else
                prm.Value = Guid;
            sqlCmd.Parameters.Add(prm);


            prm = new SqlParameter(GlobalSqlParamNames.DatabaseId, SqlDbType.Int) { IsNullable = false, Value = DatabaseId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DbSourceId, SqlDbType.Int) { IsNullable = true};
            if (DbSourceId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = DbSourceId;
            sqlCmd.Parameters.Add(prm);

            if (!insertCommand && validateVersion)
            {
                prm = new SqlParameter(GlobalSqlParamNames.Version, SqlDbType.Binary, 8) { IsNullable = true };

                if (_objectVersion == null || _objectVersion.All(v => v == 0))
                    prm.Value = DBNull.Value;
                else
                    prm.Value = _objectVersion;
                sqlCmd.Parameters.Add(prm);
            }
            
            prm = new SqlParameter(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128)
                      {
                          IsNullable = true,
                          Value = DBNull.Value
                      };
            if(Workarea.IsWebApplication)
            {
                prm.Value = UserName;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateModified, SqlDbType.DateTime)
                      {
                          IsNullable = true,
                          Value = DBNull.Value
                      };
            // TODO: Предусмотреть установку даты изменения
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.StateId, SqlDbType.Int) { IsNullable = false, Value = _stateId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Flags, SqlDbType.Int) { IsNullable = false, Value = _flagsValue };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            sqlCmd.Parameters.Add(prm);
        }

        void ICoreObject.Load(SqlDataReader reader, bool endInit)
        {
            Load(reader, endInit);
        }
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public virtual void Load(SqlDataReader reader, bool endInit=true)
        {
            OnBeginInit();
            try
            {
                _id = reader.GetInt32(0);
                _guid = reader.GetGuid(1);
                _databaseId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                _dbSourceId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                _objectVersion = reader.GetSqlBinary(4).Value;
                _userName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                if (reader.IsDBNull(6))
                    _dateModified = null;
                else
                    _dateModified = reader.GetDateTime(6);
                _flagsValue = reader.GetInt32(7);
                _stateId = reader.GetInt32(8);
                _lastLoad = DateTime.Now;
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex); 
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Окончание инициализации</summary>
        /// <remarks>Метод вызывается после загрузки данных из базы данных</remarks>
        protected override void OnEndInit()
        {
            IsNew = false;
            
            base.OnEndInit();
            
        }
        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "Load"</remarks>
        /// <param name="value">Идентификатор</param>
        public virtual void Load(int value)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnLoading(e);
            if (e.Cancel)
                return;
            Load(value, FindProcedure(GlobalMethodAlias.Load));
            OnLoadEnd();
        }
        /// <summary>Загрузить экземпляр из базы данных по его глобальному идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "LoadByGuid"</remarks>
        /// <param name="value">Идентификатор</param>
        public virtual void Load(Guid value)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnLoading(e);
            if (e.Cancel)
                return;
            Load(value, FindProcedure(GlobalMethodAlias.LoadGuid));
            OnLoadEnd();
        }
        /// <summary>
        /// Обновление данных из базы данных
        /// </summary>
        public virtual void Refresh(bool all)
        {
            if (Id!=0)
                Load(Id);
        }
        /// <summary>
        /// Загрузка данных по идентификатору
        /// </summary>
        /// <param name="procedureName">Хранимая процедура</param>
        protected virtual void Load(string procedureName)
        {
            Load(Id, procedureName);
        }
        void ICoreObject.Load(string procedureName)
        {
            Load(procedureName);
        }
        /// <summary>Поиск метода</summary>
        /// <param name="metodAliasName">Псевдоним</param>
        /// <returns></returns>
        protected internal virtual string FindProcedure(string metodAliasName)
        {
            return Entity.FindMethod(metodAliasName).FullName;
        }

        string ICoreObject.FindProcedure(string metodAliasName)
        {
            return FindProcedure(metodAliasName);
        }

        /// <summary>Обновление объекта в базе данных</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "Update"</remarks>
        /// <seealso cref="Create()"/>
        /// <seealso cref="Load(int)"/>
        /// <seealso cref="Validate()"/>
        protected virtual void Update(bool versionControl = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnUpdating(e);
            if (e.Cancel)
                return;
            Update(FindProcedure(GlobalMethodAlias.Update), versionControl);
            OnUpdated();
        }
        protected virtual void Update(SqlTransaction trans, bool versionControl = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnUpdating(e);
            if (e.Cancel)
                return;
            Update(FindProcedure(GlobalMethodAlias.Update), versionControl, trans);
            OnUpdated();
        }
        public virtual void CanDelete()
        {
            if (IsStateDeny)
                new ValidateException("Объект не может быть удален, поскольку находится в состоянии Запрешен к использованию");
            if((FlagsValue & FlagValue.FLAGREADONLY)== FlagValue.FLAGREADONLY)
                new ValidateException("Объект не может быть удален, поскольку имеет аттрибут Только чтение");
            if ((FlagsValue & FlagValue.FLAGSYSTEM) == FlagValue.FLAGSYSTEM)
                new ValidateException("Объект не может быть удален, поскольку имеет аттрибут Системный");
            if ((FlagsValue & FlagValue.FLAGHIDEN) == FlagValue.FLAGHIDEN)
                new ValidateException("Объект не может быть удален, поскольку имеет аттрибут Скрытый");
        }
        /// <summary>Проверяет возможность удаления сущности из базы данных</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "CanDelete"</remarks>
        public virtual bool CanDeleteFromDataBase()
        {
            bool res;
            using (SqlConnection cnn = (this as ICoreObject).Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return false;
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = FindProcedure(GlobalMethodAlias.CanDelete);
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                        sqlCmd.Parameters.Add(prm);
                        sqlCmd.ExecuteNonQuery();
                        int code = (int)sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                        res = (code == 0);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            return res;
        }
        ///// <summary>Загрузить экземпляр сущности из базы данных по его глобальному идентификатору</summary>
        ///// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "LoadGuid"</remarks>
        ///// <param name="value">Глобальный идентификатор</param>
        //public virtual void Load(Guid value)
        //{
        //    using (SqlConnection cnn = (this as ICoreObject).Workarea.GetDatabaseConnection())
        //    {
        //        if (cnn == null) return;

        //        int id;
        //        try
        //        {
        //            using (SqlCommand sqlCmd = cnn.CreateCommand())
        //            {
        //                sqlCmd.CommandType = CommandType.StoredProcedure;
        //                sqlCmd.CommandText = FindProcedure(GlobalMethodAlias.LoadGuid);
        //                sqlCmd.Parameters.Add(GlobalSqlParamNames.Guid, SqlDbType.UniqueIdentifier).Value = value;
        //                SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
        //                sqlCmd.Parameters.Add(prm);
        //                sqlCmd.ExecuteNonQuery();
        //                id = (int)sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
        //            }
        //        }
        //        finally
        //        {
        //            if (cnn.State != ConnectionState.Closed)
        //                cnn.Close();
        //        }

        //        // Загрузить через value
        //        if (id != 0)
        //            Load(id);
        //    }
        //}
        /// <summary>
        /// Загрузить данные из базы данных
        /// </summary>
        /// <param name="value">Идентификатор</param>
        /// <param name="procedureName">Хранимая процедура</param>
        protected virtual void Load(int value, string procedureName)
        {
            if (value == 0)
                return;
            OnBeginInit();
            using (SqlConnection cnn = (this as ICoreObject).Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value;
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.Read() && reader.HasRows)
                        {
                            Load(reader);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                    OnEndInit();
                }
            }
        }
        /// <summary>
        /// Загрузить данные из базы данных по глобальному идентификатору
        /// </summary>
        /// <param name="value">Глобальный идентификатор</param>
        /// <param name="procedureName">Хранимая процедура</param>
        protected virtual void Load(Guid value, string procedureName)
        {
            if (value.Equals(Guid.Empty))
                return;
            OnBeginInit();
            using (SqlConnection cnn = (this as ICoreObject).Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Guid, SqlDbType.UniqueIdentifier).Value = value;
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.Read() && reader.HasRows)
                        {
                            Load(reader);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                    OnEndInit();
                }
            }
        }
        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="procedureName">Хранимая процедура</param>
        /// <param name="versionControl">Использовать контроль версии объекта</param>
        protected virtual void Update(string procedureName, bool versionControl)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        SetParametersToInsertUpdate(sqlCmd, false, versionControl);

                        if (sqlCmd.Connection.State != ConnectionState.Open)
                            sqlCmd.Connection.Open();
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                Load(reader);
                            }
                            reader.Close();
                            _lastLoadPartial = null;
                            object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                            if (retval == null)
                                throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                            if ((int)retval != 0)
                                throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        }
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }

        protected virtual void Update(string procedureName, bool versionControl, SqlTransaction trans)
        {
            SqlConnection cnn = trans.Connection;
            
                if (cnn == null) return;
                
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        sqlCmd.Transaction = trans;
                        SetParametersToInsertUpdate(sqlCmd, false, versionControl);

                        if (sqlCmd.Connection.State != ConnectionState.Open)
                            sqlCmd.Connection.Open();
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                Load(reader);
                            }
                            reader.Close();
                            _lastLoadPartial = null;
                            object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                            if (retval == null)
                                throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                            if ((int)retval != 0)
                                throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        }
                    }
                
                
            
        }
        /// <summary>
        /// Создать
        /// </summary>
        /// <param name="procedureName">Хранимая процедура</param>
        protected virtual void Create(string procedureName)
        {
            using (SqlConnection cnn = (this as ICoreObject).Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        SetParametersToInsertUpdate(sqlCmd, true);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Load(reader);
                        }
                        reader.Close();

                        object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));
                        if ((Int32)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }

        protected virtual void Create(string procedureName, SqlTransaction trans)
        {
            SqlConnection cnn = trans.Connection;
            
                if (cnn == null) return;

                
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        sqlCmd.Transaction = trans;
                        SetParametersToInsertUpdate(sqlCmd, true);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Load(reader);
                        }
                        reader.Close();

                        object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));
                        if ((Int32)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);
                    }
                
                
            
        }

        /// <summary>Представление сущности в виде форматированной строки</summary>
        /// <remarks>Допустимая маска :
        /// <list type="bullet">
        /// <item><term>%id%</term>
        /// <description>Идентификатор<see cref="Id"/></description>
        /// </item>
        /// <item><term>%guid%</term>
        /// <description>Глобальный идентификатор<see cref="Guid"/></description>
        /// </item>
        /// <item><term>%databaseid%</term>
        /// <description>Идентификатор базы данных<see cref="DatabaseId"/></description>
        /// </item>
        /// <item><term>%dbsourceid%</term>
        /// <description>Идентификатор в базе источнике<see cref="DbSourceId"/></description>
        /// </item>
        /// <item><term>%version%</term>
        /// <description>Версия<see cref="GetVersion()"/></description>
        /// </item>
        /// <item><term>%flags%</term>
        /// <description>Флаги<see cref="GetVersion()"/></description>
        /// </item>
        /// </list>
        /// </remarks>
        public virtual string ToString(string mask)
        {
            if (string.IsNullOrEmpty(mask))
            {
                return ToString();
            }
            string res = mask;
            // Макроподстановка для id
            res = res.Replace("%id%", Id.ToString());
            // Макроподстановка для guid
            res = res.Replace("%guid%", Guid.ToString());
            // Макроподстановка для databaseid
            res = res.Replace("%databaseid%", DatabaseId.ToString());
            // Макроподстановка для dbsourceid
            res = res.Replace("%dbsourceid%", DbSourceId.ToString());
            // Макроподстановка для версии
            res = res.Replace("%version%", Hex.ToHexString("0x", ObjectVersion));
            // Макроподстановка для флагов
            res = res.Replace("%flags%", _flagsValue.ToString());
            return res;
        }
        public void Save()
        {
            Save(true);
        }

        public void Save(SqlTransaction trans)
        {
            Save(trans, true);
        }

        protected virtual void Save(bool endSave=true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnSaving(e);
            if (e.Cancel)
                return;
            Validate();
            if (IsNew)
                Create();
            else
                Update();

            _baseStruct = new BaseCoreStruct();
            if(endSave)
                OnSaved();
        }
        protected virtual void Save(SqlTransaction trans, bool endSave = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnSaving(e);
            if (e.Cancel)
                return;
            Validate();
            if (IsNew)
                Create(trans);
            else
                Update(trans);

            _baseStruct = new BaseCoreStruct();
            if (endSave)
                OnSaved();
        }
        /// <summary>Удаление из базы данных</summary>
        /// <remarks>Удаление выполняется без каких либо проверок. Для осуществления проверки используйте метод 
        /// <see cref="CanDeleteFromDataBase"/>.
        /// Метод использует хранимую процедуру указанную в методах объекта по ключу "Delete"
        /// </remarks>
        /// <param name="checkVersion">Выполнять проверку версий</param>
        public virtual void Delete(bool checkVersion=true)
        {
            CanDelete();
            Delete(FindProcedure(GlobalMethodAlias.Delete), checkVersion);
            // TODO: Добавить процедуры CANDELETE
            //if (CanDeleteFromDataBase())
            //    Delete(FindProcedure(GlobalMethodAlias.Delete), checkVersion);
            //else
            //    throw new ValidateException("Объект не может быть удален на основе проверки целосности данных в базе данных!");
        }

        protected virtual void Delete(string procedureName, bool checkVersion = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnDeleting(e);
            if (e.Cancel)
                return;
            using (SqlConnection cnn = (this as ICoreObject).Workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new DatabaseException("Отсутствует соединение");
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;//FindProcedure(GlobalMethodAlias.Delete);
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        if (checkVersion)
                            sqlCmd.Parameters.Add(GlobalSqlParamNames.Version, SqlDbType.Binary, 8).Value = GetVersion();
                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                        sqlCmd.Parameters.Add(prm);
                        sqlCmd.ExecuteNonQuery();
                        object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((int)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        else
                        {
                            Workarea.TryRemoveFromCasheCollection(this);
                        }
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            OnDeleted();
        }

        /// <summary>
        /// Изменить состояние объекта
        /// </summary>
        /// <param name="stateValue">Идентификатор нового состояния</param>
        public virtual void ChangeState(int stateValue)
        {
            if (stateValue < 0 && stateValue > 5)
                throw new ArgumentOutOfRangeException("stateValue", "Новое состояние объекта не может быть меньше 0 или больше 5");
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = string.Empty;
                        if (EntityId != 0)
                        {
                            procedureName = FindProcedure(GlobalMethodAlias.ChangeState);
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateValue;
                        cmd.ExecuteNonQuery();
                        StateId = stateValue;
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /// <summary>
        /// Пометить объект как удаленный
        /// </summary>
        public void Remove()
        {
            if (IsSystem)
                throw new ValidateException("Системный объект не может быть удален");
            if (IsReadOnly)
                throw new ValidateException("В состоянии \"Только чтение\" нельзя удалять");
            ChangeState(State.STATEDELETED);
        }
        /// <summary>Создание объекта в базе данных</summary>
        /// <remarks>Перед созданием объекта не выполняется проверка <see cref="Validate()"/>.
        /// Метод использует хранимую процедуру указанную в методах объекта по ключу "Create".
        /// </remarks>
        /// <seealso cref="Load(int)"/>
        /// <seealso cref="Validate()"/>
        /// <seealso cref="Update(bool)"/>
        protected virtual void Create()
        {
            CancelEventArgs e = new CancelEventArgs();
            OnCreating(e);
            if (e.Cancel)
                return;
            Create(FindProcedure(GlobalMethodAlias.Create));
            OnCreated();
        }

        protected virtual void Create(SqlTransaction trans)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnCreating(e);
            if (e.Cancel)
                return;
            Create(FindProcedure(GlobalMethodAlias.Create), trans);
            OnCreated();
        }
        /// <summary>
        /// Поиск идентификатора объекта в базе данных по глобальному идентификатору
        /// </summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "FindIdByGuid"</remarks>
        /// <returns>Идентификатор найденного объекта или 0 если объект не найден</returns>
        /// <seealso cref="ExistsGuids(DataTable,string)"/>
        /// <seealso cref="ExistsGuids(IEnumerable{Guid})"/>
        public int ExistsGuids(Guid value)
        {
            int res = 0;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = FindProcedure(GlobalMethodAlias.FindIdByGuid);

                    cmd.Parameters.Add(new SqlParameter { ParameterName = GlobalSqlParamNames.Guid, SqlDbType = SqlDbType.UniqueIdentifier, Value = value });

                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    object val = cmd.ExecuteScalar();
                    if (val != null)
                        res = (int)val;
                }

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return res;
        }
        /// <summary>
        /// Поиск объектов в БД по Guid
        /// </summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "ExistsGuids"</remarks>
        /// <param name="table">Таблица</param>
        /// <param name="fieldName">Наименование столбца, по умолчанию "Guid"</param>
        /// <returns>Словарь содержащий глобальные идентификаторы и соответствующие им идентификаторы объектов. 
        /// Если значение идентификатора равно 0 - значит он отсутствует в базе данных</returns>
        /// <seealso cref="ExistsGuids(Guid)"/>
        /// <seealso cref="ExistsGuids(IEnumerable{Guid})"/>
        public Dictionary<Guid, int> ExistsGuids(DataTable table, string fieldName = "Guid")
        {
            List<Guid> values = (from DataRow row in table.Rows select (Guid)row[fieldName]).ToList();
            return ExistsGuids(values);
        }

        /// <summary>
        /// Поиск идентификаторов объектов в базе данных по глобальным идегтификаторам
        /// </summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "ExistsGuids"</remarks>
        /// <param name="guids">Глобальные идентификаторы</param>
        /// <returns>Словарь содержащий глобальные идентификаторы и соответствующие им идентификаторы объектов. 
        /// Если значение идентификатора равно 0 - значит он отсутствует в базе данных</returns>
        /// <seealso cref="ExistsGuids(DataTable,string)"/>
        /// <seealso cref="ExistsGuids(Guid)"/>
        public Dictionary<Guid, int> ExistsGuids(IEnumerable<Guid> guids)
        {
            Dictionary<Guid, int> ret = new Dictionary<Guid, int>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = FindProcedure(GlobalMethodAlias.ExistsGuids);

                    DatabaseHelper.AddTvpParamKeyListGuid(cmd, guids);
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(reader.GetGuid(0), reader.GetInt32(1));
                    }
                }

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return ret;
        }
        #region IDataErrorInfo Members

        [NonSerialized]
        private Dictionary<string, string> _errors = new Dictionary<string, string>();


        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
        }

        [XmlIgnore]
        public Dictionary<string, string> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return _errors.ContainsKey(columnName) ? _errors[columnName] : String.Empty;
            }
        }
        public void ClearErrors()
        {
            _errors.Clear();
        }

        #endregion
    }
}