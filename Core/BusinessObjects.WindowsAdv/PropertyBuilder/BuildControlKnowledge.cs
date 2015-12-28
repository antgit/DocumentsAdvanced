using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств аналитики
    /// </summary>
    internal sealed class BuildControlKnowledge : BasePropertyControlIBase<Knowledge>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlKnowledge()
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

            SaveStateData();

            InternalSave();
        }

        ControlKnowledge _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlKnowledge
                {
                    Name = ExtentionString.CONTROL_COMMON_NAME,
                    txtName = { Text = SelectedItem.Name },
                    txtNameFull2 = { Text = SelectedItem.NameFull },
                    txtCode = { Text = SelectedItem.Code },
                    txtCodeFind = { Text = SelectedItem.CodeFind },
                    txtMemo = { Text = SelectedItem.Memo },
                    Workarea = SelectedItem.Workarea
                };
                if(SelectedItem.FileId!=0)
                {
                    _common.btnEditFile.Text = SelectedItem.File.NameFull;
                }
                _common.btnEditFile.ButtonClick += delegate
                                                       {
                                                           Hierarchy rootFiles = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_FILEDATA_KNOWLEDGE);
                                                           List<FileData> collFiles = rootFiles.GetTypeContents<FileData>();
                                                           List<FileData> selFiles = SelectedItem.Workarea.Empty<FileData>().BrowseList(null, collFiles);
                                                           if(selFiles!=null && selFiles.Count>0)
                                                           {
                                                               SelectedItem.FileId = selFiles[0].Id;
                                                               _common.btnEditFile.Text = selFiles[0].NameFull;
                                                               if(SelectedItem.IsNew)
                                                               {
                                                                   _common.txtName.Text = selFiles[0].Name;
                                                               }
                                                           }
                                                       };

                BarButtonItem btnImport = new BarButtonItem
                {
                    Caption = "Импорт файла",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAINTO_X32)
                };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnImport);
                btnImport.ItemClick += delegate
                {
                    try
                    {
                        OpenFileDialog dlg = new OpenFileDialog
                        {
                            FileName = SelectedItem.NameFull,
                            Filter = "Все файлы|*.*|Microsoft Excel|*.xlsx|Adobe PDF|*.pdf|Презентация Microsoft PowerPoint|*.pptx|Документ Microsoft Word|*.docx"
                        };
                        
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            
                            FileData fd = new FileData
                                              {Workarea = SelectedItem.Workarea, KindId = FileData.KINDID_FILEDATA};
                            fd.SetStreamFromFile(dlg.FileName);
                            fd.Name = Path.GetFileNameWithoutExtension(dlg.FileName);
                            fd.StateId = State.STATEACTIVE;
                            fd.Save();

                            Hierarchy rootFiles = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_FILEDATA_KNOWLEDGE);

                            rootFiles.ContentAdd<FileData>(fd);

                            SelectedItem.FileId = fd.Id;
                            _common.btnEditFile.Text = fd.NameFull;
                            _common.txtName.Text = fd.Name;

                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
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