using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BusinessObjects.Security;
namespace BusinessObjects
{
    /// <summary>
    /// Рабочая область
    /// </summary>
    public interface IWorkarea
    {
        bool IsWebApplication { get; set; }
        /// <summary>Код сессии</summary>
        Guid SessionId { get; set; }
        List<EntityProperty> GetCollectionEntityProperties(int entityId);
        List<EntityPropertyName> GetEntityPropertyNames(int cultureId);
        List<EntityDocument> CollectionDocumentTypes();
        List<ResourceString> GetCollectionResourceString(int cultureId);
        List<CustomPropertyDescriptor> CustomPropertyDescriptors();
        /// <summary>
        /// Удаление данных по идентификатору
        /// </summary>
        /// <param name="value">Идентификатор</param>
        /// <param name="procedureName">Наименование хранимой процедура</param>
        void DeleteById(int value, string procedureName);
        /// <summary>
        /// Получить текущее соединение с базой данных
        /// </summary>
        /// <returns></returns>
        SqlConnection GetDatabaseConnection();
        /// <summary>
        /// Управляемый кеш объектов
        /// </summary>
        WorkareaCashe Cashe { get; }
        /// <summary>
        /// Пустой объект приявязанный к текущей рабочей области
        /// </summary>
        /// <remarks>Используется для доступа к методам</remarks>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns></returns>
        T Empty<T>() where T : class, ICoreObject, new();
        ///// <summary>
        ///// Удалить
        ///// </summary>
        ///// <typeparam name="T">Удаляемый объект</typeparam>
        ///// <param name="checkVersion">Выполнять проверку версии объекта</param>
        ///// <param name="value"></param>
        //void Delete<T>(T value, bool checkVersion = true) where T : class, IBase, new();

        /// <summary>
        /// Получить объект по идентификатору
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        T GetObject<T>(int id) where T : class, ICoreObject, new();
        ///// <summary>
        ///// Получить объект по идентификатору
        ///// </summary>
        ///// <typeparam name="T">Тип</typeparam>
        ///// <param name="id">Идентификатор</param>
        ///// <returns></returns>
        //T GetObjectById<T>(int id) where T : ICoreObject;
        /// <summary>
        /// Создать новый объект основе шаблона
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <returns></returns>
        T CreateNewObject<T>(T templateValue) where T : class, IBase, new();
        /// <summary>
        /// Информация об ошибке на уровне сервера базы данных
        /// </summary>
        /// <param name="value">Идентификатор ошибки</param>
        /// <returns></returns>
        ErrorLog GetErrorLog(int value);
        /// <summary>
        /// Коллекция видов системных объектов
        /// </summary>
        List<EntityKind> CollectionEntityKinds { get; }
        /// <summary>
        /// Коллекция системных объектов
        /// </summary>
        List<EntityType> CollectionEntities { get; }
        /// <summary>
        /// Коллекция состояний
        /// </summary>
        List<State> CollectionStates { get; }
        /// <summary>
        /// Моя база данных
        /// </summary>
        Branche MyBranche { get; }
        /// <summary>Язык</summary>
        int LanguageId { get; }
        /// <summary>
        /// Рабочий период
        /// </summary>
        Period Period { get; }
        /// <summary>
        /// Коллекция объектов
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <returns></returns>
        List<T> GetCollection<T>(bool refresh = false) where T : class, ICoreObject, new();
        /// <summary>
        /// Коллекция объектов
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="kind">Идентификатор вида</param>
        /// <returns></returns>
        List<T> GetCollection<T>(int kind, bool refresh=false) where T : class, ICoreObject, new();//BaseCore<T>;
        /// <summary>
        /// Коллекция шаблонов
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <returns></returns>
        List<T> GetTemplates<T>(bool refresh = false) where T : class, IBase, new();//BaseCore<T>;
        ///// <summary>
        ///// Коллекция наименований фактов
        ///// </summary>
        ///// <param name="toEntityId">Идентификатор системного типа</param>
        ///// <param name="kindSubType">Идентификатор вида системного объекта</param>
        ///// <returns></returns>
        //List<FactName> GetCollectionFactNames(Int16 toEntityId);
        ///// <summary>
        ///// Коллекция значений дат факта для объекта
        ///// </summary>
        ///// <param name="entityId">Идентификатор объекта</param>
        ///// <param name="kindType">Идентификатор системного объекта</param>
        ///// <param name="columnId">Идентификатор колонки факта</param>
        ///// <param name="ds">Дата начала</param>
        ///// <param name="de">Дата окончания</param>
        ///// <returns></returns>
        //List<FactDate> GetCollectionFactDates(int entityId, int kindType, int columnId, DateTime ds, DateTime de);
        ///// <summary>
        ///// Максимальные значения факта
        ///// </summary>
        ///// <param name="entityId">Идентификатор объекта</param>
        ///// <param name="kindType">Идентификатор системного объекта</param>
        ///// <param name="columnId">Идентификатор колонки</param>
        ///// <returns></returns>
        //List<FactDate> GetCollectionFactDatesMax(int entityId, int kindType, int columnId);
        ///// <summary>
        ///// Коллекция видов связей
        ///// </summary>
        ///// <param name="kind">Идентификатор системного типа</param>
        ///// <returns></returns>
        //List<ChainKind> GetCollectionChainKinds(Int16 kind);
        /// <summary>
        /// Коллекция видов связей
        /// </summary>
        List<ChainKind> CollectionChainKinds { get; }
        /// <summary>
        /// Коллекция общих прав текущего пользователя
        /// </summary>
        //List<UserRightCommon> CollectionCurrentUserRight { get; }
        //List<UserRightCommon> GetCollectionUserRights(int kindId, int userId);

        //List<UserRightElement> GetCollectionUserRightsElements(int kindId, int userId, int ElementId, short dbEntityId);

        //List<Hierarchy> GetHierarchyChild(int parentId);
        //List<Hierarchy> GetCollectionHierarchy(int kind);
        //List<HierarchyContent> GetCollectionHierarchyContent(int id);
        //List<HierarchyContent> GetCollectionHierarchyContent(int id, Int16 kind);
        //System.Data.DataTable GetCollectionCustomViewListId(Hierarchy value);
        System.Data.DataTable GetCollectionCustomViewList(CustomViewList list, int elementId, int kindType, int hierarchyId);


        List<IChain<T>> CollectionChainSources<T>(T source, int? kind) where T : class, IBase, new();
        List<EntityKind> GetCollectionEntityKind(int value);

        //List<ExtentionView> GetExtentionView(string name);
        List<CustomViewList> GetCustomViewList(int value);
        /// <summary>
        /// Текущий пользователь системы
        /// </summary>
        Uid CurrentUser { get; }
        /// <summary>
        /// Безопастность
        /// </summary>
        ISecurirty Access { get; }
        void Swap<T>(Chain<T> chainFrom, Chain<T> chainTo) where T : class, IBase, new();
        void Swap<T, TDestination>(ChainAdvanced<T, TDestination> chainFrom, ChainAdvanced<T, TDestination> chainTo)
            where T : class, IBase, new()
        where TDestination : class, IBase, new();
        /// <summary>
        /// Поиск сопоставления метода
        /// </summary>
        /// <param name="methodName">Имя метода</param>
        /// <returns></returns>
        ProcedureMap FindMethod(string methodName);
    }
}
