using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.Windows;
using BusinessObjects.Exchange.Controls;
using System.Drawing;
using BusinessObjects.Developer;
using DevExpress.XtraEditors;

namespace BusinessObjects.Exchange
{
    public static partial class Extentions
    {
        #region Свойства
        /// <summary>
        /// Свойства настроек обмена
        /// </summary>
        /// <param name="item">Хранилище данных</param>
        /// <returns></returns>
        public static Form ShowPropertyExchangeSettings(this XmlStorage item, Workarea wa)
        {
            InternalShowPropertyBase<XmlStorage> showPropertyBase = new InternalShowPropertyBase<XmlStorage>
                                                                        {
                                                                            SelectedItem = item,
                                                                            ControlBuilder =
                                                                                new BuildControlExchangeSetting
                                                                                    {
                                                                                        SelectedItem = item
                                                                                        
                                                                                    }
                                                                        };
            FormProperties frm=showPropertyBase.ShowDialog() as FormProperties;
            frm.ribbon.ApplicationIcon = ResourceImage.GetByCode(wa,ResourceImage.DATABASE_X16);
            return frm;
        }
        #endregion
    }
    internal class BuildControlExchangeSetting : BasePropertyControlIBase<XmlStorage>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlExchangeSetting()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            //TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, 8);
            //TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, 4096);
            //TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, 4);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            //Сохранение результатов диалога
            newExchangeSettings.Name = _common.textEditName.Text;
            newExchangeSettings.Source = _common.textEditSource.Text;
            newExchangeSettings.Destination = _common.textEditDestination.Text;
            newExchangeSettings.Code = _common.cmbCode.Text;

            newExchangeSettings.StoredProcedure = _common.cmbStorProcedure.Text;
            newExchangeSettings.ExportTo = _common.cmbExportTo.SelectedIndex;
            newExchangeSettings.Path = _common.textEditPath.Text;
            newExchangeSettings.FileName = _common.textEditFileName.Text;
            newExchangeSettings.Compression = _common.checkEditCompression.Checked ? 1 : 0;
            newExchangeSettings.Memo = _common.textEditMemo.Text;

            newExchangeSettings.Memo = _common.textEditMemo.Text;
            
            switch (_common.cmbCode.SelectedIndex)
            {
                case 0:
                    newExchangeSettings.Code = "IMPORT";
                    break;
                case 1:
                    newExchangeSettings.Code = "EXPORT";
                    break;
            }

            switch (_common.cmbKind.SelectedIndex)
            {
                case 0:
                    newExchangeSettings.Kind = "SYSTEM";
                    break;
                case 1:
                    newExchangeSettings.Kind = "TEMPLATES";
                    break;
                case 2:
                    newExchangeSettings.Kind = "DOCTEMPLATES";
                    break;
                case 3:
                    newExchangeSettings.Kind = "LIBRARIES";
                    break;
                case 4:
                    newExchangeSettings.Kind = "OPTIONAL";
                    break;
                case 5:
                    newExchangeSettings.Kind = "USER";
                    break;
                case 6:
                    newExchangeSettings.Kind = "TABLES";
                    break;
            }

            SelectedItem.Name = _common.textEditName.Text;
            SelectedItem.Memo = _common.textEditMemo.Text;
            SelectedItem.Code = _common.cmbCode.Text;
            SelectedItem.XmlData = ExchangeSettings.SaveToString(newExchangeSettings);

            SaveStateData();

            try
            {
                SelectedItem.Validate();
                SelectedItem.Save();
            }
            catch (DatabaseException dbe)
            {
                Windows.Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049)
                    + Environment.NewLine + ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ControlExchangeSetting _common;
        ExchangeSettings newExchangeSettings;
        /// <summary>
        /// Получение коллекции имен таблиц, имеющих процедуру экспорта
        /// </summary>
        /// <returns></returns>
        private static List<ComboboxItem> GetTableNames(Workarea WA)
        {
            List<ComboboxItem> coll = WA.GetCollection<DbObject>().Where(s => !string.IsNullOrEmpty(s.ProcedureExport)).ToList().Select(ti => new ComboboxItem {Name = ti.Schema+"."+ti.Name, DisplayedName = ti.Memo}).ToList();

            coll.Sort((c1, c2) => c1.DisplayedName.CompareTo(c2.DisplayedName));

            return coll;
        }
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                newExchangeSettings = new ExchangeSettings();
                if (!SelectedItem.IsNew)
                {
                    newExchangeSettings = ExchangeSettings.LoadFromString(SelectedItem.XmlData);
                }
                _common = new ControlExchangeSetting
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  textEditName = {Text = newExchangeSettings.Name},
                                  textEditSource = {Text = newExchangeSettings.Source},
                                  textEditDestination = {Text = newExchangeSettings.Destination}
                              };
                //_common.textEdit1.Text = SelectedItem.Name;
                //_common.textEdit2.Text = SelectedItem.Code;
                //_common.textEdit3.Text = SelectedItem.Memo;

                #region Инициализация элементов управления формы

                switch (newExchangeSettings.Code)
                {
                    case "IMPORT":
                        _common.cmbCode.SelectedIndex = 0;
                        break;
                    case "EXPORT":
                        _common.cmbCode.SelectedIndex = 1;
                        break;
                    default:
                        _common.cmbCode.SelectedIndex = 0;
                        break;
                }

                switch (newExchangeSettings.Kind)
                {
                    case "SYSTEM":
                        _common.cmbKind.SelectedIndex = 0;
                        break;
                    case "TEMPLATES":
                        _common.cmbKind.SelectedIndex = 1;
                        break;
                    case "DOCTEMPLATES":
                        _common.cmbKind.SelectedIndex = 2;
                        break;
                    case "LIBRARIES":
                        _common.cmbKind.SelectedIndex = 3;
                        break;
                    case "OPTIONAL":
                        _common.cmbKind.SelectedIndex = 4;
                        break;
                    case "USER":
                        _common.cmbKind.SelectedIndex = 5;
                        break;
                    case "TABLES":
                        _common.cmbKind.SelectedIndex = 6;
                        break;
                }

                _common.cmbStorProcedure.Text = newExchangeSettings.StoredProcedure;
                _common.cmbExportTo.SelectedIndex = newExchangeSettings.ExportTo;
                _common.cmbImportFrom.SelectedIndex = newExchangeSettings.ExportTo;
                _common.textEditPath.Text = newExchangeSettings.Path;
                _common.textEditFileName.Text = newExchangeSettings.FileName;
                _common.checkEditCompression.Checked = (newExchangeSettings.Compression==1);
                _common.textEditMemo.Text = newExchangeSettings.Memo;

                BindingSource bindValues1 = new BindingSource { DataSource = newExchangeSettings.Values };
                _common.gridValues.DataSource = bindValues1;
                List<ExchangeValueList<int>> BackupValuesList = new List<ExchangeValueList<int>>(newExchangeSettings.Values);

                BindingSource bindValues2 = new BindingSource { DataSource = newExchangeSettings.ComplianceId };
                _common.gridComplianceId.DataSource = bindValues2;
                List<Compliance<int>> BackupComplianceIdList = new List<Compliance<int>>(newExchangeSettings.ComplianceId);

                BindingSource bindValues3 = new BindingSource { DataSource = newExchangeSettings.ComplianceCode };
                _common.gridComplianceCode.DataSource = bindValues3;
                List<Compliance<string>> BackupComplianceCodeList = new List<Compliance<string>>(newExchangeSettings.ComplianceCode);

                //Заполнение комбобокса
                _common.editorCode.DataSource = GetTableNames(SelectedItem.Workarea);
                _common.editorCode.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayedName"));
                _common.editorCode.DisplayMember = "DisplayedName";
                _common.editorCode.ValueMember = "Name";
                _common.editorCode.ShowHeader = false;
                _common.editorCode.ShowFooter = false;
                #endregion

                #region Собитыия
                _common.gridView1.DoubleClick += delegate
                {
                    Point p = _common.gridValues.PointToClient(Control.MousePosition);
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = _common.gridView1.CalcHitInfo(p.X, p.Y);

                    if (!hit.InRow || _common.gridView1.FocusedRowHandle <= -1) return;
                    ExchangeValueList<int> exchangeValueList = (_common.gridValues.DataSource as BindingSource).Current as ExchangeValueList<int>;
                    ShowPropertyValues(SelectedItem.Workarea, exchangeValueList);
                };

                _common.cmbCode.SelectedIndexChanged += delegate
                {
                    ChangeFormControlsState();
                };

                _common.cmbKind.SelectedIndexChanged += delegate
                {
                    ChangeFormControlsState();
                };

                _common.cmbExportTo.SelectedIndexChanged += delegate
                {
                    _common.cmbImportFrom.SelectedIndex = _common.cmbExportTo.SelectedIndex;
                    switch (_common.cmbExportTo.SelectedIndex)
                    {
                        case 0://Память
                            _common.textEditPath.Enabled = false;
                            _common.textEditFileName.Enabled = false;
                            break;
                        default:
                            _common.textEditPath.Enabled = true;
                            _common.textEditFileName.Enabled = true;
                            break;
                    }
                };

                _common.cmbImportFrom.SelectedIndexChanged += delegate
                {
                    _common.cmbExportTo.SelectedIndex = _common.cmbImportFrom.SelectedIndex;
                    switch (_common.cmbImportFrom.SelectedIndex)
                    {
                        case 0://Память
                            _common.textEditPath.Enabled = false;
                            _common.textEditFileName.Enabled = false;
                            break;
                        default:
                            _common.textEditPath.Enabled = true;
                            _common.textEditFileName.Enabled = true;
                            break;
                    }
                };

                _common.textEditPath.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    switch (_common.textEditPath.Properties.Buttons.IndexOf(e.Button))
                    {
                        case 0:
                            FolderBrowserDialog dialog = new FolderBrowserDialog();
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                _common.textEditPath.Text = dialog.SelectedPath;
                            }
                            break;
                        case 1:
                            _common.textEditPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            break;
                        case 2:
                            _common.textEditPath.Text = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "inout"); ;
                            break;
                    }
                };


                //_common.ParentForm.FormClosing += delegate(Object sender, FormClosingEventArgs e)
                //{
                //    if (!Directory.Exists(_common.editPath.Text))
                //    {
                //        if (XtraMessageBox.Show("Данный путь не существует. Хотите продолжить?", Application.ProductName, MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                //        {
                //            e.Cancel = true;
                //        }
                //    }
                //};
                #endregion

                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
                ChangeFormControlsState();
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }

        /// <summary>
        /// Изменяет состояние контролов формы (Enabled и Visible) в соответствии с состоянием комбобоксов
        /// </summary>
        private void ChangeFormControlsState()
        {
            switch (_common.cmbCode.SelectedIndex)
            {
                case 0://IMPORT
                    _common.layoutKind.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutStorProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutExportTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutImportFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.checkEditCompression.Enabled = false;
                 
                    _common.layoutValues.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutComplianceId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutComplianceCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.splitterItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.splitterItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.splitterItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    break;
                case 1://EXPORT
                    _common.layoutKind.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutStorProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutExportTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutImportFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.checkEditCompression.Enabled = true;

                    if (_common.cmbKind.SelectedIndex == 5)//USER
                    {
                        _common.cmbStorProcedure.Enabled = true;
                        _common.layoutStorProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        _common.gridValues.Enabled = true;
                    }
                    else
                    {
                        _common.cmbStorProcedure.Enabled = false;
                        _common.gridValues.Enabled = true;
                    }

                    if (_common.cmbKind.SelectedIndex == 4)//OPTIONAL
                    {
                        _common.layoutValues.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        _common.splitterItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        
                    }
                    else
                    {
                        _common.layoutValues.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        _common.splitterItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }

                    _common.layoutComplianceId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutComplianceCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.splitterItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.splitterItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
            }

            switch (_common.cmbImportFrom.SelectedIndex)
            {
                case 0://Память
                    _common.textEditPath.Enabled = false;
                    _common.textEditFileName.Enabled = false;
                    break;
                default:
                    _common.textEditPath.Enabled = true;
                    _common.textEditFileName.Enabled = true;
                    break;
            }
        }

        private static Form ShowPropertyValues(Workarea wa, ExchangeValueList<int> value)
        {
            FormProperties formExchangeValue = new FormProperties();
            ControlExchangeValue controlExchangeValue = new ControlExchangeValue();
            DevExpress.XtraGrid.Views.Grid.GridView LastFocusedView = controlExchangeValue.ViewValues;

            formExchangeValue.MinimumSize = new Size(controlExchangeValue.MinimumSize.Width + 50, controlExchangeValue.MinimumSize.Height + 200);

            formExchangeValue.StartPosition = FormStartPosition.CenterParent;
            controlExchangeValue.Dock = DockStyle.Fill;
            formExchangeValue.clientPanel.Controls.Add(controlExchangeValue);

            if (value.OptionCreate == null)
                value.OptionCreate = new CreateOption();
            if (value.OptionsFind == null)
                value.OptionsFind = new FindOption();
            if (value.OptionUpdate == null)
                value.OptionUpdate = new UpdateOption();
            if (value.Values == null)
                value.Values = new List<ExchangeValueItem<int>>();

            #region Инициализация элементов управления формы
            controlExchangeValue.comboBoxCode.Properties.DataSource = GetTableNames(wa);
            controlExchangeValue.comboBoxCode.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayedName"));
            controlExchangeValue.comboBoxCode.Properties.DisplayMember = "DisplayedName";
            controlExchangeValue.comboBoxCode.Properties.ValueMember = "Name";
            controlExchangeValue.comboBoxCode.Properties.ShowHeader = false;
            controlExchangeValue.comboBoxCode.Properties.ShowFooter = false;
            controlExchangeValue.comboBoxCode.EditValue = value.Code;

            controlExchangeValue.textEditFlag.Text = value.Flag.ToString();

            controlExchangeValue.checkedListBoxFindOptions.SetItemChecked(0, value.OptionsFind.AllowId);
            controlExchangeValue.checkedListBoxFindOptions.SetItemChecked(1, value.OptionsFind.AllowGuid);
            controlExchangeValue.checkedListBoxFindOptions.SetItemChecked(2, value.OptionsFind.AllowCode);
            controlExchangeValue.checkedListBoxFindOptions.SetItemChecked(3, value.OptionsFind.AllowName);
            controlExchangeValue.checkedListBoxFindOptions.SetItemChecked(4, value.OptionsFind.AllowSourceId);
            controlExchangeValue.textEditFindFlag.Text = value.OptionsFind.Value.ToString();

            controlExchangeValue.checkedListBoxCreateOptions.SetItemChecked(0, value.OptionCreate.AllowCreate);
            controlExchangeValue.checkedListBoxCreateOptions.SetItemChecked(1, value.OptionCreate.TryIdentity);
            controlExchangeValue.textEditCreateFlag.Text = value.OptionCreate.Value.ToString();

            controlExchangeValue.checkedListBoxUpdateOptions.SetItemChecked(0, value.OptionUpdate.AllowUpdate);
            controlExchangeValue.checkedListBoxUpdateOptions.SetItemChecked(1, value.OptionUpdate.UpdateGuid);
            controlExchangeValue.textEditUpdateFlag.Text = value.OptionUpdate.Value.ToString();

            //Резервное копирование коллекции
            List<ExchangeValueItem<int>> BackupValuesList = new List<ExchangeValueItem<int>>(value.Values);
            List<ExchangeValueItem<int>> BackupGroupsList = new List<ExchangeValueItem<int>>(value.Groups);

            BindingSource BindingValues = new BindingSource { DataSource = value.Values };
            controlExchangeValue.gridValues.DataSource = BindingValues;
            BindingSource BindingGroups = new BindingSource { DataSource = value.Groups };
            controlExchangeValue.gridGroups.DataSource = BindingGroups;
            #endregion

            #region События
            controlExchangeValue.simpleButtonSelect.Click += delegate
            {
                switch (controlExchangeValue.comboBoxCode.GetColumnValue("Name").ToString())
                {
                    case "Analitic.Analitics":
                        ShowBrowseDialog<Analitic>(wa, value);
                        break;
                    case "Contractor.Agents":
                        ShowBrowseDialog<Agent>(wa, value);
                        break;
                    case "Product.Products":
                        ShowBrowseDialog<Product>(wa, value);
                        break;
                    default:
                        XtraMessageBox.Show("Выбор данной таблицы пока не предусмотрен", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
                controlExchangeValue.gridValues.RefreshDataSource();
                controlExchangeValue.gridGroups.RefreshDataSource();
            };

            controlExchangeValue.simpleButtonDelete.Click += delegate
            {
                LastFocusedView.DeleteSelectedRows();
            };

            controlExchangeValue.simpleButtonClean.Click += delegate
            {
                value.Values.Clear();
                controlExchangeValue.ViewValues.RefreshData();
                value.Groups.Clear();
                controlExchangeValue.ViewGroups.RefreshData();
            };

            controlExchangeValue.checkedListBoxFindOptions.ItemCheck += delegate
            {
                FindOption fo = new FindOption
                                    {
                                        AllowId = controlExchangeValue.checkedListBoxFindOptions.GetItemChecked(0),
                                        AllowGuid = controlExchangeValue.checkedListBoxFindOptions.GetItemChecked(1),
                                        AllowCode = controlExchangeValue.checkedListBoxFindOptions.GetItemChecked(2),
                                        AllowName = controlExchangeValue.checkedListBoxFindOptions.GetItemChecked(3),
                                        AllowSourceId = controlExchangeValue.checkedListBoxFindOptions.GetItemChecked(4)
                                    };
                controlExchangeValue.textEditFindFlag.Text = fo.Value.ToString();
            };

            controlExchangeValue.checkedListBoxCreateOptions.ItemCheck += delegate
            {
                CreateOption co = new CreateOption
                                      {
                                          AllowCreate =
                                              controlExchangeValue.checkedListBoxCreateOptions.GetItemChecked(0),
                                          TryIdentity =
                                              controlExchangeValue.checkedListBoxCreateOptions.GetItemChecked(1)
                                      };
                controlExchangeValue.textEditCreateFlag.Text = co.Value.ToString();
            };

            controlExchangeValue.checkedListBoxUpdateOptions.ItemCheck += delegate
            {
                UpdateOption uo = new UpdateOption
                                      {
                                          AllowUpdate =
                                              controlExchangeValue.checkedListBoxUpdateOptions.GetItemChecked(0),
                                          UpdateGuid =
                                              controlExchangeValue.checkedListBoxUpdateOptions.GetItemChecked(1)
                                      };
                controlExchangeValue.textEditUpdateFlag.Text = uo.Value.ToString();
            };

            controlExchangeValue.gridValues.GotFocus += delegate
            {
                LastFocusedView = controlExchangeValue.ViewValues;
            };

            controlExchangeValue.gridGroups.GotFocus += delegate
            {
                LastFocusedView = controlExchangeValue.ViewGroups;
            };
            #endregion

            formExchangeValue.ShowDialog();

            if (formExchangeValue.DialogResult == DialogResult.OK)
            {
                //Сохранение результатов диалога
                value.Code = controlExchangeValue.comboBoxCode.GetColumnValue("Name").ToString();
                value.Flag = int.Parse(controlExchangeValue.textEditFlag.Text);
                value.OptionsFind.Value = int.Parse(controlExchangeValue.textEditFindFlag.Text);
                value.OptionCreate.Value = int.Parse(controlExchangeValue.textEditCreateFlag.Text);
                value.OptionUpdate.Value = int.Parse(controlExchangeValue.textEditUpdateFlag.Text);
            }
            else
            {
                value.Values = BackupValuesList;
                value.Groups = BackupGroupsList;
            }

            return formExchangeValue;
        }
        /// <summary>
        /// Показывает дилог выбора для объекта соответствующего типа и добавляет выбранные элемнты в списоки выбранных элементов и групп
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="wa">Рабочая область</param>
        /// <param name="List">Список</param>
        private static void ShowBrowseDialog<T>(Workarea wa, ExchangeValueList<int> Value) where T : class, IBase, new()
        {
            TreeListBrowser<T> browseDialog = new TreeListBrowser<T> { Workarea = wa, ShowCheckSingle = true, ShowCheckAll = true };

            //Устанавливаем изначальное положение флажков
            foreach (ExchangeValueItem<int> item in Value.Groups)
            {
                if (item.FullGroups)
                    browseDialog.CheckedRightNodeHierarchyId.Add(item.Value);
                else
                    browseDialog.CheckedLeftNodeHierarchyId.Add(item.Value);
            }

            browseDialog.ShowDialog();

            if (browseDialog.Form.DialogResult != DialogResult.OK) return;
            //Добавление элементов из списка
            //Value.Values.Clear();
            if (browseDialog.ListBrowserBaseObjects.SelectedValues != null)
            {
                foreach (ExchangeValueItem<int> exchangeValueItem in
                    from a in browseDialog.ListBrowserBaseObjects.SelectedValues
                    where Value.Values.FirstOrDefault(s => (s.Value == a.Id)) == null
                    select new ExchangeValueItem<int>
                               {
                                   Value = a.Id, Caption = a.Name, FullGroups = false
                               })
                {
                    Value.Values.Add(exchangeValueItem);
                }
            }

            //Добавление элементов из дерева
            Value.Groups.Clear();
            foreach (int i in browseDialog.CheckedRightNodeHierarchyId)
            {
                ExchangeValueItem<int> item = Value.Groups.FirstOrDefault(s => (s.Value == i));
                if (item == null)
                {
                    Hierarchy hierarchy = wa.Cashe.GetCasheData<Hierarchy>().Item(i);
                    ExchangeValueItem<int> exchangeValueItem = new ExchangeValueItem<int>
                                                                   {
                                                                       Value = hierarchy.Id,
                                                                       Caption = hierarchy.Name,
                                                                       FullGroups = true
                                                                   };
                    Value.Groups.Add(exchangeValueItem);
                }
                else
                {
                    item.FullGroups = true;
                }
            }
            foreach (int i in browseDialog.CheckedLeftNodeHierarchyId)
            {
                ExchangeValueItem<int> item = Value.Groups.FirstOrDefault(s => (s.Value == i));
                if (item == null)
                {
                    Hierarchy hierarchy = wa.Cashe.GetCasheData<Hierarchy>().Item(i);
                    ExchangeValueItem<int> exchangeValueItem = new ExchangeValueItem<int>
                                                                   {
                                                                       Value = hierarchy.Id,
                                                                       Caption = hierarchy.Name,
                                                                       FullGroups = false
                                                                   };
                    Value.Groups.Add(exchangeValueItem);
                }
                else
                {
                    item.FullGroups = false;
                }
            }
        }
    }
    /// <summary>
    /// Настройки обмена данными
    /// </summary>
    [Serializable]
    public class ExchangeSettings
    {
        #region private fields
        private List<ExchangeValueList<int>> _values;
        private List<Compliance<int>> _complianceId;
        private List<Compliance<string>> _complianceCode; 
        #endregion

        public const string IMPORT = "IMPORT";
        public const string EXPORT = "EXPORT";
        //public const string TYPENAMEWHELLKNOWN = "WHELLKNOWN";
        //public const string TYPENAMETABLE = "TABLE";
        /// <summary>
        /// Конструктор
        /// </summary>
        public ExchangeSettings()
        {
            _values = new List<ExchangeValueList<int>>();
            _complianceId = new List<Compliance<int>>();
            _complianceCode = new List<Compliance<string>>();
        }
        ///// <summary>
        ///// Получение коллекции имен таблиц, имеющих процедуру экспорта
        ///// </summary>
        ///// <returns></returns>
        //private static List<ComboboxItem> GetTableNames(Workarea WA)
        //{
        //    List<ComboboxItem> coll = TableInfo.GetCollection(WA).Where(s => !string.IsNullOrEmpty(s.ProcedureExport)).ToList().Select(ti => new ComboboxItem {Name = ti.Name, DisplayedName = ti.Memo}).ToList();

        //    coll.Sort((c1, c2) => c1.DisplayedName.CompareTo(c2.DisplayedName));

        //    return coll;
        //}
        
        /// <summary>
        /// Отображения диалога списка идентификаторов обмена данными
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="value">Список идентификаторов</param>
        /// <returns>Форма свойств</returns>

        public static ExchangeSettings ExportSystemData(Workarea workarea)
        {
            ExchangeSettings exSettings = new ExchangeSettings
                                              {
                                                  Name = "Экспорт системной информации",
                                                  Code = EXPORT,
                                                  Source =
                                                      string.Format("{0}.{1}", workarea.MyBranche.ServerName,
                                                                    workarea.MyBranche.DatabaseName)
                                              };

            // необходимо получить данные о всех системных записях базы данных и заполнить данные класса exSettings;
            const int systemFlag = FlagValue.FLAGSYSTEM;
            // папки
            List<Folder> folderCollection = workarea.GetCollection<Folder>().Where(s => (s.FlagsValue & systemFlag) == systemFlag).ToList();
            ExchangeValueList<int> folders = new ExchangeValueList<int> { Code = "Core.Folders" };
            foreach (Folder folder in folderCollection)
            {
                folders.Values.Add(new ExchangeValueItem<int>{Value = folder.Id});
            }
            if (folders.Values.Count>0)
                exSettings.Values.Add(folders);
            //

            return exSettings;
        }

        /// <summary>
        /// Загрузка настроек экспорта из строки
        /// </summary>
        /// <param name="str">Строка XML</param>
        /// <returns>Объхект настроек</returns>
        public static ExchangeSettings LoadFromString(string str)
        {
            using(StringReader sr=new StringReader(str))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ExchangeSettings));
                return xs.Deserialize(sr) as ExchangeSettings;
            }
        }
        /// <summary>
        /// Сохранение настроек экспорта в строку
        /// </summary>
        /// <param name="Settings"></param>
        /// <returns></returns>
        public static string SaveToString(ExchangeSettings Settings)
        {
            using(StringWriter sw = new StringWriter())
            {
                XmlSerializer xs = new XmlSerializer(typeof(ExchangeSettings));
                xs.Serialize(sw, Settings);
                return sw.ToString();
            }
        }

        /// <summary>
        /// Загрузка настроек экспорта из файла
        /// </summary>
        /// <param name="filename">Имя XML-файла</param>
        /// <returns>Загруженный объект</returns>
        public static ExchangeSettings LoadFromFile(string filename)
        {
            using (TextReader tr = new StreamReader(filename))
            {
                return LoadFromString(tr.ReadToEnd());
            }
        }

        /// <summary>
        /// Сохранение настроек экспорта в файл
        /// </summary>
        /// <param name="filename">Имя XML-файла</param>
        /// <param name="Settings">Загружаемый объект</param>
        public static void SaveToFile(string filename, ExchangeSettings Settings)
        {
            using (TextWriter tw = new StreamWriter(filename))
            {
                tw.Write(SaveToString(Settings));
                tw.Close();
            }
        }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// Тип настройки (импорт, экспорт)
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Дополнительный тип настройки
        /// </summary>
        /// <remarks>
        /// Экспорт системной информации - SYSTEM
        /// <para>Экспорт шаблонов документов - DOCTEMPLATES</para>
        /// <para>Экспорт библиотек - LIBARY</para>
        /// </remarks>
        public string Kind { get; set; }
        /// <summary>
        /// База источник
        /// </summary>
        /// <remarks></remarks>
        public string Source { get; set; }
        /// <summary>
        /// База назначений
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// Хранимая процедура для обмена данными
        /// </summary>
        public string StoredProcedure { get; set; }
        /// <summary>
        /// Направление экеспорта (0-память, 1-файл XML, 2-файл SQL Compact, 3- файл Microsoft Access)
        /// </summary>
        public int ExportTo { get; set; }
        /// <summary>
        /// Путь сохранения
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Сжатие даных
        /// </summary>
        public int Compression { get; set; }
        /// <summary>
        /// Коллекция наборов значений идентификаторов для обмена
        /// </summary>
        public List<ExchangeValueList<int>> Values
        {
            get { return _values; }
            set { _values = value; }
        }


        /// <summary>
        /// Таблица соответствия идентификаторов
        /// </summary>
        public List<Compliance<int>> ComplianceId
        {
            get { return _complianceId; }
            set { _complianceId = value; }
        }

        /// <summary>
        /// Таблица соответствия кодов
        /// </summary>
        public List<Compliance<string>> ComplianceCode
        {
            get { return _complianceCode; }
            set { _complianceCode = value; }
        }
    }

    public class ComboboxItem
    {
        public string Name { get; set; }
        public string DisplayedName { get; set; }
    }
}