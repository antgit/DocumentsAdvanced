using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>��������� ������� "�������� �������"</summary>
    internal struct DateRegionStruct
    {
        /// <summary>���� ������</summary>
        public DateTime DateStart;
        /// <summary>���� ���������</summary>
        public DateTime DateEnd;
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId;
    }
    /// <summary>�������� �������</summary>
    /// <remarks></remarks>
    public sealed class DateRegion : BaseCore<DateRegion>, IChains<DateRegion>, IReportChainSupport, IEquatable<DateRegion>,
                                     IComparable, IComparable<DateRegion>,
                                     IFacts<DateRegion>,
                                     ICodes<DateRegion>,
        ICompanyOwner
    {
        #region ��������� �������� ����� � ��������
        // ReSharper disable InconsistentNaming

        /// <summary>������������ ��������, ������������� �������� 1</summary>
        public const int KINDVALUE_CUSTOM = 1;
        /// <summary>�����, ������������� �������� 2</summary>
        public const int KINDVALUE_MONTH = 2;
        /// <summary>�������, ������������� �������� 3</summary>
        public const int KINDVALUE_QUARTAL = 3;
        /// <summary>���������, ������������� �������� 4</summary>
        public const int KINDVALUE_HALFYEAR = 4;
        /// <summary>���, ������������� �������� 5</summary>
        public const int KINDVALUE_YEAR = 5;
        /// <summary>������, ������������� �������� 6</summary>
        public const int KINDVALUE_WEEK = 6;

        /// <summary>������������ ��������, ������������� �������� 5505025</summary>
        public const int KINDID_CUSTOM = 5505025;
        /// <summary>�����, ������������� �������� 5505026</summary>
        public const int KINDID_MONTH = 5505026;
        /// <summary>�������, ������������� �������� 5505027</summary>
        public const int KINDID_QUARTAL = 5505027;
        /// <summary>���������, ������������� �������� 5505028</summary>
        public const int KINDID_HALFYEAR = 5505028;
        /// <summary>���, ������������� �������� 5505029</summary>
        public const int KINDID_YEAR = 5505029;
        /// <summary>���, ������������� �������� 5505030</summary>
        public const int KINDID_WEEK = 5505030;
        
        // ReSharper restore InconsistentNaming

        #endregion
        bool IEquatable<DateRegion>.Equals(DateRegion other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// ��������� ���� ���������� �� ��������������
        /// </summary>
        /// <param name="obj">������</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            DateRegion otherObj = (DateRegion)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// ��������� ���� ���������� �� ��������������
        /// </summary>
        /// <param name="other">������ ��������� �������</param>
        /// <returns></returns>
        public int CompareTo(DateRegion other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>
        /// ��������� ������� ��� ������ ������ �������
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange(DateRegion value)
        {
            if (!base.CompareExchange(value))
            {
                return false;
            }

            if (value.DateStart != DateStart)
                return false;
            if (value.DateEnd != DateEnd)
                return false;

            return true;
        }
        /// <summary>�����������</summary>
        public DateRegion()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.DateRegion;
        }
        protected override void CopyValue(DateRegion template)
        {
            base.CopyValue(template);
            DateStart = template.DateStart;
            DateEnd = template.DateEnd;
        }
        #region ��������

        private DateTime _dateStart;
        /// <summary>
        /// ���� ������
        /// </summary>
        public DateTime DateStart
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

        private DateTime _dateEnd;
        /// <summary>
        /// ���� ���������
        /// </summary>
        public DateTime DateEnd
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
        #endregion

        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);
            writer.WriteAttributeString(GlobalPropertyNames.DateStart, XmlConvert.ToString(DateStart, XmlDateTimeSerializationMode.Local));
            writer.WriteAttributeString(GlobalPropertyNames.DateEnd, XmlConvert.ToString(DateEnd, XmlDateTimeSerializationMode.Local));
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.DateStart) != null)
                _dateStart = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateStart));
            if (reader.GetAttribute(GlobalPropertyNames.DateEnd) != null)
                _dateEnd = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateEnd));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion
        #region ���������
        DateRegionStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new DateRegionStruct { DateStart = _dateStart, DateEnd = _dateEnd, MyCompanyId = _myCompanyId};
                return true;
            }
            return false;
        }

        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            DateStart = _baseStruct.DateStart;
            DateEnd = _baseStruct.DateEnd;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion
        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();

            if (_dateStart < System.Data.SqlTypes.SqlDateTime.MinValue )
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_DATEREGIONMINDATESTART", 1049));

            if (_dateStart > System.Data.SqlTypes.SqlDateTime.MaxValue)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_DATEREGIONMAXDATESTART", 1049));

            if (_dateEnd < System.Data.SqlTypes.SqlDateTime.MinValue)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_DATEREGIONMINDATEEND", 1049));

            if (_dateEnd > System.Data.SqlTypes.SqlDateTime.MaxValue)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_DATEREGIONMAXDATEEND", 1049));
            
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
                _dateStart = reader.GetDateTime(17);
                _dateEnd = reader.GetDateTime(18);
                _myCompanyId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.DateStart, SqlDbType.Date) { IsNullable = false, Value = _dateStart };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateEnd, SqlDbType.Date) { IsNullable = false, Value = _dateEnd};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<DateRegion> Members
        /// <summary>����� ���������</summary>
        /// <returns></returns>
        public List<IChain<DateRegion>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>����� ���������</summary>
        /// <param name="kind">��� �����</param>
        /// <returns></returns>
        public List<IChain<DateRegion>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<DateRegion> IChains<DateRegion>.SourceList(int chainKindId)
        {
            return Chain<DateRegion>.GetChainSourceList(this, chainKindId);
        }
        List<DateRegion> IChains<DateRegion>.DestinationList(int chainKindId)
        {
            return Chain<DateRegion>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<DateRegion>> GetValues(bool allKinds)
        {
            return CodeHelper<DateRegion>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<DateRegion>.GetView(this, true);
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
    }
}