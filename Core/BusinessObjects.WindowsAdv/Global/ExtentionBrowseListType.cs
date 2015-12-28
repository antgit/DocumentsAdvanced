using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects.Security;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        public static Form ShowPropertyType<T>(this T value) where T : class, IBase
        {
            Form form;
            if (value.GetType() == typeof(EquipmentNode))
            {
                form = (value as EquipmentNode).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Equipment))
            {
                form = (value as Equipment).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(EquipmentDetail))
            {
                form = (value as EquipmentDetail).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Depatment))
            {
                form = (value as Depatment).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Calendar))
            {
                form = (value as Calendar).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Territory))
            {
                if((value as Territory).KindValue == Territory.KINDVALUE_REGION)
                    form = (value as Territory).ShowPropertyTerritory();
                if ((value as Territory).KindValue == Territory.KINDVALUE_REGIONALDISTRICT)
                    form = (value as Territory).ShowPropertyRegion();
                else
                    form = null;
                return form;
            }
            if (value.GetType() == typeof(Town))
            {
                form = (value as Town).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Event))
            {
                form = (value as Event).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(UserAccount))
            {
                form = (value as UserAccount).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Task))
            {
                form = (value as Task).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(TimePeriod))
            {
                form = (value as TimePeriod).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Note))
            {
                form = (value as Note).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Folder))
            {
                form = (value as Folder).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(XmlStorage))
            {
                form = (value as XmlStorage).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(CodeName))
            {
                form = (value as CodeName).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Product))
            {
                form = (value as Product).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(FileData))
            {
                form = (value as FileData).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Agent))
            {
                form = (value as Agent).ShowProperty();
                return form;
            }
            if (value.GetType() == typeof(Library))
            {
                form = (value as Library).ShowProperty();
                return form;
            }
            return null;
        }

        public static T BrowseListType<T>(this T value) where T : class, IBase
        {
            if (value.GetType() == typeof(Equipment))
            {
                Equipment product = BrowseList(value as Equipment);
                return product as T;
            }
            if (value.GetType() == typeof(EquipmentNode))
            {
                EquipmentNode product = BrowseList(value as EquipmentNode);
                return product as T;
            }
            if (value.GetType() == typeof(EquipmentDetail))
            {
                EquipmentDetail product = BrowseList(value as EquipmentDetail);
                return product as T;
            }
            if (value.GetType() == typeof(Depatment))
            {
                Depatment product = BrowseList(value as Depatment);
                return product as T;
            }
            if (value.GetType() == typeof(Calendar))
            {
                Calendar product = BrowseList(value as Calendar);
                return product as T;
            }
            if (value.GetType() == typeof(Town))
            {
                Town product = BrowseList(value as Town);
                return product as T;
            }
            if (value.GetType() == typeof(Territory))
            {
                Territory product = BrowseList(value as Territory);
                return product as T;
            }
            if (value.GetType() == typeof(Event))
            {
                Event product = BrowseList(value as Event);
                return product as T;
            }
            if (value.GetType() == typeof(UserAccount))
            {
                UserAccount product = BrowseList(value as UserAccount);
                return product as T;
            }
            if (value.GetType() == typeof(Task))
            {
                Task product = BrowseList(value as Task);
                return product as T;
            }
            if (value.GetType() == typeof(TimePeriod))
            {
                TimePeriod product = BrowseList(value as TimePeriod);
                return product as T;
            }
            if (value.GetType() == typeof(Note))
            {
                Note product = BrowseList(value as Note);
                return product as T;
            }
            if (value.GetType() == typeof(CodeName))
            {
                CodeName product = BrowseList(value as CodeName);
                return product as T;
            }
            if (value.GetType() == typeof(XmlStorage))
            {
                XmlStorage product = BrowseList(value as XmlStorage);
                return product as T;
            }
            if (value.GetType() == typeof(FileData))
            {
                FileData product = BrowseList(value as FileData);
                return product as T;
            }
            if (value.GetType() == typeof(Product))
            {
                Product product = BrowseList(value as Product);
                return product as T;
            }
            if (value.GetType() == typeof(Agent))
            {
                Agent product = BrowseList(value as Agent);
                return product as T;
            }
            if (value.GetType() == typeof(Account))
            {
                Account product = BrowseList(value as Account);
                return product as T;
            }
            if (value.GetType() == typeof(Analitic))
            {
                Analitic product = BrowseList(value as Analitic);
                return product as T;
            }
            if (value.GetType() == typeof(Branche))
            {
                Branche product = BrowseList(value as Branche);
                return product as T;
            }
            if (value.GetType() == typeof(Currency))
            {
                Currency product = BrowseList(value as Currency);
                return product as T;
            }
            if (value.GetType() == typeof(Unit))
            {
                Unit product = BrowseList(value as Unit);
                return product as T;
            }
            if (value.GetType() == typeof(Library))
            {
                Library product = BrowseList(value as Library);
                return product as T;
            }
            if (value.GetType() == typeof(PriceName))
            {
                PriceName product = BrowseList(value as PriceName);
                return product as T;
            }
            if (value.GetType() == typeof(Folder))
            {
                Folder product = BrowseList(value as Folder);
                return product as T;
            }
            if (value.GetType() == typeof(Library))
            {
                Library product = BrowseList(value as Library);
                return product as T;
            }
            if (value.GetType() == typeof(CustomViewList))
            {
                CustomViewList product = BrowseList(value as CustomViewList);
                return product as T;
            }
            if (value.GetType() == typeof(SystemParameter))
            {
                SystemParameter product = BrowseList(value as SystemParameter);
                return product as T;
            }
            if (value.GetType() == typeof(Right))
            {
                Right product = BrowseList(value as Right, null);
                return product as T;
            }
            // TODO:
            if (value.GetType() == typeof(EntityType))
            {
                EntityType product = BrowseList(value as EntityType);
                return product as T;
            }
            return null;
        }

        public static List<T> BrowseListType<T>(this T value, Predicate<T> filter, List<T> sourceCollection) where T : class, IBase
        {
            if (value.GetType() == typeof(Equipment))
            {
                Predicate<Equipment> myFilter = filter as Predicate<Equipment>;
                List<Equipment> mySourceCollection = sourceCollection as List<Equipment>;
                List<Equipment> coll = (value as Equipment).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(EquipmentNode))
            {
                Predicate<EquipmentNode> myFilter = filter as Predicate<EquipmentNode>;
                List<EquipmentNode> mySourceCollection = sourceCollection as List<EquipmentNode>;
                List<EquipmentNode> coll = (value as EquipmentNode).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(EquipmentDetail))
            {
                Predicate<EquipmentDetail> myFilter = filter as Predicate<EquipmentDetail>;
                List<EquipmentDetail> mySourceCollection = sourceCollection as List<EquipmentDetail>;
                List<EquipmentDetail> coll = (value as EquipmentDetail).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Depatment))
            {
                Predicate<Depatment> myFilter = filter as Predicate<Depatment>;
                List<Depatment> mySourceCollection = sourceCollection as List<Depatment>;
                List<Depatment> coll = (value as Depatment).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Calendar))
            {
                Predicate<Calendar> myFilter = filter as Predicate<Calendar>;
                List<Calendar> mySourceCollection = sourceCollection as List<Calendar>;
                List<Calendar> coll = (value as Calendar).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Town))
            {
                Predicate<Town> myFilter = filter as Predicate<Town>;
                List<Town> mySourceCollection = sourceCollection as List<Town>;
                List<Town> coll = (value as Town).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Territory))
            {
                Predicate<Territory> myFilter = filter as Predicate<Territory>;
                List<Territory> mySourceCollection = sourceCollection as List<Territory>;
                List<Territory> coll = (value as Territory).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Event))
            {
                Predicate<Event> myFilter = filter as Predicate<Event>;
                List<Event> mySourceCollection = sourceCollection as List<Event>;
                List<Event> coll = (value as Event).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(UserAccount))
            {
                Predicate<UserAccount> myFilter = filter as Predicate<UserAccount>;
                List<UserAccount> mySourceCollection = sourceCollection as List<UserAccount>;
                List<UserAccount> coll = (value as UserAccount).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Task))
            {
                Predicate<Task> myFilter = filter as Predicate<Task>;
                List<Task> mySourceCollection = sourceCollection as List<Task>;
                List<Task> coll = (value as Task).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(TimePeriod))
            {
                Predicate<TimePeriod> myFilter = filter as Predicate<TimePeriod>;
                List<TimePeriod> mySourceCollection = sourceCollection as List<TimePeriod>;
                List<TimePeriod> coll = (value as TimePeriod).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Note))
            {
                Predicate<Note> myFilter = filter as Predicate<Note>;
                List<Note> mySourceCollection = sourceCollection as List<Note>;
                List<Note> coll = (value as Note).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(AgentBankAccount))
            {
                Predicate<AgentBankAccount> myFilter = filter as Predicate<AgentBankAccount>;
                List<AgentBankAccount> mySourceCollection = sourceCollection as List<AgentBankAccount>;
                List<AgentBankAccount> coll = (value as AgentBankAccount).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Message))
            {
                Predicate<Message> myFilter = filter as Predicate<Message>;
                List<Message> mySourceCollection = sourceCollection as List<Message>;
                List<Message> coll = (value as Message).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(DateRegion))
            {
                Predicate<DateRegion> myFilter = filter as Predicate<DateRegion>;
                List<DateRegion> mySourceCollection = sourceCollection as List<DateRegion>;
                List<DateRegion> coll = (value as DateRegion).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Knowledge))
            {
                Predicate<Knowledge> myFilter = filter as Predicate<Knowledge>;
                List<Knowledge> mySourceCollection = sourceCollection as List<Knowledge>;
                List<Knowledge> coll = (value as Knowledge).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(WebService))
            {
                Predicate<WebService> myFilter = filter as Predicate<WebService>;
                List<WebService> mySourceCollection = sourceCollection as List<WebService>;
                List<WebService> coll = (value as WebService).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(DataCatalog))
            {
                Predicate<DataCatalog> myFilter = filter as Predicate<DataCatalog>;
                List<DataCatalog> mySourceCollection = sourceCollection as List<DataCatalog>;
                List<DataCatalog> coll = (value as DataCatalog).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(CodeName))
            {
                Predicate<CodeName> myFilter = filter as Predicate<CodeName>;
                List<CodeName> mySourceCollection = sourceCollection as List<CodeName>;
                List<CodeName> coll = (value as CodeName).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(XmlStorage))
            {
                Predicate<XmlStorage> myFilter = filter as Predicate<XmlStorage>;
                List<XmlStorage> mySourceCollection = sourceCollection as List<XmlStorage>;
                List<XmlStorage> coll = (value as XmlStorage).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Ruleset))
            {
                Predicate<Ruleset> myFilter = filter as Predicate<Ruleset>;
                List<Ruleset> mySourceCollection = sourceCollection as List<Ruleset>;
                List<Ruleset> coll = (value as Ruleset).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(FileData))
            {
                Predicate<FileData> myFilter = filter as Predicate<FileData>;
                List<FileData> mySourceCollection = sourceCollection as List<FileData>;
                List<FileData> coll = (value as FileData).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Product))
            {
                Predicate<Product> myFilter = filter as Predicate<Product>;
                List<Product> mySourceCollection = sourceCollection as List<Product>;
                List<Product> coll = (value as Product).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Agent))
            {
                Predicate<Agent> myFilter = filter as Predicate<Agent>;
                List<Agent> mySourceCollection = sourceCollection as List<Agent>;
                List<Agent> coll = (value as Agent).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Account))
            {
                Predicate<Account> myFilter = filter as Predicate<Account>;
                List<Account> mySourceCollection = sourceCollection as List<Account>;
                List<Account> coll = (value as Account).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Analitic))
            {
                Predicate<Analitic> myFilter = filter as Predicate<Analitic>;
                List<Analitic> mySourceCollection = sourceCollection as List<Analitic>;
                List<Analitic> coll = (value as Analitic).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
                
            }
            if (value.GetType() == typeof(Branche))
            {
                Predicate<Branche> myFilter = filter as Predicate<Branche>;
                List<Branche> mySourceCollection = sourceCollection as List<Branche>;
                List<Branche> coll = (value as Branche).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Currency))
            {
                Predicate<Currency> myFilter = filter as Predicate<Currency>;
                List<Currency> mySourceCollection = sourceCollection as List<Currency>;
                List<Currency> coll = (value as Currency).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Unit))
            {
                Predicate<Unit> myFilter = filter as Predicate<Unit>;
                List<Unit> mySourceCollection = sourceCollection as List<Unit>;
                List<Unit> coll = (value as Unit).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Library))
            {
                Predicate<Library> myFilter = filter as Predicate<Library>;
                List<Library> mySourceCollection = sourceCollection as List<Library>;
                List<Library> coll = (value as Library).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(PriceName))
            {
                Predicate<PriceName> myFilter = filter as Predicate<PriceName>;
                List<PriceName> mySourceCollection = sourceCollection as List<PriceName>;
                List<PriceName> coll = (value as PriceName).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Folder))
            {
                Predicate<Folder> myFilter = filter as Predicate<Folder>;
                List<Folder> mySourceCollection = sourceCollection as List<Folder>;
                List<Folder> coll = (value as Folder).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(CustomViewList))
            {
                Predicate<CustomViewList> myFilter = filter as Predicate<CustomViewList>;
                List<CustomViewList> mySourceCollection = sourceCollection as List<CustomViewList>;
                List<CustomViewList> coll = (value as CustomViewList).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Country))
            {
                Predicate<Country> myFilter = filter as Predicate<Country>;
                List<Country> mySourceCollection = sourceCollection as List<Country>;
                List<Country> coll = (value as Country).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(SystemParameter))
            {
                Predicate<SystemParameter> myFilter = filter as Predicate<SystemParameter>;
                List<SystemParameter> mySourceCollection = sourceCollection as List<SystemParameter>;
                List<SystemParameter> coll = (value as SystemParameter).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(Right))
            {
                Predicate<Right> myFilter = filter as Predicate<Right>;
                List<Right> mySourceCollection = sourceCollection as List<Right>;
                List<Right> coll = (value as Right).BrowseListMulty(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(EntityType))
            {
                Predicate<EntityType> myFilter = filter as Predicate<EntityType>;
                List<EntityType> mySourceCollection = sourceCollection as List<EntityType>;
                if (mySourceCollection == null)
                    mySourceCollection = value.Workarea.CollectionEntities;
                List<EntityType> coll = (value as EntityType).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            if (value.GetType() == typeof(EntityDocument))
            {
                Predicate<EntityDocument> myFilter = filter as Predicate<EntityDocument>;
                List<EntityDocument> mySourceCollection = sourceCollection as List<EntityDocument>;
                if (mySourceCollection == null)
                    mySourceCollection = value.Workarea.CollectionDocumentTypes();
                List<EntityDocument> coll = (value as EntityDocument).BrowseList(myFilter, mySourceCollection);
                if (coll == null) return null;
                return coll.ConvertAll((v => v as T));
            }
            return null;
        }
    }
}
