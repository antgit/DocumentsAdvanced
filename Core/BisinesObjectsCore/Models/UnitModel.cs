namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель единиц измерения
    /// </summary>
    public class UnitModel : BaseModel<Unit>
    {
        /// <summary>Конструктор</summary>
        public UnitModel()
        {
        }
        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="value">Единица измерения</param>
        public override void GetData(Unit value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
            CodeInternational = value.CodeInternational;
        }
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Основная група</summary>
        public string DefaultGroup { get; set; }
        /// <summary>Международное наименование</summary>
        public string CodeInternational { get; set; }    

    }
}