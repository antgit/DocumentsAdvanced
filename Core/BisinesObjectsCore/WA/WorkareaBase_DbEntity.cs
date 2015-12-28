using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessObjects
{
    public partial class Workarea
    {
        public ProcedureMap FindMethod(string methodName)
        {
            try
            {
                return GetGlobalMethods().FirstOrDefault(m => m.Method == methodName);
            }
            catch (Exception ex)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}", methodName), ex);
            }
        }
        private List<ProcedureMap> _dbEntityMethodsCollection;
        /// <summary>
        /// Коллекция глобальных методов
        /// </summary>
        /// <returns></returns>
        public virtual List<ProcedureMap> GetGlobalMethods()
        {
            if (_dbEntityMethodsCollection == null)
                _dbEntityMethodsCollection = new List<ProcedureMap>();
            else
                return _dbEntityMethodsCollection;
            
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return _dbEntityMethodsCollection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Core].[EntityMethodsLoadShared]";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ProcedureMap item = new ProcedureMap { Workarea = this };
                            item.Load(reader);
                            _dbEntityMethodsCollection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return _dbEntityMethodsCollection;
        }

        public virtual List<EntityPropertyName> GetEntityPropertyNames(int cultureId)
        {
            List<EntityPropertyName> coll = new List<EntityPropertyName>();

            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindMethod("EntityPropertyNamesLoadAll").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.CultureId, SqlDbType.Int).Value = cultureId;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            EntityPropertyName item = new EntityPropertyName { Workarea = this };
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
