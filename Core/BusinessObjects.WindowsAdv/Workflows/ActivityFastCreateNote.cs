using System.Activities;
using System.Linq;

namespace BusinessObjects.Windows.Workflows
{
    public sealed class ActivityFastCreateNote<T>: CodeActivity<Note> where T: class, IBase, new()
    {
        public ActivityFastCreateNote()
            : base()
        {
            this.DisplayName = "Ѕыстрое создание пользовательского примечани€";
        }
        // Define an activity input argument of type string
        public InArgument<T> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override Note Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            Note tml = wa.GetTemplates<Note>().First(s => s.KindValue == Note.KINDVALUE_USER);
            Note newNote = wa.CreateNewObject<Note>(tml);
            newNote.UserOwnerId = wa.CurrentUser.Id;
            T owner = CurrentObject.Get(context);

            newNote.Created += delegate
                                   {
                                       ChainNotes<T> newNoteValue = new ChainNotes<T> { Workarea = wa };
                                       newNoteValue.Left = owner;
                                       newNoteValue.RightId = newNote.Id;
                                       newNoteValue.StateId = State.STATEACTIVE;
                                       newNoteValue.UserOwnerId = newNote.UserOwnerId;
                                       newNoteValue.KindId =
                                           wa.CollectionChainKinds.Find(
                                               f => f.Code == ChainKind.NOTES & f.FromEntityId == newNoteValue.Left.EntityId).Id;
                                       newNoteValue.Save();

                                   };
            newNote.ShowProperty();
            return newNote;
        }
    }
}