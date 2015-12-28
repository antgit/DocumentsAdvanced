using System.Activities;
using System.Collections.Generic;
using BusinessObjects.Security;

namespace BusinessObjects.Windows.Workflows
{
    /// <summary>
    /// Ёкспорт владельца в базу данных
    /// </summary>
    public sealed class ActivityExchangeSendBrancheToDatabase : CodeActivity
    {

        public ActivityExchangeSendBrancheToDatabase()
        {
            this.DisplayName = "Ёкспорт владельца в базу данных";
        }
        public InArgument<Branche> CurrentObject { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Workarea wa = CurrentObject.Get(context).Workarea;
            Branche owner = CurrentObject.Get(context);
            List<Branche> coll = wa.MyBranche.BrowseList(s => s.KindId == Branche.KINDID_DEFAULT && s.Id != wa.MyBranche.Id, null);
            if (coll != null && coll.Count > 0)
            {
                Branche toBranche = coll[0];
                toBranche.Workarea.GetDatabaseConnectionAdmin();
                Workarea newwa = toBranche.GetWorkarea();

                Branche destination = newwa.GetObject<Branche>(owner.Guid);
                if (destination == null || destination.Id == 0)
                {
                    destination = new Branche();
                    destination.Workarea = newwa;
                    destination.Guid = owner.Guid;
                }
                (destination as ICopyValue<Branche>).CopyValue(owner);
                
                destination.KindId = owner.KindId;
                
                destination.Save();

                Hierarchy h = owner.FirstHierarchy();

                Hierarchy newH = newwa.GetObject<Hierarchy>(h.Guid);
                if (newH != null && newH.Id != 0)
                {
                    newH.ContentAdd(destination);
                }
            }
        }
    }
}