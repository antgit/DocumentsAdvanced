namespace BusinessObjects.Print
{
    /// <summary>
    /// ����� ������ �������� ����������� ���������
    /// </summary>
    public class PrintDataDocumentPrinterDetail
    {
        /// <summary>
        /// ������������ ������
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// ������������ ������� ���������
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public int YearStart { get; set; }

        /// <summary>
        /// ����� ������ �� �������� ����� 
        /// </summary>
        public decimal CountMonth { get; set; }
        /// <summary>
        /// ��������� ��������
        /// </summary>
        public decimal CountTotal { get; set; }
        /// <summary>
        /// ��� ������
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// ��������� ������������
        /// </summary>
        public string EqupmentState { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        public string Configuration { get; set; }
        
    }
}