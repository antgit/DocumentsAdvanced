using System;

namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс детелизации документа
    /// </summary>
    public interface IDocumentDetail : ICoreObject
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        EntityDocument EntityDocument { get; }
        DateTime Date { get; set; }
        int Kind { get; set; }
    }
}
