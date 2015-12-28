using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{

    internal sealed class BuildControlDocumentNotes : BasePropertyControlICore<Document>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlDocumentNotes()
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

        private ControlList _controlNoteView;
        private BindingSource _sourceBindNotes;
        private List<NoteValueView> _collNoteView;
        protected override void BuildPageCommon()
        {

            if (_controlNoteView == null)
            {
                _controlNoteView = new ControlList { Name = ExtentionString.CONTROL_COMMON_NAME };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlNoteView.View,
                                                       "NOTE_VIEW");
                _controlNoteView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlNoteView);
                _sourceBindNotes = new BindingSource();
                _collNoteView = NoteValueView.GetView<Document>(SelectedItem, true);
                _sourceBindNotes.DataSource = _collNoteView;
                _controlNoteView.Grid.DataSource = _sourceBindNotes;
                _controlNoteView.View.ExpandAllGroups();

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_COMMON_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Новое значение
                BarButtonItem btnValueCreate = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.DropDown,
                    ActAsDropDown = true,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                };
                groupLinksAction.ItemLinks.Add(btnValueCreate);
                List<Note> collectionTemplates = SelectedItem.Workarea.GetTemplates<Note>();
                PopupMenu mnuTemplates = new PopupMenu { Ribbon = frmProp.ribbon };
                foreach (Note itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem { Caption = itemTml.Name };
                    mnuTemplates.AddItem(btn);
                    btn.Tag = itemTml;
                    btn.ItemClick += delegate
                    {
                        Note objectTml = (Note)btn.Tag;
                        Note newNote = SelectedItem.Workarea.CreateNewObject<Note>(objectTml);
                        newNote.UserOwnerId = SelectedItem.Workarea.CurrentUser.Id;
                        newNote.Created += delegate
                                               {
                                                   ChainNotes<Document> newNoteValue = new ChainNotes<Document>{Workarea = SelectedItem.Workarea};
                                                   newNoteValue.Left = SelectedItem;
                                                   newNoteValue.RightId = newNote.Id;
                                                   newNoteValue.StateId = State.STATEACTIVE;
                                                   newNoteValue.UserOwnerId = newNote.UserOwnerId;
                                                   newNoteValue.KindId =
                                                       SelectedItem.Workarea.CollectionChainKinds.Find(
                                                           f => f.Code == ChainKind.NOTES & f.FromEntityId == 20).Id;
                                                   newNoteValue.Save();
                                                   _sourceBindNotes.Add(NoteValueView.ConvertToView(newNoteValue));

                                               };
                        newNote.ShowProperty();
                    };
                }
                btnValueCreate.DropDownControl = mnuTemplates;

                #endregion
                #region Изменить
                BarButtonItem btnValueEdit = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                };
                groupLinksAction.ItemLinks.Add(btnValueEdit);
                btnValueEdit.ItemClick += delegate
                {
                    NoteValueView rowValue = _sourceBindNotes.Current as NoteValueView;
                    if (rowValue == null) return;

                    Note value = NoteValueView.GetNote(SelectedItem, rowValue);
                    value.ShowProperty();

                };
                #endregion

                page.Groups.Add(groupLinksAction);
                _controlNoteView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlNoteView.Grid;
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }

    internal sealed class BuildControlDocumentCodes : BasePropertyControlICore<Document>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlDocumentCodes()
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
        private List<CodeValueView> _collCodeView;
        protected override void BuildPageCommon()
        {
            
            if (_controlCodeView == null)
            {
                _controlCodeView = new ControlList { Name = ExtentionString.CONTROL_COMMON_NAME };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlCodeView.View,
                                                       "DEFAULT_LISTVIEWCODEVALUEVIEW");
                _controlCodeView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlCodeView);
                _sourceBindCodes = new BindingSource();
                _collCodeView = SelectedItem.GetView(false);
                _sourceBindCodes.DataSource = _collCodeView;
                _controlCodeView.Grid.DataSource = _sourceBindCodes;
                _controlCodeView.View.ExpandAllGroups();

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_COMMON_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Новое значение
                BarButtonItem btnValueCreate = new BarButtonItem
                                                   {
                                                       ButtonStyle = BarButtonStyle.DropDown,
                                                       ActAsDropDown = true,
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnValueCreate);
                short docTypeId = EntityDocumentKind.ExtractEntityKind(SelectedItem.KindId);
                List<CodeName> collectionTemplates = SelectedItem.Workarea.GetCollection<CodeName>().FindAll(f => f.ToEntityId == SelectedItem.EntityId && f.DocTypeId == docTypeId);
                PopupMenu mnuTemplates = new PopupMenu { Ribbon = frmProp.ribbon };
                foreach (CodeName itemTml in collectionTemplates)
                {
                    // TODO:
                    //List<CodeNameEntityKind> collValues = CodeNameEntityKind.GetCollection(SelectedItem.Workarea, itemTml.Id);
                    //if (collValues.Exists(s => s.EntityKindId == SelectedItem.KindId))
                    //{
                    BarButtonItem btn = new BarButtonItem { Caption = itemTml.Name };
                    mnuTemplates.AddItem(btn);
                    btn.Tag = itemTml;
                    btn.ItemClick += delegate
                                         {
                                             CodeName objectTml = (CodeName)btn.Tag;
                                             CodeValue<Document> newCodeValue = new CodeValue<Document>
                                                                                    {
                                                                                        Workarea = SelectedItem.Workarea,
                                                                                        CodeNameId = objectTml.Id,
                                                                                        ElementId = SelectedItem.Id,
                                                                                        Element = SelectedItem
                                                                                    };
                                             newCodeValue.ShowProperty();
                                         };
                    //}
                }
                btnValueCreate.DropDownControl = mnuTemplates;

                #endregion
                #region Изменить
                BarButtonItem btnValueEdit = new BarButtonItem
                                                 {
                                                     Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                                                     RibbonStyle = RibbonItemStyles.Large,
                                                     Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                                 };
                groupLinksAction.ItemLinks.Add(btnValueEdit);
                btnValueEdit.ItemClick += delegate
                                              {
                                                  CodeValueView rowValue = _sourceBindCodes.Current as CodeValueView;
                                                  if (rowValue == null) return;

                                                  CodeValue<Document> value = rowValue.ConvertToCodeValue<Document>(SelectedItem);
                                                  value.Load(rowValue.Id);
                                                  value.ShowProperty();

                                              };
                #endregion

                page.Groups.Add(groupLinksAction);
                _controlCodeView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlCodeView.Grid;
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }


    internal sealed class BuildControlDocumentWorkFlowView : BasePropertyControlICore<Document>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlDocumentWorkFlowView()
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
        private List<DocumentWorkFlowView> _collCodeView;
        protected override void BuildPageCommon()
        {

            if (_controlCodeView == null)
            {
                _controlCodeView = new ControlList { Name = ExtentionString.CONTROL_COMMON_NAME };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlCodeView.View,
                                                       "DEFAULT_LISTVIEWCODEVALUEVIEW");
                _controlCodeView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlCodeView);
                _sourceBindCodes = new BindingSource();
                _collCodeView = DocumentWorkFlowView.GetView(SelectedItem);
                _sourceBindCodes.DataSource = _collCodeView;
                _controlCodeView.Grid.DataSource = _sourceBindCodes;
                _controlCodeView.View.ExpandAllGroups();

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_COMMON_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Новое значение
                BarButtonItem btnValueCreate = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = true,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                };
                groupLinksAction.ItemLinks.Add(btnValueCreate);
                btnValueCreate.ItemClick += delegate
                                                {
                                                    //SelectedItem.Workarea.Empty<Ruleset>().BrowseList();

                                                };

                #endregion
                #region Изменить
                BarButtonItem btnValueEdit = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                };
                groupLinksAction.ItemLinks.Add(btnValueEdit);
                btnValueEdit.ItemClick += delegate
                {
                    //CodeValueView rowValue = _sourceBindCodes.Current as CodeValueView;
                    //if (rowValue == null) return;

                    //CodeValue<Document> value = rowValue.ConvertToCodeValue<Document>(SelectedItem);
                    //value.Load(rowValue.Id);
                    //value.ShowProperty();

                };
                #endregion

                page.Groups.Add(groupLinksAction);
                _controlCodeView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlCodeView.Grid;
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}