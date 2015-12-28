using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств
    /// </summary>
    internal sealed class BuildControlAgent : BasePropertyControlIBase<Agent>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlAgent()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_AGENTBANK, ExtentionString.CONTROL_AGENTBANK);
            TotalPages.Add(ExtentionString.CONTROL_ADDRESSINFO, ExtentionString.CONTROL_ADDRESSINFO);
            TotalPages.Add(ExtentionString.CONTROL_AGENT_CODES, ExtentionString.CONTROL_AGENT_CODES);
            TotalPages.Add(ExtentionString.CONTROL_AGENT_PASSPORT, ExtentionString.CONTROL_AGENT_PASSPORT);
            TotalPages.Add(ExtentionString.CONTROL_AGENT_DRIVINGLICENCE, ExtentionString.CONTROL_AGENT_DRIVINGLICENCE);
            TotalPages.Add(ExtentionString.CONTROL_BANKACC_NAME, ExtentionString.CONTROL_BANKACC_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CONTACT_NAME, ExtentionString.CONTROL_CONTACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINKFILES, ExtentionString.CONTROL_LINKFILES);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_KNOWLEDGES, ExtentionString.CONTROL_KNOWLEDGES);
            TotalPages.Add(ExtentionString.CONTROL_NOTES, ExtentionString.CONTROL_NOTES);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            if(!SelectedItem.IsCompany)
            {
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_AGENT_CODES))
                    TotalPages.Remove(ExtentionString.CONTROL_AGENT_CODES);
            }
            if(!SelectedItem.IsBank)
            {
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_AGENTBANK))
                    TotalPages.Remove(ExtentionString.CONTROL_AGENTBANK);
            }
            if (SelectedItem.KindValue != 2)
            {
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_AGENT_PASSPORT))
                    TotalPages.Remove(ExtentionString.CONTROL_AGENT_PASSPORT);
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_AGENT_DRIVINGLICENCE))
                    TotalPages.Remove(ExtentionString.CONTROL_AGENT_DRIVINGLICENCE);
            }

        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.CodeTax = _common.txtCodeTax.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.AddressLegal = _common.editAddressLegal.Text;
            SelectedItem.AddressPhysical = _common.editAddressPhysical.Text;
            SelectedItem.AmmountTrust = _common.editAmmountTrust.Value;
            SelectedItem.TimeDelay = Convert.ToInt32(_common.editTimeDelay.Text);
            SelectedItem.Phone = _common.txtPhone.Text;
            SaveStateData();
            if (SelectedItem.IsPeople)
            {
                _people.Sex = _common.cmdSex.SelectedIndex == 1 ? true : false;
                _people.TaxSocialPrivilege = _common.editTaxSocialPrivilege.Value;
                _people.MidleName = _common.txtMidleName.Text;
                _people.FirstName = _common.txtFirstName.Text;
                _people.LastName = _common.txtLastName.Text;
                _employer.TabNo = _common.txtTabNo.Text;
                _employer.Mol = _common.checkMol.Checked;
                _employer.DateStart = _common.dateEditStart.DateTime;
                _employer.DateEnd = _common.dateEditEnd.DateTime;
                if(_common.cmbEmloyerCategory.EditValue!=null)
                    _employer.CategoryId = (int)_common.cmbEmloyerCategory.EditValue;

                _people.InsuranceNumber = _common.txtInsuranceNumber.Text;
                _people.InsuranceSeries = _common.txtInsuranceSeries.Text;
                if (_common.cmbLastPlaceWork.EditValue!=null)
                    _people.LastPlaceWorkId = (int)_common.cmbLastPlaceWork.EditValue;
                _people.Invalidity = _common.checkInvalidity.Checked;
                _people.Pension = _common.checkPension.Checked;
                _people.LegalWorker = _common.checkLegalWorker.Checked;
                if (_common.cmbPlaceEmploymentBook.EditValue!=null)
                    _people.PlaceEmploymentBookId = (int)_common.cmbPlaceEmploymentBook.EditValue;
                if (_common.cmbMinors.EditValue!=null)
                    _people.MinorsId = (int)_common.cmbMinors.EditValue;
            }
            if(SelectedItem.IsStore)
            {
                if (_common.cmbStorekeeper.EditValue != null)
                    _store.StorekeeperId = (int)_common.cmbStorekeeper.EditValue;
                else
                    _store.StorekeeperId = 0;
                
            }
            if (SelectedItem.IsCompany)
            {
                _company.StateId = SelectedItem.StateId;
                _company.Okpo = _common.txtOkpo.Text;
                _company.NdsPayer = _common.checkNdsPayer.Checked;
                _company.Tax = _common.editTax.Value;
                if (_common.dtRegDate.EditValue != null)
                    _company.RegDate = _common.dtRegDate.DateTime;
                else
                    _company.RegDate = null;

                if (_common.cmbRegisteredBy.EditValue !=null)
                    _company.RegisteredById = (int) _common.cmbRegisteredBy.EditValue;
                _company.ActivityEconomic = _common.txtActivityEconomic.Text;
                if (_common.cmbIndustry.EditValue!=null)
                    _company.IndustryId = (int) _common.cmbIndustry.EditValue;
                _company.RegNumber = _common.textRegNumber.Text;
                if (_common.cmbTypeOutlet.EditValue!=null)
                    _company.TypeOutletId = (int) _common.cmbTypeOutlet.EditValue;
                if (_common.cmbMetricArea.EditValue!=null)
                    _company.MetricAreaId = (int) _common.cmbMetricArea.EditValue;
                if (_common.cmbCategory.EditValue!=null)
                    _company.CategoryId = (int) _common.cmbCategory.EditValue;
                if (_common.cmbSalesRepresentative.EditValue!=null)
                    _company.SalesRepresentativeId = (int) _common.cmbSalesRepresentative.EditValue;
                if (_common.cmbOwnership.EditValue!=null)
                    _company.OwnershipId = (int)_common.cmbOwnership.EditValue;
                if (_controlCodes != null)
                {
                    _company.TaxInspectionId = (int) _controlCodes.cmbTaxInspection.EditValue;
                    _company.RegPensionFund = _controlCodes.txtRegPensionFund.Text;
                    _company.RegEmploymentService = _controlCodes.txtRegEmploymentService.Text;
                    _company.RegSocialInsuranceDisability = _controlCodes.txtRegSocialInsuranceDisability.Text;
                    _company.RegSocialInsuranceNesch = _controlCodes.txtRegSocialInsuranceNesch.Text;
                    _company.RegPfu = _controlCodes.txtRegPfu.Text;
                    _company.RegOpfg = _controlCodes.txtRegOpfg.Text;
                    _company.RegKoatu = _controlCodes.txtRegKoatu.Text;
                    _company.RegKfv = _controlCodes.txtRegKfv.Text;
                    _company.RegZkgng = _controlCodes.txtRegZkgng.Text;
                    _company.RegKved = _controlCodes.txtRegKved.Text;
                    _company.DirectorId = (int) _controlCodes.cmbDirector.EditValue;
                    _company.BuhId = (int) _controlCodes.cmbBuh.EditValue;
                    _company.CashierId = (int) _controlCodes.cmbCashier.EditValue;
                    _company.PersonnelId = (int) _controlCodes.cmbPersonnel.EditValue;
                }
                if (SelectedItem.IsBank)
                {
                    _company.Bank.Mfo = _common.txtMfo.Text;

                    if(_controlAgentBank!=null)
                    {
                        _company.Bank.CorrBankAccount = _controlAgentBank.txtCorrBankAccount.Text;
                        _company.Bank.LicenseNo = _controlAgentBank.txtLicenseNo.Text;
                        _company.Bank.SertificateNo = _controlAgentBank.txtSertificateNo.Text;
                        _company.Bank.Swift = _controlAgentBank.txtSwift.Text;
                        _company.Bank.CorrBankAccount = _controlAgentBank.txtCorrBankAccount.Text;

                        if (_controlAgentBank.dtLicenseDate.EditValue != null)
                            _company.Bank.LicenseDate = _controlAgentBank.dtLicenseDate.DateTime;
                        else
                            _company.Bank.LicenseDate = null;

                        if (_controlAgentBank.dtSertificateDate.EditValue != null)
                            _company.Bank.SertificateDate = _controlAgentBank.dtSertificateDate.DateTime;
                        else
                            _company.Bank.SertificateDate = null;
                    }
                }
            }
            InternalSave();
            
        }

        /// <summary>
        /// Механизм внутреннего сохранения объекта с соответствующей обработкой исключений
        /// </summary>
        public override void InternalSave()
        {
            try
            {
                CanClose = true;
                if (SelectedItem.ValidateRuleSet())
                    SelectedItem.Save();
                else
                    SelectedItem.ShowDialogValidationErrors();

                if (SelectedItem.IsPeople)
                {
                    if (_people.Id == 0)
                        _people.Id = SelectedItem.Id;

                    if (_people.ValidateRuleSet())
                        _people.Save();
                    else
                        _people.ShowDialogValidationErrors();

                    if (_employer.ValidateRuleSet())
                        _employer.Save();
                    else
                        _employer.ShowDialogValidationErrors();
                }
                if (SelectedItem.IsCompany)
                {
                    _company.FlagsValue = SelectedItem.FlagsValue;
                    _company.StateId = SelectedItem.StateId;
                    if (_company.ValidateRuleSet())
                        _company.Save();
                    else
                        _company.ShowDialogValidationErrors();
                }
                if(SelectedItem.IsStore)
                {
                    if (_store.Id == 0)
                        _store.Id = SelectedItem.Id;
                    _store.FlagsValue = SelectedItem.FlagsValue;
                    _store.StateId = SelectedItem.StateId;
                    if (_store.ValidateRuleSet())
                        _store.Save();
                    else
                        _store.ShowDialogValidationErrors();
                    
                }
                if (SelectedItem.IsBank)
                {
                    if (_company.Bank.Id == 0)
                        _company.Bank.Id = SelectedItem.Id;
                    _company.Bank.FlagsValue = SelectedItem.FlagsValue;
                    _company.Bank.StateId = SelectedItem.StateId;
                    if (_company.ValidateRuleSet())
                        _company.Bank.Save();
                    else
                        _company.ShowDialogValidationErrors();
                }
            }
            catch (DatabaseException dbe)
            {
                CanClose = false;
                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
            }
            catch (Exception ex)
            {
                CanClose = false;
                Extentions.ShowMessagesExeption(SelectedItem.Workarea,
                                                SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049),
                                                ex);
            }
        }

        /// <summary>
        /// Метод построения страницы свойств по коду страницы
        /// </summary>
        /// <param name="value">Код страницы</param>
        protected override void BuildPage(string value)
        {
            if (_company == null)
                _company = SelectedItem.Company;
            if (_people == null)
                _people = SelectedItem.People;
            if (_employer == null)
                _employer = _people.Employer;
            if (_store == null)
                _store = SelectedItem.Store;

            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_BANKACC_NAME)
                BuildPageBankAccount();
            if (value == ExtentionString.CONTROL_CONTACT_NAME)
                BuildPageContacts();
            if (value == ExtentionString.CONTROL_AGENT_CODES)
                BuildPageAgentCodes();
            if (value == ExtentionString.CONTROL_AGENT_PASSPORT)
                BuildPageAgentPassport();
            if (value == ExtentionString.CONTROL_AGENT_DRIVINGLICENCE)
                BuildPageAgentDrivingLicence();
            if (value == ExtentionString.CONTROL_LINKFILES)
                BuildPageLinkedFiles();
            if (value == ExtentionString.CONTROL_AGENTBANK)
                BuildPageBank();
            if (value == ExtentionString.CONTROL_ADDRESSINFO)
                BuildPageAddress();
        }
        private Company _company;
        private People _people;
        private Employer _employer;
        private Store _store;
        private BindingSource _bindRegisteredBy;
        private List<Agent> _collRegisteredBy;
        private BindingSource _bindStoreKeep;
        private List<Agent> _collStoreKeep;
        private BindingSource _bindSalesRepresentative;
        private List<Agent> _collSalesRepresentative;
        private BindingSource _bindOwnership;
        private List<Analitic> _collOwnership;
        private BindingSource _bindIndustry;
        private List<Analitic> _collIndustry;
        private BindingSource _bindTypeOutlet;
        private List<Analitic> _collTypeOutlet;
        private BindingSource _bindMetricArea;
        private List<Analitic> _collMetricArea;
        private BindingSource _bindCategory;
        private List<Analitic> _collCategory;
        private BindingSource _bindEmployerCategory;
        private List<Analitic> _collEmployerCategory;
        private BindingSource _bindPlaceEmploymentBook;
        private List<Analitic> _collPlaceEmploymentBook;
        private BindingSource _bindMinors;
        private List<Analitic> _collMinors;
        private BindingSource _bindLastPlaceWork;
        private List<Agent> _collLastPlaceWork;
        ControlAgent _common;
        private PopupMenu imageEditPopupMenu;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlAgent
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  txtCodeTax = {Text = SelectedItem.CodeTax},
                                  txtCodeFind = {Text = SelectedItem.CodeFind},
                                  txtNameFull2 = {Text = SelectedItem.NameFull},
                                  editAddressLegal = {Text = SelectedItem.AddressLegal},
                                  editAddressPhysical = {Text = SelectedItem.AddressPhysical},
                                  editAmmountTrust = {Value = SelectedItem.AmmountTrust},
                                  editTimeDelay =
                                      {
                                          Text = Convert.ToString(SelectedItem.TimeDelay),
                                      },
                                  txtPhone = {Text = SelectedItem.Phone},
                                  Workarea = SelectedItem.Workarea,
                                  Key = SelectedItem.KindName
                              };

                #region Настройка расположения элементов управления
                //// Поиск данных о настройке
                //string controlName = string.Empty;
                //string entityKind = string.Empty;
                //string keyValue = _common.Tag != null ? _common.Tag.ToString() : _common.GetType().Name;

                //if (!string.IsNullOrWhiteSpace((Owner as IWorkareaForm).Key))
                //    controlName = (Owner as IWorkareaForm).Key;

                //entityKind = SelectedItem.KindId.ToString();

                //// Общие поисковые данные
                //List<XmlStorage> collSet = SelectedItem.Workarea.Empty<XmlStorage>().FindBy(kindId: 2359299,
                //                                                                            name: controlName,
                //                                                                            code: keyValue,
                //                                                                            flagString: entityKind);
                //if (collSet.Count > 0)
                //{
                //    // Уточняющий подзапрос
                //    XmlStorage setiings = collSet.FirstOrDefault(f => f.KindId == 2359299
                //                                                                && f.Name == controlName
                //                                                                && f.Code == keyValue
                //                                                                && f.FlagString == entityKind);
                //    if (setiings != null && !string.IsNullOrWhiteSpace(setiings.XmlData))
                //    {
                //        MemoryStream s = new MemoryStream();
                //        StreamWriter w = new StreamWriter(s) { AutoFlush = true };
                //        w.Write(setiings.XmlData);
                //        s.Position = 0;
                //        try
                //        {
                //            _common.LayoutControl.RestoreLayoutFromStream(s);
                //        }
                //        catch (Exception)
                //        {

                //        }
                //    }
                //}
                ////
                //if (SelectedItem.Workarea.Access.RightCommon.AdminEnterprize)
                //{
                //    _common.LayoutControl.AllowCustomizationMenu = true;
                //    _common.LayoutControl.RegisterUserCustomizatonForm(typeof(FormCustomLayout));
                //}
                //else
                //{
                //    _common.LayoutControl.AllowCustomizationMenu = false;
                //} 
                #endregion
                
                if (SelectedItem.IsCompany)
                {
                    _common.layoutControlGroupCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlGroupCompany.Expanded = true;

                    _common.txtOkpo.Text = _company.Okpo;
                    _common.checkNdsPayer.Checked = _company.NdsPayer;
                    _common.editTax.Value = _company.Tax;
                    if (_company.RegDate.HasValue)
                        _common.dtRegDate.DateTime = _company.RegDate.Value;
                    _common.textRegNumber.Text = _company.RegNumber;
                    _common.txtActivityEconomic.Text = _company.ActivityEconomic;
                    #region Данные для списка "Корреспондент - кем зарегистрирован"
                    _common.cmbRegisteredBy.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbRegisteredBy.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindRegisteredBy = new BindingSource();
                    _collRegisteredBy = new List<Agent>();
                    if (_company.RegisteredById != 0)
                        _collRegisteredBy.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_company.RegisteredById));
                    _bindRegisteredBy.DataSource = _collRegisteredBy;
                    _common.cmbRegisteredBy.Properties.DataSource = _bindRegisteredBy;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewRegisteredBy, "DEFAULT_LOOKUPAGENT");
                    _common.cmbRegisteredBy.Properties.View.BestFitColumns();
                    _common.cmbRegisteredBy.EditValue = _company.RegisteredById;
                    _common.ViewRegisteredBy.CustomUnboundColumnData += ViewRegisteredByCustomUnboundColumnData;
                    _common.cmbRegisteredBy.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbRegisteredBy.ButtonClick += CmbRegisteredByButtonClick;
                    _common.cmbRegisteredBy.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbRegisteredBy.EditValue = 0;
                    };
                    #endregion
                    #region Данные для списка "Аналитика - форма собственности"
                    _common.cmbOwnership.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbOwnership.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindOwnership = new BindingSource();
                    _collOwnership = new List<Analitic>();
                    if (_company.OwnershipId != 0)
                        _collOwnership.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(_company.OwnershipId));
                    _bindOwnership.DataSource = _collOwnership;
                    _common.cmbOwnership.Properties.DataSource = _bindOwnership;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewActivityBranche, "DEFAULT_LOOKUP_ANALITIC_OWNERSHIP");
                    _common.cmbOwnership.EditValue = _company.OwnershipId;
                    _common.cmbOwnership.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbOwnership.ButtonClick += CmbActivityBrancheButtonClick;
                    _common.cmbOwnership.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbOwnership.EditValue = 0;
                    };
                    #endregion
                    #region Аналитика - отрасль
                    _common.cmbIndustry.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbIndustry.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindIndustry = new BindingSource();
                    _collIndustry = new List<Analitic>();
                    if (_company.IndustryId != 0)
                        _collIndustry.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(_company.IndustryId));
                    _bindIndustry.DataSource = _collIndustry;
                    _common.cmbIndustry.Properties.DataSource = _bindIndustry;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewIndustry, "DEFAULT_LOOKUP_NAME");
                    _common.cmbIndustry.EditValue = _company.IndustryId;
                    _common.cmbIndustry.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbIndustry.ButtonClick += CmbIndustryButtonClick;
                    #endregion
                    #region Аналитика - тип торговой точки
                    _common.cmbTypeOutlet.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbTypeOutlet.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindTypeOutlet = new BindingSource();
                    _collTypeOutlet = new List<Analitic>();
                    if (_company.TypeOutletId != 0)
                        _collTypeOutlet.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(_company.TypeOutletId));
                    _bindTypeOutlet.DataSource = _collTypeOutlet;
                    _common.cmbTypeOutlet.Properties.DataSource = _bindTypeOutlet;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewTypeOutlet, "DEFAULT_LOOKUP_NAME");
                    _common.cmbTypeOutlet.EditValue = _company.TypeOutletId;
                    _common.cmbTypeOutlet.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbTypeOutlet.ButtonClick += CmbTypeOutletButtonClick;
                    _common.cmbTypeOutlet.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbTypeOutlet.EditValue = 0;
                    };
                    #endregion
                    #region Аналитика - метраж
                    _common.cmbMetricArea.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbMetricArea.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindMetricArea = new BindingSource();
                    _collMetricArea = new List<Analitic>();
                    if (_company.MetricAreaId != 0)
                        _collMetricArea.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(_company.MetricAreaId));
                    _bindMetricArea.DataSource = _collMetricArea;
                    _common.cmbMetricArea.Properties.DataSource = _bindMetricArea;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewMetricArea, "DEFAULT_LOOKUP_NAME");
                    _common.cmbMetricArea.EditValue = _company.MetricAreaId;
                    _common.cmbMetricArea.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbMetricArea.ButtonClick += CmbMetricAreaButtonClick;
                    _common.cmbMetricArea.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbMetricArea.EditValue = 0;
                    };
                    #endregion
                    #region Аналитика - категория торговой точки
                    _common.cmbCategory.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbCategory.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindCategory = new BindingSource();
                    _collCategory = new List<Analitic>();
                    if (_company.CategoryId != 0)
                        _collCategory.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(_company.CategoryId));
                    _bindCategory.DataSource = _collCategory;
                    _common.cmbCategory.Properties.DataSource = _bindCategory;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewCategory, "DEFAULT_LOOKUP_NAME");
                    _common.cmbCategory.EditValue = _company.CategoryId;
                    _common.cmbCategory.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbCategory.ButtonClick += CmbCategoryButtonClick;
                    _common.cmbCategory.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbCategory.EditValue = 0;
                    };
                    #endregion
                    #region Данные для списка "Корреспондент - торговый представитель"
                    _common.cmbSalesRepresentative.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbSalesRepresentative.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindSalesRepresentative = new BindingSource();
                    _collSalesRepresentative = new List<Agent>();
                    if (_company.SalesRepresentativeId != 0)
                        _collSalesRepresentative.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_company.SalesRepresentativeId));
                    _bindSalesRepresentative.DataSource = _collSalesRepresentative;
                    _common.cmbSalesRepresentative.Properties.DataSource = _bindSalesRepresentative;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewSalesRepresentative, "DEFAULT_LOOKUPAGENT");
                    _common.cmbSalesRepresentative.Properties.View.BestFitColumns();
                    _common.cmbSalesRepresentative.EditValue = _company.SalesRepresentativeId;
                    _common.ViewSalesRepresentative.CustomUnboundColumnData += ViewSalesRepresentativeCustomUnboundColumnData;
                    _common.cmbSalesRepresentative.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbSalesRepresentative.ButtonClick += CmbSalesRepresentativeButtonClick;
                    #endregion
                }
                else
                    _common.layoutControlGroupCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                if (SelectedItem.IsPeople)
                {
                    _common.layoutControlGroupPeople.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlGroupPeople.Expanded = true;

                    _common.cmdSex.SelectedIndex = _people.Sex ? 1 : 0;
                    _common.editTaxSocialPrivilege.Value = _people.TaxSocialPrivilege;
                    _common.txtMidleName.Text = _people.MidleName;
                    _common.txtFirstName.Text = _people.FirstName;
                    _common.txtLastName.Text = _people.LastName;
                    _common.txtTabNo.Text = _employer.TabNo;
                    _common.checkMol.Checked = _employer.Mol;
                    _common.txtInsuranceNumber.Text = _people.InsuranceNumber;
                    _common.txtInsuranceSeries.Text = _people.InsuranceSeries;
                    _common.cmbLastPlaceWork.EditValue = _people.LastPlaceWorkId;
                    _common.checkInvalidity.Checked = _people.Invalidity;
                    _common.checkPension.Checked = _people.Pension;
                    _common.checkLegalWorker.Checked = _people.LegalWorker;

                    if (_employer.DateStart.HasValue)
                        _common.dateEditStart.DateTime = _employer.DateStart.Value;
                    if (_employer.DateEnd.HasValue)
                        _common.dateEditEnd.DateTime = _employer.DateEnd.Value;

                    #region Аналитика - категория сторудника
                    _common.cmbEmloyerCategory.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbEmloyerCategory.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindEmployerCategory = new BindingSource();
                    _collEmployerCategory = new List<Analitic>();
                    if (_employer.CategoryId != 0)
                        _collEmployerCategory.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(_employer.CategoryId));
                    _bindEmployerCategory.DataSource = _collEmployerCategory;
                    _common.cmbEmloyerCategory.Properties.DataSource = _bindEmployerCategory;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewEmployerCategory, "DEFAULT_LOOKUP_NAME");
                    _common.cmbEmloyerCategory.EditValue = _employer.CategoryId;
                    _common.cmbEmloyerCategory.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    //_common.cmbPlaceEmploymentBook.ButtonClick += CmbPlaceEmploymentBookButtonClick;
                    _common.cmbEmloyerCategory.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbEmloyerCategory.EditValue = 0;
                    };
                    #endregion
                    
                    #region Аналитика - расположение трудовой книжки
                    _common.cmbPlaceEmploymentBook.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbPlaceEmploymentBook.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindPlaceEmploymentBook = new BindingSource();
                    _collPlaceEmploymentBook = new List<Analitic>();
                    if (_people.PlaceEmploymentBookId != 0)
                        _collPlaceEmploymentBook.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(_people.PlaceEmploymentBookId));
                    _bindPlaceEmploymentBook.DataSource = _collPlaceEmploymentBook;
                    _common.cmbPlaceEmploymentBook.Properties.DataSource = _bindPlaceEmploymentBook;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewPlaceEmploymentBook, "DEFAULT_LOOKUP_NAME");
                    _common.cmbPlaceEmploymentBook.EditValue = _people.PlaceEmploymentBookId;
                    _common.cmbPlaceEmploymentBook.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    //_common.cmbPlaceEmploymentBook.ButtonClick += CmbPlaceEmploymentBookButtonClick;
                    _common.cmbPlaceEmploymentBook.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbPlaceEmploymentBook.EditValue = 0;
                    };
                    #endregion

                    #region Аналитика - вид несовершеннролетия
                    _common.cmbMinors.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbMinors.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindMinors = new BindingSource();
                    _collMinors = new List<Analitic>();
                    if (_people.MinorsId != 0)
                        _collMinors.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(_people.MinorsId));
                    _bindMinors.DataSource = _collMinors;
                    _common.cmbMinors.Properties.DataSource = _bindMinors;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewMinors, "DEFAULT_LOOKUP_NAME");
                    _common.cmbMinors.EditValue = _people.MinorsId;
                    _common.cmbMinors.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    //_common.cmbPlaceEmploymentBook.ButtonClick += CmbPlaceEmploymentBookButtonClick;
                    _common.cmbMinors.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbMinors.EditValue = 0;
                    };
                    #endregion

                    #region Данные для списка "Корреспондент - прошлое место работы"
                    _common.cmbLastPlaceWork.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbLastPlaceWork.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindLastPlaceWork = new BindingSource();
                    _collLastPlaceWork = new List<Agent>();
                    if (_people.LastPlaceWorkId != 0)
                        _collLastPlaceWork.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_people.LastPlaceWorkId));
                    _bindLastPlaceWork.DataSource = _collLastPlaceWork;
                    _common.cmbLastPlaceWork.Properties.DataSource = _bindLastPlaceWork;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewLastPlaceWork, "DEFAULT_LOOKUPAGENT");
                    _common.cmbLastPlaceWork.Properties.View.BestFitColumns();
                    _common.cmbLastPlaceWork.EditValue = _people.LastPlaceWorkId;
                    //_common.ViewLastPlaceWork.CustomUnboundColumnData += ViewSalesRepresentativeCustomUnboundColumnData;
                    _common.cmbLastPlaceWork.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbLastPlaceWork.ButtonClick += CmbLastPlaceWorkButtonClick;
                    #endregion
                }
                else
                    _common.layoutControlGroupPeople.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                if (SelectedItem.IsStore)
                {
                    _common.layoutControlItemStorekeeper.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    #region Данные для списка "Корреспондент - торговый представитель"
                    _common.cmbStorekeeper.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbStorekeeper.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindStoreKeep = new BindingSource();
                    _collStoreKeep= new List<Agent>();
                    if (_store.StorekeeperId!= 0)
                        _collStoreKeep.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_store.StorekeeperId));
                    _bindStoreKeep.DataSource = _collStoreKeep;
                    _common.cmbStorekeeper.Properties.DataSource = _bindStoreKeep;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewStorekeeper, "DEFAULT_LOOKUPAGENT");
                    _common.cmbStorekeeper.Properties.View.BestFitColumns();
                    _common.cmbStorekeeper.EditValue = _store.StorekeeperId;
                    _common.ViewStorekeeper.CustomUnboundColumnData += ViewStoreKeepCustomUnboundColumnData;
                    _common.cmbStorekeeper.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbStorekeeper.ButtonClick += CmbStoreButtonClick;
                    #endregion
                }
                else
                    _common.layoutControlItemStorekeeper.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                if(SelectedItem.IsBank)
                {
                    _common.layoutControlItemMfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //_common.layoutControlGroupBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    _common.layoutControlItemMfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //_common.layoutControlGroupBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

                if(SelectedItem.IsBank)
                {
                    _common.txtMfo.Text = _company.Bank.Mfo;
                }
                UIHelper.GenerateTooltips<Agent>(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                if (!SelectedItem.IsNew && SelectedItem.IsReadOnly)
                {
                    _common.Enabled = false;
                }
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            CurrentPrintControl = _common.LayoutControl;
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }

        #region Страница "Файлы"
        private List<IChainAdvanced<Agent, FileData>> _collectionFiles;
        private BindingSource _bindFiles;
        private DevExpress.XtraGrid.GridControl _gridFiles;
        public GridView ViewFiles;

        protected void BuildPageLinkedFiles()
        {

            if (_gridFiles == null)
            {
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LINKFILES)];
                RibbonPageGroup groupLinksActionFiles = new RibbonPageGroup();
                //RibbonPageGroup groupLinksActionFiles = page.GetGroupByName(page.Name + "_ACTIONLIST");

                groupLinksActionFiles = new RibbonPageGroup { Name = page.Name + "_ACTIONLIST", Text = "Действия с файлами" };

                #region Добавить новый файл
                BarButtonItem btnFileCreate = new BarButtonItem
                {
                    Name = "btnFileCreate",
                    ButtonStyle = BarButtonStyle.Default,
                    Caption = "Добавить новый файл",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.NEW_X32), "Добавить новый файл",
                                                  "Добавление файла и связывание его с текущим документом из файловой системы. Не забудьте выбрать правильный фильтр отображаемых файлов!")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileCreate);
                btnFileCreate.ItemClick += BtnFileCreateItemClick;
                #endregion

                #region Связать с файлом
                BarButtonItem btnFileLink = new BarButtonItem
                {
                    Name = "btnFileLink",
                    Caption = "Связать с файлом",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINKNEW_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, "LINKNEW32"), "Связать с файлом",
                                                  "Связать с файлом уже зарегестрированных в базе данных на текущего корреспондента."),
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileLink);
                btnFileLink.ItemClick += BtnFileLinkItemClick;

                #endregion

                #region Экспорт файла
                BarButtonItem btnFileExport = new BarButtonItem
                {
                    Name = "btnFileExport",
                    Caption = "Экспорт файла",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAOUT_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAOUT_X32), "Экспорт файла",
                                                  "Экспор файла из базы данных для просмотра или изменений. После изменений необходимо повторно импортировать файл в базу данных")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileExport);
                btnFileExport.ItemClick += BtnFileExportItemClick;
                #endregion

                #region Просмотр файла
                BarButtonItem btnFilePreview = new BarButtonItem
                {
                    Name = "btnFilePreview",
                    Caption = "Просмотр файла",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = BusinessObjects.Windows.Properties.Resources.PREVIEW_X32,
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, "PREVIEW32"), "Просмотр файла",
                                                  "Просмотр файла приложением соответствующим для данного файла, программа должна быть установлена на Вашем компьютере. После просмотра в окне сообщения нажмите кнопку <b>Ок</b> для удаления временно созданного файла <br><i>(не нажимайте кнопку <b>Ок</b> до закрытия файла)</i>")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFilePreview);
                btnFilePreview.ItemClick += BtnFilePreviewItemClick;
                #endregion

                #region Удаление файла
                BarButtonItem btnFileDelete = new BarButtonItem
                {
                    Name = "btnFileDelete",
                    Caption = "Удалить файл",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32), "Удалить файл",
                                                  "Удаление файла связанного с документом. Возможно полное удаление - удаление связи и файла и удаление только связи.")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileDelete);
                btnFileDelete.ItemClick += BtnFileDeleteItemClick;

                #endregion
                page.Groups.Add(groupLinksActionFiles);

                _gridFiles = new DevExpress.XtraGrid.GridControl();
                ViewFiles = new GridView();
                _gridFiles.Dock = DockStyle.Fill;
                _gridFiles.ViewCollection.Add(ViewFiles);
                _gridFiles.MainView = ViewFiles;
                ViewFiles.GridControl = _gridFiles;

                ViewFiles.OptionsBehavior.AllowIncrementalSearch = true;
                ViewFiles.OptionsBehavior.CacheValuesOnRowUpdating = DevExpress.Data.CacheRowValuesMode.Disabled;
                ViewFiles.OptionsBehavior.Editable = false;
                ViewFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
                ViewFiles.OptionsView.ShowGroupPanel = false;
                ViewFiles.OptionsView.ShowIndicator = false;
                _gridFiles.ShowOnlyPredefinedDetails = true;
                Control.Controls.Add(_gridFiles);
                _gridFiles.Dock = DockStyle.Fill;
                //Form.clientPanel.Controls.Add(_gridFiles);

                _gridFiles.Name = ExtentionString.CONTROL_LINKFILES;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, ViewFiles, "DEFAULT_LISTVIEWCONTRACTFILES");
                // TODO: Заменить 13 на правильное представление типа связи
                _collectionFiles = SelectedItem.GetLinkedFiles().Where(s => s.StateId == State.STATEACTIVE).ToList();
                _bindFiles = new BindingSource { DataSource = _collectionFiles };
                _gridFiles.DataSource = _bindFiles;
                ViewFiles.CustomUnboundColumnData += ViewCustomUnboundColumnDataFiles;
                _gridFiles.DoubleClick += GridFilesDoubleClick;

            }
            HidePageControls(ExtentionString.CONTROL_LINKFILES);
        }
        // Обработка отрисовки изображения файлов в списке
        void ViewCustomUnboundColumnDataFiles(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Agent, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Agent, FileData>;
                if (link != null && link.Right != null)
                {
                    e.Value = link.Right.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Agent, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Agent, FileData>;
                if (link != null)
                {
                    e.Value = link.State.GetImage();
                }
            }
        }
        private void BtnFileDeleteItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileDelete();
        }

        protected void InvokeFileDelete()
        {
            if (_bindFiles.Current == null) return;
            ChainAdvanced<Agent, FileData> link = _bindFiles.Current as ChainAdvanced<Agent, FileData>;
            if (link == null) return;
            // TODO: Использовать строку ресурсов
            int res = Extentions.ShowMessageChoice(SelectedItem.Workarea, "Удаление файла", "Удаление файла", "Удаление данных о файлах связанных с данным документом. Удаление связи удаляет только связь с данным файлом, удаление связи и файла удаляет все данные включая файл.", "Удалить только связь|Удалить связь и файл");
            switch (res)
            {
                case 0:
                    try
                    {
                        link.StateId = State.STATEDELETED;
                        link.Save();
                        _bindFiles.RemoveCurrent();
                    }
                    catch (DatabaseException dbe)
                    {
                        // TODO: Использовать строку ресурсов
                        Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 1:
                    try
                    {
                        link.StateId = State.STATEDELETED;
                        link.Save();
                        link.Right.StateId = State.STATEDELETED;
                        link.Right.Save();
                        _bindFiles.RemoveCurrent();
                    }
                    catch (DatabaseException dbe)
                    {
                        // TODO: Использовать строку ресурсов
                        Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                                               SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                                                                   SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void BtnFilePreviewItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFilePreview();
        }

        private void BtnFileExportItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileExport();
        }

        protected void InvokeFileExport()
        {
            try
            {
                if (_bindFiles.Current == null) return;
                ChainAdvanced<Agent, FileData> link = _bindFiles.Current as ChainAdvanced<Agent, FileData>;
                if (link == null) return;
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = link.Right.Name,
                    DefaultExt = link.Right.FileExtention,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    link.Right.ExportStreamDataToFile(dlg.FileName);
                    System.Diagnostics.Process.Start(dlg.FileName);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFileLinkItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileLink();
        }

        protected void InvokeFileLink()
        {
            int AgentIdRelatedFiles = 0;
            int currentAgId = AgentIdRelatedFiles;
            List<FileData> collFilesToBrowse = FileData.GetCollectionClientFiles(SelectedItem.Workarea, currentAgId);
            List<FileData> retColl = SelectedItem.Workarea.Empty<FileData>().BrowseList(null, collFilesToBrowse.Count == 0 ? null : collFilesToBrowse);
            if (retColl == null || retColl.Count == 0) return;
            ChainKind ckind = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(
                    f =>
                    f.Code == ChainKind.FILE & f.FromEntityId == SelectedItem.EntityId &
                    f.ToEntityId == (int)WhellKnownDbEntity.FileData);
            foreach (ChainAdvanced<Agent, FileData> link in
                retColl.Select(item => new ChainAdvanced<Agent, FileData>(SelectedItem) { Right = item, StateId = State.STATEACTIVE, KindId = ckind.Id }))
            {
                link.Save();
                _collectionFiles.Add(link);
                if (!_bindFiles.Contains(link))
                    _bindFiles.Add(link);
                _bindFiles.ResetBindings(false);
            }
        }

        private void BtnFileCreateItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileCreate();
        }

        protected void InvokeFileCreate()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter =
                    "Adobe PDF|*.pdf|Microsoft Excel 2007|*.xlsx|Microsoft Excel|*.xls|Microsoft Word 2007|*.docx|Microsoft Word|*.doc|PNG|*.png|JPG|*.jpg|XPS|*.xps|Все файлы|*.*"
            };
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            foreach (string fileName in dlg.FileNames)
            {
                FileData fileData = new FileData { Workarea = SelectedItem.Workarea, Name = Path.GetFileName(fileName) };
                fileData.KindId = FileData.KINDID_FILEDATA;
                fileData.SetStreamFromFile(fileName);
                fileData.Save();
                ChainKind ckind = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(
                    f =>
                    f.Code == ChainKind.FILE & f.FromEntityId == SelectedItem.EntityId &
                    f.ToEntityId == (int)WhellKnownDbEntity.FileData);

                ChainAdvanced<Agent, FileData> link =
                    new ChainAdvanced<Agent, FileData>(SelectedItem) { Right = fileData, StateId = State.STATEACTIVE, KindId = ckind.Id };

                link.Save();
                _collectionFiles.Add(link);
                if (!_bindFiles.Contains(link))
                    _bindFiles.Add(link);
            }
            _bindFiles.ResetBindings(false);
        }

        private void GridFilesDoubleClick(object sender, EventArgs e)
        {
            Point p = _gridFiles.PointToClient(Control.MousePosition);
            GridHitInfo hit = ViewFiles.CalcHitInfo(p.X, p.Y);
            if (hit.InRow)
            {
                InvokeFilePreview();
            }
        }
        List<FilePreviewThread<Agent>> OpennedDocs;
        protected void InvokeFilePreview()
        {
            try
            {

                if (_bindFiles.Current == null) return;
                if (OpennedDocs == null)
                    OpennedDocs = new List<FilePreviewThread<Agent>>();
                for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                    if (OpennedDocs[i].IsExit)
                        OpennedDocs.Remove(OpennedDocs[i]);
                FilePreviewThread<Agent> dpt = new FilePreviewThread<Agent>(_bindFiles.Current as ChainAdvanced<Agent, FileData>) { OpennedDocs = OpennedDocs }; //{ DocView = this };
                OpennedDocs.Add(dpt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridLookUpEdit cmb = sender as GridLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbRegisteredBy" && _bindRegisteredBy.Count < 2)
                {
                    _collRegisteredBy = SelectedItem.Workarea.GetCollection<Agent>().Where(s => s.KindValue == 1).ToList();
                    _bindRegisteredBy.DataSource = _collRegisteredBy;
                }
                else if (cmb.Name == "cmbSalesRepresentative" && _bindSalesRepresentative.Count < 2)
                {
                    ChainKind chainKind = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS);
                    if (chainKind != null)
                        _collSalesRepresentative = ((IChains<Agent>)SelectedItem).SourceList(chainKind.Id); //Agent.GetChainSourceList(SelectedItem.Workarea, SelectedItem.Id, chainKind.Id);
                    _bindSalesRepresentative.DataSource = _collSalesRepresentative;
                }
                else if (cmb.Name == "cmbIndustry" && _bindIndustry.Count < 2)
                {
                    Hierarchy rootIndustry = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_AGENTINDUSTRY);
                    if (rootIndustry != null)
                        _collIndustry = rootIndustry.GetTypeContents<Analitic>();
                    _bindIndustry.DataSource = _collIndustry;
                }
                else if (cmb.Name == "cmbTypeOutlet" && _bindTypeOutlet.Count < 2)
                {
                    Hierarchy rootTypeOutlet = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_AGENTTYPEOUTLET);
                    if (rootTypeOutlet != null)
                        _collTypeOutlet = rootTypeOutlet.GetTypeContents<Analitic>();
                    _bindTypeOutlet.DataSource = _collTypeOutlet;
                }
                else if (cmb.Name == "cmbMetricArea" && _bindMetricArea.Count < 2)
                {
                    Hierarchy rootMetricArea = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_AGENTMETRICAREA);
                    if (rootMetricArea != null)
                        _collMetricArea = rootMetricArea.GetTypeContents<Analitic>();
                    _bindMetricArea.DataSource = _collMetricArea;
                }
                else if (cmb.Name == "cmbCategory" && _bindCategory.Count < 2)
                {
                    Hierarchy rootCategory = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_AGENTCATEGORY);
                    if (rootCategory != null)
                        _collCategory = rootCategory.GetTypeContents<Analitic>();
                    _bindCategory.DataSource = _collCategory;
                }
                else if (cmb.Name == "cmbOwnership" && _bindOwnership.Count < 2)
                {
                    Hierarchy rootOwnership = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_AGENTOWNERSHIP);
                    if (rootOwnership != null)
                        _collOwnership = rootOwnership.GetTypeContents<Analitic>();
                    _bindOwnership.DataSource = _collOwnership;
                }
                else if (cmb.Name == "cmbTaxInspection" && _bindTaxInspection.Count < 2)
                {
                    _collTaxInspection = SelectedItem.Workarea.GetCollection<Agent>().Where(s => s.KindValue == 1).ToList();
                    _bindTaxInspection.DataSource = _collTaxInspection;
                }
                else if ((cmb.Name == "cmbDirector" || cmb.Name == "cmbBuh" || cmb.Name == "cmbCashier" || cmb.Name == "cmbPersonnel") && _bindWorkers.Count < 5)
                {
                    ChainKind chainKind = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS);
                    if (chainKind != null)
                        _collWorkers = ((IChains<Agent>)SelectedItem).SourceList(chainKind.Id);//Agent.GetChainSourceList(SelectedItem.Workarea, SelectedItem.Id, chainKind.Id);
                    _bindWorkers.DataSource = _collWorkers;
                }
                else if (cmb.Name == "cmbStorekeeper" && _bindStoreKeep.Count<2)
                {
                    ChainKind chainKind = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS);
                    if (chainKind != null)
                        _collStoreKeep = ((IChains<Agent>)SelectedItem).SourceList(chainKind.Id);//Agent.GetChainSourceList(SelectedItem.Workarea, SelectedItem.Id, chainKind.Id);
                    _bindStoreKeep.DataSource = _collStoreKeep;
                }
                else if (cmb.Name == "cmbPlaceEmploymentBook")
                {
                    Hierarchy rootCategory = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_EMPLOYMENTBOOK);
                    if (rootCategory != null)
                        _collPlaceEmploymentBook = rootCategory.GetTypeContents<Analitic>();
                    _bindPlaceEmploymentBook.DataSource = _collPlaceEmploymentBook;
                }
                else if (cmb.Name == "cmbMinors")
                {
                    Hierarchy rootCategory = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_MINORS);
                    if (rootCategory != null)
                        _collMinors = rootCategory.GetTypeContents<Analitic>();
                    _bindMinors.DataSource = _collMinors;
                }
                else if (cmb.Name == "cmbLastPlaceWork")
                {
                    Hierarchy rootCategory = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.GetSystemFavoriteCodeValue(WhellKnownDbEntity.Agent));
                    if (rootCategory != null)
                        _collLastPlaceWork = rootCategory.GetTypeContents<Agent>();
                    _bindLastPlaceWork.DataSource = _collLastPlaceWork;
                }
                else if (cmb.Name == "cmbEmloyerCategory")
                {
                    Hierarchy rootCategory = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SYSTEM_ANALITIC_WORKERCATEGORY");
                    if (rootCategory != null)
                        _collEmployerCategory = rootCategory.GetTypeContents<Analitic>();
                    _bindEmployerCategory.DataSource = _collEmployerCategory;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
        void ViewRegisteredByCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, _bindRegisteredBy);
            //if (e.Column.FieldName == "Image" && e.IsGetData && bindRegisteredBy.Count > 0)
            //{
            //    Agent imageItem = bindRegisteredBy[e.ListSourceRowIndex] as Agent;
            //    if (imageItem != null)
            //    {
            //        e.Value = imageItem.GetImage();
            //    }
            //}
            //else if (e.Column.Name == "colStateImage" && e.IsGetData && bindRegisteredBy.Count > 0)
            //{
            //    Agent imageItem = bindRegisteredBy[e.ListSourceRowIndex] as Agent;
            //    if (imageItem != null)
            //    {
            //        e.Value = imageItem.State.GetImage();
            //    }
            //}
        }
        void CmbRegisteredByButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
      
            if (!_bindRegisteredBy.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindRegisteredBy.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbRegisteredBy.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
        void ViewStoreKeepCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindStoreKeep.Count > 0)
            {
                Agent imageItem = _bindStoreKeep[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindStoreKeep.Count > 0)
            {
                Agent imageItem = _bindStoreKeep[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        void CmbStoreButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindStoreKeep.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindStoreKeep.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbStorekeeper.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
        void ViewSalesRepresentativeCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindSalesRepresentative.Count > 0)
            {
                Agent imageItem = _bindSalesRepresentative[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindSalesRepresentative.Count > 0)
            {
                Agent imageItem = _bindSalesRepresentative[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        void CmbSalesRepresentativeButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindSalesRepresentative.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindSalesRepresentative.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbSalesRepresentative.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
        void CmbActivityBrancheButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Analitic> browseDialog = new TreeListBrowser<Analitic> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindOwnership.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindOwnership.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbOwnership.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }

        void CmbIndustryButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Analitic> browseDialog = new TreeListBrowser<Analitic> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindIndustry.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindIndustry.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbIndustry.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
        void CmbTypeOutletButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Analitic> browseDialog = new TreeListBrowser<Analitic> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if (browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) return;
            if (!_bindTypeOutlet.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindTypeOutlet.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbTypeOutlet.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
        void CmbMetricAreaButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Analitic> browseDialog = new TreeListBrowser<Analitic> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindMetricArea.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindMetricArea.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbMetricArea.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
        void CmbCategoryButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Analitic> browseDialog = new TreeListBrowser<Analitic> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindCategory.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindCategory.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbCategory.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }


        void CmbLastPlaceWorkButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea, RootCode = Hierarchy.GetSystemFavoriteCodeValue(WhellKnownDbEntity.Agent) }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindLastPlaceWork.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindLastPlaceWork.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbCategory.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }

        ControlList _controlAccountValues;
        /// <summary>
        /// Закладка банковских счетов
        /// </summary>
        private void BuildPageBankAccount()
        {
            if (_controlAccountValues == null)
            {
                // TODO: Переделать
                _controlAccountValues = new ControlList {Name = ExtentionString.CONTROL_BANKACC_NAME};
                // Данные для отображения в списке связей
                BindingSource valuesCollectinBind = new BindingSource {DataSource = SelectedItem.BankAccounts};
                _controlAccountValues.Grid.DataSource = valuesCollectinBind;

                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_BANKACC_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                BarButtonItem btnChainCreate = new BarButtonItem
                                                   {
                                                       ButtonStyle = BarButtonStyle.DropDown,
                                                       ActAsDropDown = true,
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainCreate);
                #region Новая запись
                List<AgentBankAccount> collectionTemplates = SelectedItem.Workarea.GetTemplates<AgentBankAccount>();
                PopupMenu mnuTemplates = new PopupMenu {Ribbon = frmProp.ribbon};

                foreach (AgentBankAccount itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem {Caption = itemTml.Name};
                    mnuTemplates.AddItem(btn);
                    btn.Tag = itemTml;
                    btn.Glyph = itemTml.GetImage();
                    btn.ItemClick += delegate
                    {
                        AgentBankAccount objectTml = (AgentBankAccount)btn.Tag;
                        AgentBankAccount newObject = objectTml.Workarea.CreateNewObject(objectTml);
                        newObject.Agent = SelectedItem;
                        newObject.CurrencyId = objectTml.CurrencyId;
                        newObject.BankId = objectTml.BankId;
                        Form frmProperties = newObject.ShowProperty();
                        frmProperties.FormClosed += delegate
                        {
                            if (!newObject.IsNew)
                            {
                                int position = valuesCollectinBind.Add(newObject);
                                valuesCollectinBind.Position = position;
                            }
                        };
                    };
                }
                btnChainCreate.DropDownControl = mnuTemplates;
                #endregion

                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_PROP, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                groupLinksAction.ItemLinks.Add(btnProp);
                #region Свойства
                btnProp.ItemClick += delegate
                {
                    AgentBankAccount currentObject = (AgentBankAccount)valuesCollectinBind.Current;
                    if (currentObject != null)
                    {
                        currentObject.ShowProperty();
                    }
                };
                _controlAccountValues.Grid.DoubleClick += delegate
                {
                    System.Drawing.Point p = _controlAccountValues.Grid.PointToClient(Control.MousePosition);
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = _controlAccountValues.View.CalcHitInfo(p.X, p.Y);
                    if (hit.InRow)
                    {
                        AgentBankAccount currentObject = (AgentBankAccount)valuesCollectinBind.Current;
                        if (currentObject != null)
                        {
                            currentObject.ShowProperty();
                        }
                    }
                };
                #endregion

                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph =
                                                      ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                              ResourceImage.DELETE_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnDelete);
                #region Удаление
                btnDelete.ItemClick += delegate
                {
                    AgentBankAccount currentObject = (AgentBankAccount)valuesCollectinBind.Current;
                    if (currentObject != null)
                    {
                        int res = Extentions.ShowMessageChoice(SelectedItem.Workarea, 
                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                            "Удаление расчетного счета",
                                 string.Empty,
                                 Properties.Resources.STR_CHOICE_DEL);

                        if (res == 0)
                        {
                            try
                            {
                                currentObject.Remove();
                                valuesCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления расчетного счета!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (res == 1)
                        {
                            try
                            {
                                currentObject.Delete();
                                valuesCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления расчетного счета!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message, 
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion

                page.Groups.Add(groupLinksAction);
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlAccountValues.View, "DEFAULT_LISTVIEWAGENTBANKACCOUNT");
                _controlAccountValues.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        Rectangle r = e.Bounds;
                        Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.MONEY_X16);
                        e.Graphics.DrawImageUnscaled(img, r);
                        e.Handled = true;
                    }
                    if (e.Column.Name == "colStateImage")
                    {
                        Rectangle r = e.Bounds;
                        int index = _controlAccountValues.View.GetDataSourceRowIndex(e.RowHandle);
                        AgentBankAccount v = (AgentBankAccount)valuesCollectinBind[index];
                        Image img = v.State.GetImage();
                        e.Graphics.DrawImageUnscaled(img, r);
                        e.Handled = true;
                    }
                };
                Control.Controls.Add(_controlAccountValues);
                _controlAccountValues.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlAccountValues.Grid;
            HidePageControls(ExtentionString.CONTROL_BANKACC_NAME);
        }
        // TODO:
        private void BuildPageCompany()
        {

        }

        private ControlAgentBank _controlAgentBank;
        private void BuildPageBank()
        {
            if (_controlAgentBank == null)
            {
                _controlAgentBank = new ControlAgentBank
                                        {
                                            Name = ExtentionString.CONTROL_AGENTBANK,
                                            txtCorrBankAccount = {Text = _company.Bank.CorrBankAccount},
                                            txtLicenseNo = {Text = _company.Bank.LicenseNo},
                                            txtSertificateNo = {Text = _company.Bank.SertificateNo},
                                            txtSwift = {Text = _company.Bank.Swift}
                                        };
                if(_company.Bank.LicenseDate.HasValue)
                    _controlAgentBank.dtLicenseDate.DateTime = _company.Bank.LicenseDate.Value;
                if (_company.Bank.SertificateDate.HasValue)
                    _controlAgentBank.dtSertificateDate.DateTime = _company.Bank.SertificateDate.Value;

                Control.Controls.Add(_controlAgentBank);
                _controlAgentBank.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlAgentBank;
            HidePageControls(ExtentionString.CONTROL_AGENTBANK);

        }
        ControlList _controlContactValues;
        private void BuildPageContacts()
        {
            if (_controlContactValues == null)
            {
                RelationHelper<Agent, Contact> relation = new RelationHelper<Agent, Contact>();
                List<Contact> list = relation.GetListObject(SelectedItem);
                ListBrowserBaseObjects<Contact> browserBaseObjects = new ListBrowserBaseObjects<Contact>(SelectedItem.Workarea, list, null, null, true, false, false, true);
                browserBaseObjects.Build();
                browserBaseObjects.ListControl.Name = ExtentionString.CONTROL_CONTACT_NAME;
                _controlContactValues = browserBaseObjects.ListControl;

                browserBaseObjects.CreateNew += delegate(Contact obj, Contact objTemplate)
                {
                    obj.OwnId = SelectedItem.Id;
                };
                browserBaseObjects.ShowProperty += delegate(Contact obj)
                {
                    Form frmProperties = obj.ShowProperty();
                    frmProperties.FormClosed += delegate
                    {
                        if (!obj.IsNew)
                        {
                            if(!list.Contains(obj))
                            {
                                int position = browserBaseObjects.BindingSource.Add(obj);
                                browserBaseObjects.BindingSource.Position = position;
                            }
                        }
                    };
                };
                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_CONTACT_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                BarButtonItem btnChainCreate = new BarButtonItem
                                                   {
                                                       ButtonStyle = BarButtonStyle.DropDown,
                                                       ActAsDropDown = true,
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainCreate);

                browserBaseObjects.ListControl.CreateMenu.Ribbon = frmProp.ribbon;
                btnChainCreate.DropDownControl = browserBaseObjects.ListControl.CreateMenu;


                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_PROP, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph =  ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                groupLinksAction.ItemLinks.Add(btnProp);
                btnProp.ItemClick += delegate
                                         {
                                             browserBaseObjects.InvokeProperties();
                                         };

                BarButtonItem btnNavigate = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.STR_DOC_ACTIONS, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea,ResourceImage.TRIANGLEGREEN_X32)
                };
                groupLinksAction.ItemLinks.Add(btnNavigate);
                btnNavigate.ItemClick += delegate
                {

                    Contact currentObject = browserBaseObjects.BindingSource.Current as Contact;
                    if (currentObject == null)
                    {
                        System.Data.DataRowView rv = browserBaseObjects.BindingSource.Current as System.Data.DataRowView;
                        if (rv != null)
                        {
                            int id = (int)rv[GlobalPropertyNames.Id];
                            currentObject = SelectedItem.Workarea.GetObject<Contact>(id);
                        }
                    }
                    if(currentObject!=null)
                    {
                        //WWW	2031619
                        if(currentObject.KindId==2031619 && !string.IsNullOrEmpty(currentObject.Name))
                        {
                            System.Diagnostics.Process.Start(currentObject.Name);
                        }
                        //Email	2031620
                        else if (currentObject.KindId == 2031620 && !string.IsNullOrEmpty(currentObject.Name))
                        {
                            System.Diagnostics.Process.Start(string.Format("mailto:{0}", currentObject.Name));
                        }
                    }
                };
                

                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph =
                                                      ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                              ResourceImage.DELETE_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnDelete);
                btnDelete.ItemClick += delegate
                {
                    browserBaseObjects.InvokeDelete();
                };
                page.Groups.Add(groupLinksAction);

                Control.Controls.Add(_controlContactValues);
                _controlContactValues.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlContactValues.Grid;
            HidePageControls(ExtentionString.CONTROL_CONTACT_NAME);
        }

        ControlList _controlAgentAddress;
        private void BuildPageAddress()
        {
            if (_controlAgentAddress == null)
            {
                _controlAgentAddress = new ControlList { Name = ExtentionString.CONTROL_ADDRESSINFO };

                // Данные для отображения в списке паспортов
                BindingSource valuesCollectinBind = new BindingSource { DataSource = SelectedItem.AddressCollection };
                _controlAgentAddress.Grid.DataSource = valuesCollectinBind;

                // Построение группы упраления паспортами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_ADDRESSINFO)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                BarButtonItem btnChainCreate = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                };
                groupLinksAction.ItemLinks.Add(btnChainCreate);
                #region Новая запись
                btnChainCreate.ItemClick += delegate
                {
                    AgentAddress newObject = new AgentAddress
                    {
                        Workarea = SelectedItem.Workarea,
                        OwnId = _people.Id
                    };
                    Form frmProperties = newObject.ShowProperty();
                    frmProperties.FormClosed += delegate
                    {
                        if (!newObject.IsNew)
                        {
                            int position = valuesCollectinBind.Add(newObject);
                            valuesCollectinBind.Position = position;
                        }
                    };
                };
                #endregion

                BarButtonItem btnProp = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_PROP, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                };
                groupLinksAction.ItemLinks.Add(btnProp);
                #region Свойства
                btnProp.ItemClick += delegate
                {
                    AgentAddress currentObject = (AgentAddress)valuesCollectinBind.Current;
                    if (currentObject != null)
                    {
                        currentObject.ShowProperty();
                    }
                };
                _controlAgentAddress.Grid.DoubleClick += delegate
                {
                    Point p = _controlAgentAddress.Grid.PointToClient(Control.MousePosition);
                    GridHitInfo hit = _controlAgentAddress.View.CalcHitInfo(p.X, p.Y);
                    if (hit.InRow)
                    {
                        AgentAddress currentObject = (AgentAddress)valuesCollectinBind.Current;
                        if (currentObject != null)
                        {
                            currentObject.ShowProperty();
                        }
                    }
                };
                #endregion

                BarButtonItem btnDelete = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph =
                        ResourceImage.GetByCode(SelectedItem.Workarea,
                                                ResourceImage.DELETE_X32)
                };
                groupLinksAction.ItemLinks.Add(btnDelete);
                #region Удаление
                btnDelete.ItemClick += delegate
                {
                    AgentAddress currentObject = (AgentAddress)valuesCollectinBind.Current;
                    if (currentObject != null)
                    {
                        int res = Extentions.ShowMessageChoice(SelectedItem.Workarea,
                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                            "Удаление адреса",
                                 string.Empty,
                                 Properties.Resources.STR_CHOICE_DEL);

                        if (res == 0)
                        {
                            try
                            {
                                currentObject.Remove();
                                valuesCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления адреса!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (res == 1)
                        {
                            try
                            {
                                currentObject.Delete();
                                valuesCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления адреса!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion

                page.Groups.Add(groupLinksAction);

                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlAgentAddress.View, "DEFAULT_LISTVIEWAGENTADDRESS");
                _controlAgentAddress.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        Rectangle r = e.Bounds;
                        Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.MONEY_X16);
                        e.Graphics.DrawImageUnscaled(img, r);
                        e.Handled = true;
                    }
                    if (e.Column.Name == "colStateImage")
                    {
                        Rectangle r = e.Bounds;
                        int index = _controlAgentAddress.View.GetDataSourceRowIndex(e.RowHandle);
                        AgentAddress v = (AgentAddress)valuesCollectinBind[index];
                        Image img = v.State.GetImage();
                        e.Graphics.DrawImageUnscaled(img, r);
                        e.Handled = true;
                    }
                };

                Control.Controls.Add(_controlAgentAddress);
                _controlAgentAddress.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlAgentAddress.Grid;
            HidePageControls(ExtentionString.CONTROL_ADDRESSINFO);
        }

        ControlAgentCodes _controlCodes;
        private BindingSource _bindWorkers;
        private List<Agent> _collWorkers;
        private BindingSource _bindTaxInspection;
        private List<Agent> _collTaxInspection;
        /// <summary>
        /// Закладка с регистрационными кодами предприятия
        /// </summary>
        private void BuildPageAgentCodes() 
        {
            if (_controlCodes == null)
            {
                _controlCodes = new ControlAgentCodes
                                    {
                                        Name = ExtentionString.CONTROL_AGENT_CODES,
                                        txtRegPensionFund = {Text = _company.RegPensionFund},
                                        txtRegEmploymentService = {Text = _company.RegEmploymentService},
                                        txtRegSocialInsuranceDisability = {Text = _company.RegSocialInsuranceDisability},
                                        txtRegSocialInsuranceNesch = {Text = _company.RegSocialInsuranceNesch},
                                        txtRegPfu = {Text = _company.RegPfu},
                                        txtRegOpfg = {Text = _company.RegOpfg},
                                        txtRegKoatu = {Text = _company.RegKoatu},
                                        txtRegKfv = {Text = _company.RegKfv},
                                        txtRegZkgng = {Text = _company.RegZkgng},
                                        txtRegKved = {Text = _company.RegKved}
                                    };

                // Регистрационные коды

                _bindWorkers = new BindingSource();
                _collWorkers = new List<Agent>();
                #region Данные для списка "Корреспондент - директор"
                _controlCodes.cmbDirector.Properties.DisplayMember = GlobalPropertyNames.Name;
                _controlCodes.cmbDirector.Properties.ValueMember = GlobalPropertyNames.Id;
                if (_company.DirectorId != 0)
                        _collWorkers.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_company.DirectorId));
                _bindWorkers.DataSource = _collWorkers;
                _controlCodes.cmbDirector.Properties.DataSource = _bindWorkers;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlCodes.ViewDirector, "DEFAULT_LOOKUPAGENT");
                _controlCodes.cmbDirector.Properties.View.BestFitColumns();
                _controlCodes.cmbDirector.EditValue = _company.DirectorId;
                _controlCodes.ViewDirector.CustomUnboundColumnData += ViewWorkersCustomUnboundColumnData;
                _controlCodes.cmbDirector.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _controlCodes.cmbDirector.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 0) return;
                    TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea }.ShowDialog();
                    if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
                    
                    if (!_bindWorkers.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                        _bindWorkers.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
                    _controlCodes.cmbDirector.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
                };
                _controlCodes.cmbDirector.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _controlCodes.cmbDirector.EditValue = 0;
                };
                #endregion
                #region Данные для списка "Корреспондент - бухгалтер"
                _controlCodes.cmbBuh.Properties.DisplayMember = GlobalPropertyNames.Name;
                _controlCodes.cmbBuh.Properties.ValueMember = GlobalPropertyNames.Id;
                if (_company.BuhId != 0)
                {
                    if (!_collWorkers.Contains(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_company.BuhId)))
                        _collWorkers.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_company.BuhId));
                }
                _bindWorkers.DataSource = _collWorkers;
                _controlCodes.cmbBuh.BindingContext = new BindingContext();
                _controlCodes.cmbBuh.Properties.DataSource = _bindWorkers;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlCodes.ViewBuh, "DEFAULT_LOOKUPAGENT");
                _controlCodes.cmbBuh.Properties.View.BestFitColumns();
                _controlCodes.cmbBuh.EditValue = _company.BuhId;
                _controlCodes.ViewBuh.CustomUnboundColumnData += ViewWorkersCustomUnboundColumnData;
                _controlCodes.cmbBuh.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _controlCodes.cmbBuh.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 0) return;
                    TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea }.ShowDialog();
                    if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
                    if (!_bindWorkers.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                        _bindWorkers.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
                    _controlCodes.cmbBuh.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
                };
                _controlCodes.cmbBuh.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _controlCodes.cmbBuh.EditValue = 0;
                };
                #endregion
                #region Данные для списка "Корреспондент - кассир"
                _controlCodes.cmbCashier.Properties.DisplayMember = GlobalPropertyNames.Name;
                _controlCodes.cmbCashier.Properties.ValueMember = GlobalPropertyNames.Id;
                if (_company.CashierId != 0)
                {
                    if (!_collWorkers.Contains(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_company.CashierId)))
                        _collWorkers.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_company.CashierId));
                }
                _bindWorkers.DataSource = _collWorkers;
                _controlCodes.cmbCashier.BindingContext = new BindingContext();
                _controlCodes.cmbCashier.Properties.DataSource = _bindWorkers;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlCodes.ViewCashier, "DEFAULT_LOOKUPAGENT");
                _controlCodes.cmbCashier.Properties.View.BestFitColumns();
                _controlCodes.cmbCashier.EditValue = _company.CashierId;
                _controlCodes.ViewCashier.CustomUnboundColumnData += ViewWorkersCustomUnboundColumnData;
                _controlCodes.cmbCashier.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _controlCodes.cmbCashier.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 0) return;
                    TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea }.ShowDialog();
                    if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
                    if (!_bindWorkers.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                        _bindWorkers.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
                    _controlCodes.cmbCashier.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
                };
                _controlCodes.cmbCashier.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _controlCodes.cmbCashier.EditValue = 0;
                };
                #endregion
                #region Данные для списка "Корреспондент - начальник по персоналу"
                _controlCodes.cmbPersonnel.Properties.DisplayMember = GlobalPropertyNames.Name;
                _controlCodes.cmbPersonnel.Properties.ValueMember = GlobalPropertyNames.Id;
                if (_company.PersonnelId != 0)
                {
                    if (!_collWorkers.Contains(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_company.PersonnelId)))
                        _collWorkers.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_company.PersonnelId));
                }
                _bindWorkers.DataSource = _collWorkers;
                _controlCodes.cmbPersonnel.BindingContext = new BindingContext();
                _controlCodes.cmbPersonnel.Properties.DataSource = _bindWorkers;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlCodes.ViewPersonnel, "DEFAULT_LOOKUPAGENT");
                _controlCodes.cmbPersonnel.Properties.View.BestFitColumns();
                _controlCodes.cmbPersonnel.EditValue = _company.PersonnelId;
                _controlCodes.ViewPersonnel.CustomUnboundColumnData += ViewWorkersCustomUnboundColumnData;
                _controlCodes.cmbPersonnel.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _controlCodes.cmbPersonnel.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 0) return;
                    TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea }.ShowDialog();
                    if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
                    if (!_bindWorkers.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                        _bindWorkers.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
                    _controlCodes.cmbPersonnel.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
                };
                _controlCodes.cmbPersonnel.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _controlCodes.cmbPersonnel.EditValue = 0;
                };
                #endregion
                #region Данные для списка "Корреспондент - налоговая инспекция"
                _controlCodes.cmbTaxInspection.Properties.DisplayMember = GlobalPropertyNames.Name;
                _controlCodes.cmbTaxInspection.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindTaxInspection = new BindingSource();
                _collTaxInspection = new List<Agent>();
                if (_company.TaxInspectionId != 0)
                    _collTaxInspection.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(_company.TaxInspectionId));
                _bindTaxInspection.DataSource = _collTaxInspection;
                _controlCodes.cmbTaxInspection.Properties.DataSource = _bindTaxInspection;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlCodes.ViewTaxInspection, "DEFAULT_LOOKUPAGENT");
                _controlCodes.cmbTaxInspection.Properties.View.BestFitColumns();
                _controlCodes.cmbTaxInspection.EditValue = _company.TaxInspectionId;
                _controlCodes.ViewTaxInspection.CustomUnboundColumnData += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "Image" && e.IsGetData && _bindTaxInspection.Count > 0)
                    {
                        Agent imageItem = _bindTaxInspection[e.ListSourceRowIndex] as Agent;
                        if (imageItem != null)
                        {
                            e.Value = imageItem.GetImage();
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindTaxInspection.Count > 0)
                    {
                        Agent imageItem = _bindTaxInspection[e.ListSourceRowIndex] as Agent;
                        if (imageItem != null)
                        {
                            e.Value = imageItem.State.GetImage();
                        }
                    }
                };
                _controlCodes.cmbTaxInspection.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _controlCodes.cmbTaxInspection.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 0) return;
                    TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea }.ShowDialog();
                    if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
                    if (!_bindTaxInspection.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                        _bindTaxInspection.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
                    _controlCodes.cmbTaxInspection.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
                };
                _controlCodes.cmbTaxInspection.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _controlCodes.cmbTaxInspection.EditValue = 0;
                };
                #endregion

                #region Лого
                _controlCodes.imageEditLogoId.Properties.QueryPopUp += delegate
                                                              {
                                                                  if (_controlCodes.imageEditLogoId.Properties.PopupFormSize.Width != _controlCodes.imageEditLogoId.Width)
                                                                      _controlCodes.imageEditLogoId.Properties.PopupFormSize = new Size(_controlCodes.imageEditLogoId.Width, 150);

                                                                  if((_company.LogoId!=0)&&(_controlCodes.imageEditLogoId.Image==null))
                                                                  {
                                                                      FileData file = new FileData() { Workarea = SelectedItem.Workarea };
                                                                      file.Load(_company.LogoId);
                                                                      _controlCodes.imageEditLogoId.Image = Image.FromStream(new MemoryStream(file.StreamData));
                                                                  }
                                                              };
                _controlCodes.imageEditLogoId.Properties.Popup += delegate(object sender, EventArgs e)
                                                         {
                                                             ImagePopupForm f = (_controlCodes.imageEditLogoId as DevExpress.Utils.Win.IPopupControl).PopupWindow as ImagePopupForm;
                                                             PropertyInfo pi = typeof (ImagePopupForm).GetProperty("Picture", BindingFlags.NonPublic | BindingFlags.Instance);
                                                             PictureEdit pe = pi.GetValue(f, null) as PictureEdit;
                                                             pe.MouseClick -= new MouseEventHandler(PictureEditLogo_MouseClick);
                                                             pe.MouseClick += new MouseEventHandler(PictureEditLogo_MouseClick);
                                                         };

                _controlCodes.imageEditLogoId.Properties.ShowMenu = false;
                #endregion

                #region Печать
                _controlCodes.imageEditStampId.Properties.QueryPopUp += delegate
                                                        {
                                                            if (_controlCodes.imageEditStampId.Properties.PopupFormSize.Width != _controlCodes.imageEditStampId.Width)
                                                                _controlCodes.imageEditStampId.Properties.PopupFormSize = new Size(_controlCodes.imageEditStampId.Width, 150);

                                                            if ((_company.LogostampId != 0) && (_controlCodes.imageEditStampId.Image == null))
                                                            {
                                                                FileData file = new FileData() { Workarea = SelectedItem.Workarea };
                                                                file.Load(_company.LogostampId);
                                                                _controlCodes.imageEditStampId.Image = Image.FromStream(new MemoryStream(file.StreamData));
                                                            }
                                                        };
                _controlCodes.imageEditStampId.Properties.Popup += delegate(object sender, EventArgs e)
                                                        {
                                                            ImagePopupForm f = (_controlCodes.imageEditStampId as DevExpress.Utils.Win.IPopupControl).PopupWindow as ImagePopupForm;
                                                            PropertyInfo pi = typeof(ImagePopupForm).GetProperty("Picture", BindingFlags.NonPublic | BindingFlags.Instance);
                                                            PictureEdit pe = pi.GetValue(f, null) as PictureEdit;
                                                            pe.MouseClick -= new MouseEventHandler(PictureEditStamp_MouseClick);
                                                            pe.MouseClick += new MouseEventHandler(PictureEditStamp_MouseClick);
                                                        };

                _controlCodes.imageEditStampId.Properties.ShowMenu = false;
                #endregion

                #region Подпись
                _controlCodes.imageEditSignId.Properties.QueryPopUp += delegate
                                                        {
                                                            if (_controlCodes.imageEditSignId.Properties.PopupFormSize.Width != _controlCodes.imageEditSignId.Width)
                                                                _controlCodes.imageEditSignId.Properties.PopupFormSize = new Size(_controlCodes.imageEditSignId.Width, 150);

                                                            if ((_company.LogoSignId != 0) && (_controlCodes.imageEditSignId.Image == null))
                                                            {
                                                                FileData file = new FileData() { Workarea = SelectedItem.Workarea };
                                                                file.Load(_company.LogoSignId);
                                                                _controlCodes.imageEditSignId.Image = Image.FromStream(new MemoryStream(file.StreamData));
                                                            }
                                                        };
                _controlCodes.imageEditSignId.Properties.Popup += delegate(object sender, EventArgs e)
                                                        {
                                                            ImagePopupForm f = (_controlCodes.imageEditSignId as DevExpress.Utils.Win.IPopupControl).PopupWindow as ImagePopupForm;
                                                            PropertyInfo pi = typeof(ImagePopupForm).GetProperty("Picture", BindingFlags.NonPublic | BindingFlags.Instance);
                                                            PictureEdit pe = pi.GetValue(f, null) as PictureEdit;
                                                            pe.MouseClick -= new MouseEventHandler(PictureEditSign_MouseClick);
                                                            pe.MouseClick += new MouseEventHandler(PictureEditSign_MouseClick);
                                                        };

                _controlCodes.imageEditSignId.Properties.ShowMenu = false;
                #endregion

                Control.Controls.Add(_controlCodes);
                _controlCodes.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlCodes.LayoutControl;
            HidePageControls(ExtentionString.CONTROL_AGENT_CODES);
        }

        private void PictureEditLogo_MouseClick(object sender, MouseEventArgs e)
        {
            RegisterPictureEditEvents(_controlCodes.imageEditLogoId, sender, e);
        }

        private void PictureEditStamp_MouseClick(object sender, MouseEventArgs e)
        {
            RegisterPictureEditEvents(_controlCodes.imageEditStampId, sender, e);
        }

        private void PictureEditSign_MouseClick(object sender, MouseEventArgs e)
        {
            RegisterPictureEditEvents(_controlCodes.imageEditSignId, sender, e);
        }

        private int GetLogoIdByImageControl(ImageEdit imageEdit)
        {
            switch(imageEdit.Name)
            {
                case "imageEditLogoId":
                    return _company.LogoId;
                case "imageEditStampId":
                    return _company.LogostampId;
                case "imageEditSignId":
                    return _company.LogoSignId;
                default:
                    throw new Exception("Неизвестное имя контрола");
            }
        }

        private void SetLogoIdByImageControl(ImageEdit imageEdit, int value)
        {
            switch (imageEdit.Name)
            {
                case "imageEditLogoId":
                    _company.LogoId=value;
                    break;
                case "imageEditStampId":
                    _company.LogostampId=value;
                    break;
                case "imageEditSignId":
                    _company.LogoSignId=value;
                    break;
                default:
                    throw new Exception("Неизвестное имя контрола");
            }
        }

        private void RegisterPictureEditEvents(ImageEdit imageEdit, object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (imageEditPopupMenu == null)
                {
                    imageEditPopupMenu = new PopupMenu { Ribbon = frmProp.Ribbon };

                    BarButtonItem mnuAdd = null;
                    BarButtonItem mnuOpen = null;
                    BarButtonItem mnuSave = null;
                    BarButtonItem mnuDelete = null;

                    #region Выбрать
                    mnuAdd = new BarButtonItem { Caption = "Выбрать", Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.ADD_X16) };
                    mnuAdd.ItemClick += delegate
                    {
                        TreeListBrowser<FileData> browseDialog = new TreeListBrowser<FileData> { Workarea = SelectedItem.Workarea, RootCode = Hierarchy.SYSTEM_FILEDATA_LOGOAGENT }.ShowDialog();
                        if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
                        SetLogoIdByImageControl(imageEdit, browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id);

                        FileData file = new FileData { Workarea = SelectedItem.Workarea };
                        file.Load(GetLogoIdByImageControl(imageEdit));
                        imageEdit.Image = Image.FromStream(new MemoryStream(file.StreamData));
                        mnuSave.Enabled = true;
                        mnuDelete.Enabled = true;
                    };
                    imageEditPopupMenu.AddItem(mnuAdd);
                    #endregion

                    #region Открыть
                    mnuOpen = new BarButtonItem { Caption = "Открыть", Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.OPEN_X16) };
                    mnuOpen.ItemClick += delegate
                    {
                        OpenFileDialog dialog = new OpenFileDialog { Filter = "Графические файлы|*.bmp;*.jpg;*.jpeg;*.png|Все файлы|*.*" };
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            imageEdit.Image = Image.FromFile(dialog.FileName);

                            FileData fileData = new FileData
                            {
                                Workarea = SelectedItem.Workarea,
                                Name = Path.GetFileNameWithoutExtension(dialog.FileName),
                                FileExtention = Path.GetExtension(dialog.FileName).Substring(1),
                                StreamData = File.ReadAllBytes(dialog.FileName),
                                KindId = FileData.KINDID_FILEDATA,
                                StateId = State.STATEACTIVE
                            };

                            fileData.Save();
                            SetLogoIdByImageControl(imageEdit, fileData.Id);

                            Hierarchy emblemsRoot = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_FILEDATA_LOGOAGENT);
                            if (emblemsRoot == null)
                                throw new Exception("Не найдена иерархия для хранения файлов гербов стран");
                            emblemsRoot.ContentAdd(fileData, true);

                            mnuSave.Enabled = true;
                            mnuDelete.Enabled = true;
                        }
                    };
                    imageEditPopupMenu.AddItem(mnuOpen);
                    #endregion

                    #region Сохранить
                    mnuSave = new BarButtonItem { Caption = "Сохранить", Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.SAVE_X16), Enabled = GetLogoIdByImageControl(imageEdit) > 0 };
                    mnuSave.ItemClick += delegate
                    {
                        if (GetLogoIdByImageControl(imageEdit) == 0)
                            return;

                        FileData file = new FileData() { Workarea = SelectedItem.Workarea };
                        file.Load(GetLogoIdByImageControl(imageEdit));

                        SaveFileDialog dialog = new SaveFileDialog()
                        {
                            FileName = file.Name,
                            Filter = string.Format("{0}|*.{0}|Все файлы|*.*", file.FileExtention),
                            AddExtension = true
                        };
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            file.ExportStreamDataToFile(dialog.FileName);
                        }
                    };
                    imageEditPopupMenu.AddItem(mnuSave);
                    #endregion

                    #region Удалить
                    mnuDelete = new BarButtonItem { Caption = "Удалить", Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X16), Enabled = GetLogoIdByImageControl(imageEdit) > 0 };
                    mnuDelete.ItemClick += delegate
                    {
                        //_common.edEmblem.Image = null;
                        ImagePopupForm f = (imageEdit as DevExpress.Utils.Win.IPopupControl).PopupWindow as ImagePopupForm;
                        PropertyInfo pi = typeof(ImagePopupForm).GetProperty("Picture", BindingFlags.NonPublic | BindingFlags.Instance);
                        PictureEdit pe = pi.GetValue(f, null) as PictureEdit;
                        pe.Image = null;
                        SetLogoIdByImageControl(imageEdit, 0);
                        mnuSave.Enabled = false;
                        mnuDelete.Enabled = false;
                    };
                    imageEditPopupMenu.AddItem(mnuDelete);
                    #endregion
                }
                imageEditPopupMenu.ShowPopup(Cursor.Position);
            }
        }

        void ViewWorkersCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, _bindWorkers);
            //if (e.Column.FieldName == "Image" && e.IsGetData && bindWorkers.Count > 0)
            //{
            //    Agent imageItem = bindWorkers[e.ListSourceRowIndex] as Agent;
            //    if (imageItem != null)
            //    {
            //        e.Value = imageItem.GetImage();
            //    }
            //}
            //else if (e.Column.Name == "colStateImage" && e.IsGetData && bindWorkers.Count > 0)
            //{
            //    Agent imageItem = bindWorkers[e.ListSourceRowIndex] as Agent;
            //    if (imageItem != null)
            //    {
            //        e.Value = imageItem.State.GetImage();
            //    }
            //}
        }

        ControlList _controlAgentPassport;
        /// <summary>
        /// Закладка с данными паспортов
        /// </summary>
        private void BuildPageAgentPassport()
        {
            if (_controlAgentPassport == null)
            {
                _controlAgentPassport = new ControlList {Name = ExtentionString.CONTROL_AGENT_PASSPORT};

                // Данные для отображения в списке паспортов
                BindingSource valuesCollectinBind = new BindingSource {DataSource = _people.AgentPassport};
                _controlAgentPassport.Grid.DataSource = valuesCollectinBind;

                // Построение группы упраления паспортами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_AGENT_PASSPORT)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                BarButtonItem btnChainCreate = new BarButtonItem
                                                   {
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainCreate);
                #region Новая запись
                btnChainCreate.ItemClick += delegate
                {
                    Passport newObject = new Passport
                                             { 
                        Workarea = SelectedItem.Workarea, 
                        Birthday = DateTime.Today, 
                        OwnId = _people.Id
                    };
                    Form frmProperties = newObject.ShowProperty();
                    frmProperties.FormClosed += delegate
                    {
                        if (!newObject.IsNew)
                        {
                            int position = valuesCollectinBind.Add(newObject);
                            valuesCollectinBind.Position = position;
                        }
                    };
                };
                #endregion

                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_PROP, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                groupLinksAction.ItemLinks.Add(btnProp);
                #region Свойства
                btnProp.ItemClick += delegate
                {
                    Passport currentObject = (Passport)valuesCollectinBind.Current;
                    if (currentObject != null)
                    {
                        currentObject.ShowProperty();
                    }
                };
                _controlAgentPassport.Grid.DoubleClick += delegate
                {
                    Point p = _controlAgentPassport.Grid.PointToClient(Control.MousePosition);
                    GridHitInfo hit = _controlAgentPassport.View.CalcHitInfo(p.X, p.Y);
                    if (hit.InRow)
                    {
                        Passport currentObject = (Passport)valuesCollectinBind.Current;
                        if (currentObject != null)
                        {
                            currentObject.ShowProperty();
                        }
                    }
                };
                #endregion

                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph =
                                                      ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                              ResourceImage.DELETE_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnDelete);
                #region Удаление
                btnDelete.ItemClick += delegate
                {
                    Passport currentObject = (Passport)valuesCollectinBind.Current;
                    if (currentObject != null)
                    {
                        int res = Extentions.ShowMessageChoice(SelectedItem.Workarea,
                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                            "Удаление паспорта",
                                 string.Empty,
                                 Properties.Resources.STR_CHOICE_DEL);

                        if (res == 0)
                        {
                            try
                            {
                                currentObject.Remove();
                                valuesCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления паспорта!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (res == 1)
                        {
                            try
                            {
                                currentObject.Delete();
                                valuesCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления паспорта!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion

                page.Groups.Add(groupLinksAction);

                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlAgentPassport.View, "DEFAULT_LISTVIEWAGENTPASSPORT");
                _controlAgentPassport.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        Rectangle r = e.Bounds;
                        Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.MONEY_X16);
                        e.Graphics.DrawImageUnscaled(img, r);
                        e.Handled = true;
                    }
                    if (e.Column.Name == "colStateImage")
                    {
                        Rectangle r = e.Bounds;
                        int index = _controlAgentPassport.View.GetDataSourceRowIndex(e.RowHandle);
                        Passport v = (Passport)valuesCollectinBind[index];
                        Image img = v.State.GetImage();
                        e.Graphics.DrawImageUnscaled(img, r);
                        e.Handled = true;
                    }
                };

                Control.Controls.Add(_controlAgentPassport);
                _controlAgentPassport.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlAgentPassport.Grid;
            HidePageControls(ExtentionString.CONTROL_AGENT_PASSPORT);
        }

        ControlList _controlAgentDrivingLicence;
        /// <summary>
        /// Закладка с данными водительских удостоверений
        /// </summary>
        private void BuildPageAgentDrivingLicence()
        {
            if (_controlAgentDrivingLicence == null)
            {
                _controlAgentDrivingLicence = new ControlList
                                                  {
                                                      Name = ExtentionString.CONTROL_AGENT_DRIVINGLICENCE
                                                  };

                // Данные для отображения в списке водительских удостоверений
                BindingSource valuesCollectinBind = new BindingSource {DataSource = _people.DrivingLicence};
                _controlAgentDrivingLicence.Grid.DataSource = valuesCollectinBind;

                // Построение группы упраления водительскими удостоверениями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_AGENT_DRIVINGLICENCE)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                BarButtonItem btnChainCreate = new BarButtonItem
                                                   {
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainCreate);
                #region Новая запись
                btnChainCreate.ItemClick += delegate
                {
                    DrivingLicence newObject = new DrivingLicence
                                                   {
                        Workarea = SelectedItem.Workarea,
                        Date = DateTime.Today,
                        CategoryDate = DateTime.Today,
                        OwnId = _people.Id
                    };
                    Form frmProperties = newObject.ShowProperty();
                    frmProperties.FormClosed += delegate
                    {
                        if (!newObject.IsNew)
                        {
                            int position = valuesCollectinBind.Add(newObject);
                            valuesCollectinBind.Position = position;
                        }
                    };
                };
                #endregion

                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption =  SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_PROP, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                groupLinksAction.ItemLinks.Add(btnProp);
                #region Свойства
                btnProp.ItemClick += delegate
                {
                    DrivingLicence currentObject = (DrivingLicence)valuesCollectinBind.Current;
                    if (currentObject != null)
                    {
                        currentObject.ShowProperty();
                    }
                };
                _controlAgentDrivingLicence.Grid.DoubleClick += delegate
                {
                    System.Drawing.Point p = _controlAgentDrivingLicence.Grid.PointToClient(Control.MousePosition);
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = _controlAgentDrivingLicence.View.CalcHitInfo(p.X, p.Y);
                    if (hit.InRow)
                    {
                        DrivingLicence currentObject = (DrivingLicence)valuesCollectinBind.Current;
                        if (currentObject != null)
                        {
                            currentObject.ShowProperty();
                        }
                    }
                };
                #endregion

                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph =
                                                      ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                              ResourceImage.DELETE_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnDelete);
                #region Удаление
                btnDelete.ItemClick += delegate
                {
                    DrivingLicence currentObject = (DrivingLicence)valuesCollectinBind.Current;
                    if (currentObject != null)
                    {
                        int res = Extentions.ShowMessageChoice(SelectedItem.Workarea,
                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                            "Удаление водительского удостоверения",
                                 string.Empty,
                                 Properties.Resources.STR_CHOICE_DEL);

                        if (res == 0)
                        {
                            try
                            {
                                currentObject.Remove();
                                valuesCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления водительского удостоверения!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (res == 1)
                        {
                            try
                            {
                                currentObject.Delete();
                                valuesCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления водительского удостоверения!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion

                page.Groups.Add(groupLinksAction);

                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlAgentDrivingLicence.View, "DEFAULT_LISTVIEWDRIVINGLICENCE");
                _controlAgentDrivingLicence.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        System.Drawing.Rectangle r = e.Bounds;
                        System.Drawing.Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.MONEY_X16);
                        e.Graphics.DrawImageUnscaled(img, r);
                        e.Handled = true;
                    }
                    if (e.Column.Name == "colStateImage")
                    {
                        System.Drawing.Rectangle r = e.Bounds;
                        int index = _controlAgentDrivingLicence.View.GetDataSourceRowIndex(e.RowHandle);
                        DrivingLicence v = (DrivingLicence)valuesCollectinBind[index];
                        System.Drawing.Image img = v.State.GetImage();
                        e.Graphics.DrawImageUnscaled(img, r);
                        e.Handled = true;
                    }
                };

                Control.Controls.Add(_controlAgentDrivingLicence);
                _controlAgentDrivingLicence.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlAgentDrivingLicence.Grid;
            HidePageControls(ExtentionString.CONTROL_AGENT_DRIVINGLICENCE);
        }
    }
}
