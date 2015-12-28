using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.Utils;
namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        public static void CreateOpenWindowsButton(RibbonControl ribbon, Workarea wa)
        {
            BarButtonItem btnWindowsList = new BarButtonItem();
            ribbon.Items.Add(btnWindowsList);
            ribbon.PageHeaderItemLinks.Add(btnWindowsList);

            btnWindowsList.ActAsDropDown = true;
            btnWindowsList.ButtonStyle = BarButtonStyle.DropDown;
            btnWindowsList.Caption = "Список окон";
            //btnWindowsList.Id = 22;
            btnWindowsList.Name = "btnWindowsList";
            btnWindowsList.RibbonStyle = RibbonItemStyles.SmallWithText;
            btnWindowsList.SmallWithTextWidth = 150;
            btnWindowsList.SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(wa, ResourceImage.WINDOWSLIST_X16), "Список открытых окон",
                "Список открытых окон программы позволяет быстро переключаться между окнами документов");

            btnWindowsList.Glyph = ResourceImage.GetByCode(wa, ResourceImage.WINDOWSLIST_X16);
            PopupMenu popupMenuOpenWindows = GetPeriodPopupMenu(ribbon);
            btnWindowsList.DropDownControl = popupMenuOpenWindows;

        }
        public static BarButtonItem CreateHelpButton(RibbonControl ribbon, Workarea wa)
        {
            BarButtonItem btnRequestHelp = new BarButtonItem();
            ribbon.Items.Add(btnRequestHelp);
            ribbon.PageHeaderItemLinks.Add(btnRequestHelp);
            btnRequestHelp.Caption = "Помощь";
            btnRequestHelp.Name = "btnRequestHelp";
            btnRequestHelp.RibbonStyle = RibbonItemStyles.SmallWithText;
            btnRequestHelp.SmallWithTextWidth = 150;
            btnRequestHelp.SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(wa, ResourceImage.HELP_X16), "Помощь",
                "Помощь, справочная информация, документация - поможет вам эффективнее работать в программе");

            btnRequestHelp.Glyph = ResourceImage.GetByCode(wa, ResourceImage.HELP_X16);
            return btnRequestHelp;
        }
        public static PopupMenu GetPeriodPopupMenu(RibbonControl ribbon)
        {
            PopupMenu popupMenuOpenWindows = new PopupMenu {Ribbon = ribbon};
            popupMenuOpenWindows.BeforePopup += delegate
                                                    {


                                                        //if (popupMenuOpenWindows.)
                                                        popupMenuOpenWindows.ItemLinks.Clear();
                                                        foreach (Form form in Application.OpenForms)
                                                        {
                                                            if (!String.IsNullOrEmpty(form.Text))
                                                            {
                                                                BarButtonItem item = new BarButtonItem
                                                                                         {
                                                                                             Caption = form.Text,
                                                                                             Tag = form
                                                                                         };
                                                                popupMenuOpenWindows.AddItem(item);
                                                                item.ItemClick += delegate
                                                                                      {
                                                                                          (item.Tag as Form).Activate();
                                                                                      };
                                                            }
                                                        }
                                                    };
            return popupMenuOpenWindows;
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
    }
}
