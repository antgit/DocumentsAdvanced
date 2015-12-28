using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BusinessObjects.Windows.Controls
{
    internal sealed partial class ControlSystemParameter : BusinessObjects.Windows.Controls.ControlCommon
    {
        public ControlSystemParameter()
        {
            InitializeComponent();
        }
 // TODO: 
        /*
         private void spinEdit1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
    if(e.Control && (e.KeyCode == Keys.Delete)) {
        spinEdit1.EditValue = null;
        spinEdit1.IsModified = false;
        e.Handled = true;
    }
}
         */
    }
}
