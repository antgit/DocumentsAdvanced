using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Customization.Controls;
/*


 */
namespace BusinessObjects.Windows
{
    [Obsolete]
    public partial class FormCustomLayoutOld : DevExpress.XtraLayout.Customization.CustomizationForm 
    {
        public FormCustomLayoutOld()
        {
            InitializeComponent();
        }
        private MyButtpnsPAnel newPanel;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LayoutControlItem lci = layoutControl1.GetItemByControl(buttonsPanel1);
            Control oldPanel = lci.Control;
            newPanel = new MyButtpnsPAnel();
            lci.BeginInit();
            lci.Control = newPanel;
            oldPanel.Parent = null;
            lci.EndInit();
            newPanel.Register();
            lci.Update();
            
        }

        class MyButtpnsPAnel : ButtonsPanel
        {
            
            protected override void OnSaveLayoutButtonClick(object sender, EventArgs e)
            {
                Form form = ((LayoutControl)OwnerControl).FindForm();
                IWorkareaForm myForm = form as IWorkareaForm;
                if(myForm!=null)
                {
                    string controlName = string.Empty;
                    string entityKind = string.Empty;
                    string keyValue = OwnerControl.Parent.Tag != null ? OwnerControl.Parent.Tag.ToString() : OwnerControl.Parent.GetType().Name;
                    if(!string.IsNullOrWhiteSpace(myForm.Key))
                        controlName = myForm.Key;
                    if(form.Tag!=null)
                    {
                        entityKind = form.Tag.ToString();
                    }
                    XmlStorage setiings = myForm.Workarea.GetCollection<XmlStorage>().FirstOrDefault(f => f.KindId == 2359299
                                                                                    && f.Name == controlName
                                                                                    && f.Code == keyValue
                                                                                    && f.FlagString==entityKind);
                    if(setiings==null)
                        setiings = new XmlStorage
                                              {
                                                  Workarea = myForm.Workarea,
                                                  KindId = 2359299,
                                                  Name = controlName,
                                                  Code = keyValue,
                                                  UserId = myForm.Workarea.CurrentUser.Id,
                                                  FlagString = entityKind
                                              };

                    MemoryStream stream = new MemoryStream();
                    OwnerControl.SaveLayoutToStream(stream);
                    stream.Position = 0;
                    StreamReader reader = new StreamReader(stream);
                    setiings.XmlData = reader.ReadToEnd();
                    setiings.Save();
                    /*
controlName	"Окно свойств - Корреспонденты"
entityKind	"196617"	
keyValue	"ControlAgent"

                     */
                }
                //base.OnSaveLayoutButtonClick(sender, e);
            }
            protected override void OnLoadLayoutButtonClick(object sender, EventArgs e)
            {
                base.OnLoadLayoutButtonClick(sender, e);
            }
        }

    }
}
