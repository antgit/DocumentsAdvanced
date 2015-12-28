using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlPriceRegion : BasePropertyControlICore<PriceRegion>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlPriceRegion()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_COMMON_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Value = _common.edValue.Value;
            SelectedItem.ValueMin = _common.edValueMin.Value;
            SelectedItem.ValueMax = _common.edValueMax.Value;
            SelectedItem.ProductId = (int)_common.cmbProduct.EditValue;
            SelectedItem.PriceNameId = (int)_common.cmbPriceName.EditValue;
            SelectedItem.DateStart = _common.dtStart.DateTime;
            SelectedItem.DateEnd= _common.dtEnd.DateTime;

            InternalSave();
        }

        ControlPriceRegion _common;
        BindingSource _bindingSourcePriceName;
        List<PriceName> _collPriceName;
        BindingSource _bindingSourceProduct;
        List<Product> _collProduct;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlPriceRegion
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  edValue = { Value = SelectedItem.Value},
                                  edValueMax = { Value = SelectedItem.ValueMax },
                                  edValueMin = { Value = SelectedItem.ValueMin },
                                  dtStart = { DateTime = SelectedItem.DateStart },
                                  dtEnd = { DateTime = SelectedItem.DateEnd },
                                  Workarea = SelectedItem.Workarea
                              };
                _common.layoutControlItemCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                _common.layoutControlItemCodeFind.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                _common.layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                _common.layoutControlItemNameFull2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                #region Данные для списка "Виды цен"
                _common.cmbPriceName.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbPriceName.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourcePriceName= new BindingSource();
                _collPriceName = new List<PriceName>();
                if (SelectedItem.PriceNameId != 0)
                    _collPriceName.Add(SelectedItem.Workarea.Cashe.GetCasheData<PriceName>().Item(SelectedItem.PriceNameId));
                _bindingSourcePriceName.DataSource = _collPriceName;
                _common.cmbPriceName.Properties.DataSource = _bindingSourcePriceName;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbPriceName, "DEFAULT_LOOKUP_NAME");
                _common.cmbPriceName.EditValue = SelectedItem.PriceNameId;
                _common.cmbPriceName.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbPriceName.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbPriceName.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Товары"
                _common.cmbProduct.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbProduct.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceProduct= new BindingSource();
                _collProduct = new List<Product>();
                if (SelectedItem.ProductId != 0)
                    _collProduct.Add(SelectedItem.Workarea.Cashe.GetCasheData<Product>().Item(SelectedItem.ProductId));
                _bindingSourceProduct.DataSource = _collProduct;
                _common.cmbProduct.Properties.DataSource = _bindingSourceProduct;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbProduct, "DEFAULT_LOOKUP_NAME");
                _common.cmbProduct.EditValue = SelectedItem.ProductId;
                _common.cmbProduct.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbProduct.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbProduct.EditValue = 0;
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
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LookUpEdit cmb = sender as LookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbPriceName" && _bindingSourcePriceName.Count < 2)
                {
                    _collPriceName = SelectedItem.Workarea.GetCollection<PriceName>();
                    _bindingSourcePriceName.DataSource = _collPriceName;
                }
                else if (cmb.Name == "cmbProduct" && _bindingSourceProduct.Count < 2)
                {
                    _collProduct = SelectedItem.Workarea.GetCollection<Product>();
                    _bindingSourceProduct.DataSource = _collProduct;
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
    }
}