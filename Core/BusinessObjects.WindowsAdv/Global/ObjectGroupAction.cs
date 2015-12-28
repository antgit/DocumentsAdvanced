using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    internal class GroupActionObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public bool Status { get; set; }
    }
    /// <summary>
    /// Класс внутреннего использования для выполнения действий над несколькими объектами 
    /// и отображения статуса выполнения, например удаления, печати
    /// </summary>
    internal class ObjectGroupAction<T> where T : class, ICoreObject
    {
        private BackgroundWorker worker;
        private ControlList ctl;
        public ObjectGroupAction()
        {
            frm = new FormProperties();
            
            frm.ShowInTaskbar = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            ctl = new ControlList();
            frm.clientPanel.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;


            frm.btnRun.Visibility = BarItemVisibility.Always;
            
            frm.btnRun.ItemClick += BtnSelectItemClick;
            ctl.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;

            bindValues = new BindingSource();
            ctl.Grid.DataSource = bindValues;
            
            frm.Load += FrmLoad;
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsInProcess = false;
            frm.btnRun.Visibility = BarItemVisibility.Never;
        }

        public Action<T> Action { get; set; } 
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (GroupActionObject v in SourceCollection)
            {
                ICoreObject obj = SourceValues.Find(f => f.Id == v.Id);
                try
                {
                    Action.Invoke(obj as T);
                    v.Status = true;
                    v.Info = "Выполнено";
                }
                catch(Exception ex)
                {
                    v.Status = false;
                    v.Info = ex.Message;
                }
                
                
                frm.Invoke((MethodInvoker)delegate
                                              {
                                                  bindValues.Position = bindValues.IndexOf(v);
                                                  ctl.View.RefreshRow(ctl.View.FocusedRowHandle);
                                              });
            }
        }

        void BtnSelectItemClick(object sender, ItemClickEventArgs e)
        {
            IsInProcess = true;
            frm.btnSelect.Enabled = false;
            ctl.View.Columns["Image"].Visible = true;
            ctl.View.Columns["Image"].VisibleIndex = 0;
            //frm.colImage.Visible = true;
            worker.RunWorkerAsync();

        }

        void FrmLoad(object sender, EventArgs e)
        {
            frm.Ribbon.ApplicationIcon = ResourceImage.GetByCode(Workarea, ResourceImage.ERROR_X16);
            frm.btnRun.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TRIANGLEGREEN_X32);
            DataGridViewHelper.GenerateGridColumns(Workarea, ctl.View, "DEFAULT_LISTVIEWACTIONSTATUS");
            ctl.View.Columns["Image"].Visible = false;
        }
        public IWorkarea Workarea { get; set; }
        void ViewCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                GroupActionObject curValue = bindValues[e.ListSourceRowIndex] as GroupActionObject;
                if (curValue != null)
                {
                    if (curValue.Status)
                        e.Value = ResourceImage.GetByCode(Workarea, ResourceImage.OK_X16);
                    else
                        e.Value = ResourceImage.GetByCode(Workarea, ResourceImage.ERROR_X16);
                }
            }
        }
        private BindingSource bindValues;
        private FormProperties frm;
        /// <summary>
        /// Форма отображения процесса выполнения
        /// </summary>
        public Form Form { get { return frm; } }
        public bool IsInProcess { get; set; }
        private List<GroupActionObject> _sourceCollection;

        /// <summary>
        /// Коллекция над которой необходимо выполнить действие
        /// </summary>
        public List<GroupActionObject> SourceCollection
        {
            get
            {
                return _sourceCollection;
            }
            set
            {
                _sourceCollection = value;
                bindValues.DataSource = _sourceCollection;
            }
        }

        private List<T> SourceValues;
        public void FillFromCollection(List<T> values)
        {
            SourceValues = values;
            _sourceCollection = new List<GroupActionObject>();
            foreach (T v in values)
            {
                SourceCollection.Add(new GroupActionObject { Id = v.Id, Name = v.ToString() });
            }
            bindValues.DataSource = _sourceCollection;
        }

        /// <summary>
        /// Показать диалог выполнения действия
        /// </summary>
        public void ShowDialog()
        {
            frm.ShowDialog();
        }
    }
}