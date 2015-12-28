using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств водительского удостоверения
    /// </summary>
    internal sealed class BuildControlAgentDrivingLicence : BasePropertyControlICore<DrivingLicence>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlAgentDrivingLicence()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }

        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Date = _common.dtDate.DateTime;
            SelectedItem.SeriesNo = _common.txtSeriesNo.Text;
            SelectedItem.Number = _common.txtNumber.Text;
            if (_common.dtExpireDate.EditValue == null)
                SelectedItem.Expire = null;
            else
                SelectedItem.Expire = _common.dtExpireDate.DateTime;
            SelectedItem.AuthorityId = (int)_common.cmbAuthority.EditValue;
            SelectedItem.Category = _common.txtCategory.Text;
            SelectedItem.CategoryDate = _common.dtCategoryDate.DateTime;
            if (_common.dtCategoryExpire.EditValue == null)
                SelectedItem.CategoryExpire = null;
            else
                SelectedItem.CategoryExpire = _common.dtCategoryExpire.DateTime;
            SelectedItem.Restriction = _common.txtRestriction.Text;

            SaveStateData();

            InternalSave();
        }

        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_STATES_NAME)
                BuildPageState();
        }

        ControlAgentDrivingLicence _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlAgentDrivingLicence
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  dtDate = {DateTime = SelectedItem.Date},
                                  txtSeriesNo = {Text = SelectedItem.SeriesNo},
                                  txtNumber = { Text = SelectedItem.Number },
                                  Workarea = SelectedItem.Workarea
                              };
                if (SelectedItem.Expire.HasValue)
                    _common.dtExpireDate.DateTime = SelectedItem.Expire.Value;
                _common.txtCategory.Text = SelectedItem.Category;
                _common.dtCategoryDate.DateTime = SelectedItem.CategoryDate;
                if (SelectedItem.CategoryExpire.HasValue)
                    _common.dtCategoryExpire.DateTime = SelectedItem.CategoryExpire.Value;
                _common.txtRestriction.Text = SelectedItem.Restriction;

                #region Данные для списка "Корреспондент - кем выдан"
                _common.cmbAuthority.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbAuthority.Properties.ValueMember = GlobalPropertyNames.Id;
                BindingSource bindAuthority = new BindingSource();
                List<Agent> collAuthority = new List<Agent>();
                if (SelectedItem.AuthorityId != 0)
                    collAuthority.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(SelectedItem.AuthorityId));
                bindAuthority.DataSource = collAuthority;
                _common.cmbAuthority.Properties.DataSource = bindAuthority;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewAuthority, "DEFAULT_LOOKUPAGENT");
                _common.cmbAuthority.Properties.View.BestFitColumns();
                _common.cmbAuthority.EditValue = SelectedItem.AuthorityId;
                _common.ViewAuthority.CustomUnboundColumnData += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "Image" && e.IsGetData && bindAuthority.Count > 0)
                    {
                        Agent imageItem = bindAuthority[e.ListSourceRowIndex] as Agent;
                        if (imageItem != null)
                        {
                            e.Value = imageItem.GetImage();
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && bindAuthority.Count > 0)
                    {
                        Agent imageItem = bindAuthority[e.ListSourceRowIndex] as Agent;
                        if (imageItem != null)
                        {
                            e.Value = imageItem.State.GetImage();
                        }
                    }
                };
                _common.cmbAuthority.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                {
                    if (_common.cmbAuthority != null && _common.cmbAuthority.Properties.PopupFormSize.Width != _common.cmbAuthority.Width)
                        _common.cmbAuthority.Properties.PopupFormSize = new System.Drawing.Size(_common.cmbAuthority.Width, 150);
                    try
                    {
                        _common.Cursor = Cursors.WaitCursor;
                        if (bindAuthority.Count < 2)
                        {
                            collAuthority = SelectedItem.Workarea.GetCollection<Agent>().Where(s => (s.KindValue & 1) == 1).ToList();
                            bindAuthority.DataSource = collAuthority;
                        }
                    }
                    catch (Exception)
                    { }
                    finally
                    {
                        _common.Cursor = Cursors.Default;
                    }
                };
                _common.cmbAuthority.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 0) return;
                    TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea }.ShowDialog();
                    if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
                    if (!bindAuthority.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                        bindAuthority.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
                    _common.cmbAuthority.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
                };
                _common.cmbAuthority.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbAuthority.EditValue = 0;
                };
                #endregion

                UIHelper.GenerateTooltips<DrivingLicence>(SelectedItem, _common);
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
    }
}
