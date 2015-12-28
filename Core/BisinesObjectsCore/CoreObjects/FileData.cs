using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Файловые данные"</summary>
    internal struct FileDataStruct
    {
        /// <summary>Разрешить ведение версионности файла</summary>
        public bool AllowVersion;
        /// <summary>Расширение файла</summary>
        public string FileExtention;
        /// <summary>Данные</summary>
        public byte[] StreamData;

        /// <summary>Код версии</summary>
        public int VersionCode;
        /// <summary>
        /// Идентификатор компании владельца
        /// </summary>
        public int MyCompanyId;
        /// <summary>
        /// Идентификатор пользователя владельца
        /// </summary>
        public int UserOwnerId;
    }
    /// <summary>
    /// Файловые данные
    /// </summary>
    /// <remarks>
    /// Свойству <see cref="P:BusinessObjects.BaseCore`1.Name">наименование</see>
    /// соответствует имя файла.
    /// </remarks>
    public sealed class FileData : BaseCore<FileData>, IChains<FileData>, IReportChainSupport, IEquatable<FileData>,
        ICodes<FileData>,
        IChainsAdvancedList<FileData, Note>,
        IHierarchySupport, ICompanyOwner
    {
        // ReSharper disable InconsistentNaming
        /// <summary>Файловые данные, соответствует значению 1</summary>
        public const int KINDVALUE_FILEDATA = 1;

        /// <summary>Файловые данные, соответствует значению 1507329</summary>
        public const int KINDID_FILEDATA = 1507329;
        // ReSharper restore InconsistentNaming

        bool IEquatable<FileData>.Equals(FileData other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public FileData()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.FileData;
        }
        protected override void CopyValue(FileData template)
        {
            base.CopyValue(template);
            AllowVersion = template.AllowVersion;
            FileExtention = template.FileExtention;
            VersionCode = template.VersionCode;
            //data == null ? null : (int[])data.Clone();
            if (!IsStreamDataRefreshDone)
                RefreshSteamData();
            if(!template.IsStreamDataNull)
                StreamData = (byte[])template.StreamData.Clone();
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override FileData Clone(bool endInit)
        {
            FileData obj = base.Clone(false);
            obj.FileExtention = FileExtention;
            obj.StreamData = StreamData;

            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства
        private int _versionCode;
        /// <summary>Код версии</summary>
        public int VersionCode
        {
            get { return _versionCode; }
            set
            {
                if (value == _versionCode) return;
                OnPropertyChanging(GlobalPropertyNames.VersionCode);
                _versionCode = value;
                OnPropertyChanged(GlobalPropertyNames.VersionCode);
            }
        }
        private string _fileExtention;
        /// <summary>
        /// Расширение файла
        /// </summary>
        public string FileExtention
        {
            get { return _fileExtention; }
            set
            {
                if (value == _fileExtention) return;
                OnPropertyChanging(GlobalPropertyNames.FileExtention);
                _fileExtention = value;
                OnPropertyChanged(GlobalPropertyNames.FileExtention);
            }
        }
        /// <summary>
        /// Полное имя файла
        /// </summary>
        public new string NameFull
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(FileExtention))
                    return string.Format("{0}.{1}", Name, FileExtention);
                return string.Empty;
            }
        }


        private bool _allowVersion;
        /// <summary>
        /// Разрешить ведение версионности файла
        /// </summary>
        public bool AllowVersion
        {
            get { return _allowVersion; }
            set
            {
                if (value == _allowVersion) return;
                OnPropertyChanging(GlobalPropertyNames.AllowVersion);
                _allowVersion = value;
                OnPropertyChanged(GlobalPropertyNames.AllowVersion);
            }
        }
        
        private byte[] _streamData;

        /// <summary>
        /// Данные
        /// </summary>
        public byte[] StreamData
        {
            get
            {
                if (!_refreshDode)
                    RefreshSteamData();
                return _streamData;
            }
            set
            {
                //if (value == _streamData) return;
                OnPropertyChanging(GlobalPropertyNames.StreamData);
                _streamData = value;
                _refreshDode = true;
                OnPropertyChanged(GlobalPropertyNames.StreamData);
            }
        }
        /// <summary>
        /// Имеются ли двоичные данные
        /// </summary>
        public bool IsStreamDataNull
        {
            get
            {
                return (_streamData == null || _streamData.All(v => v == 0));
            }
        }



        private int _myCompanyId;
        /// <summary>
        /// Идентификатор компании владельца
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
        /// Моя компания, предприятие которому принадлежит файл
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
        

        private int _userOwnerId;
        /// <summary>
        /// Идентификатор пользователя владельца
        /// </summary>
        public int UserOwnerId
        {
            get { return _userOwnerId; }
            set
            {
                if (value == _userOwnerId) return;
                OnPropertyChanging(GlobalPropertyNames.UserOwnerId);
                _userOwnerId = value;
                OnPropertyChanged(GlobalPropertyNames.UserOwnerId);
            }
        }

        private Uid _userOwner;
        /// <summary>
        /// Пользователь владелец
        /// </summary>
        public Uid UserOwner
        {
            get
            {
                if (_userOwnerId == 0)
                    return null;
                if (_userOwner == null)
                    _userOwner = Workarea.Cashe.GetCasheData<Uid>().Item(_userOwnerId);
                else if (_userOwner.Id != _userOwnerId)
                    _userOwner = Workarea.Cashe.GetCasheData<Uid>().Item(_userOwnerId);
                return _userOwner;
            }
            set
            {
                if (_userOwner == value) return;
                OnPropertyChanging(GlobalPropertyNames.UserOwner);
                _userOwner = value;
                _userOwnerId = _userOwner == null ? 0 : _userOwner.Id;
                OnPropertyChanged(GlobalPropertyNames.UserOwner);
            }
        }

        
        
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_fileExtention))
                writer.WriteAttributeString(GlobalPropertyNames.FileExtention, _fileExtention);
            if (_allowVersion)
                writer.WriteAttributeString(GlobalPropertyNames.AllowVersion, XmlConvert.ToString(_allowVersion));
            if (_streamData != null)
                writer.WriteAttributeString(GlobalPropertyNames.StreamData, Convert.ToBase64String(_streamData));
            if (_versionCode != 0)
                writer.WriteAttributeString(GlobalPropertyNames.VersionCode, XmlConvert.ToString(_versionCode));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.FileExtention) != null)
                _fileExtention = reader[GlobalPropertyNames.FileExtention];
            if (reader.GetAttribute(GlobalPropertyNames.AllowVersion) != null)
                _allowVersion = XmlConvert.ToBoolean(reader[GlobalPropertyNames.AllowVersion]);
            if (reader.GetAttribute(GlobalPropertyNames.StreamData) != null)
                _streamData = Convert.FromBase64String(reader[GlobalPropertyNames.StreamData]);
            if (reader.GetAttribute(GlobalPropertyNames.VersionCode) != null)
                _versionCode = XmlConvert.ToInt32(reader[GlobalPropertyNames.VersionCode]);
        }
        #endregion

        #region Состояние
        FileDataStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new FileDataStruct
                {
                    FileExtention = _fileExtention,
                    AllowVersion = _allowVersion,
                    //IsStreamDataNull = _is,
                    StreamData = _streamData,
                    VersionCode = _versionCode,
                    UserOwnerId = _userOwnerId
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            FileExtention = _baseStruct.FileExtention;
            AllowVersion = _baseStruct.AllowVersion;
            StreamData = _baseStruct.StreamData;
            VersionCode = _baseStruct.VersionCode;
            UserOwnerId = _baseStruct.UserOwnerId;
            IsChanged = false;
        }
        #endregion
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if(!IsTemplate)
            {
                if (string.IsNullOrEmpty(_fileExtention))
                throw new ValidateException("Не указано расширение файла");
            }
            if (IsStreamDataNull && !_refreshDode)
                RefreshSteamData();
            if (_userOwnerId == 0)
            {
                _userOwnerId = Workarea.CurrentUser.Id;
            }
        }

        private bool _refreshDode;
        /// <summary>
        /// Получены ли двоичные данные
        /// </summary>
        public bool IsStreamDataRefreshDone
        {
            get { return _refreshDode; }
        }
        /// <summary>
        /// Загрузка двоичных данных из базы данных 
        /// </summary>
        public void RefreshSteamData()
        {
            _streamData = new byte[] {};
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        cmd.CommandText = FindProcedure(GlobalMethodAlias.GetStreamData);// "FileData.FileLoadStreamData";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            _streamData = !reader.IsDBNull(0) ? reader.GetSqlBinary(0).Value : null;
                        }
                        reader.Close();
                    }
                    _refreshDode = true;
                }
                finally
                {
                    cnn.Close();
                    
                }
            }
        }
        /// <summary>
        /// Импорт данных из файла
        /// </summary>
        /// <param name="filename">Полный путь к импортируемому файлу</param>
        public void SetStreamFromFile(string filename)
        {
            StreamData = System.IO.File.ReadAllBytes(filename);
            _fileExtention = System.IO.Path.GetExtension(filename).Substring(1);
            Name = System.IO.Path.GetFileNameWithoutExtension(filename);
        }
        /// <summary>
        /// Экспорт данных в файл
        /// </summary>
        /// <param name="filename">Полный путь к файлу</param>
        public void ExportStreamDataToFile(string filename)
        {
            if (IsStreamDataNull && !_refreshDode)
                RefreshSteamData();
            if (!IsStreamDataNull)
                System.IO.File.WriteAllBytes(filename, _streamData);
        }
        /// <summary>Загрузить экземпляр из базы данных</summary>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _fileExtention = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                _allowVersion = reader.IsDBNull(18) ? false : reader.GetBoolean(18);
                _versionCode = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _myCompanyId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _userOwnerId = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
                //_streamData = !reader.IsDBNull(16) ? reader.GetSqlBinary(16).Value : null;
            }
            catch(Exception ex)
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.StreamData, SqlDbType.VarBinary) { IsNullable = true };
            if (_streamData == null || _streamData.All(v => v == 0))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _streamData.Length;
                prm.Value = _streamData;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.FileExtention, SqlDbType.NVarChar, 10) { IsNullable = false, Value = _fileExtention };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AllowVersion, SqlDbType.Bit) { IsNullable = false, Value = AllowVersion };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.VersionCode, SqlDbType.Int) { IsNullable = true };
            if (_versionCode == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _versionCode;

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;

            prm = new SqlParameter(GlobalSqlParamNames.UserOwnerId, SqlDbType.Int) { IsNullable = true };
            if (_userOwnerId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _userOwnerId;

            sqlCmd.Parameters.Add(prm);

            prm = sqlCmd.Parameters[GlobalSqlParamNames.NameFull];
            prm.Value = NameFull;
        }
        #region ILinks<Analitic> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<FileData>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<FileData>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<FileData> IChains<FileData>.SourceList(int chainKindId)
        {
            return Chain<FileData>.GetChainSourceList(this, chainKindId);
        }
        List<FileData> IChains<FileData>.DestinationList(int chainKindId)
        {
            return Chain<FileData>.DestinationList(this, chainKindId);
        }
        #endregion

        public static List<FileData> GetCollectionClientFiles(Workarea workarea, int id)
        {
            FileData item;
            List<FileData> collection = new List<FileData>();
            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = string.Empty;
                        procedureName = "Contracts.FilesByClient";
                        // TODO:
                        //if (item.EntityId != 0)
                        //{
                        //    ProcedureMap procedureMap = item.Entity.FindMethod("LoadAll");
                        //    if (procedureMap != null)
                        //    {
                        //        procedureName = procedureMap.FullName;
                        //    }
                        //}
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new FileData { Workarea = workarea };
                            item.Load(reader);
                            collection.Add(item);
                            workarea.Cashe.SetCasheData(item);
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

        private List<FileDataVersion> collectionFileVersions;
        /// <summary>Коллекция версий файлов</summary>
        /// <param name="refresh">Выполнять обновление версий</param>
        /// <returns>Коллекция данных файловых версий</returns>
        public List<FileDataVersion> GetAllVersions(bool refresh=false)
        {
            //
            if (!refresh && collectionFileVersions != null)
                return collectionFileVersions;

            collectionFileVersions = new List<FileDataVersion>();
            FileDataVersion item;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collectionFileVersions;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = string.Empty;
                        procedureName = FindProcedure("GetVersions");
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new FileDataVersion { Workarea = Workarea };
                            item.Load(reader);
                            collectionFileVersions.Add(item);
                            Workarea.Cashe.SetCasheData(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collectionFileVersions;
        }
        /// <summary>
        /// Поиск файла по наименованию и расширению файла в группе
        /// </summary>
        /// <param name="name">Имя файла</param>
        /// <param name="fileExtention">Расширение</param>
        /// <param name="hierarchyId">Идентификатор группы</param>
        /// <param name="filter">Дополнительный фильтр</param>
        /// <returns></returns>
        public List<FileData> FindByFullName(string name, string fileExtention, int? hierarchyId = null,
                    Predicate<FileData> filter = null)
        {
            FileData item = new FileData { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<FileData> collection = new List<FileData>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // TODO: использовать маппинг
                        cmd.CommandText = "[FileData].[FilesFindByFullName]"; //FindProcedure(GlobalMethodAlias.FindBy); 
                        
                        if (hierarchyId != null && hierarchyId.Value!=0)
                            cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;

                        if (!string.IsNullOrWhiteSpace(fileExtention))
                            cmd.Parameters.Add(GlobalSqlParamNames.FileExtention, SqlDbType.NVarChar, 255).Value = fileExtention;
                        if (!string.IsNullOrWhiteSpace(name))
                            cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = name;
                        
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new FileData { Workarea = Workarea };
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


        public static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            return b1.SequenceEqual(b2); 

            //for (int i = 0; i < b1.Length; i++)
            //{
            //    if (b1[i] != b2[i]) return false;
            //}
            //return true;
        }

        #region ICodes
        public List<CodeValue<FileData>> GetValues(bool allKinds)
        {
            return CodeHelper<FileData>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<FileData>.GetView(this, true);
        }
        #endregion
        #region IChainsAdvancedList<FileData,Note> Members

        List<IChainAdvanced<FileData, Note>> IChainsAdvancedList<FileData, Note>.GetLinks()
        {
            return ChainAdvanced<FileData, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<FileData, Note>> IChainsAdvancedList<FileData, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<FileData, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<FileData, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<FileData, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<FileData, Note>.GetChainView()
        {
            return ChainValueView.GetView<FileData, Note>(this);
        }
        #endregion

        /// <summary>
        /// Первая группа в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<FileData>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }

        public List<FileData> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<FileData> filter = null,
            string fileExtention=null, byte[] streamData=null, bool? allowVersion=null,
            bool useAndFilter = false)
        {
            FileData item = new FileData { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<FileData> collection = new List<FileData>();
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

                        if (!string.IsNullOrEmpty(fileExtention))
                            cmd.Parameters.Add(GlobalSqlParamNames.FileExtention, SqlDbType.NVarChar, 5).Value = fileExtention;
                        if (streamData!=null)
                            cmd.Parameters.Add(GlobalSqlParamNames.StreamData, SqlDbType.VarBinary).Value = streamData;
                        if (allowVersion.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.AllowVersion, SqlDbType.Bit).Value = allowVersion.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new FileData { Workarea = Workarea };
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