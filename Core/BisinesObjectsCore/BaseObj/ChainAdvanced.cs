using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>
    /// ���������� ��������� ������� ��� ���������� ���������
    /// </summary>
    internal struct ChainAdvancedStruct
    {
        /// <summary>������������� ���� �������</summary>
        public int KindId;
        /// <summary>������������� ���������</summary>
        public int LeftId;
        /// <summary>���������� ����� � ������</summary>
        public int OrderNo;
        /// <summary>����������</summary>
        public int RightId;
        /// <summary>���</summary>
        public string Code;
        /// <summary>����������</summary>
        public string Memo;
        /// <summary>������������� ������������ ���������</summary>
        public int UserOwnerId;
    }
    /// <summary>������� ������ ����� ������������ ���������</summary>
    /// <typeparam name="T">���</typeparam>
    /// <typeparam name="T2">���</typeparam>
    public class ChainAdvanced<T, T2> : BaseCoreObject, IChainAdvanced<T,T2> where T : class, ICoreObject, new()
        where T2 : class, ICoreObject, new()
    {
        /// <summary>�����������</summary>
        public ChainAdvanced()
            : base()
        {
        }
        /// <summary>�����������</summary>
        /// <param name="left">��������</param>
        public ChainAdvanced(T left)
            : this()
        {
            _left = left;
            _leftId = left.Id;
            Workarea = left.Workarea;
        }
        private T _left;
        /// <summary>��������</summary>
        public T Left
        {
			//get { return _left; }
			//set
			//{
			//    _left = value;
			//    _leftId = _left == null ? 0 : _left.Id;
			//}
			get
			{
				if (_leftId == 0)
					return default(T);
				if (_left == null)
					_left = Workarea.Cashe.GetCasheData<T>().Item(_leftId);
				else if (_left.Id != _leftId)
					_left = Workarea.Cashe.GetCasheData<T>().Item(_leftId);
				return _left;
			}
			set
			{
				_left = value;
				_leftId = _left == null ? 0 : _left.Id;
			}
        }

        private T2 _right;
        /// <summary>����������</summary>
        public T2 Right
        {
            get
            {
                if (_rightId == 0)
                    return default(T2);
                if (_right == null)
                    _right = Workarea.Cashe.GetCasheData<T2>().Item(_rightId);
                else if (_right.Id != _rightId)
                    _right = Workarea.Cashe.GetCasheData<T2>().Item(_rightId);
                return _right;
            }
            set
            {
                _right = value;
                _rightId = _right == null ? 0 : _right.Id;
            }
        }

        private int _userOwnerId;
        /// <summary>
        /// ������������� ������������ ���������
        /// </summary>
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
        /// ������������-��������
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


        /// <summary>
        /// ��� ������������-���������
        /// </summary>
        public string UserOwnerName
        {
            get
            {
                return UserOwner==null? string.Empty:UserOwner.Name;
            }
            
        }
        /// <summary>
        /// ����������� ����������-���������
        /// </summary>
        public string WorkerName
        {
            get
            {
                if (UserOwner != null && UserOwner.AgentId != 0)
                    return UserOwner.Agent.Name;
                return string.Empty;
            }
         
        }
        
        

        #region ILink<T> Members
        private int _leftId;
        /// <summary>������������� ���������</summary>
        public int LeftId
        {
            get { return _leftId; }
            set
            {
                if (value == _leftId) return;
                OnPropertyChanging(GlobalPropertyNames.LeftId);
                _leftId = value;
                OnPropertyChanged(GlobalPropertyNames.LeftId);
            }
        }
        private int _rightId;
        /// <summary>������������� ����������</summary>
        public int RightId
        {
            get { return _rightId; }
            set
            {
                if (value == _rightId) return;
                OnPropertyChanging(GlobalPropertyNames.RightId);
                _rightId = value;
                OnPropertyChanged(GlobalPropertyNames.RightId);
            }
        }

        private int _kindId;
        /// <summary>������������� ���� �������</summary>
        public int KindId
        {
            get { return _kindId; }
            set
            {
                if (value == _kindId) return;
                OnPropertyChanging(GlobalPropertyNames.KindValue);
                _kindId = value;
                OnPropertyChanged(GlobalPropertyNames.KindValue);
            }
        }

        private ChainKind _kind;
        /// <summary>��� �����</summary>
        public ChainKind Kind
        {
            get
            {
                if (_kind == null)
                    _kind = Workarea.CollectionChainKinds.FirstOrDefault(f => f.Id == _kindId);
                else if (_kind.Id != _kindId)
                    _kind = ((ICoreObject)this).Workarea.CollectionChainKinds.FirstOrDefault(f => f.Id == _kindId);
                return _kind;
            }
        }

        private string _code;
        /// <summary>���</summary>
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
        private int _orderNo;
        /// <summary>���������� ����� � ������</summary>
        public int OrderNo
        {
            get { return _orderNo; }
            set
            {
                if (value == _orderNo) return;
                OnPropertyChanging(GlobalPropertyNames.OrderNo);
                _orderNo = value;
                OnPropertyChanged(GlobalPropertyNames.OrderNo);
            }
        }

        private string _memo;
        /// <summary>
        /// ����������
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set
            {
                if (value == _memo) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }
        
        #endregion

        #region ���������
        ChainAdvancedStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new ChainAdvancedStruct
                { 
                    KindId = _kindId,
                    LeftId = _leftId,
                    OrderNo = _orderNo,
                    RightId = _rightId,
                    Memo = _memo,
                    UserOwnerId = _userOwnerId
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
            KindId = _baseStruct.KindId;
            LeftId = _baseStruct.LeftId;
            OrderNo = _baseStruct.OrderNo;
            RightId = _baseStruct.RightId;
            Memo = _baseStruct.Memo;
            UserOwnerId = _baseStruct.UserOwnerId;
            IsChanged = false;
        }
        #endregion

        #region ������������
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, _code);
            if (_kindId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.KindId, XmlConvert.ToString(_kindId));
            if (_leftId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.LeftId, XmlConvert.ToString(_leftId));
            if (_orderNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_orderNo));
            if (_rightId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.RightId, XmlConvert.ToString(_rightId));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (_userOwnerId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserOwnerId, XmlConvert.ToString(_userOwnerId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                _code = reader.GetAttribute(GlobalPropertyNames.Code);
            if (reader.GetAttribute(GlobalPropertyNames.KindId) != null)
                _kindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.KindId));
            if (reader.GetAttribute(GlobalPropertyNames.LeftId) != null)
                _leftId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.LeftId));
            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _orderNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OrderNo));
            if (reader.GetAttribute(GlobalPropertyNames.RightId) != null)
                _rightId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RightId));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.UserOwnerId) != null)
                _userOwnerId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UserOwnerId));
        }
        #endregion

        #region ������
        /// <summary>
        /// �������� ��������� �������
        /// </summary>
        /// <param name="stateValue">������������� ������ ���������</param>
        public override void ChangeState(int stateValue)
        {
            if (stateValue < 0 && stateValue > 4)
                throw new ArgumentOutOfRangeException("stateValue", "����� ��������� ������� �� ����� ���� ������ 0 ��� ������ 4");
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        
                        string methotAlias = typeof(T).Name + typeof(T2).Name;
                        string procedureName = Left.Entity.FindMethod(methotAlias + "ChainChangeState").FullName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateValue;
                        cmd.ExecuteNonQuery();
                        StateId = stateValue;
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /// <summary>���������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">��������� ������������� �������</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _leftId = reader.GetInt32(9);
                _rightId = reader.GetInt32(10);
                _kindId = reader.GetInt32(11);
                _orderNo = reader.GetInt32(12);
                _code = reader.IsDBNull(13)? string.Empty: reader.GetString(13);
                _memo = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                _userOwnerId = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        protected override void Create()
        {
            CancelEventArgs e = new CancelEventArgs();
            OnCreating(e);
            if (e.Cancel)
                return;
            string methotAlias = typeof(T).Name + typeof(T2).Name;
            Create(Left.Entity.FindMethod(methotAlias + "ChainInsertUpdate").FullName);
            OnCreated();
        }
        protected virtual void Create(SqlTransaction trans)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnCreating(e);
            if (e.Cancel)
                return;
            string methotAlias = typeof(T).Name + typeof(T2).Name;
            Create(Left.Entity.FindMethod(methotAlias + "ChainInsertUpdate").FullName, trans);
            OnCreated();
        }
        protected override void Update(bool versionControl = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnUpdating(e);
            if (e.Cancel)
                return;
            string methotAlias = typeof(T).Name + typeof(T2).Name;
            Update(Left.Entity.FindMethod(methotAlias + "ChainInsertUpdate").FullName, true);
            OnUpdated();
        }
        protected override void Update(SqlTransaction trans, bool versionControl = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnUpdating(e);
            if (e.Cancel)
                return;
            string methotAlias = typeof(T).Name + typeof(T2).Name;
            Update(Left.Entity.FindMethod(methotAlias + "ChainInsertUpdate").FullName, true, trans);
            OnUpdated();
        }

        /// <summary>���������� �������� ���������� ��� �������� �������� ��� ����������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ����������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.LeftId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _leftId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.RightId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _rightId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _kindId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.OrderNo, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _orderNo;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 255);
            prm.Direction = ParameterDirection.Input;
            if(string.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, -1);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _memo;
            }

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.UserOwnerId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            if (_userOwnerId == 0)
                prm.Value = Workarea.CurrentUser.Id;
            else
                prm.Value = _userOwnerId;
        }
        /// <summary>�������� ������� �� ���� ������</summary>
        public override void Delete(bool checkVersion = true)
        {
            string methotAlias = typeof(T).Name + typeof(T2).Name;
            Delete(Left.Entity.FindMethod(methotAlias + "ChainDelete").FullName, checkVersion);
        }
        /// <summary>��������� ������ ������� �� ���� ������ �� ��������������</summary>
        /// <param name="value">�������������</param>
        public override void Load(int value)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnLoading(e);
            if (e.Cancel)
                return;
            string methotAlias = typeof(T).Name + typeof(T2).Name;
            Load(value, Left.Entity.FindMethod(methotAlias + "ChainLoad").FullName);
        }

        /// <summary>��������� ������� ����������</summary>
        /// <param name="left">�������� ������� �����</param>
        /// <param name="kind">�������� �������������</param>
        /// <returns></returns>
        public static List<IChainAdvanced<T, T2>> CollectionDestinations(T left, int? kind)
        {
            List<IChainAdvanced<T, T2>> collection = new List<IChainAdvanced<T, T2>>();
            using (SqlConnection cnn = left.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    string methotAlias = typeof(T).Name + typeof(T2).Name;
                    cmd.CommandText = left.Entity.FindMethod(methotAlias + "ChainLoadDestinations").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.RightId, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = left.Id;
                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    if (kind.HasValue)
                        prm.Value = kind.Value;
                    else
                        prm.Value = DBNull.Value;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainAdvanced<T, T2> item = new ChainAdvanced<T, T2> { Workarea = left.Workarea };
                                item.Load(reader);
                                collection.Add(item);
                            }
                            reader.Close();
                        }
                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(left.Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(left.Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

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

        /// <summary>��������� �������</summary>
        /// <param name="left">�������� ������� �����</param>
        /// <param name="kind">�������� �������������</param>
        /// <returns></returns>
        public static List<IChainAdvanced<T, T2>> CollectionSource(T left, int? kind=null)
        {
            List<IChainAdvanced<T, T2>> collection = new List<IChainAdvanced<T, T2>>();
            using (SqlConnection cnn = left.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    string methotAlias = typeof(T).Name + typeof(T2).Name;
                    cmd.CommandText = left.Entity.FindMethod(methotAlias + "ChainLoadSources").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = left.Id;
                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    if (kind.HasValue)
                        prm.Value = kind.Value;
                    else
                        prm.Value = DBNull.Value;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainAdvanced<T, T2> item = new ChainAdvanced<T, T2> { Workarea = left.Workarea };
                                item.Load(reader);
                                collection.Add(item);
                            }
                            
                        }
                        reader.Close();
                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(left.Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(left.Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

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

        /// <summary>
        /// ��������� �������� ���������� (������)
        /// </summary>
        /// <typeparam name="T">���</typeparam>
        /// <param name="left">�������� ������� �����</param>
        /// <param name="chainKindId">������������� ���� �����</param>
        /// <param name="stateId">���������, �� ��������� (-1) - ��� �����, ���������� �������� � ����������� � ���������� �����������</param>
        /// <returns></returns>
        public static List<T2> GetChainSourceList<T, T2>(T left, int? chainKindId=null, int stateId = -1) where T : class, ICoreObject, new()
            where T2 : class, ICoreObject, new()
        {
            T2 item;
            List<T2> collection = new List<T2>();
            using (SqlConnection cnn = left.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        string methotAlias = typeof(T).Name + typeof(T2).Name;
                        cmd.CommandText = left.Entity.FindMethod(methotAlias + "LoadChainId").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = left.Id;
                        SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
                        if (chainKindId.HasValue)
                            prm.Value = chainKindId.Value;
                        else
                            prm.Value = DBNull.Value;
                        if (stateId != 1)
                            cmd.Parameters.Add(GlobalSqlParamNames.ChainStateId, SqlDbType.Int).Value = stateId;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new T2 { Workarea = left.Workarea };
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
        #endregion
    }

    /// <summary>
    /// ������������� �������� �����
    /// </summary>
    public class ChainValueView
    {
        /// <summary>
        /// ������� �������
        /// </summary>
        public Workarea Workarea { get; set; }
        /// <summary>
        /// �������������
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int FlagsValue { get; set; }
        /// <summary>
        /// ������������� ��������� �����
        /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// ������������� �������� �����
        /// </summary>
        public int LeftId { get; set; }
        /// <summary>
        /// ������������� �������� ������
        /// </summary>
        public int RightId { get; set; }
        /// <summary>
        /// ��� �������� ������
        /// </summary>
        public int RightKind { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        public string RightMemo { get; set; }
        /// <summary>
        /// ��� �����
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// ���������� �������
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// ���������� ����� � ������ �������
        /// </summary>
        public int OrderNo { get; set; }
        /// <summary>
        /// ������������ ��������� �����
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// ������������ �������� � �������� ��������� �������
        /// </summary>
        public string LeftName { get; set; }
        /// <summary>
        /// ������������ �������� � �������� ��������� �������
        /// </summary>
        public string RightName { get; set; }

        /// <summary>
        /// ������������ �������� � �������� ��������� �������
        /// </summary>
        public string LeftCode { get; set; }
        /// <summary>
        /// ������������ �������� � �������� ��������� �������
        /// </summary>
        public string RightCode { get; set; }
        
        /// <summary>
        /// ������������� ���� �����
        /// </summary>
        public int KindId { get; set; }

        /// <summary>
        /// ��� ���� �����
        /// </summary>
        public string KindCode { get; set; }

        /// <summary>
        /// ������������ �����
        /// </summary>
        public string KindName { get; set; }

        /// <summary>
        /// ������ ������
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// ��� ������������
        /// </summary>
        public int UserOwnerId { get; set; }
        /// <summary>
        /// ��� ������������
        /// </summary>
        public string UserOwnerName { get; set; }
        /// <summary>
        /// ������������ ����������
        /// </summary>
        public string WorkerName { get; set; }
        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="reader">������ ������ ������</param>
        public virtual void Load(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            if (reader.IsDBNull(1))
                Date = null;
            else
                Date = reader.GetDateTime(1);
            FlagsValue = reader.GetInt32(2);
            LeftId = reader.GetInt32(3);
            RightId = reader.GetInt32(4);
            StateId = reader.GetInt32(5);
            KindId = reader.GetInt32(6);
            OrderNo = reader.GetInt32(7);
            Code = reader.IsDBNull(8) ? String.Empty : reader.GetString(8);
            Memo = reader.IsDBNull(9) ? String.Empty : reader.GetString(9);
            UserOwnerId = reader.GetInt32(10);
            StateName = reader.GetString(11);
            KindCode = reader.GetString(12);
            KindName = reader.GetString(13);
            LeftName = reader.IsDBNull(14) ? String.Empty : reader.GetString(14);
            LeftCode = reader.IsDBNull(15) ? String.Empty : reader.GetString(15);
            RightName = reader.IsDBNull(16) ? String.Empty : reader.GetString(16);
            RightCode = reader.IsDBNull(17) ? String.Empty : reader.GetString(17);
            GroupName = reader.IsDBNull(18) ? String.Empty : reader.GetString(18);
            RightKind = reader.GetInt32(19);
            RightMemo = reader.IsDBNull(20) ? String.Empty : reader.GetString(20);
            UserOwnerName = reader.GetString(21);
            WorkerName = reader.IsDBNull(22) ? String.Empty : reader.GetString(22);
        }
        public static ChainAdvanced<T, T2> ConvertToValue<T, T2>(T item, ChainValueView c) where T : class, IBase, new()
            where T2 : class, ICoreObject, new()
        {
            ChainAdvanced<T, T2> val = new ChainAdvanced<T, T2> { Workarea = c.Workarea, Left = item };
            val.Load(c.Id);
            return val;
        }
        public ChainAdvanced<T, T2> ConvertToValue<T, T2>(T itemLeft, T2 itemRight) where T : class, IBase, new()
            where T2 : class, ICoreObject, new()
        {
            return ConvertToValue<T, T2>(itemLeft, itemRight, this);
        }

        public static ChainAdvanced<T, T2> ConvertToValue<T, T2>(T itemLeft, T2 itemRight, ChainValueView c) where T : class, IBase, new()
            where T2 : class, ICoreObject, new()
        {
            ChainAdvanced<T, T2> val = new ChainAdvanced<T, T2> { Workarea = c.Workarea, Left = itemLeft, Right = itemRight };
            val.Load(c.Id);
            return val;
        }
        public static ChainValueView ConvertToView<T, T2>(ChainAdvanced<T, T2> value) where T : class, IBase, new()
            where T2 : class, IBase, new()
        {
            ChainValueView obj = new ChainValueView();
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
            obj.UserOwnerId = value.UserOwnerId;
            obj.UserOwnerName = value.UserOwnerName;
            obj.WorkerName = value.WorkerName;
            if(value.Right is IHierarchySupport)
            {
                Hierarchy h = (value.Right as IHierarchySupport).FirstHierarchy();
                if (h != null)
                    obj.GroupName = h.Name;
            }
            return obj;
        }
        public static T GetRight<T>(T item, ChainValueView c) where T : class, IBase, new()
        {
            T obj = item.Workarea.Cashe.GetCasheData<T>().Item(c.RightId);
            return obj;
        }
        public static T GetLeft<T>(T item, ChainValueView c) where T : class, IBase, new()
        {
            T obj = item.Workarea.Cashe.GetCasheData<T>().Item(c.LeftId);
            return obj;
        }
        
        public static List<ChainValueView> GetView<T>(T value, int kindId = 0)
            where T : class, IBase, new()
        {
            ChainValueView item;
            List<ChainValueView> collection = new List<ChainValueView>();
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = value.Workarea.Empty<T>().Entity.FindMethod("ChainGetView").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new ChainValueView { Workarea = value.Workarea };
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
        public static List<ChainValueView> GetView<T, T2>(T value, int kindId=0) where T : class, IBase, new()
            where T2 : class, IBase, new()
        {
            ChainValueView item;
            List<ChainValueView> collection = new List<ChainValueView>();
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        string methotAlias = typeof(T).Name + typeof(T2).Name + "GetView";

                        cmd.CommandText = value.Workarea.Empty<T>().Entity.FindMethod(methotAlias).FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new ChainValueView { Workarea = value.Workarea };
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