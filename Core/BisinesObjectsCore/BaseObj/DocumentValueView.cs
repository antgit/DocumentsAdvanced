using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessObjects.Documents;

namespace BusinessObjects
{
    /// <summary>
    /// Представление значений связанных документов
    /// </summary>
    public sealed class DocumentValueView : ChainValueView
    {
        /// <summary>
        /// Полное наименование документа
        /// </summary>
        public string DocNameFull { get; set; }
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocDate { get; set; }
        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocNumber { get; set; }
        
        /// <summary>
        /// Раздел документа
        /// </summary>
        public string DocTypeName { get; set; }
        
        /// <summary>
        /// Компания владелец
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Наименование клиента
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="reader">Объект чтения данных</param>
        public override void Load(SqlDataReader reader)
        {
            base.Load(reader);
            
            DocNameFull = reader.IsDBNull(20) ? String.Empty : reader.GetString(20);
            DocDate = reader.GetDateTime(21);
            DocNumber = reader.IsDBNull(22) ? String.Empty : reader.GetString(22);
            DocTypeName = reader.IsDBNull(23) ? String.Empty : reader.GetString(23);
            CompanyName = reader.IsDBNull(24) ? String.Empty : reader.GetString(24);
            ClientName = reader.IsDBNull(25) ? String.Empty : reader.GetString(25);
        }
        public static List<DocumentValueView> GetView<T>(T value, int kindId = 0)
            where T : class, IBase, new()
        {
            DocumentValueView item;
            List<DocumentValueView> collection = new List<DocumentValueView>();
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        string methotAlias = typeof(T).Name + typeof(Document).Name + "GetView";

                        cmd.CommandText = value.Workarea.Empty<T>().Entity.FindMethod(methotAlias).FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new DocumentValueView { Workarea = value.Workarea };
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
            }
            return collection;
        }
        public static DocumentValueView ConvertToView<T, T2>(ChainAdvanced<T, T2> value)
            where T : class, IBase, new()
            where T2 : class, IBase, new()
        {
            DocumentValueView obj = new DocumentValueView();
            obj.Workarea = value.Workarea;
            obj.Id = value.Id;
            obj.Date = value.DateModified;
            obj.FlagsValue = value.FlagsValue;
            obj.Code = value.Code;
            obj.Memo = value.Memo;
            obj.OrderNo = value.OrderNo;
            obj.KindId = value.KindId;
            obj.KindName = value.Kind.Name;
            obj.KindCode = value.Kind.Code;
            obj.StateId = value.StateId;
            obj.StateName = value.State.Name;
            obj.LeftId = value.Left.Id;
            obj.LeftName = value.Left.Name;
            obj.LeftCode = value.Left.Code;
            obj.RightId = value.Right.Id;
            obj.RightName = value.Right.Name;
            obj.RightCode = value.Right.Code;
            obj.RightKind = value.Right.KindValue;
            obj.RightMemo = value.Right.Memo;
            if (value.Right is IHierarchySupport)
            {
                Hierarchy h = (value.Right as IHierarchySupport).FirstHierarchy();
                if (h != null)
                    obj.GroupName = h.Name;
            }
            obj.DocNameFull = value.Right.NameFull;
            obj.DocDate = (value.Right as Document).Date;
            obj.DocNumber = (value.Right as Document).Number;
            
            obj.DocTypeName =
                value.Right.Workarea.CollectionDocumentTypes().Find(
                    s => s.Id == (value.Right as Document).GetTypeValue()).Name;

            if ((value.Right as Document).MyCompany!=null)
                obj.CompanyName = (value.Right as Document).MyCompany.Name;

            if ((value.Right as Document).Client!=null)
                obj.ClientName = (value.Right as Document).Client.Name;

            return obj;
        }

        public static ChainAdvanced<T, Document> ConvertToValue<T>(T item, DocumentValueView c)
            where T : class, IBase, new()
        {
            ChainAdvanced<T, Document> val = new ChainAdvanced<T, Document> { Workarea = c.Workarea, Left = item };
            val.Load(c.Id);
            return val;
        }
        public ChainAdvanced<T, Document> ConvertToValue<T>(T itemLeft, Document itemRight)
            where T : class, IBase, new()
            
        {
            return ConvertToValue<T, Document>(itemLeft, itemRight, this);
        }

        public static ChainAdvanced<T, Document> ConvertToValue<T>(T itemLeft, Document itemRight, DocumentValueView c)
            where T : class, IBase, new()
            
        {
            ChainAdvanced<T, Document> val = new ChainAdvanced<T, Document> { Workarea = c.Workarea, Left = itemLeft, Right = itemRight };
            val.Load(c.Id);
            return val;
        }
    }
}