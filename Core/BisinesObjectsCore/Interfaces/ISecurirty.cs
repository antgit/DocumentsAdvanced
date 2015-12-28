using System;
using System.Collections.Generic;

namespace BusinessObjects.Security
{
    /// <summary>
    /// Безопасность
    /// </summary>
    public  interface ISecurirty
    {
        void RefreshCompanyScopeOpenContext(string uidContext);
        void RefreshCompanyScopeEditContext(string uidContext);
        void RefreshCompanyScopeCreateContext(string uidContext);
        void RefreshCompanyScopeViewContext(string uidContext);
        List<Uid> GetAllGroups(bool refresh = false);
        List<Uid> GetAllUsers(bool refresh = false);
        List<int> HierarchyCompanyScopeViewContext(string uidContext, int hierarchyId);
        List<int> GetCompanyScopeView(string uidContext);
        List<int> GetCompanyScopeEdit(string uidContext);
        List<int> GetCompanyScopeOpen(string uidContext);
        List<int> GetCompanyScopeCreate(string uidContext);
        void ExcludeFromGroup(string userName, string groupName);
        void IncludeInGroup(string userName, string groupName);
        /// <summary>
        /// Создать новую группу пользователей
        /// </summary>
        Uid CreateRole(string name);
        bool IsDocumentAllowEdit(int companyId);
        List<Uid> GetUserGroupsRightForElement(int elementId, int dbEntityId, int rightKindId = 1769474);
        DateTime GetMinFactDateAllow(int value);
        /// <summary>
        /// Общие права и разрешения
        /// </summary>
        ICommonRights RightCommon { get; }
        /// <summary>
        /// Проверка принадлежности пользователя к группе
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="roleName">Имя группы</param>
        /// <returns><c>true</c> - если пользователь принадлежит указанной группе</returns>
        bool IsUserExistsInGroup(string userName, string roleName);
        /// <summary>
        /// Список групп для пользователя
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns></returns>
        List<string> GetUserGroups(string name);
        /// <summary>
        /// Список всех существующих групп
        /// </summary>
        /// <returns></returns>
        List<string> GetDatabaseGroups();
        /// <summary>
        /// Общие права и разрешения для пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        ICommonRights GetCommonRights(Uid user);
        /// <summary>
        /// Представление разрешений для элементов
        /// </summary>
        /// <param name="kind">Идентификатор типа <see cref="EntityType.Id"/></param>
        /// <returns></returns>
        ElementRightView ElementRightView(short kind);
        ElementRightView ElementRightView(short kind, string uidName = null);
        EntityRightView DbentityRightView();

        List<UserRightCommon> GetCollectionUserRights(int kindId, int userId);
        List<UserRightCommon> CollectionCurrentUserRight { get; }
        List<UserRightElement> GetCollectionUserRightsElements(int kindId, int userId, int elementId, short dbEntityId);
        ///// <summary>
        ///// Разрешения для пользователя или группы пользователей для группы объектов
        ///// </summary>
        ///// <param name="user">Пользователь</param>
        ///// <param name="id">Идентификатор системного типа</param>
        ///// <returns></returns>
        //List<UserRightEntity> GetCollectionUserRightsDbEntity(Uid user, short id);
    }
    /// <summary>
    /// Общие права
    /// </summary>
    public interface ICommonRights
    {
        /// <summary>
        /// Администратор
        /// </summary>
        bool Admin { get; }
        /// <summary>
        /// Системный администратор
        /// </summary>
        bool AdminEnterprize { get; }
        /// <summary>
        /// Соединение
        /// </summary>
        bool Connect { get; }
        /// <summary>
        /// Только чтение
        /// </summary>
        bool ReadOnly { get; }
        /// <summary>
        /// Администратор интерфейса
        /// </summary>
        bool AdminUserInterface { get; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        int UidId { get; }
        /// <summary>
        /// Ограничение на просмотр собственных документов
        /// </summary>
        bool ViewOnlyMyDocyments { get; }
    }
    /// <summary>
    /// Разрешения элемента
    /// </summary>
    internal interface IRightElementView
    {
        /// <summary>
        /// Создание
        /// </summary>
        bool Create { get; }
        /// <summary>
        /// Удаление
        /// </summary>
        bool Delete { get; }
        /// <summary>
        /// Просмотр свойств
        /// </summary>
        bool View { get; }
        /// <summary>
        /// Удаление в корзину
        /// </summary>
        bool Trash { get; }
        /// <summary>
        /// Идентификатор пользователя 
        /// </summary>
        int UidId { get; }
    }
}
