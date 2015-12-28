using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace BusinessObjects.Web.Core
{
    public class EquimpmentWebView
    {
        public int Id1 { get; set; }
        public int StateId1 { get; set; }
        public string Name1 { get; set; }
        public int KindId1 { get; set; }

        public int Id2 { get; set; }
        public int StateId2 { get; set; }
        public string Name2 { get; set; }
        public int KindId2 { get; set; }

        public int Id3 { get; set; }
        public int StateId3 { get; set; }
        public string Name3 { get; set; }
        public int KindId3 { get; set; }

        public int Id4 { get; set; }
        public int StateId4 { get; set; }
        public string Name4 { get; set; }
        public int KindId4 { get; set; }

        public int Id5 { get; set; }
        public int StateId5 { get; set; }
        public string Name5 { get; set; }
        public int KindId5 { get; set; }

		public string Nomenclature { get; set; }
		public string WorkShopType { get; set; }

        private Workarea _workarea;
        public Workarea Workarea
        {
            get { return _workarea; }
        }

        public void Load(SqlDataReader reader)
        {
            Id1 = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            StateId1 = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
            Name1 = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
            KindId1 = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);

            Id2 = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
            StateId2 = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
            Name2 = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
            KindId2 = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);

            Id3 = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
            StateId3 = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
            Name3 = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
            KindId3 = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);

            Id4 = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
            StateId4 = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
            Name4 = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
            KindId4 = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);

            Id5 = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
            StateId5 = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
            Name5 = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
            KindId5 = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);

			Nomenclature = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
			WorkShopType = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
        }

        public static List<EquimpmentWebView> GetView(Workarea wa)
        {
            List<EquimpmentWebView> collection = new List<EquimpmentWebView>();
            EquimpmentWebView item = new EquimpmentWebView();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        //string procedureName = "";
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "Select * From [Ourp].[EquipmentsTree]";

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new EquimpmentWebView { _workarea = wa };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
                return collection;
            }
        }
    }
}
