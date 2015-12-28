using System.Activities;
using System.Collections.Generic;

namespace BusinessObjects.Windows.Workflows
{
    public sealed class ActivityChangeDbOwner<T> : CodeActivity<T> where T : class, ICoreObject, new()
    {
        public ActivityChangeDbOwner()
            : base()
        {
            this.DisplayName = "Изменение базы владельца объекта";
        }
        // Define an activity input argument of type string
        public InArgument<T> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override T Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            ICoreObject owner = CurrentObject.Get(context);
            List<Branche> selected = wa.MyBranche.BrowseContent();

            if (selected != null && selected.Count > 0)
            {
                owner.DatabaseId = selected[0].Id;
                owner.Save();
            }
            return owner as T;
        }
    }
}