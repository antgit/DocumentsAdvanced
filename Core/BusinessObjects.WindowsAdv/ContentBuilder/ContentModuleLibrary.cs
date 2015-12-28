using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������ ������ "����������"
    /// </summary>
    public sealed class ContentModuleLibrary : ContentModuleBase<Library>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <remarks>
        /// � ������������ �������������� ���������, ������������ ������� � ������� 
        /// ����������� ������ � ������� � ������� ���������� ����������� � ���� �����.
        /// ��������� ������ �� ��������� - "����������".
        /// � ��������� ������� ����������� �������������� ������ �������� ��� 
        /// ����������, ����������� ������� ������� � ���� ������ � �����.
        /// </remarks>
        public ContentModuleLibrary()
        {
            Caption = "����������";
            Show += ContentModuleShow;
            CreateControlTreeList += ContentModuleCreateTreeList;
        }

        /// <summary>
        /// ����� ����������� ��������������� ����������� �������� Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.LIBRARY_X32); 
        }
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        Action<Library> _showProp;
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� �����
        /// </summary>
        Action<Library> _showPropTreeList;
        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        Action<Library> _saveLibrary;
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowProp(Library value)
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
        void OnShowPropTreeList(Library value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                {
                    /*if (!treeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(value))
                    {
                        int position = treeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                        treeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;
                    }
                    treeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(value);*/
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
        void OnSaveObject(Library value)
        {
            value.Save();
        }
        /// <summary>
        /// ���������� ������� "���������� ������ � ��������".
        /// </summary>
        /// <remarks>� ����������� ����������� �������������� ������ "���������� ���������"</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ContentModuleCreateTreeList(object sender, EventArgs e)
        {
            BarButtonItem btnUpdateLibrarires = new BarButtonItem
            {
                Caption = "���������� ���������",
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TRIANGLEGREEN_X32)
            };
            btnUpdateLibrarires.SuperTip = UIHelper.CreateSuperToolTip(btnUpdateLibrarires.Glyph, "���������� ���������",
                "���������� ������������ ��������� � ���� ������. ����������� ������ ���������� ������� ���������� ����� dll, exe � mrt. ��������� ������� ��� ���������� ���������.");
            groupLinksActionTreeList.ItemLinks.Insert(btnCommon.Links[0], btnUpdateLibrarires);
            btnUpdateLibrarires.ItemClick += delegate
                                                 {
                                                     OpenFileDialog dlg = new OpenFileDialog();
                                                     dlg.Filter = "�������� ������������|*.exe;*.dll;*.mrt|����������|*.dll|�������� ����� ����������|*.mrt|����������|*.exe|����� ������������|*.config";
                                                     //|�������� ����� ����������|*.mrt|����� ������������|*.config
                                                     dlg.Multiselect = true;
                                                     if(dlg.ShowDialog()== DialogResult.OK)
                                                     {
                                                         List<Library> coll = Workarea.GetCollection<Library>();

                                                         foreach (string v in dlg.FileNames)
                                                         {
                                                             
                                                             string fileName = Path.GetFileNameWithoutExtension(v);
                                                             string fileExtention = Path.GetExtension(v).Substring(1);
                                                             bool IsLib = fileExtention.EndsWith("dll") || fileExtention.EndsWith("exe");
                                                             bool IsPrintForm = fileName.EndsWith("mrt");
                                                             bool IsDone = false;
                                                             List<FileData> collFiles = Workarea.Empty<FileData>().FindByFullName(fileName, fileExtention);
                                                             if(collFiles.Count==1)
                                                             {
                                                                 Library findObj = coll.FirstOrDefault(f => f.AssemblyId == collFiles[0].Id);

                                                                 if (findObj != null)
                                                                 {
                                                                     if (IsLib)
                                                                     {
                                                                         findObj.SetLibrary(v);
                                                                         findObj.NameFull = findObj.GetAssembly().FullName;

                                                                         object[] attributes = findObj.GetAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
                                                                         if (attributes.Length > 0)
                                                                         {
                                                                             ((ICoreObject)findObj).Guid = new Guid(((System.Runtime.InteropServices.GuidAttribute)attributes[0]).Value);
                                                                         }
                                                                         findObj.Save();
                                                                         IsDone = true;
                                                                     }
                                                                     else if (IsPrintForm && findObj.KindValue == 8)
                                                                     {
                                                                         findObj.SetSource(v);
                                                                         byte[] value = findObj.GetSource();
                                                                         Stimulsoft.Report.StiReport rep = new Stimulsoft.Report.StiReport();
                                                                         rep.Load(value);
                                                                         findObj.AssemblySource.StreamData = rep.SaveToByteArray();
                                                                         MemoryStream stream = new MemoryStream();
                                                                         rep.Compile(stream);
                                                                         findObj.AssemblyDll.StreamData = stream.ToArray();
                                                                         findObj.Save();
                                                                         IsDone = true;
                                                                     }

                                                                     if (!IsDone)
                                                                     {
                                                                         XtraMessageBox.Show(string.Format("���������� {0} �� ���������!", fileName),
                                                                                         "��������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                     }
                                                                 }
                                                                 else
                                                                 {
                                                                     XtraMessageBox.Show(string.Format("���������� {0} �� ���������������� � �������!", fileName),
                                                                                         "��������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                 }
                                                             }
                                                             else // ���������� ������ ������ 1
                                                             {
                                                                 XtraMessageBox.Show(string.Format("������� ����� ����� ����� � ������ {0}!", fileName),
                                                                                         "��������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                             }
                                                         }
                                                         XtraMessageBox.Show("���������� ���������! ������������� ���������",
                                                                                     "��������!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                     }
                                                 };
        }
        /// <summary>
        /// ���������� ������� ����������� ������
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            if (BrowserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = new Action<Library>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<Library>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveLibrary == null)
                {
                    _saveLibrary = new Action<Library>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveLibrary;
                }
            }
        }

        private BarButtonItem _btnCreateCopy;

        /// <summary>
        /// ����������� ������ ������ ����������
        /// </summary>
        /// <remarks>
        /// ������������� �� ������ ���������� ����������� ������ "������� �����".
        /// </remarks>
        protected override void RegisterPageAction()
        {
            base.RegisterPageAction();
            #region ������� ����� ������
            if (_btnCreateCopy != null) return;
            _btnCreateCopy = new BarButtonItem
            {
                Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATECOPY, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.EDITCOPY_X32)
            };
            groupLinksActionTreeList.ItemLinks.Add(_btnCreateCopy);
            _btnCreateCopy.ItemClick += delegate
            {
                if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue == null) return;
                Library copyObj = Library.CreateCopy(TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue);
                if (copyObj != null)
                {
                    TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(copyObj);
                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position = TreeListBrowser.ListBrowserBaseObjects.BindingSource.Add(copyObj);
                }

            };
            #endregion
        }
    }
}