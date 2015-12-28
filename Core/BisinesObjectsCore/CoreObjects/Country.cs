using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Страна"</summary>
    internal struct CountryStruct
    {
        /// <summary>Идентификатор валюты</summary>
        public int CurrencyId;
        /// <summary>Федеральные стандарты обработки информации</summary>
        public string Fips;
        /// <summary>Администрация адресного пространства Интернет</summary>
        public string Iana;
        /// <summary>ISO</summary>
        public string Iso;
        /// <summary>ISO</summary>
        public string Iso3;
        /// <summary>Числовой код ISO</summary>
        public int IsoNum;
        /// <summary>Соглашение по стандартизации</summary>
        public string Stanag;
        /// <summary>Координата X</summary>
        public decimal X;
        /// <summary>Координата Y</summary>
        public decimal Y;
        /// <summary>Континент</summary>
        public string Continent;
        /// <summary>Идентификатор герба</summary>
        public int EmblemId;
    }
    /// <summary>
    /// Регион
    /// </summary>
    /// <remarks>
    /// В зависимости от значения свойства KindId представляет различный вид адрессного
    /// расположения: 
    /// <list type="table">
    /// <listheader>
    /// <term>Значение</term>
    /// <description>Описание</description></listheader>
    /// <item>
    /// <term>1</term>
    /// <description>Континент</description></item>
    /// <item>
    /// <term>2</term>
    /// <description>Страна</description></item>
    /// <item>
    /// <term>3</term>
    /// <description>Область</description></item>
    /// <item>
    /// <term>4</term>
    /// <description>Район</description></item>
    /// <item>
    /// <term>5</term>
    /// <description>Город</description></item>
    /// <item>
    /// <term>6</term>
    /// <description>Городской район</description></item>
    /// <item>
    /// <term>7</term>
    /// <description>Улица</description></item></list>
    /// </remarks>
    public sealed class Country : BaseCore<Country>, IChains<Country>, IEquatable<Country>
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Страна, соответствует значению 1</summary>
        public const int KINDVALUE_COUNTRY = 1;
        
        /// <summary>Страна, соответствует значению 2162690</summary>
        public const int KINDID_COUNTRY = 2162689;
        
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Country>.Equals(Country other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public Country():base()
        {
            EntityId = (short) WhellKnownDbEntity.Country;
        }
        protected override void CopyValue(Country template)
        {
            base.CopyValue(template);
            CurrencyId = template.CurrencyId;
            Fips = template.Fips;
            Iana = template.Iana;
            Iso = template.Iso;
            Iso3 = template.Iso3;
            IsoNum = template.IsoNum;
            Stanag = template.Stanag;
            Continent = template.Continent;
            X = template.X;
            Y = template.Y;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Country Clone(bool endInit)
        {
            Country obj = base.Clone(false);
            obj.CurrencyId = CurrencyId;
            obj.Fips = Fips;
            obj.Iana = Iana;
            obj.Iso = Iso;
            obj.Iso3 = Iso3;
            obj.IsoNum = IsoNum;
            obj.Stanag = Stanag;
            obj.X = X;
            obj.Y = Y;
            obj.Continent = Continent;
            obj.EmblemId = EmblemId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        /// <summary>
        /// Сравнение объекта для службы обмена данными
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange(Country value)
        {
            if (!base.CompareExchange(value))
            {
                return false;
            }

            if (!StringNullCompare(value.Fips, Fips))
                return false;
            if (!StringNullCompare(value.Iana, Iana))
                return false;
            if (!StringNullCompare(value.Iso, Iso))
                return false;
            if (!StringNullCompare(value.Iso3, Iso3))
                return false;
            if (value.IsoNum != IsoNum)
                return false;
            if (!StringNullCompare(value.Stanag, Stanag))
                return false;
            if (value.X != X)
                return false;
            if (value.Y != Y)
                return false;
            if (!StringNullCompare(value.Continent, Continent))
                return false;
            if (value.EmblemId != EmblemId)
                return false;

            return true;
        }
        // Как работать с Georaphy
        //
        //http://consultingblogs.emc.com/stevewright/archive/2009/09/28/14057.aspx
        //http://www.reimers.dk/blogs/jacob_reimers_weblog/archive/2009/02/09/sql-server-2008-kml-and-shapefiles.aspx
        //http://dbaknowledge.blogspot.com/2009/12/import-google-map-kml-file-into-table.html
        //http://victorjitlin.wordpress.com/2009/03/07/sql-server-2008-with-google-maps/
        //http://blogs.msdn.com/davidlean/archive/2008/11/14/sql-spatial-how-to-get-spatial-data-free-maps-n-demographics.aspx

        //http://mappointbatchgeocode.codeplex.com/
        //http://msdn.microsoft.com/en-us/magazine/dd434647.aspx
        //http://ru.wikipedia.org/wiki/%D0%A8%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD:%D0%9D%D0%B0%D1%81%D0%B5%D0%BB%D1%91%D0%BD%D0%BD%D1%8B%D0%B9_%D0%BF%D1%83%D0%BD%D0%BA%D1%82_%D0%A3%D0%BA%D1%80%D0%B0%D0%B8%D0%BD%D1%8B
        //http://ru.wikipedia.org/wiki/%D0%9A%D0%B0%D1%82%D0%B5%D0%B3%D0%BE%D1%80%D0%B8%D1%8F:%D0%9D%D0%B0%D1%81%D0%B5%D0%BB%D1%91%D0%BD%D0%BD%D1%8B%D0%B5_%D0%BF%D1%83%D0%BD%D0%BA%D1%82%D1%8B_%D0%A3%D0%BA%D1%80%D0%B0%D0%B8%D0%BD%D1%8B
        //http://social.msdn.microsoft.com/Forums/en-US/sqlspatial/thread/9645b2dc-bc64-46a6-9efc-4ed5fc91b235
        //http://www.microsoft.com/maps/developers/
        //http://sqlblog.com/blogs/mds_team/archive/2010/02/19/creating-a-bing-map-url-with-a-business-rule.aspx
        //http://www.bing.com/toolbox/blogs/maps/archive/2009/07/30/sql-server-and-mappoint-2009-together-at-last.aspx

        //http://mapki.com/wiki/Google_Map_Parameters
        //http://help.live.com/help.aspx?project=wl_local&market=en-us&querytype=topic&query=wl_local_proc_buildurl.htm
        
        //http://maps.google.com/maps?ll=47.974756,37.852352&z=10&t=h&hl=ru
        //http://maps.google.com/maps?ll=48.036917,37.779011&z=10&t=h&hl=ru
        //http://maps.google.com/maps?ll=48.036917,37.779011&z=10&t=m&hl=ru&z=18
        //http://maps.google.com/maps?ll=47.1117360,37.5633070&z=10&t=m&hl=ru&z=18
         
        //http://www.sql-server-helper.com/sql-server-2008/convert-latitude-longitude-to-geography-point.aspx

        //http://social.msdn.microsoft.com/Forums/en-US/vemapcontroldev/thread/1ee6dfc4-b4cd-4bfc-a3ad-71acf80c61c3

        #region Свойства
        private int _currencyId;
        /// <summary>Идентификатор валюты</summary>
        public int CurrencyId
        {
            get { return _currencyId; }
            set
            {
                if (value == _currencyId) return;
                OnPropertyChanging(GlobalPropertyNames.CurrencyId);
                _currencyId = value;
                OnPropertyChanged(GlobalPropertyNames.CurrencyId);
            }
        }

        private Currency _currency;
        /// <summary>
        /// Валюта
        /// </summary>
        public Currency Currency
        {
            get
            {
                if (_currencyId == 0)
                    return null;
                if (_currency == null)
                    _currency = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyId);
                else if (_currency.Id != _currencyId)
                    _currency = Workarea.Cashe.GetCasheData<Currency>().Item(_currencyId);
                return _currency;
            }
            set
            {
                if (_currency == value) return;
                OnPropertyChanging(GlobalPropertyNames.Currency);
                _currency = value;
                _currencyId = _currency == null ? 0 : _currency.Id;
                OnPropertyChanged(GlobalPropertyNames.Currency);
            }
        }
        private string _iso;
        /// <summary>ISO</summary>
        /// <remarks>Значение соответствует двум символам</remarks>
        public string Iso
        {
            get { return _iso; }
            set
            {
                if (value == _iso) return;
                OnPropertyChanging(GlobalPropertyNames.Iso);
                _iso = value;
                OnPropertyChanged(GlobalPropertyNames.Iso);
            }
        }
        private string _iso3;
        /// <summary>ISO</summary>
        /// <remarks>Значение соответствует трем символам</remarks>
        public string Iso3
        {
            get { return _iso3; }
            set
            {
                if (value == _iso3) return;
                OnPropertyChanging(GlobalPropertyNames.Iso3);
                _iso3 = value;
                OnPropertyChanged(GlobalPropertyNames.Iso3);
            }
        }
        private string _fips;
        /// <summary>Федеральные стандарты обработки информации</summary>
        /// <remarks>Значение соответствует двум символам</remarks>
        public string Fips
        {
            get { return _fips; }
            set
            {
                if (value == _fips) return;
                OnPropertyChanging(GlobalPropertyNames.Fips);
                _fips = value;
                OnPropertyChanged(GlobalPropertyNames.Fips);
            }
        }
        private string _stanag;
        /// <summary>Соглашение по стандартизации</summary>
        /// <remarks>Значение соответствует трем символам</remarks>
        public string Stanag
        {
            get { return _stanag; }
            set
            {
                if (value == _stanag) return;
                OnPropertyChanging(GlobalPropertyNames.Stanag);
                _stanag = value;
                OnPropertyChanged(GlobalPropertyNames.Stanag);
            }
        }
        private string _iana;
        /// <summary>Администрация адресного пространства Интернет</summary>
        /// <remarks>
        /// IANA (от англ. Internet Assigned Numbers Authority — «Администрация адресного пространства Интернет») — американская 
        /// некоммерческая организация, управляющая пространствами IP-адресов, доменов верхнего уровня, а также регистрирующая типы данных MIME 
        /// и параметры прочих протоколов Интернета. Находится под контролем ICANN.
        /// Значение не более пяти символов</remarks>
        public string Iana
        {
            get { return _iana; }
            set
            {
                if (value == _iana) return;
                OnPropertyChanging(GlobalPropertyNames.Iana);
                _iana = value;
                OnPropertyChanged(GlobalPropertyNames.Iana);
            }
        }
        private int _isoNum;
        /// <summary>Числовой код ISO</summary>
        public int IsoNum
        {
            get { return _isoNum; }
            set
            {
                if (value == _isoNum) return;
                OnPropertyChanging(GlobalPropertyNames.IsoNum);
                _isoNum = value;
                OnPropertyChanged(GlobalPropertyNames.IsoNum);
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

        private string _continent;
        /// <summary>
        /// Континент
        /// </summary>
        public string Continent
        {
            get { return _continent; }
            set
            {
                if (value == _continent) return;
                OnPropertyChanging(GlobalPropertyNames.Continent);
                _continent = value;
                OnPropertyChanged(GlobalPropertyNames.Continent);
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
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_currencyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CurrencyId, XmlConvert.ToString(_currencyId));
            if (!string.IsNullOrEmpty(_fips))
                writer.WriteAttributeString(GlobalPropertyNames.Fips, _fips);
            if (!string.IsNullOrEmpty(_iana))
                writer.WriteAttributeString(GlobalPropertyNames.Iana, _iana);
            if (!string.IsNullOrEmpty(_iso))
                writer.WriteAttributeString(GlobalPropertyNames.Iso, _iso);
            if (!string.IsNullOrEmpty(_iso3))
                writer.WriteAttributeString(GlobalPropertyNames.Iso3, _iso3);
            if (_isoNum != 0)
                writer.WriteAttributeString(GlobalPropertyNames.IsoNum, XmlConvert.ToString(_isoNum));
            if (!string.IsNullOrEmpty(_stanag))
                writer.WriteAttributeString(GlobalPropertyNames.Stanag, _stanag);
            if (_x != 0)
                writer.WriteAttributeString(GlobalPropertyNames.X, XmlConvert.ToString(_x));
            if (_y != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Y, XmlConvert.ToString(_y));
            if (!string.IsNullOrEmpty(_continent))
                writer.WriteAttributeString(GlobalPropertyNames.Continent, _continent);
            if (_emblemId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.EmblemId, XmlConvert.ToString(_emblemId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.CurrencyId) != null)
                _currencyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CurrencyId));
            if (reader.GetAttribute(GlobalPropertyNames.Fips) != null)
                _fips = reader[GlobalPropertyNames.Fips];
            if (reader.GetAttribute(GlobalPropertyNames.Iana) != null)
                _iana = reader[GlobalPropertyNames.Iana];
            if (reader.GetAttribute(GlobalPropertyNames.Iso) != null)
                _iso = reader[GlobalPropertyNames.Iso];
            if (reader.GetAttribute(GlobalPropertyNames.Iso3) != null)
                _iso3 = reader[GlobalPropertyNames.Iso3];
            if (reader.GetAttribute(GlobalPropertyNames.IsoNum) != null)
                _isoNum = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.IsoNum));
            if (reader.GetAttribute(GlobalPropertyNames.Stanag) != null)
                _stanag = reader[GlobalPropertyNames.Stanag];
            if (reader.GetAttribute(GlobalPropertyNames.X) != null)
                _x = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.X));
            if (reader.GetAttribute(GlobalPropertyNames.Y) != null)
                _y = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Y));
            if (reader.GetAttribute(GlobalPropertyNames.Continent) != null)
                _continent = reader[GlobalPropertyNames.Continent];
            if (reader.GetAttribute(GlobalPropertyNames.EmblemId) != null)
                _emblemId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.EmblemId));
        }
        #endregion

        #region Состояние
        CountryStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new CountryStruct
                {
                    CurrencyId = _currencyId,
                    Fips = _fips,
                    Iana = _iana,
                    Iso = _iso,
                    Iso3 = _iso3,
                    IsoNum = _isoNum,
                    Stanag = _stanag,
                    X = _x,
                    Y = _y,
                    Continent = _continent,
                    EmblemId = _emblemId
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            CurrencyId = _baseStruct.CurrencyId;
            Fips = _baseStruct.Fips;
            Iana = _baseStruct.Iana;
            Iso = _baseStruct.Iso;
            Iso3 = _baseStruct.Iso3;
            IsoNum = _baseStruct.IsoNum;
            Stanag = _baseStruct.Stanag;
            X = _baseStruct.X;
            Y = _baseStruct.Y;
            Continent = _baseStruct.Continent;
            EmblemId = _baseStruct.EmblemId;
            IsChanged = false;
        }
        #endregion

        /// <summary>Проверка соответствия объекта бизнес правилам</summary>
        /// <remarks>Метод выполняет проверку наименования объекта <see cref="BaseCore{T}.Name"/> на предмет null, <see cref="string.Empty"/> и максимальную длину не более 255 символов
        /// Объект требует наличия ISO кода размером в два символа и ISO3 кода размером в три символа.
        /// </remarks>
        /// <returns><c>true</c> - если объект соответствует бизнес правилам, <c>false</c> в противном случае</returns>
        /// <exception cref="ValidateException">Если объект не соответствует текущим правилам</exception>
        public override void Validate()
        {
            base.Validate();
            // Страна
            if (KindValue == 2)
            {
                if (string.IsNullOrEmpty(_iso))
                    throw new ValidateException(Workarea.Cashe.ResourceString("EX_VALIDATE_ISOEMPTY", 1049));
                if (_iso.Length != 2)
                    throw new ValidateException(Workarea.Cashe.ResourceString("EX_VALIDATE_ISOLEN", 1049));

                if (string.IsNullOrEmpty(_iso3))
                    throw new ValidateException(Workarea.Cashe.ResourceString("EX_VALIDATE_ISO3EMPTY", 1049));
                if (_iso3.Length != 3)
                    throw new ValidateException(Workarea.Cashe.ResourceString("EX_VALIDATE_ISO3LEN", 1049));
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
                _x = reader.IsDBNull(17) ? 0 : reader.GetDecimal(17);
                _y = reader.IsDBNull(18) ? 0 : reader.GetDecimal(18);
                _currencyId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _iso = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                _iso3 = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                _isoNum = reader.IsDBNull(22) ? 0 : reader.GetInt32(22);
                _fips = reader.IsDBNull(23) ? string.Empty : reader.GetString(23);
                _stanag = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                _iana = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                _continent = reader.IsDBNull(26) ? string.Empty : reader.GetString(26);
                _emblemId = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
            }
            catch(Exception ex)
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
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.CurrencyId, SqlDbType.Int) { IsNullable = true };
            if (_currencyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _currencyId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Iso, SqlDbType.NVarChar, 2) { IsNullable = true };
            if (string.IsNullOrEmpty(_iso))
                prm.Value = DBNull.Value;
            else
                prm.Value = _iso;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Iso3, SqlDbType.NVarChar, 3) { IsNullable = true };
            if (string.IsNullOrEmpty(_iso3))
                prm.Value = DBNull.Value;
            else
                prm.Value = _iso3;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.IsoNum, SqlDbType.Int) { IsNullable = true };
            if (_isoNum == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _isoNum;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Fips, SqlDbType.NVarChar, 2) { IsNullable = true };
            if (string.IsNullOrEmpty(_fips))
                prm.Value = DBNull.Value;
            else
                prm.Value = _fips;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Stanag, SqlDbType.NVarChar, 3) { IsNullable = true };
            if (string.IsNullOrEmpty(_stanag))
                prm.Value = DBNull.Value;
            else
                prm.Value = _stanag;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Iana, SqlDbType.NVarChar, 5) { IsNullable = true };
            if (string.IsNullOrEmpty(_iana))
                prm.Value = DBNull.Value;
            else
                prm.Value = _iana;
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

            prm = new SqlParameter(GlobalSqlParamNames.Continent, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_continent))
                prm.Value = DBNull.Value;
            else
                prm.Value = _continent;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EmblemId, SqlDbType.Int) { IsNullable = true };
            if (_emblemId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _emblemId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion
        /// <summary>
        /// Список уникальных континентов 
        /// </summary>
        /// <returns></returns>
        public List<string> GetDistinctContinents()
        {
            List<string> res = new List<string>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return res;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindProcedure(GlobalMethodAlias.GetDistinctContinents);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            res.Add(reader.GetString(0));
                        }
                            
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }

            return res;
        }
        #region ILinks<Region> Members
        /// <summary>
        /// Связи региона
        /// </summary>
        /// <returns></returns>
        public List<IChain<Country>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи региона
        /// </summary>
        /// <param name="kind">Тип связей</param>
        /// <returns></returns>
        public List<IChain<Country>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Country> IChains<Country>.SourceList(int chainKindId)
        {
            return Chain<Country>.GetChainSourceList(this, chainKindId);
        }
        List<Country> IChains<Country>.DestinationList(int chainKindId)
        {
            return Chain<Country>.DestinationList(this, chainKindId);
        }
        #endregion

        public List<Country> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Country> filter = null, decimal? x = null, decimal? y = null, int? currencyId = null,
            string iso = null, string iso3 = null, string isoNum = null, string fips = null,
            string stanag = null, string iana = null, string continent = null,
            bool useAndFilter = false)
        {
            Country item = new Country { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Country> collection = new List<Country>();
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
                        if (y.HasValue && y.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Y, SqlDbType.Decimal).Value = y;
                        if (x.HasValue && x.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.X, SqlDbType.Decimal).Value = x;

                        if (currencyId.HasValue && currencyId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.CurrencyId, SqlDbType.Int).Value = currencyId;
                        if (!string.IsNullOrEmpty(iso))
                            cmd.Parameters.Add(GlobalSqlParamNames.Iso, SqlDbType.NVarChar).Value = iso;
                        if (!string.IsNullOrEmpty(iso3))
                            cmd.Parameters.Add(GlobalSqlParamNames.Iso3, SqlDbType.NVarChar).Value = iso3;
                        if (!string.IsNullOrEmpty(isoNum))
                            cmd.Parameters.Add(GlobalSqlParamNames.IsoNum, SqlDbType.NVarChar).Value = isoNum;
                        if (!string.IsNullOrEmpty(fips))
                            cmd.Parameters.Add(GlobalSqlParamNames.Fips, SqlDbType.NVarChar).Value = fips;
                        if (!string.IsNullOrEmpty(stanag))
                            cmd.Parameters.Add(GlobalSqlParamNames.Stanag, SqlDbType.NVarChar).Value = stanag;
                        if (!string.IsNullOrEmpty(iana))
                            cmd.Parameters.Add(GlobalSqlParamNames.Iana, SqlDbType.NVarChar).Value = iana;
                        if (!string.IsNullOrEmpty(continent))
                            cmd.Parameters.Add(GlobalSqlParamNames.Continent, SqlDbType.NVarChar).Value = continent;


                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Country { Workarea = Workarea };
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