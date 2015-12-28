using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BusinessObjects.Web
{
    //[ServiceContract]
    [ServiceContract(Namespace = "BusinessObjects.Web", SessionMode = SessionMode.Required)]
    public interface IWebOrdersService
    {
        [OperationContract]
        bool Login(string userName, string password);
        ///// <summary>
        ///// Обновление товара и цен
        ///// </summary>
        ///// <param name="guid"></param>
        ///// <param name="name"></param>
        ///// <param name="memo"></param>
        ///// <param name="price"></param>
        //[OperationContract(IsInitiating = false)]
        //void UpdateProduct(Guid guid, string name, string memo, decimal price);
        [OperationContract(IsInitiating = false)]
        int CreateProduct(Guid guid, string name, string nomenclature, decimal price, string brend, string tradeMark, string productType);
        [OperationContract(IsInitiating = false)]
        void CreateProductEnd();
        [OperationContract(IsInitiating = false)]
        int CreateBrand(Guid guid, string name);
        [OperationContract(IsInitiating = false)]
        int CreateTradeMark(Guid guid, string name);
        [OperationContract(IsInitiating = false)]
        int CreateProductType(Guid guid, string name);
        [OperationContract(IsInitiating = false)]
        int CreateAnalitic(Guid guid, string name, int kindId);
    }
}
