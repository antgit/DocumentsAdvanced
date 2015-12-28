using System.Activities;
using System.Data.SqlClient;

namespace BusinessObjects.Windows.Workflows
{
    public sealed class ActivityBrancheAddLinkedServer : CodeActivity
    {
        public ActivityBrancheAddLinkedServer()
            : base()
        {
            this.DisplayName = "Создание линкед сервера";
        }
        // Define an activity input argument of type string
        public InArgument<Branche> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            Branche owner = CurrentObject.Get(context);

            using (SqlConnection cnn = wa.GetDatabaseConnectionAdmin())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    string FirstStr = string.Format("EXEC master.dbo.sp_addlinkedserver @srvproduct ='', @server = N'{0}', @provider=N'SQLNCLI', @datasrc=N'{1}'",
                                                    owner.ServerName, owner.IpAddress);

                    cmd.CommandText = FirstStr;
                    cmd.ExecuteNonQuery();
                    FirstStr = string.Format("EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'{0}',@useself=N'False',@locallogin=NULL,@rmtuser=N'{1}',@rmtpassword='{2}'",
                                             owner.ServerName, owner.Uid, owner.Password);
                    cmd.CommandText = FirstStr;
                    cmd.ExecuteNonQuery();
                }
            }
            
            
            
        }
    }
}