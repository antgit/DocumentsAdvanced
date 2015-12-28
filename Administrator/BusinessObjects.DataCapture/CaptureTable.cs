namespace BusinessObjects.DataCapture
{
    internal class CaptureTable
    {
        /// <summary>
        /// Схема 
        /// </summary>
        public string Schema { get; set; }
        /// <summary>
        /// Наименование таблицы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Наименование инстанса
        /// </summary>
        public string Instans { get; set; }
        /// <summary>
        /// Файловая группа
        /// </summary>
        public string FileGroup { get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        public string RoleName { get; set; }
    }
}