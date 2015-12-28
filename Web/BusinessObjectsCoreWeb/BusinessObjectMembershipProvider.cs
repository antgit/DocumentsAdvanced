using System;
using System.Linq;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using BusinessObjects.Security;

namespace BusinessObjects.Web.Core
{
    //http://books.google.ru/books?id=2wg5LCKuChcC&pg=PA1086&lpg=PA1086&dq=asp.net+retrive+UserIsOnlineTimeWindow&source=bl&ots=HjuffKvgJI&sig=upinfNfgn705iJwoV8l4cXLZzsI&hl=ru&sa=X&ei=aXZCT9WvBsnLtAaWsOTSBA#v=onepage&q&f=false
    //http://www.codeproject.com/Articles/22503/Custom-Membership-Role-Providers-Website-administr
    //http://www.eggheadcafe.com/community/asp-net/17/10239854/tracking-the-user-in-aspnet-using-sql-server.aspx
    public class BusinessObjectMembershipProvider : MembershipProvider
    {
        //
        // Properties from web.config, default all to False
        //
        private string _ApplicationName;
        private bool _EnablePasswordReset;
        private bool _EnablePasswordRetrieval = false;
        private bool _RequiresQuestionAndAnswer = false;
        private bool _RequiresUniqueEmail = true;
        private int _MaxInvalidPasswordAttempts;
        private int _PasswordAttemptWindow;
        private int _MinRequiredPasswordLength;
        private int _MinRequiredNonalphanumericCharacters;
        private string _PasswordStrengthRegularExpression;
        private MembershipPasswordFormat _PasswordFormat = MembershipPasswordFormat.Hashed;

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "BusinessObjectMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom Membership Provider");
            }

            base.Initialize(name, config);

            _ApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _MaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _PasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            _MinRequiredNonalphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonalphanumericCharacters"], "1"));
            _MinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "6"));
            _EnablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            _PasswordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));

        }

        public override string ApplicationName
        {
            get { return _ApplicationName; }
            set { _ApplicationName = value; }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if(string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(username))
                return false;

            Uid u = DataSecurityProvider.WA.Access.GetAllUsers(true).FirstOrDefault(f => f.Name == username);
            if(u!=null && u.Password==oldPassword)
            {
                u.Password = newPassword;
                u.Save();
                return true;
            }
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username,
                                                                    password,
                                                                    true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(username, false);

            if (u == null)
            {
                UserRepository _user = new UserRepository();

                _user.CreateUser(username, password, email);
                status = MembershipCreateStatus.Success;

                return GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return _EnablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _EnablePasswordRetrieval; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            return
                DataSecurityProvider.WA.Access.GetAllUsers().Count(
                    f =>
                    f.LastActivityDate.HasValue &&
                    f.LastActivityDate.Value.AddMinutes(Membership.UserIsOnlineTimeWindow) > DateTime.Now);

        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        /// <returns></returns>
        public MembershipUser GetUser()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(userName))
                return null;
            return GetUser(userName, true);
        }
        
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            UserRepository _user = new UserRepository();

            return _user.GetUser(username, userIsOnline);
        }
        public MembershipUser RefreshUser(string username)
        {
            UserRepository _user = new UserRepository();
            return _user.RefreshUser(username);
        }
        public void Refresh()
        {
            DataSecurityProvider.WA.GetCollection<Uid>(true);
            DataSecurityProvider.WA.Access.GetAllGroups(true);
            DataSecurityProvider.WA.Access.GetAllUsers(true);
        }
        public Uid GetUid(string username)
        {
            UserRepository _user = new UserRepository();
            return _user.GetUid(username);
        }
        /// <summary>
        /// Пользователь по ключу
        /// </summary>
        /// <param name="providerUserKey">Идентификатор пользователя</param>
        /// <param name="userIsOnline">Обновить данные о последней активности</param>
        /// <returns></returns>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            if (providerUserKey == null)
                return null;
            int id = 0;
            if (!int.TryParse(providerUserKey.ToString(), out id))
                return null;
            UserRepository _user = new UserRepository();
            return _user.GetUser(id);
        }

        public override string GetUserNameByEmail(string email)
        {
            UserRepository _user = new UserRepository();

            return _user.GetUserNameByEmail(email);
        }


        public override int MaxInvalidPasswordAttempts
        {
            get { return _MaxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _MinRequiredNonalphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _MinRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _PasswordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _PasswordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _PasswordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _RequiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _RequiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            UserRepository _user = new UserRepository();

            return _user.ValidateUser(username, password);
        }

        //
        // A helper function to retrieve config values from the configuration file.
        //  

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }
    }

}
