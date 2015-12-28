//http://msdn.microsoft.com/en-us/library/ms998558.aspx
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects;
using BusinessObjects.Security;
using BusinessObjects.Windows;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraSplashScreen;

namespace Documents2012
{
    
    static class Program
    {
        public static Workarea WA;
        public static Uid user;
        internal static SplashScreenManager manager;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
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
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            try
            {
                AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
                Application.ThreadException += ApplicationThreadException;
                

                DevExpress.UserSkins.OfficeSkins.Register();
                DevExpress.UserSkins.BonusSkins.Register();
                //DevExpress.UserSkins.OfficeSkins.Register();
                //Application.Run(new Form1());
                MainForm frm = new MainForm { Size = new System.Drawing.Size(800,600) };
                new FormStateMaintainer(frm, string.Format("MAINAPP{0}", frm.GetType().Name));
                DataInfo dbInfo=null;
                if (args.Length > 0)
                {
                    string fulldata = string.Empty;
                    foreach (string s in args)
                        fulldata += s + " ";
                    dbInfo = new DataInfo {ConnectionString = fulldata};
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(dbInfo.ConnectionString);
                    user = new Uid {Name = builder.UserID, Password = builder.Password };
                    if (builder.IntegratedSecurity)
                        user.AuthenticateKind = 1;


                }
                else
                {
                    
                    dbInfo = Helper.HelpDataBaseListMethod(out user);
                }
                
                if (dbInfo != null)
                {
                    frm.Text = dbInfo.Name;
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(dbInfo.ConnectionString);
                    //if (!builder.IntegratedSecurity)
                    //    user = BusinessObjects.WindowsAdv.Extentions.ShowLogin(builder.UserID, builder.Password, builder.IntegratedSecurity);
                    //else
                    //    user = new Uid {Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1};
                    if (user != null)
                    {
                        WA = new Workarea();
                        builder.UserID = user.Name;
                        builder.Password = user.Password;
                        builder.IntegratedSecurity = user.AuthenticateKind == 1;
                        WA.ConnectionString = builder.ConnectionString;
                        WA.ConnectionCode = dbInfo.Name;
                        try
                        {
                            // TODO: Выполнять проверку входа с систему и при необходимости выводить окно несколько раз.
                            if (WA.LogOn(user.Name))
                            {
                                
                                //FormMain frm = new FormMain();
                                //frm.BuildUI(true);
                                //splashScreenManager1

                                manager = new SplashScreenManager(frm, null, true, true); //typeof(SplashScreen1)
                                Application.Run(frm);
                            }
                        }
                        #region Обработка ошибок
                        catch (SqlException sqlEx)
                        {

                            // спец коды для определения необходимости замены пароля
                            if ((sqlEx.Number == 18487) || (sqlEx.Number == 18488))
                            {
                                // Получить новый пароль вызвав диалог ввода пароля с дополнительным сообщением...
                                int idx = Extentions.ShowMessageChoice(WA, 
                                    "Внимание", "Изменение пароля",
                                                         "Ваш пароль требуется изменить в соответсвии с требованиями учетной политики",
                                                         "Выполнить изменение парля самостоятельно|Я отказываюсь изменить свой пароль (работа с программой будет закончена)");
                                //int idx = BusinessObjects.Controls.cTaskDialog.ShowCommandBox("Требуется вмешательство пользователя...", "Изменение пароля", "Ваш пароль требуется изменить в соответсвии с требованиями учетной политики", "Выполнить изменение парля самостоятельно|Я отказываюсь изменить свой пароль (работа с программой будет закончена)", false);
                                if (idx == 0)
                                {
                                    string cnnString = builder.ConnectionString;
                                    user = Extentions.ShowLogin(builder.UserID, builder.Password, builder.IntegratedSecurity);
                                    // Установить новый пароль путем вызова SqlConnection.Open()
                                    builder.Password = user.Password;
                                    WA.ConnectionString = builder.ConnectionString;
                                    SqlConnection.ChangePassword(cnnString, builder.Password);
                                }
                            }
                            else
                                DevExpress.XtraEditors.XtraMessageBox.Show(sqlEx.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(e.ExceptionObject.ToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void ApplicationThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(e.Exception.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
