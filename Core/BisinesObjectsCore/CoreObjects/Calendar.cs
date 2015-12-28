using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessObjects
{
    /// <summary>���������</summary>
    public sealed class Calendar : BaseCore<Calendar>, IChains<Calendar>, IReportChainSupport, IEquatable<Calendar>,
                                   IComparable, IComparable<Calendar>,
                                   IFacts<Calendar>,
                                   IChainsAdvancedList<Calendar, Knowledge>,
                                   IChainsAdvancedList<Calendar, Note>,
                                   ICodes<Calendar>, IHierarchySupport, ICompanyOwner
    {
        #region ��������� �������� ����� � ��������
        // ReSharper disable InconsistentNaming

        /// <summary>���������, ������������� �������� 1</summary>
        public const int KINDVALUE_EVENT = 1;
        /// <summary>�������� ������, ������������� �������� 2</summary>
        public const int KINDVALUE_EVENTTAX = 2;

        /// <summary>���������, ������������� �������� 7012353</summary>
        public const int KINDID_ANALITIC = 7012353;
        /// <summary>�������� ������, ������������� �������� 7012354</summary>
        public const int KINDID_TRADEGROUP = 7012354;
        
        // ReSharper restore InconsistentNaming

        #endregion
        bool IEquatable<Calendar>.Equals(Calendar other)
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
            Calendar otherObj = (Calendar)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// ��������� ���� �������� �� ��������������
        /// </summary>
        /// <param name="other">������ ���������</param>
        /// <returns></returns>
        public int CompareTo(Calendar other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>�����������</summary>
        public Calendar()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Calendar;
        }

        protected override void CopyValue(Calendar template)
        {
            base.CopyValue(template);
            StartDate = template.StartDate;
            StartTime = template.StartTime;
            PriorityId = template.PriorityId;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override Calendar Clone(bool endInit)
        {
            Calendar obj = base.Clone(false);
            obj.StartDate = StartDate;
            obj.StartTime = StartTime;
            obj.PriorityId = PriorityId;

            if (endInit)
                OnEndInit();
            return obj;
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


        private DateTime _startDate;
        /// <summary>
        /// ����
        /// </summary>
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (value == _startDate) return;
                OnPropertyChanging(GlobalPropertyNames.StartDate);
                _startDate = value;
                OnPropertyChanged(GlobalPropertyNames.StartDate);
            }
        }

        private TimeSpan _startTime;
        /// <summary>
        /// �����
        /// </summary>
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                if (value == _startTime) return;
                OnPropertyChanging(GlobalPropertyNames.StartTime);
                _startTime = value;
                OnPropertyChanged(GlobalPropertyNames.StartTime);
            }
        }




        private int _priorityId;
        /// <summary>
        /// ������������� ����������
        /// </summary>
        public int PriorityId
        {
            get { return _priorityId; }
            set
            {
                if (value == _priorityId) return;
                OnPropertyChanging(GlobalPropertyNames.PriorityId);
                _priorityId = value;
                OnPropertyChanged(GlobalPropertyNames.PriorityId);
            }
        }


        private Analitic _priority;
        /// <summary>
        /// ���������
        /// </summary>
        public Analitic Priority
        {
            get
            {
                if (_priorityId == 0)
                    return null;
                if (_priority == null)
                    _priority = Workarea.Cashe.GetCasheData<Analitic>().Item(_priorityId);
                else if (_priority.Id != _priorityId)
                    _priority = Workarea.Cashe.GetCasheData<Analitic>().Item(_priorityId);
                return _priority;
            }
            set
            {
                if (_priority == value) return;
                OnPropertyChanging(GlobalPropertyNames.Priority);
                _priority = value;
                _priorityId = _priority == null ? 0 : _priority.Id;
                OnPropertyChanged(GlobalPropertyNames.Priority);
            }
        }

        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();

            if (_priorityId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_CALENDARPRIORITY", 1049));
            
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
                _myCompanyId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _startDate = reader.GetDateTime(18);
                _startTime = reader.GetTimeSpan(19);
                _priorityId = reader.GetInt32(20);
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.NVarChar) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.StartDate, SqlDbType.Date) { IsNullable = false };
            prm.Value = _startDate;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.StartTime, SqlDbType.Time) { IsNullable = false };
            prm.Value = _startTime;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PriorityId, SqlDbType.Int) { IsNullable = false };
            prm.Value = _priorityId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Calendar> Members
        /// <summary>����� ���������</summary>
        /// <returns></returns>
        public List<IChain<Calendar>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>����� ���������</summary>
        /// <param name="kind">��� �����</param>
        /// <returns></returns>
        public List<IChain<Calendar>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Calendar> IChains<Calendar>.SourceList(int chainKindId)
        {
            return Chain<Calendar>.GetChainSourceList(this, chainKindId);
        }
        List<Calendar> IChains<Calendar>.DestinationList(int chainKindId)
        {
            return Chain<Calendar>.DestinationList(this, chainKindId);

        }
        #endregion

        #region IChainsAdvancedList<Agent,Knowledge> Members

        List<IChainAdvanced<Calendar, Knowledge>> IChainsAdvancedList<Calendar, Knowledge>.GetLinks()
        {
            return ((IChainsAdvancedList<Calendar, Knowledge>)this).GetLinks(59);
        }

        List<IChainAdvanced<Calendar, Knowledge>> IChainsAdvancedList<Calendar, Knowledge>.GetLinks(int? kind)
        {
            return GetLinkedKnowledges();
        }
        public List<IChainAdvanced<Calendar, Knowledge>> GetLinkedKnowledges()
        {
            List<IChainAdvanced<Calendar, Knowledge>> collection = new List<IChainAdvanced<Calendar, Knowledge>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Calendar>().Entity.FindMethod("LoadKnowledges").FullName;
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
                                ChainAdvanced<Calendar, Knowledge> item = new ChainAdvanced<Calendar, Knowledge> { Workarea = Workarea, Left = this };
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
        List<ChainValueView> IChainsAdvancedList<Calendar, Knowledge>.GetChainView()
        {
            return ChainValueView.GetView<Calendar, Knowledge>(this);
        }
        #endregion
        #region IChainsAdvancedList<Calendar,Note> Members

        List<IChainAdvanced<Calendar, Note>> IChainsAdvancedList<Calendar, Note>.GetLinks()
        {
            return ChainAdvanced<Calendar, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Calendar, Note>> IChainsAdvancedList<Calendar, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Calendar, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Calendar, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Calendar, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Calendar, Note>.GetChainView()
        {
            return ChainValueView.GetView<Calendar, Note>(this);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Calendar>> GetValues(bool allKinds)
        {
            return CodeHelper<Calendar>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Calendar>.GetView(this, true);
        }
        #endregion

        #region IFacts

        private List<FactView> _factView;
        public List<FactView> GetCollectionFactView()
        {
            return _factView ?? (_factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId));
        }

        public void RefreshFa�tView()
        {
            _factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId);
        }

        public FactView GetFactViewValue(string factCode, string columnCode)
        {
            return GetCollectionFactView().FirstOrDefault(s => s.FactNameCode == factCode && s.ColumnCode == columnCode);
        }

        public List<FactName> GetFactNames()
        {
            return FactHelper.GetFactNames(Workarea, EntityId);
        }
        #endregion
        /// <summary>
        /// ������ ������ � ������� ������ ������
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<Calendar>(this);
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
        public List<Calendar> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
                                     int? stateId = null, string name = null, int kindId = 0, string code = null,
                                     string memo = null, string flagString = null, int templateId = 0,
                                     int count = 100, Predicate<Calendar> filter = null, bool useAndFilter = false)
        {
            Calendar item = new Calendar { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("�� ������ ����� {0}, ������ ��� ������ ������ �� ���������������", GlobalMethodAlias.LoadList));
            }
            List<Calendar> collection = new List<Calendar>();
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
                            item = new Calendar { Workarea = Workarea };
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