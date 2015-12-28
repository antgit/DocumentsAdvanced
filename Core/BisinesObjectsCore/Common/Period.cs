using System;

namespace BusinessObjects
{
	#region Enums

	/// <summary> Тип периода </summary>
	public enum PeriodType
	{
        /// <summary>Произвольный период ограниченный двумя датами</summary>
		Custom,
        /// <summary>Период в один день</summary>
        Day,
        /// <summary>Период в один месяц</summary>
		Month,
        /// <summary>Период в несколько месяцев</summary>
		MonthRange,
        /// <summary>Период в один квартал</summary>
		Quarter,
        /// <summary>Период в один год</summary>
		Year
	}

	#endregion

    internal struct PeriodStruct
    {
        /// <summary>Начало периода</summary>
        public DateTime Start;
        /// <summary>Конец периода</summary>
        public DateTime End;
    }
    // TODO: Улучшить поддержку уведомлений свойств в методах установки периода
    /// <summary>Период</summary>
    public sealed class Period : BasePropertyChangeSupport
	{
        /// <summary>
        /// Преобразование TimeSpan к дате со временем, в качестве даты используется текущая дата, время - на основе параметра
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? TimeSpan2DateTime(TimeSpan? value)
        {
            if (!value.HasValue)
                return null;

            DateTime now = DateTime.Now;

            return new DateTime(now.Year, now.Month, now.Day, value.Value.Hours, value.Value.Minutes, value.Value.Seconds);
        }
        /// <summary>
        /// Преобразование даты к TimeSpan
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeSpan? DateTime2TimeSpan(DateTime? value)
        {
            if (!value.HasValue)
                return null;

            return new TimeSpan(value.Value.Hour, value.Value.Minute, value.Value.Second);
        }
        public static DateTime FirstDayOfMonth(DateTime dtDate)
        {
            DateTime dtFrom = dtDate;
            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));
            return TrimToStart(dtFrom);
        }
        public static DateTime FirstDayOfMonth(int iMonth)
        {
            DateTime dtFrom = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            return TrimToStart(dtFrom);
        } 

        public static DateTime LastDayOfCurrentMonth()
        {
            DateTime today = DateTime.Today;
            return TrimToEnd(new DateTime(today.Year, today.Month, 1).AddMonths(1).AddDays(-1));
        }
        public static DateTime FirstDayOfCurrentMonth()
        {
            return FirstDayOfMonth(DateTime.Today);
        }
        public static DateTime LastDayOfMonth(DateTime dtDate)
        {
            DateTime dtTo = dtDate;
            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));
            return TrimToEnd(dtTo);
        }


        public static DateTime LastDayOfMonth(int iMonth)
        {
            DateTime dtTo = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));

            return TrimToEnd(dtTo);
        }

        public static DateTime LastDayOfNextMonth()
        {
            DateTime dtTo = DateTime.Today;
            dtTo = dtTo.AddMonths(2);
            dtTo = dtTo.AddDays(-(dtTo.Day));
            return TrimToEnd(dtTo);

            
        }

        public static DateTime EndOfCurrentWeek()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime start = DateTime.Now.AddDays(-days);
            return TrimToEnd(start.AddDays(6));
        }
        public static DateTime EndOfNextWeek()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime start = DateTime.Now.AddDays(-days);
            return TrimToEnd(start.AddDays(6).AddDays(7));
        }
        public static DateTime EndOfTwoNextWeek()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime start = DateTime.Now.AddDays(-days);
            return TrimToEnd(start.AddDays(6).AddDays(14));
        }
        public static DateTime EndOfThreeNextWeek()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime start = DateTime.Now.AddDays(-days);
            return TrimToEnd(start.AddDays(6).AddDays(21));
        }
        public static DateTime EndOfToday()
        {
            return TrimToEnd(DateTime.Now);
        }
        public static DateTime EndOfTomorow()
        {
            return TrimToEnd(DateTime.Now.AddDays(1));
        }
        public static DateTime EndOfThedDay()
        {
            return TrimToEnd(DateTime.Now.AddDays(2));
        }
		private DateTime _pStart;		//Начало периода
		private DateTime _pEnd;			//Конец периода
		private PeriodType _pType;		//Тип периода
		private int _pYear;				//Текущий год

		#region Constructors
		/// <summary>Конструктор - создает период типа день с текущей системной датой</summary>
		public Period()
		{
			SetDay(DateTime.Today);
		}

		/// <summary>Конструктор - создает период типа ден </summary>
        /// <param name="day">День</param>
		public Period(DateTime day)
		{
			SetDay(day);
		}

		/// <summary>Конструктор - создает период произвольного тип </summary>
        /// <param name="startDay">Начало периода</param>
        /// <param name="endDay">Окончание периода</param>
		public Period(DateTime startDay, DateTime endDay)
		{
			SetCustom(startDay, endDay);
		}

		/// <summary>Конструктор - создает период типа месяц</summary>
        /// <param name="month">Номер месяца</param>
		public Period(int month)
		{
			SetMonth(month);
		}
		
		/// <summary>Конструктор - создает период типа диапазон месяцев </summary>
        /// <param name="startMonth">Номер месяца для начала периода</param>
        /// <param name="endMonth">Номер месяца для конца периода</param>
		public Period(int startMonth, int endMonth)
		{
			SetMonthRange(startMonth, endMonth);
		}
		#endregion

		#region Properties

		/// <summary>Начало периода</summary>
		public DateTime Start
		{
			get { return _pStart; }
			set 
			{ 
				if (_pStart == value) return;
                OnPropertyChanging(GlobalPropertyNames.Start);
                _pStart = value;
				_pType = PeriodType.Custom;
				_pYear = _pStart.Year;
                OnPropertyChanged(GlobalPropertyNames.Start);
			}
		}

		/// <summary>Конец периода</summary>
		public DateTime End
		{
			get { return _pEnd; }
			set 
			{ 
				if (_pEnd == value) return;
                OnPropertyChanging(GlobalPropertyNames.End);
				_pEnd = value;
				_pType = PeriodType.Custom;
                OnPropertyChanged(GlobalPropertyNames.End);
			}
		}

		/// <summary>Тип периода</summary>
		public PeriodType Type
		{
			get { return _pType; }
		}

		/// <summary>Текущий год периода. Определяется по дате начала периода</summary>
		public int Year
		{
			get { return _pYear; }
			set 
			{	if (value == _pYear) return;
				if (value < DateTime.MinValue.Year || value > DateTime.MaxValue.Year) return;
                OnPropertyChanging(GlobalPropertyNames.Year);
				int delta = value - _pYear;
				_pStart = _pStart.AddYears(delta);
				_pEnd = _pEnd.AddYears(delta);
				_pYear = value;
                OnPropertyChanged(GlobalPropertyNames.Year);
			}
		}

		#endregion

		#region Methods

        /// <summary>Устанавливает период в произвольный диапазон дат</summary>
        /// <param name="startDate">начальная дата</param>
        /// <param name="endDate">конечная дата</param>
		public void SetCustom(DateTime startDate, DateTime endDate)
		{
			if (startDate > endDate) return;
			_pType = PeriodType.Custom;
            _pStart = TrimToStart(startDate.Date);
			_pEnd = TrimToEnd(endDate.Date.AddDays(1));
			_pYear = _pStart.Year;
            IsChanged = true;
		}

		/// <summary>Устанавливает период в заданный день</summary>
		/// <param name="day">Требуемая дата</param>
		public void SetDay(DateTime day)
		{
			_pType = PeriodType.Day;
            _pStart = TrimToStart(day.Date);
            _pEnd = TrimToEnd(_pStart.AddDays(1));
			_pYear = _pStart.Year;
            IsChanged = true;
		}
        /// <summary>Установить период за последнии 30 дней</summary>
        public void SetLast30Day()
        {
            SetLast(30, DateTime.Today);
            IsChanged = true;
        }
        /// <summary>Установить период за последнии n-дней</summary>
        /// <param name="count">Количество дней</param>
        /// <param name="day">Конец периода</param>
        public void SetLast(int count, DateTime day)
        {
            _pType = PeriodType.Custom;
            _pEnd = TrimToEnd(day);
            _pStart = TrimToStart(day.AddDays(-count));
            
            _pYear = _pStart.Year;
            IsChanged = true;
        }

        private static DateTime TrimToStart(DateTime value)
        {
            return value.AddHours(-value.Hour).AddMinutes(-value.Minute).AddSeconds(-value.Second);
        }
        private static DateTime TrimToEnd(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
        }

		/// <summary>Устанавливает период в заданный месяц</summary>
		/// <param name="month">номер заданного месяца (от 1 до 12)</param>
		public void SetMonth(int month)
		{
			if (month < 1 || month > 12) return;
			_pType = PeriodType.Month;
			_pStart = TrimToStart(new DateTime(_pYear, month, 1));
			_pEnd = TrimToEnd(_pStart.AddMonths(1));
			_pYear = _pStart.Year;
            IsChanged = true;
		}
		/// <summary>Устанавливает период в заданный диапазон месяцев</summary>
        /// <param name="startMonth">начальный месяц (от 1 до 12)</param>
        /// <param name="endMonth">конечный месяц (от 1 до 12)</param>
		public void SetMonthRange(int startMonth, int endMonth)
		{
			if (startMonth < 1 || startMonth > 12) return;
			if (endMonth < 1 || endMonth > 12) return;
			if (startMonth > endMonth) return;
			_pType = PeriodType.MonthRange;
			_pStart = TrimToStart(new DateTime(_pYear, startMonth, 1));
			_pEnd = TrimToEnd(_pStart.AddMonths(endMonth - startMonth + 1));
			_pYear = _pStart.Year;
            IsChanged = true;
		}

		/// <summary>Устанавливает период в заданный квартал</summary>
		/// <param name="quart">номер заданного квартала (от 1 до 4)</param>
		public void SetQuarter(int quart)
		{
			if (quart < 1 || quart > 4) return;
			_pType = PeriodType.Quarter;
			int smonth = 1 + (quart - 1) * 3;
			_pStart =TrimToStart(new DateTime(_pYear, smonth, 1));
			_pEnd = TrimToEnd(_pStart.AddMonths(3));
			_pYear = _pStart.Year;
            IsChanged = true;
		}

		/// <summary>Устанавливает период в заданный год</summary>
		/// <param name="year">требуемый год</param>
		public void SetYear(int year)
		{
			if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year) return;
			_pType = PeriodType.Year;
			_pStart = TrimToStart(new DateTime(year, 1, 1));
			_pEnd = TrimToEnd(_pStart.AddYears(1).AddDays(-1));
			_pYear = _pStart.Year;
            IsChanged = true;
		}
        /// <summary>Устанавливает период в несколько лет</summary>
        /// <param name="yearFrom">Начало</param>
        /// <param name="yearTo">Конец</param>
        public void SetYearRange(int yearFrom, int yearTo)
        {
            if (yearFrom < DateTime.MinValue.Year || yearFrom > DateTime.MaxValue.Year) return;
            if (yearTo < DateTime.MinValue.Year || yearTo > DateTime.MaxValue.Year) return;
            if (yearTo < yearFrom) return;
            _pType = PeriodType.Custom;
            _pStart = TrimToStart(new DateTime(yearFrom, 1, 1));
            _pEnd = TrimToEnd(new DateTime(yearTo, 12, 31));
            IsChanged = true;
        }

		/// <summary>Возвращает номер квартала по номеру месяца</summary>
		/// <param name="month">номер месяца (от 1 до 12)</param>
		private static int GetQuarterNum(int month)
		{
			if (month < 1 || month > 12) return 0;

			decimal idx = Math.Round(month / 3.0M + 0.3M, 0);
			return (int)idx;
		}

		/// <summary>Строковое представление периода</summary>
		public override string ToString()
		{
			string res;
			switch(_pType)
			{
				case PeriodType.Custom:
                    res = string.Format(Properties.Resources.FORMAT_PERIODCUSTOM, _pStart.ToShortDateString(), _pEnd.ToShortDateString());
					break;

				case PeriodType.Day:
					res = _pStart.ToLongDateString();
					break;

				case PeriodType.Month:
                    res = string.Format(Properties.Resources.FORMAT_PERIODMONTH, _pStart.ToString("MMMM"), _pYear);
					break;

				case PeriodType.MonthRange:
					res = string.Format(Properties.Resources.FORMAT_PERIODMONTHRANGE, _pStart.ToString("MMMM"), _pEnd.ToString("MMMM"), _pYear);
					break;

				case PeriodType.Quarter:
					res = string.Format(Properties.Resources.FORMAT_PERIODQARTAL, GetQuarterNum(_pStart.Month), _pYear);
					break;

				case PeriodType.Year:
					res = string.Format(Properties.Resources.FORMAT_PERIODYEAR, _pYear);
					break;

				default:
                    res = string.Format(Properties.Resources.FORMAT_PERIODCUSTOM, _pStart.ToShortDateString(), _pEnd.ToShortDateString());
					break;
			}
			return res;
		}

		#endregion

        private PeriodStruct _baseStruct;

        /// <summary>Сохранение состояния</summary>
        /// <param name="overwrite">Переписать текущее состояние</param>
        public override bool SaveState(bool overwrite)
        {
            if (overwrite)
            {
                _baseStruct = new PeriodStruct
                {
                    Start = _pStart,
                    End = _pEnd
                };
                return true;
            }
            return false;
        }

        /// <summary>Востановить состояние</summary>
        public override void RestoreState()
        {
            _pStart = _baseStruct.Start;
            _pEnd = _baseStruct.End;
            IsChanged = false;
        }
	}
}
