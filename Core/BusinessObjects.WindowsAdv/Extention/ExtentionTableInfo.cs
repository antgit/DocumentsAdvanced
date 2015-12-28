using System.Windows.Forms;
using BusinessObjects.Developer;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства таблицы
        /// </summary>
        /// <param name="item">Таблица</param>
        /// <returns></returns>
        public static Form ShowProperty(this DbObject item)
        {
            InternalShowPropertyCore<DbObject> showProperty = new InternalShowPropertyCore<DbObject>
                                                                   {
                                                                       SelectedItem = item,
                                                                       ControlBuilder =
                                                                           new BuildControlTableInfo
                                                                               {
                                                                                   SelectedItem = item
                                                                               }
                                                                   };
            return showProperty.ShowDialog();
        }
        /// <summary>
        /// Свойства столбца
        /// </summary>
        /// <param name="item">Столбец</param>
        /// <returns></returns>
        public static Form ShowProperty(this DbObjectChild item)
        {
            InternalShowPropertyCore<DbObjectChild> showProperty = new InternalShowPropertyCore<DbObjectChild>
                                                                    {
                                                                        SelectedItem = item,
                                                                        ControlBuilder =
                                                                            new BuildControlColumnInfo
                                                                                {
                                                                                    SelectedItem = item
                                                                                }
                                                                    };
            return showProperty.ShowDialog();
        }
        #endregion
    }
}
