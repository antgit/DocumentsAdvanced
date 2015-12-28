namespace BusinessObjects.Models
{
    /// <summary>
    /// ������ ������
    /// </summary>
    public class CurrencyModel : BaseModel<Currency>
    {
        /// <summary>�����������</summary>
        public CurrencyModel()
        {
        }
        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <param name="value">������</param>
        public override void GetData(Currency value)
        {
            base.GetData(value);
            IntCode = value.IntCode;
            //MyCompanyId = value.MyCompanyId;
            //MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
        }
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId { get; set; }
        /// <summary>������������ �����������, �������� ����������� ������</summary>
        public string MyCompanyName { get; set; }
        /// <summary>�������� ��� ������</summary>
        public int IntCode { get; set; }
        /// <summary>�������� �����</summary>
        public string DefaultGroup { get; set; }

    }
}