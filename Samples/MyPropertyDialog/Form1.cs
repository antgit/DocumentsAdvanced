using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects.Security;
using BusinessObjects.Windows;

namespace BusinessObjects.Samples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnShowProperty_Click(object sender, EventArgs e)
        {
            // Подключаемся к базе данных...
            Uid user = null;
            Workarea wa = Helper.OpenDataBase(out user);
            if (wa != null)
            {
                // число 5 - это идентификатор корреспондента, возможно в Вашей базе его нет - установите правильное значение
                Agent obj = wa.GetObject<Agent>(5);

                FormProperty frm = new FormProperty();
                frm.SelectedObject = obj;
                frm.ShowDialog();
            }
        }
    }
}
