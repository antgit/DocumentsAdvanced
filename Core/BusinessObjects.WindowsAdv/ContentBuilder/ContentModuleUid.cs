using System;
using System.Linq;
using BusinessObjects.Security;
using DevExpress.XtraBars;
using System.Collections.Generic;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.Utils;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������ ������ "������������ � ������"
    /// </summary>
    public sealed class ContentModuleUid : ContentModuleBase<Uid>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <remarks>
        /// � ������������ �������������� ��������� � ������������ ������� � ������� 
        /// ����������� ������.
        /// ��������� ������ �� ��������� - "������������ � ������".
        /// � ��������� ������� ����������� �������������� ������ �������� ��� 
        /// ����������, ����������� ������� ������� � ���� ������ � �����.
        /// </remarks>
        public ContentModuleUid()
        {
            Caption = "������������ � ������";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// ����� ����������� ��������������� ����������� �������� Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.USER_X32);
        }
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        Action<Uid> _showProp;
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� �����
        /// </summary>
        Action<Uid> _showPropTreeList;
        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        Action<Uid> _saveUid;
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowProp(Uid value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                {
                    int position = BrowserBaseObjects.BindingSource.Add(value);
                    BrowserBaseObjects.BindingSource.Position = position;
                };
            }
        }
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ��������� � ���� �����
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowPropTreeList(Uid value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                {
                    /*int position = treeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                    treeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;*/
                    TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(value);
                    if (!TreeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(value))
                    {
                        int position = TreeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                        TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;
                    }
                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position =
                        TreeListBrowser.ListBrowserBaseObjects.BindingSource.IndexOf(value);
                };
            }
        }
        /// <summary>
        /// ���������� ������ ���������� �������
        /// </summary>
        /// <param name="value">������ ��� ����������</param>
        void OnSaveObject(Uid value)
        {
            value.Save();
        }
        /// <summary>
        /// ���������� ������� ����������� ������
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            TreeListBrowser.ViewRightPanel = false;
            btnActions.Visibility = BarItemVisibility.Never;
            btnFind.Visibility = BarItemVisibility.Never;
            if (groupLinksView!=null)
                ((BarCheckItemLink)groupLinksView.ItemLinks[1]).Item.Visibility = BarItemVisibility.Never;
            if (BrowserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = OnShowProp;
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = OnShowPropTreeList;
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveUid == null)
                {
                    _saveUid = OnSaveObject;
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveUid;
                }
            }
        }

        private TreeList _groups;
        private Controls.ControlList _users;
        private ImageCollection _images;
        private Uid _curentItem;
        private bool _groupsIsActive;
        protected override void OnCreateControlTreeList()
        {
            base.OnCreateControlTreeList();

            #region ������ � ������ ����������� ��������
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;


            if (ActiveView == "TREELIST")
            {
                groupLinksActionTreeList = page.GetGroupByName(TYPENAME + "_ACTIONTREELIST");
                groupLinksActionTreeList.ItemLinks.Clear();

                #region ����� ������
                BarButtonItem btnChainCreate = new BarButtonItem
                                                   {
                                                       ButtonStyle = BarButtonStyle.DropDown,
                                                       ActAsDropDown = true,
                                                       Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                btnChainCreate.SuperTip = UIHelper.CreateSuperToolTip(btnChainCreate.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                "������� ����� ������ ��� ������. ��� �������� �������, �� ������������� ���������� � ������� ������");
                groupLinksActionTreeList.ItemLinks.Add(btnChainCreate);

                PopupMenu mnuTemplates = new PopupMenu {Ribbon = form.Ribbon};
                BarButtonItem btnGroup = new BarButtonItem
                                             {
                                                 Caption = "������",
                                                 Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.USERGROUP_X16)
                                             };
                mnuTemplates.AddItem(btnGroup);
                btnGroup.ItemClick += delegate
                {
                    Uid newGroup = new Uid { Workarea = Workarea, KindValue = 2 };
                    newGroup.ShowProperty().FormClosed += delegate(object s, FormClosedEventArgs ev)
                    {
                        if (s != null)
                        {
                            Form f = s as Form;
                            if (f.DialogResult == DialogResult.OK)
                            {
                                TreeListNode node = _groups.AppendNode(new object[] { newGroup.Id, newGroup.Name }, -1);
                                node.ImageIndex = _images.Images.Keys.IndexOf("GROUP");
                                node.SelectImageIndex = node.ImageIndex;
                            }
                        }
                    };
                };

                BarButtonItem btnUser = new BarButtonItem {Caption = "������������", Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.AGENTWORKER_X16)};
                mnuTemplates.AddItem(btnUser);
                btnUser.ItemClick += delegate
                {
                    Uid newGroup = new Uid { Workarea = Workarea, KindValue = 1 };
                    newGroup.ShowProperty().FormClosed += delegate(object s, FormClosedEventArgs ev)
                    {
                        if (s != null)
                        {
                            Form f = s as Form;
                            if (f.DialogResult == DialogResult.OK)
                            {
                                int index = _bindingUsers.Add(newGroup);
                                _bindingUsers.Position = index;
                            }
                        }
                    };
                };

                btnChainCreate.DropDownControl = mnuTemplates;
                #endregion

                #region ��������
                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                btnProp.SuperTip = UIHelper.CreateSuperToolTip(btnProp.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                "�������� ���� ���������� ���������� �������� ������� ��� ������");
                groupLinksActionTreeList.ItemLinks.Add(btnProp);
                btnProp.ItemClick += delegate
                {
                    if (_curentItem != null)
                        _curentItem.ShowProperty();
                };

                #endregion

                #region ��������
                BarButtonItem btnRefresh = new BarButtonItem
                                               {
                                                   Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                   RibbonStyle = RibbonItemStyles.Large,
                                                   Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
                                               };
                btnRefresh.SuperTip = UIHelper.CreateSuperToolTip(btnRefresh.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                "��������� ������ ����� � ������� ��������");
                groupLinksActionTreeList.ItemLinks.Add(btnRefresh);
                btnRefresh.ItemClick += delegate
                {
                    BuildTree();
                    _bindingUsers.Clear();
                };
                #endregion

                #region ��������
                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE,1049),
                                                  Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32),
                                                  RibbonStyle = RibbonItemStyles.Large
                                              };
                btnDelete.SuperTip = UIHelper.CreateSuperToolTip(btnDelete.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                "������� ������ ��� ������. ��� �������� ����� ������� ������ �������� (� ������� ��� ��������). ������������� ������ �������� - � �������");
                groupLinksActionTreeList.ItemLinks.Add(btnDelete);

                btnDelete.ItemClick += delegate
                {
                    int res = Extentions.ShowMessageChoice(Workarea,
                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                                        "�������� ��������������",
                                        "��� �������� � \"�������\" �������� ������ �������������, � ��� ������ �������� ���������� ������������� ����������. ������������� ������������ �������� � �������, ������������ ������ �������� �������� ������ ��� ������ ����������� � ������������ ����� ��������.",
                                        Properties.Resources.STR_CHOICE_DEL);

                    // �������� � �������
                    if (res == 0)
                    {
                        try
                        {
                            _curentItem.Remove();
                            if (_groupsIsActive)
                                _groups.Nodes.Remove(_groups.FocusedNode);
                            else
                                _bindingUsers.Remove(_curentItem);
                        }
                        catch (DatabaseException dbe)
                        {
                            Extentions.ShowMessageDatabaseExeption(Workarea,
                                Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                            "������ ��������!", dbe.Message, dbe.Id);
                        }
                        catch (Exception ex)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    // ��������
                    else if (res == 1)
                    {
                        try
                        {
                            _curentItem.Delete();
                            if (_groupsIsActive)
                                _groups.Nodes.Remove(_groups.FocusedNode);
                            else
                                _bindingUsers.Remove(_curentItem);
                        }
                        catch (DatabaseException dbe)
                        {
                            Extentions.ShowMessageDatabaseExeption(Workarea,
                                Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                            "������ ��������!", dbe.Message, dbe.Id);
                        }
                        catch (Exception ex)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };
                #endregion
                
                #region ����������
                /*if (Workarea.Access.RightCommon.AdminEnterprize)
                {
                    //
                    BarButtonItem btnAcl = new BarButtonItem();
                    btnAcl.Caption = "����������";
                    btnAcl.RibbonStyle = RibbonItemStyles.Large;
                    btnAcl.Glyph = ResourceImage.GetSystemImage(ResourceImage.KEYS32);
                    btnAcl.SuperTip = CreateSuperToolTip(btnAcl.Glyph, "����������",
                        "������ ���������� �� ���������� ��������� � �������� ��� �������������");
                    groupLinksActionTreeList.ItemLinks.Add(btnAcl);
                    btnAcl.ItemClick += delegate
                    {
                        /*if (controlTreeList.ActiveControl is ControlTree)
                        {
                            if (treeListBrowser.TreeBrowser.SelectedHierarchy != null)
                                treeListBrowser.TreeBrowser.SelectedHierarchy.BrowseElemenRight(2);
                        }
                        else
                        {
                            if (treeListBrowser.ListBrowserBaseObjects.FirstSelectedValue != null)
                                treeListBrowser.ListBrowserBaseObjects.FirstSelectedValue.BrowseElemenRight(2);
                        }*/
                   /* };
                }*/

                #endregion
                
                page.Groups.Add(groupLinksActionTreeList);
            }
            #endregion

            TreeListBrowser._control.SplitContainerControl.Panel1.Controls.Clear();
            TreeListBrowser._control.SplitProperyListControl.Panel1.Controls.Clear();

            _groups = new TreeList();
            _images = new ImageCollection();
            TreeListBrowser._control.SplitContainerControl.Panel1.Controls.Add(_groups);
            _groups.Dock = DockStyle.Fill;
            _groups.SelectImageList = _images;
            _images.AddImage(ResourceImage.GetByCode(Workarea, ResourceImage.USERGROUP_X16), "GROUP");
            _groups.OptionsBehavior.AutoPopulateColumns = false;
            _groups.OptionsBehavior.Editable = false;
            _groups.OptionsSelection.EnableAppearanceFocusedCell = false;
            _groups.OptionsView.ShowIndicator = false;
            _groups.OptionsView.ShowAutoFilterRow = true;
            
            _groups.Click += delegate
            {
                if (_groups.Selection.Count > 0 && !(_groups.Selection[0] is TreeListAutoFilterNode))
                {
                    _groupsIsActive = true;
                    TreeListNode node = _groups.Selection[0];
                    int id = (int)node.GetValue(GlobalPropertyNames.Id);
                    Uid curr = new Uid { Workarea = Workarea };
                    curr.Load(id);
                    _curentItem = curr;
                    BuildGrid(id);
                }
            };

            #region ������� ������
            DevExpress.XtraTreeList.Columns.TreeListColumn colId = new DevExpress.XtraTreeList.Columns.TreeListColumn
                                                                       {
                                                                           Caption = "��",
                                                                           FieldName = GlobalPropertyNames.Id,
                                                                           Visible = false
                                                                       };

            DevExpress.XtraTreeList.Columns.TreeListColumn colName = new DevExpress.XtraTreeList.Columns.TreeListColumn
                                                                         {
                                                                             Caption = "������������",
                                                                             FieldName = "Name",
                                                                             VisibleIndex = 0,
                                                                             Width = 150
                                                                         };

            _groups.Columns.AddRange(new[] { colId, colName });
            #endregion

            _users = new Controls.ControlList();
            TreeListBrowser._control.SplitProperyListControl.Panel1.Controls.Add(_users);
            _users.Dock = DockStyle.Fill;
            
            DataGridViewHelper.GenerateGridColumns(Workarea, _users.View, "DEFAULT_LISTVIEWUID");
            //_users.View.OptionsView.ShowAutoFilterRow = true;
            _users.Grid.Click += delegate
            {
                if (_users.View.GetSelectedRows().Length > 0)
                {
                    Uid curr = (Uid)_users.View.GetRow(_users.View.GetSelectedRows()[0]);
                    _curentItem = curr;
                    _groupsIsActive = false;
                }
            };
            _users.Grid.DoubleClick += delegate
            {
                //if (_users.View.GetSelectedRows().Length > 0)
                //{
                //    Uid curr = (Uid)_users.View.GetRow(_users.View.GetSelectedRows()[0]);
                //    CurentItem = curr;
                //    GroupsIsActive = false;
                    _curentItem.ShowProperty();
               // }
            };
            _users.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
            {
                if (e.Column.Name == "colImage")
                {
                    System.Drawing.Rectangle r = e.Bounds;
                    System.Drawing.Image img = ResourceImage.GetByCode(Workarea, ResourceImage.AGENTWORKER_X16);
                    e.Graphics.DrawImageUnscaled(img, r);
                    e.Handled = true;
                }
                if (e.Column.Name == "colStateImage")
                {
                    if (_bindingUsers.Count - 1 >= _users.View.GetDataSourceRowIndex(e.RowHandle))
                    {
                        System.Drawing.Rectangle r = e.Bounds;
                        int index = _users.View.GetDataSourceRowIndex(e.RowHandle);
                        Uid v = (Uid)_bindingUsers[index];
                        System.Drawing.Image img = v.State.GetImage();
                        e.Graphics.DrawImageUnscaled(img, r);
                        e.Handled = true;
                    }
                }
            };
            BuildTree();
        }

        private void BuildTree()
        {
            _groups.Nodes.Clear();
            List<Uid> usersGroups = Workarea.GetCollection<Uid>();
            if (usersGroups.Count > 0)
            {
                ElementRightView secure = Workarea.Access.ElementRightView(usersGroups[0].EntityId);
                List<int> denyedObject = secure.GetDeny("VIEW");
                if (denyedObject.Count > 0)
                {
                    usersGroups = usersGroups.Where(p => !denyedObject.Contains(p.Id)).ToList();
                }
            }
            foreach (Uid group in usersGroups)
            {
                if (group.KindValue != 2) continue;
                TreeListNode node = _groups.AppendNode(new object[] { group.Id, group.Name }, -1);
                node.ImageIndex = _images.Images.Keys.IndexOf("GROUP");
                node.SelectImageIndex = node.ImageIndex;
            }
        }

        BindingSource _bindingUsers;
        public void BuildGrid(int groupId)
        {
            if (_bindingUsers == null)
                _bindingUsers = new BindingSource();
            else
                _bindingUsers.Clear();
            _users.Grid.DataSource = _bindingUsers;

            List<Uid> usersGroups = Workarea.GetCollection<Uid>();
            foreach (Uid user in usersGroups)
                if (user.KindValue == 1)
                {
                    bool flag = user.Groups.Any(group => group.Id == groupId);
                    if (flag)
                        _bindingUsers.Add(user);
                }
        }
    }
}