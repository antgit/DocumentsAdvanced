using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// дополнительная аналитика строки документа
    /// </summary>
    public sealed class DocumentDetailAnalitic : DocumentBaseDetail, IEditableObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailAnalitic()
            : base()
        {
            // TODO: правильно установить тип документа при создании строки документа...
            _entityId = -1;
        }
        #region Свойства

        public IDocumentDetail Document { get; set; }


        private int _analiticId;
        /// <summary>Идентификатор аналитики</summary>
        public int AnaliticId
        {
            get { return _analiticId; }
            set
            {
                if (value == _analiticId) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId);
                _analiticId = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId);
            }
        }

        private Analitic _analitic;
        /// <summary>Аналитика</summary>
        public Analitic Analitic
        {
            get
            {
                if (_analiticId == 0)
                    return null;
                if (_analitic == null)
                    _analitic = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId);
                else if (_analitic.Id != _analiticId)
                    _analitic = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId);
                return _analitic;
            }
            set
            {
                if (_analitic == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic);
                _analitic = value;
                _analiticId = _analitic == null ? 0 : _analitic.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic);
            }
        }


        private int _analiticId2;
        /// <summary>Идентификатор аналитики №2</summary>
        public int AnaliticId2
        {
            get { return _analiticId2; }
            set
            {
                if (value == _analiticId2) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId2);
                _analiticId2 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId2);
            }
        }

        private Analitic _analitic2;
        /// <summary>Аналитика №2</summary>
        public Analitic Analitic2
        {
            get
            {
                if (_analiticId2 == 0)
                    return null;
                if (_analitic2 == null)
                    _analitic2 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId2);
                else if (_analitic2.Id != _analiticId2)
                    _analitic2 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId2);
                return _analitic2;
            }
            set
            {
                if (_analitic2 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic2);
                _analitic2 = value;
                _analiticId2 = _analitic2 == null ? 0 : _analitic2.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic2);
            }
        }

        private int _analiticId3;
        /// <summary>Идентификатор аналитики №3</summary>
        public int AnaliticId3
        {
            get { return _analiticId3; }
            set
            {
                if (value == _analiticId3) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId3);
                _analiticId3 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId3);
            }
        }


        private Analitic _analitic3;
        /// <summary>Аналитика №3</summary>
        public Analitic Analitic3
        {
            get
            {
                if (_analiticId3 == 0)
                    return null;
                if (_analitic3 == null)
                    _analitic3 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId3);
                else if (_analitic3.Id != _analiticId3)
                    _analitic3 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId3);
                return _analitic3;
            }
            set
            {
                if (_analitic3 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic3);
                _analitic3 = value;
                _analiticId3 = _analitic3 == null ? 0 : _analitic3.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic3);
            }
        }


        private int _analiticId4;
        /// <summary>Идентификатор аналитики №4</summary>
        public int AnaliticId4
        {
            get { return _analiticId4; }
            set
            {
                if (value == _analiticId4) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId4);
                _analiticId4 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId4);
            }
        }


        private Analitic _analitic4;
        /// <summary>Аналитика №4</summary>
        public Analitic Analitic4
        {
            get
            {
                if (_analiticId4 == 0)
                    return null;
                if (_analitic4 == null)
                    _analitic4 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId4);
                else if (_analitic4.Id != _analiticId4)
                    _analitic4 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId4);
                return _analitic4;
            }
            set
            {
                if (_analitic4 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic4);
                _analitic4 = value;
                _analiticId4 = _analitic4 == null ? 0 : _analitic4.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic4);
            }
        }


        private int _analiticId5;
        /// <summary>Идентификатор аналитики №5</summary>
        public int AnaliticId5
        {
            get { return _analiticId5; }
            set
            {
                if (value == _analiticId5) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId5);
                _analiticId5 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId5);
            }
        }


        private Analitic _analitic5;
        /// <summary>Аналитика №5</summary>
        public Analitic Analitic5
        {
            get
            {
                if (_analiticId5 == 0)
                    return null;
                if (_analitic5 == null)
                    _analitic5 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId5);
                else if (_analitic5.Id != _analiticId5)
                    _analitic5 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId5);
                return _analitic5;
            }
            set
            {
                if (_analitic5 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic5);
                _analitic5 = value;
                _analiticId5 = _analitic5 == null ? 0 : _analitic5.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic5);
            }
        }



        private decimal _summa;
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Summa
        {
            get { return _summa; }
            set
            {
                if (value == _summa) return;
                OnPropertyChanging(GlobalPropertyNames.Summa);
                _summa = value;
                OnPropertyChanged(GlobalPropertyNames.Summa);
            }
        }

        private string _memo;
        /// <summary>
        /// Примечание
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set
            {
                if (value == _memo) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }


        private decimal _summValue1;
        /// <summary>Денежное значение №1</summary>
        public decimal SummValue1
        {
            get { return _summValue1; }
            set
            {
                if (value == _summValue1) return;
                OnPropertyChanging(GlobalPropertyNames.SummValue1);
                _summValue1 = value;
                OnPropertyChanged(GlobalPropertyNames.SummValue1);
            }
        }

        private decimal _summValue2;
        /// <summary>Денежное значение №2</summary>
        public decimal SummValue2
        {
            get { return _summValue2; }
            set
            {
                if (value == _summValue2) return;
                OnPropertyChanging(GlobalPropertyNames.SummValue2);
                _summValue2 = value;
                OnPropertyChanged(GlobalPropertyNames.SummValue2);
            }
        }

        private decimal _summValue3;
        /// <summary>Денежное значение №3</summary>
        public decimal SummValue3
        {
            get { return _summValue3; }
            set
            {
                if (value == _summValue3) return;
                OnPropertyChanging(GlobalPropertyNames.SummValue3);
                _summValue3 = value;
                OnPropertyChanged(GlobalPropertyNames.SummValue3);
            }
        }

        private decimal _summValue4;
        /// <summary>Денежное значение №4</summary>
        public decimal SummValue4
        {
            get { return _summValue4; }
            set
            {
                if (value == _summValue4) return;
                OnPropertyChanging(GlobalPropertyNames.SummValue4);
                _summValue4 = value;
                OnPropertyChanged(GlobalPropertyNames.SummValue4);
            }
        }

        private decimal _summValue5;
        /// <summary>Денежное значение №5</summary>
        public decimal SummValue5
        {
            get { return _summValue5; }
            set
            {
                if (value == _summValue5) return;
                OnPropertyChanging(GlobalPropertyNames.SummValue5);
                _summValue5 = value;
                OnPropertyChanged(GlobalPropertyNames.SummValue5);
            }
        }


        private int _intValue1;
        /// <summary>Целочисленное значение №1</summary>
        public int IntValue1
        {
            get { return _intValue1; }
            set
            {
                if (value == _intValue1) return;
                OnPropertyChanging(GlobalPropertyNames.IntValue1);
                _intValue1 = value;
                OnPropertyChanged(GlobalPropertyNames.IntValue1);
            }
        }


        private int _intValue2;
        /// <summary>Целочисленное значение №2</summary>
        public int IntValue2
        {
            get { return _intValue2; }
            set
            {
                if (value == _intValue2) return;
                OnPropertyChanging(GlobalPropertyNames.IntValue2);
                _intValue2 = value;
                OnPropertyChanged(GlobalPropertyNames.IntValue2);
            }
        }


        private int _intValue3;
        /// <summary>Целочисленное значение №3</summary>
        public int IntValue3
        {
            get { return _intValue3; }
            set
            {
                if (value == _intValue3) return;
                OnPropertyChanging(GlobalPropertyNames.IntValue3);
                _intValue3 = value;
                OnPropertyChanged(GlobalPropertyNames.IntValue3);
            }
        }


        private int _intValue4;
        /// <summary>Целочисленное значение №4</summary>
        public int IntValue4
        {
            get { return _intValue4; }
            set
            {
                if (value == _intValue4) return;
                OnPropertyChanging(GlobalPropertyNames.IntValue4);
                _intValue4 = value;
                OnPropertyChanged(GlobalPropertyNames.IntValue4);
            }
        }

        private int _intValue5;
        /// <summary>Целочисленное значение №5</summary>
        public int IntValue5
        {
            get { return _intValue5; }
            set
            {
                if (value == _intValue5) return;
                OnPropertyChanging(GlobalPropertyNames.IntValue5);
                _intValue5 = value;
                OnPropertyChanged(GlobalPropertyNames.IntValue5);
            }
        }


        private string _stringValue1;
        /// <summary>Строковое значение №1</summary>
        public string StringValue1
        {
            get { return _stringValue1; }
            set
            {
                if (value == _stringValue1) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue1);
                _stringValue1 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue1);
            }
        }


        private string _stringValue2;
        /// <summary>Строковое значение №2</summary>
        public string StringValue2
        {
            get { return _stringValue2; }
            set
            {
                if (value == _stringValue2) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue2);
                _stringValue2 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue2);
            }
        }



        private string _stringValue3;
        /// <summary>Строковое значение №3</summary>
        public string StringValue3
        {
            get { return _stringValue3; }
            set
            {
                if (value == _stringValue3) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue3);
                _stringValue3 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue3);
            }
        }

        private string _stringValue4;
        /// <summary>Строковое значение №4</summary>
        public string StringValue4
        {
            get { return _stringValue4; }
            set
            {
                if (value == _stringValue4) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue4);
                _stringValue4 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue4);
            }
        }


        private string _stringValue5;
        /// <summary>Строковое значение №5</summary>
        public string StringValue5
        {
            get { return _stringValue5; }
            set
            {
                if (value == _stringValue5) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue5);
                _stringValue5 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue5);
            }
        }



        private int _groupNo;
        /// <summary>Номер группы</summary>
        public int GroupNo
        {
            get { return _groupNo; }
            set
            {
                if (value == _groupNo) return;
                OnPropertyChanging(GlobalPropertyNames.GroupNo);
                _groupNo = value;
                OnPropertyChanged(GlobalPropertyNames.GroupNo);
            }
        }

        private int _groupNo2;
        /// <summary>Номер группы №2</summary>
        public int GroupNo2
        {
            get { return _groupNo2; }
            set
            {
                if (value == _groupNo2) return;
                OnPropertyChanging(GlobalPropertyNames.GroupNo2);
                _groupNo2 = value;
                OnPropertyChanged(GlobalPropertyNames.GroupNo2);
            }
        }

        private int _groupNo3;
        /// <summary>Номер группы №3</summary>
        public int GroupNo3
        {
            get { return _groupNo3; }
            set
            {
                if (value == _groupNo3) return;
                OnPropertyChanging(GlobalPropertyNames.GroupNo3);
                _groupNo3 = value;
                OnPropertyChanged(GlobalPropertyNames.GroupNo3);
            }
        }

        private int _groupNo4;
        /// <summary>Номер группы №4</summary>
        public int GroupNo4
        {
            get { return _groupNo4; }
            set
            {
                if (value == _groupNo4) return;
                OnPropertyChanging(GlobalPropertyNames.GroupNo4);
                _groupNo4 = value;
                OnPropertyChanged(GlobalPropertyNames.GroupNo4);
            }
        }

        private int _groupNo5;
        /// <summary>Номер группы №5</summary>
        public int GroupNo5
        {
            get { return _groupNo5; }
            set
            {
                if (value == _groupNo5) return;
                OnPropertyChanging(GlobalPropertyNames.GroupNo5);
                _groupNo5 = value;
                OnPropertyChanged(GlobalPropertyNames.GroupNo5);
            }
        }

        private DateTime? _dateValue1;
        /// <summary>Значение даты №1</summary>
        public DateTime? DateValue1
        {
            get { return _dateValue1; }
            set
            {
                if (value == _dateValue1) return;
                OnPropertyChanging(GlobalPropertyNames.DateValue1);
                _dateValue1 = value;
                OnPropertyChanged(GlobalPropertyNames.DateValue1);
            }
        }

        private DateTime? _dateValue2;
        /// <summary>Значение даты №2</summary>
        public DateTime? DateValue2
        {
            get { return _dateValue2; }
            set
            {
                if (value == _dateValue2) return;
                OnPropertyChanging(GlobalPropertyNames.DateValue2);
                _dateValue2 = value;
                OnPropertyChanged(GlobalPropertyNames.DateValue2);
            }
        }

        private DateTime? _dateValue3;
        /// <summary>Значение даты №3</summary>
        public DateTime? DateValue3
        {
            get { return _dateValue3; }
            set
            {
                if (value == _dateValue3) return;
                OnPropertyChanging(GlobalPropertyNames.DateValue3);
                _dateValue3 = value;
                OnPropertyChanged(GlobalPropertyNames.DateValue3);
            }
        }

        private DateTime? _dateValue4;
        /// <summary>Значение даты №4</summary>
        public DateTime? DateValue4
        {
            get { return _dateValue4; }
            set
            {
                if (value == _dateValue4) return;
                OnPropertyChanging(GlobalPropertyNames.DateValue4);
                _dateValue4 = value;
                OnPropertyChanged(GlobalPropertyNames.DateValue4);
            }
        }

        private DateTime? _dateValue5;
        /// <summary>Значение даты №5</summary>
        public DateTime? DateValue5
        {
            get { return _dateValue5; }
            set
            {
                if (value == _dateValue5) return;
                OnPropertyChanging(GlobalPropertyNames.DateValue5);
                _dateValue5 = value;
                OnPropertyChanged(GlobalPropertyNames.DateValue5);
            }
        }


        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_analiticId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AnaliticId, XmlConvert.ToString(_analiticId));
            if (_summa != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Summa, XmlConvert.ToString(_summa));
            if (_dateValue1.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DayToPay, XmlConvert.ToString(_dateValue1.Value));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.AnaliticId) != null)
                _analiticId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AnaliticId));
            if (reader.GetAttribute(GlobalPropertyNames.Summa) != null)
                _summa = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Summa));
            if (reader.GetAttribute(GlobalPropertyNames.DayToPay) != null)
                _dateValue1 = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DayToPay));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
        }
        #endregion

        #region Состояния
        BaseStructDocumentAnalitic _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentAnalitic
                                  {
                                      Memo = _memo,
                                      Summa = _summa,
                                      AnaliticId = _analiticId,
                                      AnaliticId2 = _analiticId2,
                                      AnaliticId3 = _analiticId3,
                                      AnaliticId4 = _analiticId4,
                                      AnaliticId5 = _analiticId5,
                                      KindId = Kind,
                                      SummValue1 = _summValue1,
                                      SummValue2 = _summValue2,
                                      SummValue3 = _summValue3,
                                      SummValue4 = _summValue4,
                                      SummValue5 = _summValue5,
                                      IntValue1 = _intValue1,
                                      IntValue2 = _intValue2,
                                      IntValue3 = _intValue3,
                                      IntValue4 = _intValue4,
                                      IntValue5 = _intValue5,
                                      StringValue1 = _stringValue1,
                                      StringValue2 = _stringValue2,
                                      StringValue3 = _stringValue3,
                                      StringValue4 = _stringValue4,
                                      StringValue5 = _stringValue5,
                                      DateValue1 = _dateValue1,
                                      DateValue2 = _dateValue2,
                                      DateValue3 = _dateValue3,
                                      DateValue4 = _dateValue4,
                                      DateValue5 = _dateValue5,
                                      GroupNo = _groupNo,
                                      GroupNo2 = _groupNo2,
                                      GroupNo3 = _groupNo3,
                                      GroupNo4 = _groupNo4,
                                      GroupNo5 = _groupNo5,
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
            _memo = _baseStruct.Memo;
            _summa = _baseStruct.Summa;
            _analiticId = _baseStruct.AnaliticId;
            _analiticId2 = _baseStruct.AnaliticId2;
            _analiticId3 = _baseStruct.AnaliticId3;
            _analiticId4 = _baseStruct.AnaliticId4;
            _analiticId5 = _baseStruct.AnaliticId5;
            Kind = _baseStruct.KindId;
            _summValue1 = _baseStruct.SummValue1;
            _summValue2 = _baseStruct.SummValue2;
            _summValue3 = _baseStruct.SummValue3;
            _summValue4 = _baseStruct.SummValue4;
            _summValue5 = _baseStruct.SummValue5;
            _intValue1 = _baseStruct.IntValue1;
            _intValue2 = _baseStruct.IntValue2;
            _intValue3 = _baseStruct.IntValue3;
            _intValue4 = _baseStruct.IntValue4;
            _intValue5 = _baseStruct.IntValue5;
            _stringValue1 = _baseStruct.StringValue1;
            _stringValue2 = _baseStruct.StringValue2;
            _stringValue3 = _baseStruct.StringValue3;
            _stringValue4 = _baseStruct.StringValue4;
            _stringValue5 = _baseStruct.StringValue5;
            _dateValue1 = _baseStruct.DateValue1;
            _dateValue2 = _baseStruct.DateValue2;
            _dateValue3 = _baseStruct.DateValue3;
            _dateValue4 = _baseStruct.DateValue4;
            _dateValue5 = _baseStruct.DateValue5;

            _groupNo = _baseStruct.GroupNo;
            _groupNo2 = _baseStruct.GroupNo2;
            _groupNo3 = _baseStruct.GroupNo3;
            _groupNo4 = _baseStruct.GroupNo4;
            _groupNo5 = _baseStruct.GroupNo5;
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
            if (StateId != State.STATEDELETED)
            {
                StateId = Document.StateId;
            }
            Date = Document.Date;
            //if (Kind == 0)
            Kind = Document.Kind;

            if (_analiticId == 0)
                throw new ValidateException("Не указана аналитика");
        }
        #region База данных
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
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
                DateModified = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6);
                FlagsValue = reader.GetInt32(7);
                StateId = reader.GetInt32(8);
                Date = reader.GetDateTime(9);
                OwnerId = reader.GetInt32(10);
                Kind = reader.GetInt32(11);
                _analiticId = reader.GetInt32(12);
                _analiticId2 = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _analiticId3 = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                _analiticId4 = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
                _analiticId5 = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                _summa = reader.GetDecimal(17);
                _memo = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                _summValue1 = reader.GetDecimal(19);
                _summValue2 = reader.GetDecimal(20);
                _summValue3 = reader.GetDecimal(21);
                _summValue4 = reader.GetDecimal(22);
                _summValue5 = reader.GetDecimal(23);
                _intValue1 = reader.IsDBNull(24) ? 0 : reader.GetInt32(24);
                _intValue2 = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                _intValue3 = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                _intValue4 = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                _intValue5 = reader.IsDBNull(28) ? 0 : reader.GetInt32(28);
                _stringValue1 = reader.IsDBNull(29) ? string.Empty : reader.GetString(29);
                _stringValue2 = reader.IsDBNull(30) ? string.Empty : reader.GetString(30);
                _stringValue3 = reader.IsDBNull(31) ? string.Empty : reader.GetString(31);
                _stringValue4 = reader.IsDBNull(32) ? string.Empty : reader.GetString(32);
                _stringValue5 = reader.IsDBNull(33) ? string.Empty : reader.GetString(33);
                _dateValue1 = reader.IsDBNull(34) ? (DateTime?)null : reader.GetDateTime(34);
                _dateValue2 = reader.IsDBNull(35) ? (DateTime?)null : reader.GetDateTime(35);
                _dateValue3 = reader.IsDBNull(36) ? (DateTime?)null : reader.GetDateTime(36);
                _dateValue4 = reader.IsDBNull(37) ? (DateTime?)null : reader.GetDateTime(37);
                _dateValue5 = reader.IsDBNull(38) ? (DateTime?)null : reader.GetDateTime(38);
                _groupNo = reader.IsDBNull(39) ? 0 : reader.GetInt32(39);
                _groupNo2 = reader.IsDBNull(40) ? 0 : reader.GetInt32(40);
                _groupNo3 = reader.IsDBNull(41) ? 0 : reader.GetInt32(41);
                _groupNo4 = reader.IsDBNull(42) ? 0 : reader.GetInt32(42);
                _groupNo5 = reader.IsDBNull(43) ? 0 : reader.GetInt32(43);

            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }

        internal class TpvCollection : List<DocumentDetailAnalitic>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {

                var sdr = new SqlDataRecord
                    (
                    new SqlMetaData(GlobalPropertyNames.Id, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Guid, SqlDbType.UniqueIdentifier),
                    new SqlMetaData(GlobalPropertyNames.DatabaseId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.DbSourceId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Version, SqlDbType.Binary, 8),
                    new SqlMetaData(GlobalPropertyNames.UserName, SqlDbType.NVarChar, 50),
                    new SqlMetaData(GlobalPropertyNames.DateModified, SqlDbType.DateTime),
                    new SqlMetaData(GlobalPropertyNames.Flags, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StateId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Date, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.OwnId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.KindId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId2, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId3, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId4, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId5, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Summa, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, 255),

                    new SqlMetaData(GlobalPropertyNames.SummValue1, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.SummValue2, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.SummValue3, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.SummValue4, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.SummValue5, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.IntValue1, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.IntValue2, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.IntValue3, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.IntValue4, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.IntValue5, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StringValue1, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.StringValue2, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.StringValue3, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.StringValue4, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.StringValue5, SqlDbType.NVarChar, 100),

                    new SqlMetaData(GlobalPropertyNames.DateValue1, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.DateValue2, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.DateValue3, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.DateValue4, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.DateValue5, SqlDbType.Date),

                    new SqlMetaData(GlobalPropertyNames.GroupNo, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.GroupNo2, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.GroupNo3, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.GroupNo4, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.GroupNo5, SqlDbType.Int)
                    );

                foreach (DocumentDetailAnalitic doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailAnalitic doc)
        {
            sdr.SetInt32(0, doc.Id);
            sdr.SetGuid(1, doc.Guid);
            sdr.SetInt32(2, doc.DatabaseId);
            if (doc.DbSourceId == 0)
                sdr.SetValue(3, DBNull.Value);
            else
                sdr.SetInt32(3, doc.DbSourceId);
            if (doc.ObjectVersion == null || doc.ObjectVersion.All(v => v == 0))
                sdr.SetValue(4, DBNull.Value);
            else
                sdr.SetValue(4, doc.ObjectVersion);

            if (string.IsNullOrEmpty(doc.UserName))
                sdr.SetValue(5, DBNull.Value);
            else
                sdr.SetString(5, doc.UserName);

            if (doc.DateModified.HasValue)
                sdr.SetDateTime(6, doc.DateModified.Value);
            else
                sdr.SetValue(6, DBNull.Value);

            sdr.SetInt32(7, doc.FlagsValue);
            sdr.SetInt32(8, doc.StateId);
            sdr.SetDateTime(9, doc.Date);
            sdr.SetInt32(10, doc.Document.Id);
            sdr.SetInt32(11, doc.Kind);

            sdr.SetInt32(12, doc.AnaliticId);
            if (doc.AnaliticId2 == 0)
                sdr.SetValue(13, DBNull.Value);
            else
                sdr.SetInt32(13, doc.AnaliticId2);

            if (doc.AnaliticId3 == 0)
                sdr.SetValue(14, DBNull.Value);
            else
                sdr.SetInt32(14, doc.AnaliticId3);
            if (doc.AnaliticId4 == 0)
                sdr.SetValue(15, DBNull.Value);
            else
                sdr.SetInt32(15, doc.AnaliticId4);
            if (doc.AnaliticId5 == 0)
                sdr.SetValue(16, DBNull.Value);
            else
                sdr.SetInt32(16, doc.AnaliticId5);

            sdr.SetDecimal(17, doc.Summa);
            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetString(18, doc.Memo);

            sdr.SetDecimal(19, doc.SummValue1);
            sdr.SetDecimal(20, doc.SummValue2);
            sdr.SetDecimal(21, doc.SummValue3);
            sdr.SetDecimal(22, doc.SummValue4);
            sdr.SetDecimal(23, doc.SummValue5);

            if (doc.IntValue1 == 0)
                sdr.SetValue(24, DBNull.Value);
            else
                sdr.SetInt32(24, doc.IntValue1);

            if (doc.IntValue2 == 0)
                sdr.SetValue(25, DBNull.Value);
            else
                sdr.SetInt32(25, doc.IntValue2);

            if (doc.IntValue3 == 0)
                sdr.SetValue(26, DBNull.Value);
            else
                sdr.SetInt32(26, doc.IntValue3);

            if (doc.IntValue4 == 0)
                sdr.SetValue(27, DBNull.Value);
            else
                sdr.SetInt32(27, doc.IntValue4);

            if (doc.IntValue5 == 0)
                sdr.SetValue(28, DBNull.Value);
            else
                sdr.SetInt32(28, doc.IntValue5);

            if (string.IsNullOrEmpty(doc.StringValue1))
                sdr.SetValue(29, DBNull.Value);
            else
                sdr.SetString(29, doc.StringValue1);

            if (string.IsNullOrEmpty(doc.StringValue2))
                sdr.SetValue(30, DBNull.Value);
            else
                sdr.SetString(30, doc.StringValue2);

            if (string.IsNullOrEmpty(doc.StringValue3))
                sdr.SetValue(31, DBNull.Value);
            else
                sdr.SetString(31, doc.StringValue3);

            if (string.IsNullOrEmpty(doc.StringValue4))
                sdr.SetValue(32, DBNull.Value);
            else
                sdr.SetString(32, doc.StringValue4);

            if (string.IsNullOrEmpty(doc.StringValue5))
                sdr.SetValue(33, DBNull.Value);
            else
                sdr.SetString(33, doc.StringValue5);

            if (doc.DateValue1.HasValue)
                sdr.SetDateTime(34, doc.DateValue1.Value);
            else
                sdr.SetValue(34, DBNull.Value);

            if (doc.DateValue2.HasValue)
                sdr.SetDateTime(35, doc.DateValue2.Value);
            else
                sdr.SetValue(35, DBNull.Value);

            if (doc.DateValue3.HasValue)
                sdr.SetDateTime(36, doc.DateValue3.Value);
            else
                sdr.SetValue(36, DBNull.Value);

            if (doc.DateValue4.HasValue)
                sdr.SetDateTime(37, doc.DateValue4.Value);
            else
                sdr.SetValue(37, DBNull.Value);

            if (doc.DateValue5.HasValue)
                sdr.SetDateTime(38, doc.DateValue5.Value);
            else
                sdr.SetValue(38, DBNull.Value);

            sdr.SetValue(39, doc.GroupNo);
            sdr.SetValue(40, doc.GroupNo2);
            sdr.SetValue(41, doc.GroupNo3);
            sdr.SetValue(42, doc.GroupNo4);
            sdr.SetValue(43, doc.GroupNo5);

            return sdr;
        }
        #endregion

        #region IEditableObject Members
        void IEditableObject.BeginEdit()
        {
            SaveState(false);
        }

        void IEditableObject.CancelEdit()
        {
            RestoreState();
        }

        void IEditableObject.EndEdit()
        {
            _baseStruct = new BaseStructDocumentAnalitic();
        }

        #endregion

        public override void Load(int value)
        {
            //throw new NotImplementedException();
        }
    }
}