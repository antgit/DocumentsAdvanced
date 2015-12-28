namespace BusinessObjects
{
    /// <summary>
    /// ��������� ��������� ���������� �������� ���������
    /// </summary>
    public interface ICompanyOwner
    {
        /// <summary>
        /// ��� ��������, ����������� �������� ����������� ������
        /// </summary>
        Agent MyCompany { get; }
        /// <summary>
        /// ������������� �����������, �������� ����������� ������
        /// </summary>
        int MyCompanyId { get; set; }
    }
}