namespace BusinessObjects.Security
{
    /// <summary>Права для свойств объекта</summary>
    class RightObjectProperty : UserRightElement
    {
        /// <summary>Конструктор</summary>
        public RightObjectProperty()
        {

        }	
        private string _propertyName;
        /// <summary>Наименование свойства</summary>
        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }
    }

    class RightObjectPropertyView<T> : NamedRight
    {
        public RightObjectPropertyView(IWorkarea wa): base(wa)
        {
            DefaltValue = false;
        }
        protected override void LoadRights()
        {
            //using (SqlConnection cnn = Workarea.Database.GetDatabaseConnection())
            //{
            //    if (cnn == null) return;
            //    try
            //    {
            //        using (SqlCommand cmd = cnn.CreateCommand())
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            cmd.CommandText = "[Secure].[RightCommonByUser]";
            //            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = Workarea.CurrentUser.Name;
            //            SqlDataReader reader = cmd.ExecuteReader();

            //            while (reader.Read())
            //            {
            //                ACL.Add(reader.GetString(0), reader.IsDBNull(1) ? new Nullable<int>() : reader.GetInt32(1));
            //            }
            //            reader.Close();
            //        }
            //    }
            //    catch (SqlException)
            //    {
            //        throw;
            //    }
            //    finally
            //    {
            //        cnn.Close();
            //    }
            //}
        }
        /// <summary>
        /// Просмотр идентификатора
        /// </summary>
        public bool IdView 
        {
            get { return IsAllow("VIEW|ID"); }
        }
        /// <summary>
        /// Изменение идентификатора
        /// </summary>
        public bool IdChange
        {
            get { return IsAllow("CHANGE|ID"); }
        }
        /// <summary>
        /// Просмотр глобального идентификатора
        /// </summary>
        public bool GuidView
        {
            get { return IsAllow("VIEW|GUID"); }
        }
        /// <summary>
        /// Изменение глобального идентификатора
        /// </summary>
        public bool GuidChange
        {
            get { return IsAllow("CHANGE|GUID"); }
        }
        /// <summary>
        /// Просмотр наименование
        /// </summary>
        public bool NameView
        {
            get { return IsAllow("VIEW|NAME"); }
        }
        /// <summary>
        /// Изменение наименование
        /// </summary>
        public bool NameChange
        {
            get { return IsAllow("CHANGE|NAME"); }
        }

        /// <summary>
        /// Просмотр наименование
        /// </summary>
        public bool CodeView
        {
            get { return IsAllow("VIEW|CODE"); }
        }
        /// <summary>
        /// Изменение наименование
        /// </summary>
        public bool CodeChange
        {
            get { return IsAllow("CHANGE|CODE"); }
        }

        /// <summary>
        /// Просмотр наименование
        /// </summary>
        public bool MemoView
        {
            get { return IsAllow("VIEW|MEMO"); }
        }
        /// <summary>
        /// Изменение наименование
        /// </summary>
        public bool MemoChange
        {
            get { return IsAllow("CHANGE|MEMO"); }
        }
    }
}
