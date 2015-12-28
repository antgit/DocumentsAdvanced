using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль раздела "Управление финансами"
    /// </summary>
    public class ContentModuleFinance : ContentModuleByFolders, IContentModule
    {
        public ContentModuleFinance(): base()
        {
            TYPENAME = "MODULEFINANCE";
            Caption = "Управление финансами";
            Key = TYPENAME + "_MODULE";
            
        }
        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.RATE_X32);
        }

        /// <summary>
        /// Заполняет грид с группами
        /// </summary>
        protected override void RefreshGroupGrid()
        {
            Hierarchy rootData = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("FINANCE");
            List<Folder> folders = rootData.GetTypeContents<Folder>();
            List<Hierarchy> hierarchies = Workarea.GetCollection<Hierarchy>(options.Hierarchies);
            var query1 = from f in folders
                         where f.IsStateActive && !f.IsHiden
                         select new ViewFolders { Id = f.Id, Name = f.Name, HierarchyName = rootData.Name, Memo = f.Memo, Folder = f, Hierarchy = rootData, Kind = 7 };
            var query2 = from h in hierarchies
                         select new ViewFolders { Id = h.Id, Name = h.Name, HierarchyName = h.Parent.Name, Memo = h.Memo, Folder = null, Hierarchy = h, Kind = 28 };


            Hierarchy rootDataNds = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("FINANCE_NDS");
            List<Folder> foldersNds = rootDataNds.GetTypeContents<Folder>();
            var query3 = from f in foldersNds
                         where f.IsStateActive && !f.IsHiden
                         select new ViewFolders { Id = f.Id, Name = f.Name, HierarchyName = rootDataNds.Name, Memo = f.Memo, Folder = f, Hierarchy = rootDataNds, Kind = 7 };


            BindingFolderView.DataSource = query1.Union(query2).Union(query3);
        }
    }
}