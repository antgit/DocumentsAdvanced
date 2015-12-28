namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс поддержки заполнения объекта по шаблону
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICopyValue<T>
    {
        /// <summary>
        /// Заполнение данными текущего объекта по данным шаблона
        /// </summary>
        /// <param name="template">Шаблон</param>
        void CopyValue(T template);
    }
}
