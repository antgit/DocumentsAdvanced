using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Маппинг хранимых процедур
    /// </summary>
    [Serializable]
    public sealed class ProcedureMap : BaseCoreObject
    {
        private static List<ProcedureMap> _dbEntityMethodsCollection;
        /// <summary>
        /// Коллекция маппинга в рабочей области
        /// </summary>
        /// <param name="workarea">Рабочая область</param>
        /// <returns></returns>
        public static List<ProcedureMap> Collection( Workarea workarea)
        {
            if (_dbEntityMethodsCollection == null)
                _dbEntityMethodsCollection = new List<ProcedureMap>();
            else
                return _dbEntityMethodsCollection;

            return RefreshCollection(workarea);
        }

        private static List<ProcedureMap> RefreshCollection(Workarea workarea)
        {
            _dbEntityMethodsCollection = new List<ProcedureMap>();
            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null) return _dbEntityMethodsCollection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Core].[ProcedureMapLoadAll]";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ProcedureMap item = new ProcedureMap { Workarea = workarea };
                            item.Load(reader);
                            _dbEntityMethodsCollection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return _dbEntityMethodsCollection;
        }

        #region Свойства
        // private Int16 _entityId;
        private int _subKindId;
        private string _name;
        private string _method;
        private string _procedure;
        private string _schema;

        /// <summary>Конструктор</summary>
        public ProcedureMap(): base()
        {
        }
        /// <summary>
        /// Идентификатор типа
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// Наименование типа: "Объект", "Документ" или "Пользовательский"
        /// </summary>
        public string TypeName
        {
            get
            {
                if (TypeId == 0)
                    return "Объект";
                return TypeId == 1 ? "Документ" : "Пользовательский";
            }
        }
        ///// <summary>Идентификатор типа</summary>
        //public new Int16 EntityId
        //{
        //    get { return _entityId; }
        //    set
        //    {
        //        if (value == _entityId) return;
        //        OnPropertyChanging("EntityId");
        //        _entityId = value;
        //        OnPropertyChanged("EntityId");
        //    }
        //}

        /// <summary>Подтип</summary>
        public int SubKindId
        {
            get { return _subKindId; }
            set
            {
                if (value == _subKindId) return;
                OnPropertyChanging(GlobalPropertyNames.SubKindId);
                _subKindId = value;
                OnPropertyChanged(GlobalPropertyNames.SubKindId);
            }
        }

        /// <summary>Наименование</summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                OnPropertyChanging(GlobalPropertyNames.Name);
                _name = value;
                OnPropertyChanged(GlobalPropertyNames.Name);
            }
        }

        /// <summary>Наименование метода</summary>
        public string Method
        {
            get { return _method; }
            set
            {
                if (value == _method) return;
                OnPropertyChanging(GlobalPropertyNames.Method);
                _method = value;
                OnPropertyChanged(GlobalPropertyNames.Method);
            }
        }

        /// <summary>Наименование хранимой процедуры</summary>
        public string Procedure
        {
            get { return _procedure; }
            set
            {
                if (value == _procedure) return;
                OnPropertyChanging(GlobalPropertyNames.Procedure);
                _procedure = value;
                OnPropertyChanged(GlobalPropertyNames.Procedure);
            }
        }

        /// <summary>Схема данных</summary>
        public string Schema
        {
            get { return _schema; }
            set
            {
                if (value == _schema) return;
                OnPropertyChanging(GlobalPropertyNames.Schema);
                _schema = value;
                OnPropertyChanged(GlobalPropertyNames.Schema);
            }
        }
        #endregion

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_subKindId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SubKindId, XmlConvert.ToString(_subKindId));
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
            if (!string.IsNullOrEmpty(_method))
                writer.WriteAttributeString(GlobalPropertyNames.Method, _method);
            if (!string.IsNullOrEmpty(_procedure))
                writer.WriteAttributeString(GlobalPropertyNames.Procedure, _procedure);
            if (!string.IsNullOrEmpty(_schema))
                writer.WriteAttributeString(GlobalPropertyNames.Schema, _schema);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.SubKindId) != null)
                _subKindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SubKindId));
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
            if (reader.GetAttribute(GlobalPropertyNames.Method) != null)
                _method = reader.GetAttribute(GlobalPropertyNames.Method);
            if (reader.GetAttribute(GlobalPropertyNames.Procedure) != null)
                _procedure = reader.GetAttribute(GlobalPropertyNames.Procedure);
            if (reader.GetAttribute(GlobalPropertyNames.Schema) != null)
                _schema = reader.GetAttribute(GlobalPropertyNames.Schema);
        }
        #endregion

        /// <summary>Полное наименование процедуры с учетом схемы</summary>
        public string FullName
        {
            get{ return string.Format("[{0}].[{1}]", _schema, _procedure); }
        }

        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                EntityId = reader.IsDBNull(9) ? (short)0 : reader.GetInt16(9);
                //_entityId = reader.IsDBNull(9) ? (short) 0 : reader.GetInt16(9);
                _subKindId = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                _name = reader.GetString(11);
                _method = reader.GetString(12);
                _schema = reader.GetString(13);
                _procedure = reader.GetString(14);
                TypeId = reader.GetInt32(15);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }

        protected override void Save(bool endSave=true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnSaving(e);
            if (e.Cancel)
                return;
            Validate();
            if (TypeId == 1)
            {
                if (Id == 0)
                    Create(Workarea.FindMethod("Core.DocumentTypeMethodInsert").FullName);
                else
                    Update(Workarea.FindMethod("Core.DocumentTypeMethodUpdate").FullName, true);
            }
            if (TypeId != 0) return;
            if (Id == 0)
                Create(Workarea.FindMethod("Core.EntityMethodInsert").FullName);
            else
                Update(Workarea.FindMethod("Core.EntityMethodUpdate").FullName, true);

            ProcedureMap map = Entity.Methods.FirstOrDefault(f => f.Id == Id);
            if (map!=null)
            {
                map = this;
                int idxEntity = Workarea.CollectionEntities.FindIndex(f => f.Id == EntityId);
                if(Workarea.CollectionEntities[idxEntity].Methods.Exists(f=>f.Id==Id))
                {
                    int idxMethod = Workarea.CollectionEntities[idxEntity].Methods.FindIndex(f => f.Id == Id);
                    Workarea.CollectionEntities[idxEntity].Methods[idxMethod] = this;
                }
                else
                {
                    Workarea.CollectionEntities[idxEntity].Methods.Add(this);
                }
            }
            

            if (endSave)
                OnSaved();
        }
        /// <summary>Загрузить</summary>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            switch (TypeId)
            {
                case 1:
                    Load(value, Workarea.FindMethod("Core.DocumentTypeMethodLoad").FullName);
                    break;
                case 0:
                    Load(value, Workarea.FindMethod("Core.EntityMethodLoad").FullName);
                    break;
            }
        }

        /// <summary>Удаление из базы данных</summary>
        /// <remarks>Удаление выполняется без каких либо проверок. Для осуществления проверки используйте метод 
        /// <see cref="BaseCoreObject.CanDeleteFromDataBase"/>.
        /// Метод использует хранимую процедуру "[Core].[DocumentTypeMethodDelete]"
        /// </remarks>
        /// <param name="checkVersion">Выполнять проверку версий</param>
        public override void Delete(bool checkVersion = true)
        {
            switch (TypeId)
            {
                case 1:
                    Delete("[Core].[DocumentTypeMethodDelete]", checkVersion);
                    break;
                case 0:
                    Delete("Core.EntityMethodDelete", checkVersion);
                    break;
            }            
        }
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        /// <exception cref="ValidateException">Наименование не может быть пустым</exception>
        public override void Validate()
        {
            base.Validate();
            // TODO: Соответсвующая проверка для глобального типа и для привязанного к типу
            //if (_entityId == 0)
            //    throw new ValidateException("Системный тип объекта не может быть равен 0");
            if (string.IsNullOrEmpty(_name))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_NAMEISEMPTY, 1049));
            if (string.IsNullOrEmpty(_method))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_METHODISEMPTY, 1049));
            if (string.IsNullOrEmpty(_procedure))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_PROCNAMEISEMPTY, 1049));
            if (string.IsNullOrEmpty(_schema))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_SHEMANAMEISEMPTY, 1049));
        }

        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt)
                                   {
                                       IsNullable = true,
                                       Value = (EntityId == 0 ? (object) DBNull.Value : EntityId)
                                   };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.KindId, SqlDbType.Int)
                      {
                          IsNullable = true,
                          Value = (_subKindId == 0 ? (object) DBNull.Value : _subKindId)
                      };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255)
                      {
                          IsNullable = false,
                          Value = _name
                      };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Method, SqlDbType.NVarChar, 255) {IsNullable = false, Value = _method};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Schema, SqlDbType.NVarChar, 128) {IsNullable = false, Value = _schema};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ProcedureName, SqlDbType.NVarChar, 128)
                      {
                          IsNullable = false,
                          Value = _procedure
                      };
            sqlCmd.Parameters.Add(prm);
        }
        /// <summary>Представление объекта в виде строки</summary>
        /// <rereturns>Соответствует наименованию объекта <see cref="ProcedureMap.Name"/></rereturns>
        public override string ToString()
        {
            return _name;
        }
        /// <summary>
        /// Представление сущности в виде форматированной строки
        /// </summary>
        /// <param name="mask">Маска 
        /// <para> </para>
        /// <para> </para>
        /// <list type="table">
        /// <listheader>
        /// <term>Маска</term>
        /// <description>Описание</description></listheader>
        /// <item>
        /// <term>%name%</term>
        /// <description><see
        /// cref="P:BusinessObjects.ProcedureMap.Name">Наименование</see></description></item>
        /// <item>
        /// <term>%method%</term>
        /// <description><see
        /// cref="P:BusinessObjects.ProcedureMap.Method">Метод</see></description></item>
        /// <item>
        /// <term>%procedure%</term>
        /// <description><see
        /// cref="P:BusinessObjects.ProcedureMap.Procedure">Процедура</see></description></item>
        /// <item>
        /// <term>%fulname%</term>
        /// <description><see cref="P:BusinessObjects.ProcedureMap.FullName">Полное
        /// наименование</see> </description></item>
        /// <item>
        /// <term>%schema%</term>
        /// <description><see
        /// cref="P:BusinessObjects.ProcedureMap.Schema">Схема</see></description></item></list></param>
        /// <seealso
        /// cref="BusinessObjects.BaseCoreObject.ToString(System.String)">BaseCoreObject.ToString()</seealso>
        public override string ToString(string mask)
        {
            if (string.IsNullOrEmpty(mask))
            {
                return ToString();
            }
            string res = mask;
            
            res = res.Replace("%name%", Name);
            res = res.Replace("%method%", Method);
            res = res.Replace("%procedure%", Procedure);
            res = res.Replace("%schema%", Schema);
            res = res.Replace("%fullname%", FullName);
            res = res.Replace("%typename%", TypeName);
            return res;
        }
    }
}