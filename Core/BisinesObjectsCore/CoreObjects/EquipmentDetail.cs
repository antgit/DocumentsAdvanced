using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>���������� ��������� ������� "������ ������������"</summary>
    internal struct EquipmentDetailStruct
    {
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId;
		/// <summary>
		/// Id ���������� ������������
		/// </summary>
		public int UserOwnerId;
		/// <summary>
		/// ����� �������
		/// </summary>
		public string DrawingNumber;
		/// <summary>
		/// ����� ���������� �������
		/// </summary>
		public string DrawingAssemblyNumber;
		/// <summary>
		/// �����
		/// </summary>
		public decimal Weight;
		/// <summary>
		/// ���� ��������
		/// </summary>
		public DateTime? DateBuild;
		/// <summary>
		/// ������� ��������� (id)
		/// </summary>
		public int UnitId;
		/// <summary>
		/// �������������� �����
		/// </summary>
		public string Nomenclature;
    }
    /// <summary>������������</summary>
    public sealed class EquipmentDetail : BaseCore<EquipmentDetail>, IChains<EquipmentDetail>, IReportChainSupport, IEquatable<EquipmentDetail>,
                                    IComparable, IComparable<EquipmentDetail>,
                                    IFacts<EquipmentDetail>,
                                    IChainsAdvancedList<EquipmentDetail, Knowledge>,
                                    IChainsAdvancedList<EquipmentDetail, Note>,
                                    IChainsAdvancedList<EquipmentDetail, FileData>,
                                    ICodes<EquipmentDetail>, IHierarchySupport, ICompanyOwner
    {
        #region ��������� �������� ����� � ��������
        // ReSharper disable InconsistentNaming

        /// <summary>������ 1</summary>
        public const int KINDVALUE_DETAIL = 1;
        /// <summary>����������� �������� 2</summary>
        public const int KINDVALUE_STDDETAIL = 2;

		/// <summary>������ 7602177</summary>
		public const int KINDID_DETAIL = 7602177;
		/// <summary>����������� �������� 7602178</summary>
		public const int KINDID_STDDETAIL = 7602178;
        #endregion
        bool IEquatable<EquipmentDetail>.Equals(EquipmentDetail other)
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
            EquipmentDetail otherObj = (EquipmentDetail)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// ��������� ���� �������� �� ��������������
        /// </summary>
        /// <param name="other">������ ���������</param>
        /// <returns></returns>
        public int CompareTo(EquipmentDetail other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>�����������</summary>
        public EquipmentDetail()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.EquipmentDetail;
        }
        protected override void CopyValue(EquipmentDetail template)
        {
            base.CopyValue(template);
            MyCompanyId = template.MyCompanyId;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override EquipmentDetail Clone(bool endInit)
        {
            EquipmentDetail obj = base.Clone(false);
            obj.MyCompanyId = MyCompanyId;
			obj.UserOwnerId = UserOwnerId;
			obj.DrawingNumber = DrawingNumber;
			obj.DrawingAssemblyNumber = DrawingAssemblyNumber;
			obj.Weight = Weight;
			obj.DateBuild = DateBuild;
			obj.UnitId = UnitId;
			obj.Nomenclature = Nomenclature;
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

		private int _userOwnerId;
		/// <summary>
		/// ������������� ������������, ������� ������ ������� ������ ������������
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

		private Agent _userOwner;
		/// <summary>
		/// ������������, ������� ������ ������� ������ ������������
		/// </summary>
		public Agent UserOwner
		{
			get
			{
				if (_userOwnerId == 0)
					return null;
				if (_userOwner == null)
					_userOwner = Workarea.Cashe.GetCasheData<Agent>().Item(_userOwnerId);
				else if (_userOwner.Id != _userOwnerId)
					_userOwner = Workarea.Cashe.GetCasheData<Agent>().Item(_userOwnerId);
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

		private string _drawingNumber;
		/// <summary>
		/// ����� ������� ��� �������� ������ ������������
		/// </summary>
		public string DrawingNumber
		{
			get { return _drawingNumber; }
			set
			{
				if (value == _drawingNumber) return;
				OnPropertyChanging(GlobalPropertyNames.DrawingNumber);
				_drawingNumber = value;
				OnPropertyChanged(GlobalPropertyNames.DrawingNumber);
			}
		}

		private string _drawingAssemblyNumber;
		/// <summary>
		/// ����� ���������� ������� ��� �������� ������ ������������
		/// </summary>
		public string DrawingAssemblyNumber
		{
			get { return _drawingAssemblyNumber; }
			set
			{
				if (value == _drawingAssemblyNumber) return;
				OnPropertyChanging(GlobalPropertyNames.DrawingAssemblyNumber);
				_drawingAssemblyNumber = value;
				OnPropertyChanged(GlobalPropertyNames.DrawingAssemblyNumber);
			}
		}

        private decimal _weight;
		/// <summary>
		/// ����� �������� ������ ������������
		/// </summary>
		public decimal Weight
		{
			get { return _weight; }
			set
			{
				if (value == _weight) return;
				OnPropertyChanging(GlobalPropertyNames.Weight);
				_weight = value;
				OnPropertyChanged(GlobalPropertyNames.Weight);
			}
		}

		private DateTime? _dateBuild;
		/// <summary>
		/// ���� ������������
		/// </summary>
		public DateTime? DateBuild
		{
			get { return _dateBuild; }
			set
			{
				if (value == _dateBuild) return;
				OnPropertyChanging(GlobalPropertyNames.DateBuild);
				_dateBuild = value;
				OnPropertyChanged(GlobalPropertyNames.DateBuild);
			}
		}

		private int _unitId;
		/// <summary>
		/// Id ������� ��������� ������� ������ ������������
		/// </summary>
		public int UnitId
		{
			get { return _unitId; }
			set
			{
				if (value == _unitId) return;
				OnPropertyChanging(GlobalPropertyNames.UnitId);
				_unitId = value;
				OnPropertyChanged(GlobalPropertyNames.UnitId);
			}
		}

		private Unit _unit;
		/// <summary>
		/// ������� ��������� ������� ������ ������������
		/// </summary>
		public Unit Unit
		{
			get
			{
				if (_unitId == 0)
					return null;
				if (_unit == null)
					_unit = Workarea.Cashe.GetCasheData<Unit>().Item(_unitId);
				else if (_unit.Id != _unitId)
					_unit = Workarea.Cashe.GetCasheData<Unit>().Item(_unitId);
				return _unit;
			}
			set
			{
				if (_unit == value) return;
				OnPropertyChanging(GlobalPropertyNames.Unit);
				_unit = value;
				_unitId = _unit == null ? 0 : _unit.Id;
				OnPropertyChanged(GlobalPropertyNames.Unit);
			}
		}

		private string _Nomenclature;
		/// <summary>
		/// �������������� ����� �������� ������ ������������
		/// </summary>
		public string Nomenclature
		{
			get { return _Nomenclature; }
			set
			{
				if (value == _Nomenclature) return;
				OnPropertyChanging(GlobalPropertyNames.Nomenclature);
				_Nomenclature = value;
				OnPropertyChanged(GlobalPropertyNames.Nomenclature);
			}
		}

        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();

            if (_myCompanyId == 0)
                throw new ValidateException("�� ������� �������� ��������");
                //throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_CURRENCYINTCODE", 1049));
            //if (string.IsNullOrEmpty(Code))
                //throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_CURRENCYCODE", 1049));
        }
        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

			writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.UserOwnerId, _userOwnerId.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.DrawingNumber, _drawingNumber.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.DrawingAssemblyNumber, _drawingAssemblyNumber.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.Weight, _weight.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.DateBuild, _dateBuild.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.UnitId, _unitId.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.Nomenclature, _Nomenclature.ToString());
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);
			if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
			if (reader.GetAttribute(GlobalPropertyNames.UserOwnerId) != null) _userOwnerId = Int32.Parse(reader[GlobalPropertyNames.UserOwnerId]);
			if (reader.GetAttribute(GlobalPropertyNames.DrawingNumber) != null) _drawingNumber = reader[GlobalPropertyNames.DrawingNumber];
			if (reader.GetAttribute(GlobalPropertyNames.DrawingAssemblyNumber) != null) _drawingAssemblyNumber = reader[GlobalPropertyNames.DrawingAssemblyNumber];
			if (reader.GetAttribute(GlobalPropertyNames.Weight) != null) _weight = Decimal.Parse(reader[GlobalPropertyNames.Weight]);
			if (reader.GetAttribute(GlobalPropertyNames.DateBuild) != null) _dateBuild = DateTime.Parse(reader[GlobalPropertyNames.DateBuild]);
			if (reader.GetAttribute(GlobalPropertyNames.UnitId) != null) _unitId = Int32.Parse(reader[GlobalPropertyNames.UnitId]);
			if (reader.GetAttribute(GlobalPropertyNames.Nomenclature) != null) _Nomenclature = reader[GlobalPropertyNames.Nomenclature];
        }
        #endregion

        #region ���������
        EquipmentDetailStruct _baseDetailStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseDetailStruct = new EquipmentDetailStruct
                                  {
                                      MyCompanyId = _myCompanyId,
									  UserOwnerId = _userOwnerId,
									  DrawingNumber = _drawingNumber,
									  DrawingAssemblyNumber = _drawingAssemblyNumber,
									  Weight = _weight,
									  DateBuild = _dateBuild,
									  UnitId = _unitId,
									  Nomenclature = _Nomenclature
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
			MyCompanyId = _baseDetailStruct.MyCompanyId;
			UserOwnerId = _baseDetailStruct.UserOwnerId;
			DrawingNumber = _baseDetailStruct.DrawingNumber;
			DrawingAssemblyNumber = _baseDetailStruct.DrawingAssemblyNumber;
			Weight = _baseDetailStruct.Weight;
			DateBuild = _baseDetailStruct.DateBuild;
			UnitId = _baseDetailStruct.UnitId;
			Nomenclature = _baseDetailStruct.Nomenclature;
            IsChanged = false;
        }
        #endregion
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
				_userOwnerId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _drawingNumber = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                _drawingAssemblyNumber = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
				_weight = reader.IsDBNull(21) ? 0 : reader.GetDecimal(21);
				_dateBuild = reader.IsDBNull(22) ? null : (DateTime?)reader.GetDateTime(22);
				_unitId = reader.IsDBNull(23) ? 0 : reader.GetInt32(23);
				_Nomenclature = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.UserOwnerId, SqlDbType.Int) { IsNullable = true };
			if (_userOwnerId == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _userOwnerId;
			sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DrawingNumber, SqlDbType.NVarChar, 50) { IsNullable = true };
            if (string.IsNullOrEmpty(_drawingNumber))
				prm.Value = DBNull.Value;
			else
				prm.Value = _drawingNumber;
			sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DrawingAssemblyNumber, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_drawingAssemblyNumber))
				prm.Value = DBNull.Value;
			else
				prm.Value = _drawingAssemblyNumber;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.Weight, SqlDbType.Money) { IsNullable = true };
			prm.Value = _weight;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.DateBuild, SqlDbType.Date) { IsNullable = true };
			if (_dateBuild == null)
				prm.Value = DBNull.Value;
			else
				prm.Value = _dateBuild;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.UnitId, SqlDbType.Int) { IsNullable = true };
			if (_unitId == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _unitId;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.Nomenclature, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_Nomenclature))
				prm.Value = DBNull.Value;
			else
				prm.Value = _Nomenclature;
			sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<EquipmentDetail> Members
        /// <summary>����� ���������</summary>
        /// <returns></returns>
        public List<IChain<EquipmentDetail>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>����� ���������</summary>
        /// <param name="kind">��� �����</param>
        /// <returns></returns>
        public List<IChain<EquipmentDetail>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<EquipmentDetail> IChains<EquipmentDetail>.SourceList(int chainKindId)
        {
            return Chain<EquipmentDetail>.GetChainSourceList(this, chainKindId);
        }
        List<EquipmentDetail> IChains<EquipmentDetail>.DestinationList(int chainKindId)
        {
            return Chain<EquipmentDetail>.DestinationList(this, chainKindId);

        }
        #endregion

        #region IChainsAdvancedList<Agent,Knowledge> Members

        List<IChainAdvanced<EquipmentDetail, Knowledge>> IChainsAdvancedList<EquipmentDetail, Knowledge>.GetLinks()
        {
            return ((IChainsAdvancedList<EquipmentDetail, Knowledge>)this).GetLinks(59);
        }

        List<IChainAdvanced<EquipmentDetail, Knowledge>> IChainsAdvancedList<EquipmentDetail, Knowledge>.GetLinks(int? kind)
        {
            return GetLinkedKnowledges();
        }
        public List<IChainAdvanced<EquipmentDetail, Knowledge>> GetLinkedKnowledges()
        {
            List<IChainAdvanced<EquipmentDetail, Knowledge>> collection = new List<IChainAdvanced<EquipmentDetail, Knowledge>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<EquipmentDetail>().Entity.FindMethod("LoadKnowledges").FullName;
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
                                ChainAdvanced<EquipmentDetail, Knowledge> item = new ChainAdvanced<EquipmentDetail, Knowledge> { Workarea = Workarea, Left = this };
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
        List<ChainValueView> IChainsAdvancedList<EquipmentDetail, Knowledge>.GetChainView()
        {
            return ChainValueView.GetView<EquipmentDetail, Knowledge>(this);
        }
        #endregion
        #region IChainsAdvancedList<EquipmentDetail,Note> Members

        List<IChainAdvanced<EquipmentDetail, Note>> IChainsAdvancedList<EquipmentDetail, Note>.GetLinks()
        {
            return ChainAdvanced<EquipmentDetail, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<EquipmentDetail, Note>> IChainsAdvancedList<EquipmentDetail, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<EquipmentDetail, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<EquipmentDetail, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<EquipmentDetail, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<EquipmentDetail, Note>.GetChainView()
        {
            return ChainValueView.GetView<EquipmentDetail, Note>(this);
        }
        #endregion
        #region IChainsAdvancedList<EquipmentDetail,FileData> Members

        List<IChainAdvanced<EquipmentDetail, FileData>> IChainsAdvancedList<EquipmentDetail, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<EquipmentDetail, FileData>)this).GetLinks(70);
        }

        List<IChainAdvanced<EquipmentDetail, FileData>> IChainsAdvancedList<EquipmentDetail, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<EquipmentDetail, FileData>.GetChainView()
        {
            return ChainValueView.GetView<EquipmentDetail, FileData>(this);
        }
        public List<IChainAdvanced<EquipmentDetail, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<EquipmentDetail, FileData>> collection = new List<IChainAdvanced<EquipmentDetail, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<EquipmentDetail>().Entity.FindMethod("LoadFiles").FullName;
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
                                ChainAdvanced<EquipmentDetail, FileData> item = new ChainAdvanced<EquipmentDetail, FileData> { Workarea = Workarea, Left = this };
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
        #region ICodes
        public List<CodeValue<EquipmentDetail>> GetValues(bool allKinds)
        {
            return CodeHelper<EquipmentDetail>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<EquipmentDetail>.GetView(this, true);
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

        private int? _firstHierarchy;
        /// <summary>
        /// ������ ������ � ������� ������ ������
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            return FirstHierarchy(false);
        }
        /// <summary>
        /// ������ ������ � ������� ������ ������
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy(bool refresh)
        {
            if (!refresh && (LastLoadPartial.HasValue && LastLoadPartial.Value.AddMinutes(Workarea.Cashe.DefaultPartalReloadTime) > DateTime.Now))
            {
                if (!_firstHierarchy.HasValue) return null;
                return Workarea.Cashe.GetCasheData<Hierarchy>().Item(_firstHierarchy.Value);
            }
            _firstHierarchy = Hierarchy.FirstHierarchy<EquipmentDetail>(this);
            LastLoadPartial = DateTime.Now;
            if (!_firstHierarchy.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(_firstHierarchy.Value);
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
        public List<EquipmentDetail> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
                                     int? stateId = null, string name = null, int kindId = 0, string code = null,
                                     string memo = null, string flagString = null, int templateId = 0,
                                     int count = 100, Predicate<EquipmentDetail> filter = null, bool useAndFilter = false)
        {
            EquipmentDetail item = new EquipmentDetail { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("�� ������ ����� {0}, ������ ��� ������ ������ �� ���������������", GlobalMethodAlias.LoadList));
            }
            List<EquipmentDetail> collection = new List<EquipmentDetail>();
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
                            item = new EquipmentDetail { Workarea = Workarea };
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