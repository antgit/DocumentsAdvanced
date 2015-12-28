using System.Activities;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>Показать коды документа</summary>
    public sealed class ActivityShowDocumentCodes: CodeActivity
    {
        public ActivityShowDocumentCodes()
        {
            this.DisplayName = "Показать коды документа";
        }
        /// <summary>
        /// Документ
        /// </summary>
        public InArgument<Documents.Document> Value { get; set; }
        /// <summary>
        /// Отображать модально
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
        public InArgument<bool> Modal { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Value.Get(context).ShowCodes();
        }
    }

    /// <summary>Показать текущие процессы документа</summary>
    public sealed class ActivityShowDocumentProcess : CodeActivity
    {
        public ActivityShowDocumentProcess()
        {
            this.DisplayName = "Показать процесы документа";
        }
        /// <summary>
        /// Документ
        /// </summary>
        public InArgument<Documents.Document> Value { get; set; }
        /// <summary>
        /// Отображать модально
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
        public InArgument<bool> Modal { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Value.Get(context).ShowWorkflowList();
        }
    }
}