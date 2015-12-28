using System;
using System.Collections.Generic;

namespace BusinessObjects.Security
{
    /// <summary>
    /// ������������
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
        /// ������� ����� ������ �������������
        /// </summary>
        Uid CreateRole(string name);
        bool IsDocumentAllowEdit(int companyId);
        List<Uid> GetUserGroupsRightForElement(int elementId, int dbEntityId, int rightKindId = 1769474);
        DateTime GetMinFactDateAllow(int value);
        /// <summary>
        /// ����� ����� � ����������
        /// </summary>
        ICommonRights RightCommon { get; }
        /// <summary>
        /// �������� �������������� ������������ � ������
        /// </summary>
        /// <param name="userName">��� ������������</param>
        /// <param name="roleName">��� ������</param>
        /// <returns><c>true</c> - ���� ������������ ����������� ��������� ������</returns>
        bool IsUserExistsInGroup(string userName, string roleName);
        /// <summary>
        /// ������ ����� ��� ������������
        /// </summary>
        /// <param name="name">��� ������������</param>
        /// <returns></returns>
        List<string> GetUserGroups(string name);
        /// <summary>
        /// ������ ���� ������������ �����
        /// </summary>
        /// <returns></returns>
        List<string> GetDatabaseGroups();
        /// <summary>
        /// ����� ����� � ���������� ��� ������������
        /// </summary>
        /// <param name="user">������������</param>
        /// <returns></returns>
        ICommonRights GetCommonRights(Uid user);
        /// <summary>
        /// ������������� ���������� ��� ���������
        /// </summary>
        /// <param name="kind">������������� ���� <see cref="EntityType.Id"/></param>
        /// <returns></returns>
        ElementRightView ElementRightView(short kind);
        ElementRightView ElementRightView(short kind, string uidName = null);
        EntityRightView DbentityRightView();

        List<UserRightCommon> GetCollectionUserRights(int kindId, int userId);
        List<UserRightCommon> CollectionCurrentUserRight { get; }
        List<UserRightElement> GetCollectionUserRightsElements(int kindId, int userId, int elementId, short dbEntityId);
        ///// <summary>
        ///// ���������� ��� ������������ ��� ������ ������������� ��� ������ ��������
        ///// </summary>
        ///// <param name="user">������������</param>
        ///// <param name="id">������������� ���������� ����</param>
        ///// <returns></returns>
        //List<UserRightEntity> GetCollectionUserRightsDbEntity(Uid user, short id);
    }
    /// <summary>
    /// ����� �����
    /// </summary>
    public interface ICommonRights
    {
        /// <summary>
        /// �������������
        /// </summary>
        bool Admin { get; }
        /// <summary>
        /// ��������� �������������
        /// </summary>
        bool AdminEnterprize { get; }
        /// <summary>
        /// ����������
        /// </summary>
        bool Connect { get; }
        /// <summary>
        /// ������ ������
        /// </summary>
        bool ReadOnly { get; }
        /// <summary>
        /// ������������� ����������
        /// </summary>
        bool AdminUserInterface { get; }
        /// <summary>
        /// ������������� ������������
        /// </summary>
        int UidId { get; }
        /// <summary>
        /// ����������� �� �������� ����������� ����������
        /// </summary>
        bool ViewOnlyMyDocyments { get; }
    }
    /// <summary>
    /// ���������� ��������
    /// </summary>
    internal interface IRightElementView
    {
        /// <summary>
        /// ��������
        /// </summary>
        bool Create { get; }
        /// <summary>
        /// ��������
        /// </summary>
        bool Delete { get; }
        /// <summary>
        /// �������� �������
        /// </summary>
        bool View { get; }
        /// <summary>
        /// �������� � �������
        /// </summary>
        bool Trash { get; }
        /// <summary>
        /// ������������� ������������ 
        /// </summary>
        int UidId { get; }
    }
}
