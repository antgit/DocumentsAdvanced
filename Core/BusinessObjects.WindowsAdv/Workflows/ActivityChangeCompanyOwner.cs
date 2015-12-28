using System.Activities;
using System.Collections.Generic;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>
    /// ������� ��������� ��������-��������� ��� �������
    /// </summary>
    public class ActivityChangeCompanyOwner<T> : CodeActivity<T> where T : class, ICoreObject, ICompanyOwner, new()
    {
        public ActivityChangeCompanyOwner()
            : base()
        {
            this.DisplayName = "��������� �������� ��������� �������";
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
    /// ������� ��������� ��������-��������� ��� �������� ������
    /// </summary>
    public class ActivityChangeFileDataCompanyOwner : ActivityChangeCompanyOwner<FileData>
    {
        public ActivityChangeFileDataCompanyOwner()
            : base()
        {

        }
    }
    /// <summary>
    /// ������� ��������� ��������-��������� ��� ������������/������
    /// </summary>
    public class ActivityChangeUidCompanyOwner: ActivityChangeCompanyOwner<Security.Uid>
    {
        public ActivityChangeUidCompanyOwner(): base()
        {
            
        }
    }
    /// <summary>
    /// ������� ��������� ��������-��������� ��� ���� ����
    /// </summary>
    public class ActivityChangePriceNameCompanyOwner : ActivityChangeCompanyOwner<PriceName>
    {
        public ActivityChangePriceNameCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// ������� ��������� ��������-��������� ��� �������������� �����
    /// </summary>
    public class ActivityChangeAccountCompanyOwner : ActivityChangeCompanyOwner<Account>
    {
        public ActivityChangeAccountCompanyOwner()
            : base()
        {

        }
    }
    /// <summary>
    /// ������� ��������� ��������-��������� ��� ������������ ���������������� ����
    /// </summary>
    public class ActivityChangeCodeNameCompanyOwner : ActivityChangeCompanyOwner<CodeName>
    {
        public ActivityChangeCodeNameCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// ������� ��������� ��������-��������� ��� ����������
    /// </summary>
    public class ActivityChangeLibraryCompanyOwner : ActivityChangeCompanyOwner<Library>
    {
        public ActivityChangeLibraryCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// ������� ��������� ��������-��������� ��� ������� ��� ��������
    /// </summary>
    public class ActivityChangeRulesetCompanyOwner : ActivityChangeCompanyOwner<Ruleset>
    {
        public ActivityChangeRulesetCompanyOwner()
            : base()
        {

        }
    }
    /// <summary>
    /// ������� ��������� ��������-��������� ��� �������
    /// </summary>
    public class ActivityChangeEventCompanyOwner : ActivityChangeCompanyOwner<Event>
    {
        public ActivityChangeEventCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// ������� ��������� ��������-��������� ��� ������
    /// </summary>
    public class ActivityChangeTaskCompanyOwner : ActivityChangeCompanyOwner<Task>
    {
        public ActivityChangeTaskCompanyOwner()
            : base()
        {

        }
    }
    /// <summary>
    /// ������� ��������� ��������-��������� ��� ������ ���� ������
    /// </summary>
    public class ActivityChangeKnowledgeCompanyOwner : ActivityChangeCompanyOwner<Knowledge>
    {
        public ActivityChangeKnowledgeCompanyOwner()
            : base()
        {

        }
    }
    /// <summary>
    /// ������� ��������� ��������-��������� ��� ���������
    /// </summary>
    public class ActivityChangeMessageCompanyOwner : ActivityChangeCompanyOwner<Message>
    {
        public ActivityChangeMessageCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// ������� ��������� ��������-��������� ��� ����������
    /// </summary>
    public class ActivityChangeNoteCompanyOwner : ActivityChangeCompanyOwner<Note>
    {
        public ActivityChangeNoteCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// ������� ��������� ��������-��������� ��� ������� ������
    /// </summary>
    public class ActivityChangeTimePeriodCompanyOwner : ActivityChangeCompanyOwner<TimePeriod>
    {
        public ActivityChangeTimePeriodCompanyOwner()
            : base()
        {

        }
    }

    /// <summary>
    /// ������� ��������� ��������-��������� ��� �������� ������
    /// </summary>
    public class ActivityChangeDataCatalogCompanyOwner : ActivityChangeCompanyOwner<DataCatalog>
    {
        public ActivityChangeDataCatalogCompanyOwner()
            : base()
        {

        }
    }
}