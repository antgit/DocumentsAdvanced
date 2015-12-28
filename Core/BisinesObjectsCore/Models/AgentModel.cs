using System;

namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель аналитики
    /// </summary>
    public class AgentModel : BaseModel<Agent>
    {
        /// <summary>Конструктор</summary>
        public AgentModel()
        {
        }
        /// <summary>Конструктор</summary>
        public AgentModel(Agent value)
            : this()
        {
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            GetData(value);
// ReSharper restore DoNotCallOverridableMethodsInConstructor
        }
        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="value">Корреспондент</param>
        public override void GetData(Agent value)
        {
            if (value == null)
                return;
            base.GetData(value);
            
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
            AddressPhysical = value.AddressPhysical;
            AddressLegal = value.AddressLegal;
            TaxNumber = value.CodeFind;
            Phone = value.Phone;
            TaxNumber = value.CodeTax;

            if(value.IsCompany && value.Company!=null)
            {
                OwnerShipId = value.Company.OwnershipId==0? (int?)null:value.Company.OwnershipId;
                OwnerShip = value.Company.OwnershipId == 0 ? string.Empty : value.Company.Ownership.Name;
                RegNumber = value.Company.RegNumber;
                NdsPayer = value.Company.NdsPayer;
                Okpo = value.Company.Okpo;
                TypeOutletId = value.Company.TypeOutletId == 0 ? (int?) null : value.Company.TypeOutletId;
                TypeOutletName = value.Company.TypeOutletId == 0 ? string.Empty : value.Company.TypeOutlet.Name;
                MetricAreaId = value.Company.MetricAreaId == 0 ? (int?)null : value.Company.MetricAreaId;
                MetricAreaName = value.Company.MetricAreaId == 0 ? string.Empty : value.Company.MetricArea.Name;
                CategoryId = value.Company.CategoryId == 0 ? (int?)null : value.Company.CategoryId;
                CategoryName = value.Company.CategoryId == 0 ? string.Empty : value.Company.Category.Name;
                InternationalName = value.Company.InternationalName;
                RegPensionFund = value.Company.RegPensionFund;
                RegEmploymentService = value.Company.RegEmploymentService;
                RegSocialInsuranceNesch = value.Company.RegSocialInsuranceNesch;
                RegSocialInsuranceDisability = value.Company.RegSocialInsuranceDisability;
                RegPfu = value.Company.RegPfu;
                RegOpfg = value.Company.RegOpfg;
                RegKoatu = value.Company.RegKoatu;
                RegKfv = value.Company.RegKfv;
                RegZkgng = value.Company.RegZkgng;
                RegKved = value.Company.RegKved;

                if(value.IsBank && value.Company.Bank!=null)
                {
                    Mfo = value.Company.Bank.Mfo;
                    SertificateNo = value.Company.Bank.SertificateNo;
                    SertificateDate = value.Company.Bank.SertificateDate;
                    LicenseNo = value.Company.Bank.LicenseNo;
                    LicenseDate = value.Company.Bank.LicenseDate;
                    Swift = value.Company.Bank.Swift;
                    CorrBankAccount = value.Company.Bank.Swift;
                }
            }
            if(value.IsPeople && value.People != null)
            {
                FirstName = value.People.FirstName;
                LastName = value.People.LastName;
                MidleName = value.People.MidleName;
                TaxSocialPrivilege = value.People.TaxSocialPrivilege;
                PlaceEmploymentBookId = value.People.PlaceEmploymentBookId;
                PlaceEmploymentBookName = value.People.PlaceEmploymentBookId == 0 ? string.Empty : value.People.PlaceEmploymentBook.Name;
                InsuranceNumber = value.People.InsuranceNumber;
                InsuranceSeries = value.People.InsuranceSeries;
                Invalidity = value.People.Invalidity;
                Pension = value.People.Pension;
                LegalWorker = value.People.LegalWorker;
                Sex = value.People.Sex;
                SexName = value.People.SexName;
                if(value.People.Employer!=null)
                {
                    Mol = value.People.Employer.Mol;
                    TabNo = value.People.Employer.TabNo;
                    DateStart = value.People.Employer.DateStart;
                    DateEnd = value.People.Employer.DateEnd;
                }
            }
            if (value.IsStore && value.Store != null)
            {
                StorekeeperId = value.Store.StorekeeperId==0? (int?)null: value.Store.StorekeeperId;
                StorekeeperName = value.Store.StorekeeperId == 0 ? string.Empty : value.Workarea.Cashe.GetCasheData<Agent>().Item(value.Store.StorekeeperId).Name;
            }
            
             
        }
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }

        #region Данные предприятия
        /// <summary>Форма собственности (сокращение)</summary>
        public string OwnerShip { get; set; }
        /// <summary>Идентификатор формы собственности</summary>
        public int? OwnerShipId { get; set; }

        /// <summary>Плательщик НДС</summary>
        public bool NdsPayer { get; set; }
        /// <summary>Номер свидетельства о регистрации</summary>
        public string RegNumber { get; set; }

        /// <summary>Окпо</summary>
        public string Okpo { get; set; }
        /// <summary>Тип торговой точки</summary>
        public int? TypeOutletId { get; set; }
        /// <summary>Наименование типа торговой точки</summary>
        public string TypeOutletName { get; set; }
        
        /// <summary>Идентификатор метража</summary>
        public int? MetricAreaId { get; set; }
        /// <summary>Наименование метража</summary>
        public string MetricAreaName { get; set; }
        
        /// <summary>Идентификатор категории торговой точки</summary>
        public int? CategoryId { get; set; }
        /// <summary>Наименование категория торговой точки</summary>
        public string CategoryName { get; set; }
        
        /// <summary> Международное название </summary>
        public string InternationalName { get; set; }
        /// <summary> Пенсионный фонд </summary>
        public string RegPensionFund { get; set; }
        /// <summary> Служба занятости </summary>
        public string RegEmploymentService { get; set; }
        /// <summary> Фонд социального тстрахования от несчастных случаев </summary>
        public string RegSocialInsuranceNesch { get; set; }
        /// <summary> Фонд социального страхования по временной потере трудоспособности </summary>
        public string RegSocialInsuranceDisability { get; set; }
        /// <summary> ПФУ </summary>
        public string RegPfu { get; set; }
        /// <summary> ОПФГ </summary>
        public string RegOpfg { get; set; }
        /// <summary> КОАТУ </summary>
        public string RegKoatu { get; set; }
        /// <summary> КФВ </summary>
        public string RegKfv { get; set; }
        /// <summary> ЗКГНГ </summary>
        public string RegZkgng { get; set; }
        /// <summary> КВЕД </summary>
        public string RegKved { get; set; }
        #endregion
        
        #region Физ лицо
        /// <summary>Фамилия</summary>
        public string FirstName { get; set; }
        /// <summary>Имя</summary>
        public string LastName { get; set; }
        /// <summary>Отчество</summary>
        public string MidleName { get; set; }

        /// <summary>Налоговые льготы</summary>
        public decimal TaxSocialPrivilege { get; set; }
        /// <summary>Идентификатор места хранения трудовой</summary>
        public int? PlaceEmploymentBookId { get; set; }
        /// <summary>Место хранения трудовой</summary>
        public string PlaceEmploymentBookName { get; set; }
        /// <summary>Номер страховки</summary>
        public string InsuranceNumber { get; set; }
        /// <summary>Серия страховки</summary>
        public string InsuranceSeries { get; set; }
        /// <summary> Инвалидность </summary>
        public bool Invalidity { get; set; }
        /// <summary> Пенсионер </summary>
        public bool Pension { get; set; }
        /// <summary> Легальный работник </summary>
        public bool LegalWorker { get; set; }
        /// <summary>Секс :-)</summary>
        public bool Sex { get; set; }
        /// <summary>Секс :-)</summary>
        public string SexName { get; set; }
        #endregion

        #region Склад
        /// <summary>Идентификатор заведующего складом</summary>
        public int? StorekeeperId { get; set; }

        /// <summary>Заведующий складом</summary>
        public string StorekeeperName { get; set; }
        #endregion

        #region Данные корреспондента

        /// <summary>Основная група</summary>
        public string DefaultGroup { get; set; }

        /// <summary> Физический адрес </summary>
        public string AddressPhysical { get; set; }

        /// <summary> Юр. адрес </summary>
        public string AddressLegal { get; set; }

        /// <summary>Телефонный номер</summary>
        public string Phone { get; set; }

        /// <summary>Налоговый номер</summary>
        public string TaxNumber { get; set; }

        #endregion

        #region Сотрудник
        /// <summary> Табельный номер </summary>
        public string TabNo { get; set; }
        /// <summary> Материально ответственный </summary>
        public bool Mol { get; set; }

        /// <summary>Дата принятия на работу</summary>
        public DateTime? DateStart { get; set; }
        /// <summary>Дата увольнения</summary>
        public DateTime? DateEnd { get; set; }
        #endregion


        #region Банк
        /// <summary>Мфо банка</summary>
        public string Mfo { get; set; }
        /// <summary>Номер свидетельства регистрации НБУ</summary>
        public string SertificateNo { get; set; }
        /// <summary>Дата свидетельства регистрации НБУ</summary>
        public DateTime? SertificateDate { get; set; }
        /// <summary>Номер банковской лицензии</summary>
        public string LicenseNo { get; set; }
        /// <summary>Дата банковской лицензии</summary>
        public DateTime? LicenseDate { get; set; }
        /// <summary>S.W.I.F.T.</summary>
        public string Swift { get; set; }
        /// <summary>Корреспондентский счет</summary>
        public string CorrBankAccount { get; set; }
        #endregion
        
    }
}