using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Курс валюты"</summary>
    internal struct RateStruct
    {
        /// <summary>Идентификатор банка</summary>
        public int BankId;
        /// <summary>Идентификатор первой ваюты</summary>
        public int CurrencyFromId;
        /// <summary>Идентификатор второй валюты</summary>
        public int CurrencyToId;
        /// <summary>Дата</summary>
        public DateTime Date;
        /// <summary>Множитель</summary>
        public decimal Multiplier;
        /// <summary>Значение</summary>
        public decimal Value;
    }
    /// <summary>
    /// Курс валюты
    /// </summary>
    public sealed class Rate : BaseCore<Rate>, IEquatable<Rate>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Курс валюты, соответствует значению 1</summary>
        public const int KINDVALUE_RATE = 1;

        /// <summary>Курс валюты</summary>
        public const int KINDID_RATE = 851969;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Rate>.Equals(Rate other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public Rate():base()
        {
            EntityId = (short)WhellKnownDbEntity.Rate;
        }
        protected override void CopyValue( Rate template)
        {
            base.CopyValue(template);
            CurrencyFromId = template.CurrencyFromId;
            CurrencyToId = template.CurrencyToId;
            BankId = template.BankId;
            Multiplier = template.Multiplier;
            Value = template.Value;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Rate Clone(bool endInit)
        {
            Rate obj = base.Clone(false);
            obj.BankId = BankId;
            obj.CurrencyFromId = CurrencyFromId;
            obj.CurrencyToId = CurrencyToId;
            obj.Date = Date;
            obj.Multiplier = Multiplier;
            obj.Date = Date;
            obj.Value = Value;

            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства
        private DateTime _date;
        /// <summary>
        /// Дата
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


        private int _currencyFromId;
        /// <summary>
        /// Идентификатор первой ваюты
        /// </summary>
        public int CurrencyFromId
        {
            get { return _currencyFromId; }
            set
            {
                if (value == _currencyFromId) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyFromId);
                _currencyFromId = value;
                OnPropertyChanged(GlobalPropertyNames.CurrencyFromId);
            }
        }

        private Currency _currencyFrom;
        /// <summary>
        /// Первая валюта
        /// </summary>
        public Currency CurrencyFrom
        {
            get
            {
                if (_currencyFromId == 0)
                    return null;
                if (_currencyFrom == null)
                    _currencyFrom = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyFromId);
                else if (_currencyFrom.Id != _currencyFromId)
                    _currencyFrom = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyFromId);
                return _currencyFrom;
            }
            set
            {
                if (_currencyFrom == value) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyFrom);
                _currencyFrom = value;
                _currencyFromId = _currencyFrom == null ? 0 : _currencyFrom.Id;
                OnPropertyChanged(GlobalPropertyNames.CurrencyFrom);
            }
        }

        private int _currencyToId;
        /// <summary>
        /// Идентификатор второй валюты
        /// </summary>
        public int CurrencyToId
        {
            get { return _currencyToId; }
            set
            {
                if (value == _currencyToId) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyToId);
                _currencyToId = value;
                OnPropertyChanged(GlobalPropertyNames.CurrencyToId);
            }
        }

        private Currency _currencyTo;
        /// <summary>
        /// Вторая валюта
        /// </summary>
        public Currency CurrencyTo
        {
            get
            {
                if (_currencyToId == 0)
                    return null;
                if (_currencyTo == null)
                    _currencyTo = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyToId);
                else if (_currencyTo.Id != _currencyToId)
                    _currencyTo = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyToId);
                return _currencyTo;
            }
            set
            {
                if (_currencyTo == value) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyTo);
                _currencyTo = value;
                _currencyToId = _currencyTo == null ? 0 : _currencyTo.Id;
                OnPropertyChanged(GlobalPropertyNames.CurrencyTo);
            }
        }
        private decimal _value;
        /// <summary>
        /// Значение
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

        private decimal _multiplier;
        /// <summary>
        /// Множитель
        /// </summary>
        public decimal Multiplier
        {
            get { return _multiplier; }
            set
            {
                if (value == _multiplier) return;
                OnPropertyChanging(GlobalPropertyNames.Multiplier);
                _multiplier = value;
                OnPropertyChanged(GlobalPropertyNames.Multiplier);
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

            if (_bankId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.BankId, XmlConvert.ToString(_bankId));
            if (_currencyFromId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CurrencyFromId, XmlConvert.ToString(_currencyFromId));
            if (_currencyToId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CurrencyToId, XmlConvert.ToString(_currencyToId));
            //if (_date != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Date, XmlConvert.ToString(_date));
            if (_multiplier != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Multiplier, XmlConvert.ToString(_multiplier));
            if (_value != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Value, XmlConvert.ToString(_value));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.BankId) != null)
                _bankId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.BankId));
            if (reader.GetAttribute(GlobalPropertyNames.CurrencyFromId) != null)
                _currencyFromId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CurrencyFromId));
            if (reader.GetAttribute(GlobalPropertyNames.CurrencyToId) != null)
                _currencyToId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CurrencyToId));
            if (reader.GetAttribute(GlobalPropertyNames.Date) != null)
                _date = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.Date));
            if (reader.GetAttribute(GlobalPropertyNames.Multiplier) != null)
                _multiplier = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Multiplier));
            if (reader.GetAttribute(GlobalPropertyNames.Value) != null)
                _value = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Value));
        }
        #endregion

        #region Состояние
        RateStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new RateStruct
                                  {
                                      BankId = _bankId,
                                      CurrencyFromId = _currencyFromId,
                                      CurrencyToId = _currencyToId,
                                      Date = _date,
                                      Multiplier = _multiplier,
                                      Value = _value
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
            BankId = _baseStruct.BankId;
            CurrencyFromId = _baseStruct.CurrencyFromId;
            CurrencyToId = _baseStruct.CurrencyToId;
            Date = _baseStruct.Date;
            Multiplier = _baseStruct.Multiplier;
            Value = _baseStruct.Value;
            IsChanged = false;
        }
        #endregion
        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
        
            if (_bankId == 0)
                throw new ValidateException("Не указан банк");
            if (_currencyFromId == 0)
                throw new ValidateException("Не указан первая валюта");
            if (_currencyToId == 0)
                throw new ValidateException("Не указана вторая валюта");
            if (_multiplier == 0 || _multiplier<0)
                throw new ValidateException("Множитель не может быть равен 0 или меньше 0");
            
        }

        #region База данных
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _date = reader.GetDateTime(17);
                _bankId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _currencyFromId = reader.GetInt32(19);
                _currencyToId = reader.GetInt32(20);
                _value = reader.GetDecimal(21);
                _multiplier = reader.GetDecimal(22);
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
        /// <param name="insertCommand">Sql комманда создания или обновления данных</param>
        /// <param name="validateVersion">Выполнять проверку версии. Параметр используется
        /// только для комманды обновления данных.</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Date, SqlDbType.DateTime) { IsNullable = false, Value = _date };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.BankId, SqlDbType.Int) { IsNullable = true };
            if (_bankId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _bankId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.CurrencyFromId, SqlDbType.Int) {IsNullable = false, Value = _currencyFromId};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.CurrencyToId, SqlDbType.Int) {IsNullable = false, Value = _currencyToId};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Value, SqlDbType.Money) { IsNullable = false, Value = _value };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Multiplier, SqlDbType.Money) {IsNullable = false, Value = _multiplier};
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        public List<Rate> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Rate> filter = null,
            DateTime? datetime=null, int? bankId =null, int? currencyFromId =null, int? currencyToId =null, decimal? value =null, decimal? multiplier =null,
            bool useAndFilter = false)
        {
            Rate item = new Rate { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Rate> collection = new List<Rate>();
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
                        if (datetime.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.Date, SqlDbType.DateTime).Value = datetime.Value;
                        if (bankId.HasValue && bankId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.BankId, SqlDbType.Int).Value = bankId.Value;
                        if (currencyFromId.HasValue && currencyFromId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.CurrencyFromId, SqlDbType.Int).Value = currencyFromId.Value;
                        if (currencyToId.HasValue && currencyToId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.CurrencyToId, SqlDbType.Int).Value = currencyToId.Value;
                        if (value.HasValue && value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Value, SqlDbType.Int).Value = value.Value;
                        if (multiplier.HasValue && multiplier != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Multiplier, SqlDbType.Int).Value = multiplier.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Rate { Workarea = Workarea };
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
