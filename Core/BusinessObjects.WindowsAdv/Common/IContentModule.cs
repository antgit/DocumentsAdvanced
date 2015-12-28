using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    /// <summary>������������ ������</summary>
    public interface IContentModule
    {
        /// <summary>�����������</summary>
        Bitmap Image32 { get; }
        /// <summary>������� �������</summary>
        Workarea Workarea { get; set; }
        /// <summary>����</summary>
        string Key { get; set; }
        /// <summary>��������</summary>
        string Caption { get; set; }
        /// <summary>�������� ������� ��� �����������</summary>
        Control Control { get; }
        /// <summary>��������</summary>
        void PerformShow();
        /// <summary>������</summary>
        void PerformHide();
        /// <summary>����� - �������� �� ������� ���������� ���������� ������</summary>
        Form Owner { get; set; }
        /// <summary>�������� � ����� ����</summary>
        void ShowNewWindows();
        /// <summary>
        /// �������� ���������� ����������
        /// </summary>
        void InvokeHelp();
        /// <summary>
        /// ���������
        /// </summary>
        IContentNavigator ContentNavigator { get; set; }
        /// <summary>
        /// ������������ ���� ������������� ���� ������ � ������� ���������� ������.
        /// </summary>
        string ParentKey { get; set; }

        Library SelfLibrary { get; }
    }
    /// <summary>
    /// ��������� ������������ ������� ��� ������������.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContentModule<T>: IContentModule
    {
        /// <summary>
        /// ������ ��������� ��������
        /// </summary>
        List<T> Selected { get; }
        /// <summary>
        /// �������� ������ � ��������� ����
        /// </summary>
        /// <returns>��������� ���������� ��������</returns>
        /// <param name="showModal">�������� ������ ��������</param>
        List<T> ShowDialog(bool showModal=true);
    }
}