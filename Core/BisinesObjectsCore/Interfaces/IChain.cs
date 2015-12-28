using System.Collections.Generic;

namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс цепочки
    /// </summary>
    /// <typeparam name="T">Тип</typeparam>
    public interface IChain<T> : ICoreObject
    {
        /// <summary>
        /// Источник
        /// </summary>
        T Left { get; }
        /// <summary>
        /// Назначение
        /// </summary>
        T Right { get; }
        /// <summary>
        /// Идентификатор источника
        /// </summary>
        int LeftId { get;}
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
        int OrderNo { get; set; }
    }

    /// <summary>
    /// Интерфейс типа реализующий наличие цепочек
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IChains<T>
    {
        /// <summary>
        /// Коллекция всех связей
        /// </summary>
        /// <returns></returns>
        List<IChain<T>> GetLinks();
        /// <summary>
        /// Коллекция связей указанного типа
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        List<IChain<T>> GetLinks(int kind);
        /// <summary>
        /// Коллекция всех объектов по указанному типу связи 
        /// </summary>
        /// <remarks>Метод реализующий данную возможность использует маппинг хранимой процедуры по ключу "LoadChainSourceList". 
        /// Метод возвращает коллекцию всех связанных объектов по указанному виду связи для основного объекта 
        /// (объекты расположенные "справа" от указанного объекта). 
        /// Если вид связи не указан или равен 0 - возвращаются все связанные объекты.
        /// <para>Метод используется для получения коллекций на основе связей. 
        /// Примеры использования - в окне свойств предприятия: выбо ответственных сотрудников.</para>
        /// </remarks>
        /// <param name="chainKindId">Тип связи</param>
        /// <returns>Коллекция связанных объектов расположенных справа.</returns>
        List<T> SourceList(int chainKindId);
        /// <summary>
        /// Коллекция всех объектов по указанному типу связи 
        /// </summary>
        /// <remarks>Метод реализующий данную возможность использует маппинг хранимой процедуры по ключу "LoadChainDestinationList". 
        /// Метод возвращает коллекцию всех связанных объектов по указанному виду связи для основного объекта 
        /// (объекты расположенные "слева" от указанного объекта). 
        /// Если вид связи не указан или равен 0 - возвращаются все связанные объекты.
        /// <para>Метод используется для получения коллекций на основе связей. 
        /// Примеры использования - в окне свойств предприятия: выбо ответственных сотрудников.</para>
        /// </remarks>
        /// <param name="chainKindId">Тип связи</param>
        /// <returns>Коллекция связанных объектов расположенных справа.</returns>
        List<T> DestinationList(int chainKindId);
    }
}
