namespace BusinessObjects.Models
{
    /// <summary>
    /// ������ ������
    /// </summary>
    public class ReportModel : BaseModel<Library>
    {
        /// <summary>�����������</summary>
        public ReportModel()
        {
        }
        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <param name="value">���������</param>
        public override void GetData(Library value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
        }
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId { get; set; }
        /// <summary>������������ �����������, �������� ����������� ������</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Url ������������ ������</summary>
        public string HelpUrl { get; set; }
        /// <summary>������ �� ����� � ������� �������� ������������� Flash</summary>
        public string NavigateUrl { get; set; }
        /// <summary>������ �� ����� � Flash </summary>
        public string NavigateUrlFx { get; set; }

    }
}