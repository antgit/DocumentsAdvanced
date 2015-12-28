using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public static class Sales
    {
        /// <summary>
        /// Список всех товаров с текущими ценами на товар
        /// </summary>
        /// <param name="wa"></param>
        /// <returns></returns>
        public static DataTable GetProductListWithCurrentPrice(Workarea wa)
        {
            DataTable tbl = new DataTable("ProductListWithCurrentPrice");
            if (wa == null)
                return tbl;

            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "Sales.GetProductAllPrices";
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        da.Fill(tbl);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            return tbl;
        }

        /// <summary>
        /// Список всех товаров имеющих остатки на предприятии
        /// </summary>
        /// <param name="wa"></param>
        /// <returns></returns>
        public static DataTable GetProductListLeaveCurrent(Workarea wa)
        {
            DataTable tbl = new DataTable("ProductListWithCurrentPrice");
            if (wa == null)
                return tbl;

            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "Sales.ShowSalesProductLeaveCurrent";
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        da.Fill(tbl);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            return tbl;
        }
    }
}
