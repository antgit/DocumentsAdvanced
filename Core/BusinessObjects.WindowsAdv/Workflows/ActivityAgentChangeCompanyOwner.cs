using System.Activities;
using System.Collections.Generic;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>
    /// Процесс изменение компании-владельца для корреспондента
    /// </summary>
    public sealed class ActivityAgentChangeCompanyOwner : CodeActivity
    {
        public ActivityAgentChangeCompanyOwner()
            : base()
        {
            this.DisplayName = "Изменение компании владельца корреспондента";
        }
        // Define an activity input argument of type string
        public InArgument<Agent> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            Agent owner = CurrentObject.Get(context);

            List<Agent> newOwner = owner.BrowseList(s => s.KindValue == Agent.KINDVALUE_MYCOMPANY,
                                                    wa.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY));

            if(newOwner!=null && newOwner.Count>0)
            {
                owner.MyCompanyId = newOwner[0].Id;
                owner.Save();
            }

            //newNote.Created += delegate
            //{
            //    ChainNotes<T> newNoteValue = new ChainNotes<T> { Workarea = wa };
            //    newNoteValue.Left = owner;
            //    newNoteValue.RightId = newNote.Id;
            //    newNoteValue.StateId = State.STATEACTIVE;
            //    newNoteValue.UserOwnerId = newNote.UserOwnerId;
            //    newNoteValue.KindId =
            //        wa.CollectionChainKinds.Find(
            //            f => f.Code == ChainKind.NOTES & f.FromEntityId == newNoteValue.Left.EntityId).Id;
            //    newNoteValue.Save();

            //};
            //newNote.ShowProperty();
        }
    }
}