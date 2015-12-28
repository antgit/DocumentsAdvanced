using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Developer;
using BusinessObjects.Documents;
using BusinessObjects.Models;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>
    /// Управляемый кеш данных
    /// </summary>
    public class WorkareaCashe
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        public WorkareaCashe(IWorkarea wa)
        {
            _workarea = wa;
            _cashedCollections = new Dictionary<Type, object>();
            _cashedChainCollections = new Dictionary<Type, object>();
            _cashedListCodeCasheDataCollections = new Dictionary<Type, object>();
            _cashedDocTemplateChains = new InternalCasheDataListDocChain();
            _modelCashe = new ModelCashe {Workarea = wa as Workarea};
            DefaultPartalReloadTime = 10;
        }

        private ModelCashe _modelCashe;
        /// <summary>
        /// Глобальное кеширование моделей
        /// </summary>
        public ModelCashe ModelCashe
        {
            get { return _modelCashe; }
            set { _modelCashe = value; }
        }

        private int _defaultPartalReloadTime;
        /// <summary>
        /// Время в минутах используемое для перегрузки объекта из базы данных при необходимости
        /// </summary>
        public int DefaultPartalReloadTime
        {
            get { return _defaultPartalReloadTime; }
            set
            {
                _defaultPartalReloadTime = value;
                
            }
        }
        
        private IWorkarea _workarea;
        /// <summary>
        /// Рабочая область
        /// </summary>
        public IWorkarea Workarea
        {
            get { return _workarea; }
        }

        /// <summary>
        /// Кешированные объекты иерархий
        /// </summary>
        public CasheData<Hierarchy> Hierarhies
        {
            get 
            {
                return GetCasheData<Hierarchy>();
            }
        }

        private CasheData<FactName> _factNames;
        /// <summary>
        /// Кешированные объекты наименований фактов
        /// </summary>
        public CasheData<FactName> FactNames
        {
            get { return GetCasheData<FactName>(); }
        }

        private CasheData<CustomViewList> _customViewLists;
        /// <summary>
        /// Кешированные объекты списков
        /// </summary>
        public CasheData<CustomViewList> CustomViewLists
        {
            get { return GetCasheData<CustomViewList>(); }
        }

        private CasheData<SystemParameter> _systemParameters;
        /// <summary>
        /// Кешированные объекты системных параметров
        /// </summary>
        public CasheData<SystemParameter> SystemParameters
        {
            get { return GetCasheData<SystemParameter>();}
        }


        internal Dictionary<Type, object> _cashedCollections;

        protected Dictionary<Type, object> CashedCollections
        {
            get 
            { 
                return _cashedCollections;
            }
        }
        
        public virtual CasheData<T> GetCasheData<T>() where T : class, ICoreObject, new()
        {
            if (!CashedCollections.ContainsKey(typeof(T)))
            {
                CasheData<T> cashedData = new CasheData<T> { Workarea = Workarea };
                CashedCollections.Add(typeof(T), cashedData);
            }
            return CashedCollections[typeof(T)] as CasheData<T>;
        }
        public virtual object GetCasheItem(int kind, int id)
        {
            switch ((WhellKnownDbEntity)kind)
            {
                case 0:
                    return null;
                case WhellKnownDbEntity.Product:
                    return GetCasheData<Product>().Item(id);
                case WhellKnownDbEntity.Account:
                    return GetCasheData<Account>().Item(id);
                case WhellKnownDbEntity.Agent:
                    return GetCasheData<Agent>().Item(id);
                case WhellKnownDbEntity.Analitic:
                    return GetCasheData<Analitic>().Item(id);
                case WhellKnownDbEntity.Currency:
                    return GetCasheData<Currency>().Item(id);
                case WhellKnownDbEntity.Recipe:
                    return GetCasheData<Recipe>().Item(id);
                case WhellKnownDbEntity.Folder:
                    return GetCasheData<Folder>().Item(id);
                //case WhellKnownDbEntity.StorageCellTurn:
                //    return GetCasheData<StorageCellTurn>().Item(id);
                case WhellKnownDbEntity.PriceName:
                    return GetCasheData<PriceName>().Item(id);
                case WhellKnownDbEntity.Unit:
                    return GetCasheData<Unit>().Item(id);
                case WhellKnownDbEntity.Branche:
                    return GetCasheData<Branche>().Item(id);
                //case WhellKnownDbEntity.ProductRecipeItem:
                //    return GetCasheData<ProductRecipeItem>().Item(id);
                case WhellKnownDbEntity.Rate:
                    return GetCasheData<Rate>().Item(id);
                case WhellKnownDbEntity.EntityDocument:
                    return GetCasheData<EntityDocument>().Item(id);
                case WhellKnownDbEntity.Library:
                    return GetCasheData<Library>().Item(id);
                case WhellKnownDbEntity.Price:
                    return GetCasheData<PriceName>().Item(id);
                case WhellKnownDbEntity.FactName:
                    return GetCasheData<FactName>().Item(id);
                case WhellKnownDbEntity.FactColumn:
                    return GetCasheData<FactColumn>().Item(id);
                //case WhellKnownDbEntity.DbEntity:
                //    return GetCasheData<DbEntity>().Item(id);
                case WhellKnownDbEntity.Document:
                    return GetCasheData<Document>().Item(id);
                case WhellKnownDbEntity.CustomViewList:
                    return GetCasheData<CustomViewList>().Item(id);
                case WhellKnownDbEntity.Column:
                    return GetCasheData<CustomViewColumn>().Item(id);
                case WhellKnownDbEntity.FileData:
                    return GetCasheData<FileData>().Item(id);
                case WhellKnownDbEntity.BankAccount:
                    return GetCasheData<Account>().Item(id);
                case WhellKnownDbEntity.SystemParameter:
                    return GetCasheData<SystemParameter>().Item(id);
                //case WhellKnownDbEntity.Users:
                //    return GetCasheData<Users>().Item(id);
                //case WhellKnownDbEntity.Acl:
                //    return GetCasheData<Acl>().Item(id);
                case WhellKnownDbEntity.Hierarchy:
                    return GetCasheData<Hierarchy>().Item(id);
                case WhellKnownDbEntity.HierarchyContent:
                    return GetCasheData<HierarchyContent>().Item(id);
                case WhellKnownDbEntity.Series:
                    return GetCasheData<Series>().Item(id);
                case WhellKnownDbEntity.Contact:
                    return GetCasheData<Contact>().Item(id);
                case WhellKnownDbEntity.ProductUnit:
                    return GetCasheData<ProductUnit>().Item(id);
                case WhellKnownDbEntity.Country:
                    return GetCasheData<Country>().Item(id);
                //case WhellKnownDbEntity.PropertyGroup:
                //    return GetCasheData<PropertyGroup>().Item(id);
                case WhellKnownDbEntity.Ruleset:
                    return GetCasheData<Ruleset>().Item(id);
                case WhellKnownDbEntity.XmlStorage:
                    return GetCasheData<XmlStorage>().Item(id);
                case WhellKnownDbEntity.Territory:
                    return GetCasheData<Territory>().Item(id);
                case WhellKnownDbEntity.Town:
                    return GetCasheData<Town>().Item(id);
                case WhellKnownDbEntity.StorageCell:
                    return GetCasheData<StorageCell>().Item(id);
                //case WhellKnownDbEntity.Chain:
                //    return GetCasheData<Chain>().Item(id);
                case WhellKnownDbEntity.DbObject:
                    return GetCasheData<DbObject>().Item(id);
                case WhellKnownDbEntity.DbObjectChild:
                    return GetCasheData<DbObjectChild>().Item(id);
                case WhellKnownDbEntity.Passport:
                    return GetCasheData<Passport>().Item(id);
                case WhellKnownDbEntity.DrivingLicence:
                    return GetCasheData<DrivingLicence>().Item(id);
                case WhellKnownDbEntity.Store:
                    return GetCasheData<Store>().Item(id);
                case WhellKnownDbEntity.Company:
                    return GetCasheData<Company>().Item(id);
                case WhellKnownDbEntity.Bank:
                    return GetCasheData<Bank>().Item(id);
                case WhellKnownDbEntity.SystemParameterUser:
                    return GetCasheData<SystemParameterUser>().Item(id);
                case WhellKnownDbEntity.LibraryContent:
                    return GetCasheData<LibraryContent>().Item(id);
                //case WhellKnownDbEntity.ReportChain:
                //    return GetCasheData<ReportChain>().Item(id);
                case WhellKnownDbEntity.FlagValue:
                    return GetCasheData<FlagValue>().Item(id);
                case WhellKnownDbEntity.DocumentSign:
                    return GetCasheData<DocumentSign>().Item(id);
                case WhellKnownDbEntity.Taxe:
                    return GetCasheData<Taxe>().Item(id);
                //case WhellKnownDbEntity.ResourceImage:
                //    return GetCasheData<ResourceImage>().Item(id);
                case WhellKnownDbEntity.ResourceString:
                    return GetCasheData<ResourceString>().Item(id);
                case WhellKnownDbEntity.State:
                    return GetCasheData<State>().Item(id);
                case WhellKnownDbEntity.People:
                    return GetCasheData<People>().Item(id);
                case WhellKnownDbEntity.Employer:
                    return GetCasheData<Employer>().Item(id);
                case WhellKnownDbEntity.ChainKind:
                    return GetCasheData<ChainKind>().Item(id);
                case WhellKnownDbEntity.FactValue:
                    return GetCasheData<FactValue>().Item(id);
                case WhellKnownDbEntity.UserRightCommon:
                    return GetCasheData<UserRightCommon>().Item(id);
                case WhellKnownDbEntity.UserRightElement:
                    return GetCasheData<UserRightElement>().Item(id);
                case WhellKnownDbEntity.FactDate:
                    return GetCasheData<FactDate>().Item(id);
                case WhellKnownDbEntity.FactColumnEntityKind:
                    return GetCasheData<FactColumnEntityKind>().Item(id);
                case WhellKnownDbEntity.LogUserAction:
                    return GetCasheData<LogUserAction>().Item(id);
                case WhellKnownDbEntity.AgentAddress:
                    return GetCasheData<AgentAddress>().Item(id);
                default:
                    throw new Exception("Неизвестный тип");
            }
        }
        public virtual void SetCasheData<T>(T value) where T: class, ICoreObject, new()
        {
            GetCasheData<T>().Add(value);
        }
        public virtual void UpdateCasheData<T>(T value) where T: class, ICoreObject, new()
        {
            GetCasheData<T>().Update(value);
        }
        
        /*кеширование данных связей объектов*/
        internal Dictionary<Type, object> _cashedChainCollections;

        protected Dictionary<Type, object> CashedChainCollections
        {
            get
            {
                return _cashedChainCollections;
            }
        }

        public virtual ListChainCasheData<T> GetChainCasheData<T>() where T : class, ICoreObject, new()
        {
            if (!CashedChainCollections.ContainsKey(typeof(T)))
            {
                ListChainCasheData<T> cashedData = new ListChainCasheData<T>();
                CashedChainCollections.Add(typeof(T), cashedData);
            }
            return CashedChainCollections[typeof(T)] as ListChainCasheData<T>;
        }
        public virtual void SetChainCasheData<T>(int id, int kind, List<T> value) where T : class, ICoreObject, new()
        {
            GetChainCasheData<T>().Add(id, kind, value);
        }

        public virtual void RefreshChainCasheData()
        {
            _cashedChainCollections.Clear();
        }

        /*кеширование данных коллекций*/
        internal Dictionary<Type, object> _cashedListCodeCasheDataCollections;

        protected Dictionary<Type, object> CashedListCodeCasheDataCollections
        {
            get
            {
                return _cashedListCodeCasheDataCollections;
            }
        }

        public virtual ListCodeCasheData<T> GetListCodeCasheData<T>() where T : class, ICoreObject, new()
        {
            if (!CashedListCodeCasheDataCollections.ContainsKey(typeof(T)))
            {
                ListCodeCasheData<T> cashedData = new ListCodeCasheData<T>();
                CashedListCodeCasheDataCollections.Add(typeof(T), cashedData);
            }
            return CashedListCodeCasheDataCollections[typeof(T)] as ListCodeCasheData<T>;
        }
        public virtual void SetListCodeCasheData<T>(string key, List<T> value) where T : class, ICoreObject, new()
        {
            GetListCodeCasheData<T>().Add(key, value);
        }

        public virtual void RefreshListCodeCasheData()
        {
            _cashedListCodeCasheDataCollections.Clear();
        }

        /*кеширование данных коллекций связанных шаблонов*/
        private InternalCasheDataListDocChain _cashedDocTemplateChains;

        internal InternalCasheDataListDocChain CashedDocTemplateChains
        {
            get
            {
                return _cashedDocTemplateChains;
            }
        }



        //public virtual CasheDataIBase<T> GetCasheDataIBase<T>() where T : class, IBase, new()
        //{
        //    if (!CashedCollections.ContainsKey(typeof(T)))
        //    {
        //        CasheDataIBase<T> cashedData = new CasheDataIBase<T> { Workarea = Workarea };
        //        CashedCollections.Add(typeof(T), cashedData);
        //    }
        //    return CashedCollections[typeof(T)] as CasheDataIBase<T>;
        //}

        //public virtual void SetCasheDataIBase<T>(T value) where T : class, IBase, new()
        //{
        //    GetCasheDataIBase<T>().Add(value);
        //}


        private Dictionary<int, List<ResourceString>> _resourceStringCashe;
        /// <summary>
        /// Коллекция строковых ресурсов
        /// </summary>
        /// <param name="cultureId"></param>
        /// <returns></returns>
        public List<ResourceString> CollectionResourceString(int cultureId)
        {
            if (_resourceStringCashe == null) _resourceStringCashe = new Dictionary<int, List<ResourceString>>();
            if (_resourceStringCashe.ContainsKey(cultureId))
                return _resourceStringCashe[cultureId];

            _resourceStringCashe.Add(cultureId, Workarea.GetCollectionResourceString(cultureId));
            return _resourceStringCashe[cultureId];
        }
        public string ResourceString(string key, int cultureId=0)
        {
            if (cultureId==0)
            {
                ResourceString item = CollectionResourceString(Workarea.LanguageId).FirstOrDefault(f => f.Code == key);
                return item != null ? item.Value : string.Empty;
            }
            else
                return CollectionResourceString(cultureId).Find(f => f.Code == key).Value;
        }
        

        private List<EntityDocument> _documentTypes;
        public List<EntityDocument> CollectionDocumentTypes()
        {
            return _documentTypes ?? (_documentTypes = Workarea.CollectionDocumentTypes());
        }

        private List<EntityPropertyName> _entityPropertyNames;

        public List<EntityPropertyName> CollectionEntityPropertyNames()
        {
            return _entityPropertyNames ?? (_entityPropertyNames = Workarea.GetEntityPropertyNames(1049));
        }
    }
}
