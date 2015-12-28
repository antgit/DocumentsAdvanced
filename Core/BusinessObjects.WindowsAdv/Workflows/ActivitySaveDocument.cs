using System.Activities;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>������ ���������</summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ActivitySaveDocument<T> : CodeActivity where T : class, IDocumentView
    {
        public ActivitySaveDocument()
            : base()
        {
            this.DisplayName = "���������� ���������";
        }
        public InArgument<T> Value { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Value.Get(context).InvokeSave();
        }
    }
}