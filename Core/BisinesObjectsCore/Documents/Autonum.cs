using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// Автонумерация документов
    /// </summary>
    public sealed class Autonum : BaseCoreObject, ICompanyOwner
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Autonum()
        {
            EntityId = (int)WhellKnownDbEntity.Autonum;
        }

        #region Свойства
        private int _number;
        /// <summary>
        /// Номер
        /// </summary>
        public int Number
        {
            get { return _number; }
            set
            {
                if (value == _number) return;
                OnPropertyChanging(GlobalPropertyNames.Number);
                _number = value;
                OnPropertyChanged(GlobalPropertyNames.Number);
            }
        }

        private int _docKind;
        /// <summary>
        /// Идентификатор типа документа
        /// </summary>
        public int DocKind
        {
            get { return _docKind; }
            set
            {
                if (value == _docKind) return;
                OnPropertyChanging(GlobalPropertyNames.DocKind);
                _docKind = value;
                OnPropertyChanged(GlobalPropertyNames.DocKind);
            }
        }
        /// <summary>
        /// Тип документа
        /// </summary>
        public EntityDocumentKind DocumentKind
        {
            get { return Workarea.CollectionDocumentKinds.FirstOrDefault(s => s.Id == DocKind); }
        }
        private int _userId;
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId
        {
            get { return _userId; }
            set
            {
                if (value == _userId) return;
                OnPropertyChanging(GlobalPropertyNames.UserId);
                _userId = value;
                OnPropertyChanged(GlobalPropertyNames.UserId);
            }
        }


        private string _prefix;
        /// <summary>
        /// Префикс
        /// </summary>
        public string Prefix
        {
            get { return _prefix; }
            set
            {
                if (value == _prefix) return;
                OnPropertyChanging(GlobalPropertyNames.Prefix);
                _prefix = value;
                OnPropertyChanged(GlobalPropertyNames.Prefix);
            }
        }


        private string _suffix;
        /// <summary>
        /// Суффикс
        /// </summary>
        public string Suffix
        {
            get { return _suffix; }
            set
            {
                if (value == _suffix) return;
                OnPropertyChanging(GlobalPropertyNames.Suffix);
                _suffix = value;
                OnPropertyChanged(GlobalPropertyNames.Suffix);
            }
        }


        private int _wfId;
        /// <summary>
        /// Идентификатор действия для формирования номера на основании сложных вычислений.
        /// </summary>
        public int WfId
        {
            get { return _wfId; }
            set
            {
                if (value == _wfId) return;
                OnPropertyChanging(GlobalPropertyNames.WfId);
                _wfId = value;
                OnPropertyChanged(GlobalPropertyNames.WfId);
            }
        }

        private string _code;
        /// <summary>
        /// Дополнительный код
        /// </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (value == _code) return;
                OnPropertyChanging(GlobalPropertyNames.Code);
                _code = value;
                OnPropertyChanged(GlobalPropertyNames.Code);
            }
        }

        private int _myCompanyId;
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит аналитика
        /// </summary>
        public int MyCompanyId
        {
            get { return _myCompanyId; }
            set
            {
                if (value == _myCompanyId) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompanyId);
                _myCompanyId = value;
                OnPropertyChanged(GlobalPropertyNames.MyCompanyId);
            }
        }


        private Agent _myCompany;
        /// <summary>
        /// Моя компания, предприятие которому принадлежит аналитика
        /// </summary>
        public Agent MyCompany
        {
            get
            {
                if (_myCompanyId == 0)
                    return null;
                if (_myCompany == null)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                else if (_myCompany.Id != _myCompanyId)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                return _myCompany;
            }
            set
            {
                if (_myCompany == value) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompany);
                _myCompany = value;
                _myCompanyId = _myCompany == null ? 0 : _myCompany.Id;
                OnPropertyChanged(GlobalPropertyNames.MyCompany);
            }
        }
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_number != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Number, XmlConvert.ToString(_number));
            if (_docKind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DocKind, XmlConvert.ToString(_docKind));
            if (_userId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserId, XmlConvert.ToString(_userId));
            if (_myCompanyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Number) != null)
                _number = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Number));
            if (reader.GetAttribute(GlobalPropertyNames.DocKind) != null)
                _docKind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DocKind));
            if (reader.GetAttribute(GlobalPropertyNames.UserId) != null)
                _userId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UserId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
        }
        #endregion

        
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _number = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                _docKind = !reader.IsDBNull(10) ? reader.GetInt32(10) : 0;
                _userId = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0;
                _prefix = !reader.IsDBNull(12) ? reader.GetString(12) : string.Empty;
                _suffix = !reader.IsDBNull(13) ? reader.GetString(13) : string.Empty;
                _wfId = !reader.IsDBNull(14) ? reader.GetInt32(14) : 0;
                _code = !reader.IsDBNull(15) ? reader.GetString(15) : string.Empty;
                _myCompanyId = !reader.IsDBNull(16) ? reader.GetInt32(16) : 0;
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Number, SqlDbType.Int) {Value = _number};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DocKind, SqlDbType.Int) {Value = _docKind};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserId, SqlDbType.Int);
            if (_userId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _userId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.WfId, SqlDbType.Int);
            if (_wfId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _wfId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Prefix, SqlDbType.NVarChar, 50);
            if (string.IsNullOrEmpty(_prefix))
                prm.Value = DBNull.Value;
            else
                prm.Value = _prefix;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Suffix, SqlDbType.NVarChar, 50);
            if (string.IsNullOrEmpty(_suffix))
                prm.Value = DBNull.Value;
            else
                prm.Value = _suffix;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100);
            if (string.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);

        }
        /// <summary>
        /// Автонумерация документа по типу документа
        /// </summary>
        /// <remarks>Сквозная автонумерация документов в разрезе типов документов</remarks>
        /// <param name="workarea">Рабочая область</param>
        /// <param name="documentKind">Тип документа</param>
        /// <returns></returns>
        public static Autonum GetAutonumByDocumentKind(Workarea workarea, int documentKind)
        {
            Autonum item = new Autonum {Workarea = workarea, DocKind = documentKind};
            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = workarea.FindMethod("AutonumLoadByDocKind").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = documentKind;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item.Load(reader);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return item;
        }
        /// <summary>
        /// Автонумерация документа по типу документа и коду
        /// </summary>
        /// <param name="workarea">Рабочая область</param>
        /// <param name="documentKind">Тип документа</param>
        /// <param name="code">Код</param>
        /// <returns></returns>
        public static Autonum GetAutonumByDocumentKind(Workarea workarea, int documentKind, string code)
        {
            Autonum item = new Autonum { Workarea = workarea, DocKind = documentKind, Code = code };
            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = workarea.FindMethod("AutonumLoadByDocKindCode").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = documentKind;
                        cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item.Load(reader);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return item;
        }
        /// <summary>
        /// Автонумерация документа по типу документа и идентификатору компании
        /// </summary>
        /// <param name="workarea">Рабочая область</param>
        /// <param name="documentKind">Тип документа</param>
        /// <param name="myCompanyId">Идентификатор компании</param>
        /// <returns></returns>
        public static Autonum GetAutonumByDocumentKind(Workarea workarea, int documentKind, int myCompanyId)
        {
            Autonum item = new Autonum { Workarea = workarea, DocKind = documentKind, MyCompanyId=myCompanyId };
            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = workarea.FindMethod("AutonumLoadByDocKindMyCompany").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = documentKind;
                        cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = myCompanyId;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item.Load(reader);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return item;
        }

        /// <summary>
        /// Автонумерация документа по типу документа и идентификатору пользователя
        /// </summary>
        /// <param name="workarea">Рабочая область</param>
        /// <param name="documentKind">Тип документа</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public static Autonum GetAutonumByDocumentKindUser(Workarea workarea, int documentKind, int userId)
        {
            Autonum item = new Autonum { Workarea = workarea, DocKind = documentKind, UserId = userId };
            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = workarea.FindMethod("AutonumLoadByDocKindUserId").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = documentKind;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserId, SqlDbType.Int).Value = userId;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item.Load(reader);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return item;
        }
        
    }
}
