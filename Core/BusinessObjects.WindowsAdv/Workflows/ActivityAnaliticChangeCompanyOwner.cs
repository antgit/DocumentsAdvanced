using System.Activities;
using System.Collections.Generic;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>
    /// Процесс изменение компании-владельца для аналитики
    /// </summary>
    public sealed class ActivityAnaliticChangeCompanyOwner : CodeActivity
    {
        public ActivityAnaliticChangeCompanyOwner()
            : base()
        {
            this.DisplayName = "Изменение компании владельца объекта учета";
        }
        public InArgument<Analitic> CurrentObject { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Workarea wa = CurrentObject.Get(context).Workarea;
            Analitic owner = CurrentObject.Get(context);

            List<Agent> newOwner = wa.Empty<Agent>().BrowseList(s => s.KindValue == Agent.KINDVALUE_MYCOMPANY,
                                                                wa.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY));

            if (newOwner != null && newOwner.Count > 0)
            {
                owner.MyCompanyId = newOwner[0].Id;
                owner.Save();
            }
        }
    }
}