namespace BusinessObjects
{
    /// <summary>
    /// Стандартные наименования параметров хранимых процедур
    /// </summary>
    public static class GlobalSqlParamNames
    {
        /// <summary>Максимальный размер</summary>
        public const string MaxSize = "@MaxSize";
        /// <summary>Идентификатор перевозчика</summary>
        public const string ShippingId = "@ShippingId";
        /// <summary>График работы или перерыва, завист от места использования. 
        /// Например для пользователя - график разрешенного входа в систему, для корреспондента - текущее время работы и переывов.
        /// </summary>
        public const string TimePeriodId = "@TimePeriodId";
        /// <summary>Разрешить смену пароля пользователем</summary>
        public const string AllowChangePassword = "@AllowChangePassword";
        /// <summary>Рекомендуемая дата смены пароля</summary>
        public const string RecommendedDateChangePassword = "@RecommendedDateChangePassword";
        /// <summary>Автоматически генерировать следующий пароль</summary>
        public const string AutogenerateNextPassword = "@AutogenerateNextPassword";
        /// <summary>Идентификатор подтипа объекта источника</summary>
        public const string EntityKindIdFrom = "@EntityKindIdFrom";
        /// <summary>Идентификатор холдинга</summary>
        public const string HoldingId = "@HoldingId";
        /// <summary>Идентификатор клиента</summary>
        public const string ClientId = "@ClientId";
        /// <summary>Собственное имя типа</summary>
        public const string TypeNameValue = "@TypeNameValue";
        /// <summary>Собственный идентификатор модели</summary>
        public const string ModelId = "@ModelId";
        /// <summary>Время жизни</summary>
        public const string TimeLive = "@TimeLive";
        /// <summary>Реальный адрес пользователя (используется в Web)</summary>
        public const string RemoteAddr = "@RemoteAddr";
        /// <summary>Код версии</summary>
        public const string VersionCode = "@VersionCode";
        /// <summary>Идентификатор сотрудника заместителя</summary>
        public const string AgentToSubId = "@AgentToSubId";
        /// <summary>Отметка отправителя</summary>
        public const string MarkedOwner = "@MarkedOwner";
        /// <summary>Уровень отметки отправителя</summary>
        public const string MarkScoreOwner = "@MarkScoreOwner";
        /// <summary>Отметка получателя</summary>
        public const string MarkedRecipient = "@MarkedRecipient";
        /// <summary>Уровень отметки получателя</summary>
        public const string MarkScoreRecipient = "@MarkScoreRecipient";
        /// <summary>Только пользователи</summary>
        public const string OnlyUsers = "@OnlyUsers";
        /// <summary>Идентификатор уровня группы</summary>
        public const string GroupLevelId = "@GroupLevelId";
        /// <summary>Идентификатор отдела</summary>
        public const string DepatmentId = "@DepatmentId";
        /// <summary>Радиус зоны (для GPS мониторинга)</summary>
        public const string ZoneRadius = "@ZoneRadius";
        /// <summary>Время</summary>
        public const string Time = "@Time";
        /// <summary>Дата POS</summary>
        public const string PosDate = "@PosDate";
        /// <summary>Время POS</summary>
        public const string PosTime = "@PosTime";
        /// <summary>Дата RTS</summary>
        public const string RtcDate = "@RtcDate";
        /// <summary>Время RTS</summary>
        public const string RtcTime = "@RtcTime";
        /// <summary>Значение с первого аналогового входа</summary>
        public const string Input1 = "@Input1";
        /// <summary>Значение со второго аналогового входа</summary>
        public const string Input2 = "@Input2";
        /// <summary>Статус устройства</summary>
        public const string IOStatus = "@IOStatus";
        /// <summary>Число спутников</summary>
        public const string Satellites = "@Satellites";
        /// <summary>Точность замеров</summary>
        public const string HDOP = "@HDOP";
        /// <summary>Направление движения</summary>
        public const string Direction = "@Direction";
        /// <summary>Высота</summary>
        public const string Altitude = "@Altitude";
        /// <summary>Код статуса</summary>
        public const string StatusCode = "@StatusCode";
        /// <summary>Адрес корреспондента</summary>
        public const string AddressId = "@AddressId";
        /// <summary>Показания одометра</summary>
        public const string Odometer = "@Odometer";
        /// <summary>Дистанция</summary>
        public const string Distance = "@Distance";
        /// <summary>Скорость движения</summary>
        public const string Speed = "@Speed";
        /// <summary>Идентификатор объекта слежения</summary>
        public const string RouteMemberId = "@RouteMemberId";
        /// <summary>Идентификатор устройства</summary>
        public const string DeviceId = "@DeviceId";
        /// <summary>Идентификатор сотрудника</summary>
        public const string EmployerId = "@EmployerId";
        /// <summary>Значение 1</summary>
        public const string Value1 = "@Value1";
        /// <summary>Значение 2</summary>
        public const string Value2 = "@Value2";
        /// <summary>Значение 3</summary>
        public const string Value3 = "@Value3";
        /// <summary>Значение 4</summary>
        public const string Value4 = "@Value4";
        /// <summary>Значение 5</summary>
        public const string Value5 = "@Value5";
        /// <summary>Значение 6</summary>
        public const string Value6 = "@Value6";
        /// <summary>Значение 7</summary>
        public const string Value7 = "@Value7";
        /// <summary>Значение 8</summary>
        public const string Value8 = "@Value8";
        /// <summary>Значение 9</summary>
        public const string Value9 = "@Value9";
        /// <summary>Значение 10</summary>
        public const string Value10 = "@Value10";
        /// <summary>Значение 11</summary>
        public const string Value11 = "@Value11";
        /// <summary>Значение 12</summary>
        public const string Value12 = "@Value12";
        /// <summary>Значение 13</summary>
        public const string Value13 = "@Value13";
        /// <summary>Значение 14</summary>
        public const string Value14 = "@Value14";
        /// <summary>Значение 15</summary>
        public const string Value15 = "@Value15";
        /// <summary>Идентификатор заместителя</summary>
        public const string DepatmentSubHeadId = "@DepatmentSubHeadId";
        /// <summary>Идентификатор руководителя</summary>
        public const string DepatmentHeadId = "@DepatmentHeadId";
        /// <summary>Идентификатор сотрудника фактически выполнившего подписание</summary>
        public const string AgentSignId = "@AgentSignId";
        /// <summary>Дата фактической подписи</summary>
        public const string DateSign = "@DateSign";
        /// <summary>Необходимость формировать задачу</summary>
        public const string TaskNeed = "@TaskNeed";
        /// <summary>Необходимость формировать сообщение</summary>
        public const string MessageNeed = "@MessageNeed";
        /// <summary>Идентификатор сообщения</summary>
        public const string MessageId = "@MessageId";
        /// <summary>Идентификатор задачи</summary>
        public const string TaskId = "@TaskId";
        /// <summary>Тип подписания ИЛИ/И</summary>
        public const string SignKind = "@SignKind";
        /// <summary>Является ли главным подписантом</summary>
        public const string IsMain = "@IsMain";
        /// <summary>Заместитель</summary>
        public const string AgentSubId = "@AgentSubId";

        /// <summary>Url справочной информации</summary>
        public const string HelpUrl = "HelpUrl";
        /// <summary>Использовать минимальные значения</summary>
        public const string UseMin = "@UseMin";
        /// <summary>Разрешено</summary>
        public const string IsAllow = "@IsAllow";
        /// <summary>Время старта</summary>
        public const string StartTime = "@StartTime";
        /// <summary>Дата старта</summary>
        public const string StartDate = "@StartDate";
        /// <summary>Идентификатор логотипа</summary>
        public const string LogoId = "@LogoId";
        /// <summary>Идентификатор печати</summary>
        public const string LogostampId = "@LogostampId";
        /// <summary>Идентификатор подписи</summary>
        public const string LogoSignId = "@LogoSignId";
        /// <summary>Идентификатор несовершеннолетия</summary>
        public const string MinorsId = "@MinorsId";
        /// <summary>Идентификатор места хранения трудовой</summary>
        public const string PlaceEmploymentBookId = "@PlaceEmploymentBookId";
        /// <summary>Официальный сотрудник</summary>
        public const string LegalWorker = "@LegalWorker";
        /// <summary>Пенсионность</summary>
        public const string Pension = "@Pension";
        /// <summary>Инвалидность</summary>
        public const string Invalidity = "@Invalidity";
        /// <summary>Идентификатор последнего места работы</summary>
        public const string LastPlaceWorkId = "@LastPlaceWorkId";
        /// <summary>Номер страхового свидетельства</summary>
        public const string InsuranceNumber = "@InsuranceNumber";
        /// <summary>Серия страхового свидетельства</summary>
        public const string InsuranceSeries = "@InsuranceSeries";
        /// <summary>Отображать индикатор</summary>
        public const string ShowIndicator = "@ShowIndicator";
        /// <summary>Примечание в виде текста</summary>
        public const string MemoTxt = "@MemoTxt";
        /// <summary>Код класса</summary>
        public const string CodeClass = "@CodeClass";
        /// <summary>Наименоване схемы</summary>
        public const string NameSchema = "@NameSchema";
        /// <summary>Код номера телефона для АМТС</summary>
        public const string Amts = "@Amts";
        /// <summary>Плотность населения людей/кв.км</summary>
        public const string PopulationDensity = "@PopulationDensity";
        /// <summary>Население в тысячах людей</summary>
        public const string Population = "@Population";
        /// <summary>Территория в тис. кв. км.</summary>
        public const string TerritoryKvKm = "@TerritoryKvKm";
        /// <summary>Прошлое наименование</summary>
        public const string NameOld = "@NameOld";
        /// <summary>Дата основания</summary>
        public const string DateFoundation = "@DateFoundation";
        /// <summary>Международное наименование</summary>
        public const string NameInternational = "@NameInternational";
        /// <summary>Национальное наименование</summary>
        public const string NameNational = "@NameNational";
        /// <summary>Время отправки</summary>
        public const string SendTime = "@SendTime";
        /// <summary>Дата отправки</summary>
        public const string SendDate = "@SendDate";
        /// <summary>Отправлено</summary>
        public const string IsSend = "@IsSend";
        /// <summary>Идентификатор типа рекурсии</summary>
        public const string RecursiveId = "@RecursiveId";
        /// <summary>Дата планового запуска</summary>
        public const string StartPlanDate = "@StartPlanDate";
        /// <summary>Время планового запуска</summary>
        public const string StartPlanTime = "@StartPlanTime";
        /// <summary>Ежедневно</summary>
        public const string EveryDay = "@IsRecurcive";
        /// <summary>Идентификатор процесса владельца</summary>
        public const string WfOwnerId = "@WfOwnerId";
        /// <summary>Идентификатор процесса для запуска</summary>
        public const string WfToStartId = "@WfToStartId";
        /// <summary>Идентификатор цвета</summary>
        public const string ColorId = "@ColorId";
        /// <summary>Идентификатор областного района</summary>
        public const string RegionId = "@RegionId";
        /// <summary>Время прочтения</summary>
        public const string ReadTime = "@ReadTime";
        /// <summary>Дата прочтения</summary>
        public const string ReadDate = "@ReadDate";
        /// <summary>Требовать уведомления</summary>
        public const string HasRead = "@HasRead";
        /// <summary>Уведомление</summary>
        public const string ReadDone = "@ReadDone";
        /// <summary>Суфикс</summary>
        public const string Prefix = "@Prefix";
        /// <summary>Суфикс</summary>
        public const string Suffix = "@Suffix";
        /// <summary>Ид склада</summary>
        public const string StoreId = "@StoreId";
        /// <summary>Признак собственного, частного</summary>
        public const string GroupByFlags = "@GroupByFlags";
        /// <summary>Признак собственного, частного</summary>
        public const string InPrivate = "@InPrivate";
        /// <summary>Дата планового старта</summary>
        public const string DateStartPlan = "@DateStartPlan";
        /// <summary>Дата планового старта</summary>
        public const string DateEndPlan = "@DateEndPlan";
        /// <summary>Время планового старта</summary>
        public const string DateStartPlanTime = "@DateStartPlanTime";
        /// <summary>Время начала</summary>
        public const string DateStartTime = "@DateStartTime";
        /// <summary>Плановое время окончания</summary>
        public const string DateEndPlanTime = "@DateEndPlanTime";
        /// <summary>Время окончания</summary>
        public const string DateEndTime = "@DateEndTime";
        /// <summary>Идентификатор состояния задачи</summary>
        public const string TaskStateId = "@TaskStateId";
        /// <summary>Номер задания</summary>
        public const string TaskNumber = "@TaskNumber";
        /// <summary>Процент выполнения</summary>
        public const string DonePersent = "@DonePersent";
        /// <summary>Идентификатор пользователя "Кому"</summary>
        public const string UserToId = "@UserToId";
        /// <summary>Использовать фильтр по И</summary>
        public const string UseAndFilter = "@UseAndFilter";
        /// <summary>Идентификатор страны</summary>
        public const string CountryId = "@CountryId";
        /// <summary>Идентификатор территории</summary>
        public const string TerritoryId = "@TerritoryId";
        /// <summary>Идентификатор города</summary>
        public const string TownId = "@TownId";
        /// <summary>Почтовый индекс</summary>
        public const string PostIndex = "@PostIndex";
        /// <summary>Идентификатор компании владельца</summary>
        public const string MyCompanyId = "@MyCompanyId";
        /// <summary>Понедельник - рабочий день</summary>
        public const string MondayW = "@MondayW";
        /// <summary>Вторник - рабочий день</summary>
        public const string TuesdayW = "@TuesdayW";
        /// <summary>Среда - рабочий день</summary>
        public const string WednesdayW = "@WednesdayW";
        /// <summary>Четверг - рабочий день</summary>
        public const string ThursdayW = "@ThursdayW";
        /// <summary>Пятница - рабочий день</summary>
        public const string FridayW = "@FridayW";
        /// <summary>Суббота - рабочий день</summary>
        public const string SaturdayW = "@SaturdayW";
        /// <summary>Воскресенье - рабочий день</summary>
        public const string SundayW = "@SundayW";
        
        /// <summary>Понедельник - началов в часов </summary>
        public const string MondaySH = "@MondaySH";
        /// <summary>Понедельник - начало миниут</summary>
        public const string MondaySM = "@MondaySM";
        /// <summary>Понедельник - конец в часов</summary>
        public const string MondayEH = "@MondayEH";
        /// <summary>Понедельник - конец минут</summary>
        public const string MondayEM = "@MondayEM";
        /// <summary>Вторник - началов в часов</summary>
        public const string TuesdaySH = "@TuesdaySH";
        /// <summary>Вторник - начало миниут</summary>
        public const string TuesdaySM = "@TuesdaySM";
        /// <summary>Вторник - конец в часов</summary>
        public const string TuesdayEH = "@TuesdayEH";
        /// <summary>Вторник - конец минут</summary>
        public const string TuesdayEM = "@TuesdayEM";
        /// <summary>Среда - началов в часов</summary>
        public const string WednesdaySH = "@WednesdaySH";
        /// <summary>Среда - начало минут</summary>
        public const string WednesdaySM = "@WednesdaySM";
        /// <summary>Среда - конец минут</summary>
        public const string WednesdayEM = "@WednesdayEM";
        /// <summary>Среда - конец часов</summary>
        public const string WednesdayEH = "@WednesdayEH";
        /// <summary>Четверг - началов в часов</summary>
        public const string ThursdaySH = "@ThursdaySH";
        /// <summary>Четверг - начало минут</summary>
        public const string ThursdaySM = "@ThursdaySM";
        /// <summary>Четверг - конец минут</summary>
        public const string ThursdayEM = "@ThursdayEM";
        /// <summary>Четверг - конец часов</summary>
        public const string ThursdayEH = "@ThursdayEH";
        /// <summary>Пятница - началов в часов</summary>
        public const string FridaySH = "@FridaySH";
        /// <summary>Пятница - начало минут</summary>
        public const string FridaySM = "@FridaySM";
        /// <summary>Пятница - конец в часов</summary>
        public const string FridayEH = "@FridayEH";
        /// <summary>Пятница - конец минут</summary>
        public const string FridayEM = "@FridayEM";
        /// <summary>Суббота - началов в часов</summary>
        public const string SaturdaySH = "@SaturdaySH";
        /// <summary>Суббота - начало минут</summary>
        public const string SaturdaySM = "@SaturdaySM";
        /// <summary>Суббота - конец в часов</summary>
        public const string SaturdayEH = "@SaturdayEH";
        /// <summary>Суббота - конец минут</summary>
        public const string SaturdayEM = "@SaturdayEM";
        /// <summary>Воскресенье -началов в часов </summary>
        public const string SundaySH = "@SundaySH";
        /// <summary>Воскресенье - конец в часов</summary>
        public const string SundayEH = "@SundayEH";
        /// <summary>Воскресенье - начало минут</summary>
        public const string SundaySM = "@SundaySM";
        /// <summary>Воскресенье - конец минут</summary>
        public const string SundayEM = "@SundayEM";
        
        /// <summary>Кореспондентский счет</summary>
        public const string CorrBankAccount = "@CorrBankAccount";
        /// <summary>S.W.I.F.T.</summary>
        public const string Swift = "@Swift";
        /// <summary>Дата банковской лицензии</summary>
        public const string LicenseDate = "@LicenseDate";
        /// <summary>Номер банковской лицензии</summary>
        public const string LicenseNo = "@LicenseNo";
        /// <summary>Дата свидетельства регистрации НБУ, дата сертификата</summary>
        public const string SertificateDate = "@SertificateDate";
        /// <summary>Номер свидетельства регистрации НБУ, номер сертификата</summary>
        public const string SertificateNo = "@SertificateNo";
        /// <summary>Использовать рекурсию</summary>
        public const string Nested = "@Nested";
        /// <summary>Ид герба</summary>
        public const string EmblemId = "@EmblemId";
        /// <summary>Континент</summary>
        public const string Continent = "@Continent";
        /// <summary>Хеш пароля</summary>
        public const string PasswordHash = "@PasswordHash";
        /// <summary>Новый Email</summary>
        public const string NewEmailKey = "@NewEmailKey";
        /// <summary>Дата создание</summary>
        public const string DateCreated = "@DateCreated";
        /// <summary>Секретный вопрос</summary>
        public const string PasswordQuestion = "@PasswordQuestion";
        /// <summary>Salt пароль</summary>
        public const string PasswordSalt = "@PasswordSalt";
        /// <summary>Дата последненго изменения пароля</summary>
        public const string LastPasswordChangedDate = "@LastPasswordChangedDate";
        /// <summary>Дата последнего входа</summary>
        public const string LastLoginDate = "@LastLoginDate";
        /// <summary>Дата последней блокировки</summary>
        public const string LastLockedOutDate = "@LastLockedOutDate";
        /// <summary>Дата последней активности</summary>
        public const string LastActivityDate = "@LastActivityDate";
        /// <summary>Максимальное значение</summary>
        public const string ValueMin = "@ValueMin";
        /// <summary>Минимальное значение</summary>
        public const string ValueMax = "@ValueMax";
        /// <summary>Порт</summary>
        public const string Port = "@Port";
        /// <summary></summary>
        public const string BindingType = "@BindingType";
        /// <summary></summary>
        public const string ServiceContract = "@ServiceContract";
        /// <summary></summary>
        public const string UseHttps = "@UseHttps";
        /// <summary></summary>
        public const string InternalClientConfiguration = "@InternalClientConfiguration";
        /// <summary></summary>
        public const string EndPointBehavior = "@EndPointBehavior";
        /// <summary></summary>
        public const string ServiceBinding = "@ServiceBinding";
        /// <summary>Хранить пароль</summary>
        public const string StorePassword = "@StorePassword";
        /// <summary>Идентификатор файла</summary>
        public const string FileId = "@FileId";
        /// <summary>Тип авторизации</summary>
        public const string AuthKind = "@AuthKind";
        /// <summary>Адрес</summary>
        public const string Url = "@Url";
        /// <summary>Адрес службы</summary>
        public const string UrlAddress = "@UrlAddress";
        /// <summary>Идентификатор группы пользователей</summary>
        public const string UserGroupId = "@UserGroupId";
        /// <summary>Каталог данных</summary>
        public const string Directory = "@Directory";
        /// <summary>Идентификатор типа документа</summary>
        public const string DocTypeId = "@DocTypeId";
        /// <summary>Идентификатор статуса</summary>
        public const string StatusId = "@StatusId";
        /// <summary>Время окончания</summary>
        public const string EndOnTime = "@EndOnTime";
        /// <summary>Дата окончания</summary>
        public const string EndOn = "@EndOn";
        /// <summary>Время начала</summary>
        public const string StartOnTime = "@StartOnTime";
        /// <summary>Дата начала</summary>
        public const string StartOn = "@StartOn";
        /// <summary>Значения</summary>
        public const string Values = "@Values";
        /// <summary>Действие</summary>
        public const string Action = "@Action";
        /// <summary>Экспорт иерархий с содержимым</summary>
        public const string WithContent = "@WithContent";
        /// <summary>Экспорт только основных данных</summary>
        public const string OnlyMainData = "@OnlyMainData";
        /// <summary>Табличный параметр</summary>
        public const string TVP = "@TVP";
        /// <summary>Имена</summary>
        public const string Names = "@Names";
        /// <summary>Схема</summary>
        public const string source_schema = "@source_schema";
        /// <summary>Наименование таблицы</summary>
        public const string source_name = "@source_name";
        /// <summary>Роль</summary>
        public const string role_name = "@role_name";
        /// <summary>Инстанс</summary>
        public const string capture_instance = "@capture_instance";
        /// <summary>Файловая группа</summary>
        public const string filegroup_name = "@filegroup_name";
        /// <summary>Возвращаемое значение</summary>
        public const string RC = "@RC";
        ///// <summary>Начало периода</summary>
        //public const string ds = "@ds";
        ///// <summary>Конец периода</summary>
        //public const string de = "@de";
        /// <summary>Возвращаемое значение</summary>
        public const string ReturnValue = "@ReturnValue";
        /// <summary>Идентификатор документа</summary>
        public const string DocumentId = "@DocumentId";
        /// <summary>Возвращать полное наименование</summary>
        public const string WithSchemaName = "@WithSchemaName";
        /// <summary>Тип поиска уточняющий или дополняющий</summary>
        public const string SearchKind = "@SearchKind";
        /// <summary>Сумма</summary>
        public const string Summ = "@Summ";
        /// <summary>Дата</summary>
        public const string DateValue = "@DateValue";
        /// <summary>Идентификатор типа документа</summary>
        public const string DocKind = "@DocKind";
        /// <summary>Является вычисляемым столбцом</summary>
        public const string IsFormula = "@IsFormula";
        /// <summary>Тип Sql</summary>
        public const string TypeNameSql = "@TypeNameSql";
        /// <summary>Тип .Net</summary>
        public const string TypeNameNet = "@TypeNameNet";
        /// <summary>Длина типа</summary>
        public const string TypeLen = "@TypeLen";
        /// <summary>Разрешить Null значения</summary>
        public const string AllowNull = "@AllowNull";
        /// <summary>Описание</summary>
        public const string Description = "@Description";
        /// <summary>Процедура импорта данных</summary>
        public const string ProcedureImport = "@ProcedureImport";
        /// <summary>Процедура экспорта данных</summary>
        public const string ProcedureExport = "@ProcedureExport";
        /// <summary>XML</summary>
        public const string Xml = "@Xml";
        /// <summary>МФО</summary>
        public const string Mfo = "@Mfo";
        /// <summary>Идентификатор приоритета</summary>
        public const string UserOwnerId = "@UserOwnerId";
        /// <summary>Идентификатор приоритета</summary>
        public const string PriorityId = "@PriorityId";
        /// <summary>Разрешить ведение версионности</summary>
        public const string AllowVersion = "@AllowVersion";
        /// <summary>Отображаемое имя</summary>
        public const string DisplayName = "@DisplayName";
        /// <summary>Идентификатор наименования свойства</summary>
        public const string PropertyId = "@PropertyId";
        /// <summary>Идентификатор группы свойств</summary>
        public const string GroupId = "@GroupId";
        /// <summary>Адрес юридический</summary>
        public const string AddressLegal = "@AddressLegal";
        /// <summary>Адрес фактический</summary>
        public const string AddressPhysical = "@AddressPhysical";
        /// <summary>Максимальная сумма задолженности</summary>
        public const string AmmountTrust = "@AmmountTrust";
        /// <summary>Максимальное количество дней отсрочки</summary>
        public const string TimeDelay = "@TimeDelay";
        /// <summary>Идентификатор системного типа ссылки</summary>
        public const string EntityReferenceId = "@EntityReferenceId";
        /// <summary>Идентификатор ссылки</summary>
        public const string ReferenceId = "@ReferenceId";
        /// <summary>Обратное наименование</summary>
        public const string NameRight = "@NameRight";
        /// <summary>Идентификатор заведующего складом</summary>
        public const string StorekeeperId = "@StorekeeperId";
        /// <summary>Социальные привелегии</summary>
        public const string TaxSocialPrivilege = "@TaxSocialPrivilege";
        /// <summary>Пол</summary>
        public const string Sex = "@Sex";
        /// <summary>Роспись</summary>
        public const string SignatureOfficial = "@SignatureOfficial";
        /// <summary>Пол</summary>
        public const string Male = "@Male";
        /// <summary>Кем выдан паспорт</summary>
        public const string Whogives = "@Whogives";
        /// <summary>Фотография</summary>
        public const string Signature = "@Signature";
        /// <summary>Где родился</summary>
        public const string BirthTown = "@BirthTown";
        /// <summary>День рождения</summary>
        public const string Birthday = "@Birthday";
        /// <summary>Отчетство</summary>
        public const string MidleName = "@MidleName";
        /// <summary>Фамилия</summary>
        public const string LastName = "@LastName";
        /// <summary>Имя</summary>
        public const string FirstName = "@FirstName";
        /// <summary>Материально ответственное лицо</summary>
        public const string Mol = "@Mol";
        /// <summary>Табельный номер</summary>
        public const string TabNo = "@TabNo";
        /// <summary>Серия</summary>
        public const string SeriesNo = "@SeriesNo";
        /// <summary>Идентификатор организации выдавшей документ</summary>
        public const string AuthorityId = "@AuthorityId";
        /// <summary>Аналитика категория торговой точки</summary>
        public const string Category = "@Category";
        /// <summary>Дата открытия категории</summary>
        public const string CategoryDate = "@CategoryDate";
        /// <summary>Дата окончания категории</summary>
        public const string CategoryExpire = "@CategoryExpire";
        /// <summary>Ограничения</summary>
        public const string Restriction = "@Restriction";
        /// <summary>Идентификатор начальника по персоналу</summary>
        public const string PersonnelId = "@PersonnelId";
        /// <summary>Идентификатор кассира</summary>
        public const string CashierId = "@CashierId";
        /// <summary>Идентификатор главного бухгалтера</summary>
        public const string BuhId = "@BuhId";
        /// <summary>Идентификатор директора</summary>
        public const string DirectorId = "@DirectorId";
        /// <summary>Код ЗКГНГ</summary>
        public const string RegZkgng = "@RegZkgng";
        /// <summary>Код по КВЕД</summary>
        public const string RegKved = "@RegKved";
        /// <summary>Код КФВ</summary>
        public const string RegKfv = "@RegKfv";
        /// <summary>Код КОАТУУ</summary>
        public const string RegKoatu = "@RegKoatu";
        /// <summary>Код ОПФГ</summary>
        public const string RegOpfg = "@RegOpfg";
        /// <summary>Код ПФУ</summary>
        public const string RegPfu = "@RegPfu";
        /// <summary>Номер регистрации в фонде социального страхования от несчатного случая</summary>
        public const string RegSocialInsuranceNesch = "@RegSocialInsuranceNesch";
        /// <summary>Номер регистрации в фонде социального страхования от временной потери трудоспособности</summary>
        public const string RegSocialInsuranceDisability = "@RegSocialInsuranceDisability";
        /// <summary>Номер регистрации в службе занятости</summary>
        public const string RegEmploymentService = "@RegEmploymentService";
        /// <summary>Номер регистрации в пенсионном фонде</summary>
        public const string RegPensionFund = "@RegPensionFund";
        /// <summary>Идентификатор корреспондента "@Налоговая испекция"</summary>
        public const string TaxInspectionId = "@TaxInspectionId";
        /// <summary>Форма собственности</summary>
        public const string OwnershipId = "@OwnershipId";
        /// <summary>Идентификатор корреспондента "@Торговый представитель</summary>
        public const string SalesRepresentativeId = "@SalesRepresentativeId";
        /// <summary>Идентификатор аналитики категория торговой точки</summary>
        public const string CategoryId = "@CategoryId";
        /// <summary>Идентификатор аналитики метраж</summary>
        public const string MetricAreaId = "@MetricAreaId";
        /// <summary>Идентификатор аналитики тип торговой точки</summary>
        public const string TypeOutletId = "@TypeOutletId";
        /// <summary>Номер свидетельства о регистрации</summary>
        public const string RegNumber = "@RegNumber";
        /// <summary>Идентификатор аналитики отрасль деятельности</summary>
        public const string IndustryId = "@IndustryId";
        /// <summary>Аналитика вид экономической деятельности</summary>
        public const string ActivityEconomic = "@ActivityEconomic";
        /// <summary>Идентификатор корреспондента, зарегистрироваашего компанию</summary>
        public const string RegisteredById = "@RegisteredById";
        /// <summary>Дата регистрации предприятия</summary>
        public const string RegDate = "@RegDate";
        /// <summary>Плательщик НДС</summary>
        public const string NdsPayer = "@NdsPayer";
        /// <summary>Ставка налогов</summary>
        public const string Tax = "@Tax";
        /// <summary>Международное наименование</summary>
        public const string InternationalName = "@InternationalName";
        /// <summary>Международный код</summary>
        public const string CodeInternational = "@CodeInternational";
        /// <summary>Дата определяющая срок хранения "Годен до"</summary>
        public const string DateOut = "@DateOut";
        /// <summary>Дата изготовления</summary>
        public const string DateOn = "@DateOn";
        /// <summary>Имя типа</summary>
        public const string ActivityName = "@ActivityName";
        /// <summary>Идентификатор вид продукции</summary>
        public const string ProductTypeId = "@ProductTypeId";
        /// <summary>Идентификатор упаковка</summary>
        public const string PakcTypeId = "@PakcTypeId";
        /// <summary>Тип в родительской библиотеке</summary>
        public const string LibraryTypeId = "@LibraryTypeId";
        /// <summary>Дополнительная строка параметров для отчетов ReportingService</summary>
        public const string Params = "@Params";
        /// <summary>Идентификатор списка представлений</summary>
        public const string ListId = "@ListId";
        /// <summary>Идентификатор подтипа системного объекта</summary>
        public const string EntityKindId = "@EntityKindId";
        /// <summary>Наименование процедуры обновления данных в строке представления</summary>
        public const string SystemNameRefresh = "@SystemNameRefresh";
        /// <summary>Идентификатор четвертого фильтра корреспондента</summary>
        public const string AgentFourthFilterId = "@AgentFourthFilterId";
        /// <summary>Идентификатор третьего фильтра корреспондента</summary>
        public const string AgentThirdFilterId = "@AgentThirdFilterId";
        /// <summary>Идентификатор второго фильтра корреспондента</summary>
        public const string AgentSecondFilterId = "@AgentSecondFilterId";
        /// <summary>Идентификатор первого фильтра корреспондента</summary>
        public const string AgentFirstFilterId = "@AgentFirstFilterId";
        /// <summary>Использовать нестандартное поведение для выбора корреспондентов</summary>
        public const string UseCustomFilter = "@UseCustomFilter";
        /// <summary>Тип корреспонденции</summary>
        public const string CorrespondenceId = "@CorrespondenceId";
        /// <summary>Идентификатор резолюции</summary>
        public const string ResolutionId = "@ResolutionId";
        /// <summary>Идентификатор пользователя отправителя</summary>
        public const string UserIdFrom = "@UserIdFrom";
        /// <summary>Идентификатор пользователя получателя</summary>
        public const string UserIdTo = "@UserIdTo";
        /// <summary>Идентификатор процесса</summary>
        public const string WfId = "@WfId";
        /// <summary>Текущее состояние</summary>
        public const string CurrentState = "@CurrentState";
        /// <summary>Автор</summary>
        public const string Author = "@Author";
        /// <summary>Дата начала</summary>
        public const string DateStart = "@DateStart";
        /// <summary>Дата окончания</summary>
        public const string DateEnd = "@DateEnd";
        /// <summary>Идентификатор кода</summary>
        public const string CodeNameId = "@CodeNameId";
        /// <summary>Наименование приложения</summary>
        public const string App = "@App";
        /// <summary>Идентификатор описателя</summary>
        public const string LayoutId = "@LayoutId";
        /// <summary>Использовать описатель</summary>
        public const string UseLayout = "@UseLayout";
        /// <summary>Тип разрешений</summary>
        public const string RightKindId = "@RightKindId";
        /// <summary>Формула</summary>
        public const string Formula = "@Formula";
        /// <summary>Идентификатор вида цены</summary>
        public const string PrcNameId = "@PrcNameId";
        /// <summary>Отображать автофильтр</summary>
        public const string AutoFilterVisible = "@AutoFilterVisible";
        /// <summary>Состояние связи</summary>
        public const string ChainStateId = "@ChainStateId";
        /// <summary>Год</summary>
        public const string Year = "@Year";
        /// <summary>Месяц</summary>
        public const string Month = "@Month";
        /// <summary>Дополнительные корреспонденты документа</summary>
        public const string Contractors = "@Contractors";
        /// <summary>Подписи документа</summary>
        public const string DocumentSings = "@DocumentSings";
        /// <summary>Детализация документа</summary>
        public const string Detail = "@Detail";
        /// <summary>Аналитика документа</summary>
        public const string AnaliticDetail = "@AnaliticDetail";
        /// <summary>Дополнительный заголовок документа</summary>
        public const string HeaderType = "@HeaderType";
        /// <summary>Заголовок документа</summary>
        public const string Header = "@Header";
        /// <summary>Идентификатор исходного кода библиотеки</summary>
        public const string AssemblySourceId = "@AssemblySourceId";
        /// <summary>Идентификатор библиотеки</summary>
        public const string AssemblyId = "@AssemblyId";
        /// <summary>Первая ссылка</summary>
        public const string ValueRef1 = "@ValueRef1";
        /// <summary>Вторая ссылка</summary>
        public const string ValueRef2 = "@ValueRef2";
        /// <summary>Третья ссылка</summary>
        public const string ValueRef3 = "@ValueRef3";
        /// <summary>Возможность радактирвания</summary>
        public const string EditAble = "@EditAble";
        /// <summary>Xml данные</summary>
        public const string XmlData = "@XmlData";
        /// <summary>Индекс группировки</summary>
        public const string GroupPanelVisible = "@GroupPanelVisible";
        /// <summary>Индекс группировки</summary>
        public const string GroupIndex = "@GroupIndex";
        /// <summary>Код поиска</summary>
        public const string CodeFind = "@CodeFind";
        /// <summary>Налоговый код (инн)</summary>
        public const string CodeTax = "@CodeTax";
        /// <summary>Идентификатор языка</summary>
        public const string CultureId = "@CultureId";
        /// <summary></summary>
        public const string ToId = "@ToId";
        /// <summary></summary>
        public const string OnlyId = "@OnlyId";
        /// <summary></summary>
        public const string KindType = "@KindType";
        /// <summary>Дата оконцания действия</summary>
        public const string ExpireDate = "@ExpireDate";
        /// <summary>Коррекпондент "Кому"</summary>
        public const string AgentTo = "@AgentTo";
        /// <summary>Дата актуальности</summary>
        public const string DateActual = "@DateActual";
        /// <summary></summary>
        public const string ValueXml = "@ValueXml";
        /// <summary></summary>
        public const string ValueBit = "@ValueBit";
        /// <summary></summary>
        public const string DbEntitySubKindId = "@DbEntitySubKindId";
        /// <summary></summary>
        public const string EntrySubtype = "@EntrySubtype";
        /// <summary></summary>
        public const string RootId3 = "@RootId3";
        /// <summary></summary>
        public const string RootId2 = "@RootId2";
        /// <summary></summary>
        public const string RootId = "@RootId";
        /// <summary></summary>
        public const string ReferenceType3 = "@ReferenceType3";
        /// <summary></summary>
        public const string ReferenceType2 = "@ReferenceType2";
        /// <summary></summary>
        public const string ReferenceType = "@ReferenceType";
        /// <summary></summary>
        public const string TrustDocId = "@TrustDocId";
        /// <summary></summary>
        public const string SalesDocId = "@SalesDocId";
        /// <summary></summary>
        public const string Okpo = "@Okpo";
        /// <summary></summary>
        public const string Phone = "@Phone";
        /// <summary></summary>
        public const string Www = "@Www";
        /// <summary></summary>
        public const string Email = "@Email";
        /// <summary></summary>
        public const string KeyValue = "@KeyValue";
        /// <summary></summary>
        public const string KeyData = "@KeyData";
        /// <summary></summary>
        public const string CurrencyToId = "@CurrencyToId";
        /// <summary></summary>
        public const string CurrencyFromId = "@CurrencyFromId";
        /// <summary></summary>
        public const string MethodId = "@MethodId";
        /// <summary></summary>
        public const string TypeUrl = "@Url";
        /// <summary></summary>
        public const string Divider = "@Divider";
        /// <summary></summary>
        public const string RecipeId = "@RecipeId";
        /// <summary></summary>
        public const string Percent = "@Percent";
        /// <summary></summary>
        public const string Count = "@Count";
        /// <summary></summary>
        public const string AuthenticateKind = "@AuthenticateKind";
        /// <summary></summary>
        public const string Password = "@Password";
        /// <summary></summary>
        public const string ReportId = "@ReportId";
        /// <summary></summary>
        public const string BrandId = "@BrandId";
        /// <summary></summary>
        public const string TradeMarkId = "@TradeMarkId";
        /// <summary></summary>
        public const string ManufacturerId = "@ManufacturerId";
        /// <summary></summary>
        public const string StoragePeriod = "@StoragePeriod";
        /// <summary></summary>
        public const string Size = "@Size";
        /// <summary></summary>
        public const string Depth = "@Depth";
        /// <summary></summary>
        public const string Width = "@Width";
        /// <summary></summary>
        public const string Height = "@Height";
        /// <summary></summary>
        public const string Weight = "@Weight";
        /// <summary></summary>
        public const string Barcode = "@Barcode";
        /// <summary></summary>
        public const string Cataloque = "@Cataloque";
        /// <summary></summary>
        public const string Articul = "@Articul";
        /// <summary></summary>
        public const string Nomenclature = "@Nomenclature";
        /// <summary></summary>
        public const string PriceListId = "@PriceListId";
        /// <summary></summary>
        public const string PriceNameId = "@PriceNameId";
        /// <summary></summary>
        public const string FullTypeName = "@FullTypeName";
        /// <summary></summary>
        public const string IsGeneric = "@IsGeneric";
        /// <summary></summary>
        public const string KindCode = "@KindCode";
        /// <summary></summary>
        public const string LibraryId = "@LibraryId";
        /// <summary></summary>
        public const string TypeName = "@TypeName";
        /// <summary></summary>
        public const string AssemblyVersion = "@AssemblyVersion";
        /// <summary></summary>
        public const string NameFull = "@NameFull";
        /// <summary></summary>
        public const string FileName = "@FileName";
        /// <summary>Расширение файла</summary>
        public const string FileExtention = "@FileExtention";
        /// <summary></summary>
        public const string AssemblySource = "@AssemblySource";
        /// <summary></summary>
        public const string AssemblyDll = "@AssemblyDll";
        /// <summary>Файловые данные</summary>
        public const string StreamData = "@StreamData";
        /// <summary></summary>
        public const string ValueDate2 = "@ValueDate2";
        /// <summary></summary>
        public const string ValueDate1 = "@ValueDate1";
        /// <summary></summary>
        public const string Inn = "@Inn";
        /// <summary></summary>
        public const string TurnKind = "@TurnKind";
        /// <summary></summary>
        public const string Turn = "@Turn";
        /// <summary></summary>
        public const string PrcContentId = "@PrcContentId";
        /// <summary></summary>
        public const string DeliveryId = "@DeliveryId";
        /// <summary></summary>
        public const string ManagerId = "@ManagerId";
        /// <summary></summary>
        public const string SupervisorId = "@SupervisorId";
        /// <summary></summary>
        public const string RecipientId = "@RecipientId";
        /// <summary></summary>
        public const string SenderId = "@RegistratorId";
        /// <summary></summary>
        public const string SeriesId = "@SeriesId";
        /// <summary></summary>
        public const string Qty = "@Qty";
        /// <summary></summary>
        public const string Price = "@Price";
        /// <summary></summary>
        public const string UnitId = "@UnitId";
        /// <summary></summary>
        public const string ProductId = "@ProductId";
        /// <summary></summary>
        public const string AccCrId = "@AccCrId";
        /// <summary></summary>
        public const string AccDbId = "@AccDbId";
        /// <summary></summary>
        public const string GroupNo = "@GroupNo";
        /// <summary></summary>
        public const string Number = "@Number";
        /// <summary></summary>
        public const string SummaTransaction = "@SummaTransaction";
        /// <summary></summary>
        public const string SummaBase = "@SummaBase";
        /// <summary></summary>
        public const string Summa = "@Summa";
        /// <summary></summary>
        public const string FolderId = "@FolderId";
        /// <summary></summary>
        public const string CurrencyTransactionId = "@CurrencyTransactionId";
        /// <summary></summary>
        public const string CurrencyBaseId = "@CurrencyBaseId";
        /// <summary></summary>
        public const string AgentToId = "@AgentToId";
        /// <summary></summary>
        public const string AgentFromId = "@AgentFromId";
        /// <summary></summary>
        public const string IntCode = "@IntCode";
        /// <summary></summary>
        public const string FormId = "@FormId";
        /// <summary></summary>
        public const string OperationId = "@OperationId";
        /// <summary></summary>
        public const string ContentFlags = "@ContentFlags";
        /// <summary></summary>
        public const string ViewDocId = "@ViewDocId";
        /// <summary></summary>
        public const string ViewId = "@ViewId";
        /// <summary></summary>
        public const string ParentId = "@ParentId";
        /// <summary></summary>
        public const string ValueBinary = "@ValueBinary";
        /// <summary></summary>
        public const string ValueGuid = "@ValueGuid";
        /// <summary></summary>
        public const string Multiplier = "@Multiplier";
        /// <summary></summary>
        public const string BankId = "@BankId";
        /// <summary></summary>
// ReSharper disable InconsistentNaming
        public const string DE = "@de";
// ReSharper restore InconsistentNaming
        /// <summary></summary>
// ReSharper disable InconsistentNaming
        public const string DS = "@ds";
// ReSharper restore InconsistentNaming
        /// <summary></summary>
        public const string DocId = "@DocId";
        /// <summary></summary>
        public const string AgentId = "@AgentId";
        /// <summary></summary>
        public const string Membername = "@membername";
        /// <summary></summary>
        public const string Rolename = "@rolename";
        /// <summary></summary>
        public const string ElementKind = "@ElementKind";
        /// <summary></summary>
        public const string FactDateId = "@FactDateId";
        /// <summary></summary>
        public const string ActualDate = "@ActualDate";
        /// <summary></summary>
        public const string EntryType = "@EntryType";
        /// <summary></summary>
        public const string EntryId = "@EntryId";
        /// <summary></summary>
        public const string ColumnId = "@ColumnId";
        /// <summary></summary>
        public const string DateId = "@DateId";
        /// <summary></summary>
        public const string FactNameId = "@FactNameId";
        /// <summary></summary>
        public const string IsCollectionBased = "@IsCollectionBased";
        /// <summary></summary>
        public const string CustomViewId = "@CustomViewId";
        /// <summary></summary>
        public const string Alignment = "@Alignment";
        /// <summary></summary>
        public const string DisplayHeader = "@DisplayHeader";
        /// <summary></summary>
        public const string AutoSizeMode = "@AutoSizeMode";
        /// <summary></summary>
        public const string Frozen = "@Frozen";
        /// <summary></summary>
        public const string Date = "@Date";
        /// <summary></summary>
        public const string CurrencyId = "@CurrencyId";
        /// <summary></summary>
        public const string Value = "@Value";
        /// <summary></summary>
        public const string ValueOld = "@ValueOld";
        /// <summary></summary>
        public const string Discount = "@Discount";
        /// <summary></summary>
        public const string RoleName = "@RoleName";
        /// <summary>Имя пользователя</summary>
        public const string UserName = "@UserName";
        /// <summary></summary>
        public const string ValueDate = "@ValueDate";
        /// <summary></summary>
        public const string ValueMoney = "@ValueMoney";
        /// <summary></summary>
        public const string ValueString = "@ValueString";
        /// <summary></summary>
        public const string ValueInt = "@ValueInt";
        /// <summary></summary>
        public const string ValueFloat = "@ValueFloat";
        /// <summary>Идентификатор пользователя</summary>
        public const string UserId = "@UserId";
        /// <summary>Идентификатор владельца</summary>
        public const string OwnId = "@OwnId";
        /// <summary>Идентификатор сожержимого</summary>
        public const string ContentId = "@ContentId";
        /// <summary>Идентификатор элемента</summary>
        public const string ElementId = "@ElementId";
        /// <summary>Идентификатор иерархии</summary>
        public const string HierarchyId = "@HierarchyId";
        /// <summary>Включено/выключено</summary>
        public const string Enabled = "@Enabled";
        /// <summary>Идентификатор слева</summary>
        public const string LeftId = "@LeftId";
        /// <summary>Новый идентификатор</summary>
        public const string NewId = "@NewId";
        /// <summary>Видимость</summary>
        public const string Format = "@Format";
        /// <summary>Видимость</summary>
        public const string Visible = "@Visible";
        /// <summary>Ширина</summary>
        public const string With = "@With";
        /// <summary>Тип данных</summary>
        public const string DataType = "@DataType";
        /// <summary>Имя свойства</summary>
        public const string DataProperty = "@DataProperty";
        /// <summary>Идентификатор списка</summary>
        public const string CustomViewListId = "@CustomViewListId";
        /// <summary>Идентификатор системного вида</summary>
        public const string KindId = "@KindId";
        /// <summary>Вид</summary>
        public const string Kind = "@Kind";
        /// <summary>Максимальный вид</summary>
        public const string MaxKind = "@MaxKind";
        /// <summary>Подтип</summary>
        public const string EntityKind = "@EntityKind";
        /// <summary>Системное наименование</summary>
        public const string SystemName = "@SystemName";
        /// <summary>Идентификатор правой ссылки</summary>
        public const string RightId = "@RightId";
        /// <summary>Код базы данных</summary>
        public const string DatabaseCode = "@DbCode";
        /// <summary>Имя базы данных</summary>
        public const string DatabaseName = "@DbName";
        /// <summary>Имя сервера</summary>
        public const string ServerName = "@ServerName";
        /// <summary>IP-адрес сервера</summary>
        public const string Ip = "@Ip";
        /// <summary>Пользователь базы данных</summary>
        public const string Uid = "@Uid";
        /// <summary>Домен</summary>
        public const string Domain = "@Domain";
        /// <summary>Тип аутентификация</summary>
        public const string Authentication = "@Authentication";
        /// <summary>Признак сортировки</summary>
        public const string OrderNo = "@OrderNo";
        /// <summary>Опции</summary>
        public const string Options = "@Options";
        
        /// <summary>Процедура</summary>
        public const string ProcedureName = "@Procedure";
        /// <summary>Схема</summary>
        public const string Schema = "@Schema";
        /// <summary>Метод</summary>
        public const string Method = "@Method";
        /// <summary>Идентификатор системного типа</summary>
        public const string EntityId = "@EntityId";
        /// <summary>Идентификатор системного типа</summary>
        public const string FromEntityId = "@FromEntityId";
        /// <summary>Идентификатор системного типа</summary>
        public const string ToEntityId = "@ToEntityId";
        /// <summary>Код выполнения, результат</summary>
        public const string Return = "@RETURN";
        /// <summary>Идентификатор</summary>
        public const string Id = "@Id";
        /// <summary>Глобальный идентификатор</summary>
        public const string Guid = "@Guid";
        /// <summary>Наименование</summary>
        public const string Name = "@Name";
        /// <summary>Идентификатор состояния</summary>
        public const string StateId = "@StateId";

        /// <summary>Код</summary>
        public const string Code = "@Code";
        /// <summary>Примечание</summary>
        public const string Memo = "@Memo";
        /// <summary>Идентификатор шаблона</summary>
        public const string TemplateId = "@TemplateId";
        /// <summary>Версия</summary>
        public const string Version = "@Version";
        /// <summary>Флаг</summary>
        public const string Flags = "@Flags";
        /// <summary>Идентификатор владельца</summary>
        public const string DatabaseId = "@DatabaseId";
        /// <summary>Идентификатор в базе источнике</summary>
        public const string DbSourceId = "@DbSourceId";
        /// <summary>Дата модификации</summary>
        public const string DateModified = "@DateModified";
        /// <summary> Географическое положение </summary>
        public const string Location = "@Location";
        /// <summary> Координата X (географическое положение) </summary>
        public const string X = "@X";
        /// <summary> Координата Y (географическое положение) </summary>
        public const string Y = "@Y";
        /// <summary> Координата Z (географическое положение) </summary>
        public const string Z = "@Z";
        /// <summary> ISO Country </summary>
        public const string Iso = "@Iso";
        /// <summary> ISO3 Country </summary>
        public const string Iso3 = "@Iso3";
        /// <summary> ISONUM Country </summary>
        public const string IsoNum = "@IsoNum";
        /// <summary> Fips Country </summary>
        public const string Fips = "@Fips";
        /// <summary> Stanag Country </summary>
        public const string Stanag = "@Stanag";
        /// <summary> Iana Country </summary>
        public const string Iana = "@Iana";
        /// <summary> Идентификатор налога </summary>
        public const string TaxId = "@TaxId";
        /// <summary>Значение флага</summary>
        public const string FlagValue = "@FlagValue";
        public const string FlagString = "@FlagString";
        /// <summary>Идентификатор построителя виртуальной иерархии</summary>
        public const string VirtualBuildId = "@VirtualBuildId";

        /// <summary>Дополнительное пятое строковое значение</summary>
        public const string StringValue5 = "@StringValue5";
        /// <summary>Дополнительное четвертое строковое значение</summary>
        public const string StringValue4 = "@StringValue4";
        /// <summary>Дополнительное третье строковое значение</summary>
        public const string StringValue3 = "@StringValue3";
        /// <summary>Дополнительное второе строковое значение</summary>
        public const string StringValue2 = "@StringValue2";
        /// <summary>Дополнительное строковое значение</summary>
        public const string StringValue1 = "@StringValue1";

		/// <summary>Номер чертежа</summary>
		public const string DrawingNumber = "@DrawingNumber";
		/// <summary>Номер сборочного чертежа</summary>
		public const string DrawingAssemblyNumber = "@DrawingAssemblyNumber";
		/// <summary>Дата изготовления элемента образа оборудования</summary>
		public const string DateBuild = "@DateBuild";

		/// <summary>Государственный номер автомобиля</summary>
		public const string GosNumber = "@GosNumber";
		/// <summary>Id Используемого топлива</summary>
		public const string UsedFuelId = "@UsedFuelId";
		/// <summary>Номер тех. паспорта автомобиля</summary>
		public const string AutoPassportNumber = "@AutoPassportNumber";
		/// <summary>Номер шасси автомобиля</summary>
		public const string AutoChassis = "@AutoChassis";
		/// <summary>Номер двигателя автомобиля</summary>
		public const string AutoMotor = "@AutoMotor";
		/// <summary>Норма расхода топлива</summary>
		public const string RateFuelConsumption = "@RateFuelConsumption";
		/// <summary>Полная масса</summary>
		public const string GrossVehicleWeight = "@GrossVehicleWeight";
		/// <summary>Собственная масса</summary>
		public const string UnladenWeight = "@UnladenWeight";
		/// <summary>Объем двигнателя</summary>
		public const string VolumeVehicleEngine = "@VolumeVehicleEngine";
		/// <summary>Номер лицензии</summary>
		public const string LicenseNumber = "@LicenseNumber";
		/// <summary>Дата начала лицензии</summary>
		public const string LicenseDateStart = "@LicenseDateStart";
		/// <summary>Дата окончания лицензии</summary>
		public const string LicenseDateExpiration = "@LicenseDateExpiration";
		/// <summary>Дата начала страховки</summary>
		public const string InsuranceDateStart = "@InsuranceDateStart";
		/// <summary>Дата окончания страховки</summary>
		public const string InsuranceDateExpiration = "@InsuranceDateExpiration";
		/// <summary>Номер санитарного пасспорта</summary>
		public const string SanitaryNumber = "@SanitaryNumber";
		/// <summary>Дата начала санитарного пасспорта</summary>
		public const string SanitaryDateStart = "@SanitaryDateStart";
		/// <summary>Дата окончания санитарного пасспорта</summary>
		public const string SanitaryDateExpiration = "@SanitaryDateExpiration";
		/// <summary>Id Вида ТС по тоннажу</summary>
		public const string TonnageKindId = "@TonnageKindId";
		/// <summary>Id Типа транспортного средства</summary>
		public const string AutoTypeId = "@AutoTypeId";
		/// <summary>Id Закрепленного за прицепом автомобиля</summary>
		public const string TrailerUsedAutoId = "@TrailerUsedAutoId";
		/// <summary>Отсекаемый объем слева</summary>
		public const string InterceptedVolumesLeft = "@InterceptedVolumesLeft";
		/// <summary>Отсекаемый объем справа</summary>
		public const string InterceptedVolumesRight = "@InterceptedVolumesRight";
		/// <summary>Паллетоместа</summary>
		public const string Pallet = "@Pallet";
		/// <summary>Дополнительный бак</summary>
		public const string AdditionalFuelTank = "@AdditionalFuelTank";
		/// <summary>Id модели авто</summary>
		public const string EquipmentId = "@EquipmentId";
	}
}
