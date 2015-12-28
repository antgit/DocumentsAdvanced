using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;
using BusinessObjects.ReportingService;

//http://dotnetslackers.com/articles/aspnet/Securing-ASP-Net-Web-Services-with-Forms-Authentication.aspx
//http://progtutorials.tripod.com/Authen.htm
namespace BusinessObjects.Web.ServiceUpdateReports
{
    /*
/// <summary>
/// Soap Header for the Secured Web Service.
/// Username and Password are required for AuthenticateUser(),
///   and AuthenticatedToken is required for everything else.
/// </summary>
    public class SecuredWebServiceHeader : System.Web.Services.Protocols.SoapHeader
    {
        public string Username;
        public string Password;
        public string AuthenticatedToken;

    }
    */
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://atlantsoft/services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UpdateReports2011 : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetContents()
        {
            if (!Context.User.IsInRole("RoleReportUpdate"))
                return string.Empty;
            string folder = GetLastFolder();
            string loadPath = System.IO.Path.Combine(folder, "Content.xml");

            StreamReader sr = new StreamReader(loadPath);
            string ret = new StringReader(sr.ReadToEnd()).ReadToEnd();

            //XmlReader reader = new XmlTextReader(Path.Combine(loadPath, "Content.xml"));
            //XmlSerializer serializer = new XmlSerializer(typeof(RSSerializedData));
            //RSSerializedData sd = (RSSerializedData)serializer.Deserialize(reader);
            //reader.Read
            //reader.Close();
            return ret;
        }
        [WebMethod]
        public string GetItem(string path)
        {
            if (!Context.User.IsInRole("RoleReportUpdate"))
                return string.Empty;
            string folder = GetLastFolder();
            path = path.Trim('/');
            string loadPath = System.IO.Path.Combine(folder, path.Replace(@"/", @"\"));
            if(!System.IO.File.Exists(loadPath))
            {
                return string.Empty;
            }
            else
            {
                StreamReader sr = new StreamReader(loadPath);
                string ret = new StringReader(sr.ReadToEnd()).ReadToEnd();
                sr.Close();

                return ret;
            }
        }
        string GetLastFolder()
        {
            if (!Context.User.IsInRole("RoleReportUpdate"))
                return string.Empty;
            string root = GetRootFolder();
            DirectoryInfo directories = new DirectoryInfo(root);
            DirectoryInfo[] folderList = directories.GetDirectories();
            DateTime value = DateTime.MinValue;
            DateTime valueMax = DateTime.MinValue;
            DirectoryInfo diMax = null;
            foreach (DirectoryInfo di in folderList)
            {
                if(di.Name.Length==19)
                {
                    //18.05.2011 12.04.21
                    int Y = Convert.ToInt32(di.Name.Substring(6, 4));
                    int dd = Convert.ToInt32(di.Name.Substring(0,2));
                    int MM = Convert.ToInt32(di.Name.Substring(3,2));

                    int HH = Convert.ToInt32(di.Name.Substring(11, 2));
                    int mm = Convert.ToInt32(di.Name.Substring(14, 2));
                    int ss = Convert.ToInt32(di.Name.Substring(17, 2));

                    value = new DateTime(Y, MM, dd, HH, mm, ss);
                    if (value > valueMax)
                    {
                        valueMax = value;
                        diMax = di;
                    }
                }
            }
            
            return diMax.FullName;
        }
        string GetRootFolder()
        {
            return Server.MapPath("/Services/ReportUpdates");

            //XmlTextReader xrdr = new XmlTextReader(Server.MapPath("~/books.xml"));
            //DataSet ds = new DataSet();
            //ds.ReadXml(xrdr);
            //return ds;
        }
        private string AuthenticateUser(string strUser, string strPwd)
        {
            bool aut = System.Web.Security.Membership.ValidateUser("sadmin", "123456789~");
            if (aut)
                return "RoleReportUpdate";
            else
                return null;
            //if (strUser == "sadmin" && strPwd == "123456789~")
            //    return "RoleReportUpdate";
            ////else if (strUser == "yang" && strPwd == "password")
            ////    return "yang's role";
            //else
            //    return null;
        }


        [WebMethod]
        public bool Login(string strUser, string strPwd)
        {
            string strRole = AuthenticateUser(strUser, strPwd);

            if (strRole != null)
            {

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                   1,                            // version
                   strUser,                      // user name
                   DateTime.Now,                 // create time
                   DateTime.Now.AddSeconds(30),  // expire time
                   false,                        // persistent
                   strRole);                     // user data

                string strEncryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strEncryptedTicket);
                Context.Response.Cookies.Add(cookie);
                return true;
            }
            else
                return false;

        }

        [WebMethod]
        public void LogOut()
        {
            // Deprive client of the authentication key
            FormsAuthentication.SignOut();
        }


        /*
        public SecuredWebServiceHeader SoapHeader;
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string AuthenticateUser()
        {
            if (SoapHeader == null)
                return "Please provide a Username and Password";
            if (string.IsNullOrEmpty(SoapHeader.Username) || string.IsNullOrEmpty(SoapHeader.Password))
                return "Please provide a Username and Password";
            // Are the credentials valid?
            if (!IsUserValid(SoapHeader.Username, SoapHeader.Password))
                return "Invalid Username or Password";
            // Create and store the AuthenticatedToken before returning it
            string token = Guid.NewGuid().ToString();
            HttpRuntime.Cache.Add(token,
                SoapHeader.Username,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                TimeSpan.FromMinutes(60),
                System.Web.Caching.CacheItemPriority.NotRemovable,
                null);

            return token;
        }

        private bool IsUserValid(string Username, string Password)
        {

            // TODO: Implement Authentication

            return true;

        }
        private bool IsUserValid(SecuredWebServiceHeader SoapHeader)
        {
            if (SoapHeader == null)
                return false;
            // Does the token exists in our Cache?
            if (!string.IsNullOrEmpty(SoapHeader.AuthenticatedToken))
                return (HttpRuntime.Cache[SoapHeader.AuthenticatedToken] != null);
            return false;
        }

        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string HelloWorld()
        {

            if (!IsUserValid(SoapHeader))

                return "Please call AuthenitcateUser() first.";

            return "Hello " + HttpRuntime.Cache[SoapHeader.AuthenticatedToken];

        }
        */
    }
}