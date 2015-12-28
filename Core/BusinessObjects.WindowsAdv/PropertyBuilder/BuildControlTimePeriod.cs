using System;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств объекта "Графики работ"
    /// </summary>
    internal sealed class BuildControlTimePeriod : BasePropertyControlIBase<TimePeriod>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlTimePeriod()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;

            SelectedItem.MondayW = _common.controlTimePeriodValues.chkMonday.Checked ? 1 : 0;
            SelectedItem.TuesdayW = _common.controlTimePeriodValues.chkTuesday.Checked ? 1 : 0;
            SelectedItem.WednesdayW = _common.controlTimePeriodValues.chkWednesday.Checked ? 1 : 0;
            SelectedItem.ThursdayW = _common.controlTimePeriodValues.chkThursday.Checked ? 1 : 0;
            SelectedItem.FridayW = _common.controlTimePeriodValues.chkFriday.Checked ? 1 : 0;
            SelectedItem.SaturdayW = _common.controlTimePeriodValues.chkSaturday.Checked ? 1 : 0;
            SelectedItem.SundayW = _common.controlTimePeriodValues.chkSunday.Checked ? 1 : 0;

            SelectedItem.MondaySH = _common.controlTimePeriodValues.TimeMondayS.Time.Hour;
            SelectedItem.MondaySM = _common.controlTimePeriodValues.TimeMondayS.Time.Minute;
            SelectedItem.TuesdaySH = _common.controlTimePeriodValues.TimeTuesdayS.Time.Hour;
            SelectedItem.TuesdaySM = _common.controlTimePeriodValues.TimeTuesdayS.Time.Minute;
            SelectedItem.WednesdaySH = _common.controlTimePeriodValues.TimeWednesdayS.Time.Hour;
            SelectedItem.WednesdaySM = _common.controlTimePeriodValues.TimeWednesdayS.Time.Minute;
            SelectedItem.ThursdaySH = _common.controlTimePeriodValues.TimeThursdayS.Time.Hour;
            SelectedItem.ThursdaySM = _common.controlTimePeriodValues.TimeThursdayS.Time.Minute;
            SelectedItem.FridaySH = _common.controlTimePeriodValues.TimeFridayS.Time.Hour;
            SelectedItem.FridaySM = _common.controlTimePeriodValues.TimeFridayS.Time.Minute;
            SelectedItem.SaturdaySH = _common.controlTimePeriodValues.TimeSaturdayS.Time.Hour;
            SelectedItem.SaturdaySM = _common.controlTimePeriodValues.TimeSaturdayS.Time.Minute;
            SelectedItem.SundaySH = _common.controlTimePeriodValues.TimeSundayS.Time.Hour;
            SelectedItem.SundaySM = _common.controlTimePeriodValues.TimeSundayS.Time.Minute;


            SelectedItem.MondayEH = _common.controlTimePeriodValues.TimeMondayE.Time.Hour;
            SelectedItem.MondayEM = _common.controlTimePeriodValues.TimeMondayE.Time.Minute;
            SelectedItem.TuesdayEH = _common.controlTimePeriodValues.TimeTuesdayE.Time.Hour;
            SelectedItem.TuesdayEM = _common.controlTimePeriodValues.TimeTuesdayE.Time.Minute;
            SelectedItem.WednesdayEH = _common.controlTimePeriodValues.TimeWednesdayE.Time.Hour;
            SelectedItem.WednesdayEM = _common.controlTimePeriodValues.TimeWednesdayE.Time.Minute;
            SelectedItem.ThursdayEH = _common.controlTimePeriodValues.TimeThursdayE.Time.Hour;
            SelectedItem.ThursdayEM = _common.controlTimePeriodValues.TimeThursdayE.Time.Minute;
            SelectedItem.FridayEH = _common.controlTimePeriodValues.TimeFridayE.Time.Hour;
            SelectedItem.FridayEM = _common.controlTimePeriodValues.TimeFridayE.Time.Minute;
            SelectedItem.SaturdayEH = _common.controlTimePeriodValues.TimeSaturdayE.Time.Hour;
            SelectedItem.SaturdayEM = _common.controlTimePeriodValues.TimeSaturdayE.Time.Minute;
            SelectedItem.SundayEH = _common.controlTimePeriodValues.TimeSundayE.Time.Hour;
            SelectedItem.SundayEM = _common.controlTimePeriodValues.TimeSundayE.Time.Minute;

            if (string.IsNullOrEmpty(SelectedItem.CodeFind))
            {
                SelectedItem.CodeFind = (SelectedItem.Name + Transliteration.Front(SelectedItem.Name)).Replace(" ", "");
            }

            SaveStateData();

            InternalSave();
        }

        ControlTimePeriod _common;
        private DateTime ConvertFromInt(int hour, int minute)
        {
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hour, minute, 0);
        }
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlTimePeriod
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  Workarea = SelectedItem.Workarea
                              };

                _common.controlTimePeriodValues.chkMonday.Checked = (SelectedItem.MondayW == 1);
                _common.controlTimePeriodValues.chkTuesday.Checked = (SelectedItem.TuesdayW == 1);
                _common.controlTimePeriodValues.chkWednesday.Checked = (SelectedItem.WednesdayW == 1);
                _common.controlTimePeriodValues.chkThursday.Checked = (SelectedItem.ThursdayW == 1);
                _common.controlTimePeriodValues.chkFriday.Checked = (SelectedItem.FridayW == 1);
                _common.controlTimePeriodValues.chkSaturday.Checked = (SelectedItem.SaturdayW == 1);
                _common.controlTimePeriodValues.chkSunday.Checked = (SelectedItem.SundayW == 1);

                _common.controlTimePeriodValues.TimeMondayS.EditValue = ConvertFromInt(SelectedItem.MondaySH, SelectedItem.MondaySM);
                _common.controlTimePeriodValues.TimeTuesdayS.EditValue = ConvertFromInt(SelectedItem.TuesdaySH, SelectedItem.TuesdaySM);
                _common.controlTimePeriodValues.TimeWednesdayS.EditValue = ConvertFromInt(SelectedItem.WednesdaySH, SelectedItem.WednesdaySM);
                _common.controlTimePeriodValues.TimeThursdayS.EditValue = ConvertFromInt(SelectedItem.ThursdaySH, SelectedItem.ThursdaySM);
                _common.controlTimePeriodValues.TimeFridayS.EditValue = ConvertFromInt(SelectedItem.FridaySH, SelectedItem.FridaySM);
                _common.controlTimePeriodValues.TimeSaturdayS.EditValue = ConvertFromInt(SelectedItem.SaturdaySH, SelectedItem.SaturdaySM);
                _common.controlTimePeriodValues.TimeSundayS.EditValue = ConvertFromInt(SelectedItem.SundaySH, SelectedItem.SundaySM);

                _common.controlTimePeriodValues.TimeMondayE.EditValue = ConvertFromInt(SelectedItem.MondayEH, SelectedItem.MondayEM);
                _common.controlTimePeriodValues.TimeTuesdayE.EditValue = ConvertFromInt(SelectedItem.TuesdayEH, SelectedItem.TuesdayEM);
                _common.controlTimePeriodValues.TimeWednesdayE.EditValue = ConvertFromInt(SelectedItem.WednesdayEH, SelectedItem.WednesdayEM);
                _common.controlTimePeriodValues.TimeThursdayE.EditValue = ConvertFromInt(SelectedItem.ThursdayEH, SelectedItem.ThursdayEM);
                _common.controlTimePeriodValues.TimeFridayE.EditValue = ConvertFromInt(SelectedItem.FridayEH, SelectedItem.FridayEM);
                _common.controlTimePeriodValues.TimeSaturdayE.EditValue = ConvertFromInt(SelectedItem.SaturdayEH, SelectedItem.SaturdayEM);
                _common.controlTimePeriodValues.TimeSundayE.EditValue = ConvertFromInt(SelectedItem.SundayEH, SelectedItem.SundayEM);

                UIHelper.GenerateTooltips(SelectedItem, _common);
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