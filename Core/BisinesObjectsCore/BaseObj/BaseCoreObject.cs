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
    /// <summary>��������� ������������ ��� ���������� �������</summary>
    internal struct BaseCoreStruct
    {
        /// <summary>�������������</summary>
        public int Id;
        /// <summary>���������� �������������</summary>
        public Guid Guid;
        /// <summary>������������� � ���� ���������</summary>
        public int DbSourceId;
        /// <summary>������������� ���������</summary>
        public int DatabaseId;
        /// <summary>������������� ���������</summary>
        public int StateId;
        /// <summary>���� �������� ��� ���������� ��������� ������</summary>
        public DateTime? DateModified;
        /// <summary>������������� ���������� ����</summary>
        public short EntityId;
        /// <summary>����� ������</summary>
        public int FlagsValue;
        /// <summary>��� ������������ ���������� ��� ����������� ������</summary>
        public string UserName;
    }

    /// <summary>������� ����� ������ ��������</summary>
    //[Serializable]
    [DataContract(Namespace="http://atlan.com.ua/BusinessObjects")]
    public abstract class BaseCoreObject : BasePropertyChangeSupport, ICoreObject, IDataErrorInfo, ISerializable, IXmlSerializable 
    {
        #region ������� ����������
        [NonSerialized]
        private CancelEventHandler _savingHandlers;
        /// <summary>
        /// ������� ������ ����������
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
        /// ������� ����� ����������
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
        /// ����� ���������� ��������������� ����� ����������
        /// </summary>
        protected virtual void OnSaved()
        {
            OnSaved(new EventArgs());
        }
        /// <summary>
        /// ����� ���������� ��������������� ����� ����������
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSaved(EventArgs e)
        {
            if (_savedHandlers != null)
                _savedHandlers.Invoke(this, e);
        }
        /// <summary>
        /// ����� ���������� ��������������� ����� ����������
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSaving(CancelEventArgs e)
        {
            if (_savingHandlers != null)
                _savingHandlers.Invoke(this, e);
        }
        #endregion
        #region ������� ��������
        [NonSerialized]
        private CancelEventHandler _loadingHandlers;
        /// <summary>
        /// ������� ������ ��������
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
        /// ������� ����� ��������
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
        /// ����� ���������� ������������� ����� �������� �� ���� ������
        /// </summary>
        protected virtual void OnLoadEnd()
        {
            OnLoadEnd(new EventArgs());
        }
        /// <summary>
        /// ����� ���������� ������������� ����� �������� �� ���� ������
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLoadEnd(EventArgs e)
        {
            if (_loadHandlers != null)
                _loadHandlers.Invoke(this, e);
        }
        /// <summary>
        /// ����� ���������� ������������� ����� ��������� �� ���� ������
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLoading(CancelEventArgs e)
        {
            if (_loadingHandlers != null)
                _loadingHandlers.Invoke(this, e);
        }
        #endregion
        #region ������� ��������
        [NonSerialized]
        private CancelEventHandler _deletingHandlers;
        /// <summary>
        /// ������� ������ ��������
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
        /// ������� ����� ��������
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
        /// ����� ���������� ��������������� ����� ��������
        /// </summary>
        protected virtual void OnDeleted()
        {
            OnDeleted(new EventArgs());
        }
        /// <summary>
        /// ����� ���������� ��������������� ����� ��������
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDeleted(EventArgs e)
        {
            if (_deletedHandlers != null)
                _deletedHandlers.Invoke(this, e);
        }
        /// <summary>
        /// ����� ���������� ��������������� ����� ��������
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDeleting(CancelEventArgs e)
        {
            if (_deletingHandlers != null)
                _deletingHandlers.Invoke(this, e);
        }
        #endregion

        #region ������� ��������
        [NonSerialized]
        private CancelEventHandler _creatingHandlers;
        /// <summary>
        /// ������� ������ ��������
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
        /// ������� ����� ��������
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
        /// ����� ���������� ������������� ����� �������� ������ � ���� ������
        /// </summary>
        protected virtual void OnCreated()
        {
            OnCreated(new EventArgs());
            this.Workarea.OnCreatedCoreObject(this);
        }
        /// <summary>
        /// ����� ���������� ������������� ����� �������� ������ � ���� ������
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCreated(EventArgs e)
        {
            if (_createdHandlers != null)
                _createdHandlers.Invoke(this, e);
        }
        /// <summary>
        /// ����� ���������� ������������� ����� �������� ������ � ���� ������
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCreating(CancelEventArgs e)
        {
            if (_creatingHandlers != null)
                _creatingHandlers.Invoke(this, e);
        }
        #endregion

        #region ������� ����������
        [NonSerialized]
        private CancelEventHandler _updatingHandlers;
        /// <summary>
        /// ������� ������ ����������
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
        /// ������� ����� ����������
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
        /// ����� ���������� ������������� ����� ���������� ������ � ���� ������
        /// </summary>
        protected virtual void OnUpdated()
        {
            OnUpdated(new EventArgs());
        }
        /// <summary>
        /// ����� ���������� ������������� ����� ���������� ������ � ���� ������
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUpdated(EventArgs e)
        {
            if (_updatedHandlers != null)
                _updatedHandlers.Invoke(this, e);
        }
        /// <summary>
        /// ����� ���������� ������������� ����� ���������� ������ � ���� ������
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUpdating(CancelEventArgs e)
        {
            if (_updatingHandlers != null)
                _updatingHandlers.Invoke(this, e);
        }
        #endregion

        #region ������� ��������
        [NonSerialized]
        private CancelEventHandler _validatingHandlers;
        /// <summary>
        /// ������� ������ ��������
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
        /// ������� ����� ��������
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

        #region ������������
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
        /// ������ ������ �� XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
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
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
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
        /// ������ ������ � XML
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(GetType().Name.ToString());
            WritePartialXml(writer);
            writer.WriteEndElement();
        }
        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
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
        /// <summary>�����������</summary>
        protected BaseCoreObject(): base()
        {
            _isNew = true;
        }

        /// <summary>
        /// ��������� ������� ��� ������ ������ �������
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
        /// <summary>��� ��������</summary>
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
        /// <summary>������������� ���������� ����</summary>
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
        
        #region ��������
        [NonSerialized]
        private Workarea _iWorkarea;
        /// <summary>������� �������</summary>
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
        /// <summary>�������������</summary>
        [Description("�������������"),
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
        /// <summary>������������� � ���� ���������</summary>
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
        /// <summary>������������� � ���� ���������</summary>
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
        /// <summary>������������� ���������</summary>
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
        /// <summary>������������� ���������</summary>
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
        /// <summary>���������� �������������</summary>
        [Description("���������� �������������"), DataMember]
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
        /// <summary>������</summary>
        /// <returns></returns>
        public virtual byte[] GetVersion()
        {
            if (_objectVersion == null)
                return null;
            return (byte[])_objectVersion.Clone();
        }
        private byte[] _objectVersion;
        /// <summary>������</summary>
        [Description("������")]
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
        /// <summary>����� ������</summary>
        [Description("����� ������"), 
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
        /// �������� ������ � ���� ������
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
        /// <summary>������������� ���������</summary>
        [Description("������������� ���������"),
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
        /// <summary>��������� �� ���������</summary>
        public bool IsStateDefault { get { return _stateId == State.STATEDEFAULT; } }
        /// <summary>��������� "��������"</summary>
        public bool IsStateActive { get { return _stateId == State.STATEACTIVE; } }
        /// <summary>��������� "������"</summary>
        public bool IsStateDeleted { get { return _stateId == State.STATEDELETED; } }
        /// <summary>��������� "��������"</summary>
        public bool IsStateDeny{ get { return _stateId == State.STATEDENY; } }
        /// <summary>��������� "��������"</summary>
        public bool IsStateAllow { get { return (_stateId == State.STATEACTIVE) || (_stateId == State.STATEDEFAULT); } }
        /// <summary>���� "������ ������"</summary>
        public bool IsReadOnly { get { return (FlagsValue & FlagValue.FLAGREADONLY) == FlagValue.FLAGREADONLY; } }
        /// <summary>���� "�������"</summary>
        public bool IsHiden { get { return (FlagsValue & FlagValue.FLAGHIDEN) == FlagValue.FLAGHIDEN; } }
        /// <summary>���� "���������"</summary>
        public bool IsSystem { get { return (FlagsValue & FlagValue.FLAGSYSTEM) == FlagValue.FLAGSYSTEM; } }
        /// <summary>���� "������"</summary>
        public bool IsTemplate { get { return (FlagsValue & FlagValue.FLAGTEMPLATE) == FlagValue.FLAGTEMPLATE; } }
        /// <summary>���� "�������������"</summary>
        public bool IsNoupdate { get { return (FlagsValue & FlagValue.EXCNOUPDATE) == FlagValue.EXCNOUPDATE; } }
        /// <summary>���� "��������"</summary>
        public bool IsArhive { get { return (FlagsValue & FlagValue.FLAGARHIVE) == FlagValue.FLAGARHIVE; } }

        /// <summary>����������-�������� ���� ��� �������</summary>
        /// <param name="value">��������</param>
        public virtual void SetFlagValue(int value)
        {
            if (!((FlagsValue & value) == value))
                FlagsValue = FlagsValue + value;
        }
        /// <summary>����������-�������� ���� ��� �������</summary>
        /// <param name="value">��������</param>
        public virtual void RemoveFlagValue(int value)
        {
            if (((FlagsValue & value) == value))
                FlagsValue = FlagsValue - value;
        }
        /// <summary>�������� ���� "������ ������"</summary>
        public void SetFlagReadOnly()
        {
            SetFlagValue(FlagValue.FLAGREADONLY);
        }
        /// <summary>������� ���� "������ ������"</summary>
        public void RemoveFlagReadOnly()
        {
            RemoveFlagValue(FlagValue.FLAGREADONLY);
        }
        /// <summary>�������� ���� "�������"</summary>
        public void SetFlagHidden()
        {
            SetFlagValue(FlagValue.FLAGHIDEN);
        }
        /// <summary>������� ���� "�������"</summary>
        public void RemoveFlagHidden()
        {
            RemoveFlagValue(FlagValue.FLAGHIDEN);
        }
        /// <summary>�������� ���� "���������"</summary>
        public void SetFlagSystem()
        {
            SetFlagValue(FlagValue.FLAGSYSTEM);
        }
        /// <summary>������� ���� "���������"</summary>
        public void RemoveFlagSystem()
        {
            RemoveFlagValue(FlagValue.FLAGSYSTEM);
        }
        /// <summary>�������� ���� "������"</summary>
        public void SetFlagTemplate()
        {
            SetFlagValue(FlagValue.FLAGTEMPLATE);
        }
        /// <summary>������� ���� "������"</summary>
        public void RemoveFlagTemplate()
        {
            RemoveFlagValue(FlagValue.FLAGTEMPLATE);
        }
        /// <summary>�������� ���� "�������������"</summary>
        public void SetFlagNoupdate()
        {
            SetFlagValue(FlagValue.EXCNOUPDATE);
        }
        /// <summary>������� ���� "�������������"</summary>
        public void RemoveFlagNoupdate()
        {
            RemoveFlagValue(FlagValue.EXCNOUPDATE);
        }
        /// <summary>�������� ���� "��������"</summary>
        public void SetFlagArhive()
        {
            SetFlagValue(FlagValue.FLAGARHIVE);
        }
        /// <summary>������� ���� "��������"</summary>
        public void RemoveFlagArhive()
        {
            RemoveFlagValue(FlagValue.FLAGARHIVE);
        }
        private State _state;
        /// <summary>���������</summary>
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
        /// ��� ������������ ���������� ��� ����������� ������
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
        /// ���� �������� ��� ���������� ��������� ������
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
        /// <summary>�������� �� ������ �����</summary>
        [Description("�������� �� ������ �����"), Browsable(false)]
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
        /// <summary>���� ��������� �������� ������� �� ���� ������</summary>
        /// <remarks>������������ ��� ��������������� ���������� ������ ������� �� ���� ������</remarks>
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
        /// ���� � ����� ��������� ��������� �������� �������
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

        #region ���������
        BaseCoreStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
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
        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
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
        /// �������� ������������ ������� ��������� �����������
        /// </summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public virtual void Validate()
        {
            if (_databaseId == 0)
                _databaseId = Workarea.MyBranche.Id;
        }
        /// <summary>���������� �������� ���������� ��� �������� �������� ��� ����������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ����������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
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
            // TODO: ������������� ��������� ���� ���������
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
        /// <summary>�������� ������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">������� ��������� ��������</param>
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
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex); 
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>��������� �������������</summary>
        /// <remarks>����� ���������� ����� �������� ������ �� ���� ������</remarks>
        protected override void OnEndInit()
        {
            IsNew = false;
            
            base.OnEndInit();
            
        }
        /// <summary>��������� ��������� �� ���� ������ �� ��� ��������������</summary>
        /// <remarks>����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "Load"</remarks>
        /// <param name="value">�������������</param>
        public virtual void Load(int value)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnLoading(e);
            if (e.Cancel)
                return;
            Load(value, FindProcedure(GlobalMethodAlias.Load));
            OnLoadEnd();
        }
        /// <summary>��������� ��������� �� ���� ������ �� ��� ����������� ��������������</summary>
        /// <remarks>����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "LoadByGuid"</remarks>
        /// <param name="value">�������������</param>
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
        /// ���������� ������ �� ���� ������
        /// </summary>
        public virtual void Refresh(bool all)
        {
            if (Id!=0)
                Load(Id);
        }
        /// <summary>
        /// �������� ������ �� ��������������
        /// </summary>
        /// <param name="procedureName">�������� ���������</param>
        protected virtual void Load(string procedureName)
        {
            Load(Id, procedureName);
        }
        void ICoreObject.Load(string procedureName)
        {
            Load(procedureName);
        }
        /// <summary>����� ������</summary>
        /// <param name="metodAliasName">���������</param>
        /// <returns></returns>
        protected internal virtual string FindProcedure(string metodAliasName)
        {
            return Entity.FindMethod(metodAliasName).FullName;
        }

        string ICoreObject.FindProcedure(string metodAliasName)
        {
            return FindProcedure(metodAliasName);
        }

        /// <summary>���������� ������� � ���� ������</summary>
        /// <remarks>����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "Update"</remarks>
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
                new ValidateException("������ �� ����� ���� ������, ��������� ��������� � ��������� �������� � �������������");
            if((FlagsValue & FlagValue.FLAGREADONLY)== FlagValue.FLAGREADONLY)
                new ValidateException("������ �� ����� ���� ������, ��������� ����� �������� ������ ������");
            if ((FlagsValue & FlagValue.FLAGSYSTEM) == FlagValue.FLAGSYSTEM)
                new ValidateException("������ �� ����� ���� ������, ��������� ����� �������� ���������");
            if ((FlagsValue & FlagValue.FLAGHIDEN) == FlagValue.FLAGHIDEN)
                new ValidateException("������ �� ����� ���� ������, ��������� ����� �������� �������");
        }
        /// <summary>��������� ����������� �������� �������� �� ���� ������</summary>
        /// <remarks>����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "CanDelete"</remarks>
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
        ///// <summary>��������� ��������� �������� �� ���� ������ �� ��� ����������� ��������������</summary>
        ///// <remarks>����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "LoadGuid"</remarks>
        ///// <param name="value">���������� �������������</param>
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

        //        // ��������� ����� value
        //        if (id != 0)
        //            Load(id);
        //    }
        //}
        /// <summary>
        /// ��������� ������ �� ���� ������
        /// </summary>
        /// <param name="value">�������������</param>
        /// <param name="procedureName">�������� ���������</param>
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
        /// ��������� ������ �� ���� ������ �� ����������� ��������������
        /// </summary>
        /// <param name="value">���������� �������������</param>
        /// <param name="procedureName">�������� ���������</param>
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
        /// ��������
        /// </summary>
        /// <param name="procedureName">�������� ���������</param>
        /// <param name="versionControl">������������ �������� ������ �������</param>
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
        /// �������
        /// </summary>
        /// <param name="procedureName">�������� ���������</param>
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

        /// <summary>������������� �������� � ���� ��������������� ������</summary>
        /// <remarks>���������� ����� :
        /// <list type="bullet">
        /// <item><term>%id%</term>
        /// <description>�������������<see cref="Id"/></description>
        /// </item>
        /// <item><term>%guid%</term>
        /// <description>���������� �������������<see cref="Guid"/></description>
        /// </item>
        /// <item><term>%databaseid%</term>
        /// <description>������������� ���� ������<see cref="DatabaseId"/></description>
        /// </item>
        /// <item><term>%dbsourceid%</term>
        /// <description>������������� � ���� ���������<see cref="DbSourceId"/></description>
        /// </item>
        /// <item><term>%version%</term>
        /// <description>������<see cref="GetVersion()"/></description>
        /// </item>
        /// <item><term>%flags%</term>
        /// <description>�����<see cref="GetVersion()"/></description>
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
            // ���������������� ��� id
            res = res.Replace("%id%", Id.ToString());
            // ���������������� ��� guid
            res = res.Replace("%guid%", Guid.ToString());
            // ���������������� ��� databaseid
            res = res.Replace("%databaseid%", DatabaseId.ToString());
            // ���������������� ��� dbsourceid
            res = res.Replace("%dbsourceid%", DbSourceId.ToString());
            // ���������������� ��� ������
            res = res.Replace("%version%", Hex.ToHexString("0x", ObjectVersion));
            // ���������������� ��� ������
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
        /// <summary>�������� �� ���� ������</summary>
        /// <remarks>�������� ����������� ��� ����� ���� ��������. ��� ������������� �������� ����������� ����� 
        /// <see cref="CanDeleteFromDataBase"/>.
        /// ����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "Delete"
        /// </remarks>
        /// <param name="checkVersion">��������� �������� ������</param>
        public virtual void Delete(bool checkVersion=true)
        {
            CanDelete();
            Delete(FindProcedure(GlobalMethodAlias.Delete), checkVersion);
            // TODO: �������� ��������� CANDELETE
            //if (CanDeleteFromDataBase())
            //    Delete(FindProcedure(GlobalMethodAlias.Delete), checkVersion);
            //else
            //    throw new ValidateException("������ �� ����� ���� ������ �� ������ �������� ���������� ������ � ���� ������!");
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
                    throw new DatabaseException("����������� ����������");
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
        /// �������� ��������� �������
        /// </summary>
        /// <param name="stateValue">������������� ������ ���������</param>
        public virtual void ChangeState(int stateValue)
        {
            if (stateValue < 0 && stateValue > 5)
                throw new ArgumentOutOfRangeException("stateValue", "����� ��������� ������� �� ����� ���� ������ 0 ��� ������ 5");
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
        /// �������� ������ ��� ���������
        /// </summary>
        public void Remove()
        {
            if (IsSystem)
                throw new ValidateException("��������� ������ �� ����� ���� ������");
            if (IsReadOnly)
                throw new ValidateException("� ��������� \"������ ������\" ������ �������");
            ChangeState(State.STATEDELETED);
        }
        /// <summary>�������� ������� � ���� ������</summary>
        /// <remarks>����� ��������� ������� �� ����������� �������� <see cref="Validate()"/>.
        /// ����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "Create".
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
        /// ����� �������������� ������� � ���� ������ �� ����������� ��������������
        /// </summary>
        /// <remarks>����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "FindIdByGuid"</remarks>
        /// <returns>������������� ���������� ������� ��� 0 ���� ������ �� ������</returns>
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
        /// ����� �������� � �� �� Guid
        /// </summary>
        /// <remarks>����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "ExistsGuids"</remarks>
        /// <param name="table">�������</param>
        /// <param name="fieldName">������������ �������, �� ��������� "Guid"</param>
        /// <returns>������� ���������� ���������� �������������� � ��������������� �� �������������� ��������. 
        /// ���� �������� �������������� ����� 0 - ������ �� ����������� � ���� ������</returns>
        /// <seealso cref="ExistsGuids(Guid)"/>
        /// <seealso cref="ExistsGuids(IEnumerable{Guid})"/>
        public Dictionary<Guid, int> ExistsGuids(DataTable table, string fieldName = "Guid")
        {
            List<Guid> values = (from DataRow row in table.Rows select (Guid)row[fieldName]).ToList();
            return ExistsGuids(values);
        }

        /// <summary>
        /// ����� ��������������� �������� � ���� ������ �� ���������� ���������������
        /// </summary>
        /// <remarks>����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "ExistsGuids"</remarks>
        /// <param name="guids">���������� ��������������</param>
        /// <returns>������� ���������� ���������� �������������� � ��������������� �� �������������� ��������. 
        /// ���� �������� �������������� ����� 0 - ������ �� ����������� � ���� ������</returns>
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