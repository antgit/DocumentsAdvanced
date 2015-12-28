using System.Collections.Generic;
namespace BusinessObjects.Security
{
    /// <summary>Разрешение</summary>
    public sealed class Right : BaseCore<Right>, IChains<Right>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Общие права, соответствует значению 1</summary>
        public const int KINDVALUE_GENERALE = 1;
        /// <summary>Действие, соответствует значению 2</summary>
        public const int KINDVALUE_ACTION = 2;
        /// <summary>Интерфейс, соответствует значению 4</summary>
        public const int KINDVALUE_INTERFACE = 4;

        /// <summary>Общие права, соответствует значению 1769473</summary>
        public const int KINDID_GENERALE = 1769473;
        /// <summary>Действие, соответствует значению 1769474</summary>
        public const int KINDID_ACTION = 1769474;
        /// <summary>Интерфейс, соответствует значению 1769476</summary>
        public const int KINDID_INTERFACE = 1769476;
        // ReSharper restore InconsistentNaming
        #endregion

        #region Константы кодов разрешений
        // ReSharper disable InconsistentNaming
        /// <summary>Разрешения интерфейса "Редактирование"</summary>
        public const string UIEDIT = "UIEDIT";
        /// <summary>Разрешения интерфейса "Удаление в корзину"</summary>
        public const string UITRASH = "UITRASH";
        /// <summary>Разрешения интерфейса "Удаление"</summary>
        public const string UIDELETE = "UIDELETE";
        /// <summary>Разрешения интерфейса "Создание нового элемента"</summary>
        public const string UICREATE = "UICREATE";
        /// <summary>Разрешения интерфейса "Структура предприятия"</summary>
        public const string UIAGENTSTRUCT = "UIAGENTSTRUCT";
        /// <summary>Разрешения интерфейса "Просмотр свойств"</summary>
        public const string UIVIEW = "UIVIEW";

        /// <summary>Разрешения интерфейса "Печать"</summary>
        public const string UIPRINT = "UIPRINT";
        /// <summary>Разрешения интерфейса "Предварительный просмотр"</summary>
        public const string UIPREVIEW = "UIPREVIEW";
        /// <summary>Разрешения интерфейса "Построить отчет"</summary>
        public const string UIREPORTBUILD = "UIREPORTBUILD";
        /// <summary>Разрешения интерфейса "Построитель/конструктор отчета"</summary>
        public const string UIEDITREPORT = "UIEDITREPORT";
        

        /// <summary>Разрешения элемента "Просмотр"</summary>
        public const string VIEW = "VIEW";

        /// <summary>Разрешение блокировки документа</summary>
        public const string DOCLOCK = "DOCLOCK";
        /// <summary>Разрешение разблокировки документа</summary>
        public const string DOCUNLOCK = "DOCUNLOCK";
        /// <summary>Разрешение редактирования документа</summary>
        public const string DOCEDIT = "DOCEDIT";
        /// <summary>Разрешение удаления документа в корзину</summary>
        public const string DOCTRASH = "DOCTRASH";
        /// <summary>Разрешение удаления документа</summary>
        public const string DOCDELETE = "DOCDELETE";
        /// <summary>Разрешение создания документа</summary>
        public const string DOCCREATE = "DOCCREATE";
        /// <summary>Разрешен просмотр документа</summary>
        public const string DOCVIEW = "DOCVIEW";
        /// <summary>Разрешен просмотр печатного представления</summary>
        public const string DOCPREVIEW = "DOCPREVIEW";
        /// <summary>Разрешены комментарии</summary>
        public const string DOCNOTE = "DOCNOTE";
        /// <summary>Резрешен просмотр документа в интернете</summary>
        public const string DOCSHARE = "DOCSHARE";
        /// <summary>Разрешен просмотр печатного представления документа</summary>
        public const string DOCSHAREPREVIEW = "DOCSHAREPREVIEW";
        
        // ReSharper restore InconsistentNaming
        #endregion

        /// <summary>Конструктор</summary>
        public Right(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Acl;
        }
        #region ILinks<Right> Members
        /// <summary>
        /// Связи бухгалтерского счета
        /// </summary>
        /// <returns></returns>
        public List<IChain<Right>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи разрешений
        /// </summary>
        /// <param name="kind">Тип связей</param>
        /// <returns></returns>
        public List<IChain<Right>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Right> IChains<Right>.SourceList(int chainKindId)
        {
            return Chain<Right>.GetChainSourceList(this, chainKindId);
        }
        List<Right> IChains<Right>.DestinationList(int chainKindId)
        {
            return Chain<Right>.DestinationList(this, chainKindId);
        }
        #endregion
    }
}
