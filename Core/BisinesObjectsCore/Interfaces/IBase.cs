using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс базовых типов 2-го уровня
    /// </summary>
    public interface IBase : ICoreObject
    {
        
        /// <summary>
        /// Числовое значение вида 
        /// </summary>
        Int16 KindValue { get; set; }
        /// <summary>
        /// Полный (составной) идентификатор вида объекта
        /// </summary>
        int KindId { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        string NameFull { get; set; }
        /// <summary>
        /// Признак
        /// </summary>
        string Code { get; set; }
        /// <summary>
        /// Признак
        /// </summary>
        string CodeFind { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        string Memo { get; set; }
        /// <summary>
        /// Идентификатор шаблона
        /// </summary>
        int TemplateId { get; set; }
    }

    public interface IFindBy<T>
    {
        List<T> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
                       int? stateId = null, string name = null, int kindId = 0, string code = null,
                       string memo = null, string flagString = null, int templateId = 0,
                       int count = 100, Predicate<T> filter = null);
    }
    public interface IFlagString
    {
        string GetFlagStringAll();
        string FlagString { get; set; }
    }
    /// <summary>
    /// Интерфейс копирования объекта на уровне базы данных
    /// </summary>
    /// <remarks>Если объект поддерживает данный интерфейс, создание копии объекта осуществляется с использованием 
    /// хранимой процедуры "CreateCopy" и возвращает объект польностью соответствующий текущему. В отличии от Clone 
    /// копия является точной и включает вложенные зависимые объекты.</remarks>
    /// <typeparam name="T"></typeparam>
    public interface ICopyData<T>
    {
        /// <summary>
        /// Создание копии объекта в базе данных
        /// </summary>
        /// <returns></returns>
        T CreateCopy();
    }
}
