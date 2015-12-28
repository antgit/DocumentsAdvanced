using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>��������� ������� "����������� ������� ������"</summary>
    internal struct DataCatalogStruct
    {
        /// <summary>���� � ��������</summary>
        public string Directory;
        /// <summary>������������� ������ �������������</summary>
        public int UserGroupId;
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId;
        /// <summary>������������ ������ ��������� ������ � ��������</summary>
        public int MaxSize;
        /// <summary>������������� ������������ ���������, �������� ����������� ������</summary>
        public int UserOwnerId;
        
    }
    /// <summary>����������� ������� ������</summary>
    /// <remarks>������������ ��������� ������...</remarks>
    public sealed class DataCatalog : BaseCore<DataCatalog>, IChains<DataCatalog>, IEquatable<DataCatalog>,
                                      IComparable, IComparable<DataCatalog>,
                                      ICodes<DataCatalog>, IChainsAdvancedList<DataCatalog, FileData>, ICompanyOwner
    {
        #region ��������� �������� ����� � ��������
        // ReSharper disable InconsistentNaming

        /// <summary>�������� �������, ������������� �������� 1</summary>
        public const int KINDVALUE_IN = 1;
        /// <summary>��������� �������, ������������� �������� 2</summary>
        public const int KINDVALUE_OUT = 2;
        /// <summary>������� ������, ������������� �������� 3</summary>
        public const int KINDVALUE_DATA = 3;
        /// <summary>����������� ������� ������, ������������� �������� 4</summary>
        public const int KINDVALUE_VDATA = 3;

        /// <summary>�������� �������, ������������� �������� 5308417</summary>
        public const int KINDID_IN = 5308417;
        /// <summary>��������� �������, ������������� �������� 5308418</summary>
        public const int KINDID_OUT = 5308418;
        /// <summary>������� ������, ������������� �������� 5308419</summary>
        public const int KINDID_DATA = 5308419;
        /// <summary>����������� ������� ������, ������������� �������� 5308420</summary>
        public const int KINDID_VDATA = 5308420;
        // ReSharper restore InconsistentNaming

        #endregion
        bool IEquatable<DataCatalog>.Equals(DataCatalog other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// ��������� ���� �������� �� ��������������
        /// </summary>
        /// <param name="obj">������</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            DataCatalog otherPerson = (DataCatalog)obj;
            return Id.CompareTo(otherPerson.Id);
        }
        /// <summary>
        /// ��������� ���� �������� �� ��������������
        /// </summary>
        /// <param name="other">������ ���������</param>
        /// <returns></returns>
        public int CompareTo(DataCatalog other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>�����������</summary>
        public DataCatalog()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DataCatalog;
        }
        protected override void CopyValue(DataCatalog template)
        {
            base.CopyValue(template);
            Directory = template.Directory;
            UserGroupId = template.UserGroupId;
            MaxSize = template.MaxSize;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override DataCatalog Clone(bool endInit)
        {
            DataCatalog obj = base.Clone(false);
            obj.Directory = Directory;
            obj.UserGroupId = UserGroupId;
            obj.MyCompanyId = MyCompanyId;
            obj.MaxSize = MaxSize;
            obj.UserOwnerId = UserOwnerId;
            
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region ��������
        private string _directory;
        /// <summary>
        /// ���� � ��������
        /// </summary>
        public string Directory
        {
            get { return _directory; }
            set
            {
                if (value == _directory) return;
                OnPropertyChanging(GlobalPropertyNames.Directory);
                _directory = value;
                OnPropertyChanged(GlobalPropertyNames.Directory);
            }
        }

        private int _userGroupId;
        /// <summary>
        /// ������������� ������ �������������
        /// </summary>
        public int UserGroupId
        {
            get { return _userGroupId; }
            set
            {
                if (value == _userGroupId) return;
                OnPropertyChanging(GlobalPropertyNames.UserGroupId);
                _userGroupId = value;
                OnPropertyChanged(GlobalPropertyNames.UserGroupId);
            }
        }
        private int _myCompanyId;
        /// <summary>
        /// ������������� �����������, �������� ����������� ���������
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
        /// ��� ��������, ����������� �������� ����������� ���������
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


        private int _maxSize;
        /// <summary>������������ ������ ��������� ������ � ��������</summary>
        public int MaxSize
        {
            get { return _maxSize; }
            set
            {
                if (value == _maxSize) return;
                OnPropertyChanging(GlobalPropertyNames.MaxSize);
                _maxSize = value;
                OnPropertyChanged(GlobalPropertyNames.MaxSize);
            }
        }


        private int _userOwnerId;
        /// <summary>������������� ������������ ���������, �������� ����������� ������</summary>
        public int UserOwnerId
        {
            get { return _userOwnerId; }
            set
            {
                if (value == _userOwnerId) return;
                OnPropertyChanging(GlobalPropertyNames.UserOwnerId);
                _userOwnerId = value;
                OnPropertyChanged(GlobalPropertyNames.UserOwnerId);
            }
        }

        private Uid _userOwner;
        /// <summary>
        /// <summary>������������-��������, �������� ����������� ������</summary> 
        /// </summary>
        public Uid UserOwner
        {
            get
            {
                if (_userOwnerId == 0)
                    return null;
                if (_userOwner == null)
                    _userOwner = Workarea.Cashe.GetCasheData<Uid>().Item(_userOwnerId);
                else if (_userOwner.Id != _userOwnerId)
                    _userOwner = Workarea.Cashe.GetCasheData<Uid>().Item(_userOwnerId);
                return _userOwner;
            }
            set
            {
                if (_userOwner == value) return;
                OnPropertyChanging(GlobalPropertyNames.UserOwner);
                _userOwner = value;
                _userOwnerId = _userOwner == null ? 0 : _userOwner.Id;
                OnPropertyChanged(GlobalPropertyNames.UserOwner);
            }
        }
        
        #endregion

        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_directory))
                writer.WriteAttributeString(GlobalPropertyNames.Directory, _directory);
            if (_userGroupId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserGroupId, XmlConvert.ToString(_userGroupId));
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
            if (_maxSize != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MaxSize, XmlConvert.ToString(_maxSize));
            if (_userOwnerId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserOwnerId, XmlConvert.ToString(_userOwnerId));
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Directory) != null)
                _directory = reader[GlobalPropertyNames.Directory];
            if (reader.GetAttribute(GlobalPropertyNames.UserGroupId) != null)
                _userGroupId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UserGroupId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
            if (reader.GetAttribute(GlobalPropertyNames.MaxSize) != null)
                _maxSize = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MaxSize));
            if (reader.GetAttribute(GlobalPropertyNames.UserOwnerId) != null)
                _userOwnerId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UserOwnerId));

        }
        #endregion

        #region ���������
        DataCatalogStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new DataCatalogStruct { Directory = _directory, UserGroupId = _userGroupId, MyCompanyId=_myCompanyId, MaxSize = _maxSize, UserOwnerId=_userOwnerId};
                return true;
            }
            return false;
        }

        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            Directory = _baseStruct.Directory;
            UserGroupId = _baseStruct.UserGroupId;
            MyCompanyId = _baseStruct.MyCompanyId;
            MaxSize = _baseStruct.MaxSize;
            UserOwnerId = _baseStruct.UserOwnerId;
            IsChanged = false;
        }
        #endregion
        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(_directory))
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_DATACALOGDIR", 1049));
            if (_myCompanyId == 0)
                _myCompanyId = Workarea.CurrentUser.MyCompanyId;
            if (_userOwnerId == 0)
                _userOwnerId = Workarea.CurrentUser.Id;
        }
        #region ���� ������
        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="reader">������ <see cref="SqlDataReader"/> ������ ������</param>
        /// <param name="endInit">��������� �������������</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _directory = reader.IsDBNull(17)? string.Empty: reader.GetString(17);
                _userGroupId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _myCompanyId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _maxSize = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _userOwnerId = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>���������� �������� ���������� ��� �������� ��������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ��������</param>
        /// <param name="validateVersion">��������� �� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Directory, SqlDbType.NVarChar, 255) { IsNullable = false, Value = _directory };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserGroupId, SqlDbType.Int) { IsNullable = true, Value = _userGroupId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int)
                {IsNullable = false, Value = _myCompanyId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MaxSize, SqlDbType.Int) {IsNullable = false, Value = _maxSize};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserOwnerId, SqlDbType.Int) {IsNullable = false, Value = _userOwnerId};
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<DataCatalog> Members
        /// <summary>����� ���������</summary>
        /// <returns></returns>
        public List<IChain<DataCatalog>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>����� ���������</summary>
        /// <param name="kind">��� �����</param>
        /// <returns></returns>
        public List<IChain<DataCatalog>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<DataCatalog> IChains<DataCatalog>.SourceList(int chainKindId)
        {
            return Chain<DataCatalog>.GetChainSourceList(this, chainKindId);
        }
        List<DataCatalog> IChains<DataCatalog>.DestinationList(int chainKindId)
        {
            return Chain<DataCatalog>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<DataCatalog>> GetValues(bool allKinds)
        {
            return CodeHelper<DataCatalog>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<DataCatalog>.GetView(this, true);
        }
        #endregion

        #region IChainsAdvancedList<DataCatalog,FileData> Members

        List<IChainAdvanced<DataCatalog, FileData>> IChainsAdvancedList<DataCatalog, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<DataCatalog, FileData>)this).GetLinks(44);
        }

        List<IChainAdvanced<DataCatalog, FileData>> IChainsAdvancedList<DataCatalog, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<DataCatalog, FileData>.GetChainView()
        {
            return ChainValueView.GetView<DataCatalog, FileData>(this);
        }
        public List<IChainAdvanced<DataCatalog, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<DataCatalog, FileData>> collection = new List<IChainAdvanced<DataCatalog, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Library>().Entity.FindMethod("LoadFiles").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = Id;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainAdvanced<DataCatalog, FileData> item = new ChainAdvanced<DataCatalog, FileData> { Workarea = Workarea, Left = this };
                                item.Load(reader);
                                collection.Add(item);
                            }
                        }
                        reader.Close();

                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
            return collection;
        }

        #endregion
    }
}