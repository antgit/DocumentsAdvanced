using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects.Security;
using BusinessObjects.Windows;

namespace BusinessObjects.Samples
{
    public static class Helper
    {
        public static Workarea OpenDataBase(out Uid user)
        {
            // Пользователь
            user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            // Соединяемся под интегрированной аутентификацией
            builder.IntegratedSecurity = true;
            // Текущий сервер - локальный, если необходимо меняем
            //builder.DataSource = ".";
            builder.DataSource = ".";
            // Имя базы данных
            builder.InitialCatalog = "Documents2011";
            builder.UserID = user.Name;
            builder.Password = user.Password;
            builder.IntegratedSecurity = user.AuthenticateKind == 1;

            Workarea WA = new Workarea();
            WA.ConnectionString = builder.ConnectionString;
            try
            {
                if (WA.LogOn(user.Name))
                {
                    return WA;
                }
            }
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
                    if (idx == 0)
                    {
                        string cnnString = builder.ConnectionString;
                        user = BusinessObjects.Windows.Extentions.ShowLogin(builder.UserID, builder.Password,
                                                                            builder.IntegratedSecurity);
                        // Установить новый пароль путем вызова SqlConnection.Open()
                        builder.Password = user.Password;
                        WA.ConnectionString = builder.ConnectionString;
                        System.Data.SqlClient.SqlConnection.ChangePassword(cnnString, builder.Password);
                    }
                }
                else
                    DevExpress.XtraEditors.XtraMessageBox.Show(sqlEx.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }
}
