using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства связи
        /// </summary>
        /// <param name="item">Связь</param>
        /// <returns></returns>
        public static Form ShowProperty<T>(this Chain<T> item) where T : class, IBase, new()
        {
            InternalShowPropertyCore<Chain<T>> showProperty = new InternalShowPropertyCore<Chain<T>>();
            showProperty.SelectedItem = item;
            showProperty.ControlBuilder = new BuildControlChain<T> { SelectedItem = item};
            return showProperty.ShowDialog();
        }
        /// <summary>
        /// Свойства связи
        /// </summary>
        /// <param name="item">Связь</param>
        /// <returns></returns>
        public static Form ShowProperty<TSource, TDestination>(this ChainAdvanced<TSource, TDestination> item)
            where TSource : class, IBase, new()
            where TDestination : class, IBase, new()
        {
            InternalShowPropertyCore<ChainAdvanced<TSource, TDestination>> showProperty = new InternalShowPropertyCore<ChainAdvanced<TSource, TDestination>>();
            showProperty.SelectedItem = item;
            showProperty.ControlBuilder = new BuildControlChainAdvanced<TSource, TDestination> { SelectedItem = item};
            return showProperty.ShowDialog();
        }
        #endregion
    }
}
