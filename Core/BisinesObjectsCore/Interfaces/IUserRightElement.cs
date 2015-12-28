namespace BusinessObjects.Security
{
    /// <summary>
    /// Разрешение для элемента
    /// </summary>
    interface IUserRightElement : IUserRight
    {
        /// <summary>
        /// Идентификатор элемента
        /// </summary>
        int ElementId { get; set; }
        /// <summary>
        /// Идентификатор системного типа
        /// </summary>
        short DbEntityId { get; set; }
    }
}
