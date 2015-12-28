using System;
using System.Security.Permissions;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Основным назначением является просмотр отчетов службы Reporting Service 
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public sealed partial class FormWebViewer : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FormWebViewer()
        {
            InitializeComponent();
            //this.ribbon.Minimized = true;
            new FormStateMaintainer(this, string.Format("Property{0}", GetType().Name));

        }

        private void FormWebViewerLoad(object sender, EventArgs e)
        {
            Browser.AllowWebBrowserDrop = false;
            Browser.IsWebBrowserContextMenuEnabled = false;
            Browser.WebBrowserShortcutsEnabled = false;
            Browser.ObjectForScripting = this;
        }

        private void BtnCloseItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnBackItemClick(object sender, ItemClickEventArgs e)
        {
            Browser.GoBack();
        }

        private void BtnNextItemClick(object sender, ItemClickEventArgs e)
        {
            Browser.GoForward();
        }
    }
}