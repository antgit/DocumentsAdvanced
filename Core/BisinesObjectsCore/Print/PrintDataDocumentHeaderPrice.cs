using System;

namespace BusinessObjects.Print
{
    /// <summary>
    /// ����� ������ ��������� ���������
    /// </summary>
    public class PrintDataDocumentHeaderPrice
    {
        public string CompanyFromName { get; set; }
        /// <summary>
        /// ���� ���������
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        /// <summary>���� ������ ��������</summary>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// ����� ���������
        /// </summary>
        public string DocNo { get; set; }

        /// <summary>
        /// ���� ���������
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// ������������ �������������� "���"
        /// </summary>
        public string AgFromName { get; set; }

        /// <summary>
        /// ������������ �������������� "����"
        /// </summary>
        public string AgToName { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public decimal Summa { get; set; }

        /// <summary>
        /// ��������� ���� �������������� "���"
        /// </summary>
        public string AgentFromAcount { get; set; }

        /// <summary>
        /// ��������� ���� �������������� "����"
        /// </summary>
        public string AgentToAcount { get; set; }

        /// <summary>
        /// ������������ ����� �������������� "���"
        /// </summary>
        public string AgentFromBank { get; set; }

        /// <summary>
        /// ������������ ����� �������������� "����"
        /// </summary>
        public string AgentToBank { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// ������� �������������� "���"
        /// </summary>
        public string AgentFromPhone { get; set; }
        /// <summary>
        /// ������� �������������� "����"
        /// </summary>
        public string AgentToPhone { get; set; }
        /// <summary>
        /// ����� ������������� �������������� "���"
        /// </summary>
        public string AgentFromReg { get; set; }
        /// <summary>
        /// ����� ������������� �������������� "����"
        /// </summary>
        public string AgentToReg { get; set; }
        /// <summary>
        /// ��� �������������� "����"
        /// </summary>
        public string AgentToInn { get; set; }
        /// <summary>
        /// ��� �������������� "���"
        /// </summary>
        public string AgentFromInn { get; set; }
        /// <summary>
        /// ����� �������������� "���"
        /// </summary>
        public string AgentFromAddres { get; set; }
        /// <summary>
        /// ����� �������������� "����"
        /// </summary>
        public string AgentToAddres { get; set; }
        /// <summary>
        /// ���� �������������� "���" 
        /// </summary>
        public string AgentFromOkpo { get; set; }
        /// <summary>
        /// ���� �������������� "����" 
        /// </summary>
        public string AgentToOkpo { get; set; }
        /// <summary>
        /// ��� ����� �������������� "���" 
        /// </summary>
        public string AgentFromBankMfo { get; set; }
        /// <summary>
        /// ��� ����� �������������� "����" 
        /// </summary>
        public string AgentToBankMfo { get; set; }

    }
}