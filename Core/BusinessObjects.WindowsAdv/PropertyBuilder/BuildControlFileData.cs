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
    /// Формирование контрола для отображения свойств
    /// </summary>
    internal sealed class BuildControlFileData : BasePropertyControlIBase<FileData>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlFileData()
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
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.FileExtention = _common.txtFileExtention.Text;
            SelectedItem.AllowVersion = _common.chkAllowVersion.Checked;
            SelectedItem.VersionCode = (int)_common.edVersionCode.Value;
            SaveStateData();

            InternalSave();
        }

        ControlFileData _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlFileData
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = {Text = SelectedItem.NameFull},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = {Text = SelectedItem.CodeFind},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  txtFileExtention = {Text = SelectedItem.FileExtention},
                                  Workarea = SelectedItem.Workarea,
                                  chkAllowVersion = {Checked = SelectedItem.AllowVersion}
                              };
                _common.edVersionCode.Value = SelectedItem.VersionCode;
                #region Экспорт файла
                BarButtonItem btnExport = new BarButtonItem
                                                      {
                                                          Caption = "Экспорт файла",
                                                          RibbonStyle = RibbonItemStyles.Large,
                                                          Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAOUT_X32)
                                                      };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnExport);
                btnExport.ItemClick += delegate
                                                       {
                                                           try
                                                           {
                                                               SaveFileDialog dlg = new SaveFileDialog
                                                                                        {
                                                                                            FileName = SelectedItem.NameFull,
                                                                                            DefaultExt = SelectedItem.FileExtention,
                                                                                            InitialDirectory =
                                                                                                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                                                                        };

                                                               if (dlg.ShowDialog() == DialogResult.OK)
                                                               {
                                                                   SelectedItem.ExportStreamDataToFile(dlg.FileName);
                                                               }
                                                           }
                                                           catch (Exception ex)
                                                           {
                                                               XtraMessageBox.Show(ex.Message,
                                                                   SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                           }
                                                       }; 
                #endregion
                #region Импорт файла
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
                                Filter = "Все файлы|*.*|Библиотеки|*.dll|Приложения|*.exe|Файлы конфигурации|*.config|Документы Word|*.docx",

                                //InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),

                            };
                            if (SelectedItem.FileExtention == "dll")
                                dlg.FilterIndex = 2;
                            else if (SelectedItem.FileExtention == "exe")
                                dlg.FilterIndex = 3;
                            //dlg.DefaultExt = SelectedItem.FileExtention;
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                _common.txtName.Text = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                                _common.txtFileExtention.Text = System.IO.Path.GetExtension(dlg.FileName).Substring(1);
                                SelectedItem.SetStreamFromFile(dlg.FileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message,
                                SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }; 
                    #endregion

                    #region Просмотр файла
                    BarButtonItem btnPreview = new BarButtonItem
                    {
                        Caption = "Просмотр файла",
                        RibbonStyle = RibbonItemStyles.Large,
                        Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.PREVIEW_X32)
                    };
                    frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnPreview);
                    btnPreview.ItemClick += delegate
                                                {
                                                    string filename= string.Empty;
                        try
                        {
                            if (SelectedItem.IsNew)
                                return;
                            if (!SelectedItem.IsStreamDataRefreshDone)
                                SelectedItem.RefreshSteamData();
                            if (SelectedItem.IsStreamDataNull)
                                return;

                            filename = Path.GetTempFileName() + "." + SelectedItem.FileExtention;

                            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                            {
                                writer.Write(SelectedItem.StreamData);
                            }

                            //System.Diagnostics.Process prc = System.Diagnostics.Process.Start(filename);
                            System.Diagnostics.Process prc = new System.Diagnostics.Process();
                            prc.StartInfo.FileName = filename;
                            prc.StartInfo.Verb = "Open";
                            prc.EnableRaisingEvents = true;
                            prc.StartInfo.CreateNoWindow = true;

                            prc.Exited += delegate
                                              {

                                                  try
                                                  {
                                                      File.Delete(filename);
                                                  }
                                                  catch (Exception)
                                                  {
                                                      
                                                  }
                                              };
                            
                            prc.Start();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                File.Delete(filename);
                            }
                            catch (Exception)
                            {

                            }
                            XtraMessageBox.Show(ex.Message,
                                SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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