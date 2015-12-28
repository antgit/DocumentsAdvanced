using System.Activities;
using System.Collections.Generic;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>
    /// Изменение компании владельца статьи базы знаний
    /// </summary>
    public sealed class ActivityKnowledgeChangeCompanyOwner : CodeActivity
    {
        public ActivityKnowledgeChangeCompanyOwner()
            : base()
        {
            this.DisplayName = "Изменение компании владельца статьи базы знаний";
        }
        // Define an activity input argument of type string
        public InArgument<Knowledge> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            Knowledge owner = CurrentObject.Get(context);

            List<Agent> newOwner =  wa.Empty<Agent>().BrowseList(s => s.KindValue == Agent.KINDVALUE_MYCOMPANY,
                                                    wa.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY));

            if (newOwner != null && newOwner.Count > 0)
            {
                owner.MyCompanyId = newOwner[0].Id;
                owner.Save();
            }
        }
    }
}