using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Интервал времени"</summary>
    internal struct DateRegionStruct
    {
        /// <summary>Дата начала</summary>
        public DateTime DateStart;
        /// <summary>Дата окончания</summary>
        public DateTime DateEnd;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>Интервал времени</summary>
    /// <remarks></remarks>
    public sealed class DateRegion : BaseCore<DateRegion>, IChains<DateRegion>, IReportChainSupport, IEquatable<DateRegion>,
                                     IComparable, IComparable<DateRegion>,
                                     IFacts<DateRegion>,
                                     ICodes<DateRegion>,
        ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>Произвольный интервал, соответствует значению 1</summary>
        public const int KINDVALUE_CUSTOM = 1;
        /// <summary>Месяц, соответствует значению 2</summary>
        public const int KINDVALUE_MONTH = 2;
        /// <summary>Квартал, соответствует значению 3</summary>
        public const int KINDVALUE_QUARTAL = 3;
        /// <summary>Полугодие, соответствует значению 4</summary>
        public const int KINDVALUE_HALFYEAR = 4;
        /// <summary>Год, соответствует значению 5</summary>
        public const int KINDVALUE_YEAR = 5;
        /// <summary>Неделя, соответствует значению 6</summary>
        public const int KINDVALUE_WEEK = 6;

        /// <summary>Произвольный интервал, соответствует значению 5505025</summary>
        public const int KINDID_CUSTOM = 5505025;
        /// <summary>Месяц, соответствует значению 5505026</summary>
        public const int KINDID_MONTH = 5505026;
        /// <summary>Квартал, соответствует значению 5505027</summary>
        public const int KINDID_QUARTAL = 5505027;
        /// <summary>Полугодие, соответствует значению 5505028</summary>
        public const int KINDID_HALFYEAR = 5505028;
        /// <summary>Год, соответствует значению 5505029</summary>
        public const int KINDID_YEAR = 5505029;
        /// <summary>Год, соответствует значению 5505030</summary>
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
        /// Сравнение двух интервалов по идентификатору
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            DateRegion otherObj = (DateRegion)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// Сравнение двух интервалов по идентификатору
        /// </summary>
        /// <param name="other">Объект интервала времени</param>
        /// <returns></returns>
        public int CompareTo(DateRegion other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>
        /// Сравнение объекта для службы обмена данными
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
        /// <summary>Конструктор</summary>
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
        #region Свойства

        private DateTime _dateStart;
        /// <summary>
        /// Дата начала
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
        /// Дата окончания
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
            writer.WriteAttributeString(GlobalPropertyNames.DateStart, XmlConvert.ToString(DateStart, XmlDateTimeSerializationMode.Local));
            writer.WriteAttributeString(GlobalPropertyNames.DateEnd, XmlConvert.ToString(DateEnd, XmlDateTimeSerializationMode.Local));
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
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
        #region Состояние
        DateRegionStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new DateRegionStruct { DateStart = _dateStart, DateEnd = _dateEnd, MyCompanyId = _myCompanyId};
                return true;
            }
            return false;
        }

        /// <summary>Востановить текущее состояние объекта</summary>
        /// <remarks>Востановление состояние возможно только после выполнения сосхранения состояния</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            DateStart = _baseStruct.DateStart;
            DateEnd = _baseStruct.DateEnd;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
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
                _dateStart = reader.GetDateTime(17);
                _dateEnd = reader.GetDateTime(18);
                _myCompanyId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
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
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<DateRegion>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
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
    }
}