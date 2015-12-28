using System.Collections.Generic;

namespace BusinessObjects
{
    public interface IVirtualGroupBuilderExtender
    {
       
    }
    /// <summary>Постоитель виртуальных иерархий</summary>
    public interface IVirtualGroupBuilder<T> : IVirtualGroupBuilderExtender
    {
        //T Object { get; set; }
        /// <summary>
        /// Идентификатор системного объекта для которого будут строится группы
        /// </summary>
        int EntityId { get; set; }
        /// <summary>
        /// Выполнить построение
        /// </summary>
        /// <returns></returns>
        void Build();
        /// <summary>
        /// Список используемый по умолчанию для представления содержимого
        /// </summary>
        CustomViewList DefaultViewList { get; set; }
        /// <summary>
        /// Корневая иерархия, к которой привызан соответствующий построитель
        /// </summary>
        Hierarchy Hierarchy { get; set; }
        /// <summary>
        /// Коллекция корневых виртуальных групп
        /// </summary>
        /// <returns></returns>
        List<IVirtualGroup<T>> Roots();
    }
    //
}