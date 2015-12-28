using System.Activities;
using BusinessObjects.Workflows.System;
using BusinessObjects.Workflows.Web;

namespace BusinessObjects.Workflows
{
    public static class WfCore
    {
        public static Activity FindByCodeInternal(string code)
        {
            code = code.ToUpper();
            Activity value = null;
            switch (code)
            {
                case "ACTIVITYSENDMESSAGEBYEVENT":
                    value = new ActivitySendMessageByEvent();
                    break;
                case "ACTIVITYTASKSETDONE":
                    value = new ActivityTaskSetDone();
                    break;
            }
            return value;
        }
        public static Activity FindByCodeInternal<T>(string code) where T : class, ICoreObject, new()
        {
            code = code.ToUpper();
            Activity value = null;
            switch (code)
            {
                default:
                    value = null;
                    break;
                    //case "ACTIVITYCHANGEDBOWNER":
                    //    value = new ActivityChangeDbOwner<T>();
                    //    break;
            }
            return value;
        }
        public static Activity FindByCodeInternalIBase<T>(string code) where T : class, IBase, new()
        {
            code = code.ToUpper();
            Activity value = null;
            value = FindByCodeInternal(code);
            switch (code)
            {
                case "ACTIVITYENTITYTYPEMETACODES":
                    value = new ActivityEntityTypeMetaCodes();
                    break;
                case "ACTIVITYENTITYTYPEMETAFILES":
                    value = new ActivityEntityTypeMetaFiles();
                    break;
                case "ACTIVITYENTITYTYPEMETAREPORTS":
                    value = new ActivityEntityTypeMetaReports();
                    break;
                    
                case "ACTIVITYENTITYTYPEMETAMAIN":
                    value = new ActivityEntityTypeMetaMain();
                    break;
                case "ACTIVITYENTITYTYPEMETACHAINS":
                    value = new ActivityEntityTypeMetaChains();
                    break;    
                    
                default:
                    value = null;
                    break;
                    
                    //case "FASTNOTECREATE":
                    //    value = new ActivityFastCreateNote<T>();
                    break;
            }
            return value;
        }
        /**/
    }
}