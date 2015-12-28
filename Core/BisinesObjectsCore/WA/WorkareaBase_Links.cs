using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace BusinessObjects
{
    public partial class Workarea
    {
        /// <summary>
        /// Коллекция видов связей
        /// </summary>
        /// <param name="kind">Числовое представление типа</param>
        /// <returns></returns>
        private List<ChainKind> GetCollectionChainKinds(Int16 kind)
        {
            List<ChainKind> collection = new List<ChainKind>();
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindMethod("Core.ChainKindsLoadAll").FullName;
                        if (kind != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.SmallInt).Value = kind;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ChainKind item = new ChainKind {Workarea = this};
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

        List<ChainKind> _collectionLinkKinds;
        /// <summary>
        /// Коллекция видов связей
        /// </summary>
        public List<ChainKind> CollectionChainKinds 
        { 
            get { return _collectionLinkKinds ?? (_collectionLinkKinds = GetCollectionChainKinds(0)); }
        }

        List<Library> _collectionPages;
        /// <summary>
        /// Коллекция страниц-закладок используемых для построения интерфейса программы
        /// </summary>
        public List<Library> CollectionPages
        {
            get { return _collectionPages ?? (_collectionPages = GetCollectionPages()); }
        }
        public List<Library> GetCollectionPages()
        {
            List<Library> collection = new List<Library>();
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Empty<Library>().Entity.FindMethod("LibraryLoadAllPages").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Library item = new Library { Workarea = this };
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

        /// <summary>
        /// Поменять местами две цепочки
        /// </summary>
        /// <remarks>Изменяется только порядковый номер</remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <exception cref="ArgumentOutOfRangeException"><c>Типы связей должны быть одинаковые, нельзя перемещать разные типы связей</c> is out of range.</exception>
        /// <exception cref="NullReferenceException">Не указана цепочка назначения</exception>
        /// <exception cref="NullReferenceException">Не указана цепочка источника</exception>
        public void Swap<T>(Chain<T> from, Chain<T> to) where T : class, IBase, new()
        {
            if (from == null)
                throw new ArgumentNullException(Cashe.ResourceString("EX_MSG_CHAINSOURCENULL", 1049), (Exception)null);
            if(to==null)
                throw new ArgumentNullException(Cashe.ResourceString("EX_MSG_CHAINDESTINATIONNULL", 1049), (Exception)null);

            if (from.KindId != to.KindId)
                throw new ArgumentOutOfRangeException(string.Empty, Cashe.ResourceString("EX_MSG_CHAINKINDNONEQUAL", 1049));

            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = from.Left.Entity.FindMethod("ChainSwap").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = from.Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.ToId, SqlDbType.Int).Value = to.Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if(reader.Read())
                        {
                            from.Load(reader);
                        }
                        if (reader.NextResult())
                        {
                            if (reader.Read())
                            {
                                to.Load(reader);
                            }
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /// <summary>
        /// Поменять местами две цепочки
        /// </summary>
        /// <remarks>Изменяется только порядковый номер</remarks>
        /// <typeparam name="T">Тип "Откуда"</typeparam>
        /// <typeparam name="TDestination">Тип "Куда"</typeparam>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        public void Swap<T, TDestination>(ChainAdvanced<T, TDestination> from, ChainAdvanced<T, TDestination> to) 
            where T : class, IBase, new()
            where TDestination : class, IBase, new()
        {
            if (from == null)
                throw new ArgumentNullException(Cashe.ResourceString("EX_MSG_CHAINSOURCENULL", 1049), (Exception)null);
            if (to == null)
                throw new ArgumentNullException(Cashe.ResourceString("EX_MSG_CHAINDESTINATIONNULL", 1049), (Exception)null);

            //if (from.KindId != to.KindId)
            //    throw new ArgumentOutOfRangeException(string.Empty, Cashe.ResourceString("EX_MSG_CHAINKINDNONEQUAL", 1049));

            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        string methotAlias = typeof(T).Name + typeof(TDestination).Name;
                        cmd.CommandText = from.Left.Entity.FindMethod(methotAlias + "ChainSwap").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = from.Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.ToId, SqlDbType.Int).Value = to.Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            from.Load(reader);
                        }
                        if (reader.NextResult())
                        {
                            if (reader.Read())
                            {
                                to.Load(reader);
                            }
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
    }
}
