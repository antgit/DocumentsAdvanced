using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.Utils;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraLayout;

namespace BusinessObjects.Windows
{
    internal static class UIHelper
    {
        static UIHelper()
        {
            LastActivePage = new Dictionary<string, string>();
        }
        /// <summary>
        /// Подготовка подсказок для элементов управления
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="selectedItem">Текущий объект</param>
        /// <param name="mainControl">Основной элемент управления</param>
        public static void GenerateTooltips<T>( T selectedItem,  ControlBase mainControl)
            where T : class, ICoreObject, new()
        {
            foreach (Control ctl in mainControl.LayoutControl.Controls)
            {

                if (ctl is DevExpress.Utils.Controls.ControlBase)
                {
                    string key = string.Format("TOOLTIP_{0}_{1}", selectedItem.GetType().Name.ToUpper(), ctl.Name.ToUpper());
                    string value = selectedItem.Workarea.Cashe.ResourceString(key);
                    LayoutControlItem layoutItem = mainControl.LayoutControl.GetItemByControl(ctl);
                    if (layoutItem != null)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            (ctl as DevExpress.XtraEditors.BaseControl).SuperTip =
                                UIHelper.CreateSuperToolTip(
                                    ResourceImage.GetByCode(selectedItem.Workarea, ResourceImage.INFO_X32), layoutItem.CustomizationFormText, value);
                        }
                        else
                        {
                            Debug.WriteLine(key);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Идентификатор вида связи для сотрудников
        /// </summary>
        /// <param name="wa"></param>
        /// <returns></returns>
        public static int PageDefaultChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.PAGESDEFAULT && s.FromEntityId == (int)WhellKnownDbEntity.Library).Id;
        }
        private static Dictionary<string, string> LastActivePage;
        internal static RibbonPage GetPageByTag(RibbonForm mainform, string value)
        {
            foreach (RibbonPage s in mainform.Ribbon.Pages)
            {
                if (s.Tag != null && s.Tag.ToString() == value) return s;
            }
            return null;
        }
        public static string GetLastActivePageIndex(string typeKey)
        {
            if(LastActivePage.ContainsKey(typeKey))
                return LastActivePage[typeKey];
            return string.Empty;
        }
        public static void SetLastActivePageIndex(string typeKey, string value)
        {
            if (LastActivePage.ContainsKey(typeKey))
                LastActivePage[typeKey] = value;
            else
                LastActivePage.Add(typeKey, value);
        }
        internal static string FindParentHierarchy(Hierarchy value)
        {
            if (value == null)
                return string.Empty;
            if (value.Parent == null)
                return string.Empty;
            if (value.Parent.Code == "UI_DOCUMENTS2012")
                return value.Code;
            else
                return FindParentHierarchy(value.Parent);
        }
        /// <summary>
        /// Поиск системных модулей реализованных в библиотеке BusinessObjects.Windows  по строковому коду системного модуля
        /// </summary>
        /// <param name="key">Ключ для поиска</param>
        /// <returns></returns>
        public static IContentModule FindIContentModuleSystem(string key)
        {
            switch (key)
            {
                case "CALENDAR_MODULE":
                    return new ContentModuleCalendar();
                case "DEPATMENT_MODULE":
                    return new ContentModuleDepatment();
                case "TERRITORYUA_MODULE":
                    return new ContentModuleTerritoryUA();
                case "EVENT_MODULE":
                    return new ContentModuleEvent();
                case "KNOWLEDGE_MODULE":
                    return new ContentModuleKnowledge();
                case "TASK_MODULE":
                    return new ContentModuleTask();
                case "REPORTSWEB_MODULE":
                    return new ReportsWebContentModule();
                case "DOCUMENT_MODULE":
                    return new ContentModuleDocumentHierarchy();
                case "REPORTSPRINT_MODULE":
                    return new ReportsPrintContentModule();
                case "REPORTSINTERACTIVE_MODULE":
                    return new ReportsContentModule();
                case "WEBSERVICE_MODULE":
                    return new ContentModuleWebService();
                case "AUTONUM_MODULE":
                    return new ContentModuleAutonum();
                case "DATACATALOG_MODULE":
                    return new ContentModuleDataCatalog();
                case "MESSAGE_MODULE":
                    return new ContentModuleMessage();
                case "SYSTEM_SALESLEAVE_MODULE":
                    return new ContentModuleSaleLeave();
                case "SYSTEM_SALESPRICE_MODULE":
                    return new ContentModuleSalePrice();
                case "ENTITYTYPE_MODULE":
                    return new ContentModuleEntityType();
                case "MODULEDOGOVORMANAGER_MODULE":
                    return new ContentModuleDogovorManager();
                case "MODULEDOGOVOR_MODULE":
                    return new ContentModuleDogovor();
                case "MODULEPRICE_MODULE":
                    return new ContentModulePrice();
                case "MODULEBOOKKEEP_MODULE":
                    return new ContentModuleBookKeep();
                case "MODULESALEMANAGER_MODULE":
                    return new ContentModuleSaleManager();
                case "MODULETAX_MODULE":
                    return new ContentModuleTax();
                case "MODULESERVICE_MODULE":
                    return new ContentModuleService();
                case "MODULEMAIN_MODULE":
                    return new ContentModuleMain();
                case "MAINDOCUMENTS_MODULE":
                    return new ContentModuleDocuments();
                case "AGENT_MODULE":
                    return new ContentModuleAgent();
                case "ACCOUNT_MODULE":
                    return new ContentModuleAccount();
                case "BRANCHE_MODULE":
                    return new ContentModuleBranche();
                case "COUNTRY_MODULE":
                    return new ContentModuleCountry();
                case "CURRENCY_MODULE":
                    return new ContentModuleCurrency();
                case "CUSTOMVIEWLIST_MODULE":
                    return new ContentModuleCustomViewList();
                case "FOLDER_MODULE":
                    return new ContentModuleFolder();
                case "FILEDATA_MODULE":
                    return new ContentModuleFileData();
                case "LIBRARY_MODULE":
                    return new ContentModuleLibrary();
                case "RATE_MODULE":
                    return new ContentModuleRate();
                case "RECIPE_MODULE":
                    return new ContentModuleRecipe();
                case "REPORTSERVER_MODULE":
                    return new ReportServerContentModule();
                case "RESOURCESTRING_MODULE":
                    return new ContentModuleResourceString();
                case "RULESET_MODULE":
                    return new ContentModuleRuleset();
                case "STORAGECELL_MODULE":
                    return new ContentModuleStorageCell();
                case "SYSTEMPARAMETER_MODULE":
                    return new ContentModuleSystemParameter();
                case "UID_MODULE":
                    return new ContentModuleUid();
                case "XMLSTORAGE_MODULE":
                    return new ContentModuleXMLStorage();
                case "RESOURCEIMAGE_MODULE":
                    return new ContentModuleResourceImage();
                case "ANALITIC_MODULE":
                    return new ContentModuleAnalitic();
                case "PRODUCT_MODULE":
                    return new ProductContentModule();
                case "UNIT_MODULE":
                    return new ContentModuleUnit();
                case "PRICENAME_MODULE":
                    return new ContentModulePriceName();
                case "MODULESALE_MODULE":
                    return new ContentModuleSale();
                case "MODULEFINANCE_MODULE":
                    return new ContentModuleFinance();
                case "MODULESTORE_MODULE":
                    return new ContentModuleStore();
                case "MODULEPERSON_MODULE":
                    return new ContentModulePerson();
                case "NOTE_MODULE":
                    return new ContentModuleNote();
            }
            return null;
        }

        public static SuperToolTip CreateSuperToolTip(Image image, string caption, string text)
        {
            SuperToolTip superToolTip = new SuperToolTip { AllowHtmlText = DefaultBoolean.True };
            ToolTipTitleItem toolTipTitle = new ToolTipTitleItem { Text = caption };
            ToolTipItem toolTipItem = new ToolTipItem { LeftIndent = 6, Text = text };
            toolTipItem.Appearance.Image = image;
            toolTipItem.Appearance.Options.UseImage = true;
            superToolTip.Items.Add(toolTipTitle);
            superToolTip.Items.Add(toolTipItem);

            return superToolTip;
        }
        public static Library FindWindow2(object selectedItem)
        {
            IBase val = selectedItem as IBase;
            string keyWindows = string.Format("WINDOW_{0}_KINDVALUE{1}", selectedItem.GetType().Name.ToUpper(),
                                              val.KindValue);
            Library libWindow = val.Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(keyWindows);
            if (libWindow == null)
            {
                keyWindows = string.Format("WINDOW_{0}", selectedItem.GetType().Name.ToUpper());
                libWindow = val.Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(keyWindows);
            }
            return libWindow;
        }
        public static Library FindWindow<T>(T selectedItem) where T: class, IBase, new()
        {
            string keyWindows = string.Format("WINDOW_{0}_KINDVALUE{1}", selectedItem.GetType().Name.ToUpper(),
                                              selectedItem.KindValue);
            Library libWindow = selectedItem.Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(keyWindows);
            if(libWindow==null)
            {
                keyWindows = string.Format("WINDOW_{0}", selectedItem.GetType().Name.ToUpper());
                libWindow = selectedItem.Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(keyWindows);
            }
            return libWindow;
        }

        /// <summary>Отображение иконок для списков корреспондентов</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceAgents"></param>
        internal static void DisplayAgentImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAgents)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Agent imageItem = bindSourceAgents[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Agent imageItem = bindSourceAgents[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        /// <summary>Отображение иконок для списков графиков работ и перерывов</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceTimePeriod"></param>
        internal static void DisplayTimePriodImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceTimePeriod)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceTimePeriod.Count > 0)
            {
                TimePeriod imageItem = bindSourceTimePeriod[e.ListSourceRowIndex] as TimePeriod;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceTimePeriod.Count > 0)
            {
                TimePeriod imageItem = bindSourceTimePeriod[e.ListSourceRowIndex] as TimePeriod;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        /// <summary>Отображение иконок для списков корреспондентов</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceAgents"></param>
        internal static void DisplayAgentBankImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAgents)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                AgentBankAccount imageItem = bindSourceAgents[e.ListSourceRowIndex] as AgentBankAccount;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                AgentBankAccount imageItem = bindSourceAgents[e.ListSourceRowIndex] as AgentBankAccount;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        internal static void DisplayCurrencyImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAgents)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Currency imageItem = bindSourceAgents[e.ListSourceRowIndex] as Currency;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Currency imageItem = bindSourceAgents[e.ListSourceRowIndex] as Currency;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        /// <summary>Отображение иконок для списков корреспондентов</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceAnalitics"></param>
        internal static void DisplayAnaliticImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAnalitics)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAnalitics.Count > 0)
            {
                Analitic imageItem = bindSourceAnalitics[e.ListSourceRowIndex] as Analitic;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAnalitics.Count > 0)
            {
                Analitic imageItem = bindSourceAnalitics[e.ListSourceRowIndex] as Analitic;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        /// <summary>Отображение иконок для списков пользователей</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceUids"></param>
        internal static void DisplayUidImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceUids)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceUids.Count > 0)
            {
                Uid imageItem = bindSourceUids[e.ListSourceRowIndex] as Uid;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceUids.Count > 0)
            {
                Uid imageItem = bindSourceUids[e.ListSourceRowIndex] as Uid;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }

        /// <summary>Отображение иконок для списков процессов</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceUids"></param>
        internal static void DisplayRulesetImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceUids)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceUids.Count > 0)
            {
                Ruleset imageItem = bindSourceUids[e.ListSourceRowIndex] as Ruleset;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceUids.Count > 0)
            {
                Ruleset imageItem = bindSourceUids[e.ListSourceRowIndex] as Ruleset;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
    }
}
