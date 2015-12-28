using System;
using System.Linq;
using System.Web.Security;
using BusinessObjects.Security;

namespace BusinessObjects.Web.Core
{
    public class BusinessObjectRoleProvider : RoleProvider
    {

        public override string[] GetRolesForUser(string username)
        {
            Console.WriteLine("GetUserRoles");
            //HttpContext.User.IsInRole
            return DataSecurityProvider.WA.Access.GetUserGroups(username).ToArray();

            //Uid user = WADataSecurityProvider.WA.GetCollection<Uid>().FirstOrDefault(f => f.Name == username);
            //if(user!=null)
            //{
            //    string[] roles = user.Groups.Select(ur => ur.Name).ToArray();
            //    return roles;
            //}
            //else
            //    return new string[] { };


            //using (DatabaseEntities db = new DatabaseEntities())
            //{
            //    User user = db.Users.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase) 
            //                                             || u.Email.Equals(username, StringComparison.CurrentCultureIgnoreCase)); 
            //    var roles = from ur in user.UserRoles from r in db.Roles 
            //                where ur.RoleId == r.Id select r.Name; 
            //    if (roles != null)
            //        return roles.ToArray(); 
            //    else
            //        return new string[] { }; ;
            //}
        }

        public override void CreateRole(string roleName)
        {
            DataSecurityProvider.WA.Access.CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return
                DataSecurityProvider.WA.GetCollection<Uid>().Any(
                    f => f.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                foreach (string roleName in roleNames)
                {
                    DataSecurityProvider.WA.Access.IncludeInGroup(username, roleName);
                }
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                foreach (string roleName in roleNames)
                {
                    DataSecurityProvider.WA.Access.ExcludeFromGroup(username, roleName);
                }
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return DataSecurityProvider.WA.Access.GetAllGroups(true).Select(s => s.Name).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return DataSecurityProvider.WA.Access.IsUserExistsInGroup(username, roleName);

            
        }
    }
}
