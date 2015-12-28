using System.Activities;
using System.Collections.Generic;

namespace BusinessObjects.Windows.Workflows
{
    public sealed class ActivityKnoweledgeMoveDatabase : CodeActivity
    {
        public ActivityKnoweledgeMoveDatabase()
            : base()
        {
            this.DisplayName = "Отправить статью в базу данных";
        }
        // Define an activity input argument of type string
        public InArgument<Knowledge> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            Knowledge owner = CurrentObject.Get(context);

            List<Branche> coll = wa.MyBranche.BrowseList(s => s.KindId == Branche.KINDID_DEFAULT && s.Id!=wa.MyBranche.Id, null);
            if(coll!=null && coll.Count>0)
            {
                Branche toBranche = coll[0];
                toBranche.Workarea.GetDatabaseConnectionAdmin();
                Workarea newwa = toBranche.GetWorkarea();

                Knowledge destination = newwa.GetObject<Knowledge>(owner.Guid);
                if(destination==null || destination.Id==0)
                {
                    destination = new Knowledge();
                    destination.Workarea = newwa;
                    destination.Guid = owner.Guid;
                }

                destination.KindId = owner.KindId;
                destination.Name = owner.Name;
                destination.CodeFind = owner.CodeFind;

                if(owner.FileId!=0)
                {
                    FileData fdata = newwa.GetObject<FileData>(owner.File.Guid);
                    if (fdata != null && fdata.Id!=0)
                    {
                        destination.FileId = fdata.Id;
                        if (!FileData.ByteArraysEqual(destination.File.StreamData, owner.File.StreamData))
                        {
                            destination.File.StreamData = owner.File.StreamData;
                            destination.File.Save();
                        }
                    }
                    else
                    {
                        fdata = new FileData();
                        fdata.Workarea = newwa;
                        (fdata as ICopyValue<FileData>).CopyValue(owner.File);
                        fdata.Guid = owner.File.Guid;
                        fdata.KindId = owner.File.KindId;
                        fdata.StreamData = owner.File.StreamData;
                        fdata.Save();
                        destination.FileId = fdata.Id;
                        Hierarchy hFdata = owner.File.FirstHierarchy();

                        Hierarchy newHFData = newwa.GetObject<Hierarchy>(hFdata.Guid);
                        if (newHFData != null && newHFData.Id != 0)
                        {
                            newHFData.ContentAdd(fdata);
                        }
                    }
                }

                destination.Save();

                Hierarchy h = owner.FirstHierarchy();

                Hierarchy newH = newwa.GetObject<Hierarchy>(h.Guid);
                if(newH!=null && newH.Id!=0)
                {
                    newH.ContentAdd(destination);
                }
            }
        }
    }
}