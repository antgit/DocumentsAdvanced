using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows.Controls
{
    public partial class ControlBase : XtraUserControl, IWorkareaForm
    {
        public ControlBase()
        {
            InitializeComponent();
            AllowLayoutAction = true;
        }
        /// <summary>
        /// Ключ используемый для сохранения настроек
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Ключ используемый для сохранения настроек в наименовании
        /// </summary>
        public string KeyName { get; set; }
        /// <summary>
        /// Рабочая область
        /// </summary>
        public Workarea Workarea { get; set; }
        /// <summary>
        /// Разрешать ли действия с сохранением/востановлением разположения элементов
        /// </summary>
        [DefaultValue(true)]
        public bool AllowLayoutAction { get; set; }

        private void ControlBaseLoad(object sender, EventArgs e)
        {
            if (Workarea == null) return;
            if (Workarea.Access.RightCommon.AdminEnterprize)
            {
                if (LayoutControl.AllowCustomizationMenu)
                {
                    LayoutControl.AllowCustomizationMenu = true;
                    LayoutControl.RegisterUserCustomizatonForm(typeof (FormCustomLayout));
                }
            }
            else
            {
                LayoutControl.AllowCustomizationMenu = false;
            }
            if (!AllowLayoutAction)
                return;
            if(string.IsNullOrEmpty(KeyName))
                KeyName = GetType().ToString();
            string key = Key;
            if (string.IsNullOrEmpty(key))
                key = GetType().Name;

            List<XmlStorage> collSet = Workarea.Empty<XmlStorage>().FindBy(kindId: XmlStorage.KINDID_LAYOUTCONTROLDATA,
                                                                                            name: KeyName,
                                                                                            code: key, useAndFilter: true);
            if (collSet.Count > 0)
            {
                // Уточняющий подзапрос
                XmlStorage setiings = collSet.FirstOrDefault(f =>
                    f.KindId == XmlStorage.KINDID_LAYOUTCONTROLDATA
                    && f.Name == KeyName
                    && f.Code == key
                    );
                if (setiings != null && !string.IsNullOrWhiteSpace(setiings.XmlData))
                {
                    MemoryStream s = new MemoryStream();
                    StreamWriter w = new StreamWriter(s) { AutoFlush = true };
                    w.Write(setiings.XmlData);
                    s.Position = 0;
                    try
                    {
                        LayoutControl.RestoreLayoutFromStream(s);
                    }
                    catch (Exception)
                    {

                    }
                }

            }
        }
    }
}
