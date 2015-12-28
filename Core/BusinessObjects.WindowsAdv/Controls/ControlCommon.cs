using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows.Controls
{
    internal partial class ControlCommon : BusinessObjects.Windows.Controls.ControlBase
    {
        public ControlCommon()
        {
            InitializeComponent();
            Tag = "Главная";
        }
    }
}
