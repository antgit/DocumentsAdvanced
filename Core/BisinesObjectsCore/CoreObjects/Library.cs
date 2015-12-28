using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Библиотека"</summary>
    internal struct LibraryStruct
    {
        /// <summary>Идентификатор файла библиотеки</summary>
        public int AssemblyId;
        /// <summary>Идентификатор файла исходника</summary>
        public int AssemblySourceId;
        /// <summary>Версия</summary>
        public string AssemblyVersion;
        /// <summary>Идентификатор списка представлений</summary>
        public int ListId;
        /// <summary>Ссылка для отчетов SQL Server Reporting Service</summary>
        public string TypeUrl;
        /// <summary>Тип в родительской библиотеке</summary>
        public int LibraryTypeId;
        /// <summary>Дополнительная строка параметров для отчетов ReportingService</summary>
        public string Params;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
        /// <summary>Url справочной информации</summary>
        public string HelpUrl;
    }
    /// <summary>
    /// Библиотека
    /// </summary>
    /// <remarks> Свойство KindValue для интерактивных отчетов - 256</remarks>
    public sealed class Library : BaseCore<Library>, IChains<Library>, IEquatable<Library>, IFacts<Library>,
        IChainsAdvancedList<Library, FileData>,
        IChainsAdvancedList<Library, Ruleset>,
        IChainsAdvancedList<Library, XmlStorage>,
        IChainsAdvancedList<Library, Note>,
        ICodes<Library>, ICompanyOwner
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Библиотека, соответствует значению 983041</summary>
        public const int KINDID_LIBRARY = 983041;
        /// <summary>Библиотка ресурсов, соответствует значению 983042</summary>
        public const int KINDID_RESOURCE = 983042;
        /// <summary>Приложение, соответствует значению 983043</summary>
        public const int KINDID_APP = 983043;
        /// <summary>Отчет SQL Reporting Service, соответствует значению 983044</summary>
        public const int KINDID_REPSQL = 983044;
        /// <summary>Отчет табличный, соответствует значению 983045</summary>
        public const int KINDID_REPTBL = 983045;
        /// <summary>Окно, соответствует значению 983046</summary>
        public const int KINDID_WINDOW = 983046;
        /// <summary>Закладка, соответствует значению 983047</summary>
        public const int KINDID_PAGE = 983047;
        /// <summary>Печатная форма, соответствует значению 983048</summary>
        public const int KINDID_PRINTFORM = 983048;
        /// <summary>Конфигурационный файл, соответствует значению 983049</summary>
        public const int KINDID_CONFIGFILE = 983049;
        /// <summary>Web отчет Stimulsoft, соответствует значению 983051</summary>
        public const int KINDID_WEBREPORT = 983051;
        /// <summary>Отчет DevExpress, соответствует значению 983050</summary>
        public const int KINDID_DXREPORT = 983050;
        /// <summary>Форма документа, соответствует значению 983056</summary>
        public const int KINDID_DOCFORM = 983056;
        /// <summary>Метод, соответствует значению 983057</summary>
        public const int KINDID_METHOD = 983057;
        /// <summary>Интерфейсный модуль, соответствует значению 983058</summary>
        public const int KINDID_UIMODULE = 983058;
        /// <summary>Печатный отчет, соответствует значению 983059</summary>
        public const int KINDID_REPPRINT = 983059;
        /// <summary>Интерфейсный модуль Web, соответствует значению 983060</summary>
        public const int KINDID_WEBMODULE = 983060;
        /// <summary>Форма Web документа, соответствует значению 983061</summary>
        public const int KINDID_WEBDOCFORM = 983061;
        /// <summary>Форма Web документа, соответствует значению 983061</summary>
        public const int KINDID_WEBPRINTFORM = 983062;

        /// <summary>Библиотека, соответствует значению 1</summary>
        public const int KINDVALUE_LIBRARY = 1;
        /// <summary>Библиотка ресурсов, соответствует значению 2</summary>
        public const int KINDVALUE_RESOURCE = 2;
        /// <summary>Приложение, соответствует значению 3</summary>
        public const int KINDVALUE_APP = 3;
        /// <summary>Отчет SQL Reporting Service, соответствует значению 4</summary>
        public const int KINDVALUE_REPSQL = 4;
        /// <summary>Отчет табличный, соответствует значению 5</summary>
        public const int KINDVALUE_REPTBL = 5;
        /// <summary>Окно, соответствует значению 6</summary>
        public const int KINDVALUE_WINDOW = 6;
        /// <summary>Закладка, соответствует значению 7</summary>
        public const int KINDVALUE_PAGE = 7;
        /// <summary>Печатная форма, соответствует значению 8</summary>
        public const int KINDVALUE_PRINTFORM = 8;
        /// <summary>Конфигурационный файл, соответствует значению 9</summary>
        public const int KINDVALUE_CONFIGFILE = 9;
        /// <summary>Отчет DevExpress, соответствует значению 10</summary>
        public const int KINDVALUE_DXREPORT = 10;
        /// <summary>Web отчет Stimulsoft, соответствует значению 11</summary>
        public const int KINDVALUE_WEBREPORT = 11;
        /// <summary>Форма документа, соответствует значению 16</summary>
        public const int KINDVALUE_DOCFORM = 16;
        /// <summary>Метод, соответствует значению 17</summary>
        public const int KINDVALUE_METHOD = 17;
        /// <summary>Интерфейсный модуль, соответствует значению 18</summary>
        public const int KINDVALUE_UIMODULE = 18;
        /// <summary>Печатный отчет, соответствует значению 19</summary>
        public const int KINDVALUE_REPPRINT = 19;
        /// <summary>Интерфейсный модуль Web, соответствует значению 20</summary>
        public const int KINDVALUE_WEBMODULE = 20;
        /// <summary>Форма Web документа, соответствует значению 21</summary>
        public const int KINDVALUE_WEBDOCFORM = 21;
        /// <summary>Форма Web документа, соответствует значению 21</summary>
        public const int KINDVALUE_WEBPRINTFORM = 22;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Library>.Equals(Library other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        public static Assembly GetAssemblyFromGac(Library lib)
        {
            if (lib == null || string.IsNullOrEmpty(lib.NameFull))
                return null;
            Assembly loadedAss = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(s => s.FullName == lib.NameFull);
            if (loadedAss != null)
                return loadedAss;
            AssemblyName asm = new AssemblyName(lib.NameFull);
            try
            {
                return Assembly.Load(asm);
            }
            catch (Exception)
            {

            }

            return null;
        }
        protected override void CopyValue(Library template)
        {
            base.CopyValue(template);
            AssemblyId = template.AssemblyId;
            AssemblySourceId = template.AssemblySourceId;
            AssemblyVersion = template.AssemblyVersion;
            ListId = template.ListId;
            TypeUrl = template.TypeUrl;
            LibraryTypeId = template.LibraryTypeId;
            Params = template.Params;
            MyCompanyId = template.MyCompanyId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit"></param>
        /// <returns></returns>
        protected override Library Clone(bool endInit)
        {
            Library obj = base.Clone(false);
            obj.AssemblyId = AssemblyId;
            obj.AssemblySourceId = AssemblySourceId;
            obj.AssemblyVersion = AssemblyVersion;
            obj.ListId = ListId;
            obj.TypeUrl = TypeUrl;
            obj.LibraryTypeId = LibraryTypeId;
            obj.Params = Params;
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        /// <summary>
        /// Соиск содержимого библиотеки по аттрибутам
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="typeName">Имя типа</param>
        /// <param name="libraryId">Ид библиотеки</param>
        /// <param name="code">Код</param>
        /// <param name="fullName">Полное имя типа</param>
        /// <returns></returns>
        public static int LibraryContentFind(Workarea wa, string typeName, int libraryId, string code, string fullName)
        {
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Library>().Entity.FindMethod("LibraryContentFindId").FullName;

                        cmd.Parameters.Add(GlobalSqlParamNames.TypeName, SqlDbType.NVarChar, 255).Value = typeName;

                        cmd.Parameters.Add(GlobalSqlParamNames.LibraryId, SqlDbType.Int).Value = libraryId;

                        cmd.Parameters.Add(GlobalSqlParamNames.KindCode, SqlDbType.NVarChar, 255).Value = code;

                        cmd.Parameters.Add(GlobalSqlParamNames.FullTypeName, SqlDbType.NVarChar, 255).Value = fullName;

                        cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                        if (cnn.State != ConnectionState.Open)
                            cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        if (cmd.Parameters[GlobalSqlParamNames.Return].Value != null)
                            return (int)cmd.Parameters[GlobalSqlParamNames.Return].Value;
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return 0;
        }
        /// <summary>
        /// Список зарегестрированных в библиотеке форм документов и методов
        /// </summary>
        /// <param name="value">Библиотека</param>
        /// <returns></returns>
        public static List<LibraryContent> GetLibraryContents(Library value)
        {
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = value.Entity.FindMethod("LibraryContent").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        if (cnn.State != ConnectionState.Open)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<LibraryContent> coll = new List<LibraryContent>();
                        while (reader.Read())
                        {
                            LibraryContent item = new LibraryContent { Workarea = value.Workarea };
                            item.Load(reader);
                            coll.Add(item);
                        }
                        return coll;
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /// <summary>
        /// Поиск библиотеки по идентификатору строки состава
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="value">Идентификатор состава библиотеки</param>
        /// <returns></returns>
        public static int GetLibraryIdByContent(Workarea wa, int value)
        {
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // TODO: Перенести в глобальные методы
                        cmd.CommandText = "[Core].[LibraryContentFindLibraryId]";// value.Entity.FindMethod("LibraryContent").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value;
                        if (cnn.State != ConnectionState.Open)
                            cmd.Connection.Open();
                        object val = cmd.ExecuteScalar();
                        if (val == null)
                            return 0;
                        return (int)val;
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /// <summary>
        /// Поиск библиотеки по имени
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="fileName">Имя файла библиотеки</param>
        /// <returns></returns>
        public static Library GetLibraryByFile(Workarea wa, string fileName)
        {
            Library item = null;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return item;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.FileName, SqlDbType.NVarChar, 255).Value = fileName;
                        cmd.CommandText =
                            wa.Empty<Library>().Entity.FindMethod("Core.LibraryGetByFile").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new Library { Workarea = wa };
                            item.Load(reader);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return item;
        }

        /// <summary>Конструктор</summary>
        public Library():base()
        {
            EntityId = (short)WhellKnownDbEntity.Library;
        }
        protected override void OnSaving(System.ComponentModel.CancelEventArgs e)
        {
            if (AssemblyId != 0)
                AssemblyDll.Save();
            if (AssemblySourceId != 0)
                AssemblySource.Save();
            base.OnSaving(e);
        }
        #region Свойтсва

        private int _assemblyId;
        /// <summary>
        /// Идентификатор файла библиотеки
        /// </summary>
        public int AssemblyId
        {
            get { return _assemblyId; }
            set
            {
                if (value == _assemblyId) return;
                OnPropertyChanging(GlobalPropertyNames.AssemblyId);
                _assemblyId = value;
                OnPropertyChanged(GlobalPropertyNames.AssemblyId);
            }
        }

        private int _assemblySourceId;
        /// <summary>
        /// Идентификатор файла исходника
        /// </summary>
        public int AssemblySourceId
        {
            get { return _assemblySourceId; }
            set
            {
                if (value == _assemblySourceId) return;
                OnPropertyChanging(GlobalPropertyNames.AssemblySourceId);
                _assemblySourceId = value;
                OnPropertyChanged(GlobalPropertyNames.AssemblySourceId);
            }
        }

        private FileData _assemblyDll;
        public FileData AssemblyDll
        {
            get
            {
                if (_assemblyId == 0)
                    return null;
                if (_assemblyDll == null)
                    _assemblyDll = Workarea.Cashe.GetCasheData<FileData>().Item(_assemblyId);
                else if (_assemblyDll.Id != _assemblyId)
                    _assemblyDll = Workarea.Cashe.GetCasheData<FileData>().Item(_assemblyId);
                return _assemblyDll;
            }
            set
            {
                if (_assemblyDll == value) return;
                OnPropertyChanging(GlobalPropertyNames.AssemblyDll);
                _assemblyDll = value;
                _assemblyId = _assemblyDll == null ? 0 : _assemblyDll.Id;
                OnPropertyChanged(GlobalPropertyNames.AssemblyDll);
            }
        }

        private FileData _assemblySource;
        public FileData AssemblySource
        {
            get
            {
                if (_assemblySourceId == 0)
                    return null;
                if (_assemblySource == null)
                    _assemblySource = Workarea.Cashe.GetCasheData<FileData>().Item(_assemblySourceId);
                else if (_assemblySource.Id != _assemblySourceId)
                    _assemblySource = Workarea.Cashe.GetCasheData<FileData>().Item(_assemblySourceId);
                return _assemblySource;
            }
            set
            {
                if (_assemblySource == value) return;
                OnPropertyChanging(GlobalPropertyNames.AssemblySource);
                _assemblySource = value;
                _assemblySourceId = _assemblySource == null ? 0 : _assemblySource.Id;
                OnPropertyChanged(GlobalPropertyNames.AssemblySource);
            }
        }

        private int _libraryTypeId;
        /// <summary>Тип в родительской библиотеке</summary>
        public int LibraryTypeId
        {
            get { return _libraryTypeId; }
            set
            {
                if (_libraryTypeId == value) return;
                OnPropertyChanging(GlobalPropertyNames.LibraryTypeId);
                _libraryTypeId = value;
                OnPropertyChanged(GlobalPropertyNames.LibraryTypeId);
            }
        }

        private LibraryContent _libraryContent;

        public LibraryContent LibraryContent
        {
            get
            {
                if (_libraryTypeId == 0)
                    return null;
                if (_libraryContent != null && _libraryContent.Id == _libraryTypeId)
                    return _libraryContent;
                _libraryContent = new LibraryContent { Workarea = Workarea };
                _libraryContent.Load(_libraryTypeId);
                return _libraryContent;
            }
        }

        private string _params;
        /// <summary>Дополнительная строка параметров для отчетов ReportingService</summary>
        public string Params
        {
            get { return _params; }
            set
            {
                if (_params == value) return;
                OnPropertyChanging(GlobalPropertyNames.Params);
                _params = value;
                OnPropertyChanged(GlobalPropertyNames.Params);
            }
        }

        private string _typeUrl;
        /// <summary>Ссылка для отчетов SQL Server Reporting Service</summary>
        public string TypeUrl
        {
            get { return _typeUrl; }
            set
            {
                if (_typeUrl == value) return;
                OnPropertyChanging(GlobalPropertyNames.TypeUrl);
                _typeUrl = value;
                OnPropertyChanged(GlobalPropertyNames.TypeUrl);
            }
        }        
        private string _assemblyVersion;
        /// <summary>
        /// Версия
        /// </summary>
        public string AssemblyVersion
        {
            get { return _assemblyVersion; }
            set 
            {
                if (value == _assemblyVersion) return;
                OnPropertyChanging(GlobalPropertyNames.AssemblyVersion);
                _assemblyVersion = value;
                OnPropertyChanged(GlobalPropertyNames.AssemblyVersion);
            }
        }
        private int _listId;
        /// <summary>Идентификатор списка представлений</summary>
        public int ListId
        {
            get { return _listId; }
            set
            {
                if (_listId == value) return;
                OnPropertyChanging(GlobalPropertyNames.ListId);
                _listId = value;
                OnPropertyChanged(GlobalPropertyNames.ListId);
            }
        }

        private CustomViewList _listView;
        /// <summary>
        /// Список представлений
        /// </summary>
        public CustomViewList ListView
        {
            get
            {
                if (_listId == 0)
                    return null;
                if (_listView != null && _listView.Id == _listId)
                    return _listView;
                _listView = new CustomViewList { Workarea = Workarea };
                _listView.Load(_listId);
                return _listView;
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



        private string _helpUrl;
        /// <summary>Url справочной информации</summary>
        public string HelpUrl
        {
            get { return _helpUrl; }
            set
            {
                if (value == _helpUrl) return;
                OnPropertyChanging(GlobalPropertyNames.HelpUrl);
                _helpUrl = value;
                OnPropertyChanged(GlobalPropertyNames.HelpUrl);
            }
        }
        
        
        #endregion

        #region Состояние
        LibraryStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new LibraryStruct
                                  {
                                      AssemblyId = _assemblyId,
                                      AssemblySourceId = _assemblySourceId,
                                      AssemblyVersion = _assemblyVersion,
                                      LibraryTypeId = _libraryTypeId,
                                      ListId = _listId,
                                      Params = _params,
                                      TypeUrl = _typeUrl,
                                      MyCompanyId = _myCompanyId,
                                      HelpUrl = _helpUrl
                                  };
                return true;
            }
            return false;
        }
        /// <summary>
        /// Востановить состояние
        /// </summary>
        public override void RestoreState()
        {
            base.RestoreState();
            AssemblyId = _baseStruct.AssemblyId;
            AssemblySourceId = _baseStruct.AssemblySourceId;
            AssemblyVersion = _baseStruct.AssemblyVersion;
            LibraryTypeId = _baseStruct.LibraryTypeId;
            ListId = _baseStruct.ListId;
            Params = _baseStruct.Params;
            TypeUrl = _baseStruct.TypeUrl;
            MyCompanyId = _baseStruct.MyCompanyId;
            HelpUrl = _baseStruct.HelpUrl;
            IsChanged = false;
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

            if (_assemblyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AssemblyId, XmlConvert.ToString(_assemblyId));
            if (_assemblySourceId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AssemblySourceId, XmlConvert.ToString(_assemblySourceId));
            if (_myCompanyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));

            if (!string.IsNullOrEmpty(_helpUrl))
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _helpUrl);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.AssemblyId) != null)
                _assemblyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AssemblyId));
            if (reader.GetAttribute(GlobalPropertyNames.AssemblySourceId) != null)
                _assemblySourceId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AssemblySourceId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));

            if (reader.GetAttribute(GlobalPropertyNames.HelpUrl) != null)
                _helpUrl = reader.GetAttribute(GlobalPropertyNames.HelpUrl);
        }
        #endregion

        /// <summary>Загрузить экземпляр из базы данных</summary>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _assemblyId = !reader.IsDBNull(17) ? reader.GetInt32(17) : 0;
                _assemblySourceId = !reader.IsDBNull(18) ? reader.GetInt32(18) : 0;
                _assemblyVersion = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                _libraryTypeId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _typeUrl = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                _params = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                _listId = reader.IsDBNull(23) ? 0 : reader.GetInt32(23);
                _myCompanyId = reader.IsDBNull(24) ? 0 : reader.GetInt32(24);
                _helpUrl = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.AssemblyId, SqlDbType.Int) {IsNullable = true};
            if (AssemblyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = AssemblyId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AssemblySourceId, SqlDbType.Int) { IsNullable = true };
            if (AssemblySourceId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = AssemblySourceId;
            sqlCmd.Parameters.Add(prm);
                      
            prm = new SqlParameter(GlobalSqlParamNames.AssemblyVersion, SqlDbType.NVarChar, 255) {IsNullable = true};
            if (string.IsNullOrEmpty(_assemblyVersion))
                prm.Value = DBNull.Value;
            else
                prm.Value = _assemblyVersion;
            sqlCmd.Parameters.Add(prm);


            prm = new SqlParameter(GlobalSqlParamNames.LibraryTypeId, SqlDbType.Int) { IsNullable = true };
            if (_libraryTypeId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _libraryTypeId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TypeUrl, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_typeUrl))
                prm.Value = DBNull.Value;
            else
                prm.Value = _typeUrl;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Params, SqlDbType.NVarChar, -1) { IsNullable = true };
            if (string.IsNullOrEmpty(_params))
                prm.Value = DBNull.Value;
            else
                prm.Value = _params;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ListId, SqlDbType.Int) { IsNullable = true };
            if (_listId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _listId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.HelpUrl, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_helpUrl))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _helpUrl.Length;
                prm.Value = _helpUrl;
            }
            sqlCmd.Parameters.Add(prm);
        }
        /// <summary>
        /// Установить значение для библиотеки
        /// </summary>
        /// <param name="value"></param>
        public void SetLibrary(byte[] value)
        {
            if (AssemblyDll== null)
                AssemblyDll = new FileData { KindValue = 1, StateId = State.STATEACTIVE, FlagsValue = FlagValue.FLAGSYSTEM };
            AssemblyDll.StreamData = value;
        }
        /// <summary>
        /// Загрузить описание библиотеки из файла
        /// </summary>
        /// <param name="filename"></param>
        public void SetLibrary(string filename)
        {
            ClearLib();
            if (AssemblyDll == null)
                AssemblyDll = new FileData { KindValue = 1, StateId = State.STATEACTIVE, FlagsValue = FlagValue.FLAGSYSTEM };
            AssemblyDll.SetStreamFromFile(filename);
            
        }
        ///// <summary>
        ///// Значение библиотеки
        ///// </summary>
        ///// <returns></returns>
        //public byte[] GetLibrary()
        //{
        //    return _assemblyDll;
        //}
        /// <summary>
        /// Очистить библиотеку
        /// </summary>
        public void ClearLib()
        {
            if (AssemblyDll != null)
                AssemblyDll.StreamData = new byte[] {};
        }
        /// <summary>
        /// Установить исходный код библиотеки
        /// </summary>
        /// <param name="value">Значение</param>
        public void SetSource(byte[] value)
        {
            if (AssemblySource == null)
                AssemblySource = new FileData { KindValue = 1, StateId = State.STATEACTIVE, FlagsValue = FlagValue.FLAGSYSTEM };
            AssemblySource.StreamData = value;
        }
        /// <summary>
        /// Установить исходный код библиотеки из файла
        /// </summary>
        /// <param name="filename">Полный путь к файлу</param>
        public void SetSource(string filename)
        {
            if (AssemblySource == null)
                AssemblySource = new FileData { KindValue = 1, StateId = State.STATEACTIVE, FlagsValue = FlagValue.FLAGSYSTEM };
            AssemblySource.SetStreamFromFile(filename);
        }
        public void ExportSourceToFile(string filename)
        {
            if (!IsSourceDataNull)
                System.IO.File.WriteAllBytes(filename, AssemblySource.StreamData);
        }
        bool IsSourceDataNull
        {
            get
            {
                return (AssemblySource == null || AssemblySource.StreamData.All(v => v == 0));
            }
        }
        /// <summary>
        /// Значение исходного кода библиотеки
        /// </summary>
        /// <returns></returns>
        public byte[] GetSource()
        {
            return AssemblySource.StreamData;
        }
        /// <summary>
        /// Очистить данные исходного кода
        /// </summary>
        public void ClearSource()
        {
            if (AssemblySource!=null)
                AssemblySource.StreamData = new byte[] { };
        }
        /// <summary>
        /// Получить сборку данной библиотеки
        /// </summary>
        /// <returns></returns>
        public Assembly GetAssembly()
        {
            Assembly asm = Assembly.Load(AssemblyDll.StreamData);
            AssemblyName[] list = asm.GetReferencedAssemblies();
            return asm;
        }
        ///// <summary>
        ///// Содержимое библиотеки
        ///// </summary>
        ///// <returns></returns>
        //public List<IRepositoryItem> GetContents()
        //{
        //    List<IRepositoryItem> collection = new List<IRepositoryItem>();
        //    System.Reflection.Assembly asm = GetAssembly();
        //    Type[] theTypes = asm.GetTypes();
        //    for (int i = 0; i < theTypes.Length; i++)
        //    {
        //        Type t = theTypes[i].GetInterface("BusinessObjects.IRepositoryItem");

        //        if (t != null)
        //        {
        //            if (!theTypes[i].IsAbstract)
        //            {
        //                object v = asm.CreateInstance(theTypes[i].FullName);

        //                IRepositoryItem pl = (v as IRepositoryItem);
        //                collection.Add(pl);
        //            }
        //        }
        //    }
        //    return collection;
        //}

        



        private List<LibraryContent> _storedContents;
        public List<LibraryContent> StoredContents()
        {
            return _storedContents ?? (_storedContents = GetLibraryContents(this));
        }

        public List<string> GetPrintFormContents()
        {
            Assembly asm = GetAssembly();
            Type[] theTypes = asm.GetTypes();
            return (from t1 in theTypes
                    let t = t1.GetInterface("Stimulsoft.Report.Components.IStiSelect")
                    where t != null
                    where !t1.IsAbstract
                    select t1.FullName).ToList();
        }

        #region ILinks<Library> Members
        /// <summary>
        /// Связи библиотеки
        /// </summary>
        /// <returns></returns>
        public List<IChain<Library>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи библиотеки
        /// </summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Library>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Library> IChains<Library>.SourceList(int chainKindId)
        {
            return Chain<Library>.GetChainSourceList(this, chainKindId);
        }
        List<Library> IChains<Library>.DestinationList(int chainKindId)
        {
            return Chain<Library>.DestinationList(this, chainKindId);
        }
        #endregion

        #region IFacts<Library> Members

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

        #region ICodes
        public List<CodeValue<Library>> GetValues(bool allKinds)
        {
            return CodeHelper<Library>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Library>.GetView(this, true);
        }
        #endregion

        #region IChainsAdvancedList<Library,FileData> Members

        List<IChainAdvanced<Library, FileData>> IChainsAdvancedList<Library, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<Library, FileData>)this).GetLinks(14);
        }

        List<IChainAdvanced<Library, FileData>> IChainsAdvancedList<Library, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<Library, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Library, FileData>(this);
        }
        public List<IChainAdvanced<Library, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<Library, FileData>> collection = new List<IChainAdvanced<Library, FileData>>();
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
                                ChainAdvanced<Library, FileData> item = new ChainAdvanced<Library, FileData> { Workarea = Workarea, Left = this };
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

        #region IChainsAdvancedList<Library,Ruleset> Members

        List<IChainAdvanced<Library, Ruleset>> IChainsAdvancedList<Library, Ruleset>.GetLinks()
        {
            return ((IChainsAdvancedList<Library, Ruleset>)this).GetLinks(17);
        }

        List<IChainAdvanced<Library, Ruleset>> IChainsAdvancedList<Library, Ruleset>.GetLinks(int? kind)
        {
            return GetLinkedRuleset();
        }
        List<ChainValueView> IChainsAdvancedList<Library, Ruleset>.GetChainView()
        {
            return ChainValueView.GetView<Library, Ruleset>(this);
        }
        public List<IChainAdvanced<Library, Ruleset>> GetLinkedRuleset()
        {
            List<IChainAdvanced<Library, Ruleset>> collection = new List<IChainAdvanced<Library, Ruleset>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Library>().Entity.FindMethod("LoadRuleset").FullName;
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
                                ChainAdvanced<Library, Ruleset> item = new ChainAdvanced<Library, Ruleset> { Workarea = Workarea, Left = this };
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

        #region IChainsAdvancedList<Library,XmlStorage> Members

        List<IChainAdvanced<Library, XmlStorage>> IChainsAdvancedList<Library, XmlStorage>.GetLinks()
        {
            return ((IChainsAdvancedList<Library, XmlStorage>)this).GetLinks(24);
        }

        List<IChainAdvanced<Library, XmlStorage>> IChainsAdvancedList<Library, XmlStorage>.GetLinks(int? kind)
        {
            return GetLinkedXmlStorage();
        }
        List<ChainValueView> IChainsAdvancedList<Library, XmlStorage>.GetChainView()
        {
            return ChainValueView.GetView<Library, XmlStorage>(this);
        }
        public List<IChainAdvanced<Library, XmlStorage>> GetLinkedXmlStorage()
        {
            List<IChainAdvanced<Library, XmlStorage>> collection = new List<IChainAdvanced<Library, XmlStorage>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Library>().Entity.FindMethod("LoadXmlStorage").FullName;
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
                                ChainAdvanced<Library, XmlStorage> item = new ChainAdvanced<Library, XmlStorage> { Workarea = Workarea, Left = this };
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

        #region IChainsAdvancedList<Library,Note> Members

        List<IChainAdvanced<Library, Note>> IChainsAdvancedList<Library, Note>.GetLinks()
        {
            return ChainAdvanced<Library, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Library, Note>> IChainsAdvancedList<Library, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Library, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Library, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Library, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Library, Note>.GetChainView()
        {
            return ChainValueView.GetView<Library, Note>(this);
        }
        #endregion

        public List<Library> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Library> filter = null,
            int? assemblyId = null, int? assemblySourceId = null, string nameFull = null, string assemblyVersion = null, int? libraryTypeId = null, string url = null, int? listId = null, int? myCompanyId = null,
            bool useAndFilter = false)
        {
            Library item = new Library { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Library> collection = new List<Library>();
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
                        if (assemblyId.HasValue && assemblyId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.AssemblyId, SqlDbType.Int).Value = assemblyId.Value;
                        if (assemblySourceId.HasValue && assemblySourceId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.AssemblySourceId, SqlDbType.Int).Value = assemblySourceId.Value;
                        if (!string.IsNullOrEmpty(nameFull))
                            cmd.Parameters.Add(GlobalSqlParamNames.NameFull, SqlDbType.NVarChar, 255).Value = nameFull;
                        if (!string.IsNullOrEmpty(assemblyVersion))
                            cmd.Parameters.Add(GlobalSqlParamNames.AssemblyVersion, SqlDbType.NVarChar, 255).Value = assemblyVersion;
                        if (libraryTypeId.HasValue && libraryTypeId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.LibraryTypeId, SqlDbType.Int).Value = libraryTypeId.Value;
                        if (!string.IsNullOrEmpty(url))
                            cmd.Parameters.Add(GlobalSqlParamNames.Url, SqlDbType.NVarChar, 255).Value = url;
                        if (listId.HasValue && listId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.ListId, SqlDbType.Int).Value = listId.Value;
                        if (myCompanyId.HasValue && myCompanyId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = myCompanyId.Value;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Library { Workarea = Workarea };
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
