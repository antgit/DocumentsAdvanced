using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BusinessObjects.Windows;
namespace BusinessObjects.Windows.Controls
{
    internal sealed partial class ControlList : DevExpress.XtraEditors.XtraUserControl
    {
        public ControlList()
        {
            InitializeComponent();
            Grid.Name = "Grid";
        }
    }
}
