using System.Collections.Generic;

namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс поддержки дополнительных кодов
    /// </summary>
    /// <typeparam name="T">Тип</typeparam>
    public interface ICode<T> : ICoreObject
    {
        /// <summary>
        /// Идентификатор наименование кода
        /// </summary>
        int CodeNameId { get; }
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        int ElementId { get; }
        /// <summary>
        /// Наименование кода
        /// </summary>
        CodeName CodeName { get; }
        /// <summary>
        /// Объект
        /// </summary>
        T Element { get; }
    }
    /// <summary>
    /// Интерфейс поддержки дополнительных кодов
    /// </summary>
    public interface ICodes
    {
        
    }
    /// <summary>
    /// Интерфейс поддержки дополнительных кодов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICodes<T> : ICodes where T : class, IBase, new()
    {
        /// <summary>
        /// Список значений дополнительных кодов для объекта
        /// </summary>
        /// <param name="allKinds"></param>
        /// <returns></returns>
        List<CodeValue<T>> GetValues(bool allKinds);
        /// <summary>
        /// Список представлний для отображения
        /// </summary>
        /// <param name="allKinds"></param>
        /// <returns></returns>
        List<CodeValueView> GetView(bool allKinds);
    }
}