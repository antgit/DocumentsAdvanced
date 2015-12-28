using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Бухгалтерский счет"</summary>
    internal struct AccountStruct
    {
        /// <summary>Сворачивать субсчета в оборотно-сальдовой ведомости</summary>
        public bool Turn;
        /// <summary>Тип свертки</summary>
        public int TurnKind;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>
    /// Бухгалтерский счет
    /// </summary>
    /// <remarks>
    /// Свойство <see cref="P:BusinessObjects.BaseCore`1.KindValue">KindId</see>
    /// имеет следующие значения: 
    /// <para> </para>
    /// <para> </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Значение</term>
    /// <description>Описание</description></listheader>
    /// <item>
    /// <term>1</term>
    /// <description>Шаблон</description></item>
    /// <item>
    /// <term>2</term>
    /// <description>Активный счет</description></item>
    /// <item>
    /// <term>4</term>
    /// <description>Пассивный счет</description></item>
    /// <item>
    /// <term>6</term>
    /// <description>Пассивно-активный счет</description></item></list>Субсчетами
    /// бухгалтерского счета являются бухгалтерские счета, имеющие связь
    /// &quot;Субсчета&quot; с текущим счетом. Связь &quot;Субсчета&quot; соответствует
    /// связи по умолчанию. Получить коллекцию субсчетов можно используя метод <see
    /// cref="M:BusinessObjects.Account.GetLinks">GetLinks</see>
    /// </remarks>
    /// <example>
    /// <code lang="C#">using BusinessObjects;
    /// namespace Examples
    /// {
    ///     class ExaplesAccount
    ///     {
    ///         void Samles()
    ///         {
    ///             string ConnectionString = &quot;ConnectionString&quot;;
    ///             Workarea wa = new Workarea();
    ///             wa.ConnectionString = ConnectionString;
    ///             if(wa.LogOn(&quot;Admin&quot;))
    ///             {
    ///                 Account acc = wa.GetObject&lt;Account&gt;(235);
    ///             }
    ///         }
    ///         void SampleCollection()
    ///         {
    ///             string ConnectionString = &quot;ConnectionString&quot;;
    ///             Workarea wa = new Workarea();
    ///             wa.ConnectionString = ConnectionString;
    ///             if (wa.LogOn(&quot;Admin&quot;))
    ///             {
    ///                 List&lt;Account&gt; coll = wa.GetCollection&lt;Account&gt;();
    ///             }
    ///         }
    ///         void SampleDelete()
    ///         {
    ///             string ConnectionString = &quot;ConnectionString&quot;;
    ///             Workarea wa = new Workarea();
    ///             wa.ConnectionString = ConnectionString;
    ///             if (wa.LogOn(&quot;Admin&quot;))
    ///             {
    ///                 Account acc = wa.GetObject&lt;Account&gt;(235);
    ///                 wa.Delete&lt;Account&gt;(acc);
    ///             }
    ///         }
    ///         void SampleTemplates()
    ///         {
    ///             string ConnectionString = &quot;ConnectionString&quot;;
    ///             Workarea wa = new Workarea();
    ///             wa.ConnectionString = ConnectionString;
    ///             if (wa.LogOn(&quot;Admin&quot;))
    ///             {
    ///                 List&lt;Account&gt; coll = wa.GetTemplates&lt;Account&gt;();
    ///             }
    ///         }
    ///     }
    /// }</code>
    /// </example>
    public sealed class Account : BaseCore<Account>, IChains<Account>, IReportChainSupport, IEquatable<Account>,
        ICodes<Account>, ICompanyOwner
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Счет активный, соответствует значению 1</summary>
        public const int KINDVALUE_ACTIVE = 1;
        /// <summary>Счет пассивный, соответствует значению 2</summary>
        public const int KINDVALUE_PASSIVE = 2;
        /// <summary>Счет пассивно-активный, соответствует значению 3</summary>
        public const int KINDVALUE_PASSIVEACTIVE = 3;
        /// <summary>Счет забалансовый, соответствует значению 4</summary>
        public const int KINDVALUE_OFFBALANCE = 4;

        /// <summary>Счет активный, соответствует значению 131073</summary>
        public const int KINDID_ACTIVE = 131073;
        /// <summary>Счет пассивный, соответствует значению 131074</summary>
        public const int KINDID_PASSIVE = 131074;
        /// <summary>Счет пассивно-активный, соответствует значению 131075</summary>
        public const int KINDID_PASSIVEACTIVE = 131075;
        /// <summary>Счет забалансовый, соответствует значению 131076</summary>
        public const int KINDID_OFFBALANCE = 131076;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Account>.Equals(Account other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// Инициалиpируется только свойство <see
        /// cref="BusinessObjects.BaseCoreObject.EntityId">EntityId</see>. Значение данного
        /// свойства равно 2
        /// </remarks>
        public Account(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Account;
        }
        protected override void CopyValue(Account template)
        {
            base.CopyValue(template);
            Turn = template.Turn;
            TurnKind = template.TurnKind;
            MyCompanyId = template.MyCompanyId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit"></param>
        /// <returns></returns>
        protected override Account Clone(bool endInit)
        {
            Account obj = base.Clone(false);
            obj.Turn = Turn;
            obj.TurnKind = TurnKind;
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        /// <summary>
        /// Сравнение объекта для службы обмена данными
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange(Account value)
        {
            if (!base.CompareExchange(value))
            {
                return false;
            }

            if (value.Turn != Turn)
                return false;
            if (value.TurnKind != TurnKind)
                return false;

            return true;
        }

        #region Свойства
        private bool _turn;
        /// <summary>
        /// Сворачивать субсчета в оборотно-сальдовой ведомости
        /// </summary>
        /// <remarks>
        /// Субсчетами бухгалтерского счета являются бухгалтерские счета, имеющие связь
        /// &quot;Субсчета&quot; с текущим счетом. Связь &quot;Субсчета&quot; соответствует
        /// связи по умолчанию. Получить коллекцию субсчетов можно используя метод <see
        /// cref="M:BusinessObjects.Account.GetLinks">GetLinks</see>
        /// </remarks>
        public bool Turn
        {
            get { return _turn; }
            set 
            {
                if (value == _turn) return;
                OnPropertyChanging(GlobalPropertyNames.Turn);
                _turn = value;
                OnPropertyChanged(GlobalPropertyNames.Turn);
            }
        }

        private int _turnKind;
        /// <summary>Тип свертки</summary>
        /// <remarks> 0 - свернутое
        /// 1 - развернутое
        /// 2 - свернутое по корреспондентам
        /// 3 - свернутое по объектам
        /// </remarks>
        public int TurnKind
        {
            get { return _turnKind; }
            set 
            {
                if (value == _turnKind) return;
                OnPropertyChanging(GlobalPropertyNames.TurnKind);
                _turnKind = value;
                OnPropertyChanged(GlobalPropertyNames.TurnKind);
            }
        }

        private int _myCompanyId;
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит объект
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
        /// Моя компания, предприятие которому принадлежит объект
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

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_turnKind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TurnKind, XmlConvert.ToString(_turnKind));
            if (_turn)
                writer.WriteAttributeString(GlobalPropertyNames.Turn, XmlConvert.ToString(_turn));
            if (_myCompanyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.TurnKind) != null)
                _turnKind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TurnKind));
            if (reader.GetAttribute(GlobalPropertyNames.Turn) != null)
                _turn = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Turn));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
        }
        #endregion

        #region Состояние
        AccountStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new AccountStruct {Turn = _turn, TurnKind = _turnKind, MyCompanyId = _myCompanyId};
                return true;
            }
            return false;
        }
        /// <summary>
        /// Востановить состояние
        /// </summary>
        public override void RestoreState()
        {
            base.RestoreState();
            Turn = _baseStruct.Turn;
            TurnKind = _baseStruct.TurnKind;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion

        #region База данных
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="reader">Объект <see cref="SqlDataReader"/> чтения данных</param>
        /// <param name="endInit">Закончить инициализацию</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _turn = reader.GetSqlBoolean(17).Value;
                _turnKind = reader.GetSqlInt32(18).Value;
                _myCompanyId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>
        /// Установить значения параметров для комманды создания
        /// </summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Turn, SqlDbType.Bit) {IsNullable = false, Value = _turn};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TurnKind, SqlDbType.Int) {IsNullable = false, Value = _turnKind};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.NVarChar) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Account> Members
        /// <summary>
        /// Связи бухгалтерского счета
        /// </summary>
        /// <returns></returns>
        public List<IChain<Account>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи бухгалтерского счета
        /// </summary>
        /// <param name="kind">Тип связей</param>
        /// <returns></returns>
        public List<IChain<Account>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Account> IChains<Account>.SourceList(int chainKindId)
        {
            return Chain<Account>.GetChainSourceList(this, chainKindId);
        }
        List<Account> IChains<Account>.DestinationList(int chainKindId)
        {
            return Chain<Account>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Account>> GetValues(bool allKinds)
        {
            return CodeHelper<Account>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Account>.GetView(this, true);
        }
        #endregion

        public List<Account> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Account> filter = null,
            bool? turn=null, int? turnKind=null,int? myCompanyId=null,
            bool useAndFilter = false)
        {
            Account item = new Account { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Account> collection = new List<Account>();
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
                        if (turn.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.Turn, SqlDbType.Int).Value = turn.Value;
                        if (turnKind.HasValue && turnKind != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TurnKind, SqlDbType.Int).Value = turnKind.Value;
                        if (myCompanyId.HasValue && myCompanyId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = myCompanyId.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Account { Workarea = Workarea };
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
