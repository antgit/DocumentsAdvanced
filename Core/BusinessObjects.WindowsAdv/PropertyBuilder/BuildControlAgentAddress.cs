using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств адреса корреспондента
    /// </summary>
    internal sealed class BuildControlAgentAddress : BasePropertyControlIBase<AgentAddress>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlAgentAddress()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.cmbStreet.Text;
            
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.CountryId = (int)_common.cmbCountry.EditValue;
            SelectedItem.TerritoryId = (int)_common.cmbTerritory.EditValue;
            SelectedItem.RegionId = (int)_common.cmbRegion.EditValue;
            SelectedItem.TownId = (int)_common.cmbTown.EditValue;
            SelectedItem.PostIndex = _common.txtIndex.Text;
            string nameFull=string.Empty;
            if (SelectedItem.Country!=null)
            {
                nameFull = SelectedItem.Country.Name;
            }
            if(SelectedItem.Territory!=null)
            {
                if (nameFull.Length > 0)
                    nameFull = nameFull + ", ";
                nameFull = nameFull  + SelectedItem.Territory.Name;
            }
            if (SelectedItem.Town != null)
            {
                if (nameFull.Length > 0)
                    nameFull = nameFull + ", ";
                nameFull = nameFull + SelectedItem.Town.Name;
            }
            if (!string.IsNullOrEmpty(SelectedItem.Name))
            {
                if (nameFull.Length > 0)
                    nameFull = nameFull + ", ";
                nameFull = nameFull + SelectedItem.Name;
            }
            if (!string.IsNullOrEmpty(SelectedItem.Code))
            {
                if (nameFull.Length > 0)
                    nameFull = nameFull + ", ";
                nameFull = nameFull + SelectedItem.Code;
            }
            SelectedItem.NameFull = nameFull;
            _common.txtNameFull2.Text = nameFull;
            SaveStateData();

            InternalSave();
        }

        BindingSource _bindingSourceCountry;
        List<Country> _collCountry;

        BindingSource _bindingSourceTerritory;
        List<Territory> _collTerritory;

        BindingSource _bindingSourceRegion;
        List<Territory> _collRegion;

        BindingSource _bindingSourceTown;
        List<Town> _collTown;

        ControlGeoLocation _ccl;
        ControlAgentAdress _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlAgentAdress
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  //txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = {Text = SelectedItem.NameFull},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = {Text = SelectedItem.CodeFind},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  Workarea = SelectedItem.Workarea,
                                  txtIndex = {Text = SelectedItem.PostIndex}
                              };

                //_common.layoutControlItemName.Text = "Улица:";
                //_common.layoutControlItemCode.Text = "Дом:";
                _common.cmbStreet.Text = SelectedItem.Name;
                _common.txtNameFull2.Properties.ReadOnly = true;

                #region Данные для списка "Страна"
                _common.cmbCountry.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbCountry.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceCountry = new BindingSource();
                _collCountry = new List<Country>();
                if (SelectedItem.CountryId != 0)
                    _collCountry.Add(SelectedItem.Workarea.Cashe.GetCasheData<Country>().Item(SelectedItem.CountryId));
                _bindingSourceCountry.DataSource = _collCountry;
                _common.cmbCountry.Properties.DataSource = _bindingSourceCountry;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbCountry, CustomViewList.DEFAULT_LOOKUP_NAME);
                _common.cmbCountry.EditValue = SelectedItem.CountryId;
                _common.cmbCountry.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbCountry.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbCountry.EditValue = 0;
                };
                _common.cmbCountry.EditValueChanged += new EventHandler(cmbCountry_EditValueChanged);

                _common.cmbCountry.ButtonClick +=
                        delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                        {
                            if (e.Button.Index == 1)
                            {
                                if (_common.cmbCountry.EditValue != null)
                                {
                                    Country t = SelectedItem.Workarea.Cashe.GetCasheData<Country>().Item((int)_common.cmbCountry.EditValue);
                                    t.ShowOnGoogleMap();
                                }
                            }
                        };
                #endregion

                #region Данные для списка "Область"
                _common.cmbTerritory.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbTerritory.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceTerritory = new BindingSource();
                _collTerritory = new List<Territory>();
                if (SelectedItem.TerritoryId != 0)
                    _collTerritory.Add(SelectedItem.Workarea.Cashe.GetCasheData<Territory>().Item(SelectedItem.TerritoryId));
                _bindingSourceTerritory.DataSource = _collTerritory;
                _common.cmbTerritory.Properties.DataSource = _bindingSourceTerritory;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbTerritory, CustomViewList.DEFAULT_LOOKUP_NAME);
                _common.cmbTerritory.EditValue = SelectedItem.TerritoryId;
                _common.cmbTerritory.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbTerritory.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbTerritory.EditValue = 0;
                };
                _common.cmbTerritory.EditValueChanged += new EventHandler(cmbTerritory_EditValueChanged);
                _common.cmbTerritory.ButtonClick +=
                    delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                    {
                        if(e.Button.Index==1)
                        {
                            if (_common.cmbTerritory.EditValue != null)
                            {
                                Territory t = SelectedItem.Workarea.Cashe.GetCasheData<Territory>().Item((int)_common.cmbTerritory.EditValue);
                                t.ShowOnGoogleMap();
                            }
                        }
                    };
                #endregion
                #region Данные для списка "Район"
                _common.cmbRegion.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbRegion.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceRegion = new BindingSource();
                _collRegion = new List<Territory>();
                if (SelectedItem.RegionId != 0)
                    _collRegion.Add(SelectedItem.Workarea.Cashe.GetCasheData<Territory>().Item(SelectedItem.RegionId));
                _bindingSourceRegion.DataSource = _collRegion;
                _common.cmbRegion.Properties.DataSource = _bindingSourceRegion;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbRegion, CustomViewList.DEFAULT_LOOKUP_NAME);
                _common.cmbRegion.EditValue = SelectedItem.RegionId;
                _common.cmbRegion.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbRegion.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbRegion.EditValue = 0;
                };
                // TODO: дополнительная фильтрация городов по районам
                //_common.cmbRegion.EditValueChanged += new EventHandler(cmbRegion_EditValueChanged);
                _common.cmbRegion.ButtonClick +=
                    delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                    {
                        if (e.Button.Index == 1)
                        {
                            if (_common.cmbTerritory.EditValue != null)
                            {
                                Territory t = SelectedItem.Workarea.Cashe.GetCasheData<Territory>().Item((int)_common.cmbRegion.EditValue);
                                t.ShowOnGoogleMap();
                            }
                        }
                    };
                #endregion

                #region Данные для списка "Город"
                _common.cmbTown.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbTown.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceTown = new BindingSource();
                _collTown = new List<Town>();
                if (SelectedItem.TownId != 0)
                    _collTown.Add(SelectedItem.Workarea.Cashe.GetCasheData<Town>().Item(SelectedItem.TownId));
                _bindingSourceTown.DataSource = _collTown;
                _common.cmbTown.Properties.DataSource = _bindingSourceTown;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbTown, CustomViewList.DEFAULT_LOOKUP_NAME);
                _common.cmbTown.EditValue = SelectedItem.TownId;
                _common.cmbTown.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbTown.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbTown.EditValue = 0;
                };
                _common.cmbTown.ButtonClick +=
                    delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                        {
                            if (e.Button.Index == 1)
                            {
                                if (_common.cmbTown.EditValue != null)
                                {
                                    Town t =SelectedItem.Workarea.Cashe.GetCasheData<Town>().Item((int) _common.cmbTown.EditValue);
                                    t.ShowOnGoogleMap();
                                }
                            }
                        };
                #endregion
                
                _common.cmbStreet.QueryPopUp += delegate
                                                    {
                                                        if (_common.cmbStreet.Properties.Items.Count > 1)
                                                            return;
                                                        if (_common.cmbTown.EditValue != null)
                                                        {
                                                            Town t =
                                                                SelectedItem.Workarea.Cashe.GetCasheData<Town>().Item(
                                                                    (int)_common.cmbTown.EditValue);
                                                            List<string> street = t.GetStreets();
                                                            if (!street.Contains(_common.cmbStreet.Text))
                                                                street.Add(_common.cmbStreet.Text);
                                                            _common.cmbStreet.Properties.Items.AddRange(street.ToArray());
                                                        }
                                                    };

                PopupContainerControl pcc = new PopupContainerControl();
                _ccl = new ControlGeoLocation();
                pcc.Controls.Add(_ccl);
                pcc.Size = new Size(_common.ppcLocation.Size.Width, _ccl.Size.Height);
                _ccl.Dock = DockStyle.Fill;
                _ccl.seLocationX.Value = SelectedItem.X;
                _ccl.seLocationY.Value = SelectedItem.Y;
                _common.ppcLocation.Closed += delegate
                {
                    SelectedItem.X = _ccl.seLocationX.Value;
                    SelectedItem.Y = _ccl.seLocationY.Value;
                    _common.ppcLocation.Text = SelectedItem.X + ";" + SelectedItem.Y;
                };
                _common.ppcLocation.Text = SelectedItem.X + ";" + SelectedItem.Y;
                _common.ppcLocation.Properties.PopupControl = pcc;
                _common.ppcLocation.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 1)
                    {
                        SelectedItem.ShowOnGoogleMap();
                    }
                };

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


        

        void cmbCountry_EditValueChanged(object sender, EventArgs e)
        {
            _collTerritory = new List<Territory>();
            _bindingSourceTerritory.DataSource = _collTerritory;
            _collTown = new List<Town>();
            _bindingSourceTown.DataSource = _collTown;
        }

        void cmbTerritory_EditValueChanged(object sender, EventArgs e)
        {
            int territotyId = (int)_common.cmbTerritory.EditValue;
            if (territotyId != 0)
            {
                _collTown = SelectedItem.Workarea.GetCollection<Town>().Where(s => s.TerritoryId == territotyId).ToList();
                Territory selTerr = SelectedItem.Workarea.GetObject<Territory>(territotyId);
                int chainId = SelectedItem.Workarea.CollectionChainKinds.First(
                    s =>
                    s.FromEntityId == selTerr.EntityId && s.ToEntityId == selTerr.EntityId &&
                    s.Code == ChainKind.TREE).Id;
                _collRegion = (selTerr as IChains<Territory>).SourceList(chainId);
                _bindingSourceRegion.DataSource = _collRegion;
            }
            else
            {
                _collTown = new List<Town>();
            }
            _bindingSourceTown.DataSource = _collTown;
        }
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LookUpEdit cmb = sender as LookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbCountry" && _bindingSourceCountry.Count < 2)
                {
                    _collCountry = SelectedItem.Workarea.GetCollection<Country>();
                    _bindingSourceCountry.DataSource = _collCountry;
                }
                else if (cmb.Name == "cmbTerritory" && _bindingSourceTerritory.Count < 2)
                {
                    int countryId = (int) _common.cmbCountry.EditValue;
                    if (countryId!=0)
                    {
                        _collTerritory = SelectedItem.Workarea.GetCollection<Territory>().Where(s => s.CountryId == countryId && s.KindValue == Territory.KINDVALUE_REGION).ToList();
                    }
                    else
                    {
                        _collTerritory = new List<Territory>();
                    }
                    _bindingSourceTerritory.DataSource = _collTerritory;
                }
                else if (cmb.Name == "cmbRegion" && _bindingSourceRegion.Count < 2)
                {
                    int territoryId = (int)_common.cmbTerritory.EditValue;
                    if (territoryId != 0)
                    {
                        Territory selTerr = SelectedItem.Workarea.GetObject<Territory>(territoryId);
                        int chainId = SelectedItem.Workarea.CollectionChainKinds.First(
                            s =>
                            s.FromEntityId == selTerr.EntityId && s.ToEntityId == selTerr.EntityId &&
                            s.Code == ChainKind.TREE).Id;
                        _collRegion = (selTerr as IChains<Territory>).SourceList(chainId);
                    }
                    else
                    {
                        _collRegion = new List<Territory>();
                    }
                    _bindingSourceRegion.DataSource = _collRegion;
                }
                else if (cmb.Name == "cmbTown" && _bindingSourceTown.Count < 2)
                {
                    int territotyId = (int)_common.cmbTerritory.EditValue;
                    if(territotyId!=0)
                    {
                        //List<Town> _collTown1 = SelectedItem.Workarea.GetCollection<Town>().Where(s => s.TerritoryId == territotyId).ToList();
                        _collTown = SelectedItem.Workarea.Empty<Town>().FindBy(count: int.MaxValue, territoryId: territotyId,  filter: s=>!s.IsTemplate);
                    }
                    else
                    {
                        _collTown = new List<Town>();
                    }
                    _bindingSourceTown.DataSource = _collTown;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
    }
}