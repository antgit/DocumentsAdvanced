using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct UnitStruct
    {
        /// <summary>Международный код</summary>
        public string CodeInternational;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }

    /// <summary>
    /// Единица измерения
    /// </summary>
    /// <remarks>Свойство Code соотверствует сокращенному наименованию.</remarks>
    public sealed class Unit : BaseCore<Unit>, IChains<Unit>, IEquatable<Unit>,
        ICodes<Unit>,
        IChainsAdvancedList<Unit, Note>,
        IHierarchySupport, ICompanyOwner
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Единица измерения, соответствует значению 1</summary>
        public const int KINDVALUE_UNIT = 1;

        /// <summary>Единица измерения, соответствует значению 655361</summary>
        public const int KINDID_UNIT = 655361;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Unit>.Equals(Unit other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public Unit():base()
        {
            EntityId = (short)WhellKnownDbEntity.Unit;
        }
        protected override void CopyValue(Unit template)
        {
            base.CopyValue(template);
            CodeInternational = template.CodeInternational;
            MyCompanyId = template.MyCompanyId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Unit Clone(bool endInit)
        {
            Unit obj = base.Clone(false);
            obj.CodeInternational = CodeInternational;
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства
        private string _codeInternational;
        /// <summary>
        /// Международный код
        /// </summary>
        public string CodeInternational
        {
            get { return _codeInternational; }
            set
            {
                if (value == _codeInternational) return;
                OnPropertyChanging(GlobalPropertyNames.CodeInternational);
                _codeInternational = value;
                OnPropertyChanged(GlobalPropertyNames.CodeInternational);
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

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_codeInternational))
                writer.WriteAttributeString(GlobalPropertyNames.CodeInternational, _codeInternational);
            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.CodeInternational) != null) _codeInternational = reader[GlobalPropertyNames.CodeInternational];
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
        }
        #endregion

        #region Состояние
        UnitStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new UnitStruct
                {
                    CodeInternational = _codeInternational,
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
            CodeInternational = _baseStruct.CodeInternational;
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
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _codeInternational = reader.IsDBNull(17) ? null : reader.GetString(17);
                _myCompanyId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.CodeInternational, SqlDbType.NVarChar, 50) { IsNullable = true };
            if (string.IsNullOrEmpty(_codeInternational))
                prm.Value = DBNull.Value;
            else
                prm.Value = _codeInternational;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Unit> Members
        /// <summary>
        /// Связи единицы измерения
        /// </summary>
        /// <returns></returns>
        public List<IChain<Unit>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>
        /// Связи единицы измерения
        /// </summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Unit>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Unit> IChains<Unit>.SourceList(int chainKindId)
        {
            return Chain<Unit>.GetChainSourceList(this, chainKindId);
        }
        List<Unit> IChains<Unit>.DestinationList(int chainKindId)
        {
            return Chain<Unit>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Unit>> GetValues(bool allKinds)
        {
            return CodeHelper<Unit>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Unit>.GetView(this, true);
        }
        #endregion
        #region IChainsAdvancedList<Unit,Note> Members

        List<IChainAdvanced<Unit, Note>> IChainsAdvancedList<Unit, Note>.GetLinks()
        {
            return ChainAdvanced<Unit, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Unit, Note>> IChainsAdvancedList<Unit, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Unit, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Unit, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Unit, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Unit, Note>.GetChainView()
        {
            return ChainValueView.GetView<Unit, Note>(this);
        }
        #endregion

        /// <summary>
        /// Коллекция товаров в которой используется данная единица измерения
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            Product item = new Product { Workarea = Workarea };

            List<Product> collection = new List<Product>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = string.Empty;
                        ProcedureMap procedureMap = item.Entity.FindMethod("LoadListByUnitId");
                        if (procedureMap != null)
                        {
                                procedureName = procedureMap.FullName;
                        }
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Product { Workarea = Workarea };
                            item.Load(reader);
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

        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<Unit>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }

        public List<Unit> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Unit> filter = null,
            string codeInternational=null,
            bool useAndFilter = false)
        {
            Unit item = new Unit { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Unit> collection = new List<Unit>();
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
                        if (!string.IsNullOrEmpty(codeInternational))
                            cmd.Parameters.Add(GlobalSqlParamNames.CodeInternational, SqlDbType.NVarChar, 50).Value = codeInternational;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Unit { Workarea = Workarea };
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
