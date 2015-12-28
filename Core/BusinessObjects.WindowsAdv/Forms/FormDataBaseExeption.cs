using System.Drawing;

namespace BusinessObjects.Windows
{
    internal sealed partial class FormDataBaseExeption : FormProperties
    {
        public FormDataBaseExeption()
        {
            InitializeComponent();
            ribbon.ApplicationIcon = SystemIcons.Error.ToBitmap();
            Icon = SystemIcons.Error;
        }
    }
}
