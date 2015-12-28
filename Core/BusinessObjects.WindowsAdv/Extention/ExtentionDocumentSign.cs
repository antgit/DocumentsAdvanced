using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Окно свойств подписи документа
        /// </summary>
        /// <param name="item">Подпись документа</param>
        /// <param name="agent">Корреспондент</param>
        /// <returns></returns>
        public static Form ShowProperty(this DocumentSign item, Agent agent=null)
        {
            InternalShowPropertyCore<DocumentSign> showProperty = new InternalShowPropertyCore<DocumentSign>
            {
                SelectedItem = item,
                ControlBuilder =
                    new BuildControlDocumentSign()
                    {
                        SelectedItem = item,
                        Agent = agent
                    }
            };
            return showProperty.ShowDialog();
        }
        #endregion
    }
}
