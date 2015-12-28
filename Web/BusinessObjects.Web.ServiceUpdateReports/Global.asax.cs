using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace BusinessObjects.Web.ServiceUpdateReports
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            try
            {

                HttpCookie authenCookie = Context.Request.Cookies.Get(FormsAuthentication.FormsCookieName);

                if (authenCookie == null) return;

                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authenCookie.Value);

                FormsIdentity id = new FormsIdentity(ticket);

                string[] astrRoles = ticket.UserData.Split(new char[] { ',' });

                GenericPrincipal principal = new GenericPrincipal(id, astrRoles);

                Context.User = principal;

            }

            catch (Exception ex)
            {

                System.IO.StreamWriter wr = new System.IO.StreamWriter(Context.Request.MapPath("log.txt"));

                wr.WriteLine(ex.Message);

                wr.Close();

            }


        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}