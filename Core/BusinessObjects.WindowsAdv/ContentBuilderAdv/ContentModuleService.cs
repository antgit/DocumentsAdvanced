using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль раздела "Услуги"
    /// </summary>
    public class ContentModuleService : ContentModuleByFolders, IContentModule
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ContentModuleService(): base()
        {
            TYPENAME = "MODULESERVICE";
            Caption = "Услуги";
            Key = TYPENAME + "_MODULE";
            
        }
        #region IContentModule Members
        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.NODE_X32);
        }

        /// <summary>
        /// Заполняет грид с группами
        /// </summary>
        protected override void RefreshGroupGrid()
        {
            Hierarchy rootData = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SERVICE");
            List<Folder> folders = rootData.GetTypeContents<Folder>();
            List<Hierarchy> hierarchies = new List<Hierarchy>();
            if (options.Hierarchies != null)
            {
                hierarchies = Workarea.GetCollection<Hierarchy>(options.Hierarchies.Distinct());
            }
            var query1 = from f in folders
                         where f.IsStateActive && !f.IsHiden
                         select new ViewFolders { Id = f.Id, Name = f.Name, HierarchyName = rootData.Name, Memo = f.Memo, Folder = f, Hierarchy = rootData, Kind = 7 };

            var query2 = from h in hierarchies
                         select new ViewFolders { Id = h.Id, Name = h.Name, HierarchyName = h.Parent.Name, Memo = h.Memo, Folder = null, Hierarchy = h, Kind = 28 };

            Hierarchy rootDataNoNds = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SERVICENONDS");
            List<Folder> foldersNoNds = rootDataNoNds.GetTypeContents<Folder>();
            var query3 = from f in foldersNoNds
                         where f.IsStateActive && !f.IsHiden
                         select new ViewFolders { Id = f.Id, Name = f.Name, HierarchyName = rootDataNoNds.Name, Memo = f.Memo, Folder = f, Hierarchy = rootDataNoNds, Kind = 7 };

            BindingFolderView.DataSource = query1.Union(query2).Union(query3);
        }
        #endregion
    }
}