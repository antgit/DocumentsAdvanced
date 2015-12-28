using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства
        public static RuleSet DeserializeRuleSet(this Ruleset value)
        {
            if (!String.IsNullOrEmpty(value.Value))
            {
                WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                StringReader stringReader = new StringReader(value.Value);
                XmlTextReader reader = new XmlTextReader(stringReader);
                return serializer.Deserialize(reader) as RuleSet;
            }
            return null;
        }
        public static void Save(this RuleSet ruleSet, string fileName)
        {
            if (ruleSet != null)
            {
                WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8);
                serializer.Serialize(writer, ruleSet);
                writer.Flush();
                writer.Close();
            }
        }
        public static string RuleSetSerialize(this RuleSet ruleSet)
        {
            StringBuilder ruleDefinition = new StringBuilder();

            if (ruleSet != null)
            {
                try
                {
                    WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                    StringWriter stringWriter = new StringWriter(ruleDefinition, CultureInfo.InvariantCulture);
                    XmlTextWriter writer = new XmlTextWriter(stringWriter);
                    serializer.Serialize(writer, ruleSet);
                    writer.Flush();
                    writer.Close();
                    stringWriter.Flush();
                    stringWriter.Close();
                }
                catch (Exception ex)
                {
                    //if (selectedRuleSetData != null)
                    //    MessageBox.Show(string.Format(CultureInfo.InvariantCulture, "Error serializing RuleSet: '{0}'. \r\n\n{1}", selectedRuleSetData.Name, ex.Message), "Serialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //else
                    //    MessageBox.Show(string.Format(CultureInfo.InvariantCulture, "Error serializing RuleSet. \r\n\n{0}", ex.Message), "Serialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //if (selectedRuleSetData != null)
                //    MessageBox.Show(String.Format(CultureInfo.InvariantCulture, "Error serializing RuleSet: '{0}'.", selectedRuleSetData.Name), "Serialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //else
                //    MessageBox.Show("Error serializing RuleSet.", "Serialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ruleDefinition.ToString();
        }

        /// <summary>
        /// Свойства набора правил
        /// </summary>
        /// <param name="item">Набор правил</param>
        /// <returns></returns>
        public static Form ShowProperty(this Ruleset item)
        {
            InternalShowPropertyBase<Ruleset> showPropertyBase = new InternalShowPropertyBase<Ruleset>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlRuleset { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список наборов правил
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static Ruleset BrowseList(this Ruleset item)
        {
            List<Ruleset> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список правил
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<Ruleset> BrowseList(this Ruleset item, Predicate<Ruleset> filter, List<Ruleset> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<Ruleset> BrowseMultyList(this Ruleset item, Workarea wa, Predicate<Ruleset> filter, List<Ruleset> sourceCollection, bool allowMultySelect)
        {
            List<Ruleset> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = wa.Empty<Ruleset>().Entity.GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<Ruleset> browserBaseObjects = new ListBrowserBaseObjects<Ruleset>(wa, sourceCollection, filter, item, true, false, false, true)
                                                                     {Owner = frm};
            browserBaseObjects.Build();

            new FormStateMaintainer(frm, String.Format("Browse{0}", item.GetType().Name));
            frm.clientPanel.Controls.Add(browserBaseObjects.ListControl);
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnProp.Visibility = BarItemVisibility.Always;
            frm.btnCreate.Visibility = BarItemVisibility.Always;
            frm.btnDelete.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Visibility = BarItemVisibility.Always;

            frm.btnDelete.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.DELETE_X32);
            frm.btnSelect.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.SELECT_X32);

            frm.btnProp.ItemClick += delegate
            {
                browserBaseObjects.InvokeProperties();
            };
            frm.btnDelete.ItemClick += delegate
            {
                browserBaseObjects.InvokeDelete();
            };
            browserBaseObjects.ListControl.Dock = DockStyle.Fill;
            browserBaseObjects.ShowProperty += delegate(Ruleset obj)
            {
                obj.ShowProperty();
                if (obj.IsNew)
                {
                    obj.Created += delegate
                    {
                        int position = browserBaseObjects.BindingSource.Add(obj);
                        browserBaseObjects.BindingSource.Position = position;
                    };
                }
            };

            browserBaseObjects.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.Alt)
                {
                    returnValue = browserBaseObjects.SelectedValues;
                    frm.Close();
                }
            };
            frm.btnSelect.ItemClick += delegate
            {
                returnValue = browserBaseObjects.SelectedValues;
                frm.Close();
            };
            frm.btnClose.ItemClick += delegate
            {
                returnValue = null;
                frm.Close();

            };
            frm.ShowDialog();

            return returnValue;
        }

        public static List<Ruleset> BrowseContent(this Ruleset item, Workarea wa = null)
        {
            ContentModuleRuleset module = new ContentModuleRuleset();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
    }
}

