using System;

namespace BusinessObjects
{
	#region Enums

	/// <summary> ��� ������� </summary>
	public enum PeriodType
	{
        /// <summary>������������ ������ ������������ ����� ������</summary>
		Custom,
        /// <summary>������ � ���� ����</summary>
        Day,
        /// <summary>������ � ���� �����</summary>
		Month,
        /// <summary>������ � ��������� �������</summary>
		MonthRange,
        /// <summary>������ � ���� �������</summary>
		Quarter,
        /// <summary>������ � ���� ���</summary>
		Year
	}

	#endregion

    internal struct PeriodStruct
    {
        /// <summary>������ �������</summary>
        public DateTime Start;
        /// <summary>����� �������</summary>
        public DateTime End;
    }
    // TODO: �������� ��������� ����������� ������� � ������� ��������� �������
    /// <summary>������</summary>
    public sealed class Period : BasePropertyChangeSupport
	{
        /// <summary>
        /// �������������� TimeSpan � ���� �� ��������, � �������� ���� ������������ ������� ����, ����� - �� ������ ���������
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
        /// �������������� ���� � TimeSpan
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
		private DateTime _pStart;		//������ �������
		private DateTime _pEnd;			//����� �������
		private PeriodType _pType;		//��� �������
		private int _pYear;				//������� ���

		#region Constructors
		/// <summary>����������� - ������� ������ ���� ���� � ������� ��������� �����</summary>
		public Period()
		{
			SetDay(DateTime.Today);
		}

		/// <summary>����������� - ������� ������ ���� ��� </summary>
        /// <param name="day">����</param>
		public Period(DateTime day)
		{
			SetDay(day);
		}

		/// <summary>����������� - ������� ������ ������������� ��� </summary>
        /// <param name="startDay">������ �������</param>
        /// <param name="endDay">��������� �������</param>
		public Period(DateTime startDay, DateTime endDay)
		{
			SetCustom(startDay, endDay);
		}

		/// <summary>����������� - ������� ������ ���� �����</summary>
        /// <param name="month">����� ������</param>
		public Period(int month)
		{
			SetMonth(month);
		}
		
		/// <summary>����������� - ������� ������ ���� �������� ������� </summary>
        /// <param name="startMonth">����� ������ ��� ������ �������</param>
        /// <param name="endMonth">����� ������ ��� ����� �������</param>
		public Period(int startMonth, int endMonth)
		{
			SetMonthRange(startMonth, endMonth);
		}
		#endregion

		#region Properties

		/// <summary>������ �������</summary>
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

		/// <summary>����� �������</summary>
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

		/// <summary>��� �������</summary>
		public PeriodType Type
		{
			get { return _pType; }
		}

		/// <summary>������� ��� �������. ������������ �� ���� ������ �������</summary>
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

        /// <summary>������������� ������ � ������������ �������� ���</summary>
        /// <param name="startDate">��������� ����</param>
        /// <param name="endDate">�������� ����</param>
		public void SetCustom(DateTime startDate, DateTime endDate)
		{
			if (startDate > endDate) return;
			_pType = PeriodType.Custom;
            _pStart = TrimToStart(startDate.Date);
			_pEnd = TrimToEnd(endDate.Date.AddDays(1));
			_pYear = _pStart.Year;
            IsChanged = true;
		}

		/// <summary>������������� ������ � �������� ����</summary>
		/// <param name="day">��������� ����</param>
		public void SetDay(DateTime day)
		{
			_pType = PeriodType.Day;
            _pStart = TrimToStart(day.Date);
            _pEnd = TrimToEnd(_pStart.AddDays(1));
			_pYear = _pStart.Year;
            IsChanged = true;
		}
        /// <summary>���������� ������ �� ��������� 30 ����</summary>
        public void SetLast30Day()
        {
            SetLast(30, DateTime.Today);
            IsChanged = true;
        }
        /// <summary>���������� ������ �� ��������� n-����</summary>
        /// <param name="count">���������� ����</param>
        /// <param name="day">����� �������</param>
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

		/// <summary>������������� ������ � �������� �����</summary>
		/// <param name="month">����� ��������� ������ (�� 1 �� 12)</param>
		public void SetMonth(int month)
		{
			if (month < 1 || month > 12) return;
			_pType = PeriodType.Month;
			_pStart = TrimToStart(new DateTime(_pYear, month, 1));
			_pEnd = TrimToEnd(_pStart.AddMonths(1));
			_pYear = _pStart.Year;
            IsChanged = true;
		}
		/// <summary>������������� ������ � �������� �������� �������</summary>
        /// <param name="startMonth">��������� ����� (�� 1 �� 12)</param>
        /// <param name="endMonth">�������� ����� (�� 1 �� 12)</param>
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

		/// <summary>������������� ������ � �������� �������</summary>
		/// <param name="quart">����� ��������� �������� (�� 1 �� 4)</param>
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

		/// <summary>������������� ������ � �������� ���</summary>
		/// <param name="year">��������� ���</param>
		public void SetYear(int year)
		{
			if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year) return;
			_pType = PeriodType.Year;
			_pStart = TrimToStart(new DateTime(year, 1, 1));
			_pEnd = TrimToEnd(_pStart.AddYears(1).AddDays(-1));
			_pYear = _pStart.Year;
            IsChanged = true;
		}
        /// <summary>������������� ������ � ��������� ���</summary>
        /// <param name="yearFrom">������</param>
        /// <param name="yearTo">�����</param>
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

		/// <summary>���������� ����� �������� �� ������ ������</summary>
		/// <param name="month">����� ������ (�� 1 �� 12)</param>
		private static int GetQuarterNum(int month)
		{
			if (month < 1 || month > 12) return 0;

			decimal idx = Math.Round(month / 3.0M + 0.3M, 0);
			return (int)idx;
		}

		/// <summary>��������� ������������� �������</summary>
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

        /// <summary>���������� ���������</summary>
        /// <param name="overwrite">���������� ������� ���������</param>
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

        /// <summary>����������� ���������</summary>
        public override void RestoreState()
        {
            _pStart = _baseStruct.Start;
            _pEnd = _baseStruct.End;
            IsChanged = false;
        }
	}
}
