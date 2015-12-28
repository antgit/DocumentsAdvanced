namespace BusinessObjects.Models
{
    /// <summary>
    /// ������ ������ ���������
    /// </summary>
    public class UnitModel : BaseModel<Unit>
    {
        /// <summary>�����������</summary>
        public UnitModel()
        {
        }
        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <param name="value">������� ���������</param>
        public override void GetData(Unit value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
            CodeInternational = value.CodeInternational;
        }
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId { get; set; }
        /// <summary>������������ �����������, �������� ����������� ������</summary>
        public string MyCompanyName { get; set; }
        /// <summary>�������� �����</summary>
        public string DefaultGroup { get; set; }
        /// <summary>������������� ������������</summary>
        public string CodeInternational { get; set; }    

    }
}