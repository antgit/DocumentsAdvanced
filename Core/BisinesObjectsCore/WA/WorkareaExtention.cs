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
    /// Расширения рабочей области
    /// </summary>
    public static class WorkareaExtention
    {
        /// <summary>
        /// Идентификатор типа связи для печатных представлений
        /// </summary>
        /// <param name="wa"></param>
        /// <returns></returns>
        public static int PrintFormChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.PRINTFORM && s.FromEntityId == (int)WhellKnownDbEntity.Library).Id;
        }

        /// <summary>
        /// Идентификатор типа связи для отчетов связанных с документом
        /// </summary>
        /// <param name="wa"></param>
        /// <returns></returns>
        public static int ReportFormChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.REPORT && s.FromEntityId == (int)WhellKnownDbEntity.Library).Id;
        }
        
        /// <summary>
        /// Редакция сервера базы данных
        /// </summary>
        /// <param name="wa">Рабочая область</param>
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
        /// Поиск папки по коду поиска
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="codeFind">Код поиска</param>
        /// <param name="refresh">Обновить</param>
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
        /// Идентификатор вида связи для складов
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <returns></returns>
        public static int StoreChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.STORE && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
        }
        
        /// <summary>
        /// Идентификатор вида связи для сотрудников
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <returns></returns>
        public static int WorkresChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
        }
        /// <summary>
        /// Уволенные сотрудники
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <returns></returns>
        public static int WorkreDissmisedChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.DISMISSED && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
        }
        
        /// <summary>
        /// Идентификатор вида связи для торговых представителей
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <returns></returns>
        public static int TradersChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TRADERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
        }
        /// <summary>
        /// Идентификатор вида связи для основных связей корреспондентов
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <returns></returns>
        public static int CompanyTreeChainId(this Workarea wa)
        {
            return wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TREE && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
        }
        /// <summary>
        /// Коллекция покупателей
        /// </summary>
        /// <remarks>Данные на основе соответствующей иерархий</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="nested">Учитывать вложенные иерархии</param>
        /// <returns></returns>
        public static List<Agent> GetByers(this Workarea wa, bool nested=false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_BUYERS);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// Коллекция банков
        /// </summary>
        /// <remarks>Данные на основе соответствующей иерархий</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="nested">Учитывать вложенные иерархии</param>
        /// <returns></returns>
        public static List<Agent> GetBanks(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_BANKS);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// Коллекция государственных огранов
        /// </summary>
        /// <remarks>Данные на основе соответствующей иерархий</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="nested">Учитывать вложенные иерархии</param>
        /// <returns></returns>
        public static List<Agent> GetGovenments(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_GOVENMENT);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// Коллекция кандидатов на работу
        /// </summary>
        /// <remarks>Данные на основе соответствующей иерархий</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="nested">Учитывать вложенные иерархии</param>
        /// <returns></returns>
        public static List<Agent> GetJobCandidates(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_JOBCANDIDATES);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// Коллекция собственных холдингов
        /// </summary>
        /// <remarks>Данные на основе соответствующей иерархий</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="nested">Учитывать вложенные иерархии</param>
        /// <returns></returns>
        public static List<Agent> GetMyHoldings(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYCOMPANY);
            return h.GetTypeContents<Agent>(nested);
        }

        /// <summary>
        /// Коллекция филиалов и подразделений
        /// </summary>
        /// <remarks>Данные на основе соответствующей иерархий</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="nested">Учитывать вложенные иерархии</param>
        /// <returns></returns>
        public static List<Agent> GetMyDepatments(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYDEPATMENTS);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// Коллекция складов
        /// </summary>
        /// <remarks>Данные на основе соответствующей иерархий</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="nested">Учитывать вложенные иерархии</param>
        /// <returns></returns>
        public static List<Agent> GetMyStores(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYSTORES);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// Коллекция сотрудников
        /// </summary>
        /// <remarks>Данные на основе соответствующей иерархий</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="nested">Учитывать вложенные иерархии</param>
        /// <returns></returns>
        public static List<Agent> GetMyWorker(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYWORKERS);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// Коллекция конкурентов
        /// </summary>
        /// <remarks>Данные на основе соответствующей иерархий</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="nested">Учитывать вложенные иерархии</param>
        /// <returns></returns>
        public static List<Agent> GetCompetitors(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_COMPETITOR);
            return h.GetTypeContents<Agent>(nested);
        }
        /// <summary>
        /// Коллекция поставщиков
        /// </summary>
        /// <remarks>Данные на основе соответствующей иерархий</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <param name="nested">Учитывать вложенные иерархии</param>
        /// <returns></returns>
        public static List<Agent> GetSuppliers(this Workarea wa, bool nested = false)
        {
            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_SUPPLIERS);
            return h.GetTypeContents<Agent>(nested);
        }
    }
}