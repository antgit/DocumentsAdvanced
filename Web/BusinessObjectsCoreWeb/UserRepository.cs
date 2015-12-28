using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Security;
using BusinessObjects.Security;

namespace BusinessObjects.Web.Core
{
    public class UserRepository
    {
        private static string GenerateKey()
        {
            Guid emailKey = Guid.NewGuid();

            return emailKey.ToString();
        }
        private static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(
                    saltAndPwd, "sha1");
            return hashedPwd;
        }
        public bool ValidateUser(string username, string password)
        {
            Uid dbuser = DataSecurityProvider.WA.Access.GetAllUsers().FirstOrDefault(f => f.Name == username); //DataSecurityProvider.WA.GetCollection<Uid>().FirstOrDefault(f => f.Name == username);
            if (dbuser != null)
            {
                dbuser.Refresh(true);
                if (dbuser.PasswordHash == CreatePasswordHash(password, dbuser.PasswordSalt))
                    return true;
                else
                    return false;
            }
            else
                return false;

            
        }
        public MembershipUser CreateUser(string username, string password, string email)
        {
            Uid user = new Uid { Workarea = DataSecurityProvider.WA };
            user.KindId = Uid.KINDID_USER;
            user.AuthenticateKind = (int)AuthenticateKind.NoLogin;
            user.Name = username;
            user.Password = password;
            user.PasswordSalt = CreateSalt();
            user.PasswordHash = CreatePasswordHash(password, user.PasswordSalt);
            user.Email = email;
            user.IsActivated = false;
            user.IsLockedOut = false;
            user.LastLockedOutDate = DateTime.Now;
            user.LastLoginDate = DateTime.Now;
            user.NewEmailKey = GenerateKey();

            user.Save();

            Uid defaultUserGroup = DataSecurityProvider.WA.GetCollection<Uid>().FirstOrDefault(f => f.Name == "Web пользователи");
            if (defaultUserGroup != null)
                user.IncludeInGroup(defaultUserGroup);
           

            return GetUser(username);

            
        }

        public string GetUserNameByEmail(string email)
        {
            Uid dbuser = DataSecurityProvider.WA.GetCollection<Uid>().FirstOrDefault(f => f.Email == email);
            if (dbuser != null)
                return dbuser.Name;
            else
                return string.Empty;

           
        }

        public MembershipUser GetUser(string username, bool userIsOnline=false)
        {
            // TODO:
            //WADataSecurityProvider.WA.Access.FindUserByName()
            Uid dbuser = DataSecurityProvider.WA.Access.GetAllUsers().FirstOrDefault(f => f.Name == username); //DataSecurityProvider.WA.GetCollection<Uid>().FirstOrDefault(f => f.Name == username);
            if (dbuser != null)
            {
                string _username = dbuser.Name;
                int _providerUserKey = dbuser.Id;
                string _email = dbuser.Email;
                string _passwordQuestion = dbuser.PasswordQuestion;
                string _comment = dbuser.Memo;
                bool _isApproved = dbuser.IsActivated;
                bool _isLockedOut = dbuser.IsLockedOut;

                DateTime _creationDate = DateTime.Today;
                if (dbuser.DateCreated.HasValue)
                    _creationDate = dbuser.DateCreated.Value;
                DateTime _lastLoginDate = DateTime.Today;
                if (dbuser.LastLoginDate.HasValue)
                    _lastLoginDate = dbuser.LastLoginDate.Value;

                DateTime _lastActivityDate = DateTime.Now;
                DateTime _lastPasswordChangedDate = DateTime.Today;
                if (dbuser.LastPasswordChangedDate.HasValue)
                    _lastPasswordChangedDate = dbuser.LastPasswordChangedDate.Value;

                DateTime _lastLockedOutDate = DateTime.Today;
                if (dbuser.LastLockedOutDate.HasValue)
                    _lastLockedOutDate = dbuser.LastLockedOutDate.Value;
                if (userIsOnline)
                    dbuser.SetLastActivityDate(null);
                MembershipUser user = new MembershipUser("BusinessObjectMembershipProvider",
                                                              _username,
                                                              _providerUserKey,
                                                              _email,
                                                              _passwordQuestion,
                                                              _comment,
                                                              _isApproved,
                                                              _isLockedOut,
                                                              _creationDate,
                                                              _lastLoginDate,
                                                              _lastActivityDate,
                                                              _lastPasswordChangedDate,
                                                              _lastLockedOutDate);

                return user;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Обновить данные о пользователе
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public MembershipUser RefreshUser(string username)
        {
            // TODO:
            //WADataSecurityProvider.WA.Access.FindUserByName()
            Uid dbuser = DataSecurityProvider.WA.Access.GetAllUsers(true).FirstOrDefault(f => f.Name == username); //DataSecurityProvider.WA.GetCollection<Uid>().FirstOrDefault(f => f.Name == username);
            if (dbuser != null)
            {
                string _username = dbuser.Name;
                int _providerUserKey = dbuser.Id;
                string _email = dbuser.Email;
                string _passwordQuestion = dbuser.PasswordQuestion;
                string _comment = dbuser.Memo;
                bool _isApproved = dbuser.IsActivated;
                bool _isLockedOut = dbuser.IsLockedOut;

                DateTime _creationDate = DateTime.Today;
                if (dbuser.DateCreated.HasValue)
                    _creationDate = dbuser.DateCreated.Value;
                DateTime _lastLoginDate = DateTime.Today;
                if (dbuser.LastLoginDate.HasValue)
                    _lastLoginDate = dbuser.LastLoginDate.Value;

                DateTime _lastActivityDate = DateTime.Now;
                DateTime _lastPasswordChangedDate = DateTime.Today;
                if (dbuser.LastPasswordChangedDate.HasValue)
                    _lastPasswordChangedDate = dbuser.LastPasswordChangedDate.Value;

                DateTime _lastLockedOutDate = DateTime.Today;
                if (dbuser.LastLockedOutDate.HasValue)
                    _lastLockedOutDate = dbuser.LastLockedOutDate.Value;

                MembershipUser user = new MembershipUser("BusinessObjectMembershipProvider",
                                                              _username,
                                                              _providerUserKey,
                                                              _email,
                                                              _passwordQuestion,
                                                              _comment,
                                                              _isApproved,
                                                              _isLockedOut,
                                                              _creationDate,
                                                              _lastLoginDate,
                                                              _lastActivityDate,
                                                              _lastPasswordChangedDate,
                                                              _lastLockedOutDate);

                return user;
            }
            else
            {
                return null;
            }
        }

        public MembershipUser GetUser(int id, bool userIsOnline = false)
        {
            // TODO:
            //WADataSecurityProvider.WA.Access.FindUserByName()
            Uid dbuser = DataSecurityProvider.WA.Access.GetAllUsers().FirstOrDefault(f => f.Id == id); //DataSecurityProvider.WA.GetCollection<Uid>().FirstOrDefault(f => f.Name == username);
            if (dbuser != null)
            {
                string _username = dbuser.Name;
                int _providerUserKey = dbuser.Id;
                string _email = dbuser.Email;
                string _passwordQuestion = dbuser.PasswordQuestion;
                string _comment = dbuser.Memo;
                bool _isApproved = dbuser.IsActivated;
                bool _isLockedOut = dbuser.IsLockedOut;

                DateTime _creationDate = DateTime.Today;
                if (dbuser.DateCreated.HasValue)
                    _creationDate = dbuser.DateCreated.Value;
                DateTime _lastLoginDate = DateTime.Today;
                if (dbuser.LastLoginDate.HasValue)
                    _lastLoginDate = dbuser.LastLoginDate.Value;

                DateTime _lastActivityDate = DateTime.Now;
                DateTime _lastPasswordChangedDate = DateTime.Today;
                if (dbuser.LastPasswordChangedDate.HasValue)
                    _lastPasswordChangedDate = dbuser.LastPasswordChangedDate.Value;

                DateTime _lastLockedOutDate = DateTime.Today;
                if (dbuser.LastLockedOutDate.HasValue)
                    _lastLockedOutDate = dbuser.LastLockedOutDate.Value;
                if (userIsOnline)
                    dbuser.SetLastActivityDate(null);
                MembershipUser user = new MembershipUser("BusinessObjectMembershipProvider",
                                                              _username,
                                                              _providerUserKey,
                                                              _email,
                                                              _passwordQuestion,
                                                              _comment,
                                                              _isApproved,
                                                              _isLockedOut,
                                                              _creationDate,
                                                              _lastLoginDate,
                                                              _lastActivityDate,
                                                              _lastPasswordChangedDate,
                                                              _lastLockedOutDate);

                return user;
            }
            else
            {
                return null;
            }
        }

        public Uid GetUid(string userName)
        {
            Uid dbuser = DataSecurityProvider.WA.Access.GetAllUsers().FirstOrDefault(f => f.Name.ToUpper() == userName.ToUpper()); //DataSecurityProvider.WA.GetCollection<Uid>().FirstOrDefault(f => f.Name.ToUpper() == userName.ToUpper());
            return dbuser;
        }

    }
}
