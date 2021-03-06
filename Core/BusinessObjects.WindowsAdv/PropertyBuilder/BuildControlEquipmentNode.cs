using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������ �������� ��� ����������� ������� ������ ������������
    /// </summary>
    internal sealed class BuildControlEquipmentNode : BasePropertyControlIBase<EquipmentNode>
    {
        /// <summary>
        /// �����������
        /// </summary>
        public BuildControlEquipmentNode()
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
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_LINKFILES)
                BuildPageLinkedFiles();

        }
        /// <summary>����������</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;

            SelectedItem.DrawingAssemblyNumber = _common.txtDrawingAssemblyNumber.Text;
            SelectedItem.DrawingNumber = _common.txtDrawingNumber.Text;
            SelectedItem.Weight = (decimal)_common.edWeight.Value;

            if (string.IsNullOrEmpty(SelectedItem.CodeFind))
            {
                SelectedItem.CodeFind = (SelectedItem.Name + Transliteration.Front(SelectedItem.Name)).Replace(" ", "");
            }

            SaveStateData();

            InternalSave();
        }

        ControlEquipmentDetail _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlEquipmentDetail
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  txtDrawingAssemblyNumber = { Text = SelectedItem.DrawingAssemblyNumber },
                                  txtDrawingNumber = { Text = SelectedItem.DrawingNumber },
                                  edWeight = { Value = SelectedItem.Weight },
                                  Workarea = SelectedItem.Workarea
                              };

                UIHelper.GenerateTooltips<EquipmentNode>(SelectedItem, _common);
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
        #region �������� "�����"
        private List<IChainAdvanced<EquipmentNode, FileData>> _collectionFiles;
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

                groupLinksActionFiles = new RibbonPageGroup { Name = page.Name + "_ACTIONLIST", Text = "�������� � �������" };

                #region �������� ����� ����
                BarButtonItem btnFileCreate = new BarButtonItem
                                                  {
                                                      Name = "btnFileCreate",
                                                      ButtonStyle = BarButtonStyle.Default,
                                                      Caption = "�������� ����� ����",
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32),
                                                      SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.NEW_X32), "�������� ����� ����",
                                                                                               "���������� ����� � ���������� ��� � ������� ���������� �� �������� �������. �� �������� ������� ���������� ������ ������������ ������!")
                                                  };
                groupLinksActionFiles.ItemLinks.Add(btnFileCreate);
                btnFileCreate.ItemClick += BtnFileCreateItemClick;
                #endregion

                #region ������� � ������
                BarButtonItem btnFileLink = new BarButtonItem
                                                {
                                                    Name = "btnFileLink",
                                                    Caption = "������� � ������",
                                                    RibbonStyle = RibbonItemStyles.Large,
                                                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINKNEW_X32),
                                                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, "LINKNEW32"), "������� � ������",
                                                                                             "������� � ������ ��� ������������������ � ���� ������ �� �������� ��������������."),
                                                };
                groupLinksActionFiles.ItemLinks.Add(btnFileLink);
                btnFileLink.ItemClick += BtnFileLinkItemClick;

                #endregion

                #region ������� �����
                BarButtonItem btnFileExport = new BarButtonItem
                                                  {
                                                      Name = "btnFileExport",
                                                      Caption = "������� �����",
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAOUT_X32),
                                                      SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAOUT_X32), "������� �����",
                                                                                               "������ ����� �� ���� ������ ��� ��������� ��� ���������. ����� ��������� ���������� �������� ������������� ���� � ���� ������")
                                                  };
                groupLinksActionFiles.ItemLinks.Add(btnFileExport);
                btnFileExport.ItemClick += BtnFileExportItemClick;
                #endregion

                #region �������� �����
                BarButtonItem btnFilePreview = new BarButtonItem
                                                   {
                                                       Name = "btnFilePreview",
                                                       Caption = "�������� �����",
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = Properties.Resources.PREVIEW_X32,
                                                       SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, "PREVIEW32"), "�������� �����",
                                                                                                "�������� ����� ����������� ��������������� ��� ������� �����, ��������� ������ ���� ����������� �� ����� ����������. ����� ��������� � ���� ��������� ������� ������ <b>��</b> ��� �������� �������� ���������� ����� <br><i>(�� ��������� ������ <b>��</b> �� �������� �����)</i>")
                                                   };
                groupLinksActionFiles.ItemLinks.Add(btnFilePreview);
                btnFilePreview.ItemClick += BtnFilePreviewItemClick;
                #endregion

                #region �������� �����
                BarButtonItem btnFileDelete = new BarButtonItem
                                                  {
                                                      Name = "btnFileDelete",
                                                      Caption = "������� ����",
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32),
                                                      SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32), "������� ����",
                                                                                               "�������� ����� ���������� � ����������. �������� ������ �������� - �������� ����� � ����� � �������� ������ �����.")
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
                // TODO: �������� 13 �� ���������� ������������� ���� �����
                _collectionFiles = SelectedItem.GetLinkedFiles().Where(s => s.StateId == State.STATEACTIVE).ToList();
                _bindFiles = new BindingSource { DataSource = _collectionFiles };
                _gridFiles.DataSource = _bindFiles;
                ViewFiles.CustomUnboundColumnData += ViewCustomUnboundColumnDataFiles;
                _gridFiles.DoubleClick += GridFilesDoubleClick;

            }
            HidePageControls(ExtentionString.CONTROL_LINKFILES);
        }
        // ��������� ��������� ����������� ������ � ������
        void ViewCustomUnboundColumnDataFiles(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<EquipmentNode, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<EquipmentNode, FileData>;
                if (link != null && link.Right != null)
                {
                    e.Value = link.Right.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<EquipmentNode, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<EquipmentNode, FileData>;
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
            ChainAdvanced<EquipmentNode, FileData> link = _bindFiles.Current as ChainAdvanced<EquipmentNode, FileData>;
            if (link == null) return;
            // TODO: ������������ ������ ��������
            int res = Extentions.ShowMessageChoice(SelectedItem.Workarea, "�������� �����", "�������� �����", "�������� ������ � ������ ��������� � ������ ����������. �������� ����� ������� ������ ����� � ������ ������, �������� ����� � ����� ������� ��� ������ ������� ����.", "������� ������ �����|������� ����� � ����");
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
                        // TODO: ������������ ������ ��������
                        Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "������ ��������!", dbe.Message, dbe.Id);
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
                        // TODO: ������������ ������ ��������
                        Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                                               SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "������ ��������!", dbe.Message, dbe.Id);
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
                ChainAdvanced<EquipmentNode, FileData> link = _bindFiles.Current as ChainAdvanced<EquipmentNode, FileData>;
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
            foreach (ChainAdvanced<EquipmentNode, FileData> link in
                retColl.Select(item => new ChainAdvanced<EquipmentNode, FileData>(SelectedItem) { Right = item, StateId = State.STATEACTIVE, KindId = ckind.Id }))
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
                                             "Adobe PDF|*.pdf|Microsoft Excel 2007|*.xlsx|Microsoft Excel|*.xls|Microsoft Word 2007|*.docx|Microsoft Word|*.doc|PNG|*.png|JPG|*.jpg|XPS|*.xps|��� �����|*.*"
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

                ChainAdvanced<EquipmentNode, FileData> link =
                    new ChainAdvanced<EquipmentNode, FileData>(SelectedItem) { Right = fileData, StateId = State.STATEACTIVE, KindId = ckind.Id };

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
        List<FilePreviewThread<EquipmentNode>> OpennedDocs;
        protected void InvokeFilePreview()
        {
            try
            {

                if (_bindFiles.Current == null) return;
                if (OpennedDocs == null)
                    OpennedDocs = new List<FilePreviewThread<EquipmentNode>>();
                for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                    if (OpennedDocs[i].IsExit)
                        OpennedDocs.Remove(OpennedDocs[i]);
                FilePreviewThread<EquipmentNode> dpt = new FilePreviewThread<EquipmentNode>(_bindFiles.Current as ChainAdvanced<EquipmentNode, FileData>) { OpennedDocs = OpennedDocs }; //{ DocView = this };
                OpennedDocs.Add(dpt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}