namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель валюты
    /// </summary>
    public class CurrencyModel : BaseModel<Currency>
    {
        /// <summary>Конструктор</summary>
        public CurrencyModel()
        {
        }
        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="value">Валюта</param>
        public override void GetData(Currency value)
        {
            base.GetData(value);
            IntCode = value.IntCode;
            //MyCompanyId = value.MyCompanyId;
            //MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
        }
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Цифровой код валюты</summary>
        public int IntCode { get; set; }
        /// <summary>Основная група</summary>
        public string DefaultGroup { get; set; }

    }
}