using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// ���������� ������� �������
    /// </summary>
    public static class WorkareaExtention
    {
        /// <summary>
        /// ������������� ���� ����� ��� �������� �������������
        /// </summary>
        /// <param name="wa"></param>
        /// <returns></returns>
        public static int PrintFormChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.PRINTFORM && s.FromEntityId == (int)WhellKnownDbEntity.Library).Id;
        }

        /// <summary>
        /// ������������� ���� ����� ��� ������� ��������� � ����������
        /// </summary>
        /// <param name="wa"></param>
        /// <returns></returns>
        public static int ReportFormChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.REPORT && s.FromEntityId == (int)WhellKnownDbEntity.Library).Id;
        }
        
        /// <summary>
        /// �������� ������� ���� ������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <returns></returns>
        public static string ServerEdition(this Workarea wa)
        {

            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {

                    cmd.CommandText = "SELECT SERVERPROPERTY('Edition')";
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();
                    object val = cmd.ExecuteScalar();
                    if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                    return val.ToString();
                }
            }

        }
        private static Dictionary<string, int> _privateFolderCodeId;
        /// <summary>
        /// ����� ����� �� ���� ������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="codeFind">��� ������</param>
        /// <param name="refresh">��������</param>
        /// <returns></returns>
        public static Folder GetFolderByCodeFind(this Workarea wa, string codeFind, bool refresh=false)
        {
            if(_privateFolderCodeId==null || refresh)
            {
                _privateFolderCodeId = new Dictionary<string, int>();
                List<Folder> coll = wa.GetCollection<Folder>().Where(s => !string.IsNullOrEmpty(s.CodeFind)).ToList();
                foreach (Folder f in coll)
                {
                    if (!_privateFolderCodeId.ContainsKey(f.CodeFind))
                        _privateFolderCodeId.Add(f.CodeFind, f.Id);
                }
            }
            if (_privateFolderCodeId.ContainsKey(codeFind))
                return wa.Cashe.GetCasheData<Folder>().Item(_privateFolderCodeId[codeFind]);
            else
                return null;
        }
        /// <summary>
        /// ������������� ���� ����� ��� �������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <returns></returns>
        public static int StoreChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.STORE && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
        }
        
        /// <summary>
        /// ������������� ���� ����� ��� �����������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <returns></returns>
        public static int WorkresChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
        }
        /// <summary>
        /// ��������� ����������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <returns></returns>
        public static int WorkreDissmisedChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.DISMISSED && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
        }
        
        /// <summary>
        /// ������������� ���� ����� ��� �������� ��������������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <returns></returns>
        public static int TradersChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TRADERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
        }
        /// <summary>
        /// ������������� ���� ����� ��� �������� ������ ���������������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <returns></returns>
        public static int CompanyTreeChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TREE && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
        }
        /// <summary>
        /// ��������� �����������
        /// </summary>
        /// <remarks>������ �� ������ ��������������� ��������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="nested">��������� ��������� ��������</param>
        /// <returns></returns>
        public static List<Agent> GetByers(this Workarea wa, bool nested=false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_BUYERS);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <remarks>������ �� ������ ��������������� ��������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="nested">��������� ��������� ��������</param>
        /// <returns></returns>
        public static List<Agent> GetBanks(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_BANKS);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// ��������� ��������������� �������
        /// </summary>
        /// <remarks>������ �� ������ ��������������� ��������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="nested">��������� ��������� ��������</param>
        /// <returns></returns>
        public static List<Agent> GetGovenments(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_GOVENMENT);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// ��������� ���������� �� ������
        /// </summary>
        /// <remarks>������ �� ������ ��������������� ��������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="nested">��������� ��������� ��������</param>
        /// <returns></returns>
        public static List<Agent> GetJobCandidates(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_JOBCANDIDATES);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// ��������� ����������� ���������
        /// </summary>
        /// <remarks>������ �� ������ ��������������� ��������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="nested">��������� ��������� ��������</param>
        /// <returns></returns>
        public static List<Agent> GetMyHoldings(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYCOMPANY);
            return h.GetTypeContents<Agent>(nested);
        }

        /// <summary>
        /// ��������� �������� � �������������
        /// </summary>
        /// <remarks>������ �� ������ ��������������� ��������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="nested">��������� ��������� ��������</param>
        /// <returns></returns>
        public static List<Agent> GetMyDepatments(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYDEPATMENTS);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// ��������� �������
        /// </summary>
        /// <remarks>������ �� ������ ��������������� ��������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="nested">��������� ��������� ��������</param>
        /// <returns></returns>
        public static List<Agent> GetMyStores(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYSTORES);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// ��������� �����������
        /// </summary>
        /// <remarks>������ �� ������ ��������������� ��������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="nested">��������� ��������� ��������</param>
        /// <returns></returns>
        public static List<Agent> GetMyWorker(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYWORKERS);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// ��������� �����������
        /// </summary>
        /// <remarks>������ �� ������ ��������������� ��������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="nested">��������� ��������� ��������</param>
        /// <returns></returns>
        public static List<Agent> GetCompetitors(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_COMPETITOR);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// ��������� �����������
        /// </summary>
        /// <remarks>������ �� ������ ��������������� ��������</remarks>
        /// <param name="wa">������� �������</param>
        /// <param name="nested">��������� ��������� ��������</param>
        /// <returns></returns>
        public static List<Agent> GetSuppliers(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_SUPPLIERS);
            return h.GetTypeContents<Agent>(nested);
        }
    }
}