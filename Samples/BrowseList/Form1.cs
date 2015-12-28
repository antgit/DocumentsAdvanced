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

        private void btnShowListAgent_Click(object sender, EventArgs e)
        {
            Uid user = null;
            Workarea wa = Helper.OpenDataBase(out user);
            if (wa != null)
            {
                // пустой объект
                Agent obj = wa.Empty<Agent>();
                obj.BrowseList();
            }
        }

        private void btnShowListProduct_Click(object sender, EventArgs e)
        {
            Uid user = null;
            Workarea wa = Helper.OpenDataBase(out user);
            if (wa != null)
            {
                // пустой объект
                Product obj = wa.Empty<Product>();
                obj.BrowseList();
            }
        }

        private void btnShowListGroupProduct_Click(object sender, EventArgs e)
        {
            Uid user = null;
            Workarea wa = Helper.OpenDataBase(out user);
            if (wa != null)
            {
                // универсальный объект для представления списков
                TreeListBrowser<Product> browser = new TreeListBrowser<Product> { Workarea = wa };
                browser.ShowDialog();
            }
        }

        private void btnShowListGroupAgent_Click(object sender, EventArgs e)
        {
            Uid user = null;
            Workarea wa = Helper.OpenDataBase(out user);
            if (wa != null)
            {
                // универсальный объект для представления списков
                TreeListBrowser<Agent> browser = new TreeListBrowser<Agent> {Workarea = wa};
                browser.ShowDialog();
            }
        }
    }
}
