namespace BusinessObjects.Models
{
    /// <summary>
    /// ������ ������� ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseModel<T> : CoreModel where T : class, ICoreObject, new()
    {
        /// <summary>�����������</summary>
        public BaseModel():base()
        {
            
        }
        /// <summary>
        /// ���������� ������
        /// </summary>
        /// <param name="value"></param>
        public virtual void GetData(T value) 
        {
            base.GetData(value);
            BaseCore<T> val = value as BaseCore<T>;
            Code = val.Code;
            Name = val.Name;
            Memo = val.Memo;
            FlagString = val.FlagString;
            TemplateId = val.TemplateId;
            KindId = val.KindId;
            KindValue = val.KindValue;
            CodeFind = val.CodeFind;
            NameFull = val.NameFull;
            if (val.Memo != null && val.Memo.Length > 100)
                DisplayMemo = val.Memo.Substring(0, 100) + "...";
            else
                DisplayMemo = val.Memo;
        }
        /// <summary>�������</summary>
        public string Code { get; set; }
        /// <summary>����������</summary>
        public string Memo { get; set; }
        /// <summary>���������� �� 100 ��������</summary>
        public string DisplayMemo { get; set; }
        /// <summary>������������</summary>
        public string Name { get; set; }
        /// <summary>�������������� ��������� ����</summary>
        public string FlagString { get; set; }
        /// <summary>������������� �������</summary>
        public int TemplateId { get; set; }
        /// <summary>������ ������������� ����</summary>
        public int KindId { get; set; }
        /// <summary>������������� ������� ��������</summary>
        public short KindValue { get; set; }
        /// <summary>��� ������</summary>
        public string CodeFind { get; set; }
        /// <summary>������ ������������</summary>
        public string NameFull { get; set; }
    }
}