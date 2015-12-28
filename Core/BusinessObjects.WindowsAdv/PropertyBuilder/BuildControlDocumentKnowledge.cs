using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlDocumentKnowledge : BasePropertyControlICore<Document>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlDocumentKnowledge()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            InternalSave();
        }

        private ControlList _controlCodeView;
        private BindingSource _sourceBindCodes;
        private List<ChainValueView> _collCodeView;
        protected override void BuildPageCommon()
        {

            if (_controlCodeView == null)
            {
                _controlCodeView = new ControlList { Name = ExtentionString.CONTROL_COMMON_NAME };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlCodeView.View,
                                                       "DEFAULT_LISTVIEWCHAINVALUE");
                _controlCodeView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlCodeView);
                _sourceBindCodes = new BindingSource();
                List<ChainValueView> selfView = ChainValueView.GetView<Document, Knowledge>(SelectedItem);

                if (SelectedItem.TemplateId != 0)
                {
                    List<ChainValueView> tmlView = ChainValueView.GetView<Document, Knowledge>(SelectedItem.GetTemplate());

                    _collCodeView = selfView.Union(tmlView).ToList();
                }
                else
                    _collCodeView = selfView;
                
                _sourceBindCodes.DataSource = _collCodeView;
                _controlCodeView.Grid.DataSource = _sourceBindCodes;
                _controlCodeView.View.ExpandAllGroups();

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_COMMON_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                if (SelectedItem.Workarea.Access.RightCommon.Admin || SelectedItem.Workarea.Access.RightCommon.AdminEnterprize)
                {
                    #region Новое значение

                    BarButtonItem btnValueCreate = new BarButtonItem
                                                       {
                                                           ButtonStyle = BarButtonStyle.DropDown,
                                                           ActAsDropDown = true,
                                                           Caption =
                                                               SelectedItem.Workarea.Cashe.ResourceString(
                                                                   ResourceString.BTN_CAPTION_CREATE, 1049),
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                       };
                    groupLinksAction.ItemLinks.Add(btnValueCreate);
                    //short docTypeId = EntityDocumentKind.ExtractEntityKind(SelectedItem.KindId);
                    List<ChainKind> collectionTemplates =
                        SelectedItem.Workarea.CollectionChainKinds.FindAll(
                            f =>
                            f.FromEntityId == SelectedItem.EntityId &&
                            f.ToEntityId == (int) WhellKnownDbEntity.Knowledge && f.Code == ChainKind.KNOWLEDGES);
                    //List<CodeName> collectionTemplates = SelectedItem.Workarea.GetCollection<CodeName>().FindAll(f => f.ToEntityId == SelectedItem.EntityId && f.DocTypeId == docTypeId);
                    PopupMenu mnuTemplates = new PopupMenu {Ribbon = frmProp.ribbon};
                    foreach (ChainKind itemTml in collectionTemplates)
                    {
                        BarButtonItem btn = new BarButtonItem {Caption = itemTml.Name};
                        mnuTemplates.AddItem(btn);
                        btn.Tag = itemTml;

                        btn.SuperTip = UIHelper.CreateSuperToolTip(btnValueCreate.Glyph, btn.Caption, itemTml.Memo);
                        btn.ItemClick += delegate
                                             {
                                                 ChainKind objectTml = (ChainKind) btn.Tag;
                                                 List<int> types = objectTml.GetContentTypeKindId();
                                                 TreeListBrowser<Knowledge> browseDialog =
                                                     new TreeListBrowser<Knowledge> {Workarea = SelectedItem.Workarea}.
                                                         ShowDialog();
                                                 if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) ||
                                                     (browseDialog.DialogResult != DialogResult.OK)) return;
                                                 List<Knowledge> newAgent =
                                                     browseDialog.ListBrowserBaseObjects.SelectedValues;
                                                 //if (!BindSourceAgentTo.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                                                 //    BindSourceAgentTo.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
                                                 //List<Knowledge> newAgent = SelectedItem.Workarea.Empty<Knowledge>().BrowseListType(null, SelectedItem.Workarea.GetCollection<Knowledge>());
                                                 if (newAgent != null)
                                                 {
                                                     foreach (Knowledge selItem in newAgent)
                                                     {
                                                         ChainAdvanced<Document, Knowledge> link =
                                                             new ChainAdvanced<Document, Knowledge>(SelectedItem)
                                                                 {
                                                                     RightId = selItem.Id,
                                                                     KindId = objectTml.Id,
                                                                     StateId = State.STATEACTIVE
                                                                 };

                                                         try
                                                         {
                                                             link.Save();
                                                             ChainValueView view =
                                                                 ChainValueView.ConvertToView<Document, Knowledge>(link);
                                                             _sourceBindCodes.Add(view);
                                                         }
                                                         catch (DatabaseException dbe)
                                                         {
                                                             Extentions.ShowMessageDatabaseExeption(
                                                                 SelectedItem.Workarea,
                                                                 SelectedItem.Workarea.Cashe.ResourceString(
                                                                     ResourceString.MSG_CAPERROR, 1049),
                                                                 "Создание новой связи невозможно!", dbe.Message, dbe.Id);
                                                         }
                                                         catch (Exception ex)
                                                         {
                                                             XtraMessageBox.Show(ex.Message,
                                                                                 SelectedItem.Workarea.Cashe.
                                                                                     ResourceString(
                                                                                         ResourceString.MSG_CAPERROR,
                                                                                         1049),
                                                                                 MessageBoxButtons.OK,
                                                                                 MessageBoxIcon.Error);
                                                         }
                                                     }
                                                 }
                                             };
                    }
                    btnValueCreate.DropDownControl = mnuTemplates;

                    #endregion
                }

                #region Навигация
                BarButtonItem btnValueEdit = new BarButtonItem
                                                 {
                                                     Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PREVIEW, 1049),
                                                     RibbonStyle = RibbonItemStyles.Large,
                                                     Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.PREVIEW_X32)
                                                 };
                groupLinksAction.ItemLinks.Add(btnValueEdit);
                btnValueEdit.ItemClick += delegate
                                              {
                                                  ChainValueView currentObject = _sourceBindCodes.Current as ChainValueView;

                                                  if (currentObject != null)
                                                  {
                                                      ChainAdvanced<Document, Knowledge> value = ChainValueView.ConvertToValue<Document, Knowledge>(SelectedItem, currentObject);
                                                      if (value != null)
                                                      {
                                                          value.Right.ShowKnowledge();
                                                      }
                                                  }

                                              };
                #endregion

                if(SelectedItem.Workarea.Access.RightCommon.Admin || SelectedItem.Workarea.Access.RightCommon.AdminEnterprize)
                {
                    #region Удаление
                BarButtonItem btnChainDelete = new BarButtonItem
                                                   {
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainDelete);


                btnChainDelete.ItemClick += delegate
                                                {
                                                    ChainValueView currentObject = _sourceBindCodes.Current as ChainValueView;
                                                    if (currentObject != null)
                                                    {
                                                        int res = Extentions.ShowMessageChoice(SelectedItem.Workarea,
                                                                                               SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), "Удаление связей",
                                                                                               string.Empty,
                                                                                               Properties.Resources.STR_CHOICE_DEL);
                                                        if (res == 0)
                                                        {
                                                            try
                                                            {
                                                                ChainAdvanced<Document, Knowledge> value = ChainValueView.ConvertToValue<Document, Knowledge>(SelectedItem, currentObject);
                                                                value.Remove();
                                                                _sourceBindCodes.Remove(currentObject);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                XtraMessageBox.Show(ex.Message,
                                                                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                        }
                                                        else if (res == 1)
                                                        {
                                                            try
                                                            {
                                                                ChainAdvanced<Document, Knowledge> value = ChainValueView.ConvertToValue<Document, Knowledge>(SelectedItem, currentObject);
                                                                value.Delete();
                                                                _sourceBindCodes.Remove(currentObject);
                                                            }
                                                            catch (DatabaseException dbe)
                                                            {
                                                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                                                                                       SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                                       "Ошибка удаления связи!", dbe.Message, dbe.Id);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                XtraMessageBox.Show(ex.Message,
                                                                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                        }
                                                    }
                                                };
                #endregion
                }
                _controlCodeView.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
                                                            {
                                                                if (e.Column.Name == "colImage")
                                                                {
                                                                    Rectangle r = e.Bounds;
                                                                    Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINK_X16);
                                                                    e.Graphics.DrawImageUnscaledAndClipped(img, r);
                                                                    e.Handled = true;
                                                                }
                                                                else if (e.Column.Name == "colStateImage")
                                                                {
                                                                    ChainValueView rowValue = _sourceBindCodes.Current as ChainValueView;
                                                                    if (rowValue == null) return;
                                                                }
                                                            };
                _controlCodeView.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                                                                     {
                                                                         if (e.Column.FieldName == "ImageRight" && e.IsGetData && _sourceBindCodes.Count > 0)
                                                                         {
                                                                             ChainValueView imageItem = _sourceBindCodes[e.ListSourceRowIndex] as ChainValueView;
                                                                             if (imageItem != null)
                                                                             {
                                                                                 e.Value = ExtentionsImage.GetImageKnowledge(imageItem.Workarea, imageItem.RightKind);
                                                                             }
                                                                         }
                                                                         else if (e.Column.Name == "colStateImage" && e.IsGetData && _sourceBindCodes.Count > 0)
                                                                         {
                                                                             ChainValueView imageItem = _sourceBindCodes[e.ListSourceRowIndex] as ChainValueView;
                                                                             if (imageItem != null)
                                                                             {
                                                                                 e.Value = ExtentionsImage.GetImageState(imageItem.Workarea, imageItem.StateId);
                                                                             }
                                                                         }
                                                                     };
                page.Groups.Add(groupLinksAction);
                _controlCodeView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlCodeView.Grid;
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}