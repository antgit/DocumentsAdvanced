using System.Collections.Generic;

namespace BusinessObjects
{
    /// <summary>Интерфейс поддержки дополнительных свойств базовыми объектами</summary>
    /// <remarks>Дополнительные свойства соответствую дополнительным столбцам в базе данных, 
    /// "расширяющие" базовую таблицу по отношению к базовому объекту. Например, для класса "Единиц измерения" необходимо добавить 
    /// строковый столбец "StringData": 
    /// в оcновной таблице добавляем столбец "StringData" тип nvarchar(255) 
    /// в хранимой процедуре загрузки данных добавляем соответсвующий столбец
    /// в системе регестрируется дополнительное свойство "StringData" с соответсвующими свойствами.
    /// </remarks>
    public interface ICustomPropertySupport 
    {
        /// <summary>Зарегистрировать дополнительное свойство</summary>
        /// <param name="descriptor">Описатель свойства</param>
        void RegisterProperty(CustomPropertyDescriptor descriptor);
        /// <summary>Удалить регистрацию дополнительного свойства</summary>
        /// <param name="descriptor">Описатель свойства</param>
        void UnRegisterProperty(CustomPropertyDescriptor descriptor) ;
        /// <summary>Список дополнительных свойств</summary>
        List<CustomPropertyDescriptor> PropertyDescriptors { get; }
        ///// <summary>
        ///// Список дополнительных свойств
        ///// </summary>
        ///// <typeparam name="T">Тип</typeparam>
        ///// <param name="item">Объект</param>
        ///// <returns></returns>
        //List<CustomProperty<T>> Values(T item);
    }
}