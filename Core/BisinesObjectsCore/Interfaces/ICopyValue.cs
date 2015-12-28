namespace BusinessObjects
{
    /// <summary>
    /// ��������� ��������� ���������� ������� �� �������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICopyValue<T>
    {
        /// <summary>
        /// ���������� ������� �������� ������� �� ������ �������
        /// </summary>
        /// <param name="template">������</param>
        void CopyValue(T template);
    }
}
