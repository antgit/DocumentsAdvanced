using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using BusinessObjects.Documents;
using BusinessObjects.Security;

namespace BusinessObjects.Web
{
    // Служба доступа к данным для Web заказов.
    // Общие принципы работы с Web службой:
    // 1. Проводится синхронизация аналитических данных: бренды, товарные группы, вид продукции.
    // 2. Проводиться синхронизация данных о товарах.
    // 3. Проводиться синхронизация данных о ценах.
    // 4. Проводится синхронизация данных о документах созданных в системе Web заказы в произвольную систему путем выгрузки списка документов

    //http://www.dotnetspider.com/resources/34519-WCF-Forms-authentication.aspx
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class WebOrdersService : IWebOrdersService
    {
        private string _userName = string.Empty;

        private Workarea _workarea;
        private Workarea workarea
        {
            get
            {
                if(_workarea==null)
                {
                    Uid uid = null;
                    _workarea = OpenDataBase(out uid);
                }
                return _workarea;
            }
        }

        private DocumentPrices _priceDoc;
        private DocumentPrices PriceDoc
        {
            get
            {
                if(_priceDoc==null)
                {
                    Document document = workarea.Empty<Document>().FindBy(code: "IMPORTFROMACCENT").FirstOrDefault();
                    if ((document == null) || (document.Id == 0))
                        throw new Exception("Документ 'IMPORTFROMACCENT' не найден!");

                    DocumentPrices priceDoc = new DocumentPrices { Workarea = workarea };
                    priceDoc.Load(document.Id);
                    _priceDoc = priceDoc;
                }
                return _priceDoc;
            }
        }

        public bool Login(string userName, string password)
        {
            if (Membership.ValidateUser(userName, password))
            {
                _userName = userName;
                FormsAuthentication.SetAuthCookie(userName, true);
                return true;
            }

            return false;
        }

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
        
        private Product FindProductByNomenclature(string nom)
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            Product val = wa.Empty<Product>().GetCollectionByNomenclature(wa, Product.KINDID_PRODUCT, nom, 1).FirstOrDefault();
            return val;
        }

        private Product FindProductByGuid(Guid guid)
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            Product val = wa.GetObject<Product>(guid);
            return val;
        }

        private Product FindProductByName(string name)
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            Product val = wa.Empty<Product>().FindBy(name: name).FirstOrDefault();
            return val;
        }

        private Analitic FindAnaliticByGuid(Guid guid)
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            Analitic val = wa.GetObject<Analitic>(guid);
            return val;
        }

        private Analitic FindAnalitic(string name, int kindId)
        {
            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            Analitic val = wa.Empty<Analitic>().FindBy(name: name).FirstOrDefault(s=>s.KindId==kindId);
            return val;
        }

        public int CreateProduct(Guid guid, string name, string nomenclature, decimal price, string brand, string tradeMark, string productType)
        {
            if (_userName == string.Empty)
                return -1;
            //if (!HttpContext.Current.User.Identity.IsAuthenticated)
            //    return;

            int res = 0;
            //Uid uid = null;
            //Workarea wa = OpenDataBase(out uid);
            //Поиск по Guid
            Product product = FindProductByGuid(guid);
            //Поиск по номенклатуре
            if((product==null)||(product.Id==0))
                product = FindProductByNomenclature(nomenclature);
            //Поиск по имени
            if ((product == null) || (product.Id == 0))
                product = FindProductByName(name);

            if((product==null)||(product.Id==0))
            {
                product = new Product
                              {
                                  Workarea = workarea,
                                  KindId = Product.KINDID_PRODUCT,
                                  Guid = guid,
                                  Name = name,
                                  Nomenclature = nomenclature,
                                  StateId = State.STATEACTIVE
                              };
                res = 1;
            }

            if(!string.IsNullOrEmpty(brand))
            {
                Analitic objBrend = FindAnalitic(brand, Analitic.KINDID_BRAND);
                if ((objBrend != null) && (objBrend.Id != 0))
                {
                    product.BrandId = objBrend.Id;
                }
            }

            if (!string.IsNullOrEmpty(tradeMark))
            {
                Analitic objTradeMark = FindAnalitic(tradeMark, Analitic.KINDID_TRADEGROUP);
                if ((objTradeMark != null) && (objTradeMark.Id != 0))
                {
                    product.TradeMarkId = objTradeMark.Id;
                }
            }

            if (!string.IsNullOrEmpty(productType))
            {
                Analitic objproductType = FindAnalitic(productType, Analitic.KINDID_PRODUCTTYPE);
                if ((objproductType != null) && (objproductType.Id != 0))
                {
                    product.ProductTypeId = objproductType.Id;
                }
            }

            product.Save();

            //Обновление цены
            
            DocumentDetailPrice detail = PriceDoc.Details.FirstOrDefault(s => s.ProductId == product.Id);

            if(detail==null)
            {
                detail = new DocumentDetailPrice
                             {
                                 Workarea = workarea,
                                 ProductId = product.Id,
                                 StateId = State.STATEACTIVE,
                                 Document = PriceDoc
                             };
                PriceDoc.Details.Add(detail);
            }

            detail.Value = price;
            
            //Добавление в иерархию
            //if (res == 1)
            {
                Hierarchy h = workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("ROOTPRODUCTS");
                if (h == null)
                    throw new Exception("Иерархия 'ROOTPRODUCTS' не найдена!");
                h.ContentAdd(product);
            }

            return res;
        }

        public void CreateProductEnd()
        {
            PriceDoc.Save();
        }

        public int CreateBrand(Guid guid, string name)
        {
            return CreateAnalitic(guid, name, Analitic.KINDID_BRAND);
        }

        public int CreateTradeMark(Guid guid, string name)
        {
            return CreateAnalitic(guid, name, Analitic.KINDID_TRADEGROUP);
        }

        public int CreateProductType(Guid guid, string name)
        {
            return CreateAnalitic(guid, name, Analitic.KINDID_PRODUCTTYPE);
        }

        public int CreateAnalitic(Guid guid, string name, int kindId)
        {
            if (_userName == string.Empty)
                return -1;

            Uid uid = null;
            Workarea wa = OpenDataBase(out uid);
            //Поиск по Guid
            Analitic analitic = FindAnaliticByGuid(guid);
            //Поиск по номенклатуре
            if ((analitic == null) || (analitic.Id == 0))
                analitic = FindAnalitic(name, kindId);

            if ((analitic == null) || (analitic.Id == 0))
            {
                analitic = new Analitic
                {
                    Workarea = wa,
                    KindId = kindId,
                    Guid = guid,
                    Name = name
                };
                analitic.Save();
                return 1;
            }
            return 0;
        }
    }
}
