namespace BusinessObjects.Models
{
    /// <summary>
    /// ������ ������ ���� ������
    /// </summary>
    public class KnowledgeModel : BaseModel<Knowledge>
    {
        /// <summary>�����������</summary>
        public KnowledgeModel()
        {
            
        }
        /// <summary>���������� ������</summary>
        public override void GetData(Knowledge value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
            FileId = value.FileId != 0 ? (int?) value.FileId : null;
            if(value.FileId!=0)
            {
                FileName = value.File.Name;
                FileNameFull = value.File.NameFull;
                FileExtention = value.File.FileExtention;
            }
            else
            {
                FileName = string.Empty;
                FileNameFull = string.Empty;
                FileExtention =  string.Empty;
            }
        }
        #region ��������
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId { get; set; }
        /// <summary>������������ �����������, �������� ����������� ������</summary>
        public string MyCompanyName { get; set; }
        /// <summary>������������� �����</summary>
        public int? FileId { get; set; }

        /// <summary>������������ �����</summary>
        public string FileName { get; set; }
        /// <summary>���������� �����</summary>
        public string FileExtention { get; set; }
        /// <summary>������ ������������ ����� (� ����������� �����)</summary>
        public string FileNameFull { get; set; }
        #endregion

    }

}