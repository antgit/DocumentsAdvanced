using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BusinessObjects.Windows.Controls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств паспорта
    /// </summary>
    internal class BuildControlPassport : BasePropertyControlICore<Passport>
    {
// ReSharper disable InconsistentNaming
        public const int PAGESMAX = 1 + 2 + 32;
// ReSharper restore InconsistentNaming

        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlPassport()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, 1);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, 32);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, 2);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            if (_common != null)
            {
                SelectedItem.Male = _common.cmbMale.SelectedIndex == 1 ? true : false;
                SelectedItem.SeriesNo = _common.txtSeriesNo.Text;
                SelectedItem.Number = _common.txtNumber.Text;
                SelectedItem.FirstName = _common.txtFirstName.Text;
                SelectedItem.MidleName = _common.txtMidleName.Text;
                SelectedItem.LastName = _common.txtLastName.Text;
                SelectedItem.Birthday = _common.dtBirthday.DateTime;
                SelectedItem.BirthTown = _common.txtBirthTown.Text;
                SelectedItem.Whogives = _common.txtWhogives.Text;
                MemoryStream ms;
                if (_common.imgSignature.Image != null)
                {
                    ms = new MemoryStream();
                    _common.imgSignature.Image.Save(ms, ImageFormat.Png);
                    SelectedItem.Signature = ms.ToArray();
                }
                if (_common.imgSignatureOfficial.Image != null)
                {
                    ms = new MemoryStream();
                    _common.imgSignatureOfficial.Image.Save(ms, ImageFormat.Png);
                    SelectedItem.SignatureOfficial = ms.ToArray();
                }
            }

            SaveStateData();
            try
            {
                CanClose = true;
                if (SelectedItem.ValidateRuleSet())
                    SelectedItem.Save();
                else
                    SelectedItem.ShowDialogValidationErrors();
            }
            catch (DatabaseException dbe)
            {
                CanClose = false;
                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
            }
            catch (Exception ex)
            {
                CanClose = false;
                XtraMessageBox.Show(SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049)
                    + Environment.NewLine + ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ControlAgentPassport _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlAgentPassport
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  cmbMale = {SelectedIndex = SelectedItem.Male ? 1 : 0},
                                  txtSeriesNo = {Text = SelectedItem.SeriesNo},
                                  txtNumber = {Text = SelectedItem.Number},
                                  txtFirstName = {Text = SelectedItem.FirstName},
                                  txtMidleName = {Text = SelectedItem.MidleName},
                                  txtLastName = {Text = SelectedItem.LastName},
                                  dtBirthday = {DateTime = SelectedItem.Birthday},
                                  txtBirthTown = {Text = SelectedItem.BirthTown},
                                  txtWhogives = {Text = SelectedItem.Whogives}
                              };

                if (SelectedItem.Signature != null)
                { 
                    MemoryStream ms = new MemoryStream(SelectedItem.Signature);
                    _common.imgSignature.Image = Image.FromStream(ms);
                }
                if (SelectedItem.SignatureOfficial != null)
                {
                    MemoryStream ms = new MemoryStream(SelectedItem.SignatureOfficial);
                    _common.imgSignatureOfficial.Image = Image.FromStream(ms);
                }

                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}
