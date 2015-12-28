using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects
{
    public partial class Workarea
    {
        /// <summary>
        /// Коллекция глобальных методов
        /// </summary>
        /// <returns></returns>
        public virtual List<CustomPropertyDescriptor> CustomPropertyDescriptors()
        {
            List<CustomPropertyDescriptor> coll = new List<CustomPropertyDescriptor>();
            // TODO: Срочно!!! ХП
            //using (SqlConnection cnn = GetDatabaseConnection())
            //{
            //    if (cnn == null) return coll;

            //    try
            //    {
            //        using (SqlCommand cmd = cnn.CreateCommand())
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            //TODO : Правильная процедура
            //            //cmd.CommandText = "[Core].[EntityMethodsLoadShared]";
            //            SqlDataReader reader = cmd.ExecuteReader();

            //            if (reader != null)
            //            {
            //                while (reader.Read())
            //                {
            //                    CustomPropertyDescriptor value = new CustomPropertyDescriptor { Workarea = this };
            //                    Action<SqlDataReader> loader = value.Load;
            //                    loader(reader);
            //                    coll.Add(value);
            //                }
            //                reader.Close();
            //            }
            //        }
            //    }
            //    finally
            //    {
            //        cnn.Close();
            //    }
            //}
            return coll;
        }

        /// <summary>
        /// Коллекция свойств объектов
        /// </summary>
        /// <returns></returns>
        public virtual List<EntityProperty> GetCollectionEntityProperties(int entityId)
        {
            List<EntityProperty> coll = new List<EntityProperty>();

            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindMethod("EntityPropertyLoadAll").FullName;
                        //cmd.Parameters.Add(GlobalSqlParamNames.CultureId, SqlDbType.Int).Value = entityId;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            EntityProperty item = new EntityProperty { Workarea = this };
                            item.Load(reader);
                            coll.Add(item);
                        }
                        reader.Close();
             
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return coll;
        }
    }
}
