using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств аналитики
    /// </summary>
    internal sealed class BuildControlCalendar : BasePropertyControlIBase<Calendar>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlCalendar()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_KNOWLEDGES, ExtentionString.CONTROL_KNOWLEDGES);
            TotalPages.Add(ExtentionString.CONTROL_NOTES, ExtentionString.CONTROL_NOTES);
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
            SelectedItem.StartDate = _common.edStartDate.DateTime;
            SelectedItem.StartTime = _common.edStartTime.Time.TimeOfDay;
            if (string.IsNullOrEmpty(SelectedItem.CodeFind))
            {
                SelectedItem.CodeFind = (SelectedItem.Name + Transliteration.Front(SelectedItem.Name)).Replace(" ", "");
            }
            SelectedItem.PriorityId = (int)_common.cmbPriorityId.EditValue;
            SaveStateData();

            InternalSave();
        }

        BindingSource _bindingSourcePriorityId;
        List<Analitic> _collPriorityId;
        ControlCalendar _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlCalendar
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  Workarea = SelectedItem.Workarea
                              };
                _common.edStartDate.DateTime = SelectedItem.StartDate;
                _common.edStartTime.EditValue = SelectedItem.StartTime;

                #region Данные для списка "Приоритет"
                _common.cmbPriorityId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbPriorityId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourcePriorityId = new BindingSource();
                _collPriorityId = new List<Analitic>();
                if (SelectedItem.PriorityId != 0)
                    _collPriorityId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.PriorityId));
                _bindingSourcePriorityId.DataSource = _collPriorityId;
                _common.cmbPriorityId.Properties.DataSource = _bindingSourcePriorityId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewPriority, "DEFAULT_LOOKUP_ANALITIC");
                _common.cmbPriorityId.EditValue = SelectedItem.PriorityId;
                _common.cmbPriorityId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.ViewPriority.CustomUnboundColumnData += ViewPriorityCustomUnboundColumnData;
                _common.cmbPriorityId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbPriorityId.EditValue = 0;
                };
                #endregion

                UIHelper.GenerateTooltips<Calendar>(SelectedItem, _common);
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
                cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbPriorityId" && _bindingSourcePriorityId.Count < 2)
                {
                    Hierarchy rootPriority = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("CONTRACT_PRIORITY");
                    if (rootPriority != null)
                        _collPriorityId = rootPriority.GetTypeContents<Analitic>();
                    else
                        _collPriorityId = SelectedItem.Workarea.GetCollection<Analitic>(Analitic.KINDVALUE_IMPORTANCE).Where(f => f.KindValue == Analitic.KINDVALUE_IMPORTANCE).ToList();

                    _bindingSourcePriorityId.DataSource = _collPriorityId;
                }
            }
            catch (Exception z)
            { }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
        void ViewPriorityCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayAnaliticImagesLookupGrid(e, _bindingSourcePriorityId);
        }
    }
}