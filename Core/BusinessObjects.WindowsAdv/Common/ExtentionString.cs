using System.Collections.Generic;
using BusinessObjects.Windows.Properties;
using System.Linq;
namespace BusinessObjects.Windows
{
    internal static class ExtentionString
    {
// ReSharper disable InconsistentNaming
        /// <summary>Страница "Главная"</summary>
        public const string CONTROL_COMMON_NAME = "PAGECOMMON";    // 0x0001
        /// <summary>Страница идентификаторов</summary>
        public const string CONTROL_ID_NAME = "PAGEID";            // 0x0002
        public const string CONTROL_FACT_NAME = "PAGEFACTVALUES";  // 0x0004
        public const string CONTROL_LINK_NAME = "PAGELINKS";       // 0x0008
        public const string CONTROL_BANKACC_NAME = "PAGEBANKACC";  // 0x0016
        /// <summary>Страница состояний</summary>
        public const string CONTROL_STATES_NAME = "PAGESTATES";    // 0x0032
        public const string CONTROL_COLUMNS_NAME = "PAGECOLUMNS";  // 0x0064
        public const string CONTROL_UNITS_NAME = "PAGEUNITS";      // 0x0128
        /// <summary>Наименование элемента управления закладки "Ценовые диапазоны" для товара и цида цен</summary>
        public const string CONTROL_PRICEREGION_NAME = "PAGEPRICEREGIONS";
        public const string CONTROL_DBENTITYKIND_NAME = "PAGEDBENTITYKINDS";            // 0x0256
        public const string CONTROL_DBENTITYMETHOD_NAME = "PAGEDBENTITYMETHOD";         // 0x0512
        public const string CONTROL_USERGROUPS_NAME = "PAGEUSERGROUPS";                 // 0x1024
        public const string CONTROL_USERRIGHTSEL_NAME = "PAGEUSERRIGHTSELEMENTS";       // 0x2048
        public const string CONTROL_HIERARCHIES_NAME = "PAGEHIERARCHIES";               // 0x4096
        public const string CONTROL_RIGHTDBENTITY_NAME = "PAGEUSERRIGHTSDBENTITY";      // 0x8192
        public const string CONTROL_FLAGS_NAME = "PAGEFLAGSNAME";                       // 0x16384
        public const string CONTROL_CONTACT_NAME = "PAGECONTACT";                       // 0x32768
        public const string CONTROL_PROPERTY_NAME = "PAGEPROPERTY";                     // 0x65536
        public const string CONTROL_USERS_PARAMETERS = "PAGEUSERSPARAMETERS";           // 0x131072
        public const string CONTROL_COUNTRYREGIONS = "PAGECOUNTRYREGIONS";              // 0x262144
        public const string CONTROL_TOWNS = "PAGETOWNS";                                // 0x524288
        /// <summary>Наименование элемента управления закладки "Банк" для корреспондента</summary>
        public const string CONTROL_AGENTBANK = "PAGEAGENTBANK";                        // 0x524288
        public const string CONTROL_ADDRESSINFO = "PAGEADDRESS";                        // 0x524288
        public const string CONTROL_REGIONS = "PAGEREGIONS";                            // 0x1048576
        public const string CONTROL_FACTCOLUMN = "PAGEFACTCOLUMNS";                     // 0x2097152
        public const string CONTROL_AGENT_CODES = "PAGEAGENTCODES";                     // 0x4194304
        public const string CONTROL_AGENT_PASSPORT = "PAGEAGENTPASSPORT";               // 0x8388608
        public const string CONTROL_AGENT_DRIVINGLICENCE = "PAGEAGENTDRIVINGLICENCE";   // 0x16777216
        public const string CONTROL_SERIES_NAME = "PAGESERIES";                         // 0x33554432
        public const string CONTROL_LIBRARY_PARAMS = "PAGELIBRARYPARAMS";               // 0x67108864
        public const string CONTROL_LINKRULESET = "PAGELINKRULESET";                    // 0x134217728
        public const string CONTROL_LINKXMLSTORAGE = "PAGELINKXMLSTORAGE";                    // 0x134217728
        public const string CONTROL_LOGACTION = "PAGELOGACTION";
        /// <summary>Страница связанных файлов</summary>
        public const string CONTROL_LINKFILES = "PAGELINKFILES";                        // 0x268435456
        public const string CONTROL_SETUP = "PAGESETUP";
        public const string CONTROL_LIBCOMPOSITION = "PAGELIBCOMPOSITION";
        public const string CONTROL_CALCULATE = "PAGECALCULATE";
        public const string CONTROL_FACTCOLUMNENTITYKIND = "PAGEFACTCOLUMNENTITYKINDS";
        public const string CONTROL_LINKUIDAGENT = "PAGELINKUIDAGENT";                    // 0x134217728
        /// <summary>Страница дополнительных кодов</summary>
        public const string CONTROL_CODES = "CONTROL_CODES";                             //0x536870912
        public const string CONTROL_KNOWLEDGES = "PAGEKNOWLEDGES";                             //0x536870912
        /// <summary>Страница примечаний</summary>
        public const string CONTROL_NOTES = "PAGENOTES";
        /// <summary>Страница связанных сообщений</summary>
        public const string CONTROL_MESSAGES = "PAGELINKMESSAGE";
        /// <summary>Страница связанных документов</summary>
        public const string CONTROL_LINKDOCUMENTS = "PAGELINKDOCUMENTS";
        /// <summary>Страница связанных событий</summary>
        public const string CONTROL_LINKEVENTS = "PAGELINKEVENTS";
        public const string CONTROL_LINKTOWNS = "PAGELINKTOWNS";
        public const string CONTROL_LINKWORKER = "PAGELINKWORKER";
        
        
        
// ReSharper restore InconsistentNaming
        private static readonly Dictionary<string, string> DictionaryPageHeader;
        static ExtentionString()
        {
            DictionaryPageHeader = new Dictionary<string, string>
                                        {
                                            {CONTROL_ID_NAME, Resources.STR_LABEL_PAGEID},
                                            {CONTROL_COMMON_NAME, Resources.STR_LABEL_PAGECOMMON},
                                            {CONTROL_LINK_NAME, Resources.STR_LABEL_PAGE_LINKS},
                                            {CONTROL_FACT_NAME, Resources.STR_LABEL_PAGEFACTS},
                                            {CONTROL_HIERARCHIES_NAME, Resources.STR_LABEL_PAGEHIERARCHY},
                                            {CONTROL_UNITS_NAME, Resources.STR_LABEL_PAGEPRODUNITS},
                                            {CONTROL_STATES_NAME, Resources.STR_LABEL_PAGESTATE},
                                            {CONTROL_BANKACC_NAME, Resources.SRT_LABEL_PAGE_BANKACC},
                                            {CONTROL_AGENTBANK,"Банковкая информация"},
                                            {CONTROL_AGENT_CODES, "Регистрационные коды"},
                                            {CONTROL_AGENT_PASSPORT, "Паспорта"},
                                            {CONTROL_AGENT_DRIVINGLICENCE, "Водительские удостоверения"},
                                            {CONTROL_SERIES_NAME, "Партии"},
                                            {CONTROL_LIBRARY_PARAMS, "Дополнительные параметры"},
                                            {CONTROL_SETUP, "Настройка"},
                                            {CONTROL_LINKFILES, "Файлы"},
                                            {CONTROL_LOGACTION, "Протокол"},
                                            {CONTROL_USERGROUPS_NAME, "Группы пользователей"},
                                            {CONTROL_CONTACT_NAME, "Контакты"},
                                            {CONTROL_COLUMNS_NAME, "Колонки"},
                                            {CONTROL_DBENTITYKIND_NAME, "Подтипы"},
                                            {CONTROL_DBENTITYMETHOD_NAME, "Методы"},
                                            {CONTROL_FLAGS_NAME, "Аттрибуты"},
                                            {CONTROL_RIGHTDBENTITY_NAME, "Общие разрешения"},
                                            {CONTROL_USERS_PARAMETERS, "Пользовательские параметры"},
                                            {CONTROL_LIBCOMPOSITION, "Состав библиотеки"},
                                            {CONTROL_COUNTRYREGIONS, "Области"},
                                            {CONTROL_TOWNS, "Города"},
                                            {CONTROL_REGIONS, "Районы"},
                                            {CONTROL_CALCULATE, "Расчет"},
                                            {CONTROL_FACTCOLUMN, "Колонки"},
                                            {CONTROL_FACTCOLUMNENTITYKIND,"Типы сущности"},
                                            {CONTROL_LINKRULESET, "Правила и процессы"},
                                            {CONTROL_LINKXMLSTORAGE, "Расположение элементов"},
                                            {CONTROL_PRICEREGION_NAME, "Ценовые диапазоны"},
                                            {CONTROL_LINKUIDAGENT, "Область видимости"},
                                            {CONTROL_CODES, "Дополнительные коды"},
                                            {CONTROL_KNOWLEDGES, "Статьи"},
                                            {CONTROL_ADDRESSINFO, "Адреса"},
                                            {CONTROL_NOTES, "Примечания"},
                                            {CONTROL_MESSAGES, "Сообщения"},
                                            {CONTROL_LINKDOCUMENTS, "Документы"},
                                            {CONTROL_LINKEVENTS, "События"},
                                            {CONTROL_LINKTOWNS, "Связанные города"},
                                            {CONTROL_LINKWORKER, "Сотрудники"}
                                            
                                        };
        }
        public static string GetPageNameByKey(string key)
        {
            return DictionaryPageHeader.ContainsKey(key) ? DictionaryPageHeader[key] : key;
        }

        public static string GetPageNameByKey(Workarea wa, string key)
        {
            Library lib = wa.CollectionPages.FirstOrDefault(s => s.Code == key);
            if(lib!=null)
            {
                return lib.CodeFind;
            }
            else
              return GetPageNameByKey(key);
        }
    }
}
