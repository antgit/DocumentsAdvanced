using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Workflow.Activities.Rules;
using BusinessObjects.Windows;
using System.Linq;
using System.ComponentModel;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        public static void ShowDialogValidationErrors<T>(this T value, Dictionary<string, string> errors=null) where T : class, ICoreObject, new() 
        {
            FormProperties frm = new FormProperties();
            frm.Ribbon.ApplicationIcon = ResourceImage.GetByCode(value.Workarea, ResourceImage.ERROR_X16);
            frm.ShowInTaskbar = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            ControlList ctl = new ControlList();
            frm.clientPanel.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            DataGridViewHelper.GenerateGridColumns(value.Workarea, ctl.View, "DEFAULT_LISTVIEWDICTIONARY");

            BindingSource sourceErrors = new BindingSource();
            if (errors == null)
                sourceErrors.DataSource = value.Errors;
            else
                sourceErrors.DataSource = errors;
            // {DataSource = value.Errors};
            ctl.Grid.DataSource = sourceErrors;
            ctl.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                                                    {
                                                        if (e.Column.FieldName == "Image" && e.IsGetData)
                                                        {
                                                            e.Value = ResourceImage.GetByCode(value.Workarea, ResourceImage.ERROR_X16);
                                                        }
                                                    };
            frm.ShowDialog();
        }


    }
}
