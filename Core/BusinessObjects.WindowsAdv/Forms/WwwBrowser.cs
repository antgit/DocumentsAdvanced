using System.Security.Permissions;

namespace BusinessObjects.Windows
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class WwwBrowser : System.Windows.Forms.WebBrowser
    {
        public WwwBrowser(): base()
        {
            AllowWebBrowserDrop = false;
            IsWebBrowserContextMenuEnabled = false;
            WebBrowserShortcutsEnabled = false;
            ObjectForScripting = this;
        }
        
    }
}