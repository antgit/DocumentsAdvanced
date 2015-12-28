using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml;
using BusinessObjects;
using BusinessObjects.Workflows.Documents;
using BusinessObjects.Workflows.Web;

namespace BusinessObjects.Workflows
{ 
    /// <summary>
    /// Дополнительные методы работы в WF
    /// </summary>
    public static class WfCore
    {
// ReSharper disable InconsistentNaming

        public const string WF_PriceListCommandToPriceList = "PRICELISTCOMMANDTOPRICELIST";
        public const string WF_PriceListCommandIndToPriceListInd = "PRICELISTCOMMANDINDTOPRICELISTIND";
        
        public const string WF_SalesInventoryToActOut = "SALESINVENTORYTOACTOUT";
        public const string WF_SalesInventoryToActIn = "SALESINVENTORYTOACTIN";

        public const string WF_SalesInToMove = "SALESINTOMOVE";
        public const string WF_SalesAccountInToMoneyOut = "SALESACCOUNTINTOMONEYOUT";
        public const string WF_SaleAccountOutToMoneyIn = "SALEACCOUNTOUTTOMONEYIN";
        public const string WF_SalesOutToMoneyIn = "SALESOUTTOMONEYIN";
        public const string WF_SalesOutToStoreOut = "SALESOUTTOSTOREOUT";
        public const string WF_SalesAccountInToSaleIn = "SALESACCOUNTINTOSALESIN";
        public const string WF_SalesAccountOutToSaleOut = "SALESACCOUNTOUTTOSALESOUT";
        public const string WF_SalesAssortimentToOrderIn = "SALESASSORTIMENTTOORDERIN";
        public const string WF_SalesAssortimentToOrderOut = "SALESASSORTIMENTTOORDEROUT";
        public const string WF_SalesAssortimentToPriceList = "SALESASSORTIMENTTOPRICELIST";
        public const string WF_SalesAssortimentToSaleAccountOut = "SALESASSORTIMENTTOSALEACCOUNTOUT";
        public const string WF_SalesAssortimentToSaleOut = "SALESASSORTIMENTTOSALESOUT";
        public const string WF_SalesInToOrderOut = "SALESINTOSALESORDEROUT";
        public const string WF_SalesInToPriceList = "SALESINTOPRICELIST";
        public const string WF_SalesInToSalesAccountIn = "SALESINTOSALESACCOUNTIN";
        public const string WF_SalesInToSalesReturnSupplyer = "SALESINTOSALESRETURNSUPPLYER";
        public const string WF_SalesInToSalesOrderIn = "SALESINTOSALESORDERIN";
        public const string WF_SalesInToTaxIn = "SALESINTOTAXIN";
        public const string WF_SalesOrderInToAccountOut = "SALESORDERINTOACCOUNTOUT";
        public const string WF_SalesOrderInToSaleOut = "SALESORDERINTOSALESOUT";
        public const string WF_SalesOrderOutToSaleAccountIn = "SALESORDEROUTTOSALESACCOUNTIN";
        public const string WF_SalesOrderOutToSaleIn = "SALESORDEROUTTOSALESIN";
        public const string WF_SalesOutToPriceList = "SALESOUTTOPRICELIST";
        public const string WF_SalesOutToSalesOrderOut = "SALESOUTTOSALESORDEROUT";
        public const string WF_SalesOutToSalesReturnByer = "SALESOUTTOSALESRETURNBYER";
        public const string WF_SalesOutToSalesAccount = "SALESOUTTOSALESACCOUNT";
        public const string WF_SalesOutToTaxOut = "SALESOUTTOTAXOUT";
        public const string WF_StoreInMove = "STOREINMOVE";
        public const string WF_StoreInReturnSupplyer = "STOREINRETURNSUPPLYER";
        public const string WF_StoreOutReturnReturnByer = "STOREOUTRETURNRETURNBYER";

        public const string WF_ServicesAccountInToActIn = "SERVICESACCOUNTINTOACTIN";
        public const string WF_ServicesAccountInToMoneyOut = "SERVICESACCOUNTINTOMONEYOUT";
        public const string WF_ServicesAccountOutToActOut = "SERVICESACCOUNTOUTTOACTOUT";
        public const string WF_ServicesAccountOutToMoneyIn = "SERVICESACCOUNTOUTTOMONEYIN";
        public const string WF_ServicesActInToMoneyOut = "SERVICESACTINTOMONEYOUT";
        public const string WF_ServicesActInToTaxIn = "SERVICESACTINTOTAXIN";
        public const string WF_ServicesActOutToMoneyIn = "SERVICESACTOUTTOMONEYIN";
        public const string WF_ServicesActOutToTaxOut = "SERVICESACTOUTTOTAXOUT";
        public const string WF_ServicesOrderInToAccountOut = "SERVICESORDERINTOACCOUNTOUT";
        public const string WF_ServicesOrderOutToAccountIn = "SERVICESORDEROUTTOACCOUNTIN";

        public const string WFA_ActionsSalesOutRecalcNds = "ACTIONSSALESOUTRECALCNDS";
        public const string WFA_ActionsSalesInRecalcNds = "ACTIONSSALESINRECALCNDS";
        public const string WFA_ActionsServicesInRecalcNds = "ACTIONSSERVICESRECALCNDS";
        [Obsolete("Больше не использовать, удалить в базе")]
        public const string WFA_ActivityShowProductInfo = "ACTIVITYSHOWPRODUCTINFO";
        
// ReSharper restore InconsistentNaming
        
        /// <summary>Поиск системного WF в текущей библиотеке по коду</summary>
        /// <param name="code">Код</param>
        public static Activity FindByCodeInternal(string code)
        {
            code = code.ToUpper();
            Activity value=null;
            switch (code)
            {
                case WF_PriceListCommandIndToPriceListInd:
                    value = new PriceListCommandIndToPriceListInd();
                    break;
                case WF_PriceListCommandToPriceList:
                    value = new PriceListCommandToPriceList();
                    break;
                case "ACTIVITYREGISTERNEWCOMPANY":
                    value = new ActivityRegisterNewCompany();
                    break;
                case WF_SalesInventoryToActOut:
                    value = new SalesInventoryToActOut();
                    break;
                case WF_SalesInventoryToActIn:
                    value = new SalesInventoryToActIn();
                    break;
                case WF_SalesInToMove:
                    value = new SalesInToMove();
                    break;
                    
                case WFA_ActionsServicesInRecalcNds:
                    value = new CustomActions.ActionServicesRecalcNds();
                    break;
                case WF_SalesAccountInToMoneyOut:
                    value = new SalesAccountInToMoneyOut();
                    break;
                case WF_SaleAccountOutToMoneyIn:
                    value = new SaleAccountOutToMoneyIn();
                    break;
                //case WFA_ActivityShowProductInfo:
                //    value = new ActivityShowProductInfo();
                //    break;
                case WFA_ActionsSalesInRecalcNds:
                    value = new CustomActions.ActionsSalesInRecalcNds();
                    break;
                case WFA_ActionsSalesOutRecalcNds:
                    value = new CustomActions.ActionsSalesOutRecalcNds();
                    break;
                case WF_SalesOutToMoneyIn:
                    value = new SalesOutToMoneyIn();
                    break;
                case WF_SalesOutToStoreOut:
                    value = new SalesOutToStoreOut();
                    break;
                case WF_SalesAccountInToSaleIn:
                    value = new SalesAccountInToSalesIn();
                    break;
                case WF_SalesAccountOutToSaleOut:
                    value = new SalesAccountOutToSalesOut();
                    break;
                case WF_SalesAssortimentToOrderIn:
                    value = new SalesAssortimentToOrderIn();
                    break;
                case WF_SalesAssortimentToOrderOut:
                    value = new SalesAssortimentToOrderOut();
                    break;
                case WF_SalesAssortimentToPriceList:
                    value = new SalesAssortimentToPriceList();
                    break;
                case WF_SalesAssortimentToSaleAccountOut:
                    value = new SalesAssortimentToSaleAccountOut();
                    break;
                case WF_SalesAssortimentToSaleOut:
                    value = new SalesAssortimentToSalesOut();
                    break;
                case WF_SalesInToOrderOut:
                    value = new SalesInToSalesOrderOut();
                    break;
                case WF_SalesInToPriceList:
                    value = new SalesInToPriceList();
                    break;
                case WF_SalesInToSalesAccountIn:
                    value = new SalesInToSalesAccountIn();
                    break;
                case WF_SalesInToSalesReturnSupplyer:
                    value = new SalesInToSalesReturnSupplyer();
                    break;
                case WF_SalesInToSalesOrderIn:
                    value = new SalesInToSalesOrderIn();
                    break;
                case WF_SalesInToTaxIn:
                    value = new SalesInToTaxIn();
                    break;
                case WF_SalesOrderInToAccountOut:
                    value = new SalesOrderInToAccountOut();
                    break;
                case WF_SalesOrderOutToSaleIn:
                    value = new SalesOrderOutToSalesIn();
                    break;
                case WF_SalesOrderOutToSaleAccountIn:
                    value = new SalesOrderOutToSalesAccountIn();
                    break;
                case WF_SalesOutToPriceList:
                    value = new SalesOutToPriceList();
                    break;
                case WF_SalesOutToSalesOrderOut:
                    value = new SalesOutToSalesOrderOut();
                    break;
                case WF_SalesOutToSalesReturnByer:
                    value = new SalesOutToSalesReturnByer();
                    break;
                case WF_SalesOutToSalesAccount:
                    value = new SalesOutToSalesAccount();
                    break;
                case WF_SalesOutToTaxOut:
                    value = new SalesOutToTaxOut();
                    break;
                case WF_StoreInMove:
                    value = new StoreInMove();
                    break;
                case WF_StoreInReturnSupplyer:
                    value = new StoreInReturnSupplyer();
                    break;
                case WF_SalesOrderInToSaleOut:
                    value = new SalesOrderInToSalesOut();
                    break;
                case WF_StoreOutReturnReturnByer:
                    value = new StoreOutReturnReturnByer();
                    break;

                case WF_ServicesAccountInToActIn:
                    value = new ServicesAccountInToActIn();
                    break;
                case WF_ServicesAccountInToMoneyOut:
                    value = new ServicesAccountInToMoneyOut();
                    break;
                case WF_ServicesAccountOutToActOut:
                    value = new ServicesAccountOutToActOut();
                    break;
                case WF_ServicesAccountOutToMoneyIn:
                    value = new ServicesAccountOutToMoneyIn();
                    break;
                case WF_ServicesActInToMoneyOut:
                    value = new ServicesActInToMoneyOut();
                    break;
                case WF_ServicesActInToTaxIn:
                    value = new ServicesActInToTaxIn();
                    break;
                case WF_ServicesActOutToMoneyIn:
                    value = new ServicesActOutToMoneyIn();
                    break;
                case WF_ServicesActOutToTaxOut:
                    value = new ServicesActOutToTaxOut();
                    break;
                case WF_ServicesOrderInToAccountOut:
                    value = new ServicesOrderInToAccountOut();
                    break;
                case WF_ServicesOrderOutToAccountIn:
                    value = new ServicesOrderOutToAccountIn();
                    break;

            }
            return value;
        }
        /// <summary>Поиск WF в базе данных</summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="code">Код</param>
        public static Activity FindByCode(Workarea wa, string code)
        {
            Ruleset obj = wa.Cashe.GetCasheData<Ruleset>().ItemCode<Ruleset>(code);
            if (obj == null)
                throw new ApplicationException(string.Format("Не найден рабочий процесс с кодом {0}", code));
            //Activity value = ActivityXamlServices.Load(obj.ValueToStream());

            Activity value = ActivityXamlServices.Load(ActivityXamlServices.CreateReader(new XamlXmlReader(obj.ValueToStream(), new XamlXmlReaderSettings { LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly() })));
            //Activity workflow = ActivityXamlServices.Load(ActivityXamlServices.CreateReader(new XamlXmlReader(@"../../Workflow1.xaml", new XamlXmlReaderSettings { LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly() }))); 
            return value;
        }
    }
}
