using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства
        /// <summary>
        /// Свойства водительского удостоверения
        /// </summary>
        /// <param name="item">Водительское удостоверение</param>
        /// <returns></returns>
        public static Form ShowProperty(this DrivingLicence item)
        {
            InternalShowPropertyCore<DrivingLicence> showProperty = new InternalShowPropertyCore<DrivingLicence>
            {
                SelectedItem = item,
                ControlBuilder =
                    new BuildControlAgentDrivingLicence
                    {
                        SelectedItem = item
                    }
            };
            return showProperty.ShowDialog();
        }
        #endregion
    }
}
