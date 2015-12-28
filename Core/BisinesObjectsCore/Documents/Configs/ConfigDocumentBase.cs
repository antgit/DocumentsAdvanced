namespace BusinessObjects.Documents
{
    /// <summary>
    /// Базовый класс настроек документов
    /// </summary>
    public class ConfigDocumentBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ConfigDocumentBase()
        {
            
        }
        /// <summary>
        /// Сброс настроек на значения по умолчанию
        /// </summary>
        public virtual void Reset()
        {
            AllowNumberEdit = true;
            AllowAgenToEdit = true;
            AllowAgenToCreate = true;
            AllowAgenToSearch = true;

            AllowProductSearch = true;
            AllowProductEdit = true;
            
            DistpalyFormatQty = "0.00";
            DisplayFormatPrice = "0.00";
            DisplayFormatSumma = "0.00";

            DecimalPlacesQty = 2;
            DecimalPlacesPrice = 2;
            DecimalPlacesSumma = 2;
        }

        #region Шапка
        /// <summary>
        /// Разрешить редактирование номера
        /// </summary>
        public bool AllowNumberEdit { get; set; }

        /// <summary>
        /// Разрешить редактирование корреспондента непосредственно в документе
        /// </summary>
        public bool AllowAgenToEdit { get; set; }

        /// <summary>
        /// Разрешить создание корреспондента непосредственно в документе
        /// </summary>
        public bool AllowAgenToCreate { get; set; }

        /// <summary>
        /// Разрешить поиск корреспондента
        /// </summary>
        public bool AllowAgenToSearch { get; set; }

        #endregion
        #region Табличная часть

        /// <summary>
        /// Разрешить поиск товара
        /// </summary>
        public bool AllowProductSearch { get; set; }

        /// <summary>
        /// Разрешить редактирование товара непосредственно в документе
        /// </summary>
        public bool AllowProductEdit { get; set; }

        /// <summary>
        /// Разрешить создание товара непосредственно в документе
        /// </summary>
        public bool AllowProductCreate { get; set; }
        
        /// <summary>
        /// Формат "количество" при отображении
        /// </summary>
        public string DistpalyFormatQty { get; set; }

        /// <summary>
        /// Формат "цен" при отображении
        /// </summary>
        public string DisplayFormatPrice { get; set; }

        /// <summary>
        /// Формат "сумма" при отображении
        /// </summary>
        public string DisplayFormatSumma { get; set; }

        /// <summary>
        /// Количество  десятичных знаков "сумма" при редактировании
        /// </summary>
        public int DecimalPlacesSumma { get; set; }

        /// <summary>
        /// Количество  десятичных знаков "количество" при редактировании
        /// </summary>
        public int DecimalPlacesQty { get; set; }

        /// <summary>
        /// Количество  десятичных знаков "цен" при редактировании
        /// </summary>
        public int DecimalPlacesPrice { get; set; } 
        #endregion
        #region Панель действий

        /// <summary>
        /// Количество максимальных ссылок на подчиненные документы 
        /// </summary>
        public bool MaxLeftChainCount { get; set; }

        /// <summary>
        /// Количество максимальных ссылок на родительские документы 
        /// </summary>
        public bool MaxRightChainCount { get; set; }

        /// <summary>
        /// Количество максимальных ссылок на отчеты 
        /// </summary>
        public bool MaxReportCount { get; set; } 
        #endregion
    }
}