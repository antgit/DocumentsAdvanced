namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель отчета
    /// </summary>
    public class ReportModel : BaseModel<Library>
    {
        /// <summary>Конструктор</summary>
        public ReportModel()
        {
        }
        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="value">Аналитика</param>
        public override void GetData(Library value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
        }
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Url документации отчета</summary>
        public string HelpUrl { get; set; }
        /// <summary>Ссылка на отчет с учетеом настроек использования Flash</summary>
        public string NavigateUrl { get; set; }
        /// <summary>Ссылка на отчет с Flash </summary>
        public string NavigateUrlFx { get; set; }

    }
}