using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>��������� ������� "��������"</summary>
    internal struct HierarchyStruct
    {
        /// <summary>����������� ���������� � ���� �����</summary>
        public int ContentFlags;
        /// <summary>������� �����������</summary>
        public bool HasContents;
        /// <summary>������������� ��������</summary>
        public int ParentId;
        /// <summary>������ ����</summary>
        public string Path;
        /// <summary>������������� �����</summary>
        public int RootId;
        /// <summary>������������� ������</summary>
        public int ViewListId;
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId;
    }
    /// <summary>��� ���������� ���� ��� ��������</summary>
    public enum HierarchyCodeKind
    {
        /// <summary>��� ������</summary>
        All,
        /// <summary>����������� � �������������</summary>
        Deny,
        /// <summary>���������</summary>
        Trash,
        /// <summary>�� �������� � ������</summary>
        NotInGroup,
        /// <summary>���������</summary>
        System,
        /// <summary>������� �������������</summary>
        NotDone,
        /// <summary>�������</summary>
        Template,
        /// <summary>�������� ������ ������</summary>
        FindRoot
    }
    /// <summary>��������</summary>
    public sealed class Hierarchy : BaseCore<Hierarchy>, IChains<Hierarchy>, IEquatable<Hierarchy>, 
        IChainsAdvancedList<Hierarchy, Agent>,
        IChainsAdvancedList<Hierarchy, Note>,
        ICodes<Hierarchy>, ICompanyOwner
    {
// ReSharper disable InconsistentNaming
        /// <summary>��� �������� ��� �������� ������� ��������� ������ ��� �������������</summary>
        public const string SYSTEM_RULESET_AUTONUMMETHOD = "SYSTEM_RULESET_AUTONUMMETHOD";

        /// <summary>��� �������� ��� �������� ������ ���� ������</summary>
        public const string SYSTEM_FILEDATA_KNOWLEDGE = "SYSTEM_FILEDATA_KNOWLEDGE";
        /// <summary>��� �������� ��� �������� ������ ���������� ���� ������</summary>
        public const string SYSTEM_FILEDATA_SQLUPDATE = "SYSTEM_FILEDTA_SQLUPDATE";
        /// <summary>��� �������� �������� ��� �������� ������ ������� ������</summary>
        public const string SYSTEM_EXCFILEDATA_ROOT = "SYSTEM_EXCFILEDATA_ROOT";
        /// <summary>��� �������� ��� �������� ������ ������� ������ ��� ��������� ���������</summary>
        public const string SYSTEM_EXCFILEDATA_MOBILESALES = "SYSTEM_EXCFILEDATA_MOBILESALES";
        /// <summary>��� �������� ��� �������� ������ ������ �����</summary>
        public const string SYSTEM_FILEDATA_COUNTRYEMBLEMS = "SYSTEM_FILEDATA_COUNTRYEMBLEMS";
        /// <summary>��� �������� "����������� �������"</summary>
        public const string SYSTEM_FILEDATA_LOGOPRODUCT = "SYSTEM_FILEDATA_LOGOPRODUCT";
        /// <summary>��� �������� "�������� ��������"</summary>
        public const string SYSTEM_FILEDATA_LOGOAGENT = "SYSTEM_FILEDATA_LOGOAGENT";
        /// <summary>��� �������� "����� �������"</summary>
        public const string SYSTEM_FILEDATA_TOWNEMBLEMS = "SYSTEM_FILEDATA_TOWNEMBLEMS";
        
        

        /// <summary>��� �������� �������� ��� �������� ��������������</summary>
        public const string SYSTEM_AGENT_MANUFACTURERS = "SYSTEM_AGENT_MANUFACTURERS";
        /// <summary>��� �������� �������� ��� �������� ��������</summary>
        public const string SYSTEM_AGENT_BUYERS = "SYSTEM_AGENT_BUYERS";
        /// <summary>��� �������� �������� ��� �������� ����������� ��������� � �����������</summary>
        public const string SYSTEM_AGENT_MYCOMPANY = "SYSTEM_AGENT_MYCOMPANY";
        /// <summary>��� �������� �������� ��� �������� ����������� �����������</summary>
        public const string SYSTEM_AGENT_MYDEPATMENTS = "SYSTEM_AGENT_MYDEPATMENTS";
        /// <summary>��� �������� �������� ��� �������� ����������� �����������</summary>
        public const string SYSTEM_AGENT_MYWORKERS = "SYSTEM_AGENT_MYWORKERS";
        /// <summary>��� �������� �������� ��� �������� ����������� �������</summary>
        public const string SYSTEM_AGENT_MYSTORES = "SYSTEM_AGENT_MYSTORES";
        /// <summary>��� �������� �������� ��� �������� �����������</summary>
        public const string SYSTEM_AGENT_SUPPLIERS = "SYSTEM_AGENT_SUPPLIERS";
        /// <summary>��� �������� �������� ��� �������� ���������� ������</summary>
        public const string SYSTEM_AGENT_BANKASSOCIATION = "SYSTEM_AGENT_BANKASSOCIATION";
        

        /// <summary>��� �������� �������� ��� �������� ������</summary>
        public const string SYSTEM_AGENT_BANKS = "SYSTEM_AGENT_BANKS";
        /// <summary>��� �������� �������� ��� �������� ��������������� �������</summary>
        public const string SYSTEM_AGENT_GOVENMENT = "SYSTEM_AGENT_GOVENMENT";
        /// <summary>��� �������� �������� ��� �������� ���������� �� ������</summary>
        public const string SYSTEM_AGENT_JOBCANDIDATES = "SYSTEM_AGENT_JOBCANDIDATES";
        /// <summary>��� �������� �������� ��� �������� �����������</summary>
        public const string SYSTEM_AGENT_COMPETITOR = "SYSTEM_AGENT_COMPETITOR";

        /// <summary>��� �������� �������� ��� �������� �������</summary>
        public const string SYSTEM_PRODUCTS = "SYSTEM_PRODUCTS";
        /// <summary>��� �������� �������� ��� �������� �������� �������</summary>
        public const string SYSTEM_PRODUCT_ELIMINATED = "SYSTEM_PRODUCT_ELIMINATED";
        /// <summary>��� �������� �������� ��� �������� ������� �������</summary>
        public const string SYSTEM_PRODUCT_CURRENT = "SYSTEM_PRODUCT_CURRENT";
        /// <summary>��� �������� �������� ��� �������� �����</summary>
        public const string SYSTEM_PRODUCT_SERVICE = "SYSTEM_PRODUCT_SERVICE";
        /// <summary>��� �������� �������� ��� �������� �������� �������</summary>
        public const string SYSTEM_PRODUCT_ASSETS = "SYSTEM_PRODUCT_ASSETS";
        /// <summary>��� �������� �������� ��� �������� �����</summary>
        public const string SYSTEM_PRODUCT_RAWMATERIALS = "SYSTEM_PRODUCT_RAWMATERIALS";
        /// <summary>��� �������� �������� ��� �������� ������� ���������</summary>
        public const string SYSTEM_PRODUCT_FINISHEDPRODUCTION = "SYSTEM_PRODUCT_FINISHEDPRODUCTION";
        /// <summary>��� �������� �������� ��� �������� �������� ��������</summary>
        public const string SYSTEM_PRODUCT_MONEY = "SYSTEM_PRODUCT_MONEY";
        /// <summary>��� �������� �������� ��� �������� ����</summary>
        public const string SYSTEM_PRODUCT_PACK = "SYSTEM_PRODUCT_PACK";
        /// <summary>��� �������� �������� ��� �������� ��� �������</summary>
        public const string SYSTEM_PRODUCT_ORGTEX = "SYSTEM_PRODUCT_ORGTEX";
        /// <summary>��� �������� �������� ��� �������� ����������</summary>
        public const string SYSTEM_PRODUCT_COMPUTER = "SYSTEM_PRODUCT_COMPUTER";
        
        /// <summary>��� �������� �������� ��� �������� ���������������� ���������</summary>
        public const string SYSTEM_MESSAGE_USERS = "SYSTEM_MESSAGE_USERS";
        /// <summary>��� �������� �������� ��� �������� ��������� � ��������</summary>
        public const string SYSTEM_MESSAGE_WEBNEWS = "SYSTEM_MESSAGE_WEBNEWS";
        /// <summary>��� �������� �������� ��� �������� ��������� ��������� ������</summary>
        public const string SYSTEM_MESSAGE_FILESERVICE = "SYSTEM_MESSAGE_FILESERVICE";
        /// <summary>��� �������� �������� ��� �������� ��������� ������ ������� ������</summary>
        public const string SYSTEM_MESSAGE_IMPORTSERVICE = "SYSTEM_MESSAGE_IMPORTSERVICE";
        /// <summary>��� �������� �������� ��� �������� ��������� ������ �������� ������</summary>
        public const string SYSTEM_MESSAGE_EXPORTSERVICE = "SYSTEM_MESSAGE_EXPORTSERVICE";
        
        /// <summary>��� �������� �������� ��� �������� ����� ��������</summary>
        public const string SYSTEM_ANALITIC_TAXDOCPAYMENTMETHOD = "SYSTEM_ANALITIC_TAXDOCPAYMENTMETHOD";
        /// <summary>��� �������� �������� ��� �������� �������</summary>
        public const string SYSTEM_ANALITIC_BRANDS = "SYSTEM_ANALITIC_BRANDS";
        /// <summary>��� �������� �������� ��� �������� �������� �����</summary>
        public const string SYSTEM_ANALITIC_TRADEGROUP = "SYSTEM_ANALITIC_TRADEGROUP";
        /// <summary>��� �������� �������� ��� �������� ��������� "��� ���������"</summary>
        public const string SYSTEM_ANALITIC_PRODUCTTYPE = "SYSTEM_PRODUCTTYPE";
        /// <summary>��� �������� �������� ��� �������� ��������� "��� ��������"</summary>
        public const string SYSTEM_ANALITIC_PACKTYPE = "SYSTEM_PACKTYPE";
        /// <summary>��� �������� �������� ��� �������� ��������� "������� ��������"</summary>
        public const string SYSTEM_ANALITIC_RETURNREASON = "SYSTEM_RETURNREASON";
        /// <summary>��� �������� �������� ��� �������� ��������� "������������ �������� �����"</summary>
        public const string SYSTEM_ANALITIC_OUTLETLOCATION = "SYSTEM_OUTLETLOCATION";
        /// <summary>��� �������� �������� ��� �������� ��������� "��� ������������ ������������"</summary>
        public const string SYSTEM_ANALITIC_TYPEEQUIPMENTHOLOD = "SYSTEM_TYPEEQUIPMENTHOLOD";
        /// <summary>��� �������� �������� ��� �������� ��������� "��� ������������"</summary>
        public const string SYSTEM_ANALITIC_TYPEEQUIPMENT = "SYSTEM_TYPEEQUIPMENT";
        /// <summary>��� �������� �������� ��� �������� ��������� "����"</summary>
        public const string SYSTEM_ANALITIC_COLOR = "SYSTEM_ANALITIC_COLOR";
        /// <summary>��� �������� �������� ��� �������� ��������� "�������"</summary>
        public const string SYSTEM_ANALITIC_EVENT = "SYSTEM_ANALITIC_EVENT";
        /// <summary>��� �������� �������� ��� �������� ��������� "��� ��������"</summary>
        public const string SYSTEM_ANALITITIC_RECURCIVE = "SYSTEM_ANALITIC_RECURCIVE";
        
        /// <summary>��� �������� �������� ��� �������� ��������� "�������� ��������"</summary>
        public const string SYSTEM_ANALITIC_PAYMENTTYPE = "SYSTEM_ANALITIC_PAYMENTTYPE";
        /// <summary>��� �������� �������� ��� �������� ��������� "�������� ��������" ������</summary>
        public const string SYSTEM_ANALITIC_PAYMENTTYPEOUT = "SYSTEM_ANALITIC_PAYMENTTYPE_OUT";
        /// <summary>��� �������� �������� ��� �������� ��������� "�������� ��������" ������</summary>
        public const string SYSTEM_ANALITIC_PAYMENTTYPEIN = "SYSTEM_ANALITIC_PAYMENTTYPE_IN";

        /// <summary>��� �������� �������� ��� �������� ��������� "��������� �������� �����"</summary>
        public const string SYSTEM_ANALITIC_AGENTCATEGORY = "SYSTEM_AGENTCATEGORY";
        /// <summary>��� �������� �������� ��� �������� ��������� "������"</summary>
        public const string SYSTEM_ANALITIC_AGENTMETRICAREA = "SYSTEM_AGENTMETRICAREA";
        /// <summary>��� �������� �������� ��� �������� ��������� "�������������� �������� ����� ��������������"</summary>
        public const string SYSTEM_ANALITIC_AGENTOWNERSHIP = "SYSTEM_AGENTOWNERSHIP";
        /// <summary>��� �������� �������� ��� �������� ��������� "�������"</summary>
        public const string SYSTEM_ANALITIC_AGENTINDUSTRY = "SYSTEM_AGENTINDUSTRY";
        /// <summary>��� �������� �������� ��� �������� ��������� "��� �������� �����"</summary>
        public const string SYSTEM_ANALITIC_AGENTTYPEOUTLET = "SYSTEM_AGENTTYPEOUTLET";
        /// <summary>��� �������� �������� ��� �������� ��������� "����� ���������������"</summary>
        public const string SYSTEM_ANALITICCFO = "SYSTEM_ANALITICCFO";
        /// <summary>��� �������� �������� "���������"</summary>
        public const string SYSTEM_WORKPOST = "SYSTEM_WORKPOST";
        /// <summary>��� �������� �������� "��������� ����������"</summary>
        public const string SYSTEM_ANALITIC_WORKERCATEGORY = "SYSTEM_ANALITIC_WORKERCATEGORY";
        /// <summary>��� �������� �������� "��������� �����"</summary>
        public const string SYSTEM_ANALITIC_TASKSTATE = "SYSTEM_ANALITIC_TASKSTATE";
        /// <summary>��� �������� �������� "����� �������� ��������"</summary>
        public const string SYSTEM_ANALITIC_EMPLOYMENTBOOK = "SYSTEM_ANALITIC_EMPLOYMENTBOOK";
        /// <summary>��� �������� �������� "��� �����������������"</summary>
        public const string SYSTEM_ANALITIC_MINORS = "SYSTEM_ANALITICS_MINORS";
        /// <summary>��� �������� �������� "������ ����������"</summary>
        public const string SYSTEM_ANALITIC_SIGNLEVEL = "SYSTEM_ANALITIC_SIGNLEVEL";

        /// <summary>��� �������� �������� "������ ��������"</summary>
        public const string SYSTEM_ANALITIC_ROUTESATUS = "SYSTEM_ANALITIC_ROUTESATUS";
        /// <summary>��� �������� �������� "����������� ������ ��������"</summary>
        public const string SYSTEM_ANALITIC_ROUTEFACT = "SYSTEM_ANALITIC_ROUTEFACT";

        /// <summary>��� �������� �������� "��������� ������������"</summary>
        public const string SYSTEM_ANALITIC_EQUPMENTSTATE = "SYSTEM_EQUPMENTSTATE";
        /// <summary>��� �������� �������� "���������� ������������"</summary>
        public const string SYSTEM_ANALITIC_EQUIPMENTPLACEMENT = "SYSTEM_EQUIPMENTPLACEMENT";
        
        /// <summary>��� �������� �������� ��������� "��������� ������������"</summary>
        public const string SYSTEM_ANALITIC_FINPLANRESULT = "FINPLAN_RESULT";
        /// <summary>��� �������� �������� ��������� "��� ����������� ������������"</summary>
        public const string SYSTEM_ANALITIC_FINPLANKIND = "FINPLAN_KIND";
        /// <summary>��� �������� �������� ��������� "��������"</summary>
        public const string SYSTEM_ANALITIC_CONTRACTPRIORITY = "CONTRACT_PRIORITY";

        /// <summary>��� �������� �������� "��������� ��������� � �������� ������"</summary>
        public const string SYSTEM_ANALITIC_REASONTRADEPOINT = "SYSTEM_REASONTRADEPOINT";

        /// <summary>��� �������� �������� "������� ��������"</summary>
        public const string SYSTEM_ANALITIC_DELIVERYCONDITION = "TAXDOCDELIVERYCONDITION";
        
        

        /// <summary>��� �������� �������� ��� �������� ����� ���������� ����������� �����</summary>
        public const string SYSTEM_FOLDER_FINANCENDS = "FINANCENDS";
        /// <summary>��� �������� �������� ��� �������� ����� ���������� ����������� �����</summary>
        public const string SYSTEM_FOLDER_FINANCE = "FINANCE";
        /// <summary>��� �������� �������� ��� �������� ����� ���������� �����</summary>
        public const string SYSTEM_FOLDER_SERVICE = "SERVICE";
        /// <summary>��� �������� �������� ��� �������� ����� ���������� ����� ��� ���</summary>
        public const string SYSTEM_FOLDER_SERVICENONDS = "SERVICENONDS";
        /// <summary>��� �������� �������� ��� �������� ����� ���������� ��������</summary>
        public const string SYSTEM_FOLDER_SALES = "SYSTEM_FLD_SALES";
        /// <summary>��� �������� �������� ��� �������� ����� ���������� �������� ��� ���</summary>
        public const string SYSTEM_FOLDER_SALESNONDS = "SYSTEM_FLD_SALESNONDS";
        
        /// <summary>��� �������� �������� ��� �������� ����� ���������� ��������� �����</summary>
        public const string SYSTEM_FOLDER_PERSONAL = "SYSTEM_FLD_PERSONALS";
        
        /// <summary>��� �������� �������� ��� �������� ����� ���������� ���������� �����</summary>
        public const string SYSTEM_FOLDER_TAX = "SYSTEM_FLD_TAX";
        /// <summary>��� �������� �������� ��� �������� ����� ���������� ���������� ������</summary>
        public const string SYSTEM_FOLDER_PRICEPOLICY = "PRICEPOLICY";
        
        /// <summary>��� �������� �������� ��� �������� ������� �����</summary>
        public const string SYSTEM_EVENT_TASK = "SYSTEM_EVENT_TASK";


        /// <summary>��� �������� �������� ��� �������� ��� ����������</summary>
        public const string SYSTEM_PRICENAME_OUTPRICES = "SYSTEM_PRICENAME_OUTPRICES";
        /// <summary>��� �������� �������� ��� �������� ��� �����������</summary>
        public const string SYSTEM_PRICENAME_COMPETITORPRICES = "SYSTEM_PRICECOMPETITOR";
        /// <summary>��� �������� �������� ��� �������� ��� �����������</summary>
        public const string SYSTEM_PRICENAME_INPRICES = "SYSTEM_PRICESUPPLIER";
        
        /// <summary>��� �������� �������� ��� �������� ��������� ������� ���������</summary>
        public const string SYSTEM_SIGN = "SYSTEM_SIGN";

        /// <summary>��� �������� �������� ��� �������� ��������� ��������</summary>
        public const string SYSTEM_CONTRACT_STATE = "CONTRACT_STATE";
        /// <summary>��� �������� �������� ��� �������� ����� ���������</summary>
        public const string SYSTEM_CONTRACT_KIND = "CONTRACT_KIND";
        /// <summary>��� �������� �������� ��� �������� ����� ��������������� ���������</summary>
        public const string SYSTEM_CONTRACT_CORRESPONDENCE = "SYSTEM_CORRESPONDENCE";

        /// <summary>��� �������� �������� ��� �������� �������� ����������� ������ ������������</summary>
        public const string SYSTEM_TIMEPERIOD_USERLOGON = "SYSTEM_TIMEPERIOD_USERLOGON";
        

        /*
        /// <summary>��� �������� ������ ���� �������� ������</summary>
        public const string SYSTEM_GROUPFIND_ALL_FILEDATA = "SYSTEM_GROUPFIND_ALL_FILEDATA";
        /// <summary>��� �������� ������ ���� ����������� �������� ������</summary>
        public const string SYSTEM_GROUPFIND_DENY_FILEDATA = "SYSTEM_GROUPFIND_DENY_FILEDATA";
        /// <summary>��� �������� ������ ���� ��������� �������� ������</summary>
        public const string SYSTEM_GROUPFIND_TRASH_FILEDATA = "SYSTEM_GROUPFIND_TRASH_FILEDATA";
        /// <summary>��� �������� ������ ���� �� �������� � ������ �������� ������</summary>
        public const string SYSTEM_GROUPFIND_NOINGROUP_FILEDATA = "SYSTEM_GROUPFIND_NOINGROUP_FILEDATA";
        /// <summary>��� �������� ������ ���� ��������� �������� ������</summary>
        public const string SYSTEM_GROUPFIND_SYS_FILEDATA = "SYSTEM_GROUPFIND_SYS_FILEDATA";
        /// <summary>��� �������� ������ ���� ��������� ������������� �������� ������</summary>
        public const string SYSTEM_GROUPFIND_NOTDONE_FILEDATA = "SYSTEM_GROUPFIND_NOTDONE_FILEDATA";
        /// <summary>��� �������� ������ ���� �������� �������� ������</summary>
        public const string SYSTEM_GROUPFIND_TEMPLATE_FILEDATA = "SYSTEM_GROUPFIND_TEMPLATE_FILEDATA";

        */
// ReSharper restore InconsistentNaming

        /// <summary>
        /// �������� ���� �������� ��� ����� ������
        /// </summary>
        /// <remarks>�������� ������������ ��� ����� ������ ���� ��������� ������������� �������� ������ �������� 
        /// �������� "SYSTEM_GROUPFIND_NOTDONE_FILEDATA"</remarks>
        /// <param name="value"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static string GetSystemFindCodeValue(WhellKnownDbEntity value, HierarchyCodeKind kind)
        {
            if (kind!= HierarchyCodeKind.FindRoot)
                return string.Format("SYSTEM_GROUPFIND_{0}_{1}", kind, value).ToUpper();
            return string.Format("SYSTEM_{0}_FINDROOT", value).ToUpper();
        }

        /// <summary>
        /// ��� �������� "�������� ��������" ��� ���������� ���� �������
        /// </summary>
        /// <param name="value">������� ��������</param>
        /// <returns></returns>
        public static string GetGroupActionCode(WhellKnownDbEntity value)
        {
            return string.Format("SYSTEM_PROCESS_GROUPACTION_{0}", value).ToUpper();
        }
        /// <summary>
        /// ��� ����� ������ ���������� 
        /// </summary>
        /// <param name="kind">��� ���������� ���� ��������</param>
        /// <returns></returns>
        public static string GetSystemFindDocumentCodeValue(HierarchyCodeKind kind)
        {
            return string.Format("SYSTEM_GROUPFIND_FLDDOC_DOCUMENTS_{0}", kind).ToUpper();
        }
        
        /// <summary>
        /// �������� ���� �������� ��� �������� ��������
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSystemRootCodeValue(WhellKnownDbEntity value)
        {
            return string.Format("SYSTEM_{0}_ROOT", value).ToUpper();
        }
        /// <summary>
        /// �������� ���� �������� ��� �������� ��������
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSystemFavoriteCodeValue(WhellKnownDbEntity value)
        {
            return string.Format("SYSTEM_{0}_FAVORITE", value).ToUpper();
        }

        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>������, ������������� �������� 1</summary>
        public const int KINDVALUE_GROUP = 1;
        /// <summary>����������� ������, ������������� �������� 2</summary>
        public const int KINDVALUE_VIRTUAL = 2;

        /// <summary>������, ������������� �������� 1835009</summary>
        public const int KINDID_GROUP = 1835009;
        /// <summary>����������� ������, ������������� �������� 1835010</summary>
        public const int KINDID_VIRTUAL = 1835010;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Hierarchy>.Equals(Hierarchy other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>�����������</summary>
        public Hierarchy(): base()
        {
            EntityId = (short) WhellKnownDbEntity.Hierarchy ;
        }
        protected override void CopyValue(Hierarchy template)
        {
            base.CopyValue(template);
            ContentEntityId = template.ContentEntityId;
            ContentFlags = template.ContentFlags;
            ParentId = template.ParentId;
            OrderNo = template.OrderNo;
            RootId = template.RootId;
            ViewListDocumentsId = template.ViewListDocumentsId;
            VirtualBuildId = template.VirtualBuildId;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override Hierarchy Clone(bool endInit)
        {
            Hierarchy obj = base.Clone(false);
            obj.ContentEntityId = ContentEntityId;
            obj.ContentFlags = ContentFlags;
            obj.ParentId = ParentId;
            obj.OrderNo = OrderNo;
            obj.RootId = RootId;
            obj.ViewListId = ViewListId;
            obj.ViewListDocumentsId = ViewListDocumentsId;
            obj.VirtualBuildId = VirtualBuildId;
            if (endInit)
                OnEndInit();
            return obj;
        }

        #region ��������
        private int _myCompanyId;
        /// <summary>
        /// ������������� �����������, �������� ����������� ���������
        /// </summary>
        public int MyCompanyId
        {
            get { return _myCompanyId; }
            set
            {
                if (value == _myCompanyId) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompanyId);
                _myCompanyId = value;
                OnPropertyChanged(GlobalPropertyNames.MyCompanyId);
            }
        }


        private Agent _myCompany;
        /// <summary>
        /// ��� ��������, ����������� �������� ����������� ���������
        /// </summary>
        public Agent MyCompany
        {
            get
            {
                if (_myCompanyId == 0)
                    return null;
                if (_myCompany == null)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                else if (_myCompany.Id != _myCompanyId)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                return _myCompany;
            }
            set
            {
                if (_myCompany == value) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompany);
                _myCompany = value;
                _myCompanyId = _myCompany == null ? 0 : _myCompany.Id;
                OnPropertyChanged(GlobalPropertyNames.MyCompany);
            }
        } 
        private short _orderNo;
        /// <summary>������� ����������</summary>
        public short OrderNo
        {
            get { return _orderNo; }
            set
            {
                if (_orderNo == value) return;
                OnPropertyChanging(GlobalPropertyNames.OrderNo);
                _orderNo = value;
                OnPropertyChanged(GlobalPropertyNames.OrderNo);
            }
        }
	
        private short _contentEntityId;
        /// <summary>������������� ���������� ���� �����������</summary>
        public short ContentEntityId
        {
            get { return _contentEntityId; }
            set
            {
                if (_contentEntityId == value) return;
                OnPropertyChanging(GlobalPropertyNames.ContentEntityId);
                _contentEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.ContentEntityId);
            }
        }

        private EntityType _contentEntityType;
        /// <summary>��������� ��� �����������</summary>
        public EntityType ContentEntityType
        {
            get
            {
                if (_contentEntityId == 0)
                    return null;
                if (_contentEntityType == null)
                    _contentEntityType = Workarea.CollectionEntities.Find(f => f.Id == _contentEntityId);
                else if (_contentEntityType.Id != _contentEntityId)
                    _contentEntityType = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == _contentEntityId);
                return _contentEntityType;
            }
        }

        private int _viewListId;
        /// <summary>������������� ������</summary>
        public int ViewListId
        {
            get { return _viewListId; }
            set 
            {
                if (value == _viewListId) return;
                OnPropertyChanging(GlobalPropertyNames.ViewListId);
                _viewListId = value;
                OnPropertyChanged(GlobalPropertyNames.ViewListId);
            }
        }

        private CustomViewList _viewList;
        /// <summary>������������� ������������ ��� ������ ���������</summary>
        public CustomViewList ViewList
        {
            get
            {
                if (_viewListId == 0)
                    return null;
                if (_viewList == null)
                    _viewList = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_viewListId);
                else if (_viewList.Id != _viewListId)
                    _viewList = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_viewListId);
                return _viewList;
            }
            set
            {
                if (_viewList == value) return;
                OnPropertyChanging(GlobalPropertyNames.ViewList);
                _viewList = value;
                _viewListId = _viewList == null ? 0 : _viewList.Id;
                OnPropertyChanged(GlobalPropertyNames.ViewList);
            }
        }

        private int _viewListDocumentsId;
        /// <summary>������������� ������ ��� ����������� ����������</summary>
        public int ViewListDocumentsId
        {
            get { return _viewListDocumentsId; }
            set
            {
                if (value == _viewListDocumentsId) return;
                OnPropertyChanging(GlobalPropertyNames.ViewListDocumentsId);
                _viewListDocumentsId = value;
                OnPropertyChanged(GlobalPropertyNames.ViewListDocumentsId);
            }
        }

        private CustomViewList _viewListDocuments;
        /// <summary>C���� ��� ����������� ����������</summary>
        /// <remarks>������������ ��� ������ ����������</remarks>
        public CustomViewList ViewListDocuments
        {
            get
            {
                if (_viewListDocumentsId == 0)
                    return null;
                if (_viewListDocuments == null)
                    _viewListDocuments = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_viewListDocumentsId);
                else if (_viewListDocuments.Id != _viewListDocumentsId)
                    _viewListDocuments = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_viewListDocumentsId);
                return _viewListDocuments;
            }
            set
            {
                if (_viewListDocuments == value) return;
                OnPropertyChanging(GlobalPropertyNames.ViewListDocuments);
                _viewListDocuments = value;
                _viewListDocumentsId = _viewListDocuments == null ? 0 : _viewListDocuments.Id;
                OnPropertyChanged(GlobalPropertyNames.ViewListDocuments);
            }
        }

        private int _parentId;
        /// <summary>������������� ��������</summary>
        public int ParentId
        {
            get { return _parentId; }
            set 
            {
                if (value == _parentId) return;
                OnPropertyChanging(GlobalPropertyNames.ParentId);
                _parentId = value;
                OnPropertyChanged(GlobalPropertyNames.ParentId);
            }
        }

        private Hierarchy _parent;
        /// <summary>��������</summary>
        public Hierarchy Parent
        {
            get
            {
                if (_parentId == 0)
                    return null;
                if (_parent == null)
                    _parent = Workarea.Cashe.GetCasheData<Hierarchy>().Item(_parentId);
                else if (_viewListId != 0 && _viewList != null && _viewList.Id != _viewListId)
                    _parent = Workarea.Cashe.GetCasheData<Hierarchy>().Item(_parentId);
                return _parent;
            }
            set
            {
                if (_parent == value) return;
                OnPropertyChanging(GlobalPropertyNames.Parent);
                _parent = value;
                _parentId = _parent == null ? 0 : _parent.Id;
                OnPropertyChanged(GlobalPropertyNames.Parent);
            }
        } 

        private int _rootId;
        /// <summary>������������� �����</summary>
        public int RootId
        {
            get { return _rootId; }
            protected internal set { _rootId = value; }
        }

        private int _contentFlags;
        /// <summary>����������� ���������� � ���� �����</summary>
        public int ContentFlags
        {
            get { return _contentFlags; }
            set 
            {
                if (value == _contentFlags) return;
                OnPropertyChanging(GlobalPropertyNames.ContentFlags);
                _contentFlags = value;
                OnPropertyChanged(GlobalPropertyNames.ContentFlags);
            }
        }

        private string _path;
        /// <summary>������ ����</summary>
        public string Path
        {
            get { return _path; }
            internal protected set { _path = value; }
        }

        private bool _hasContents;
        /// <summary>������� �����������</summary>
        public bool HasContents
        {
            get { return _hasContents; }
            internal protected set { _hasContents = value; }
        }

        private bool _hasChildren;
        /// <summary>������� ���������</summary>
        public bool HasChildren
        {
            get { return _hasChildren; }
            protected internal set { _hasChildren = value; }
        }

        private int _virtualBuildId;
        /// <summary>
        /// ������������� ������ ���������� ����������� ��������
        /// </summary>
        public int VirtualBuildId
        {
            get { return _virtualBuildId; }
            set
            {
                if (value == _virtualBuildId) return;
                OnPropertyChanging(GlobalPropertyNames.VirtualBuildId);
                _virtualBuildId = value;
                OnPropertyChanged(GlobalPropertyNames.VirtualBuildId);
            }
        }
        /// <summary>
        /// �������� �������� ��������� ����������� �������� 
        /// </summary>
        public bool IsVirtualRoot
        {
            get { return KindValue == 2 && VirtualBuildId != 0; }
        }
        /// <summary>
        /// �������� ����������� ���������
        /// </summary>
        public bool IsVirtual { get; set; }
        
        private List<Hierarchy> _children;
        /// <summary>�������� ��������</summary>
        public List<Hierarchy> Children
        {
            get { return _children ?? (_children = GetHierarchyChild(Id)); }
        }
        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="all">��������� ����������� ����������</param>
        public override void Refresh(bool all=true)
        {
            base.Refresh(all);
            if (all)
            {
                _children = GetHierarchyChild(Id);
                _contents = GetCollectionHierarchyContent(Id, ContentEntityId);
            }
        }

        private List<HierarchyContent> _contents;
        /// <summary>��������� ����������� ��������</summary>
        public List<HierarchyContent> Contents
        {
            get
            {
                if (_hasContents &(_contents == null || _contents.Count==0) )
                    _contents = GetCollectionHierarchyContent(Id, ContentEntityId);
                return _contents ?? (_contents = new List<HierarchyContent>());
            }
        }
        /// <summary>
        /// ��������� �������� � ��������
        /// </summary>
        /// <param name="id">������������� ��������</param>
        /// <param name="kind">�������� �������� ���� �������� <see cref="EntityType.Id"/></param>
        /// <returns></returns>
        private List<HierarchyContent> GetCollectionHierarchyContent(int id, Int16 kind)
        {
            List<HierarchyContent> collection = new List<HierarchyContent>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.SmallInt).Value = kind;
                        cmd.CommandText = Workarea.Empty<Hierarchy>().Entity.FindMethod("Hierarchy.HierarchyContentLoadByHierarchyId").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HierarchyContent item = new HierarchyContent { Workarea = Workarea };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
        /// <summary>
        /// ��������� ��������� ��������
        /// </summary>
        /// <param name="parentId">������������� ������������ ��������</param>
        /// <returns></returns>
        private List<Hierarchy> GetHierarchyChild(int parentId)
        {

            List<Hierarchy> collection = new List<Hierarchy>();
            if (Workarea == null)
                return collection;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = parentId;
                        cmd.CommandText = Workarea.Empty<Hierarchy>().Entity.FindMethod(GlobalMethodAlias.HierarchyLoadChild).FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Hierarchy item = new Hierarchy { Workarea = Workarea };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
        
        /// <summary>
        /// ��������� �������� �������� ��� ����
        /// </summary>
        /// <param name="kind">������������� ����, �� ��������� 0 - ��������� �������� �����<see cref="EntityType.Id"/></param>
        /// <returns></returns>
        public List<Hierarchy> GetCollectionHierarchy(int kind=0)
        {

            List<Hierarchy> collection = new List<Hierarchy>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = kind;
                        cmd.CommandText = Workarea.Empty<Hierarchy>().Entity.FindMethod(GlobalMethodAlias.HierarchyLoadRoot).FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Hierarchy item = new Hierarchy { Workarea = Workarea };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }

        /// <summary>
        /// ��������� �������� ��� ����, ���������� ���������� �������� ����������.
        /// </summary>
        /// <param name="kind">������������� ���� <see cref="EntityType.Id"/></param>
        /// <returns></returns>
        public List<Hierarchy> GetCollectionHierarchyFind(int kind)
        {

            List<Hierarchy> collection = new List<Hierarchy>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = kind;
                        cmd.CommandText = "[Hierarchy].[HierarchiesLoadFindByEntityType]";
                        //Workarea.Empty<Hierarchy>().Entity.FindMethod(GlobalMethodAlias.HierarchyLoadRoot).FullName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Hierarchy item = new Hierarchy { Workarea = Workarea };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
        
        // ����������� �������� � �������� ��� ���������� ���������
        private class CasheDataContent<T>
        {
            
            public CasheDataContent()
            {
                Data = new List<T>();
            }

            public List<T> Data{ get; set; }
            public List<T> DataNested { get; set; } 
        }

        private object _casheContents;
        /// <summary>��������� �������� � ������ ��������</summary>
        /// <typeparam name="T">���</typeparam>
        /// <returns></returns>
        public List<T> GetTypeContents<T>(bool nested = false, bool refresh=false, bool byFlagString=false) where T : class, ICoreObject, new()//IBase, new()
        {
            lock (this)
            {
                if (refresh || _casheContents == null)
                {
                    _casheContents = new CasheDataContent<T>();
                    if (nested)
                        (_casheContents as CasheDataContent<T>).DataNested = RefreshTypeContents<T>(nested, byFlagString);
                    else
                        (_casheContents as CasheDataContent<T>).Data = RefreshTypeContents<T>(nested, byFlagString);
                }
                //if ((casheContents as CasheDataContent<T>).Nested != nested)
                //{
                //    casheContents = new CasheDataContent<T>();
                //    (casheContents as CasheDataContent<T>).Data = RefreshTypeContents<T>(nested, byFlagString);
                //    (casheContents as CasheDataContent<T>).Nested = nested;
                //}
                
            }
            if (nested)
            {
                if ((_casheContents as CasheDataContent<T>).DataNested==null)
                    (_casheContents as CasheDataContent<T>).DataNested = RefreshTypeContents<T>(nested, byFlagString);
                return (_casheContents as CasheDataContent<T>).DataNested;
            }
            else
            {
                if ((_casheContents as CasheDataContent<T>).Data==null)
                    (_casheContents as CasheDataContent<T>).Data = RefreshTypeContents<T>(nested, byFlagString);
                return (_casheContents as CasheDataContent<T>).Data;
            }
        }

        /// <summary>
        /// �������� ��� �������� ���������� �������� � ��������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        internal void AddTypedContent<T>(T value)where T : class, ICoreObject, new()
        {
            if(_casheContents!=null)
            {
                if((_casheContents as CasheDataContent<T>).Data!=null)
                {
                    if((_casheContents as CasheDataContent<T>).Data.Contains(value))
                    {
                        int idx = (_casheContents as CasheDataContent<T>).Data.IndexOf(value);
                        (_casheContents as CasheDataContent<T>).Data[idx] = value;
                    }
                    else if((_casheContents as CasheDataContent<T>).Data.Exists(f=>f.Id==value.Id))
                    {
                        int idx = (_casheContents as CasheDataContent<T>).Data.FindIndex(f => f.Id == value.Id);
                        (_casheContents as CasheDataContent<T>).Data[idx] = value;
                    }
                    else
                    {
                        (_casheContents as CasheDataContent<T>).Data.Add(value);
                    }
                }
                if ((_casheContents as CasheDataContent<T>).DataNested != null)
                {
                    if ((_casheContents as CasheDataContent<T>).DataNested.Contains(value))
                    {
                        int idx = (_casheContents as CasheDataContent<T>).DataNested.IndexOf(value);
                        (_casheContents as CasheDataContent<T>).DataNested[idx] = value;
                    }
                    else if ((_casheContents as CasheDataContent<T>).DataNested.Exists(f => f.Id == value.Id))
                    {
                        int idx = (_casheContents as CasheDataContent<T>).DataNested.FindIndex(f => f.Id == value.Id);
                        (_casheContents as CasheDataContent<T>).DataNested[idx] = value;
                    }
                    else
                    {
                        (_casheContents as CasheDataContent<T>).DataNested.Add(value);
                    }
                }
            }
        }

        private List<T> RefreshTypeContents<T>(bool nested, bool byFlagString=false) where T : class, ICoreObject, new()
        {
            lock (this)
            {
                T item = new T {Workarea = Workarea};
                List<T> collection = new List<T>();
                using (SqlConnection cnn = Workarea.GetDatabaseConnection())
                {
                    if (cnn == null) return collection;

                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            string procedureName = string.Empty;
                            if (item.EntityId != 0)
                            {
                                ProcedureMap procedureMap = item.Entity.FindMethod(GlobalMethodAlias.LoadByHierarchies);
                                if (procedureMap != null)
                                {
                                    procedureName = procedureMap.FullName;
                                }
                            }
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = procedureName;
                            cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                            if (nested)
                                cmd.Parameters.Add(GlobalSqlParamNames.Nested, SqlDbType.Bit).Value = true;

                            if (byFlagString)
                                cmd.Parameters.Add(GlobalSqlParamNames.GroupByFlags, SqlDbType.Bit).Value = true;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                if (Workarea.Cashe.GetCasheData<T>().Exists(id))
                                {
                                    item = Workarea.Cashe.GetCasheData<T>().Item(id);
                                    item.Load(reader);
                                }
                                else
                                {
                                    item = new T { Workarea = Workarea };
                                    item.Load(reader);
                                    Workarea.Cashe.SetCasheData(item);
                                }
                                collection.Add(item);
                            }
                            reader.Close();
                        }
                    }
                    finally
                    {
                        cnn.Close();
                    }
                }
                return collection;
            }
        }

        /// <summary>�������� ��������� �����������</summary>
        public void RefreshContents()
        {
            _contents = GetCollectionHierarchyContent(Id, EntityId);
        }
        /// <summary>������� ������, ���������� ���������� �� ������ "�������������"</summary>
        /// <returns></returns>
        public DataTable GetCustomView()
        {
            //return Workarea.GetCollectionCustomViewList(this);
            if (ViewList == null)
                return null;
            if (string.IsNullOrEmpty(ViewList.SystemName))
                return null;
            DataTable tbl = new DataTable();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        // TODO: ��������... ��� ����� ������
                        if (ViewList.KindValue == 2 || ViewList.KindValue == 3)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add(GlobalSqlParamNames.KindType, SqlDbType.Int).Value = KindValue;
                            cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = Id;
                            cmd.CommandText = ViewList.SystemName;
                        }
                        else
                        {
                            cmd.CommandText = "select * from " + ViewList.SystemName;
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tbl);
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return tbl;
        }
        /// <summary>������� ������, ���������� �������������� �������� ����������� � "�������������"</summary>
        /// <returns></returns>
        public DataTable GetCustomViewIdList()
        {
            if (string.IsNullOrEmpty(ViewList.SystemName))
                return null;
            DataTable tbl = new DataTable { Locale = System.Threading.Thread.CurrentThread.CurrentCulture };
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {

                        switch (ViewList.KindValue)
                        {
                            case 2:
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = 0;
                                cmd.Parameters.Add(GlobalSqlParamNames.KindType, SqlDbType.Int).Value = KindValue;
                                cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = Id;
                                cmd.Parameters.Add(GlobalSqlParamNames.OnlyId, SqlDbType.Bit).Value = 1;
                                cmd.CommandText = ViewList.SystemName;
                                break;
                            case 4:
                                cmd.CommandText = "select * from " + ViewList.SystemName;
                                break;
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tbl);
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return tbl;
        }
        /// <summary>
        /// ������������� ������ ������������ �������� 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataTable GetCustomView(CustomViewList list)
        {
            return Workarea.GetCollectionCustomViewList(list, 0, KindValue, Id);
        }

        #endregion

        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_contentFlags!=0)
                writer.WriteAttributeString(GlobalPropertyNames.ContentFlags, XmlConvert.ToString(_contentFlags));
            if (_hasContents)
                writer.WriteAttributeString(GlobalPropertyNames.HasContents, XmlConvert.ToString(_hasContents));
            if (_parentId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.ParentId, XmlConvert.ToString(_parentId));
            if (!string.IsNullOrEmpty(_path))
                writer.WriteAttributeString(GlobalPropertyNames.Path, _path);
            if (_rootId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.RootId, XmlConvert.ToString(_rootId));
            if (_viewListId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.ViewListId, XmlConvert.ToString(_viewListId));
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ContentFlags) != null)
                _contentFlags = XmlConvert.ToInt32(reader[GlobalPropertyNames.ContentFlags]);
            if (reader.GetAttribute(GlobalPropertyNames.HasContents) != null)
                _hasContents = XmlConvert.ToBoolean(reader[GlobalPropertyNames.HasContents]);
            if (reader.GetAttribute(GlobalPropertyNames.ParentId) != null)
                _parentId = XmlConvert.ToInt32(reader[GlobalPropertyNames.ParentId]);
            if (reader.GetAttribute(GlobalPropertyNames.Path) != null)
                _path = reader.GetAttribute(GlobalPropertyNames.Path);
            if (reader.GetAttribute(GlobalPropertyNames.RootId) != null)
                _rootId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RootId));
            if (reader.GetAttribute(GlobalPropertyNames.ViewListId) != null)
                _viewListId = XmlConvert.ToInt32(reader[GlobalPropertyNames.ViewListId]);
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion

        #region ���������
        HierarchyStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new HierarchyStruct
                                  {
                                      ContentFlags = _contentFlags,
                                      HasContents = _hasContents,
                                      ParentId = _parentId,
                                      Path = _path,
                                      RootId = _rootId,
                                      ViewListId = _viewListId,
                                      MyCompanyId = _myCompanyId
                                  };
                return true;
            }
            return false;
        }
        /// <summary>����������� ���������</summary>
        public override void RestoreState()
        {
            base.RestoreState();
            ContentFlags = _baseStruct.ContentFlags;
            HasContents = _baseStruct.HasContents;
            ParentId = _baseStruct.ParentId;
            Path = _baseStruct.Path;
            RootId = _baseStruct.RootId;
            ViewListId = _baseStruct.ViewListId;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion
        public override void Validate()
        {
            base.Validate();
            if(_rootId==0 && _parentId!=0)
            {
                if(Parent!=null)
                {
                    _rootId = Parent.RootId == 0 ? Parent.Id : Parent.RootId;
                }
            }
        }
        #region ���� ������
        /// <summary>��������� ��������� �� ���� ������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">��������� ������������� �������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                //[DbEntityId],[ViewId],[ParentId],[RootId],[ContentFlags],[Path],[HasContents],SortOrder,ViewDocId, HasChild
                _contentEntityId = reader.GetInt16(17);
                _viewListId = reader.IsDBNull(18) ? 0 : reader.GetSqlInt32(18).Value;
                _parentId = reader.IsDBNull(19) ? 0 : reader.GetSqlInt32(19).Value;
                _rootId = reader.IsDBNull(20) ? 0 : reader.GetSqlInt32(20).Value;
                _contentFlags = reader.IsDBNull(21) ? 0 : reader.GetSqlInt32(21).Value;
                _path = reader.IsDBNull(22) ? null : reader.GetString(22);
                _hasContents = reader.GetBoolean(23);
                _orderNo = reader.GetInt16(24);
                _viewListDocumentsId = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                _hasChildren = reader.GetInt32(26) == 0 ? false : true;
                _virtualBuildId = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                _myCompanyId = reader.IsDBNull(28) ? 0 : reader.GetInt32(28);
                if (!endInit) return;
                OnEndInit();
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex); 
            }
            
        }
        /// <summary>��������� ��������� �� ���� ������</summary>
        /// <param name="value">�������������</param>
        public override void Load(int value)
        {
            Load(value, Entity.FindMethod("Load").FullName);
        }
        /// <summary>���������� �������� ���������� ��� �������� ��������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ��������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ViewId, SqlDbType.Int) {IsNullable = true};
            if (_viewListId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _viewListId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ParentId, SqlDbType.Int) {IsNullable = true};
            if (_parentId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _parentId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EntityId, SqlDbType.Int) { IsNullable = false, Value = _contentEntityId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ContentFlags, SqlDbType.Int) {IsNullable = false, Value = _contentFlags};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.SmallInt) {IsNullable = false, Value = _orderNo};
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ViewDocId, SqlDbType.Int) {IsNullable = true};
            if (_viewListDocumentsId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _viewListDocumentsId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RootId, SqlDbType.Int) { IsNullable = true };
            if (_rootId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _rootId;
            sqlCmd.Parameters.Add(prm);
            

            prm = new SqlParameter(GlobalSqlParamNames.VirtualBuildId, SqlDbType.Int) { IsNullable = true };
            if (_virtualBuildId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _virtualBuildId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion
        /// <summary>������ �������� � ������ ������� ������</summary>
        /// <typeparam name="T">��� �������</typeparam>
        /// <param name="value">������</param>
        /// <returns></returns>
        public static int? FirstHierarchy<T>(T value) where T : class, IBase, new()
        {
            int? res = default(int?);
            if (value.Id == 0)
                return res;
            ProcedureMap procedureMap = value.Entity.FindMethod("FirstHierarchy");
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureMap.FullName;
                    cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                    if (cmd.Connection.State== ConnectionState.Closed)
                        cmd.Connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        res = (Int32)obj;
                    }
                    if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                }
            }
            return res;
        }
        //List<HierarchyContentType> collValues = HierarchyContentType.GetCollection(SelectedItem.Workarea, SelectedItem.Id);
        private List<HierarchyContentType> _contentTypes;
        /// <summary>
        /// ��������� ���������� ����� ��� ��������.
        /// </summary>
        /// <returns></returns>
        public List<HierarchyContentType> GetContentType()
        {
            return _contentTypes ?? (_contentTypes = HierarchyContentType.GetCollection(Workarea, Id));
        }

        /// <summary>
        /// ������ ��������������� ����������� ����������� ��������
        /// </summary>
        /// <returns></returns>
        public List<int> GetContentTypeKindId()
        {
            List<int> val = new List<int>();
            foreach (HierarchyContentType contentType in GetContentType())
            {
                val.Add(contentType.EntityKindId);
            }
            return val;
        }
        /// <summary>
        /// ����� �������� �� ����
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="code">���</param>
        /// <param name="entityId">������������� ����</param>
        /// <returns></returns>
        public static List<Hierarchy> FindByCode(Workarea wa, string code, short entityId)
        {
            List<Hierarchy> collection = new List<Hierarchy>();
            if (wa == null)
                return collection;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "[Hierarchy].[HierarchiesFindByCode]";//Entity.FindMethod("Hierarchy.Hierarchies").FullName;// "[Hierarchy].[ElementInHierarchies]";
                    cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;
                    cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = entityId;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Hierarchy item = new Hierarchy { Workarea = wa };
                            item.Load(reader);
                            wa.Cashe.SetCasheData(item);
                            collection.Add(item);
                        }
                    }
                    reader.Close();

                    if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                }
            }
            return collection;
        }

        private Dictionary<int, List<Hierarchy>> _casheObjectHierarhies = new Dictionary<int, List<Hierarchy>>();
        /// <summary>
        /// <summary>��������� �������� � ������� ������ ������</summary>
        /// </summary>
        /// <remarks>��������� �������� ������ � �������������� ����������� ���� ��������</remarks>
        /// <typeparam name="T">��� �������</typeparam>
        /// <param name="value">������</param>
        /// <returns></returns>
        public List<Hierarchy> Hierarchies<T>(T value) where T : class, IBase, new()
        {
            return Hierarchies(value, false);
        }

        /// <summary>��������� �������� � ������� ������ ������</summary>
        /// <typeparam name="T">��� �������</typeparam>
        /// <param name="value">������</param>
        /// <param name="refresh">�������� ������, ��� �������� <c>true</c> ����������� ���������� ������ �� ���� ������, � 
        /// ��������� ������ ������������ ��� ��������</param>
        /// <returns></returns>
        public List<Hierarchy> Hierarchies<T>(T value, bool refresh) where T : class, IBase, new()
        {
            if (_casheObjectHierarhies == null)
                _casheObjectHierarhies = new Dictionary<int, List<Hierarchy>>();

            if (!refresh && _casheObjectHierarhies.ContainsKey(value.Id))
            {
                return _casheObjectHierarhies[value.Id];
            }

            List<Hierarchy> collection = new List<Hierarchy>();
            if (value.Id == 0)
                return collection;
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = Entity.FindMethod("Hierarchy.Hierarchies").FullName;// "[Hierarchy].[ElementInHierarchies]";
                    cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                    cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = value.EntityId;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Hierarchy item = new Hierarchy {Workarea = Workarea};
                            item.Load(reader);
                            Workarea.Cashe.SetCasheData(item);
                            collection.Add(item);
                        }
                    }
                    reader.Close();

                    if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                }
            }
            if(_casheObjectHierarhies.ContainsKey(value.Id))
            {
                _casheObjectHierarhies[value.Id] = collection;
            }
            else
            {
                _casheObjectHierarhies.Add(value.Id, collection);
            }
            return collection;
        }

        /// <summary>�������� ���������� ��������</summary>
        /// <typeparam name="T">���</typeparam>
        /// <param name="value">������, ������� ���������� ��������</param>
        /// <param name="addToCashe">��������� ������ � ������������ ��������� �����������</param>
        public void ContentAdd<T>(T value, bool addToCashe = false) where T : class, IBase, new()
        {
            ProcedureMap procedureMap = Entity.FindMethod("ContentAdd");
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureMap.FullName;
                    SqlParameter prmRes = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prmRes.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = Id;
                    cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = value.Id;
                    cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.Int).Value = value.EntityId;
                    cmd.Parameters.Add(GlobalSqlParamNames.DatabaseId, SqlDbType.Int).Value = Workarea.MyBranche.Id; 
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    object obj = prmRes.Value;
                    int res = (int)obj;
                    if (res == -1)
                    {
                        throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_CONTENTADD", 1049));
                    }
                    if(res!=0)
                    {
                        throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_CONTENTADD", 1049), res);
                    }
                    if (addToCashe)
                        if (_casheContents!=null)
                        {
                            if (!(_casheContents as CasheDataContent<T>).Data.Contains(value))
                                (_casheContents as CasheDataContent<T>).Data.Add(value);
                        }
                }
            }
        }
        public void Reorder<T>(T value, bool kind) where T : class, IBase, new()
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // TODO: ������������� ������
                        //Workarea.Empty<Hierarchy>().Entity.FindMethod("HierarchyContent.Copy").FullName;  //"[Hierarchy].[HierarchyContentCopy]";
                        cmd.CommandText = "Hierarchy.HierarchyContentMoveUpDown";

                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Bit).Value = kind;
                        cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = value.Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.Int).Value = value.EntityId;
                        SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                        prm.Direction = ParameterDirection.ReturnValue;
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((int)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }

        }
        /// <summary>������� ���������� ��������</summary>
        /// <typeparam name="T">���</typeparam>
        /// <param name="value">������, ������� ���������� �������</param>
        public void ContentRemove<T>(T value) where T : class, IBase, new()
        {
            ProcedureMap procedureMap = Entity.FindMethod("ContentRemove");
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureMap.FullName;
                    SqlParameter prmRes = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prmRes.Direction = ParameterDirection.ReturnValue;
                    
                    cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = Id;
                    cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = value.Id;
                    cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.Int).Value = value.EntityId;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    object obj = prmRes.Value;
                    if ((int)obj == -1)
                    {
                        throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_CONTENTREMOVE", 1049));
                    }
                }
            }
        }

        /// <summary>
        /// �������� �������� �� �������� �������� �� ��������� � ��������
        /// </summary>
        /// <param name="parent">�������� ��������</param>
        /// <returns></returns>
        public bool IsChildTo(Hierarchy parent)
        {
            Hierarchy current = this;
            while(current!=null)
            {
                if (current.ParentId == parent.Id)
                    return true;
                current = current.Parent;
            }
            return false;
        }

        #region ILinks<Hierarchy> Members
        /// <summary>����� ���������</summary>
        /// <returns></returns>
        public List<IChain<Hierarchy>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>����� ���������</summary>
        /// <param name="kind">��� �����</param>
        /// <returns></returns>
        public List<IChain<Hierarchy>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Hierarchy> IChains<Hierarchy>.SourceList(int chainKindId)
        {
            return Chain<Hierarchy>.GetChainSourceList(this, chainKindId);
        }
        List<Hierarchy> IChains<Hierarchy>.DestinationList(int chainKindId)
        {
            return Chain<Hierarchy>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Hierarchy>> GetValues(bool allKinds)
        {
            return CodeHelper<Hierarchy>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Hierarchy>.GetView(this, true);
        }
        #endregion

        #region IChainsAdvancedList<Hierarchy, Agent> Members
        List<IChainAdvanced<Hierarchy, Agent>> IChainsAdvancedList<Hierarchy, Agent>.GetLinks()
        {
            return ((IChainsAdvancedList<Hierarchy, Agent>)this).GetLinks(42);
        }

        List<IChainAdvanced<Hierarchy, Agent>> IChainsAdvancedList<Hierarchy, Agent>.GetLinks(int? kind)
        {
            return GetLinkedHierarchy();
        }
        List<ChainValueView> IChainsAdvancedList<Hierarchy, Agent>.GetChainView()
        {
            return ChainValueView.GetView<Hierarchy, Agent>(this);
        }

        private List<IChainAdvanced<Hierarchy, Agent>> collectionLinkedHierarchies;
        private bool isCollectionLinkedHierarchiesDone;
        /// <summary>
        /// ��������� ����������� �����.
        /// </summary>
        /// <returns></returns>
        public List<IChainAdvanced<Hierarchy, Agent>> GetLinkedHierarchy(bool refresh=false)
        {
            if (collectionLinkedHierarchies==null)
                collectionLinkedHierarchies = new List<IChainAdvanced<Hierarchy, Agent>>();
            if (!refresh && isCollectionLinkedHierarchiesDone)
                return collectionLinkedHierarchies;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Hierarchy>().Entity.FindMethod("HierarchyAgentChainLoadBySource").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = Id;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainAdvanced<Hierarchy, Agent> item = new ChainAdvanced<Hierarchy, Agent> { Workarea = Workarea, Left = this };
                                item.Load(reader);
                                collectionLinkedHierarchies.Add(item);
                            }
                        }
                        reader.Close();

                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                        isCollectionLinkedHierarchiesDone = true;
                    }

                }
            }
            return collectionLinkedHierarchies;
        }

        #endregion
        #region IChainsAdvancedList<Hierarchy,Note> Members

        List<IChainAdvanced<Hierarchy, Note>> IChainsAdvancedList<Hierarchy, Note>.GetLinks()
        {
            return ChainAdvanced<Hierarchy, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Hierarchy, Note>> IChainsAdvancedList<Hierarchy, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Hierarchy, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Hierarchy, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Hierarchy, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Hierarchy, Note>.GetChainView()
        {
            return ChainValueView.GetView<Hierarchy, Note>(this);
        }
        #endregion
    }
}
