using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
namespace BusinessObjects.DocumentLibrary
{
    /// <summary>
    /// Дополнительные настройки документов
    /// </summary>
    public static class DocumentViewConfig
    {
        static DocumentViewConfig()
        {

        }
        public static int StoreChainId { get; set; }
        /// <summary>
        /// Идентификатор вида связи для печатных форм
        /// </summary>
        public static int PrintFormChainId { get; set; }
        /// <summary>
        /// Идентификатор вида связи для подразделений
        /// </summary>
        public static int DepatmentChainId { get; set; }
        /// <summary>
        /// Идентификатор вида связи для сотрудников
        /// </summary>
        public static int WorkresChainId { get; set; }
        /// <summary>
        /// Идентификатор вида связи для торговых представителей
        /// </summary>
        public static int TradersChainId { get; set; }
        /// <summary>
        /// Идентификатор вида связи для перевозчиков
        /// </summary>
        public static int DeliveryChainId{ get; set; }
        /// <summary>
        /// Рабочая область
        /// </summary>
        public static Workarea Workarea { get; set; }
        /// <summary>
        /// Выполнена ли начальная инициалазия
        /// </summary>
        public static bool IsInit { get; set; }
        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="wa"></param>
        public static void Init(Workarea wa)
        {
            if (wa == null) return;
            Workarea = wa;
// ReSharper disable PossibleNullReferenceException
            StoreChainId = Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.STORE && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
            DepatmentChainId = Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TREE && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
            PrintFormChainId = Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.PRINTFORM && s.FromEntityId == (int)WhellKnownDbEntity.Library).Id;
            WorkresChainId = Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
            TradersChainId = Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TRADERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
            DeliveryChainId = Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.DELIVERY && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
            
// ReSharper restore PossibleNullReferenceException
            IsInit = true;
        }
    }

    // 
    
}