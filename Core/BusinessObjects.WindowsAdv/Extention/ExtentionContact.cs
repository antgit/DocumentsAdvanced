using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства контактной информации
        /// </summary>
        /// <param name="item">Контактная информация</param>
        /// <returns></returns>
        public static Form ShowProperty(this Contact item)
        {
            InternalShowPropertyBase<Contact> showPropertyBase = new InternalShowPropertyBase<Contact>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlContact { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion
    }
}
