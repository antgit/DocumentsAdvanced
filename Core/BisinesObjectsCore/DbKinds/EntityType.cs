using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Xml;
using BusinessObjects.Documents;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс системного объекта
    /// </summary>
    public interface IEntityType
    {
        /// <summary>
        /// Примечание
        /// </summary>
        string Memo { get; set; }

        /// <summary>Наименование</summary>
        string Name { get; set; }

        /// <summary>Код</summary>
        /// <remarks>Данное свойство содержит полное имя типа, например, 
        /// <c>BusinessObjects.Product, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null</c>, 
        /// и предназначено для динамического создания объекта.</remarks>
        string Code { get; set; }

        /// <summary>Коллекция методов</summary>
        /// <remarks>Коллекция является 
        /// кешированной <see cref="EntityType.RefreshMethods()"/>.
        /// </remarks>
        /// <seealso cref="ProcedureMap"/>
        List<ProcedureMap> Methods { get; }

        /// <summary>Поиск метода по наименованию</summary>
        /// <param name="methodAliasName">Наименование метода</param>
        /// <returns></returns>
        /// <exception cref="BusinessObjects.MethodFindException">Исключение происходит если требуемый метод не найден.</exception>
        ProcedureMap FindMethod(string methodAliasName);
    }

    /// <summary>Системный объект</summary>
    /// <remarks>
    /// Системные объекты используются для динамического расширения системы зарегестрированных типов. 
    /// Система имеет набор зарегестрированных системных объектов:
    /// <list type="table">
    /// <listheader><term>Значение <see cref="EntityType.Id"/>Id</term><description>Тип объекта</description>
    /// </listheader>
    /// <item><term>1</term><description cref="BusinessObjects.Product"></description></item>
    /// <item><term>2</term><description cref="BusinessObjects.Account"></description></item>
    /// <item><term>3</term><description cref="BusinessObjects.Agent"></description></item>
    /// <item><term>4</term><description cref="BusinessObjects.Analitic"></description></item>
    /// <item><term>5</term><description cref="BusinessObjects.Currency"></description></item>
    /// <item><term>7</term><description cref="BusinessObjects.Folder"></description></item>
    /// <item><term>9</term><description cref="BusinessObjects.PriceName"></description></item>
    /// <item><term>10</term><description cref="BusinessObjects.Unit"></description></item>
    /// <item><term>11</term><description cref="BusinessObjects.Branche"></description></item>
    /// <item><term>12</term><description cref="BusinessObjects.ProductRecipe"></description></item>
    /// <item><term>13</term><description cref="BusinessObjects.Rate"></description></item>
    /// <item><term>14</term><description cref="BusinessObjects.EntityDocument"></description></item>
    /// <item><term>15</term><description cref="BusinessObjects.Library"></description></item>
    /// <item><term>16</term><description cref="BusinessObjects.PriceValue"></description></item>
    /// <item><term>17</term><description cref="BusinessObjects.FactName"></description></item>
    /// <item><term>18</term><description cref="BusinessObjects.FactColumn"></description></item>
    /// <item><term>19</term><description>Системные объекты</description></item>
    /// <item><term>20</term><description cref="Document"></description></item>
    /// <item><term>21</term><description cref="BusinessObjects.CustomViewList"></description></item>
    /// <item><term>22</term><description cref="BusinessObjects.CustomViewColumn"></description></item>
    /// <item><term>24</term><description cref="BusinessObjects.AgentBankAccount"></description></item>
    /// <item><term>25</term><description cref="BusinessObjects.SystemParameter"></description></item>
    /// <item><term>26</term><description cref="Uid"></description></item>
    /// <item><term>27</term><description cref="BusinessObjects.Security.Right"></description></item>
    /// <item><term>28</term><description cref="BusinessObjects.Hierarchy"></description></item>
    /// <item><term>29</term><description cref="BusinessObjects.HierarchyContent"></description></item>
    /// </list>
    /// </remarks>
    public sealed class EntityType : BaseCoreObject, IBase, IEquatable<EntityType>, IFacts<EntityType>, IEntityType
    {
        bool IEquatable<EntityType>.Equals(EntityType other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }

        /// <summary>Конструктор</summary>
        public EntityType(): base()
        {
        }
        #region Свойства
        EntityType ICoreObject.Entity
        {
            get
            {
                return Workarea.CollectionEntities.Find(s => s.Id == 19);
                //return this;
            }
        }
        protected override EntityType OnRequestEntity()
        {
            return Workarea.CollectionEntities.Find(s => s.Id == 19);
        }
        int IBase.KindId
        {
            get { return 0; }
            set{}
        }
        int IBase.TemplateId
        {
            get { return 0; }
            set { }
        }
        private string _memo;
        /// <summary>
        /// Примечание
        /// </summary>
        public string Memo
        {
            get{ return _memo;} 
            set
            {
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }
        private string _name;
        /// <summary>Наименование</summary>
        public string Name
        {
            get { return _name; }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.Name);
                _name = value;
                OnPropertyChanged(GlobalPropertyNames.Name);
            }
        }
        private string _nameFull;
        /// <summary>
        /// Полное наименование
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string NameFull
        {
            get { return _nameFull; }
            set
            {
                if (value == _nameFull) return;
                OnPropertyChanging(GlobalPropertyNames.NameFull);
                _nameFull = value;
                OnPropertyChanged(GlobalPropertyNames.NameFull);
            }
        }
        private string _code;
        /// <summary>Код</summary>
        /// <remarks>Данное свойство содержит полное имя типа, например, 
        /// <c>BusinessObjects.Product, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null</c>, 
        /// и предназначено для динамического создания объекта.</remarks>
        public string Code
        {
            get { return _code; }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.Code);
                _code = value;
                OnPropertyChanged(GlobalPropertyNames.Code);
            }
        }
        private string _codeFind;
        /// <summary>
        /// Код поиска
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string CodeFind
        {
            get { return _codeFind; }
            set
            {
                if (value == _codeFind) return;
                OnPropertyChanging(GlobalPropertyNames.CodeFind);
                _codeFind = value;
                OnPropertyChanged(GlobalPropertyNames.CodeFind);
            }
        }
        private short _maxKindValue;
        /// <summary>Максимальный элементарный вид</summary>
        /// <remarks>Максимальный тип является ясловным значением, которое определяет наибольший 
        /// "элементарный" вид объекта в виде числа <see cref="EntityKind.SubKind"/>.</remarks>
        public short MaxKind
        {
            get { return _maxKindValue; }
            set
            {
                if (_maxKindValue == value) return;
                OnPropertyChanging(GlobalPropertyNames.MaxKind);
                _maxKindValue = value;
                OnPropertyChanged(GlobalPropertyNames.MaxKind);
            }
        }

        private string _nameSchema;
        /// <summary>
        /// Схема данных
        /// </summary>
        public string NameSchema
        {
            get { return _nameSchema; }
            set
            {
                if (value == _nameSchema) return;
                OnPropertyChanging(GlobalPropertyNames.NameSchema);
                _nameSchema = value;
                OnPropertyChanged(GlobalPropertyNames.NameSchema);
            }
        }


        private string _codeClass;
        /// <summary>
        /// Код класса
        /// </summary>
        public string CodeClass
        {
            get { return _codeClass; }
            set
            {
                if (value == _codeClass) return;
                OnPropertyChanging(GlobalPropertyNames.CodeClass);
                _codeClass = value;
                OnPropertyChanged(GlobalPropertyNames.CodeClass);
            }
        }
        
        #endregion
        
        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, _code);
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                _code = reader.GetAttribute(GlobalPropertyNames.Code);
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
        }
        #endregion

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        /// <exception cref="ValidateException">Исключение возникает при значении свойства <see cref="EntityKind.Name"/> равным <c>null</c> 
        /// или длина значения данного свойства соответствует <see cref="String.Empty"/> или длинна данного свойства превышает 255 символов.</exception>
        public override void Validate()
        {
            // TODO: Дополнительный механизм проверки объекта на основе делегата...
            base.Validate();
            if (string.IsNullOrEmpty(_name))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_NAMEISEMPTY, 1049));
            if (!string.IsNullOrEmpty(_code) && _code.Length > 255)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_NAMELENTH", 1049));
            StateId = 1;
            if (FlagsValue==0)
                FlagsValue = 8;
        }
        /// <summary>
        /// Проверка наличия маппинга хранимых процедур
        /// </summary>
        /// <remarks>
        /// Проверяется наличие хранимых процедур в маппинге для следующих процедур:
        /// <list type="bullet">
        /// <item>
        /// <description>Создание</description></item>
        /// <item>
        /// <description>Обновление</description></item>
        /// <item>
        /// <description>Удаление</description></item>
        /// <item>
        /// <description>Проверка по идентификатору</description></item>
        /// <item>
        /// <description>Проверка по глобальному идентификатору</description></item>
        /// <item>
        /// <description>Загрузка по идентификатору</description></item></list>
        /// </remarks>
        /// <returns>
        /// Возвращаемый результат соотвествует таблице: Ключ-Значение, в качестве значений
        /// используется буква &quot;Y&quot; или &quot;N&quot;
        /// </returns>
        public Dictionary<string,string> CheckMapping()
        {
            Dictionary<string, string> res = new Dictionary<string, string>
                                                 {
                                                     {GlobalMethodAlias.Create, "N"},
                                                     {GlobalMethodAlias.Update, "N"},
                                                     {GlobalMethodAlias.Delete, "N"},
                                                     {GlobalMethodAlias.Load, "N"},
                                                     {GlobalMethodAlias.ExistId, "N"},
                                                     {GlobalMethodAlias.FindIdByGuid, "N"}
                                                 };

            foreach (string v in res.Keys)
            {
                try
                {
                    FindMethod(v);
                    res[v] = "Y";
                }
                catch (MethodFindException ex)
                {
                }   
            }
            return res;    
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            if(Workarea!=null)
            {
                if (!Workarea.CollectionEntities.Contains(this))
                    Workarea.CollectionEntities.Add(this);
            }
        }
        
        #region База данных

        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            OnBeginInit();
            try
            {
                _name = reader.GetString(9);
                _code = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                _maxKindValue = reader.GetInt16(11);
                _memo = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                _nameSchema = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                _codeClass = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
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
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255)
                      {
                          IsNullable = false,
                          Value = _name
                      };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 255) {IsNullable = true};
            if (string.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MaxKind, SqlDbType.SmallInt) {IsNullable = false, Value = _maxKindValue};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _memo.Length;
                prm.Value = _memo;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.NameSchema, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_nameSchema))
                prm.Value = DBNull.Value;
            else
                prm.Value = _nameSchema;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CodeClass, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_codeClass))
                prm.Value = DBNull.Value;
            else
                prm.Value = _codeClass;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        private List<ProcedureMap> _methods;
        /// <summary>Коллекция методов</summary>
        /// <remarks>Коллекция является 
        /// кешированной <see cref="EntityType.RefreshMethods()"/>.
        /// </remarks>
        /// <seealso cref="ProcedureMap"/>
        public List<ProcedureMap> Methods
        {
            get 
            {
                if (_methods == null)
                    RefreshMethods();
                return _methods; 
            }
        }
        /// <summary>Коллекция методов</summary>
        /// <remarks>Только для внутреннего использования, используйте свойство <see cref="EntityType.Methods"/></remarks>
        /// <returns></returns>
        public void RefreshMethods()
        {
            
                _methods = new List<ProcedureMap>();
                lock (_methods)
                {
                    using (SqlConnection cnn = Workarea.GetDatabaseConnection())
                    {
                        if (cnn == null) return;
                        try
                        {
                            using (SqlCommand cmd = cnn.CreateCommand())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                                cmd.CommandText = Workarea.FindMethod("Core.EntityMethodsByDbEntity").FullName;
                                SqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    ProcedureMap item = new ProcedureMap {Workarea = Workarea};
                                    item.Load(reader);
                                    _methods.Add(item);
                                }
                                reader.Close();
                            }
                        }
                        finally
                        {
                            cnn.Close();
                        }
                    }
                }
            return;
        }

        /// <summary>Поиск метода по наименованию</summary>
        /// <param name="methodAliasName">Наименование метода</param>
        /// <returns></returns>
        /// <exception cref="BusinessObjects.MethodFindException">Исключение происходит если требуемый метод не найден.</exception>
        public ProcedureMap FindMethod(string methodAliasName)
        {
            try
            {
                return Methods.First(m => m.Method == methodAliasName);
            }
            catch (Exception ex)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}", methodAliasName), ex);
            }
        }

        private List<EntityKind> _entityKinds;
        /// <summary>Коллекция видов объекта</summary>
        /// <remarks>Коллекция является кешированной, <see cref="RefreshCollectionEntityKind"/></remarks>
        public List<EntityKind> EntityKinds 
        { 
            get { return _entityKinds ?? (_entityKinds = RefreshCollectionEntityKind()); }
        }
        /// <summary>Коллекция видов объекта</summary>
        /// <remarks>Только для внутреннего использования, для доступа к коллекции видов 
        /// используйте свойство <see cref="EntityKinds"/></remarks>
        /// <returns></returns>
        private List<EntityKind> RefreshCollectionEntityKind()
        {
            List<EntityKind> collection = new List<EntityKind>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.FindMethod("Core.EntityKindsLoadAll").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            EntityKind item = new EntityKind {Workarea = Workarea};
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
        /// <summary>
        /// Допустимы ли значения подтипов в виде флагов
        /// </summary>
        public bool IsSubKindAsFlag
        {
            get{ return (FlagsValue & 16) == 16; }
        }

        private List<FlagValue> _flagValueCollection;
        /// <summary>Коллекия флагов системного типа</summary>
        public List<FlagValue> FlagValues
        {
            get
            {
                if (_flagValueCollection == null)
                {
                    RefreshCollectionFlagValue();
                }
                return _flagValueCollection;
            }
        }


        /// <summary>Коллекция флагов для системного типа</summary>
        /// <remarks>Только для внутреннего использования, для доступа к коллекции флагов 
        /// используете свойство <see cref="EntityType.FlagValues"/></remarks>
        /// <returns></returns>
        private void RefreshCollectionFlagValue()
        {
            _flagValueCollection = new List<FlagValue>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.FindMethod("Core.FlagValuesLoadByDbEntity").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = (short)Id;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            FlagValue item = new FlagValue {Workarea = Workarea};
                            item.Load(reader);
                            _flagValueCollection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return;
        }

        #region IBase Members

        short IBase.KindValue
        {
            get { return (Int16)Id; }
            set { }
        }

        string IBase.Name
        {
            get { return Name; }
            set { Name = value; }
        }

        string IBase.Code
        {
            get { return Code; }
            set { Code = value; }
        }

        short ICoreObject.EntityId
        {
            get { return 19; }
        }

        #endregion
        /// <summary>Преобразовать в строку</summary>
        /// <returns>Возвращает наименование</returns>
        public override string ToString()
        {
            return _name;
        }

        #region IFacts<EntityType> Members

        private List<FactView> _factView;
        public List<FactView> GetCollectionFactView()
        {
            return _factView ?? (_factView = FactHelper.GetCollectionFactView(Workarea, Id, 19));
        }

        public void RefreshFaсtView()
        {
            _factView = FactHelper.GetCollectionFactView(Workarea, Id, 19);
        }

        public FactView GetFactViewValue(string factCode, string columnCode)
        {
            return GetCollectionFactView().FirstOrDefault(s => s.FactNameCode == factCode && s.ColumnCode == columnCode);
        }
        public List<FactName> GetFactNames()
        {
            return FactHelper.GetFactNames(Workarea, 19);
        }
        #endregion
    } 
}
