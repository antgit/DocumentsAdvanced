using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.DocumentLibrary.Controls;
using BusinessObjects.Documents;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.DocumentLibrary
{
    public static class DocumentExtention
    {
        /// <summary>
        /// Показать дополнительные свойства документа
        /// </summary>
        /// <param name="value">Документ</param>
        /// <returns></returns>
        public static Form ShowDocumentPropertiesAdvanced(this Document value)
        {
            FormProperties frm = new FormProperties();
            frm.ShowInTaskbar = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            
            ControlDocumentAdv ctl = new ControlDocumentAdv();
            frm.clientPanel.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            int maxWith = (frm.clientPanel.Width.CompareTo(ctl.MinimumSize.Width) > 0) ? frm.clientPanel.Width : ctl.MinimumSize.Width;
            int maxHeight = (frm.clientPanel.Height.CompareTo(ctl.MinimumSize.Height) > 0) ? frm.clientPanel.Height : ctl.MinimumSize.Height;
            Size mix = (frm.Size - frm.clientPanel.Size) + new Size(maxWith, maxHeight);
            frm.MinimumSize = mix;

            BindingSource signaturesBindingSource = new BindingSource();
            signaturesBindingSource.DataSource = value.Signs();
            ctl.GridLeftSign.DataSource = signaturesBindingSource;
            
            RibbonPageGroup groupLinksAction = frm.ribbon.Pages[0].Groups[0];

            ctl.ViewLeftSign.CustomRowFilter += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
            {
                if ((signaturesBindingSource.List[e.ListSourceRow] as DocumentSign).StateId == State.STATEDELETED)
                {
                    e.Visible = false;
                    e.Handled = true;
                }
            };
            #region Создать
            PopupMenu mnuCreate = new PopupMenu { Ribbon = frm.ribbon };
            List<EntityKind> entityKinds = value.Workarea.GetCollectionEntityKind((int)WhellKnownDbEntity.DocumentSign);
            foreach (EntityKind kind in entityKinds)
            {
                BarButtonItem btn = new BarButtonItem { Caption = kind.Name, Tag = kind, Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X16) };
                btn.ItemClick += delegate
                                     {
                                         EntityKind currKind = (EntityKind) btn.Tag;
                                         DocumentSign newSign = new DocumentSign()
                                                                    {
                                                                        Kind = currKind.Id,
                                                                        Date = DateTime.Now,
                                                                        OwnId = value.Id,
                                                                        Workarea = value.Workarea,
                                                                        StateId = State.STATEACTIVE
                                                                    };
                                         Analitic anStateDefault = value.Workarea.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>(Analitic.SYSTEM_SIGN_EMPTY);
                                         if (anStateDefault != null && anStateDefault.Id != 0)
                                             newSign.ResolutionId = anStateDefault.Id;
                                         newSign.Created += delegate
                                         {
                                             int position = signaturesBindingSource.Add(newSign);
                                             signaturesBindingSource.Position = position;
                                         };

                                         switch (currKind.SubKind)
                                         {
                                             case DocumentSign.KINDVALUE_FIRST:
                                                 newSign.ShowProperty(value.AgentDepartmentFrom);
                                                 break;
                                             case DocumentSign.KINDVALUE_SECOND:
                                                 newSign.ShowProperty(value.AgentDepartmentTo);
                                                 break;
                                         }
                                     };
                mnuCreate.ItemLinks.Add(btn);
            }

            BarButtonItem btnCreate = new BarButtonItem()
                                          {
                                              ButtonStyle = BarButtonStyle.DropDown,
                                              ActAsDropDown = true,
                                              Caption = value.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                              RibbonStyle = RibbonItemStyles.Large,
                                              Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32),
                                              DropDownControl = mnuCreate
                                          };
            groupLinksAction.ItemLinks.Add(btnCreate);
            #endregion

            #region Редактировать
            BarButtonItem btnEdit = new BarButtonItem
            {
                Caption = value.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(value.Workarea, ResourceImage.EDIT_X32)
            };
            btnEdit.ItemClick += delegate
            {
                DocumentSign current = signaturesBindingSource.Current as DocumentSign;
                if (current == null) return;
                current.ShowProperty();
            };
            groupLinksAction.ItemLinks.Add(btnEdit);
            #endregion

            #region Удалить
            BarButtonItem btnDelete = new BarButtonItem
                                        {
                                            Caption = value.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                            RibbonStyle = RibbonItemStyles.Large,
                                            Glyph = ResourceImage.GetByCode(value.Workarea, ResourceImage.DELETE_X32)
                                        };
            btnDelete.ItemClick += delegate
                                       {
                                           DocumentSign current = signaturesBindingSource.Current as DocumentSign;
                                           if (current == null) return;
                                           try
                                           {
                                               current.StateId = State.STATEDELETED;
                                               ctl.ViewLeftSign.RefreshData();
                                               //current.Delete(false);
                                               //signaturesBindingSource.Remove(current);
                                           }
                                           catch (DatabaseException dbe)
                                           {
                                               Extentions.ShowMessageDatabaseExeption(value.Workarea, value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                      "Ошибка удаления объекта!", dbe.Message, dbe.Id);
                                           }
                                           catch (Exception ex)
                                           {
                                               DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                                           }

                                           
                                           
                                       };
            groupLinksAction.ItemLinks.Add(btnDelete);
            #endregion

            #region Создать подписи по документу
            BarButtonItem btnmnuSelectSignTemplate = new BarButtonItem()
             {
                 ButtonStyle = BarButtonStyle.Default,
                 //ActAsDropDown = true,
                 Caption = "Заполнить подписи",
                 RibbonStyle = RibbonItemStyles.Large,
                 Glyph = ResourceImage.GetByCode(value.Workarea, ResourceImage.TRIANGLEGREEN_X32),
            };
            groupLinksAction.ItemLinks.Add(btnmnuSelectSignTemplate);
            btnmnuSelectSignTemplate.ItemClick += delegate
                                                      {
                                                          Document docSingTml = ShowDocumentListForSignatureTemplates(value);
                                                          if(docSingTml!=null)
                                                          {
                                                              foreach (DocumentSign sgnTml in docSingTml.Signs().Where(f => f.StateId == State.STATEACTIVE))
                                                              {
                                                                  DocumentSign newSign = new DocumentSign {Workarea = value.Workarea};
                                                                  newSign.CopyValue(sgnTml);
                                                                  newSign.OwnId = value.Id;
                                                                  signaturesBindingSource.Add(newSign);
                                                              }
                                                              ctl.ViewLeftSign.RefreshData();

                                                          }
                                                      };
            #endregion

            ctl.GridLeftSign.DoubleClick += delegate
                                                {
                                                    DocumentSign current = signaturesBindingSource.Current as DocumentSign;
                                                    if (current==null) return;
                                                    Point p = ctl.GridLeftSign.PointToClient(Control.MousePosition);
                                                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = ctl.ViewLeftSign.CalcHitInfo(p.X, p.Y);
                                                    if (!hit.InRow || ctl.ViewLeftSign.FocusedRowHandle <= -1) return;
                                                    current.ShowProperty();
                                                };

            // установка свойств в элементе управления
            ctl.edSummaTrans.Value = value.SummaTransaction;
            ctl.edSummaCountry.Value = value.SummaBase;
            ctl.chkAccounting.Checked = (value.Accounting == 1);

            //value.Workarea.CollectionEntityKinds.Where(s=>s.EntityId = WhellKnownDbEntity.DocumentSign)


            #region Данные для "Центр ответственности"
            ctl.cmbCfo.Properties.DisplayMember = GlobalPropertyNames.Name;
            ctl.cmbCfo.Properties.ValueMember = GlobalPropertyNames.Id;
            List<Analitic> CollectionCfo = new List<Analitic>();
            if (value.CfoId != 0)
            {
                CollectionCfo.Add(value.Cfo);
            }
            BindingSource BindSourceCfo = new BindingSource { DataSource = CollectionCfo };
            ctl.cmbCfo.Properties.DataSource = BindSourceCfo;
            DataGridViewHelper.GenerateGridColumns(value.Workarea, ctl.ViewCfo, "DEFAULT_LOOKUP_NAME");
            ctl.cmbCfo.EditValue = value.CfoId;
            ctl.cmbCfo.Properties.View.BestFitColumns();
            ctl.cmbCfo.Properties.PopupFormSize = new Size(ctl.cmbCfo.Width, 150);
            //ctl.ViewCfo.CustomUnboundColumnData += delegate { };
                //ViewManagerCustomUnboundColumnData;
            ctl.cmbCfo.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                                         {
                                             GridLookUpEdit cmb = sender as GridLookUpEdit;
                                             if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                                                 cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
                                             try
                                             {
                                                 ctl.Cursor = Cursors.WaitCursor;
                                                 if (cmb.Name == "cmbCfo" && BindSourceCfo.Count < 2)
                                                 {
                                                     Hierarchy rootMyCfo = value.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SYSTEM_CFO");
                                                     CollectionCfo = rootMyCfo.GetTypeContents<Analitic>();
                                                     BindSourceCfo.DataSource = CollectionCfo;
                                                 }
                                             }
                                             catch (Exception z)
                                             { }
                                             finally
                                             {
                                                 ctl.Cursor = Cursors.Default;
                                             }
                                         };
            ctl.cmbCfo.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                    ctl.cmbCfo.EditValue = 0;
            };
            #endregion
            //SYSTEM_WFACCOUNTING
            #region Данные для "Метод формирования бухгалтеских проводок"
            ctl.cmbAccounttingWf.Properties.DisplayMember = GlobalPropertyNames.Name;
            ctl.cmbAccounttingWf.Properties.ValueMember = GlobalPropertyNames.Id;
            List<Ruleset> CollectionAccounting = new List<Ruleset>();
            if (value.AccountingWfId != 0)
            {
                CollectionAccounting.Add(value.AccountingWf);
            }
            BindingSource BindSourceAccounting = new BindingSource { DataSource = CollectionAccounting };
            ctl.cmbAccounttingWf.Properties.DataSource = BindSourceAccounting;
            DataGridViewHelper.GenerateGridColumns(value.Workarea, ctl.ViewAccounting, "DEFAULT_LOOKUP_NAME");
            ctl.cmbAccounttingWf.EditValue = value.AccountingWfId;
            ctl.cmbAccounttingWf.Properties.View.BestFitColumns();
            ctl.cmbAccounttingWf.Properties.PopupFormSize = new Size(ctl.cmbAccounttingWf.Width, 150);
            //ctl.ViewCfo.CustomUnboundColumnData += delegate { };
            //ViewManagerCustomUnboundColumnData;
            ctl.cmbAccounttingWf.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
            {
                GridLookUpEdit cmb = sender as GridLookUpEdit;
                if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                    cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
                try
                {
                    ctl.Cursor = Cursors.WaitCursor;
                    if (BindSourceAccounting.Count < 2)
                    {
                        Hierarchy rootMyWf =
                            value.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SYSTEM_WFACCOUNTING");
                        CollectionAccounting = rootMyWf.GetTypeContents<Ruleset>();
                        BindSourceAccounting.DataSource = CollectionAccounting;
                    }
                }
                catch (Exception z)
                { }
                finally
                {
                    ctl.Cursor = Cursors.Default;
                }
            };
            ctl.cmbAccounttingWf.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                    ctl.cmbAccounttingWf.EditValue = 0;
            };
            #endregion

            #region Данные для "Валюта сделки"
            ctl.cmbCurrencyTrans.Properties.DisplayMember = GlobalPropertyNames.Name;
            ctl.cmbCurrencyTrans.Properties.ValueMember = GlobalPropertyNames.Id;
            List<Currency> CollectionCurrencyTrans = new List<Currency>();
            if (value.CurrencyTransactionId != 0)
            {
                CollectionCurrencyTrans.Add(value.CurrencyTransaction);
            }
            BindingSource BindSourceCurrencyTrans = new BindingSource { DataSource = CollectionCurrencyTrans };
            ctl.cmbCurrencyTrans.Properties.DataSource = BindSourceCurrencyTrans;
            DataGridViewHelper.GenerateGridColumns(value.Workarea, ctl.ViewCurrencyTrans, "DEFAULT_LOOKUP_NAME");
            ctl.cmbCurrencyTrans.EditValue = value.CurrencyTransactionId;
            ctl.cmbCurrencyTrans.Properties.View.BestFitColumns();
            ctl.cmbCurrencyTrans.Properties.PopupFormSize = new Size(ctl.cmbCurrencyTrans.Width, 150);
            //ctl.ViewCfo.CustomUnboundColumnData += delegate { };
            //ViewManagerCustomUnboundColumnData;
            ctl.cmbCurrencyTrans.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
            {
                GridLookUpEdit cmb = sender as GridLookUpEdit;
                if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                    cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
                try
                {
                    ctl.Cursor = Cursors.WaitCursor;
                    if (BindSourceCurrencyTrans.Count < 2)
                    {
                        CollectionCurrencyTrans = value.Workarea.GetCollection<Currency>();
                        BindSourceCurrencyTrans.DataSource = CollectionCurrencyTrans;
                    }
                }
                catch (Exception z)
                { }
                finally
                {
                    ctl.Cursor = Cursors.Default;
                }
            };
            ctl.cmbCurrencyTrans.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                    ctl.cmbCurrencyTrans.EditValue = 0;
            };
            #endregion

            #region Данные для "Валюта страны"
            ctl.cmbCurrencyCountry.Properties.DisplayMember = GlobalPropertyNames.Name;
            ctl.cmbCurrencyCountry.Properties.ValueMember = GlobalPropertyNames.Id;
            List<Currency> CollectionCurrencyCountry = new List<Currency>();
            if (value.CurrencyBaseId != 0)
            {
                CollectionCurrencyCountry.Add(value.CurrencyBase);
            }
            BindingSource BindSourceCurrencyCountry = new BindingSource { DataSource = CollectionCurrencyCountry };
            ctl.cmbCurrencyCountry.Properties.DataSource = BindSourceCurrencyCountry;
            DataGridViewHelper.GenerateGridColumns(value.Workarea, ctl.ViewCurrencyCountry, "DEFAULT_LOOKUP_NAME");
            ctl.cmbCurrencyCountry.EditValue = value.CurrencyBaseId;
            ctl.cmbCurrencyCountry.Properties.View.BestFitColumns();
            ctl.cmbCurrencyCountry.Properties.PopupFormSize = new Size(ctl.cmbCurrencyCountry.Width, 150);
            //ctl.ViewCfo.CustomUnboundColumnData += delegate { };
            //ViewManagerCustomUnboundColumnData;
            ctl.cmbCurrencyCountry.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
            {
                GridLookUpEdit cmb = sender as GridLookUpEdit;
                if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                    cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
                try
                {
                    ctl.Cursor = Cursors.WaitCursor;
                    if (BindSourceCurrencyCountry.Count < 2)
                    {
                        CollectionCurrencyCountry = value.Workarea.GetCollection<Currency>();
                        BindSourceCurrencyCountry.DataSource = CollectionCurrencyCountry;
                    }
                }
                catch (Exception z)
                { }
                finally
                {
                    ctl.Cursor = Cursors.Default;
                }
            };
            ctl.cmbCurrencyCountry.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                    ctl.cmbCurrencyCountry.EditValue = 0;
            };
            #endregion

            frm.btnSave.ItemClick += delegate
                                         {
                                             value.CurrencyTransactionId = (int)ctl.cmbCurrencyTrans.EditValue;
                                             value.CurrencyBaseId= (int)ctl.cmbCurrencyCountry.EditValue;
                                             value.AccountingWfId = (int)ctl.cmbAccounttingWf.EditValue;
                                             value.CfoId = (int)ctl.cmbCfo.EditValue;
                                             if (ctl.chkAccounting.Checked)
                                                 value.Accounting = 1;
                                             else
                                                 value.Accounting = 0;
                                             value.SummaBase = ctl.edSummaCountry.Value;
                                             value.SummaTransaction = ctl.edSummaTrans.Value;
                                         };
            if (!value.IsNew && value.IsReadOnly)
            {
                ctl.Enabled = false;
                btnCreate.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                frm.btnSave.Enabled = false;
            }
            frm.Shown += delegate
                             {
                                 ctl.ViewLeftSign.RefreshData();
                             };
            frm.ShowDialog();
            
            return frm;
        }

        public static Document ShowDocumentListForSignatureTemplates(this Document value)
        {
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(value.Workarea.Empty<Document>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            frm.ShowInTaskbar = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;

            ListBrowserBaseObjects<Document> browser = new ListBrowserBaseObjects<Document>(value.Workarea,
                                                                              DocumentSign.
                                                                                  GetCollectionDocumentSignTemplates(
                                                                                      value.Workarea, value.KindId),
                                                                              null, null, false, true, false, false) { Owner = frm };

            browser.Owner = frm;
            browser.Build();
            frm.clientPanel.Controls.Add(browser.ListControl);
            browser.ListControl.Dock = DockStyle.Fill;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnProp.Visibility = BarItemVisibility.Never;
            frm.btnCreate.Visibility = BarItemVisibility.Never;
            frm.btnDelete.Visibility = BarItemVisibility.Never;
            frm.btnSelect.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Glyph = ResourceImage.GetByCode(value.Workarea, ResourceImage.SELECT_X32);
            List<Document> returnValue = new List<Document>();
            frm.btnSelect.ItemClick += delegate
            {
                returnValue = browser.SelectedValues;
                frm.Close();
            };
            frm.btnClose.ItemClick += delegate
            {
                returnValue = null;
                frm.Close();

            };
            frm.ShowDialog();

            if (returnValue != null && returnValue.Count>0)
                return returnValue[0];
            return null;
        }
        /// <summary>
        /// Показать отчет о системной информации документа
        /// </summary>
        /// <param name="value"></param>
        public static void ShowDocumentInfoReports(this Document value)
        {
            string findCode = string.Format("SYSTEM_REP_DOCUMENT_INFO{0}", value.GetTypeValue());
            Library rep = value.Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(findCode);
            if (rep != null)
                rep.ShowReport(value);
        }
    }
}