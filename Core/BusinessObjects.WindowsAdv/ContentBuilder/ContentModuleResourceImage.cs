using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Графические ресурсы"
    /// </summary>
    public sealed class ContentModuleResourceImage : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
        private string TYPENAME;
        BindingSource _source;
        private RibbonPageGroup _groupLinksAction;
        private ControlList _controlMain;

        public ContentModuleResourceImage()
        {
            TYPENAME = "RESOURCEIMAGE";
            Key = TYPENAME + "_MODULE";
            
            Caption = "Графические ресурсы";
        }
        #region IContentModule Members
        private Library _selfLib;
        public Library SelfLibrary
        {
            get
            {
                if (_selfLib == null && Workarea != null)
                    _selfLib = Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(Key);
                return _selfLib;
            }
        }
        private string _parentKey;
        public string ParentKey
        {
            get
            {
                if (_parentKey == null && Workarea != null)
                {
                    if (SelfLibrary != null)
                    {
                        int? fHierarchyId = Hierarchy.FirstHierarchy<Library>(SelfLibrary);
                        if (fHierarchyId.HasValue && fHierarchyId.Value != 0)
                        {
                            Hierarchy h = Workarea.Cashe.GetCasheData<Hierarchy>().Item(fHierarchyId.Value);
                            _parentKey = UIHelper.FindParentHierarchy(h);

                        }

                    }

                }
                return _parentKey;
            }
            set { _parentKey = value; }
        }
        public void InvokeHelp()
        {
            
        }
        public Bitmap Image32 { get; set; }
        private Workarea _workarea;
        public Workarea Workarea
        {
            get { return _workarea; }
            set
            {
                _workarea = value;
                Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.IMAGE_X32);
            }
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        public Control Control
        {
            get
            {
                return _controlMain;
            }
        }

        void CreateControls()
        {
            _controlMain = new ControlList();
            _controlMain.View.OptionsSelection.MultiSelect = true;
            _source = new BindingSource();
            RefreshGrid();
            _controlMain.Grid.DoubleClick += GridDoubleClick;
            _controlMain.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;
            _controlMain.View.OptionsView.RowAutoHeight = true; 
        }

        private void RefreshGrid()
        {
            List<ResourceImage> coll = ResourceImage.Collection(Workarea);
            _source.DataSource = coll;
            _controlMain.Grid.DataSource = _source;
            DataGridViewHelper.GenerateGridColumns(Workarea, _controlMain.View, "DEFAULT_LISTVIEWIMAGERESOURCE");
        }

        void ViewCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                ResourceImage imageItem = _source[e.ListSourceRowIndex] as ResourceImage;
                if (imageItem != null)
                {
                    //e.Value = imageItem.GetImage();
                }
                else
                {
                    System.Data.DataRowView rv = _source[e.ListSourceRowIndex] as System.Data.DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId) && rv.DataView.Table.Columns.Contains("KindId"))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];
                        int kindId = (int)rv["KindId"];
                        e.Value = ExtentionsImage.GetImage(Workarea, kindId, stId);
                    }
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                ResourceImage imageItem = _source[e.ListSourceRowIndex] as ResourceImage;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
                else
                {
                    System.Data.DataRowView rv = _source[e.ListSourceRowIndex] as System.Data.DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];
                        e.Value = ExtentionsImage.GetImageState(Workarea, stId);
                    }
                }
            }
            else if (e.Column.Name == "colValue" && e.IsGetData)
            {
                ResourceImage imageItem = _source[e.ListSourceRowIndex] as ResourceImage;
                e.Value = imageItem.Value;
                //_controlMain.View.Ro e.RowHandle
                //if (_controlMain.View.RowHeight < imageItem.Value.Size.Height)
                //    _controlMain.View.RowHeight = imageItem.Value.Size.Height;
            }
        
        }
        void GridDoubleClick(object sender, System.EventArgs e)
        {
            Point p = _controlMain.Grid.PointToClient(Control.MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = _controlMain.View.CalcHitInfo(p.X, p.Y);
            if (hit.InRowCell)
            {
                if (_source.Current == null) return;
                (_source.Current as ResourceImage).ShowProperty();
            }
        }

        public void PerformShow()
        {
            if (_controlMain == null)
            {
                CreateControls();
            }
            if (_groupLinksAction != null)
                _groupLinksAction.Visible = true;

            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksAction = page.GetGroupByName(TYPENAME + "_ACTIONLIST");
            if (_groupLinksAction == null)
            {
                _groupLinksAction = new RibbonPageGroup {Name = TYPENAME + "_ACTIONLIST", Text = Workarea.Cashe.ResourceString("LB_RIBBON_ACTION", 1049)};

                #region Создать
                BarButtonItem btnCreate = new BarButtonItem
                                              {
                                                  Caption = Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                              };
                _groupLinksAction.ItemLinks.Add(btnCreate);
                btnCreate.ItemClick += delegate
                                           {
                                               ResourceImage res = new ResourceImage { Workarea = Workarea, StateId= State.STATEACTIVE, FlagsValue = FlagValue.FLAGSYSTEM};
                                               res.ShowProperty();
                                               res.Created += delegate
                                               {
                                                   int index = _source.Add(res);
                                                   _source.Position = index;
                                               };
                                               //if (!res.IsNew)
                                               //{
                                               //    int index =_source.Add(res);
                                               //    _source.Position = index;
                                               //}

                                           };
                #endregion

                #region Изменить
                BarButtonItem btnEdit = new BarButtonItem
                                            {
                                                Caption = Workarea.Cashe.ResourceString("BTN_CAPTION_EDIT", 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                _groupLinksAction.ItemLinks.Add(btnEdit);
                btnEdit.ItemClick += delegate
                                         {
                                             if (_source.Current == null) return;

                                             (_source.Current as ResourceImage).ShowProperty();

                                         };
                #endregion

                #region Обновить
                BarButtonItem btnRefresh = new BarButtonItem
                {
                    Caption = Workarea.Cashe.ResourceString("BTN_CAPTION_REFRESH", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnRefresh);
                btnRefresh.ItemClick += delegate
                {
                    RefreshGrid();
                };
                #endregion

                ResourceImage image;

                #region Импорт
                BarButtonItem btnImport = new BarButtonItem
                                              {
                                                  Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_IMAGEIMPORT, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DATAINTO_X32)
                                              };
                _groupLinksAction.ItemLinks.Add(btnImport);
                btnImport.ItemClick += delegate
                {
                    OpenFileDialog dlg = new OpenFileDialog {Filter = "PNG|*.png"};
                    dlg.Multiselect = true;
                    if (dlg.ShowDialog() == DialogResult.Cancel) return;
                    foreach (string fileName in dlg.FileNames)
                    {
                        image = new ResourceImage
                                    {
                            Value = Image.FromFile(fileName),
                            Code = System.IO.Path.GetFileNameWithoutExtension(fileName),
                            Workarea = Workarea
                        };

                        if (!_source.Contains(image))
                        {
                            image.Save();
                            _source.Add(image);
                        }
                    }
                }; 
                #endregion

                #region Экспорт
                BarButtonItem btnExport = new BarButtonItem
                                              {
                                                  Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_IMAGEEXPORT, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DATAOUT_X32)
                                              };
                _groupLinksAction.ItemLinks.Add(btnExport);
                btnExport.ItemClick += delegate
                {
                    FolderBrowserDialog dlg = new FolderBrowserDialog();
                    if (dlg.ShowDialog() == DialogResult.Cancel) return;
                    int[] rows = _controlMain.View.GetSelectedRows();
                    for (int j = rows.Length - 1; j >= 0; j--)
                    {
                        int i = rows[j];
                        image = _controlMain.View.GetRow(i) as ResourceImage;
                        //TODO: Path.Combine
                        string path = dlg.SelectedPath + @"\" + image.Code + ".png";
                        if (!System.IO.File.Exists(path))
                            image.Value.Save(path);
                    }
                }; 
                #endregion

                #region Удалить
                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = Workarea.Cashe.ResourceString("BTN_CAPTION_DELETE", 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                              };
                _groupLinksAction.ItemLinks.Add(btnDelete);
                btnDelete.ItemClick += delegate
                                           {
                                               if (_source.Current == null) return;
                                               // TODO: 
                                               (_source.Current as ResourceImage).Delete();
                                               _source.RemoveCurrent();
                                           };
                #endregion

                page.Groups.Add(_groupLinksAction);
            }
        }

        public void PerformHide()
        {
            if (_groupLinksAction != null)
                _groupLinksAction.Visible = false;
        }

        public Form Owner { get; set; }
        public void ShowNewWindows()
        {
            FormProperties frm = new FormProperties
            {
                Width = 800,
                Height = 480
            };
            Bitmap img = Workarea.Empty<ResourceImage>().GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };

            navigator.SafeAddModule(Key, this);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;

            frm.Show();
        }
        #endregion
    }
}