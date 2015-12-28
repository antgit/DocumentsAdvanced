using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessObjects.Developer;

namespace BusinessObjects.Web
{
    // http://msdn.microsoft.com/ru-RU/library/dd699756.aspx

    //http://www.xmarks.com/site/www.codeproject.com/KB/WCF/WCFServiceSample.aspx
    //http://msdn.microsoft.com/en-us/netframework/dd728058
    //http://www.codeproject.com/KB/architecture/wcfbyexample_introduction.aspx
    //http://msdn.microsoft.com/en-us/library/ms734765.aspx
    //http://keithelder.net/2008/01/17/Exposing-a-WCF-Service-With-Multiple-Bindings-and-Endpoints/
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceUpdate" in both code and config file together.
    [ServiceContract(Namespace = "http://docssys.com/biservices")]
    public interface IServiceUpdate
    {
        [OperationContract]
        CustomViewList[] CustomViewLists();
        
        [OperationContract]
        CodeName[] CodeNames();
        
        [OperationContract]
        Currency[] Currencies();
        
        [OperationContract]
        Account[] Accounts();

        [OperationContract]
        Country[] Countries();
        [OperationContract]
        DataCatalog[] DataCatalogs();

        [OperationContract]
        Folder[] Folders();

        [OperationContract]
        Hierarchy[] Hierarchies();

        [OperationContract]
        Knowledge[] Knowledges();

        [OperationContract]
        Library[] Libraries();

        [OperationContract]
        Message[] Messages();

        [OperationContract]
        Note[] Notes();

        [OperationContract]
        PriceName[] PriceNames();

        [OperationContract]
        Product[] Products();

        [OperationContract]
        Rate[] Rates();

        [OperationContract]
        StorageCell[] StorageCells();

        [OperationContract]
        Town[] Towns();

        //[OperationContract]
        //WhatNew[] WhatNews();

        [OperationContract]
        XmlStorage[] XmlStorages();

        [OperationContract]
        EntityType[] EntityTypes();

        [OperationContract]
        EntityDocument[] EntityDocuments();

        [OperationContract]
        FactName[] FactNames();

        [OperationContract]
        DbObject[] DbObjects();

        [OperationContract]
        ChainKind[] ChainKinds();
        
        [OperationContract]
        Branche[] Branches();
        
        [OperationContract]
        ResourceString[] ResourceStrings();
    }
}
