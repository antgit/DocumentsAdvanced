using System.Activities;
using System.Collections.Generic;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>
    /// Процесс изменение компании-владельца для объекта
    /// </summary>
    public class ActivityChangeCompanyOwner<T> : CodeActivity<T> where T : class, ICoreObject, ICompanyOwner, new()
    {
        public ActivityChangeCompanyOwner()
            : base()
        {
            this.DisplayName = "Изменение компании владельца объекта";
        }
        public InArgument<T> CurrentObject { get; set; }

        protected override T Execute(CodeActivityContext context)
        {
            Workarea wa = CurrentObject.Get(context).Workarea;
            T owner = CurrentObject.Get(context);

            List<Agent> newOwner = wa.Empty<Agent>().BrowseList(s => s.KindValue == Agent.KINDVALUE_MYCOMPANY,
                                                                wa.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY));

            if (newOwner != null && newOwner.Count > 0)
            {
                owner.MyCompanyId = newOwner[0].Id;
                owner.Save();
            }
            return owner as T;
        }
    }
    /// <summary>
    /// Процесс изменение компании-владельца для файловых данных
    /// </summary>
    public class ActivityChangeFileDataCompanyOwner : ActivityChangeCompanyOwner<FileData>
    {
        public ActivityChangeFileDataCompanyOwner()
            : base()
        {

        }
    }
    /// <summary>
    /// Процесс изменение компании-владельца для пользователя/группы
    /// </summary>
    public class ActivityChangeUidCompanyOwner: ActivityChangeCompanyOwner<Security.Uid>
    {
        public ActivityChangeUidCompanyOwner(): base()
        {
            
        }
    }
    /// <summary>
    /// Процесс изменение компании-владельца для вида цены
    /// </summary>
    public class ActivityChangePriceNameCompanyOwner : ActivityChangeCompanyOwner<PriceName>
    {
        public ActivityChangePriceNameCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// Процесс изменение компании-владельца для бухгалтерского счета
    /// </summary>
    public class ActivityChangeAccountCompanyOwner : ActivityChangeCompanyOwner<Account>
    {
        public ActivityChangeAccountCompanyOwner()
            : base()
        {

        }
    }
    /// <summary>
    /// Процесс изменение компании-владельца для наименования допольнительного кода
    /// </summary>
    public class ActivityChangeCodeNameCompanyOwner : ActivityChangeCompanyOwner<CodeName>
    {
        public ActivityChangeCodeNameCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// Процесс изменение компании-владельца для библиотеки
    /// </summary>
    public class ActivityChangeLibraryCompanyOwner : ActivityChangeCompanyOwner<Library>
    {
        public ActivityChangeLibraryCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// Процесс изменение компании-владельца для правила или процесса
    /// </summary>
    public class ActivityChangeRulesetCompanyOwner : ActivityChangeCompanyOwner<Ruleset>
    {
        public ActivityChangeRulesetCompanyOwner()
            : base()
        {

        }
    }
    /// <summary>
    /// Процесс изменение компании-владельца для события
    /// </summary>
    public class ActivityChangeEventCompanyOwner : ActivityChangeCompanyOwner<Event>
    {
        public ActivityChangeEventCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// Процесс изменение компании-владельца для задачи
    /// </summary>
    public class ActivityChangeTaskCompanyOwner : ActivityChangeCompanyOwner<Task>
    {
        public ActivityChangeTaskCompanyOwner()
            : base()
        {

        }
    }
    /// <summary>
    /// Процесс изменение компании-владельца для статьи базы знаний
    /// </summary>
    public class ActivityChangeKnowledgeCompanyOwner : ActivityChangeCompanyOwner<Knowledge>
    {
        public ActivityChangeKnowledgeCompanyOwner()
            : base()
        {

        }
    }
    /// <summary>
    /// Процесс изменение компании-владельца для сообщения
    /// </summary>
    public class ActivityChangeMessageCompanyOwner : ActivityChangeCompanyOwner<Message>
    {
        public ActivityChangeMessageCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// Процесс изменение компании-владельца для примечания
    /// </summary>
    public class ActivityChangeNoteCompanyOwner : ActivityChangeCompanyOwner<Note>
    {
        public ActivityChangeNoteCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// Процесс изменение компании-владельца для графика работы
    /// </summary>
    public class ActivityChangeTimePeriodCompanyOwner : ActivityChangeCompanyOwner<TimePeriod>
    {
        public ActivityChangeTimePeriodCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// Процесс изменение компании-владельца для каталога данных
    /// </summary>
    public class ActivityChangeDataCatalogCompanyOwner : ActivityChangeCompanyOwner<DataCatalog>
    {
        public ActivityChangeDataCatalogCompanyOwner()
            : base()
        {

        }
    }
}