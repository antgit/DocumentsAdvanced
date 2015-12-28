using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessObjects.Developer;
using BusinessObjects.Security;

namespace BusinessObjects.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceUpdate" in code, svc and config file together.
    
    public class ServiceUpdate : IServiceUpdate
    {
        public CustomViewList[] CustomViewLists()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<CustomViewList>().Where(f=>f.IsSystem).ToArray();
        }

        public CodeName[] CodeNames()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<CodeName>().Where(f => f.IsSystem).ToArray();
        }
        public Currency[] Currencies()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Currency>().Where(f => f.IsSystem).ToArray();
        }
        public Account[] Accounts()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Account>().Where(f => f.IsSystem).ToArray();
        }
        public Country[] Countries()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Country>().Where(f => f.IsSystem).ToArray();
        }

        public DataCatalog[] DataCatalogs()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<DataCatalog>().Where(f => f.IsSystem).ToArray();
        }

        public Folder[] Folders()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Folder>().Where(f => f.IsSystem).ToArray();
        }

        public Hierarchy[] Hierarchies()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Hierarchy>().Where(f => f.IsSystem).ToArray();
        }

        public Knowledge[] Knowledges()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Knowledge>().Where(f => f.IsSystem).ToArray();
        }

        public Library[] Libraries()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Library>().Where(f => f.IsSystem).ToArray();
        }

        public Message[] Messages()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Message>().Where(f => f.IsSystem).ToArray();
        }

        public Note[] Notes()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Note>().Where(f => f.IsSystem).ToArray();
        }
        public PriceName[] PriceNames()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<PriceName>().Where(f => f.IsSystem).ToArray();
        }
        public Product[] Products()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Product>().Where(f => f.IsSystem).ToArray();
        }
        public Rate[] Rates()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Rate>().Where(f => f.IsSystem).ToArray();
        }

        public StorageCell[] StorageCells()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<StorageCell>().Where(f => f.IsSystem).ToArray();
        }

        public Town[] Towns()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Town>().Where(f => f.IsSystem).ToArray();
        }


        public XmlStorage[] XmlStorages()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<XmlStorage>().Where(f => f.IsSystem).ToArray();
        }

        public EntityType[] EntityTypes()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.CollectionEntities.ToArray();
        }
        public EntityDocument[] EntityDocuments()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.CollectionDocumentTypes().ToArray();
        }

        public FactName[] FactNames()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<FactName>().Where(f => f.IsSystem).ToArray();
        }

        public DbObject[] DbObjects()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<DbObject>().Where(f => f.IsSystem).ToArray();
        }

        public ChainKind[] ChainKinds()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<ChainKind>().Where(f => f.IsSystem).ToArray();
        }

        public Branche[] Branches()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<Branche>().Where(f => f.IsSystem).ToArray();
        }

        public ResourceString[] ResourceStrings()
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            return wa.GetCollection<ResourceString>().Where(f => f.IsSystem).ToArray();
        }

        private static Workarea OpenDataBase(out Uid user)
        {
            string cnnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Document2011"].ConnectionString;
            // Пользователь
            //user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cnnStr);
            //user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            user = new Uid { Name = builder.UserID, Password = string.Empty, AuthenticateKind = 0 };
            // Соединяемся под интегрированной аутентификацией
            //builder.IntegratedSecurity = true;
            // Текущий сервер - локальный, если необходимо меняем
            //builder.DataSource = ".";
            // TODO: Использовать файл конфигурации
            //builder.DataSource = "SRV-DEVEL";
            // Имя базы данных
            //builder.InitialCatalog = "Documents2011";
            //builder.UserID = user.Name;
            //builder.Password = user.Password;
            //builder.IntegratedSecurity = user.AuthenticateKind == 1;

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
