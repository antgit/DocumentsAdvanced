using System.Collections.Generic;

namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс расширенной цепочки
    /// </summary>
    /// <typeparam name="T">Тип</typeparam>
    /// <typeparam name="T2">Тип</typeparam>
    public interface IChainAdvanced<T, T2> : ICoreObject
    {
        /// <summary>
        /// Источник
        /// </summary>
        T Left { get; }
        /// <summary>
        /// Назначение
        /// </summary>
        T2 Right { get; }
        /// <summary>
        /// Идентификатор источника
        /// </summary>
        int LeftId { get; }
        ///// <summary>
        ///// Идентификатор записи в базе источнике
        ///// </summary>
        //int DbSourceId { get;} 
        /// <summary>
        /// Идентификатор назначения
        /// </summary>
        int RightId { get; }
        /// <summary>
        /// Идентификатор вида
        /// </summary>
        int KindId { get; }
        /// <summary>
        /// Вид связи
        /// </summary>
        ChainKind Kind { get; }
        /// <summary>
        /// Глобальный номер в списке
        /// </summary>
        int OrderNo { get; }
        /// <summary>
        /// Код
        /// </summary>
        string Code { get; }
        /// <summary>
        /// Примечание
        /// </summary>
        string Memo { get; }
    }

    /// <summary>
    /// Интерфейс типа реализующий наличие расширенных цепочек
    /// </summary>
    /// <typeparam name="T">Тип первого значения (источник)</typeparam>
    /// <typeparam name="T2">Тип второго значения (ссылка)</typeparam>
    public interface IChainsAdvancedList<T, T2>
    {
        List<IChainAdvanced<T, T2>> GetLinks();
        List<IChainAdvanced<T, T2>> GetLinks(int? kind);
        List<ChainValueView> GetChainView();
    }
}