using System;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    public partial class FormProperties : DevExpress.XtraBars.Ribbon.RibbonForm, IWorkareaForm
    {
        public FormProperties()
        {
            InitializeComponent();
            Ribbon.ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.PROPERTIES_X16);
            btnClose.Glyph = ResourceImage.GetSystemImage(ResourceImage.EXIT_X32);
            btnSave.Glyph = ResourceImage.GetSystemImage(ResourceImage.SAVE_X32); 
            // TODO: Определять в коде
            //btnSelect.Glyph = BusinessObjects.Windows.Properties.Resources.SELECT_X32;
            btnSaveClose.Glyph = ResourceImage.GetSystemImage(ResourceImage.SAVECLOSE_X32);
            btnCreate.Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32);
            btnProp.Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32);
            btnPrint.Glyph = ResourceImage.GetSystemImage(ResourceImage.PRINT_X32);
            // TODO: опреелять в коде вызова
            //btnDelete.Glyph = BusinessObjects.Windows.Properties.Resources.DELETE_X32;
            btnRefresh.Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32);
            Load += FormPropertiesLoad;
        }

        void FormPropertiesLoad(object sender, EventArgs e)
        {
            if(Workarea!=null)
            {
                btnPrint.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.PRINT_X32);
                btnActions.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ACTION_X32);
            }
        }
        public string Key { get; set; }
        public string KeyName { get; set; }
        public Workarea Workarea { get; set; }
        // TODO: просто интересный метод установки protected свойств с использованием Reflection - вынести в какой нубуть класс утилит...
        // SetResizeRedraw(this);
        public static void SetResizeRedraw(Control control)
        {
            typeof(Control).InvokeMember("ResizeRedraw",
              BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
              null, control, new object[] { true });
        }
        public static void SetDoubleBuffer(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
              BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
              null, control, new object[] { true });
        }

        private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ribbon_ApplicationButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSaveClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}