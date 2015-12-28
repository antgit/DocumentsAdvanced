using System.Windows.Forms;
using BusinessObjects.Documents;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства автонумерации
        /// </summary>
        /// <param name="item">Автонумерация</param>
        /// <returns></returns>
        public static Form ShowProperty(this Autonum item)
        {
            InternalShowPropertyCore<Autonum> showProperty = new InternalShowPropertyCore<Autonum>
            {
                SelectedItem = item,
                ControlBuilder =
                    new BuildControlAutonum
                    {
                        SelectedItem = item
                    }
            };
            return showProperty.ShowDialog();
        }
        #endregion
    }
}
