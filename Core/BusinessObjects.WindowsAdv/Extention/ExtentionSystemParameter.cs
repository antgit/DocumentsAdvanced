using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraBars;
using BusinessObjects.Windows.Controls;
using BusinessObjects.Security;
namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства системного параметра
        /// </summary>
        /// <param name="item">Системный параметр</param>
        /// <returns></returns>
        public static Form ShowProperty(this SystemParameter item)
        {
            InternalShowPropertyBase<SystemParameter> showPropertyBase = new InternalShowPropertyBase<SystemParameter>
                                                                             {
                                                                                 SelectedItem = item,
                                                                                 ControlBuilder =
                                                                                     new BuildControlSystemParameter
                                                                                         {
                                                                                             SelectedItem = item
                                                                                         }
                                                                             };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список системных параметров
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static SystemParameter BrowseList(this SystemParameter item)
        {
            List<SystemParameter> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показатьб список системных параметров
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<SystemParameter> BrowseList(this SystemParameter item, Predicate<SystemParameter> filter, List<SystemParameter> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<SystemParameter> BrowseMultyList(this SystemParameter item, Workarea wa, Predicate<SystemParameter> filter, List<SystemParameter> sourceCollection, bool allowMultySelect)
        {
            List<SystemParameter> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = wa.Empty<SystemParameter>().Entity.GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ListBrowserBaseObjects<SystemParameter> browserBaseObjects = new ListBrowserBaseObjects<SystemParameter>(wa, sourceCollection, filter, item, true, false, false, true)
                                                                             {Owner = frm};
            browserBaseObjects.Build();

            new FormStateMaintainer(frm, String.Format("Browse{0}", item.GetType().Name));
            frm.clientPanel.Controls.Add(browserBaseObjects.ListControl);
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnProp.Visibility = BarItemVisibility.Always;
            frm.btnCreate.Visibility = BarItemVisibility.Always;
            frm.btnDelete.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Visibility = BarItemVisibility.Always;

            frm.btnDelete.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.DELETE_X32);
            frm.btnSelect.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.SELECT_X32);

            frm.btnProp.ItemClick += delegate
            {
                browserBaseObjects.InvokeProperties();
            };
            frm.btnDelete.ItemClick += delegate
            {
                browserBaseObjects.InvokeDelete();
            };
            browserBaseObjects.ListControl.Dock = DockStyle.Fill;
            browserBaseObjects.ShowProperty += delegate(SystemParameter obj)
            {
                obj.ShowProperty();
                if (obj.IsNew)
                {
                    obj.Created += delegate
                    {
                        int position = browserBaseObjects.BindingSource.Add(obj);
                        browserBaseObjects.BindingSource.Position = position;
                    };
                }
            };

            browserBaseObjects.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.Alt)
                {
                    returnValue = browserBaseObjects.SelectedValues;
                    frm.Close();
                }
            };
            frm.btnSelect.ItemClick += delegate
            {
                returnValue = browserBaseObjects.SelectedValues;
                frm.Close();
            };
            frm.btnClose.ItemClick += delegate
            {
                returnValue = null;
                frm.Close();

            };
            frm.ShowDialog();

            return returnValue;
        }

        /// <summary>
        /// Открывает диалог просмотра и изменения пользовательского параметра
        /// </summary>
        /// <param name="value"></param>
        public static Form BrowseProperties(this SystemParameterUser value)
        {
            ControlSystemParameterUser mainControl = new ControlSystemParameterUser();
            FormProperties frm = new FormProperties
                                     {
                                         ribbon =
                                             {
                                                 ApplicationIcon =
                                                     ResourceImage.GetSystemImage(ResourceImage.PROPERTIES_X16)
                                             },
                                         btnSave = {Visibility = BarItemVisibility.Always},
                                         btnSaveClose = {Visibility = BarItemVisibility.Always},
                                         btnRefresh = {Visibility = BarItemVisibility.Never},
                                         Width = 650,
                                         Height = 380,
                                         FormBorderStyle = FormBorderStyle.FixedDialog,
                                         Text = "Свойства: Список пользовательских параметров"
                                     };

            mainControl.txtString.Text = value.ValueString;
            mainControl.numInt.EditValue = value.ValueInt;
            mainControl.numFloat.EditValue = value.ValueFloat;
            mainControl.numMoney.EditValue = value.ValueMoney;
            mainControl.txtGuid.Text = value.ValueGuid.HasValue ? value.ValueGuid.Value.ToString() : String.Empty;

            BindingSource bindingSourceUsers = new BindingSource
                                                   {
                                                       DataSource =
                                                           value.Workarea.GetCollection<Uid>().Where(
                                                               u =>
                                                               u.KindValue == 1 &&
                                                               !value.Owner.GetUserParams().Exists(
                                                                   u2 => u2.UserId == u.Id && u2.UserId != value.UserId))
                                                   };
            mainControl.cmbUser.Properties.DataSource = bindingSourceUsers;
            mainControl.cmbUser.Properties.DisplayMember = GlobalPropertyNames.Name;
            mainControl.cmbUser.Properties.ValueMember = GlobalPropertyNames.Id;
            mainControl.cmbUser.EditValue = value.UserId;
            DataGridViewHelper.GenerateLookUpColumns(value.Workarea, mainControl.cmbUser,
                                                     "DEFAULT_LOOKUP_NAME");

            BindingSource bindingSourceReferenceKind = new BindingSource
                                                           {
                                                               DataSource = value.Workarea.CollectionEntities
                                                           };
            mainControl.cmbReferenceKind.Properties.DataSource = bindingSourceReferenceKind;
            mainControl.cmbReferenceKind.Properties.DisplayMember = GlobalPropertyNames.Name;
            mainControl.cmbReferenceKind.Properties.ValueMember = GlobalPropertyNames.Id;
            mainControl.cmbReferenceKind.EditValue = value.EntityReferenceId;
            DataGridViewHelper.GenerateLookUpColumns(value.Workarea, mainControl.cmbReferenceKind,
                                                     "DEFAULT_LOOKUPENTITYTYPE");

            mainControl.cmbReference.Properties.DisplayMember = GlobalPropertyNames.Name;
            mainControl.cmbReference.Properties.ValueMember = GlobalPropertyNames.Id;
            BindingSource bindingSourceReference = new BindingSource();
            if (value.EntityReferenceId == 0)
            {
                mainControl.cmbReference.Properties.DataSource = bindingSourceReference;
            }
            if (value.EntityReferenceId == 21)
            {
                mainControl.cmbReference.Properties.Columns.Clear();
                DataGridViewHelper.GenerateLookUpColumns(value.Workarea, mainControl.cmbReference,
                                                         "DEFAULT_LOOKUPCUSTOMVIEWLIST");
                bindingSourceReference.DataSource = value.Workarea.GetCollection<CustomViewList>();
                mainControl.cmbReference.Properties.DataSource = bindingSourceReference;
            }
            mainControl.cmbReference.EditValue = value.ReferenceId;
            mainControl.cmbReference.EditValueChanged += delegate
            {
                bindingSourceReference = new BindingSource();
                if ((int)mainControl.cmbReference.EditValue == 0)
                {
                    mainControl.cmbReference.Properties.DataSource = bindingSourceReference;
                }
                if ((int)mainControl.cmbReference.EditValue == 21)
                {
                    mainControl.cmbReference.Properties.Columns.Clear();
                    DataGridViewHelper.GenerateLookUpColumns(value.Workarea, mainControl.cmbReference,
                                                             "DEFAULT_LOOKUPCUSTOMVIEWLIST");
                    bindingSourceReference.DataSource = value.Workarea.GetCollection<CustomViewList>();
                    mainControl.cmbReference.Properties.DataSource = bindingSourceReference;
                }
                mainControl.cmbReference.EditValue = value.ReferenceId;
            };

            #region Выйти
            frm.btnClose.ItemClick += delegate
            {
                frm.Close();
            };
            #endregion

            #region Сохранить
            frm.btnSave.ItemClick += delegate
            {
                value.ValueString = mainControl.txtString.Text;
                value.ValueInt = (int?)mainControl.numInt.EditValue;
                value.ValueFloat = (float?)mainControl.numFloat.EditValue;
                value.ValueMoney = (decimal?)mainControl.numMoney.EditValue;
                if (mainControl.txtGuid.Text.Length > 0)
                    value.ValueGuid = new Guid(mainControl.txtGuid.Text);
                else
                    value.ValueGuid = null;
                value.UserId = (int)mainControl.cmbUser.EditValue;
                value.EntityReferenceId = (int)mainControl.cmbReferenceKind.EditValue;
                value.ReferenceId = (int)mainControl.cmbReference.EditValue;
                value.Save();
            };
            #endregion

            #region Сохранить и выйти
            frm.btnSaveClose.ItemClick += delegate
            {
                value.ValueString = mainControl.txtString.Text;
                value.ValueInt = (int?)mainControl.numInt.EditValue;
                value.ValueFloat = (float?)mainControl.numFloat.EditValue;
                value.ValueMoney = (decimal?)mainControl.numMoney.EditValue;
                if (mainControl.txtGuid.Text.Length > 0)
                    value.ValueGuid = new Guid(mainControl.txtGuid.Text);
                else
                    value.ValueGuid = null;
                value.UserId = (int)mainControl.cmbUser.EditValue;
                value.EntityReferenceId = (int)mainControl.cmbReferenceKind.EditValue;
                value.ReferenceId = (int)mainControl.cmbReference.EditValue;
                value.Save();
                frm.Close();
            };
            #endregion

            frm.clientPanel.Controls.Add(mainControl);
            mainControl.Dock = DockStyle.Fill;
            frm.Show();
            return frm;
        }



        public static List<SystemParameter> BrowseContent(this SystemParameter item, Workarea wa = null)
        {
            ContentModuleSystemParameter module = new ContentModuleSystemParameter();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
    }
}
