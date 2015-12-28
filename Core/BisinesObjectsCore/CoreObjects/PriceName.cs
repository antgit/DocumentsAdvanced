using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>��������� ������� "��� ����"</summary>
    internal struct PriceNameStruct
    {
        /// <summary>������������� ������</summary>
        public int CurrencyId;
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId;
    }
    /// <summary>
    /// ������������ ���� ����
    /// </summary>
    public sealed class PriceName : BaseCore<PriceName>, IChains<PriceName>, IEquatable<PriceName>, ICodes<PriceName>, IHierarchySupport, ICompanyOwner
    {
        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>��� ����, ������������� �������� 1</summary>
        public const int KINDVALUE_PRICENAME = 1;
        /// <summary>���� ����������, ������������� �������� 2</summary>
        public const int KINDVALUE_PROVIDER = 2;
        /// <summary>���� �����������, ������������� �������� 3</summary>
        public const int KINDVALUE_COMPETITOR = 3;

        /// <summary>��� ����, ������������� �������� 589825</summary>
        public const int KINDID_PRICENAME = 589825;
        /// <summary>���� ����������, ������������� �������� 589826</summary>
        public const int KINDID_PROVIDER = 589826;
        /// <summary>���� �����������, ������������� �������� 589827</summary>
        public const int KINDID_COMPETITOR = 589827;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<PriceName>.Equals(PriceName other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>�����������</summary>
        public PriceName():base()
        {
            EntityId = (short)WhellKnownDbEntity.PriceName;
        }
        protected override void CopyValue(PriceName template)
        {
            base.CopyValue(template);
            CurrencyId = template.CurrencyId;
            MyCompanyId = template.MyCompanyId;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override PriceName Clone(bool endInit)
        {
            PriceName obj = base.Clone(false);
            obj.CurrencyId = CurrencyId;
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }

        #region ��������
        private int _currencyId;
        /// <summary>
        /// ������������� ������
        /// </summary>
        public int CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value; }
        }
        private Currency _currency;
        /// <summary>
        /// ������
        /// </summary>
        public Currency Currency
        {
            get
            {
                if (_currencyId == 0)
                    return null;
                if (_currency == null)
                    _currency = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyId);
                else if (_currency.Id != _currencyId)
                    _currency = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyId);
                return _currency;
            }
            set
            {
                if (_currency == value) return;
                OnPropertyChanging(GlobalPropertyNames.Currency);
                _currency = value;
                _currencyId = _currency == null ? 0 : _currency.Id;
                OnPropertyChanged(GlobalPropertyNames.Currency);
            }
        }

        private int _myCompanyId;
        /// <summary>
        /// ������������� �����������, �������� ����������� ������
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
        /// ��� ��������, ����������� �������� ����������� ������
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

            if (_currencyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CurrencyId, XmlConvert.ToString(_currencyId));
            if (_myCompanyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.CurrencyId) != null)
                _currencyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CurrencyId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
        }
        #endregion

        #region ���������
        PriceNameStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new PriceNameStruct {CurrencyId = _currencyId, MyCompanyId = _myCompanyId};
                return true;
            }
            return false;
        }

        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            CurrencyId = _baseStruct.CurrencyId;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion

        /// <summary>
        /// �������� ������������ ������� ��������� �����������
        /// </summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();

            if (_currencyId == 0)
                throw new ValidateException("�� ������� ������");
            
        }
        #region ���� ������
        /// <summary>��������� ��������� �� ���� ������</summary>
        /// <param name="reader">������ SqlDataReader</param>
        /// <param name="endInit">������� ��������� ��������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _currencyId = reader.GetSqlInt32(17).Value;
                _myCompanyId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
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
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.CurrencyId, SqlDbType.Int) { IsNullable = false, Value = _currencyId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.NVarChar) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<PriceName> Members
        /// <summary>
        /// ����� ������������ ���� ����
        /// </summary>
        /// <returns></returns>
        public List<IChain<PriceName>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// ����� ������������ ���� ����
        /// </summary>
        /// <param name="kind">��� �����</param>
        /// <returns></returns>
        public List<IChain<PriceName>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<PriceName> IChains<PriceName>.SourceList(int chainKindId)
        {
            return Chain<PriceName>.GetChainSourceList(this, chainKindId);
        }
        List<PriceName> IChains<PriceName>.DestinationList(int chainKindId)
        {
            return Chain<PriceName>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<PriceName>> GetValues(bool allKinds)
        {
            return CodeHelper<PriceName>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<PriceName>.GetView(this, true);
        }
        #endregion

        /// <summary>
        /// ������ ������ � ������� ������ ������
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<PriceName>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }

        public List<PriceName> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<PriceName> filter = null,
            int? currencyId = null, int? myCompanyId=null,
            bool useAndFilter = false)
        {
            PriceName item = new PriceName { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("�� ������ ����� {0}, ������ ��� ������ ������ �� ���������������", GlobalMethodAlias.LoadList));
            }
            List<PriceName> collection = new List<PriceName>();
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
                        if (currencyId.HasValue && currencyId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.CurrencyId, SqlDbType.Int).Value = currencyId.Value;
                        if (myCompanyId.HasValue && myCompanyId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = myCompanyId.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new PriceName { Workarea = Workarea };
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
