using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace BusinessObjects.Documents
{
    /// <summary>Структура документа раздела "Маркетинг"</summary>
    internal struct BaseStructDocumentMktg
    {
        /// <summary>Идентификатор старшего менеджера</summary>
        public int SupervisorId;
        /// <summary>Идентификатор менеджера</summary>
        public int ManagerId;
        /// <summary>Идентификатор ценовой политики</summary>
        public int PrcNameId;
        /// <summary>Идентификатор расчетного счета отправителя</summary>
        public int BankAccFromId;
        /// <summary>Идентификатор расчетного счета получателя</summary>
        public int BankAccToId;
        /// <summary>Контактное лицо</summary>
        public string ContactName;
        /// <summary>Телефон контактного лица</summary>
        public string ContactPhone;
        /// <summary>Идентификатор должности контактного лица</summary>
        public int WorkPostId;
        /// <summary>Идентификатор "Метража" торговой точки</summary>
        public int MetricAreaId;
        /// <summary>Идентификатор типа торговой точки</summary>
        public int OutletTypeId;
        /// <summary>Идентификатор относительного расположения торговой точки</summary>
        public int OutletLocationId;
        /// <summary>Идентификатор графика работы торговой точки</summary>
        public int TimePeriodId;
        /// <summary>Идентификатор перерывов торговой точки</summary>
        public int BreakPeriodId;
    }
    /// <summary>
    /// Документ раздела "Маркетинг" 
    /// </summary>
    public class DocumentMktg : DocumentBase, IEditableObject, IChainsAdvancedList<Document, FileData>
    {
        /// <summary>Конструктор</summary>
        public DocumentMktg()
            : base()
        {
            EntityId = 14;
            _details = new List<DocumentDetailMktg>();
            _analitics = new List<DocumentAnalitic>();
        }
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Анкета клиента, соответствует значению 1</summary>
        public const int KINDVALUE_MRAK = 1;
        /// <summary>Настройки раздела, соответствует значению 100</summary>
        public const int KINDVALUE_CONFIG = 100;

        /// <summary>Анкета клиента, соответствует значению 917505</summary>
        public const int KINDID_MRAK = 917505;
        /// <summary>Настройки раздела, соответствует значению 917604</summary>
        public const int KINDID_CONFIG = 917604;
        // ReSharper restore InconsistentNaming
        #endregion

        public void CopyValue(DocumentMktg template)
        {
            this.PrcNameId = template.PrcNameId;
            this.OutletTypeId = template.OutletTypeId;
            this.OutletLocationId = template.OutletLocationId;
            this.MetricAreaId = template.MetricAreaId;
            this.ManagerId = template.ManagerId;
            this.SupervisorId = template.SupervisorId;
            this.WorkPostId = template.WorkPostId;
            this.TimePeriodId = template.TimePeriodId;
            this.BreakPeriodId = template.BreakPeriodId;
            foreach (DocumentAnalitic a in template.Analitics)
            {
                DocumentAnalitic row = NewAnaliticRow();
                row.AnaliticId = a.AnaliticId;
                row.GroupNo = a.GroupNo;
                row.GroupNo = a.GroupNo2;
                row.GroupNo3 = a.GroupNo3;
                row.GroupNo4 = a.GroupNo4;
                row.GroupNo5 = a.GroupNo5;

                row.IntValue1 = a.IntValue1;
                row.IntValue2 = a.IntValue2;
                row.IntValue3 = a.IntValue3;
                row.IntValue4 = a.IntValue4;
                row.IntValue5 = a.IntValue5;

                row.StringValue1 = a.StringValue1;
                row.StringValue2 = a.StringValue2;
                row.StringValue3 = a.StringValue3;
                row.StringValue4 = a.StringValue4;
                row.StringValue5 = a.StringValue5;

                row.SummValue1 = a.SummValue1;
                row.SummValue2 = a.SummValue2;
                row.SummValue3 = a.SummValue3;
                row.SummValue4 = a.SummValue4;
                row.SummValue5 = a.SummValue5;

                row.DateValue1 = a.DateValue1;
                row.DateValue2 = a.DateValue2;
                row.DateValue3 = a.DateValue3;
                row.DateValue4 = a.DateValue4;
                row.DateValue5 = a.DateValue5;
                row.Kind = a.Kind;
                row.StateId = a.StateId;
                row.Summa = row.Summa;
                row.Memo = a.Memo;
            }
        }
        #region Свойства

        private List<DocumentDetailMktg> _details;
        /// <summary>
        /// Детализация документа на уровне строк
        /// </summary>
        public List<DocumentDetailMktg> Details
        {
            get { return _details; }
            set { _details = value; }
        }


        private List<DocumentAnalitic> _analitics;
        /// <summary>
        /// Детализация документа на уровне аналитики
        /// </summary>
        public List<DocumentAnalitic> Analitics
        {
            get { return _analitics; }
            set { _analitics = value; }
        }


        private int _supervisorId;
        /// <summary>
        /// Идентификатор старшего менеджера
        /// </summary>
        public int SupervisorId
        {
            get { return _supervisorId; }
            set
            {
                if (value == _supervisorId) return;
                OnPropertyChanging(GlobalPropertyNames.SupervisorId);
                _supervisorId = value;
                OnPropertyChanged(GlobalPropertyNames.SupervisorId);
            }
        }

        private Agent _supervisor;
        /// <summary>
        /// Старший менеджер
        /// </summary>
        public Agent Supervisor
        {
            get
            {
                if (_supervisorId == 0)
                    return null;
                if (_supervisor == null)
                    _supervisor = Workarea.Cashe.GetCasheData<Agent>().Item(_supervisorId);
                else if (_supervisor.Id != _supervisorId)
                    _supervisor = Workarea.Cashe.GetCasheData<Agent>().Item(_supervisorId);
                return _supervisor;
            }
            set
            {
                if (_supervisor == value) return;
                OnPropertyChanging(GlobalPropertyNames.Supervisor);
                _supervisor = value;
                _supervisorId = _supervisor == null ? 0 : _supervisor.Id;
                OnPropertyChanged(GlobalPropertyNames.Supervisor);
            }
        }
        private int _managerId;
        /// <summary>
        /// Идентификатор менеджера
        /// </summary>
        public int ManagerId
        {
            get { return _managerId; }
            set
            {
                if (value == _managerId) return;
                OnPropertyChanging(GlobalPropertyNames.ManagerId);
                _managerId = value;
                OnPropertyChanged(GlobalPropertyNames.ManagerId);
            }
        }

        private Agent _manager;
        /// <summary>
        /// Менеджер
        /// </summary>
        public Agent Manager
        {
            get
            {
                if (_managerId == 0)
                    return null;
                if (_manager == null)
                    _manager = Workarea.Cashe.GetCasheData<Agent>().Item(_managerId);
                else if (_manager.Id != _managerId)
                    _manager = Workarea.Cashe.GetCasheData<Agent>().Item(_managerId);
                return _manager;
            }
            set
            {
                if (_manager == value) return;
                OnPropertyChanging(GlobalPropertyNames.Manager);
                _manager = value;
                _managerId = _manager == null ? 0 : _manager.Id;
                OnPropertyChanged(GlobalPropertyNames.Manager);
            }
        }

        private int _prcNameId;
        /// <summary>
        /// Идентификатор ценовой политики
        /// </summary>
        public int PrcNameId
        {
            get { return _prcNameId; }
            set
            {
                if (value == _prcNameId) return;
                OnPropertyChanging(GlobalPropertyNames.PrcNameId);
                _prcNameId = value;
                OnPropertyChanged(GlobalPropertyNames.PrcNameId);
            }
        }

        private PriceName _priceName;
        /// <summary>
        /// Ценовая политика 
        /// </summary>
        public PriceName PriceName
        {
            get
            {
                if (_prcNameId == 0)
                    return null;
                if (_priceName == null)
                    _priceName = Workarea.Cashe.GetCasheData<PriceName>().Item(_prcNameId);
                else if (_priceName.Id != _prcNameId)
                    _priceName = Workarea.Cashe.GetCasheData<PriceName>().Item(_prcNameId);
                return _priceName;
            }
            set
            {
                if (_priceName == value) return;
                OnPropertyChanging(GlobalPropertyNames.PriceName);
                _priceName = value;
                _prcNameId = _priceName == null ? 0 : _priceName.Id;
                OnPropertyChanged(GlobalPropertyNames.PriceName);
            }
        }
     
        
        private int _bankAccFromId; 
        /// <summary>
        /// Идентификатор расчетного счета отправителя
        /// </summary>
        public int BankAccFromId 
        { 
            get{ return _bankAccFromId; } 
            set
            {
               if (value == _bankAccFromId) return;
                OnPropertyChanging(GlobalPropertyNames.BankAccFromId);
                _bankAccFromId = value;
                OnPropertyChanged(GlobalPropertyNames.BankAccFromId);
            } 
        }


        private AgentBankAccount _bankAccFrom;
        /// <summary>
        /// Расчетный счет отправителя
        /// </summary>
        public AgentBankAccount BankAccFrom
        {
            get
            {
                if (_bankAccFromId == 0)
                    return null;
                if (_bankAccFrom == null)
                    _bankAccFrom = Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(_bankAccFromId);
                else if (_bankAccFrom.Id != _bankAccFromId)
                    _bankAccFrom = Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(_bankAccFromId);
                return _bankAccFrom;
            }
            set
            {
                if (_bankAccFrom == value) return;
                OnPropertyChanging(GlobalPropertyNames.BankAccFrom);
                _bankAccFrom = value;
                _bankAccFromId = _bankAccFrom == null ? 0 : _bankAccFrom.Id;
                OnPropertyChanged(GlobalPropertyNames.BankAccFrom);
            }
        }
        

        private int _bankAccToId;
        /// <summary>
        /// Идентификатор расчетного счета получателя
        /// </summary>
        public int BankAccToId
        {
            get { return _bankAccToId; }
            set
            {
                if (value == _bankAccToId) return;
                OnPropertyChanging(GlobalPropertyNames.BankAccToId);
                _bankAccToId = value;
                OnPropertyChanged(GlobalPropertyNames.BankAccToId);
            }
        }


        private AgentBankAccount _bankAccTo;
        /// <summary>
        /// Расчетный счет получателя
        /// </summary>
        public AgentBankAccount BankAccTo
        {
            get
            {
                if (_bankAccToId == 0)
                    return null;
                if (_bankAccTo == null)
                    _bankAccTo = Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(_bankAccToId);
                else if (_bankAccTo.Id != _bankAccToId)
                    _bankAccTo = Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(_bankAccToId);
                return _bankAccTo;
            }
            set
            {
                if (_bankAccTo == value) return;
                OnPropertyChanging(GlobalPropertyNames.BankAccTo);
                _bankAccTo = value;
                _bankAccToId = _bankAccTo == null ? 0 : _bankAccTo.Id;
                OnPropertyChanged(GlobalPropertyNames.BankAccTo);
            }
        }


        private string _contactName;
        /// <summary>
        /// Контактное лицо
        /// </summary>
        public string ContactName
        {
            get { return _contactName; }
            set
            {
                if (value == _contactName) return;
                OnPropertyChanging(GlobalPropertyNames.ContactName);
                _contactName = value;
                OnPropertyChanged(GlobalPropertyNames.ContactName);
            }
        }


        private string _contactPhone;
        /// <summary>
        /// Телефон контактного лица
        /// </summary>
        public string ContactPhone
        {
            get { return _contactPhone; }
            set
            {
                if (value == _contactPhone) return;
                OnPropertyChanging(GlobalPropertyNames.ContactPhone);
                _contactPhone = value;
                OnPropertyChanged(GlobalPropertyNames.ContactPhone);
            }
        }


        private int _workPostId;
        /// <summary>
        /// Идентификатор должности контактного лица
        /// </summary>
        public int WorkPostId
        {
            get { return _workPostId; }
            set
            {
                if (value == _workPostId) return;
                OnPropertyChanging(GlobalPropertyNames.WorkPostId);
                _workPostId = value;
                OnPropertyChanged(GlobalPropertyNames.WorkPostId);
            }
        }


        private Analitic _workPost;
        /// <summary>
        /// Должность контактного лица
        /// </summary>
        public Analitic WorkPost
        {
            get
            {
                if (_workPostId == 0)
                    return null;
                if (_workPost == null)
                    _workPost = Workarea.Cashe.GetCasheData<Analitic>().Item(_workPostId);
                else if (_workPost.Id != _workPostId)
                    _workPost = Workarea.Cashe.GetCasheData<Analitic>().Item(_workPostId);
                return _workPost;
            }
            set
            {
                if (_workPost == value) return;
                OnPropertyChanging("WorkPost");
                _workPost = value;
                _workPostId = _workPost == null ? 0 : _workPost.Id;
                OnPropertyChanged("WorkPost");
            }
        }
        


        private int _metricAreaId;
        /// <summary>
        /// Идентификатор "Метража" торговой точки
        /// </summary>
        public int MetricAreaId
        {
            get { return _metricAreaId; }
            set
            {
                if (value == _metricAreaId) return;
                OnPropertyChanging(GlobalPropertyNames.MetricAreaId);
                _metricAreaId = value;
                OnPropertyChanged(GlobalPropertyNames.MetricAreaId);
            }
        }


        private Analitic _metricArea;
        /// <summary>
        /// "Метраж торговой точки"
        /// </summary>
        public Analitic MetricArea
        {
            get
            {
                if (_metricAreaId == 0)
                    return null;
                if (_metricArea == null)
                    _metricArea = Workarea.Cashe.GetCasheData<Analitic>().Item(_metricAreaId);
                else if (_metricArea.Id != _metricAreaId)
                    _metricArea = Workarea.Cashe.GetCasheData<Analitic>().Item(_metricAreaId);
                return _metricArea;
            }
            set
            {
                if (_metricArea == value) return;
                OnPropertyChanging("MetricArea");
                _metricArea = value;
                _metricAreaId = _metricArea == null ? 0 : _metricArea.Id;
                OnPropertyChanged("MetricArea");
            }
        }
        


        private int _outletTypeId;
        /// <summary>
        /// Идентификатор типа торговой точки
        /// </summary>
        public int OutletTypeId
        {
            get { return _outletTypeId; }
            set
            {
                if (value == _outletTypeId) return;
                OnPropertyChanging(GlobalPropertyNames.OutletTypeId);
                _outletTypeId = value;
                OnPropertyChanged(GlobalPropertyNames.OutletTypeId);
            }
        }


        private int _outletLocationId;
        /// <summary>
        /// Идентификатор относительного расположения торговой точки
        /// </summary>
        public int OutletLocationId
        {
            get { return _outletLocationId; }
            set
            {
                if (value == _outletLocationId) return;
                OnPropertyChanging(GlobalPropertyNames.OutletLocationId);
                _outletLocationId = value;
                OnPropertyChanged(GlobalPropertyNames.OutletLocationId);
            }
        }



        private Analitic _outletLocation;
        /// <summary>
        /// Относительное расположение торговой точки
        /// </summary>
        public Analitic OutletLocation
        {
            get
            {
                if (_outletLocationId == 0)
                    return null;
                if (_outletLocation == null)
                    _outletLocation = Workarea.Cashe.GetCasheData<Analitic>().Item(_outletLocationId);
                else if (_outletLocation.Id != _outletLocationId)
                    _outletLocation = Workarea.Cashe.GetCasheData<Analitic>().Item(_outletLocationId);
                return _outletLocation;
            }
            set
            {
                if (_outletLocation == value) return;
                OnPropertyChanging("OutletLocation");
                _outletLocation = value;
                _outletLocationId = _outletLocation == null ? 0 : _outletLocation.Id;
                OnPropertyChanged("OutletLocation");
            }
        }
        


        private int _timePeriodId;
        /// <summary>
        /// Идентификатор графика работы торговой точки
        /// </summary>
        public int TimePeriodId
        {
            get { return _timePeriodId; }
            set
            {
                if (value == _timePeriodId) return;
                OnPropertyChanging(GlobalPropertyNames.TimePeriodId);
                _timePeriodId = value;
                OnPropertyChanged(GlobalPropertyNames.TimePeriodId);
            }
        }


        private TimePeriod _timePeriod;
        /// <summary>
        /// График работы торговой точки
        /// </summary>
        public TimePeriod TimePeriod
        {
            get
            {
                if (_timePeriodId == 0)
                    return null;
                if (_timePeriod == null)
                    _timePeriod = Workarea.Cashe.GetCasheData<TimePeriod>().Item(_timePeriodId);
                else if (_timePeriod.Id != _timePeriodId)
                    _timePeriod = Workarea.Cashe.GetCasheData<TimePeriod>().Item(_timePeriodId);
                return _timePeriod;
            }
            set
            {
                if (_timePeriod == value) return;
                OnPropertyChanging("TimePeriod");
                _timePeriod = value;
                _timePeriodId = _timePeriod == null ? 0 : _timePeriod.Id;
                OnPropertyChanged("TimePeriod");
            }
        }
        

        private int _breakPeriodId;
        /// <summary>
        /// Идентификатор перерывов торговой точки
        /// </summary>
        public int BreakPeriodId
        {
            get { return _breakPeriodId; }
            set
            {
                if (value == _breakPeriodId) return;
                OnPropertyChanging(GlobalPropertyNames.BreakPeriodId);
                _breakPeriodId = value;
                OnPropertyChanged(GlobalPropertyNames.BreakPeriodId);
            }
        }


        private TimePeriod _breakPeriod;
        /// <summary>
        /// График перерывов торговой точки
        /// </summary>
        public TimePeriod BreakPeriod
        {
            get
            {
                if (_breakPeriodId == 0)
                    return null;
                if (_breakPeriod == null)
                    _breakPeriod = Workarea.Cashe.GetCasheData<TimePeriod>().Item(_breakPeriodId);
                else if (_breakPeriod.Id != _breakPeriodId)
                    _breakPeriod = Workarea.Cashe.GetCasheData<TimePeriod>().Item(_breakPeriodId);
                return _breakPeriod;
            }
            set
            {
                if (_breakPeriod == value) return;
                OnPropertyChanging("BreakPeriod");
                _breakPeriod = value;
                _breakPeriodId = _breakPeriod == null ? 0 : _breakPeriod.Id;
                OnPropertyChanged("BreakPeriod");
            }
        }
        
        
        #endregion
        /// <summary>
        /// Собственный обработчик обновления
        /// </summary>
        protected override void OnUpdated()
        {
            base.OnUpdated();

            foreach (DocumentDetailMktg row in _details)
            {
                // сохраняем данные о аналитике строки данных
                if (row.Analitics.Count > 0)
                {
                    row.SaveAnalitics();
                }
            }
            this.Document.InternalOnUpdated();
        }

        protected override void OnCreated()
        {
            base.OnCreated();

            foreach (DocumentDetailMktg row in _details)
            {
                // сохраняем данные о аналитике строки данных
                if (row.Analitics.Count > 0)
                {
                    row.SaveAnalitics();
                }
            }
            this.Document.InternalOnCreated();
        }
        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_supervisorId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SupervisorId, XmlConvert.ToString(_supervisorId));
            if (_managerId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ManagerId, XmlConvert.ToString(_managerId));
            if (_prcNameId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.PrcNameId, XmlConvert.ToString(_prcNameId));
            if (_bankAccFromId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.BankAccFromId, XmlConvert.ToString(_bankAccFromId));
            if (_bankAccToId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.BankAccToId, XmlConvert.ToString(_bankAccToId));
            if (!string.IsNullOrEmpty(_contactName))
                writer.WriteAttributeString(GlobalPropertyNames.ContactName, _contactName);
            if (!string.IsNullOrEmpty(_contactPhone))
                writer.WriteAttributeString(GlobalPropertyNames.ContactPhone, _contactPhone);
            if (_workPostId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.WorkPostId, XmlConvert.ToString(_workPostId));
            if (_metricAreaId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MetricAreaId, XmlConvert.ToString(_metricAreaId));
            if (_outletTypeId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OutletTypeId, XmlConvert.ToString(_outletTypeId));
            if (_outletLocationId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OutletLocationId, XmlConvert.ToString(_outletLocationId));
            if (_timePeriodId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TimePeriodId, XmlConvert.ToString(_timePeriodId));
            if (_breakPeriodId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.BreakPeriodId, XmlConvert.ToString(_breakPeriodId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.SupervisorId) != null)
                _supervisorId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SupervisorId));
            if (reader.GetAttribute(GlobalPropertyNames.ManagerId) != null)
                _managerId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ManagerId));
            if (reader.GetAttribute(GlobalPropertyNames.PrcNameId) != null)
                _prcNameId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PrcNameId));
            if (reader.GetAttribute(GlobalPropertyNames.BankAccFromId) != null)
                _bankAccFromId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.BankAccFromId));
            if (reader.GetAttribute(GlobalPropertyNames.BankAccToId) != null)
                _bankAccToId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.BankAccToId));

            if (reader.GetAttribute(GlobalPropertyNames.ContactName) != null)
                _contactName = reader.GetAttribute(GlobalPropertyNames.ContactName);
            if (reader.GetAttribute(GlobalPropertyNames.ContactPhone) != null)
                _contactPhone = reader.GetAttribute(GlobalPropertyNames.ContactPhone);

            if (reader.GetAttribute(GlobalPropertyNames.WorkPostId) != null)
                _workPostId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.WorkPostId));
            if (reader.GetAttribute(GlobalPropertyNames.MetricAreaId) != null)
                _metricAreaId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MetricAreaId));
            if (reader.GetAttribute(GlobalPropertyNames.OutletTypeId) != null)
                _outletTypeId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OutletTypeId));
            if (reader.GetAttribute(GlobalPropertyNames.OutletLocationId) != null)
                _outletLocationId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OutletLocationId));
            if (reader.GetAttribute(GlobalPropertyNames.TimePeriodId) != null)
                _timePeriodId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TimePeriodId));
            if (reader.GetAttribute(GlobalPropertyNames.BreakPeriodId) != null)
                _breakPeriodId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.BreakPeriodId));
        }
        #endregion

        #region Состояния
        BaseStructDocumentMktg _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentMktg
                {
                    ManagerId = _managerId,
                    PrcNameId = _prcNameId,
                    SupervisorId = _supervisorId,
                    BankAccFromId = _bankAccFromId,
                    BankAccToId = _bankAccToId,
                    ContactName = _contactName,
                    ContactPhone = _contactPhone,
                    WorkPostId = _workPostId,
                    MetricAreaId = _metricAreaId,
                    OutletTypeId = _outletTypeId,
                    OutletLocationId = _outletLocationId,
                    TimePeriodId = _timePeriodId,
                    BreakPeriodId = _breakPeriodId
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
            _managerId = _baseStruct.ManagerId;
            _prcNameId = _baseStruct.PrcNameId;
            _supervisorId = _baseStruct.SupervisorId;
            _bankAccToId = _baseStruct.BankAccToId;
            _bankAccFromId = _baseStruct.BankAccFromId;
            _contactName = _baseStruct.ContactName;
            _contactPhone = _baseStruct.ContactPhone;
            _workPostId = _baseStruct.WorkPostId;
            _metricAreaId = _baseStruct.MetricAreaId;
            _outletTypeId = _baseStruct.OutletTypeId;
            _outletLocationId = _baseStruct.OutletLocationId;
            _timePeriodId = _baseStruct.TimePeriodId;
            _breakPeriodId = _baseStruct.BreakPeriodId;
            IsChanged = false;
        }
        #endregion

        #region Дополнительные методы
        /// <summary>
        /// Добавить новую строку детализации
        /// </summary>
        /// <returns></returns>
        public DocumentDetailMktg NewRow()
        {
            DocumentDetailMktg row = new DocumentDetailMktg
            {
                Workarea = Workarea,
                Document = this,
                StateId = State.STATEACTIVE,
                Date = Date,
                Kind = Kind,
                OwnerId = Id
            };
            Details.Add(row);
            return row;
        }
        /// <summary>
        /// Новая строка аналитики
        /// </summary>
        /// <returns></returns>
        public DocumentAnalitic NewAnaliticRow()
        {
            DocumentAnalitic row = new DocumentAnalitic
                                       {
                                           Workarea = Workarea,
                                           Document = this.Document,
                                           StateId = State.STATEACTIVE,
                                           Kind = Kind,
                                           OwnerId = Id
                                       };
            Analitics.Add(row);
            return row;
        }
        #endregion

        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            Document.StateId = StateId;
            Document.FlagsValue = FlagsValue;
            if(Workarea.IsWebApplication)
                Document.UserName = UserName;
            Document.Validate();
            Date = Document.Date;
            base.Validate();
            foreach (DocumentDetailMktg row in _details)
            {
                if (Workarea.IsWebApplication)
                    row.UserName = UserName;
                row.Validate();
            }
            foreach (DocumentAnalitic row in _analitics)
            {
                if (Workarea.IsWebApplication)
                    row.UserName = UserName;
                row.Validate();
            }
            if (_collDocumentContractor != null)
            {
                foreach (DocumentContractor row in _collDocumentContractor)
                {
                    row.OwnId = this.Id;
                    row.Validate();
                }
            }
            if (Document._signs != null)
            {
                foreach (DocumentSign s in Document._signs)
                {
                    s.Validate();
                }
            }
        }
        #region Подписи документа
        /// <summary>Коллекция подписей документа</summary>
        public List<DocumentSign> Signs()
        {
            if (Document._signs == null)
                Document.LoadSigns();
            return Document._signs;
        }
        #endregion
        #region Сохранить
        //public override void Save()
        //{
        //    Save(true);
        //}
        #endregion
        #region Дополнительные корреспонденты документа
        internal List<DocumentContractor> _collDocumentContractor;
        /// <summary>Загрузить подписи документа их базы данных</summary>
        internal void LoadContractors()
        {
            _collDocumentContractor = new List<DocumentContractor>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<DocumentContractor>().Entity.FindMethod(GlobalMethodAlias.LoadByOwnerId).FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                    cmd.Parameters.Add(prm);

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.OwnId, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = Id;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DocumentContractor item = new DocumentContractor
                                {
                                    Workarea = Workarea,
                                    Owner = Document
                                };
                                item.Load(reader);
                                _collDocumentContractor.Add(item);
                            }
                            reader.Close();
                        }
                        // TODO: Проверка ....
                        //object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        //if (retval == null)
                        //    throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        //if ((Int32)retval != 0)
                        //    throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
        }
        /// <summary>Коллекция дополнительных корреспондентов документа</summary>
        public List<DocumentContractor> Contractors()
        {
            if (_collDocumentContractor == null)
                LoadContractors();
            return _collDocumentContractor;
        }
        /// <summary>
        /// Обновить подписи
        /// </summary>
        public void RefreshContractors()
        {
            LoadContractors();
        }
        #endregion
        #region База данных
        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="procedureName">Хранимая процедура</param>
        /// <param name="versionControl">Использовать контроль версии объекта</param>
        protected override void Update(string procedureName, bool versionControl)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    SqlTransaction transaction = cnn.BeginTransaction("DocumentSaveTransaction");
                    #region Основной документ
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.Transaction = transaction;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        SetParametersToInsertUpdate(sqlCmd, false, versionControl);

                        if (sqlCmd.Connection.State != ConnectionState.Open)
                            sqlCmd.Connection.Open();
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.Read() && reader.HasRows)
                            {
                                if (Document == null)
                                    Document = new Document { Workarea = Workarea };
                                Document.Load(reader);
                                if (reader.NextResult())
                                {
                                    if (reader.Read() && reader.HasRows)
                                        Load(reader);
                                }
                                _details.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentDetailMktg docRow = new DocumentDetailMktg { Workarea = Workarea, Document = this };
                                            docRow.Load(reader);
                                            _details.Add(docRow);
                                        }
                                    }
                                }
                                _analitics.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentAnalitic docRow = new DocumentAnalitic { Workarea = Workarea, Document = Document };
                                            docRow.Load(reader);
                                            _analitics.Add(docRow);
                                        }
                                    }
                                }
                            }
                            // NOTE: Перед проверкой Return параметра - закрыть Reader!!!
                            reader.Close();
                            object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                            if (retval == null)
                                throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));
                                
                            if ((int)retval != 0)
                                throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        }
                    }
                    #endregion
                    #region Дополнительные корреспонденты документа
                    if (_collDocumentContractor != null)
                    {
                        using (SqlCommand sqlCmd = cnn.CreateCommand())
                        {
                            sqlCmd.Transaction = transaction;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandText = "Document.ContractorsInsertUpdateAll";
                            SetParametersToContracts(sqlCmd, false);

                            if (sqlCmd.Connection.State != ConnectionState.Open)
                                sqlCmd.Connection.Open();
                            using (SqlDataReader reader = sqlCmd.ExecuteReader())
                            {
                                _collDocumentContractor.Clear();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentContractor docRow = new DocumentContractor { Workarea = Workarea, OwnId = Id };
                                        docRow.Load(reader);
                                        docRow.Owner = Document;
                                        _collDocumentContractor.Add(docRow);
                                    }
                                }
                                reader.Close();

                                object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                                if (retval == null)
                                {
                                    throw new SqlReturnException(Workarea.Cashe.ResourceString(
                                        "EX_MSG_DBUNCNOWNRESULTS", 1049));
                                }

                                if ((int)retval != 0)
                                {
                                    throw new DatabaseException(
                                        Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                                }
                            }
                        }
                    }

                    #endregion
                    #region Подписи документа
                    if (Document._signs != null)
                    {
                        using (SqlCommand sqlCmd = cnn.CreateCommand())
                        {
                            sqlCmd.Transaction = transaction;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandText = "Document.SignatureInsertUpdateAll";
                            SetParametersToSigns(Document._signs, sqlCmd, false);

                            if (sqlCmd.Connection.State != ConnectionState.Open)
                                sqlCmd.Connection.Open();
                            using (SqlDataReader reader = sqlCmd.ExecuteReader())
                            {
                                Document._signs.Clear();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentSign docRow = new DocumentSign { Workarea = Workarea, OwnId = Id };
                                        docRow.Load(reader);
                                        docRow.Owner = Document;
                                        Document._signs.Add(docRow);
                                    }
                                }
                                reader.Close();

                                object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                                if (retval == null)
                                {
                                    throw new SqlReturnException(Workarea.Cashe.ResourceString(
                                        "EX_MSG_DBUNCNOWNRESULTS", 1049));
                                }

                                if ((int)retval != 0)
                                {
                                    throw new DatabaseException(
                                        Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                                }
                            }
                        }
                    }

                    #endregion
                    transaction.Commit();
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }

        /// <summary>
        /// Создать
        /// </summary>
        /// <param name="procedureName">Хранимая процедура</param>
        protected override void Create(string procedureName)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    SqlTransaction transaction = cnn.BeginTransaction("DocumentSaveTransaction");
                    #region Основной документ
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.Transaction = transaction;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        SetParametersToInsertUpdate(sqlCmd, true, false);

                        if (sqlCmd.Connection.State != ConnectionState.Open)
                            sqlCmd.Connection.Open();
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.Read() && reader.HasRows)
                            {
                                if (Document == null)
                                    Document = new Document { Workarea = Workarea };
                                Document.Load(reader);
                                if (reader.NextResult())
                                {
                                    if (reader.Read() && reader.HasRows)
                                        Load(reader);
                                }
                                _details.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentDetailMktg docRow = new DocumentDetailMktg { Workarea = Workarea, Document = this };
                                            docRow.Load(reader);
                                            _details.Add(docRow);
                                        }
                                    }
                                }
                                _analitics.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentAnalitic docRow = new DocumentAnalitic { Workarea = Workarea, Document = Document };
                                            docRow.Load(reader);
                                            _analitics.Add(docRow);
                                        }
                                    }
                                }
                            }
                            reader.Close();
                            object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                            if (retval == null)
                                throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                            if ((int)retval != 0)
                                throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        }
                    }
                    #endregion
                    #region Дополнительные корреспонденты документа
                    if (_collDocumentContractor != null)
                    {
                        using (SqlCommand sqlCmd = cnn.CreateCommand())
                        {
                            sqlCmd.Transaction = transaction;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandText = "Document.ContractorsInsertUpdateAll";
                            SetParametersToContracts(sqlCmd, false);

                            if (sqlCmd.Connection.State != ConnectionState.Open)
                                sqlCmd.Connection.Open();
                            using (SqlDataReader reader = sqlCmd.ExecuteReader())
                            {
                                _collDocumentContractor.Clear();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentContractor docRow = new DocumentContractor { Workarea = Workarea, OwnId = Id };
                                        docRow.Load(reader);
                                        docRow.Owner = Document;
                                        _collDocumentContractor.Add(docRow);
                                    }
                                }
                                reader.Close();

                                object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                                if (retval == null)
                                {
                                    throw new SqlReturnException(Workarea.Cashe.ResourceString(
                                        "EX_MSG_DBUNCNOWNRESULTS", 1049));
                                }

                                if ((int)retval != 0)
                                {
                                    throw new DatabaseException(
                                        Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                                }
                            }
                        }
                    }

                    #endregion
                    #region Подписи документа
                    if (Document._signs != null)
                    {
                        using (SqlCommand sqlCmd = cnn.CreateCommand())
                        {
                            sqlCmd.Transaction = transaction;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandText = "Document.SignatureInsertUpdateAll";
                            SetParametersToSigns(Document._signs, sqlCmd, false);

                            if (sqlCmd.Connection.State != ConnectionState.Open)
                                sqlCmd.Connection.Open();
                            using (SqlDataReader reader = sqlCmd.ExecuteReader())
                            {
                                Document._signs.Clear();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentSign docRow = new DocumentSign { Workarea = Workarea, OwnId = Id };
                                        docRow.Load(reader);
                                        docRow.Owner = Document;
                                        Document._signs.Add(docRow);
                                    }
                                }
                                reader.Close();

                                object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                                if (retval == null)
                                {
                                    throw new SqlReturnException(Workarea.Cashe.ResourceString(
                                        "EX_MSG_DBUNCNOWNRESULTS", 1049));
                                }

                                if ((int)retval != 0)
                                {
                                    throw new DatabaseException(
                                        Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                                }
                            }
                        }
                    }

                    #endregion
                    transaction.Commit();
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }

        public static DocumentMktg CreateCopy(DocumentMktg value)
        {

            DocumentMktg doc = new DocumentMktg { Workarea = value.Workarea };
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        sqlCmd.CommandText = value.FindProcedure(GlobalMethodAlias.Copy); //"Core.CustomViewListCopy";
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.Read() && reader.HasRows)
                            {


                                doc.Document = new Document { Workarea = value.Workarea };
                                doc.Document.Load(reader);
                                if (reader.NextResult())
                                {
                                    if (reader.Read() && reader.HasRows)
                                        doc.Load(reader);
                                }
                                doc._details.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentDetailMktg docRow = new DocumentDetailMktg { Workarea = value.Workarea, Document = doc };
                                            docRow.Load(reader);
                                            doc._details.Add(docRow);
                                        }
                                    }
                                }
                                doc._analitics.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentAnalitic docRow = new DocumentAnalitic { Workarea = value.Workarea, Document = doc.Document };
                                            docRow.Load(reader);
                                            doc._analitics.Add(docRow);
                                        }
                                    }
                                }
                            }
                            reader.Close();
                            object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                            if (retval == null)
                                throw new SqlReturnException(value.Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                            if ((int)retval != 0)
                                throw new DatabaseException(value.Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return doc;
        }

        /// <summary>
        /// Загрузить данные из базы данных
        /// </summary>
        /// <param name="value">Идентификатор</param>
        /// <param name="procedureName">Хранимая процедура</param>
        protected override void Load(int value, string procedureName)
        {
            if (value == 0)
                return;
            OnBeginInit();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
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
                            Document = new Document {Workarea = Workarea};
                            Document.Load(reader);
                            if (reader.NextResult())
                            {
                                if (reader.Read() && reader.HasRows)
                                    Load(reader);
                            }
                            _details.Clear();
                            if (reader.NextResult())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentDetailMktg docRow = new DocumentDetailMktg { Workarea = Workarea, Document = this };
                                        docRow.Load(reader);
                                        _details.Add(docRow);
                                    }
                                }
                            }
                            _analitics.Clear();
                            if (reader.NextResult())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentAnalitic docRow = new DocumentAnalitic { Workarea = Workarea, Document = Document };
                                        docRow.Load(reader);
                                        _analitics.Add(docRow);
                                    }
                                }
                            }
                        }
                        
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
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                Kind = reader.GetInt32(10);
                _supervisorId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                _managerId = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                _prcNameId = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _bankAccFromId = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                _bankAccToId = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
                _contactName = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                _contactPhone = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                _workPostId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _metricAreaId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _outletTypeId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _outletLocationId = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
                _timePeriodId = reader.IsDBNull(22) ? 0 : reader.GetInt32(22);
                _breakPeriodId = reader.IsDBNull(23) ? 0 : reader.GetInt32(23);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }
        internal class TpvCollection : List<DocumentMktg>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {

                var sdr = new SqlDataRecord
                (
                    new SqlMetaData(GlobalPropertyNames.Id, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Guid, SqlDbType.UniqueIdentifier),
                    new SqlMetaData(GlobalPropertyNames.DatabaseId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.DbSourceId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Version, SqlDbType.Binary, 8),
                    new SqlMetaData(GlobalPropertyNames.UserName, SqlDbType.NVarChar, 50),
                    new SqlMetaData(GlobalPropertyNames.DateModified, SqlDbType.DateTime),
                    new SqlMetaData(GlobalPropertyNames.Flags, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StateId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Date, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.KindId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.SupervisorId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ManagerId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.PrcContentId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.BankAccFromId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.BankAccToId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ContactName, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.ContactPhone, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.WorkPostId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.MetricAreaId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.OutletTypeId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.OutletLocationId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.TimePeriodId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.BreakPeriodId, SqlDbType.Int)
                );

                foreach (DocumentMktg doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentMktg doc)
        {
            sdr.SetInt32(0, doc.Id);
            sdr.SetGuid(1, doc.Guid);
            sdr.SetInt32(2, doc.DatabaseId);
            if (doc.DbSourceId == 0)
                sdr.SetValue(3, DBNull.Value);
            else
                sdr.SetInt32(3, doc.DbSourceId);
            if (doc.ObjectVersion == null || doc.ObjectVersion.All(v => v == 0))
                sdr.SetValue(4, DBNull.Value);
            else
                sdr.SetValue(4, doc.ObjectVersion);

            if (string.IsNullOrEmpty(doc.UserName))
                sdr.SetValue(5, DBNull.Value);
            else
                sdr.SetString(5, doc.UserName);

            if (doc.DateModified.HasValue)
                sdr.SetDateTime(6, doc.DateModified.Value);
            else
                sdr.SetValue(6, DBNull.Value);

            sdr.SetInt32(7, doc.FlagsValue);
            sdr.SetInt32(8, doc.StateId);
            sdr.SetDateTime(9, doc.Date);
            sdr.SetInt32(10, doc.Kind);

            if (doc.SupervisorId == 0)
                sdr.SetValue(11, DBNull.Value);
            else
                sdr.SetInt32(11, doc.SupervisorId);

            if (doc.ManagerId == 0)
                sdr.SetValue(12, DBNull.Value);
            else
                sdr.SetInt32(12, doc.ManagerId);

            if (doc.PrcNameId == 0)
                sdr.SetValue(13, DBNull.Value);
            else
                sdr.SetInt32(13, doc.PrcNameId);
            
            if (doc.BankAccFromId == 0)
                sdr.SetValue(14, DBNull.Value);
            else
                sdr.SetInt32(14, doc.BankAccFromId);

            if (doc.BankAccToId == 0)
                sdr.SetValue(15, DBNull.Value);
            else
                sdr.SetInt32(15, doc.BankAccToId);

            if(string.IsNullOrEmpty(doc.ContactName))
                sdr.SetValue(16, DBNull.Value);
            else
                sdr.SetString(16, doc.ContactName);

            if (string.IsNullOrEmpty(doc.ContactPhone))
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetString(17, doc.ContactPhone);

            if (doc.WorkPostId == 0)
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetInt32(18, doc.WorkPostId);

            if (doc.MetricAreaId == 0)
                sdr.SetValue(19, DBNull.Value);
            else
                sdr.SetInt32(19, doc.MetricAreaId);

            if (doc.OutletTypeId == 0)
                sdr.SetValue(20, DBNull.Value);
            else
                sdr.SetInt32(20, doc.OutletTypeId);

            if (doc.OutletLocationId == 0)
                sdr.SetValue(21, DBNull.Value);
            else
                sdr.SetInt32(21, doc.OutletLocationId);

            if (doc.TimePeriodId == 0)
                sdr.SetValue(22, DBNull.Value);
            else
                sdr.SetInt32(22, doc.TimePeriodId);

            if (doc.BreakPeriodId == 0)
                sdr.SetValue(23, DBNull.Value);
            else
                sdr.SetInt32(23, doc.BreakPeriodId);

            return sdr;
        }

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            sqlCmd.Parameters.Add(prm);

            Document.TpvCollection coll = new Document.TpvCollection { Document };
            var headerParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Header, coll);
            headerParam.SqlDbType = SqlDbType.Structured;

            TpvCollection collTypes = new TpvCollection {this};
            var headerTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.HeaderType, collTypes);
            headerTypeParam.SqlDbType = SqlDbType.Structured;

            DocumentDetailMktg.TpvCollection collRows = new DocumentDetailMktg.TpvCollection();
            collRows.AddRange(_details);
            if (_details.Count == 0)
            {
                var detailsTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Detail, null);
                detailsTypeParam.SqlDbType = SqlDbType.Structured;
            }
            else
            {
                var detailsTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Detail, collRows);
                detailsTypeParam.SqlDbType = SqlDbType.Structured;
            }

            DocumentAnalitic.TpvCollection collAnalitics = new DocumentAnalitic.TpvCollection();
            collAnalitics.AddRange(_analitics);
            if (_analitics.Count == 0)
            {
                var analiticTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.AnaliticDetail, null);
                analiticTypeParam.SqlDbType = SqlDbType.Structured;
            }
            else
            {
                var analiticTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.AnaliticDetail, collAnalitics);
                analiticTypeParam.SqlDbType = SqlDbType.Structured;
            }
        }
        private void SetParametersToContracts(SqlCommand sqlCmd, bool validateVersion)
        {
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            sqlCmd.Parameters.Add(prm);

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = Id;

            DocumentContractor.TpvCollection collRows = new DocumentContractor.TpvCollection();
            foreach (DocumentContractor row in _collDocumentContractor)
            {
                row.OwnId = this.Id;
                row.Validate();
            }
            collRows.AddRange(_collDocumentContractor);
            if (_collDocumentContractor.Count == 0)
            {
                var detailsTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Contractors, null);
                detailsTypeParam.SqlDbType = SqlDbType.Structured;
            }
            else
            {
                var detailsTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Contractors, collRows);
                detailsTypeParam.SqlDbType = SqlDbType.Structured;
            }
        }
        #endregion

        #region IEditableObject Members
        void IEditableObject.BeginEdit()
        {
            SaveState(false);
        }

        void IEditableObject.CancelEdit()
        {
            RestoreState();
        }

        void IEditableObject.EndEdit()
        {
            //_baseStruct = new BaseStructDocumentStore();
        }
        #endregion

        #region Связи
        //public List<IChain<DocumentContract>> GetLinks()
        //{
        //    List<IChain<DocumentContract>> collection = new List<IChain<DocumentContract>>();
        //    using (SqlConnection cnn = Workarea.GetDatabaseConnection())
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandText = EntityDocument.FindMethod("DocumentChainLoadSource").FullName;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
        //            prm.Direction = ParameterDirection.ReturnValue;

        //            prm = cmd.Parameters.Add(GlobalSqlParamNames.DbSourceId, SqlDbType.Int);
        //            prm.Direction = ParameterDirection.Input;
        //            prm.Value = Id;

        //            try
        //            {
        //                if (cmd.Connection.State == ConnectionState.Closed)
        //                    cmd.Connection.Open();
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                if (reader != null)
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            DocumentChain<DocumentContract> item = new DocumentChain<DocumentContract> { Workarea = Workarea, Left = this };
        //                            item.Load(reader);
        //                            collection.Add(item);
        //                        }
        //                    }
        //                    reader.Close();
        //                }
        //                object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
        //                if (retval == null)
        //                    throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

        //                if ((Int32)retval != 0)
        //                    throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

        //            }
        //            finally
        //            {
        //                if (cmd.Connection.State == ConnectionState.Open)
        //                    cmd.Connection.Close();
        //            }

        //        }
        //    }
        //    return collection;
        //}
        #endregion

        #region IChainsAdvancedList<DocumentContract,FileData> Members
        List<IChainAdvanced<Document, FileData>> IChainsAdvancedList<Document, FileData>.GetLinks()
        {
            return GetLinks(13);
        }
        List<ChainValueView> IChainsAdvancedList<Document, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Document, FileData>(this.Document);
        }
        public List<IChainAdvanced<Document, FileData>> GetLinks(int? kind)
        {
            List<IChainAdvanced<Document, FileData>> collection = new List<IChainAdvanced<Document, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = EntityDocument.FindMethod("LoadFiles").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = Id;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainAdvanced<Document, FileData> item = new ChainAdvanced<Document, FileData> { Workarea = Workarea, Left = Document };
                                item.Load(reader);
                                collection.Add(item);
                            }
                        }
                        reader.Close();

                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

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
    }
}