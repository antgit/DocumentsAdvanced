using System.Collections.Generic;
using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства наименования факта
        /// </summary>
        /// <param name="item">Наименование факта</param>
        /// <returns></returns>
        public static Form ShowProperty(this FactName item)
        {
            InternalShowPropertyBase<FactName> showPropertyBase = new InternalShowPropertyBase<FactName>
                                                                      {
                                                                          SelectedItem = item,
                                                                          ControlBuilder =
                                                                              new BuildControlFactName
                                                                                  {
                                                                                      SelectedItem = item
                                                                                  }
                                                                      };
            return showPropertyBase.ShowDialog();
        }

        /// <summary>
        /// Свойства наименования колонки факта или свойства
        /// </summary>
        /// <param name="item">Колонка факта или свойства</param>
        /// <returns></returns>
        public static Form ShowProperty(this FactColumn item)
        {
            InternalShowPropertyBase<FactColumn> showPropertyBase = new InternalShowPropertyBase<FactColumn>
            {
                SelectedItem = item,
                ControlBuilder =
                    new BuildControlFactColumn
                    {
                        SelectedItem = item
                    }
            };
            return showPropertyBase.ShowDialog();
        }

        public static Form ShowProperty(this FactValue item)
        {
            /*if (item.FactDate.FactColumn.FactName.KindValue == 1)
            {
                return item.ShowPropertyFactValues();
            }
            else*/
                return item.ShowPropertyFactValue();
        }
        /// <summary>
        /// Свойства значения факта или свойства
        /// </summary>
        /// <param name="item">Значение факта или свойства</param>
        /// <returns></returns>
        public static Form ShowPropertyFactValue(this FactValue item)
        {
            InternalShowPropertyCore<FactValue> showProperty = new InternalShowPropertyCore<FactValue>();
            showProperty.SelectedItem = item;
            showProperty.ControlBuilder = new BuildControlFactValue { SelectedItem = item };
            return showProperty.ShowDialog();
        }
        public static Form ShowPropertyFactValues(this FactValue item)
        {
            InternalShowPropertyCore<FactValue> showProperty = new InternalShowPropertyCore<FactValue>();
            showProperty.SelectedItem = item;
            showProperty.ControlBuilder = new BuildControlFactValues { SelectedItem = item };
            return showProperty.ShowDialog();
        }
        public static List<FactName> BrowseContent(this FactName item, Workarea wa = null)
        {
            ContentModuleFactName module = new ContentModuleFactName();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
        #endregion
    }
}
