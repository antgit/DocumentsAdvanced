using System.Collections.Generic;

namespace BusinessObjects
{
    /// <summary>��������� ��������� �������������� ������� �������� ���������</summary>
    /// <remarks>�������������� �������� ������������ �������������� �������� � ���� ������, 
    /// "�����������" ������� ������� �� ��������� � �������� �������. ��������, ��� ������ "������ ���������" ���������� �������� 
    /// ��������� ������� "StringData": 
    /// � �c������ ������� ��������� ������� "StringData" ��� nvarchar(255) 
    /// � �������� ��������� �������� ������ ��������� �������������� �������
    /// � ������� �������������� �������������� �������� "StringData" � ��������������� ����������.
    /// </remarks>
    public interface ICustomPropertySupport 
    {
        /// <summary>���������������� �������������� ��������</summary>
        /// <param name="descriptor">��������� ��������</param>
        void RegisterProperty(CustomPropertyDescriptor descriptor);
        /// <summary>������� ����������� ��������������� ��������</summary>
        /// <param name="descriptor">��������� ��������</param>
        void UnRegisterProperty(CustomPropertyDescriptor descriptor) ;
        /// <summary>������ �������������� �������</summary>
        List<CustomPropertyDescriptor> PropertyDescriptors { get; }
        ///// <summary>
        ///// ������ �������������� �������
        ///// </summary>
        ///// <typeparam name="T">���</typeparam>
        ///// <param name="item">������</param>
        ///// <returns></returns>
        //List<CustomProperty<T>> Values(T item);
    }
}