using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using BusinessObjects.Security;

namespace BusinessObjects
{
    public partial class Workarea
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public void SaveObjects(List<ICoreObject> collection)
        {
            if (collection == null)
                return;
            if (collection.Count == 0)
                return;
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    SqlTransaction transaction = cnn.BeginTransaction("SaveObjectsTransaction");
                    // try
                    try
                    {
                        foreach (ICoreObject v in collection)
                        {

                            try
                            {
                                v.Save(transaction);
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }
                        }
                    }
                    catch (Exception exsave)
                    {
                        transaction.Rollback();
                        throw;
                    }

                    transaction.Commit();
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }
        internal int GetMaxFlagValue(int dbEntityId)
        {
            int res;
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindMethod("Core.FlagValuesMaxByDbEntity").FullName;

                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = dbEntityId;
                        res = (int)cmd.ExecuteScalar();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return res;
        }
        /// <summary>
        /// Удалить запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="procedureName">Хранимая процедура</param>
        [Obsolete("Более не использовать!!!")]
        public void DeleteById(int id, string procedureName)
        {
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                        cmd.Parameters.Add(prm);
                        cmd.ExecuteNonQuery();
                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((int)retval != 0)
                            throw new DatabaseException(Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }

        }
        //public void ChangeStateId(int id, int stateId, string procedureName)
        //{
        //    if (id == 0)
        //        throw new ArgumentOutOfRangeException("id", Cashe.ResourceString("EX_MSG_IDISZERO", 1049));
        //    if (stateId < 0 && stateId > 4)
        //        throw new ArgumentOutOfRangeException("stateId", Cashe.ResourceString("EX_MSG_STATEIDOUTOFRANGE", 1049));
        //    using (SqlConnection cnn = GetDatabaseConnection())
        //    {
        //        try
        //        {
        //            using (SqlCommand cmd = cnn.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = procedureName;
        //                cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
        //                cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateId;
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        finally
        //        {
        //            cnn.Close();
        //        }
        //    }
        //}
        private string _pConnectionString = string.Empty;
        private string _pCnnStr = string.Empty;			   // Кешированная строка соединения с приемником

        /// <summary>Строка соединения для базы данных Акцент</summary>
        /// <remarks>После установки строки соединения необходимо выполнить подключение к базе данных методом 
        /// <see cref="LogOn"/></remarks>
        public string ConnectionString
        {
            get { return _pConnectionString; }
            set
            {
                if (value == _pConnectionString) return;
                _pConnectionString = value;
                _pCnnStr = string.Empty;
            }
        }

        /// <summary>
        /// Текущий код соединения
        /// </summary>
        public string ConnectionCode { get; set; }

        private string _userName;
        /// <summary>Имя пользователя системы</summary>
        public string UserName
        {
            get { return _userName; }
        }

        /// <summary>Подключение к базе данных</summary>
        /// <param name="userName">Имя пользователя - используется при аутентификации SQL Server, 
        /// в противном случае допускается значение <c>null</c> или <c>String.Empty</c></param>
        /// <returns><c>true</c> в случае успешного подключения и <c>false</c> в противном случае</returns>
        public bool LogOn(string userName)
        {
            string st;
            _userName = userName;
            if (string.IsNullOrEmpty(_userName))
                _userName = Environment.UserName;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_pConnectionString);
            if (builder.IntegratedSecurity)
            {
                st = _pConnectionString;
            }
            else if (string.IsNullOrEmpty(builder.UserID))
            {
                return false;
            }
            else
            {
                st = _pConnectionString;
            }
            string oldCnnString = _pCnnStr;
            try
            {
                using (SqlConnection cnnDst = new SqlConnection())
                {
                    cnnDst.ConnectionString = st;
                    cnnDst.Open();
                    cnnDst.Close();
                }

                _pCnnStr = st;
                OnAfterLogin();
                return true;
            }
            catch (Exception)
            {
                _pCnnStr = oldCnnString;
                throw;
            }
        }
        private BusinessObjects.LicValidator licValidator;
        private bool _afterlogon;
        /// <summary>
        /// Выполняется после входа в систему
        /// </summary>
        private void OnAfterLogin()
        {
            MakeCasheData();

            int id = Empty<SystemParameter>().ExistsGuids(new Guid("72C6B5CB-B53B-47E8-9986-D61F39AD2E8A"));
            int? myCompanyCode = Cashe.GetCasheData<SystemParameter>().Item(id).ValueInt;
            _myBranche = Cashe.GetCasheData<Branche>().Item(myCompanyCode.Value);

            int langPrmId = Empty<SystemParameter>().ExistsGuids(new Guid("a4c52f0b-e58c-dd11-80d9-000c6e4ec13d"));
            int? myLangId = Cashe.GetCasheData<SystemParameter>().Item(langPrmId).ValueInt;
            _langId = myLangId.HasValue ? myLangId.Value : 1049;

            CurrentUser = GetCurrentUid();
            if (CurrentUser == null)
                throw new SecurityException("Текущий пользователь не зарегестрирован в системе");
            if (CurrentUser.StateId != 1)
                throw new SecurityException("Вход в систему текущим пользователем заблокирован администратором");
            string v = Cashe.GetCasheData<SystemParameter>().Item(id).ValueString;
            int registeredCounts = 1;
            if(!string.IsNullOrEmpty(v))
            {
                // получить правильные данные

            }
            else
            {
                // нет данных
                //KillAllConnectionExcludeMine();
            }

            
            licValidator = new LicValidator();
            _afterlogon = true;
            //licValidator.IsLic(GetDatabaseConnection());
        }

        private void KillAllConnectionExcludeMine()
        {
            // убить все подключения в текущей базе
            try
            {
                //         SqlConnection myBrancheConnection =  _myBranche.GetDatabaseConnection();
                //         //ALTER DATABASE [Works] SET MULTI_USER WITH NO_WAIT
                //         SqlCommand cmd = myBrancheConnection.CreateCommand();
                //         cmd.CommandText = string.Format("IF (select user_access from sys.databases WHERE NAME= '{0}')<> 1 \n"
                //+ "BEGIN \n"
                //+ "	ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE \n"
                //+ "END", _myBranche.DatabaseName);
                //         cmd.ExecuteNonQuery();
                string sql = string.Format("DECLARE @SQL varchar(max) \n"
       + " SET @SQL = '' \n"
       + "  \n"
       + " SELECT @SQL = @SQL + 'Kill ' + Convert(varchar, SPId) + ';' \n"
       + " FROM sys.sysprocesses WHERE dbid = DB_ID() AND (loginame<> SYSTEM_USER and loginame<>'{0}' and nt_username<>'{0}') and [program_name]='Documents System 2011' AND SPId <> @@SPId \n"
        + " Print @SQL \n"
       + " exec (@SQL)", UserName);

                SqlConnection myBrancheConnection = _myBranche.GetDatabaseConnection();
                SqlCommand cmd = myBrancheConnection.CreateCommand();
                myBrancheConnection.InfoMessage += delegate(object sender, SqlInfoMessageEventArgs e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);        
                };
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {


            }
        }

        public int CountConnectedHost()
        {
            int count = 3;
            
            try
            {
                string sql = "SELECT count(DISTINCT [host_name])  \n"
                    + "FROM sys.dm_exec_sessions WHERE [program_name] ='Documents System 2011'   \n"
                    + "GROUP BY [host_name];";

                SqlConnection myBrancheConnection = _myBranche.GetDatabaseConnection();
                SqlCommand cmd = myBrancheConnection.CreateCommand();
                myBrancheConnection.InfoMessage += delegate(object sender, SqlInfoMessageEventArgs e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                };
                cmd.CommandText = sql;
                object obj = cmd.ExecuteScalar();
                if (obj != null)
                    count = (int) obj;

            }
            catch (Exception)
            {


            }
            return count;
        }



        /// <summary>Был ли произведен первичный вход в базу данных</summary>
        public bool IsDatabaseLogon
        {
            get
            {
                bool res = !string.IsNullOrEmpty(_pCnnStr);
                return res;
            }
        }

        
        /// <summary>Создаёт новое соединение с базой данных из кешированной строки соединения и открывает его</summary>
        public SqlConnection GetDatabaseConnection()
        {
            if (string.IsNullOrEmpty(_pCnnStr)) return null;
            SqlConnection cnnDst = new SqlConnection { ConnectionString = _pCnnStr };
            // TODO: ms-help://MS.VSCC.v90/MS.MSDNQTR.v90.en/fxref_system.data.sqlclient/html/2c745192-9129-c150-1897-78fd3acab48c.htm
            // TODO: ms-help://MS.VSCC.v90/MS.MSDNQTR.v90.en/fxref_system.data.sqlclient/html/522565ac-4aa5-78dc-3429-ff7969d2cbfd.htm
            cnnDst.Open();
#if(RELEASE)
            if (licValidator == null)
                licValidator = new LicValidator();
            if(IsDatabaseLogon && _afterlogon )
            {
                if(!licValidator.IsLic(_pCnnStr))
                throw new SecurityException("Отсутствует лицензия на сервер баз данных!");    
            }
#endif
            return cnnDst;
        }
        /// <summary>Создаёт новое соединение с базой данных из данных текущей базы данных и открывает его</summary>
        internal SqlConnection GetDatabaseConnectionAdmin()
        {
            if (MyBranche == null) return null;
            return MyBranche.GetDatabaseConnection();
        }
        /// <summary>Получить текущего пользователя</summary>
        /// <returns></returns>
        public Uid GetCurrentUid()
        {
            Uid uid = new Uid { Workarea = this };
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null) return uid;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Empty<Uid>().Entity.FindMethod("Secure.UserLoadByName").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = UserName;
                        SqlDataReader reader = cmd.ExecuteReader();


                        while (reader.Read())
                        {
                            uid.Load(reader);
                        }
                        reader.Close();
                        
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return uid;
        }
    }
}
