using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства расчетного счета
        /// </summary>
        /// <param name="item">Предприятие</param>
        /// <returns></returns>
        public static Form ShowProperty(this AgentBankAccount item)
        {
            InternalShowPropertyBase<AgentBankAccount> showPropertyBase = new InternalShowPropertyBase<AgentBankAccount>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildAgentBankAccountControl { SelectedItem = item, ActivePage = 1, AvailablePages = BuildAgentBankAccountControl.PAGESMAX };
            return showPropertyBase.ShowDialog();
        }
        #endregion
    }
}
