using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

namespace SaleManager
{
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

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU")
            {
                NumberFormat =
                {
                    CurrencyDecimalSeparator = ".",
                    NumberDecimalSeparator = "."
                }
            };
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("Black");//DevExpress Style Lilian

            Application.Run(new FormMain());
        }
    }
}