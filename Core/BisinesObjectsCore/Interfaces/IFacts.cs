using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс поддержки дополнительных свойств и фактов
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    public interface IFacts<T>: ICoreObject
    {
        ///// <summary>
        ///// Текущие значения свойств
        ///// </summary>
        //List<FactValue> GetFactActualValues();
        /// <summary>
        /// Коллекция наименований фактов зарегестрированных для типа
        /// </summary>
        /// <returns></returns>
        List<FactName> GetFactNames();
        ///// <summary>
        ///// Коллекция значений фактов или дополнительных свойств для типа
        ///// </summary>
        ///// <param name="factCode">Код наименования факта</param>
        ///// <param name="columnCode">Код колонки</param>
        ///// <param name="actualDate">Дата, на которую необъобходимо получить значение</param>
        ///// <returns></returns>
        //FactValue GetFactValue(string factCode, string columnCode, DateTime actualDate);

        /// <summary>
        /// Обновить значения фактов в представлении
        /// </summary>
        void RefreshFaсtView();

        FactView GetFactViewValue(string factCode, string columnCode);
        /// <summary>
        /// Текущее представление значений фактов
        /// </summary>
        /// <returns></returns>
        List<FactView> GetCollectionFactView();
    }
}