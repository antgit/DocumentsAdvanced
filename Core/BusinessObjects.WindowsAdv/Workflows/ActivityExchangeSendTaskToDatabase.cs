using System.Collections.Generic;
using BusinessObjects;
using System.Activities;
using BusinessObjects.Security;
using BusinessObjects.Workflows.Exchange;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>
    /// Ёкспорт задачи в базу данных
    /// </summary>
    public sealed class ActivityExchangeSendTaskToDatabase : CodeActivity<bool>
    {

        public ActivityExchangeSendTaskToDatabase()
        {
            this.DisplayName = "Ёкспорт задачи в базу данных";
        }
        public InArgument<Task> CurrentObject { get; set; }
        protected override bool Execute(CodeActivityContext context)
        {
            Workarea wa = CurrentObject.Get(context).Workarea;
            Task owner = CurrentObject.Get(context);
            bool canprocess = true;
            List<Branche> coll = wa.MyBranche.BrowseList(s => s.KindId == Branche.KINDID_DEFAULT && s.Id != wa.MyBranche.Id, null);
            if (coll != null && coll.Count > 0)
            {
                //ExchangeSendTaskToDatabase act = new ExchangeSendTaskToDatabase();
                //act.CurrentObject = owner;
                //act.DestinationBranche = coll[0];
                //WorkflowInvoker.Invoke(act);
                Branche toBranche = coll[0];
                toBranche.Workarea.GetDatabaseConnectionAdmin();
                Workarea newwa = toBranche.GetWorkarea();

                Task destination = newwa.GetObject<Task>(owner.Guid);
                if (destination == null || destination.Id == 0)
                {
                    destination = new Task();
                    destination.Workarea = newwa;
                    destination.Guid = owner.Guid;
                }
                (destination as ICopyValue<Task>).CopyValue(owner);


                destination.KindId = owner.KindId;
                destination.Memo = owner.Memo;
                destination.MemoTxt = owner.MemoTxt;

                Analitic priority = newwa.GetObject<Analitic>(owner.Priority.Guid);
                if (priority != null)
                {
                    destination.PriorityId = priority.Id;
                }
                else
                {
                    canprocess = false;
                }
                if (!canprocess)
                    return false;
                Analitic taskstate = newwa.GetObject<Analitic>(owner.TaskState.Guid);
                if (taskstate != null)
                {
                    destination.TaskStateId = taskstate.Id;
                }
                else
                {
                    canprocess = false;
                }

                if (!canprocess)
                    return false;

                Uid userFrom = newwa.GetObject<Uid>(owner.UserOwner.Guid);
                if (userFrom != null)
                    destination.UserOwnerId = userFrom.Id;
                else
                    canprocess = false;
                if (!canprocess)
                    return false;

                Uid userTo = newwa.GetObject<Uid>(owner.UserTo.Guid);
                if (userTo != null)
                    destination.UserToId = userTo.Id;
                else
                    canprocess = false;
                if (!canprocess)
                    return false;

                destination.Save();

                Hierarchy h = owner.FirstHierarchy();

                Hierarchy newH = newwa.GetObject<Hierarchy>(h.Guid);
                if (newH != null && newH.Id != 0)
                {
                    newH.ContentAdd(destination);
                }
                return true;
            }
            return false;
        }
    }
}