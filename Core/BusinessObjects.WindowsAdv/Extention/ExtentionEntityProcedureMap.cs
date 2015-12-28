using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства маппинга хранимых процедур
        /// </summary>
        /// <param name="item">Маппинг хранимых процедур</param>
        /// <returns></returns>
        public static Form ShowProperty(this ProcedureMap item)
        {
            InternalShowPropertyCore<ProcedureMap> showProperty = new InternalShowPropertyCore<ProcedureMap>
                                                                      {
                                                                          SelectedItem = item,
                                                                          ControlBuilder =
                                                                              new BuildControlProcedureMap
                                                                                  {
                                                                                      SelectedItem = item
                                                                                  }
                                                                      };
            return showProperty.ShowDialog();
        }
        #endregion
    }
}
