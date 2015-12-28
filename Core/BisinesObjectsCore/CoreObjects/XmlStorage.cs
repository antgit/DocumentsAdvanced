using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Xml данные"</summary>
    internal struct XmlStorageStruct
    {
        /// <summary>Данные XML</summary>
        public string XmlData;
        /// <summary>Идентификатор пользователя</summary>
        public int UserId;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>Xml данные</summary>
    public sealed class XmlStorage : BaseCore<XmlStorage>, IChains<XmlStorage>, IEquatable<XmlStorage>, ICompanyOwner
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Общие данные в Xml формате, соответствует значению 1</summary>
        public const int KINDVALUE_GENERALDATA = 1;
        /// <summary>Настройки экспорта-импорта данных, соответствует значению 2</summary>
        public const int KINDVALUE_EXCHANGEDATA = 2;
        /// <summary>Настройки LayoutControl, соответствует значению 3</summary>
        public const int KINDVALUE_LAYOUTCONTROLDATA = 3;
        /// <summary>Настройки интерфесных модулей, соответствует значению 4</summary>
        public const int KINDVALUE_INTERFACEDATA = 4;
        /// <summary>Настройки списков, соответствует значению 5</summary>
        public const int KINDVALUE_LISTDATA = 5;
        /// <summary>Настройки схемы документа, соответствует значению 6</summary>
        public const int KINDVALUE_XMLSCHEMADOC = 6;
        /// <summary>Настройки документов, соответствует значению 7</summary>
        public const int KINDVALUE_DOCSETTINGS = 7;

        /// <summary>Общие данные в Xml формате, соответствует значению 2359297</summary>
        public const int KINDID_GENERALDATA = 2359297;
        /// <summary>Настройки экспорта-импорта данных, соответствует значению 2359298</summary>
        public const int KINDID_EXCHANGEDATA = 2359298;
        /// <summary>Настройки LayoutControl, соответствует значению 2359299</summary>
        public const int KINDID_LAYOUTCONTROLDATA = 2359299;
        /// <summary>Настройки интерфесных модулей, соответствует значению 2359300</summary>
        public const int KINDID_INTERFACEDATA = 2359300;
        /// <summary>Настройки списков, соответствует значению 2359301</summary>
        public const int KINDID_LISTDATA = 2359301;
        /// <summary>Настройки схемы документа, соответствует значению 2359302</summary>
        public const int KINDID_XMLSCHEMADOC = 2359302;
        /// <summary>Настройки документов, соответствует значению 2359303</summary>
        public const int KINDID_DOCSETTINGS = 2359303;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<XmlStorage>.Equals(XmlStorage other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public XmlStorage()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.XmlStorage;
        }
        protected override void CopyValue(XmlStorage template)
        {
            base.CopyValue(template);
            XmlData = template.XmlData;
            UserId = template.UserId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override XmlStorage Clone(bool endInit)
        {
            XmlStorage obj = base.Clone(false);
            obj.XmlData = XmlData;
            obj.UserId = UserId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства
        private string _xmlData;
        /// <summary>Данные XML</summary>
        public string XmlData
        {
            get { return _xmlData; }
            set
            {
                if (value == _xmlData) return;
                OnPropertyChanging(GlobalPropertyNames.XmlData);
                _xmlData = value;
                OnPropertyChanged(GlobalPropertyNames.XmlData);
            }
        }
        private int _userId;
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId
        {
            get { return _userId; }
            set
            {
                if (value == _userId) return;
                OnPropertyChanging(GlobalPropertyNames.UserId);
                _userId = value;
                OnPropertyChanged(GlobalPropertyNames.UserId);
            }
        }
        
        private Uid _uid;
        /// <summary>
        /// Пользователь
        /// </summary>
        public Uid Uid
        {
            get
            {
                if (_userId == 0)
                    return null;
                if (_uid == null)
                    _uid = Workarea.Cashe.GetCasheData<Uid>().Item(_userId);
                else if (_uid.Id != _userId)
                    _uid = Workarea.Cashe.GetCasheData<Uid>().Item(_userId);
                return _uid;
            }
            set
            {
                if (_uid == value) return;
                OnPropertyChanging(GlobalPropertyNames.User);
                _uid = value;
                _userId = _uid == null ? 0 : _uid.Id;
                OnPropertyChanged(GlobalPropertyNames.User);
            }
        }

        private int _myCompanyId;
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит аналитика
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
        /// Моя компания, предприятие которому принадлежит аналитика
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

            if (!string.IsNullOrEmpty(_xmlData))
                writer.WriteAttributeString(GlobalPropertyNames.XmlData, _xmlData);
            if (_userId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserId, XmlConvert.ToString(_userId));
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.XmlData) != null)
                _xmlData = reader[GlobalPropertyNames.XmlData];
            if (reader.GetAttribute(GlobalPropertyNames.UserId) != null)
                _userId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UserId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion

        #region Состояние
        XmlStorageStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new XmlStorageStruct { XmlData = _xmlData, UserId = _userId, MyCompanyId = _myCompanyId };
                return true;
            }
            return false;
        }
        /// <summary>Востановить состояние</summary>
        public override void RestoreState()
        {
            base.RestoreState();
            XmlData = _baseStruct.XmlData;
            UserId = _baseStruct.UserId;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion

        #region База данных
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект <see cref="SqlDataReader"/> чтения данных</param>
        /// <param name="endInit">Закончить инициализацию</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _xmlData = reader.IsDBNull(17)? string.Empty: reader.GetString(17);
                _userId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _myCompanyId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
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
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.XmlData, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_xmlData))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _xmlData.Length;
                prm.Value = _xmlData;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserId, SqlDbType.Int) { IsNullable = true};
            if (_userId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _userId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<XmlStorage> Members
        /// <summary>Связи xml данных</summary>
        /// <returns></returns>
        public List<IChain<XmlStorage>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи xml данных</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<XmlStorage>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<XmlStorage> IChains<XmlStorage>.SourceList(int chainKindId)
        {
            return Chain<XmlStorage>.GetChainSourceList(this, chainKindId);
        }
        List<XmlStorage> IChains<XmlStorage>.DestinationList(int chainKindId)
        {
            return Chain<XmlStorage>.DestinationList(this, chainKindId);
        }
        #endregion

        public List<XmlStorage> FindByCodeUserId(string code, int userId, Predicate<XmlStorage> filter = null)
        {
            return FindBy(code: code, userId: userId, filter: filter, useAndFilter: true);
            //XmlStorage item = new XmlStorage { Workarea = Workarea };
            //if (item.EntityId == 0)
            //{
            //    throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            //}
            //List<XmlStorage> collection = new List<XmlStorage>();
            //using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            //{
            //    if (cnn == null) return collection;

            //    try
            //    {
            //        using (SqlCommand cmd = cnn.CreateCommand())
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            // TODO: использовать маппинг
            //            cmd.CommandText = "[Core].[XmlStoragesFindByCodeUserId]"; //FindProcedure(GlobalMethodAlias.FindBy);

            //            if (!string.IsNullOrWhiteSpace(code))
            //                cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;

            //            if (userId != 0)
            //                cmd.Parameters.Add(GlobalSqlParamNames.UserId, SqlDbType.Int).Value = userId;
            //            SqlDataReader reader = cmd.ExecuteReader();

            //            while (reader.Read())
            //            {
            //                item = new XmlStorage { Workarea = Workarea };
            //                item.Load(reader);
            //                Workarea.Cashe.SetCasheData(item);
            //                if (filter != null && filter.Invoke(item))
            //                    collection.Add(item);
            //                else if (filter == null)
            //                    collection.Add(item);

            //            }
            //            reader.Close();

            //        }
            //    }
            //    finally
            //    {
            //        cnn.Close();
            //    }
            //}
            //return collection;
        }

        public List<XmlStorage> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<XmlStorage> filter = null,
            int? userId=null,
            bool useAndFilter = false)
        {
            XmlStorage item = new XmlStorage { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<XmlStorage> collection = new List<XmlStorage>();
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
                        if (userId.HasValue && userId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.UserId, SqlDbType.Int).Value = userId.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new XmlStorage { Workarea = Workarea };
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