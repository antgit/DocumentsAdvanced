using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct AgentAddressStruct
    {
        /// <summary>Идентификатор корреспондента</summary>
        public int OwnId;
        /// <summary>Идентификатор страны</summary>
        public int CountryId;
        /// <summary>Идентификатор города</summary>
        public int TownId;
        /// <summary>Идентификатор района</summary>
        public int TerritoryId;
        /// <summary>Почтовый индекс</summary>
        public string PostIndex;
        /// <summary>Координата X</summary>
        public decimal X;
        /// <summary>Координата Y</summary>
        public decimal Y;
        /// <summary>Идентификатор областного района</summary>
        public int RegionId;
        /// <summary>Радиус зоны корреспондента</summary>
        public int ZoneRadius;
    }
    /// <summary>
    /// Адрес корреспондента
    /// </summary>
    public sealed class AgentAddress : BaseCore<AgentAddress>, IRelationMany
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Юридический адрес, соответствует значению 1</summary>
        public const int KINDVALUE_LEGALADDRESS = 1;
        /// <summary>Фактический адрес, соответствует значению 2</summary>
        public const int KINDVALUE_ACTUALADDRESS = 2;
        /// <summary>Адрес доставки, соответствует значению 3</summary>
        public const int KINDVALUE_DELIVERYADDRESS = 3;
        

        /// <summary>Юридический адрес, соответствует значению 4325377</summary>
        public const int KINDID_LEGALADDRESS = 4325377;
        /// <summary>Фактический адрес, соответствует значению 4325378</summary>
        public const int KINDID_ACTUALADDRESS = 4325378;
        /// <summary>Адрес доставки, соответствует значению 4325379</summary>
        public const int KINDID_DELIVERYADDRESS = 4325379;
        // ReSharper restore InconsistentNaming
        #endregion
        /// <summary>
        /// Конструктор
        /// </summary>
        public AgentAddress()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.AgentAddress;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit"></param>
        /// <returns></returns>
        protected override AgentAddress Clone(bool endInit)
        {
            AgentAddress obj = base.Clone(false);
            obj.CountryId = CountryId;
            obj.OwnId = OwnId;
            obj.PostIndex = PostIndex;
            obj.TerritoryId = TerritoryId;
            obj.TownId = TownId;
            obj.X = X;
            obj.Y = Y;
            obj.RegionId = RegionId;
            obj.ZoneRadius = ZoneRadius;
            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства
        private int _ownId;
        /// <summary>
        /// Идентификатор корреспондента
        /// </summary>
        public int OwnId
        {
            get { return _ownId; }
            set
            {
                if (_ownId == value) return;
                OnPropertyChanging(GlobalPropertyNames.OwnId);
                _ownId = value;
                OnPropertyChanged(GlobalPropertyNames.OwnId);
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

        private Country _сountry;
        /// <summary>
        /// Страна
        /// </summary>
        public Country Country
        {
            get
            {
                if (_countryId == 0)
                    return null;
                if (_сountry == null)
                    _сountry = Workarea.Cashe.GetCasheData<Country>().Item(_countryId);
                else if (_сountry.Id != _countryId)
                    _сountry = Workarea.Cashe.GetCasheData<Country>().Item(_countryId);
                return _сountry;
            }
            set
            {
                if (_сountry == value) return;
                OnPropertyChanging(GlobalPropertyNames.Country);
                _сountry = value;
                _countryId = _сountry == null ? 0 : _сountry.Id;
                OnPropertyChanged(GlobalPropertyNames.Country);
            }
        }

        private int _townId;
        /// <summary>
        /// Идентификатор города
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
        /// Город
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
        


        private int _territoryId;
        /// <summary>
        /// Идентификатор области
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


        private string _postIndex;
        /// <summary>
        /// Почтовый индекс
        /// </summary>
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


        private int _regionId;
        /// <summary>
        /// Идентификатор областного района
        /// </summary>
        public int RegionId
        {
            get { return _regionId; }
            set
            {
                if (value == _regionId) return;
                OnPropertyChanging(GlobalPropertyNames.RegionId);
                _regionId = value;
                OnPropertyChanged(GlobalPropertyNames.RegionId);
            }
        }

        private Territory _region;
        /// <summary>
        /// Областной район
        /// </summary>
        public Territory Region
        {
            get
            {
                if (_regionId == 0)
                    return null;
                if (_region == null)
                    _region = Workarea.Cashe.GetCasheData<Territory>().Item(_regionId);
                else if (_region.Id != _regionId)
                    _region = Workarea.Cashe.GetCasheData<Territory>().Item(_regionId);
                return _region;
            }
            set
            {
                if (_region == value) return;
                OnPropertyChanging(GlobalPropertyNames.Region);
                _region = value;
                _regionId = _region == null ? 0 : _region.Id;
                OnPropertyChanged(GlobalPropertyNames.Region);
            }
        }

        private int _zoneRadius;
        /// <summary>
        /// Радиус зоны корреспондента в метрах (для GPS мониторинга)
        /// </summary>
        public int ZoneRadius
        {
            get { return _zoneRadius; }
            set
            {
                if (value == _zoneRadius) return;
                OnPropertyChanging(GlobalPropertyNames.ZoneRadius);
                _zoneRadius = value;
                OnPropertyChanged(GlobalPropertyNames.ZoneRadius);
            }
        }

        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));
            if (_countryId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CountryId, XmlConvert.ToString(_countryId));
            if (_townId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TownId, XmlConvert.ToString(_townId));
            if (_territoryId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TerritoryId, XmlConvert.ToString(_territoryId));
            if (_regionId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.RegionId, XmlConvert.ToString(_regionId));
            if (!string.IsNullOrEmpty(_postIndex))
                writer.WriteAttributeString(GlobalPropertyNames.PostIndex, _postIndex);
            if (_x != 0)
                writer.WriteAttributeString(GlobalPropertyNames.X, XmlConvert.ToString(_x));
            if (_x != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Y, XmlConvert.ToString(_y));
            if (_zoneRadius != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ZoneRadius, XmlConvert.ToString(_zoneRadius));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnId));
            if (reader.GetAttribute(GlobalPropertyNames.CountryId) != null)
                _countryId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CountryId));
            if (reader.GetAttribute(GlobalPropertyNames.TownId) != null)
                _townId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TownId));
            if (reader.GetAttribute(GlobalPropertyNames.TerritoryId) != null)
                _territoryId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TerritoryId));
            if (reader.GetAttribute(GlobalPropertyNames.RegionId) != null)
                _regionId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RegionId));
            if (reader.GetAttribute(GlobalPropertyNames.PostIndex) != null)
                _postIndex = reader.GetAttribute(GlobalPropertyNames.PostIndex);
            if (reader.GetAttribute(GlobalPropertyNames.X) != null)
                _x = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.X));
            if (reader.GetAttribute(GlobalPropertyNames.Y) != null)
                _y = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Y));
            if (reader.GetAttribute(GlobalPropertyNames.ZoneRadius) != null)
                _zoneRadius = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ZoneRadius));
        }
        #endregion

        #region Состояние
        AgentAddressStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new AgentAddressStruct
                {
                    OwnId = _ownId,
                    CountryId = _countryId,
                    TownId=_townId,
                    TerritoryId=_territoryId,
                    PostIndex=_postIndex,
                    RegionId = _regionId,
                    X=_x,
                    Y=_y,
                    ZoneRadius = _zoneRadius
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            OwnId = _baseStruct.OwnId;
            CountryId = _baseStruct.CountryId;
            TownId = _baseStruct.TownId;
            TerritoryId = _baseStruct.TerritoryId;
            PostIndex = _baseStruct.PostIndex;
            RegionId = _baseStruct.RegionId;
            X = _baseStruct.X;
            Y = _baseStruct.Y;
            ZoneRadius = _baseStruct.ZoneRadius;
            IsChanged = false;
        }
        #endregion
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _countryId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _territoryId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _townId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _postIndex = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                _x = reader.IsDBNull(22) ? 0 : reader.GetDecimal(22);
                _y = reader.IsDBNull(23) ? 0 : reader.GetDecimal(23);
                _regionId = reader.IsDBNull(24) ? 0 : reader.GetInt32(24);
                _zoneRadius = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (endInit)
                OnEndInit();
        }
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.OwnId, SqlDbType.Int) { Value = _ownId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CountryId, SqlDbType.Int);
            if(_countryId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _countryId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TerritoryId, SqlDbType.Int);
            if(_territoryId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _territoryId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.TownId, SqlDbType.Int);
            if(_townId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _townId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RegionId, SqlDbType.Int);
            if (_regionId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _regionId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PostIndex, SqlDbType.Int);
            if(string.IsNullOrEmpty(_postIndex))
                prm.Value = DBNull.Value;
            else
                prm.Value = _postIndex;
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

            prm = new SqlParameter(GlobalSqlParamNames.ZoneRadius, SqlDbType.Int) { IsNullable = true };
            if (_zoneRadius == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _zoneRadius;
            sqlCmd.Parameters.Add(prm);

        }

        #region IRelationSingle Members

        string IRelationSingle.Schema
        {
            get { return GlobalSchemaNames.Contractor; }
        }

        #endregion
    }
}