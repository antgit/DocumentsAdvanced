namespace BusinessObjects
{
    /// <summary>
    /// ������ �������
    /// </summary>
    public sealed class EntityPropertyGroup: BaseCore<EntityPropertyGroup>
    {
        /// <summary>
        /// �����������
        /// </summary>
        public EntityPropertyGroup(): base()
        {
            EntityId = (short)WhellKnownDbEntity.PropertyGroup;
        }
    }
}