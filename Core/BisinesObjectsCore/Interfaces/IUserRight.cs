namespace BusinessObjects.Security
{
    /// <summary>
    /// Разрешение
    /// </summary>
    interface IUserRight : ICoreObject
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        Uid Uid { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        int DbUidId { get; set; }
        /// <summary>
        /// Разрешение
        /// </summary>
        Right Right { get; set; }
        /// <summary>
        /// Идентификатор разрешения
        /// </summary>
        int RightId { get; set; }
        /// <summary>
        /// Сохранить
        /// </summary>
        void Save();
        /// <summary>
        /// Значение
        /// </summary>
        short? Value { get; set; }
    }
}
