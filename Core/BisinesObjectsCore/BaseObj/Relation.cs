using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects
{
	/// <summary>
	/// Дополнительный класс работы со связанными сущностями
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TObject"></typeparam>
    public class RelationHelper<TSource, TObject>
        where TSource : class, IBase
        where TObject : ICoreObject, IRelationSingle
    {
        public RelationHelper()
        {
            
        }

        public TObject GetObject(TSource value)
        {
            TObject obj = Activator.CreateInstance<TObject>();

            if (value == null || value.Id == 0)
                return obj;

            obj.Workarea = value.Workarea;
            string procedureKey = obj.Schema+ "." + typeof (TObject).Name + "Load";
            string procedureName = value.Workarea.FindMethod(procedureKey).FullName;

            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) 
                    return obj;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.SingleRow);
                        if (reader.Read() && reader.HasRows)
                        {
                            obj.Load(reader);
                        }
                        else
                        {
                            obj.Id = value.Id;
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }

            return obj;
        }

        public List<TObject> GetListObject(TSource value)
        {
            TObject obj = Activator.CreateInstance<TObject>();
            List<TObject> tmpCollection = new List<TObject>();

            if (value == null || value.Id == 0)
                return tmpCollection;

            obj.Workarea = value.Workarea;
            string procedureName = obj.Entity.FindMethod("LoadByOwner").FullName;

            using (SqlConnection cnn = obj.Workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    return tmpCollection;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            obj.Load(reader);
                            tmpCollection.Add(obj);
                            obj = Activator.CreateInstance<TObject>();
                            obj.Workarea = value.Workarea;
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }

            return tmpCollection;
        }
    }
}
