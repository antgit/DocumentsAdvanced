namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс поддержки иерархий объектами
    /// </summary>
    public interface IHierarchySupport
    {
        /// <summary>
        /// Первая иерархия в которую входит объект
        /// </summary>
        /// <returns></returns>
        Hierarchy FirstHierarchy();
    }
}