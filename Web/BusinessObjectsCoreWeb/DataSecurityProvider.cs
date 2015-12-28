using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BusinessObjects.Security;

namespace BusinessObjects.Web.Core
{
    public static class DataSecurityProvider
    {
        private static Workarea wa;

        public static Workarea WA
        {
            get
            {
                if (wa == null)
                {
                    Uid uid = null;
                    wa = OpenDataBase(out uid);
                    wa.Period.SetYear(2011);
                }
                return wa;
            }
        }
        private static Workarea OpenDataBase(out Uid user)
        {
            string cnnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DocumentsSecurity"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cnnStr);

            user = new Uid { Name = builder.UserID, Password = string.Empty, AuthenticateKind = 0 };


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

            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
