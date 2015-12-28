using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessObjects.Security;

namespace BusinessObjects.Web
{
    /*
     http://www.fiddler2.com/Fiddler2/firstrun.asp
     * http://msdn.microsoft.com/en-us/library/ms732015.aspx
     * http://sankarsan.wordpress.com/2010/06/06/fileless-activation-of-wcf-service-in-net-4-0/
     */

    /*
     WCF file
     * 
     * http://www.codeproject.com/KB/WCF/WCF_FileTransfer_Progress.aspx
     * http://www.a2zdotnet.com/View.aspx?Id=188
     * http://stefanoricciardi.com/2010/09/02/file-transfer-with-wcf-part-iii/
     * http://blogs.msdn.com/b/carlosfigueira/archive/2008/04/17/wcf-raw-programming-model-web.aspx
     * http://geekswithblogs.net/michelotti/archive/2010/08/21/restful-wcf-services-with-no-svc-file-and-no-config.aspx
     * http://kjellsj.blogspot.com/2007/02/wcf-streaming-upload-files-over-http.html
     */

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceDocuments" in code, svc and config file together.
    public class ServiceDocuments : IServiceDocuments
    {
        //public WhatNew[] GetWhatNews()
        //{
        //    Uid uid = null;
        //    Workarea wa = OpenDataBase(out uid);
        //    return WhatNew.GetCollection(wa).ToArray();
        //}

        public CustomViewList[] GetCustomViewList()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<CustomViewList>().ToArray();
        }
        /// <summary>
        /// Рабочая область
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        private static Workarea OpenDataBase(out Uid user)
        {
            string cnnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Document2011"].ConnectionString;
            // Пользователь
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
