namespace BusinessObjects.Security
{
    /// <summary>Общие разрешения пользователя или группы</summary>
    public sealed class UserRightCommon : UserRightBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public UserRightCommon(): base()
        {
            EntityId = (short) WhellKnownDbEntity.UserRightCommon;
        }
        ///// <summary>Сохранить</summary>
        //public override void Save()
        //{
        //    Validate();
        //    if (IsNew)
        //        Create("Secure.RightsUsersInsert");
        //    else
        //        Update("Secure.RightsUsersUpdate", true);
        //}
        ///// <summary>Удалить</summary>
        ///// <returns></returns>
        //public override int Delete()
        //{
        //    return Delete("Secure.RightsUsersDelete");
        //}
        //// TODO: соотвктствующая обработка
        //public override void Load(int value)
        //{

        //}

    }
}
