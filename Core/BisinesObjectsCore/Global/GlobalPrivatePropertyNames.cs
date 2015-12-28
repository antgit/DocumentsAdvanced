namespace BusinessObjects
{
    /// <summary>
    /// Наименования столбцов и свойств
    /// </summary>
    public static class GlobalPropertyNames
    {
        /// <summary>Максимальный размер</summary>
        public const string MaxSize = "MaxSize";
        /// <summary>Идентификатор перевозчика</summary>
        public const string ShippingId = "ShippingId";
        /// <summary>Перевозчик</summary>
        public const string Shipping = "Shipping";

        /// <summary>Понедельник</summary>
        public const string Monday = "Monday";
        /// <summary>Вторник</summary>
        public const string Tuesday = "Tuesday";
        /// <summary>Среда</summary>
        public const string Wednesday = "Wednesday";
        /// <summary>Четверг</summary>
        public const string Thursday = "Thursday";
        /// <summary>Пятница</summary>
        public const string Friday = "Friday";
        /// <summary>Суббота</summary>
        public const string Saturday = "Saturday";
        /// <summary>Воскресенье</summary>
        public const string Sunday = "Sunday";

        /// <summary>График работы или перерыва, завист от места использования. 
        /// Например для пользователя - график разрешенного входа в систему, для корреспондента - текущее время работы и переывов.
        /// </summary>
        public const string TimePeriod = "TimePeriod";
        /// <summary>Разрешить смену пароля пользователем</summary>
        public const string AllowChangePassword = "AllowChangePassword";
        /// <summary>Рекомендуемая дата смены пароля</summary>
        public const string RecommendedDateChangePassword = "RecommendedDateChangePassword";
        /// <summary>Автоматически генерировать следующий пароль</summary>
        public const string AutogenerateNextPassword = "AutogenerateNextPassword";
        /// <summary>Наименование объекта учета</summary>
        public const string ProductName = "ProductName";
        /// <summary>Группа по умолчанию, основная группа</summary>
        public const string DefaultGroup = "DefaultGroup";
        /// <summary>Наименование состояния</summary>
        public const string StateName = "StateName";
        /// <summary>Номер документа</summary>
        public const string DocNumber = "DocNumber";
        /// <summary>Сумма документа</summary>
        public const string DocSumma = "DocSumma";
        /// <summary>Идентификатор компании "Кому"</summary>
        public const string MainClientDepatmentId = "MainClientDepatmentId";
        /// <summary>Идентификатор компании "Кто"</summary>
        public const string MainCompanyDepatmentId = "MainCompanyDepatmentId";
        /// <summary>Дата окончания</summary>
        public const string DateExpire = "DateExpire";

        /// <summary>Краткое примечание</summary>
        public const string DisplayMemo = "DisplayMemo";
        /// <summary>Наименование нашего предприятия</summary>
        public const string MyCompanyName = "MyCompanyName";
        /// <summary>Код единицы измерения</summary>
        public const string UnitCode = "UnitCode";
        /// <summary>Наименование единицы измерения</summary>
        public const string UnitName = "UnitName";
        /// <summary>Наименование вид продукции</summary>
        public const string ProductTypeName = "ProductTypeName";
        /// <summary>Наименование упаковки</summary>
        public const string PakcTypeName = "PakcTypeName";
        /// <summary>Наименование бренда</summary>
        public const string BrandName = "BrandName";
        /// <summary>Наименование торговой марки</summary>
        public const string TradeMarkName = "TradeMarkName";
        /// <summary>Заведующий складом</summary>
        public const string StorekeeperName = "StorekeeperName";
        /// <summary>Пол человека в виде строки</summary>
        public const string SexName = "SexName";
        /// <summary>Место хранения трудовой</summary>
        public const string PlaceEmploymentBookName = "PlaceEmploymentBookName";
        /// <summary>Место хранения трудовой</summary>
        public const string PlaceEmploymentBook = "PlaceEmploymentBook";
        
        /// <summary>Наименование руководителя отдела</summary>
        public const string DepatmentHeadName = "DepatmentHeadName";
        /// <summary>Наименование заместителя отдела</summary>
        public const string DepatmentSubHeadName = "DepatmentSubHeadName";
        /// <summary>Идентификатор подтипа объекта источника</summary>
        public const string EntityKindIdFrom = "EntityKindIdFrom";
        /// <summary>Реальный адрес пользователя (используется в Web)</summary>
        public const string RemoteAddr = "RemoteAddr";
        /// <summary>Служебный глобальный идентификатор, не предназначен для прямого использования!</summary>
        public const string MGuid = "MGuid";
        /// <summary>Сотрудник "Кому"</summary>
        public const string AgentToSub = "AgentToSub";
        /// <summary>Сотрудник-заместитель "Кому"</summary>
        public const string AgentToSubId = "AgentToSubId";
        /// <summary>Сотрудник "Кому"</summary>
        public const string WorkerTo = "WorkerTo";
        /// <summary>Идентификатор сотрудника "Кому"</summary>
        public const string WorkerToId = "WorkerToId";
        /// <summary>Сотрудник "Кто"</summary>
        public const string WorkerFrom = "WorkerFrom";
        /// <summary>Идентификатор сотрудника "Кто"</summary>
        public const string WorkerFromId = "WorkerFromId";
        /// <summary>Интервал времени</summary>
        public const string Dateregion = "Dateregion";
        /// <summary>Идентификатор интервала времени</summary>
        public const string DateregionId = "DateregionId";
        /// <summary>Идентификатор корреспондента</summary>
        public const string AgentId5 = "AgentId5";
        /// <summary>Идентификатор корреспондента</summary>
        public const string AgentId4 = "AgentId4";
        /// <summary>Идентификатор корреспондента</summary>
        public const string AgentId3 = "AgentId3";
        /// <summary>Идентификатор корреспондента</summary>
        public const string AgentId2 = "AgentId2";
        /// <summary>Объекта учета</summary>
        public const string Product5 = "Product5";
        /// <summary>Объекта учета</summary>
        public const string Product4 = "Product4";
        /// <summary>Объекта учета</summary>
        public const string Product3 = "Product3";
        /// <summary>Объекта учета</summary>
        public const string Product2 = "Product2";
        /// <summary>Идентификатор объекта учета</summary>
        public const string ProductId5 = "ProductId5";
        /// <summary>Идентификатор объекта учета</summary>
        public const string ProductId4 = "ProductId4";
        /// <summary>Идентификатор объекта учета</summary>
        public const string ProductId3 = "ProductId3";
        /// <summary>Идентификатор объекта учета</summary>
        public const string ProductId2 = "ProductId2";
        /// <summary>Идентификатор отдела</summary>
        public const string DepatmentToId = "DepatmentToId";
        /// <summary>Идентификатор отдела</summary>
        public const string DepatmentFromId = "DepatmentFromId";
        /// <summary>Идентификатор заключения</summary>
        public const string StateResultId = "StateResultId";
        /// <summary>Идентификатор типа планирования</summary>
        public const string PlanKindId = "PlanKindId";
        /// <summary>Отметка отправителя</summary>
        public const string MarkedOwner="MarkedOwner";
        /// <summary>Уровень отметки отправителя</summary>
        public const string MarkScoreOwner="MarkScoreOwner";
        /// <summary>Отметка получателя</summary>
        public const string MarkedRecipient="MarkedRecipient";
        /// <summary>Уровень отметки получателя</summary>
        public const string MarkScoreRecipient = "MarkScoreRecipient";
        /// <summary>Дата и время послежней загрузки из базы данных</summary>
        public const string LastLoadTime = "LastLoadTime";
        /// <summary>Статус фактический</summary>
        public const string StatusFact = "StatusFact";
        /// <summary>Статус</summary>
        public const string Status = "Status";
        /// <summary>Уровень группы</summary>
        public const string GroupLevel = "GroupLevel";
        /// <summary>Идентификатор уровня группы</summary>
        public const string GroupLevelId = "GroupLevelId";
        /// <summary>Отдел</summary>
        public const string Depatment = "Depatment";
        /// <summary>Идентификатор отдела</summary>
        public const string DepatmentId = "DepatmentId";
        /// <summary>Адрес</summary>
        public const string Address = "Address";
        /// <summary>Идентификатор состояния посещения клиента</summary>
        public const string StatusFactId = "StatusFactId";
        /// <summary>Время стоянки фактическое в минутах</summary>
        public const string FactStaying = "FactStaying";
        /// <summary>Время стоянки по плану в минутах</summary>
        public const string PlanStaying = "PlanStaying";
        /// <summary>Время по плану</summary>
        public const string PlanTime = "PlanTime";
        /// <summary>Дата по плану</summary>
        public const string PlanDate = "PlanDate";
        /// <summary>Радиус зоны (для GPS мониторинга)</summary>
        public const string ZoneRadius = "ZoneRadius";
        /// <summary>Дата POS</summary>
        public const string PosDate = "PosDate";
        /// <summary>Время POS</summary>
        public const string PosTime = "PosTime";
        /// <summary>Дата RTS</summary>
        public const string RtcDate = "RtcDate";
        /// <summary>Время RTS</summary>
        public const string RtcTime = "RtcTime";
        /// <summary>Значение с первого аналогового входа</summary>
        public const string Input1 = "Input1";
        /// <summary>Значение со второго аналогового входа</summary>
        public const string Input2 = "Input2";
        /// <summary>Статус устройства</summary>
// ReSharper disable InconsistentNaming
        public const string IOStatus = "IOStatus";
// ReSharper restore InconsistentNaming
        /// <summary>Число спутников</summary>
        public const string Satellites = "Satellites";
        /// <summary>Точность замеров</summary>
// ReSharper disable InconsistentNaming
        public const string HDOP = "HDOP";
// ReSharper restore InconsistentNaming
        /// <summary>Направление движения</summary>
        public const string Direction = "Direction";
        /// <summary>Высота</summary>
        public const string Altitude = "Altitude";
        /// <summary>Код статуса</summary>
        public const string StatusCode = "StatusCode";
        /// <summary>Адрес корреспондента</summary>
        public const string AddressId = "AddressId";
        /// <summary>Показания одометра</summary>
        public const string Odometer = "Odometer";
        /// <summary>Дистанция</summary>
        public const string Distance = "Distance";
        /// <summary>Скорость движения</summary>
        public const string Speed = "Speed";
        /// <summary>Объект слежения</summary>
        public const string RouteMember = "RouteMember";
        /// <summary>Идентификатор объекта слежения</summary>
        public const string RouteMemberId = "RouteMemberId";
        /// <summary>Устройство</summary>
        public const string Device = "Device";
        /// <summary>Идентификатор устройства</summary>
        public const string DeviceId = "DeviceId";
        /// <summary>Идентификатор сотрудника</summary>
        public const string Employer = "Employer";
        /// <summary>Идентификатор сотрудника</summary>
        public const string EmployerId = "EmployerId";
        /// <summary>Руководитель</summary>
        public const string DepatmentHead = "DepatmentHead";
        /// <summary>Заместитель</summary>
        public const string DepatmentSubHead = "DepatmentSubHead";
        /// <summary>Идентификатор заместителя</summary>
        public const string DepatmentSubHeadId = "DepatmentSubHeadId";
        /// <summary>Идентификатор руководителя</summary>
        public const string DepatmentHeadId = "DepatmentHeadId";
        /// <summary>Сотрудник фактически выполнивший подписание</summary>
        public const string AgentSign = "AgentSign";
        /// <summary>Идентификатор сотрудника фактически выполнившего подписание</summary>
        public const string AgentSignId = "AgentSignId";
        /// <summary>Сотрудник-заместитель</summary>
        public const string AgentSub = "AgentSub";
        /// <summary>Дата фактической подписи</summary>
        public const string DateSign = "DateSign";
        /// <summary>Необходимость формировать задачу</summary>
        public const string TaskNeed = "TaskNeed";
        /// <summary>Необходимость формировать сообщение</summary>
        public const string MessageNeed = "MessageNeed";
        /// <summary>Идентификатор сообщения</summary>
        public const string MessageId = "MessageId";
        /// <summary>Идентификатор задачи</summary>
        public const string TaskId = "TaskId";
        /// <summary>Тип подписания ИЛИ/И</summary>
        public const string SignKind = "SignKind";
        /// <summary>Является ли главным подписантом</summary>
        public const string IsMain = "IsMain";
        /// <summary>Заместитель</summary>
        public const string AgentSubId = "AgentSubId";
        /// <summary>Дата и время последней частичной загрузки объекта</summary>
        public const string LastLoadPartial = "LastLoadPartial";
        /// <summary>Дата и время последней загрузки объекта</summary>
        public const string LastLoad = "LastLoad";
        /// <summary>Url справочной информации</summary>
        public const string HelpUrl = "HelpUrl";
        /// <summary>Разрешено</summary>
        public const string IsAllow = "IsAllow";
        /// <summary>Время старта</summary>
        public const string StartTime = "StartTime";
        /// <summary>Дата старта</summary>
        public const string StartDate = "StartDate";
        /// <summary>Идентификатор логотипа</summary>
        public const string LogoId = "LogoId";
        /// <summary>Идентификатор печати</summary>
        public const string LogostampId = "LogostampId";
        /// <summary>Идентификатор подписи</summary>
        public const string LogoSignId = "LogoSignId";
        /// <summary>Идентификатор несовершеннолетия</summary>
        public const string MinorsId = "MinorsId";
        /// <summary>Идентификатор места хранения трудовой</summary>
        public const string PlaceEmploymentBookId = "PlaceEmploymentBookId";
        /// <summary>Официальный сотрудник</summary>
        public const string LegalWorker = "LegalWorker";
        /// <summary>Пенсионность</summary>
        public const string Pension = "Pension";
        /// <summary>Инвалидность</summary>
        public const string Invalidity = "Invalidity";
        /// <summary>Идентификатор последнего места работы</summary>
        public const string LastPlaceWorkId = "LastPlaceWorkId";
        /// <summary>Последнее места работы</summary>
        public const string LastPlaceWork = "LastPlaceWork";
        /// <summary>Номер страхового свидетельства</summary>
        public const string InsuranceNumber = "InsuranceNumber";
        /// <summary>Серия страхового свидетельства</summary>
        public const string InsuranceSeries = "InsuranceSeries";
        /// <summary>Отображать индикатор</summary>
        public const string ShowIndicator = "ShowIndicator";
        /// <summary>Примечание в виде текста</summary>
        public const string MemoTxt = "MemoTxt";
        /// <summary>Код класса</summary>
        public const string CodeClass = "CodeClass";
        /// <summary>Наименоване схемы</summary>
        public const string NameSchema = "NameSchema";
        /// <summary>Код номера телефона для АМТС</summary>
        public const string Amts = "Amts";
        /// <summary>Плотность населения людей/кв.км</summary>
        public const string PopulationDensity = "PopulationDensity";
        /// <summary>Население в тысячах людей</summary>
        public const string Population = "Population";
        /// <summary>Территория в тис. кв. км.</summary>
        public const string TerritoryKvKm = "TerritoryKvKm";
        /// <summary>Прошлое наименование</summary>
        public const string NameOld = "NameOld";
        /// <summary>Дата основания</summary>
        public const string DateFoundation = "DateFoundation";
        /// <summary>Международное наименование</summary>
        public const string NameInternational = "NameInternational";
        /// <summary>Национальное наименование</summary>
        public const string NameNational = "NameNational";
        /// <summary>Время отправки</summary>
        public const string SendTime = "SendTime";
        /// <summary>Дата отправки</summary>
        public const string SendDate = "SendDate";
        /// <summary>Отправлено</summary>
        public const string IsSend = "IsSend";
        /// <summary>Тип рекурсии</summary>
        public const string Recursive = "Recursive";
        /// <summary>Идентификатор типа рекурсии</summary>
        public const string RecursiveId = "RecursiveId";
        /// <summary>Дата планового запуска</summary>
        public const string Emblem = "Emblem";
        /// <summary>Дата планового запуска</summary>
        public const string StartPlanDate = "StartPlanDate";
        /// <summary>Время планового запуска</summary>
        public const string StartPlanTime = "StartPlanTime";
        /// <summary>Пользователь исполнитель</summary>
        public const string UserTo = "UserTo";
        /// <summary>Процесс владельц</summary>
        public const string WfOwner = "WfOwner";
        /// <summary>Процесс для запуска</summary>
        public const string WfToStart = "WfToStart";
        /// <summary>Идентификатор процесса владельца</summary>
        public const string WfOwnerId = "WfOwnerId";
        /// <summary>Идентификатор процесса для запуска</summary>
        public const string WfToStartId = "WfToStartId";
        /// <summary>Цвета</summary>
        public const string Color = "Color";
        /// <summary>Идентификатор цвета</summary>
        public const string ColorId = "ColorId";
        /// <summary>Областного района</summary>
        public const string Region = "Region";
        /// <summary>Идентификатор областного района</summary>
        public const string RegionId = "RegionId";
        /// <summary>Время прочтения</summary>
        public const string ReadTime = "ReadTime";
        /// <summary>Дата прочтения</summary>
        public const string ReadDate = "ReadDate";
        /// <summary>Требовать уведомления</summary>
        public const string HasRead = "HasRead";
        /// <summary>Уведомление</summary>
        public const string ReadDone = "ReadDone";
        /// <summary>Пользователь</summary>
        public const string User = "User";
        /// <summary>Суфикс</summary>
        public const string Prefix = "Prefix";
        /// <summary>Суфикс</summary>
        public const string Suffix = "Suffix";
        /// <summary>Аналитика №2</summary>
        public const string Analitic5 = "Analitic5";
        /// <summary>Аналитика №2</summary>
        public const string Analitic4 = "Analitic4";
        /// <summary>Аналитика №2</summary>
        public const string Analitic3 = "Analitic3";
        /// <summary>Аналитика №2</summary>
        public const string Analitic2 = "Analitic2";
        /// <summary>Аналитика</summary>
        public const string Analitic = "Analitic";
        /// <summary>Признак собственного, частного</summary>
        public const string InPrivate = "InPrivate";
        /// <summary>Дата планового старта</summary>
        public const string DateStartPlan = "DateStartPlan";
        /// <summary>Дата планового завершения</summary>
        public const string DateEndPlan = "DateEndPlan";
        /// <summary>Время планового старта</summary>
        public const string DateStartPlanTime = "DateStartPlanTime";
        /// <summary>Время старта</summary>
        public const string DateStartTime = "DateStartTime";
        /// <summary>Время планового окончания</summary>
        public const string DateEndPlanTime = "DateEndPlanTime";
        /// <summary>Время окончания</summary>
        public const string DateEndTime = "DateEndTime";
        /// <summary>Время планового окончания</summary>
        public const string DateEndPlanTimeAsDate = "DateEndPlanTimeAsDate";
        /// <summary>Время окончания</summary>
        public const string DateEndTimeAsDate = "DateEndTimeAsDate";
        /// <summary>Время планового старта</summary>
        public const string DateStartPlanTimeAsDate = "DateStartPlanTimeAsDate";
        /// <summary>Время старта</summary>
        public const string DateStartTimeAsDate = "DateStartTimeAsDate";
        /// <summary>Идентификатор состояния задачи</summary>
        public const string TaskStateId = "TaskStateId";
        /// <summary>Номер задачи</summary>
        public const string TaskNumber = "TaskNumber";
        /// <summary>Процент выполнения</summary>
        public const string DonePersent = "DonePersent";
        /// <summary>Идентификатор пользователя "Кому"</summary>
        public const string UserToId = "UserToId";
        /// <summary>Дополнительное пятое строковое значение</summary>
        public const string StringValue5 = "StringValue5";
        /// <summary>Дополнительное четвертое строковое значение</summary>
        public const string StringValue4 = "StringValue4";
        /// <summary>Дополнительное третье строковое значение</summary>
        public const string StringValue3 = "StringValue3";
        /// <summary>Дополнительное второе строковое значение</summary>
        public const string StringValue2 = "StringValue2";
        /// <summary>Дополнительное строковое значение</summary>
        public const string StringValue1 = "StringValue1";
        /// <summary>Дополнительное числовое значение</summary>
        public const string IntValue1 = "IntValue1";
        /// <summary>Дополнительное второе числовое значение</summary>
        public const string IntValue2 = "IntValue2";
        /// <summary>Дополнительное третье числовое значение</summary>
        public const string IntValue3 = "IntValue3";
        /// <summary>Дополнительное четвертое числовое значение</summary>
        public const string IntValue4 = "IntValue4";
        /// <summary>Дополнительное пятое числовое значение</summary>
        public const string IntValue5 = "IntValue5";
        /// <summary>Дополнительное денежное значение</summary>
        public const string SummValue1 = "SummValue1";
        /// <summary>Дополнительное второе денежное значение</summary>
        public const string SummValue2 = "SummValue2";
        /// <summary>Дополнительное третье денежное значение</summary>
        public const string SummValue3 = "SummValue3";
        /// <summary>Дополнительное четвертое денежное значение</summary>
        public const string SummValue4 = "SummValue4";
        /// <summary>Дополнительное пятое денежное значение</summary>
        public const string SummValue5 = "SummValue5";
        /// <summary>Идентификатор графика работ</summary>
        public const string TimePeriodId = "TimePeriodId";
        /// <summary>Идентификатор графика перерывов</summary>
        public const string BreakPeriodId = "BreakPeriodId";
        /// <summary>Контактное лицо</summary>
        public const string ContactName = "ContactName";
        /// <summary>Контактный телефон</summary>
        public const string ContactPhone = "ContactPhone";
        /// <summary>Идентификатор должности</summary>
        public const string WorkPostId = "WorkPostId";
        /// <summary>Идентификатор типа торговой точки</summary>
        public const string OutletTypeId = "OutletTypeId";
        /// <summary>Идентификатор расположения торговой точки</summary>
        public const string OutletLocationId = "OutletLocationId";
        /// <summary>Понедельник - рабочий день</summary>
        public const string MondayW = "MondayW";
        /// <summary>Вторник - рабочий день</summary>
        public const string TuesdayW = "TuesdayW";
        /// <summary>Среда - рабочий день</summary>
        public const string WednesdayW = "WednesdayW";
        /// <summary>Четверг - рабочий день</summary>
        public const string ThursdayW = "ThursdayW";
        /// <summary>Пятница - рабочий день</summary>
        public const string FridayW = "FridayW";
        /// <summary>Суббота - рабочий день</summary>
        public const string SaturdayW = "SaturdayW";
        /// <summary>Воскресенье - рабочий день</summary>
        public const string SundayW = "SundayW";
        // ReSharper disable InconsistentNaming
        /// <summary>Понедельник - началов в часов </summary>
        public const string MondaySH="MondaySH";
        /// <summary>Понедельник - начало миниут</summary>
        public const string MondaySM="MondaySM";
        /// <summary>Понедельник - конец в часов</summary>
        public const string MondayEH="MondayEH";
        /// <summary>Понедельник - конец минут</summary>
        public const string MondayEM="MondayEM";
        /// <summary>Вторник - началов в часов</summary>
        public const string TuesdaySH="TuesdaySH";
        /// <summary>Вторник - начало миниут</summary>
        public const string TuesdaySM="TuesdaySM";
        /// <summary>Вторник - конец в часов</summary>
        public const string TuesdayEH="TuesdayEH";
        /// <summary>Вторник - конец минут</summary>
        public const string TuesdayEM="TuesdayEM";
        /// <summary>Среда - началов в часов</summary>
        public const string WednesdaySH="WednesdaySH";
        /// <summary>Среда - начало минут</summary>
        public const string WednesdaySM="WednesdaySM";
        /// <summary>Среда - конец минут</summary>
        public const string WednesdayEM="WednesdayEM";
        /// <summary>Среда - конец часов</summary>
        public const string WednesdayEH="WednesdayEH";
        /// <summary>Четверг - началов в часов</summary>
        public const string ThursdaySH="ThursdaySH";
        /// <summary>Четверг - начало минут</summary>
        public const string ThursdaySM="ThursdaySM";
        /// <summary>Четверг - конец минут</summary>
        public const string ThursdayEM="ThursdayEM";
        /// <summary>Четверг - конец часов</summary>
        public const string ThursdayEH="ThursdayEH";
        /// <summary>Пятница - началов в часов</summary>
        public const string FridaySH="FridaySH";
        /// <summary>Пятница - начало минут</summary>
        public const string FridaySM="FridaySM";
        /// <summary>Пятница - конец в часов</summary>
        public const string FridayEH="FridayEH";
        /// <summary>Пятница - конец минут</summary>
        public const string FridayEM="FridayEM";
        /// <summary>Суббота - началов в часов</summary>
        public const string SaturdaySH="SaturdaySH";
        /// <summary>Суббота - начало минут</summary>
        public const string SaturdaySM="SaturdaySM";
        /// <summary>Суббота - конец в часов</summary>
        public const string SaturdayEH="SaturdayEH";
        /// <summary>Суббота - конец минут</summary>
        public const string SaturdayEM="SaturdayEM";
        /// <summary>Воскресенье -началов в часов </summary>
        public const string SundaySH="SundaySH";
        /// <summary>Воскресенье - конец в часов</summary>
        public const string SundayEH="SundayEH";
        /// <summary>Воскресенье - начало минут</summary>
        public const string SundaySM="SundaySM";
        /// <summary>Воскресенье - конец минут</summary>
        public const string SundayEM = "SundayEM";
        // ReSharper restore InconsistentNaming        
        /// <summary>Расчетный счет получателя</summary>
        public const string BankAccTo = "BankAccTo";
        /// <summary>Расчетный счет отправителя</summary>
        public const string BankAccFrom = "BankAccFrom";
        /// <summary>Идентификатор расчетного счета получателя</summary>
        public const string BankAccToId = "BankAccToId";
        /// <summary>Идентификатор расчетного счета отправителя</summary>
        public const string BankAccFromId = "BankAccFromId";
        /// <summary>Кореспондентский счет</summary>
        public const string CorrBankAccount = "CorrBankAccount";
        /// <summary>S.W.I.F.T.</summary>
        public const string Swift = "Swift";
        /// <summary>Дата банковской лицензии</summary>
        public const string LicenseDate = "LicenseDate";
        /// <summary>Номер банковской лицензии</summary>
        public const string LicenseNo = "LicenseNo";
        /// <summary>Дата свидетельства регистрации НБУ, дата сертификата</summary>
        public const string SertificateDate = "SertificateDate";
        /// <summary>Номер свидетельства регистрации НБУ, номер сертификата</summary>
        public const string SertificateNo = "SertificateNo";
        /// <summary>Расчетный счет получателя/плательщика</summary>
        public const string AgToBankAcc = "AgToBankAcc";
        /// <summary>Расчетный счет поставщика/плательщика</summary>
        public const string AgFromBankAcc = "AgFromBankAcc";
        /// <summary>Ид герба</summary>
        public const string EmblemId = "EmblemId";
        /// <summary>Континент</summary>
        public const string Continent = "Continent";
        /// <summary>Хеш пароля</summary>
        public const string PasswordHash = "PasswordHash";
        /// <summary>Новый Email</summary>
        public const string NewEmailKey = "NewEmailKey";
        /// <summary>Дата создание</summary>
        public const string DateCreated = "DateCreated";
        /// <summary>Секретный вопрос</summary>
        public const string PasswordQuestion = "PasswordQuestion";
        /// <summary>Salt пароль</summary>
        public const string PasswordSalt = "PasswordSalt";
        /// <summary>Дата последненго изменения пароля</summary>
        public const string LastPasswordChangedDate = "LastPasswordChangedDate";
        /// <summary>Дата последнего входа</summary>
        public const string LastLoginDate = "LastLoginDate";
        /// <summary>Дата последней блокировки</summary>
        public const string LastLockedOutDate = "LastLockedOutDate";
        /// <summary>Email</summary>
        public const string Email = "Email";
        /// <summary>Дата последней активности</summary>
        public const string LastActivityDate = "LastActivityDate";
        /// <summary>Максимальное значение</summary>
        public const string ValueMin = "ValueMin";
        /// <summary>Минимальное значение</summary>
        public const string ValueMax = "ValueMax";
        /// <summary></summary>
        public const string EndPointBehavior = "EndPointBehavior";
        /// <summary></summary>
        public const string ServiceBinding = "ServiceBinding";
        /// <summary></summary>
        public const string InternalClientConfiguration = "InternalClientConfiguration";
        /// <summary>Использовать HTTPS</summary>
        public const string UseHttps = "UseHttps";
        /// <summary></summary>
        public const string ServiceContract = "ServiceContract";
        /// <summary></summary>
        public const string BindingType = "BindingType";
        /// <summary>Порт</summary>
        public const string Port = "Port";
        /// <summary>Идентификатор формы документа</summary>
        public const string ProjectItemId = "ProjectItemId";
        /// <summary>Имеются ли значения на дату</summary>
        public const string HasValue = "HasValue";
        /// <summary>Идентификатор системного типа</summary>
        public const string DbEntityId = "DbUidId";
        /// <summary>Идентификатор пользователя</summary>
        public const string DbUidId = "DbUidId";
        /// <summary>Код версии</summary>
        public const string VersionCode = "VersionCode";
        /// <summary>Хранить пароль</summary>
        public const string StorePassword = "StorePassword";
        /// <summary>IP-адрес сервера</summary>
        public const string IpAddress = "IpAddress";
        /// <summary>Каталожный номер</summary>
        public const string Catalogue = "Catalogue";
        /// <summary>Идентификатор изготовителя</summary>
        public const string ManufactureId = "ManufactureId";
        /// <summary>Наличие содержимого</summary>
        public const string HasContents = "HasContents";
        /// <summary>Полный путь</summary>
        public const string Path = "Path";
        /// <summary>Ежедневное</summary>
        public const string IsRecurcive = "IsRecurcive";
        /// <summary>Идентификатор статуса</summary>
        public const string StatusId = "StatusId";
        /// <summary>Колонки</summary>
        public const string Columns = "Columns";
        /// <summary>Тип документа</summary>
        public const string DocType = "DocType";
        /// <summary>Идентификатор файла</summary>
        public const string FileId = "FileId";
        /// <summary>Тип авторизации</summary>
        public const string AuthKind = "AuthKind";
        /// <summary>Адрес службы</summary>
        public const string UrlAddress = "UrlAddress";
        /// <summary>Идентификатор группы пользователей</summary>
        public const string UserGroupId = "UserGroupId";
        /// <summary>Каталог данных</summary>
        public const string Directory = "Directory";
        /// <summary></summary>
        public const string PrcContentId = "PrcContentId";
        /// <summary></summary>
        public const string CurrencyIdTrans = "CurrencyIdTrans";
        /// <summary></summary>
        public const string CurrencyIdCountry = "CurrencyIdCountry";
        /// <summary></summary>
        public const string SummaCurrency = "SummaCurrency";
        /// <summary></summary>
        public const string DocId = "DocId";
        /// <summary></summary>
        public const string ContractKind = "ContractKind";
        /// <summary></summary>
        public const string StateCurrent = "StateCurrent";
        /// <summary>Флаги</summary>
        public const string Flags = "Flags";
        /// <summary>Идентификатор типа документа</summary>
        public const string DocTypeId = "DocTypeId";
        /// <summary>Время окончания</summary>
        public const string EndOnTime = "EndOnTime";
        /// <summary>Дата окончания</summary>
        public const string EndOn = "EndOn";
        /// <summary>Время начала</summary>
        public const string StartOnTime = "StartOnTime";
        /// <summary>Дата начала</summary>
        public const string StartOn = "StartOn";
        /// <summary>Разрешение</summary>
        public const string Right = "Right";
        /// <summary>Тип авторизации</summary>
        public const string AuthenticateKind = "AuthenticateKind";
        /// <summary>Время факта</summary>
        public const string FactTime = "FactTime";
        /// <summary>Дата факта</summary>
        public const string FactDate = "FactDate";
        /// <summary>Идентификатор даты актуальности</summary>
        public const string FactDateId = "FactDateId";
        /// <summary>Числовое значение</summary>
        public const string ValueInt = "ValueInt";
        /// <summary>Числовое значение</summary>
        public const string ValueRef1 = "ValueRef1";
        /// <summary>Числовое значение</summary>
        public const string ValueRef2 = "ValueRef2";
        /// <summary>Числовое значение</summary>
        public const string ValueRef3 = "ValueRef3";
        /// <summary>Строковое значение</summary>
        public const string ValueString = "ValueString";
        /// <summary>Денежное значение</summary>
        public const string ValueMoney = "ValueMoney";
        /// <summary>Значение даты/времени</summary>
        public const string ValueDate = "ValueDate";
        /// <summary>Логическое значение</summary>
        public const string ValueBit = "ValueBit";
        /// <summary>Значение XML</summary>
        public const string ValueXml = "ValueXml";
        /// <summary>Значение глобального идентификатора</summary>
        public const string ValueGuid = "ValueGuid";
        /// <summary>Десятичное значение</summary>
        public const string ValueDecimal = "ValueDecimal";
        /// <summary>Двоичное значение</summary>
        public const string ValueBinary = "ValueBinary";
        /// <summary>Вещественное значение</summary>
        public const string ValueFloat = "ValueFloat";
        /// <summary>Значение 1</summary>
        public const string Value1 = "Value1";
        /// <summary>Значение 2</summary>
        public const string Value2 = "Value2";
        /// <summary>Значение 3</summary>
        public const string Value3 = "Value3";
        /// <summary>Значение 4</summary>
        public const string Value4 = "Value4";
        /// <summary>Значение 5</summary>
        public const string Value5 = "Value5";
        /// <summary>Значение 6</summary>
        public const string Value6 = "Value6";
        /// <summary>Значение 7</summary>
        public const string Value7 = "Value7";
        /// <summary>Значение 8</summary>
        public const string Value8 = "Value8";
        /// <summary>Значение 9</summary>
        public const string Value9 = "Value9";
        /// <summary>Значение 10</summary>
        public const string Value10 = "Value10";
        /// <summary>Значение 11</summary>
        public const string Value11 = "Value11";
        /// <summary>Значение 12</summary>
        public const string Value12 = "Value12";
        /// <summary>Значение 13</summary>
        public const string Value13 = "Value13";
        /// <summary>Значение 14</summary>
        public const string Value14 = "Value14";
        /// <summary>Значение 15</summary>
        public const string Value15 = "Value15";
        /// <summary>Колонка факта</summary>
        public const string FactColumn = "FactColumn";
        /// <summary>Дата</summary>
        public const string ActualDate = "ActualDate";
        /// <summary>Идентификатор колонки</summary>
        public const string ColumnId = "ColumnId";
        /// <summary>Идентификатор наименования</summary>
        public const string FactNameId = "FactNameId";
        /// <summary>Наименование факта</summary>
        public const string FactName = "FactName";
        /// <summary>Тип первой ссылки</summary>
        public const string ReferenceType = "ReferenceType";
        /// <summary>Тип первой ссылки</summary>
        public const string Reference = "Reference";
        /// <summary>Тип второй ссылки</summary>
        public const string ReferenceType2 = "ReferenceType2";
        /// <summary>Тип первой ссылки</summary>
        public const string Reference2 = "Reference2";
        /// <summary>Тип третьей ссылки</summary>
        public const string ReferenceType3 = "ReferenceType3";
        /// <summary>Тип первой ссылки</summary>
        public const string Reference3 = "Reference3";
        /// <summary>Идентификатор корня первой ссылки</summary>
        public const string RootId = "RootId";
        /// <summary>Идентификатор корня второй ссылки</summary>
        public const string RootId2 = "RootId2";
        /// <summary>Идентификатор корня третьей ссылки</summary>
        public const string RootId3 = "RootId3";
        /// <summary>Идентификатор документа доверенности</summary>
        public const string TrustDocId = "TrustDocId";
        /// <summary>Идентификатор партии товара</summary>
        public const string SeriesId = "SeriesId";
        /// <summary>Единица измерения</summary>
        public const string FUnit = "FUnit";
        /// <summary>Идентификатор ценовой политики 2</summary>
        public const string PrcNameId2 = "PrcNameId2";
        /// <summary>Ценовая политика 2</summary>
        public const string PriceName2 = "PriceName2";
        /// <summary>Регистратор</summary>
        public const string Registrator = "Registrator";
        /// <summary>Идентификатор типа договора</summary>
        public const string ContractKindId = "ContractKindId";
        /// <summary>Идентификатор текущего состояния договора</summary>
        public const string StateCurrentId = "StateCurrentId";
        /// <summary>Важность договора</summary>
        public const string Importance = "Importance";
        /// <summary>Идентификатор важности договора</summary>
        public const string ImportanceId = "ImportanceId";
        /// <summary>Исходящий номер</summary>
        public const string NumberOut = "NumberOut";
        /// <summary>Иденгтификатор регистратора документа</summary>
        public const string RegistratorId = "RegistratorId";
        /// <summary>Счет по кредиту</summary>
        public const string AccountCr = "AccountCr";
        /// <summary>Идентификатор счета по кредиту</summary>
        public const string AccountCrId = "AccountCrId";
        /// <summary>Идентификатор счета по дебету</summary>
        public const string AccountDbId = "AccountDbId";
        /// <summary>Счет по дебету</summary>
        public const string AccountDb = "AccountDb";
        /// <summary>Идентификатор расчетного счета корреспондента "Кто"</summary>
        public const string AgFromBankAccId = "AgFromBankAccId";
        /// <summary>Идентификатор расчетного счета корреспондента "Кому"</summary>
        public const string AgToBankAccId = "AgToBankAccId";
        /// <summary>Идентификатор типа документа</summary>
        public const string DocKind = "DocKind";
        /// <summary>Идентификатор строки документа в разделе "Управление товарными запасами"</summary>
        public const string RowId = "RowId";
        /// <summary>Идентификатор ячейки хранения</summary>
        public const string CellId = "CellId";
        /// <summary>Наименование приложения</summary>
        public const string App = "App";
        /// <summary>Обратное наименование</summary>
        public const string NameRight = "NameRight";
        /// <summary>Флаговое значение подтипов для которых действительна связь</summary>
        public const string EntityContent = "EntityContent";
        /// <summary>Xml</summary>
        public const string Xml = "Xml";
        /// <summary>Пользователь владелец</summary>
        public const string UserOwner = "UserOwner";
        /// <summary>Идентификатор пользователя владелеца</summary>
        public const string UserOwnerId = "UserOwnerId";
        /// <summary>Идентификатор приоритета</summary>
        public const string PriorityId = "PriorityId";
        /// <summary>Приоритет</summary>
        public const string Priority = "Priority";
        /// <summary>Разрешить ведение версионности</summary>
        public const string AllowVersion = "AllowVersion";
        /// <summary>Идентификатор четвертого фильтра корреспондента</summary>
        public const string AgentFourthFilterId = "AgentFourthFilterId";
        /// <summary>Идентификатор третьего фильтра корреспондента</summary>
        public const string AgentThirdFilterId = "AgentThirdFilterId";
        /// <summary>Идентификатор второго фильтра корреспондента</summary>
        public const string AgentSecondFilterId = "AgentSecondFilterId";
        /// <summary>Идентификатор первого фильтра корреспондента</summary>
        public const string AgentFirstFilterId = "AgentFirstFilterId";
        /// <summary>Использовать нестандартное поведение для выбора корреспондентов</summary>
        public const string UseCustomFilter = "UseCustomFilter";
        /// <summary>Тип корреспонденции</summary>
        public const string CorrespondenceId = "CorrespondenceId";
        /// <summary>Роспись</summary>
        public const string SignatureOfficial = "SignatureOfficial";
        /// <summary>Фотография</summary>
        public const string Signature = "Signature";
        /// <summary>Дата окончания</summary>
        public const string ExpireDate = "ExpireDate";
        /// <summary>Идентификатор налога</summary>
        public const string TaxId = "TaxId";
        /// <summary>Идентификатор Workflow</summary>
        public const string WfId = "WfId";
        /// <summary>Идентификатор документа</summary>
        public const string OwnerId = "OwnerId";
        /// <summary>Идентификатор основной папки документа</summary>
        public const string FolderId = "FolderId";
        /// <summary>Папка документов</summary>
        public const string Folder = "Folder";
        /// <summary>Идентификатор корреспондента-отделения "Кто"</summary>
        public const string AgentDepartmentFromId = "AgentDepartmentFromId";
        /// <summary>Идентификатор корреспондента-отделения "Кому"</summary>
        public const string AgentDepartmentToId = "AgentDepartmentToId";
        /// <summary>Наименование корреспондента "Кто"</summary>
        public const string AgentFromName = "AgentFromName";
        /// <summary>Наименование корреспондента "Кому"</summary>
        public const string AgentToName = "AgentToName";
        /// <summary>Базовая валюта (валюта страны)</summary>
        public const string CurrencyBase = "CurrencyBase";
        /// <summary>Идентификатор валюты сделки</summary>
        public const string CurrencyTransactionId = "CurrencyTransactionId";
        /// <summary>Валюта сделки</summary>
        public const string CurrencyTransaction = "CurrencyTransaction";
        /// <summary>Сумма в базовой валюте</summary>
        public const string SummaBase = "SummaBase";
        /// <summary>Сумма в валюте сделки</summary>
        public const string SummaTransaction = "SummaTransaction";
        /// <summary>Сумма "Всего", расчитывается как Summa + SummaTax</summary>
        public const string SummaTotal = "SummaTotal";
        /// <summary>Время</summary>
        public const string Time = "Time";
        /// <summary>Идентификатор метода создания бухгалтеских проводок</summary>
        public const string AccountingWfId = "AccountingWfId";
        /// <summary>Метода создания бухгалтеских проводок</summary>
        public const string AccountingWf = "AccountingWf";
        /// <summary></summary>
        public const string Accounting = "Accounting";
        /// <summary>Идентификатор центра финансовой ответственности</summary>
        public const string CfoId = "CfoId";
        /// <summary>Центр финансовой ответственности</summary>
        public const string Cfo = "Cfo";
        /// <summary>Идентификатор предприятия, которому принадлежит документ</summary>
        public const string MyCompanyId = "MyCompanyId";
        /// <summary>Клиент, которому принадлежит документ</summary>
        public const string Client = "Client";
        /// <summary>Идентификатор клиента</summary>
        public const string ClientId = "ClientId";
        /// <summary></summary>
        public const string MyCompany = "MyCompany";
        /// <summary>Наименование корреспондента-отделения "Кто"</summary>
        public const string AgentDepartmentFromName = "AgentDepartmentFromName";
        /// <summary>Наименование корреспондента-отделения "Кому"</summary>
        public const string AgentDepartmentToName = "AgentDepartmentToName";
        /// <summary>Корреспондент "Кто"</summary>
        public const string AgentDepartmentFrom = "AgentDepartmentFrom";
        /// <summary>Корреспондент "Кому"</summary>
        public const string AgentDepartmentTo = "AgentDepartmentTo";
        /// <summary>Корреспондент "Кто"</summary>
        public const string AgentFrom = "AgentFrom";
        /// <summary>Идентификатор корреспондента "Кому"</summary>
        public const string AgentToId = "AgentToId";
        /// <summary>Корреспондент "Кому"</summary>
        public const string AgentTo = "AgentTo";
        /// <summary>Идентификатор базовой валюты (валюта страны)</summary>
        public const string CurrencyBaseId = "CurrencyBaseId";
        /// <summary>Группа</summary>
        public const string GroupNo = "GroupNo";
        /// <summary>Группа №2</summary>
        public const string GroupNo2 = "GroupNo2";
        /// <summary>Группа №3</summary>
        public const string GroupNo3 = "GroupNo3";
        /// <summary>Группа №4</summary>
        public const string GroupNo4 = "GroupNo4";
        /// <summary>Группа №5</summary>
        public const string GroupNo5 = "GroupNo5";
        /// <summary>Является вычисляемым столбцом</summary>
        public const string IsFormula = "IsFormula";
        /// <summary></summary>
        public const string TypeNameSql = "TypeNameSql";
        /// <summary></summary>
        public const string TypeNameNet = "TypeNameNet";
        /// <summary></summary>
        public const string TypeLen = "TypeLen";
        /// <summary>Описание</summary>
        public const string Description = "Description";
        /// <summary>Процедура импорта данных</summary>
        public const string ProcedureImport = "ProcedureImport";
        /// <summary>Процедура экспорта данных</summary>
        public const string ProcedureExport = "ProcedureExport";
        /// <summary>Подтип</summary>
        public const string SubKindId = "SubKindId";
        /// <summary>Наименование метода</summary>
        public const string Method = "Method";
        /// <summary>Наименование хранимой процедуры</summary>
        public const string Procedure = "Procedure";
        /// <summary>Схема данных</summary>
        public const string Schema = "Schema";
        /// <summary>Максимальный элементарный вид</summary>
        public const string MaxKind = "MaxKind";
        /// <summary>Идентификатор группы свойств</summary>
        public const string GroupId = "GroupId";
        /// <summary>Группа</summary>
        public const string Group = "Group";
        /// <summary>Идентификатор наименования свойства</summary>
        public const string PropertyId = "PropertyId";
        /// <summary>Наименование свойства</summary>
        public const string Property = "Property";
        /// <summary>Тип элемента</summary>
        public const string Entity = "Entity";
        /// <summary>Начало периода</summary>
        public const string Start = "Start";
        /// <summary>Конец периода</summary>
        public const string End = "End";
        /// <summary>Идентификатор системного типа ссылки</summary>
        public const string EntityReferenceId = "EntityReferenceId";
        /// <summary>Системный тип ссылки</summary>
        public const string EntityReference = "EntityReference";
        /// <summary>Идентификатор ссылки</summary>
        public const string ReferenceId = "ReferenceId";
        /// <summary></summary>
        public const string DbTypeSource = "DbTypeSource";
        /// <summary>Наименование столбца в базе данных</summary>
        public const string DataField = "DataField";
        /// <summary>Тип в базе данных</summary>
        public const string DatabaseType = "DatabaseType";
        /// <summary>Разрешено ли принимать значение NULL</summary>
        public const string AllowNull = "AllowNull";
        /// <summary>Идентификатор кода</summary>
        public const string CodeNameId = "CodeNameId";
        /// <summary>Наименование кода</summary>
        public const string CodeName = "CodeName";
        /// <summary></summary>
        public const string Summ = "Summ";
        /// <summary>Значение даты</summary>
        public const string DateValue = "DateValue";
        /// <summary>Значение даты №1</summary>
        public const string DateValue1 = "DateValue1";
        /// <summary>Значение даты №2</summary>
        public const string DateValue2 = "DateValue2";
        /// <summary>Значение даты №3</summary>
        public const string DateValue3 = "DateValue3";
        /// <summary>Значение даты №4</summary>
        public const string DateValue4 = "DateValue4";
        /// <summary>Значение даты №5</summary>
        public const string DateValue5 = "DateValue5";
        /// <summary>Код базы данных</summary>
        public const string DatabaseCode = "DatabaseCode";
        /// <summary>Имя базы данных</summary>
        public const string DatabaseName = "DatabaseName";
        /// <summary>Имя сервера базы данных</summary>
        public const string ServerName = "ServerName";
        /// <summary>IP-адрес сервера</summary>
// ReSharper disable InconsistentNaming
        public const string IP = "IP";
// ReSharper restore InconsistentNaming
        /// <summary>Пароль базы данных</summary>
        public const string Password = "Password";
        /// <summary>Пользователь базы данных</summary>
        public const string Uid = "Uid";
        /// <summary>Домен</summary>
        public const string Domain = "Domain";
        /// <summary>Тип аутентификации</summary>
        public const string Authentication = "Authentication";
        /// <summary>Идентификатор заведующего складом</summary>
        public const string StorekeeperId = "StorekeeperId";
        /// <summary>Заведующий складом</summary>
        public const string Storekeeper = "Storekeeper";
        /// <summary>Социальные привелегии</summary>
        public const string TaxSocialPrivilege = "TaxSocialPrivilege";
        /// <summary>Пол</summary>
        public const string Sex = "Sex";
        /// <summary>Имя</summary>
        public const string FirstName = "FirstName";
        /// <summary>Фамилия</summary>
        public const string LastName = "LastName";
        /// <summary>Отчетство</summary>
        public const string MidleName = "MidleName";
        /// <summary>День рождения</summary>
        public const string Birthday = "Birthday";
        /// <summary>Где родился</summary>
        public const string BirthTown = "BirthTown";
        /// <summary>Кем выдан паспорт</summary>
        public const string Whogives = "Whogives";
        /// <summary>Пол</summary>
        public const string Male = "Male";
        /// <summary>Табельный номер</summary>
        public const string TabNo = "TabNo";
        /// <summary>Материально ответственное лицо</summary>
        public const string Mol = "Mol";
        /// <summary>Серия</summary>
        public const string SeriesNo = "SeriesNo";
        /// <summary>Дата окончания</summary>
        public const string Expire = "Expire";
        /// <summary>Идентификатор организации выдавшей документ</summary>
        public const string AuthorityId = "AuthorityId";
        /// <summary>Корреспондент, организация, выдавшая документ</summary>
        public const string AgentAuthority = "AgentAuthority";
        /// <summary>Дата открытия категории</summary>
        public const string CategoryDate = "CategoryDate";
        /// <summary>Дата окончания категории</summary>
        public const string CategoryExpire = "CategoryExpire";
        /// <summary>Ограничения</summary>
        public const string Restriction = "Restriction";
        /// <summary>Международное наименование</summary>
        public const string InternationalName = "InternationalName";
        /// <summary>Ставка налогов</summary>
        public const string Tax = "Tax";
        /// <summary>Плательщик НДС</summary>
        public const string NdsPayer = "NdsPayer";
        /// <summary>Окпо</summary>
        public const string Okpo = "Okpo";
        /// <summary>Дата регистрации предприятия</summary>
        public const string RegDate = "RegDate";
        /// <summary>Идентификатор корреспондента, зарегистрироваашего компанию</summary>
        public const string RegisteredById = "RegisteredById";
        /// <summary>Корреспондент, зареистрировавший компанию</summary>
        public const string AgentRegister = "AgentRegister";
        /// <summary>Форма собственности</summary>
        public const string OwnershipId = "OwnershipId";
        /// <summary>Аналитика форма собственности</summary>
        public const string Ownership = "Ownership";
        /// <summary>Аналитика вид экономической деятельности</summary>
        public const string ActivityEconomic = "ActivityEconomic";
        /// <summary>Идентификатор аналитики отрасль деятельности</summary>
        public const string IndustryId = "IndustryId";
        /// <summary>Аналитика отрасль деятельности</summary>
        public const string Industry = "Industry";
        /// <summary>Номер свидетельства о регистрации</summary>
        public const string RegNumber = "RegNumber";
        /// <summary>Идентификатор аналитики тип торговой точки</summary>
        public const string TypeOutletId = "TypeOutletId";
        /// <summary>Аналитика тип торговой точки</summary>
        public const string TypeOutlet = "TypeOutlet";
        /// <summary>Идентификатор аналитики метраж</summary>
        public const string MetricAreaId = "MetricAreaId";
        /// <summary>Аналитика метраж</summary>
        public const string MetricArea = "MetricArea";
        /// <summary>Идентификатор аналитики категория торговой точки</summary>
        public const string CategoryId = "CategoryId";
        /// <summary>Аналитика категория торговой точки</summary>
        public const string Category = "Category";
        /// <summary>Идентификатор корреспондента "Торговый представитель</summary>
        public const string SalesRepresentativeId = "SalesRepresentativeId";
        /// <summary>Корреспондент торговый представитель</summary>
        public const string SalesRepresentative = "SalesRepresentative";
        /// <summary>Идентификатор корреспондента "Налоговая испекция"</summary>
        public const string TaxInspectionId = "TaxInspectionId";
        /// <summary>Корреспондент "Налоговая испекция"</summary>
        public const string TaxInspection = "TaxInspection";
        /// <summary>Номер регистрации в пенсионном фонде</summary>
        public const string RegPensionFund = "RegPensionFund";
        /// <summary>Номер регистрации в службе занятости</summary>
        public const string RegEmploymentService = "RegEmploymentService";
        /// <summary>Номер регистрации в фонде социального страхования от временной потери трудоспособности</summary>
        public const string RegSocialInsuranceDisability = "RegSocialInsuranceDisability";
        /// <summary>Номер регистрации в фонде социального страхования от несчатного случая</summary>
        public const string RegSocialInsuranceNesch = "RegSocialInsuranceNesch";
        /// <summary>Код ПФУ</summary>
        public const string RegPfu = "RegPfu";
        /// <summary>Код ОПФГ</summary>
        public const string RegOpfg = "RegOpfg";
        /// <summary>Код КОАТУУ</summary>
        public const string RegKoatu = "RegKoatu";
        /// <summary>Код КФВ</summary>
        public const string RegKfv = "RegKfv";
        /// <summary>Код ЗКГНГ</summary>
        public const string RegZkgng = "RegZkgng";
        /// <summary>Код по КВЕД</summary>
        public const string RegKved = "RegKved";
        /// <summary>Идентификатор директора</summary>
        public const string DirectorId = "DirectorId";
        /// <summary>Корреспондент директор</summary>
        public const string Director = "Director";
        /// <summary>Идентификатор главного бухгалтера</summary>
        public const string BuhId = "BuhId";
        /// <summary>Корреспондент бухгалтер</summary>
        public const string Buh = "Buh";
        /// <summary>Идентификатор кассира</summary>
        public const string CashierId = "CashierId";
        /// <summary>Идентификатор директора</summary>
        public const string Cashier = "Cashier";
        /// <summary>Идентификатор начальника по персоналу</summary>
        public const string PersonnelId = "PersonnelId";
        /// <summary>Корреспондент начальник по персоналу</summary>
        public const string Personnel = "Personnel";
        /// <summary>Мфо банка</summary>
        public const string Mfo = "Mfo";
        /// <summary>Географическое расположение</summary>
        public const string SpatialLocation = "SpatialLocation";
        /// <summary>Почтовый индекс</summary>
        public const string PostIndex = "PostIndex";
        /// <summary>Район</summary>
        public const string Territory = "Territory";
        /// <summary>Город</summary>
        public const string Town = "Town";
        /// <summary>Страна</summary>
        public const string Country = "Country";
        /// <summary>Бренд</summary>
        public const string Brand = "Brand";
        /// <summary>Идентификатор вид продукции</summary>
        public const string ProductTypeId = "ProductTypeId";
        /// <summary>Вид продукции</summary>
        public const string ProductType = "ProductType";
        /// <summary>Идентификатор упаковка</summary>
        public const string PakcTypeId = "PakcTypeId";
        /// <summary>Упаковка</summary>
        public const string PakcType = "PakcType";
        /// <summary>Данные XML</summary>
        public const string XmlData = "XmlData";
        /// <summary>Международный код</summary>
        public const string CodeInternational = "CodeInternational";
        /// <summary>Идентификатор территории, области</summary>
        public const string TerritoryId = "TerritoryId";
        /// <summary>Идентификатор страны</summary>
        public const string CountryId = "CountryId";
        /// <summary>Идентификатор города административного управления</summary>
        public const string TownId = "TownId";
        /// <summary>Идентификатор склада</summary>
        public const string StoreId = "StoreId";
        /// <summary>Склад</summary>
        public const string Store = "Store";
        /// <summary>Дата определяющая срок хранения "Годен до"</summary>
        public const string DateOut = "DateOut";
        /// <summary>Дата изготовления</summary>
        public const string DateOn = "DateOn";
        /// <summary>Дата прихода</summary>
        public const string DateIn = "DateIn";
        /// <summary>Библиотека</summary>
        public const string Library = "Library";
        /// <summary>Имя типа</summary>
        public const string ActivityName = "ActivityName";
        /// <summary>Идентификатор отчета</summary>
        public const string ReportId = "ReportId";
        /// <summary>Идентификатор первой ваюты</summary>
        public const string CurrencyFromId = "CurrencyFromId";
        /// <summary>Первая валюта</summary>
        public const string CurrencyFrom = "CurrencyFrom";
        /// <summary>Идентификатор второй валюты</summary>
        public const string CurrencyToId = "CurrencyToId";
        /// <summary>Вторая валюта</summary>
        public const string CurrencyTo = "CurrencyTo";
        /// <summary>Идентификатор состава</summary>
        public const string RecipeId = "RecipeId";
        /// <summary>Процент</summary>
        public const string Percent = "Percent";
        /// <summary>Номенклатурный номер</summary>
        public const string Nomenclature = "Nomenclature";
        /// <summary>Артикульный номер</summary>
        public const string Articul = "Articul";
        /// <summary>Каталожный номер</summary>
        public const string Cataloque = "Cataloque";
        /// <summary>Штрих код</summary>
        public const string Barcode = "Barcode";
        /// <summary>Вес</summary>
        public const string Weight = "Weight";
        /// <summary>Высота</summary>
        public const string Height = "Height";
        /// <summary>Ширина</summary>
        public const string Width = "Width";
        /// <summary>Глубина</summary>
        public const string Depth = "Depth";
        /// <summary>Типоразмер</summary>
        public const string Size = "Size";
        /// <summary>Период хранения</summary>
        public const string StoragePeriod = "StoragePeriod";
        /// <summary>Идентификатор изготовителя</summary>
        public const string ManufacturerId = "ManufacturerId";
        /// <summary>Изгтовитель</summary>
        public const string Manufacturer = "Manufacturer";
        /// <summary>Идентификатор торговой марки</summary>
        public const string TradeMarkId = "TradeMarkId";
        /// <summary>Торговая марка </summary>
        public const string TradeMark = "TradeMark";
        /// <summary>Идентификатор бренда</summary>
        public const string BrandId = "BrandId";
        /// <summary>Делитель</summary>
        public const string Divider = "Divider";
        /// <summary>Множитель</summary>
        public const string Multiplier = "Multiplier";
        /// <summary>Идентификатор вида цены</summary>
        public const string PriceNameId = "PriceNameId";
        /// <summary>Идентификатор корреспондента, которому принадлежат цены</summary>
        public const string AgentFromId = "AgentFromId";
        /// <summary>Идентификатор библиотеки</summary>
        public const string LibraryId = "LibraryId";
        /// <summary>Имя типа или метода</summary>
        public const string TypeName = "TypeName";
        /// <summary>Строковое обозначение типа</summary>
        public const string KindCode = "KindCode";
        /// <summary>Обобщение</summary>
        public const string IsGeneric = "IsGeneric";
        /// <summary>Полное имя типа</summary>
        public const string FullTypeName = "FullTypeName";
        /// <summary>Идентификатор файла библиотеки</summary>
        public const string AssemblyId = "AssemblyId";
        /// <summary>Идентификатор файла исходника</summary>
        public const string AssemblySourceId = "AssemblySourceId";
        /// <summary></summary>
        public const string AssemblyDll = "AssemblyDll";
        /// <summary></summary>
        public const string AssemblySource = "AssemblySource";
        /// <summary>Тип в родительской библиотеке</summary>
        public const string LibraryTypeId = "LibraryTypeId";
        /// <summary>Дополнительная строка параметров для отчетов ReportingService</summary>
        public const string Params = "Params";
        /// <summary>Ссылка для отчетов SQL Server Reporting Service</summary>
        public const string TypeUrl = "TypeUrl";
        /// <summary>Версия</summary>
        public const string AssemblyVersion = "AssemblyVersion";
        /// <summary>Идентификатор списка представлений</summary>
        public const string ListId = "ListId";
        /// <summary>Идентификатор подтипа системного объекта</summary>
        public const string EntityKindId = "EntityKindId";
        /// <summary>Сортировка</summary>
        public const string SortOrder = "SortOrder";
        /// <summary>Идентификатор системного типа содержимого</summary>
        public const string ContentEntityId = "ContentEntityId";
        /// <summary>Идентификатор списка</summary>
        public const string ViewListId = "ViewListId";
        /// <summary>Представление используемое для списка элементов</summary>
        public const string ViewList = "ViewList";
        /// <summary>Разрешенное содержимое в виде флага</summary>
        public const string ContentFlags = "ContentFlags";
        /// <summary>Идентификатор основной формы документов</summary>
        public const string FormId = "FormId";
        /// <summary>Форма</summary>
        public const string ProjectItem = "ProjectItem";
        /// <summary>Идентификатор шаблона операции</summary>
        public const string DocumentId = "DocumentId";
        /// <summary>Шаблон документа</summary>
        public const string Document = "Document";
        /// <summary>Идентификатор списка для отображения документов</summary>
        public const string ViewListDocumentsId = "ViewListDocumentsId";
        /// <summary>Cписк для отображения документов</summary>
        public const string ViewListDocuments = "ViewListDocuments";
        /// <summary>Данные</summary>
        public const string StreamData = "StreamData";
        /// <summary>Расширение файла</summary>
        public const string FileExtention = "FileExtention";
        /// <summary>Является ли список основанным на коллекции</summary>
        public const string IsCollectionBased = "IsCollectionBased";
        /// <summary>Наименование используемого представления, функции или процедуры</summary>
        public const string SystemName = "SystemName";
        /// <summary>Наименование процедуры обновления данных в строке представления</summary>
        public const string SystemNameRefresh = "SystemNameRefresh";
        /// <summary>Идентификатор системного типа которому принадлежит список</summary>
        public const string SourceEntityTypeId = "SourceEntityTypeId";
        /// <summary>Системный тип которому принадлежит список</summary>
        public const string SourceEntityType = "SourceEntityType";
        /// <summary>Отображать панель группировки</summary>
        public const string GroupPanelVisible = "GroupPanelVisible";
        /// <summary>Показывать строку автофильтра</summary>
        public const string AutoFilterVisible = "AutoFilterVisible";
        /// <summary>Дополнительные настройки</summary>
        public const string Options = "Options";
        /// <summary>Использовать описание Layout</summary>
        public const string UseLayout = "UseLayout";
        /// <summary>Идентификатор Layout используемый для построения списка</summary>
        public const string LayoutId = "LayoutId";
        /// <summary>Описатель Layout</summary>
        public const string Layout = "Layout";
        /// <summary>Возможность редактирования</summary>
        public const string EditAble = "EditAble";
        /// <summary>Индекс группировки</summary>
        public const string GroupIndex = "GroupIndex";
        /// <summary>Выравнивание в колонке</summary>
        public const string Alignment = "Alignment";
        /// <summary>Отображать текст заголовка</summary>
        public const string DisplayHeader = "DisplayHeader";
        /// <summary>Авто размер</summary>
        public const string AutoSizeMode = "AutoSizeMode";
        /// <summary>Закрепление</summary>
        public const string Frozen = "Frozen";
        /// <summary>Форматирование</summary>
        public const string Format = "Format";
        /// <summary>Видимость колонки</summary>
        public const string Visible = "Visible";
        /// <summary>Ширина</summary>
        public const string With = "With";
        /// <summary>Тип данных</summary>
        public const string DataType = "DataType";
        /// <summary>Список которому принадлежит колонка</summary>
        public const string CustomViewList = "CustomViewList";
        /// <summary>Идентификатор списка, которому принадлежит колонка</summary>
        public const string CustomViewListId = "CustomViewListId";
        /// <summary>Наименование столбца данных</summary>
        public const string DataProperty = "DataProperty";
        /// <summary>Номер</summary>
        public const string Owner = "Owner";
        /// <summary>Номер</summary>
        public const string Number = "Number";
        /// <summary>Идентификатор пользователя</summary>
        public const string UserId = "UserId";
        /// <summary>Числовой код</summary>
        public const string IntCode = "IntCode";
        /// <summary>Y</summary>
        public const string Y = "Y";
        /// <summary>X</summary>
        public const string X = "X";
        /// <summary>Z</summary>
        public const string Z = "Z";
        /// <summary>Числовой код ISO</summary>
        public const string IsoNum = "IsoNum";
        /// <summary>Администрация адресного пространства Интернет</summary>
        public const string Iana = "Iana";
        /// <summary>Соглашение по стандартизации</summary>
        public const string Stanag = "Stanag";
        /// <summary>Федеральные стандарты обработки информации</summary>
        public const string Fips = "Fips";
        /// <summary>ISO 3</summary>
        public const string Iso3 = "Iso3";
        /// <summary>ISO</summary>
        public const string Iso = "Iso";
        /// <summary>Банк</summary>
        public const string Bank = "Bank";
        /// <summary>Идентификатор банка</summary>
        public const string BankId = "BankId";
        /// <summary>Максимальное количество дней отсрочки</summary>
        public const string TimeDelay = "TimeDelay";
        /// <summary>Максимальная сумма задолженности</summary>
        public const string AmmountTrust = "AmmountTrust";
        /// <summary>Адрес фактический</summary>
        public const string AddressPhysical = "AddressPhysical";
        /// <summary>Адрес юридический</summary>
        public const string AddressLegal = "AddressLegal";
        /// <summary>Налоговый номер</summary>
        public const string CodeTax = "CodeTax";
        /// <summary>Тип свертки</summary>
        public const string TurnKind = "TurnKind";
        /// <summary>Сворачивать субсчета в оборотно-сальдовой ведомости</summary>
        public const string Turn = "Turn";
        /// <summary>Корреспондент</summary>
        public const string Agent = "Agent";
        /// <summary>Корреспондент</summary>
        public const string Agent2 = "Agent2";
        /// <summary>Корреспондент</summary>
        public const string Agent3 = "Agent3";
        /// <summary>Корреспондент</summary>
        public const string Agent4 = "Agent4";
        /// <summary>Корреспондент</summary>
        public const string Agent5 = "Agent5";
        /// <summary>Идентификатор корреспондента</summary>
        public const string AgentId = "AgentId";
        /// <summary>Резолюции</summary>
        public const string Resolution = "Resolution";
        /// <summary>Идентификатор резолюции</summary>
        public const string ResolutionId = "ResolutionId";
        /// <summary>Идентификатор пользователя отправителя</summary>
        public const string UserIdFrom = "UserIdFrom";
        /// <summary>Идентификатор пользователя получателя</summary>
        public const string UserIdTo = "UserIdTo";
        /// <summary>Текущее состояние</summary>
        public const string CurrentState = "CurrentState";
        /// <summary>Дата начала</summary>
        public const string DateStart = "DateStart";
        /// <summary>Дата окончания</summary>
        public const string DateEnd = "DateEnd";
        /// <summary>Автор</summary>
        public const string Author = "Author";
        /// <summary>Дата оплаты</summary>
        public const string DayToPay = "DayToPay";
        /// <summary>Идентификатор аналитики №5</summary>
        public const string AnaliticId5 = "AnaliticId5";
        /// <summary>Идентификатор аналитики №4</summary>
        public const string AnaliticId4 = "AnaliticId4";
        /// <summary>Идентификатор аналитики №3</summary>
        public const string AnaliticId3 = "AnaliticId3";
        /// <summary>Идентификатор аналитики №2</summary>
        public const string AnaliticId2 = "AnaliticId2";
        /// <summary>Идентификатор аналитики</summary>
        public const string AnaliticId = "AnaliticId";
        /// <summary>Системный объект</summary>
        public const string ToEntity = "ToEntity";
        /// <summary>Идентификатор вида цены</summary>
        public const string PriceId = "PriceId";
        /// <summary>Количество в базовой единице при использовании производной</summary>
        public const string QtyBase = "QtyBase";
        /// <summary>Фактическое количество</summary>
        public const string QtyFact = "QtyFact";
        /// <summary>Фактическая сумма</summary>
        public const string SummaFact = "SummaFact";
        /// <summary>Телефон организации</summary>
        public const string Phone = "Phone";
        /// <summary>Номер договора в налоговых</summary>
        public const string DogovorNo = "DogovorNo";
        /// <summary>Дата договора в налоговых</summary>
        public const string DogovorDate = "DogovorDate";
        /// <summary>Вида платежа или денежной операции</summary>
        public const string PaymentType = "PaymentType";
        /// <summary>Идентификатор вида платежа</summary>
        public const string PaymentTypeId = "PaymentTypeId";
        /// <summary>Формула</summary>
        public const string Formula = "Formula";
        /// <summary>Сумма списания декларативная</summary>
        public const string SummaLastIn = "SummaLastIn";
        /// <summary>Сумма списания декларативная</summary>
        public const string SummaIn = "SummaIn";
        /// <summary>Иднентификатор "справа"</summary>
        public const string RightId = "RightId";
        /// <summary>Иднентификатор "слева"</summary>
        public const string LeftId = "LeftId";
        /// <summary>Код поиска</summary>
        public const string CodeFind = "CodeFind";
        /// <summary>Полное наименование</summary>
        public const string NameFull = "NameFull";
        /// <summary>Год</summary>
        public const string Year = "Year";
        /// <summary>Месяц</summary>
        public const string Month = "Month";
        /// <summary>Сумма списания</summary>
        public const string SummaExp = "SummaExp";
        /// <summary>Идентификатор документа торговли</summary>
        public const string SalesDocId = "SalesDocId";
        /// <summary>Условия поставки</summary>
        public const string DeliveryCondition = "DeliveryCondition";
        /// <summary>Метод расчетов</summary>
        public const string PaymentMethod = "PaymentMethod";
        /// <summary>Идентификатор условий поставки</summary>
        public const string DeliveryConditionId = "DeliveryConditionId";
        /// <summary>Идентификатор метода расчетов</summary>
        public const string PaymentMethodId = "PaymentMethodId";
        /// <summary>Полное наименование</summary>
        public const string FullName = "NameFull";
        /// <summary>Причина возврата</summary>
        public const string ReturnReason = "ReturnReason";
        /// <summary>Идентификатор причины возврата</summary>
        public const string ReturnReasonId = "ReturnReasonId";
        /// <summary>Дата "Оплатить до"</summary>
        public const string DatePay = "DatePay";
        /// <summary>Дата фактической отгрузки</summary>
        public const string DateShip = "DateShip";
        /// <summary>Склад получателя</summary>
        public const string StoreTo = "StoreTo";
        /// <summary>Идентификатор склада получателя</summary>
        public const string StoreToId = "StoreToId";
        /// <summary>Склад отправителя</summary>
        public const string StoreFrom = "StoreFrom";
        /// <summary>Идентификатор склада отправителя</summary>
        public const string StoreFromId = "StoreFromId";
        /// <summary>Идентификатор налогового документа</summary>
        public const string TaxDocId = "TaxDocId";
        /// <summary>Перевозчик</summary>
        public const string AgentDelivery = "AgentDelivery";
        /// <summary>Идентификатор перевозчика</summary>
        public const string DeliveryId = "DeliveryId";
        /// <summary>Торговый представитель</summary>
        public const string Manager = "Manager";
        /// <summary>Идентификатор торгового представителя</summary>
        public const string ManagerId = "ManagerId";
        /// <summary>Идентификатор супервизора</summary>
        public const string Supervisor = "Supervisor";
        /// <summary>Идентификатор супервизора</summary>
        public const string SupervisorId = "SupervisorId";
        /// <summary>Идентификатор вида цены</summary>
        public const string PrcNameId = "PrcNameId";
        /// <summary>Вид цены</summary>
        public const string PriceName = "PriceName";
        /// <summary>Идентификатор в базе источнике</summary>
        public const string DbSourceId = "DbSourceId";
        /// <summary>Дата</summary>
        public const string Date = "Date";
        /// <summary>Сумма налогов</summary>
        public const string SummaTax = "SummaTax";
        /// <summary>Идентификатор производной единицы измерения</summary>
        public const string FUnitId = "FUnitId";
        /// <summary>Количество в производной единице измерения</summary>
        public const string FQty = "FQty";
        /// <summary>Идентификатор налоговой аналитики</summary>
        public const string TaxAnaliticId = "TaxAnaliticId";
        /// <summary>Количество</summary>
        public const string Qty = "Qty";
        /// <summary>Количество</summary>
        public const string Qty2 = "Qty2";
        /// <summary>Количество</summary>
        public const string Qty3 = "Qty3";
        /// <summary>Количество</summary>
        public const string Qty4 = "Qty4";
        /// <summary>Количество</summary>
        public const string Qty5 = "Qty5";
        /// <summary>Сумма</summary>
        public const string Summa = "Summa";
        /// <summary>Сумма</summary>
        public const string Summa2 = "Summa2";
        /// <summary>Сумма</summary>
        public const string Summa3 = "Summa3";
        /// <summary>Сумма</summary>
        public const string Summa4 = "Summa4";
        /// <summary>Сумма</summary>
        public const string Summa5 = "Summa5";
        /// <summary>Цена</summary>
        public const string Price = "Price";
        /// <summary>Цена</summary>
        public const string Price2 = "Price2";
        /// <summary>Цена</summary>
        public const string Price3 = "Price3";
        /// <summary>Цена</summary>
        public const string Price4 = "Price4";
        /// <summary>Цена</summary>
        public const string Price5 = "Price5";
        /// <summary>Единица измерения</summary>
        public const string Unit = "Unit";
        /// <summary>Товар</summary>
        public const string Product = "Product";
        /// <summary>Идентификатор единицы измерения</summary>
        public const string UnitId = "UnitId";
        /// <summary>Идентификатор типа</summary>
        public const string FromEntityId = "FromEntityId";
        /// <summary>Идентификатор подтипа</summary>
        public const string SubKind = "SubKind";
        /// <summary>Идентификатор подтипа элемента</summary>
        public const string KindValue = "KindValue";
        /// <summary>Дополнительный строковый флаг</summary>
        public const string FlagString = "FlagString";
        /// <summary>Идентификатор родителя</summary>
        public const string ParentId = "ParentId";
        /// <summary>Родитель</summary>
        public const string Parent = "Parent";
        /// <summary>Номер в списке</summary>
        public const string OrderNo = "OrderNo";
        /// <summary>Язык</summary>
        public const string CultureInfo = "CultureInfo";
        /// <summary>Отображаемое имя</summary>
        public const string DisplayName = "DisplayName";
        /// <summary>Идентификатор языка</summary>
        public const string CultureId = "CultureId";
        /// <summary>Валюта</summary>
        public const string Currency = "Currency";
        /// <summary>Идентификатор валюты</summary>
        public const string CurrencyId = "CurrencyId";
        /// <summary>Значение</summary>
        public const string Value = "Value";
        /// <summary>Прошлое значение</summary>
        public const string ValueOld = "ValueOld";
        /// <summary>Коэффициент скидки</summary>
        public const string Discount = "Discount";
        /// <summary>Идентификатор</summary>
        public const string Id = "Id";
        /// <summary>Глобальный идентификатор</summary>
        public const string Guid = "Guid";
        /// <summary>Наиенование</summary>
        public const string Name = "Name";
        /// <summary>Код</summary>
        public const string Code = "Code";
        /// <summary>Тип</summary>
        public const string Kind = "Kind";
        /// <summary>Идентификатор типа</summary>
        public const string KindId = "KindId";
        /// <summary>Примечание</summary>
        public const string Memo = "Memo";
        /// <summary>Идентификатор владельца</summary>
        public const string DatabaseId = "DatabaseId";
        /// <summary>Идентификатор системного типа</summary>
        public const string EntityId = "EntityId";
        /// <summary>Идентификатор состояния</summary>
        public const string StateId = "StateId";
        /// <summary>Состояние</summary>
        public const string State = "State";
        /// <summary>Идентификатор источника</summary>
        public const string SourceId = "DbSourceId";
        /// <summary>Идентификатор шаблона</summary>
        public const string TemplateId = "TemplateId";
        /// <summary>Шаблон</summary>
        public const string Template = "Template";
        /// <summary>Значение флага</summary>
        public const string FlagsValue = "FlagsValue";
        /// <summary>Рабочая область</summary>
        public const string Workarea = "Workarea";
        /// <summary>Версия</summary>
        public const string Version = "Version";
        /// <summary>Имя пользователя</summary>
        public const string UserName = "UserName";
        /// <summary>Дата изменения</summary>
        public const string DateModified = "DateModified";
        /// <summary>Является ли объект новым</summary>
        public const string IsNew = "IsNew";
        /// <summary>Идентификатор системного объекта</summary>
        public const string ToEntityId = "ToEntityId";
        /// <summary>Идентификатор владельца</summary>
        public const string OwnId = "OwnId";
        /// <summary>Идентификатор метода построения виртуальной иеархии</summary>
        public const string VirtualBuildId = "VirtualBuildId";
        /// <summary>Идентификатор элемента</summary>
        public const string ElementId = "ElementId";
        /// <summary>Идентификатор иерархии</summary>
        public const string HierarchyId = "HierarchyId";
		/// <summary>Идентификатор товара</summary>
		public const string ProductId = "ProductId";
		/// <summary>Номер чертежа</summary>
		public const string DrawingNumber = "DrawingNumber";
		/// <summary>Номер сборочного чертежа</summary>
		public const string DrawingAssemblyNumber = "DrawingAssemblyNumber";
		/// <summary>Дата изготовления элемента образа оборудования</summary>
		public const string DateBuild = "DateBuild";
		/// <summary>Государственный номер автомобиля</summary>
		public const string GosNumber = "GosNumber";
		/// <summary>Id Используемого топлива</summary>
		public const string UsedFuelId = "UsedFuelId";
		/// <summary>Используемое топливо</summary>
		public const string UsedFuel = "UsedFuel";
		/// <summary>Номер тех. паспорта автомобиля</summary>
		public const string AutoPassportNumber = "AutoPassportNumber";
		/// <summary>Номер шасси автомобиля</summary>
		public const string AutoChassis = "AutoChassis";
		/// <summary>Номер двигателя автомобиля</summary>
		public const string AutoMotor = "AutoMotor";
		/// <summary>Норма расхода топлива</summary>
		public const string RateFuelConsumption = "RateFuelConsumption";
		/// <summary>Полная масса</summary>
		public const string GrossVehicleWeight = "GrossVehicleWeight";
		/// <summary>Собственная масса</summary>
		public const string UnladenWeight = "UnladenWeight";
		/// <summary>Объем двигнателя</summary>
		public const string VolumeVehicleEngine = "VolumeVehicleEngine";
		/// <summary>Номер лицензии</summary>
		public const string LicenseNumber = "LicenseNumber";
		/// <summary>Дата начала лицензии</summary>
		public const string LicenseDateStart = "LicenseDateStart";
		/// <summary>Дата окончания лицензии</summary>
		public const string LicenseDateExpiration = "LicenseDateExpiration";
		/// <summary>Дата начала страховки</summary>
		public const string InsuranceDateStart = "InsuranceDateStart";
		/// <summary>Дата окончания страховки</summary>
		public const string InsuranceDateExpiration = "InsuranceDateExpiration";
		/// <summary>Номер санитарного пасспорта</summary>
		public const string SanitaryNumber = "SanitaryNumber";
		/// <summary>Дата начала санитарного пасспорта</summary>
		public const string SanitaryDateStart = "SanitaryDateStart";
		/// <summary>Дата окончания санитарного пасспорта</summary>
		public const string SanitaryDateExpiration = "SanitaryDateExpiration";
		/// <summary>Id Вида ТС по тоннажу</summary>
		public const string TonnageKindId = "TonnageKindId";
		/// <summary>Вид ТС по тоннажу</summary>
		public const string TonnageKind = "TonnageKind";
		/// <summary>Id Типа транспортного средства</summary>
		public const string AutoTypeId = "AutoTypeId";
		/// <summary>Тип транспортного средства</summary>
		public const string AutoType = "AutoType";
		/// <summary>Id Закрепленного за прицепом автомобиля</summary>
		public const string TrailerUsedAutoId = "TrailerUsedAutoId";
		/// <summary>Закрепленный за прицепом автомобиль</summary>
		public const string TrailerUsedAuto = "TrailerUsedAuto";
		/// <summary>Отсекаемый объем слева</summary>
		public const string InterceptedVolumesLeft = "InterceptedVolumesLeft";
		/// <summary>Отсекаемый объем справа</summary>
		public const string InterceptedVolumesRight = "InterceptedVolumesRight";
		/// <summary>Паллетоместа</summary>
		public const string Pallet = "Pallet";
		/// <summary>Дополнительный бак</summary>
		public const string AdditionalFuelTank = "AdditionalFuelTank";
		/// <summary>Id модели авто</summary>
		public const string EquipmentId = "EquipmentId";
		/// <summary>Модель авто</summary>
		public const string Equipment = "Equipment";
	}
}
