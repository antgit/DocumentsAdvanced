using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Xml;


namespace BusinessObjects
{
    /// <summary>Виды документов</summary>
    public sealed class EntityDocumentKind : BaseKind
    {
        /// <summary>Конструктор</summary>
        public EntityDocumentKind():base()
        {
        }

        #region Свойства
        private int _correspondenceId;
        /// <summary>
        /// Тип корреспонденции документа (входящий, исходящий внутренний)
        /// </summary>
        public int CorrespondenceId
        {
            get { return _correspondenceId; }
            set
            {
                if (value == _correspondenceId) return;
                OnPropertyChanging(GlobalPropertyNames.CorrespondenceId);
                _correspondenceId = value;
                OnPropertyChanged(GlobalPropertyNames.CorrespondenceId);
            }
        }


        private bool _useCustomFilter;
        /// <summary>
        /// Использовать нестандартное поведение для выбора корреспондентов
        /// </summary>
        public bool UseCustomFilter
        {
            get { return _useCustomFilter; }
            set
            {
                if (value == _useCustomFilter) return;
                OnPropertyChanging(GlobalPropertyNames.UseCustomFilter);
                _useCustomFilter = value;
                OnPropertyChanged(GlobalPropertyNames.UseCustomFilter);
            }
        }


        private int _agentFirstFilterId;
        /// <summary>
        /// Идентификатор первого фильтра корреспондента
        /// </summary>
        public int AgentFirstFilterId
        {
            get { return _agentFirstFilterId; }
            set
            {
                if (value == _agentFirstFilterId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentFirstFilterId);
                _agentFirstFilterId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentFirstFilterId);
            }
        }


        private int _agentSecondFilterId;
        /// <summary>
        /// Идентификатор второго фильтра корреспондента
        /// </summary>
        public int AgentSecondFilterId
        {
            get { return _agentSecondFilterId; }
            set
            {
                if (value == _agentSecondFilterId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentSecondFilterId);
                _agentSecondFilterId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentSecondFilterId);
            }
        }

        private int _agentThirdFilterId;
        /// <summary>
        /// Идентификатор третьего фильтра корреспондента
        /// </summary>
        public int AgentThirdFilterId
        {
            get { return _agentThirdFilterId; }
            set
            {
                if (value == _agentThirdFilterId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentThirdFilterId);
                _agentThirdFilterId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentThirdFilterId);
            }
        }


        private int _agentFourthFilterId;
        /// <summary>
        /// Идентификатор четвертого фильтра корреспондента
        /// </summary>
        public int AgentFourthFilterId
        {
            get { return _agentFourthFilterId; }
            set
            {
                if (value == _agentFourthFilterId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentFourthFilterId);
                _agentFourthFilterId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentFourthFilterId);
            }
        }
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_correspondenceId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CorrespondenceId, XmlConvert.ToString(_correspondenceId));
            if (_useCustomFilter)
                writer.WriteAttributeString(GlobalPropertyNames.UseCustomFilter, XmlConvert.ToString(_useCustomFilter));
            if (_agentFirstFilterId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentFirstFilterId, XmlConvert.ToString(_agentFirstFilterId));
            if (_agentSecondFilterId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentSecondFilterId, XmlConvert.ToString(_agentSecondFilterId));
            if (_agentThirdFilterId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentThirdFilterId, XmlConvert.ToString(_agentThirdFilterId));
            if (_agentFourthFilterId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentFourthFilterId, XmlConvert.ToString(_agentFourthFilterId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);


            if (reader.GetAttribute(GlobalPropertyNames.CorrespondenceId) != null)
                _correspondenceId = Convert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CorrespondenceId));
            if (reader.GetAttribute(GlobalPropertyNames.UseCustomFilter) != null)
                _useCustomFilter = Convert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.UseCustomFilter));
            if (reader.GetAttribute(GlobalPropertyNames.AgentFirstFilterId) != null)
                _agentFirstFilterId = Convert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentFirstFilterId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentSecondFilterId) != null)
                _agentSecondFilterId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentSecondFilterId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentThirdFilterId) != null)
                _agentThirdFilterId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentThirdFilterId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentFourthFilterId) != null)
                _agentFourthFilterId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentFourthFilterId));
        }
        #endregion

        protected override void Create()
        {
            CancelEventArgs e = new CancelEventArgs();
            OnCreating(e);
            if (e.Cancel)
                return;
            Create("Core.DocumentKindInsert");
            OnCreated();
        }

        /// <summary>Обновление объекта в базе данных</summary>
        /// <remarks>Метод использует хранимую процедуру "Core.DocumentKindUpdate"</remarks>
        /// <seealso cref="BaseCoreObject.Create()"/>
        /// <seealso cref="BaseCoreObject.Load(int)"/>
        /// <seealso cref="BaseCoreObject.Validate"/>
        protected override void Update(bool versionControl = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnUpdating(e);
            if (e.Cancel)
                return;
            Update("Core.DocumentKindUpdate", true);
            OnUpdated();
        }

        /// <summary>Удаление из базы данных</summary>
        /// <remarks>Удаление выполняется без каких либо проверок. Для осуществления проверки используйте метод 
        /// <see cref="BaseCoreObject.CanDeleteFromDataBase"/>.
        /// Метод использует хранимую процедуру "Core.DocumentKindDelete"
        /// </remarks>
        /// <param name="checkVersion">Выполнять проверку версий</param>
        public override void Delete(bool checkVersion = true)
        {
            CanDelete();
            Delete(Workarea.FindMethod("Core.DocumentKindDelete").FullName, checkVersion);
            // TODO: Добавить процедуры CANDELETE
            //if (CanDeleteFromDataBase())
            //    Delete(FindProcedure(GlobalMethodAlias.Delete), checkVersion);
            //else
            //    throw new ValidateException("Объект не может быть удален на основе проверки целосности данных в базе данных!");
        }
        protected override void Delete(string procedureName, bool checkVersion = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnDeleting(e);
            if (e.Cancel)
                return;
            using (SqlConnection cnn = (this as ICoreObject).Workarea.GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new DatabaseException("Отсутствует соединение");
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;//FindProcedure(GlobalMethodAlias.Delete);
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = EntityId;
                        if (checkVersion)
                            sqlCmd.Parameters.Add(GlobalSqlParamNames.Version, SqlDbType.Binary, 8).Value = GetVersion();
                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                        sqlCmd.Parameters.Add(prm);
                        sqlCmd.ExecuteNonQuery();
                        object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((int)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);

                        Workarea.TryRemoveFromCasheCollection(this);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            OnDeleted();
        }

        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру "Core.DocumentKindLoad"</remarks>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnLoading(e);
            if (e.Cancel)
                return;
            Load(value, "Core.DocumentKindLoad");
        }

        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _correspondenceId = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                _useCustomFilter = reader.IsDBNull(15) ? false : reader.GetBoolean(15);
                _agentFirstFilterId = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                _agentSecondFilterId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _agentThirdFilterId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _agentFourthFilterId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }

        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.CorrespondenceId, SqlDbType.Int) { IsNullable = false };
            prm.Value = _correspondenceId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UseCustomFilter, SqlDbType.Bit) { IsNullable = false };
            prm.Value = _useCustomFilter;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AgentFirstFilterId, SqlDbType.Int) { IsNullable = true};
            prm.Value = _agentFirstFilterId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AgentSecondFilterId, SqlDbType.Int) { IsNullable = true };
            prm.Value = _agentSecondFilterId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AgentThirdFilterId, SqlDbType.Int) { IsNullable = true };
            prm.Value = _agentThirdFilterId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AgentFourthFilterId, SqlDbType.Int) { IsNullable = true };
            prm.Value = _agentFourthFilterId;
            sqlCmd.Parameters.Add(prm);
        }
    }
}
