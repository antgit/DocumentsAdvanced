using System.Collections.Generic;

namespace BusinessObjects
{
    /// <summary>
    /// ��������� ����������� �������
    /// </summary>
    /// <typeparam name="T">���</typeparam>
    /// <typeparam name="T2">���</typeparam>
    public interface IChainAdvanced<T, T2> : ICoreObject
    {
        /// <summary>
        /// ��������
        /// </summary>
        T Left { get; }
        /// <summary>
        /// ����������
        /// </summary>
        T2 Right { get; }
        /// <summary>
        /// ������������� ���������
        /// </summary>
        int LeftId { get; }
        ///// <summary>
        ///// ������������� ������ � ���� ���������
        ///// </summary>
        //int DbSourceId { get;} 
        /// <summary>
        /// ������������� ����������
        /// </summary>
        int RightId { get; }
        /// <summary>
        /// ������������� ����
        /// </summary>
        int KindId { get; }
        /// <summary>
        /// ��� �����
        /// </summary>
        ChainKind Kind { get; }
        /// <summary>
        /// ���������� ����� � ������
        /// </summary>
        int OrderNo { get; }
        /// <summary>
        /// ���
        /// </summary>
        string Code { get; }
        /// <summary>
        /// ����������
        /// </summary>
        string Memo { get; }
    }

    /// <summary>
    /// ��������� ���� ����������� ������� ����������� �������
    /// </summary>
    /// <typeparam name="T">��� ������� �������� (��������)</typeparam>
    /// <typeparam name="T2">��� ������� �������� (������)</typeparam>
    public interface IChainsAdvancedList<T, T2>
    {
        List<IChainAdvanced<T, T2>> GetLinks();
        List<IChainAdvanced<T, T2>> GetLinks(int? kind);
        List<ChainValueView> GetChainView();
    }
}