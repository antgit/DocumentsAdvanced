using System.Activities;
using System.Collections.Generic;

namespace BusinessObjects.Windows.Workflows
{
    public sealed class ActivitySaveArrayDocument : CodeActivity
    {
        public ActivitySaveArrayDocument()
            : base()
        {
            this.DisplayName = "Сохранение документов";
        }
        // Define an activity input argument of type string
        public InArgument<List<IDocumentView>> Values { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Values);
            List<IDocumentView> coll = context.GetValue(this.Values);
            foreach (IDocumentView documentView in coll)
            {
                documentView.InvokeSave();
            }
        }
    }
}