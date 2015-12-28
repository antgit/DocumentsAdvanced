using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>��������� ������� "������"</summary>
    internal struct CurrencyStruct
    {
        /// <summary>�������� ��� ������</summary>
        public int IntCode;
    }
    /// <summary>������</summary>
    public sealed class Currency : BaseCore<Currency>, IChains<Currency>, IEquatable<Currency>, IHierarchySupport
    {
        #region ��������� �������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>������, ������������� �������� 1</summary>
        public const int KINDVALUE_CURRENCY = 1;

        /// <summary>������, ������������� �������� 327681</summary>
        public const int KINDID_CURRENCY = 327681;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Currency>.Equals(Currency other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>�����������</summary>
        public Currency(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Currency;
        }
        protected override void CopyValue(Currency template)
        {
            base.CopyValue(template);
            IntCode = template.IntCode;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override Currency Clone(bool endInit)
        {
            Currency obj = base.Clone(false);
            obj.IntCode = IntCode;

            if (endInit)
                OnEndInit();
            return obj;
        }
        /// <summary>
        /// ��������� ������� ��� ������ ������ �������
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange(Currency value)
        {
            if (!base.CompareExchange(value))
            {
                return false;
            }

            if (value.IntCode != IntCode)
                return false;

            return true;
        }
        private int _intCode;
        /// <summary>�������� ��� ������</summary>
        public int IntCode
        {
            get { return _intCode; }
            set
            {
                if (value == _intCode) return;
                OnPropertyChanging(GlobalPropertyNames.IntCode);
                _intCode = value;
                OnPropertyChanged(GlobalPropertyNames.IntCode);
                
            }
        }

        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);
            writer.WriteAttributeString(GlobalPropertyNames.IntCode, XmlConvert.ToString(IntCode));

        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.IntCode) != null)
                _intCode = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.IntCode));
        } 
        #endregion
        #region ���������
        CurrencyStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new CurrencyStruct {IntCode = _intCode};
                return true;
            }
            return false;
        }

        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            IntCode = _baseStruct.IntCode;
            IsChanged = false;
        }
        #endregion
        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();
            
            if (_intCode == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_CURRENCYINTCODE", 1049));
            if (string.IsNullOrEmpty(Code))
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_CURRENCYCODE", 1049));
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
                _intCode = reader.GetSqlInt32(17).Value;
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
        /// <param name="validateVersion">��������� �� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.IntCode, SqlDbType.Int) {IsNullable = false, Value = _intCode};
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Currency> Members
        /// <summary>����� ������</summary>
        /// <returns></returns>
        public List<IChain<Currency>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>����� ������</summary>
        /// <param name="kind">��� �����</param>
        /// <returns></returns>
        public List<IChain<Currency>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Currency> IChains<Currency>.SourceList(int chainKindId)
        {
            return Chain<Currency>.GetChainSourceList(this, chainKindId);
        }
        List<Currency> IChains<Currency>.DestinationList(int chainKindId)
        {
            return Chain<Currency>.DestinationList(this, chainKindId);
        }
        #endregion

        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<Currency>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }

        public List<Currency> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Currency> filter = null,
            int? intCode=null,
            bool useAndFilter = false)
        {
            Currency item = new Currency { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("�� ������ ����� {0}, ������ ��� ������ ������ �� ���������������", GlobalMethodAlias.LoadList));
            }
            List<Currency> collection = new List<Currency>();
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
                        if (intCode.HasValue && intCode != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.IntCode, SqlDbType.Int).Value = intCode.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Currency { Workarea = Workarea };
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
