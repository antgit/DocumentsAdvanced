using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства колонки
        /// </summary>
        /// <param name="item">Колонка</param>
        /// <returns></returns>
        public static Form ShowProperty(this CustomViewColumn item)
        {
            InternalShowPropertyBase<CustomViewColumn> showPropertyBase = new InternalShowPropertyBase<CustomViewColumn>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlColumn() { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion
    }
}
