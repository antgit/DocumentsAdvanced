using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using BusinessObjects.Security;
using Microsoft.SqlServer.Server;

namespace BusinessObjects.Documents
{
    /// <summary>��������</summary>
    public sealed class Document : BaseCore<Document>, IChains<Document>, 
        IChainsAdvancedList<Document, FileData>, IReportChainSupport,
        ICodes<Document>,
        IChainsAdvancedList<Document, Knowledge>, ICompanyOwner,
        IChainsAdvancedList<Document, Message>,
        IChainsAdvancedList<Document, Note>
    {
        #region ���� ����������
        // ReSharper disable InconsistentNaming
        /// <summary>������ ���������� "���������� ��������� ��������", ������������� �������� 1</summary>
        public const int DOCUMENT_TYPESTORE = 1;
        /// <summary>������ ���������� "���������� ���������", ������������� �������� 2</summary>
        public const int DOCUMENT_TYPESALE = 2;
        /// <summary>������ ���������� "�����������", ������������� �������� 3</summary>
        public const int DOCUMENT_TYPEBOOKKEEP = 3;
        /// <summary>������ ���������� "���� ���������", ������������� �������� 4</summary>
        public const int DOCUMENT_TYPE�ONTRACT = 4;
        /// <summary>������ ���������� "���������������", ������������� �������� 5</summary>
        public const int DOCUMENT_TYPEPRICE = 5;
        /// <summary>������ ���������� "���������� ���������", ������������� �������� 6</summary>
        public const int DOCUMENT_TYPEFINANCE = 6;
        /// <summary>������ ���������� "��������� ���������", ������������� �������� 7</summary>
        public const int DOCUMENT_TYPETAX = 7;
        /// <summary>������ ���������� "���������� �������� �� �����", ������������� �������� 8</summary>
        public const int DOCUMENT_TYPEDISCOUNT = 8;
        /// <summary>������ ���������� "������", ������������� �������� 9</summary>
        public const int DOCUMENT_TYPESERVICE = 9;
        /// <summary>������ ���������� "������������", ������������� �������� 10</summary>
        public const int DOCUMENT_TYPEMANUFACT = 10;
        /// <summary>������ ���������� "�������� ��������", ������������� �������� 11</summary>
        public const int DOCUMENT_TYPEASSETS = 11;
        /// <summary>������ ���������� "������������", ������������� �������� 12</summary>
        public const int DOCUMENT_TYPEPL = 12;
        /// <summary>������ ���������� "�������� ����", ������������� �������� 13</summary>
        public const int DOCUMENT_TYPEPEOPLE = 13;
        /// <summary>������ ���������� "���������", ������������� �������� 14</summary>
        public const int DOCUMENT_TYPEMKTG = 14;
        // ReSharper restore InconsistentNaming 
        #endregion

        /// <summary>
        /// �������� ������ ����������
        /// </summary>
        /// <param name="collection">��������� ��� ��������</param>
        /// <returns></returns>
        public Dictionary<Document, bool> DeleteList(List<Document> collection)
        {
            Dictionary<Document, bool> ret = new Dictionary<Document, bool>();

            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = FindProcedure("DocumentDeleteList");

                    DatabaseHelper.AddTvpParamKeyListId(cmd, collection.Select(s => s.Id));
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int docId = reader.GetInt32(0);
                        bool v = reader.GetBoolean(1);

                        ret.Add(collection.Find(s => s.Id == docId), v);
                    }
                }

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return ret;
        }
        /// <summary>
        /// ��������� ���������� �� ����� ����������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="id">������������� ����� ����������</param>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentByFolder(Workarea wa, int id)
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Document>().Entity.FindMethod("Document.DocumentsLoadByFolder").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = wa.Period.Start;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = wa.Period.End;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
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
        /// ��������� ���������� �� ��������������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="id">������������� ����� ����������</param>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentByAgent(Workarea wa, int id, int? kindId=null, DateTime? ds=null, DateTime? de=null )
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Document>().Entity.FindMethod("Document.DocumentsLoadByAgent").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = ds??wa.Period.Start;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = de??wa.Period.End;
                        if(kindId.HasValue)
                        {
                            cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindId.Value;    
                        }
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }

        /// <summary>
        /// ��������� ���������� � �������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="id">������������� ������� ����������</param>
        /// <param name="ds">���� ������ (������������) </param>
        /// <param name="de">���� ��������� (������������)</param>
        /// <param name="userName">��� ������������</param>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentByTypeValue(Workarea wa, int id, DateTime ds, DateTime de, string userName)
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Document>().Entity.FindMethod("Document.DocumentsLoadByTypeValue").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = ds;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = de;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
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
        /// ��������� ���������� ���������� ����
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="id">������������� ������� ����������</param>
        /// <param name="ds">���� ������ (������������) </param>
        /// <param name="de">���� ��������� (������������)</param>
        /// <param name="userName">��� ������������</param>
        /// <param name="folderId">������������� �����</param>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentByKind(Workarea wa, int id, DateTime ds, DateTime de, string userName, int? folderId=null)
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Document>().Entity.FindMethod("Document.DocumentsLoadByKind").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = ds;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = de;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;
                        if(folderId.HasValue)
                        {
                            cmd.Parameters.Add(GlobalSqlParamNames.FolderId, SqlDbType.Int).Value = folderId.Value;
                        }
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
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
        /// ��������� �������� ���������� ���������� ����
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="id">������������� ������� ����������</param>
        /// <param name="ds">���� ������ (������������) </param>
        /// <param name="de">���� ��������� (������������)</param>
        /// <param name="userName">��� ������������</param>
        /// <param name="folderId">������������� �����</param>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentTemplatesByKind(Workarea wa, int id, DateTime ds, DateTime de, string userName, int? folderId=null)
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Document>().Entity.FindMethod("Document.DocumentTemplatesLoadByKind").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = ds;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = de;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;
                        if(folderId.HasValue)
                        {
                            cmd.Parameters.Add(GlobalSqlParamNames.FolderId, SqlDbType.Int).Value = folderId.Value;
                        }
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
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
        /// 
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="id"></param>
        /// <param name="ds">������ �������</param>
        /// <param name="de">����� �������</param>
        /// <param name="userName">��� ������������</param>
        /// <param name="folderCodeFind">��� ����� ����������</param>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentByKind(Workarea wa, int id, DateTime ds, DateTime de, string userName, string folderCodeFind)
        {
            if (string.IsNullOrEmpty(folderCodeFind))
                return GetCollectionDocumentByKind(wa, id, ds, de, userName);
            else
            {
                Folder f = wa.GetFolderByCodeFind(folderCodeFind);
                if(f!=null)
                    return GetCollectionDocumentByKind(wa, id, ds, de, userName, f.Id);
                else
                    return GetCollectionDocumentByKind(wa, id, ds, de, userName);
            }
        }

        

        /// <summary>
        /// ��������� ���������� �� ���������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="id">������������� ����� ����������</param>
        /// <param name="start">���� ������</param>
        /// <param name="end">���� ���������</param>
        /// <remarks>�� ��������� ������ �� �������� �������</remarks>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentByAnalitic(Workarea wa, int id, DateTime? start=null, DateTime? end=null )
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Document>().Entity.FindMethod("Document.DocumentsLoadByAnalitic").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        if (start==null)
                        {
                            cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = wa.Period.Start;
                            cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = wa.Period.End;
                        }
                        else
                        {
                            cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = start.Value;
                            cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = end.Value;
                        }
                        
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
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
        /// ��������� ���������� �� ������ � ������� ���������� ���������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="id">������������� ������</param>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentSalesByProduct(Workarea wa, int id)
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Document>().Entity.FindMethod("DocumentsSalesLoadByProduct").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = wa.Period.Start;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = wa.Period.End;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
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
        /// ��������� ���������� �� �������������� � ������� ���������� ���������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="id">������������� ����� ����������</param>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentSalesByAgent(Workarea wa, int id)
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Document>().Entity.FindMethod("DocumentsSalesLoadByAgent").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = wa.Period.Start;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = wa.Period.End;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
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
        /// ��������� ���������� �� �������� ������ ����������
        /// </summary>
        /// <remarks>��������� �������� ��������� �� ���� ����� ���������� �������� � ������ �������� ����������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="id">������������� ��������</param>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentByHierarchyFolder(Workarea wa, int id)
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.CommandText =
                            wa.Empty<Document>().Entity.FindMethod("Document.HierarchyLoadNestedDocumentsByFolder").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = wa.Period.Start;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = wa.Period.End;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
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

        /// <summary>�����������</summary>
        public Document():base()
        {
            EntityId = (short)WhellKnownDbEntity.Document;
            _date = DateTime.Now;
            AgentNamesAuto = true;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override Document Clone(bool endInit)
        {
            Document obj = base.Clone(endInit);
            obj.Accounting = Accounting;
            obj.AccountingWfId = AccountingWfId;
            obj.AgentDepartmentToId = AgentDepartmentToId;
            obj.AgentDepartmentToName = AgentDepartmentToName;
            obj.AgentDepartmentFromName = AgentDepartmentFromName;
            obj.AgentDepartmentFromId = AgentDepartmentFromId;
            obj.AgentToId = AgentToId;
            obj.AgentToName = AgentToName;
            obj.AgentFromId = AgentFromId;
            obj.AgentFromName = AgentFromName;
            obj.CfoId = CfoId;
            obj.ClientId = ClientId;
            obj.CurrencyBaseId = obj.CurrencyBaseId;
            obj.CurrencyId = CurrencyId;
            obj.CurrencyTransactionId = CurrencyTransactionId;
            obj.Date = Date;
            obj.FolderId = Folder.Id;
            obj.MyCompanyId = MyCompanyId;
            obj.ProjectItemId = ProjectItemId;
            obj.Summa = Summa;
            obj.SummaBase = SummaBase;
            obj.SummaTax = SummaTax;
            obj.SummaTotal = SummaTotal;
            obj.SummaTransaction = SummaTransaction;
            obj.Time = Time;
            if (endInit)
                OnEndInit();
            return obj;
        }

        internal void InternalOnCreated()
        {
            OnCreated();
        }

        internal void InternalOnUpdated()
        {
            OnUpdated();
        }
        protected override void OnCreated()
        {
            base.OnCreated();
            if (_stringData != null && _stringData.Id != this.Id)
            {
                _stringData.Id = Id;
                _stringData.Save();
            }
            
        }
        protected override void OnUpdated()
        {
            base.OnUpdated();
            if (_stringData != null && _stringData.Id != this.Id)
            {
                _stringData.Id = Id;
                _stringData.Save();
            }

        }
        #region ��������

        private DocumentStringData _stringData;
        /// <summary>
        /// �������������� ��������� ������
        /// </summary>
        /// <returns></returns>
        public DocumentStringData GetStringData()
        {
            if (_stringData==null)
            {
                _stringData = new DocumentStringData{ Workarea = Workarea, Id = Id};
                if (Id != 0)
                {
                    _stringData = Workarea.GetObject<DocumentStringData>(Id);
                    if (_stringData.Id == 0)
                        _stringData.Id = Id;
                }   
            }
            if (Id != 0 && _stringData!=null)
            {
                if (_stringData.Id == 0)
                    _stringData.Id = Id;
            }
            return _stringData;
        }

        private List<DocumentXml> _xmlData;
        /// <summary>
        /// Xml ������ ���������
        /// </summary>
        /// <returns></returns>
        public List<DocumentXml> GetXmlData()
        {

            if(_xmlData== null && !IsNew)
            {
                _xmlData = DocumentXml.GetCollection(this);
            }
            if (IsNew)
                _xmlData = new List<DocumentXml>();
            return _xmlData;
        }
        /// <summary>
        /// ����� ������ xml ������
        /// </summary>
        /// <returns></returns>
        public DocumentXml NewXmlDataRow()
        {
            GetXmlData();
            DocumentXml newXmlData = new DocumentXml { Workarea = Workarea, Owner = this };
            _xmlData.Add(newXmlData);
            return newXmlData;
        }
        /// <summary>
        /// ������������� ������������ ��������������� �������������
        /// </summary>
        public bool AgentNamesAuto { get; set; }
        private DateTime _date;
        /// <summary>���� ���������</summary>
        public DateTime Date
        {
            get { return _date; }
            set 
            {
                if (_date == value) return;
                OnPropertyChanging(GlobalPropertyNames.Date);
                _date = value;
                OnPropertyChanged(GlobalPropertyNames.Date);
            }
        }

        private int _projectItemId;
        /// <summary>������������� ����� ���������</summary>
        public int ProjectItemId
        {
            get { return _projectItemId; }
            set 
            {
                if (_projectItemId == value) return;
                OnPropertyChanging(GlobalPropertyNames.ProjectItemId);
                _projectItemId = value;
                OnPropertyChanged(GlobalPropertyNames.ProjectItemId);
            }
        }

        private Library _projectItem;
        /// <summary>����� ���������</summary>
        public Library ProjectItem
        {
            get
            {
                if (_projectItemId == 0)
                    return null;
                if (_projectItem == null)
                    _projectItem = Workarea.Cashe.GetCasheData<Library>().Item(_projectItemId);
                else if (_projectItem.Id != _projectItemId)
                    _projectItem = Workarea.Cashe.GetCasheData<Library>().Item(_projectItemId);
                return _projectItem;
            }
            set
            {
                if (_projectItem == value) return;
                OnPropertyChanging(GlobalPropertyNames.ProjectItem);
                _projectItem = value;
                _projectItemId = _projectItem == null ? 0 : _projectItem.Id;
                OnPropertyChanged(GlobalPropertyNames.ProjectItem);
            }
        }

        private int _folderId;
        /// <summary>������������� �������� ����� ���������</summary>
        public int FolderId
        {
            get { return _folderId; }
            set 
            {
                if (_folderId == value) return;
                OnPropertyChanging(GlobalPropertyNames.FolderId);
                _folderId = value;
                OnPropertyChanged(GlobalPropertyNames.FolderId);
            }
        }

        private Folder _folder;
        /// <summary>����� ����������</summary>
        public Folder Folder
        {
            get
            {
                if (_folderId == 0)
                    return null;
                if (_folder == null)
                    _folder = Workarea.Cashe.GetCasheData<Folder>().Item(_folderId);
                else if (_folder.Id != _folderId)
                    _folder = Workarea.Cashe.GetCasheData<Folder>().Item(_folderId);
                return _folder;
            }
            set
            {
                if (_folder == value) return;
                OnPropertyChanging(GlobalPropertyNames.Folder);
                _folder = value;
                _folderId = _folder == null ? 0 : _folder.Id;
                OnPropertyChanged(GlobalPropertyNames.Folder);
            }
        }

        private int _agentDepartmentFromId;
        /// <summary>������������� ��������������-��������� "���"</summary>
        public int AgentDepartmentFromId
        {
            get { return _agentDepartmentFromId; }
            set
            {
                if (_agentDepartmentFromId == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentDepartmentFromId);
                _agentDepartmentFromId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentDepartmentFromId);
            }
        }
        private int _agentDepartmentToId;
        /// <summary>������������� ��������������-��������� "����"</summary>
        public int AgentDepartmentToId
        {
            get { return _agentDepartmentToId; }
            set
            {
                if (_agentDepartmentToId == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentDepartmentToId);
                _agentDepartmentToId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentDepartmentToId);
            }
        }

        private int _agentFromId;
        /// <summary>������������� �������������� "���"</summary>
        public int AgentFromId
        {
            get { return _agentFromId; }
            set 
            {
                if (_agentFromId == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentFromId);
                _agentFromId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentFromId);
            }
        }
        private string _agentFromName;
        /// <summary>������������ �������������� "���"</summary>
        public string AgentFromName
        {
            get { return _agentFromName; }
            set 
            {
                if (_agentFromName == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentFromName);
                _agentFromName = value;
                OnPropertyChanged(GlobalPropertyNames.AgentFromName);
            }
        }
        
        private string _agentToName;
        /// <summary>������������ �������������� "����"</summary>
        public string AgentToName
        {
            get { return _agentToName; }
            set 
            {
                if (_agentToName == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentToName);
                _agentToName = value;
                OnPropertyChanged(GlobalPropertyNames.AgentToName);
            }
        }

        private string _agentDepartmentFromName;
        /// <summary>������������ ��������������-��������� "���"</summary>
        public string AgentDepartmentFromName
        {
            get { return _agentDepartmentFromName; }
            set 
            {
                if (_agentDepartmentFromName == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentDepartmentFromName);
                _agentDepartmentFromName = value;
                OnPropertyChanged(GlobalPropertyNames.AgentDepartmentFromName);
            }
        }

        private string _agentDepartmentToName;
        /// <summary>������������ ��������������-��������� "����"</summary>
        public string AgentDepartmentToName
        {
            get { return _agentDepartmentToName; }
            set 
            {
                if (_agentDepartmentToName == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentDepartmentToName);
                _agentDepartmentToName = value;
                OnPropertyChanged(GlobalPropertyNames.AgentDepartmentToName);
            }
        }

        private Agent _agentDepartmentFrom;
        /// <summary>������������� "���"</summary>
        public Agent AgentDepartmentFrom
        {
            get
            {
                if (_agentDepartmentFromId == 0)
                    return null;
                if (_agentDepartmentFrom == null)
                    _agentDepartmentFrom = Workarea.Cashe.GetCasheData<Agent>().Item(_agentDepartmentFromId);
                else if (_agentDepartmentFrom.Id != _agentDepartmentFromId)
                    _agentDepartmentFrom = Workarea.Cashe.GetCasheData<Agent>().Item(_agentDepartmentFromId);
                return _agentDepartmentFrom;
            }
            set
            {
                if (_agentFrom == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentDepartmentFrom);
                _agentDepartmentFrom = value;
                _agentDepartmentFromId = _agentDepartmentFrom == null ? 0 : _agentDepartmentFrom.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentDepartmentFrom);
            }
        }

        private Agent _agentDepartmentTo;
        /// <summary>������������� "����"</summary>
        public Agent AgentDepartmentTo
        {
            get
            {
                if (_agentDepartmentToId == 0)
                    return null;
                if (_agentDepartmentTo == null)
                    _agentDepartmentTo = Workarea.Cashe.GetCasheData<Agent>().Item(_agentDepartmentToId);
                else if (_agentDepartmentTo.Id != _agentDepartmentToId)
                    _agentDepartmentTo = Workarea.Cashe.GetCasheData<Agent>().Item(_agentDepartmentToId);
                return _agentDepartmentTo;
            }
            set
            {
                if (_agentDepartmentTo == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentDepartmentTo);
                _agentDepartmentTo = value;
                _agentDepartmentToId = _agentDepartmentTo == null ? 0 : _agentDepartmentTo.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentDepartmentTo);
            }
        }

        private Agent _agentFrom;
        /// <summary>������������� "���"</summary>
        public Agent AgentFrom
        {
            get
            {
                if (_agentFromId == 0)
                    return null;
                if (_agentFrom == null)
                    _agentFrom = Workarea.Cashe.GetCasheData<Agent>().Item(_agentFromId);
                else if (_agentFrom.Id != _agentFromId)
                    _agentFrom = Workarea.Cashe.GetCasheData<Agent>().Item(_agentFromId);
                return _agentFrom;
            }
            set
            {
                if (_agentFrom == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentFrom);
                _agentFrom = value;
                _agentFromId = _agentFrom == null ? 0 : _agentFrom.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentFrom);
            }
        }

        private int _agentToId;
        /// <summary>������������� �������������� "����"</summary>
        public int AgentToId
        {
            get { return _agentToId; }
            set 
            {
                if (_agentToId == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentToId);
                _agentToId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentToId);
            }
        }

        private Agent _agentTo;
        /// <summary>������������� "����"</summary>
        public Agent AgentTo
        {
            get
            {
                if (_agentToId == 0)
                    return null;
                if (_agentTo == null)
                    _agentTo = Workarea.Cashe.GetCasheData<Agent>().Item(_agentToId);
                else if (_agentTo.Id != _agentToId)
                    _agentTo = Workarea.Cashe.GetCasheData<Agent>().Item(_agentToId);
                return _agentTo;
            }
            set
            {
                if (_agentTo == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentTo);
                _agentTo = value;
                _agentToId = _agentTo == null ? 0 : _agentTo.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentTo);
            }
        }

        private int _currencyId;
        /// <summary>������������� ������</summary>
        public int CurrencyId
        {
            get { return _currencyId; }
            set 
            {
                if (_currencyId == value) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyId);
                _currencyId = value;
                OnPropertyChanged(GlobalPropertyNames.CurrencyId);
            }
        }

        private Currency _currency;
        /// <summary>������</summary>
        public Currency Currency
        {
            get
            {
                if (_currencyId == 0)
                    return null;
                if (_currency == null)
                    _currency = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyId);
                else if (_currency.Id != _currencyId)
                    _currency = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyId);
                return _currency;
            }
            set
            {
                if (_currency == value) return;
                OnPropertyChanging(GlobalPropertyNames.Currency);
                _currency = value;
                _currencyId = _currency == null ? 0 : _currency.Id;
                OnPropertyChanged(GlobalPropertyNames.Currency);
            }
        }
        private int _currencyBaseId;
        /// <summary>������������� ������� ������ (������ ������)</summary>
        public int CurrencyBaseId
        {
            get { return _currencyBaseId; }
            set 
            {
                if (_currencyBaseId == value) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyBaseId);
                _currencyBaseId = value;
                OnPropertyChanged(GlobalPropertyNames.CurrencyBaseId);
            }
        }

        private Currency _currencyBase;
        /// <summary>
        /// ������� ������ (������ ������)
        /// </summary>
        public Currency CurrencyBase
        {
            get
            {
                if (_currencyBaseId == 0)
                    return null;
                if (_currencyBase == null)
                    _currencyBase = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyBaseId);
                else if (_currencyBase.Id != _currencyBaseId)
                    _currencyBase = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyBaseId);
                return _currencyBase;
            }
            set
            {
                if (_currencyBase == value) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyBase);
                _currencyBase = value;
                _currencyBaseId = _currencyBase == null ? 0 : _currencyBase.Id;
                OnPropertyChanged(GlobalPropertyNames.CurrencyBase);
            }
        }
        

        private int _currencyTransactionId;
        /// <summary>������������� ������ ������</summary>
        public int CurrencyTransactionId
        {
            get { return _currencyTransactionId; }
            set 
            {
                if (_currencyTransactionId == value) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyTransactionId);
                _currencyTransactionId = value;
                OnPropertyChanged(GlobalPropertyNames.CurrencyTransactionId);
            }
        }

        private Currency _currencyTransaction;
        /// <summary>
        /// ������ ������
        /// </summary>
        public Currency CurrencyTransaction
        {
            get
            {
                if (_currencyTransactionId == 0)
                    return null;
                if (_currencyTransaction == null)
                    _currencyTransaction = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyTransactionId);
                else if (_currencyTransaction.Id != _currencyTransactionId)
                    _currencyTransaction = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyTransactionId);
                return _currencyTransaction;
            }
            set
            {
                if (_currencyTransaction == value) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyTransaction);
                _currencyTransaction = value;
                _currencyTransactionId = _currencyTransaction == null ? 0 : _currencyTransaction.Id;
                OnPropertyChanged(GlobalPropertyNames.CurrencyTransaction);
            }
        }
        
	

        private decimal _summa;
        /// <summary>����� ��������� ��� ����� �������</summary>
        public decimal Summa
        {
            get { return _summa; }
            set 
            {
                if (_summa == value) return;
                OnPropertyChanging(GlobalPropertyNames.Summa);
                _summa = value;
                OnPropertyChanged(GlobalPropertyNames.Summa);
                SummaTotal = _summa + _summaTax;
            }
        }

        private decimal _summaBase;
        /// <summary>����� � ������� ������</summary>
        public decimal SummaBase
        {
            get { return _summaBase; }
            set 
            {
                if (_summaBase == value) return;
                OnPropertyChanging(GlobalPropertyNames.SummaBase);
                _summaBase = value;
                OnPropertyChanged(GlobalPropertyNames.SummaBase);
            }
        }

        private decimal _summaTransaction;
        /// <summary>����� � ������ ������</summary>
        public decimal SummaTransaction
        {
            get { return _summaTransaction; }
            set 
            {
                if (_summaTransaction == value) return;
                OnPropertyChanging(GlobalPropertyNames.SummaTransaction);
                _summaTransaction = value;
                OnPropertyChanged(GlobalPropertyNames.SummaTransaction);
            }
        }

        private decimal _summaTax;
        /// <summary>
        /// ����� �������
        /// </summary>
        public decimal SummaTax
        {
            get { return _summaTax; }
            set
            {
                if (value == _summaTax) return;
                OnPropertyChanging(GlobalPropertyNames.SummaTax);
                _summaTax = value;
                OnPropertyChanged(GlobalPropertyNames.SummaTax);
                SummaTotal = _summa + _summaTax;
            }
        }

        private decimal _summaTotal;

        /// <summary>
        /// ����� "�����", ������������� ��� Summa + SummaTax
        /// </summary>
        public decimal SummaTotal
        {
            get { return _summaTotal; }
            internal set
            {
                if (value == _summaTotal) return;
                OnPropertyChanging(GlobalPropertyNames.SummaTotal);
                _summaTotal = value;
                OnPropertyChanged(GlobalPropertyNames.SummaTotal);
            }
        }

        //private string _userName;
        ///// <summary>������������ ��������� ��� ���������� ��������</summary>
        //public string UserName
        //{
        //    get { return _userName; }
        //}

        private string _number;
        /// <summary>����� ���������</summary>
        public string Number
        {
            get { return _number; }
            set 
            {
                if (_number == value) return;
                OnPropertyChanging(GlobalPropertyNames.Number);
                _number = value;
                OnPropertyChanged(GlobalPropertyNames.Number);
            }
        }

        private TimeSpan _time;
        public TimeSpan Time
        {
            get
            {
                // 
                // TODO: �������� �������� ������� ���������
                if (_time.Equals(TimeSpan.Zero))
                    _time = Date.TimeOfDay;
                return _time;
                
            }
            set
            {
                if (value == _time) return;
                OnPropertyChanging(GlobalPropertyNames.Time);
                _time = value;
                OnPropertyChanged(GlobalPropertyNames.Time);
            }
        }


        private int _accountingWfId;
        /// <summary>
        /// ������������� ������ �������� ������������ ��������
        /// </summary>
        public int AccountingWfId
        {
            get { return _accountingWfId; }
            set
            {
                if (value == _accountingWfId) return;
                OnPropertyChanging(GlobalPropertyNames.AccountingWfId);
                _accountingWfId = value;
                OnPropertyChanged(GlobalPropertyNames.AccountingWfId);
            }
        }

        private Ruleset _accountingWf;
        /// <summary>
        /// ������ �������� ������������ ��������
        /// </summary>
        public Ruleset AccountingWf
        {
            get
            {
                if (_accountingWfId == 0)
                    return null;
                if (_accountingWf == null)
                    _accountingWf = Workarea.Cashe.GetCasheData<Ruleset>().Item(_accountingWfId);
                else if (_accountingWf.Id != _accountingWfId)
                    _accountingWf = Workarea.Cashe.GetCasheData<Ruleset>().Item(_accountingWfId);
                return _accountingWf;
            }
            set
            {
                if (_accountingWf == value) return;
                OnPropertyChanging(GlobalPropertyNames.AccountingWf);
                _accountingWf = value;
                _accountingWfId = _accountingWf == null ? 0 : _accountingWf.Id;
                OnPropertyChanged(GlobalPropertyNames.AccountingWf);
            }
        }
        

        private int _accounting;
        public int Accounting
        {
            get { return _accounting; }
            set
            {
                if (value == _accounting) return;
                OnPropertyChanging(GlobalPropertyNames.Accounting);
                _accounting = value;
                OnPropertyChanged(GlobalPropertyNames.Accounting);
            }
        }


        private int _cfoId;
        /// <summary>
        /// ������������� ������ ���������� ���������������
        /// </summary>
        public int CfoId
        {
            get { return _cfoId; }
            set
            {
                if (value == _cfoId) return;
                OnPropertyChanging(GlobalPropertyNames.CfoId);
                _cfoId = value;
                OnPropertyChanged(GlobalPropertyNames.CfoId);
            }
        }

        private Analitic _cfo;
        /// <summary>
        /// ����� ���������� ���������������
        /// </summary>
        public Analitic Cfo
        {
            get
            {
                if (_cfoId == 0)
                    return null;
                if (_cfo == null)
                    _cfo = Workarea.Cashe.GetCasheData<Analitic>().Item(_cfoId);
                else if (_cfo.Id != _cfoId)
                    _cfo = Workarea.Cashe.GetCasheData<Analitic>().Item(_cfoId);
                return _cfo;
            }
            set
            {
                if (_cfo == value) return;
                OnPropertyChanging(GlobalPropertyNames.Cfo);
                _cfo = value;
                _cfoId = _cfo == null ? 0 : _cfo.Id;
                OnPropertyChanged(GlobalPropertyNames.Cfo);
            }
        }
        // MyCompanyId, ClientId


        private int _myCompanyId;
        /// <summary>
        /// ������������� �����������, �������� ����������� ��������
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
        /// ��� ��������, ����������� �������� ����������� ��������
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
        

        private int _clientId;
        /// <summary>
        /// ������������� �������
        /// </summary>
        public int ClientId
        {
            get { return _clientId; }
            set
            {
                if (value == _clientId) return;
                OnPropertyChanging(GlobalPropertyNames.ClientId);
                _clientId = value;
                OnPropertyChanged(GlobalPropertyNames.ClientId);
            }
        }


        private Agent _client;
        /// <summary>
        /// ������, �������� ����������� ��������
        /// </summary>
        /// <remarks>��� ���������� ���������� ����������� ������������� MyCompany</remarks>
        public Agent Client
        {
            get
            {
                if (_clientId == 0)
                    return null;
                if (_client == null)
                    _client = Workarea.Cashe.GetCasheData<Agent>().Item(_clientId);
                else if (_client.Id != _clientId)
                    _client = Workarea.Cashe.GetCasheData<Agent>().Item(_clientId);
                return _client;
            }
            set
            {
                if (_client == value) return;
                OnPropertyChanging(GlobalPropertyNames.Client);
                _client = value;
                _clientId = _client == null ? 0 : _client.Id;
                OnPropertyChanged(GlobalPropertyNames.Client);
            }
        }


        private int _userOwnerId;
        /// <summary>
        /// ������������� ������������ ��������� ���������
        /// </summary>
        public int UserOwnerId
        {
            get { return _userOwnerId; }
            set
            {
                if (value == _userOwnerId) return;
                OnPropertyChanging(GlobalPropertyNames.UserOwnerId);
                _userOwnerId = value;
                OnPropertyChanged(GlobalPropertyNames.UserOwnerId);
            }
        }



        private Uid _userOwner;
        /// <summary>
        /// ������������ ��������
        /// </summary>
        public Uid UserOwner
        {
            get
            {
                if (_userOwnerId == 0)
                    return null;
                if (_userOwner == null)
                    _userOwner = Workarea.Cashe.GetCasheData<Uid>().Item(_userOwnerId);
                else if (_userOwner.Id != _userOwnerId)
                    _userOwner = Workarea.Cashe.GetCasheData<Uid>().Item(_userOwnerId);
                return _userOwner;
            }
            set
            {
                if (_userOwner == value) return;
                OnPropertyChanging(GlobalPropertyNames.UserOwner);
                _userOwner = value;
                _userOwnerId = _userOwner == null ? 0 : _userOwner.Id;
                OnPropertyChanged(GlobalPropertyNames.UserOwner);
            }
        }
        
        /// <summary>
        /// ��� ���������, ������ � �������� ��������� ��������
        /// </summary>
        /// <returns></returns>
        public short GetTypeValue()
        {
            return EntityKind.ExtractEntityKind(KindId);
        }
        /// <summary>
        /// ����������� �������� ���� ��������� � �������
        /// </summary>
        /// <returns></returns>
        public short GetKindValue()
        {
            return EntityKind.ExtractSubKind(KindId);
        }

        #endregion

        #region ������������
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            //if (_date)
                writer.WriteAttributeString(GlobalPropertyNames.Date, XmlConvert.ToString(_date));
            if (_projectItemId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProjectItemId, XmlConvert.ToString(_projectItemId));
            if (_folderId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.FolderId, XmlConvert.ToString(_folderId));
            if (_agentDepartmentFromId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentDepartmentFromId, XmlConvert.ToString(_agentDepartmentFromId));
            if (_agentDepartmentToId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentDepartmentToId, XmlConvert.ToString(_agentDepartmentToId));
            if (_agentFromId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentFromId, XmlConvert.ToString(_agentFromId));
            if (!string.IsNullOrEmpty(_agentFromName))
                writer.WriteAttributeString(GlobalPropertyNames.AgentFromName, _agentFromName);
            if (!string.IsNullOrEmpty(_agentDepartmentToName))
                writer.WriteAttributeString(GlobalPropertyNames.AgentToName, _agentDepartmentToName);
            if (!string.IsNullOrEmpty(_agentDepartmentFromName))
                writer.WriteAttributeString(GlobalPropertyNames.AgentDepartmentFromName, _agentDepartmentFromName);
            if (!string.IsNullOrEmpty(_agentDepartmentToName))
                writer.WriteAttributeString(GlobalPropertyNames.AgentDepartmentToName, _agentDepartmentToName);
            if (_agentToId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentToId, XmlConvert.ToString(_agentToId));
            if (_currencyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CurrencyId, XmlConvert.ToString(_currencyId));
            if (_currencyBaseId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CurrencyBaseId, XmlConvert.ToString(_currencyBaseId));
            if (_currencyTransactionId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CurrencyTransactionId, XmlConvert.ToString(_currencyTransactionId));
            if (_summa != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Summa, XmlConvert.ToString(_summa));
            if (_summaBase != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SummaBase, XmlConvert.ToString(_summaBase));
            if (_summaTransaction != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SummaTransaction, XmlConvert.ToString(_summaTransaction));
            if (_summaTax != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SummaTax, XmlConvert.ToString(_summaTax));
            if (_summaTotal != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SummaTotal, XmlConvert.ToString(_summaTotal));
            if (!string.IsNullOrEmpty(_number))
                writer.WriteAttributeString(GlobalPropertyNames.Number, _number);
            if (_accountingWfId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AccountingWfId, XmlConvert.ToString(_accountingWfId));
            if (_accounting != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Accounting, XmlConvert.ToString(_accounting));
            if (_cfoId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CfoId, XmlConvert.ToString(_cfoId));
            if (_myCompanyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
            if (_clientId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ClientId, XmlConvert.ToString(_clientId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Date) != null)
                _date = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.Date));
            if (reader.GetAttribute(GlobalPropertyNames.ProjectItemId) != null)
                _projectItemId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProjectItemId));
            if (reader.GetAttribute(GlobalPropertyNames.FolderId) != null)
                _folderId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FolderId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentDepartmentFromId) != null)
                _agentDepartmentFromId = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.AgentDepartmentFromId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentDepartmentToId) != null)
                _agentDepartmentToId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentDepartmentToId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentFromId) != null)
                _agentFromId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentFromId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentFromName) != null)
                _agentFromName = reader.GetAttribute(GlobalPropertyNames.AgentFromName);
            if (reader.GetAttribute(GlobalPropertyNames.AgentToName) != null)
                _agentToName = reader.GetAttribute(GlobalPropertyNames.AgentToName);
            if (reader.GetAttribute(GlobalPropertyNames.AgentDepartmentFromName) != null)
                _agentDepartmentFromName = reader.GetAttribute(GlobalPropertyNames.AgentDepartmentFromName);
            if (reader.GetAttribute(GlobalPropertyNames.AgentDepartmentToName) != null)
                _agentDepartmentToName = reader.GetAttribute(GlobalPropertyNames.AgentDepartmentToName);
            if (reader.GetAttribute(GlobalPropertyNames.AgentToId) != null)
                _agentToId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentToId));
            if (reader.GetAttribute(GlobalPropertyNames.CurrencyId) != null)
                _currencyId = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.CurrencyId));
            if (reader.GetAttribute(GlobalPropertyNames.CurrencyBaseId) != null)
                _currencyBaseId= XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CurrencyBaseId));
            if (reader.GetAttribute(GlobalPropertyNames.CurrencyTransactionId) != null)
                _currencyTransactionId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CurrencyTransactionId));
            if (reader.GetAttribute(GlobalPropertyNames.Summa) != null)
                _summa = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Summa));
            if (reader.GetAttribute(GlobalPropertyNames.SummaBase) != null)
                _summaBase = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaBase));
            if (reader.GetAttribute(GlobalPropertyNames.SummaTransaction) != null)
                _summaTransaction = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaTransaction));
            if (reader.GetAttribute(GlobalPropertyNames.SummaTax) != null)
                _summaTax = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaTax));
            if (reader.GetAttribute(GlobalPropertyNames.SummaTotal) != null)
                _summaTotal = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaTotal));
            if (reader.GetAttribute(GlobalPropertyNames.Number) != null)
                _number = reader.GetAttribute(GlobalPropertyNames.Number);
            if (reader.GetAttribute(GlobalPropertyNames.AccountingWfId) != null)
                _accountingWfId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AccountingWfId));
            if (reader.GetAttribute(GlobalPropertyNames.Accounting) != null)
                _accounting = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Accounting));
            if (reader.GetAttribute(GlobalPropertyNames.CfoId) != null)
                _cfoId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CfoId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
            if (reader.GetAttribute(GlobalPropertyNames.ClientId) != null)
                _clientId = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.ClientId));
        }
        #endregion

        #region �������������� �������
        //System.Collections.Hashtable _documents;
        ///// <summary>�������������� ������ ���������</summary>
        ///// <typeparam name="T">��� ��������������� ��������</typeparam>
        ///// <returns></returns>
        //public T GetDocument<T>() where T: IDocument
        //{
        //    if (_documents == null)
        //        _documents = new System.Collections.Hashtable();
        //    T item = System.Activator.CreateInstance<T>();
        //    if (_documents.ContainsKey(item.GetType()))
        //    {
        //        return (T)_documents[item.GetType()];
        //    }
        //    item.Workarea = Workarea;
        //    item.Load(this);
        //    _documents.Add(item.GetType(), item);
        //    return item;
        //}

        //private System.Collections.Generic.Dictionary<Document, object> _journals;
        //public List<T> GetDocumentDeails<T>() where T : IDocumentDetail
        //{
        //    if (_journals == null)
        //        _journals = new System.Collections.Generic.Dictionary<Document, object>();
        //    if (_journals.ContainsKey(this))
        //    {
        //        return (List<T>)_journals[this];
        //    }
        //    List<T> coll = RefreshDocumentDeails<T>();
        //    _journals.Add(this, coll);
        //    return coll;
        //}

        ///// <summary>����������� ���������</summary>
        ///// <typeparam name="T">��� �����������</typeparam>
        ///// <returns></returns>
        //public List<T> RefreshDocumentDeails<T>() where T : IDocumentDetail
        //{
        //    T item = System.Activator.CreateInstance<T>();
        //    item.Workarea = Workarea;
        //    List<T> collection = new List<T>();
        //    using (SqlConnection cnn = Workarea.GetDatabaseConnection())
        //    {
        //        if (cnn == null || Id==0) return collection;

        //        try
        //        {
        //            using (SqlCommand cmd = cnn.CreateCommand())
        //            {
        //                string procedureName = string.Empty;
        //                if (item.EntityId != 0)
        //                {
        //                    ProcedureMap method = item.EntityDocument.FindMethod("LoadByDocId");
        //                    if (method != null)
        //                    {
        //                        procedureName = method.FullName;
        //                    }
        //                }
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = procedureName;
        //                cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
        //                SqlDataReader reader = cmd.ExecuteReader();

        //                if (reader != null)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        item = System.Activator.CreateInstance<T>();
        //                        item.Workarea = Workarea;
        //                        item.Document = this;
        //                        System.Action<SqlDataReader> loader = item.Load;
        //                        loader(reader);
        //                        collection.Add(item);
        //                    }
        //                    reader.Close();
        //                }
        //            }
        //        }
        //        catch (SqlException)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            cnn.Close();
        //        }
        //    }
        //    return collection;

        //}


        #endregion
        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(UserName) & Workarea!=null)
                UserName = Workarea.UserName;
            if (_currencyId == 0)
            {
                _currencyId = Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("CYRRENCY").ValueInt.Value;
            }
            if(_userOwnerId==0)
            {
                _userOwnerId = Workarea.CurrentUser.Id;
            }
            NameFull = string.Format("{0} �� {1} � {2}", Name, _date.ToShortDateString(), _number);
            CodeFind = (NameFull + Transliteration.Front(NameFull)).Replace(" ", "");
            if (CodeFind.Length > 255)
                CodeFind = CodeFind.Substring(1, 255);

            if (_agentDepartmentFromId == 0)
            {
                _agentDepartmentFromId = AgentFromId;
            }

            if (_agentDepartmentToId == 0)
            {
                _agentDepartmentToId = AgentToId;
            }

            if (AgentNamesAuto)
            {
                if (_agentFromId != 0)
                {
                    _agentFromName = AgentFrom.Name;
                }
                if (_agentToId != 0)
                {
                    _agentToName = AgentTo.Name;
                }
                if (_agentDepartmentFromId != 0)
                {
                    _agentDepartmentFromName = AgentDepartmentFrom.Name;
                }
                if (_agentDepartmentToId != 0)
                {
                    _agentDepartmentToName = AgentDepartmentTo.Name;
                }
            }
            

        }
        #region ���� ������
        internal class TpvCollection : List<Document>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {

                SqlDataRecord sdr = new SqlDataRecord
                (
                new SqlMetaData(GlobalPropertyNames.Id, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.Guid, SqlDbType.UniqueIdentifier),
                new SqlMetaData(GlobalPropertyNames.DatabaseId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.DbSourceId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.Version, SqlDbType.Binary, 8),
                new SqlMetaData(GlobalPropertyNames.UserName, SqlDbType.NVarChar, 128),
                new SqlMetaData(GlobalPropertyNames.DateModified, SqlDbType.DateTime),
                new SqlMetaData(GlobalPropertyNames.FlagsValue, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.StateId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.Name, SqlDbType.NVarChar, 255),
                new SqlMetaData(GlobalPropertyNames.NameFull, SqlDbType.NVarChar -1),
                new SqlMetaData(GlobalPropertyNames.Kind, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.Code, SqlDbType.NVarChar, 100),
                new SqlMetaData(GlobalPropertyNames.CodeFind, SqlDbType.NVarChar, 255),
                new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, -1),
                new SqlMetaData(GlobalPropertyNames.FlagString, SqlDbType.NVarChar, 255),
                new SqlMetaData(GlobalPropertyNames.TemplateId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.Date, SqlDbType.Date),
                new SqlMetaData(GlobalPropertyNames.FormId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.FolderId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.AgentFromId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.AgentToId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.AgentDepartmentFromId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.AgentDepartmentToId, SqlDbType.Int),

                new SqlMetaData(GlobalPropertyNames.AgentFromName, SqlDbType.NVarChar, 255),
                new SqlMetaData(GlobalPropertyNames.AgentToName, SqlDbType.NVarChar, 255),
                new SqlMetaData(GlobalPropertyNames.AgentDepartmentFromName, SqlDbType.NVarChar, 255),
                new SqlMetaData(GlobalPropertyNames.AgentDepartmentToName, SqlDbType.NVarChar, 255),

                new SqlMetaData(GlobalPropertyNames.CurrencyId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.CurrencyIdTrans, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.CurrencyIdCountry, SqlDbType.Int),

                new SqlMetaData(GlobalPropertyNames.Summa, SqlDbType.Money),
                new SqlMetaData(GlobalPropertyNames.SummaBase, SqlDbType.Money),
                new SqlMetaData(GlobalPropertyNames.SummaCurrency, SqlDbType.Money),
                new SqlMetaData(GlobalPropertyNames.SummaTax, SqlDbType.Money),
                new SqlMetaData(GlobalPropertyNames.Number, SqlDbType.NVarChar, 50),
                new SqlMetaData(GlobalPropertyNames.Time, SqlDbType.Time),
                new SqlMetaData(GlobalPropertyNames.Accounting, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.AccountingWfId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.CfoId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.MyCompanyId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.ClientId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.UserOwnerId, SqlDbType.Int)
                
                );

                foreach (Document doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, Document doc)
        {
            sdr.SetInt32(0, doc.Id);
            sdr.SetGuid(1, doc.Guid);
            sdr.SetInt32(2, doc.DatabaseId);
            if (doc.DbSourceId == 0)
                sdr.SetValue(3, DBNull.Value);
            else
                sdr.SetInt32(3, doc.DbSourceId);
            if (doc.ObjectVersion == null || doc.ObjectVersion.All(v => v == 0))
                sdr.SetValue(4, DBNull.Value);
            else
                sdr.SetValue(4, doc.ObjectVersion);

            sdr.SetString(5, doc.UserName);
            if (doc.DateModified.HasValue)
                sdr.SetDateTime(6, doc.DateModified.Value);
            else
                sdr.SetValue(6, DBNull.Value);

            sdr.SetInt32(7, doc.FlagsValue);
            sdr.SetInt32(8, doc.StateId);
            sdr.SetString(9, doc.Name);
            
            if (string.IsNullOrEmpty(doc.NameFull))
                sdr.SetValue(10, DBNull.Value);
            else
                sdr.SetString(10, doc.NameFull);

            sdr.SetInt32(11, doc.KindId);
            
            if (string.IsNullOrEmpty(doc.Code))
                sdr.SetValue(12, DBNull.Value);
            else
                sdr.SetString(12, doc.Code);

            if (string.IsNullOrEmpty(doc.CodeFind))
                sdr.SetValue(13, DBNull.Value);
            else
                sdr.SetString(13, doc.CodeFind);
            
            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(14, DBNull.Value);
            else
                sdr.SetString(14, doc.Memo);
            
            if (string.IsNullOrEmpty(doc.FlagString))
                sdr.SetValue(15, DBNull.Value);
            else
                sdr.SetString(15, doc.FlagString);

            if (doc.TemplateId == 0)
                sdr.SetValue(16, DBNull.Value);
            else
                sdr.SetInt32(16, doc.TemplateId);

            sdr.SetDateTime(17, doc.Date);
            if (doc.ProjectItemId == 0)
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetInt32(18, doc.ProjectItemId);
            sdr.SetInt32(19, doc.FolderId);

            if (doc.AgentFromId == 0)
                sdr.SetValue(20, DBNull.Value);
            else
                sdr.SetInt32(20, doc.AgentFromId);
            if (doc.AgentToId == 0)
                sdr.SetValue(21, DBNull.Value);
            else
                sdr.SetInt32(21, doc.AgentToId);

            if (doc.AgentDepartmentFromId == 0)
                sdr.SetValue(22, DBNull.Value);
            else
                sdr.SetInt32(22, doc.AgentDepartmentFromId);

            if (doc.AgentDepartmentToId == 0)
                sdr.SetValue(23, DBNull.Value);
            else
                sdr.SetInt32(23, doc.AgentDepartmentToId);

            if (string.IsNullOrEmpty(doc.AgentFromName))
                sdr.SetValue(24, DBNull.Value);
            else
                sdr.SetString(24, doc.AgentFromName);

            if (string.IsNullOrEmpty(doc.AgentToName))
                sdr.SetValue(25, DBNull.Value);
            else
                sdr.SetString(25, doc.AgentToName);

            if (string.IsNullOrEmpty(doc.AgentDepartmentFromName))
                sdr.SetValue(26, DBNull.Value);
            else
                sdr.SetString(26, doc.AgentDepartmentFromName);

            if (string.IsNullOrEmpty(doc.AgentDepartmentToName))
                sdr.SetValue(27, DBNull.Value);
            else
                sdr.SetString(27, doc.AgentDepartmentToName);

            sdr.SetInt32(28, doc.CurrencyId);
            if (doc.CurrencyTransactionId == 0)
                sdr.SetValue(29, DBNull.Value);
            else
                sdr.SetInt32(29, doc.CurrencyTransactionId);

            if (doc.CurrencyBaseId == 0)
                sdr.SetValue(30, DBNull.Value);
            else
                sdr.SetInt32(30, doc.CurrencyBaseId);
            
            sdr.SetDecimal(31, doc.Summa);
            sdr.SetDecimal(32, doc.SummaBase);
            sdr.SetDecimal(33, doc.SummaTransaction);
            sdr.SetDecimal(34, doc.SummaTax);

            if (string.IsNullOrEmpty(doc.Number))
                sdr.SetValue(35, DBNull.Value);
            else
                sdr.SetString(35, doc.Number);
            sdr.SetTimeSpan(36, doc.Time);
            //sdr.SetTimeSpan(24, doc.Date.TimeOfDay);
            sdr.SetInt32(37, doc.Accounting);

            if (doc.AccountingWfId== 0)
                sdr.SetValue(38, DBNull.Value);
            else
                sdr.SetInt32(38, doc.AccountingWfId);

            if (doc.CfoId == 0)
                sdr.SetValue(39, DBNull.Value);
            else
                sdr.SetInt32(39, doc.CfoId);

            if (doc.MyCompanyId == 0)
                sdr.SetValue(40, DBNull.Value);
            else
                sdr.SetInt32(40, doc.MyCompanyId);

            if (doc.ClientId == 0)
                sdr.SetValue(41, DBNull.Value);
            else
                sdr.SetInt32(41, doc.ClientId);

            if (doc.UserOwnerId == 0)
                sdr.SetValue(42, DBNull.Value);
            else
                sdr.SetInt32(42, doc.UserOwnerId);
            return sdr;
        }
        /// <summary>�������� ������</summary>
        /// <param name="reader">������ SqlDataReader</param>
        /// <param name="endInit">��������� �������������</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _date = reader.GetDateTime(17);
                _projectItemId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _folderId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _agentFromId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _agentToId = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
                _agentDepartmentFromId = reader.IsDBNull(22) ? 0 : reader.GetInt32(22);
                _agentDepartmentToId = reader.IsDBNull(23) ? 0 : reader.GetInt32(23);


                _agentFromName = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                _agentToName = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                _agentDepartmentFromName = reader.IsDBNull(26) ? string.Empty : reader.GetString(26);
                _agentDepartmentToName = reader.IsDBNull(27) ? string.Empty : reader.GetString(27);

                _currencyId = reader.GetInt32(28);
                _currencyTransactionId = reader.IsDBNull(29) ? 0 : reader.GetInt32(29);
                _currencyBaseId = reader.IsDBNull(30) ? 0 : reader.GetInt32(30);
                _summa = reader.GetDecimal(31);
                _summaBase = reader.GetDecimal(32);
                _summaTransaction = reader.GetDecimal(33);
                _summaTax = reader.GetDecimal(34);
                _number = reader.IsDBNull(35) ? string.Empty : reader.GetString(35);
                _time = reader.IsDBNull(36) ? TimeSpan.Zero : reader.GetTimeSpan(36);
                _accounting = reader.IsDBNull(37) ? 0 : reader.GetInt32(37);
                _accountingWfId = reader.IsDBNull(38) ? 0 : reader.GetInt32(38);
                _cfoId = reader.IsDBNull(39) ? 0 : reader.GetInt32(39);
                _myCompanyId = reader.IsDBNull(40) ? 0 : reader.GetInt32(40);
                _clientId = reader.IsDBNull(41) ? 0 : reader.GetInt32(41);
                _userOwnerId = reader.IsDBNull(42) ? 0 : reader.GetInt32(42);
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
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion)
        {
        }
        #endregion

        #region ������� ���������
        internal List<DocumentSign> _signs;
        /// <summary>��������� ������� ��������� �� ���� ������</summary>
        internal void LoadSigns()
        {
            _signs = new List<DocumentSign>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<DocumentSign>().Entity.FindMethod("LoadByDoc").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int)
                                           {Direction = ParameterDirection.ReturnValue};
                    cmd.Parameters.Add(prm);

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
                                DocumentSign item = new DocumentSign
                                {
                                    Workarea = Workarea
                                };
                                item.Load(reader);
                                _signs.Add(item);
                            }
                            reader.Close();
                        }
                        // TODO: �������� ....
                        //object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        //if (retval == null)
                        //    throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        //if ((Int32)retval != 0)
                        //    throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
        }
        /// <summary>��������� �������� ���������</summary>
        public List<DocumentSign> Signs()
        {
            if (_signs == null)
                LoadSigns();
            return _signs;
        }
        /// <summary>
        /// �������� �������
        /// </summary>
        public void RefreshSigns()
        {
            LoadSigns();
        }
        #endregion
        
        #region �������������� ���������� ���������
        private List<DocumentSecure> _documentSecures;
        /// <summary>
        /// ��������� �������������� ����������
        /// </summary>
        /// <returns></returns>
        public List<DocumentSecure> DocumentSecures()
        {
            if (_documentSecures == null)
                RefreshDocumentSecures();
            if (_documentSecures != null && _documentSecures.Count==0)
                RefreshDocumentSecures();
            return _documentSecures;
        }
        /// <summary>
        /// �������� �������
        /// </summary>
        public void RefreshDocumentSecures()
        {
            if (IsNew)
                _documentSecures = new List<DocumentSecure>();
            _documentSecures = DocumentSecure.GetCollectionByDocumentId(Workarea, Id);
        } 
        public bool GetEffectiveSecure(string userName, string code)
        {
            try
            {
                Uid user = Workarea.Access.GetAllUsers().FirstOrDefault(s => s.Name.ToUpper() == userName.ToUpper());
                List<int> uids = user.Groups.Select(s => s.Id).Union(new List<int> {user.Id}).ToList();
                DateTime now = DateTime.Now;
                List<DocumentSecure> res = (from u in uids
                          join r in DocumentSecures() on u equals r.UserIdTo
                          where r.Right.Code==code && r.DateStart<now && r.DateEnd>now
                          select r).ToList();

                if (res.Exists(s => !s.IsAllow))
                    return false;
                if (res.Exists(s => !s.IsAllow))
                    return true;
            }
            catch
            {
                return false;
            }
            
            return false;
        }
        #endregion

        #region ������ ���������
        private List<Taxe> _taxes;
        /// <summary>��������� ������� ��������� �� ���� ������</summary>
        private void LoadTaxes()
        {
            _taxes = new List<Taxe>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Taxe>().Entity.FindMethod("LoadByDoc").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                    cmd.Parameters.Add(prm);

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
                                Taxe item = new Taxe
                                {
                                    Workarea = Workarea
                                };
                                item.Load(reader);
                                _taxes.Add(item);
                            }
                            reader.Close();
                        }
                        // TODO: �������� ....
                        //object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        //if (retval == null)
                        //    throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        //if ((Int32)retval != 0)
                        //    throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);
                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
        }

        /// <summary>��������� ������� ���������</summary>
        public List<Taxe> Taxes()
        {
            if (_taxes == null)
                LoadTaxes();
            return _taxes;
        }
        #endregion

        #region ����� ���������
        
        #endregion

        #region ICodes
        public List<CodeValue<Document>> GetValues(bool allKinds)
        {
            return CodeHelper<Document>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Document>.GetView(this, true);
        }
        #endregion

        #region IChains<Document> Members
        
        private List<ChainDocument> CollectionChainSources(Document source, int? kind)
        {
            List<ChainDocument> collection = new List<ChainDocument>();
            using (SqlConnection cnn = source.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = source.Entity.FindMethod("ChainLoadSource").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = source.Id;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    if (kind.HasValue)
                        prm.Value = kind.Value;
                    else
                        prm.Value = DBNull.Value;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainDocument item = new ChainDocument { Workarea = source.Workarea, Left = source };
                                item.Load(reader);
                                collection.Add(item);
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
                    }

                }
            }
            return collection;
        }
        List<IChain<Document>> IChains<Document>.GetLinks()
        {
            List<IChain<Document>> coll = new List<IChain<Document>>();
            foreach (ChainDocument item in GetLinks())
            {
                coll.Add(item);
            }
            return coll;
        }
        public List<ChainDocument> GetLinks()
        {
            return CollectionChainSources(this, null);
        }

        List<IChain<Document>> IChains<Document>.GetLinks(int kind)
        {
            List<IChain<Document>> coll = new List<IChain<Document>>();
            foreach (ChainDocument item in GetLinks(kind))
            {
                coll.Add(item);
            }
            return coll;
        }
        public List<ChainDocument> GetLinks(int kind)
        {
            return CollectionChainSources(this, kind);
        }
        List<Document> IChains<Document>.SourceList(int chainKindId)
        {
            return Chain<Document>.GetChainSourceList(this, chainKindId);
        }
        List<Document> IChains<Document>.DestinationList(int chainKindId)
        {
            return Chain<Document>.DestinationList(this, chainKindId);
        }
        // TODO: ��������� � ��������� IChains
        public static List<Document> GetChainSourceList(Workarea workarea, int id, int chainKindId)
        {
            Document item;
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // TODO: ������� ��
                        cmd.CommandText = "[Document].[DocumentLoadChainSources]";//FindMethod("Core.EntitiesHasBrowseTree").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = chainKindId;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new Document { Workarea = workarea };
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

        // TODO: ��������� � ��������� IChains
        public static List<Document> GetChainDestinationList(Workarea workarea, int id, int chainKindId)
        {
            Document item;
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // TODO: ������� ��
                        cmd.CommandText = "[Document].[DocumentLoadChainDestinations]";//FindMethod("Core.EntitiesHasBrowseTree").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = chainKindId;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Document { Workarea = workarea };
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

        
        #endregion

        #region IChainsAdvancedList<Document, Knowledge> Members

        List<IChainAdvanced<Document, Knowledge>> IChainsAdvancedList<Document, Knowledge>.GetLinks()
        {
            return ChainAdvanced<Document, Knowledge>.CollectionSource(this);
        }

        List<IChainAdvanced<Document, Knowledge>> IChainsAdvancedList<Document, Knowledge>.GetLinks(int? kind)
        {
            return ChainAdvanced<Document, Knowledge>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Document, Knowledge>> GetLinkedEvents(int? kind = null)
        {
            return ChainAdvanced<Document, Knowledge>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Document, Knowledge>.GetChainView()
        {
            return ChainValueView.GetView<Document, Knowledge>(this);
        }
        #endregion

        #region IChainsAdvancedList<Document,FileData> Members

        List<IChainAdvanced<Document, FileData>> IChainsAdvancedList<Document, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<Document, FileData>)this).GetLinks(13);
        }

        List<IChainAdvanced<Document, FileData>> IChainsAdvancedList<Document, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<Document, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Document, FileData>(this);
        }
        public List<IChainAdvanced<Document, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<Document, FileData>> collection = new List<IChainAdvanced<Document, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Document>().Entity.FindMethod("LoadFiles").FullName;
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
                                ChainAdvanced<Document, FileData> item = new ChainAdvanced<Document, FileData> { Workarea = Workarea, Left = this };
                                item.Load(reader);
                                collection.Add(item);
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
                    }

                }
            }
            return collection;
        }

        #endregion
        #region IChainsAdvancedList<Document,Message> Members

        List<IChainAdvanced<Document, Message>> IChainsAdvancedList<Document, Message>.GetLinks()
        {
            return ChainAdvanced<Document, Message>.CollectionSource(this);
        }

        List<IChainAdvanced<Document, Message>> IChainsAdvancedList<Document, Message>.GetLinks(int? kind)
        {
            return ChainAdvanced<Document, Message>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Document, Message>> GetLinkedMessage(int? kind = null)
        {
            return ChainAdvanced<Document, Message>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Document, Message>.GetChainView()
        {
            return ChainValueView.GetView<Document, Message>(this);
        }
        #endregion

        #region IChainsAdvancedList<Document,Note> Members

        List<IChainAdvanced<Document, Note>> IChainsAdvancedList<Document, Note>.GetLinks()
        {
            return ChainAdvanced<Document, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Document, Note>> IChainsAdvancedList<Document, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Document, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Document, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Document, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Document, Note>.GetChainView()
        {
            return ChainValueView.GetView<Document, Note>(this);
        }
        #endregion
        public override string ToString()
        {
            return ToString("%name% � %number% �� %date% �����: %summa%");
        }
        public override string ToString(string mask)
        {
            string res = base.ToString(mask);

            // ���������������� ��� ��������
            res = res.Replace("%date%", Date.ToString());
            // ���������������� ��� ��������
            if (_number != null) res = res.Replace("%number%", _number);
            // ���������������� ��� ����������
            res = res.Replace("%summa%", _summa.ToString());
            return res;
        }

        /// <summary>
        /// ����� �������
        /// </summary>
        /// <param name="hierarchyId">������������� �������� � ������� ������������ �����</param>
        /// <param name="userName">��� ������������</param>
        /// <param name="flags">����</param>
        /// <param name="stateId">������������� ���������</param>
        /// <param name="name">������������</param>
        /// <param name="kindId">������������� ����</param>
        /// <param name="code">�������</param>
        /// <param name="memo">������������</param>
        /// <param name="flagString">���������������� ����</param>
        /// <param name="templateId">������������� �������</param>
        /// <param name="count">����������, �� ��������� 100</param>
        /// <param name="filter">�������������� ������</param>
        /// <param name="useAndFilter">������������ ������ �</param>
        /// <returns></returns>
        public List<Document> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0, int agentToId=0, int agentFromId=0,
            int count = 100, Predicate<Document> filter = null, bool useAndFilter = false)
        {
            Document item = new Document { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("�� ������ ����� {0}, ������ ��� ������ ������ �� ���������������", GlobalMethodAlias.LoadList));
            }
            List<Document> collection = new List<Document>();
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
                        if (agentToId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.AgentToId, SqlDbType.Int).Value = agentToId;
                        if (agentFromId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.AgentFromId, SqlDbType.Int).Value = agentFromId;
                        if (useAndFilter)
                            cmd.Parameters.Add(GlobalSqlParamNames.UseAndFilter, SqlDbType.Bit).Value = true;



                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Document { Workarea = Workarea };
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

    /// <summary>
    /// ����� ��� ������ �� ���������� ����������� (��� ������ ���������)
    /// </summary>
    public class DocChain
    {
        /// <summary>������������� ������� ���������</summary>
        public int Id { get; set; }
        /// <summary>����� (������) ����������</summary>
        public int OrderNo { get; set; }
        /// <summary>��� �����, ��������������� ���� �������� WF</summary>
        public string Code { get; set; }
        /// <summary>������������ ���������</summary>
        public string Name { get; set; }
        /// <summary>������������� ���� ���������</summary>
        public int Kind { get; set; }
        /// <summary>���������� �����, ������������ ��� ���������</summary>
        public string Memo { get; set; }
        /// <summary>��� ����� ���������� - ������������ ��� �������� ����������</summary>
        public string CodeFind { get; set; }
        private void Load(SqlDataReader reader)
        {
            OrderNo = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            Code = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
            Id = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
            Name = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
            Kind = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
            Memo = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
            CodeFind = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
        }

        /// <summary>
        /// ��������� ������ �� ���� ������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="templateId">������������� ������� ���������</param>
        /// <param name="refresh">��������� ������ ���������� ������ ����������� ����</param>
        /// <returns>null ��� ������ ��������� � ������� ���������� ������!</returns>
        public static List<DocChain> Get(Workarea wa, int templateId, bool refresh=false)
        {
            
            if(!refresh && wa.Cashe.CashedDocTemplateChains!=null)
            {
                if (wa.Cashe.CashedDocTemplateChains.Exists(templateId))
                    return wa.Cashe.CashedDocTemplateChains.Get(templateId);
            }
            List<DocChain> collection = new List<DocChain>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Document.GetChainForUIForm";
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = templateId;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocChain item = new DocChain();
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
            wa.Cashe.CashedDocTemplateChains.Add(templateId, collection);
           return collection;
        }
    }
}