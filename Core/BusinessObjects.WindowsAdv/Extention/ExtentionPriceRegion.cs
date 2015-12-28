using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства ценового диапазона
        /// </summary>
        /// <param name="item">Ценовой диапазон</param>
        /// <returns></returns>
        public static Form ShowProperty(this PriceRegion item)
        {
            InternalShowPropertyCore<PriceRegion> showProperty = new InternalShowPropertyCore<PriceRegion>();
            showProperty.SelectedItem = item;
            showProperty.ControlBuilder = new BuildControlPriceRegion { SelectedItem = item };
            return showProperty.ShowDialog();
        }

        /// <summary>
        /// Свойства ценового диапазона
        /// </summary>
        /// <param name="item">Ценовой диапазон</param>
        /// <param name="allowProduct">Разрешить изменение товара</param>
        /// <param name="allowPriceName">Разрешить изменение вида цены</param>
        /// <returns></returns>
        public static Form ShowPriceRegion(this PriceRegion item, bool allowProduct, bool allowPriceName, bool modal=false)
        {
            InternalShowPropertyCore<PriceRegion> showProperty = new InternalShowPropertyCore<PriceRegion>();
            showProperty.SelectedItem = item;
            showProperty.ControlBuilder = new BuildControlPriceRegion { SelectedItem = item };

            showProperty.Show += delegate
                                     {
                                         ControlPriceRegion ctl =
                                             (showProperty.ControlBuilder.Control.Controls[0] as ControlPriceRegion);
                                         if (!allowProduct)
                                             ctl.cmbProduct.Properties.ReadOnly = true;
                                         if (!allowPriceName)
                                             ctl.cmbPriceName.Properties.ReadOnly = true;
                                     };
            return showProperty.ShowDialog(modal);
        }
        #endregion
    }
}
