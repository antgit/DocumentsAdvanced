using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlFolder : BasePropertyControlIBase<Folder>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlFolder()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
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
            SelectedItem.ViewListDocumentsId = (int)_common.cmbViewDocs.EditValue;
            SelectedItem.DocumentId = (int)_common.cmbOperationTemplate.EditValue;
            SelectedItem.FormId = (int)_common.cmbForm.EditValue;
            SaveStateData();

            InternalSave();
        }

        BindingSource _bindingSourceOperation;
        List<Document> _collOperation;
        BindingSource _bindingSourceForm;
        List<Library> _collForm;
        BindingSource _bindingSourceViewDocs;
        List<CustomViewList> _collViewDocs;
        ControlFolder _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlFolder
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  Workarea = SelectedItem.Workarea
                              };
                
                #region Данные для списка "Шаблон операции"
                _common.cmbOperationTemplate.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbOperationTemplate.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceOperation = new BindingSource();
                _collOperation = new List<Document>();
                if (SelectedItem.DocumentId != 0)
                    _collOperation.Add(SelectedItem.Workarea.Cashe.GetCasheData<Document>().Item(SelectedItem.DocumentId));
                _bindingSourceOperation.DataSource = _collOperation;
                _common.cmbOperationTemplate.Properties.DataSource = _bindingSourceOperation;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbOperationTemplate, "DEFAULT_LISTVIEWDOCUMENT_TEMPLATES");
                _common.cmbOperationTemplate.EditValue = SelectedItem.DocumentId;
                _common.cmbOperationTemplate.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbOperationTemplate.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbOperationTemplate.EditValue = 0;
                };
                #endregion

                #region Данные для списка "форма"
                _common.cmbForm.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbForm.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceForm = new BindingSource();
                _collForm = new List<Library>();
                if (SelectedItem.FormId != 0)
                    _collForm.Add(SelectedItem.Workarea.Cashe.GetCasheData<Library>().Item(SelectedItem.FormId));
                _bindingSourceForm.DataSource = _collForm;
                _common.cmbForm.Properties.DataSource = _bindingSourceForm;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbForm, "DEFAULT_LOOKUPLIBRARY");
                _common.cmbForm.EditValue = SelectedItem.FormId;
                _common.cmbForm.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbForm.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbForm.EditValue = 0;
                };
                #endregion

                #region Данные для списка "список документов"
                _common.cmbViewDocs.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbViewDocs.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceViewDocs = new BindingSource();
                _collViewDocs = new List<CustomViewList>();
                if (SelectedItem.ViewListDocumentsId != 0)
                    _collViewDocs.Add(SelectedItem.Workarea.Cashe.GetCasheData<CustomViewList>().Item(SelectedItem.ViewListDocumentsId));
                _bindingSourceViewDocs.DataSource = _collViewDocs;
                _common.cmbViewDocs.Properties.DataSource = _bindingSourceViewDocs;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbViewDocs, "DEFAULT_LOOKUPCUSTOMVIEWLIST");
                _common.cmbViewDocs.EditValue = SelectedItem.ViewListDocumentsId;
                _common.cmbViewDocs.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbViewDocs.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbViewDocs.EditValue = 0;
                };
                #endregion

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                if (!SelectedItem.IsNew && SelectedItem.IsReadOnly)
                {
                    _common.Enabled = false;
                }
                frmProp.btnRefresh.ItemClick += delegate
                {
                    _collOperation = SelectedItem.Workarea.GetTemplates<Document>(true);
                    _bindingSourceOperation.DataSource = _collOperation;

                    _collForm = SelectedItem.Workarea.GetCollection<Library>(true).Where(s => s.KindValue == 16).ToList();
                    _bindingSourceForm.DataSource = _collForm;

                    _collViewDocs = SelectedItem.Workarea.GetCollection<CustomViewList>(true).Where(s => s.SourceEntityTypeId == 20).ToList();
                    _bindingSourceViewDocs.DataSource = _collViewDocs;
                };
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
                if (cmb.Name == "cmbOperationTemplate" && _bindingSourceOperation.Count < 2)
                {
                    _collOperation = SelectedItem.Workarea.GetTemplates<Document>();
                    _bindingSourceOperation.DataSource = _collOperation;
                }
                else if (cmb.Name == "cmbForm" && _bindingSourceForm.Count < 2)
                {
                    _collForm = SelectedItem.Workarea.GetCollection<Library>().Where(s => s.KindValue == 16).ToList();
                    _bindingSourceForm.DataSource = _collForm;
                }
                else if (cmb.Name == "cmbViewDocs" && _bindingSourceViewDocs.Count < 2)
                {
                    _collViewDocs = SelectedItem.Workarea.GetCollection<CustomViewList>().Where(s => s.SourceEntityTypeId == 20).ToList();
                    _bindingSourceViewDocs.DataSource = _collViewDocs;
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