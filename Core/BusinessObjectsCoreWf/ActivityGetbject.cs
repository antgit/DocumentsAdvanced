using System;
using System.Activities;

namespace BusinessObjects.Workflows
{
    public sealed class ActivityGetbject<T> : CodeActivity<T> where T : class, ICoreObject, new()
    {
        public ActivityGetbject()
            : base()
        {
            this.DisplayName = "Получение объекта из базы данных по идентификатору";
        }
        /// <summary>
        /// Объект из необходимой рабочей области 
        /// </summary>
        [RequiredArgument]
        public InArgument<T> CurrentObject { get; set; }

        /// <summary>
        /// Объект являющийся шаблоном
        /// </summary>
        [RequiredArgument]
        public InArgument<int> ObjectId { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override T Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            int objId = ObjectId.Get(context);
            T retObject =  wa.GetObject<T>(objId);

            return retObject;
        }
    }

    public sealed class ActivityGetbjectGuid<T> : CodeActivity<T> where T : class, ICoreObject, new()
    {
        public ActivityGetbjectGuid()
            : base()
        {
            this.DisplayName = "Получение объекта из базы данных по глобальному идентификатору";
        }
        /// <summary>
        /// Объект из необходимой рабочей области 
        /// </summary>
        [RequiredArgument]
        public InArgument<T> CurrentObject { get; set; }

        /// <summary>
        /// Объект являющийся шаблоном
        /// </summary>
        [RequiredArgument]
        public InArgument<Guid> ObjectId { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override T Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            Guid objId = ObjectId.Get(context);
            T retObject = wa.GetObject<T>(objId);

            return retObject;
        }
    }
}