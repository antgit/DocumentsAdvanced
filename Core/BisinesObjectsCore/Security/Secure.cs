using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace BusinessObjects.Security
{
    public class Secure: ISecurirty
    {
        internal Secure(Workarea wa)
        {
            _workarea = wa;
        }

        private readonly Workarea _workarea;
        /// <summary>������� �������</summary>
        public Workarea Workarea
        {
            get { return _workarea; }
        }

        private CommonRightView _rightCommon;
        /// <summary>����� �����</summary>
        public CommonRightView RightCommon
        {
            get 
            {
                if (_rightCommon == null)
                {
                    _rightCommon = new CommonRightView(_workarea);
                    _rightCommon.Refresh();
                }
                return _rightCommon; 
            }
        }

        internal List<Uid> _allUsers;
        /// <summary>
        /// ������ ���� �������������
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns></returns>
        public List<Uid> GetAllUsers(bool refresh=false)
        {
            if (_allUsers != null && !refresh)
                return _allUsers;
            _allUsers = new List<Uid>();
            _allUsers = Workarea.GetCollection<Uid>(true).Where(f => f.KindValue == Uid.KINDVALUE_USER).ToList();
            return _allUsers;
        }

        internal List<Uid> _allGroups;
        /// <summary>
        /// ������ ���� �����
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns></returns>
        public List<Uid> GetAllGroups(bool refresh = false)
        {
            if (_allGroups != null && !refresh)
                return _allGroups;
            _allGroups = new List<Uid>();
            _allGroups = Workarea.GetCollection<Uid>(true).Where(f => f.KindValue == Uid.KINDVALUE_GROUP).ToList();
            return _allGroups;
        }

        /// <summary>����� ����� ������������</summary>
        /// <param name="user">������������</param>
        /// <returns></returns>
        public CommonRightView GetCommonRights(Uid user)
        {
            CommonRightView values = new CommonRightView(Workarea);
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("RightCommonByUser").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = user.Name;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            values.Acl.Add(reader.GetString(0), reader.IsDBNull(1) ? new int?() : reader.GetInt32(1));
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return values;
        }
        /// <summary>������ ����� ���� ������</summary>
        /// <returns></returns>
        public List<string> GetDatabaseGroups()
        {
            List<string> values = new List<string>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("DataBaseGroups").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            values.Add(reader.GetString(0));
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return values;
        }
        /// <summary>������ ������������ ����� � ������� ������ ������������</summary>
        /// <param name="name">��� ������������</param>
        /// <returns>������ ����� � ������� ������ ������������</returns>
        public List<string> GetUserGroups(string name)
        {
            List<string> values = new List<string>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException(Workarea.Cashe.ResourceString("EX_MSG_CONNECTIONLOST", 1049));
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("GroupsOfUser").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = name;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            values.Add(reader.GetString(0));
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return values;
        }
        /// <summary>
        /// ������� ����� ������ �������������
        /// </summary>
        /// <param name="name">������������ ������</param>
        /// <returns></returns>
        public Uid CreateRole(string name)
        {
            Uid uidTml = Workarea.GetTemplates<Uid>().FirstOrDefault(f => f.KindId == Uid.KINDID_GROUP);

            Uid newRole = Workarea.CreateNewObject<Uid>(uidTml);
            newRole.Name = name;
            newRole.Save();

            return newRole;
        }

        public void IncludeInGroup(string userName, string groupName)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                // TODO: �������������� �������� if (cnn == null) ������ false �� ���� � ���� throw?
                //if (cnn == null)
                //    throw new ConnectionException(Workarea.Cashe.ResourceString("EX_MSG_CONNECTIONLOST", 1049));

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("Uid.IncludeInGroup").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Rolename, SqlDbType.NVarChar, 255).Value = groupName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Membername, SqlDbType.NVarChar, 255).Value = userName;
                        cmd.ExecuteNonQuery();

                        if (groupName.ToUpper() == "��������������")
                        {
                            cmd.CommandText =
                                String.Format("EXEC master..sp_addsrvrolemember @loginame = N'{0}', @rolename = N'securityadmin'", userName);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            cmd.CommandText =
                                String.Format("USE MASTER GO GRANT CONTROL SERVER TO {0}", userName);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        public void ExcludeFromGroup(string userName, string groupName)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException(Workarea.Cashe.ResourceString("EX_MSG_CONNECTIONLOST", 1049));
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("DbUid.ExcludeFromGroup").FullName; //"sp_droprolemember";
                        cmd.Parameters.Add(GlobalSqlParamNames.Rolename, SqlDbType.NVarChar, 255).Value = groupName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Membername, SqlDbType.NVarChar, 255).Value = userName;
                        cmd.ExecuteNonQuery();

                        if (groupName.ToUpper() == "��������������")
                        {
                            cmd.CommandText =
                                String.Format("EXEC master..sp_dropsrvrolemember @loginame = N'{0}', @rolename = N'securityadmin'", userName);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();

                            cmd.CommandText =
                                String.Format("USE MASTER GO REVOKE CONTROL SERVER TO {0}", userName);

                            cmd.ExecuteNonQuery();
                        }
                        //
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /// <summary>�������� �� ������������ ���������� ������</summary>
        /// <param name="userName">��� ������������</param>
        /// <param name="roleName">��� ������</param>
        /// <returns></returns>
        public bool IsUserExistsInGroup(string userName, string roleName)
        {
            bool ret = false;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException(Workarea.Cashe.ResourceString("EX_MSG_CONNECTIONLOST", 1049));
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("DataBaseUserExistsInGroup").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 255).Value = userName;
                        cmd.Parameters.Add(GlobalSqlParamNames.RoleName, SqlDbType.NVarChar, 255).Value = roleName;
                        object val = cmd.ExecuteScalar();

                        if (val != null)
                        {
                            ret = (int)val == 1 ? true: false;        
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return ret;
        }

        public ElementRightView ElementRightView(short kind)
        {
            return new ElementRightView(_workarea, kind);
        }
        /// <summary>
        /// ���������� ������������
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="uidName">��� ������������</param>
        /// <returns></returns>
        public ElementRightView ElementRightView(short kind, string uidName=null)
        {
            return new ElementRightView(_workarea, kind, uidName);
        }
        public EntityRightView DbentityRightView()
        {
            return new EntityRightView(_workarea);
        }
        #region ISecurirty Members
        ElementRightView ISecurirty.ElementRightView(short kind)
        {
            return ElementRightView(kind);
        }

        ICommonRights ISecurirty.RightCommon
        {
            get { return RightCommon; }
        }

        ICommonRights ISecurirty.GetCommonRights(Uid user)
        {
            return GetCommonRights(user);
        }

        bool ISecurirty.IsUserExistsInGroup(string userName, string roleName)
        {
            return IsUserExistsInGroup(userName, roleName);
        }

        List<string> ISecurirty.GetUserGroups(string name)
        {
            return GetUserGroups(name);
        }

        List<string> ISecurirty.GetDatabaseGroups()
        {
            return GetDatabaseGroups();
        }
        #endregion

        //public virtual List<UserRightEntity> GetCollectionUserRightsDbEntity(Uid user, short id)
        //{
        //    List<UserRightEntity> collection = new List<UserRightEntity>();
        //    using (SqlConnection cnn = _workarea.GetDatabaseConnection())
        //    {
        //        if (cnn == null) return collection;

        //        try
        //        {
        //            using (SqlCommand cmd = cnn.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("RightsDbEntityLoadByUser").FullName;
        //                cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.SmallInt).Value = id;
        //                cmd.Parameters.Add(GlobalSqlParamNames.UserId, SqlDbType.Int).Value = user.Id;
        //                SqlDataReader reader = cmd.ExecuteReader();

        //                if (reader != null)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        UserRightEntity item = new UserRightEntity { Workarea = _workarea };
        //                        Action<SqlDataReader> loader = item.Load;
        //                        loader(reader);
        //                        collection.Add(item);
        //                    }
        //                    reader.Close();
        //                }
        //            }
        //        }
        //        finally
        //        {
        //            cnn.Close();
        //        }
        //    }
        //    return collection;
        //}
        public virtual List<UserRightCommon> GetCollectionUserRights(int kindId, int userId)
        {
            List<UserRightCommon> collection = new List<UserRightCommon>();
            using (SqlConnection cnn = _workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("RightsUsersLoadByUser").FullName;
                        if(kindId==0)
                            cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindId;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserId, SqlDbType.Int).Value = userId;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UserRightCommon item = new UserRightCommon {Workarea = _workarea};
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
        List<UserRightCommon> _collectionCurrentUserRight;
        /// <summary>��������� �������� ���� �������� ������������</summary>
        public List<UserRightCommon> CollectionCurrentUserRight
        {
            get {
                return _collectionCurrentUserRight ??
                       (_collectionCurrentUserRight = GetCollectionUserRights(1769473, _workarea.CurrentUser.Id));
            }
        }

        /// <summary>
        /// ������ ����� � ������������� ������� ����� ���������� ��� ������ ������
        /// </summary>
        /// <param name="elementId">������������� �������� (������)</param>
        /// <param name="dbEntityId">������������� ���� �������� (������)</param>
        /// <param name="rightKindId">1769474 - ��� ���������� �� �������; 1769476 - ��� ���������������� ����������</param>
        /// <returns></returns>
        public virtual List<Uid> GetUserGroupsRightForElement(int elementId, int dbEntityId, int rightKindId = 1769474) 
        {
            List<Uid> collection = new List<Uid>();
            using (SqlConnection cnn = _workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("SecureElementRightsUsers").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = dbEntityId;
                        cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = elementId;
                        cmd.Parameters.Add(GlobalSqlParamNames.RightKindId, SqlDbType.Int).Value = rightKindId;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Uid item = new Uid { Workarea = _workarea };
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
        public virtual List<UserRightElement> GetCollectionUserRightsElements(int kindId, int userId, int elementId, short dbEntityId)
        {
            List<UserRightElement> collection = new List<UserRightElement>();
            using (SqlConnection cnn = _workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("ElementRightsLoadByUserDbEntityId").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindId;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserId, SqlDbType.Int).Value = userId;                        
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = dbEntityId;
                        cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = elementId;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UserRightElement item = new UserRightElement {Workarea = _workarea};
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

        public DateTime GetMinFactDateAllow(int value)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Fact.GetMinDateAllow";
                    cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = value;
                    object obj = cmd.ExecuteScalar();
                    if (obj == null)
                        return DateTime.MinValue;
                    return obj == DBNull.Value ? DateTime.MinValue : (DateTime) obj;
                }
            }
        }

        private List<int> _companyScopeView;
        /// <summary>
        /// ������ ��������������� �����������, ��� ������� ��������� ��������� ����������
        /// </summary>
        /// <returns></returns>
        public List<int> GetCompanyScopeView()
        {
            if (_companyScopeView == null)
                RefreshCompanyScopeView();
            return _companyScopeView;
        }

        // ��������� ��� ������������ ���������
        private string _lastUidContext;

        private Dictionary<string, List<int>> _companyScopeViewContext;
        /// <summary>
        /// ������ ��������������� �����������, ��� ������� ��������� ��������� ����������
        /// </summary>
        /// <param name="uidContext">��� ������������ ��� �������� ���������� �������� ������� ���������</param>
        /// <returns></returns>
        public List<int> GetCompanyScopeView(string uidContext)
        {
            if (_companyScopeViewContext == null)
            {
                _companyScopeViewContext = new Dictionary<string, List<int>>();
                
            }
            if (!_companyScopeViewContext.ContainsKey(uidContext))
            {
                RefreshCompanyScopeViewContext(uidContext);
                
            }
            return _companyScopeViewContext[uidContext];

        }
        private List<int> _companyScopeEdit;
        /// <summary>
        /// ������ ��������������� �����������, ��� ������� ��������� ��������������  ����������
        /// </summary>
        /// <returns></returns>
        public List<int> GetCompanyScopeEdit()
        {
            if (_companyScopeEdit == null)
                RefreshCompanyScopeEdit();
            return _companyScopeEdit;
        }

        private Dictionary<string, List<int>> _companyScopeEditContext;
        /// <summary>
        /// ������ ��������������� �����������, ��� ������� ��������� ��������� ����������
        /// </summary>
        /// <param name="uidContext">��� ������������ ��� �������� ���������� �������� ������� ���������</param>
        /// <returns></returns>
        public List<int> GetCompanyScopeEdit(string uidContext)
        {
            if (_companyScopeEditContext == null)
            {
                _companyScopeEditContext = new Dictionary<string, List<int>>();

            }
            if (!_companyScopeEditContext.ContainsKey(uidContext))
            {
                RefreshCompanyScopeEditContext(uidContext);

            }
            return _companyScopeEditContext[uidContext];

        }

        private Dictionary<string, List<int>> _companyScopeOpenContext;
        /// <summary>
        /// ������ ��������������� �����������, ��� ������� ��������� ��������� ����������
        /// </summary>
        /// <param name="uidContext">��� ������������ ��� �������� ���������� �������� ������� ���������</param>
        /// <returns></returns>
        public List<int> GetCompanyScopeOpen(string uidContext)
        {
            if (_companyScopeOpenContext == null)
            {
                _companyScopeOpenContext = new Dictionary<string, List<int>>();
            }
            if (!_companyScopeOpenContext.ContainsKey(uidContext))
            {
                RefreshCompanyScopeOpenContext(uidContext);

            }
            return _companyScopeOpenContext[uidContext];

        }

        private Dictionary<string, List<int>> _companyScopeCreateContext;
        /// <summary>
        /// ������ ��������������� �����������, ��� ������� ��������� ��������� ����������
        /// </summary>
        /// <param name="uidContext">��� ������������ ��� �������� ���������� �������� ������� ���������</param>
        /// <returns></returns>
        public List<int> GetCompanyScopeCreate(string uidContext)
        {
            if (_companyScopeCreateContext == null)
            {
                _companyScopeCreateContext = new Dictionary<string, List<int>>();
            }
            if (!_companyScopeCreateContext.ContainsKey(uidContext))
            {
                RefreshCompanyScopeCreateContext(uidContext);

            }
            return _companyScopeCreateContext[uidContext];

        }
        /// <summary>
        /// ��������� �� �������������� ���������
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public bool IsDocumentAllowEdit(int companyId)
        {
            if (companyId == 0)
                return true;
            return GetCompanyScopeEdit().Contains(companyId);
        }
        public void RefreshCompanyScopeView()
        {
            _companyScopeView = new List<int>();
            using (SqlConnection cnn = _workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Secure.GetAgentScopeView";
                        //@Uid NVARCHAR(128)=null

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            _companyScopeView.Add(reader.GetInt32(0));
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
        /// �������� ������ ��������������� ����������� ��� ������� ��������� ������� ��������� � ��������� ������������
        /// </summary>
        /// <param name="uidContext">��� ������������ ��� �������� ���������� �������� ������� ���������</param>
        public void RefreshCompanyScopeViewContext(string uidContext)
        {
            List<int> viewData = new List<int>();
            
            if (RightCommon.Admin || RightCommon.AdminEnterprize)
            {

                using (SqlConnection cnn = _workarea.GetDatabaseConnection())
                {
                    if (cnn == null) return;

                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Secure.GetAgentScopeView";
                            SqlParameter prm = cmd.Parameters.Add("@Uid", SqlDbType.NVarChar, 128);
                            if (string.IsNullOrEmpty(uidContext))
                                prm.Value = DBNull.Value;
                            else
                                prm.Value = uidContext;
                            //@Uid NVARCHAR(128)=null

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                viewData.Add(reader.GetInt32(0));
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
            if (_companyScopeViewContext==null)
                _companyScopeViewContext = new Dictionary<string, List<int>>();
            if (_companyScopeViewContext.ContainsKey(uidContext))
                _companyScopeViewContext[uidContext] = viewData;
            else
                _companyScopeViewContext.Add(uidContext, viewData);
        }
        /// <summary>
        /// �������� ������ ��������������� ����������� ��� ������� ��������� ������� �������������� � ��������� ������������
        /// </summary>
        /// <param name="uidContext">��� ������������ ��� �������� ���������� �������� ������� ��������������</param>
        public void RefreshCompanyScopeEditContext(string uidContext)
        {
            List<int> viewData = new List<int>();

            if (RightCommon.Admin || RightCommon.AdminEnterprize)
            {

                using (SqlConnection cnn = _workarea.GetDatabaseConnection())
                {
                    if (cnn == null) return;

                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Secure.GetAgentScopeEdit";
                            SqlParameter prm = cmd.Parameters.Add("@Uid", SqlDbType.NVarChar, 128);
                            if (string.IsNullOrEmpty(uidContext))
                                prm.Value = DBNull.Value;
                            else
                                prm.Value = uidContext;
                            //@Uid NVARCHAR(128)=null

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                viewData.Add(reader.GetInt32(0));
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
            if (_companyScopeEditContext == null)
                _companyScopeEditContext = new Dictionary<string, List<int>>();
            if (_companyScopeEditContext.ContainsKey(uidContext))
                _companyScopeEditContext[uidContext] = viewData;
            else
                _companyScopeEditContext.Add(uidContext, viewData);
        }

        /// <summary>
        /// �������� ������ ��������������� ����������� ��� ������� ��������� ������� �������� � ��������� ������������
        /// </summary>
        /// <param name="uidContext">��� ������������ ��� �������� ���������� �������� ������� ��������</param>
        public void RefreshCompanyScopeOpenContext(string uidContext)
        {
            List<int> viewData = new List<int>();

            if (RightCommon.Admin || RightCommon.AdminEnterprize)
            {

                using (SqlConnection cnn = _workarea.GetDatabaseConnection())
                {
                    if (cnn == null) return;

                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Secure.GetAgentScopeOpen";
                            SqlParameter prm = cmd.Parameters.Add("@Uid", SqlDbType.NVarChar, 128);
                            if (string.IsNullOrEmpty(uidContext))
                                prm.Value = DBNull.Value;
                            else
                                prm.Value = uidContext;
                            //@Uid NVARCHAR(128)=null

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                viewData.Add(reader.GetInt32(0));
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
            if (_companyScopeOpenContext == null)
                _companyScopeOpenContext = new Dictionary<string, List<int>>();
            if (_companyScopeOpenContext.ContainsKey(uidContext))
                _companyScopeOpenContext[uidContext] = viewData;
            else
                _companyScopeOpenContext.Add(uidContext, viewData);
        }

        /// <summary>
        /// �������� ������ ��������������� ����������� ��� ������� ��������� ������� �������� � ��������� ������������
        /// </summary>
        /// <param name="uidContext">��� ������������ ��� �������� ���������� �������� ������� ��������</param>
        public void RefreshCompanyScopeCreateContext(string uidContext)
        {
            List<int> viewData = new List<int>();

            if (RightCommon.Admin || RightCommon.AdminEnterprize)
            {

                using (SqlConnection cnn = _workarea.GetDatabaseConnection())
                {
                    if (cnn == null) return;

                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Secure.GetAgentScopeCreate";
                            SqlParameter prm = cmd.Parameters.Add("@Uid", SqlDbType.NVarChar, 128);
                            if (string.IsNullOrEmpty(uidContext))
                                prm.Value = DBNull.Value;
                            else
                                prm.Value = uidContext;
                            //@Uid NVARCHAR(128)=null

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                viewData.Add(reader.GetInt32(0));
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
            if (_companyScopeCreateContext == null)
                _companyScopeCreateContext = new Dictionary<string, List<int>>();
            if (_companyScopeCreateContext.ContainsKey(uidContext))
                _companyScopeCreateContext[uidContext] = viewData;
            else
                _companyScopeCreateContext.Add(uidContext, viewData);
        }

        public void RefreshCompanyScopeEdit()
        {
           _companyScopeEdit = new List<int>();
            using (SqlConnection cnn = _workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Secure.GetAgentScopeEdit";//Workarea.Empty<Uid>().Entity.FindMethod("ElementRightsLoadByUserDbEntityId").FullName;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            _companyScopeEdit.Add(reader.GetInt32(0));
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
        /// ������ ��������������� �������� ��� ������� ��������� ��������� ��������
        /// </summary>
        /// <param name="uidContext">��� ������������ ��� �������� ���������� �������� ������� ���������</param>
        /// <param name="hierarchyId">������������� ��������</param>
        /// <returns></returns>
        public List<int> HierarchyCompanyScopeViewContext(string uidContext, int hierarchyId)
        {
            List<int> values = new List<int>();
            if (RightCommon.Admin || RightCommon.AdminEnterprize)
            {

                using (SqlConnection cnn = _workarea.GetDatabaseConnection())
                {
                    if (cnn == null) return values;

                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "[Secure].[GetHierarchyAgentScopeView]";
                            SqlParameter prm = cmd.Parameters.Add("@Uid", SqlDbType.NVarChar, 128);
                            if (string.IsNullOrEmpty(uidContext))
                                prm.Value = DBNull.Value;
                            else
                                prm.Value = uidContext;

                            cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = hierarchyId;
                            //@Uid NVARCHAR(128)=null

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                values.Add(reader.GetInt32(0));
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
            return values;
        }
    }
}
