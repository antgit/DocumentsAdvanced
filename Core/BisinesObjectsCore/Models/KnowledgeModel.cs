namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель статьи базы знаний
    /// </summary>
    public class KnowledgeModel : BaseModel<Knowledge>
    {
        /// <summary>Конструктор</summary>
        public KnowledgeModel()
        {
            
        }
        /// <summary>Заполнение данных</summary>
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
        #region Свойства
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Идентификатор файла</summary>
        public int? FileId { get; set; }

        /// <summary>Наименование файла</summary>
        public string FileName { get; set; }
        /// <summary>Расширение файла</summary>
        public string FileExtention { get; set; }
        /// <summary>Полное наименование файла (с расширением файла)</summary>
        public string FileNameFull { get; set; }
        #endregion

    }

}