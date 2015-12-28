using System.Activities;
using System.Linq;
namespace BusinessObjects.Workflows
{
    public sealed class ActivityCreateNewObject<T> : CodeActivity<T> where T : class, IBase, new()
    {
        public ActivityCreateNewObject()
            : base()
        {
            this.DisplayName = "Создание нового объекта";
        }
        /// <summary>
        /// Объект являющийся шаблоном
        /// </summary>
        [RequiredArgument]
        public InArgument<T> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override T Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            T owner = CurrentObject.Get(context);
            T newObject = wa.CreateNewObject<T>(owner);

            return newObject;
        }
    }

    public sealed class ActivityCreateNewObjectByCode<T> : CodeActivity<T> where T : class, IBase, new()
    {
        public ActivityCreateNewObjectByCode()
            : base()
        {
            this.DisplayName = "Создание нового объекта по коду шаблона";
        }
        /// <summary>
        /// Объект из рабочей области
        /// </summary>
        [RequiredArgument]
        public InArgument<T> CurrentObject { get; set; }

        [RequiredArgument]
        public InArgument<string> TemplateCode { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override T Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            string code = TemplateCode.Get(context);

            T owner = wa.GetTemplates<T>().FirstOrDefault(s => s.Code == code);
            T newObject = wa.CreateNewObject<T>(owner);

            return newObject;
        }
    }
}