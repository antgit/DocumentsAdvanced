using System.Activities;
using System.Windows.Forms;
using BusinessObjects.Windows;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>Показать свойства документа</summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ActivityShowProperty<T> : CodeActivity<Form> where T : class, IBase
    {
        public ActivityShowProperty()
            : base()
        {
            this.DisplayName = "Показать окно свойств";
        }
        // Define an activity input argument of type string
        public InArgument<T> Value { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override Form Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            return Value.Get(context).ShowPropertyType();
        }
    }
}