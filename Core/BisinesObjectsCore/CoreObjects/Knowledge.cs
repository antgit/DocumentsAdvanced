using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Статья базы знаний"</summary>
    internal struct KnowledgeStruct
    {
        /// <summary>Идентификатор файла</summary>
        public int FileId;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>Статья базы знаний</summary>
    public sealed class Knowledge : BaseCore<Knowledge>, IChains<Knowledge>, IEquatable<Knowledge>,
                                    IComparable, IComparable<Knowledge>, IFacts<Knowledge>,
                                    ICodes<Knowledge>, IHierarchySupport, ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>Локальная статья, соответствует значению 1</summary>
        public const int KINDVALUE_LOCAL = 1;
        /// <summary>Интернет статья, соответствует значению 2</summary>
        public const int KINDVALUE_ONLINE = 2;
        /// <summary>Файловая ссылка, соответствует значению 3</summary>
        public const int KINDVALUE_FILELINK = 3;
        /// <summary>Внутренняя текстовая статья, соответствует значению 4</summary>
        public const int KINDVALUE_TEXT = 4;
        /// <summary>Внутренняя Html статья, соответствует значению 5</summary>
        public const int KINDVALUE_HTML = 5;
        

        /// <summary>Локальная статья, соответствует значению 4915201</summary>
        public const int KINDID_LOCAL = 4915201;
        /// <summary>Интернет статья, соответствует значению 4915202</summary>
        public const int KINDID_ONLINE = 4915202;
        /// <summary>Файловая ссылка, соответствует значению 4915203</summary>
        public const int KINDID_FILELINK = 4915203;
        /// <summary>Внутренняя текстовая статья, соответствует значению 4915204</summary>
        public const int KINDID_TEXT = 4915204;
        /// <summary>Внутренняя Html статья, соответствует значению 4915205</summary>
        public const int KINDID_HTML = 4915205;
        
        // ReSharper restore InconsistentNaming

        #endregion
        bool IEquatable<Knowledge>.Equals(Knowledge other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Knowledge otherPerson = (Knowledge)obj;
            return Id.CompareTo(otherPerson.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(Knowledge other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>Конструктор</summary>
        public Knowledge()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Knowledge;
        }
        protected override void CopyValue(Knowledge template)
        {
            base.CopyValue(template);
            FileId = template.FileId;
            MyCompanyId = template.MyCompanyId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Knowledge Clone(bool endInit)
        {
            Knowledge obj = base.Clone(false);
            obj.FileId = FileId;
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства
        private int _fileId;
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public int FileId
        {
            get { return _fileId; }
            set
            {
                if (value == _fileId) return;
                OnPropertyChanging(GlobalPropertyNames.FileId);
                _fileId = value;
                OnPropertyChanged(GlobalPropertyNames.FileId);
            }
        }

        private FileData _file;
        /// <summary>
        /// Файловые данные
        /// </summary>
        public FileData File
        {
            get
            {
                if (_fileId == 0)
                    return null;
                if (_file == null)
                    _file = Workarea.Cashe.GetCasheData<FileData>().Item(_fileId);
                else if (_file.Id != _fileId)
                    _file = Workarea.Cashe.GetCasheData<FileData>().Item(_fileId);
                return _file;
            }
            set
            {
                if (_file == value) return;
                OnPropertyChanging("File");
                _file = value;
                _fileId = _file == null ? 0 : _file.Id;
                OnPropertyChanged("File");
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

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_fileId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FileId, XmlConvert.ToString(_fileId));
            if (_myCompanyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.FileId) != null)
                _fileId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FileId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
        }
        #endregion
        
        #region Состояние
        KnowledgeStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new KnowledgeStruct { FileId = _fileId, MyCompanyId = _myCompanyId };
                return true;
            }
            return false;
        }

        /// <summary>Востановить текущее состояние объекта</summary>
        /// <remarks>Востановление состояние возможно только после выполнения сосхранения состояния</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            FileId = _baseStruct.FileId;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();

            if (KindId == KINDID_LOCAL && FileId==0)
            {
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_KNOWLEDGEFILEID", 1049));
            }
            if (KindId == KINDID_ONLINE && string.IsNullOrEmpty(CodeFind))
            {
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_KNOWLEDGECODEFIND", 1049));
            }

        }

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
                _fileId = reader.IsDBNull(17)? 0: reader.GetSqlInt32(17).Value;
                _myCompanyId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.FileId, SqlDbType.Int) { IsNullable = true};
            if (_fileId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _fileId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.NVarChar) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Knowledge> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<Knowledge>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Knowledge>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Knowledge> IChains<Knowledge>.SourceList(int chainKindId)
        {
            return Chain<Knowledge>.GetChainSourceList(this, chainKindId);
        }
        List<Knowledge> IChains<Knowledge>.DestinationList(int chainKindId)
        {
            return Chain<Knowledge>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Knowledge>> GetValues(bool allKinds)
        {
            return CodeHelper<Knowledge>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Knowledge>.GetView(this, true);
        }
        #endregion

        #region IFacts

        private List<FactView> _factView;
        public List<FactView> GetCollectionFactView()
        {
            return _factView ?? (_factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId));
        }

        public void RefreshFaсtView()
        {
            _factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId);
        }

        public FactView GetFactViewValue(string factCode, string columnCode)
        {
            return GetCollectionFactView().FirstOrDefault(s => s.FactNameCode == factCode && s.ColumnCode == columnCode);
        }

        public List<FactName> GetFactNames()
        {
            return FactHelper.GetFactNames(Workarea, EntityId);
        }
        #endregion

        /// <summary>
        /// Первая группа в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<Knowledge>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }
        public List<Knowledge> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Knowledge> filter = null,
            int? fileId = null, int? myCompanyId = null,
            bool useAndFilter = false)
        {
            Knowledge item = new Knowledge { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Knowledge> collection = new List<Knowledge>();
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
                        if (fileId.HasValue && fileId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.FileId, SqlDbType.Int).Value = fileId.Value;
                        if (myCompanyId.HasValue && myCompanyId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = myCompanyId.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Knowledge { Workarea = Workarea };
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