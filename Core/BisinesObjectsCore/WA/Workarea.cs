using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace BusinessObjects
{
    /// <summary>
    /// Рабочая область
    /// </summary>
    public partial class Workarea 
    {
        /// <summary>
        /// Коллекция шаблонов
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Коллекция шаблонов для данного типа объектов</returns>
        public virtual List<T> GetTemplates<T>(bool refresh=false) where T : class, IBase, new()
        {
            T item = new T { Workarea = this };//CreateNewObject<T>();
            if (typeof(T) == typeof(EntityType))
            {
                item.Id = (int)WhellKnownDbEntity.DbEntity;
            }
            List<T> collection = new List<T>();
            string keyCode = string.Format("Workarea.GetTemplates_{0}", item.Entity.Name);
            if (!refresh && Cashe.GetListCodeCasheData<T>().Exists(keyCode))
            {
                return Cashe.GetListCodeCasheData<T>().Get(keyCode);
            }
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = string.Empty;
                        if (item.EntityId != 0)
                        {
                            ProcedureMap procedureMap = item.Entity.FindMethod("LoadTemplates");
                            if (procedureMap != null)
                            {
                                procedureName = procedureMap.FullName;
                            }
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new T { Workarea = this };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                        Cashe.GetListCodeCasheData<T>().Add(keyCode, collection);
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }

        /// <summary>
        /// Список отчетов
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="value">Значение</param>
        /// <param name="onlyKind">Учитывать только отчеты для соответствующего типа</param>
        /// <returns></returns>
        public List<ReportChain<T,T2>> GetReports<T,T2>(T2 value, bool onlyKind = true) where T2 : class, IBase, new() where T: class, ICoreObject, IEntityType
        {
            if (value is IReportChainSupport)
            {
                using (SqlConnection cnn = GetDatabaseConnection())
                {
                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = value.Entity.FindMethod(GlobalMethodAlias.ReportChainByElement).FullName;
                            cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                            if (onlyKind)
                                cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = value.KindId;
                            if (cnn.State != ConnectionState.Open)
                                cmd.Connection.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            List<ReportChain<T, T2>> coll = new List<ReportChain<T, T2>>();
                            while (reader.Read())
                            {
                                ReportChain<T, T2> item = new ReportChain<T, T2> { ElementId = value.Id, Workarea = value.Workarea };
                                item.Load(reader);
                                coll.Add(item);
                            }

                            return coll;
                        }
                    }
                    finally
                    {
                        cnn.Close();
                    }
                }
            }
            return null;
        }
        //public List<ReportChain> GetReports<T>(T value, bool onlyKind=true) where T : class, IBase, new()
        //{
        //    if (value is IReportChainSupport)
        //    {
        //        using (SqlConnection cnn = GetDatabaseConnection())
        //        {
        //            try
        //            {
        //                using (SqlCommand cmd = cnn.CreateCommand())
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.CommandText = value.Entity.FindMethod("ReportChainByElement").FullName;
        //                    cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
        //                    if(onlyKind)
        //                        cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = value.KindId;
        //                    if(cnn.State!= ConnectionState.Open)
        //                        cmd.Connection.Open();
        //                    SqlDataReader reader = cmd.ExecuteReader();
        //                    List<ReportChain> coll = new List<ReportChain>();
        //                    while (reader.Read())
        //                    {
        //                        ReportChain item = new ReportChain{ ElementId = value.Id, Workarea = value.Workarea };
        //                        item.Load(reader);
        //                        coll.Add(item);
        //                    }

        //                    return coll;
        //                }
        //            }
        //            finally
        //            {
        //                cnn.Close();
        //            }
        //        }
        //    }
        //    return null;
        //}
        //public List<ReportChainDocument> GetReportsDocument<T>(T value, bool onlyKind = true) where T : class, IBase, new()
        //{
        //    if (value is IReportChainSupport)
        //    {
        //        using (SqlConnection cnn = GetDatabaseConnection())
        //        {
        //            try
        //            {
        //                using (SqlCommand cmd = cnn.CreateCommand())
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.CommandText = value.Entity.FindMethod("ReportChainByElement").FullName;
        //                    cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
        //                    if (onlyKind)
        //                        cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = value.KindId;
        //                    if (cnn.State != ConnectionState.Open)
        //                        cmd.Connection.Open();
        //                    SqlDataReader reader = cmd.ExecuteReader();
        //                    List<ReportChainDocument> coll = new List<ReportChainDocument>();
        //                    while (reader.Read())
        //                    {
        //                        ReportChainDocument item = new ReportChainDocument { ElementId = value.Id, Workarea = value.Workarea };
        //                        item.Load(reader);
        //                        coll.Add(item);
        //                    }

        //                    return coll;
        //                }
        //            }
        //            finally
        //            {
        //                cnn.Close();
        //            }
        //        }
        //    }
        //    return null;
        //}
        // TODO: Пренести в класс "Список"
        /// <summary>
        /// Таблица данных, содержащая список документов по указанному списку
        /// </summary>
        /// <param name="listViewId">Идентификатор списка <see cref="BusinessObjects.CustomViewList"/></param>
        /// <param name="dbEntityKind">Идентификатор системного типа <see cref="EntityType.Id"/></param>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns></returns>
        public virtual DataTable GetDocumetsListByListView(int listViewId, int dbEntityKind, int id)
        {
            DataTable tbl = new DataTable();
            CustomViewList list = Cashe.GetCasheData<CustomViewList>().Item(listViewId);
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return null;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = list.SystemName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = dbEntityKind;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = Period.Start;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = Period.End;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tbl);
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            
            return tbl;
        }
    }
}
