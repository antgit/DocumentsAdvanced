using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BusinessObjects.Windows
{
    internal sealed partial class FormDataBaseList : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FormDataBaseList()
        {
            InitializeComponent();
            btnDel.Glyph = ResourceImage.GetSystemImage(ResourceImage.DELETE_X16);
            btnProp.Glyph = ResourceImage.GetSystemImage(ResourceImage.PROPERTIES_X16);
            btnNew.Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X16);
            this.ribbon.ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.DATABASE_X16);
            this.Icon = System.Drawing.Icon.FromHandle(ResourceImage.GetSystemImage(ResourceImage.DATABASE_X16).GetHicon());
            View.CustomUnboundColumnData += View_CustomUnboundColumnData;
            Grid.DefaultView.MouseDown += DefaultView_MouseDown;
        }
        void View_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                e.Value = ResourceImage.GetSystemImage(ResourceImage.DATABASE_X16);
            }
        }
        void DefaultView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                GridHitInfo hi = View.CalcHitInfo(new Point(e.X, e.Y));
                if (hi.HitTest == GridHitTest.RowCell)
                {
                    if(Source.Current!=null)
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }
    }
}