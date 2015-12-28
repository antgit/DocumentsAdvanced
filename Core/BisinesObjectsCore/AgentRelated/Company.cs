using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct CompanyStruct
    {
        /// <summary>Аналитика вид экономической деятельности</summary>
        public string ActivityEconomic;
        /// <summary>Идентификатор главного бухгалтера</summary>
        public int BuhId;
        /// <summary>Идентификатор кассира</summary>
        public int CashierId;
        /// <summary>Идентификатор аналитики категория торговой точки</summary>
        public int CategoryId;
        /// <summary>Идентификатор директора</summary>
        public int DirectorId;
        /// <summary>Идентификатор аналитики отрасль деятельности</summary>
        public int IndustryId;
        /// <summary>Международное наименование</summary>
        public string InternationalName;
        /// <summary>Идентификатор метража</summary>
        public int MetricAreaId;
        /// <summary>Плательщик НДС</summary>
        public bool NdsPayer;
        /// <summary>Окпо</summary>
        public string Okpo;
        /// <summary>Форма собственности</summary>
        public int OwnershipId;
        /// <summary>Идентификатор начальника по персоналу</summary>
        public int PersonnelId;
        /// <summary>Дата регистрации предприятия</summary>
        public DateTime? RegDate;
        /// <summary>Номер регистрации в службе занятости</summary>
        public string RegEmploymentService;
        /// <summary>Идентификатор корреспондента, зарегистрироваашего компанию</summary>
        public int RegisteredById;
        /// <summary>Код КФВ</summary>
        public string RegKfv;
        /// <summary>Код КОАТУУ</summary>
        public string RegKoatu;
        /// <summary>Код по КВЕД</summary>
        public string RegKved;
        /// <summary>Номер свидетельства о регистрации</summary>
        public string RegNumber;
        /// <summary>Код ОПФГ</summary>
        public string RegOpfg;
        /// <summary>Номер регистрации в пенсионном фонде</summary>
        public string RegPensionFund;
        /// <summary>Код ПФУ</summary>
        public string RegPfu;
        /// <summary>Номер регистрации в фонде социального страхования от временной потери трудоспособности</summary>
        public string RegSocialInsuranceDisability;
        /// <summary>Номер регистрации в фонде социального страхования от несчатного случая</summary>
        public string RegSocialInsuranceNesch;
        /// <summary>Код ЗКГНГ</summary>
        public string RegZkgng;
        /// <summary>Идентификатор корреспондента "Торговый представитель"</summary>
        public int SalesRepresentativeId;
        /// <summary>Ставка налогов</summary>
        public decimal Tax;
        /// <summary>Идентификатор корреспондента "Налоговая испекция"</summary>
        public int TaxInspectionId;
        /// <summary>Идентификатор аналитики тип торговой точки</summary>
        public int TypeOutletId;

        public int LogoId;
        public int LogostampId;
        public int LogoSignId;
    }
    //Фонд социального страхования по временной потере трудоспособности
    //Фонде социального страхования на случай безработицы 
    /// <summary>Предприятие</summary>
    public class Company : BaseCoreObject, IRelationSingle, ICloneable
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Company():base()
        {
            EntityId = (short) WhellKnownDbEntity.Company;
        }
        #region ICloneable Members
        /// <summary>Копия объекта</summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return Clone(true);
        }
        /// <summary>Копия объекта</summary>
        /// <param name="endInit">Завершить инициализацию</param>
        /// <returns></returns>
        protected internal virtual Company Clone(bool endInit)
        {
            Company cloneObj = new Company();
            
                cloneObj.Workarea = Workarea;
                cloneObj.OnBeginInit();
                cloneObj.Id = Id;
                cloneObj.Guid = Guid;
                cloneObj.DatabaseId = DatabaseId;
                cloneObj.FlagsValue = FlagsValue;
                cloneObj.DbSourceId = DbSourceId;
                cloneObj.StateId = StateId;
                cloneObj.InternationalName = InternationalName;
                cloneObj.ActivityEconomic = ActivityEconomic;
                cloneObj.BuhId = BuhId;
                cloneObj.CashierId = CashierId;
                cloneObj.CategoryId = CategoryId;
                cloneObj.DirectorId = DirectorId;
                cloneObj.IndustryId = IndustryId;
                cloneObj.MetricAreaId = MetricAreaId;
                cloneObj.Okpo = Okpo;
                cloneObj.OwnershipId = OwnershipId;
                cloneObj.PersonnelId = PersonnelId;
                cloneObj.RegDate = RegDate;
                cloneObj.RegEmploymentService = RegEmploymentService;
                cloneObj.RegisteredById = RegisteredById;
                cloneObj.RegKfv = RegKfv;
                cloneObj.RegKoatu = RegKoatu;
                cloneObj.RegKved = RegKved;
                cloneObj.RegNumber = RegNumber;
                cloneObj.RegOpfg = RegOpfg;
                cloneObj.RegPensionFund = RegPensionFund;
                cloneObj.RegPfu = RegPfu;
                cloneObj.RegSocialInsuranceDisability = RegSocialInsuranceDisability;
                cloneObj.RegSocialInsuranceNesch = RegSocialInsuranceNesch;
                cloneObj.RegZkgng = RegZkgng;
                cloneObj.SalesRepresentativeId = SalesRepresentativeId;
                cloneObj.Tax = Tax;
                cloneObj.TaxInspectionId = TaxInspectionId;
                cloneObj.TypeOutletId = TypeOutletId;
                if (endInit)
                    cloneObj.OnEndInit();
            
            return cloneObj;
        }
        #endregion
        #region Свойства
        internal Agent _owner;
        /// <summary>
        /// Объект владелец (корреспондент)
        /// </summary>
        public Agent Owner
        {
            get { return _owner; }
        }
        private string _internationalName;
        /// <summary>Международное наименование</summary>
        public string InternationalName
        {
            get { return _internationalName; }
            set
            {
                if (_internationalName == value) return;
                OnPropertyChanging(GlobalPropertyNames.InternationalName);
                _internationalName = value;
                OnPropertyChanged(GlobalPropertyNames.InternationalName);
            }
        }

        private decimal _tax;
        /// <summary>Ставка налогов</summary>
        public decimal Tax
        {
            get { return _tax; }
            set
            {
                if (_tax == value) return;
                OnPropertyChanging(GlobalPropertyNames.Tax);
                _tax = value;
                OnPropertyChanged(GlobalPropertyNames.Tax);
            }
        }

        private bool _ndsPayer;
        /// <summary>Плательщик НДС</summary>
        public bool NdsPayer
        {
            get { return _ndsPayer; }
            set
            {
                if (_ndsPayer == value) return;
                OnPropertyChanging(GlobalPropertyNames.NdsPayer);
                _ndsPayer = value;
                OnPropertyChanged(GlobalPropertyNames.NdsPayer);
            }
        }

        private string _okpo;
        /// <summary>Окпо</summary>
        public string Okpo
        {
            get { return _okpo; }
            set
            {
                if (_okpo == value) return;
                OnPropertyChanging(GlobalPropertyNames.Okpo);
                _okpo = value;
                OnPropertyChanged(GlobalPropertyNames.Okpo);
            }
        }

        internal Bank _bank;
        /// <summary>Банк</summary>
        public Bank Bank
        {
            get
            {
                if (_bank == null)
                {
                    _bank = new Bank{Workarea = Workarea};
                    if(Id!=0)
                    {
                        _bank.Load(Id);
                    }
                    _bank._owner = this;
                }
                return _bank;
            }
        }

        /// <summary>Обновить информацию о банке</summary>
        private void RefreshBank()
        {
            //if (!IsNew)
            //{
            //    RelationHelper<Agent, Company> hlp = new RelationHelper<Agent, Company>();
            //    _company = hlp.GetObject(this.);
            //}
            //else
            //    _company = new Company { Workarea = Workarea };
        }
        private DateTime? _regDate;
        /// <summary>Дата регистрации предприятия</summary>
        public DateTime? RegDate
        {
            get { return _regDate; }
            set
            {
                if (_regDate == value) return;
                OnPropertyChanging(GlobalPropertyNames.RegDate);
                _regDate = value;
                OnPropertyChanged(GlobalPropertyNames.RegDate);
            }
        }
        
        private int _registeredById;
        /// <summary>Идентификатор корреспондента, зарегистрироваашего компанию</summary>
        public int RegisteredById
        {
            get { return _registeredById; }
            set
            {
                if (_registeredById == value) return;
                OnPropertyChanging(GlobalPropertyNames.RegisteredById);
                _registeredById = value;
                OnPropertyChanged(GlobalPropertyNames.RegisteredById);
            }
        }

        private Agent _agentRegister;
        /// <summary>Корреспондент, зареистрировавший компанию</summary>
        public Agent AgentRegister
        {
            get
            {
                if (_registeredById == 0)
                    return null;
                if (_agentRegister == null)
                    _agentRegister = Workarea.Cashe.GetCasheData<Agent>().Item(_registeredById);
                else if (_agentRegister.Id != _registeredById)
                    _agentRegister = Workarea.Cashe.GetCasheData<Agent>().Item(_registeredById);
                return _agentRegister;
            }
            set
            {
                if (_agentRegister == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentRegister);
                _agentRegister = value;
                _registeredById = _agentRegister == null ? 0 : _agentRegister.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentRegister);
            }
        }

        private int _ownershipId;
        /// <summary>
        /// Форма собственности
        /// </summary>
        public int OwnershipId
        {
            get { return _ownershipId; }
            set
            {
                if (value == _ownershipId) return;
                OnPropertyChanging(GlobalPropertyNames.OwnershipId);
                _ownershipId = value;
                OnPropertyChanged(GlobalPropertyNames.OwnershipId);
            }
        }

        private Analitic _ownership;
        /// <summary>Аналитика форма собственности</summary>
        public Analitic Ownership
        {
            get
            {
                if (_ownershipId == 0)
                    return null;
                if (_ownership == null)
                    _ownership = Workarea.Cashe.GetCasheData<Analitic>().Item(_ownershipId);
                else if (_ownership.Id != _ownershipId)
                    _ownership = Workarea.Cashe.GetCasheData<Analitic>().Item(_ownershipId);
                return _ownership;
            }
            set
            {
                if (_ownership == value) return;
                OnPropertyChanging(GlobalPropertyNames.Ownership);
                _ownership = value;
                _ownershipId = _ownership == null ? 0 : _ownership.Id;
                OnPropertyChanged(GlobalPropertyNames.Ownership);
            }
        }
        
        private string _activityEconomic;
        /// <summary>Аналитика вид экономической деятельности</summary>
        public string ActivityEconomic
        {
            get
            {
                return _activityEconomic;
            }
            set
            {
                if (_activityEconomic == value) return;
                OnPropertyChanging(GlobalPropertyNames.ActivityEconomic);
                _activityEconomic = value;
                OnPropertyChanged(GlobalPropertyNames.ActivityEconomic);
            }
        }

        private int _industryId;
        /// <summary>Идентификатор аналитики отрасль деятельности</summary>
        public int IndustryId
        {
            get { return _industryId; }
            set
            {
                if (_industryId == value) return;
                OnPropertyChanging(GlobalPropertyNames.IndustryId);
                _industryId = value;
                OnPropertyChanged(GlobalPropertyNames.IndustryId);
            }
        }

        private Analitic _industry;
        /// <summary>Аналитика отрасль деятельности</summary>
        public Analitic Industry
        {
            get
            {
                if (_industryId == 0)
                    return null;
                if (_industry == null)
                    _industry = Workarea.Cashe.GetCasheData<Analitic>().Item(_industryId);
                else if (_industry.Id != _industryId)
                    _industry = Workarea.Cashe.GetCasheData<Analitic>().Item(_industryId);
                return _industry;
            }
            set
            {
                if (_industry == value) return;
                OnPropertyChanging(GlobalPropertyNames.Industry);
                _industry = value;
                _industryId = _industry == null ? 0 : _industry.Id;
                OnPropertyChanged(GlobalPropertyNames.Industry);
            }
        }

        private string _regNumber;
        /// <summary>Номер свидетельства о регистрации</summary>
        public string RegNumber
        {
            get { return _regNumber; }
            set
            {
                if (_regNumber == value) return;
                OnPropertyChanging(GlobalPropertyNames.RegNumber);
                _regNumber = value;
                OnPropertyChanged(GlobalPropertyNames.RegNumber);
            }
        }

        private int _typeOutletId;
        /// <summary>Идентификатор аналитики тип торговой точки</summary>
        public int TypeOutletId
        {
            get { return _typeOutletId; }
            set
            {
                if (_typeOutletId == value) return;
                OnPropertyChanging(GlobalPropertyNames.TypeOutletId);
                _typeOutletId = value;
                OnPropertyChanged(GlobalPropertyNames.TypeOutletId);
            }
        }

        private Analitic _typeOutlet;
        /// <summary>Аналитика тип торговой точки</summary>
        public Analitic TypeOutlet
        {
            get
            {
                if (_typeOutletId == 0)
                    return null;
                if (_typeOutlet == null)
                    _typeOutlet = Workarea.Cashe.GetCasheData<Analitic>().Item(_typeOutletId);
                else if (_typeOutlet.Id != _typeOutletId)
                    _typeOutlet = Workarea.Cashe.GetCasheData<Analitic>().Item(_typeOutletId);
                return _typeOutlet;
            }
            set
            {
                if (_typeOutlet == value) return;
                OnPropertyChanging(GlobalPropertyNames.TypeOutlet);
                _typeOutlet = value;
                _typeOutletId = _typeOutlet == null ? 0 : _typeOutlet.Id;
                OnPropertyChanged(GlobalPropertyNames.TypeOutlet);
            }
        }

        private int _metricAreaId;
        /// <summary>Идентификатор аналитики метраж</summary>
        public int MetricAreaId
        {
            get { return _metricAreaId; }
            set
            {
                if (_metricAreaId == value) return;
                OnPropertyChanging(GlobalPropertyNames.MetricAreaId);
                _metricAreaId = value;
                OnPropertyChanged(GlobalPropertyNames.MetricAreaId);
            }
        }

        private Analitic _metricArea;
        /// <summary>Аналитика метраж</summary>
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
                OnPropertyChanging(GlobalPropertyNames.MetricArea);
                _metricArea = value;
                _metricAreaId = _metricArea == null ? 0 : _metricArea.Id;
                OnPropertyChanged(GlobalPropertyNames.MetricArea);
            }
        }

        private int _categoryId;
        /// <summary>Идентификатор аналитики категория торговой точки</summary>
        public int CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (_categoryId == value) return;
                OnPropertyChanging(GlobalPropertyNames.CategoryId);
                _categoryId = value;
                OnPropertyChanged(GlobalPropertyNames.CategoryId);
            }
        }

        private Analitic _category;
        /// <summary>Аналитика категория торговой точки</summary>
        public Analitic Category
        {
            get
            {
                if (_categoryId == 0)
                    return null;
                if (_category == null)
                    _category = Workarea.Cashe.GetCasheData<Analitic>().Item(_categoryId);
                else if (_category.Id != _categoryId)
                    _category = Workarea.Cashe.GetCasheData<Analitic>().Item(_categoryId);
                return _category;
            }
            set
            {
                if (_category == value) return;
                OnPropertyChanging(GlobalPropertyNames.Category);
                _category = value;
                _categoryId = _category == null ? 0 : _category.Id;
                OnPropertyChanged(GlobalPropertyNames.Category);
            }
        }

        private int _salesRepresentativeId;
        /// <summary>Идентификатор корреспондента "Торговый представитель"</summary>
        public int SalesRepresentativeId
        {
            get { return _salesRepresentativeId; }
            set
            {
                if (_salesRepresentativeId == value) return;
                OnPropertyChanging(GlobalPropertyNames.SalesRepresentativeId);
                _salesRepresentativeId = value;
                OnPropertyChanged(GlobalPropertyNames.SalesRepresentativeId);
            }
        }

        private Agent _salesRepresentative;
        /// <summary>Корреспондент торговый представитель</summary>
        public Agent SalesRepresentative
        {
            get
            {
                if (_salesRepresentativeId == 0)
                    return null;
                if (_salesRepresentative == null)
                    _salesRepresentative = Workarea.Cashe.GetCasheData<Agent>().Item(_salesRepresentativeId);
                else if (_salesRepresentative.Id != _salesRepresentativeId)
                    _salesRepresentative = Workarea.Cashe.GetCasheData<Agent>().Item(_salesRepresentativeId);
                return _salesRepresentative;
            }
            set
            {
                if (_salesRepresentative == value) return;
                OnPropertyChanging(GlobalPropertyNames.SalesRepresentative);
                _salesRepresentative = value;
                _salesRepresentativeId = _salesRepresentative == null ? 0 : _salesRepresentative.Id;
                OnPropertyChanged(GlobalPropertyNames.SalesRepresentative);
            }
        }

        private int _taxInspectionId;
        /// <summary>
        /// Идентификатор корреспондента "Налоговая испекция"
        /// </summary>
        public int TaxInspectionId
        {
            get { return _taxInspectionId; }
            set
            {
                if (value == _taxInspectionId) return;
                OnPropertyChanging(GlobalPropertyNames.TaxInspectionId);
                _taxInspectionId = value;
                OnPropertyChanged(GlobalPropertyNames.TaxInspectionId);
            }
        }

        private Agent _taxInspection;
        /// <summary>Корреспондент "Налоговая испекция"</summary>
        public Agent TaxInspection
        {
            get
            {
                if (_taxInspectionId == 0)
                    return null;
                if (_taxInspection == null)
                    _taxInspection = Workarea.Cashe.GetCasheData<Agent>().Item(_taxInspectionId);
                else if (_taxInspection.Id != _taxInspectionId)
                    _taxInspection = Workarea.Cashe.GetCasheData<Agent>().Item(_taxInspectionId);
                return _taxInspection;
            }
            set
            {
                if (_taxInspection == value) return;
                OnPropertyChanging(GlobalPropertyNames.TaxInspection);
                _taxInspection = value;
                _taxInspectionId = _taxInspection == null ? 0 : _taxInspection.Id;
                OnPropertyChanged(GlobalPropertyNames.TaxInspection);
            }
        }

        private string _regPensionFund;
        /// <summary>
        /// Номер регистрации в пенсионном фонде
        /// </summary>
        public string RegPensionFund
        {
            get { return _regPensionFund; }
            set
            {
                if (value == _regPensionFund) return;
                OnPropertyChanging(GlobalPropertyNames.RegPensionFund);
                _regPensionFund = value;
                OnPropertyChanged(GlobalPropertyNames.RegPensionFund);
            }
        }

        private string _regEmploymentService;
        /// <summary>
        /// Номер регистрации в службе занятости
        /// </summary>
        public string RegEmploymentService
        {
            get { return _regEmploymentService; }
            set
            {
                if (value == _regEmploymentService) return;
                OnPropertyChanging(GlobalPropertyNames.RegEmploymentService);
                _regEmploymentService = value;
                OnPropertyChanged(GlobalPropertyNames.RegEmploymentService);
            }
        }


        private string _regSocialInsuranceDisability;
        /// <summary>
        /// Номер регистрации в фонде социального страхования от временной потери трудоспособности
        /// </summary>
        public string RegSocialInsuranceDisability
        {
            get { return _regSocialInsuranceDisability; }
            set
            {
                if (value == _regSocialInsuranceDisability) return;
                OnPropertyChanging(GlobalPropertyNames.RegSocialInsuranceDisability);
                _regSocialInsuranceDisability = value;
                OnPropertyChanged(GlobalPropertyNames.RegSocialInsuranceDisability);
            }
        }

        private string _regSocialInsuranceNesch;
        /// <summary>
        /// Номер регистрации в фонде социального страхования от несчатного случая
        /// </summary>
        public string RegSocialInsuranceNesch
        {
            get { return _regSocialInsuranceNesch; }
            set
            {
                if (value == _regSocialInsuranceNesch) return;
                OnPropertyChanging(GlobalPropertyNames.RegSocialInsuranceNesch);
                _regSocialInsuranceNesch = value;
                OnPropertyChanged(GlobalPropertyNames.RegSocialInsuranceNesch);
            }
        }


        private string _regPfu;
        /// <summary>
        /// Код ПФУ
        /// </summary>
        public string RegPfu
        {
            get { return _regPfu; }
            set
            {
                if (value == _regPfu) return;
                OnPropertyChanging(GlobalPropertyNames.RegPfu);
                _regPfu = value;
                OnPropertyChanged(GlobalPropertyNames.RegPfu);
            }
        }

        private string _regOpfg;
        /// <summary>
        /// Код ОПФГ
        /// </summary>
        public string RegOpfg
        {
            get { return _regOpfg; }
            set
            {
                if (value == _regOpfg) return;
                OnPropertyChanging(GlobalPropertyNames.RegOpfg);
                _regOpfg = value;
                OnPropertyChanged(GlobalPropertyNames.RegOpfg);
            }
        }

        private string _regKoatu;
        /// <summary>
        /// Код КОАТУУ
        /// </summary>
        public string RegKoatu
        {
            get { return _regKoatu; }
            set
            {
                if (value == _regKoatu) return;
                OnPropertyChanging(GlobalPropertyNames.RegKoatu);
                _regKoatu = value;
                OnPropertyChanged(GlobalPropertyNames.RegKoatu);
            }
        }

        private string _regKfv;
        /// <summary>
        /// Код КФВ
        /// </summary>
        public string RegKfv
        {
            get { return _regKfv; }
            set
            {
                if (value == _regKfv) return;
                OnPropertyChanging(GlobalPropertyNames.RegKfv);
                _regKfv = value;
                OnPropertyChanged(GlobalPropertyNames.RegKfv);
            }
        }

        private string _regZkgng;
        /// <summary>
        /// Код ЗКГНГ
        /// </summary>
        public string RegZkgng
        {
            get { return _regZkgng; }
            set
            {
                if (value == _regZkgng) return;
                OnPropertyChanging(GlobalPropertyNames.RegZkgng);
                _regZkgng = value;
                OnPropertyChanged(GlobalPropertyNames.RegZkgng);
            }
        }


        private string _regKved;
        /// <summary>
        /// Код по КВЕД
        /// </summary>
        public string RegKved
        {
            get { return _regKved; }
            set
            {
                if (value == _regKved) return;
                OnPropertyChanging(GlobalPropertyNames.RegKved);
                _regKved = value;
                OnPropertyChanged(GlobalPropertyNames.RegKved);
            }
        }


        private int _directorId;
        /// <summary>
        /// Идентификатор директора
        /// </summary>
        public int DirectorId
        {
            get { return _directorId; }
            set
            {
                if (value == _directorId) return;
                OnPropertyChanging(GlobalPropertyNames.DirectorId);
                _directorId = value;
                OnPropertyChanged(GlobalPropertyNames.DirectorId);
            }
        }

        private Agent _director;
        /// <summary>Корреспондент директор</summary>
        public Agent Director
        {
            get
            {
                if (_directorId == 0)
                    return null;
                if (_director == null)
                    _director = Workarea.Cashe.GetCasheData<Agent>().Item(_directorId);
                else if (_director.Id != _directorId)
                    _director = Workarea.Cashe.GetCasheData<Agent>().Item(_directorId);
                return _director;
            }
            set
            {
                if (_director == value) return;
                OnPropertyChanging(GlobalPropertyNames.Director);
                _director = value;
                _directorId = _director == null ? 0 : _director.Id;
                OnPropertyChanged(GlobalPropertyNames.Director);
            }
        }

        private int _buhId;
        /// <summary>
        /// Идентификатор главного бухгалтера
        /// </summary>
        public int BuhId
        {
            get { return _buhId; }
            set
            {
                if (value == _buhId) return;
                OnPropertyChanging(GlobalPropertyNames.BuhId);
                _buhId = value;
                OnPropertyChanged(GlobalPropertyNames.BuhId);
            }
        }

        private Agent _buh;
        /// <summary>Корреспондент бухгалтер</summary>
        public Agent Buh
        {
            get
            {
                if (_buhId == 0)
                    return null;
                if (_buh == null)
                    _buh = Workarea.Cashe.GetCasheData<Agent>().Item(_buhId);
                else if (_buh.Id != _buhId)
                    _buh = Workarea.Cashe.GetCasheData<Agent>().Item(_buhId);
                return _buh;
            }
            set
            {
                if (_buh == value) return;
                OnPropertyChanging(GlobalPropertyNames.Buh);
                _buh = value;
                _buhId = _buh == null ? 0 : _buh.Id;
                OnPropertyChanged(GlobalPropertyNames.Buh);
            }
        }

        private int _cashierId;
        /// <summary>
        /// Идентификатор кассира
        /// </summary>
        public int CashierId
        {
            get { return _cashierId; }
            set
            {
                if (value == _cashierId) return;
                OnPropertyChanging(GlobalPropertyNames.CashierId);
                _cashierId = value;
                OnPropertyChanged(GlobalPropertyNames.CashierId);
            }
        }

        private Agent _cashier;
        /// <summary>Корреспондент кассир</summary>
        public Agent Cashier
        {
            get
            {
                if (_cashierId == 0)
                    return null;
                if (_cashier == null)
                    _cashier = Workarea.Cashe.GetCasheData<Agent>().Item(_cashierId);
                else if (_cashier.Id != _cashierId)
                    _cashier = Workarea.Cashe.GetCasheData<Agent>().Item(_cashierId);
                return _cashier;
            }
            set
            {
                if (_cashier == value) return;
                OnPropertyChanging(GlobalPropertyNames.Cashier);
                _cashier = value;
                _cashierId = _cashier == null ? 0 : _cashier.Id;
                OnPropertyChanged(GlobalPropertyNames.Cashier);
            }
        }

        private int _personnelId;
        /// <summary>
        /// Идентификатор начальника по персоналу
        /// </summary>
        public int PersonnelId
        {
            get { return _personnelId; }
            set
            {
                if (value == _personnelId) return;
                OnPropertyChanging(GlobalPropertyNames.PersonnelId);
                _personnelId = value;
                OnPropertyChanged(GlobalPropertyNames.PersonnelId);
            }
        }

        private Agent _personnel;
        /// <summary>Корреспондент начальник по персоналу</summary>
        public Agent Personnel
        {
            get
            {
                if (_personnelId == 0)
                    return null;
                if (_personnel == null)
                    _personnel = Workarea.Cashe.GetCasheData<Agent>().Item(_personnelId);
                else if (_personnel.Id != _personnelId)
                    _personnel = Workarea.Cashe.GetCasheData<Agent>().Item(_personnelId);
                return _personnel;
            }
            set
            {
                if (_personnel == value) return;
                OnPropertyChanging(GlobalPropertyNames.Personnel);
                _personnel = value;
                _personnelId = _personnel == null ? 0 : _personnel.Id;
                OnPropertyChanged(GlobalPropertyNames.Personnel);
            }
        }

        private int _logoId;
        public int LogoId
        {
            get { return _logoId; }
            set
            {
                if (value == _logoId) return;
                OnPropertyChanging(GlobalPropertyNames.LogoId);
                _logoId = value;
                OnPropertyChanged(GlobalPropertyNames.LogoId);
            }
        }

        private int _logostampId;
        public int LogostampId
        {
            get { return _logostampId; }
            set
            {
                if (value == _logostampId) return;
                OnPropertyChanging(GlobalPropertyNames.LogostampId);
                _logostampId = value;
                OnPropertyChanged(GlobalPropertyNames.LogostampId);
            }
        }

        private int _logoSignId;
        public int LogoSignId
        {
            get { return _logoSignId; }
            set
            {
                if (value == _logoSignId) return;
                OnPropertyChanging(GlobalPropertyNames.LogoSignId);
                _logoSignId = value;
                OnPropertyChanged(GlobalPropertyNames.LogoSignId);
            }
        }
        #endregion
        #region Состояние
        CompanyStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new CompanyStruct
                {
                    ActivityEconomic = _activityEconomic,
                    BuhId = _buhId,
                    CashierId = _cashierId,
                    CategoryId = _categoryId,
                    DirectorId = _directorId,
                    IndustryId = _industryId,
                    InternationalName = _internationalName,
                    MetricAreaId = _metricAreaId,
                    NdsPayer = _ndsPayer,
                    Okpo = _okpo,
                    OwnershipId = _ownershipId,
                    PersonnelId = _personnelId,
                    RegDate = _regDate,
                    RegEmploymentService = _regEmploymentService,
                    RegisteredById = _registeredById,
                    RegKfv = _regKfv,
                    RegKoatu = _regKoatu,
                    RegKved = _regKved,
                    RegNumber = _regNumber,
                    RegOpfg = _regOpfg,
                    RegPensionFund = _regPensionFund,
                    RegPfu = _regPfu,
                    RegSocialInsuranceDisability = _regSocialInsuranceDisability,
                    RegSocialInsuranceNesch = _regSocialInsuranceNesch,
                    RegZkgng = _regZkgng,
                    SalesRepresentativeId = _salesRepresentativeId,
                    Tax = _tax,
                    TaxInspectionId = _taxInspectionId,
                    TypeOutletId = _typeOutletId,
                    LogoId = _logoId,
                    LogostampId = _logostampId,
                    LogoSignId = _logoSignId
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            ActivityEconomic = _baseStruct.ActivityEconomic;
            BuhId = _baseStruct.BuhId;
            CashierId = _baseStruct.CashierId;
            CategoryId = _baseStruct.CategoryId;
            DirectorId = _baseStruct.DirectorId;
            IndustryId = _baseStruct.IndustryId;
            InternationalName = _baseStruct.InternationalName;
            MetricAreaId = _baseStruct.MetricAreaId;
            NdsPayer = _baseStruct.NdsPayer;
            Okpo = _baseStruct.Okpo;
            OwnershipId = _baseStruct.OwnershipId;
            PersonnelId = _baseStruct.PersonnelId;
            RegDate = _baseStruct.RegDate;
            RegEmploymentService = _baseStruct.RegEmploymentService;
            RegisteredById = _baseStruct.RegisteredById;
            RegKfv = _baseStruct.RegKfv;
            RegKoatu = _baseStruct.RegKoatu;
            RegKved = _baseStruct.RegKved;
            RegNumber = _baseStruct.RegNumber;
            RegOpfg = _baseStruct.RegOpfg;
            RegPensionFund = _baseStruct.RegPensionFund;
            RegPfu = _baseStruct.RegPfu;
            RegSocialInsuranceDisability = _baseStruct.RegSocialInsuranceDisability;
            RegSocialInsuranceNesch = _baseStruct.RegSocialInsuranceNesch;
            RegZkgng = _baseStruct.RegZkgng;
            SalesRepresentativeId = _baseStruct.SalesRepresentativeId;
            Tax = _baseStruct.Tax;
            TaxInspectionId = _baseStruct.TaxInspectionId;
            TypeOutletId = _baseStruct.TypeOutletId;
            LogoId = _baseStruct.LogoId;
            LogostampId = _baseStruct.LogostampId;
            LogoSignId = _baseStruct.LogoSignId;

            IsChanged = false;
        }
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_activityEconomic))
                writer.WriteAttributeString(GlobalPropertyNames.ActivityEconomic, _activityEconomic);
            if (_buhId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.BuhId, XmlConvert.ToString(_buhId));
            if (_cashierId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CashierId, XmlConvert.ToString(_cashierId));
            if (_categoryId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CategoryId, XmlConvert.ToString(_categoryId));
            if (_directorId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DirectorId, XmlConvert.ToString(_directorId));
            if (_industryId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.IndustryId, XmlConvert.ToString(_industryId));
            if (!string.IsNullOrEmpty(_internationalName))
                writer.WriteAttributeString(GlobalPropertyNames.InternationalName, _internationalName);
            if (_metricAreaId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MetricAreaId, XmlConvert.ToString(_metricAreaId));
            if (_ndsPayer)
                writer.WriteAttributeString(GlobalPropertyNames.NdsPayer, XmlConvert.ToString(_ndsPayer));
            if (!string.IsNullOrEmpty(_okpo))
                writer.WriteAttributeString(GlobalPropertyNames.Okpo, _okpo);
            if (_ownershipId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnershipId, XmlConvert.ToString(_ownershipId));
            if (_personnelId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.PersonnelId, XmlConvert.ToString(_personnelId));
            if (_regDate.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.RegDate, XmlConvert.ToString(_regDate.Value));
            if (!string.IsNullOrEmpty(_regEmploymentService))
                writer.WriteAttributeString(GlobalPropertyNames.RegEmploymentService, _regEmploymentService);
            if (_registeredById != 0)
                writer.WriteAttributeString(GlobalPropertyNames.RegisteredById, XmlConvert.ToString(_registeredById));
            if (!string.IsNullOrEmpty(_regKfv))
                writer.WriteAttributeString(GlobalPropertyNames.RegKfv, _regKfv);
            if (!string.IsNullOrEmpty(_regKoatu))
                writer.WriteAttributeString(GlobalPropertyNames.RegKoatu, _regKoatu);
            if (!string.IsNullOrEmpty(_regKved))
                writer.WriteAttributeString(GlobalPropertyNames.RegKved, _regKved);
            if (!string.IsNullOrEmpty(_regNumber))
                writer.WriteAttributeString(GlobalPropertyNames.RegNumber, _regNumber);
            if (!string.IsNullOrEmpty(_regOpfg))
                writer.WriteAttributeString(GlobalPropertyNames.RegOpfg, _regOpfg);
            if (!string.IsNullOrEmpty(_regPensionFund))
                writer.WriteAttributeString(GlobalPropertyNames.RegPensionFund, _regPensionFund);
            if (!string.IsNullOrEmpty(_regPfu))
                writer.WriteAttributeString(GlobalPropertyNames.RegPfu, _regPfu);
            if (!string.IsNullOrEmpty(_regSocialInsuranceDisability))
                writer.WriteAttributeString(GlobalPropertyNames.RegSocialInsuranceDisability, _regSocialInsuranceDisability);
            if (!string.IsNullOrEmpty(_regSocialInsuranceNesch))
                writer.WriteAttributeString(GlobalPropertyNames.RegSocialInsuranceNesch, _regSocialInsuranceNesch);
            if (!string.IsNullOrEmpty(_regZkgng))
                writer.WriteAttributeString(GlobalPropertyNames.RegZkgng, _regZkgng);
            if (_salesRepresentativeId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SalesRepresentativeId, XmlConvert.ToString(_salesRepresentativeId));
            if (_tax != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Tax, XmlConvert.ToString(_tax));
            if (_taxInspectionId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TaxInspectionId, XmlConvert.ToString(_taxInspectionId));
            if (_typeOutletId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TypeOutletId, XmlConvert.ToString(_typeOutletId));
            if (_logoId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.LogoId, XmlConvert.ToString(_logoId));
            if (_logostampId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.LogostampId, XmlConvert.ToString(_logostampId));
            if (_logoSignId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.LogoSignId, XmlConvert.ToString(_logoSignId));
        }
       
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ActivityEconomic) != null)
                _activityEconomic = reader.GetAttribute(GlobalPropertyNames.ActivityEconomic);
            if (reader.GetAttribute(GlobalPropertyNames.BuhId) != null)
                _buhId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.BuhId));
            if (reader.GetAttribute(GlobalPropertyNames.CashierId) != null)
                _cashierId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CashierId));
            if (reader.GetAttribute(GlobalPropertyNames.CategoryId) != null)
                _categoryId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CategoryId));
            if (reader.GetAttribute(GlobalPropertyNames.DirectorId) != null)
                _directorId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DirectorId));
            if (reader.GetAttribute(GlobalPropertyNames.IndustryId) != null)
                _industryId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.IndustryId));
            if (reader.GetAttribute(GlobalPropertyNames.InternationalName) != null)
                _internationalName = reader.GetAttribute(GlobalPropertyNames.InternationalName);
            if (reader.GetAttribute(GlobalPropertyNames.MetricAreaId) != null)
                _metricAreaId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MetricAreaId));
            if (reader.GetAttribute(GlobalPropertyNames.NdsPayer) != null)
                _ndsPayer = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.NdsPayer));
            if (reader.GetAttribute(GlobalPropertyNames.Okpo) != null)
                _okpo = reader.GetAttribute(GlobalPropertyNames.Okpo);
            if (reader.GetAttribute(GlobalPropertyNames.OwnershipId) != null)
                _ownershipId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnershipId));
            if (reader.GetAttribute(GlobalPropertyNames.PersonnelId) != null)
                _personnelId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PersonnelId));
            if (reader.GetAttribute(GlobalPropertyNames.RegDate) != null)
                _regDate = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.RegDate));
            if (reader.GetAttribute(GlobalPropertyNames.RegEmploymentService) != null)
                _regEmploymentService = reader.GetAttribute(GlobalPropertyNames.RegEmploymentService);
            if (reader.GetAttribute(GlobalPropertyNames.RegisteredById) != null)
                _registeredById = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RegisteredById));
            if (reader.GetAttribute(GlobalPropertyNames.RegKfv) != null)
                _regKfv = reader.GetAttribute(GlobalPropertyNames.RegKfv);
            if (reader.GetAttribute(GlobalPropertyNames.RegKoatu) != null)
                _regKoatu = reader.GetAttribute(GlobalPropertyNames.RegKoatu);
            if (reader.GetAttribute(GlobalPropertyNames.RegKved) != null)
                _regKved = reader.GetAttribute(GlobalPropertyNames.RegKved);
            if (reader.GetAttribute(GlobalPropertyNames.RegNumber) != null)
                _regNumber = reader.GetAttribute(GlobalPropertyNames.RegNumber);
            if (reader.GetAttribute(GlobalPropertyNames.RegOpfg) != null)
                _regOpfg = reader.GetAttribute(GlobalPropertyNames.RegOpfg);
            if (reader.GetAttribute(GlobalPropertyNames.RegPensionFund) != null)
                _regPensionFund = reader.GetAttribute(GlobalPropertyNames.RegPensionFund);
            if (reader.GetAttribute(GlobalPropertyNames.RegPfu) != null)
                _regPfu = reader.GetAttribute(GlobalPropertyNames.RegPfu);
            if (reader.GetAttribute(GlobalPropertyNames.RegSocialInsuranceDisability) != null)
                _regSocialInsuranceDisability = reader.GetAttribute(GlobalPropertyNames.RegSocialInsuranceDisability);
            if (reader.GetAttribute(GlobalPropertyNames.RegSocialInsuranceNesch) != null)
                _regSocialInsuranceNesch = reader.GetAttribute(GlobalPropertyNames.RegSocialInsuranceNesch);
            if (reader.GetAttribute(GlobalPropertyNames.RegZkgng) != null)
                _regZkgng = reader.GetAttribute(GlobalPropertyNames.RegZkgng);
            if (reader.GetAttribute(GlobalPropertyNames.SalesRepresentativeId) != null)
                _salesRepresentativeId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SalesRepresentativeId));
            if (reader.GetAttribute(GlobalPropertyNames.Tax) != null)
                _tax = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Tax));
            if (reader.GetAttribute(GlobalPropertyNames.TaxInspectionId) != null)
                _taxInspectionId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TaxInspectionId));
            if (reader.GetAttribute(GlobalPropertyNames.TypeOutletId) != null)
                _typeOutletId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TypeOutletId));
            if (reader.GetAttribute(GlobalPropertyNames.LogoId) != null)
                _logoId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.LogoId));
            if (reader.GetAttribute(GlobalPropertyNames.LogostampId) != null)
                _logostampId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.LogostampId));
            if (reader.GetAttribute(GlobalPropertyNames.LogoSignId) != null)
                _logoSignId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.LogoSignId));
        }
        #endregion
        ///// <summary>Сохранить объект в базе данных</summary>
        ///// <remarks>В зависимости от состояния объекта <see cref="EntityType.IsNew"/> 
        ///// выполняется создание или обновление объекта</remarks>
        //public void Save()
        //{
        //    Validate();
        //    if (IsNew)
        //        Create(Workarea.FindMethod("Contractor.CompanyInsert").FullName);
        //    else
        //        Update(Workarea.FindMethod("Contractor.CompanyUpdate").FullName, true);
        //}

        /// <summary>Загрузить</summary>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            Load(value, Workarea.FindMethod(((IRelationSingle)this).Schema + GetType().Name).FullName);
        }
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _internationalName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                _tax = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);
                _ndsPayer = reader.GetBoolean(11);
                _okpo = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                if (reader.IsDBNull(13))
                    _regDate = null;
                else
                    _regDate = reader.GetDateTime(13);
                _registeredById = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                _activityEconomic = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                _industryId = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                _regNumber = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                _typeOutletId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _metricAreaId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _categoryId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _salesRepresentativeId = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
                _ownershipId = reader.IsDBNull(22) ? 0 : reader.GetInt32(22);
                _taxInspectionId = reader.IsDBNull(23) ? 0 : reader.GetInt32(23);
                _regPensionFund = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                _regEmploymentService = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                _regSocialInsuranceDisability = reader.IsDBNull(26) ? string.Empty : reader.GetString(26);
                _regSocialInsuranceNesch = reader.IsDBNull(27) ? string.Empty : reader.GetString(27);
                _regPfu = reader.IsDBNull(28) ? string.Empty : reader.GetString(28);
                _regOpfg = reader.IsDBNull(29) ? string.Empty : reader.GetString(29);
                _regKoatu = reader.IsDBNull(30) ? string.Empty : reader.GetString(30);
                _regKfv = reader.IsDBNull(31) ? string.Empty : reader.GetString(31);
                _regZkgng = reader.IsDBNull(32) ? string.Empty : reader.GetString(32);
                _regKved = reader.IsDBNull(33) ? string.Empty : reader.GetString(33);
                _directorId = reader.IsDBNull(34) ? 0 : reader.GetInt32(34);
                _buhId = reader.IsDBNull(35) ? 0 : reader.GetInt32(35);
                _cashierId = reader.IsDBNull(36) ? 0 : reader.GetInt32(36);
                _personnelId = reader.IsDBNull(37) ? 0 : reader.GetInt32(37);
                //_logoId = reader.IsDBNull(38) ? 0 : reader.GetInt32(38);
                //_logostampId = reader.IsDBNull(39) ? 0 : reader.GetInt32(39);
                //_logoSignId = reader.IsDBNull(40) ? 0 : reader.GetInt32(40);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (endInit)
                OnEndInit();
        }

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            sqlCmd.Parameters[GlobalSqlParamNames.Id].Value = Id;

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.InternationalName, SqlDbType.NVarChar);
            if (string.IsNullOrEmpty(_internationalName))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _internationalName.Length;
                prm.Value = _internationalName;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Tax, SqlDbType.Money) {Value = _tax};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.NdsPayer, SqlDbType.Bit) {Value = _ndsPayer};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Okpo, SqlDbType.NVarChar, 16);
            if (string.IsNullOrEmpty(_okpo))
                prm.Value = DBNull.Value;
            else
                prm.Value = _okpo;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegDate, SqlDbType.Date) { IsNullable = true };
            if (_regDate.HasValue)
                prm.Value = _regDate;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegisteredById, SqlDbType.Int) { IsNullable = true };
            if (_registeredById == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _registeredById;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ActivityEconomic, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_activityEconomic))
                prm.Value = DBNull.Value;
            else
                prm.Value = _activityEconomic;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.IndustryId, SqlDbType.Int) { IsNullable = true };
            if (_industryId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _industryId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegNumber, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regNumber))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regNumber;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TypeOutletId, SqlDbType.Int) { IsNullable = true };
            if (_typeOutletId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _typeOutletId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MetricAreaId, SqlDbType.Int) { IsNullable = true };
            if (_metricAreaId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _metricAreaId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CategoryId, SqlDbType.Int) { IsNullable = true };
            if (_categoryId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _categoryId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SalesRepresentativeId, SqlDbType.Int) { IsNullable = true };
            if (_salesRepresentativeId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _salesRepresentativeId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OwnershipId, SqlDbType.Int) { IsNullable = true };
            if (_ownershipId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _ownershipId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TaxInspectionId, SqlDbType.Int) { IsNullable = true };
            if (_taxInspectionId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _taxInspectionId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegPensionFund, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regPensionFund))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regPensionFund;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegEmploymentService, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regEmploymentService))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regEmploymentService;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegSocialInsuranceDisability, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regSocialInsuranceDisability))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regSocialInsuranceDisability;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegSocialInsuranceNesch, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regSocialInsuranceNesch))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regSocialInsuranceNesch;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegPfu, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regPfu))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regPfu;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegOpfg, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regOpfg))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regOpfg;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegKoatu, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regKoatu))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regKoatu;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegKfv, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regKfv))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regKfv;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegZkgng, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regZkgng))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regZkgng;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegKved, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_regKved))
                prm.Value = DBNull.Value;
            else
                prm.Value = _regKved;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DirectorId, SqlDbType.Int) { IsNullable = true };
            if (_directorId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _directorId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.BuhId, SqlDbType.Int) { IsNullable = true };
            if (_buhId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _buhId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CashierId, SqlDbType.Int) { IsNullable = true };
            if (_cashierId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _cashierId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PersonnelId, SqlDbType.Int) { IsNullable = true };
            if (_personnelId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _personnelId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LogoId, SqlDbType.Int) { IsNullable = true, Value = _logoId == 0 ? (object)DBNull.Value : _logoId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LogostampId, SqlDbType.Int) { IsNullable = true, Value = _logostampId == 0 ? (object)DBNull.Value : _logostampId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.LogoSignId, SqlDbType.Int) { IsNullable = true, Value = _logoSignId == 0 ? (object)DBNull.Value : _logoSignId };
            sqlCmd.Parameters.Add(prm);
        }

        #region IRelationSingle Members

        string IRelationSingle.Schema
        {
            get { return GlobalSchemaNames.Contractor; }
        }

        #endregion
    }
}
