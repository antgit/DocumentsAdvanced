using System;
using System.Activities;
using System.ComponentModel;
using System.Drawing;

namespace BusinessObjects.Windows.Workflows
{
    ///// <summary>
    ///// Отметить задачу как выполненную
    ///// </summary>
    //[ToolboxBitmap(typeof(ActivityTaskSetDone), "YourImage.bmp")]
    //public sealed class ActivityTaskSetDone : CodeActivity
    //{
    //    public ActivityTaskSetDone()
    //        : base()
    //    {
    //        this.DisplayName = "Отметить задачу как выполненную";
    //    }
    //    // Define an activity input argument of type string
    //    [RequiredArgument]
    //    public InArgument<Task> CurrentObject { get; set; }

    //    // If your activity returns a value, derive from CodeActivity<TResult>
    //    // and return the value from the Execute method.
    //    protected override void Execute(CodeActivityContext context)
    //    {
    //        // Obtain the runtime value of the Text input argument
    //        //string text = context.GetValue(this.Text);
    //        Workarea wa = CurrentObject.Get(context).Workarea;
    //        Task owner = CurrentObject.Get(context);

    //        Analitic an = wa.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>("TASK_STATE_DONE");

    //        owner.DonePersent = 100;
    //        owner.DateEnd = DateTime.Today;
    //        owner.DateEndTime = DateTime.Now.TimeOfDay;
    //        if (an != null)
    //            owner.TaskStateId = an.Id;
    //        owner.Save();
    //    }
    //}
}