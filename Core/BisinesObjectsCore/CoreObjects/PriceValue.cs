using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>��������� ������� "����"</summary>
    internal struct PriceStruct
    {
        /// <summary>����</summary>
        public DateTime Date;
        /// <summary>������������� ���� ����</summary>
        public int PriceNameId;
        /// <summary>������������� ������</summary>
        public int ProductId;
        /// <summary>��������</summary>
        public decimal Value;
        /// <summary>������������� "���"</summary>
        public int AgentFromId;
        /// <summary>������������� "����"</summary>
        public int AgentToId;
    }
    /// <summary>
    /// ����
    /// </summary>
    public sealed class PriceValue : BaseCore<PriceValue>, IEquatable<PriceValue>
    {
        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>����, ������������� �������� 1</summary>
        public const int KINDVALUE_PRICEVALUE = 1;

        /// <summary>����, ������������� �������� 1048577</summary>
        public const int KINDID_PRICEVALUE = 1048577;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<PriceValue>.Equals(PriceValue other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>�����������</summary>
        public PriceValue():base()
        {
            EntityId = (short)WhellKnownDbEntity.Price;
        }
        protected override void CopyValue(PriceValue template)
        {
            base.CopyValue(template);
            AgentFromId = template.AgentFromId;
            Date = template.Date;
            PriceNameId = template.PriceNameId;
            Value = template.Value;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override PriceValue Clone(bool endInit)
        {
            PriceValue obj = base.Clone(false);
            obj.AgentFromId = AgentFromId;
            obj.Date = Date;
            obj.PriceNameId = PriceNameId;
            obj.Value = Value;
            if (endInit)
                OnEndInit();
            return obj;
        }

        #region ��������
        private DateTime _date;
        /// <summary>
        /// ����
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value == _date) return;
                OnPropertyChanging(GlobalPropertyNames.Date);
                _date = value;
                OnPropertyChanged(GlobalPropertyNames.Date);
            }
        }

        private int _priceNameId;
        /// <summary>
        /// ������������� ���� ����
        /// </summary>
        public int PriceNameId
        {
            get { return _priceNameId; }
            set
            {
                if (value == _priceNameId) return;
                OnPropertyChanging(GlobalPropertyNames.PriceNameId);
                _priceNameId = value;
                OnPropertyChanged(GlobalPropertyNames.PriceNameId);
            }
        }

        private int _productId;
        /// <summary>
        /// ������������� ������
        /// </summary>
        public int ProductId
        {
            get { return _productId; }
            set
            {
                if (value == _productId) return;
                OnPropertyChanging(GlobalPropertyNames.ProductId);
                _productId = value;
                OnPropertyChanged(GlobalPropertyNames.ProductId);
            }
        }

        private decimal _value;
        /// <summary>
        /// ��������
        /// </summary>
        public decimal Value
        {
            get { return _value; }
            set
            {
                if (value == _value) return;
                OnPropertyChanging(GlobalPropertyNames.Value);
                _value = value;
                OnPropertyChanged(GlobalPropertyNames.Value);
            }
        }


        private int _agentFromId;
        /// <summary>
        /// ������������� ��������������
        /// </summary>
        /// <remarks>������������� ��������������, �������� ����������� ����</remarks>
        public int AgentFromId
        {
            get { return _agentFromId; }
            set
            {
                if (value == _agentFromId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentFromId);
                _agentFromId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentFromId);
            }
        }

        private int _agentToId;
        /// <summary>
        /// ������������� ��������������
        /// </summary>
        /// <remarks>������������� ��������������</remarks>
        public int AgentToId
        {
            get { return _agentToId; }
            set
            {
                if (value == _agentToId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentToId);
                _agentToId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentToId);
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

            //if (_date != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Date, XmlConvert.ToString(_date));
            if (_priceNameId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.PriceNameId, XmlConvert.ToString(_priceNameId));
            if (_productId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProductId, XmlConvert.ToString(_productId));
            if (_value != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Value, XmlConvert.ToString(_value));
            if (_agentFromId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentFromId, XmlConvert.ToString(_agentFromId));
            if (_agentToId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentToId, XmlConvert.ToString(_agentToId));
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Date) != null)
                _date = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.Date));
            if (reader.GetAttribute(GlobalPropertyNames.PriceNameId) != null)
                _priceNameId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PriceNameId));
            if (reader.GetAttribute(GlobalPropertyNames.ProductId) != null)
                _productId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProductId));
            if (reader.GetAttribute(GlobalPropertyNames.Value) != null)
                _value = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Value));
            if (reader.GetAttribute(GlobalPropertyNames.AgentFromId) != null)
                _agentFromId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentFromId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentToId) != null)
                _agentToId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentToId));
        }
        #endregion

        #region ���������
        PriceStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new PriceStruct
                                  {
                                      Date = _date,
                                      PriceNameId = _priceNameId,
                                      Value = _value
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
            Date = _baseStruct.Date;
            PriceNameId = _baseStruct.PriceNameId;
            Value = _baseStruct.Value;
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
            
                if (_priceNameId == 0)
                    throw new ValidateException("�� ������ ��� ����");
            
        }
        /// <summary>��������� ��������� �� ���� ������</summary>
        /// <param name="reader">������ SqlDataReader</param>
        /// <param name="endInit">������� ��������� ��������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            OnBeginInit();
            try
            {
                Id = reader.GetInt32(0);
                Guid = reader.GetGuid(1);
                DatabaseId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                DbSourceId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                ObjectVersion = reader.GetSqlBinary(4).Value;
                UserName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                if (reader.IsDBNull(6))
                    DateModified = null;
                else
                    DateModified = reader.GetDateTime(6);
                FlagsValue = reader.GetInt32(7);
                StateId = reader.GetInt32(8);

                //name = reader.GetString(9);
                //NameFull = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                //KindId = reader.GetInt32(11);
                //KindValue = BaseKind.ExtractSubKind(KindId);
                //Code = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                //CodeFind = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                //Memo = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                //FlagString = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                //TemplateId = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                //foreach (CustomProperty prop in CustomPropertyValues)
                //{
                //    prop.Load(reader);
                //}

                _date = reader.GetDateTime(9);
                _priceNameId = reader.GetInt32(10);
                _productId = reader.GetInt32(11);
                _value = reader.GetDecimal(12);
                _agentFromId = reader.GetInt32(13);
                _agentToId = reader.GetInt32(14);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();

            //// TODO: ���������
            //base.Load(reader, false);
            //try
            //{
            //    _date = reader.GetDateTime(11);
            //    _priceNameId = reader.GetInt32(12);
            //    _productId = reader.GetInt32(13);
            //    _value = reader.GetDecimal(14);
            //    _agentFromId = reader.GetInt32(15);
            //    _agentToId = reader.GetInt32(16);
            //}
            //catch(Exception ex)
            //{
            //    throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            //}
            //if (!endInit) return;
            //OnEndInit();
        }
        /// <summary>���������� �������� ���������� ��� �������� ��������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ��������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Id, SqlDbType.Int) { IsNullable = true };
            if (Id == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = Id;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Guid, SqlDbType.UniqueIdentifier) { IsNullable = true };
            if (Guid == Guid.Empty)
                prm.Value = DBNull.Value;
            else
                prm.Value = Guid;
            sqlCmd.Parameters.Add(prm);


            prm = new SqlParameter(GlobalSqlParamNames.DatabaseId, SqlDbType.Int) { IsNullable = false, Value = DatabaseId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DbSourceId, SqlDbType.Int) { IsNullable = true };
            if (DbSourceId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = DbSourceId;
            sqlCmd.Parameters.Add(prm);

            if (!insertCommand && validateVersion)
            {
                prm = new SqlParameter(GlobalSqlParamNames.Version, SqlDbType.Binary, 8) { IsNullable = true };

                if (ObjectVersion == null || ObjectVersion.All(v => v == 0))
                    prm.Value = DBNull.Value;
                else
                    prm.Value = ObjectVersion;
                sqlCmd.Parameters.Add(prm);
            }

            prm = new SqlParameter(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128)
            {
                IsNullable = true,
                Value = DBNull.Value
            };
            // TODO: ������������� ��������� ������������
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateModified, SqlDbType.DateTime)
            {
                IsNullable = true,
                Value = DBNull.Value
            };
            // TODO: ������������� ��������� ���� ���������
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.StateId, SqlDbType.Int) { IsNullable = false, Value = StateId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Flags, SqlDbType.Int) { IsNullable = false, Value = FlagsValue };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Date, SqlDbType.DateTime) { IsNullable = false, Value = _date };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.PrcNameId, SqlDbType.Int) { IsNullable = false, Value = _priceNameId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.ProductId, SqlDbType.Int) { IsNullable = false, Value = _productId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Value, SqlDbType.Decimal) { IsNullable = false, Value = _value };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.AgentFromId, SqlDbType.Int) { IsNullable = false, Value = _agentFromId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.AgentToId, SqlDbType.Int) { IsNullable = false, Value = _agentToId };
            sqlCmd.Parameters.Add(prm);

            //base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            //SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Date, SqlDbType.DateTime) { IsNullable = false, Value = _date };
            //sqlCmd.Parameters.Add(prm);
            
            //prm = new SqlParameter(GlobalSqlParamNames.PriceNameId, SqlDbType.Int) {IsNullable = false, Value = _priceNameId};
            //sqlCmd.Parameters.Add(prm);
            //prm = new SqlParameter(GlobalSqlParamNames.ProductId, SqlDbType.Int) { IsNullable = false, Value = _productId };
            //sqlCmd.Parameters.Add(prm);
            //prm = new SqlParameter(GlobalSqlParamNames.Value, SqlDbType.Decimal) { IsNullable = false, Value = _value };
            //sqlCmd.Parameters.Add(prm);
            //prm = new SqlParameter(GlobalSqlParamNames.AgentFromId, SqlDbType.Int) { IsNullable = false, Value = _agentFromId };
            //sqlCmd.Parameters.Add(prm);
            //prm = new SqlParameter(GlobalSqlParamNames.AgentToId, SqlDbType.Int) { IsNullable = false, Value = _agentToId };
            //sqlCmd.Parameters.Add(prm);
        }

        /// <summary>
        /// ������� ���������� ���� ��� ������
        /// </summary>
        /// <remarks>�������, ���������� ���� ������������� ��������� �������:
        /// 1. ������ � ���� ������������� ����������� � ������� ����, ������, ������� � �������� ��������� - ���� ������� ����������� ���� �� ��������� ���� - ������������ ��������� (������������� ������ ��������������� �����-�����)
        /// 2. ���� ������ ������� �����������: ���� ����������� � ������� ����, ������, ��������-��������� - ���� ������� ����������� ���� �� ��������� ���� - ������������ ��������� (������������� ������ ������ �����-����� ��������)
        /// <para>
        /// �������� ��������! ���� ������������ �� ��������� ����: � ����������� �� �������� ����������� � ������ ��� ���������� �������� ������� �������� 
        /// ������������ ������� (�� �������) ���� ��������� � �������� ���� - �� ���� ���������, � ������� ����!
        /// </para>
        /// </remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="prcId">������������� ���� ����</param>
        /// <param name="productId">������������� ������</param>
        /// <param name="agentId">������������� ��������������</param>
        /// <param name="date">����</param>
        /// <param name="agentClientId">������������� �������� �������</param>
        /// <param name="agentHoldingId">������������� ��������</param>
        /// <returns></returns>
        public static decimal GetActualPrice(Workarea wa, int prcId, int productId, int agentId, DateTime date, int? agentClientId=null, int? agentHoldingId=null)
        {
            if (productId == 0) return 0;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return 0;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = wa.Empty<PriceValue>().Entity.FindMethod("GetActualPrice").FullName;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.ProductId, SqlDbType.Int).Value = productId;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Date, SqlDbType.DateTime).Value = date;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.AgentFromId, SqlDbType.Int).Value = agentId;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.PrcNameId, SqlDbType.Int).Value = prcId;
                        if (agentClientId.HasValue && agentClientId.Value!=0)
                            sqlCmd.Parameters.Add(GlobalSqlParamNames.ClientId, SqlDbType.Int).Value = agentClientId.Value;
                        else
                            sqlCmd.Parameters.Add(GlobalSqlParamNames.ClientId, SqlDbType.Int).Value = DBNull.Value;

                        if (agentHoldingId.HasValue && agentHoldingId.Value != 0)
                            sqlCmd.Parameters.Add(GlobalSqlParamNames.HoldingId, SqlDbType.Int).Value = agentHoldingId.Value;
                        else
                            sqlCmd.Parameters.Add(GlobalSqlParamNames.HoldingId, SqlDbType.Int).Value = DBNull.Value;

                        object val = sqlCmd.ExecuteScalar();
                        return (decimal)val;
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }
    }
}
