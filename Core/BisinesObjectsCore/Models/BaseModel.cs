namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель второго уровня
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseModel<T> : CoreModel where T : class, ICoreObject, new()
    {
        /// <summary>Конструктор</summary>
        public BaseModel():base()
        {
            
        }
        /// <summary>
        /// Заполнение данных
        /// </summary>
        /// <param name="value"></param>
        public virtual void GetData(T value) 
        {
            base.GetData(value);
            BaseCore<T> val = value as BaseCore<T>;
            Code = val.Code;
            Name = val.Name;
            Memo = val.Memo;
            FlagString = val.FlagString;
            TemplateId = val.TemplateId;
            KindId = val.KindId;
            KindValue = val.KindValue;
            CodeFind = val.CodeFind;
            NameFull = val.NameFull;
            if (val.Memo != null && val.Memo.Length > 100)
                DisplayMemo = val.Memo.Substring(0, 100) + "...";
            else
                DisplayMemo = val.Memo;
        }
        /// <summary>Признак</summary>
        public string Code { get; set; }
        /// <summary>Примечание</summary>
        public string Memo { get; set; }
        /// <summary>Примечание до 100 символов</summary>
        public string DisplayMemo { get; set; }
        /// <summary>Наименование</summary>
        public string Name { get; set; }
        /// <summary>Дополнительный строковый флаг</summary>
        public string FlagString { get; set; }
        /// <summary>Идентификатор шаблона</summary>
        public int TemplateId { get; set; }
        /// <summary>Полная идентификация типа</summary>
        public int KindId { get; set; }
        /// <summary>Идентификатор подтипа элемента</summary>
        public short KindValue { get; set; }
        /// <summary>Код поиска</summary>
        public string CodeFind { get; set; }
        /// <summary>Полное наименование</summary>
        public string NameFull { get; set; }
    }
}