using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace BusinessObjects.Windows.Controls
{
    internal partial class ControlGridViewOption : BusinessObjects.Windows.Controls.ControlBase
    {
        public ControlGridViewOption()
        {
            InitializeComponent();
            
        }
        public void FillOptions(GridView gridView)
        {
            GridView = gridView;
            InitOptions(GridView.OptionsView, checkedListBoxControl1);
            InitOptions(GridView.OptionsBehavior, checkedListBoxControl2);
        }
        public GridView GridView { get; set; }
        
        private void InitOptions(object options, DevExpress.XtraEditors.CheckedListBoxControl checkedListBox)
        {
            ArrayList arr = DevExpress.Utils.SetOptions.GetOptionNames(options);
            for (int i = 0; i < arr.Count; i++)
                checkedListBox.Items.Add(new DevExpress.XtraEditors.Controls.CheckedListBoxItem
                    (arr[i], DevExpress.Utils.SetOptions.OptionValueByString(arr[i].ToString(), options)));
        }

        //<checkedListBoxControl1>
        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            bool optionSet = e.State == CheckState.Checked ? true : false;
            string option = checkedListBoxControl1.GetDisplayItemValue(e.Index).ToString();
            DevExpress.Utils.SetOptions.SetOptionValueByString(option, GridView.OptionsView, optionSet);
        }
        //</checkedListBoxControl1>

        //<checkedListBoxControl2>
        private void checkedListBoxControl2_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            bool optionSet = e.State == CheckState.Checked ? true : false;
            string option = checkedListBoxControl2.GetDisplayItemValue(e.Index).ToString();
            DevExpress.Utils.SetOptions.SetOptionValueByString(option, GridView.OptionsBehavior, optionSet);
        }
    }
}
