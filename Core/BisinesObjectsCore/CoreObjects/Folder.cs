using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using BusinessObjects.Documents;

namespace BusinessObjects
{
    /// <summary>��������� ������� "�����"</summary>
    internal struct FolderStruct
    {
        /// <summary>������������� �������� ����� ����������</summary>
        public int FormId;
        /// <summary>������������� ������� ��������</summary>
        public int DocumentId;
        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId;
    }
    /// <summary>
    /// ����� ����������
    /// </summary>
    public sealed class Folder : BaseCore<Folder>, IChains<Folder>, IReportChainSupport, IEquatable<Folder>,
        ICodes<Folder>,
        IChainsAdvancedList<Folder, Note>, ICompanyOwner
    {
        // ReSharper disable InconsistentNaming
        #region ��������� ����� � ��������
        
        /// <summary>�����, ������������� �������� 1</summary>
        public const int KINDVALUE_FOLDER = 1;

        /// <summary>�����, ������������� �������� 458753</summary>
        public const int KINDID_FOLDER = 458753;
        
        #endregion

        #region ��������� ����� ������� "��������"
        /// <summary>���� ��������� � ���</summary>
        public const string CODE_FIND_SALES_OUT_ACCOUNT_NDS = "FLD_SALES_OUT_ACCOUNT_NDS";
        /// <summary>��������� ��������� � ���</summary>
        public const string CODE_FIND_SALES_OUT_NDS = "FLD_SALES_OUT_NDS";
        /// <summary>����� �������� � ���</summary>
        public const string CODE_FIND_SALES_IN_ORDER_NDS = "FLD_SALES_IN_ORDER_NDS";
        /// <summary>������� ������� �� ���������� � ���</summary>
        public const string CODE_FIND_SALES_IN_RETURN_NDS = "FLD_SALES_IN_RETURN_NDS";
        /// <summary>��������� ��������� � ���</summary>
        public const string CODE_FIND_SALES_IN_NDS = "FLD_SALES_IN_NDS";
        /// <summary>������� ������ ���������� � ���</summary>
        public const string CODE_FIND_SALES_OUT_RETURN_NDS = "FLD_SALES_OUT_RETURN_NDS";
        /// <summary>����� ���������� � ���</summary>
        public const string CODE_FIND_SALES_OUT_ORDER_NDS = "FLD_SALES_OUT_ORDER_NDS";
        /// <summary>���� �������� � ���</summary>
        public const string CODE_FIND_SALES_IN_ACCOUNT_NDS = "FLD_SALES_IN_ACCOUNT_NDS";
        /// <summary>�������������� ���� � ���</summary>
        public const string CODE_FIND_SALES_OUT_ASSORT_NDS = "FLD_SALES_OUT_ASSORT_NDS";
        /// <summary>��������� ��������� ��� ���</summary>
        public const string CODE_FIND_SALES_OUT = "FLD_SALES_OUT";
        /// <summary>��������� ��������� ��� ���</summary>
        public const string CODE_FIND_SALES_IN = "FLD_SALES_IN";
        /// <summary>����� ���������� ��� ���</summary>
        public const string CODE_FIND_SALES_ORDER_IN = "FLD_SALES_ORDER_IN";
        /// <summary>�������������� ���� ���������� ��� ���</summary>
        public const string CODE_FIND_SALES_IN_ASSORT_NDS = "FLD_SALES_IN_ASSORT_NDS";
        /// <summary>��������� ���� ��� ���</summary>
        public const string CODE_FIND_SALES_ACCOUNT_OUT = "FLD_SALES_ACCOUNT_OUT";
        /// <summary>�������� ���� ��� ���</summary>
        public const string CODE_FIND_SALES_ACCOUNT_IN = "FLD_SALES_ACCOUNT_IN";
        /// <summary>����� ���������� ��� ���</summary>
        public const string CODE_FIND_SALES_ORDER_OUT = "FLD_SALES_ORDER_OUT";
        /// <summary>�������������� ���� ��� ���</summary>
        public const string CODE_FIND_SALES_ASSORT_OUT = "FLD_SALES_ASSORT_OUT";
        /// <summary>�������������� ���� ���������� ��� ���</summary>
        public const string CODE_FIND_SALES_ASSORT_IN = "FLD_SALES_ASSORT_IN";
        /// <summary>������� ������ �� ���������� ��� ���</summary>
        public const string CODE_FIND_SALES_RETURN_IN = "FLD_SALES_RETURN_IN";
        /// <summary>������� ������ ���������� ��� ���</summary>
        public const string CODE_FIND_SALES_RETURN_OUT = "FLD_SALES_RETURN_OUT";
        /// <summary>���������� ����������� ��� ���</summary>
        public const string CODE_FIND_SALES_MOVE = "FLD_SALES_MOVE";
        /// <summary>�������������� ��� ���</summary>
        public const string CODE_FIND_SALES_INVENTORY = "FLD_SALES_INVENTORY";
        /// <summary>���������� ����������� � ���</summary>
        public const string CODE_FIND_SALES_MOVE_NDS = "FLD_SALES_MOVE_NDS";
        /// <summary>��� �������� ������ � ���</summary>
        public const string CODE_FIND_SALES_OUT_ACT_NDS = "FLD_SALES_OUT_ACT_NDS";
        /// <summary>��� �������� ������ ��� ���</summary>
        public const string CODE_FIND_SALES_ACT_OUT = "FLD_SALES_ACT_OUT";
        /// <summary>��� ������� ������ � ���</summary>
        public const string CODE_FIND_SALES_IN_ACT_NDS = "FLD_SALES_IN_ACT_NDS";
        /// <summary>��� ������� ������ � ���</summary>
        public const string CODE_FIND_SALES_ACT_IN = "FLD_SALES_ACT_IN";
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_SALES_CONFIG = "FLD_SALES_CONFIG";
        #endregion

        #region ��������� ����� ��������� ������� "������� ��������"
        /// <summary>�������� ��������������� ������������ ��������� ��� �� ����� ������������.</summary>
        public const string CODE_FIND_PRICES_DEAULT = "FLD_PRICE";
        /// <summary>�������� ��������������� ������������ ��� ��������� ������������ ��� ��� ������������� �������. � �������� ��� ���� ���������� ������� ������� ������������ �������� ������������ ����.</summary>
        public const string CODE_FIND_PRICES_IND = "FLD_PRICE_IND";
        /// <summary>�����-����� ����� ����������� ������������ ��� ����������� ������� ��������� ��� � ������� ����������� ���� � ��������� ����������.</summary>
        public const string CODE_FIND_PRICES_SYPPLYER = "FLD_PRICE_SYPPLYER";
        /// <summary>�����-���� ����� ����������� ��� ���������� ������� ������������ ��� ����������� ������� ��������� ��� � ������������� ����������� ���.</summary>
        public const string CODE_FIND_PRICES_COMPETITOR = "FLD_PRICE_COMPETITOR";
        /// <summary>�����-���� ����� ����������� ������������ ��� ����������� ������� ��������� ��� � ������������� ����������� ���.</summary>
        public const string CODE_FIND_PRICES_COMPETITORIND = "FLD_PRICE_COMPETITOR_IND";

        /// <summary>������ ��������������� �� ����� ������������.</summary>
        public const string CODE_FIND_PRICES_COMMAND = "FLD_PRICE_COMMAND";
        /// <summary>������ ��������������� ��� ��������� ������������ ��� ��� ������������� �������. � �������� ��� ���� ���������� ������� ������� ������������ �������� ������������ ����.</summary>
        public const string CODE_FIND_PRICES_COMMANDIND = "FLD_PRICE_COMMANDIND";
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_PRICE_CONFIG = "FLD_PRICE_CONFIG";
        #endregion

        #region ��������� ����� ������� "���������� ���������"
        /// <summary>������ �� ����������</summary>
        public const string CODE_FIND_FINANCE_IN = "FLD_FINANCE_IN";
        /// <summary>������ ���������� </summary>
        public const string CODE_FIND_FINANCE_OUT = "FLD_FINANCE_OUT";
        /// <summary>������ ����������� �������� �������</summary>
        public const string CODE_FIND_FINANCE_IN_CUSTOM = "FLD_FINANCE_IN_CUSTOM";
        /// <summary>������� �������� ������� ���������� </summary>
        public const string CODE_FIND_FINANCE_OUT_RETURN = "FLD_FINANCE_OUT_RETURN";
        /// <summary>������ ������ �������� �������</summary>
        public const string CODE_FIND_FINANCE_OUT_CUSTOM = "FLD_FINANCE_OUT_CUSTOM";

        /// <summary>������ �� ����������</summary>
        public const string CODE_FIND_FINANCE_IN_NDS = "FLD_FINANCE_IN_NDS";
        /// <summary>������ ���������� </summary>
        public const string CODE_FIND_FINANCE_OUT_NDS = "FLD_FINANCE_OUT_NDS";
        /// <summary>������ ����������� �������� �������</summary>
        public const string CODE_FIND_FINANCE_IN_CUSTOM_NDS = "FLD_FINANCE_IN_CUSTOM_NDS";
        /// <summary>������� �������� ������� ���������� </summary>
        public const string CODE_FIND_FINANCE_OUT_RETURN_NDS = "FLD_FINANCE_OUT_RETURN_NDS";
        /// <summary>������ ������ �������� �������</summary>
        public const string CODE_FIND_FINANCE_OUT_CUSTOM_NDS = "FLD_FINANCE_OUT_CUSTOM_NDS";
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_FINANCE_CONFIG = "FLD_FINANCE_CONFIG";
        #endregion

        #region ��������� ����� ������� "���������"
        /// <summary>�������� ���������</summary>
        public const string CODE_FIND_TAX_IN = "FLD_TAX_IN";
        /// <summary>��������� ���������</summary>
        public const string CODE_FIND_TAX_OUT = "FLD_TAX_OUT";
        /// <summary>�������� ���������������� ���������</summary>
        public const string CODE_FIND_TAX_CORIN = "FLD_TAX_IN_COR";
        /// <summary>��������� ���������������� ���������</summary>
        public const string CODE_FIND_TAX_COROUT = "FLD_TAX_OUT_COR";
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_TAX_CONFIG = "FLD_TAX_CONFIG";
        
        #endregion

        #region ��������� ����� ������� "������"
        /// <summary>��� ���������� ����� ���������</summary>
        public const string CODE_FIND_SERVICE_OUT_NDS = "FLD_SERVICE_OUT_NDS";
        /// <summary>��� ���������� ����� ��������</summary>
        public const string CODE_FIND_SERVICE_IN_NDS = "FLD_SERVICE_IN_NDS";
        /// <summary>���� �� ������ ���������</summary>
        public const string CODE_FIND_SERVICE_OUT_ACCOUNT_NDS = "FLD_SERVICE_OUT_ACCOUNT_NDS";
        /// <summary>���� �� ����� ��������</summary>
        public const string CODE_FIND_SERVICE_IN_ACCOUNT_NDS = "FLD_SERVICE_IN_ACCOUNT_NDS";
        /// <summary>����� ���� ����������</summary>
        public const string CODE_FIND_SERVICE_OUT_ORDER_NDS = "FLD_SERVICE_OUT_ORDER_NDS";
        /// <summary>����� ����� ��������</summary>
        public const string CODE_FIND_SERVICE_IN_ORDER_NDS = "FLD_SERVICE_IN_ORDER_NDS";

        /// <summary>��� ���������� ����� ���������</summary>
        public const string CODE_FIND_SERVICE_OUT = "FLD_SERVICE_OUT";
        /// <summary>��� ���������� ����� �������� </summary>
        public const string CODE_FIND_SERVICE_IN = "FLD_SERVICE_IN";
        /// <summary>���� �� ������ ���������</summary>
        public const string CODE_FIND_SERVICE_OUT_ACCOUNT = "FLD_SERVICE_OUT_ACCOUNT";
        /// <summary>���� �� ����� ��������</summary>
        public const string CODE_FIND_SERVICE_IN_ACCOUNT = "FLD_SERVICE_IN_ACCOUNT";
        /// <summary>����� ���� ����������</summary>
        public const string CODE_FIND_SERVICE_OUT_ORDER = "FLD_SERVICE_OUT_ORDER";
        /// <summary>����� ����� ��������</summary>
        public const string CODE_FIND_SERVICE_IN_ORDER = "FLD_SERVICE_IN_ORDER";
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_SERVICE_CONFIG = "FLD_SERVICE_CONFIG";
        #endregion

        #region ��������� ������� "���������"
        /// <summary>������ �������</summary>
        public const string CODE_FIND_MKTG_MRAK = "FLD_MKTG_MRAK";
        /// <summary>�������������-�������</summary>
        public const string CODE_FIND_MKTG_ANKW = "FLD_MKTG_ANKW";
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_MKTG_CONFIG = "FLD_MKTG_CONFIG";
        #endregion

        #region ��������� ������� "��������"
        /// <summary>�������� ��������</summary>
        public const string CODE_FIND_CONTRACTS_CONTRACT = "FLD_CONTRACTS_CONTRACT";
        /// <summary>��� ������� ��������� ������������</summary>
        public const string CODE_FIND_CONTRACTS_REVISION = "FLD_CONTRACTS_REVISION";
        /// <summary>��� ������ ��������</summary>
        public const string CODE_FIND_CONTRACTS_VERIFICATION = "FLD_CONTRACTS_VERIFICATION";
        /// <summary>����� ������</summary>
        public const string CODE_FIND_CONTRACTS_ACCOUNTIN = "FLD_CONTRACTS_ACCOUNTIN";
        /// <summary>������� ��������</summary>
        public const string CODE_FIND_CONTRACTS_SALEOUT = "FLD_CONTRACTS_SALEOUT";
        /// <summary>���� �����������</summary>
        public const string CODE_FIND_CONTRACTS_COMPUTER = "FLD_CONTRACTS_COMPUTER";
        /// <summary>���� ���������</summary>
        public const string CODE_FIND_CONTRACTS_PRINTER = "FLD_CONTRACTS_PRINTERS";
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_CONTRACTS_OFFICIALNOTE = "FLD_CONTRACTS_OFFICIALNOTE";
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_CONTRACTS_CONFIG = "FLD_CONTRACTS_CONFIG";
        #endregion

        #region ��������� ������� "���������� ������������"
        /// <summary>������ �� ������������</summary>
        public const string CODE_FIND_FINPLAN_ORDER = "FLD_FINPLAN_ORDER";
        /// <summary>������������� ������ �� ������������</summary>
        public const string CODE_FIND_FINPLAN_ORDERTOTAL = "FLD_FINPLAN_ORDERTOTAL";
        /// <summary>��������� ������ �� ������������</summary>
        public const string CODE_FIND_FINPLAN_ORDERFINAL = "FLD_FINPLAN_ORDERFINAL";
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_FINPLAN_CONFIG = "FLD_FINPLAN_CONFIG";
        #endregion

        #region ��������� ������� "��������"
        /// <summary>��������</summary>
        public const string CODE_FIND_ROUTE_ROUTE = "FLD_ROUTE_ROUTE";
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_ROUTE_CONFIG = "FLD_ROUTE_CONFIG";
        #endregion
        
        #region ��������� ������� "�����������"
        /// <summary>��������� �������</summary>
        public const string CODE_FIND_BOOKKEEP_CONFIG = "FLD_BOOKKEEP_CONFIG";
        #endregion
        
        // ReSharper restore InconsistentNaming
        bool IEquatable<Folder>.Equals(Folder other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>�����������</summary>
        public Folder(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Folder;
        }
        protected override void CopyValue(Folder template)
        {
            base.CopyValue(template);
            DocumentId = template.DocumentId;
            ViewListDocumentsId = template.ViewListDocumentsId;
            FormId = template.FormId;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override Folder Clone(bool endInit)
        {
            Folder obj = base.Clone(false);
            obj.DocumentId = DocumentId;
            obj.ViewListDocumentsId = ViewListDocumentsId;
            obj.FormId = FormId;

            if (endInit)
                OnEndInit();
            return obj;
        }

        #region ��������
        private int _formId;
        /// <summary>
        /// ������������� �������� ����� ����������
        /// </summary>
        public int FormId
        {
            get { return _formId; }
            set
            {
                if (value == _formId) return;
                OnPropertyChanging(GlobalPropertyNames.FormId);
                _formId = value;
                if (_projectItem != null)
                    _projectItem = Workarea.Cashe.GetCasheData<Library>().Item(_formId);

                OnPropertyChanged(GlobalPropertyNames.FormId);
            }
        }

        private Library _projectItem;
        /// <summary>
        /// �����
        /// </summary>
        public Library ProjectItem
        {
            get
            {
                if (_formId == 0)
                    return null;
                if (_projectItem == null)
                    _projectItem = Workarea.Cashe.GetCasheData<Library>().Item(_formId);
                else if (_projectItem.Id != _formId)
                    _projectItem = Workarea.Cashe.GetCasheData<Library>().Item(_formId);
                return _projectItem;
            }
            set
            {
                if (_projectItem == value) return;
                OnPropertyChanging(GlobalPropertyNames.ProjectItem);
                _projectItem = value;
                _formId = _projectItem == null ? 0 : _projectItem.Id;
                OnPropertyChanged(GlobalPropertyNames.ProjectItem);
            }
        }

        private int _documentId;
        /// <summary>
        /// ������������� ������� ��������
        /// </summary>
        public int DocumentId
        {
            get { return _documentId; }
            set
            {
                if (value == _documentId) return;
                OnPropertyChanging(GlobalPropertyNames.DocumentId);
                _documentId = value;
                OnPropertyChanged(GlobalPropertyNames.DocumentId);
            }
        }

        private Document _document;
        /// <summary>
        /// ������ ��������� 
        /// </summary>
        public Document Document
        {
            get
            {
                if (_documentId == 0)
                    return null;
                if (_document == null)
                    _document = Workarea.Cashe.GetCasheData<Document>().Item(_documentId);
                else if (_document.Id != _documentId)
                    _document = Workarea.Cashe.GetCasheData<Document>().Item(_documentId);
                return _document;
            }
            set
            {
                if (_document == value) return;
                OnPropertyChanging(GlobalPropertyNames.Document);
                _document = value;
                _documentId = _document == null ? 0 : _document.Id;
                OnPropertyChanged(GlobalPropertyNames.Document);
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
        #endregion

        #region ������������
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_formId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FormId, XmlConvert.ToString(_formId));
            if (_documentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DocumentId, XmlConvert.ToString(_documentId));
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.FormId) != null)
                _formId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FormId));
            if (reader.GetAttribute(GlobalPropertyNames.DocumentId) != null)
                _documentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DocumentId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion

        #region ���������
        FolderStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new FolderStruct {FormId = _formId, DocumentId = _documentId, MyCompanyId=_myCompanyId};
                return true;
            }
            return false;
        }
        /// <summary>
        /// ����������� ��������� �������
        /// </summary>
        public override void RestoreState()
        {
            base.RestoreState();
            FormId = _baseStruct.FormId;
            DocumentId = _baseStruct.DocumentId;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion

        #region ���� ������
        /// <summary>��������� ��������� �� ���� ������</summary>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _formId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _documentId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _viewListDocumentsId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _myCompanyId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>���������� �������� ���������� ��� �������� ��������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ��������</param>
        /// <param name="validateVersion">��������� �� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.FormId, SqlDbType.Int) {IsNullable = true};
            if (_formId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _formId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OperationId, SqlDbType.Int) {IsNullable = true};
            if (_documentId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _documentId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ViewDocId, SqlDbType.Int) { IsNullable = true };
            if (_viewListDocumentsId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _viewListDocumentsId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Folder> Members
        /// <summary>
        /// ����� �����
        /// </summary>
        /// <returns></returns>
        public List<IChain<Folder>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// ����� �����
        /// </summary>
        /// <param name="kind">��� �����</param>
        /// <returns></returns>
        public List<IChain<Folder>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Folder> IChains<Folder>.SourceList(int chainKindId)
        {
            return Chain<Folder>.GetChainSourceList(this, chainKindId);
        }
        List<Folder> IChains<Folder>.DestinationList(int chainKindId)
        {
            return Chain<Folder>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Folder>> GetValues(bool allKinds)
        {
            return CodeHelper<Folder>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Folder>.GetView(this, true);
        }
        #endregion
        #region IChainsAdvancedList<Folder,Note> Members

        List<IChainAdvanced<Folder, Note>> IChainsAdvancedList<Folder, Note>.GetLinks()
        {
            return ChainAdvanced<Folder, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Folder, Note>> IChainsAdvancedList<Folder, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Folder, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Folder, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Folder, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Folder, Note>.GetChainView()
        {
            return ChainValueView.GetView<Folder, Note>(this);
        }
        #endregion

        public List<Folder> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Folder> filter = null,
            int? formId=null, int? operationId=null, int? viewDocId=null,
            bool useAndFilter = false)
        {
            Folder item = new Folder { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("�� ������ ����� {0}, ������ ��� ������ ������ �� ���������������", GlobalMethodAlias.LoadList));
            }
            List<Folder> collection = new List<Folder>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindProcedure(GlobalMethodAlias.FindBy);
                        cmd.Parameters.Add(GlobalSqlParamNames.Count, SqlDbType.Int).Value = count;
                        if (hierarchyId != null && hierarchyId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;
                        if (userName != null && !string.IsNullOrEmpty(userName))
                            cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;
                        if (flags.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.Flags, SqlDbType.Int).Value = flags;
                        if (stateId.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateId;
                        if (!string.IsNullOrWhiteSpace(name))
                            cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = name;
                        if (kindId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindId;
                        if (!string.IsNullOrWhiteSpace(code))
                            cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;
                        if (!string.IsNullOrWhiteSpace(memo))
                            cmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255).Value = memo;
                        if (!string.IsNullOrWhiteSpace(flagString))
                            cmd.Parameters.Add(GlobalSqlParamNames.FlagString, SqlDbType.NVarChar, 50).Value = flagString;
                        if (templateId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TemplateId, SqlDbType.Int).Value = templateId;
                        if (useAndFilter)
                            cmd.Parameters.Add(GlobalSqlParamNames.UseAndFilter, SqlDbType.Bit).Value = true;

                        if (formId.HasValue && formId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.FormId, SqlDbType.Int).Value = formId.Value;
                        if (operationId.HasValue && operationId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.OperationId, SqlDbType.Int).Value = operationId.Value;
                        if (viewDocId.HasValue && viewDocId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.ViewDocId, SqlDbType.Int).Value = viewDocId.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Folder { Workarea = Workarea };
                            item.Load(reader);
                            Workarea.Cashe.SetCasheData(item);
                            if (filter != null && filter.Invoke(item))
                                collection.Add(item);
                            else if (filter == null)
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
}
