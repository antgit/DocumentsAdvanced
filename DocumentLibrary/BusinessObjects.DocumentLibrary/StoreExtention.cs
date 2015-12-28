using System;
using System.Data;
using System.Data.SqlClient;
namespace BusinessObjects.DocumentLibrary
{
    /// <summary>
    /// Дополнительные методы для складского учета
    /// </summary>
    public static class StoreExtention
    {
        /// <summary>
        /// Цена последнего прихода товара
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="date">Дата</param>
        /// <param name="productId">Идентификатор товара</param>
        /// <param name="agentFromId">Идентификатор корреспондента поставщика</param>
        /// <returns></returns>
        public static decimal GetLastPriceIn(Workarea wa, DateTime date, int productId, int agentFromId)
        {
            if (productId == 0) return 0;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return 0;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = wa.Empty<Product>().Entity.FindMethod("Store.GetLastPriceIn").FullName;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.ProductId, SqlDbType.Int).Value = productId;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Date, SqlDbType.DateTime).Value = date;
                        if(agentFromId!=0)
                            sqlCmd.Parameters.Add(GlobalSqlParamNames.AgentFromId, SqlDbType.Int).Value = agentFromId;

                        object val = sqlCmd.ExecuteScalar();
                        return (decimal)val;               
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }
    }
}
