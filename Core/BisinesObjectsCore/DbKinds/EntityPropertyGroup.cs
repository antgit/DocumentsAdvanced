namespace BusinessObjects
{
    /// <summary>
    /// Группа свойств
    /// </summary>
    public sealed class EntityPropertyGroup: BaseCore<EntityPropertyGroup>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public EntityPropertyGroup(): base()
        {
            EntityId = (short)WhellKnownDbEntity.PropertyGroup;
        }
    }
}