using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств паспорта
    /// </summary>
    internal sealed class BuildControlPassport : BasePropertyControlICore<Passport>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlPassport()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
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
            InternalSave();
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
                                  txtWhogives = {Text = SelectedItem.Whogives},
                                  Workarea = SelectedItem.Workarea
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
                UIHelper.GenerateTooltips<Passport>(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                if (!SelectedItem.IsNew && SelectedItem.IsReadOnly)
                {
                    _common.Enabled = false;
                }
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}
