using System;

namespace BusinessObjects.Print
{
    /// <summary>
    /// ����� ������ ��������� ��������� ������� "��������"
    /// </summary>
    public class PrintDataDocumentPersonHeader
    {
        /// <summary>
        /// ����� ���������
        /// </summary>
        public string DocNo { get; set; }

        /// <summary>
        /// ���� ���������
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// ������������ �������������� "����"
        /// </summary>
        public string AgToName { get; set; }

        /// <summary>
        /// ���� �������������� "����" 
        /// </summary>
        public string AgentToOkpo { get; set; }

        /// <summary>
        /// ����� �������������� "����"
        /// </summary>
        public string AgentToAddres { get; set; }

        /// <summary>
        /// ��� ����� �������������� "����" 
        /// </summary>
        public string AgentToBankMfo { get; set; }

        /// <summary>
        /// ������������ ����� �������������� "����"
        /// </summary>
        public string AgentToBank { get; set; }

        /// <summary>
        /// ��������� ���� �������������� "����"
        /// </summary>
        public string AgentToAcount { get; set; }

        /// <summary>
        /// ��������� �������������� "����" 
        /// </summary>
        public string AgentToBuh { get; set; }
        /// <summary>
        /// �������� �������������� "����" 
        /// </summary>
        public string AgentToDirector { get; set; }

        /// <summary>
        /// ������ �������������� "���" 
        /// </summary>
        public string AgentToCashier { get; set; }
        /// <summary>
        /// ������� �������������� "����"
        /// </summary>
        public string AgentToPhone { get; set; }

        /// <summary>
        /// ������������ �������������� "����"
        /// </summary>
        public string DepatmentToName { get; set; }

        /// <summary>
        /// ������������ �������������� "����"
        /// </summary>
        public string DepatmentFromName { get; set; }

        /// <summary>
        /// ���� ������
        /// </summary>
        public DateTime? DateStart { get; set; }
        /// <summary>
        /// ���� ���������
        /// </summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public decimal Summa { get; set; }

        /// <summary>
        /// ������������ ����������
        /// </summary>
        public string EmployerName { get; set; }

        /// <summary>
        /// ������������ ����������
        /// </summary>
        public string EmployerFirstName { get; set; }

        /// <summary>
        /// ������� ����������
        /// </summary>
        public string EmployerLastName { get; set; }

        /// <summary>
        /// ��������  ����������
        /// </summary>
        public string EmployerMidleName { get; set; }

        /// <summary>
        /// ���������
        /// </summary>
        public string WorkPost { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public string Memo { get; set; }
    }
}