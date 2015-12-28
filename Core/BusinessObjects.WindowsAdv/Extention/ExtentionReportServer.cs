using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        /// <summary>
        /// Показать окно выбора серверных отчетов 
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="wa">Рабочая область</param>
        /// <returns></returns>
        public static List<Library> BrowseReports(this Library item, Workarea wa = null)
        {
            ReportServerContentModule module = new ReportServerContentModule();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }

        public static List<Library> BrowsePrintReports(this Library item, Workarea wa = null)
        {
            ReportsPrintContentModule module = new ReportsPrintContentModule();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
        public static List<Library> BrowseTableReports(this Library item, Workarea wa = null)
        {
            ReportsContentModule module = new ReportsContentModule();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
    }
}