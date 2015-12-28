using System.Activities;
using System.Collections.Generic;

namespace BusinessObjects.Windows.Workflows
{
    public sealed class ActivityBrowseMyCompanies : CodeActivity<Agent>
    {
        public ActivityBrowseMyCompanies()
            : base()
        {
            this.DisplayName = "Выбор компании";
        }
        // Define an activity input argument of type string
        public InArgument<Workarea> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override Agent Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context);

            List<Agent> newOwner =  wa.Empty<Agent>().BrowseList(s => s.KindValue == Agent.KINDVALUE_MYCOMPANY,
                                                                 wa.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY));

            if (newOwner != null && newOwner.Count > 0)
                return newOwner[0];
            else
                return null;
        }
    }
}