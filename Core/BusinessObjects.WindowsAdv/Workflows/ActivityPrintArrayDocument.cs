using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace BusinessObjects.Windows.Workflows
{

    public sealed class ActivityPrintArrayDocument : CodeActivity
    {
        public ActivityPrintArrayDocument(): base()
        {
            this.DisplayName = "Печать документов";
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
                documentView.InvokePrint();
            }
        }
    }

    public sealed class ActivityReSaveArrayDocument : CodeActivity
    {
        public ActivityReSaveArrayDocument()
            : base()
        {
            this.DisplayName = "Перепроведение документов";
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
                //SourceDocument.StateId = State.STATEACTIVE;
                //SetViewStateDone();
                //InvokeSave();
                documentView.InvokeSetState(State.STATEACTIVE);
            }
        }
    }
}
