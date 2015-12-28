using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    internal sealed partial class FormDataBaseConnection : BusinessObjects.Windows.FormProperties
    {
        public FormDataBaseConnection()
        {
            InitializeComponent();
            this.ribbon.ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.DATABASE_X16); 
        }
    }
}
