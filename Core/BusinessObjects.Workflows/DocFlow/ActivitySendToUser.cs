using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using BusinessObjects.Documents;
using BusinessObjects.Security;

namespace BusinessObjects.Workflows.DocFlow
{
    public sealed class ActivitySendToUser : CodeActivity
    {
        public ActivitySendToUser(): base()
        {
            this.DisplayName = "Направить документ на рассмотрение пользователю";
        }
        /// <summary>
        /// Текущее сообщение, которое требуется передать
        /// </summary>
        public InArgument<string> Message { get; set; }
        /// <summary>
        /// Документ владелец
        /// </summary>
        public InArgument<Document> Owner { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public InArgument<Uid> User { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            // взводим сообщение в Document.Workflow.
            // устанавливаем текущее состояние в "Задача поставлена".
            // Пользователю необходимо принять некоторые действия для установки текущего состояния в "Выполнено".

            
        }
    }
}
