using System;

namespace BusinessObjects.Models
{
    /// <summary>
    /// ������ ���������
    /// </summary>
    public class AgentModel : BaseModel<Agent>
    {
        /// <summary>�����������</summary>
        public AgentModel()
        {
        }
        /// <summary>�����������</summary>
        public AgentModel(Agent value)
            : this()
        {
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            GetData(value);
// ReSharper restore DoNotCallOverridableMethodsInConstructor
        }
        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <param name="value">�������������</param>
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
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId { get; set; }
        /// <summary>������������ �����������, �������� ����������� ������</summary>
        public string MyCompanyName { get; set; }

        #region ������ �����������
        /// <summary>����� ������������� (����������)</summary>
        public string OwnerShip { get; set; }
        /// <summary>������������� ����� �������������</summary>
        public int? OwnerShipId { get; set; }

        /// <summary>���������� ���</summary>
        public bool NdsPayer { get; set; }
        /// <summary>����� ������������� � �����������</summary>
        public string RegNumber { get; set; }

        /// <summary>����</summary>
        public string Okpo { get; set; }
        /// <summary>��� �������� �����</summary>
        public int? TypeOutletId { get; set; }
        /// <summary>������������ ���� �������� �����</summary>
        public string TypeOutletName { get; set; }
        
        /// <summary>������������� �������</summary>
        public int? MetricAreaId { get; set; }
        /// <summary>������������ �������</summary>
        public string MetricAreaName { get; set; }
        
        /// <summary>������������� ��������� �������� �����</summary>
        public int? CategoryId { get; set; }
        /// <summary>������������ ��������� �������� �����</summary>
        public string CategoryName { get; set; }
        
        /// <summary> ������������� �������� </summary>
        public string InternationalName { get; set; }
        /// <summary> ���������� ���� </summary>
        public string RegPensionFund { get; set; }
        /// <summary> ������ ��������� </summary>
        public string RegEmploymentService { get; set; }
        /// <summary> ���� ����������� ������������ �� ���������� ������� </summary>
        public string RegSocialInsuranceNesch { get; set; }
        /// <summary> ���� ����������� ����������� �� ��������� ������ ���������������� </summary>
        public string RegSocialInsuranceDisability { get; set; }
        /// <summary> ��� </summary>
        public string RegPfu { get; set; }
        /// <summary> ���� </summary>
        public string RegOpfg { get; set; }
        /// <summary> ����� </summary>
        public string RegKoatu { get; set; }
        /// <summary> ��� </summary>
        public string RegKfv { get; set; }
        /// <summary> ����� </summary>
        public string RegZkgng { get; set; }
        /// <summary> ���� </summary>
        public string RegKved { get; set; }
        #endregion
        
        #region ��� ����
        /// <summary>�������</summary>
        public string FirstName { get; set; }
        /// <summary>���</summary>
        public string LastName { get; set; }
        /// <summary>��������</summary>
        public string MidleName { get; set; }

        /// <summary>��������� ������</summary>
        public decimal TaxSocialPrivilege { get; set; }
        /// <summary>������������� ����� �������� ��������</summary>
        public int? PlaceEmploymentBookId { get; set; }
        /// <summary>����� �������� ��������</summary>
        public string PlaceEmploymentBookName { get; set; }
        /// <summary>����� ���������</summary>
        public string InsuranceNumber { get; set; }
        /// <summary>����� ���������</summary>
        public string InsuranceSeries { get; set; }
        /// <summary> ������������ </summary>
        public bool Invalidity { get; set; }
        /// <summary> ��������� </summary>
        public bool Pension { get; set; }
        /// <summary> ��������� �������� </summary>
        public bool LegalWorker { get; set; }
        /// <summary>���� :-)</summary>
        public bool Sex { get; set; }
        /// <summary>���� :-)</summary>
        public string SexName { get; set; }
        #endregion

        #region �����
        /// <summary>������������� ����������� �������</summary>
        public int? StorekeeperId { get; set; }

        /// <summary>���������� �������</summary>
        public string StorekeeperName { get; set; }
        #endregion

        #region ������ ��������������

        /// <summary>�������� �����</summary>
        public string DefaultGroup { get; set; }

        /// <summary> ���������� ����� </summary>
        public string AddressPhysical { get; set; }

        /// <summary> ��. ����� </summary>
        public string AddressLegal { get; set; }

        /// <summary>���������� �����</summary>
        public string Phone { get; set; }

        /// <summary>��������� �����</summary>
        public string TaxNumber { get; set; }

        #endregion

        #region ���������
        /// <summary> ��������� ����� </summary>
        public string TabNo { get; set; }
        /// <summary> ����������� ������������� </summary>
        public bool Mol { get; set; }

        /// <summary>���� �������� �� ������</summary>
        public DateTime? DateStart { get; set; }
        /// <summary>���� ����������</summary>
        public DateTime? DateEnd { get; set; }
        #endregion


        #region ����
        /// <summary>��� �����</summary>
        public string Mfo { get; set; }
        /// <summary>����� ������������� ����������� ���</summary>
        public string SertificateNo { get; set; }
        /// <summary>���� ������������� ����������� ���</summary>
        public DateTime? SertificateDate { get; set; }
        /// <summary>����� ���������� ��������</summary>
        public string LicenseNo { get; set; }
        /// <summary>���� ���������� ��������</summary>
        public DateTime? LicenseDate { get; set; }
        /// <summary>S.W.I.F.T.</summary>
        public string Swift { get; set; }
        /// <summary>����������������� ����</summary>
        public string CorrBankAccount { get; set; }
        #endregion
        
    }
}