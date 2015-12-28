using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Набор правил"</summary>
    internal struct RulesetStruct
    {
        /// <summary>Значение</summary>
        public string Value;
        /// <summary>Идентификатор библиотеки</summary>
        public int LibraryId;
        /// <summary>Имя типа</summary>
        public string ActivityName;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }

    /// <summary>Набор правил</summary>
    public sealed class Ruleset : BaseCore<Ruleset>, IChains<Ruleset>, IReportChainSupport, IEquatable<Ruleset>, ICodes<Ruleset>, ICompanyOwner
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Набор правил, соответствует значению 1</summary>
        public const int KINDVALUE_RULESET = 1;
        /// <summary>Процессы создания документов, соответствует значению 2</summary>
        public const int KINDVALUE_CREATEPROC = 2;
        /// <summary>Процессы фоновой обработки документов перед сохранением, соответствует значению 3</summary>
        public const int KINDVALUE_BEFORESAVEPROC = 3;
        /// <summary>Процессы фоновой обработки документов после сохранения, соответствует значению 4</summary>
        public const int KINDVALUE_AFTERSAVEPROC = 4;
        /// <summary>Дополнительные действия в документе, соответствует значению 5</summary>
        public const int KINDVALUE_ADDITIONALACTIONS= 5;
        /// <summary>Процесс формирования бухгалтерских проводок, соответствует значению 6</summary>
        public const int KINDVALUE_POSTINGPROC = 6;
        /// <summary>Процессы документов, соответствует значению 7</summary>
        public const int KINDVALUE_DOCPROC= 7;
        /// <summary>Фоновые процессы документов, соответствует значению 8</summary>
        public const int KINDVALUE_DOCPROCCESS_FONE = 8;
        /// <summary>Групповые процессы документов, соответствует значению 9</summary>
        public const int KINDVALUE_DOCPROCCESS_GROUP = 9;
        /// <summary>Действия для единичного объекта (Windows), соответствует значению 10</summary>
        public const int KINDVALUE_ACTIONWIN = 10;
        /// <summary>Набор правил WEB, соответствует значению 11</summary>
        public const int KINDVALUE_WEBRULESET = 11;

        /// <summary>Набор правил, соответствует значению 2293761</summary>
        public const int KINDID_RULESET = 2293761;
        /// <summary>Процессы создания документов, соответствует значению 2293762</summary>
        public const int KINDID_CREATEPROC = 2293762;
        /// <summary>Процессы фоновой обработки документов перед сохранением, соответствует значению 2293763</summary>
        public const int KINDID_BEFORESAVEPROC = 2293763;
        /// <summary>Процессы фоновой обработки документов после сохранения, соответствует значению 2293764</summary>
        public const int KINDID_AFTERSAVEPROC = 2293764;
        /// <summary>Дополнительные действия в документе, соответствует значению 2293765</summary>
        public const int KINDID_ADDITIONALACTIONS = 2293765;
        /// <summary>Процесс формирования бухгалтерских проводок, соответствует значению 2293766</summary>
        public const int KINDID_POSTINGPROC = 2293766;
        /// <summary>Процессы документов, соответствует значению 2293767</summary>
        public const int KINDID_DOCPROC = 2293767;
        /// <summary>Фоновые процессы документов, соответствует значению 2293768</summary>
        public const int KINDID_DOCPROCCESS_FONE = 2293768;
        /// <summary>Групповые процессы документов, соответствует значению 2293769</summary>
        public const int KINDID_DOCPROCCESS_GROUP = 2293769;
        /// <summary>Действия для единичного объекта (Windows), соответствует значению 2293770</summary>
        public const int KINDID_ACTIONWIN = 2293770;
        /// <summary>Набор правил WEB, соответствует значению 2293771</summary>
        public const int KINDID_WEBRULESET = 2293771;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Ruleset>.Equals(Ruleset other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public Ruleset()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Ruleset;
        }
        protected override void CopyValue(Ruleset template)
        {
            base.CopyValue(template);
            Value = template.Value;
            LibraryId = template.LibraryId;
            ActivityName = template.ActivityName;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Ruleset Clone(bool endInit)
        {
            Ruleset obj = base.Clone(false);
            obj.Value = Value;
            obj.LibraryId = LibraryId;
            obj.ActivityName = ActivityName;

            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства
        private string _value;
        /// <summary>Значение</summary>
        public string Value
        {
            get { return _value; }
            set
            {
                if (value == _value) return;
                OnPropertyChanging(GlobalPropertyNames.Value);
                _value = value;
                OnPropertyChanged(GlobalPropertyNames.Value);
            }
        }
        private int _libraryId;
        /// <summary>Идентификатор библиотеки</summary>
        public int LibraryId
        {
            get { return _libraryId; }
            set
            {
                if (value == _libraryId) return;
                OnPropertyChanging(GlobalPropertyNames.LibraryId);
                _libraryId = value;
                OnPropertyChanged(GlobalPropertyNames.LibraryId);
            }
        }
        private Library _library;
        /// <summary>
        /// Библиотека
        /// </summary>
        public Library Library
        {
            get
            {
                if (_libraryId == 0)
                    return null;
                if (_library == null)
                    _library = Workarea.Cashe.GetCasheData<Library>().Item(_libraryId);
                else if (_library.Id != _libraryId)
                    _library = Workarea.Cashe.GetCasheData<Library>().Item(_libraryId);
                return _library;
            }
            set
            {
                if (_library == value) return;
                OnPropertyChanging(GlobalPropertyNames.Library);
                _library = value;
                _libraryId = _library == null ? 0 : _library.Id;
                OnPropertyChanged(GlobalPropertyNames.Library);
            }
        }
        private string _activityName;
        /// <summary>Имя типа</summary>
        public string ActivityName
        {
            get { return _activityName; }
            set
            {
                if (value == _activityName) return;
                OnPropertyChanging(GlobalPropertyNames.ActivityName);
                _activityName = value;
                OnPropertyChanged(GlobalPropertyNames.ActivityName);
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

            if (!string.IsNullOrEmpty(_value))
                writer.WriteAttributeString(GlobalPropertyNames.Value, _value);
            if (_libraryId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.LibraryId, XmlConvert.ToString(_libraryId));
            if (!string.IsNullOrEmpty(_activityName))
                writer.WriteAttributeString(GlobalPropertyNames.ActivityName, _activityName);
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

            if (reader.GetAttribute(GlobalPropertyNames.Value) != null)
                _value = reader[GlobalPropertyNames.Value];
            if (reader.GetAttribute(GlobalPropertyNames.LibraryId) != null)
                _libraryId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.LibraryId));
            if (reader.GetAttribute(GlobalPropertyNames.ActivityName) != null)
                _activityName = reader[GlobalPropertyNames.ActivityName];
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
        }
        #endregion

        #region Состояние
        RulesetStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new RulesetStruct
                {
                    Value = _value,
                    ActivityName = _activityName,
                    LibraryId = _libraryId,
                    MyCompanyId = _myCompanyId
                };
                return true;
            }
            return false;
        }

        /// <summary>Востановить текущее состояние объекта</summary>
        /// <remarks>Востановление состояние возможно только после выполнения сосхранения состояния</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            Value = _baseStruct.Value;
            ActivityName = _baseStruct.ActivityName;
            LibraryId = _baseStruct.LibraryId;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion

        public Stream ValueToStream()
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(Value);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }
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
                _value = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                _libraryId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _activityName = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                _myCompanyId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.LibraryId, SqlDbType.Int)
                                   {
                                       IsNullable = true,
                                       Value = _libraryId
                                   };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ActivityName, SqlDbType.NVarChar, 255) {IsNullable = false, Value = _activityName};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Value, SqlDbType.NVarChar, -1) { IsNullable = true };
            if (_value == null)
                prm.Value = DBNull.Value;
            else
                prm.Value = _value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.NVarChar) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #region ILinks<Ruleset> Members
        /// <summary>Связи</summary>
        /// <returns></returns>
        public List<IChain<Ruleset>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Ruleset>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Ruleset> IChains<Ruleset>.SourceList(int chainKindId)
        {
            return Chain<Ruleset>.GetChainSourceList(this, chainKindId);
        }
        List<Ruleset> IChains<Ruleset>.DestinationList(int chainKindId)
        {
            return Chain<Ruleset>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Ruleset>> GetValues(bool allKinds)
        {
            return CodeHelper<Ruleset>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Ruleset>.GetView(this, true);
        }
        #endregion

        public List<Ruleset> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Ruleset> filter = null,
            int? libraryId = null, string activityName = null, int? myCompanyId = null,
            bool useAndFilter = false)
        {
            Ruleset item = new Ruleset { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Ruleset> collection = new List<Ruleset>();
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
                        if (libraryId.HasValue && libraryId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.LibraryId, SqlDbType.Int).Value = libraryId.Value;
                        if(!string.IsNullOrEmpty(activityName))
                            cmd.Parameters.Add(GlobalSqlParamNames.ActivityName, SqlDbType.NVarChar, 255).Value = activityName;
                        if (myCompanyId.HasValue && myCompanyId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = myCompanyId.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Ruleset { Workarea = Workarea };
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