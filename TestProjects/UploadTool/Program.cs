using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UploadTool
{
    //endpoint address="http://docsys.com/biservicesnadin/WebOrdersService.svc"
    //endpoint address="http://localhost:11394/biservices/WebOrdersService.svc"
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
