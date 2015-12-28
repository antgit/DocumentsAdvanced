using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства числового флага
        /// </summary>
        /// <param name="item">Числовой флаг</param>
        /// <returns></returns>
        public static Form ShowProperty(this FlagValue item)
        {
            InternalShowPropertyCore<FlagValue> showProperty = new InternalShowPropertyCore<FlagValue>
                                                                   {
                                                                       SelectedItem = item,
                                                                       ControlBuilder =
                                                                           new BuildControlFlagValue
                                                                               {
                                                                                   SelectedItem = item
                                                                               }
                                                                   };
            return showProperty.ShowDialog();
        }
        #endregion

    }
}

