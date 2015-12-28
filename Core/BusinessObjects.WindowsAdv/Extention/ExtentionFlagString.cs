using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        /// <summary>
        /// Показать диалог свойств объекта для редактирования пользовательских флагов
        /// </summary>
        /// <param name="item"></param>
        public static void ShowFlagString<T>(this T item) where T : IFlagString
        {
            FormFlagString frm = new FormFlagString();
            string val = item.GetFlagStringAll();
            if(!string.IsNullOrEmpty(val))
            {
                frm.cmbValues.Properties.Items.AddRange(val.Split(','));
                frm.listBoxControlAll.Items.AddRange(val.Split(','));
            }
            if(!string.IsNullOrEmpty(item.FlagString))
            {
                frm.listBoxControlCurrent.Items.AddRange(item.FlagString.Split(','));
            }
            frm.btnAdd.Click += delegate
                                    {
                                        if(frm.listBoxControlAll.SelectedItem!=null)
                                        {
                                            if (!frm.listBoxControlCurrent.Items.Contains(frm.listBoxControlAll.SelectedItem.ToString()))
                                                frm.listBoxControlCurrent.Items.Add(frm.listBoxControlAll.SelectedItem.ToString());
                                        }
                                    };
            frm.btnRemove.Click += delegate
                                       {
                                           if(frm.listBoxControlCurrent.SelectedItem!=null)
                                           {
                                               frm.listBoxControlCurrent.Items.Remove(frm.listBoxControlCurrent.SelectedItem);
                                           }
                                       };
            frm.cmbValues.ButtonClick +=
                delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                    {
                        if(e.Button.Index == 1)
                        {
                            if(!frm.listBoxControlCurrent.Items.Contains(frm.cmbValues.Text))
                                frm.listBoxControlCurrent.Items.Add(frm.cmbValues.Text);
                            if (!frm.listBoxControlAll.Items.Contains(frm.cmbValues.Text))
                                frm.listBoxControlAll.Items.Add(frm.cmbValues.Text);
                        }
                        if (e.Button.Index == 2)
                        {
                            if(frm.listBoxControlCurrent.SelectedIndex>-1)
                            {
                                frm.listBoxControlCurrent.Items.RemoveAt(frm.listBoxControlCurrent.SelectedIndex);
                            }
                        }
                    };

            frm.listBoxControlAll.MouseDoubleClick += delegate
                                                          {
                                                              if (frm.listBoxControlAll.SelectedItem != null)
                                                              {
                                                                  if (!frm.listBoxControlCurrent.Items.Contains(frm.listBoxControlAll.SelectedItem.ToString()))
                                                                      frm.listBoxControlCurrent.Items.Add(frm.listBoxControlAll.SelectedItem.ToString());
                                                              }
                                                          };
            if(frm.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                string res = string.Empty;
                foreach (var v in frm.listBoxControlCurrent.Items)
                {
                    if (res.Length > 0) res += ',';
                    res += v.ToString();
                }
                item.FlagString = res;
            }
        }
    }
}
