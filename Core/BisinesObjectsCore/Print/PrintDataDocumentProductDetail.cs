namespace BusinessObjects.Print
{
    /// <summary>
    /// ����� ������ �������� ����������� ���������
    /// </summary>
    public class PrintDataDocumentProductDetail
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
        public decimal Qty { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        public decimal Summa { get; set; }
        /// <summary>
        /// ��� ������
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// ������ � %
        /// </summary>
        public decimal Discount { get; set; }
    }
}