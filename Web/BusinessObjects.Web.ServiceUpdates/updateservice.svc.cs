using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Web;
using BusinessObjects.Security;

namespace BusinessObjects.Web
{
    public class updateservice : IUpdateService
    {
        //public CustomViewList[] GetCustomViewList()
        //{
        //    Uid uid = null;
        //    Workarea wa = OpenDataBase(out uid);
        //    return wa.GetCollection<CustomViewList>().Where(f=>f.IsSystem).ToArray();
        //    //DataContractSerializer dcs = new DataContractSerializer(typeof(Person));

        //}

        public WhatNew[] GetWhatNews()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return WhatNew.GetCollection(wa).ToArray();
        }
        private static Workarea OpenDataBase(out Uid user)
        {
            // Пользователь
            user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            // Соединяемся под интегрированной аутентификацией
            builder.IntegratedSecurity = true;
            // Текущий сервер - локальный, если необходимо меняем
            builder.DataSource = ".";
            // TODO: Использовать файл конфигурации
            //builder.DataSource = "SRV-DEVEL";
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

            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
