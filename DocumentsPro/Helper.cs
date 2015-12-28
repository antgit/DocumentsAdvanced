using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects;
using BusinessObjects.Security;
using BusinessObjects.Windows;

namespace Documents2012
{
    internal sealed class Helper
    {
        #region Диалог открытия базы данных
        private static void FillConfigConnection(List<DataInfo> coll)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationManager.RefreshSection("connectionStrings");
            if (coll == null)
                coll = new List<DataInfo>();
            else
                coll.Clear();
            ConnectionStringsSection csSection = config.ConnectionStrings;

            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                ConnectionStringSettings cs = csSection.ConnectionStrings[i];
                if (cs.Name != "LocalSqlServer")
                {
                    DataInfo info = new DataInfo
                    {
                        Name = cs.Name,
                        Provider = cs.ProviderName,
                        ConnectionString = cs.ConnectionString
                    };
                    coll.Add(info);
                }
            }
        }
        internal static DataInfo HelpDataBaseListMethod(out Uid userInfo)
        {
            FormDataBaseList frm = new FormDataBaseList();
            frm.ShowInTaskbar = true;
            frm.ribbon.Minimized = true;
            //frm.Ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection csSection = config.ConnectionStrings;
            List<DataInfo> coll = new List<DataInfo>();
            DataInfo info = null;
            FillConfigConnection(coll);
            BindingSource collectionBindSource = new BindingSource { DataSource = coll };
            frm.Source.DataSource = collectionBindSource;

            #region Удалить
            frm.btnDel.ItemClick += delegate
            {
                if (frm.Source.Current != null)
                {
                    if (MessageBox.Show(frm, "Удалить соединение?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;
                    info = (frm.Source.Current as DataInfo);
                    csSection.ConnectionStrings.Remove(info.Name);
                    config.Save();
                    FillConfigConnection(coll);
                    frm.Source.DataSource = null;
                    frm.Source.DataSource = coll;
                }
            };
            #endregion

            #region Изменить

            frm.btnProp.ItemClick += delegate
            {
                if (frm.Source.Current != null)
                {
                    FormDataBaseConnection frmEdit = new FormDataBaseConnection();
                    info = (frm.Source.Current as DataInfo);

                    frmEdit.controlSqlServerConnection.ConnectionString = info.ConnectionString;
                    frmEdit.controlSqlServerConnection.txtName.Text = info.Name;
                    frmEdit.controlSqlServerConnection.txtName.Properties.ReadOnly = true;

                    DataInfo findItem = coll.Find(item => item.Name == info.Name);

                    findItem.ConnectionString = frmEdit.controlSqlServerConnection.ConnectionString;

                    if (frmEdit.ShowDialog() == DialogResult.OK)
                    {
                        csSection.ConnectionStrings[info.Name].ConnectionString = frmEdit.controlSqlServerConnection.ConnectionString;

                        config.Save();
                        FillConfigConnection(coll);
                        frm.Source.DataSource = null;
                        frm.Source.DataSource = coll;
                    }
                }
            };

            #endregion

            #region Новая
            frm.btnNew.ItemClick += delegate
            {
                FormDataBaseConnection frmEdit = new FormDataBaseConnection();
                frmEdit.controlSqlServerConnection.txtName.Text = "Новое соединение";
                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    ConnectionStringSettings cs = new ConnectionStringSettings();
                    info = new DataInfo();

                    if (!string.IsNullOrEmpty(frmEdit.controlSqlServerConnection.txtName.Text))
                        cs.Name = frmEdit.controlSqlServerConnection.txtName.Text;
                    cs.ProviderName = "System.Data.SqlClient";
                    cs.ConnectionString = frmEdit.controlSqlServerConnection.ConnectionString;

                    info.Name = cs.Name;
                    info.Provider = cs.ProviderName;
                    info.ConnectionString = cs.ConnectionString;

                    csSection.ConnectionStrings.Add(cs);
                    coll.Add(info);

                    config.Save();
                    FillConfigConnection(coll);
                    frm.Source.DataSource = null;
                    frm.Source.DataSource = coll;
                }
            };

            #endregion

            #region Горячие клавиши
            frm.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
                                    {
                                        if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
                                        {
                                            if (frm.Source.Current != null)
                                            {
                                                info = frm.Source.Current as DataInfo;
                                                frm.DialogResult = DialogResult.OK;
                                                frm.Close();
                                            }
                                        }
                                    }; 
            #endregion
            #region Логин
            frm.controlLogin.chkIntegrated.CheckedChanged += delegate
            {
                if (frm.controlLogin.chkIntegrated.Checked)
                {
                    frm.controlLogin.txtUserId.Enabled = false;
                    frm.controlLogin.txtPsw.Enabled = false;
                    frm.controlLogin.txtUserId.Text = Environment.UserName;
                    //common.txtPsw.PromptText = string.Empty;
                }
                else
                {
                    frm.controlLogin.txtUserId.Enabled = true;
                    frm.controlLogin.txtPsw.Enabled = true;
                    //common.txtPsw.PromptText = Properties.Resources.PROMT_PASSWORD;
                }
            };
            frm.controlLogin.txtUserId.Validating += delegate
            {
                try
                {
                    if (!frm.controlLogin.chkIntegrated.Checked && frm.controlLogin.txtUserId.Text.Length != 0)
                    {
                        //common.errorProvider.SetError(common.txtUserId, string.Empty);
                        //common.tableLayoutPanel.ColumnStyles[common.tableLayoutPanel.ColumnCount - 1].Width = 0;
                    }
                    else
                        throw new ArgumentOutOfRangeException(frm.controlLogin.lbUsername.Text, frm.controlLogin.txtUserId.Text, "Наименование является обязательным.");
                }
                catch (Exception ex)
                {
                    //common.tableLayoutPanel.ColumnStyles[common.tableLayoutPanel.ColumnCount - 1].Width = common.errorProvider.Icon.Width + 2;
                    //common.errorProvider.SetError(common.txtUserId, ex.Message);
                }
            };
            frm.controlLogin.pictureEdit1.Image = ResourceImage.GetSystemImage(ResourceImage.KEYS_X32);

            Uid user = new Uid();
            EventHandler myDelegate = new EventHandler(delegate
                                       {
                                           info = frm.Source.Current as DataInfo;
                                           if (info != null)
                                           {
                                               SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(info.ConnectionString);
                                               if (!builder.IntegratedSecurity)
                                               {
                                                   user = new Uid { Name = builder.UserID, Password = builder.Password };
                                                   if (builder.IntegratedSecurity)
                                                       user.AuthenticateKind = 1;
                                                   else
                                                       user.AuthenticateKind = 0;

                                               }
                                               else
                                                   user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };

                                               frm.controlLogin.txtUserId.Text = user.Name;
                                               frm.controlLogin.txtPsw.Text = user.Password;
                                               frm.controlLogin.chkIntegrated.Checked = (user.AuthenticateKind == 1);
                                           }
                                       });
            frm.Source.PositionChanged += myDelegate;
                //delegate
                //                             {
                //                                 info = frm.Source.Current as DataInfo;
                //                                 if (info != null)
                //                                 {
                //                                     SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(info.ConnectionString);
                //                                     if (!builder.IntegratedSecurity)
                //                                     {
                //                                         user = new Uid {Name = builder.UserID, Password = builder.Password};
                //                                         if (builder.IntegratedSecurity)
                //                                             user.AuthenticateKind = 1;
                //                                         else
                //                                             user.AuthenticateKind = 0;

                //                                     }
                //                                     else
                //                                         user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };

                //                                     frm.controlLogin.txtUserId.Text = user.Name;
                //                                     frm.controlLogin.txtPsw.Text = user.Password;
                //                                     frm.controlLogin.chkIntegrated.Checked =  (user.AuthenticateKind == 1);
                //                                 }
                //                             };
            #endregion

            myDelegate.Invoke(null, null);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.Source.Current != null)
                {
                    info = frm.Source.Current as DataInfo;

                }
            }
            else
            {
                info = null;
            }
            userInfo = user;
            return info;
        }
        #endregion
    }
}
