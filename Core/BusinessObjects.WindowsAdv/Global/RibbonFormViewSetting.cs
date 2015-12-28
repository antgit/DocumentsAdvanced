using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
namespace BusinessObjects
{
    [Serializable]
    public class RibbonFormViewSetting : IRibbonFormViewSetting
    {
        public RibbonFormViewSetting()
        {
            Pages = null;
        }

        #region Свойства
        /// <summary>
        /// Наименование настройки
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание настройки
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// Код настройки
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Закладки
        /// </summary>
        public List<PageSetting> Pages { get; set; }
        /// <summary>
        /// Автоматическое сохранение, после создания
        /// </summary>
        public bool AutoSave { get; set; }
        /// <summary>
        /// Строка форматирования описания документа
        /// </summary>
        public string FormatSummary { get; set; }
        /// <summary>
        /// Настройки в виде XML
        /// </summary>
        [XmlIgnore]
        public XmlStorage Storage { get; set; }
        #endregion

        /// <summary>
        /// Сохранить текущие настройки
        /// </summary>
        public virtual void Save(Workarea workarea)
        {
            if (string.IsNullOrEmpty(Code)) return;

            int userId = Storage.UserId;

            //Storage = workarea.GetCollection<XmlStorage>().FirstOrDefault(s => s.Code == Code && s.UserId == userId) ??
            //          new XmlStorage { Workarea = workarea, Name = Name, Code = Code, KindValue = 1, UserId = userId };

            XmlSerializer serializer = new XmlSerializer(typeof(RibbonFormViewSetting));
            TextWriter write = new StringWriter();
            serializer.Serialize(write, this);
            Storage.XmlData = write.ToString();
            Storage.KindId = XmlStorage.KINDID_DOCSETTINGS;
            write.Close();

            Storage.Save();
        }

        /// <summary>
        /// Загрузить текущие настройки
        /// </summary>
        public virtual void Load(Workarea workarea)
        {
            int userId;

            if (Storage == null)
            {
                userId = workarea.CurrentUser.Id;
                Storage = workarea.Cashe.GetCasheData<XmlStorage>().Dictionary.Values.FirstOrDefault(
                    s => s.Code == Code && s.UserId == userId);
                if (Storage == null)
                    workarea.Cashe.GetCasheData<XmlStorage>().Dictionary.Values.FirstOrDefault(
                    s => s.Code == Code && s.UserId == 0);
                if(Storage==null)
                    Storage = workarea.Empty<XmlStorage>().FindBy(code: Code).FirstOrDefault(s => s.Code == Code && s.UserId == userId) ??
                          workarea.Empty<XmlStorage>().FindBy(code: Code).FirstOrDefault(s => s.Code == Code && s.UserId == 0);
            }
            else
            {
                userId = Storage.UserId;

                Storage = workarea.GetCollection<XmlStorage>().FirstOrDefault(s => s.Code == Code && s.UserId == userId);
            }
            if (Storage == null)
            {
                Storage = new XmlStorage { Workarea = workarea, Name = Name, Code = Code, KindValue = XmlStorage.KINDVALUE_DOCSETTINGS , UserId = userId };
                Pages = null;
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RibbonFormViewSetting));
                TextReader reader = new StringReader(Storage.XmlData);
                RibbonFormViewSetting setting = (RibbonFormViewSetting)serializer.Deserialize(reader);
                reader.Close();

                Memo = setting.Memo;
                Pages = setting.Pages;
                AutoSave = setting.AutoSave;
                FormatSummary = setting.FormatSummary;
            }
        }
        void IRibbonFormViewSetting.Reset(Workarea workarea, object form)
        {
            RibbonForm frm = form as RibbonForm;
            if (frm != null)
                Reset(workarea, frm);
        }
        /// <summary>
        /// Сбросить текущие настройки на настройки по умолчанию
        /// </summary>
        public virtual void Reset(Workarea workarea, RibbonForm form)
        {
            XmlStorage xml = null;
            Pages = new List<PageSetting>();

            if (Storage.UserId != 0)
                xml = workarea.GetCollection<XmlStorage>().FirstOrDefault(s => s.Code == Code && s.UserId == 0);
            if (xml == null)
            {
                Storage.Code = "";
                int index = form.Ribbon.SelectedPage.PageIndex;

                PageSetting pageSetting;
                PageActionGroupSetting groupSetting;
                ButtonSetting btnSetting;

                for (int i = 0; i < form.Ribbon.Pages.Count - 1; i++)
                {
                    form.Ribbon.SelectedPage = form.Ribbon.Pages[i];
                    pageSetting = new PageSetting { Name = form.Ribbon.Pages[i].Name, Visible = true, Caption = form.Ribbon.Pages[i].Text };
                    foreach (RibbonPageGroup group in form.Ribbon.Pages[i].Groups)
                    {
                        groupSetting = new PageActionGroupSetting { Name = group.Name, Visible = true, Caption = group.Text };
                        foreach (BarItemLink btn in group.ItemLinks)
                        {
                            //if (btn.Item.Name != "btnSelect" && btn.Item.Name != "btnCreate" && btn.Item.Name != "btnRefresh" && btn.Item.Name != "btnDelete" && btn.Item.Name != "btnProp")
                            //{
                                btnSetting = new ButtonSetting { Name = btn.Item.Name, Visible = btn.Item.Visibility == BarItemVisibility.Always ? true : false, Caption = btn.Item.Caption };

                                groupSetting.Buttons.Add(btnSetting);
                            //}
                        }
                        pageSetting.Groups.Add(groupSetting);
                    }
                    Pages.Add(pageSetting);
                }
                form.Ribbon.SelectedPage = form.Ribbon.Pages[index];
                Storage.Code = Code;
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RibbonFormViewSetting));
                TextReader reader = new StringReader(xml.XmlData);
                Pages = (serializer.Deserialize(reader) as RibbonFormViewSetting).Pages;
                reader.Close();
            }
        }
    }
}
