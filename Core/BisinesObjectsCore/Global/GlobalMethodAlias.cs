namespace BusinessObjects
{
    /// <summary>Алиасы стандартных метод вызова хранимых процедур</summary>
    internal static class GlobalMethodAlias
    {
        /// <summary>Хранимая процедура удаления отчетов по элементу</summary>
        public const string ReportChainDelete = "ReportChainDelete";
        /// <summary>Хранимая процедура обновления отчетов по элементу</summary>
        public const string ReportChainUpdate = "ReportChainUpdate";
        /// <summary>Хранимая процедура создания отчетов по элементу</summary>
        public const string ReportChainInsert = "ReportChainInsert";
        /// <summary>Хранимая процедура загрузки отчетов по элементу</summary>
        public const string ReportChainByElement = "ReportChainByElement";
        /// <summary>Хранимая процедура уникальных наименований континентов</summary>
        public const string GetDistinctContinents = "GetDistinctContinents";
        /// <summary>Хранимая процедура создания полной копии</summary>
        public const string GetStreamData = "GetStreamData";
        /// <summary>Хранимая процедура создания полной копии</summary>
        public const string GetFlagStringAll = "GetFlagStringAll";
        /// <summary>Хранимая процедура создания полной копии</summary>
        public const string Copy = "Copy";
        /// <summary>Хранимая процедура проверки возможности удаления записи</summary>
        public const string CanDelete = "CanDelete";
        /// <summary>Хранимая процедура удаления записи</summary>
        public const string Delete = "Delete";
        /// <summary>Хранимая процедура создания записи</summary>
        public const string Create = "Create";
        /// <summary>Хранимая процедура обновления записи</summary>
        public const string Update = "Update";
        /// <summary>Хранимая процедура загрузки записи</summary>
        public const string Load = "Load";
        /// <summary>Хранимая процедура загрузки записи по идентификатору владельца</summary>
        public const string LoadByOwnerId = "LoadByOwnId";
        /// <summary>Хранимая процедура загрузки XML записи по идентификатору владельца</summary>
        public const string LoadXmlByOwnerId = "LoadXmlByOwnerId";
        /// <summary>Хранимая процедура проверки записи по идентификатору</summary>
        public const string ExistId = "ExistId";
        /// <summary>Хранимая процедура проверки записи по глобальному идентификатору</summary>
        public const string FindIdByGuid = "FindIdByGuid";
        /// <summary>Хранимая процедура проверки записей по глобальному идентификатору</summary>
        public const string ExistsGuids = "ExistsGuids";
        /// <summary>Хранимая процедура загрузки записи по глобальному идентификатору</summary>
        public const string LoadGuid = "LoadGuid";
        /// <summary>Хранимая процедура изменения текущего состояния</summary>
        public const string ChangeState = "ChangeState";
        /// <summary>Хранимая процедура загрузки записей по идентификаторам</summary>
        public const string LoadList = "LoadList";
        /// <summary>Хранимая процедура загрузки записей результата поиска</summary>
        public const string FindBy = "FindBy";
        /// <summary>Хранимая процедура загрузки записей по иерархии</summary>
        public const string LoadByHierarchies = "LoadByHierarchies";
        /// <summary>Хранимая процедура загрузки корневых иерархий</summary>
        public const string HierarchyLoadRoot = "HierarchyLoadRoot";
        /// <summary>Хранимая процедура загрузки дочерних иерархий</summary>
        public const string HierarchyLoadChild = "HierarchyLoadChild";
    }
}