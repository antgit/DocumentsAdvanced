using System.Activities;
using BusinessObjects.Workflows;
namespace BusinessObjects.Windows.Workflows
{
    public static class WfCore
    {
        public static Activity FindByCodeInternal(string code)
        {
            code = code.ToUpper();
            Activity value = null;
            switch (code)
            {

                case "ACTIVITYAGENTCHANGECOMPANYOWNER":
                    value = new ActivityAgentChangeCompanyOwner();
                    break;
                case "ACTIVITYPRODUCTCHANGECOMPANYOWNER":
                    value = new ActivityProductChangeCompanyOwner();
                    break;
                case "ACTIVITYKNOWLEDGECHANGECOMPANYOWNER":
                    value = new ActivityKnowledgeChangeCompanyOwner();
                    break;
                case "ACTIVITYANALITICCHANGECOMPANYOWNER":
                    value = new ActivityAnaliticChangeCompanyOwner();
                    break;
                case "ACTIVITYBRANCHEADDLINKEDSERVER":
                    value = new ActivityBrancheAddLinkedServer();
                    break;
                
                case "ACTIVITYKNOWELEDGEMOVEDATABASE":
                    value = new ActivityKnoweledgeMoveDatabase();
                    break;
                case "ACTIVITYEXCHANGESENDTASKTODATABASE":
                    value = new ActivityExchangeSendTaskToDatabase();
                    break;
                case "ACTIVITYEXCHANGESENDBRANCHETODATABASE":
                    value = new ActivityExchangeSendBrancheToDatabase();
                    break;
                case "ACTIVITYCHANGEUIDCOMPANYOWNER":
                    value = new ActivityChangeUidCompanyOwner();
                    break;

                case "ACTIVITYCHANGEPRICENAMECOMPANYOWNER":
                    value = new ActivityChangePriceNameCompanyOwner();
                    break;
                case "ACTIVITYCHANGEACCOUNTCOMPANYOWNER":
                    value = new ActivityChangeAccountCompanyOwner();
                    break;
                case "ACTIVITYCHANGECODENAMECOMPANYOWNER":
                    value = new ActivityChangeCodeNameCompanyOwner();
                    break;
                case "ACTIVITYCHANGELIBRARYCOMPANYOWNER":
                    value = new ActivityChangeLibraryCompanyOwner();
                    break;
                case "ACTIVITYCHANGERULESETCOMPANYOWNER":
                    value = new ActivityChangeRulesetCompanyOwner();
                    break;
                case "ACTIVITYCHANGEEVENTCOMPANYOWNER":
                    value = new ActivityChangeEventCompanyOwner();
                    break;
                case "ACTIVITYCHANGETASKCOMPANYOWNER":
                    value = new ActivityChangeTaskCompanyOwner();
                    break;
                case "ACTIVITYCHANGEKNOWLEDGECOMPANYOWNER":
                    value = new ActivityChangeKnowledgeCompanyOwner();
                    break;
                case "ACTIVITYCHANGEMESSAGECOMPANYOWNER":
                    value = new ActivityChangeMessageCompanyOwner();
                    break;
                case "ACTIVITYCHANGENOTECOMPANYOWNER":
                    value = new ActivityChangeNoteCompanyOwner();
                    break;
                case "ACTIVITYCHANGETIMEPERIODCOMPANYOWNER":
                    value = new ActivityChangeTimePeriodCompanyOwner();
                    break;
                case "ACTIVITYCHANGEFILEDATACOMPANYOWNER":
                    value = new ActivityChangeFileDataCompanyOwner();
                    break;
                case "ACTIVITYCHANGEDATACATALOGCOMPANYOWNER":
                    value = new ActivityChangeDataCatalogCompanyOwner();
                    break;
                case "ACTIVITYSHOWANALITICUSAGE":
                    value = new ActivityShowAnaliticUsage();
                    break;      
                    
                    
                    
            }
            if(value==null)
            {
                value = BusinessObjects.Workflows.WfCore.FindByCodeInternal(code);
            }
            return value;
        }
        public static Activity FindByCodeInternal<T>(string code) where T : class, ICoreObject, new()
        {
            code = code.ToUpper();
            Activity value = null;
            switch (code)
            {
                case "ACTIVITYCHANGEDBOWNER":
                    value = new ActivityChangeDbOwner<T>();
                    break;
                    
            }
            if(value==null)
            {
                value = BusinessObjects.Workflows.WfCore.FindByCodeInternal<T>(code);
            }
            return value;
        }
        public static Activity FindByCodeInternalIBase<T>(string code) where T: class, IBase, new()
        {
            code = code.ToUpper();
            Activity value = null;
            value = FindByCodeInternal(code);
            switch (code)
            {
                case "FASTNOTECREATE":
                    value = new ActivityFastCreateNote<T>();
                    break;
            }
            if (value == null)
                value = BusinessObjects.Workflows.WfCore.FindByCodeInternalIBase<T>(code);
            return value;
        }
        /**/
    }
}
