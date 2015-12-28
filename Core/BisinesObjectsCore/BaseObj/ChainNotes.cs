using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>
    /// Цепочка пользовательских примечаний между объектами
    /// </summary>
    /// <typeparam name="T">Тип</typeparam>
    public class ChainNotes<T> : ChainAdvanced<T, Note> where T : class, IBase, new()
    {

        /// <summary>Конструктор</summary>
        public ChainNotes()
            : base()
        {
        }
        /// <summary>Конструктор</summary>
        /// <param name="left">Источник</param>
        public ChainNotes(T left)
            : this()
        {
            Left = left;
            Workarea = left.Workarea;
        }
    }

    /// <summary>
    /// Представление значений примечаний
    /// </summary>
    public sealed class NoteValueView : ChainValueView
    {

        public ChainNotes<T> ConvertToValue<T>(T item) where T : class, IBase, new()
        {
            return ConvertToValue<T>(item, this);
        }

        public static ChainNotes<T> ConvertToValue<T>(T item, NoteValueView c) where T : class, IBase, new()
        {
            ChainNotes<T> val = new ChainNotes<T> { Workarea = c.Workarea, Left = item };
            val.Load(c.Id);
            return val;
        }
        public static NoteValueView ConvertToView<T>(ChainNotes<T>  value) where T : class, IBase, new()
        {
            NoteValueView obj = new NoteValueView();
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

            obj.UserOwnerId = value.UserOwnerId;
            obj.UserOwnerName = value.UserName;
            if (value.Workarea.Cashe.GetCasheData<Uid>().Item(value.UserOwnerId).Agent != null)
                obj.WorkerName = value.Workarea.Cashe.GetCasheData<Uid>().Item(value.UserOwnerId).Agent.Name;
            else
                obj.WorkerName = string.Empty;
            return obj;
        }
        public static Note GetNote<T>(T item, NoteValueView c) where T: class, IBase, new()
        {
            Note obj = item.Workarea.Cashe.GetCasheData<Note>().Item(c.RightId);
            return obj;
        }

        public static List<NoteValueView> GetView<T>(T value, bool allKinds) where T : class, IBase, new()
        {
            NoteValueView item;
            List<NoteValueView> collection = new List<NoteValueView>();
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        string methotAlias = typeof(T).Name + "NoteGetView";
                        cmd.CommandText = value.Workarea.Empty<T>().Entity.FindMethod(methotAlias).FullName;
                        
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new NoteValueView { Workarea = value.Workarea };
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
    }
}