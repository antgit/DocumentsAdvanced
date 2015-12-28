using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessObjects.Windows.Wizard;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

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
            showPropertyBase.ControlBuilder = new BuildAgentBankAccountControl { SelectedItem = item};
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список банковских счетов
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static AgentBankAccount BrowseList(this AgentBankAccount item)
        {
            List<AgentBankAccount> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Список банковских счетов
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для просмотра</param>
        /// <returns></returns>
        public static List<AgentBankAccount> BrowseList(this AgentBankAccount item, Predicate<AgentBankAccount> filter, List<AgentBankAccount> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<AgentBankAccount> BrowseMultyList(this AgentBankAccount item, Workarea wa, Predicate<AgentBankAccount> filter, List<AgentBankAccount> sourceCollection, bool allowMultySelect)
        {
            List<AgentBankAccount> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<AgentBankAccount>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<AgentBankAccount> browserBaseObjects = new ListBrowserBaseObjects<AgentBankAccount>(wa, sourceCollection, filter, item, true, false, false, true);
            browserBaseObjects.Owner = frm;
            browserBaseObjects.Build();

            new FormStateMaintainer(frm, String.Format("Browse{0}", item.GetType().Name));
            frm.clientPanel.Controls.Add(browserBaseObjects.ListControl);
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnProp.Visibility = BarItemVisibility.Always;
            frm.btnCreate.Visibility = BarItemVisibility.Always;
            frm.btnDelete.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Visibility = BarItemVisibility.Always;

            frm.btnDelete.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.DELETE_X32);
            frm.btnSelect.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.SELECT_X32);

            frm.btnProp.ItemClick += delegate
            {
                browserBaseObjects.InvokeProperties();
            };
            frm.btnDelete.ItemClick += delegate
            {
                browserBaseObjects.InvokeDelete();
            };
            browserBaseObjects.ListControl.Dock = DockStyle.Fill;
            browserBaseObjects.ShowProperty += delegate(AgentBankAccount obj)
            {
                obj.ShowProperty();
                if (obj.IsNew)
                {
                    obj.Created += delegate
                    {
                        int position = browserBaseObjects.BindingSource.Add(obj);
                        browserBaseObjects.BindingSource.Position = position;
                    };
                }
            };

            browserBaseObjects.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.Alt)
                {
                    returnValue = browserBaseObjects.SelectedValues;
                    frm.Close();
                }
            };
            frm.btnSelect.ItemClick += delegate
            {
                returnValue = browserBaseObjects.SelectedValues;
                frm.Close();
            };
            frm.btnClose.ItemClick += delegate
            {
                returnValue = null;
                frm.Close();

            };
            frm.ShowDialog();

            return returnValue;
        }
        /// <summary>
        /// Просмотр банковских счетов в отдельном окне
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="wa">Рабочая область</param>
        /// <returns></returns>
        public static List<AgentBankAccount> BrowseContent(this AgentBankAccount item, Workarea wa = null)
        {
            ContentModuleAgentBankAccount module = new ContentModuleAgentBankAccount();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }

        /// <summary>
        /// Мастер свойств банковского счета
        /// </summary>
        /// <param name="value">Банковский счет</param>
        /// <returns></returns>
        public static AgentBankAccount ShowWizard(this AgentBankAccount value)
        {
            //Hierarchy newItem = new Hierarchy { Workarea = value.Workarea, KindId = Hierarchy.KINDID_GROUP };
            FormWizardBankAccount wizard = new FormWizardBankAccount();
            wizard.ShowInTaskbar = false;
            //wizard.Icon = Icon.FromHandle(ResourceImage.GetByCode(value.Workarea, ResourceImage.Agent_X16).GetHicon());

            wizard.txtName.Text = value.Name;
            wizard.txtMemo.Text = value.Memo;
            wizard.txtCode.Text = value.Code;
            wizard.txtNameFull.Text = value.NameFull;
            
            #region Банк
            wizard.cmbBank.Properties.DisplayMember = GlobalPropertyNames.Name;
            wizard.cmbBank.Properties.ValueMember = GlobalPropertyNames.Id;
            List<Agent> collBank = new List<Agent>();
            BindingSource bankBinding = new BindingSource();

            if (value.BankId != 0)
                collBank.Add(value.Workarea.Cashe.GetCasheData<Agent>().Item(value.BankId));

            bankBinding.DataSource = collBank;
            wizard.cmbBank.Properties.DataSource = bankBinding;
            wizard.cmbBank.EditValue = value.BankId;
            DataGridViewHelper.GenerateLookUpColumns(value.Workarea, wizard.cmbBank, "DEFAULT_LOOKUPAGENT");
            wizard.ViewBank.CustomUnboundColumnData += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
            {
                UIHelper.DisplayAgentImagesLookupGrid(e, bankBinding);
            };
            wizard.cmbBank.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
            {
                SearchLookUpEdit cmb = sender as SearchLookUpEdit;
                //if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                //    cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
                try
                {
                    wizard.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                    if (bankBinding.Count < 2)
                    {
                        collBank = value.Workarea.GetCollection<Agent>().Where(f => (f.IsBank)).ToList();
                        bankBinding.DataSource = collBank;
                    }


                }
                catch (Exception)
                {
                }
                finally
                {
                    wizard.Cursor = Cursors.Default;
                }
            };
            #endregion

            #region Валюта
            wizard.cmbCurrency.Properties.DisplayMember = GlobalPropertyNames.Name;
            wizard.cmbCurrency.Properties.ValueMember = GlobalPropertyNames.Id;
            List<Currency> collCurrency = new List<Currency>();
            BindingSource currencyBinding = new BindingSource();

            if (value.BankId != 0)
                collCurrency.Add(value.Workarea.Cashe.GetCasheData<Currency>().Item(value.CurrencyId));

            currencyBinding.DataSource = collCurrency;
            wizard.cmbCurrency.Properties.DataSource = currencyBinding;
            wizard.cmbCurrency.EditValue = value.CurrencyId;
            DataGridViewHelper.GenerateLookUpColumns(value.Workarea, wizard.cmbCurrency, "DEFAULT_LOOKUPCURRENCY");
            wizard.ViewCurrency.CustomUnboundColumnData +=
                delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
                    {
                        UIHelper.DisplayCurrencyImagesLookupGrid(e, currencyBinding);
                    };
            wizard.cmbCurrency.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
            {
                SearchLookUpEdit cmb = sender as SearchLookUpEdit;
                //if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                //    cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
                try
                {
                    wizard.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                    if (currencyBinding.Count < 2)
                    {
                        collCurrency = value.Workarea.GetCollection<Currency>().Where(f => (f.IsStateAllow)).ToList();
                        currencyBinding.DataSource = collCurrency;
                    }


                }
                catch (Exception)
                {
                }
                finally
                {
                    wizard.Cursor = Cursors.Default;
                }
            };
            #endregion
            
            wizard.welcomeWizardPage1.PageValidating +=
                delegate(object sender, DevExpress.XtraWizard.WizardPageValidatingEventArgs e)
                {
                    if (string.IsNullOrEmpty(wizard.txtName.Text))
                    {
                        if (e.Direction == DevExpress.XtraWizard.Direction.Forward)
                            e.Valid = false;
                    }
                };
            wizard.wizardControl.FinishClick += delegate(object sender, System.ComponentModel.CancelEventArgs ef)
            {
                value.Name = wizard.txtName.Text;
                value.Memo = wizard.txtMemo.Text;
                value.Code = wizard.txtCode.Text;

                value.NameFull = wizard.txtNameFull.Text;
                value.StateId = State.STATEACTIVE;

                value.BankId = (int)wizard.cmbBank.EditValue;
                value.CurrencyId = (int)wizard.cmbCurrency.EditValue;
                try
                {
                    value.Save();
                }
                catch (DatabaseException dbe)
                {
                    ShowMessageDatabaseExeption(value.Workarea,
                        value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                    ef.Cancel = true;
                }
                catch (Exception ex)
                {
                    ShowMessagesExeption(value.Workarea,
                                                    value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                    value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049),
                                                    ex);
                    ef.Cancel = true;
                }

            };
            if (!value.IsNew && value.IsReadOnly)
            {
                foreach (Control ctl in wizard.wizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.welcomeWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.completionWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                wizard.wizardControl.CustomizeCommandButtons +=
                    delegate(object sender, DevExpress.XtraWizard.CustomizeCommandButtonsEventArgs e)
                    {
                        e.FinishButton.Visible = false;
                    };
            }
            wizard.ShowDialog();
            return value;
        }
    }
}
