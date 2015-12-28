using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraEditors;
using BusinessObjects.Documents;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlAutonum : BasePropertyControlICore<Autonum>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlAutonum()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }

        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.Number = (int)_common.txtNumber.Value;
            SelectedItem.DocKind = (int)_common.cmbDocumentKind.EditValue;
            SelectedItem.UserId = (int)_common.cmbUser.EditValue;
            SelectedItem.Prefix = _common.txtPrefix.Text;
            SelectedItem.Suffix = _common.txtSuffics.Text;
            SelectedItem.DocKind = (int)_common.cmbDocumentKind.EditValue;
            SelectedItem.UserId = (int) _common.cmbUser.EditValue;
            SelectedItem.WfId = (int) _common.cmbWfId.EditValue;
            if (_common.cmbMyCompanyId.EditValue != null)
                SelectedItem.MyCompanyId = (int)_common.cmbMyCompanyId.EditValue;
            else
                SelectedItem.MyCompanyId = 0;
            SaveStateData();
            InternalSave();
        }

        ControlAutonum _common;
        private List<Agent> _collMyCompany;
        private BindingSource bindingSourceMyCompanyId;

        private BindingSource _bindingSourceDocKind;
        private BindingSource _bindingSourceWfId;
        private List<Ruleset> _collWfId;

        private BindingSource _bindingSourceUser;
        private List<Uid> _collUsers;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlAutonum
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtNumber = {Value = (decimal) SelectedItem.Number},
                                  Workarea = SelectedItem.Workarea,
                                  txtPrefix = {Text = SelectedItem.Prefix},
                                  txtSuffics = {Text = SelectedItem.Suffix},
                                  txtCode = {Text = SelectedItem.Code}
                              };

                #region Данные для списка "Предприятие"
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbMyCompanyId, "DEFAULT_LOOKUPAGENT");

                _collMyCompany = new List<Agent>();
                bindingSourceMyCompanyId = new BindingSource(); //{ DataSource = hierarchy.GetTypeContents<Agent>() };
                if (SelectedItem.MyCompanyId != 0)
                    _collMyCompany.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(SelectedItem.MyCompanyId));

                bindingSourceMyCompanyId.DataSource = _collMyCompany;

                _common.ViewMyCompanyId.CustomUnboundColumnData += (sender, e) => DisplayAgentImagesLookupGrid(e, bindingSourceMyCompanyId);
                _common.cmbMyCompanyId.Properties.DataSource = bindingSourceMyCompanyId;
                _common.cmbMyCompanyId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbMyCompanyId.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbMyCompanyId.QueryPopUp += CmbGridSearchLookUpEditQueryPopUp;
                _common.cmbMyCompanyId.EditValue = SelectedItem.MyCompanyId;
                #endregion

                #region Данные для списка "тип документа"
                _common.cmbDocumentKind.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbDocumentKind.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceDocKind = new BindingSource();
                _bindingSourceDocKind.DataSource = SelectedItem.Workarea.CollectionDocumentKinds;
                _common.cmbDocumentKind.Properties.DataSource = _bindingSourceDocKind;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbDocumentKind, "DEFAULT_LOOKUP_NAME");
                _common.cmbDocumentKind.EditValue = SelectedItem.DocKind;
                _common.cmbDocumentKind.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbDocumentKind.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Метод расчета"
                //
                _common.cmbWfId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbWfId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceWfId = new BindingSource();
                _collWfId = new List<Ruleset>();
                _bindingSourceWfId.DataSource = _collWfId;
                if (SelectedItem.WfId != 0)
                    _collWfId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Ruleset>().Item(SelectedItem.WfId));
                _common.cmbWfId.Properties.DataSource = _bindingSourceWfId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewWf, "DEFAULT_LOOKUP_NAME");
                _common.cmbWfId.EditValue = SelectedItem.WfId;
                _common.cmbWfId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbWfId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbWfId.EditValue = 0;
                };
                #endregion


                #region Данные для списка "Пользователь"
                _common.cmbUser.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbUser.Properties.ValueMember = GlobalPropertyNames.Id;
                _collUsers = new List<Uid>();
                _bindingSourceUser = new BindingSource();
                _bindingSourceUser.DataSource = _collUsers;
                if (SelectedItem.UserId != 0)
                    _collUsers.Add(SelectedItem.Workarea.Cashe.GetCasheData<Uid>().Item(SelectedItem.UserId));
                _common.cmbUser.Properties.DataSource = _bindingSourceUser;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewUsers, "DEFAULT_LOOKUP_NAME");
                _common.cmbUser.EditValue = SelectedItem.UserId;
                _common.cmbUser.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbUser.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbUser.EditValue = 0;
                };
                #endregion

                _common.layoutControlItemNameFull2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                /*
                 // TODO:
                    cmbDocumentKind = { EditValue = SelectedItem.DocKind },
                    // TODO:
                    cmbUser = { EditValue = SelectedItem.UserId },
                 */
                UIHelper.GenerateTooltips<Autonum>(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                if (!SelectedItem.IsNew && SelectedItem.IsReadOnly)
                {
                    _common.Enabled = false;
                }
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridLookUpEdit cmb = sender as GridLookUpEdit;

            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbWfId" && _bindingSourceWfId.Count < 2)
                {
                    Hierarchy rootMethod = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_RULESET_AUTONUMMETHOD);
                    if (rootMethod != null)
                        _collWfId = rootMethod.GetTypeContents<Ruleset>();
                    _bindingSourceWfId.DataSource = _collWfId;
                }
                else if (cmb.Name == "cmbUser" && _bindingSourceUser.Count < 2)
                {
                    _collUsers= SelectedItem.Workarea.GetCollection<Uid>().Where(s => s.KindValue == Uid.KINDVALUE_USER).ToList();
                    _bindingSourceUser.DataSource = _collUsers;
                }
                //else if (cmb.Name == "cmbViewDocs" && _bindingSourceViewDocs.Count < 2)
                //{
                //    _collViewDocs = SelectedItem.Workarea.GetCollection<CustomViewList>().Where(s => s.SourceEntityTypeId == 20).ToList();
                //    _bindingSourceViewDocs.DataSource = _collViewDocs;
                //}
            }
            catch (Exception)
            {
            }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
        void CmbGridSearchLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SearchLookUpEdit cmb = sender as SearchLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 400);
            if (cmb != null && cmb.Properties.PopupFormSize.Height < 200)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 400);
            try
            {
                Int32 curMyCompany = 0;
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbMyCompanyId" && bindingSourceMyCompanyId.Count < 2)
                {
                    //Hierarchy hierarchy = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYDEPATMENTS);
                    _collMyCompany = SelectedItem.Workarea.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY); //hierarchy.GetTypeContents<Agent>();
                    bindingSourceMyCompanyId.DataSource = _collMyCompany;
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
        internal static void DisplayAgentImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAgents)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Agent imageItem = bindSourceAgents[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Agent imageItem = bindSourceAgents[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
    }
}
