//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Runtime.Serialization;

//namespace BusinessObjects
//{
//    /// <summary>
//    /// Что нового в программе, текущие задачи.
//    /// </summary>
//    public sealed class WhatNew : BasePropertyChangeSupport
//    {

//        #region Свойства
//        [IgnoreDataMember]
//        private Workarea _workarea;

//        /// <summary>
//        /// Рабоча область
//        /// </summary>
//        [IgnoreDataMember]
//        public Workarea Workarea
//        {
//            get { return _workarea; }
//            set { _workarea = value; }
//        }

//        private int _id;
//        /// <summary>
//        /// Идентификатор
//        /// </summary>
//        public int Id
//        {
//            get { return _id; }
//            set
//            {
//                if (value == _id) return;
//                OnPropertyChanging(GlobalPropertyNames.Id);
//                _id = value;
//                OnPropertyChanged(GlobalPropertyNames.Id);
//            }
//        }

//        private Guid _guid;
//        /// <summary>
//        /// Глобальный идентификатор
//        /// </summary>
//        public Guid Guid
//        {
//            get { return _guid; }
//            set
//            {
//                if (value == _guid) return;
//                OnPropertyChanging(GlobalPropertyNames.Guid);
//                _guid = value;
//                OnPropertyChanged(GlobalPropertyNames.Guid);
//            }
//        }


//        private string _name;
//        /// <summary>
//        /// Наименование
//        /// </summary>
//        public string Name
//        {
//            get { return _name; }
//            set
//            {
//                if (value == _name) return;
//                OnPropertyChanging(GlobalPropertyNames.Name);
//                _name = value;
//                OnPropertyChanged(GlobalPropertyNames.Name);
//            }
//        }

//        private string _memo;
//        /// <summary>
//        /// Описание
//        /// </summary>
//        public string Memo
//        {
//            get { return _memo; }
//            set
//            {
//                if (value == _memo) return;
//                OnPropertyChanging(GlobalPropertyNames.Memo);
//                _memo = value;
//                OnPropertyChanged(GlobalPropertyNames.Memo);
//            }
//        }


//        private DateTime _date;
//        /// <summary>
//        /// Дата
//        /// </summary>
//        public DateTime Date
//        {
//            get { return _date; }
//            set
//            {
//                if (value == _date) return;
//                OnPropertyChanging(GlobalPropertyNames.Date);
//                _date = value;
//                OnPropertyChanged(GlobalPropertyNames.Date);
//            }
//        }

//        /// <summary>
//        /// Месяц
//        /// </summary>
//        public int Month
//        {
//            get { return _date.Month; }
//        }

//        /// <summary>
//        /// Год
//        /// </summary>
//        public int Year
//        {
//            get { return _date.Year; }
//        }


//        private string _author;
//        /// <summary>
//        /// Автор, исполнитель
//        /// </summary>
//        public string Author
//        {
//            get { return _author; }
//            set
//            {
//                if (value == _author) return;
//                OnPropertyChanging(GlobalPropertyNames.Author);
//                _author = value;
//                OnPropertyChanged(GlobalPropertyNames.Author);
//            }
//        }


//        private DateTime? _dateEnd;
//        /// <summary>
//        /// Окончание работ
//        /// </summary>
//        public DateTime? DateEnd
//        {
//            get { return _dateEnd; }
//            set
//            {
//                if (value == _dateEnd) return;
//                OnPropertyChanging(GlobalPropertyNames.DateEnd);
//                _dateEnd = value;
//                OnPropertyChanged(GlobalPropertyNames.DateEnd);
//            }
//        }

//        private string _currentState;
//        /// <summary>
//        /// Текущее состояние
//        /// </summary>
//        public string CurrentState
//        {
//            get { return _currentState; }
//            set
//            {
//                if (value == _currentState) return;
//                OnPropertyChanging(GlobalPropertyNames.CurrentState);
//                _currentState = value;
//                OnPropertyChanged(GlobalPropertyNames.CurrentState);
//            }
//        }
        
//        #endregion
//        public bool CompareExchange(WhatNew value)
//        {
//            if (value.Guid != Guid)
//                return false;
//            if (value.Date != Date)
//                return false;
//            if (!value.DateEnd.Equals(DateEnd))
//                return false;
//            if (!StringNullCompare(value.Author, Author))
//                return false;
//            if (!StringNullCompare(value.Memo, Memo))
//                return false;
            

//            return true;
//        }
//        public void Refresh()
//        {
            
//        }
//        public void Load(SqlDataReader reader)
//        {
//            try
//            {
//                _id = reader.GetInt32(0);
//                _guid = reader.GetGuid(1);
//                _name = reader.GetString(2);
//                _memo = reader.GetString(3);
//                _date = reader.GetDateTime(4);
//                _author = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
//                if (reader.IsDBNull(6))
//                    _dateEnd = null;
//                else
//                    _dateEnd = reader.GetDateTime(6);
//                _currentState = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
//            }
//            catch (Exception ex)
//            {
//                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
//            }
//        }
//        public void Save()
//        {
//            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
//            {
//                if (cnn == null) return;

//                try
//                {
//                    using (SqlCommand sqlCmd = cnn.CreateCommand())
//                    {
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.CommandText = "Core.WhatNewInsertUpdate";

//                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Id, SqlDbType.Int) {IsNullable = true};
//                        if (Id == 0)
//                            prm.Value = DBNull.Value;
//                        else
//                            prm.Value = Id;
//                        sqlCmd.Parameters.Add(prm);

//                        prm = new SqlParameter(GlobalSqlParamNames.Guid, SqlDbType.UniqueIdentifier) {IsNullable = true};
//                        if (Guid == Guid.Empty)
//                            prm.Value = DBNull.Value;
//                        else
//                            prm.Value = Guid;
//                        sqlCmd.Parameters.Add(prm);

//                        prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255)
//                                  {
//                                      IsNullable = false,
//                                      Value = _name
//                                  };
//                        sqlCmd.Parameters.Add(prm);

//                        prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar) {IsNullable = true};
//                        if (string.IsNullOrEmpty(_memo))
//                            prm.Value = DBNull.Value;
//                        else
//                        {
//                            prm.Size = _memo.Length;
//                            prm.Value = _memo;
//                        }
//                        sqlCmd.Parameters.Add(prm);

//                        prm = new SqlParameter(GlobalSqlParamNames.Date, SqlDbType.DateTime)
//                        {
//                            IsNullable = false,
//                            Value = _date
//                        };
//                        sqlCmd.Parameters.Add(prm);

//                        prm = new SqlParameter(GlobalSqlParamNames.Author, SqlDbType.NVarChar) { IsNullable = true };
//                        if (string.IsNullOrEmpty(_author))
//                            prm.Value = DBNull.Value;
//                        else
//                        {
//                            prm.Size = _author.Length;
//                            prm.Value = _author;
//                        }
//                        sqlCmd.Parameters.Add(prm);

//                        prm = new SqlParameter(GlobalSqlParamNames.CurrentState, SqlDbType.NVarChar) { IsNullable = true };
//                        if (string.IsNullOrEmpty(_currentState))
//                            prm.Value = DBNull.Value;
//                        else
//                        {
//                            prm.Size = _currentState.Length;
//                            prm.Value = _currentState;
//                        }
//                        sqlCmd.Parameters.Add(prm);

//                        prm = new SqlParameter(GlobalSqlParamNames.DateEnd, SqlDbType.DateTime)
//                        {
//                            IsNullable = true,
//                        };
//                        if (_dateEnd.HasValue)
//                            prm.Value = _dateEnd;
//                        else
//                            prm.Value = DBNull.Value;
//                        sqlCmd.Parameters.Add(prm);

//                        prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
//                        sqlCmd.Parameters.Add(prm);
//                        SqlDataReader reader = sqlCmd.ExecuteReader();
//                        if (reader.HasRows)
//                        {
//                            reader.Read();
//                            Load(reader);
//                        }
//                        reader.Close();

//                        object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
//                        if (retval == null)
//                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));
//                        if ((Int32) retval != 0)
//                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32) retval);
//                    }
//                }
//                finally
//                {
//                    if (cnn.State != ConnectionState.Closed)
//                        cnn.Close();
//                }
//            }
//        }

//        public static WhatNew GetLast(Workarea wa)
//        {
//            WhatNew item=null;
//            if (wa == null)
//                return item;

//            using (SqlConnection cnn = wa.GetDatabaseConnection())
//            {
//                try
//                {
//                    using (SqlCommand sqlCmd = cnn.CreateCommand())
//                    {
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.CommandText = "Core.WhatNewsLoadLast";
//                        SqlDataReader reader = sqlCmd.ExecuteReader();
//                        if (reader.Read() && reader.HasRows)
//                        {
//                            item = new WhatNew { Workarea = wa };
//                            item.Load(reader);
//                        }
//                        reader.Close();
//                    }
//                }
//                finally
//                {
//                    if (cnn.State != ConnectionState.Closed)
//                        cnn.Close();
//                }
//            }
//            return item;
//        }
//        public static List<WhatNew> GetCollection(Workarea wa)
//        {
//            List<WhatNew> coll = new List<WhatNew>();
//            if (wa == null)
//                return coll;
            
//            using (SqlConnection cnn = wa.GetDatabaseConnection())
//            {
//                try
//                {
//                    using (SqlCommand sqlCmd = cnn.CreateCommand())
//                    {
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.CommandText = "Core.WhatNewLoadAll";
//                        SqlDataReader reader = sqlCmd.ExecuteReader();
//                        if (reader.HasRows)
//                        {
//                            while(reader.Read())
//                            {
//                                WhatNew item = new WhatNew {Workarea = wa};
//                                item.Load(reader);
//                                coll.Add(item);
//                            }
//                        }
//                        reader.Close();
//                    }
//                }
//                finally
//                {
//                    if (cnn.State != ConnectionState.Closed)
//                        cnn.Close();
//                }
//            }
//            return coll;
//        }
//        public static List<string> GetCategories(Workarea wa)
//        {
//            List<string> coll = new List<string>();
//            if (wa == null)
//                return coll;
            
//            using (SqlConnection cnn = wa.GetDatabaseConnection())
//            {
//                try
//                {
//                    using (SqlCommand sqlCmd = cnn.CreateCommand())
//                    {
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.CommandText = "Core.WhatNewLoadCategories";
//                        SqlDataReader reader = sqlCmd.ExecuteReader();
//                        if (reader.HasRows)
//                        {
//                            while(reader.Read())
//                            {
//                                coll.Add(reader.GetString(0));
//                            }
//                        }
//                        reader.Close();
//                    }
//                }
//                finally
//                {
//                    if (cnn.State != ConnectionState.Closed)
//                        cnn.Close();
//                }
//            }
//            return coll;
//        }

//        public static List<string> GetAuthors(Workarea wa)
//        {
//            List<string> coll = new List<string>();
//            if (wa == null)
//                return coll;

//            using (SqlConnection cnn = wa.GetDatabaseConnection())
//            {
//                try
//                {
//                    using (SqlCommand sqlCmd = cnn.CreateCommand())
//                    {
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.CommandText = "Core.WhatNewLoadAuthors";
//                        SqlDataReader reader = sqlCmd.ExecuteReader();
//                        if (reader.HasRows)
//                        {
//                            while (reader.Read())
//                            {
//                                coll.Add(reader.GetString(0));
//                            }
//                        }
//                        reader.Close();
//                    }
//                }
//                finally
//                {
//                    if (cnn.State != ConnectionState.Closed)
//                        cnn.Close();
//                }
//            }
//            return coll;
//        }
//        public static List<string> GetCurrentStates(Workarea wa)
//        {
//            List<string> coll = new List<string>();
//            if (wa == null)
//                return coll;

//            using (SqlConnection cnn = wa.GetDatabaseConnection())
//            {
//                try
//                {
//                    using (SqlCommand sqlCmd = cnn.CreateCommand())
//                    {
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.CommandText = "Core.WhatNewLoadCurrentStates";
//                        SqlDataReader reader = sqlCmd.ExecuteReader();
//                        if (reader.HasRows)
//                        {
//                            while (reader.Read())
//                            {
//                                coll.Add(reader.GetString(0));
//                            }
//                        }
//                        reader.Close();
//                    }
//                }
//                finally
//                {
//                    if (cnn.State != ConnectionState.Closed)
//                        cnn.Close();
//                }
//            }
//            return coll;
//        }

//        public void Load()
//        {
//            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
//            {
//                try
//                {
//                    using (SqlCommand sqlCmd = cnn.CreateCommand())
//                    {
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.CommandText = "Core.WhatNewLoad";
//                        sqlCmd.Parameters.Add(new SqlParameter {ParameterName = GlobalSqlParamNames.Id, Value = Id});
//                        SqlDataReader reader = sqlCmd.ExecuteReader();
//                        if (reader.Read() && reader.HasRows)
//                        {
//                            Load(reader);
//                        }
//                        reader.Close();
//                    }
//                }
//                finally
//                {
//                    if (cnn.State != ConnectionState.Closed)
//                        cnn.Close();
//                }
//            }
//        }
//        public void Delete()
//        {
//            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
//            {
//                try
//                {
//                    using (SqlCommand sqlCmd = cnn.CreateCommand())
//                    {
//                        sqlCmd.CommandType = CommandType.StoredProcedure;
//                        sqlCmd.CommandText = "Core.WhatNewDelete";
//                        sqlCmd.Parameters.Add(new SqlParameter { ParameterName = GlobalSqlParamNames.Id, Value = Id });
//                        sqlCmd.ExecuteNonQuery();
//                    }
//                }
//                finally
//                {
//                    if (cnn.State != ConnectionState.Closed)
//                        cnn.Close();
//                }
//            }
//        }

//        public override bool SaveState(bool overwrite)
//        {
//            //throw new NotImplementedException();
//            return true;
//        }

//        public override void RestoreState()
//        {
//            //throw new NotImplementedException();
//        }
//    }
//}