using System;
using System.Drawing;
using System.Windows.Forms;
using BusinessObjects.Windows.Wizard;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства иерархии
        /// </summary>
        /// <param name="item">Иерархия</param>
        /// <returns></returns>
        public static Form ShowProperty(this Hierarchy item)
        {
            InternalShowPropertyBase<Hierarchy> showPropertyBase = new InternalShowPropertyBase<Hierarchy>
                                                                       {
                                                                           SelectedItem = item,
                                                                           ControlBuilder =
                                                                               new BuildControlHierarchy
                                                                                   {
                                                                                       SelectedItem = item
                                                                                   }
                                                                       };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        public static Hierarchy BrowseTree<T>(this Hierarchy value, T item, string rootCode=null) where T : class, IBase, new()
        {
            FormProperties frm = new FormProperties
                                     {
                                         ribbon = {ApplicationIcon = value.GetImage()},
                                         btnSave = {Visibility = BarItemVisibility.Never},
                                         btnSelect =
                                             {
                                                 Visibility = BarItemVisibility.Always,
                                                 Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.SELECT_X32)
                                             }
                                     };
            frm.MinimumSize = new System.Drawing.Size(600, 480);
            TreeBrowser<T> treeBrowser = new TreeBrowser<T>(value.Workarea)
                                  {
                                      StartValue = item,
                                      ShowContentTree = false,
                                      RootCode = rootCode
                                  };
                treeBrowser.Build();
                
                if (treeBrowser.ControlTree.Tree.Nodes.Count > 0)
                    treeBrowser.ControlTree.Tree.Nodes.FirstNode.Expanded = true;
                frm.clientPanel.Controls.Add(treeBrowser.ControlTree);

                treeBrowser.ControlTree.Dock = DockStyle.Fill;
            Hierarchy retValue=null;
            frm.btnSelect.ItemClick += delegate
                                           {
                                               frm.Close();
                                               retValue = treeBrowser.SelectedHierarchy;
                                           };
            frm.ShowDialog();
            return retValue;
        }
        /// <summary>
        /// Показать диалог создания ерархии
        /// </summary>
        /// <param name="value">Иерархия</param>
        /// <returns></returns>
        public static Hierarchy ShowCreateWizard(this Hierarchy value)
        {
            Hierarchy newItem = new Hierarchy {Workarea = value.Workarea, KindId=Hierarchy.KINDID_GROUP};
            FormHierarchyWizardCreate wizard = new FormHierarchyWizardCreate();
            wizard.ShowInTaskbar = false;
            wizard.Icon = Icon.FromHandle(ResourceImage.GetByCode(value.Workarea, ResourceImage.FOLDER_X16).GetHicon());
            BindingSource bindingEntities = new BindingSource();
            bindingEntities.DataSource = value.Workarea.CollectionEntities;
            DataGridViewHelper.GenerateGridColumns(value.Workarea, wizard.View, "DEFAULT_LOOKUP_NAME");
            wizard.Grid.DataSource = bindingEntities;
            wizard.wizardPage1.PageValidating +=
                delegate(object sender, DevExpress.XtraWizard.WizardPageValidatingEventArgs e)
                    {
                        if(string.IsNullOrEmpty(wizard.txtName.Text))
                        {
                            if(e.Direction == DevExpress.XtraWizard.Direction.Forward)
                                e.Valid = false;
                        }
                    };
            wizard.wizardControl.FinishClick += delegate(object sender, System.ComponentModel.CancelEventArgs ef)
                                                    {
                                                        if(bindingEntities.Current!=null)
                                                        {
                                                            newItem.ContentEntityId =
                                                                (short) (bindingEntities.Current as EntityType).Id;
                                                            newItem.Name = wizard.txtName.Text;
                                                            newItem.Code = wizard.txtCode.Text;
                                                            newItem.Memo = wizard.txtMemo.Text;
                                                            newItem.StateId = State.STATEACTIVE;
                                                            newItem.FlagsValue = FlagValue.FLAGSYSTEM;
                                                            try
                                                            {
                                                                newItem.Save();
                                                            }
                                                            catch (DatabaseException dbe)
                                                            {
                                                                Extentions.ShowMessageDatabaseExeption(value.Workarea,
                                                                    value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                    value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                                                                ef.Cancel = true;
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Extentions.ShowMessagesExeption(value.Workarea,
                                                                                                value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                                value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049),
                                                                                                ex);
                                                                ef.Cancel = true;
                                                            }
                                                        }
                                                    };
            wizard.ShowDialog();
            return newItem;
        }

        

        
    }
}
