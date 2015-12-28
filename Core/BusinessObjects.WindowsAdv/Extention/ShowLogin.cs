using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        /// <summary>
        /// Показать окно входа в систему 
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="userPassword">Пароль</param>
        /// <param name="integratedSecurity">Использовать интегрированную аутентификацию</param>
        /// <returns></returns>
        public static Uid ShowLogin(string userName, string userPassword, bool integratedSecurity)
        {
            FormProperties frm = new FormProperties
                                     {
                                         StartPosition = FormStartPosition.CenterScreen,
                                         Icon = Icon.FromHandle(ResourceImage.GetSystemImage(ResourceImage.KEY_X16).GetHicon()),
                                         MaximizeBox = false,
                                         FormBorderStyle = FormBorderStyle.FixedDialog,
                                         MinimizeBox = false
                                     };

            ControlLogin common = new ControlLogin();
            frm.Text = common.lbHeader.Text;
            frm.btnSave.Caption = "Подтвердить";
            frm.MinimumSize = new Size(common.Width, common.Height);
            frm.Size = new Size(common.MinimumSize.Width + 100, common.MinimumSize.Height + frm.ribbon.Height + 90);
            frm.clientPanel.Controls.Add(common);
            common.Dock = DockStyle.Fill;
            common.chkIntegrated.CheckedChanged += delegate
            {
                if (common.chkIntegrated.Checked)
                {
                    common.txtUserId.Enabled = false;
                    common.txtPsw.Enabled = false;
                    common.txtUserId.Text = Environment.UserName;
                    //common.txtPsw.PromptText = string.Empty;
                }
                else
                {
                    common.txtUserId.Enabled = true;
                    common.txtPsw.Enabled = true;
                    //common.txtPsw.PromptText = Properties.Resources.PROMT_PASSWORD;
                }
            };
            common.txtUserId.Validating += delegate
            {
                try
                {
                    if (!common.chkIntegrated.Checked && common.txtUserId.Text.Length != 0)
                    {
                        //common.errorProvider.SetError(common.txtUserId, string.Empty);
                        //common.tableLayoutPanel.ColumnStyles[common.tableLayoutPanel.ColumnCount - 1].Width = 0;
                    }
                    else
                        throw new ArgumentOutOfRangeException(common.lbUsername.Text, common.txtUserId.Text, "Наименование является обязательным.");
                }
                catch (Exception ex)
                {
                    //common.tableLayoutPanel.ColumnStyles[common.tableLayoutPanel.ColumnCount - 1].Width = common.errorProvider.Icon.Width + 2;
                    //common.errorProvider.SetError(common.txtUserId, ex.Message);
                }
            };
            frm.FormClosing += delegate(object sender, FormClosingEventArgs e)
            {
                if (frm.DialogResult == DialogResult.OK &&
                    !common.chkIntegrated.Checked && common.txtUserId.Text.Length == 0)
                {
                    e.Cancel = true;
                }
            };
            common.pictureEdit1.Image = ResourceImage.GetSystemImage(ResourceImage.KEYS_X32);
            common.txtUserId.Text = userName;
            common.txtPsw.Text = userPassword;
            common.chkIntegrated.Checked = integratedSecurity;
            //frm.btnSave.Select();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Uid user = common.chkIntegrated.Checked ? new Uid { Name = Environment.UserName, Password = String.Empty, AuthenticateKind = 1 } : new Uid { Name = common.txtUserId.Text, Password = common.txtPsw.Text, AuthenticateKind = 0 };
                return user;
            }
            return null;
        }
    }
}
