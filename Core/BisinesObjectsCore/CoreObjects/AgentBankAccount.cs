using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Расчетный счет"</summary>
    internal struct AgentBankAccountStruct
    {
        /// <summary>Идентификатор корреспондента</summary>
        public int AgentId;
        /// <summary>Идентификатор банка</summary>
        public int BankId;
        /// <summary>Идентификатор валюты</summary>
        public int CurrencyId;
    }
    /// <summary>Расчетный счет</summary>
    public sealed class AgentBankAccount : BaseCore<AgentBankAccount>, IEquatable<AgentBankAccount>,
        ICodes<AgentBankAccount>,
        IChains<AgentBankAccount>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Расчетный счет, соответствует значению 1</summary>
        public const int KINDVALUE_ACCOUNT = 1;
        /// <summary>Депозитный, соответствует значению 2</summary>
        public const int KINDVALUE_DEPOSIT = 2;
        /// <summary>Кредитный, соответствует значению 3</summary>
        public const int KINDVALUE_CREDIT = 3;
        /// <summary>Распределительный, соответствует значению 4</summary>
        public const int KINDVALUE_DISTRIBUTIVE = 4;
        /// <summary>Бюджетный, соответствует значению 5</summary>
        public const int KINDVALUE_BUDGET = 5;

        /// <summary>Расчетный счет, соответствует значению 1572865</summary>
        public const int KINDID_ACCOUNT = 1572865;
        /// <summary>Депозитный, соответствует значению 1572866</summary>
        public const int KINDID_DEPOSIT = 1572866;
        /// <summary>Кредитный, соответствует значению 1572867</summary>
        public const int KINDID_CREDIT = 1572867;
        /// <summary>Распределительный, соответствует значению 1572868</summary>
        public const int KINDID_DISTRIBUTIVE = 1572868;
        /// <summary>Бюджетный, соответствует значению 1572869</summary>
        public const int KINDID_BUDGET = 1572869;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<AgentBankAccount>.Equals(AgentBankAccount other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
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
        /// <summary>Клонирование объекта</summary>
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

        #region Свойства
        private int _agentId;
        /// <summary>
        /// Идентификатор корреспондента
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
        /// Корреспондент
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
        /// Идентификатор банка
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
        /// Банк
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
        /// <summary>Идентификатор валюты</summary>
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
        /// Валюта
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

        #region Сериализация
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
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if (_agentId == 0)
                throw new ValidateException("Не указан кореспондент");
            if(_bankId==0)
                throw new ValidateException("Не указан банк");
        }
	
        #region Состояние
        AgentBankAccountStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
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

        #region База данных
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="reader">Объект <see cref="SqlDataReader"/> чтения данных</param>
        /// <param name="endInit">Закончить инициализацию</param>
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
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
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
        /// <summary>Связи расчетных счетов</summary>
        /// <returns></returns>
        public List<IChain<AgentBankAccount>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
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