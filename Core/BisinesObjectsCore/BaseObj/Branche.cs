using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура владелца</summary>
    internal struct BrancheStruct
    {
        /// <summary>Код базы</summary>
        public int DbCode;
        /// <summary>Наименование базы</summary>
        public string DbName;
        /// <summary>Наименование сервера</summary>
        public string ServerName;
        /// <summary>Сортировка по умолчанию</summary>
        public int SortOrder;
        /// <summary>IP-адрес сервера</summary>
        public string IpAddress;
        /// <summary>Пароль базы данных</summary>
        public string PassWord;
        /// <summary>Пользователь базы данных</summary>
        public string Uid;
        /// <summary>Домен</summary>
        public string Domain;
        /// <summary>Тип аутентификация</summary>
        public int Authentication;
        /// <summary>Начальная дата информации</summary>
        public DateTime? DateStart;
        /// <summary>Конечная дата информации</summary>
        public DateTime? DateEnd;
    }
    /// <summary>Владелец</summary>
    public class Branche : BaseCore<Branche>, IChains<Branche>,
        ICodes<Branche>, IHierarchySupport
    {
// ReSharper disable InconsistentNaming
        /// <summary>Владелец, соответствует значению 1</summary>
        public const short KINDVALUE_DEFAULT = 1;
        /// <summary>База данных Акцент7, соответствует значению 2</summary>
        public const short KINDVALUE_ACCENT7 = 2;

        /// <summary>Владелец, соответствует значению 720897</summary>
        public const int KINDID_DEFAULT = 720897;
        /// <summary>База данных Акцент7, соответствует значению 720898</summary>
        public const int KINDID_ACCENT7 = 720898;
// ReSharper restore InconsistentNaming
        /// <summary>Конструктор</summary>
        public Branche(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Branche;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Branche Clone(bool endInit)
        {
            Branche obj = base.Clone(false);
            obj.Authentication = Authentication;
            obj.DatabaseCode = DatabaseCode;
            obj.DatabaseName = DatabaseName;
            obj.Domain = Domain;
            obj.IpAddress = IpAddress;
            obj.Password = Password;
            obj.ServerName = ServerName;
            obj.SortOrder = SortOrder;
            obj.Uid = Uid;
            obj.DateStart = DateStart;
            obj.DateEnd = DateEnd;

            if (endInit)
                OnEndInit();
            return obj;
        }
        protected override void CopyValue(Branche template)
        {
            base.CopyValue(template);
            SortOrder = template.SortOrder;
            DatabaseCode = template.DatabaseCode;
            DatabaseName = template.DatabaseName;
            ServerName = template.ServerName;
            IpAddress = template.IpAddress;
            Password = template.Password;
            Uid = template.Uid;
            Domain = template.Domain;
            Authentication = template.Authentication;
            DateStart = template.DateStart;
            DateEnd = template.DateEnd;
        }
        #region Свойства

        private int _sortOrder;
        /// <summary>Признак сортировки</summary>
        [Description("Признак сортировки")]
        public int SortOrder
        {
            get 
            { 
                return _sortOrder; 
            }
            set
            {
                if (value == _sortOrder) return;
                OnPropertyChanging(GlobalPropertyNames.SortOrder);
                _sortOrder = value;
                OnPropertyChanged(GlobalPropertyNames.SortOrder);
            }
        }

        private int _databaseCode;
        /// <summary>Код базы данных</summary>
        [Description("Код базы данных")]
        public int DatabaseCode
        {
            get 
            { 
                return _databaseCode; 
            }
            set
            {
                if (value == _databaseCode) return;
                OnPropertyChanging(GlobalPropertyNames.DatabaseCode);
                _databaseCode = value;
                OnPropertyChanged(GlobalPropertyNames.DatabaseCode);
            }
        }

        private string _databaseName;
        /// <summary>Имя базы данных</summary>
        [Description("Имя базы данных")]
        public string DatabaseName
        {
            get 
            { 
                return _databaseName; 
            }
            set
            {
                if (value == _databaseName) return;
                OnPropertyChanging(GlobalPropertyNames.DatabaseName);
                _databaseName = value;
                OnPropertyChanged(GlobalPropertyNames.DatabaseName);
            }
        }

        private string _serverName;
        /// <summary>Имя сервера базы данных</summary>
        [Description("Имя сервера базы данных")]
        public string ServerName
        {
            get { return _serverName; }
            set
            {
                if (value == _serverName) return;
                OnPropertyChanging(GlobalPropertyNames.ServerName);
                _serverName = value;
                OnPropertyChanged(GlobalPropertyNames.ServerName);
            }
        }

        private string _ipAddress;
        /// <summary>IP-адрес сервера</summary>
        [Description("IP-адрес сервера")]
        public string IpAddress
        {
            get { return _ipAddress; }
            set
            {
                if (value == _ipAddress) return;
                OnPropertyChanging(GlobalPropertyNames.IP);
                _ipAddress = value;
                OnPropertyChanged(GlobalPropertyNames.IP);
            }
        }

        private string _password;
        /// <summary>Пароль базы данных</summary>
        [Description("Пароль базы данных")]
        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                OnPropertyChanging(GlobalPropertyNames.Password);
                _password = value;
                OnPropertyChanged(GlobalPropertyNames.Password);
            }
        }

        private string _uid;
        /// <summary>Пользователь базы данных</summary>
        [Description("Пользователь базы данных")]
        public string Uid
        {
            get { return _uid; }
            set
            {
                if (value == _uid) return;
                OnPropertyChanging(GlobalPropertyNames.Uid);
                _uid = value;
                OnPropertyChanged(GlobalPropertyNames.Uid);
            }
        }

        private string _domain;
        /// <summary>Домен</summary>
        [Description("Домен")]
        public string Domain
        {
            get { return _domain; }
            set
            {
                if (value == _domain) return;
                OnPropertyChanging(GlobalPropertyNames.Domain);
                _domain = value;
                OnPropertyChanged(GlobalPropertyNames.Domain);
            }
        }

        private int _authentication;
        /// <summary>Тип аутентификация</summary>
        [Description("Тип аутентификация")]
        public int Authentication
        {
            get { return _authentication; }
            set
            {
                if (value == _authentication) return;
                OnPropertyChanging(GlobalPropertyNames.Authentication);
                _authentication = value;
                OnPropertyChanged(GlobalPropertyNames.Authentication);
            }
        }


        private DateTime? _dateStart;
        /// <summary>Начальная дата информации</summary>
        public DateTime? DateStart
        {
            get { return _dateStart; }
            set
            {
                if (value == _dateStart) return;
                OnPropertyChanging(GlobalPropertyNames.DateStart);
                _dateStart = value;
                OnPropertyChanged(GlobalPropertyNames.DateStart);
            }
        }


        private DateTime? _dateEnd;
        /// <summary>Конечная дата информации</summary>
        public DateTime? DateEnd
        {
            get { return _dateEnd; }
            set
            {
                if (value == _dateEnd) return;
                OnPropertyChanging(GlobalPropertyNames.DateEnd);
                _dateEnd = value;
                OnPropertyChanged(GlobalPropertyNames.DateEnd);
            }
        }
        
        
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_sortOrder != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SortOrder, XmlConvert.ToString(_sortOrder));
            if (_databaseCode !=0 )
                writer.WriteAttributeString(GlobalPropertyNames.DatabaseCode, XmlConvert.ToString(_databaseCode));
            if (!string.IsNullOrEmpty(_databaseName))
                writer.WriteAttributeString(GlobalPropertyNames.DatabaseName, _databaseName);
            if (!string.IsNullOrEmpty(_serverName))
                writer.WriteAttributeString(GlobalPropertyNames.ServerName, _serverName);
            if (!string.IsNullOrEmpty(_ipAddress))
                writer.WriteAttributeString(GlobalPropertyNames.IpAddress, _ipAddress);
            if (!string.IsNullOrEmpty(_password))
                writer.WriteAttributeString(GlobalPropertyNames.Password, _password);
            if (!string.IsNullOrEmpty(_uid))
                writer.WriteAttributeString(GlobalPropertyNames.Uid, _uid);
            if (!string.IsNullOrEmpty(_domain))
                writer.WriteAttributeString(GlobalPropertyNames.Domain, _domain);
            if (_authentication != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Authentication, XmlConvert.ToString(_authentication));
            if (_dateStart.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateStart, XmlConvert.ToString(_dateStart.Value));
            if (_dateEnd.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateEnd, XmlConvert.ToString(_dateEnd.Value));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.SortOrder) != null)
                _sortOrder = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SortOrder));
            if (reader.GetAttribute(GlobalPropertyNames.DatabaseCode) != null)
                _databaseCode = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DatabaseCode));
            if (reader.GetAttribute(GlobalPropertyNames.DatabaseName) != null)
                _databaseName = reader.GetAttribute(GlobalPropertyNames.DatabaseName);
            if (reader.GetAttribute(GlobalPropertyNames.ServerName) != null)
                _serverName = reader.GetAttribute(GlobalPropertyNames.ServerName);
            if (reader.GetAttribute(GlobalPropertyNames.IpAddress) != null)
                _ipAddress = reader.GetAttribute(GlobalPropertyNames.IpAddress);
            if (reader.GetAttribute(GlobalPropertyNames.Password) != null)
                _password = reader.GetAttribute(GlobalPropertyNames.Password);
            if (reader.GetAttribute(GlobalPropertyNames.Uid) != null)
                _uid = reader.GetAttribute(GlobalPropertyNames.Uid);
            if (reader.GetAttribute(GlobalPropertyNames.Domain) != null)
                _domain = reader.GetAttribute(GlobalPropertyNames.Domain);
            if (reader.GetAttribute(GlobalPropertyNames.Authentication) != null)
                _authentication = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Authentication));
            if (reader.GetAttribute(GlobalPropertyNames.DateStart) != null)
                _dateStart = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateStart));
            if (reader.GetAttribute(GlobalPropertyNames.DateEnd) != null)
                _dateEnd = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateStart));
        }
        #endregion

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if (_databaseCode == 0)
                throw new ValidateException("Не указан код базы данных");
            if (string.IsNullOrEmpty(_serverName))
                throw new ValidateException("Не указано наименование сервера базы данных");
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            if(Workarea.MyBranche.Id==Id)
            {
                Workarea._myBranche = this;
            }
        }
        #region Состояние
        BrancheStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new BrancheStruct
                                  {
                                      DbCode = _databaseCode,
                                      DbName = _databaseName,
                                      ServerName = _serverName,
                                      SortOrder = _sortOrder,
                                      IpAddress = _ipAddress,
                                      PassWord = _password,
                                      Uid = _uid,
                                      Domain = _domain,
                                      Authentication = _authentication
                                  };
                return true;
            }
            return false;
        }
        /// <summary>Востановить текущее состояние объекта</summary>
        /// <remarks>Востановление состояние возможно только после выполнения сосхранения состояния</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            DatabaseCode = _baseStruct.DbCode;
            DatabaseName = _baseStruct.DbName;
            ServerName = _baseStruct.ServerName;
            SortOrder = _baseStruct.SortOrder;
            IpAddress = _baseStruct.IpAddress;
            Password = _baseStruct.PassWord;
            Uid = _baseStruct.Uid;
            Domain = _baseStruct.Domain;
            Authentication = _baseStruct.Authentication;
            IsChanged = false;
        } 
        #endregion
        #region База данных
        /// <summary>Загрузить экземпляр из базы данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _sortOrder = reader.GetSqlInt32(17).Value;
                _serverName = reader.GetSqlString(18).Value;
                _databaseName = reader.IsDBNull(19) ? null : reader.GetString(19);
                _databaseCode = reader.GetSqlInt32(20).Value;
                _ipAddress = reader.IsDBNull(21) ? string.Empty : reader.GetSqlString(21).Value;
                _password = reader.IsDBNull(22) ? string.Empty : reader.GetSqlString(22).Value;
                _uid = reader.IsDBNull(23) ? string.Empty : reader.GetSqlString(23).Value;
                _domain = reader.IsDBNull(24) ? string.Empty : reader.GetSqlString(24).Value;
                _authentication = reader.IsDBNull(25) ? 0 : reader.GetSqlInt32(25).Value;
                _dateStart = reader.IsDBNull(26) ? (DateTime?)null : reader.GetDateTime(26);
                _dateEnd = reader.IsDBNull(27) ? (DateTime?)null : reader.GetDateTime(27);
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
        /// <param name="insertCommand">Является ли комменда операцией создания</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.DatabaseCode, SqlDbType.Int) {IsNullable = false, Value = _databaseCode};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DatabaseName, SqlDbType.NVarChar, 128) {IsNullable = true};
            if (string.IsNullOrEmpty(_databaseName))
                prm.Value = DBNull.Value;
            else
                prm.Value = _databaseName;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ServerName, SqlDbType.NVarChar, 128)
                      {
                          IsNullable = false,
                          Value = _serverName
                      };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.Int) {IsNullable = false, Value = _sortOrder};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Ip, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_ipAddress))
                prm.Value = DBNull.Value;
            else
                prm.Value = _ipAddress;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Password, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_password))
                prm.Value = DBNull.Value;
            else
                prm.Value = _password;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Uid, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_uid))
                prm.Value = DBNull.Value;
            else
                prm.Value = _uid;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Domain, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_domain))
                prm.Value = DBNull.Value;
            else
                prm.Value = _domain;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Authentication, SqlDbType.Int) {IsNullable = true, Value = _authentication};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateStart, SqlDbType.Date) { IsNullable = true };
            if (_dateStart.HasValue)
                prm.Value = _dateStart;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateEnd, SqlDbType.Date) { IsNullable = true };
            if (_dateEnd.HasValue)
                prm.Value = _dateEnd;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);
        }
        /// <summary>
        /// Получить текущее соединение с базой данных
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetDatabaseConnection()
        {
            var builder=new SqlConnectionStringBuilder
                            {
                                DataSource = this.ServerName,
                                InitialCatalog = this.DatabaseName,
                                IntegratedSecurity = this._authentication==(int)Security.AuthenticateKind.Windows,
                                ApplicationName = "Documents System 2011",
                                CurrentLanguage = "Russian"
                            };

            if (this._authentication == (int)Security.AuthenticateKind.SqlServer)//SQL authentication
            {
                builder.UserID = this._uid;
                builder.Password = this.Password;
            }

            var cnn = new SqlConnection(builder.ConnectionString);
            try
            {
                cnn.Open();
            }
            catch (SqlException)
            {
                //Делаем попытку соединиться по IP-адресу
                builder.DataSource = this.IpAddress;
                cnn = new SqlConnection(builder.ConnectionString);
                cnn.Open();
            }
            
            return cnn;
        }
        /// <summary>
        /// Строка соединения для базы данных
        /// </summary>
        /// <param name="useIp">Использовать ip адрес</param>
        /// <returns></returns>
        public string GetConnectionString(bool useIp=false)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = this.ServerName,
                InitialCatalog = this.DatabaseName,
                IntegratedSecurity = this._authentication == (int)Security.AuthenticateKind.Windows,
                ApplicationName = "Documents System 2011",
                CurrentLanguage = "Russian"
            };

            if (this._authentication == (int)Security.AuthenticateKind.SqlServer)//SQL authentication
            {
                builder.UserID = this._uid;
                builder.Password = this.Password;
            }
            if(useIp)
            {
                builder.DataSource = this.IpAddress;
                return builder.ConnectionString;
            }
            return builder.ConnectionString;
        }
        /// <summary>
        /// Возвращает рабочую область для текущего объекта
        /// </summary>
        /// <returns></returns>
        public Workarea GetWorkarea()
        {
            Workarea newwa = new Workarea();
            newwa.ConnectionString = GetConnectionString();
            newwa.LogOn(_uid);
            return newwa;
        }
        #endregion
        #region ILinks<Branche> Members
        /// <summary>
        /// Связи владельцев
        /// </summary>
        /// <returns></returns>
        public List<IChain<Branche>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи владельцев
        /// </summary>
        /// <param name="kind">Тип связей</param>
        /// <returns></returns>
        public List<IChain<Branche>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Branche> IChains<Branche>.SourceList(int chainKindId)
        {
            return Chain<Branche>.GetChainSourceList(this, chainKindId);
        }
        List<Branche> IChains<Branche>.DestinationList(int chainKindId)
        {
            return Chain<Branche>.DestinationList(this, chainKindId);
        }
        #endregion
        #region ICodes

        /// <summary>
        /// Список значений дополнительных кодов для объекта
        /// </summary>
        /// <param name="allKinds"></param>
        /// <returns></returns>
        public List<CodeValue<Branche>> GetValues(bool allKinds)
        {
            return CodeHelper<Branche>.GetValues(this, true);
        }

        /// <summary>
        /// Список представлний для отображения
        /// </summary>
        /// <param name="allKinds"></param>
        /// <returns></returns>
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Branche>.GetView(this, true);
        }
        #endregion

        /// <summary>
        /// Первая группа в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<Branche>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }

        /// <summary>
        /// Поиск объекта
        /// </summary>
        /// <param name="hierarchyId">Идентификатор иерархии в которой осуществлять поиск</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="flags">Флаг</param>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <param name="name">Наименование</param>
        /// <param name="kindId">Идентификатор типа</param>
        /// <param name="code">Признак</param>
        /// <param name="memo">Наименование</param>
        /// <param name="flagString">Пользовательский флаг</param>
        /// <param name="templateId">Идентификатор шаблона</param>
        /// <param name="count">Количество, по умолчанию 100</param>
        /// <param name="filter">Дополнительный фильтр</param>
        /// <param name="useAndFilter">Использовать фильтр И</param>
        /// <returns></returns>
        public List<Branche> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Branche> filter = null, bool useAndFilter = false)
        {
            Branche item = new Branche { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Branche> collection = new List<Branche>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindProcedure(GlobalMethodAlias.FindBy);
                        cmd.Parameters.Add(GlobalSqlParamNames.Count, SqlDbType.Int).Value = count;
                        if (hierarchyId != null && hierarchyId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;
                        if (userName != null && !string.IsNullOrEmpty(userName))
                            cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;
                        if (flags.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.Flags, SqlDbType.Int).Value = flags;
                        if (stateId.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateId;
                        if (!string.IsNullOrWhiteSpace(name))
                            cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = name;
                        if (kindId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindId;
                        if (!string.IsNullOrWhiteSpace(code))
                            cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;
                        if (!string.IsNullOrWhiteSpace(memo))
                            cmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255).Value = memo;
                        if (!string.IsNullOrWhiteSpace(flagString))
                            cmd.Parameters.Add(GlobalSqlParamNames.FlagString, SqlDbType.NVarChar, 50).Value = flagString;
                        if (templateId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TemplateId, SqlDbType.Int).Value = templateId;
                        if (useAndFilter)
                            cmd.Parameters.Add(GlobalSqlParamNames.UseAndFilter, SqlDbType.Bit).Value = true;



                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Branche { Workarea = Workarea };
                            item.Load(reader);
                            Workarea.Cashe.SetCasheData(item);
                            if (filter != null && filter.Invoke(item))
                                collection.Add(item);
                            else if (filter == null)
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
