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
 Private Function GetLayoutData(ByVal View As BaseView) As String
    Dim s As New IO.MemoryStream()
    View.SaveLayoutToStream(s)
    s.Position = 0
    Dim r As New IO.StreamReader(s)
    Return r.ReadToEnd()
End Function

Private Sub SetLayoutData(ByVal View As BaseView, ByVal Data As String)
    If Data Is Nothing OrElse Data.Length = 0 Then Exit Sub

    Dim s As New IO.MemoryStream()
    Dim w As New IO.StreamWriter(s)
    w.AutoFlush = True
    w.Write(Data)
    s.Position = 0
    Try
        View.RestoreLayoutFromStream(s)
    Catch ex As Exception
        Throw New Exception("Wrong data format", ex)
    End Try
End Sub

 */
namespace BusinessObjects.Windows
{
    public partial class FormCustomLayout : DevExpress.XtraLayout.Customization.CustomizationForm 
    {
        public FormCustomLayout()
        {
            InitializeComponent();
            LayoutControlItem lci = layoutControl1.GetItemByControl(buttonsPanel1);
            Control oldPanel = lci.Control;
            newPanel = new MyButtpnsPAnel();
            newPanel.Name = "buttonsPanel1";
            lci.BeginInit();
            lci.Control = newPanel;
            oldPanel.Parent = null;
            lci.EndInit();
            newPanel.Register();
            lci.Update();
            
        }
        private MyButtpnsPAnel newPanel;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //((LayoutControl)OwnerControl).OptionsCustomizationForm.ShowPropertyGrid = true;
            //this.ownerControlCore.OptionsCustomizationForm.ShowPropertyGrid = true;
            //LayoutControlItem lci = layoutControl1.GetItemByControl(buttonsPanel1);
            //Control oldPanel = lci.Control;
            //newPanel = new MyButtpnsPAnel();
            //newPanel.Name = "buttonsPanel1";
            //lci.BeginInit();
            //lci.Control = newPanel;
            //oldPanel.Parent = null;
            //lci.EndInit();
            //newPanel.Register();
            //lci.Update();

        }

        class MyButtpnsPAnel : ButtonsPanel
        {
            
            protected override void OnSaveLayoutButtonClick(object sender, EventArgs e)
            {
                //Form form = ((LayoutControl)OwnerControl).FindForm();
                //IWorkareaForm myForm = form as IWorkareaForm;
                IWorkareaForm myForm = ((LayoutControl)OwnerControl).Parent as IWorkareaForm;
                if(myForm!=null)
                {
                    string name = myForm.KeyName;
                    string key = myForm.Key;
                    if (string.IsNullOrEmpty(key))
                        key = myForm.GetType().Name;
                    if(string.IsNullOrEmpty(name))
                        name = myForm.GetType().ToString();

                    /*
                     if(string.IsNullOrEmpty(KeyName))
                KeyName = GetType().ToString();
            string key = Key;
            if (string.IsNullOrEmpty(key))
                key = GetType().Name;
                     */

                    //XmlStorage setiings = myForm.Workarea.GetCollection<XmlStorage>().FirstOrDefault(f => f.KindId == XmlStorage.KINDID_LAYOUTCONTROLDATA
                    //                                                            && f.Name == name
                    //                                                            && f.Code == key);
                    XmlStorage setiings =
                        myForm.Workarea.Empty<XmlStorage>().FindBy(kindId: XmlStorage.KINDID_LAYOUTCONTROLDATA,
                                                                   name: name, code: key, useAndFilter: true).FirstOrDefault();
                        
                    if(setiings==null)
                        setiings = new XmlStorage
                                              {
                                                  Workarea = myForm.Workarea,
                                                  KindId = XmlStorage.KINDID_LAYOUTCONTROLDATA,
                                                  Name = name,
                                                  Code = key
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
