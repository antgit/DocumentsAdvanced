using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessObjects.Samples
{
    public partial class FormProperty : Form
    {
        public FormProperty()
        {
            InitializeComponent();
        }

        private Agent _selectedObject;

        public Agent SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                if(_selectedObject!=null)
                {
                    txtName.Text = _selectedObject.Name;
                    txtCode.Text = _selectedObject.Code;
                }
            }
        }
        // Обработка кнопки "ОК"
        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
        }
        // Метод сохранения объекта
        private void Save()
        {
            SelectedObject.Name = txtName.Text;
            SelectedObject.Code = txtCode.Text;

            try
            {
                SelectedObject.Save();
            }
            catch (Exception ex)
            {

                if(MessageBox.Show(this, ex.Message, "Ошибка!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)== DialogResult.Retry)
                {
                    Save();
                }
            }
        }
    }
}
