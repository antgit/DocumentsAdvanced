using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Набор правил"</summary>
    internal struct TownStruct
    {
        /// <summary>Координата X</summary>
        public decimal X;
        /// <summary>Координата Y</summary>
        public decimal Y;
        /// <summary>Идентификатор территории, области</summary>
        public int TerritoryId;
        /// <summary>Международное наименование</summary>
        public string NameInternational;
        /// <summary>Национальное наименование</summary>
        public string NameNational;
    }

    /// <summary>
    /// Город
    /// </summary>
    public sealed class Town : BaseCore<Town>, IEquatable<Town>, ICodes<Town>,
        IChainsAdvancedList<Town, FileData>, IChains<Town>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Город, соответствует значению 1</summary>
        public const int KINDVALUE_TOWN = 1;
        /// <summary>Село, соответствует значению 2</summary>
        public const int KINDVALUE_VILLAGE = 2;
        /// <summary>Поселок городского типа, соответствует значению 3</summary>
        public const int KINDVALUE_PGT = 3;
        /// <summary>Поселок, соответствует значению 4</summary>
        public const int KINDVALUE_TOWNSHIP = 4;

        /// <summary>Город, соответствует значению 2490369</summary>
        public const int KINDID_TOWN = 2490369;
        /// <summary>Село, соответствует значению 2490370</summary>
        public const int KINDID_VILLAGE = 2490370;
        /// <summary>Поселок городского типа, соответствует значению 2490371</summary>
        public const int KINDID_PGT = 2490371;
        /// <summary>Поселок, соответствует значению 2490372</summary>
        public const int KINDID_TOWNSHIP = 2490372;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Town>.Equals(Town other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public Town()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Town;
        }
        protected override void CopyValue(Town template)
        {
            base.CopyValue(template);
            TerritoryId = template.TerritoryId;
            X = template.X;
            Y = template.Y;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Town Clone(bool endInit)
        {
            Town obj = base.Clone(false);
            obj.TerritoryId = TerritoryId;
            obj.X = X;
            obj.Y = Y;
            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства
        private decimal _x;
        /// <summary>Координата X</summary>
        public decimal X
        {
            get { return _x; }
            set
            {
                if (value == _x) return;
                OnPropertyChanging(GlobalPropertyNames.X);
                _x = value;
                OnPropertyChanged(GlobalPropertyNames.X);
            }
        }

        private decimal _y;
        /// <summary>Координата Y</summary>
        public decimal Y
        {
            get { return _y; }
            set
            {
                if (value == _y) return;
                OnPropertyChanging(GlobalPropertyNames.Y);
                _y = value;
                OnPropertyChanged(GlobalPropertyNames.Y);
            }
        } 
        private int _territoryId;
        /// <summary>
        /// Идентификатор территории, области
        /// </summary>
        public int TerritoryId
        {
            get { return _territoryId; }
            set
            {
                if (value == _territoryId) return;
                OnPropertyChanging(GlobalPropertyNames.TerritoryId);
                _territoryId = value;
                OnPropertyChanged(GlobalPropertyNames.TerritoryId);
            }
        }

        private Territory _territory;
        /// <summary>
        /// Область
        /// </summary>
        public Territory Territory
        {
            get
            {
                if (_territoryId == 0)
                    return null;
                if (_territory == null)
                    _territory = Workarea.Cashe.GetCasheData<Territory>().Item(_territoryId);
                else if (_territory.Id != _territoryId)
                    _territory = Workarea.Cashe.GetCasheData<Territory>().Item(_territoryId);
                return _territory;
            }
            set
            {
                if (_territory == value) return;
                OnPropertyChanging(GlobalPropertyNames.Territory);
                _territory = value;
                _territoryId = _territory == null ? 0 : _territory.Id;
                OnPropertyChanged(GlobalPropertyNames.Territory);
            }
        }


        private int _emblemId;
        /// <summary>
        /// Идентификатор герба
        /// </summary>
        public int EmblemId
        {
            get { return _emblemId; }
            set
            {
                if (value == _emblemId) return;
                OnPropertyChanging(GlobalPropertyNames.EmblemId);
                _emblemId = value;
                OnPropertyChanged(GlobalPropertyNames.EmblemId);
            }
        }


        private FileData _emblem;
        /// <summary>
        /// Герб
        /// </summary>
        public FileData Emblem
        {
            get
            {
                if (_emblemId == 0)
                    return null;
                if (_emblem == null)
                    _emblem = Workarea.Cashe.GetCasheData<FileData>().Item(_emblemId);
                else if (_emblem.Id != _emblemId)
                    _emblem = Workarea.Cashe.GetCasheData<FileData>().Item(_emblemId);
                return _emblem;
            }
            set
            {
                if (_emblem == value) return;
                OnPropertyChanging(GlobalPropertyNames.Emblem);
                _emblem = value;
                _emblemId = _emblem == null ? 0 : _emblem.Id;
                OnPropertyChanged(GlobalPropertyNames.Emblem);
            }
        }


        private string _nameInternational;
        /// <summary>Международное наименование</summary>
        public string NameInternational
        {
            get { return _nameInternational; }
            set
            {
                if (value == _nameInternational) return;
                OnPropertyChanging(GlobalPropertyNames.NameInternational);
                _nameInternational = value;
                OnPropertyChanged(GlobalPropertyNames.NameInternational);
            }
        }


        private string _nameNational;
        /// <summary>Национальное наименование</summary>
        public string NameNational
        {
            get { return _nameNational; }
            set
            {
                if (value == _nameNational) return;
                OnPropertyChanging(GlobalPropertyNames.NameNational);
                _nameNational = value;
                OnPropertyChanged(GlobalPropertyNames.NameNational);
            }
        }



        private DateTime? _dateFoundation;
        /// <summary>Дата основания</summary>
        public DateTime? DateFoundation
        {
            get { return _dateFoundation; }
            set
            {
                if (value == _dateFoundation) return;
                OnPropertyChanging(GlobalPropertyNames.DateFoundation);
                _dateFoundation = value;
                OnPropertyChanged(GlobalPropertyNames.DateFoundation);
            }
        }

        private string _nameOld;
        /// <summary>Прошлое наименование</summary>
        public string NameOld
        {
            get { return _nameOld; }
            set
            {
                if (value == _nameOld) return;
                OnPropertyChanging(GlobalPropertyNames.NameOld);
                _nameOld = value;
                OnPropertyChanged(GlobalPropertyNames.NameOld);
            }
        }


        private string _postIndex;
        /// <summary>Почтовый индекс</summary>
        public string PostIndex
        {
            get { return _postIndex; }
            set
            {
                if (value == _postIndex) return;
                OnPropertyChanging(GlobalPropertyNames.PostIndex);
                _postIndex = value;
                OnPropertyChanged(GlobalPropertyNames.PostIndex);
            }
        }


        private decimal? _territoryKvKm;
        /// <summary>Территория в тис. кв. км.</summary>
        public decimal? TerritoryKvKm
        {
            get { return _territoryKvKm; }
            set
            {
                if (value == _territoryKvKm) return;
                OnPropertyChanging(GlobalPropertyNames.TerritoryKvKm);
                _territoryKvKm = value;
                OnPropertyChanged(GlobalPropertyNames.TerritoryKvKm);
            }
        }


        private int? _population;
        /// <summary>Население в тысячах людей</summary>
        public int? Population
        {
            get { return _population; }
            set
            {
                if (value == _population) return;
                OnPropertyChanging(GlobalPropertyNames.Population);
                _population = value;
                OnPropertyChanged(GlobalPropertyNames.Population);
            }
        }

        private decimal? _populationDensity;
        /// <summary>Плотность населения людей/кв.км</summary>
        public decimal? PopulationDensity
        {
            get { return _populationDensity; }
            set
            {
                if (value == _populationDensity) return;
                OnPropertyChanging(GlobalPropertyNames.PopulationDensity);
                _populationDensity = value;
                OnPropertyChanged(GlobalPropertyNames.PopulationDensity);
            }
        }


        private int _agentId;
        /// <summary>
        /// Идентификатор корреспондента основного органа управления
        /// </summary>
        public int AgentId
        {
            get { return _agentId; }
            set
            {
                if (value == _agentId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentId);
                _agentId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentId);
            }
        }


        private Agent _agent;
        /// <summary>
        /// Корреспондент "Основной орган управления"
        /// </summary>
        public Agent Agent
        {
            get
            {
                if (_agentId == 0)
                    return null;
                if (_agent == null)
                    _agent = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId);
                else if (_agent.Id != _agentId)
                    _agent = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId);
                return _agent;
            }
            set
            {
                if (_agent == value) return;
                OnPropertyChanging(GlobalPropertyNames.Agent);
                _agent = value;
                _agentId = _agent == null ? 0 : _agent.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent);
            }
        }



        private string _amts;
        /// <summary>
        /// Код номера телефона для АМТС
        /// </summary>
        public string Amts
        {
            get { return _amts; }
            set
            {
                if (value == _amts) return;
                OnPropertyChanging(GlobalPropertyNames.Amts);
                _amts = value;
                OnPropertyChanged(GlobalPropertyNames.Amts);
            }
        }
        

        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_x != 0)
                writer.WriteAttributeString(GlobalPropertyNames.X, XmlConvert.ToString(_x));
            if (_y != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Y, XmlConvert.ToString(_y));
            if (_territoryId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TerritoryId, XmlConvert.ToString(_territoryId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.X) != null)
                _x = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.X));
            if (reader.GetAttribute(GlobalPropertyNames.Y) != null)
                _y = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Y));
            if (reader.GetAttribute(GlobalPropertyNames.TerritoryId) != null)
                _territoryId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TerritoryId));
        }
        #endregion

        #region Состояние
        TownStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new TownStruct
                {
                    X = _x,
                    Y = _y,
                    TerritoryId = _territoryId,
                    NameInternational = _nameInternational,
                    NameNational = _nameNational
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            X = _baseStruct.X;
            Y = _baseStruct.Y;
            TerritoryId = _baseStruct.TerritoryId;
            NameInternational = _baseStruct.NameInternational;
            NameNational = _baseStruct.NameNational;

            IsChanged = false;
        }
        #endregion

        /// <summary>Проверка соответствия объекта бизнес правилам</summary>
        /// <remarks>Метод выполняет проверку наименования объекта <see cref="BaseCore{T}.Name"/> на предмет null, <see cref="string.Empty"/> и максимальную длину не более 255 символов</remarks>
        /// <returns><c>true</c> - если объект соответствует бизнес правилам, <c>false</c> в противном случае</returns>
        /// <exception cref="ValidateException">Если объект не соответствует текущим правилам</exception>
        public override void Validate()
        {
            base.Validate();
            // Территория
            if (_territoryId == 0)
                // TODO:
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_VALIDATE_TERRITORYIDEMPTY", 1049));
        }
        #region База данных
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="reader">Объект <see cref="SqlDataReader"/> чтения данных</param>
        /// <param name="endInit">Закончить инициализацию</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _x = reader.IsDBNull(17) ? 0 : reader.GetDecimal(17);
                _y = reader.IsDBNull(18) ? 0 : reader.GetDecimal(18);
                _territoryId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _emblemId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _nameInternational = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                _nameNational = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);

                _dateFoundation = reader.IsDBNull(23) ? (DateTime?)null : reader.GetDateTime(23);
                _nameOld = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                _postIndex = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                _territoryKvKm = reader.IsDBNull(26) ? (decimal?)null : reader.GetDecimal(26);
                _population = reader.IsDBNull(27) ? (int?)null : reader.GetInt32(27);
                _populationDensity = reader.IsDBNull(28) ? (decimal?)null : reader.GetDecimal(28);
                _agentId = reader.IsDBNull(29) ? 0: reader.GetInt32(29);
                _amts = reader.IsDBNull(30) ? string.Empty : reader.GetString(30);

            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>
        /// Установить значения параметров для комманды создания
        /// </summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.TerritoryId, SqlDbType.Int)
                                   {
                                       IsNullable = true,
                                       Value = _territoryId
                                   };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.X, SqlDbType.Decimal) { IsNullable = true };
            if (_x == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _x;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Y, SqlDbType.Decimal) { IsNullable = true };
            if (_y == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _y;
            sqlCmd.Parameters.Add(prm);


            prm = new SqlParameter(GlobalSqlParamNames.EmblemId, SqlDbType.Int) { IsNullable = true };
            if (_emblemId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _emblemId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.NameInternational, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_nameInternational))
                prm.Value = DBNull.Value;
            else
                prm.Value = _nameInternational;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.NameNational, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_nameNational))
                prm.Value = DBNull.Value;
            else
                prm.Value = _nameNational;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.NameOld, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_nameOld))
                prm.Value = DBNull.Value;
            else
                prm.Value = _nameOld;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateFoundation, SqlDbType.Date) { IsNullable = true };
            if (!_dateFoundation.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateFoundation;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PostIndex, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_postIndex))
                prm.Value = DBNull.Value;
            else
                prm.Value = _postIndex;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TerritoryKvKm, SqlDbType.Decimal) { IsNullable = true };
            if (!_territoryKvKm.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _territoryKvKm;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Population, SqlDbType.Decimal) { IsNullable = true };
            if (!_population.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _population;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PopulationDensity, SqlDbType.Decimal) { IsNullable = true };
            if (!_populationDensity.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _populationDensity;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AgentId, SqlDbType.Decimal) { IsNullable = true };
            if (_agentId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _agentId;
            sqlCmd.Parameters.Add(prm);


            prm = new SqlParameter(GlobalSqlParamNames.Amts, SqlDbType.NVarChar, 10) { IsNullable = true };
            if (string.IsNullOrEmpty(_amts))
                prm.Value = DBNull.Value;
            else
                prm.Value = _amts;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Town>> GetValues(bool allKinds)
        {
            return CodeHelper<Town>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Town>.GetView(this, true);
        }
        #endregion

        #region ILinks<Town> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<Town>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи городов</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Town>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Town> IChains<Town>.SourceList(int chainKindId)
        {
            return Chain<Town>.GetChainSourceList(this, chainKindId);
        }
        List<Town> IChains<Town>.DestinationList(int chainKindId)
        {
            return Chain<Town>.DestinationList(this, chainKindId);

        }
        #endregion

        #region IChainsAdvancedList<Library,FileData> Members

        List<IChainAdvanced<Town, FileData>> IChainsAdvancedList<Town, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<Town, FileData>)this).GetLinks(82);
        }

        List<IChainAdvanced<Town, FileData>> IChainsAdvancedList<Town, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<Town, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Town, FileData>(this);
        }
        public List<IChainAdvanced<Town, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<Town, FileData>> collection = new List<IChainAdvanced<Town, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Library>().Entity.FindMethod("LoadFiles").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = Id;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainAdvanced<Town, FileData> item = new ChainAdvanced<Town, FileData> { Workarea = Workarea, Left = this };
                                item.Load(reader);
                                collection.Add(item);
                            }
                        }
                        reader.Close();

                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
            return collection;
        }

        #endregion
        public List<string> GetStreets()
        {
            List<string> values = new List<string>();
            string res = string.Empty;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return values;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Territory.GetTownStreet";//FindProcedure(GlobalMethodAlias.GetFlagStringAll);
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            values.Add(reader.GetString(0));
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }

            return values;
        }

        public List<Town> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Town> filter = null, int? territoryId = null, decimal? x=null, decimal? y=null, bool useAndFilter = false)
        {
            Town item = new Town { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Town> collection = new List<Town>();
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

                        if (territoryId.HasValue && territoryId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TerritoryId, SqlDbType.Int).Value = territoryId;
                        if (y.HasValue && y.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Y, SqlDbType.Money).Value = y;
                        if (x.HasValue && x.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.X, SqlDbType.Money).Value = x;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Town { Workarea = Workarea };
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