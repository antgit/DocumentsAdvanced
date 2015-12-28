using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств интервала времени
    /// </summary>
    internal sealed class BuildControlDateRegion : BasePropertyControlIBase<DateRegion>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlDateRegion()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
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
            SelectedItem.DateStart = _common.dtDateStart.DateTime;
            SelectedItem.DateEnd = _common.dtDateEnd.DateTime;
            SaveStateData();

            InternalSave();
        }

        ControlDateRegion _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlDateRegion
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = {Text = SelectedItem.NameFull},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = {Text = SelectedItem.CodeFind},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  Workarea = SelectedItem.Workarea,
                                  dtDateStart = {DateTime = SelectedItem.DateStart},
                                  dtDateEnd = {DateTime = SelectedItem.DateEnd}
                              };
                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                if (!SelectedItem.IsNew && SelectedItem.IsReadOnly)
                {
                    _common.Enabled = false;
                }
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);

                //frmProp.btnActions.Visibility = BarItemVisibility.Always;
                //BarItemLink lnk = frmProp.ActionMenu.AddItem(new BarButtonItem() { Caption = "Пользовательские флаги" });
                //lnk.Item.ItemClick += delegate
                //                          {
                //                              SelectedItem.ShowFlagString();
                //                          };
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}