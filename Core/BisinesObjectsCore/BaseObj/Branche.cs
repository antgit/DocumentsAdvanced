using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>��������� ��������</summary>
    internal struct BrancheStruct
    {
        /// <summary>��� ����</summary>
        public int DbCode;
        /// <summary>������������ ����</summary>
        public string DbName;
        /// <summary>������������ �������</summary>
        public string ServerName;
        /// <summary>���������� �� ���������</summary>
        public int SortOrder;
        /// <summary>IP-����� �������</summary>
        public string IpAddress;
        /// <summary>������ ���� ������</summary>
        public string PassWord;
        /// <summary>������������ ���� ������</summary>
        public string Uid;
        /// <summary>�����</summary>
        public string Domain;
        /// <summary>��� ��������������</summary>
        public int Authentication;
        /// <summary>��������� ���� ����������</summary>
        public DateTime? DateStart;
        /// <summary>�������� ���� ����������</summary>
        public DateTime? DateEnd;
    }
    /// <summary>��������</summary>
    public class Branche : BaseCore<Branche>, IChains<Branche>,
        ICodes<Branche>, IHierarchySupport
    {
// ReSharper disable InconsistentNaming
        /// <summary>��������, ������������� �������� 1</summary>
        public const short KINDVALUE_DEFAULT = 1;
        /// <summary>���� ������ ������7, ������������� �������� 2</summary>
        public const short KINDVALUE_ACCENT7 = 2;

        /// <summary>��������, ������������� �������� 720897</summary>
        public const int KINDID_DEFAULT = 720897;
        /// <summary>���� ������ ������7, ������������� �������� 720898</summary>
        public const int KINDID_ACCENT7 = 720898;
// ReSharper restore InconsistentNaming
        /// <summary>�����������</summary>
        public Branche(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Branche;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
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
        #region ��������

        private int _sortOrder;
        /// <summary>������� ����������</summary>
        [Description("������� ����������")]
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
        /// <summary>��� ���� ������</summary>
        [Description("��� ���� ������")]
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
        /// <summary>��� ���� ������</summary>
        [Description("��� ���� ������")]
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
        /// <summary>��� ������� ���� ������</summary>
        [Description("��� ������� ���� ������")]
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
        /// <summary>IP-����� �������</summary>
        [Description("IP-����� �������")]
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
        /// <summary>������ ���� ������</summary>
        [Description("������ ���� ������")]
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
        /// <summary>������������ ���� ������</summary>
        [Description("������������ ���� ������")]
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
        /// <summary>�����</summary>
        [Description("�����")]
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
        /// <summary>��� ��������������</summary>
        [Description("��� ��������������")]
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
        /// <summary>��������� ���� ����������</summary>
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
        /// <summary>�������� ���� ����������</summary>
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

        #region ������������
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

        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();
            if (_databaseCode == 0)
                throw new ValidateException("�� ������ ��� ���� ������");
            if (string.IsNullOrEmpty(_serverName))
                throw new ValidateException("�� ������� ������������ ������� ���� ������");
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            if(Workarea.MyBranche.Id==Id)
            {
                Workarea._myBranche = this;
            }
        }
        #region ���������
        BrancheStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
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
        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
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
        #region ���� ������
        /// <summary>��������� ��������� �� ���� ������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">��������� ������������� �������</param>
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
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>���������� �������� ���������� ��� �������� ��������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ��������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
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
        /// �������� ������� ���������� � ����� ������
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
                //������ ������� ����������� �� IP-������
                builder.DataSource = this.IpAddress;
                cnn = new SqlConnection(builder.ConnectionString);
                cnn.Open();
            }
            
            return cnn;
        }
        /// <summary>
        /// ������ ���������� ��� ���� ������
        /// </summary>
        /// <param name="useIp">������������ ip �����</param>
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
        /// ���������� ������� ������� ��� �������� �������
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
        /// ����� ����������
        /// </summary>
        /// <returns></returns>
        public List<IChain<Branche>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// ����� ����������
        /// </summary>
        /// <param name="kind">��� ������</param>
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
        /// ������ �������� �������������� ����� ��� �������
        /// </summary>
        /// <param name="allKinds"></param>
        /// <returns></returns>
        public List<CodeValue<Branche>> GetValues(bool allKinds)
        {
            return CodeHelper<Branche>.GetValues(this, true);
        }

        /// <summary>
        /// ������ ������������ ��� �����������
        /// </summary>
        /// <param name="allKinds"></param>
        /// <returns></returns>
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Branche>.GetView(this, true);
        }
        #endregion

        /// <summary>
        /// ������ ������ � ������� ������ ������
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<Branche>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }

        /// <summary>
        /// ����� �������
        /// </summary>
        /// <param name="hierarchyId">������������� �������� � ������� ������������ �����</param>
        /// <param name="userName">��� ������������</param>
        /// <param name="flags">����</param>
        /// <param name="stateId">������������� ���������</param>
        /// <param name="name">������������</param>
        /// <param name="kindId">������������� ����</param>
        /// <param name="code">�������</param>
        /// <param name="memo">������������</param>
        /// <param name="flagString">���������������� ����</param>
        /// <param name="templateId">������������� �������</param>
        /// <param name="count">����������, �� ��������� 100</param>
        /// <param name="filter">�������������� ������</param>
        /// <param name="useAndFilter">������������ ������ �</param>
        /// <returns></returns>
        public List<Branche> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Branche> filter = null, bool useAndFilter = false)
        {
            Branche item = new Branche { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("�� ������ ����� {0}, ������ ��� ������ ������ �� ���������������", GlobalMethodAlias.LoadList));
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
