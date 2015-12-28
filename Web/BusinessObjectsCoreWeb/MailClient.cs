using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using BusinessObjects.Security;

namespace BusinessObjects.Web.Core
{
    /// <summary>
    /// Почтовый клиент
    /// </summary>
    public static class MailClient
    {
        private static SmtpClient Client;
        private static Workarea _wa;

        public static Workarea Workarea
        {
            set
            {
                _wa = value;
                InitClient();
            }
        }
        static MailClient()
        {
            //InitClient();
            //Client.Credentials = new NetworkCredential(
            //   ConfigurationManager.AppSettings["SmtpUser"],
            //   ConfigurationManager.AppSettings["SmtpPass"]);
        }

        private static void InitClient()
        {
            Client = new SmtpClient
                         {
                             Host =
                                 _wa.Cashe.SystemParameters.ItemCode<SystemParameter>("SYSTEMPARAMETER_SMTPSERVER").ValueString,
                             //ConfigurationManager.AppSettings["SmtpServer"],
                             Port =
                                 _wa.Cashe.SystemParameters.ItemCode<SystemParameter>("SYSTEMPARAMETER_SMTPSERVER").ValueInt.
                                 Value,
                             //Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]),
                             DeliveryMethod = SmtpDeliveryMethod.Network
                         };
            Client.UseDefaultCredentials = false;
            Client.Credentials = new NetworkCredential(
                _wa.Cashe.SystemParameters.ItemCode<SystemParameter>("SYSTEMPARAMETER_SMTPUSER").ValueString,
                _wa.Cashe.SystemParameters.ItemCode<SystemParameter>("SYSTEMPARAMETER_SMTPPASS").ValueString);
        }

        private static bool SendMessage(string from, string to, 
             string subject, string body)
        {
            MailMessage mm = null;
            bool isSent = false;
            try
            {
                // Create our message
                mm = new MailMessage(from, to, subject, body);
                mm.IsBodyHtml = true;
                mm.DeliveryNotificationOptions = 
                       DeliveryNotificationOptions.OnFailure;
                // Send it
                Client.Send(mm);
                isSent = true;
            }
            // Catch any errors, these should be logged and 
            // dealt with later
            catch (Exception ex)
            {
                // If you wish to log email errors,
                // add it here...
                var exMsg = ex.Message;
            }
            finally
            {
                mm.Dispose();
            }
            return isSent;
        }

        /// <summary>
        /// Сообщение приветствия
        /// </summary>
        /// <remarks>Используются макро подстаеновки {USER_NAME}, {USER_PASSWORD}, {USER_LOGIN}</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="email">Адрес получателя</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="userPassword">Пароль пользователя</param>
        /// <param name="userLogin">Логин пользователя</param>
        /// <returns></returns>
        public static bool SendWelcomeForNewCompany(Workarea wa, string email, string userName, string userPassword, string userLogin)
        {
            Workarea = wa;
            string body = "<p>Здравствуйте, {USER_NAME}!<br>Команда интернет-системы «Мой бизнес» благодарит Вас за регистрацию.<br>Ваш логин: {USER_LOGIN}<br>Ваш пароль: {USER_PASSWORD}<br>Для того чтобы начать работу в сервисе перейдите по <a href=\"http://www.moedelo.in.ua/mybiz/\">ссылке</a>. <br>Моё дело — это первый в Украине сервис для ведения бизнеса онлайн. Ознакомиться со всеми тарифными планами Вы можете на сайте в разделе <a href=\"http://www.moedelo.in.ua/prices\">«Тарифы»</a>.  \n"
           + "<p>Если у Вас возникнут какие-либо вопросы, обратитесь в службу поддержки по телефону: +38 050-261-9868 или по электронной почте: <a href=\"mailto:moedeloinua@mail.ru\">moedeloinua@mail.ru</a>. <br>Используйте Моё дело и будьте уверены, что Ваш бизнес работает как часы.<br>С уважением, команда интернет сервиса «Мой бизнес»! </p>";

            body = body.Replace("{USER_NAME}", userName).Replace("{USER_PASSWORD}", userPassword).Replace(
                "{USER_LOGIN}", userLogin);
            //return SendMessage(ConfigurationManager.AppSettings["adminEmail"], email, "Welcome message", body);
            return SendMessage(_wa.Cashe.SystemParameters.ItemCode<SystemParameter>("SYSTEMPARAMETER_ADMINEMAIL").ValueString, email, "Welcome message", body);
        }
        /// <summary>
        /// Сообщение на основе текстового шаблона
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="email">Адрес получателя</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="template">Шаблон</param>
        /// <returns></returns>
        public static bool SendTemplateMessage(Workarea wa, string email, string userName, string template)
        {
            Workarea = wa;
            string body = template;
            if (!string.IsNullOrEmpty(userName))
                body = body.Replace("{USER_NAME}", userName);
            return SendMessage(_wa.Cashe.SystemParameters.ItemCode<SystemParameter>("SYSTEMPARAMETER_ADMINEMAIL").ValueString, email, "Welcome message", body);
        }

        ///<summary>
        /// Сообщение о восстановлении пароля
        ///</summary>
        ///<param name="wa">Рабочая область</param>
        ///<param name="uid">Пользователь, которому восстанавливается пароль</param>
        ///<returns></returns>
        public static bool SendPassword(Workarea wa, Uid uid)
        {
            Workarea = wa;
            string body = "<p>Здравствуйте, {USER_NAME}!<br>Вы запросили восстановл­ение пароля на портале документооборота.<br>Ваш пароль - {USER_PASSWORD}</p>";
            body = body.Replace("{USER_NAME}", uid.Agent.Name).Replace("{USER_PASSWORD}", uid.Password);
            return SendMessage(_wa.Cashe.SystemParameters.ItemCode<SystemParameter>("SYSTEMPARAMETER_ADMINEMAIL").ValueString, uid.Email, "Восстановление пароля", body);
        }
    }
}
