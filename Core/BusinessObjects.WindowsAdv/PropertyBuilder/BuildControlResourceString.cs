using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.Utils.Zip;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraSpellChecker;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlResourceString : BasePropertyControlICore<ResourceString>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlResourceString()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Value = _common.txtMemo.Text;
            SelectedItem.Memo = _common.txtName.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CultureId = (int)_common.cmbCulture.EditValue;
            //SelectedItem.SubKind = (short)_common.numIntCode.Value;
            
            SaveStateData();

            InternalSave();
        }

        ControlResourceString _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlResourceString
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Memo },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtMemo = {Text = SelectedItem.Value},
                                  Workarea = SelectedItem.Workarea
                              };

                BindingSource bindCulture = new BindingSource
                                                {
                                                    DataSource =
                                                        CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures)
                                                };
                _common.cmbCulture.Properties.DataSource = bindCulture;
                _common.cmbCulture.Properties.DisplayMember = "DisplayName";
                _common.cmbCulture.Properties.ValueMember = "LCID";
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbCulture, "DEFAULT_LISTVIEWCULTURE");
                //
                //CultureInfo[] allCultures = CultureInfo.GetCultures(CultureTypes.FrameworkCultures);
                //allCultures[0].
                _common.cmbCulture.EditValue = SelectedItem.CultureId;

                #region Проверка орфографии
                BarButtonItem btnSpellCheck = new BarButtonItem
                {
                    Caption = "Орфография",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.SPELLCHECK_X32)
                };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnSpellCheck);
                btnSpellCheck.ItemClick += delegate
                {
                    _common.spellChecker1.Culture = CultureInfo.InvariantCulture;
                    _common.spellChecker1.UseSharedDictionaries = false;
                    //_common.spellChecker1.SpellingFormType = SpellingFormType.Word;
                    LoadDictionary("DICTIONARY_RU", new CultureInfo("ru-RU"));
                    LoadDictionary("DICTIONARY_EN", new CultureInfo("en-US"));
                    _common.spellChecker1.Check(_common.txtMemo);
                };
                #endregion
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
        private void LoadDictionary(string dictionaryRu, CultureInfo culture)
        {
            //DICTIONARY_EN
            //DICTIONARY_RU
            FileData fileDictionary = SelectedItem.Workarea.Cashe.GetCasheData<FileData>().ItemCode<FileData>(dictionaryRu);
            if (fileDictionary != null)
            {
                HunspellDictionary result = new HunspellDictionary();
                Stream zipFileStream = new MemoryStream(fileDictionary.StreamData);
                ZipFileCollection files = ZipArchive.Open(zipFileStream);
                try
                {
                    result.LoadFromStream(GetFileStream(files, ".dic"), GetFileStream(files, ".aff"));
                }
                catch
                {
                }
                finally
                {
                    zipFileStream.Dispose();
                    DisposeZipFileStreams(files);
                }
                result.Culture = culture;
                _common.spellChecker1.Dictionaries.Add(result);
            }
        }

        private Stream GetFileStream(ZipFileCollection files, string name)
        {
            foreach (ZipFile file in files)
            {
                if (file.FileName.IndexOf(name) >= 0)
                    return file.FileDataStream;
            }
            return null;
        }

        private void DisposeZipFileStreams(ZipFileCollection files)
        {
            foreach (ZipFile file in files)
                file.FileDataStream.Dispose();
        }
    }
}