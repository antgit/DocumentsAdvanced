using System.Activities;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>Показать свойства документа</summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ActivityShowAnaliticUsage: CodeActivity
    {
        public ActivityShowAnaliticUsage()
            : base()
        {
            this.DisplayName = "Анализ использования аналитики";
        }
        // Define an activity input argument of type string
        public InArgument<Analitic> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            CurrentObject.Get(context).ShowUsageDocuments();
        }
    }
}