using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Иерархия"</summary>
    internal struct HierarchyStruct
    {
        /// <summary>Разрешенное содержимое в виде флага</summary>
        public int ContentFlags;
        /// <summary>Наличие содержимого</summary>
        public bool HasContents;
        /// <summary>Идентификатор родителя</summary>
        public int ParentId;
        /// <summary>Полный путь</summary>
        public string Path;
        /// <summary>Идентификатор корня</summary>
        public int RootId;
        /// <summary>Идентификатор списка</summary>
        public int ViewListId;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>Тип системного кода для иерархии</summary>
    public enum HierarchyCodeKind
    {
        /// <summary>Все данные</summary>
        All,
        /// <summary>Запрещенные к использованию</summary>
        Deny,
        /// <summary>Удаленные</summary>
        Trash,
        /// <summary>Не входящие в группы</summary>
        NotInGroup,
        /// <summary>Системные</summary>
        System,
        /// <summary>Требуют корректировки</summary>
        NotDone,
        /// <summary>Шаблоны</summary>
        Template,
        /// <summary>Корневая группа поиска</summary>
        FindRoot
    }
    /// <summary>Иерархия</summary>
    public sealed class Hierarchy : BaseCore<Hierarchy>, IChains<Hierarchy>, IEquatable<Hierarchy>, 
        IChainsAdvancedList<Hierarchy, Agent>,
        IChainsAdvancedList<Hierarchy, Note>,
        ICodes<Hierarchy>, ICompanyOwner
    {
// ReSharper disable InconsistentNaming
        /// <summary>Код иерархии для хранения методов получения номера для автонумерации</summary>
        public const string SYSTEM_RULESET_AUTONUMMETHOD = "SYSTEM_RULESET_AUTONUMMETHOD";

        /// <summary>Код иерархии для хранения файлов базы знаний</summary>
        public const string SYSTEM_FILEDATA_KNOWLEDGE = "SYSTEM_FILEDATA_KNOWLEDGE";
        /// <summary>Код иерархии для хранения файлов обновления базы данных</summary>
        public const string SYSTEM_FILEDATA_SQLUPDATE = "SYSTEM_FILEDTA_SQLUPDATE";
        /// <summary>Код корневой иерархии для хранения файлов системы обмена</summary>
        public const string SYSTEM_EXCFILEDATA_ROOT = "SYSTEM_EXCFILEDATA_ROOT";
        /// <summary>Код иерархии для хранения файлов системы обмена для мобильных устройств</summary>
        public const string SYSTEM_EXCFILEDATA_MOBILESALES = "SYSTEM_EXCFILEDATA_MOBILESALES";
        /// <summary>Код иерархии для хранения файлов гербов стран</summary>
        public const string SYSTEM_FILEDATA_COUNTRYEMBLEMS = "SYSTEM_FILEDATA_COUNTRYEMBLEMS";
        /// <summary>Код иерархии "Изображения товаров"</summary>
        public const string SYSTEM_FILEDATA_LOGOPRODUCT = "SYSTEM_FILEDATA_LOGOPRODUCT";
        /// <summary>Код иерархии "Логотипы компаний"</summary>
        public const string SYSTEM_FILEDATA_LOGOAGENT = "SYSTEM_FILEDATA_LOGOAGENT";
        /// <summary>Код иерархии "Гербы городов"</summary>
        public const string SYSTEM_FILEDATA_TOWNEMBLEMS = "SYSTEM_FILEDATA_TOWNEMBLEMS";
        
        

        /// <summary>Код корневой иерархии для хранения производителей</summary>
        public const string SYSTEM_AGENT_MANUFACTURERS = "SYSTEM_AGENT_MANUFACTURERS";
        /// <summary>Код корневой иерархии для хранения клиентов</summary>
        public const string SYSTEM_AGENT_BUYERS = "SYSTEM_AGENT_BUYERS";
        /// <summary>Код корневой иерархии для хранения собственных холдингов и объединений</summary>
        public const string SYSTEM_AGENT_MYCOMPANY = "SYSTEM_AGENT_MYCOMPANY";
        /// <summary>Код корневой иерархии для хранения собственных предприятий</summary>
        public const string SYSTEM_AGENT_MYDEPATMENTS = "SYSTEM_AGENT_MYDEPATMENTS";
        /// <summary>Код корневой иерархии для хранения собственных сотрудников</summary>
        public const string SYSTEM_AGENT_MYWORKERS = "SYSTEM_AGENT_MYWORKERS";
        /// <summary>Код корневой иерархии для хранения собственных складов</summary>
        public const string SYSTEM_AGENT_MYSTORES = "SYSTEM_AGENT_MYSTORES";
        /// <summary>Код корневой иерархии для хранения поставщиков</summary>
        public const string SYSTEM_AGENT_SUPPLIERS = "SYSTEM_AGENT_SUPPLIERS";
        /// <summary>Код корневой иерархии для хранения ассоциаций банков</summary>
        public const string SYSTEM_AGENT_BANKASSOCIATION = "SYSTEM_AGENT_BANKASSOCIATION";
        

        /// <summary>Код корневой иерархии для хранения банков</summary>
        public const string SYSTEM_AGENT_BANKS = "SYSTEM_AGENT_BANKS";
        /// <summary>Код корневой иерархии для хранения государственных органов</summary>
        public const string SYSTEM_AGENT_GOVENMENT = "SYSTEM_AGENT_GOVENMENT";
        /// <summary>Код корневой иерархии для хранения кондидатов на работу</summary>
        public const string SYSTEM_AGENT_JOBCANDIDATES = "SYSTEM_AGENT_JOBCANDIDATES";
        /// <summary>Код корневой иерархии для хранения конкурентов</summary>
        public const string SYSTEM_AGENT_COMPETITOR = "SYSTEM_AGENT_COMPETITOR";

        /// <summary>Код корневой иерархии для хранения товаров</summary>
        public const string SYSTEM_PRODUCTS = "SYSTEM_PRODUCTS";
        /// <summary>Код корневой иерархии для хранения выбывших товаров</summary>
        public const string SYSTEM_PRODUCT_ELIMINATED = "SYSTEM_PRODUCT_ELIMINATED";
        /// <summary>Код корневой иерархии для хранения текущих товаров</summary>
        public const string SYSTEM_PRODUCT_CURRENT = "SYSTEM_PRODUCT_CURRENT";
        /// <summary>Код корневой иерархии для хранения услуг</summary>
        public const string SYSTEM_PRODUCT_SERVICE = "SYSTEM_PRODUCT_SERVICE";
        /// <summary>Код корневой иерархии для хранения основных средств</summary>
        public const string SYSTEM_PRODUCT_ASSETS = "SYSTEM_PRODUCT_ASSETS";
        /// <summary>Код корневой иерархии для хранения сырья</summary>
        public const string SYSTEM_PRODUCT_RAWMATERIALS = "SYSTEM_PRODUCT_RAWMATERIALS";
        /// <summary>Код корневой иерархии для хранения готовой продукции</summary>
        public const string SYSTEM_PRODUCT_FINISHEDPRODUCTION = "SYSTEM_PRODUCT_FINISHEDPRODUCTION";
        /// <summary>Код корневой иерархии для хранения денежных объектов</summary>
        public const string SYSTEM_PRODUCT_MONEY = "SYSTEM_PRODUCT_MONEY";
        /// <summary>Код корневой иерархии для хранения тары</summary>
        public const string SYSTEM_PRODUCT_PACK = "SYSTEM_PRODUCT_PACK";
        /// <summary>Код корневой иерархии для хранения орг техники</summary>
        public const string SYSTEM_PRODUCT_ORGTEX = "SYSTEM_PRODUCT_ORGTEX";
        /// <summary>Код корневой иерархии для хранения копьютеров</summary>
        public const string SYSTEM_PRODUCT_COMPUTER = "SYSTEM_PRODUCT_COMPUTER";
        
        /// <summary>Код корневой иерархии для хранения пользовательских сообщений</summary>
        public const string SYSTEM_MESSAGE_USERS = "SYSTEM_MESSAGE_USERS";
        /// <summary>Код корневой иерархии для хранения сообщений о новостях</summary>
        public const string SYSTEM_MESSAGE_WEBNEWS = "SYSTEM_MESSAGE_WEBNEWS";
        /// <summary>Код корневой иерархии для хранения сообщений файлового обмена</summary>
        public const string SYSTEM_MESSAGE_FILESERVICE = "SYSTEM_MESSAGE_FILESERVICE";
        /// <summary>Код корневой иерархии для хранения сообщений службы импорта данных</summary>
        public const string SYSTEM_MESSAGE_IMPORTSERVICE = "SYSTEM_MESSAGE_IMPORTSERVICE";
        /// <summary>Код корневой иерархии для хранения сообщений службы экспорта данных</summary>
        public const string SYSTEM_MESSAGE_EXPORTSERVICE = "SYSTEM_MESSAGE_EXPORTSERVICE";
        
        /// <summary>Код корневой иерархии для хранения формы расчетов</summary>
        public const string SYSTEM_ANALITIC_TAXDOCPAYMENTMETHOD = "SYSTEM_ANALITIC_TAXDOCPAYMENTMETHOD";
        /// <summary>Код корневой иерархии для хранения брендов</summary>
        public const string SYSTEM_ANALITIC_BRANDS = "SYSTEM_ANALITIC_BRANDS";
        /// <summary>Код корневой иерархии для хранения товарных групп</summary>
        public const string SYSTEM_ANALITIC_TRADEGROUP = "SYSTEM_ANALITIC_TRADEGROUP";
        /// <summary>Код корневой иерархии для хранения аналитики "Вид продукции"</summary>
        public const string SYSTEM_ANALITIC_PRODUCTTYPE = "SYSTEM_PRODUCTTYPE";
        /// <summary>Код корневой иерархии для хранения аналитики "Вид упаковки"</summary>
        public const string SYSTEM_ANALITIC_PACKTYPE = "SYSTEM_PACKTYPE";
        /// <summary>Код корневой иерархии для хранения аналитики "Причина возврата"</summary>
        public const string SYSTEM_ANALITIC_RETURNREASON = "SYSTEM_RETURNREASON";
        /// <summary>Код корневой иерархии для хранения аналитики "Расположение торговой точки"</summary>
        public const string SYSTEM_ANALITIC_OUTLETLOCATION = "SYSTEM_OUTLETLOCATION";
        /// <summary>Код корневой иерархии для хранения аналитики "Тип холодильного оборудования"</summary>
        public const string SYSTEM_ANALITIC_TYPEEQUIPMENTHOLOD = "SYSTEM_TYPEEQUIPMENTHOLOD";
        /// <summary>Код корневой иерархии для хранения аналитики "Тип оборудования"</summary>
        public const string SYSTEM_ANALITIC_TYPEEQUIPMENT = "SYSTEM_TYPEEQUIPMENT";
        /// <summary>Код корневой иерархии для хранения аналитики "Цвет"</summary>
        public const string SYSTEM_ANALITIC_COLOR = "SYSTEM_ANALITIC_COLOR";
        /// <summary>Код корневой иерархии для хранения аналитики "События"</summary>
        public const string SYSTEM_ANALITIC_EVENT = "SYSTEM_ANALITIC_EVENT";
        /// <summary>Код корневой иерархии для хранения аналитики "Тип рекурсии"</summary>
        public const string SYSTEM_ANALITITIC_RECURCIVE = "SYSTEM_ANALITIC_RECURCIVE";
        
        /// <summary>Код корневой иерархии для хранения аналитики "Денежные средства"</summary>
        public const string SYSTEM_ANALITIC_PAYMENTTYPE = "SYSTEM_ANALITIC_PAYMENTTYPE";
        /// <summary>Код корневой иерархии для хранения аналитики "Денежные средства" расход</summary>
        public const string SYSTEM_ANALITIC_PAYMENTTYPEOUT = "SYSTEM_ANALITIC_PAYMENTTYPE_OUT";
        /// <summary>Код корневой иерархии для хранения аналитики "Денежные средства" приход</summary>
        public const string SYSTEM_ANALITIC_PAYMENTTYPEIN = "SYSTEM_ANALITIC_PAYMENTTYPE_IN";

        /// <summary>Код корневой иерархии для хранения аналитики "Категория торговой точки"</summary>
        public const string SYSTEM_ANALITIC_AGENTCATEGORY = "SYSTEM_AGENTCATEGORY";
        /// <summary>Код корневой иерархии для хранения аналитики "Метраж"</summary>
        public const string SYSTEM_ANALITIC_AGENTMETRICAREA = "SYSTEM_AGENTMETRICAREA";
        /// <summary>Код корневой иерархии для хранения аналитики "Огранизационно провавая форма хозяйствования"</summary>
        public const string SYSTEM_ANALITIC_AGENTOWNERSHIP = "SYSTEM_AGENTOWNERSHIP";
        /// <summary>Код корневой иерархии для хранения аналитики "Отрасль"</summary>
        public const string SYSTEM_ANALITIC_AGENTINDUSTRY = "SYSTEM_AGENTINDUSTRY";
        /// <summary>Код корневой иерархии для хранения аналитики "Тип торговой точки"</summary>
        public const string SYSTEM_ANALITIC_AGENTTYPEOUTLET = "SYSTEM_AGENTTYPEOUTLET";
        /// <summary>Код корневой иерархии для хранения аналитики "Центр ответственности"</summary>
        public const string SYSTEM_ANALITICCFO = "SYSTEM_ANALITICCFO";
        /// <summary>Код корневой иерархии "Должность"</summary>
        public const string SYSTEM_WORKPOST = "SYSTEM_WORKPOST";
        /// <summary>Код корневой иерархии "Категория сотрудника"</summary>
        public const string SYSTEM_ANALITIC_WORKERCATEGORY = "SYSTEM_ANALITIC_WORKERCATEGORY";
        /// <summary>Код корневой иерархии "Состояние задач"</summary>
        public const string SYSTEM_ANALITIC_TASKSTATE = "SYSTEM_ANALITIC_TASKSTATE";
        /// <summary>Код корневой иерархии "Место хранения трудовой"</summary>
        public const string SYSTEM_ANALITIC_EMPLOYMENTBOOK = "SYSTEM_ANALITIC_EMPLOYMENTBOOK";
        /// <summary>Код корневой иерархии "Вид несовершеннолетия"</summary>
        public const string SYSTEM_ANALITIC_MINORS = "SYSTEM_ANALITICS_MINORS";
        /// <summary>Код корневой иерархии "Группа подписания"</summary>
        public const string SYSTEM_ANALITIC_SIGNLEVEL = "SYSTEM_ANALITIC_SIGNLEVEL";

        /// <summary>Код корневой иерархии "Статус маршрута"</summary>
        public const string SYSTEM_ANALITIC_ROUTESATUS = "SYSTEM_ANALITIC_ROUTESATUS";
        /// <summary>Код корневой иерархии "Фактический статус маршрута"</summary>
        public const string SYSTEM_ANALITIC_ROUTEFACT = "SYSTEM_ANALITIC_ROUTEFACT";

        /// <summary>Код корневой иерархии "Состояние оборудования"</summary>
        public const string SYSTEM_ANALITIC_EQUPMENTSTATE = "SYSTEM_EQUPMENTSTATE";
        /// <summary>Код корневой иерархии "размещение оборудования"</summary>
        public const string SYSTEM_ANALITIC_EQUIPMENTPLACEMENT = "SYSTEM_EQUIPMENTPLACEMENT";
        
        /// <summary>Код корневой иерархии аналитики "Результат планирования"</summary>
        public const string SYSTEM_ANALITIC_FINPLANRESULT = "FINPLAN_RESULT";
        /// <summary>Код корневой иерархии аналитики "Тип финансового планирования"</summary>
        public const string SYSTEM_ANALITIC_FINPLANKIND = "FINPLAN_KIND";
        /// <summary>Код корневой иерархии аналитики "Важность"</summary>
        public const string SYSTEM_ANALITIC_CONTRACTPRIORITY = "CONTRACT_PRIORITY";

        /// <summary>Код корневой иерархии "Состояние отношений с торговой точкой"</summary>
        public const string SYSTEM_ANALITIC_REASONTRADEPOINT = "SYSTEM_REASONTRADEPOINT";

        /// <summary>Код корневой иерархии "Условия поставки"</summary>
        public const string SYSTEM_ANALITIC_DELIVERYCONDITION = "TAXDOCDELIVERYCONDITION";
        
        

        /// <summary>Код корневой иерархии для хранения папок документов финансового учета</summary>
        public const string SYSTEM_FOLDER_FINANCENDS = "FINANCENDS";
        /// <summary>Код корневой иерархии для хранения папок документов финансового учета</summary>
        public const string SYSTEM_FOLDER_FINANCE = "FINANCE";
        /// <summary>Код корневой иерархии для хранения папок документов услуг</summary>
        public const string SYSTEM_FOLDER_SERVICE = "SERVICE";
        /// <summary>Код корневой иерархии для хранения папок документов услуг без НДС</summary>
        public const string SYSTEM_FOLDER_SERVICENONDS = "SERVICENONDS";
        /// <summary>Код корневой иерархии для хранения папок документов торговли</summary>
        public const string SYSTEM_FOLDER_SALES = "SYSTEM_FLD_SALES";
        /// <summary>Код корневой иерархии для хранения папок документов торговли без НДС</summary>
        public const string SYSTEM_FOLDER_SALESNONDS = "SYSTEM_FLD_SALESNONDS";
        
        /// <summary>Код корневой иерархии для хранения папок документов кадрового учета</summary>
        public const string SYSTEM_FOLDER_PERSONAL = "SYSTEM_FLD_PERSONALS";
        
        /// <summary>Код корневой иерархии для хранения папок документов налогового учета</summary>
        public const string SYSTEM_FOLDER_TAX = "SYSTEM_FLD_TAX";
        /// <summary>Код корневой иерархии для хранения папок документов управления ценами</summary>
        public const string SYSTEM_FOLDER_PRICEPOLICY = "PRICEPOLICY";
        
        /// <summary>Код корневой иерархии для хранения событий задач</summary>
        public const string SYSTEM_EVENT_TASK = "SYSTEM_EVENT_TASK";


        /// <summary>Код корневой иерархии для хранения цен реализации</summary>
        public const string SYSTEM_PRICENAME_OUTPRICES = "SYSTEM_PRICENAME_OUTPRICES";
        /// <summary>Код корневой иерархии для хранения цен конкурентов</summary>
        public const string SYSTEM_PRICENAME_COMPETITORPRICES = "SYSTEM_PRICECOMPETITOR";
        /// <summary>Код корневой иерархии для хранения цен поставщиков</summary>
        public const string SYSTEM_PRICENAME_INPRICES = "SYSTEM_PRICESUPPLIER";
        
        /// <summary>Код корневой иерархии для хранения резолюций подписи документа</summary>
        public const string SYSTEM_SIGN = "SYSTEM_SIGN";

        /// <summary>Код корневой иерархии для хранения состояния договора</summary>
        public const string SYSTEM_CONTRACT_STATE = "CONTRACT_STATE";
        /// <summary>Код корневой иерархии для хранения типов договоров</summary>
        public const string SYSTEM_CONTRACT_KIND = "CONTRACT_KIND";
        /// <summary>Код корневой иерархии для хранения типов корреспонденции договоров</summary>
        public const string SYSTEM_CONTRACT_CORRESPONDENCE = "SYSTEM_CORRESPONDENCE";

        /// <summary>Код корневой иерархии для хранения графиков разрешенной работы пользователя</summary>
        public const string SYSTEM_TIMEPERIOD_USERLOGON = "SYSTEM_TIMEPERIOD_USERLOGON";
        

        /*
        /// <summary>Код иерархии поиска всех файловых данных</summary>
        public const string SYSTEM_GROUPFIND_ALL_FILEDATA = "SYSTEM_GROUPFIND_ALL_FILEDATA";
        /// <summary>Код иерархии поиска всех запрещенных файловых данных</summary>
        public const string SYSTEM_GROUPFIND_DENY_FILEDATA = "SYSTEM_GROUPFIND_DENY_FILEDATA";
        /// <summary>Код иерархии поиска всех удаленных файловых данных</summary>
        public const string SYSTEM_GROUPFIND_TRASH_FILEDATA = "SYSTEM_GROUPFIND_TRASH_FILEDATA";
        /// <summary>Код иерархии поиска всех не входящих в группы файловых данных</summary>
        public const string SYSTEM_GROUPFIND_NOINGROUP_FILEDATA = "SYSTEM_GROUPFIND_NOINGROUP_FILEDATA";
        /// <summary>Код иерархии поиска всех системных файловых данных</summary>
        public const string SYSTEM_GROUPFIND_SYS_FILEDATA = "SYSTEM_GROUPFIND_SYS_FILEDATA";
        /// <summary>Код иерархии поиска всех требующих корректировки файловых данных</summary>
        public const string SYSTEM_GROUPFIND_NOTDONE_FILEDATA = "SYSTEM_GROUPFIND_NOTDONE_FILEDATA";
        /// <summary>Код иерархии поиска всех шаблонов файловых данных</summary>
        public const string SYSTEM_GROUPFIND_TEMPLATE_FILEDATA = "SYSTEM_GROUPFIND_TEMPLATE_FILEDATA";

        */
// ReSharper restore InconsistentNaming

        /// <summary>
        /// Значение кода иерархии для папок поиска
        /// </summary>
        /// <remarks>Примером наименования для папки поиска всех требующих корректировки файловых данных является 
        /// значение "SYSTEM_GROUPFIND_NOTDONE_FILEDATA"</remarks>
        /// <param name="value"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static string GetSystemFindCodeValue(WhellKnownDbEntity value, HierarchyCodeKind kind)
        {
            if (kind!= HierarchyCodeKind.FindRoot)
                return string.Format("SYSTEM_GROUPFIND_{0}_{1}", kind, value).ToUpper();
            return string.Format("SYSTEM_{0}_FINDROOT", value).ToUpper();
        }

        /// <summary>
        /// Код иерархии "Груповые действия" для указанного типа объекта
        /// </summary>
        /// <param name="value">Базовые сущности</param>
        /// <returns></returns>
        public static string GetGroupActionCode(WhellKnownDbEntity value)
        {
            return string.Format("SYSTEM_PROCESS_GROUPACTION_{0}", value).ToUpper();
        }
        /// <summary>
        /// Код папки поиска документов 
        /// </summary>
        /// <param name="kind">Тип системного кода иерархии</param>
        /// <returns></returns>
        public static string GetSystemFindDocumentCodeValue(HierarchyCodeKind kind)
        {
            return string.Format("SYSTEM_GROUPFIND_FLDDOC_DOCUMENTS_{0}", kind).ToUpper();
        }
        
        /// <summary>
        /// Значение кода иерархии для корневых иерархий
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSystemRootCodeValue(WhellKnownDbEntity value)
        {
            return string.Format("SYSTEM_{0}_ROOT", value).ToUpper();
        }
        /// <summary>
        /// Значение кода иерархии для корневых иерархий
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSystemFavoriteCodeValue(WhellKnownDbEntity value)
        {
            return string.Format("SYSTEM_{0}_FAVORITE", value).ToUpper();
        }

        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Группа, соответствует значению 1</summary>
        public const int KINDVALUE_GROUP = 1;
        /// <summary>Виртуальная группа, соответствует значению 2</summary>
        public const int KINDVALUE_VIRTUAL = 2;

        /// <summary>Группа, соответствует значению 1835009</summary>
        public const int KINDID_GROUP = 1835009;
        /// <summary>Виртуальная группа, соответствует значению 1835010</summary>
        public const int KINDID_VIRTUAL = 1835010;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Hierarchy>.Equals(Hierarchy other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public Hierarchy(): base()
        {
            EntityId = (short) WhellKnownDbEntity.Hierarchy ;
        }
        protected override void CopyValue(Hierarchy template)
        {
            base.CopyValue(template);
            ContentEntityId = template.ContentEntityId;
            ContentFlags = template.ContentFlags;
            ParentId = template.ParentId;
            OrderNo = template.OrderNo;
            RootId = template.RootId;
            ViewListDocumentsId = template.ViewListDocumentsId;
            VirtualBuildId = template.VirtualBuildId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Hierarchy Clone(bool endInit)
        {
            Hierarchy obj = base.Clone(false);
            obj.ContentEntityId = ContentEntityId;
            obj.ContentFlags = ContentFlags;
            obj.ParentId = ParentId;
            obj.OrderNo = OrderNo;
            obj.RootId = RootId;
            obj.ViewListId = ViewListId;
            obj.ViewListDocumentsId = ViewListDocumentsId;
            obj.VirtualBuildId = VirtualBuildId;
            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства
        private int _myCompanyId;
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит аналитика
        /// </summary>
        public int MyCompanyId
        {
            get { return _myCompanyId; }
            set
            {
                if (value == _myCompanyId) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompanyId);
                _myCompanyId = value;
                OnPropertyChanged(GlobalPropertyNames.MyCompanyId);
            }
        }


        private Agent _myCompany;
        /// <summary>
        /// Моя компания, предприятие которому принадлежит аналитика
        /// </summary>
        public Agent MyCompany
        {
            get
            {
                if (_myCompanyId == 0)
                    return null;
                if (_myCompany == null)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                else if (_myCompany.Id != _myCompanyId)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                return _myCompany;
            }
            set
            {
                if (_myCompany == value) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompany);
                _myCompany = value;
                _myCompanyId = _myCompany == null ? 0 : _myCompany.Id;
                OnPropertyChanged(GlobalPropertyNames.MyCompany);
            }
        } 
        private short _orderNo;
        /// <summary>Порядок сортировки</summary>
        public short OrderNo
        {
            get { return _orderNo; }
            set
            {
                if (_orderNo == value) return;
                OnPropertyChanging(GlobalPropertyNames.OrderNo);
                _orderNo = value;
                OnPropertyChanged(GlobalPropertyNames.OrderNo);
            }
        }
	
        private short _contentEntityId;
        /// <summary>Идентификатор системного типа содержимого</summary>
        public short ContentEntityId
        {
            get { return _contentEntityId; }
            set
            {
                if (_contentEntityId == value) return;
                OnPropertyChanging(GlobalPropertyNames.ContentEntityId);
                _contentEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.ContentEntityId);
            }
        }

        private EntityType _contentEntityType;
        /// <summary>Системный тип содержимого</summary>
        public EntityType ContentEntityType
        {
            get
            {
                if (_contentEntityId == 0)
                    return null;
                if (_contentEntityType == null)
                    _contentEntityType = Workarea.CollectionEntities.Find(f => f.Id == _contentEntityId);
                else if (_contentEntityType.Id != _contentEntityId)
                    _contentEntityType = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == _contentEntityId);
                return _contentEntityType;
            }
        }

        private int _viewListId;
        /// <summary>Идентификатор списка</summary>
        public int ViewListId
        {
            get { return _viewListId; }
            set 
            {
                if (value == _viewListId) return;
                OnPropertyChanging(GlobalPropertyNames.ViewListId);
                _viewListId = value;
                OnPropertyChanged(GlobalPropertyNames.ViewListId);
            }
        }

        private CustomViewList _viewList;
        /// <summary>Представление используемое для списка элементов</summary>
        public CustomViewList ViewList
        {
            get
            {
                if (_viewListId == 0)
                    return null;
                if (_viewList == null)
                    _viewList = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_viewListId);
                else if (_viewList.Id != _viewListId)
                    _viewList = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_viewListId);
                return _viewList;
            }
            set
            {
                if (_viewList == value) return;
                OnPropertyChanging(GlobalPropertyNames.ViewList);
                _viewList = value;
                _viewListId = _viewList == null ? 0 : _viewList.Id;
                OnPropertyChanged(GlobalPropertyNames.ViewList);
            }
        }

        private int _viewListDocumentsId;
        /// <summary>Идентификатор списка для отображения документов</summary>
        public int ViewListDocumentsId
        {
            get { return _viewListDocumentsId; }
            set
            {
                if (value == _viewListDocumentsId) return;
                OnPropertyChanging(GlobalPropertyNames.ViewListDocumentsId);
                _viewListDocumentsId = value;
                OnPropertyChanged(GlobalPropertyNames.ViewListDocumentsId);
            }
        }

        private CustomViewList _viewListDocuments;
        /// <summary>Cписк для отображения документов</summary>
        /// <remarks>Используется для поиска документов</remarks>
        public CustomViewList ViewListDocuments
        {
            get
            {
                if (_viewListDocumentsId == 0)
                    return null;
                if (_viewListDocuments == null)
                    _viewListDocuments = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_viewListDocumentsId);
                else if (_viewListDocuments.Id != _viewListDocumentsId)
                    _viewListDocuments = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_viewListDocumentsId);
                return _viewListDocuments;
            }
            set
            {
                if (_viewListDocuments == value) return;
                OnPropertyChanging(GlobalPropertyNames.ViewListDocuments);
                _viewListDocuments = value;
                _viewListDocumentsId = _viewListDocuments == null ? 0 : _viewListDocuments.Id;
                OnPropertyChanged(GlobalPropertyNames.ViewListDocuments);
            }
        }

        private int _parentId;
        /// <summary>Идентификатор родителя</summary>
        public int ParentId
        {
            get { return _parentId; }
            set 
            {
                if (value == _parentId) return;
                OnPropertyChanging(GlobalPropertyNames.ParentId);
                _parentId = value;
                OnPropertyChanged(GlobalPropertyNames.ParentId);
            }
        }

        private Hierarchy _parent;
        /// <summary>Родитель</summary>
        public Hierarchy Parent
        {
            get
            {
                if (_parentId == 0)
                    return null;
                if (_parent == null)
                    _parent = Workarea.Cashe.GetCasheData<Hierarchy>().Item(_parentId);
                else if (_viewListId != 0 && _viewList != null && _viewList.Id != _viewListId)
                    _parent = Workarea.Cashe.GetCasheData<Hierarchy>().Item(_parentId);
                return _parent;
            }
            set
            {
                if (_parent == value) return;
                OnPropertyChanging(GlobalPropertyNames.Parent);
                _parent = value;
                _parentId = _parent == null ? 0 : _parent.Id;
                OnPropertyChanged(GlobalPropertyNames.Parent);
            }
        } 

        private int _rootId;
        /// <summary>Идентификатор корня</summary>
        public int RootId
        {
            get { return _rootId; }
            protected internal set { _rootId = value; }
        }

        private int _contentFlags;
        /// <summary>Разрешенное содержимое в виде флага</summary>
        public int ContentFlags
        {
            get { return _contentFlags; }
            set 
            {
                if (value == _contentFlags) return;
                OnPropertyChanging(GlobalPropertyNames.ContentFlags);
                _contentFlags = value;
                OnPropertyChanged(GlobalPropertyNames.ContentFlags);
            }
        }

        private string _path;
        /// <summary>Полный путь</summary>
        public string Path
        {
            get { return _path; }
            internal protected set { _path = value; }
        }

        private bool _hasContents;
        /// <summary>Наличие содержимого</summary>
        public bool HasContents
        {
            get { return _hasContents; }
            internal protected set { _hasContents = value; }
        }

        private bool _hasChildren;
        /// <summary>Наличие вложенных</summary>
        public bool HasChildren
        {
            get { return _hasChildren; }
            protected internal set { _hasChildren = value; }
        }

        private int _virtualBuildId;
        /// <summary>
        /// Идентификатор метода построения виртуальной иерархии
        /// </summary>
        public int VirtualBuildId
        {
            get { return _virtualBuildId; }
            set
            {
                if (value == _virtualBuildId) return;
                OnPropertyChanging(GlobalPropertyNames.VirtualBuildId);
                _virtualBuildId = value;
                OnPropertyChanged(GlobalPropertyNames.VirtualBuildId);
            }
        }
        /// <summary>
        /// Является корневых элементом виртуальной иерархии 
        /// </summary>
        public bool IsVirtualRoot
        {
            get { return KindValue == 2 && VirtualBuildId != 0; }
        }
        /// <summary>
        /// Является виртуальной иерархией
        /// </summary>
        public bool IsVirtual { get; set; }
        
        private List<Hierarchy> _children;
        /// <summary>Дочернии элементы</summary>
        public List<Hierarchy> Children
        {
            get { return _children ?? (_children = GetHierarchyChild(Id)); }
        }
        /// <summary>
        /// Обновить данные
        /// </summary>
        /// <param name="all">Выполнять комплексное обновление</param>
        public override void Refresh(bool all=true)
        {
            base.Refresh(all);
            if (all)
            {
                _children = GetHierarchyChild(Id);
                _contents = GetCollectionHierarchyContent(Id, ContentEntityId);
            }
        }

        private List<HierarchyContent> _contents;
        /// <summary>Коллекция содержимого иерархии</summary>
        public List<HierarchyContent> Contents
        {
            get
            {
                if (_hasContents &(_contents == null || _contents.Count==0) )
                    _contents = GetCollectionHierarchyContent(Id, ContentEntityId);
                return _contents ?? (_contents = new List<HierarchyContent>());
            }
        }
        /// <summary>
        /// Коллекиця объектов в иерархии
        /// </summary>
        /// <param name="id">Идентификатор иерархии</param>
        /// <param name="kind">Числовое значение типа объектов <see cref="EntityType.Id"/></param>
        /// <returns></returns>
        private List<HierarchyContent> GetCollectionHierarchyContent(int id, Int16 kind)
        {
            List<HierarchyContent> collection = new List<HierarchyContent>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.SmallInt).Value = kind;
                        cmd.CommandText = Workarea.Empty<Hierarchy>().Entity.FindMethod("Hierarchy.HierarchyContentLoadByHierarchyId").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HierarchyContent item = new HierarchyContent { Workarea = Workarea };
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
        /// <summary>
        /// Коллекция вложенных иерархий
        /// </summary>
        /// <param name="parentId">Идентификатор родительской иерархии</param>
        /// <returns></returns>
        private List<Hierarchy> GetHierarchyChild(int parentId)
        {

            List<Hierarchy> collection = new List<Hierarchy>();
            if (Workarea == null)
                return collection;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = parentId;
                        cmd.CommandText = Workarea.Empty<Hierarchy>().Entity.FindMethod(GlobalMethodAlias.HierarchyLoadChild).FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Hierarchy item = new Hierarchy { Workarea = Workarea };
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
        
        /// <summary>
        /// Коллекция корневой иерархии для типа
        /// </summary>
        /// <param name="kind">Идентификатор типа, по умолчанию 0 - коллекция корневых групп<see cref="EntityType.Id"/></param>
        /// <returns></returns>
        public List<Hierarchy> GetCollectionHierarchy(int kind=0)
        {

            List<Hierarchy> collection = new List<Hierarchy>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = kind;
                        cmd.CommandText = Workarea.Empty<Hierarchy>().Entity.FindMethod(GlobalMethodAlias.HierarchyLoadRoot).FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Hierarchy item = new Hierarchy { Workarea = Workarea };
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

        /// <summary>
        /// Коллекция иерархии для типа, являющихся поисковами группами документов.
        /// </summary>
        /// <param name="kind">Идентификатор типа <see cref="EntityType.Id"/></param>
        /// <returns></returns>
        public List<Hierarchy> GetCollectionHierarchyFind(int kind)
        {

            List<Hierarchy> collection = new List<Hierarchy>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = kind;
                        cmd.CommandText = "[Hierarchy].[HierarchiesLoadFindByEntityType]";
                        //Workarea.Empty<Hierarchy>().Entity.FindMethod(GlobalMethodAlias.HierarchyLoadRoot).FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Hierarchy item = new Hierarchy { Workarea = Workarea };
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
        
        // кеширование объектов в иерархии для повторного обращения
        private class CasheDataContent<T>
        {
            
            public CasheDataContent()
            {
                Data = new List<T>();
            }

            public List<T> Data{ get; set; }
            public List<T> DataNested { get; set; } 
        }

        private object _casheContents;
        /// <summary>Коллекция объектов в данной иерархии</summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <returns></returns>
        public List<T> GetTypeContents<T>(bool nested = false, bool refresh=false, bool byFlagString=false) where T : class, ICoreObject, new()//IBase, new()
        {
            lock (this)
            {
                if (refresh || _casheContents == null)
                {
                    _casheContents = new CasheDataContent<T>();
                    if (nested)
                        (_casheContents as CasheDataContent<T>).DataNested = RefreshTypeContents<T>(nested, byFlagString);
                    else
                        (_casheContents as CasheDataContent<T>).Data = RefreshTypeContents<T>(nested, byFlagString);
                }
                //if ((casheContents as CasheDataContent<T>).Nested != nested)
                //{
                //    casheContents = new CasheDataContent<T>();
                //    (casheContents as CasheDataContent<T>).Data = RefreshTypeContents<T>(nested, byFlagString);
                //    (casheContents as CasheDataContent<T>).Nested = nested;
                //}
                
            }
            if (nested)
            {
                if ((_casheContents as CasheDataContent<T>).DataNested==null)
                    (_casheContents as CasheDataContent<T>).DataNested = RefreshTypeContents<T>(nested, byFlagString);
                return (_casheContents as CasheDataContent<T>).DataNested;
            }
            else
            {
                if ((_casheContents as CasheDataContent<T>).Data==null)
                    (_casheContents as CasheDataContent<T>).Data = RefreshTypeContents<T>(nested, byFlagString);
                return (_casheContents as CasheDataContent<T>).Data;
            }
        }

        /// <summary>
        /// Добавить или обновить содержимое объектов в иерархии
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        internal void AddTypedContent<T>(T value)where T : class, ICoreObject, new()
        {
            if(_casheContents!=null)
            {
                if((_casheContents as CasheDataContent<T>).Data!=null)
                {
                    if((_casheContents as CasheDataContent<T>).Data.Contains(value))
                    {
                        int idx = (_casheContents as CasheDataContent<T>).Data.IndexOf(value);
                        (_casheContents as CasheDataContent<T>).Data[idx] = value;
                    }
                    else if((_casheContents as CasheDataContent<T>).Data.Exists(f=>f.Id==value.Id))
                    {
                        int idx = (_casheContents as CasheDataContent<T>).Data.FindIndex(f => f.Id == value.Id);
                        (_casheContents as CasheDataContent<T>).Data[idx] = value;
                    }
                    else
                    {
                        (_casheContents as CasheDataContent<T>).Data.Add(value);
                    }
                }
                if ((_casheContents as CasheDataContent<T>).DataNested != null)
                {
                    if ((_casheContents as CasheDataContent<T>).DataNested.Contains(value))
                    {
                        int idx = (_casheContents as CasheDataContent<T>).DataNested.IndexOf(value);
                        (_casheContents as CasheDataContent<T>).DataNested[idx] = value;
                    }
                    else if ((_casheContents as CasheDataContent<T>).DataNested.Exists(f => f.Id == value.Id))
                    {
                        int idx = (_casheContents as CasheDataContent<T>).DataNested.FindIndex(f => f.Id == value.Id);
                        (_casheContents as CasheDataContent<T>).DataNested[idx] = value;
                    }
                    else
                    {
                        (_casheContents as CasheDataContent<T>).DataNested.Add(value);
                    }
                }
            }
        }

        private List<T> RefreshTypeContents<T>(bool nested, bool byFlagString=false) where T : class, ICoreObject, new()
        {
            lock (this)
            {
                T item = new T {Workarea = Workarea};
                List<T> collection = new List<T>();
                using (SqlConnection cnn = Workarea.GetDatabaseConnection())
                {
                    if (cnn == null) return collection;

                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            string procedureName = string.Empty;
                            if (item.EntityId != 0)
                            {
                                ProcedureMap procedureMap = item.Entity.FindMethod(GlobalMethodAlias.LoadByHierarchies);
                                if (procedureMap != null)
                                {
                                    procedureName = procedureMap.FullName;
                                }
                            }
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = procedureName;
                            cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                            if (nested)
                                cmd.Parameters.Add(GlobalSqlParamNames.Nested, SqlDbType.Bit).Value = true;

                            if (byFlagString)
                                cmd.Parameters.Add(GlobalSqlParamNames.GroupByFlags, SqlDbType.Bit).Value = true;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                if (Workarea.Cashe.GetCasheData<T>().Exists(id))
                                {
                                    item = Workarea.Cashe.GetCasheData<T>().Item(id);
                                    item.Load(reader);
                                }
                                else
                                {
                                    item = new T { Workarea = Workarea };
                                    item.Load(reader);
                                    Workarea.Cashe.SetCasheData(item);
                                }
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

        /// <summary>Обновить коллекцию содержимого</summary>
        public void RefreshContents()
        {
            _contents = GetCollectionHierarchyContent(Id, EntityId);
        }
        /// <summary>Таблица данных, содержащая информацию на основе "представления"</summary>
        /// <returns></returns>
        public DataTable GetCustomView()
        {
            //return Workarea.GetCollectionCustomViewList(this);
            if (ViewList == null)
                return null;
            if (string.IsNullOrEmpty(ViewList.SystemName))
                return null;
            DataTable tbl = new DataTable();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        // TODO: Проверка... для папок поиска
                        if (ViewList.KindValue == 2 || ViewList.KindValue == 3)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add(GlobalSqlParamNames.KindType, SqlDbType.Int).Value = KindValue;
                            cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = Id;
                            cmd.CommandText = ViewList.SystemName;
                        }
                        else
                        {
                            cmd.CommandText = "select * from " + ViewList.SystemName;
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
        /// <summary>Таблица данных, содержащая идентификаторы объектов находящихся в "представлении"</summary>
        /// <returns></returns>
        public DataTable GetCustomViewIdList()
        {
            if (string.IsNullOrEmpty(ViewList.SystemName))
                return null;
            DataTable tbl = new DataTable { Locale = System.Threading.Thread.CurrentThread.CurrentCulture };
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {

                        switch (ViewList.KindValue)
                        {
                            case 2:
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = 0;
                                cmd.Parameters.Add(GlobalSqlParamNames.KindType, SqlDbType.Int).Value = KindValue;
                                cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = Id;
                                cmd.Parameters.Add(GlobalSqlParamNames.OnlyId, SqlDbType.Bit).Value = 1;
                                cmd.CommandText = ViewList.SystemName;
                                break;
                            case 4:
                                cmd.CommandText = "select * from " + ViewList.SystemName;
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
        /// Представление данных используемое иеархией 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataTable GetCustomView(CustomViewList list)
        {
            return Workarea.GetCollectionCustomViewList(list, 0, KindValue, Id);
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

            if (_contentFlags!=0)
                writer.WriteAttributeString(GlobalPropertyNames.ContentFlags, XmlConvert.ToString(_contentFlags));
            if (_hasContents)
                writer.WriteAttributeString(GlobalPropertyNames.HasContents, XmlConvert.ToString(_hasContents));
            if (_parentId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.ParentId, XmlConvert.ToString(_parentId));
            if (!string.IsNullOrEmpty(_path))
                writer.WriteAttributeString(GlobalPropertyNames.Path, _path);
            if (_rootId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.RootId, XmlConvert.ToString(_rootId));
            if (_viewListId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.ViewListId, XmlConvert.ToString(_viewListId));
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ContentFlags) != null)
                _contentFlags = XmlConvert.ToInt32(reader[GlobalPropertyNames.ContentFlags]);
            if (reader.GetAttribute(GlobalPropertyNames.HasContents) != null)
                _hasContents = XmlConvert.ToBoolean(reader[GlobalPropertyNames.HasContents]);
            if (reader.GetAttribute(GlobalPropertyNames.ParentId) != null)
                _parentId = XmlConvert.ToInt32(reader[GlobalPropertyNames.ParentId]);
            if (reader.GetAttribute(GlobalPropertyNames.Path) != null)
                _path = reader.GetAttribute(GlobalPropertyNames.Path);
            if (reader.GetAttribute(GlobalPropertyNames.RootId) != null)
                _rootId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RootId));
            if (reader.GetAttribute(GlobalPropertyNames.ViewListId) != null)
                _viewListId = XmlConvert.ToInt32(reader[GlobalPropertyNames.ViewListId]);
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion

        #region Состояние
        HierarchyStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new HierarchyStruct
                                  {
                                      ContentFlags = _contentFlags,
                                      HasContents = _hasContents,
                                      ParentId = _parentId,
                                      Path = _path,
                                      RootId = _rootId,
                                      ViewListId = _viewListId,
                                      MyCompanyId = _myCompanyId
                                  };
                return true;
            }
            return false;
        }
        /// <summary>Востановить состояние</summary>
        public override void RestoreState()
        {
            base.RestoreState();
            ContentFlags = _baseStruct.ContentFlags;
            HasContents = _baseStruct.HasContents;
            ParentId = _baseStruct.ParentId;
            Path = _baseStruct.Path;
            RootId = _baseStruct.RootId;
            ViewListId = _baseStruct.ViewListId;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion
        public override void Validate()
        {
            base.Validate();
            if(_rootId==0 && _parentId!=0)
            {
                if(Parent!=null)
                {
                    _rootId = Parent.RootId == 0 ? Parent.Id : Parent.RootId;
                }
            }
        }
        #region База данных
        /// <summary>Загрузить экземпляр из базы данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                //[DbEntityId],[ViewId],[ParentId],[RootId],[ContentFlags],[Path],[HasContents],SortOrder,ViewDocId, HasChild
                _contentEntityId = reader.GetInt16(17);
                _viewListId = reader.IsDBNull(18) ? 0 : reader.GetSqlInt32(18).Value;
                _parentId = reader.IsDBNull(19) ? 0 : reader.GetSqlInt32(19).Value;
                _rootId = reader.IsDBNull(20) ? 0 : reader.GetSqlInt32(20).Value;
                _contentFlags = reader.IsDBNull(21) ? 0 : reader.GetSqlInt32(21).Value;
                _path = reader.IsDBNull(22) ? null : reader.GetString(22);
                _hasContents = reader.GetBoolean(23);
                _orderNo = reader.GetInt16(24);
                _viewListDocumentsId = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                _hasChildren = reader.GetInt32(26) == 0 ? false : true;
                _virtualBuildId = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                _myCompanyId = reader.IsDBNull(28) ? 0 : reader.GetInt32(28);
                if (!endInit) return;
                OnEndInit();
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex); 
            }
            
        }
        /// <summary>Загрузить экземпляр из базы данных</summary>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            Load(value, Entity.FindMethod("Load").FullName);
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ViewId, SqlDbType.Int) {IsNullable = true};
            if (_viewListId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _viewListId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ParentId, SqlDbType.Int) {IsNullable = true};
            if (_parentId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _parentId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EntityId, SqlDbType.Int) { IsNullable = false, Value = _contentEntityId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ContentFlags, SqlDbType.Int) {IsNullable = false, Value = _contentFlags};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.SmallInt) {IsNullable = false, Value = _orderNo};
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ViewDocId, SqlDbType.Int) {IsNullable = true};
            if (_viewListDocumentsId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _viewListDocumentsId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RootId, SqlDbType.Int) { IsNullable = true };
            if (_rootId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _rootId;
            sqlCmd.Parameters.Add(prm);
            

            prm = new SqlParameter(GlobalSqlParamNames.VirtualBuildId, SqlDbType.Int) { IsNullable = true };
            if (_virtualBuildId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _virtualBuildId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion
        /// <summary>Первая иерархия в оторую включен объект</summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="value">Объект</param>
        /// <returns></returns>
        public static int? FirstHierarchy<T>(T value) where T : class, IBase, new()
        {
            int? res = default(int?);
            if (value.Id == 0)
                return res;
            ProcedureMap procedureMap = value.Entity.FindMethod("FirstHierarchy");
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureMap.FullName;
                    cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                    if (cmd.Connection.State== ConnectionState.Closed)
                        cmd.Connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        res = (Int32)obj;
                    }
                    if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                }
            }
            return res;
        }
        //List<HierarchyContentType> collValues = HierarchyContentType.GetCollection(SelectedItem.Workarea, SelectedItem.Id);
        private List<HierarchyContentType> _contentTypes;
        /// <summary>
        /// Коллекция допустимых типов для иерархии.
        /// </summary>
        /// <returns></returns>
        public List<HierarchyContentType> GetContentType()
        {
            return _contentTypes ?? (_contentTypes = HierarchyContentType.GetCollection(Workarea, Id));
        }

        /// <summary>
        /// Список идентификаторов разешенного содержимого иерархии
        /// </summary>
        /// <returns></returns>
        public List<int> GetContentTypeKindId()
        {
            List<int> val = new List<int>();
            foreach (HierarchyContentType contentType in GetContentType())
            {
                val.Add(contentType.EntityKindId);
            }
            return val;
        }
        /// <summary>
        /// Поиск иерархии по коду
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="code">Код</param>
        /// <param name="entityId">Идентификатор типа</param>
        /// <returns></returns>
        public static List<Hierarchy> FindByCode(Workarea wa, string code, short entityId)
        {
            List<Hierarchy> collection = new List<Hierarchy>();
            if (wa == null)
                return collection;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "[Hierarchy].[HierarchiesFindByCode]";//Entity.FindMethod("Hierarchy.Hierarchies").FullName;// "[Hierarchy].[ElementInHierarchies]";
                    cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;
                    cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = entityId;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Hierarchy item = new Hierarchy { Workarea = wa };
                            item.Load(reader);
                            wa.Cashe.SetCasheData(item);
                            collection.Add(item);
                        }
                    }
                    reader.Close();

                    if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                }
            }
            return collection;
        }

        private Dictionary<int, List<Hierarchy>> _casheObjectHierarhies = new Dictionary<int, List<Hierarchy>>();
        /// <summary>
        /// <summary>Коллекция иерархий в которые входит объект</summary>
        /// </summary>
        /// <remarks>Коллекция содержит данные с использованием внутреннего кеша объектов</remarks>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="value">Объект</param>
        /// <returns></returns>
        public List<Hierarchy> Hierarchies<T>(T value) where T : class, IBase, new()
        {
            return Hierarchies(value, false);
        }

        /// <summary>Коллекция иерархий в которые входит объект</summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="value">Объект</param>
        /// <param name="refresh">Обновить данные, при значении <c>true</c> выполняется обновление данных из базы данных, в 
        /// противном случае используется кеш объектов</param>
        /// <returns></returns>
        public List<Hierarchy> Hierarchies<T>(T value, bool refresh) where T : class, IBase, new()
        {
            if (_casheObjectHierarhies == null)
                _casheObjectHierarhies = new Dictionary<int, List<Hierarchy>>();

            if (!refresh && _casheObjectHierarhies.ContainsKey(value.Id))
            {
                return _casheObjectHierarhies[value.Id];
            }

            List<Hierarchy> collection = new List<Hierarchy>();
            if (value.Id == 0)
                return collection;
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = Entity.FindMethod("Hierarchy.Hierarchies").FullName;// "[Hierarchy].[ElementInHierarchies]";
                    cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                    cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = value.EntityId;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Hierarchy item = new Hierarchy {Workarea = Workarea};
                            item.Load(reader);
                            Workarea.Cashe.SetCasheData(item);
                            collection.Add(item);
                        }
                    }
                    reader.Close();

                    if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                }
            }
            if(_casheObjectHierarhies.ContainsKey(value.Id))
            {
                _casheObjectHierarhies[value.Id] = collection;
            }
            else
            {
                _casheObjectHierarhies.Add(value.Id, collection);
            }
            return collection;
        }

        /// <summary>Добавить содержимое иерархии</summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="value">Объект, который необходимо добавить</param>
        /// <param name="addToCashe">Добавлять объект в кешированную коллекцию содержимого</param>
        public void ContentAdd<T>(T value, bool addToCashe = false) where T : class, IBase, new()
        {
            ProcedureMap procedureMap = Entity.FindMethod("ContentAdd");
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureMap.FullName;
                    SqlParameter prmRes = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prmRes.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = Id;
                    cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = value.Id;
                    cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.Int).Value = value.EntityId;
                    cmd.Parameters.Add(GlobalSqlParamNames.DatabaseId, SqlDbType.Int).Value = Workarea.MyBranche.Id; 
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    object obj = prmRes.Value;
                    int res = (int)obj;
                    if (res == -1)
                    {
                        throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_CONTENTADD", 1049));
                    }
                    if(res!=0)
                    {
                        throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_CONTENTADD", 1049), res);
                    }
                    if (addToCashe)
                        if (_casheContents!=null)
                        {
                            if (!(_casheContents as CasheDataContent<T>).Data.Contains(value))
                                (_casheContents as CasheDataContent<T>).Data.Add(value);
                        }
                }
            }
        }
        public void Reorder<T>(T value, bool kind) where T : class, IBase, new()
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // TODO: Сопоставление метода
                        //Workarea.Empty<Hierarchy>().Entity.FindMethod("HierarchyContent.Copy").FullName;  //"[Hierarchy].[HierarchyContentCopy]";
                        cmd.CommandText = "Hierarchy.HierarchyContentMoveUpDown";

                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Bit).Value = kind;
                        cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = value.Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.Int).Value = value.EntityId;
                        SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                        prm.Direction = ParameterDirection.ReturnValue;
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((int)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }

        }
        /// <summary>Удалить содержимое иерархии</summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="value">Объект, который необходимо удалить</param>
        public void ContentRemove<T>(T value) where T : class, IBase, new()
        {
            ProcedureMap procedureMap = Entity.FindMethod("ContentRemove");
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureMap.FullName;
                    SqlParameter prmRes = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prmRes.Direction = ParameterDirection.ReturnValue;
                    
                    cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = Id;
                    cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = value.Id;
                    cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.Int).Value = value.EntityId;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    object obj = prmRes.Value;
                    if ((int)obj == -1)
                    {
                        throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_CONTENTREMOVE", 1049));
                    }
                }
            }
        }

        /// <summary>
        /// Проверка является ли иерархия дочерней по отношению к заданной
        /// </summary>
        /// <param name="parent">Заданная иерархия</param>
        /// <returns></returns>
        public bool IsChildTo(Hierarchy parent)
        {
            Hierarchy current = this;
            while(current!=null)
            {
                if (current.ParentId == parent.Id)
                    return true;
                current = current.Parent;
            }
            return false;
        }

        #region ILinks<Hierarchy> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<Hierarchy>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Hierarchy>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Hierarchy> IChains<Hierarchy>.SourceList(int chainKindId)
        {
            return Chain<Hierarchy>.GetChainSourceList(this, chainKindId);
        }
        List<Hierarchy> IChains<Hierarchy>.DestinationList(int chainKindId)
        {
            return Chain<Hierarchy>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Hierarchy>> GetValues(bool allKinds)
        {
            return CodeHelper<Hierarchy>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Hierarchy>.GetView(this, true);
        }
        #endregion

        #region IChainsAdvancedList<Hierarchy, Agent> Members
        List<IChainAdvanced<Hierarchy, Agent>> IChainsAdvancedList<Hierarchy, Agent>.GetLinks()
        {
            return ((IChainsAdvancedList<Hierarchy, Agent>)this).GetLinks(42);
        }

        List<IChainAdvanced<Hierarchy, Agent>> IChainsAdvancedList<Hierarchy, Agent>.GetLinks(int? kind)
        {
            return GetLinkedHierarchy();
        }
        List<ChainValueView> IChainsAdvancedList<Hierarchy, Agent>.GetChainView()
        {
            return ChainValueView.GetView<Hierarchy, Agent>(this);
        }

        private List<IChainAdvanced<Hierarchy, Agent>> collectionLinkedHierarchies;
        private bool isCollectionLinkedHierarchiesDone;
        /// <summary>
        /// Коллекция разрешенных групп.
        /// </summary>
        /// <returns></returns>
        public List<IChainAdvanced<Hierarchy, Agent>> GetLinkedHierarchy(bool refresh=false)
        {
            if (collectionLinkedHierarchies==null)
                collectionLinkedHierarchies = new List<IChainAdvanced<Hierarchy, Agent>>();
            if (!refresh && isCollectionLinkedHierarchiesDone)
                return collectionLinkedHierarchies;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Hierarchy>().Entity.FindMethod("HierarchyAgentChainLoadBySource").FullName;
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
                                ChainAdvanced<Hierarchy, Agent> item = new ChainAdvanced<Hierarchy, Agent> { Workarea = Workarea, Left = this };
                                item.Load(reader);
                                collectionLinkedHierarchies.Add(item);
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
                        isCollectionLinkedHierarchiesDone = true;
                    }

                }
            }
            return collectionLinkedHierarchies;
        }

        #endregion
        #region IChainsAdvancedList<Hierarchy,Note> Members

        List<IChainAdvanced<Hierarchy, Note>> IChainsAdvancedList<Hierarchy, Note>.GetLinks()
        {
            return ChainAdvanced<Hierarchy, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Hierarchy, Note>> IChainsAdvancedList<Hierarchy, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Hierarchy, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Hierarchy, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Hierarchy, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Hierarchy, Note>.GetChainView()
        {
            return ChainValueView.GetView<Hierarchy, Note>(this);
        }
        #endregion
    }
}
