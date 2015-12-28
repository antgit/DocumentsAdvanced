namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель аналитики
    /// </summary>
    public class AnaliticModel : BaseModel<Analitic>
    {
        /// <summary>Конструктор</summary>
        public AnaliticModel()
        {
        }
        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="value">Аналитика</param>
        public override void GetData(Analitic value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
        }
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Основная група</summary>
        public string DefaultGroup { get; set; }

    }
}