using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlProcedureMap : BasePropertyControlICore<ProcedureMap>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlProcedureMap()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.Method = _common.txtCode.Text;
            SelectedItem.Schema = _common.cmbSchema.Text;
            SelectedItem.Procedure = _common.cmbProcedure.Text;
            // TODO: 
            //SelectedItem.Memo = _common.txtMemo.Text;

            InternalSave();
        }

        ControlEntityMethod _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlEntityMethod
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtCode = {Text = SelectedItem.Method},
                                  cmbSchema = {Text = SelectedItem.Schema},
                                  cmbProcedure = {Text = SelectedItem.Procedure},
                                  Workarea = SelectedItem.Workarea
                              };
                
                _common.cmbSchema.Properties.Items.AddRange(SelectedItem.Workarea.GetCollectionSchema());
                _common.cmbSchema.Leave += delegate
                                               {
                                                   _common.cmbProcedure.Properties.Items.Clear();
                                                   _common.cmbProcedure.Properties.Items.AddRange(SelectedItem.Workarea.GetCollectionProcedures(_common.cmbSchema.Text, false));
                                               };
                _common.cmbProcedure.Properties.Items.AddRange(SelectedItem.Workarea.GetCollectionProcedures(_common.cmbSchema.Text, false));
                
                // TODO: 
                //_common.txtMemo.Text = SelectedItem.Memo;

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