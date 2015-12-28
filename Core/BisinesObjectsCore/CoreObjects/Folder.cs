using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using BusinessObjects.Documents;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Папка"</summary>
    internal struct FolderStruct
    {
        /// <summary>Идентификатор основной формы документов</summary>
        public int FormId;
        /// <summary>Идентификатор шаблона операции</summary>
        public int DocumentId;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>
    /// Папка документов
    /// </summary>
    public sealed class Folder : BaseCore<Folder>, IChains<Folder>, IReportChainSupport, IEquatable<Folder>,
        ICodes<Folder>,
        IChainsAdvancedList<Folder, Note>, ICompanyOwner
    {
        // ReSharper disable InconsistentNaming
        #region Константы типов и подтипов
        
        /// <summary>Папка, соответствует значению 1</summary>
        public const int KINDVALUE_FOLDER = 1;

        /// <summary>Папка, соответствует значению 458753</summary>
        public const int KINDID_FOLDER = 458753;
        
        #endregion

        #region Константы кодов раздела "Торговля"
        /// <summary>Счет исходящий с НДС</summary>
        public const string CODE_FIND_SALES_OUT_ACCOUNT_NDS = "FLD_SALES_OUT_ACCOUNT_NDS";
        /// <summary>Расходная накладная с НДС</summary>
        public const string CODE_FIND_SALES_OUT_NDS = "FLD_SALES_OUT_NDS";
        /// <summary>Заказ входящий с НДС</summary>
        public const string CODE_FIND_SALES_IN_ORDER_NDS = "FLD_SALES_IN_ORDER_NDS";
        /// <summary>Возврат товаров от покупателя с НДС</summary>
        public const string CODE_FIND_SALES_IN_RETURN_NDS = "FLD_SALES_IN_RETURN_NDS";
        /// <summary>Приходная накладная с НДС</summary>
        public const string CODE_FIND_SALES_IN_NDS = "FLD_SALES_IN_NDS";
        /// <summary>Возврат товара поставщику с НДС</summary>
        public const string CODE_FIND_SALES_OUT_RETURN_NDS = "FLD_SALES_OUT_RETURN_NDS";
        /// <summary>Заказ поставщику с НДС</summary>
        public const string CODE_FIND_SALES_OUT_ORDER_NDS = "FLD_SALES_OUT_ORDER_NDS";
        /// <summary>Счет входящий с НДС</summary>
        public const string CODE_FIND_SALES_IN_ACCOUNT_NDS = "FLD_SALES_IN_ACCOUNT_NDS";
        /// <summary>Ассортиментный лист с НДС</summary>
        public const string CODE_FIND_SALES_OUT_ASSORT_NDS = "FLD_SALES_OUT_ASSORT_NDS";
        /// <summary>Расходная накладная без НДС</summary>
        public const string CODE_FIND_SALES_OUT = "FLD_SALES_OUT";
        /// <summary>Приходная накладная без НДС</summary>
        public const string CODE_FIND_SALES_IN = "FLD_SALES_IN";
        /// <summary>Заказ покупателя без НДС</summary>
        public const string CODE_FIND_SALES_ORDER_IN = "FLD_SALES_ORDER_IN";
        /// <summary>Ассортиментный лист поставщика без НДС</summary>
        public const string CODE_FIND_SALES_IN_ASSORT_NDS = "FLD_SALES_IN_ASSORT_NDS";
        /// <summary>Исходящий счет без НДС</summary>
        public const string CODE_FIND_SALES_ACCOUNT_OUT = "FLD_SALES_ACCOUNT_OUT";
        /// <summary>Входящий счет без НДС</summary>
        public const string CODE_FIND_SALES_ACCOUNT_IN = "FLD_SALES_ACCOUNT_IN";
        /// <summary>Заказ поставщику без НДС</summary>
        public const string CODE_FIND_SALES_ORDER_OUT = "FLD_SALES_ORDER_OUT";
        /// <summary>Ассортиментный лист без НДС</summary>
        public const string CODE_FIND_SALES_ASSORT_OUT = "FLD_SALES_ASSORT_OUT";
        /// <summary>Ассортиментный лист поставщика без НДС</summary>
        public const string CODE_FIND_SALES_ASSORT_IN = "FLD_SALES_ASSORT_IN";
        /// <summary>Возврат товара от покупателя без НДС</summary>
        public const string CODE_FIND_SALES_RETURN_IN = "FLD_SALES_RETURN_IN";
        /// <summary>Возврат товара поставщику без НДС</summary>
        public const string CODE_FIND_SALES_RETURN_OUT = "FLD_SALES_RETURN_OUT";
        /// <summary>Внутреннее перемещение без НДС</summary>
        public const string CODE_FIND_SALES_MOVE = "FLD_SALES_MOVE";
        /// <summary>Инвентаризация без НДС</summary>
        public const string CODE_FIND_SALES_INVENTORY = "FLD_SALES_INVENTORY";
        /// <summary>Внутреннее перемещение с НДС</summary>
        public const string CODE_FIND_SALES_MOVE_NDS = "FLD_SALES_MOVE_NDS";
        /// <summary>Акт списания товара с НДС</summary>
        public const string CODE_FIND_SALES_OUT_ACT_NDS = "FLD_SALES_OUT_ACT_NDS";
        /// <summary>Акт списания товара без НДС</summary>
        public const string CODE_FIND_SALES_ACT_OUT = "FLD_SALES_ACT_OUT";
        /// <summary>Акт прихода товара с НДС</summary>
        public const string CODE_FIND_SALES_IN_ACT_NDS = "FLD_SALES_IN_ACT_NDS";
        /// <summary>Акт прихода товара с НДС</summary>
        public const string CODE_FIND_SALES_ACT_IN = "FLD_SALES_ACT_IN";
        /// <summary>Настройки раздела</summary>
        public const string CODE_FIND_SALES_CONFIG = "FLD_SALES_CONFIG";
        #endregion

        #region Константы папок докуентов раздела "Ценовая политика"
        /// <summary>Документ ценообразования предназначен установки цен на наших предприятиях.</summary>
        public const string CODE_FIND_PRICES_DEAULT = "FLD_PRICE";
        /// <summary>Документ ценообразования предназначен для установки персональных цен для определенного клиента. В основном это наши постоянные клиенты имеющие персональную политику формирования цены.</summary>
        public const string CODE_FIND_PRICES_IND = "FLD_PRICE_IND";
        /// <summary>Прайс-листы наших поставщиков предназначен для дальнейшего анализа изменений цен и быстрой подстановки цены в приходных документах.</summary>
        public const string CODE_FIND_PRICES_SYPPLYER = "FLD_PRICE_SYPPLYER";
        /// <summary>Прайс-лист наших конкурентов для указанного клиента предназначен для дальнейшего анализа изменений цен и корректировки собственных цен.</summary>
        public const string CODE_FIND_PRICES_COMPETITOR = "FLD_PRICE_COMPETITOR";
        /// <summary>Прайс-лист наших конкурентов предназначен для дальнейшего анализа изменений цен и корректировки собственных цен.</summary>
        public const string CODE_FIND_PRICES_COMPETITORIND = "FLD_PRICE_COMPETITOR_IND";

        /// <summary>Приказ ценообразования на наших предприятиях.</summary>
        public const string CODE_FIND_PRICES_COMMAND = "FLD_PRICE_COMMAND";
        /// <summary>Приказ ценообразования для установки персональных цен для определенного клиента. В основном это наши постоянные клиенты имеющие персональную политику формирования цены.</summary>
        public const string CODE_FIND_PRICES_COMMANDIND = "FLD_PRICE_COMMANDIND";
        /// <summary>Настройки раздела</summary>
        public const string CODE_FIND_PRICE_CONFIG = "FLD_PRICE_CONFIG";
        #endregion

        #region Константы папок раздела "Управление финансами"
        /// <summary>Оплата от покупателя</summary>
        public const string CODE_FIND_FINANCE_IN = "FLD_FINANCE_IN";
        /// <summary>Оплата поставщику </summary>
        public const string CODE_FIND_FINANCE_OUT = "FLD_FINANCE_OUT";
        /// <summary>Прочие поступление денежных средств</summary>
        public const string CODE_FIND_FINANCE_IN_CUSTOM = "FLD_FINANCE_IN_CUSTOM";
        /// <summary>Возврат денежных средств покупателю </summary>
        public const string CODE_FIND_FINANCE_OUT_RETURN = "FLD_FINANCE_OUT_RETURN";
        /// <summary>Прочий расход денежных средств</summary>
        public const string CODE_FIND_FINANCE_OUT_CUSTOM = "FLD_FINANCE_OUT_CUSTOM";

        /// <summary>Оплата от покупателя</summary>
        public const string CODE_FIND_FINANCE_IN_NDS = "FLD_FINANCE_IN_NDS";
        /// <summary>Оплата поставщику </summary>
        public const string CODE_FIND_FINANCE_OUT_NDS = "FLD_FINANCE_OUT_NDS";
        /// <summary>Прочие поступление денежных средств</summary>
        public const string CODE_FIND_FINANCE_IN_CUSTOM_NDS = "FLD_FINANCE_IN_CUSTOM_NDS";
        /// <summary>Возврат денежных средств покупателю </summary>
        public const string CODE_FIND_FINANCE_OUT_RETURN_NDS = "FLD_FINANCE_OUT_RETURN_NDS";
        /// <summary>Прочий расход денежных средств</summary>
        public const string CODE_FIND_FINANCE_OUT_CUSTOM_NDS = "FLD_FINANCE_OUT_CUSTOM_NDS";
        /// <summary>Настройки раздела</summary>
        public const string CODE_FIND_FINANCE_CONFIG = "FLD_FINANCE_CONFIG";
        #endregion

        #region Константы папок раздела "Налоговые"
        /// <summary>Входящие налоговые</summary>
        public const string CODE_FIND_TAX_IN = "FLD_TAX_IN";
        /// <summary>Исходящие налоговые</summary>
        public const string CODE_FIND_TAX_OUT = "FLD_TAX_OUT";
        /// <summary>Входящие корректировочные налоговые</summary>
        public const string CODE_FIND_TAX_CORIN = "FLD_TAX_IN_COR";
        /// <summary>Исходящие корректировочные налоговые</summary>
        public const string CODE_FIND_TAX_COROUT = "FLD_TAX_OUT_COR";
        /// <summary>Настройки раздела</summary>
        public const string CODE_FIND_TAX_CONFIG = "FLD_TAX_CONFIG";
        
        #endregion

        #region Константы папок раздела "Услуги"
        /// <summary>Акт выполненых работ исходящий</summary>
        public const string CODE_FIND_SERVICE_OUT_NDS = "FLD_SERVICE_OUT_NDS";
        /// <summary>Акт выполненых работ входящий</summary>
        public const string CODE_FIND_SERVICE_IN_NDS = "FLD_SERVICE_IN_NDS";
        /// <summary>Счет на услуги исходящий</summary>
        public const string CODE_FIND_SERVICE_OUT_ACCOUNT_NDS = "FLD_SERVICE_OUT_ACCOUNT_NDS";
        /// <summary>Счет на улуги входящий</summary>
        public const string CODE_FIND_SERVICE_IN_ACCOUNT_NDS = "FLD_SERVICE_IN_ACCOUNT_NDS";
        /// <summary>Заказ улуг поставщику</summary>
        public const string CODE_FIND_SERVICE_OUT_ORDER_NDS = "FLD_SERVICE_OUT_ORDER_NDS";
        /// <summary>Заказ услуг входящий</summary>
        public const string CODE_FIND_SERVICE_IN_ORDER_NDS = "FLD_SERVICE_IN_ORDER_NDS";

        /// <summary>Акт выполненых работ исходящий</summary>
        public const string CODE_FIND_SERVICE_OUT = "FLD_SERVICE_OUT";
        /// <summary>Акт выполненых работ входящий </summary>
        public const string CODE_FIND_SERVICE_IN = "FLD_SERVICE_IN";
        /// <summary>Счет на услуги исходящий</summary>
        public const string CODE_FIND_SERVICE_OUT_ACCOUNT = "FLD_SERVICE_OUT_ACCOUNT";
        /// <summary>Счет на улуги входящий</summary>
        public const string CODE_FIND_SERVICE_IN_ACCOUNT = "FLD_SERVICE_IN_ACCOUNT";
        /// <summary>Заказ улуг поставщику</summary>
        public const string CODE_FIND_SERVICE_OUT_ORDER = "FLD_SERVICE_OUT_ORDER";
        /// <summary>Заказ услуг входящий</summary>
        public const string CODE_FIND_SERVICE_IN_ORDER = "FLD_SERVICE_IN_ORDER";
        /// <summary>Настройки раздела</summary>
        public const string CODE_FIND_SERVICE_CONFIG = "FLD_SERVICE_CONFIG";
        #endregion

        #region Константы раздела "Маркетинг"
        /// <summary>Анкета клиента</summary>
        public const string CODE_FIND_MKTG_MRAK = "FLD_MKTG_MRAK";
        /// <summary>Анкетирование-Вопросы</summary>
        public const string CODE_FIND_MKTG_ANKW = "FLD_MKTG_ANKW";
        /// <summary>Настройки раздела</summary>
        public const string CODE_FIND_MKTG_CONFIG = "FLD_MKTG_CONFIG";
        #endregion

        #region Константы раздела "Договора"
        /// <summary>Договора клиентов</summary>
        public const string CODE_FIND_CONTRACTS_CONTRACT = "FLD_CONTRACTS_CONTRACT";
        /// <summary>Акт ревизии арендного оборудования</summary>
        public const string CODE_FIND_CONTRACTS_REVISION = "FLD_CONTRACTS_REVISION";
        /// <summary>Акт сверки клиентов</summary>
        public const string CODE_FIND_CONTRACTS_VERIFICATION = "FLD_CONTRACTS_VERIFICATION";
        /// <summary>Счета заявки</summary>
        public const string CODE_FIND_CONTRACTS_ACCOUNTIN = "FLD_CONTRACTS_ACCOUNTIN";
        /// <summary>Договор поставки</summary>
        public const string CODE_FIND_CONTRACTS_SALEOUT = "FLD_CONTRACTS_SALEOUT";
        /// <summary>Учет компьютеров</summary>
        public const string CODE_FIND_CONTRACTS_COMPUTER = "FLD_CONTRACTS_COMPUTER";
        /// <summary>Учет принтеров</summary>
        public const string CODE_FIND_CONTRACTS_PRINTER = "FLD_CONTRACTS_PRINTERS";
        /// <summary>Служебные записки</summary>
        public const string CODE_FIND_CONTRACTS_OFFICIALNOTE = "FLD_CONTRACTS_OFFICIALNOTE";
        /// <summary>Настройки раздела</summary>
        public const string CODE_FIND_CONTRACTS_CONFIG = "FLD_CONTRACTS_CONFIG";
        #endregion

        #region Константы раздела "Финансовое планирование"
        /// <summary>Заявка на приобретение</summary>
        public const string CODE_FIND_FINPLAN_ORDER = "FLD_FINPLAN_ORDER";
        /// <summary>Окончательная заявка на приобретение</summary>
        public const string CODE_FIND_FINPLAN_ORDERTOTAL = "FLD_FINPLAN_ORDERTOTAL";
        /// <summary>Финальная заявка на приобретение</summary>
        public const string CODE_FIND_FINPLAN_ORDERFINAL = "FLD_FINPLAN_ORDERFINAL";
        /// <summary>Настройки раздела</summary>
        public const string CODE_FIND_FINPLAN_CONFIG = "FLD_FINPLAN_CONFIG";
        #endregion

        #region Константы раздела "Маршруты"
        /// <summary>Маршруты</summary>
        public const string CODE_FIND_ROUTE_ROUTE = "FLD_ROUTE_ROUTE";
        /// <summary>Настройки раздела</summary>
        public const string CODE_FIND_ROUTE_CONFIG = "FLD_ROUTE_CONFIG";
        #endregion
        
        #region Константы раздела "Бухгалтерия"
        /// <summary>Настройки раздела</summary>
        public const string CODE_FIND_BOOKKEEP_CONFIG = "FLD_BOOKKEEP_CONFIG";
        #endregion
        
        // ReSharper restore InconsistentNaming
        bool IEquatable<Folder>.Equals(Folder other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public Folder(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Folder;
        }
        protected override void CopyValue(Folder template)
        {
            base.CopyValue(template);
            DocumentId = template.DocumentId;
            ViewListDocumentsId = template.ViewListDocumentsId;
            FormId = template.FormId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Folder Clone(bool endInit)
        {
            Folder obj = base.Clone(false);
            obj.DocumentId = DocumentId;
            obj.ViewListDocumentsId = ViewListDocumentsId;
            obj.FormId = FormId;

            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства
        private int _formId;
        /// <summary>
        /// Идентификатор основной формы документов
        /// </summary>
        public int FormId
        {
            get { return _formId; }
            set
            {
                if (value == _formId) return;
                OnPropertyChanging(GlobalPropertyNames.FormId);
                _formId = value;
                if (_projectItem != null)
                    _projectItem = Workarea.Cashe.GetCasheData<Library>().Item(_formId);

                OnPropertyChanged(GlobalPropertyNames.FormId);
            }
        }

        private Library _projectItem;
        /// <summary>
        /// Форма
        /// </summary>
        public Library ProjectItem
        {
            get
            {
                if (_formId == 0)
                    return null;
                if (_projectItem == null)
                    _projectItem = Workarea.Cashe.GetCasheData<Library>().Item(_formId);
                else if (_projectItem.Id != _formId)
                    _projectItem = Workarea.Cashe.GetCasheData<Library>().Item(_formId);
                return _projectItem;
            }
            set
            {
                if (_projectItem == value) return;
                OnPropertyChanging(GlobalPropertyNames.ProjectItem);
                _projectItem = value;
                _formId = _projectItem == null ? 0 : _projectItem.Id;
                OnPropertyChanged(GlobalPropertyNames.ProjectItem);
            }
        }

        private int _documentId;
        /// <summary>
        /// Идентификатор шаблона операции
        /// </summary>
        public int DocumentId
        {
            get { return _documentId; }
            set
            {
                if (value == _documentId) return;
                OnPropertyChanging(GlobalPropertyNames.DocumentId);
                _documentId = value;
                OnPropertyChanged(GlobalPropertyNames.DocumentId);
            }
        }

        private Document _document;
        /// <summary>
        /// Шаблон документа 
        /// </summary>
        public Document Document
        {
            get
            {
                if (_documentId == 0)
                    return null;
                if (_document == null)
                    _document = Workarea.Cashe.GetCasheData<Document>().Item(_documentId);
                else if (_document.Id != _documentId)
                    _document = Workarea.Cashe.GetCasheData<Document>().Item(_documentId);
                return _document;
            }
            set
            {
                if (_document == value) return;
                OnPropertyChanging(GlobalPropertyNames.Document);
                _document = value;
                _documentId = _document == null ? 0 : _document.Id;
                OnPropertyChanged(GlobalPropertyNames.Document);
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
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_formId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FormId, XmlConvert.ToString(_formId));
            if (_documentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DocumentId, XmlConvert.ToString(_documentId));
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.FormId) != null)
                _formId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FormId));
            if (reader.GetAttribute(GlobalPropertyNames.DocumentId) != null)
                _documentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DocumentId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion

        #region Состояние
        FolderStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new FolderStruct {FormId = _formId, DocumentId = _documentId, MyCompanyId=_myCompanyId};
                return true;
            }
            return false;
        }
        /// <summary>
        /// Востановить состояние объекта
        /// </summary>
        public override void RestoreState()
        {
            base.RestoreState();
            FormId = _baseStruct.FormId;
            DocumentId = _baseStruct.DocumentId;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion

        #region База данных
        /// <summary>Загрузить экземпляр из базы данных</summary>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _formId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _documentId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _viewListDocumentsId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _myCompanyId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
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
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.FormId, SqlDbType.Int) {IsNullable = true};
            if (_formId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _formId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OperationId, SqlDbType.Int) {IsNullable = true};
            if (_documentId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _documentId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ViewDocId, SqlDbType.Int) { IsNullable = true };
            if (_viewListDocumentsId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _viewListDocumentsId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Folder> Members
        /// <summary>
        /// Связи папки
        /// </summary>
        /// <returns></returns>
        public List<IChain<Folder>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи папки
        /// </summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Folder>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Folder> IChains<Folder>.SourceList(int chainKindId)
        {
            return Chain<Folder>.GetChainSourceList(this, chainKindId);
        }
        List<Folder> IChains<Folder>.DestinationList(int chainKindId)
        {
            return Chain<Folder>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Folder>> GetValues(bool allKinds)
        {
            return CodeHelper<Folder>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Folder>.GetView(this, true);
        }
        #endregion
        #region IChainsAdvancedList<Folder,Note> Members

        List<IChainAdvanced<Folder, Note>> IChainsAdvancedList<Folder, Note>.GetLinks()
        {
            return ChainAdvanced<Folder, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Folder, Note>> IChainsAdvancedList<Folder, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Folder, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Folder, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Folder, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Folder, Note>.GetChainView()
        {
            return ChainValueView.GetView<Folder, Note>(this);
        }
        #endregion

        public List<Folder> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Folder> filter = null,
            int? formId=null, int? operationId=null, int? viewDocId=null,
            bool useAndFilter = false)
        {
            Folder item = new Folder { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Folder> collection = new List<Folder>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindProcedure(GlobalMethodAlias.FindBy);
                        cmd.Parameters.Add(GlobalSqlParamNames.Count, SqlDbType.Int).Value = count;
                        if (hierarchyId != null && hierarchyId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;
                        if (userName != null && !string.IsNullOrEmpty(userName))
                            cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;
                        if (flags.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.Flags, SqlDbType.Int).Value = flags;
                        if (stateId.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateId;
                        if (!string.IsNullOrWhiteSpace(name))
                            cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = name;
                        if (kindId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindId;
                        if (!string.IsNullOrWhiteSpace(code))
                            cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;
                        if (!string.IsNullOrWhiteSpace(memo))
                            cmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255).Value = memo;
                        if (!string.IsNullOrWhiteSpace(flagString))
                            cmd.Parameters.Add(GlobalSqlParamNames.FlagString, SqlDbType.NVarChar, 50).Value = flagString;
                        if (templateId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TemplateId, SqlDbType.Int).Value = templateId;
                        if (useAndFilter)
                            cmd.Parameters.Add(GlobalSqlParamNames.UseAndFilter, SqlDbType.Bit).Value = true;

                        if (formId.HasValue && formId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.FormId, SqlDbType.Int).Value = formId.Value;
                        if (operationId.HasValue && operationId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.OperationId, SqlDbType.Int).Value = operationId.Value;
                        if (viewDocId.HasValue && viewDocId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.ViewDocId, SqlDbType.Int).Value = viewDocId.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Folder { Workarea = Workarea };
                            item.Load(reader);
                            Workarea.Cashe.SetCasheData(item);
                            if (filter != null && filter.Invoke(item))
                                collection.Add(item);
                            else if (filter == null)
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
