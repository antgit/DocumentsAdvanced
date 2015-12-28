namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс поддрежки опеделения компании владельца
    /// </summary>
    public interface ICompanyOwner
    {
        /// <summary>
        /// Моя компания, предприятие которому принадлежит объект
        /// </summary>
        Agent MyCompany { get; }
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит объект
        /// </summary>
        int MyCompanyId { get; set; }
    }
}