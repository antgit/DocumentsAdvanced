using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства состояния
        /// </summary>
        /// <param name="item">Состояние</param>
        /// <returns></returns>
        public static Form ShowProperty(this ResourceImage item)
        {
            InternalShowPropertyCore<ResourceImage> showProperty = new InternalShowPropertyCore<ResourceImage>();
            showProperty.SelectedItem = item;
            showProperty.Modal = true;
            showProperty.ControlBuilder = new BuildControlResourceImage { SelectedItem = item };
            return showProperty.ShowDialog();
        }
        #endregion
    }
}
