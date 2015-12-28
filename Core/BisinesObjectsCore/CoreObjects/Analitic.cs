using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Внутренняя структура объекта "Аналитика"</summary>
    internal struct AnaliticStruct
    {
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>Аналитика</summary>
    public sealed class Analitic : BaseCore<Analitic>, IChains<Analitic>, IReportChainSupport, IEquatable<Analitic>,
        IComparable, IComparable<Analitic>,
        IFacts<Analitic>,
        IChainsAdvancedList<Analitic, Knowledge>,
        IChainsAdvancedList<Analitic, Note>,
        ICodes<Analitic>, IHierarchySupport, ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>Аналитика, соответствует значению 1</summary>
        public const int KINDVALUE_ANALITIC = 1;
        /// <summary>Товарная группа, соответствует значению 2</summary>
        public const int KINDVALUE_TRADEGROUP = 2;
        /// <summary>Бренд, соответствует значению 3</summary>
        public const int KINDVALUE_BRAND = 3;
        /// <summary>Денежные средства, соответствует значению 4</summary>
        public const int KINDVALUE_MONEY = 4;
        /// <summary>Декларация НДС, соответствует значению 5</summary>
        public const int KINDVALUE_NDS = 5;
        /// <summary>Декларация "Прибыль", соответствует значению 6</summary>
        public const int KINDVALUE_PROFIT = 6;
        /// <summary>Канал продаж, соответствует значению 7</summary>
        public const int KINDVALUE_SALESCHANNEL = 7;
        /// <summary>Причина возврата, соответствует значению 8</summary>
        public const int KINDVALUE_RETURNREASON = 8;
        /// <summary>ЦФО, соответствует значению 9</summary>
        public const int KINDVALUE_CFO = 9;
        /// <summary>Налоги, соответствует значению 10</summary>
        public const int KINDVALUE_TAXES = 10;
        /// <summary>Форма расчетов, соответствует значению 11</summary>
        public const int KINDVALUE_PAYMENTMETHOD = 11;
        /// <summary>Условия поставки, соответствует значению 12</summary>
        public const int KINDVALUE_DELIVERY = 12;
        /// <summary>Вид образования, соответствует значению 13</summary>
        public const int KINDVALUE_EDUCATION = 13;
        /// <summary>Графики работ, соответствует значению 14</summary>
        public const int KINDVALUE_WORKS = 14;
        /// <summary>Должность, соответствует значению 15</summary>
        public const int KINDVALUE_POST = 15;
        /// <summary>Семейное положение, соответствует значению 16</summary>
        public const int KINDVALUE_MARITALSTATUS = 16;
        /// <summary>Событие, соответствует значению 17</summary>
        public const int KINDVALUE_EVENT = 17;
        /// <summary>Важность, соответствует значению 18</summary>
        public const int KINDVALUE_IMPORTANCE = 18;
        /// <summary>Состояние договора, соответствует значению 19</summary>
        public const int KINDVALUE_CONTRACTSTATE = 19;
        /// <summary>Тип договора, соответствует значению 20</summary>
        public const int KINDVALUE_CONTRACTTYPE = 20;
        /// <summary>Тип корреспонденции, соответствует значению 21</summary>
        public const int KINDVALUE_CORRESPONDENCETYPE = 21;
        /// <summary>Сервер отчетов, соответствует значению 22</summary>
        public const int KINDVALUE_REPORTSERVER = 22;
        /// <summary>Вид упаковки, соответствует значению 23</summary>
        public const int KINDVALUE_PACKAGINGTYPE = 23;
        /// <summary>Вид продукции, соответствует значению 24</summary>
        public const int KINDVALUE_PRODUCTTYPE = 24;
        /// <summary>Категория торговой точки, соответствует значению 25</summary>
        public const int KINDVALUE_OUTLETCATEGORY = 25;
        /// <summary>Метраж, соответствует значению 26</summary>
        public const int KINDVALUE_FOOTAGE = 26;
        /// <summary>Отрасль, соответствует значению 27</summary>
        public const int KINDVALUE_BRANCH = 27;
        /// <summary>Тип торговой точки, соответствует значению 28</summary>
        public const int KINDVALUE_OUTLETTYPE = 28;
        /// <summary>Форма собственности, соответствует значению 29</summary>
        public const int KINDVALUE_OWNERSHIP = 29;
        /// <summary>Резолюция подписи, соответствует значению 30</summary>
        public const int KINDVALUE_SIGNRESOLUTION = 30;
        /// <summary>Тип оборудования, соответствует значению 31</summary>
        public const int KINDVALUE_TYPEEQUIPMENT = 31;
        /// <summary>Расположение торговой точки, соответствует значению 32</summary>
        public const int KINDVALUE_OUTLETLOCATION = 32;
        /// <summary>Состояние отношений с торговой точкой, соответствует значению 33</summary>
        public const int KINDVALUE_REASONTRADEPOINT = 33;
        /// <summary>Размещение оборудования, соответствует значению 34</summary>
        public const int KINDVALUE_EQUIPMENTPLACEMENT = 34;
        /// <summary>Состояние задачи, соответствует значению 34</summary>
        public const int KINDVALUE_TASKSTATE = 35;
        /// <summary>Вопрос, соответствует значению 36</summary>
        public const int KINDVALUE_QUESTION = 36;
        /// <summary>Цвет, соответствует значению 37</summary>
        public const int KINDVALUE_COLOR = 37;
        /// <summary>Тип рекурсии, соответствует значению 38</summary>
        public const int KINDVALUE_RECURCIVE = 38;
        /// <summary>Результат планирования, соответствует значению 39</summary>
        public const int KINDVALUE_FINPLANRESULT = 39;
        /// <summary>Типы финансового планирования, соответствует значению 40</summary>
        public const int KINDVALUE_FINPLANKIND = 40;
        /// <summary>Группа корреспондента, соответствует значению 41</summary>
        public const int KINDVALUE_AGENTGROUP = 41;

        /// <summary>Аналитика, соответствует значению 262145</summary>
        public const int KINDID_ANALITIC = 262145;
        /// <summary>Товарная группа, соответствует значению 262146</summary>
        public const int KINDID_TRADEGROUP = 262146;
        /// <summary>Бренд, соответствует значению 262147</summary>
        public const int KINDID_BRAND = 262147;
        /// <summary>Денежные средства, соответствует значению 262148</summary>
        public const int KINDID_MONEY = 262148;
        /// <summary>Декларация НДС, соответствует значению 262149</summary>
        public const int KINDID_NDS = 262149;
        /// <summary>Декларация "Прибыль", соответствует значению 262150</summary>
        public const int KINDID_PROFIT = 262150;
        /// <summary>Канал продаж, соответствует значению 262151</summary>
        public const int KINDID_SALESCHANNEL = 262151;
        /// <summary>Причина возврата, соответствует значению 262152</summary>
        public const int KINDID_RETURNREASON = 262152;
        /// <summary>ЦФО, соответствует значению 262153</summary>
        public const int KINDID_CFO = 262153;
        /// <summary>Налоги, соответствует значению 262154</summary>
        public const int KINDID_TAXES = 262154;
        /// <summary>Форма расчетов, соответствует значению 262155</summary>
        public const int KINDID_PAYMENTMETHOD = 262155;
        /// <summary>Условия поставки, соответствует значению 262156</summary>
        public const int KINDID_DELIVERY = 262156;
        /// <summary>Вид образования, соответствует значению 262157</summary>
        public const int KINDID_EDUCATION = 262157;
        /// <summary>Графики работ, соответствует значению 262158</summary>
        public const int KINDID_WORKS = 262158;
        /// <summary>Должность, соответствует значению 262159</summary>
        public const int KINDID_POST = 262159;
        /// <summary>Семейное положение, соответствует значению 262160</summary>
        public const int KINDID_MARITALSTATUS = 262160;
        /// <summary>Событие, соответствует значению 262161</summary>
        public const int KINDID_EVENT = 262161;
        /// <summary>Важность, соответствует значению 262162</summary>
        public const int KINDID_IMPORTANCE = 262162;
        /// <summary>Состояние договора, соответствует значению 262163</summary>
        public const int KINDID_CONTRACTSTATE = 262163;
        /// <summary>Тип договора, соответствует значению 262164</summary>
        public const int KINDID_CONTRACTTYPE = 262164;
        /// <summary>Тип корреспонденции, соответствует значению 262165</summary>
        public const int KINDID_CORRESPONDENCETYPE = 262165;
        /// <summary>Сервер отчетов, соответствует значению 262166</summary>
        public const int KINDID_REPORTSERVER = 262166;
        /// <summary>Вид упаковки, соответствует значению 262167</summary>
        public const int KINDID_PACKAGINGTYPE = 262167;
        /// <summary>Вид продукции, соответствует значению 262168</summary>
        public const int KINDID_PRODUCTTYPE = 262168;
        /// <summary>Категория торговой точки, соответствует значению 262169</summary>
        public const int KINDID_OUTLETCATEGORY = 262169;
        /// <summary>Метраж, соответствует значению 262170</summary>
        public const int KINDID_FOOTAGE = 262170;
        /// <summary>Отрасль, соответствует значению 262171</summary>
        public const int KINDID_BRANCH = 262171;
        /// <summary>Тип торговой точки, соответствует значению 262172</summary>
        public const int KINDID_OUTLETTYPE = 262172;
        /// <summary>Форма собственности, соответствует значению 262173</summary>
        public const int KINDID_OWNERSHIP = 262173;
        /// <summary>Резолюция подписи, соответствует значению 262174</summary>
        public const int KINDID_SIGNRESOLUTION = 262174;
        /// <summary>Тип оборудования, соответствует значению 262175</summary>
        public const int KINDID_TYPEEQUIPMENT = 262175;
        /// <summary>Расположение торговой точки, соответствует значению 262176</summary>
        public const int KINDID_OUTLETLOCATION = 262176;
        /// <summary>Состояние отношений с торговой точкой, соответствует значению 262177</summary>
        public const int KINDID_REASONTRADEPOINT = 262177;
        /// <summary>Размещение оборудования, соответствует значению 262178</summary>
        public const int KINDID_EQUIPMENTPLACEMENT = 262178;
        /// <summary>Состояние задачи, соответствует значению 262179</summary>
        public const int KINDID_TASKSTATE = 262179;
        /// <summary>Вопрос, соответствует значению 262180</summary>
        public const int KINDID_QUESTION = 262180;
        /// <summary>Цвет, соответствует значению 262181</summary>
        public const int KINDID_COLOR = 262181;
        /// <summary>Тип рекурсии, соответствует значению 262182</summary>
        public const int KINDID_RECURCIVE = 262182;
        /// <summary>Результат планирования, соответствует значению 262183</summary>
        public const int KINDID_FINPLANRESULT = 262183;
        /// <summary>Типы финансового планирования, соответствует значению 262184</summary>
        public const int KINDID_FINPLANKIND = 262184;
        /// <summary>Группа корреспондента, соответствует значению 262185</summary>
        public const int KINDID_AGENTGROUP = 262185;

        /// <summary>Резолюция подписи "Не установлено"</summary>
        public const string SYSTEM_SIGN_EMPTY = "SYSTEM_SIGN_EMPTY";
        /// <summary>Резолюция подписи "Отклоняю"</summary>
        public const string SYSTEM_SIGN_NO = "SYSTEM_SIGN_NO";
        /// <summary>Резолюция подписи "Согласен"</summary>
        public const string SYSTEM_SIGN_YES = "SYSTEM_SIGN_YES";

        /// <summary>Важность "Нормальная"</summary>
        public const string SYSTEM_PRIORITY_NORMAL = "SYSTEM_PRIORITY_NORMAL";

        /// <summary>Код корневой иерархии для хранения формы расчетов</summary>
        public const string SYSTEM_ANALITIC_TAXDOCPAYMENTMETHOD = Hierarchy.SYSTEM_ANALITIC_TAXDOCPAYMENTMETHOD;
        /// <summary>Код корневой иерархии для хранения брендов</summary>
        public const string SYSTEM_ANALITIC_BRANDS = Hierarchy.SYSTEM_ANALITIC_BRANDS;
        /// <summary>Код корневой иерархии для хранения товарных групп</summary>
        public const string SYSTEM_ANALITIC_TRADEGROUP = Hierarchy.SYSTEM_ANALITIC_TRADEGROUP;
        /// <summary>Код корневой иерархии для хранения аналитики "Вид продукции"</summary>
        public const string SYSTEM_ANALITIC_PRODUCTTYPE = Hierarchy.SYSTEM_ANALITIC_PRODUCTTYPE;
        /// <summary>Код корневой иерархии для хранения аналитики "Вид упаковки"</summary>
        public const string SYSTEM_ANALITIC_PACKTYPE = Hierarchy.SYSTEM_ANALITIC_PACKTYPE;
        /// <summary>Код корневой иерархии для хранения аналитики "Причина возврата"</summary>
        public const string SYSTEM_ANALITIC_RETURNREASON = Hierarchy.SYSTEM_ANALITIC_RETURNREASON;
        /// <summary>Код корневой иерархии для хранения аналитики "Расположение торговой точки"</summary>
        public const string SYSTEM_ANALITIC_OUTLETLOCATION = Hierarchy.SYSTEM_ANALITIC_OUTLETLOCATION;
        /// <summary>Код корневой иерархии для хранения аналитики "Тип холодильного оборудования"</summary>
        public const string SYSTEM_ANALITIC_TYPEEQUIPMENTHOLOD = Hierarchy.SYSTEM_ANALITIC_TYPEEQUIPMENTHOLOD;
        /// <summary>Код корневой иерархии для хранения аналитики "Тип оборудования"</summary>
        public const string SYSTEM_ANALITIC_TYPEEQUIPMENT = Hierarchy.SYSTEM_ANALITIC_TYPEEQUIPMENT;
        /// <summary>Код корневой иерархии для хранения аналитики "Цвет"</summary>
        public const string SYSTEM_ANALITIC_COLOR = Hierarchy.SYSTEM_ANALITIC_COLOR;
        /// <summary>Код корневой иерархии для хранения аналитики "События"</summary>
        public const string SYSTEM_ANALITIC_EVENT = Hierarchy.SYSTEM_ANALITIC_EVENT;
        /// <summary>Код корневой иерархии для хранения аналитики "Тип рекурсии"</summary>
        public const string SYSTEM_ANALITITIC_RECURCIVE = Hierarchy.SYSTEM_ANALITITIC_RECURCIVE;

        /// <summary>Код корневой иерархии для хранения аналитики "Денежные средства"</summary>
        public const string SYSTEM_ANALITIC_PAYMENTTYPE = Hierarchy.SYSTEM_ANALITIC_PAYMENTTYPE;
        /// <summary>Код корневой иерархии для хранения аналитики "Денежные средства" расход</summary>
        public const string SYSTEM_ANALITIC_PAYMENTTYPEOUT = Hierarchy.SYSTEM_ANALITIC_PAYMENTTYPEOUT;
        /// <summary>Код корневой иерархии для хранения аналитики "Денежные средства" приход</summary>
        public const string SYSTEM_ANALITIC_PAYMENTTYPEIN = Hierarchy.SYSTEM_ANALITIC_PAYMENTTYPEIN;

        /// <summary>Код корневой иерархии для хранения аналитики "Категория торговой точки"</summary>
        public const string SYSTEM_ANALITIC_AGENTCATEGORY = Hierarchy.SYSTEM_ANALITIC_AGENTCATEGORY;
        /// <summary>Код корневой иерархии для хранения аналитики "Метраж"</summary>
        public const string SYSTEM_ANALITIC_AGENTMETRICAREA = Hierarchy.SYSTEM_ANALITIC_AGENTMETRICAREA;
        /// <summary>Код корневой иерархии для хранения аналитики "Огранизационно провавая форма хозяйствования"</summary>
        public const string SYSTEM_ANALITIC_AGENTOWNERSHIP = Hierarchy.SYSTEM_ANALITIC_AGENTOWNERSHIP;
        /// <summary>Код корневой иерархии для хранения аналитики "Отрасль"</summary>
        public const string SYSTEM_ANALITIC_AGENTINDUSTRY = Hierarchy.SYSTEM_ANALITIC_AGENTINDUSTRY;
        /// <summary>Код корневой иерархии для хранения аналитики "Тип торговой точки"</summary>
        public const string SYSTEM_ANALITIC_AGENTTYPEOUTLET = Hierarchy.SYSTEM_ANALITIC_AGENTTYPEOUTLET;
        /// <summary>Код корневой иерархии для хранения аналитики "Тип торговой точки"</summary>
        public const string SYSTEM_ANALITICCFO = Hierarchy.SYSTEM_ANALITICCFO;
        /// <summary>Код корневой иерархии "Должность"</summary>
        public const string SYSTEM_WORKPOST = Hierarchy.SYSTEM_WORKPOST;
        /// <summary>Код корневой иерархии "Категория сотрудника"</summary>
        public const string SYSTEM_ANALITIC_WORKERCATEGORY = Hierarchy.SYSTEM_ANALITIC_WORKERCATEGORY;
        /// <summary>Код корневой иерархии "Состояние задач"</summary>
        public const string SYSTEM_ANALITIC_TASKSTATE = Hierarchy.SYSTEM_ANALITIC_TASKSTATE;
        /// <summary>Код корневой иерархии "Место хранения трудовой"</summary>
        public const string SYSTEM_ANALITIC_EMPLOYMENTBOOK = Hierarchy.SYSTEM_ANALITIC_EMPLOYMENTBOOK;
        /// <summary>Код корневой иерархии "Вид несовершеннолетия"</summary>
        public const string SYSTEM_ANALITIC_MINORS = Hierarchy.SYSTEM_ANALITIC_MINORS;
        /// <summary>Код корневой иерархии "Группа подписания"</summary>
        public const string SYSTEM_ANALITIC_SIGNLEVEL = Hierarchy.SYSTEM_ANALITIC_SIGNLEVEL;

        /// <summary>Код корневой иерархии "Статус маршрута"</summary>
        public const string SYSTEM_ANALITIC_ROUTESATUS = Hierarchy.SYSTEM_ANALITIC_ROUTESATUS;
        /// <summary>Код корневой иерархии "Фактический статус маршрута"</summary>
        public const string SYSTEM_ANALITIC_ROUTEFACT = Hierarchy.SYSTEM_ANALITIC_ROUTEFACT;
        // ReSharper restore InconsistentNaming
        
        #endregion
        bool IEquatable<Analitic>.Equals(Analitic other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Analitic otherObj = (Analitic)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(Analitic other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>Конструктор</summary>
        public Analitic(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Analitic;
        }
        protected override void CopyValue(Analitic template)
        {
            base.CopyValue(template);
            MyCompanyId = template.MyCompanyId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Analitic Clone(bool endInit)
        {
            Analitic obj = base.Clone(false);
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства
        private int _myCompanyId;
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит аналитика
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
        /// Моя компания, предприятие которому принадлежит аналитика
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

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion

        #region Состояние
        AnaliticStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new AnaliticStruct
                {
                    MyCompanyId = _myCompanyId
                };
                return true;
            }
            return false;
        }

        /// <summary>Востановить текущее состояние объекта</summary>
        /// <remarks>Востановление состояние возможно только после выполнения сосхранения состояния</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
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
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _myCompanyId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
            }
            catch (Exception ex)
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
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);

        }
        #endregion

        #region ILinks<Analitic> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<Analitic>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Analitic>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Analitic> IChains<Analitic>.SourceList(int chainKindId)
        {
            return Chain<Analitic>.GetChainSourceList(this, chainKindId);
        }
        List<Analitic> IChains<Analitic>.DestinationList(int chainKindId)
        {
            return Chain<Analitic>.DestinationList(this, chainKindId);
            
        }
        #endregion

        #region IChainsAdvancedList<Agent,Knowledge> Members

        List<IChainAdvanced<Analitic, Knowledge>> IChainsAdvancedList<Analitic, Knowledge>.GetLinks()
        {
            return ((IChainsAdvancedList<Analitic, Knowledge>)this).GetLinks(59);
        }

        List<IChainAdvanced<Analitic, Knowledge>> IChainsAdvancedList<Analitic, Knowledge>.GetLinks(int? kind)
        {
            return GetLinkedKnowledges();
        }
        public List<IChainAdvanced<Analitic, Knowledge>> GetLinkedKnowledges()
        {
            List<IChainAdvanced<Analitic, Knowledge>> collection = new List<IChainAdvanced<Analitic, Knowledge>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Analitic>().Entity.FindMethod("LoadKnowledges").FullName;
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
                                ChainAdvanced<Analitic, Knowledge> item = new ChainAdvanced<Analitic, Knowledge> { Workarea = Workarea, Left = this };
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
        List<ChainValueView> IChainsAdvancedList<Analitic, Knowledge>.GetChainView()
        {
            return ChainValueView.GetView<Analitic, Knowledge>(this);
        }
        #endregion
        #region IChainsAdvancedList<Analitic,Note> Members

        List<IChainAdvanced<Analitic, Note>> IChainsAdvancedList<Analitic, Note>.GetLinks()
        {
            return ChainAdvanced<Analitic, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Analitic, Note>> IChainsAdvancedList<Analitic, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Analitic, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Analitic, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Analitic, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Analitic, Note>.GetChainView()
        {
            return ChainValueView.GetView<Analitic, Note>(this);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Analitic>> GetValues(bool allKinds)
        {
            return CodeHelper<Analitic>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Analitic>.GetView(this, true);
        }
        #endregion

        #region IFacts

        private List<FactView> _factView;
        public List<FactView> GetCollectionFactView()
        {
            return _factView ?? (_factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId));
        }

        public void RefreshFaсtView()
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
        /// Первая группа в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            return FirstHierarchy(false);
        }
        /// <summary>
        /// Первая группа в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy(bool refresh)
        {
            if (!refresh && (LastLoadPartial.HasValue && LastLoadPartial.Value.AddMinutes(Workarea.Cashe.DefaultPartalReloadTime) > DateTime.Now))
            {
                if (!_firstHierarchy.HasValue) return null;
                return Workarea.Cashe.GetCasheData<Hierarchy>().Item(_firstHierarchy.Value);
            }
            _firstHierarchy = Hierarchy.FirstHierarchy<Analitic>(this);
            LastLoadPartial = DateTime.Now;
            if (!_firstHierarchy.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(_firstHierarchy.Value);
        }
        /// <summary>
        /// Поиск объекта
        /// </summary>
        /// <param name="hierarchyId">Идентификатор иерархии в которой осуществлять поиск</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="flags">Флаг</param>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <param name="name">Наименование</param>
        /// <param name="kindId">Идентификатор типа</param>
        /// <param name="code">Признак</param>
        /// <param name="memo">Наименование</param>
        /// <param name="flagString">Пользовательский флаг</param>
        /// <param name="templateId">Идентификатор шаблона</param>
        /// <param name="count">Количество, по умолчанию 100</param>
        /// <param name="filter">Дополнительный фильтр</param>
        /// <param name="useAndFilter">Использовать фильтр И</param>
        /// <returns></returns>
        public List<Analitic> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Analitic> filter = null, bool useAndFilter = false)
        {
            Analitic item = new Analitic { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Analitic> collection = new List<Analitic>();
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
                            item = new Analitic { Workarea = Workarea };
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
