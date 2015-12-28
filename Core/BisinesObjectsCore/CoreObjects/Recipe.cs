using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace BusinessObjects
{
    /// <summary>Наименования комплека или набора</summary>
    public sealed class Recipe : BaseCore<Recipe>, IChains<Recipe>,IEquatable<Recipe>,
        IChainsAdvancedList<Recipe, Note>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Комплект, соответствует значению 1</summary>
        public const int KINDVALUE_KOMPLEKT = 1;
        /// <summary>Набор, соответствует значению 2</summary>
        public const int KINDVALUE_SET = 2;

        /// <summary>Комплект</summary>
        public const int KINDID_KOMPLEKT = 393217;
        /// <summary>Набор</summary>
        public const int KINDID_SET = 393218;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Recipe>.Equals(Recipe other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public Recipe():base()
        {
            EntityId = (short) WhellKnownDbEntity.Recipe;
        }
        #region Свойства
       
        #endregion

        #region ILinks<Recipe> Members
        /// <summary>
        /// Связи состава
        /// </summary>
        /// <returns></returns>
        public List<IChain<Recipe>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи состава
        /// </summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Recipe>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Recipe> IChains<Recipe>.SourceList(int chainKindId)
        {
            return Chain<Recipe>.GetChainSourceList(this, chainKindId);
        }
        List<Recipe> IChains<Recipe>.DestinationList(int chainKindId)
        {
            return Chain<Recipe>.DestinationList(this, chainKindId);
        }
        #endregion

        #region IChainsAdvancedList<Recipe,Note> Members

        List<IChainAdvanced<Recipe, Note>> IChainsAdvancedList<Recipe, Note>.GetLinks()
        {
            return ChainAdvanced<Recipe, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Recipe, Note>> IChainsAdvancedList<Recipe, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Recipe, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Recipe, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Recipe, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Recipe, Note>.GetChainView()
        {
            return ChainValueView.GetView<Recipe, Note>(this);
        }
        #endregion
    }
}
