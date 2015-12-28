using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Наименование кода"</summary>
    internal struct CodeNameStruct
    {
        /// <summary>Наименование приложения</summary>
        public string App;
        /// <summary>Идентификатор системного типа которому принадлежит код</summary>
        public int ToEntityId;
        /// <summary>Идентификатор типа документа</summary>
        public int DocTypeId;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>Наименование кода</summary>
    /// <remarks></remarks>
    public sealed class CodeName : BaseCore<CodeName>, IChains<CodeName>, IEquatable<CodeName>,
                                   IComparable, IComparable<CodeName>, ICompanyOwner
    {

        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Наименование кода объектов, соответствует значению 1</summary>
        public const int KINDVALUE_DEFAULT = 1;
        /// <summary>Наименование кода документов, соответствует значению 2</summary>
        public const int KINDVALUE_DOCUMENT = 2;

        /// <summary>Группа, соответствует значению 4587521</summary>
        public const int KINDID_DEFAULT = 4587521;
        /// <summary>Виртуальная группа, соответствует значению 4587522</summary>
        public const int KINDID_DOCUMENT = 4587522;
        // ReSharper restore InconsistentNaming
        #endregion
        protected override void CopyValue(CodeName template)
        {
            base.CopyValue(template);
            App = template.App;
            ToEntityId = template.ToEntityId;
            DocTypeId = template.DocTypeId;
        }
        bool IEquatable<CodeName>.Equals(CodeName other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        public int CompareTo(object obj)
        {
            CodeName otherPerson = (CodeName)obj;
            return Id.CompareTo(otherPerson.Id);
        }

        public int CompareTo(CodeName other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override CodeName Clone(bool endInit)
        {
            CodeName obj = base.Clone(false);
            obj.App = App;
            obj.ToEntityId = ToEntityId;
            obj.DocTypeId = DocTypeId;
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }

        /// <summary>
        /// Сравнение объекта для службы обмена данными
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange(CodeName value)
        {
            if (!base.CompareExchange(value))
            {
                return false;
            }

            if (value.ToEntityId != ToEntityId)
                return false;
            if (value.DocTypeId != DocTypeId)
                return false;
            if (!StringNullCompare(value.App, App))
                return false;

            return true;
        }
        /// <summary>Конструктор</summary>
        public CodeName()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.CodeName;
        }

        #region Свойства
        private string _app;
        /// <summary>
        /// Наименование приложения
        /// </summary>
        public string App
        {
            get { return _app; }
            set
            {
                if (value == _app) return;
                OnPropertyChanging(GlobalPropertyNames.App);
                _app = value;
                OnPropertyChanged(GlobalPropertyNames.App);
            }
        }

        private int _toEntityId;
        /// <summary>
        /// Идентификатор системного типа которому принадлежит код
        /// </summary>
        public int ToEntityId
        {
            get { return _toEntityId; }
            set
            {
                if (value == _toEntityId) return;
                OnPropertyChanging(GlobalPropertyNames.ToEntityId);
                _toEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.ToEntityId);
            }
        }

        private EntityType _toEntity;
        /// <summary>
        /// Системный тип которомы принадлежит наименование
        /// </summary>
        public EntityType ToEntity
        {
            get
            {
                if (_toEntityId == 0)
                    return null;
                if (_toEntity == null)
                    _toEntity = Workarea.CollectionEntities.Find(s => s.Id == _toEntityId);
                else if (_toEntity.Id != _toEntityId)
                    _toEntity = Workarea.CollectionEntities.Find(s => s.Id == _toEntityId);
                return _toEntity;
            }
            set
            {
                if (_toEntity == value) return;
                OnPropertyChanging(GlobalPropertyNames.ToEntity);
                _toEntity = value;
                _toEntityId = _toEntity == null ? 0 : _toEntity.Id;
                OnPropertyChanged(GlobalPropertyNames.ToEntity);
            }
        }



        private int _docTypeId;
        /// <summary>
        /// Идентификатор типа документа
        /// </summary>
        public int DocTypeId
        {
            get { return _docTypeId; }
            set
            {
                if (value == _docTypeId) return;
                OnPropertyChanging(GlobalPropertyNames.DocTypeId);
                _docTypeId = value;
                OnPropertyChanged(GlobalPropertyNames.DocTypeId);
            }
        }


        private EntityDocument _docType;
        /// <summary>
        /// Тип документа
        /// </summary>
        public EntityDocument DocType
        {
            get
            {
                if (_docTypeId == 0)
                    return null;
                if (_docType == null)
                    _docType = Workarea.CollectionDocumentTypes().Find(s => s.Id == _docTypeId);
                else if (_docType.Id != _docTypeId)
                    _docType = Workarea.CollectionDocumentTypes().Find(s => s.Id == _docTypeId);
                return _docType;
            }
            set
            {
                if (_docType == value) return;
                OnPropertyChanging(GlobalPropertyNames.DocType);
                _docType = value;
                _docTypeId = _docType == null ? 0 : _docType.Id;
                OnPropertyChanged(GlobalPropertyNames.DocType);
            }
        }

        private int _myCompanyId;
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит объект
        /// </summary>
        public int MyCompanyId
        {
            get { return _myCompanyId; }
            set
            {
                if (value == _myCompanyId) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompanyId);
                _myCompanyId = value;
                OnPropertyChanged(GlobalPropertyNames.MyCompanyId);
            }
        }


        private Agent _myCompany;
        /// <summary>
        /// Моя компания, предприятие которому принадлежит объект
        /// </summary>
        public Agent MyCompany
        {
            get
            {
                if (_myCompanyId == 0)
                    return null;
                if (_myCompany == null)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                else if (_myCompany.Id != _myCompanyId)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                return _myCompany;
            }
            set
            {
                if (_myCompany == value) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompany);
                _myCompany = value;
                _myCompanyId = _myCompany == null ? 0 : _myCompany.Id;
                OnPropertyChanged(GlobalPropertyNames.MyCompany);
            }
        }
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);
            if (_toEntityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ToEntityId, XmlConvert.ToString(ToEntityId));
            if (_docTypeId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.DocTypeId, XmlConvert.ToString(DocTypeId));
            if (!string.IsNullOrEmpty(_app))
                writer.WriteAttributeString(GlobalPropertyNames.App, App);
            if (_myCompanyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ToEntityId) != null)
                _toEntityId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ToEntityId));
            if (reader.GetAttribute(GlobalPropertyNames.DocTypeId) != null)
                _docTypeId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DocTypeId));
            if (reader.GetAttribute(GlobalPropertyNames.App) != null)
                _app = reader[GlobalPropertyNames.App];
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
        }
        #endregion

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(Code))
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_CODENAME_CODE"));
            if (ToEntityId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_CODENAME_TOENTITYID"));

            if (KINDID_DOCUMENT == KindValue && DocTypeId != 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_CODENAME_DOCTYPE"));

        }
        #region Состояние
        CodeNameStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new CodeNameStruct { App = _app, ToEntityId = _toEntityId, DocTypeId=_docTypeId, MyCompanyId = _myCompanyId};
                return true;
            }
            return false;
        }

        /// <summary>Востановить текущее состояние объекта</summary>
        /// <remarks>Востановление состояние возможно только после выполнения сосхранения состояния</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            App = _baseStruct.App;
            ToEntityId = _baseStruct.ToEntityId;
            DocTypeId = _baseStruct.DocTypeId;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion
        #region База данных
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="reader">Объект <see cref="SqlDataReader"/> чтения данных</param>
        /// <param name="endInit">Закончить инициализацию</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _app = reader.GetString(17);
                _toEntityId = reader.GetInt16(18);
                _docTypeId = reader.IsDBNull(19) ? 0 : reader.GetInt16(19);
                _myCompanyId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
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
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.App, SqlDbType.NVarChar, 255) { IsNullable = false, Value = _app };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ToEntityId, SqlDbType.SmallInt) { IsNullable = false, Value = _toEntityId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DocTypeId, SqlDbType.SmallInt) { IsNullable = true };
            if (_docTypeId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _docTypeId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.NVarChar) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion
        /// <summary>
        /// Список приложений
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <returns></returns>
        public static List<string> GetAppNames(Workarea wa)
        {
            List<string> coll = new List<string>();
            if (wa == null)
                return coll;

            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = wa.Empty<CodeName>().FindProcedure("CodeNameGetAppNames");
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                coll.Add(reader.GetString(0));
                            }
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            return coll;
        }
        /// <summary>
        /// Список приложений
        /// </summary>
        /// <returns></returns>
        public List<string> GetAppNames()
        {
            return CodeName.GetAppNames(Workarea);
        }
        /// <summary>
        /// Имеются ли дополнительные коды для системного типа
        /// </summary>
        /// <param name="entityId">Идентификатор системного типа</param>
        /// <returns></returns>
        public bool ExistsForEntity(int entityId)
        {
            bool res = false;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = FindProcedure("CodeNamesExistsForEntity");
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = entityId;
                        object value = sqlCmd.ExecuteScalar();
                        res = Convert.ToBoolean(value);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            return res;
        }
        /// <summary>
        /// Имеются ли дополнительные коды для типа документов
        /// </summary>
        /// <param name="entityId">Идентификатор системного типа документов</param>
        /// <returns></returns>
        public bool ExistsForDocType(int entityId)
        {
            bool res = false;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = FindProcedure("CodeNamesExistsForDocumentType");
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = entityId;
                        object value = sqlCmd.ExecuteScalar();
                        res = Convert.ToBoolean(value);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            return res;
        }
        // 
        //
        #region ILinks<CodeName> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<CodeName>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<CodeName>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<CodeName> IChains<CodeName>.SourceList(int chainKindId)
        {
            return Chain<CodeName>.GetChainSourceList(this, chainKindId);
        }
        List<CodeName> IChains<CodeName>.DestinationList(int chainKindId)
        {
            return Chain<CodeName>.DestinationList(this, chainKindId);
        }
        #endregion

        public List<CodeName> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<CodeName> filter = null,
            string app=null, int? toEntityId=null, int? myCompanyId=null,
            bool useAndFilter = false)
        {
            CodeName item = new CodeName { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<CodeName> collection = new List<CodeName>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindProcedure(GlobalMethodAlias.FindBy);
                        cmd.Parameters.Add(GlobalSqlParamNames.Count, SqlDbType.Int).Value = count;
                        if (hierarchyId != null && hierarchyId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;
                        if (userName != null && !string.IsNullOrEmpty(userName))
                            cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;
                        if (flags.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.Flags, SqlDbType.Int).Value = flags;
                        if (stateId.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateId;
                        if (!string.IsNullOrWhiteSpace(name))
                            cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = name;
                        if (kindId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindId;
                        if (!string.IsNullOrWhiteSpace(code))
                            cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;
                        if (!string.IsNullOrWhiteSpace(memo))
                            cmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255).Value = memo;
                        if (!string.IsNullOrWhiteSpace(flagString))
                            cmd.Parameters.Add(GlobalSqlParamNames.FlagString, SqlDbType.NVarChar, 50).Value = flagString;
                        if (templateId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TemplateId, SqlDbType.Int).Value = templateId;
                        if (useAndFilter)
                            cmd.Parameters.Add(GlobalSqlParamNames.UseAndFilter, SqlDbType.Bit).Value = true;
                        if (!string.IsNullOrEmpty(app))
                            cmd.Parameters.Add(GlobalSqlParamNames.App, SqlDbType.NVarChar, 255).Value = app;
                        if (toEntityId.HasValue && toEntityId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.ToEntityId, SqlDbType.Int).Value = toEntityId.Value;
                        if (myCompanyId.HasValue && myCompanyId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = myCompanyId.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new CodeName { Workarea = Workarea };
                            item.Load(reader);
                            Workarea.Cashe.SetCasheData(item);
                            if (filter != null && filter.Invoke(item))
                                collection.Add(item);
                            else if (filter == null)
                                collection.Add(item);

                        }
                        reader.Close();

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
    }
}