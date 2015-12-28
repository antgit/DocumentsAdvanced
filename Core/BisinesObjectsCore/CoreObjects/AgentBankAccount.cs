using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>��������� ������� "��������� ����"</summary>
    internal struct AgentBankAccountStruct
    {
        /// <summary>������������� ��������������</summary>
        public int AgentId;
        /// <summary>������������� �����</summary>
        public int BankId;
        /// <summary>������������� ������</summary>
        public int CurrencyId;
    }
    /// <summary>��������� ����</summary>
    public sealed class AgentBankAccount : BaseCore<AgentBankAccount>, IEquatable<AgentBankAccount>,
        ICodes<AgentBankAccount>,
        IChains<AgentBankAccount>
    {
        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>��������� ����, ������������� �������� 1</summary>
        public const int KINDVALUE_ACCOUNT = 1;
        /// <summary>����������, ������������� �������� 2</summary>
        public const int KINDVALUE_DEPOSIT = 2;
        /// <summary>���������, ������������� �������� 3</summary>
        public const int KINDVALUE_CREDIT = 3;
        /// <summary>�����������������, ������������� �������� 4</summary>
        public const int KINDVALUE_DISTRIBUTIVE = 4;
        /// <summary>���������, ������������� �������� 5</summary>
        public const int KINDVALUE_BUDGET = 5;

        /// <summary>��������� ����, ������������� �������� 1572865</summary>
        public const int KINDID_ACCOUNT = 1572865;
        /// <summary>����������, ������������� �������� 1572866</summary>
        public const int KINDID_DEPOSIT = 1572866;
        /// <summary>���������, ������������� �������� 1572867</summary>
        public const int KINDID_CREDIT = 1572867;
        /// <summary>�����������������, ������������� �������� 1572868</summary>
        public const int KINDID_DISTRIBUTIVE = 1572868;
        /// <summary>���������, ������������� �������� 1572869</summary>
        public const int KINDID_BUDGET = 1572869;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<AgentBankAccount>.Equals(AgentBankAccount other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>�����������</summary>
        public AgentBankAccount()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.BankAccount;
        }
        protected override void CopyValue(AgentBankAccount template)
        {
            base.CopyValue(template);
            AgentId = template.AgentId;
            BankId = template.BankId;
            CurrencyId = template.CurrencyId;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit"></param>
        /// <returns></returns>
        protected override AgentBankAccount Clone(bool endInit)
        {
            AgentBankAccount obj = base.Clone(false);
            obj.AgentId = AgentId;
            obj.BankId = BankId;
            if(endInit)
                OnEndInit();
            return obj;
        }

        #region ��������
        private int _agentId;
        /// <summary>
        /// ������������� ��������������
        /// </summary>
        public int AgentId
        {
            get { return _agentId; }
            set
            {
                if (value == _agentId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentId);
                _agentId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentId);
            }
        }
        private Agent _agent;
        /// <summary>
        /// �������������
        /// </summary>
        public Agent Agent
        {
            get
            {
                if (_agentId == 0)
                    return null;
                if (_agent == null)
                    _agent = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId);
                else if (_agent.Id != _agentId)
                    _agent = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId);
                return _agent;
            }
            set
            {
                if (_agent == value) return;
                OnPropertyChanging(GlobalPropertyNames.Agent);
                _agent = value;
                _agentId = _agent == null ? 0 : _agent.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent);
            }
        } 
        private int _bankId;
        /// <summary>
        /// ������������� �����
        /// </summary>
        public int BankId
        {
            get { return _bankId; }
            set
            {
                if (value == _bankId) return;
                OnPropertyChanging(GlobalPropertyNames.BankId);
                _bankId = value;
                OnPropertyChanged(GlobalPropertyNames.BankId);
            }
        }

        private Agent _bank;
        /// <summary>
        /// ����
        /// </summary>
        public Agent Bank
        {
            get
            {
                if (_bankId == 0)
                    return null;
                if (_bank == null)
                    _bank = Workarea.Cashe.GetCasheData<Agent>().Item(_bankId);
                else if (_bank.Id != _bankId)
                    _bank = Workarea.Cashe.GetCasheData<Agent>().Item(_bankId);
                return _bank;
            }
            set
            {
                if (_bank == value) return;
                OnPropertyChanging(GlobalPropertyNames.Bank);
                _bank = value;
                _bankId = _bank == null ? 0 : _bank.Id;
                OnPropertyChanged(GlobalPropertyNames.Bank);
            }
        }

        private int _currencyId;
        /// <summary>������������� ������</summary>
        public int CurrencyId
        {
            get { return _currencyId; }
            set
            {
                if (value == _currencyId) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyId);
                _currencyId = value;
                OnPropertyChanged(GlobalPropertyNames.CurrencyId);
            }
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
        
        
        #endregion

        #region ������������
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_agentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentId, XmlConvert.ToString(_agentId));
            if (_bankId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.BankId, XmlConvert.ToString(_bankId));
            if (_currencyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CurrencyId, XmlConvert.ToString(_currencyId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.AgentId) != null)
                _agentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentId));
            if (reader.GetAttribute(GlobalPropertyNames.BankId) != null)
                _bankId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.BankId));
            if (reader.GetAttribute(GlobalPropertyNames.CurrencyId) != null)
                _currencyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CurrencyId));
        }
        #endregion
        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();
            if (_agentId == 0)
                throw new ValidateException("�� ������ ������������");
            if(_bankId==0)
                throw new ValidateException("�� ������ ����");
        }
	
        #region ���������
        AgentBankAccountStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new AgentBankAccountStruct {AgentId = _agentId, BankId = _bankId, CurrencyId=_currencyId};
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            AgentId = _baseStruct.AgentId;
            BankId = _baseStruct.BankId;
            CurrencyId = _baseStruct.CurrencyId;
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
                _agentId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _bankId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _currencyId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
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
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.AgentId, SqlDbType.Int) { IsNullable = true };
            if (_agentId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _agentId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.BankId, SqlDbType.Int) {IsNullable = true};
            if (_bankId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _bankId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CurrencyId, SqlDbType.Int) { IsNullable = false, Value = _currencyId };
            sqlCmd.Parameters.Add(prm);

        }
        #endregion

        #region ILinks<AgentBankAccount> Members
        /// <summary>����� ��������� ������</summary>
        /// <returns></returns>
        public List<IChain<AgentBankAccount>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>����� ���������</summary>
        /// <param name="kind">��� �����</param>
        /// <returns></returns>
        public List<IChain<AgentBankAccount>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<AgentBankAccount> IChains<AgentBankAccount>.SourceList(int chainKindId)
        {
            return Chain<AgentBankAccount>.GetChainSourceList(this, chainKindId);
            
        }
        List<AgentBankAccount> IChains<AgentBankAccount>.DestinationList(int chainKindId)
        {
            return Chain<AgentBankAccount>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<AgentBankAccount>> GetValues(bool allKinds)
        {
            return CodeHelper<AgentBankAccount>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<AgentBankAccount>.GetView(this, true);
        }
        #endregion
    }
}