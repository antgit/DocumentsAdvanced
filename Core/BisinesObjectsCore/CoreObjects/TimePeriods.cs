using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Внутренняя структура объекта "График работы"</summary>
    internal struct TimePeriodStruct
    {
        /// <summary>Понедельник - активен день</summary>
        public int MondayW;
        /// <summary>Вторник - активен день</summary>
        public int TuesdayW;
        /// <summary>Среда - активен день</summary>
        public int WednesdayW;
        /// <summary>Четверг - активен день</summary>
        public int ThursdayW;
        /// <summary>Пятница - активен день</summary>
        public int FridayW;
        /// <summary>Суббота - активен день</summary>
        public int SaturdayW;
        /// <summary>Воскресенье - активен день</summary>
        public int SundayW;

        /// <summary>Понедельник - началов в часов </summary>
        public int MondaySH;
        /// <summary>Понедельник - начало миниут</summary>
        public int MondaySM;
        /// <summary>Понедельник - конец в часов</summary>
        public int MondayEH;
        /// <summary>Понедельник - конец минут</summary>
        public int MondayEM;
        /// <summary>Вторник - началов в часов</summary>
        public int TuesdaySH;
        /// <summary>Вторник - начало миниут</summary>
        public int TuesdaySM;
        /// <summary>Вторник - конец в часов</summary>
        public int TuesdayEH;
        /// <summary>Вторник - конец минут</summary>
        public int TuesdayEM;
        /// <summary>Среда - началов в часов</summary>
        public int WednesdaySH;
        /// <summary>Среда - начало минут</summary>
        public int WednesdaySM;
        /// <summary>Среда - конец минут</summary>
        public int WednesdayEM;
        /// <summary>Среда - конец часов</summary>
        public int WednesdayEH;
        /// <summary>Четверг - началов в часов</summary>
        public int ThursdaySH;
        /// <summary>Четверг - начало минут</summary>
        public int ThursdaySM;
        /// <summary>Четверг - конец минут</summary>
        public int ThursdayEM;
        /// <summary>Четверг - конец часов</summary>
        public int ThursdayEH;
        /// <summary>Пятница - началов в часов</summary>
        public int FridaySH;
        /// <summary>Пятница - начало минут</summary>
        public int FridaySM;
        /// <summary>Пятница - конец в часов</summary>
        public int FridayEH;
        /// <summary>Пятница - конец минут</summary>
        public int FridayEM;
        /// <summary>Суббота - началов в часов</summary>
        public int SaturdaySH;
        /// <summary>Суббота - начало минут</summary>
        public int SaturdaySM;
        /// <summary>Суббота - конец в часов</summary>
        public int SaturdayEH;
        /// <summary>Суббота - конец минут</summary>
        public int SaturdayEM;
        /// <summary>Воскресенье -началов в часов </summary>
        public int SundaySH;
        /// <summary>Воскресенье - конец в часов</summary>
        public int SundayEH;
        /// <summary>Воскресенье - начало минут</summary>
        public int SundaySM;
        /// <summary>Воскресенье - конец минут</summary>
        public int SundayEM;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }

    /// <summary>
    /// График работы
    /// </summary>
    public sealed class TimePeriod : BaseCore<TimePeriod>, IChains<TimePeriod>, IEquatable<TimePeriod>,
        ICodes<TimePeriod>, IHierarchySupport, ICompanyOwner
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Рабочий график, соответствует значению 1</summary>
        public const int KINDVALUE_WORK = 1;
        /// <summary>Рабочий график, соответствует значению 1</summary>
        public const int KINDVALUE_BREAK = 2;

        /// <summary>График работы, соответствует значению 6094849</summary>
        public const int KINDID_WORK = 6094849;
        /// <summary>График перерывов, соответствует значению 6094850</summary>
        public const int KINDID_BREAK = 6094850;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<TimePeriod>.Equals(TimePeriod other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public TimePeriod()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.TimePeriod;
        }
        protected override void CopyValue(TimePeriod template)
        {
            base.CopyValue(template);
            MondayW = template.MondayW;
            TuesdayW = template.TuesdayW;
            WednesdayW = template.WednesdayW;
            ThursdayW = template.ThursdayW;
            FridayW = template.FridayW;
            SaturdayW = template.SaturdayW;
            SundayW = template.SundayW;
            MondaySH = template.MondaySH;
            MondaySM = template.MondaySM;
            MondayEH = template.MondayEH;
            MondayEM = template.MondayEM;
            TuesdaySH = template.TuesdaySH;
            TuesdaySM = template.TuesdaySM;
            TuesdayEH = template.TuesdayEH;
            TuesdayEM = template.TuesdayEM;
            WednesdaySH = template.WednesdaySH;
            WednesdaySM = template.WednesdaySM;
            WednesdayEH = template.WednesdayEH;
            WednesdayEM = template.WednesdayEM;
            ThursdaySH = template.ThursdaySH;
            ThursdaySM = template.ThursdaySM;
            ThursdayEH = template.ThursdayEH;
            ThursdayEM = template.ThursdayEM;
            FridaySH = template.FridaySH;
            FridaySM = template.FridaySM;
            FridayEH = template.FridayEH;
            FridayEM = template.FridayEM;
            SaturdaySH = template.SaturdaySH;
            SaturdaySM = template.SaturdaySM;
            SaturdayEH = template.SaturdayEH;
            SaturdayEM = template.SaturdayEM;
            SundaySH = template.SundaySH;
            SundaySM = template.SundaySM;
            SundayEH = template.SundayEH;
            SundayEM = template.SundayEM;
            MyCompanyId = template.MyCompanyId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override TimePeriod Clone(bool endInit)
        {
            TimePeriod obj = base.Clone(false);
            obj.MondayW = MondayW;
            obj.TuesdayW = TuesdayW;
            obj.WednesdayW = WednesdayW;
            obj.ThursdayW = ThursdayW;
            obj.FridayW = FridayW;
            obj.SaturdayW = SaturdayW;
            obj.SundayW = SundayW;
            obj.MondaySH = MondaySH;
            obj.MondaySM = MondaySM;
            obj.MondayEH = MondayEH;
            obj.MondayEM = MondayEM;
            obj.TuesdaySH = TuesdaySH;
            obj.TuesdaySM = TuesdaySM;
            obj.TuesdayEH = TuesdayEH;
            obj.TuesdayEM = TuesdayEM;
            obj.WednesdaySH = WednesdaySH;
            obj.WednesdaySM = WednesdaySM;
            obj.WednesdayEH = WednesdayEH;
            obj.WednesdayEM = WednesdayEM;
            obj.ThursdaySH = ThursdaySH;
            obj.ThursdaySM = ThursdaySM;
            obj.ThursdayEH = ThursdayEH;
            obj.ThursdayEM = ThursdayEM;
            obj.FridaySH = FridaySH;
            obj.FridaySM = FridaySM;
            obj.FridayEH = FridayEH;
            obj.FridayEM = FridayEM;
            obj.SaturdaySH = SaturdaySH;
            obj.SaturdaySM = SaturdaySM;
            obj.SaturdayEH = SaturdayEH;
            obj.SaturdayEM = SaturdayEM;
            obj.SundaySH = SundaySH;
            obj.SundaySM = SundaySM;
            obj.SundayEH = SundayEH;
            obj.SundayEM = SundayEM;
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства

        private int _mondayW;
        /// <summary>Понедельник - активен</summary>
        public int MondayW
        {
            get { return _mondayW; }
            set
            {
                if (value == _mondayW) return;
                OnPropertyChanging(GlobalPropertyNames.MondayW);
                _mondayW = value;
                OnPropertyChanged(GlobalPropertyNames.MondayW);
            }
        }

        private int _tuesdayW;
        /// <summary>Вторник - активен</summary>
        public int TuesdayW
        {
            get { return _tuesdayW; }
            set
            {
                if (value == _tuesdayW) return;
                OnPropertyChanging(GlobalPropertyNames.TuesdayW);
                _tuesdayW = value;
                OnPropertyChanged(GlobalPropertyNames.TuesdayW);
            }
        }

        private int _wednesdayW;
        /// <summary>Среда - активен</summary>
        public int WednesdayW
        {
            get { return _wednesdayW; }
            set
            {
                if (value == _wednesdayW) return;
                OnPropertyChanging(GlobalPropertyNames.WednesdayW);
                _wednesdayW = value;
                OnPropertyChanged(GlobalPropertyNames.WednesdayW);
            }
        }

        private int _thursdayW;
        /// <summary>Четверг - активен</summary>
        public int ThursdayW
        {
            get { return _thursdayW; }
            set
            {
                if (value == _thursdayW) return;
                OnPropertyChanging(GlobalPropertyNames.ThursdayW);
                _thursdayW = value;
                OnPropertyChanged(GlobalPropertyNames.ThursdayW);
            }
        }
        
        private int _fridayW;
        /// <summary>Пятница - активен</summary>
        public int FridayW
        {
            get { return _fridayW; }
            set
            {
                if (value == _fridayW) return;
                OnPropertyChanging(GlobalPropertyNames.FridayW);
                _fridayW = value;
                OnPropertyChanged(GlobalPropertyNames.FridayW);
            }
        }
        
        private int _saturdayW;
        /// <summary>Суббота  - активен</summary>
        public int SaturdayW
        {
            get { return _saturdayW; }
            set
            {
                if (value == _saturdayW) return;
                OnPropertyChanging(GlobalPropertyNames.SaturdayW);
                _saturdayW = value;
                OnPropertyChanged(GlobalPropertyNames.SaturdayW);
            }
        }

        private int _sundayW;
        /// <summary>Воскресенье - активен</summary>
        public int SundayW
        {
            get { return _sundayW; }
            set
            {
                if (value == _sundayW) return;
                OnPropertyChanging(GlobalPropertyNames.SundayW);
                _sundayW = value;
                OnPropertyChanged(GlobalPropertyNames.SundayW);
            }
        }

        private int _mondaySH;
        /// <summary>Начало активности в часах в понедельник</summary>
        public int MondaySH
        {
            get { return _mondaySH; }
            set
            {
               if (value == _mondaySH) return;
                OnPropertyChanging(GlobalPropertyNames.MondaySH);
                _mondaySH = value;
                OnPropertyChanged(GlobalPropertyNames.MondaySH);
            }
        }
        
        private int _mondaySM;
        /// <summary>Начало активности в минутах в понедельник</summary>
        public int MondaySM
        {
            get { return _mondaySM; }
            set
            {
                if (value == _mondaySM) return;
                OnPropertyChanging(GlobalPropertyNames.MondaySM);
                _mondaySM = value;
                OnPropertyChanged(GlobalPropertyNames.MondaySM);
            }
        }

        private int _mondayEH;
        /// <summary>Окончание активности в часах в понедельник</summary>
        public int MondayEH
        {
            get { return _mondayEH; }
            set
            {
                if (value == _mondayEH) return;
                OnPropertyChanging(GlobalPropertyNames.MondayEH);
                _mondayEH = value;
                OnPropertyChanged(GlobalPropertyNames.MondayEH);
            }
        }
        
        private int _mondayEM;
        /// <summary>Окончание активности в минутах в понедельник</summary>
        public int MondayEM
        {
            get { return _mondayEM; }
            set
            {
                if (value == _mondayEM) return;
                OnPropertyChanging(GlobalPropertyNames.MondayEM);
                _mondayEM = value;
                OnPropertyChanged(GlobalPropertyNames.MondayEM);
            }
        }
        
        private int _tuesdaySH;
        /// <summary>Начало активности в часах во вторник</summary>
        public int TuesdaySH
        {
            get { return _tuesdaySH; }
            set
            {
                if (value == _tuesdaySH) return;
                OnPropertyChanging(GlobalPropertyNames.TuesdaySH);
                _tuesdaySH = value;
                OnPropertyChanged(GlobalPropertyNames.TuesdaySH);
            }
        }
        
        private int _tuesdaySM;
        /// <summary>Начало активности в минутах во вторник</summary>
        public int TuesdaySM
        {
            get { return _tuesdaySM; }
            set
            {
                if (value == _tuesdaySM) return;
                OnPropertyChanging(GlobalPropertyNames.TuesdaySM);
                _tuesdaySM = value;
                OnPropertyChanged(GlobalPropertyNames.TuesdaySM);
            }
        }
        
        private int _tuesdayEH;
        /// <summary>Окончание активности в часах во вторник</summary>
        public int TuesdayEH
        {
            get { return _tuesdayEH; }
            set
            {
                if (value == _tuesdayEH) return;
                OnPropertyChanging(GlobalPropertyNames.TuesdayEH);
                _tuesdayEH = value;
                OnPropertyChanged(GlobalPropertyNames.TuesdayEH);
            }
        }
        
        private int _tuesdayEM;
        /// <summary>Окончание активности в минутах во вторник</summary>
        public int TuesdayEM
        {
            get { return _tuesdayEM; }
            set
            {
                if (value == _tuesdayEM) return;
                OnPropertyChanging(GlobalPropertyNames.TuesdayEM);
                _tuesdayEM = value;
                OnPropertyChanged(GlobalPropertyNames.TuesdayEM);
            }
        }
        
        private int _wednesdaySH;
        /// <summary>Начало активности в часах в среду</summary>
        public int WednesdaySH
        {
            get { return _wednesdaySH; }
            set
            {
                if (value == _wednesdaySH) return;
                OnPropertyChanging(GlobalPropertyNames.WednesdaySH);
                _wednesdaySH = value;
                OnPropertyChanged(GlobalPropertyNames.WednesdaySH);
            }
        }
        
        private int _wednesdaySM;
        /// <summary>Начало активности в минутах в среду</summary>
        public int WednesdaySM
        {
            get { return _wednesdaySM; }
            set
            {
                if (value == _wednesdaySM) return;
                OnPropertyChanging(GlobalPropertyNames.WednesdaySM);
                _wednesdaySM = value;
                OnPropertyChanged(GlobalPropertyNames.WednesdaySM);
            }
        }
        
        private int _wednesdayEM;
        /// <summary>Окончание активности в минутах в среду</summary>
        public int WednesdayEM
        {
            get { return _wednesdayEM; }
            set
            {
                if (value == _wednesdayEM) return;
                OnPropertyChanging(GlobalPropertyNames.WednesdayEM);
                _wednesdayEM = value;
                OnPropertyChanged(GlobalPropertyNames.WednesdayEM);
            }
        }
        
        private int _wednesdayEH;
        /// <summary>Окончание активности в часах в среду</summary>
        public int WednesdayEH
        {
            get { return _wednesdayEH; }
            set
            {
                if (value == _wednesdayEH) return;
                OnPropertyChanging(GlobalPropertyNames.WednesdayEH);
                _wednesdayEH = value;
                OnPropertyChanged(GlobalPropertyNames.WednesdayEH);
            }
        }
        
        private int _thursdaySH;
        /// <summary>Начало активности в часах в четверг</summary>
        public int ThursdaySH
        {
            get { return _thursdaySH; }
            set
            {
                if (value == _thursdaySH) return;
                OnPropertyChanging(GlobalPropertyNames.ThursdaySH);
                _thursdaySH = value;
                OnPropertyChanged(GlobalPropertyNames.ThursdaySH);
            }
        }
        
        private int _thursdaySM;
        /// <summary>Начало активности в минутах в четверг</summary>
        public int ThursdaySM
        {
            get { return _thursdaySM; }
            set
            {
                if (value == _thursdaySM) return;
                OnPropertyChanging(GlobalPropertyNames.ThursdaySM);
                _thursdaySM = value;
                OnPropertyChanged(GlobalPropertyNames.ThursdaySM);
            }
        }
        
        private int _thursdayEM;
        /// <summary>Окончание активности в минутах в четверг</summary>
        public int ThursdayEM
        {
            get { return _thursdayEM; }
            set
            {
                if (value == _thursdayEM) return;
                OnPropertyChanging(GlobalPropertyNames.ThursdayEM);
                _thursdayEM = value;
                OnPropertyChanged(GlobalPropertyNames.ThursdayEM);
            }
        }

        private int _thursdayEH;
        /// <summary>Окончание активности в часах в черверг</summary>
        public int ThursdayEH
        {
            get { return _thursdayEH; }
            set
            {
                if (value == _thursdayEH) return;
                OnPropertyChanging(GlobalPropertyNames.ThursdayEH);
                _thursdayEH = value;
                OnPropertyChanged(GlobalPropertyNames.ThursdayEH);
            }
        }
        
        private int _fridaySH;
        /// <summary>Начало активности в часах в пятницу</summary>
        public int FridaySH
        {
            get { return _fridaySH; }
            set
            {
                if (value == _fridaySH) return;
                OnPropertyChanging(GlobalPropertyNames.FridaySH);
                _fridaySH = value;
                OnPropertyChanged(GlobalPropertyNames.FridaySH);
            }
        }
        
        private int _fridaySM;
        /// <summary>Начало активности в минутах в пятницу</summary>
        public int FridaySM
        {
            get { return _fridaySM; }
            set
            {
                if (value == _fridaySM) return;
                OnPropertyChanging(GlobalPropertyNames.FridaySM);
                _fridaySM = value;
                OnPropertyChanged(GlobalPropertyNames.FridaySM);
            }
        }
        
        private int _fridayEH;
        /// <summary>Окончание активности в часах в пятницу</summary>
        public int FridayEH
        {
            get { return _fridayEH; }
            set
            {
                if (value == _fridayEH) return;
                OnPropertyChanging(GlobalPropertyNames.FridayEH);
                _fridayEH = value;
                OnPropertyChanged(GlobalPropertyNames.FridayEH);
            }
        }
        
        private int _fridayEM;
        /// <summary>Окончание активности в минутах в пятницу</summary>
        public int FridayEM
        {
            get { return _fridayEM; }
            set
            {
                if (value == _fridayEM) return;
                OnPropertyChanging(GlobalPropertyNames.FridayEM);
                _fridayEM = value;
                OnPropertyChanged(GlobalPropertyNames.FridayEM);
            }
        }
        
        private int _saturdaySH;
        /// <summary>Начало активности в часах в субботу</summary>
        public int SaturdaySH
        {
            get { return _saturdaySH; }
            set
            {
                if (value == _saturdaySH) return;
                OnPropertyChanging(GlobalPropertyNames.SaturdaySH);
                _saturdaySH = value;
                OnPropertyChanged(GlobalPropertyNames.SaturdaySH);
            }
        }
        
        private int _saturdaySM;
        /// <summary>Начало активности в минутах в субботу</summary>
        public int SaturdaySM
        {
            get { return _saturdaySM; }
            set
            {
                if (value == _saturdaySM) return;
                OnPropertyChanging(GlobalPropertyNames.SaturdaySM);
                _saturdaySM = value;
                OnPropertyChanged(GlobalPropertyNames.SaturdaySM);
            }
        }

        private int _saturdayEH;
        /// <summary>Окончание активности в часах в субботу</summary>
        public int SaturdayEH
        {
            get { return _saturdayEH; }
            set
            {
                if (value == _saturdayEH) return;
                OnPropertyChanging(GlobalPropertyNames.SaturdayEH);
                _saturdayEH = value;
                OnPropertyChanged(GlobalPropertyNames.SaturdayEH);
            }
        }

        private int _saturdayEM;
        /// <summary>Окончание активности в минутах в субботу</summary>
        public int SaturdayEM
        {
            get { return _saturdayEM; }
            set
            {
                if (value == _saturdayEM) return;
                OnPropertyChanging(GlobalPropertyNames.SaturdayEM);
                _saturdayEM = value;
                OnPropertyChanged(GlobalPropertyNames.SaturdayEM);
            }
        }

        private int _sundaySH;
        /// <summary>Начало активности в часах в воскресенье</summary>
        public int SundaySH
        {
            get { return _sundaySH; }
            set
            {
                if (value == _sundaySH) return;
                OnPropertyChanging(GlobalPropertyNames.SundaySH);
                _sundaySH = value;
                OnPropertyChanged(GlobalPropertyNames.SundaySH);
            }
        }

        private int _sundayEH;
        /// <summary>Окончание активности в часах в воскресенье</summary>
        public int SundayEH
        {
            get { return _sundayEH; }
            set
            {
                if (value == _sundayEH) return;
                OnPropertyChanging(GlobalPropertyNames.SundayEH);
                _sundayEH = value;
                OnPropertyChanged(GlobalPropertyNames.SundayEH);
            }
        }

        private int _sundaySM;
        /// <summary>Начало активности в минутах в воскресенье</summary>
        public int SundaySM
        {
            get { return _sundaySM; }
            set
            {
                if (value == _sundaySM) return;
                OnPropertyChanging(GlobalPropertyNames.SundaySM);
                _sundaySM = value;
                OnPropertyChanged(GlobalPropertyNames.SundaySM);
            }
        }

        private int _sundayEM;
        /// <summary>Окончание активности в минутах в воскресенье</summary>
        public int SundayEM
        {
            get { return _sundayEM; }
            set
            {
                if (value == _sundayEM) return;
                OnPropertyChanging(GlobalPropertyNames.SundayEM);
                _sundayEM = value;
                OnPropertyChanged(GlobalPropertyNames.SundayEM);
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
        /// <summary>
        /// Разрешено в понедельник
        /// </summary>
        public bool IsMonday
        {
            get { return _mondayW == 1; }
        }
        /// <summary>
        /// Разрешено во вторник
        /// </summary>
        public bool IsTuesday
        {
            get { return _tuesdayW == 1; }
        }
        /// <summary>
        /// Разрешено во среду
        /// </summary>
        public bool IsWednesday
        {
            get { return _wednesdayW == 1; }
        }
        /// <summary>
        /// Разрешено в четверг
        /// </summary>
        public bool IsThursday
        {
            get { return _thursdayW == 1; }
        }
        /// <summary>
        /// Разрешено в пятницу
        /// </summary>
        public bool IsFriday
        {
            get { return _fridayW == 1; }
        }
        /// <summary>
        /// Разрешено в субботу
        /// </summary>
        public bool IsSaturday
        {
            get { return _saturdayW == 1; }
        }
        /// <summary>
        /// Разрешено в воскресенье
        /// </summary>
        public bool IsSunday
        {
            get { return _sundayW == 1; }
        }
        /// <summary>
        /// Разрешен ли день недели
        /// </summary>
        /// <param name="dayWeek"></param>
        /// <returns></returns>
        public bool IsAllowOnWeekDay(DayOfWeek dayWeek)
        {
            switch (dayWeek)
            {
                case DayOfWeek.Monday:
                    return IsMonday;
                case DayOfWeek.Thursday:
                    return IsThursday;
                case DayOfWeek.Wednesday:
                    return IsWednesday;
                case DayOfWeek.Friday:
                    return IsFriday;
                case DayOfWeek.Saturday:
                    return IsSaturday;
                case DayOfWeek.Sunday:
                    return IsSunday;
                case DayOfWeek.Tuesday:
                    return IsTuesday;
                default: return false;
            }
        }
        /// <summary>
        /// Начало в часах для дня недели
        /// </summary>
        /// <param name="dayWeek">День недели</param>
        /// <returns></returns>
        public int GetStartHour(DayOfWeek dayWeek)
        {
            switch (dayWeek)
            {
                case DayOfWeek.Monday:
                    return MondaySH;
                case DayOfWeek.Thursday:
                    return ThursdaySH;
                case DayOfWeek.Wednesday:
                    return WednesdaySH;
                case DayOfWeek.Friday:
                    return FridaySH;
                case DayOfWeek.Saturday:
                    return SaturdaySH;
                case DayOfWeek.Sunday:
                    return SundaySH;
                case DayOfWeek.Tuesday:
                    return TuesdaySH;
                default: return 0;
            }
        }
        /// <summary>
        /// Начало в минутах для дня недели
        /// </summary>
        /// <param name="dayWeek">День недели</param>
        /// <returns></returns>
        public int GetStartMinute(DayOfWeek dayWeek)
        {
            switch (dayWeek)
            {
                case DayOfWeek.Monday:
                    return MondaySM;
                case DayOfWeek.Thursday:
                    return ThursdaySM;
                case DayOfWeek.Wednesday:
                    return WednesdaySM;
                case DayOfWeek.Friday:
                    return FridaySM;
                case DayOfWeek.Saturday:
                    return SaturdaySM;
                case DayOfWeek.Sunday:
                    return SundaySM;
                case DayOfWeek.Tuesday:
                    return TuesdaySM;
                default: return 0;
            }
        }
        /// <summary>
        /// Окончание в часах для дня недели
        /// </summary>
        /// <param name="dayWeek">День недели</param>
        /// <returns></returns>
        public int GetEndHour(DayOfWeek dayWeek)
        {
            switch (dayWeek)
            {
                case DayOfWeek.Monday:
                    return MondayEH;
                case DayOfWeek.Thursday:
                    return ThursdayEH;
                case DayOfWeek.Wednesday:
                    return WednesdayEH;
                case DayOfWeek.Friday:
                    return FridayEH;
                case DayOfWeek.Saturday:
                    return SaturdayEH;
                case DayOfWeek.Sunday:
                    return SundayEH;
                case DayOfWeek.Tuesday:
                    return TuesdayEH;
                default: return 0;
            }
        }
        /// <summary>
        /// Окончание в минитах для дня недели
        /// </summary>
        /// <param name="dayWeek">День недели</param>
        /// <returns></returns>
        public int GetEndMinute(DayOfWeek dayWeek)
        {
            switch (dayWeek)
            {
                case DayOfWeek.Monday:
                    return MondayEM;
                case DayOfWeek.Thursday:
                    return ThursdayEM;
                case DayOfWeek.Wednesday:
                    return WednesdayEM;
                case DayOfWeek.Friday:
                    return FridayEM;
                case DayOfWeek.Saturday:
                    return SaturdayEM;
                case DayOfWeek.Sunday:
                    return SundayEM;
                case DayOfWeek.Tuesday:
                    return TuesdayEM;
                default: return 0;
            }
        }

        /// <summary>
        /// Дата на основе данных начала работы
        /// </summary>
        /// <param name="dayWeek">День недели</param>
        /// <returns></returns>
        public DateTime GetStartValue(DayOfWeek dayWeek)
        {
            int h = GetStartHour(dayWeek);
            int m = GetStartMinute(dayWeek);

            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, h, m, 0);
        }
        /// <summary>
        /// Дата на сонове окончания работы
        /// </summary>
        /// <param name="dayWeek">День недели</param>
        /// <returns></returns>
        public DateTime GetEndValue(DayOfWeek dayWeek)
        {
            int h = GetEndHour(dayWeek);
            int m = GetEndMinute(dayWeek);

            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, h, m, 0);
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

            writer.WriteAttributeString(GlobalPropertyNames.MondayW, _mondayW.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.TuesdayW, _tuesdayW.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.WednesdayW, _wednesdayW.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.ThursdayW, _thursdayW.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.FridayW, _fridayW.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.SaturdayW, _saturdayW.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.SundayW, _sundayW.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.MondaySH, _mondaySH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.MondaySM, _mondaySM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.MondayEH, _mondayEH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.MondayEM, _mondayEM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.TuesdaySH, _tuesdaySH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.TuesdaySM, _tuesdaySM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.TuesdayEH, _tuesdayEH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.TuesdayEM, _tuesdayEM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.WednesdaySH, _wednesdaySH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.WednesdaySM, _wednesdaySM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.WednesdayEH, _wednesdayEH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.WednesdayEM, _wednesdayEM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.ThursdaySH, _thursdaySH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.ThursdaySM, _thursdaySM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.ThursdayEH, _thursdayEH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.ThursdayEM, _thursdayEM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.FridaySH, _fridaySH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.FridaySM, _fridaySM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.FridayEH, _fridayEH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.FridayEM, _fridayEM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.SaturdaySH, _saturdaySH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.SaturdaySM, _saturdaySM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.SaturdayEH, _saturdayEH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.SaturdayEM, _saturdayEM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.SundaySH, _sundaySH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.SundaySM, _sundaySM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.SundayEH, _sundayEH.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.SundayEM, _sundayEM.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.MondayW) != null) _mondayW = Int32.Parse(reader[GlobalPropertyNames.MondayW]);
            if (reader.GetAttribute(GlobalPropertyNames.TuesdayW) != null) _tuesdayW = Int32.Parse(reader[GlobalPropertyNames.TuesdayW]);
            if (reader.GetAttribute(GlobalPropertyNames.WednesdayW) != null) _wednesdayW = Int32.Parse(reader[GlobalPropertyNames.WednesdayW]);
            if (reader.GetAttribute(GlobalPropertyNames.ThursdayW) != null) _thursdayW = Int32.Parse(reader[GlobalPropertyNames.ThursdayW]);
            if (reader.GetAttribute(GlobalPropertyNames.FridayW) != null) _fridayW = Int32.Parse(reader[GlobalPropertyNames.FridayW]);
            if (reader.GetAttribute(GlobalPropertyNames.SaturdayW) != null) _saturdayW = Int32.Parse(reader[GlobalPropertyNames.SaturdayW]);
            if (reader.GetAttribute(GlobalPropertyNames.SundayW) != null) _sundayW = Int32.Parse(reader[GlobalPropertyNames.SundayW]);
            if (reader.GetAttribute(GlobalPropertyNames.MondaySH) != null) _mondaySH = Int32.Parse(reader[GlobalPropertyNames.MondaySH]);
            if (reader.GetAttribute(GlobalPropertyNames.MondaySM) != null) _mondaySM = Int32.Parse(reader[GlobalPropertyNames.MondaySM]);
            if (reader.GetAttribute(GlobalPropertyNames.MondayEH) != null) _mondayEH = Int32.Parse(reader[GlobalPropertyNames.MondayEH]);
            if (reader.GetAttribute(GlobalPropertyNames.MondayEM) != null) _mondayEM = Int32.Parse(reader[GlobalPropertyNames.MondayEM]);
            if (reader.GetAttribute(GlobalPropertyNames.TuesdaySH) != null) _tuesdaySH = Int32.Parse(reader[GlobalPropertyNames.TuesdaySH]);
            if (reader.GetAttribute(GlobalPropertyNames.TuesdaySM) != null) _tuesdaySM = Int32.Parse(reader[GlobalPropertyNames.TuesdaySM]);
            if (reader.GetAttribute(GlobalPropertyNames.TuesdayEH) != null) _tuesdayEH = Int32.Parse(reader[GlobalPropertyNames.TuesdayEH]);
            if (reader.GetAttribute(GlobalPropertyNames.TuesdayEM) != null) _tuesdayEM = Int32.Parse(reader[GlobalPropertyNames.TuesdayEM]);
            if (reader.GetAttribute(GlobalPropertyNames.WednesdaySH) != null) _wednesdaySH = Int32.Parse(reader[GlobalPropertyNames.WednesdaySH]);
            if (reader.GetAttribute(GlobalPropertyNames.WednesdaySM) != null) _wednesdaySM = Int32.Parse(reader[GlobalPropertyNames.WednesdaySM]);
            if (reader.GetAttribute(GlobalPropertyNames.WednesdayEH) != null) _wednesdayEH = Int32.Parse(reader[GlobalPropertyNames.WednesdayEH]);
            if (reader.GetAttribute(GlobalPropertyNames.WednesdayEM) != null) _wednesdayEM = Int32.Parse(reader[GlobalPropertyNames.WednesdayEM]);
            if (reader.GetAttribute(GlobalPropertyNames.ThursdaySH) != null) _thursdaySH = Int32.Parse(reader[GlobalPropertyNames.ThursdaySH]);
            if (reader.GetAttribute(GlobalPropertyNames.ThursdaySM) != null) _thursdaySM = Int32.Parse(reader[GlobalPropertyNames.ThursdaySM]);
            if (reader.GetAttribute(GlobalPropertyNames.ThursdayEH) != null) _thursdayEH = Int32.Parse(reader[GlobalPropertyNames.ThursdayEH]);
            if (reader.GetAttribute(GlobalPropertyNames.ThursdayEM) != null) _thursdayEM = Int32.Parse(reader[GlobalPropertyNames.ThursdayEM]);
            if (reader.GetAttribute(GlobalPropertyNames.FridaySH) != null) _fridaySH = Int32.Parse(reader[GlobalPropertyNames.FridaySH]);
            if (reader.GetAttribute(GlobalPropertyNames.FridaySM) != null) _fridaySM = Int32.Parse(reader[GlobalPropertyNames.FridaySM]);
            if (reader.GetAttribute(GlobalPropertyNames.FridayEH) != null) _fridayEH = Int32.Parse(reader[GlobalPropertyNames.FridayEH]);
            if (reader.GetAttribute(GlobalPropertyNames.FridayEM) != null) _fridayEM = Int32.Parse(reader[GlobalPropertyNames.FridayEM]);
            if (reader.GetAttribute(GlobalPropertyNames.SaturdaySH) != null) _saturdaySH = Int32.Parse(reader[GlobalPropertyNames.SaturdaySH]);
            if (reader.GetAttribute(GlobalPropertyNames.SaturdaySM) != null) _saturdaySM = Int32.Parse(reader[GlobalPropertyNames.SaturdaySM]);
            if (reader.GetAttribute(GlobalPropertyNames.SaturdayEH) != null) _saturdayEH = Int32.Parse(reader[GlobalPropertyNames.SaturdayEH]);
            if (reader.GetAttribute(GlobalPropertyNames.SaturdayEM) != null) _saturdayEM = Int32.Parse(reader[GlobalPropertyNames.SaturdayEM]);
            if (reader.GetAttribute(GlobalPropertyNames.SundaySH) != null) _sundaySH = Int32.Parse(reader[GlobalPropertyNames.SundaySH]);
            if (reader.GetAttribute(GlobalPropertyNames.SundaySM) != null) _sundaySM = Int32.Parse(reader[GlobalPropertyNames.SundaySM]);
            if (reader.GetAttribute(GlobalPropertyNames.SundayEH) != null) _sundayEH = Int32.Parse(reader[GlobalPropertyNames.SundayEH]);
            if (reader.GetAttribute(GlobalPropertyNames.SundayEM) != null) _sundayEM = Int32.Parse(reader[GlobalPropertyNames.SundayEM]);
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion

        #region Состояние
        TimePeriodStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new TimePeriodStruct
                {
                    MondayW = _mondayW,
                    ThursdayW = _thursdayW,
                    WednesdayW = _wednesdayW,
                    TuesdayW = _thursdayW,
                    FridayW = _fridayW,
                    SaturdayW = _saturdayW,
                    SundayW = _sundayW,
                    MondaySH = _mondaySH,
                    MondaySM = _mondaySM,
                    MondayEH = _mondayEH,
                    MondayEM = _mondayEM,
                    TuesdaySH = _tuesdaySH,
                    TuesdaySM = _tuesdaySM,
                    TuesdayEH = _tuesdayEH,
                    TuesdayEM = _tuesdayEM,
                    WednesdaySH = _wednesdaySH,
                    WednesdaySM = _wednesdaySM,
                    WednesdayEH = _wednesdayEH,
                    WednesdayEM = _wednesdayEM,
                    ThursdaySH = _thursdaySH,
                    ThursdaySM = _thursdaySM,
                    ThursdayEH = _thursdayEH,
                    ThursdayEM = _thursdayEM,
                    FridaySH = _fridaySH,
                    FridaySM = _fridaySM,
                    FridayEH = _fridayEH,
                    FridayEM = _fridayEM,
                    SaturdaySH = _saturdaySH,
                    SaturdaySM = _saturdaySM,
                    SaturdayEH = _saturdayEH,
                    SaturdayEM = _saturdayEM,
                    SundaySH = _sundaySH,
                    SundaySM = _sundaySM,
                    SundayEH = _sundayEH,
                    SundayEM = _sundayEM,
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
            MondayW = _baseStruct.MondayW;
            ThursdayW = _baseStruct.ThursdayW;
            WednesdayW = _baseStruct.WednesdayW;
            TuesdayW = _baseStruct.ThursdayW;
            FridayW = _baseStruct.FridayW;
            SaturdayW = _baseStruct.SaturdayW;
            SundayW = _baseStruct.SundayW;
            MondaySH = _baseStruct.MondaySH;
            MondaySM = _baseStruct.MondaySM;
            MondayEH = _baseStruct.MondayEH;
            MondayEM = _baseStruct.MondayEM;
            TuesdaySH = _baseStruct.TuesdaySH;
            TuesdaySM = _baseStruct.TuesdaySM;
            TuesdayEH = _baseStruct.TuesdayEH;
            TuesdayEM = _baseStruct.TuesdayEM;
            WednesdaySH = _baseStruct.WednesdaySH;
            WednesdaySM = _baseStruct.WednesdaySM;
            WednesdayEH = _baseStruct.WednesdayEH;
            WednesdayEM = _baseStruct.WednesdayEM;
            ThursdaySH = _baseStruct.ThursdaySH;
            ThursdaySM = _baseStruct.ThursdaySM;
            ThursdayEH = _baseStruct.ThursdayEH;
            ThursdayEM = _baseStruct.ThursdayEM;
            FridaySH = _baseStruct.FridaySH;
            FridaySM = _baseStruct.FridaySM;
            FridayEH = _baseStruct.FridayEH;
            FridayEM = _baseStruct.FridayEM;
            SaturdaySH = _baseStruct.SaturdaySH;
            SaturdaySM = _baseStruct.SaturdaySM;
            SaturdayEH = _baseStruct.SaturdayEH;
            SaturdayEM = _baseStruct.SaturdayEM;
            SundaySH = _baseStruct.SundaySH;
            SundaySM = _baseStruct.SundaySM;
            SundayEH = _baseStruct.SundayEH;
            SundayEM = _baseStruct.SundayEM;
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
                _mondayW = reader.GetInt32(17);
                _thursdayW = reader.GetInt32(18);
                _wednesdayW = reader.GetInt32(19);
                _tuesdayW = reader.GetInt32(20);
                _fridayW = reader.GetInt32(21);
                _saturdayW = reader.GetInt32(22);
                _sundayW = reader.GetInt32(23);
                _mondaySH  = reader.GetInt32(24);
                _mondaySM = reader.GetInt32(25);
                _mondayEH = reader.GetInt32(26);
                _mondayEM = reader.GetInt32(27);
                _tuesdaySH = reader.GetInt32(28);
                _tuesdaySM = reader.GetInt32(29);
                _tuesdayEH = reader.GetInt32(30);
                _tuesdayEM = reader.GetInt32(31);
                _wednesdaySH = reader.GetInt32(32);
                _wednesdaySM = reader.GetInt32(33);
                _wednesdayEH = reader.GetInt32(34);
                _wednesdayEM = reader.GetInt32(35);
                _thursdaySH = reader.GetInt32(36);
                _thursdaySM = reader.GetInt32(37);
                _thursdayEH = reader.GetInt32(38);
                _thursdayEM = reader.GetInt32(39);
                _fridaySH = reader.GetInt32(40);
                _fridaySM = reader.GetInt32(41);
                _fridayEH = reader.GetInt32(42);
                _fridayEM = reader.GetInt32(43);
                _saturdaySH = reader.GetInt32(44);
                _saturdaySM = reader.GetInt32(45);
                _saturdayEH = reader.GetInt32(46);
                _saturdayEM = reader.GetInt32(47);
                _sundaySH = reader.GetInt32(48);
                _sundaySM = reader.GetInt32(49);
                _sundayEH = reader.GetInt32(50);
                _sundayEM = reader.GetInt32(51);
                _myCompanyId = reader.IsDBNull(52) ? 0 : reader.GetInt32(52);
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.MondayW, SqlDbType.Int)
                                   {IsNullable = false, Value = _mondayW};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TuesdayW, SqlDbType.Int) {IsNullable = false, Value = _tuesdayW};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.WednesdayW, SqlDbType.Int)
                      {IsNullable = false, Value = _wednesdayW};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ThursdayW, SqlDbType.Int)
                      {IsNullable = false, Value = _thursdayW};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.FridayW, SqlDbType.Int) {IsNullable = false, Value = _fridayW};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SaturdayW, SqlDbType.Int)
                      {IsNullable = false, Value = _saturdayW};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SundayW, SqlDbType.Int) {IsNullable = false, Value = _sundayW};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MondaySH, SqlDbType.Int) {IsNullable = false, Value = _mondaySH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MondaySM, SqlDbType.Int) {IsNullable = false, Value = _mondaySM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MondayEH, SqlDbType.Int) {IsNullable = false, Value = _mondayEH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MondayEM, SqlDbType.Int) {IsNullable = false, Value = _mondayEM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TuesdaySH, SqlDbType.Int)
                      {IsNullable = false, Value = _tuesdaySH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TuesdaySM, SqlDbType.Int)
                      {IsNullable = false, Value = _tuesdaySM};
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.TuesdayEH, SqlDbType.Int)
                      {IsNullable = false, Value = _tuesdayEH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TuesdayEM, SqlDbType.Int)
                      {IsNullable = false, Value = _tuesdayEM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.WednesdaySH, SqlDbType.Int)
                      {IsNullable = false, Value = _wednesdaySH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.WednesdaySM, SqlDbType.Int)
                      {IsNullable = false, Value = _wednesdaySM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.WednesdayEH, SqlDbType.Int)
                      {IsNullable = false, Value = _wednesdayEH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.WednesdayEM, SqlDbType.Int)
                      {IsNullable = false, Value = _wednesdayEM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ThursdaySH, SqlDbType.Int)
                      {IsNullable = false, Value = _thursdaySH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ThursdaySM, SqlDbType.Int)
                      {IsNullable = false, Value = _thursdaySM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ThursdayEH, SqlDbType.Int)
                      {IsNullable = false, Value = _thursdayEH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ThursdayEM, SqlDbType.Int)
                      {IsNullable = false, Value = _thursdayEM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.FridaySH, SqlDbType.Int) {IsNullable = false, Value = _fridaySH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.FridaySM, SqlDbType.Int) {IsNullable = false, Value = _fridaySM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.FridayEH, SqlDbType.Int) {IsNullable = false, Value = _fridayEH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.FridayEM, SqlDbType.Int) {IsNullable = false, Value = _fridayEM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SaturdaySH, SqlDbType.Int)
                      {IsNullable = false, Value = _saturdaySH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SaturdaySM, SqlDbType.Int)
                      {IsNullable = false, Value = _saturdaySM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SaturdayEH, SqlDbType.Int)
                      {IsNullable = false, Value = _saturdayEH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SaturdayEM, SqlDbType.Int)
                      {IsNullable = false, Value = _saturdayEM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SundaySH, SqlDbType.Int) {IsNullable = false, Value = _sundaySH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SundaySM, SqlDbType.Int) {IsNullable = false, Value = _sundaySM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SundayEH, SqlDbType.Int) {IsNullable = false, Value = _sundayEH};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SundayEM, SqlDbType.Int) {IsNullable = false, Value = _sundayEM};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<TimePeriod> Members
        /// <summary>
        /// Связи единицы измерения
        /// </summary>
        /// <returns></returns>
        public List<IChain<TimePeriod>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи единицы измерения
        /// </summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<TimePeriod>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<TimePeriod> IChains<TimePeriod>.SourceList(int chainKindId)
        {
            return Chain<TimePeriod>.GetChainSourceList(this, chainKindId);
        }
        List<TimePeriod> IChains<TimePeriod>.DestinationList(int chainKindId)
        {
            return Chain<TimePeriod>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<TimePeriod>> GetValues(bool allKinds)
        {
            return CodeHelper<TimePeriod>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<TimePeriod>.GetView(this, true);
        }
        #endregion

        //TODO: 
        /// <summary>
        /// Коллекция корресспондентов в которой используется данный график
        /// </summary>
        /// <returns></returns>
        public List<Agent> GetAgents()
        {
            Agent item = new Agent { Workarea = Workarea };

            List<Agent> collection = new List<Agent>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = string.Empty;
                        ProcedureMap procedureMap = item.Entity.FindMethod("LoadListByUnitId");
                        if (procedureMap != null)
                        {
                            procedureName = procedureMap.FullName;
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Agent { Workarea = Workarea };
                            item.Load(reader);
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

        /// <summary>
        /// Первая иерархия в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<TimePeriod>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
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
        public List<TimePeriod> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<TimePeriod> filter = null, bool useAndFilter = false)
        {
            TimePeriod item = new TimePeriod { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<TimePeriod> collection = new List<TimePeriod>();
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
                            item = new TimePeriod { Workarea = Workarea };
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
