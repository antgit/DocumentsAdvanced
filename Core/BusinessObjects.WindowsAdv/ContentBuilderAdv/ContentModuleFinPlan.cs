using System.Collections.Generic;
using System.Linq;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль раздела "Финансовое планирование"
    /// </summary>
    public class ContentModuleFinPlan : ContentModuleByFolders, IContentModule
    {
        public ContentModuleFinPlan()
            : base()
        {
            TYPENAME = "MODULEFINPLAN";
            Caption = "Финансовое планирование";
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
            Hierarchy rootData = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SYSTEM_GRP_FINPLAN");
            List<Folder> folders = rootData.GetTypeContents<Folder>();
            List<Hierarchy> hierarchies = Workarea.GetCollection<Hierarchy>(options.Hierarchies);
            var query1 = from f in folders
                         where f.IsStateActive && !f.IsHiden
                         select new ViewFolders { Id = f.Id, Name = f.Name, HierarchyName = rootData.Name, Memo = f.Memo, Folder = f, Hierarchy = rootData, Kind = 7 };
            var query2 = from h in hierarchies
                         select new ViewFolders { Id = h.Id, Name = h.Name, HierarchyName = h.Parent.Name, Memo = h.Memo, Folder = null, Hierarchy = h, Kind = 28 };


            BindingFolderView.DataSource = query1.Union(query2);
        }
    }
}