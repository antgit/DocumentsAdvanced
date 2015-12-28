using BusinessObjects.Documents;

namespace BusinessObjects
{
    /// <summary>
    /// Расширение документа
    /// </summary>
    public interface IDocument : ICoreObject
    {
        /// <summary>
        /// Основной документ
        /// </summary>
        Document Document { get; set; }
        /// <summary>
        /// Загрузка
        /// </summary>
        /// <param name="op">Основной документ</param>
        void Load(Document op);
        EntityDocument EntityDocument { get; }
        ///// <summary>
        ///// Числовое представление вида документа
        ///// </summary>
        ///// <see cref="EntityDocumentKind.Id"/>
        //Int16 EntityId { get; }
    }
}
