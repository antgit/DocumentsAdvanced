using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        // Рабочая область
        private Workarea WA;
        // Пользователь
        private Uid user;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenDataBase_Click(object sender, EventArgs e)
        {
            WA = Helper.OpenDataBase(out user);
            if(WA!=null)
                DevExpress.XtraEditors.XtraMessageBox.Show("Поздравляем, Вы подключилиь к базе!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
