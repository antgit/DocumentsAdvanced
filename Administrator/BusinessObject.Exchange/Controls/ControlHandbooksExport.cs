using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects.Windows;

namespace BusinessObjects.Exchange.Controls
{
    internal sealed partial class ControlHandbooksExport : DevExpress.XtraEditors.XtraUserControl
    {
        private Workarea WA { get; set; }

        public List<int> SelectedProducstId,SelectedAgenstId;
       
        public ControlHandbooksExport(Workarea workarea)
        {
            InitializeComponent();
            WA = workarea;
            SelectedProducstId=new List<int>();
        }

        private void simpleButtonProducts_Click(object sender, EventArgs e)
        {
            TreeListBrowser<Product> browseDialog = new TreeListBrowser<Product> { Workarea = WA }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null)||(browseDialog.DialogResult!=DialogResult.OK)) return;
            SelectedProducstId.Clear();
            foreach (Product p in browseDialog.ListBrowserBaseObjects.SelectedValues)
            {
                SelectedProducstId.Add(p.Id);
            }
        }

        private void simpleButtonAgents_Click(object sender, EventArgs e)
        {
            TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = WA }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            SelectedAgenstId.Clear();
            foreach (Agent a in browseDialog.ListBrowserBaseObjects.SelectedValues)
            {
                SelectedProducstId.Add(a.Id);
            }
        }

        private void checkEditProducts_CheckedChanged(object sender, EventArgs e)
        {
            simpleButtonProducts.Enabled = checkEditProducts.Checked;
        }

        private void checkEditAgents_CheckedChanged(object sender, EventArgs e)
        {
            simpleButtonAgents.Enabled = checkEditAgents.Checked;
        }
    }
}
