using System;

namespace BusinessObjects.Models
{
    /// <summary>
    /// ������ �������������
    /// </summary>
    public class DepatmentModel : BaseModel<Depatment>
    {
        /// <summary>
        /// �����������
        /// </summary>
        public DepatmentModel()
        {

        }
        /// <summary>
        /// ��������� ������ �� �������
        /// </summary>
        /// <param name="value">�����</param>
        public override void GetData(Depatment value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
            DepatmentHeadId = value.DepatmentHeadId;
            DepatmentSubHeadId = value.DepatmentSubHeadId;
            DepatmentHeadName = value.DepatmentHeadId != 0 ? value.DepatmentHead.Name : string.Empty;
            DepatmentSubHeadName = value.DepatmentSubHeadId != 0 ? value.DepatmentSubHead.Name : string.Empty;
            Phone = value.Phone;
        }

        /// <summary>�������� �����</summary>
        public string DefaultGroup { get; set; }
        /// <summary>������������� ������������</summary>
        public int DepatmentHeadId { get; set; }    
        /// <summary>������������� �����������</summary>
        public int DepatmentSubHeadId { get; set; }
        /// <summary>������������ ������������</summary>
        public string DepatmentHeadName { get; set; }
        /// <summary>������������ �����������</summary>
        public string DepatmentSubHeadName { get; set; }
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId { get; set; }
        /// <summary>������������ �����������, �������� ����������� ������</summary>
        public string MyCompanyName { get; set; }
        /// <summary>�������� �������</summary>
        public string Phone { get; set; }    
    }
}