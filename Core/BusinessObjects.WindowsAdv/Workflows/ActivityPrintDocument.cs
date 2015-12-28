using System;
using System.Activities;


namespace BusinessObjects.Windows.Workflows
{
    /// <summary>Печать документа</summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ActivityPrintDocument<T>: CodeActivity where T: class, IDocumentView
    {
        public ActivityPrintDocument()
            : base()
        {
            this.DisplayName = "Печать документа";
        }
        public InArgument<T> Value { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Value.Get(context).InvokePrint();
        }
    }
}