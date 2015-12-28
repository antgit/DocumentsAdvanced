namespace BusinessObjects.Models
{
    /// <summary>
    /// ������ ���������
    /// </summary>
    public class AnaliticModel : BaseModel<Analitic>
    {
        /// <summary>�����������</summary>
        public AnaliticModel()
        {
        }
        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <param name="value">���������</param>
        public override void GetData(Analitic value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
        }
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId { get; set; }
        /// <summary>������������ �����������, �������� ����������� ������</summary>
        public string MyCompanyName { get; set; }
        /// <summary>�������� �����</summary>
        public string DefaultGroup { get; set; }

    }
}