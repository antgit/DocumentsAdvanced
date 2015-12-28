using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства разрешения
        /// </summary>
        /// <param name="item">Разрешение</param>
        /// <returns></returns>
        public static Form ShowProperty(this Right item)
        {
            InternalShowPropertyBase<Right> showPropertyBase = new InternalShowPropertyBase<Right>
                                                                   {
                                                                       SelectedItem = item,
                                                                       ControlBuilder =
                                                                           new BuildControlRight
                                                                               {
                                                                                   SelectedItem = item
                                                                               }
                                                                   };
            return showPropertyBase.ShowDialog();
        }
        public static Form ShowProperty(this UserRightElement item)
        {
            InternalShowPropertyCore<UserRightElement> showPropertyBase = new InternalShowPropertyCore<UserRightElement>
                                                                              {
                                                                                  SelectedItem = item,
                                                                                  ControlBuilder =
                                                                                      new BuildControlUserRightElement
                                                                                          {
                                                                                              SelectedItem = item
                                                                                          }
                                                                              };
            return showPropertyBase.ShowDialog();
        }
        public static Form ShowProperty(this UserRightCommon item)
        {
            InternalShowPropertyCore<UserRightCommon> showPropertyBase = new InternalShowPropertyCore<UserRightCommon>
                                                                             {
                                                                                 SelectedItem = item,
                                                                                 ControlBuilder =
                                                                                     new BuildControlUserRightCommon
                                                                                         {
                                                                                             SelectedItem = item
                                                                                         }
                                                                             };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        #region Списки
        /// <summary>
        /// Список разрешения
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static Right BrowseList(this Right item, Predicate<Right> filter)
        {
            List<Right> col = BrowseMultyList(item, item.Workarea, filter, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        public static Right BrowseList(this Right item, Predicate<Right> filter, List<Right> sourceCollection)
        {
            List<Right> col = BrowseMultyList(item, item.Workarea, filter, sourceCollection, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        public static List<Right> BrowseListMulty(this Right item, Predicate<Right> filter, List<Right> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<Right> BrowseMultyList(this Right item, Workarea wa, Predicate<Right> filter, List<Right> sourceCollection, bool allowMultySelect)
        {
            List<Right> returnValue = null;
            FormProperties frm = new FormProperties();
            frm.Ribbon.ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.KEY_X16);
            frm.Icon = Icon.FromHandle(ResourceImage.GetSystemImage(ResourceImage.KEY_X16).GetHicon());
            ListBrowserBaseObjects<Right> browserBaseObjects = new ListBrowserBaseObjects<Right>(wa, sourceCollection, filter, item, true, false, false, true);
            browserBaseObjects.Owner = frm;
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
            browserBaseObjects.ShowProperty += delegate(Right obj)
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
        
        #endregion
        #region Разрешения
        public static void BrowseElemenRight<T>(this T element, short kindOfRight) where T : class, IBase, new()
        {
            FormProperties frm = new FormProperties();
            frm.Ribbon.ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.KEY_X16);
            frm.ShowInTaskbar = false;
            frm.MinimumSize = new Size(400, 400);
            new FormStateMaintainer(frm, String.Format("Property{0}", "_BrowseElemenRight"));
            frm.Text = String.Format("Разрешения: {0}", element.Name);
            ControlTreeList mainControl = new ControlTreeList();
            ControlList listUserGroups = new ControlList();
            mainControl.SplitContainerControl.Panel1.Controls.Add(listUserGroups);
            listUserGroups.Dock = DockStyle.Fill;
            DataGridViewHelper.GenerateGridColumns(element.Workarea, listUserGroups.View, "DEFAULT_LISTVIEWUID");
            List<Uid> collUsers = element.Workarea.Access.GetUserGroupsRightForElement(element.Id, element.EntityId);
            BindingSource bindListUsers = new BindingSource();
            bindListUsers.DataSource = collUsers;
            listUserGroups.Grid.DataSource = bindListUsers;

            ControlList listRights = new ControlList();
            mainControl.SplitContainerControl.Panel2.Controls.Add(listRights);
            listRights.Dock = DockStyle.Fill;

            DataGridViewHelper.GenerateGridColumns(element.Workarea, listRights.View, "DEFAULT_LISTVIEWUSERRIGHTELEMENT");
            List<UserRightElement> collRights = element.Workarea.Access.GetCollectionUserRightsElements(1, 0, element.Id, element.EntityId);
            BindingSource bindRights = new BindingSource();
            bindRights.DataSource = collRights;
            // Построение группы упраления связями
            RibbonPage page = frm.Ribbon.Pages[ExtentionString.GetPageNameByKey(element.Workarea, ExtentionString.CONTROL_COMMON_NAME)];
            RibbonPageGroup groupLinksAction = new RibbonPageGroup();
            groupLinksAction.Text = "Пользователи и группы";
            #region добавить
            BarButtonItem btnCreate = new BarButtonItem();
            btnCreate.ButtonStyle = BarButtonStyle.Default;
            btnCreate.Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049);
            btnCreate.RibbonStyle = RibbonItemStyles.Large;
            btnCreate.Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32); 
            groupLinksAction.ItemLinks.Add(btnCreate);
            btnCreate.ItemClick += delegate
                                        {
                                            List<Uid> collCurrentUsers = bindListUsers.DataSource as List<Uid>;
                                            List<Uid> selNewUser = element.Workarea.Empty<Uid>().BrowseMultyList(element.Workarea, s=>!collCurrentUsers.Exists(f=>f.Id==s.Id), null, true);
                                            if (selNewUser != null && selNewUser.Count > 0)
                                            {
                                                foreach (Uid userId in selNewUser)
                                                {
                                                    bindListUsers.Add(userId);
                                                }
                                            }
                                            //Uid userId = element.Workarea.Empty<Uid>().BrowseList();
                                            //if(userId!=null)
                                            //{
                                                //bindListUsers.Add(userId);
                                                //UserRightElement right = new UserRightElement();
                                                //right.Workarea = element.Workarea;
                                                //right.ElementId = element.Id;
                                                //right.DbUidId = userId.Id;
                                                //right.DbEntityId = element.EntityId;
                                                //bindRights.Add(right);
                                            //}
                                        };
            #endregion
            #region Удаление
            BarButtonItem btnDelete = new BarButtonItem
                                          {
                                              Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EXCLUDE, 1049),
                                              RibbonStyle = RibbonItemStyles.Large,
                                              Glyph = ResourceImage.GetByCode(element.Workarea, ResourceImage.DELETE_X32)
                                          };
            groupLinksAction.ItemLinks.Add(btnDelete);
            btnDelete.ItemClick += delegate
            {
                foreach (UserRightElement right in collRights)
                {
                    right.Delete();
                }
                bindListUsers.RemoveCurrent();
            };
            #endregion
            page.Groups.Add(groupLinksAction);
            // вторая таблица

            listRights.Grid.DataSource = bindRights;
            var d = new EventHandler(delegate
                              {
                                  int currentUidId = 0;
                                  if (bindListUsers.Current == null)
                                      currentUidId = 0;
                                  else
                                      currentUidId = (bindListUsers.Current as Uid).Id;
                                  collRights = element.Workarea.Access.GetCollectionUserRightsElements(1769474, currentUidId, element.Id, element.EntityId);
                                  bindRights.DataSource = collRights;
                              });
            bindListUsers.CurrentChanged += d;
            bindListUsers.CurrencyManager.Position = -1;
            d.Invoke(null, null);
            RibbonPageGroup groupLinksRights = new RibbonPageGroup();
            groupLinksRights.Text = "Разрешения";
            #region Добавить
            BarButtonItem btnCreateAcl = new BarButtonItem();
            btnCreateAcl.ButtonStyle = BarButtonStyle.Default;
            btnCreateAcl.Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049);
            btnCreateAcl.RibbonStyle = RibbonItemStyles.Large;
            btnCreateAcl.Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32); 
            groupLinksRights.ItemLinks.Add(btnCreateAcl);
            btnCreateAcl.ItemClick += delegate
            {
                if (bindListUsers.Current == null) return;
                Uid currentUidId = (bindListUsers.Current as Uid);

                List<Right> coll = element.Workarea.GetCollection<Right>().Where(s => s.KindValue == 2).ToList();
                var ccList = from c in coll join a in collRights on c.Id equals a.RightId into j from x in j.DefaultIfEmpty() 
                             where x==null select c;
                List<Right> browseSel = element.Workarea.Empty<Right>().BrowseListMulty(null, ccList.ToList());
                if (browseSel != null && browseSel.Count > 0)
                {
                    foreach (Right selectedRight in browseSel)
                    {
                        UserRightElement right = new UserRightElement();
                        right.Workarea = element.Workarea;
                        right.ElementId = element.Id;
                        right.DbUidId = currentUidId.Id;
                        right.DbEntityId = element.EntityId;
                        right.RightId = selectedRight.Id;
                        right.StateId = State.STATEACTIVE;
                        right.Value = 1;
                        right.Save();
                        bindRights.Add(right);
                    }
                }
                //Right selectedRight = element.Workarea.Empty<Right>().BrowseList(null, ccList.ToList());
                //if(selectedRight!=null)
                //{
                //    UserRightElement right = new UserRightElement();
                //    right.Workarea = element.Workarea;
                //    right.StateId = State.STATEACTIVE;
                //    right.ElementId = element.Id;
                //    right.DbUidId = currentUidId.Id;
                //    right.DbEntityId = element.EntityId;
                //    right.RightId = selectedRight.Id;
                //    right.Value = 1;
                //    right.Save();
                //    bindRights.Add(right);    
                //}
                
            };
            #endregion
            #region Изменить
            BarButtonItem btnPropAcl = new BarButtonItem
                                           {
                                               Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                                               RibbonStyle = RibbonItemStyles.Large,
                                               Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                           };
            groupLinksRights.ItemLinks.Add(btnPropAcl);
            btnPropAcl.ItemClick += delegate
            {
                if (bindRights.Current == null) return;
                UserRightElement right = bindRights.Current as UserRightElement;
                right.ShowProperty();
            };
            #endregion
            #region Удаление
            BarButtonItem btnDeleteAcl = new BarButtonItem
                                             {
                                                 Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EXCLUDE, 1049),
                                                 RibbonStyle = RibbonItemStyles.Large,
                                                 Glyph = ResourceImage.GetByCode(element.Workarea, ResourceImage.DELETE_X32)
                                             };
            groupLinksRights.ItemLinks.Add(btnDeleteAcl);
            btnDeleteAcl.ItemClick += delegate
            {
                if (bindRights.Current == null) return;
                UserRightElement right = bindRights.Current as UserRightElement;
                right.Delete();
                bindRights.RemoveCurrent();
            };
            #endregion
            page.Groups.Add(groupLinksRights);

            listUserGroups.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                                                               {
                                                                   if (e.Column.FieldName == "Image" && e.IsGetData)
                                                                   {
                                                                       Uid imageItem = bindListUsers[e.ListSourceRowIndex] as Uid;
                                                                       if (imageItem != null)
                                                                       {
                                                                           e.Value = imageItem.GetImage();
                                                                       }
                                                                   }
                                                                   else if (e.Column.Name == "colStateImage")
                                                                   {
                                                                       Uid imageItem = bindListUsers[e.ListSourceRowIndex] as Uid;
                                                                       if (imageItem != null)
                                                                       {
                                                                           e.Value = imageItem.State.GetImage();
                                                                       }
                                                                   }
                                                               };

            listRights.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.FieldName == "Image" && e.IsGetData)
                {
                    e.Value = ResourceImage.GetSystemImage(ResourceImage.KEY_X16);
                }
            };
            frm.clientPanel.Controls.Add(mainControl);
            mainControl.Dock = DockStyle.Fill;
            frm.ShowDialog();
        }

        public static void BrowseModuleRights(this Library element)
        {
            FormProperties frm = new FormProperties();
            frm.Ribbon.ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.KEY_X16);
            frm.ShowInTaskbar = false;
            frm.MinimumSize = new Size(400, 400);
            new FormStateMaintainer(frm, String.Format("Property{0}", "_BrowseElemenRight"));
            frm.Text = String.Format("Разрешения: {0}", element.Name);
            ControlTreeList mainControl = new ControlTreeList();
            ControlList listUserGroups = new ControlList();
            mainControl.SplitContainerControl.Panel1.Controls.Add(listUserGroups);
            listUserGroups.Dock = DockStyle.Fill;
            DataGridViewHelper.GenerateGridColumns(element.Workarea, listUserGroups.View, "DEFAULT_LISTVIEWUID");
            List<Uid> collUsers = element.Workarea.Access.GetUserGroupsRightForElement(element.Id, element.EntityId, rightKindId:1769476);
            BindingSource bindListUsers = new BindingSource();
            bindListUsers.DataSource = collUsers;
            listUserGroups.Grid.DataSource = bindListUsers;

            ControlList listRights = new ControlList();
            mainControl.SplitContainerControl.Panel2.Controls.Add(listRights);
            listRights.Dock = DockStyle.Fill;

            DataGridViewHelper.GenerateGridColumns(element.Workarea, listRights.View, "DEFAULT_LISTVIEWUSERRIGHTELEMENT");
            List<UserRightElement> collRights = element.Workarea.Access.GetCollectionUserRightsElements(1, 0, element.Id, element.EntityId);
            //List<UserRightElement> collRights = element.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("CONTRACT_KIND").GetTypeContents<UserRightElement>();
            BindingSource bindRights = new BindingSource();
            bindRights.DataSource = collRights;
            // Построение группы упраления связями
            RibbonPage page = frm.Ribbon.Pages[ExtentionString.GetPageNameByKey(element.Workarea, ExtentionString.CONTROL_COMMON_NAME)];
            RibbonPageGroup groupLinksAction = new RibbonPageGroup();
            groupLinksAction.Text = "Пользователи и группы";
            #region добавить
            BarButtonItem btnCreate = new BarButtonItem();
            btnCreate.ButtonStyle = BarButtonStyle.Default;
            btnCreate.Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049);
            btnCreate.RibbonStyle = RibbonItemStyles.Large;
            btnCreate.Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32);
            groupLinksAction.ItemLinks.Add(btnCreate);
            btnCreate.ItemClick += delegate
                                       {
                                            List<Uid> collCurrentUsers = bindListUsers.DataSource as List<Uid>;
                                            List<Uid> selNewUser = element.Workarea.Empty<Uid>().BrowseMultyList(element.Workarea, s=>!collCurrentUsers.Exists(f=>f.Id==s.Id), null, true);
                                            if (selNewUser != null && selNewUser.Count>0)
                                            {
                                                foreach (Uid userId in selNewUser)
                                                {
                                                    bindListUsers.Add(userId);    
                                                }
                                                //UserRightElement right = new UserRightElement();
                                                //right.Workarea = element.Workarea;
                                                //right.ElementId = element.Id;
                                                //right.DbUidId = userId.Id;
                                                //right.DbEntityId = element.EntityId;
                                                //bindRights.Add(right);
                                            }
            };
            #endregion
            #region Удаление
            BarButtonItem btnDelete = new BarButtonItem
            {
                Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EXCLUDE, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(element.Workarea, ResourceImage.DELETE_X32)
            };
            groupLinksAction.ItemLinks.Add(btnDelete);
            btnDelete.ItemClick += delegate
            {
                foreach (UserRightElement right in collRights)
                {
                    right.Delete();
                }
                bindListUsers.RemoveCurrent();
            };
            #endregion
            page.Groups.Add(groupLinksAction);
            // вторая таблица

            listRights.Grid.DataSource = bindRights;
            var d = new EventHandler(delegate
            {
                int currentUidId = 0;
                if (bindListUsers.Current == null)
                    currentUidId = 0;
                else
                    currentUidId = (bindListUsers.Current as Uid).Id;
                collRights = element.Workarea.Access.GetCollectionUserRightsElements(1769476, currentUidId, element.Id, element.EntityId);
                bindRights.DataSource = collRights;
            });
            bindListUsers.CurrentChanged += d;
            bindListUsers.CurrencyManager.Position = -1;
            d.Invoke(null, null);
            RibbonPageGroup groupLinksRights = new RibbonPageGroup();
            groupLinksRights.Text = "Разрешения";
            #region Добавить
            BarButtonItem btnCreateAcl = new BarButtonItem();
            btnCreateAcl.ButtonStyle = BarButtonStyle.Default;
            btnCreateAcl.Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049);
            btnCreateAcl.RibbonStyle = RibbonItemStyles.Large;
            btnCreateAcl.Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32);
            groupLinksRights.ItemLinks.Add(btnCreateAcl);
            btnCreateAcl.ItemClick += delegate
            {
                if (bindListUsers.Current == null) return;
                Uid currentUidId = (bindListUsers.Current as Uid);
                Hierarchy rootDocKind = element.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SECURE_" + element.Code);
                if(rootDocKind==null || rootDocKind.Id==0)
                    rootDocKind = element.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SYSTEM_RIGHT_UI");
                List<Right> coll = rootDocKind==null? new List<Right>() : rootDocKind.GetTypeContents<Right>();

                //List<Right> coll = element.Workarea.GetCollection<Right>().Where(s => s.KindValue == 4).ToList();
                var ccList = from c in coll
                             join a in collRights on c.Id equals a.RightId into j
                             from x in j.DefaultIfEmpty()
                             where x == null
                             select c;
                List<Right> browseSel = element.Workarea.Empty<Right>().BrowseListMulty(null, ccList.ToList());
                if(browseSel!=null && browseSel.Count>0)
                {
                    foreach (Right selectedRight in browseSel)
                    {
                        UserRightElement right = new UserRightElement();
                        right.Workarea = element.Workarea;
                        right.ElementId = element.Id;
                        right.DbUidId = currentUidId.Id;
                        right.DbEntityId = element.EntityId;
                        right.RightId = selectedRight.Id;
                        right.StateId = State.STATEACTIVE;
                        right.Value = 1;
                        right.Save();
                        bindRights.Add(right);
                    }
                }
                //Right selectedRight = element.Workarea.Empty<Right>().BrowseList(null, ccList.ToList());
                //if (selectedRight != null)
                //{
                //    UserRightElement right = new UserRightElement();
                //    right.Workarea = element.Workarea;
                //    right.ElementId = element.Id;
                //    right.DbUidId = currentUidId.Id;
                //    right.DbEntityId = element.EntityId;
                //    right.RightId = selectedRight.Id;
                //    right.StateId = State.STATEACTIVE;
                //    right.Value = 1;
                //    right.Save();
                //    bindRights.Add(right);
                //}

            };
            #endregion
            #region Изменить
            BarButtonItem btnPropAcl = new BarButtonItem
            {
                Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
            };
            groupLinksRights.ItemLinks.Add(btnPropAcl);
            btnPropAcl.ItemClick += delegate
            {
                if (bindRights.Current == null) return;
                UserRightElement right = bindRights.Current as UserRightElement;
                right.ShowProperty();
            };
            #endregion
            #region Удаление
            BarButtonItem btnDeleteAcl = new BarButtonItem
            {
                Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EXCLUDE, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(element.Workarea, ResourceImage.DELETE_X32)
            };
            groupLinksRights.ItemLinks.Add(btnDeleteAcl);
            btnDeleteAcl.ItemClick += delegate
            {
                if (bindRights.Current == null) return;
                UserRightElement right = bindRights.Current as UserRightElement;
                right.Delete();
                bindRights.RemoveCurrent();
            };
            #endregion
            page.Groups.Add(groupLinksRights);

            listUserGroups.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.FieldName == "Image" && e.IsGetData)
                {
                    Uid imageItem = bindListUsers[e.ListSourceRowIndex] as Uid;
                    if (imageItem != null)
                    {
                        e.Value = imageItem.GetImage();
                    }
                }
                else if (e.Column.Name == "colStateImage")
                {
                    Uid imageItem = bindListUsers[e.ListSourceRowIndex] as Uid;
                    if (imageItem != null)
                    {
                        e.Value = imageItem.State.GetImage();
                    }
                }
            };

            listRights.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.FieldName == "Image" && e.IsGetData)
                {
                    e.Value = ResourceImage.GetSystemImage(ResourceImage.KEY_X16);
                }
            };
            frm.clientPanel.Controls.Add(mainControl);
            mainControl.Dock = DockStyle.Fill;
            frm.ShowDialog();
        }

        public static void BrowseDocumentRights(this Document element)
        {
            FormProperties frm = new FormProperties();
            frm.Ribbon.ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.KEY_X16);
            frm.ShowInTaskbar = false;
            frm.MinimumSize = new Size(400, 400);
            new FormStateMaintainer(frm, String.Format("Property{0}", "_BrowseElemenRight"));
            frm.Text = String.Format("Разрешения: {0}", element.Name);
            ControlTreeList mainControl = new ControlTreeList();
            ControlList listUserGroups = new ControlList();
            mainControl.SplitContainerControl.Panel1.Controls.Add(listUserGroups);
            listUserGroups.Dock = DockStyle.Fill;
            DataGridViewHelper.GenerateGridColumns(element.Workarea, listUserGroups.View, "DEFAULT_LISTVIEWUID");
            List<Uid> collUsers = (from u in element.Workarea.Access.GetAllUsers()
                                  join s in element.DocumentSecures() on u.Id equals s.UserIdTo
                                  select u).ToList();
                
            BindingSource bindListUsers = new BindingSource();
            bindListUsers.DataSource = collUsers;
            listUserGroups.Grid.DataSource = bindListUsers;

            ControlList listRights = new ControlList();
            mainControl.SplitContainerControl.Panel2.Controls.Add(listRights);
            listRights.Dock = DockStyle.Fill;

            DataGridViewHelper.GenerateGridColumns(element.Workarea, listRights.View, "DEFAULT_LISTVIEWUSERRIGHTELEMENT");
            //List<UserRightElement> collRights = element.Workarea.Access.GetCollectionUserRightsElements(1, 0, element.Id, element.EntityId);
            List<DocumentSecure> collRights = element.DocumentSecures();
            BindingSource bindRights = new BindingSource();
            bindRights.DataSource = collRights;
            // Построение группы упраления связями
            RibbonPage page = frm.Ribbon.Pages[ExtentionString.GetPageNameByKey(element.Workarea, ExtentionString.CONTROL_COMMON_NAME)];
            RibbonPageGroup groupLinksAction = new RibbonPageGroup();
            groupLinksAction.Text = "Пользователи и группы";
            #region добавить
            BarButtonItem btnCreate = new BarButtonItem();
            btnCreate.ButtonStyle = BarButtonStyle.Default;
            btnCreate.Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049);
            btnCreate.RibbonStyle = RibbonItemStyles.Large;
            btnCreate.Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32);
            groupLinksAction.ItemLinks.Add(btnCreate);
            btnCreate.ItemClick += delegate
            {
                List<Uid> collCurrentUsers = bindListUsers.DataSource as List<Uid>;
                List<Uid> selNewUser = element.Workarea.Empty<Uid>().BrowseMultyList(element.Workarea, s => !collCurrentUsers.Exists(f => f.Id == s.Id), null, true);
                if (selNewUser != null && selNewUser.Count > 0)
                {
                    foreach (Uid userId in selNewUser)
                    {
                        bindListUsers.Add(userId);
                    }
                    //UserRightElement right = new UserRightElement();
                    //right.Workarea = element.Workarea;
                    //right.ElementId = element.Id;
                    //right.DbUidId = userId.Id;
                    //right.DbEntityId = element.EntityId;
                    //bindRights.Add(right);
                }
            };
            #endregion
            #region Удаление
            BarButtonItem btnDelete = new BarButtonItem
            {
                Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EXCLUDE, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(element.Workarea, ResourceImage.DELETE_X32)
            };
            groupLinksAction.ItemLinks.Add(btnDelete);
            btnDelete.ItemClick += delegate
            {
                foreach (DocumentSecure right in collRights)
                {
                    right.Delete();
                }
                bindListUsers.RemoveCurrent();
            };
            #endregion
            page.Groups.Add(groupLinksAction);
            // вторая таблица

            listRights.Grid.DataSource = bindRights;
            var d = new EventHandler(delegate
            {
                int currentUidId = 0;
                if (bindListUsers.Current == null)
                    currentUidId = 0;
                else
                    currentUidId = (bindListUsers.Current as Uid).Id;
                //collRights = element.Workarea.Access.GetCollectionUserRightsElements(1769476, currentUidId, element.Id, element.EntityId);
                collRights = element.DocumentSecures();
                bindRights.DataSource = collRights;
            });
            bindListUsers.CurrentChanged += d;
            bindListUsers.CurrencyManager.Position = -1;
            d.Invoke(null, null);
            RibbonPageGroup groupLinksRights = new RibbonPageGroup();
            groupLinksRights.Text = "Разрешения";
            #region Добавить
            BarButtonItem btnCreateAcl = new BarButtonItem();
            btnCreateAcl.ButtonStyle = BarButtonStyle.Default;
            btnCreateAcl.Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049);
            btnCreateAcl.RibbonStyle = RibbonItemStyles.Large;
            btnCreateAcl.Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32);
            groupLinksRights.ItemLinks.Add(btnCreateAcl);
            btnCreateAcl.ItemClick += delegate
            {
                if (bindListUsers.Current == null) return;
                Uid currentUidId = (bindListUsers.Current as Uid);
                Hierarchy rootDocKind = element.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SYSTEM_RIGHT_DOCUMENTS");
                List<Right> coll = rootDocKind == null ? new List<Right>() : rootDocKind.GetTypeContents<Right>();

                //List<Right> coll = element.Workarea.GetCollection<Right>().Where(s => s.KindValue == 4).ToList();
                var ccList = from c in coll
                             join a in collRights on c.Id equals a.RightId into j
                             from x in j.DefaultIfEmpty()
                             where x == null
                             select c;
                List<Right> browseSel = element.Workarea.Empty<Right>().BrowseListMulty(null, ccList.ToList());
                if (browseSel != null && browseSel.Count > 0)
                {
                    foreach (Right selectedRight in browseSel)
                    {
                        DocumentSecure documentSecure = new DocumentSecure
                                                            {
                                                                Workarea = element.Workarea,
                                                                OwnId=element.Id,
                                                                RightId = selectedRight.Id,
                                                                UserIdTo = currentUidId.Id,
                                                                StateId = State.STATEACTIVE,
                                                                DateStart = DateTime.Now,
                                                                DateEnd = DateTime.Now.AddYears(1),
                                                                IsAllow=true
                                                            };
                       documentSecure.Save();
                       bindRights.Add(documentSecure);
                    }
                }
            };
            #endregion
            #region Изменить
            BarButtonItem btnPropAcl = new BarButtonItem
            {
                Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
            };
            groupLinksRights.ItemLinks.Add(btnPropAcl);
            btnPropAcl.ItemClick += delegate
            {
                if (bindRights.Current == null) return;
                DocumentSecure right = bindRights.Current as DocumentSecure;
                //right.ShowProperty();
            };
            #endregion
            #region Удаление
            BarButtonItem btnDeleteAcl = new BarButtonItem
            {
                Caption = element.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EXCLUDE, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(element.Workarea, ResourceImage.DELETE_X32)
            };
            groupLinksRights.ItemLinks.Add(btnDeleteAcl);
            btnDeleteAcl.ItemClick += delegate
            {
                if (bindRights.Current == null) return;
                DocumentSecure right = bindRights.Current as DocumentSecure;
                right.Delete();
                bindRights.RemoveCurrent();
            };
            #endregion
            page.Groups.Add(groupLinksRights);

            listUserGroups.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.FieldName == "Image" && e.IsGetData)
                {
                    Uid imageItem = bindListUsers[e.ListSourceRowIndex] as Uid;
                    if (imageItem != null)
                    {
                        e.Value = imageItem.GetImage();
                    }
                }
                else if (e.Column.Name == "colStateImage")
                {
                    Uid imageItem = bindListUsers[e.ListSourceRowIndex] as Uid;
                    if (imageItem != null)
                    {
                        e.Value = imageItem.State.GetImage();
                    }
                }
            };

            listRights.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.FieldName == "Image" && e.IsGetData)
                {
                    e.Value = ResourceImage.GetSystemImage(ResourceImage.KEY_X16);
                }
            };
            frm.clientPanel.Controls.Add(mainControl);
            mainControl.Dock = DockStyle.Fill;
            frm.ShowDialog();
        }

        
        #endregion
    }
}
