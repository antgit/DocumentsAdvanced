using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств
    /// </summary>
    internal sealed class BuildControlPriceName : BasePropertyControlIBase<PriceName>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlPriceName()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;

            SelectedItem.CurrencyId = (int)_common.cmbCurrency.EditValue;

            SaveStateData();

            InternalSave();
        }

        ControlPriceName _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlPriceName
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  Workarea = SelectedItem.Workarea
                              };
                
                #region Валюта
                _common.cmbCurrency.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbCurrency.Properties.ValueMember = GlobalPropertyNames.Id;
                BindingSource bindCurrency = new BindingSource();
                List< Currency> collCurrency = new List<Currency>();
                if (SelectedItem.CurrencyId != 0)
                    collCurrency.Add(SelectedItem.Workarea.Cashe.GetCasheData<Currency>().Item(SelectedItem.CurrencyId));
                bindCurrency.DataSource = collCurrency;
                _common.cmbCurrency.Properties.DataSource = bindCurrency;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewCurrency, "DEFAULT_LOOKUP_NAME");
                _common.cmbCurrency.EditValue = SelectedItem.CurrencyId;
                _common.cmbCurrency.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                {
                    GridLookUpEdit cmb = sender as GridLookUpEdit;
                    if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                        cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
                    try
                    {
                        _common.Cursor = Cursors.WaitCursor;
                        if (bindCurrency.Count < 2)
                        {
                            collCurrency = SelectedItem.Workarea.GetCollection<Currency>();
                            bindCurrency.DataSource = collCurrency;
                        }
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        _common.Cursor = Cursors.Default;
                    }
                };
                _common.cmbCurrency.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 0) return;
                    TreeListBrowser<Currency> browseDialog = new TreeListBrowser<Currency> { Workarea = SelectedItem.Workarea }.ShowDialog();
                    if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
                    if (!bindCurrency.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                        bindCurrency.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
                    _common.cmbCurrency.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
                };
                _common.cmbCurrency.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbCurrency.EditValue = 0;
                };
                #endregion

                UIHelper.GenerateTooltips(SelectedItem, _common);
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
