using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Территориальная единица"</summary>
    internal struct TerritoryStruct
    {
        /// <summary>Координата X</summary>
        public decimal X;
        /// <summary>Координата Y</summary>
        public decimal Y;
        /// <summary>Идентификатор страны</summary>
        public int CountryId;
        /// <summary>Идентификатор города административного управления</summary>
        public int TownId;
        /// <summary>Международное наименование</summary>
        public string NameInternational;
        /// <summary>Национальное наименование</summary>
        public string NameNational;
    }

    /// <summary>
    /// Территориальная единица
    /// </summary>
    /// <remarks>Территорияльной единицей является область, областной район, городской район</remarks>
    public sealed class Territory : BaseCore<Territory>, IChains<Territory>, IEquatable<Territory>,
        IChainsAdvancedList<Territory, Town>,
        ICodes<Territory>,
        IChainsAdvancedList<Territory, FileData>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Область, соответствует значению 1</summary>
        public const int KINDVALUE_REGION = 1;
        /// <summary>Областной район, соответствует значению 2</summary>
        public const int KINDVALUE_REGIONALDISTRICT = 2;
        /// <summary>Городской район (административный район), соответствует значению 3</summary>
        public const int KINDVALUE_BOROUGH = 3;

        /// <summary>Область, соответствует значению 2424833</summary>
        public const int KINDID_REGION = 2424833;
        /// <summary>Областной район, соответствует значению 2424834</summary>
        public const int KINDID_REGIONALDISTRICT = 2424834;
        /// <summary>Городской район (административный район), соответствует значению 2424835</summary>
        public const int KINDID_BOROUGH = 2424835;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Territory>.Equals(Territory other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public Territory(): base()
        {
            EntityId = (short) WhellKnownDbEntity.Territory;
        }
        protected override void CopyValue(Territory template)
        {
            base.CopyValue(template);
            CountryId = template.CountryId;
            TownId = template.TownId;
            X = template.X;
            Y = template.Y;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Territory Clone(bool endInit)
        {
            Territory obj = base.Clone(false);
            obj.CountryId = CountryId;
            obj.TownId = TownId;
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
        private int _countryId;
        /// <summary>
        /// Идентификатор страны
        /// </summary>
        public int CountryId
        {
            get { return _countryId; }
            set
            {
                if (value == _countryId) return;
                OnPropertyChanging(GlobalPropertyNames.CountryId);
                _countryId = value;
                OnPropertyChanged(GlobalPropertyNames.CountryId);
            }
        }


        private Country _country;
        /// <summary>
        /// Страна
        /// </summary>
        public Country Country
        {
            get
            {
                if (_countryId == 0)
                    return null;
                if (_country == null)
                    _country = Workarea.Cashe.GetCasheData<Country>().Item(_countryId);
                else if (_country.Id != _countryId)
                    _country = Workarea.Cashe.GetCasheData<Country>().Item(_countryId);
                return _country;
            }
            set
            {
                if (_country == value) return;
                OnPropertyChanging(GlobalPropertyNames.Country);
                _country = value;
                _countryId = _country == null ? 0 : _country.Id;
                OnPropertyChanged(GlobalPropertyNames.Country);
            }
        }
        

        private int _townId;
        /// <summary>
        /// Идентификатор города административного управления
        /// </summary>
        public int TownId
        {
            get { return _townId; }
            set
            {
                if (value == _townId) return;
                OnPropertyChanging(GlobalPropertyNames.TownId);
                _townId = value;
                OnPropertyChanged(GlobalPropertyNames.TownId);
            }
        }


        private Town _town;
        /// <summary>
        /// Город административного центра
        /// </summary>
        public Town Town
        {
            get
            {
                if (_townId == 0)
                    return null;
                if (_town == null)
                    _town = Workarea.Cashe.GetCasheData<Town>().Item(_townId);
                else if (_town.Id != _townId)
                    _town = Workarea.Cashe.GetCasheData<Town>().Item(_townId);
                return _town;
            }
            set
            {
                if (_town == value) return;
                OnPropertyChanging(GlobalPropertyNames.Town);
                _town = value;
                _townId = _town == null ? 0 : _town.Id;
                OnPropertyChanged(GlobalPropertyNames.Town);
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
        
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_x != 0)
                writer.WriteAttributeString(GlobalPropertyNames.X, XmlConvert.ToString(_x));
            if (_y != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Y, XmlConvert.ToString(_y));
            if (_countryId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CountryId, XmlConvert.ToString(_countryId));
            if (_townId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TownId, XmlConvert.ToString(_townId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.X) != null)
                _x = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.X));
            if (reader.GetAttribute(GlobalPropertyNames.Y) != null)
                _y = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Y));
            if (reader.GetAttribute(GlobalPropertyNames.CountryId) != null)
                _countryId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CountryId));
            if (reader.GetAttribute(GlobalPropertyNames.TownId) != null)
                _townId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TownId));
        }
        #endregion

        #region Состояние
        TerritoryStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new TerritoryStruct
                {
                    X = _x,
                    Y = _y,
                    CountryId = _countryId,
                    TownId = _townId,
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
            CountryId = _baseStruct.CountryId;
            TownId = _baseStruct.TownId;
            NameInternational = _baseStruct.NameInternational;
            NameNational = _baseStruct.NameNational;
            IsChanged = false;
        }
        #endregion

        public override void Validate()
        {
            base.Validate();
            // Страна
            if (_countryId == 0)
                // TODO:
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_VALIDATE_COUNTRYIDEMPTY", 1049));
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
                _countryId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _townId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _emblemId = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
                _nameInternational = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                _nameNational = reader.IsDBNull(23) ? string.Empty : reader.GetString(23);
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.CountryId, SqlDbType.Int)
                                   {
                                       IsNullable = false,
                                       Value = _countryId
                                   };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TownId, SqlDbType.Int)
            {
                IsNullable = true,
            };
            if (_townId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _townId;
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
        }
        #endregion
        #region ICodes
        public List<CodeValue<Territory>> GetValues(bool allKinds)
        {
            return CodeHelper<Territory>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Territory>.GetView(this, true);
        }
        #endregion
        #region IChainsAdvancedList<Library,FileData> Members

        List<IChainAdvanced<Territory, FileData>> IChainsAdvancedList<Territory, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<Territory, FileData>)this).GetLinks(81);
        }

        List<IChainAdvanced<Territory, FileData>> IChainsAdvancedList<Territory, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<Territory, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Territory, FileData>(this);
        }
        public List<IChainAdvanced<Territory, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<Territory, FileData>> collection = new List<IChainAdvanced<Territory, FileData>>();
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
                                ChainAdvanced<Territory, FileData> item = new ChainAdvanced<Territory, FileData> { Workarea = Workarea, Left = this };
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
        #region ILinks<Region> Members
        /// <summary>
        /// Связи региона
        /// </summary>
        /// <returns></returns>
        public List<IChain<Territory>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи региона
        /// </summary>
        /// <param name="kind">Тип связей</param>
        /// <returns></returns>
        public List<IChain<Territory>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Territory> IChains<Territory>.SourceList(int chainKindId)
        {
            return Chain<Territory>.GetChainSourceList(this, chainKindId);
        }
        List<Territory> IChains<Territory>.DestinationList(int chainKindId)
        {
            return Chain<Territory>.DestinationList(this, chainKindId);
        }
        #endregion

        #region IChainsAdvancedList<Territory,Town> Members

        List<IChainAdvanced<Territory, Town>> IChainsAdvancedList<Territory, Town>.GetLinks()
        {
            return ChainAdvanced<Territory, Town>.CollectionSource(this);
        }

        List<IChainAdvanced<Territory, Town>> IChainsAdvancedList<Territory, Town>.GetLinks(int? kind)
        {
            return ChainAdvanced<Territory, Town>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Territory, Town>> GetLinkedTowns(int? kind = null)
        {
            return ChainAdvanced<Territory, Town>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Territory, Town>.GetChainView()
        {
            return ChainValueView.GetView<Territory, Town>(this);
        }
        #endregion
        /// <summary>
        /// Города областного значения
        /// </summary>
        /// <returns></returns>
        public List<Town> GetRegionTowns()
        {
            return ChainAdvanced<Territory, Town>.GetChainSourceList<Territory, Town>(this, 80);
        }
        /// <summary>
        /// Города в областной районе
        /// </summary>
        /// <returns></returns>
        public List<Town> GetRegionAlDistrictTowns()
        {
            return ChainAdvanced<Territory, Town>.GetChainSourceList<Territory, Town>(this, 78);
        }
        /// <summary>
        /// Список всех городов областного значения и в областном районе
        /// </summary>
        /// <returns></returns>
        public List<Town> GetAllTownsInRegion(Territory region, Territory regionAlDistrict)
        {
            return regionAlDistrict.GetRegionAlDistrictTowns().Union(region.GetRegionTowns()).ToList();
        }

        public List<Territory> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Territory> filter = null,  decimal? x = null, decimal? y = null, int? countryId=null, int? townId=null, bool useAndFilter = false)
        {
            Territory item = new Territory { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Territory> collection = new List<Territory>();
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
                        if (countryId.HasValue && countryId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.CountryId, SqlDbType.Int).Value = countryId;
                        if (townId.HasValue && townId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TownId, SqlDbType.Int).Value = townId;
                        if (y.HasValue && y.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Y, SqlDbType.Money).Value = y;
                        if (x.HasValue && x.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.X, SqlDbType.Money).Value = x;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Territory { Workarea = Workarea };
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