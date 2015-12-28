using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Workflow.Activities.Rules;
using System.Workflow.Activities.Rules.Design;
using BusinessObjects.Windows.Controls;
using System.Globalization;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств
    /// </summary>
    internal sealed class BuildControlRuleset : BasePropertyControlIBase<Ruleset>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlRuleset()
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
            SelectedItem.Code = _common.txtCode.Text.ToUpper();
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.ActivityName = _common.cmbType.Text;
            SelectedItem.LibraryId = (int)_common.cmbLibrary.EditValue;
            SaveStateData();

            InternalSave();
        }

        ControlRuleset _common;
        private BindingSource _bindLibrary;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlRuleset
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  cmbType = {Text = SelectedItem.ActivityName},
                                  Workarea = SelectedItem.Workarea
                              };
                
                _bindLibrary = new BindingSource();
                List<Library> collLibrary = SelectedItem.Workarea.GetCollection<Library>().Where(s => s.KindValue == 1).ToList();
                _bindLibrary.DataSource = collLibrary;
                _common.cmbLibrary.Properties.DataSource = _bindLibrary;
                _common.cmbLibrary.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbLibrary.Properties.ValueMember = GlobalPropertyNames.Id;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbLibrary, "DEFAULT_LOOKUP_NAME");
                _common.cmbLibrary.EditValue = SelectedItem.LibraryId;

                _common.cmbType.Text = SelectedItem.ActivityName;

                if (SelectedItem.LibraryId!=0)
                {
                    PopulateTypeCombobox();
                }
                _common.cmbLibrary.EditValueChanged += CmbLibraryEditValueChanged;

                BarButtonItem btnAddExists = new BarButtonItem
                                                 {
                                                     Caption =
                                                         (SelectedItem.KindValue == Ruleset.KINDVALUE_RULESET || SelectedItem.KindValue == Ruleset.KINDVALUE_WEBRULESET)
                                                             ? "Редактор правил"
                                                             : "Редактор процесса",
                                                     RibbonStyle = RibbonItemStyles.Large,
                                                     Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                                 };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnAddExists);
                btnAddExists.ItemClick += delegate
                {
                    if (SelectedItem.KindValue == Ruleset.KINDVALUE_RULESET || SelectedItem.KindValue == Ruleset.KINDVALUE_WEBRULESET)
                        ShowRuleEditor();
                    else
                        ShowWFDesigner();
                };

                BarButtonItem btnExport = new BarButtonItem
                                              {
                                                  Caption = "Экспорт в XML",
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAOUT_X32)
                                              };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnExport);
                btnExport.ItemClick += BtnExportItemClick;

                BarButtonItem btnImport = new BarButtonItem
                {
                    Caption = "Импорт",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAINTO_X32)
                };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnImport);
                btnImport.ItemClick += BtnImportItemClick;

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

        void BtnImportItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                    OpenFileDialog dlg = new OpenFileDialog
                    {
                        FileName = SelectedItem.Code
                        //InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                    };

                    if (SelectedItem.KindValue == 1)
                    {
                        dlg.Filter = "Правила|*.rules";
                        dlg.DefaultExt = "rules";
                    }
                    else if (SelectedItem.KindValue == 2)
                    {
                        dlg.Filter = "Процессы|*.xaml";
                        dlg.DefaultExt = "xaml";
                    }
                    else
                    {
                        dlg.Filter = "Все файлы|*.*";
                    }

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        using(StreamReader sr = new StreamReader(dlg.FileName, Encoding.UTF8))
                        {
                            SelectedItem.Value = sr.ReadToEnd();
                        }

                        //string fileContents;
                        //using (System.IO.StreamReader sr = new System.IO.StreamReader(dlg.FileName))
                        //{
                        //    fileContents = sr.ReadToEnd();
                        //}
                        //SelectedItem.Value = fileContents;
                    }
                
                
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void BtnExportItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (SelectedItem.KindValue == 1)
                {
                    SaveFileDialog dlg = new SaveFileDialog
                                             {
                                                 FileName = SelectedItem.Name,
                                                 Filter = "Правила|*.rules",
                                                 DefaultExt = "rules",
                                                 InitialDirectory =
                                                     Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                             };

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        RuleSet rules = SelectedItem.DeserializeRuleSet();
                        rules.Save(dlg.FileName);
                    }
                }
                else //if (SelectedItem.KindValue == 2)
                {
                    SaveFileDialog dlg = new SaveFileDialog
                                             {
                                                 FileName = SelectedItem.Name,
                                                 Filter = "Процессы|*.xaml",
                                                 DefaultExt = "xaml",
                                                 InitialDirectory =
                                                     Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                             };

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(dlg.FileName, true))
                        {
                            sw.Write(SelectedItem.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),                                       
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void ShowRuleEditor()
        {
            if (SelectedItem.IsNew)
            {
                RuleSet ruleset = new RuleSet();
                Type t = _common.cmbType.SelectedItem as Type;
                if(t == null)    
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Необходимо выбрать тип для которого предназначены правила",
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                RuleSetDialog ruleSetDialog = new RuleSetDialog(t, null, ruleset);
                DialogResult result = ruleSetDialog.ShowDialog();
                if (result == DialogResult.Cancel) return;
                SelectedItem.Value = ruleSetDialog.RuleSet.RuleSetSerialize();
            }
            else
            {
                RuleSet ruleset = SelectedItem.DeserializeRuleSet();
                Type t =  SelectedItem.Library.GetAssembly().GetTypes().FirstOrDefault(s => s.FullName == SelectedItem.ActivityName);
                RuleSetDialog ruleSetDialog = new RuleSetDialog(t, null, ruleset);
                DialogResult result = ruleSetDialog.ShowDialog();
                if (result == DialogResult.Cancel) return;
                SelectedItem.Value = ruleSetDialog.RuleSet.RuleSetSerialize();
            }
        }

        private void ShowWFDesigner()
        {
            //string fileName = Path.Combine(Application.StartupPath, "BusinessObjects.ActivityDesigner.dll");
            //if(File.Exists(fileName))
            //{
            //    try
            //    {
            //        Assembly a = Assembly.LoadFrom(fileName);
            //        object instance = a.CreateInstance("BusinessObjects.ActivityDesigner.RehostingWfDesigner");
            //        Type type = instance.GetType();//typeof(instance);

            //        object[] args = new object[] { SelectedItem.Value };
            //        PropertyInfo pi = type.GetProperty("Workarea");
            //        pi.SetValue(instance, SelectedItem.Workarea, null);

            //        PropertyInfo piAm = type.GetProperty("AssemblyMain");
            //        piAm.SetValue(instance, System.Reflection.Assembly.GetExecutingAssembly(), null);

            //        MethodInfo mi = type.GetMethod("Load");
                    
            //        FastInvokeHandler fastInvoker = FastMethodInvoker.GetMethodInvoker(mi);
            //        object result = fastInvoker(instance, args);

            //        MethodInfo mi2 = type.GetMethod("ShowDialog");
            //        FastInvokeHandler fastInvoker2 = FastMethodInvoker.GetMethodInvoker(mi2);
            //        object result2 = fastInvoker2(instance, null);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}


            string fileName = Path.Combine(Application.StartupPath, "ActivityDisigner.exe");
            if (File.Exists(fileName))
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo prc = new ProcessStartInfo();
                    prc.Arguments = string.Format("\"{0}\" {1}", SelectedItem.Workarea.ConnectionCode, SelectedItem.Id);
                    prc.FileName = "ActivityDisigner.exe";
                    System.Diagnostics.Process.Start(prc);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }

        void CmbLibraryEditValueChanged(object sender, EventArgs e)
        {
            PopulateTypeCombobox();
        }

        private void PopulateTypeCombobox()
        {
            if ((int)_common.cmbLibrary.EditValue == 0)
            {
                _common.cmbType.Properties.Items.Clear();
                return;
            }
            _common.cmbType.Properties.Items.Clear();
            int libId = (int)_common.cmbLibrary.EditValue;
            Library lib = SelectedItem.Workarea.GetObject<Library>(libId);
            Assembly assembly = lib.GetAssembly();

            if (assembly != null)
            {
                try
                {
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if(type.IsClass && type.IsPublic && !type.IsAbstract)
                        {
                            _common.cmbType.Properties.Items.Add(type);    
                        }
                        // add a check here if you want to constrain the kinds of Types (e.g. Activity) that rulesets can be authored against

                        //if (type.IsSubclassOf(typeof(Activity)))
                        //{
                        
                        //}
                    }
                    _common.cmbType.Properties.Sorted = true;
                }
                catch (ReflectionTypeLoadException ex)
                {
                    MessageBox.Show(string.Format(CultureInfo.InvariantCulture, "Error loading types from assembly '{0}': \r\n\n{1}", assembly.FullName, ex.LoaderExceptions[0].Message), "Type Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}