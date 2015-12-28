namespace BusinessObjects.Documents
{
    /// <summary>
    /// ������� ����� �������� ����������
    /// </summary>
    public class ConfigDocumentBase
    {
        /// <summary>
        /// �����������
        /// </summary>
        public ConfigDocumentBase()
        {
            
        }
        /// <summary>
        /// ����� �������� �� �������� �� ���������
        /// </summary>
        public virtual void Reset()
        {
            AllowNumberEdit = true;
            AllowAgenToEdit = true;
            AllowAgenToCreate = true;
            AllowAgenToSearch = true;

            AllowProductSearch = true;
            AllowProductEdit = true;
            
            DistpalyFormatQty = "0.00";
            DisplayFormatPrice = "0.00";
            DisplayFormatSumma = "0.00";

            DecimalPlacesQty = 2;
            DecimalPlacesPrice = 2;
            DecimalPlacesSumma = 2;
        }

        #region �����
        /// <summary>
        /// ��������� �������������� ������
        /// </summary>
        public bool AllowNumberEdit { get; set; }

        /// <summary>
        /// ��������� �������������� �������������� ��������������� � ���������
        /// </summary>
        public bool AllowAgenToEdit { get; set; }

        /// <summary>
        /// ��������� �������� �������������� ��������������� � ���������
        /// </summary>
        public bool AllowAgenToCreate { get; set; }

        /// <summary>
        /// ��������� ����� ��������������
        /// </summary>
        public bool AllowAgenToSearch { get; set; }

        #endregion
        #region ��������� �����

        /// <summary>
        /// ��������� ����� ������
        /// </summary>
        public bool AllowProductSearch { get; set; }

        /// <summary>
        /// ��������� �������������� ������ ��������������� � ���������
        /// </summary>
        public bool AllowProductEdit { get; set; }

        /// <summary>
        /// ��������� �������� ������ ��������������� � ���������
        /// </summary>
        public bool AllowProductCreate { get; set; }
        
        /// <summary>
        /// ������ "����������" ��� �����������
        /// </summary>
        public string DistpalyFormatQty { get; set; }

        /// <summary>
        /// ������ "���" ��� �����������
        /// </summary>
        public string DisplayFormatPrice { get; set; }

        /// <summary>
        /// ������ "�����" ��� �����������
        /// </summary>
        public string DisplayFormatSumma { get; set; }

        /// <summary>
        /// ����������  ���������� ������ "�����" ��� ��������������
        /// </summary>
        public int DecimalPlacesSumma { get; set; }

        /// <summary>
        /// ����������  ���������� ������ "����������" ��� ��������������
        /// </summary>
        public int DecimalPlacesQty { get; set; }

        /// <summary>
        /// ����������  ���������� ������ "���" ��� ��������������
        /// </summary>
        public int DecimalPlacesPrice { get; set; } 
        #endregion
        #region ������ ��������

        /// <summary>
        /// ���������� ������������ ������ �� ����������� ��������� 
        /// </summary>
        public bool MaxLeftChainCount { get; set; }

        /// <summary>
        /// ���������� ������������ ������ �� ������������ ��������� 
        /// </summary>
        public bool MaxRightChainCount { get; set; }

        /// <summary>
        /// ���������� ������������ ������ �� ������ 
        /// </summary>
        public bool MaxReportCount { get; set; } 
        #endregion
    }
}