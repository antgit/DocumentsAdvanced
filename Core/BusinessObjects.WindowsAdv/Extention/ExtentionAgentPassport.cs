using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства
        /// <summary>
        /// Свойства паспорта
        /// </summary>
        /// <param name="item">Состояние</param>
        /// <returns></returns>
        public static Form ShowProperty(this Passport item)
        {
            InternalShowPropertyCore<Passport> showProperty = new InternalShowPropertyCore<Passport>
            {
                SelectedItem = item,
                ControlBuilder =
                    new BuildControlPassport 
                    {
                        SelectedItem = item
                    }
            };
            return showProperty.ShowDialog();
        }
        #endregion
    }
}