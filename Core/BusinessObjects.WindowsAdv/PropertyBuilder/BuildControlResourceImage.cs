using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlResourceImage : BasePropertyControlICore<ResourceImage>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlResourceImage()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Value = _common.pictureEdit.Image;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.Value = _common.pictureEdit.Image;
            SaveStateData();
            InternalSave();
        }

        ControlResourceImage _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlResourceImage
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  pictureEdit = {Image = SelectedItem.Value},
                                  layoutControlItemName =
                                      {Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  Workarea = SelectedItem.Workarea
                              };
                BarButtonItem btnEdit = new BarButtonItem
                                            {
                                                Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_IMAGEIMPORT, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph =
                                                    ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                            ResourceImage.DATAINTO_X32)
                                            };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnEdit);
                btnEdit.ItemClick += delegate
                                         {
                                             OpenFileDialog dlg = new OpenFileDialog {Filter = "PNG|*.png"};
                                             dlg.FileName = SelectedItem.Code;
                                             if (dlg.ShowDialog() == DialogResult.Cancel) return;
                                             _common.pictureEdit.Image = System.Drawing.Image.FromFile(dlg.FileName);
                                             _common.txtCode.Text = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName).ToUpper();
                                         };

                BarButtonItem btnExport = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_IMAGEEXPORT, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph =
                                                      ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                              ResourceImage.DATAOUT_X32)
                                              };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnExport);
                btnExport.ItemClick += delegate
                {
                    SaveFileDialog dlg = new SaveFileDialog {Filter = "PNG|*.png"};
                    dlg.FileName = SelectedItem.Code;
                    if (dlg.ShowDialog() == DialogResult.Cancel) return;
                    _common.pictureEdit.Image.Save(dlg.FileName);
                };

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                if (!SelectedItem.IsNew && SelectedItem.IsReadOnly)
                {
                    _common.Enabled = false;
                }
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}