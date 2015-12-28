﻿using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства состояния
        /// </summary>
        /// <param name="item">Состояние</param>
        /// <returns></returns>
        public static Form ShowProperty(this ResourceString item)
        {
            InternalShowPropertyCore<ResourceString> showProperty = new InternalShowPropertyCore<ResourceString>();
            showProperty.SelectedItem = item;
            showProperty.Modal = true;
            showProperty.ControlBuilder = new BuildControlResourceString { SelectedItem = item};
            return showProperty.ShowDialog();
        }
        #endregion
    }
}
