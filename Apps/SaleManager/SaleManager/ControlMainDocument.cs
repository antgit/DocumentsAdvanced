using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BusinessObjects;
using BusinessObjects.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace SaleManager
{
    public partial class ControlMainDocument : DevExpress.XtraEditors.XtraUserControl
    {
        public ControlMainDocument()
        {
            InitializeComponent();
            CollectionAgentFrom = new List<Agent>();
            CollectionAgentTo = new List<Agent>();
            CollectionSuperviser = new List<Agent>();
            CollectionTrader = new List<Agent>();
            CollectionPrice = new List<PriceName>();
            CollectionBankFrom = new List<AgentBankAccount>();
            CollectionBankTo = new List<AgentBankAccount>();

            bindAgentFrom.DataSource = CollectionAgentFrom;
            bindAgentTo.DataSource = CollectionAgentTo;
            bindBankTo.DataSource = CollectionBankTo;
            bindBankFrom.DataSource = CollectionBankFrom;
            bindPrice.DataSource = CollectionPrice;
            bindSuperviser.DataSource = CollectionSuperviser;
            bindTrader.DataSource = CollectionTrader;

            cmbFrom.Properties.DisplayMember = GlobalPropertyNames.Name;
            cmbFrom.Properties.ValueMember = GlobalPropertyNames.Id;

            cmbTo.Properties.DisplayMember = GlobalPropertyNames.Name;
            cmbTo.Properties.ValueMember = GlobalPropertyNames.Id;

            cmbPrice.Properties.DisplayMember = GlobalPropertyNames.Name;
            cmbPrice.Properties.ValueMember = GlobalPropertyNames.Id;

            cmbSuperviser.Properties.DisplayMember = GlobalPropertyNames.Name;
            cmbSuperviser.Properties.ValueMember = GlobalPropertyNames.Id;

            cmbTrader.Properties.DisplayMember = GlobalPropertyNames.Name;
            cmbTrader.Properties.ValueMember = GlobalPropertyNames.Id;

            cmbBankTo.Properties.DisplayMember = GlobalPropertyNames.Name;
            cmbBankTo.Properties.ValueMember = GlobalPropertyNames.Id;
            
            cmbBankFrom.Properties.DisplayMember = GlobalPropertyNames.Name;
            cmbBankFrom.Properties.ValueMember = GlobalPropertyNames.Id;


            ViewFrom.CustomUnboundColumnData += ViewAgentFromCustomUnboundColumnData;
            ViewTo.CustomUnboundColumnData += ViewAgentToCustomUnboundColumnData;
            ViewSuperviser.CustomUnboundColumnData += ViewSuperviserCustomUnboundColumnData;
            ViewTrader.CustomUnboundColumnData += ViewTraderCustomUnboundColumnData;

            ViewBankFrom.CustomUnboundColumnData += ViewBankFromCustomUnboundColumnData;
            ViewBankTo.CustomUnboundColumnData += ViewBankToCustomUnboundColumnData;
            editNom.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;
            editName.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;
        }

        public List<Agent> CollectionAgentFrom { get; set; }
        public List<Agent> CollectionAgentTo { get; set; }
        public List<Agent> CollectionSuperviser { get; set; }
        public List<Agent> CollectionTrader { get; set; }
        public List<PriceName> CollectionPrice { get; set; }
        public List<AgentBankAccount> CollectionBankFrom { get; set; }
        public List<AgentBankAccount> CollectionBankTo { get; set; }


        void ViewCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindProduct.Count > 0)
            {
                Product imageItem = bindProduct[e.ListSourceRowIndex] as Product;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindProduct.Count > 0)
            {
                Product imageItem = bindProduct[e.ListSourceRowIndex] as Product;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        void ViewAgentFromCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, this.bindAgentFrom);
        }
        void ViewAgentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, this.bindAgentTo);
        }
        void ViewSuperviserCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, this.bindSuperviser);
        }
        void ViewTraderCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, this.bindTrader);
        }

        void ViewBankToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayBankAccountImagesLookupGrid(e, bindBankTo);
        }
        void ViewBankFromCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayBankAccountImagesLookupGrid(e, bindBankFrom);
        }
        /// <summary>Отображение иконок для списков корреспондентов</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceAgents"></param>
        internal static void DisplayAgentImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAgents)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Agent imageItem = bindSourceAgents[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Agent imageItem = bindSourceAgents[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        /// <summary>Отображение иконок для списков расчетных счетов</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceBank"></param>
        internal static void DisplayBankAccountImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceBank)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceBank.Count > 0)
            {
                AgentBankAccount imageItem = bindSourceBank[e.ListSourceRowIndex] as AgentBankAccount;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceBank.Count > 0)
            {
                AgentBankAccount imageItem = bindSourceBank[e.ListSourceRowIndex] as AgentBankAccount;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
    }


}
