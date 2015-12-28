using System;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using BusinessObjects;
using BusinessObjects.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestExistsId
{
    [TestClass]
    public class TestProducts
    {
        private Workarea WA;

        public TestProducts()
        {
            Uid user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            WA = new Workarea
                {
                    //ConnectionString =
                    //    "Data Source=.;Initial Catalog=Documents2012;Integrated Security=True;Application Name=Documents System 2012;Current Language=Russian"

                    ConnectionString =
                        "Data Source=srv-devdoc;Initial Catalog=Documents2011DMPZ;Integrated Security=False;User ID=LocalAdmin;Password=12345;Application Name=Documents System 2012 DEV;Current Language=Russian"
                };
            WA.LogOn(user.Name);
        }
        [TestMethod]
        public void Test_SaveInTrans()
        {
            System.Diagnostics.Debug.WriteLine("Test Start - Test_SaveInTrans");
            Product tml =  WA.GetTemplates<Product>().FirstOrDefault(s => s.KindId == Product.KINDID_AUTO);
            Product prod = WA.CreateNewObject<Product>(tml);

            /*
             using(TransactionScope tran = new TransactionScope()) {
    // create command, exec sp1, exec sp2 - without mentioning "tran" or
    // anything else transaction related

    tran.Complete();
}

             */

            using (SqlConnection con = WA.GetDatabaseConnection())
            {
                con.InfoMessage += delegate(object sender, SqlInfoMessageEventArgs e)
                    {
                        Console.WriteLine(e.Message);
                    };
                con.StateChange += delegate(object sender, System.Data.StateChangeEventArgs e)
                    {
                        Console.WriteLine(e.CurrentState);
                        Console.WriteLine(e.OriginalState);
                    };
                SqlTransaction tran = con.BeginTransaction("TestSave");
                
                try
                {
                    prod.Save(tran);
                    tran.Save("ProductSaved");
                    SqlCommand cmd = con.CreateCommand();
                    cmd.Transaction = tran;
                    cmd.CommandText = "Select  @@TRANCOUNT";
                    object val = cmd.ExecuteScalar();
                    prod.Auto.Save(tran);
                    tran.Commit();
                }
                catch (Exception)
                {
                    try
                    {
                        SqlCommand cmd = con.CreateCommand();
                        cmd.Transaction = tran;
                        cmd.CommandText = "Select  @@TRANCOUNT";
                        object val = cmd.ExecuteScalar();
                    
                        if (tran != null) tran.Rollback("TestSave"); 
                        //tran.Rollback("TestSave");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                finally
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.Transaction = tran;
                    cmd.CommandText = "Select  @@TRANCOUNT";
                    object val = cmd.ExecuteScalar();

                    con.Close();
                    if (con != null)
                        con.Dispose();
                    if (tran != null)
                        tran.Dispose();
                }
            }

            Test_SaveInTrans2();
            System.Diagnostics.Debug.WriteLine("Test End - Test_SaveInTrans");
        }

        public void Test_SaveInTrans2()
        {
            System.Diagnostics.Debug.WriteLine("Test Start - Test_SaveInTrans");
            Product tml = WA.GetTemplates<Product>().FirstOrDefault(s => s.KindId == Product.KINDID_AUTO);
            Product prod = WA.CreateNewObject<Product>(tml);

            using (SqlConnection con = WA.GetDatabaseConnection())
            {
                con.InfoMessage += delegate(object sender, SqlInfoMessageEventArgs e)
                {
                    Console.WriteLine(e.Message);
                };
                con.StateChange += delegate(object sender, System.Data.StateChangeEventArgs e)
                {
                    Console.WriteLine(e.CurrentState);
                    Console.WriteLine(e.OriginalState);
                };
                SqlTransaction tran = con.BeginTransaction("TestSave");

                try
                {
                    prod.Save(tran);
                    tran.Save("ProductSaved");
                    SqlCommand cmd = con.CreateCommand();
                    cmd.Transaction = tran;
                    cmd.CommandText = "Select  @@TRANCOUNT";
                    object val = cmd.ExecuteScalar();
                    prod.Auto.Save(tran);
                    tran.Commit();
                }
                catch (Exception)
                {
                    try
                    {
                        SqlCommand cmd = con.CreateCommand();
                        cmd.Transaction = tran;
                        cmd.CommandText = "Select  @@TRANCOUNT";
                        object val = cmd.ExecuteScalar();

                        if (tran != null) tran.Rollback("TestSave");
                        //tran.Rollback("TestSave");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                finally
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.Transaction = tran;
                    cmd.CommandText = "Select  @@TRANCOUNT";
                    object val = cmd.ExecuteScalar();

                    con.Close();
                    if (con != null)
                        con.Dispose();
                    if (tran != null)
                        tran.Dispose();
                }
            }
            System.Diagnostics.Debug.WriteLine("Test End - Test_SaveInTrans");
        }

        
    }
}