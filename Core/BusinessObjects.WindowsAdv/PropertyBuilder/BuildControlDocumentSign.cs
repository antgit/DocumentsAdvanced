using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlDocumentSign : BasePropertyControlICore<DocumentSign>
    {

        public Agent Agent { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlDocumentSign() : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.AgentId = (int?)_common.lookUpAgent.EditValue ?? 0;
            SelectedItem.AgentSubId = (int?)_common.lookUpAgentSub.EditValue ?? 0;
            SelectedItem.AgentSignId = (int?)_common.lookUpAgentSign.EditValue ?? 0;
            SelectedItem.OrderNo = Convert.ToInt32(_common.txtOrder.Text);
            SelectedItem.ResolutionId = (int?) _common.lookUpResolution.EditValue ?? 0;
            SelectedItem.Date = _common.dateEdit.DateTime;
            if (_common.dateSignEdit.EditValue != null)
                SelectedItem.DateSign = _common.dateSignEdit.DateTime;
            else
                SelectedItem.DateSign = (DateTime?)null;
            SelectedItem.MessageNeed = _common.chkMessageNeed.Checked;
            SelectedItem.TaskNeed = _common.chkTaskNeed.Checked;
            SelectedItem.IsMain = _common.chkIsMain.Checked;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.GroupNo = (int)_common.edGroupNo.Value;

            if(_common.cmbDepatmentId.EditValue!=null)
                SelectedItem.DepatmentId = (int) _common.cmbDepatmentId.EditValue;
            else
                SelectedItem.DepatmentId = 0;

            if (_common.cmbAgentToId.EditValue != null)
                SelectedItem.AgentToId = (int)_common.cmbAgentToId.EditValue;
            else
                SelectedItem.AgentToId = 0;

            if (_common.cmbAgentToSubId.EditValue != null)
                SelectedItem.AgentToSubId = (int)_common.cmbAgentToSubId.EditValue;
            else
                SelectedItem.AgentToSubId = 0;

            if (_common.cmbGroupLevelId.EditValue != null)
                SelectedItem.GroupLevelId = (int)_common.cmbGroupLevelId.EditValue;
            else
                SelectedItem.GroupLevelId = 0;
            
            InternalSave();
        }

        ControlDocumentSign _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlDocumentSign
                {
                    Name = ExtentionString.CONTROL_COMMON_NAME,
                    txtOrder = {Text = SelectedItem.OrderNo.ToString() },
                    dateEdit = { DateTime = SelectedItem.Date },
                    
                    txtMemo = {Text = SelectedItem.Memo },
                    chkIsMain = {Checked = SelectedItem.IsMain },
                    chkMessageNeed = { Checked = SelectedItem.MessageNeed },
                    chkTaskNeed = { Checked = SelectedItem.TaskNeed },
                    Workarea = SelectedItem.Workarea
                };
                if (SelectedItem.DateSign.HasValue)
                    _common.dateSignEdit.DateTime = SelectedItem.DateSign.Value;
                else
                    _common.dateSignEdit.EditValue = (DateTime?)null;

                _common.edGroupNo.Value = SelectedItem.GroupNo;
                #region Agent
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.lookUpAgent, "DEFAULT_LOOKUP_NAME");
                int workresChainId = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
                List<Agent> workers = Agent.GetChainSourceList(SelectedItem.Workarea, Agent == null ? SelectedItem.Owner.MyCompanyId : Agent.Id, workresChainId);
                BindingSource bindingSourceAgent= new BindingSource
                {
                    DataSource = workers
                };
                _common.lookUpAgent.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.lookUpAgent.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.lookUpAgent.Properties.DataSource = bindingSourceAgent;
                _common.lookUpAgent.EditValue = SelectedItem.AgentId;
                _common.lookUpAgent.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.lookUpAgent.EditValue = 0;
                };
                #endregion

                #region AgentSub
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.lookUpAgentSub, "DEFAULT_LOOKUP_NAME");
                //int workresChainId = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
                BindingSource bindingSourceAgentSub = new BindingSource
                {
                    DataSource = workers
                };
                _common.lookUpAgentSub.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.lookUpAgentSub.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.lookUpAgentSub.Properties.DataSource = bindingSourceAgentSub;
                _common.lookUpAgentSub.EditValue = SelectedItem.AgentSubId;
                _common.lookUpAgentSub.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.lookUpAgentSub.EditValue = 0;
                };
                #endregion

                #region AgentSign
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.lookUpAgentSign, "DEFAULT_LOOKUP_NAME");
                //int workresChainId = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
                BindingSource bindingSourceAgentSign = new BindingSource
                {
                    DataSource = workers
                };
                _common.lookUpAgentSign.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.lookUpAgentSign.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.lookUpAgentSign.Properties.DataSource = bindingSourceAgentSign;
                _common.lookUpAgentSign.EditValue = SelectedItem.AgentSignId;
                _common.lookUpAgentSign.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.lookUpAgentSign.EditValue = 0;
                };
                #endregion

                #region Depatment
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbDepatmentId, "DEFAULT_LOOKUP_NAME");
                //int workresChainId = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
                BindingSource bindingSourceDepatment = new BindingSource
                {
                    DataSource = SelectedItem.Workarea.GetCollection<Depatment>()
                };
                _common.cmbDepatmentId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbDepatmentId.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbDepatmentId.Properties.DataSource = bindingSourceDepatment;
                _common.cmbDepatmentId.EditValue = SelectedItem.DepatmentId;
                #endregion

                #region GroupLevelId
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbGroupLevelId, "DEFAULT_LOOKUP_NAME");
                BindingSource bindingSourceGroupLevelId = new BindingSource();
                Hierarchy h = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_SIGNLEVEL);
                if (h != null)
                    bindingSourceGroupLevelId.DataSource = h.GetTypeContents<Analitic>();
                _common.cmbGroupLevelId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbGroupLevelId.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbGroupLevelId.Properties.DataSource = bindingSourceGroupLevelId;
                _common.cmbGroupLevelId.EditValue = SelectedItem.GroupLevelId;
                #endregion

                #region AgentTo
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbAgentToId, "DEFAULT_LOOKUP_NAME");
                //int workresChainId = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
                BindingSource bindingSourceAgentTo = new BindingSource
                {
                    DataSource = workers
                };
                _common.cmbAgentToId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbAgentToId.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbAgentToId.Properties.DataSource = bindingSourceAgentTo;
                _common.cmbAgentToId.EditValue = SelectedItem.AgentToId;
                _common.cmbAgentToId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbAgentToId.EditValue = 0;
                };
                #endregion

                #region AgentToSub
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbAgentToSubId, "DEFAULT_LOOKUP_NAME");
                //int workresChainId = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
                BindingSource bindingSourceAgentToSub = new BindingSource
                {
                    DataSource = workers
                };
                _common.cmbAgentToSubId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbAgentToSubId.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbAgentToSubId.Properties.DataSource = bindingSourceAgentToSub;
                _common.cmbAgentToSubId.EditValue = SelectedItem.AgentToSubId;
                _common.cmbAgentToSubId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbAgentToSubId.EditValue = 0;
                };
                #endregion

                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.lookUpResolution, "DEFAULT_LOOKUP_NAME");
                BindingSource bindingSourceResolution = new BindingSource
                {
                    DataSource = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SYSTEM_SIGN").GetTypeContents<Analitic>()
                };
                _common.lookUpResolution.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.lookUpResolution.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.lookUpResolution.Properties.DataSource = bindingSourceResolution;
                _common.lookUpResolution.EditValue = SelectedItem.ResolutionId;

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
