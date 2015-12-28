using System;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Threading;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace BusinessObjects
{
    /// <summary>Структура используемая для сохранения объекта</summary>
    internal struct BaseStruct
    {
        /// <summary>Признак</summary>
        public string Code;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>Наименование</summary>
        public string Name;
        /// <summary>Дополнительный строковый флаг</summary>
        public string FlagString;
        /// <summary>Идентификатор шаблона</summary>
        public int TemplateId;
        /// <summary>Полная идентификация типа</summary>
        public int KindId;
        /// <summary>Идентификатор подтипа элемента</summary>
        public short KindValue;
        /// <summary>Код поиска</summary>
        public string CodeFind;
        /// <summary>Полное наименование</summary>
        public string NameFull;
    }
    /// <summary>Базовый класс</summary>
    /// <typeparam name="T">Тип</typeparam>
    [DataContract]
    public abstract class BaseCore<T> : BaseCoreObject, ICloneable, IEditableObject, IBase, ICustomPropertySupport, IFindBy<T>, IFlagString,
        ICopyValue<T>
        where T : class, ICoreObject, new()
    {
        
        //// TODO: 
        //public List<T> GetChainSourceList(int chainKindId, int stateId=-1)
        //{
        //    T item;
        //    List<T> collection = new List<T>();
        //    using (SqlConnection cnn = Workarea.GetDatabaseConnection())
        //    {
        //        if (cnn == null) return collection;
        //        try
        //        {
        //            using (SqlCommand cmd = cnn.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = FindProcedure("LoadChainSourceList"); //"[Contractor].[AgentsLoadByChainsId]";//
        //                cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
        //                cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = chainKindId;
        //                if (stateId!=1)
        //                    cmd.Parameters.Add(GlobalSqlParamNames.ChainStateId, SqlDbType.Int).Value = stateId;
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    item = new T { Workarea = Workarea };
        //                    item.Load(reader);
        //                    collection.Add(item);
        //                }
        //                reader.Close();
        //            }
        //        }
        //        finally
        //        {
        //            cnn.Close();
        //        }
        //    }
        //    return collection;
        //}

        //public List<T> DestinationList(int chainKindId, int stateId=-1)
        //{
        //    T item;
        //    List<T> collection = new List<T>();
        //    using (SqlConnection cnn = Workarea.GetDatabaseConnection())
        //    {
        //        if (cnn == null) return collection;
        //        try
        //        {
        //            using (SqlCommand cmd = cnn.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = FindProcedure("LoadChainDestinationList"); //"[Contractor].[AgentsLoadByChainsId]";//
        //                cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
        //                cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = chainKindId;
        //                if (stateId != 1)
        //                    cmd.Parameters.Add(GlobalSqlParamNames.ChainStateId, SqlDbType.Int).Value = stateId;
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    item = new T { Workarea = Workarea };
        //                    item.Load(reader);
        //                    collection.Add(item);
        //                }
        //                reader.Close();
        //            }
        //        }
        //        finally
        //        {
        //            cnn.Close();
        //        }
        //    }
        //    return collection;
        //}
        /// <summary>
        /// Создание копии в базе данных
        /// </summary>
        /// <param name="value">Копируемый объект</param>
        /// <returns></returns>
        public static T CreateCopy(T value) 
        {
            T item = null;
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        cmd.CommandText = value.Entity.FindMethod(GlobalMethodAlias.Copy).FullName; //"Core.CustomViewListCopy";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new T { Workarea = value.Workarea };
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

        /// <summary>
        /// Сравнение объекта для службы обмена данными
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange<T>(T value) where T : IBase, IFlagString
        {
            if(!base.CompareExchange(value))
            {
                return false;
            }
            
            if (value.KindId != KindId)
                return false;
            if (!StringNullCompare(value.Code, Code))
                return false;
            if (!StringNullCompare(value.CodeFind, CodeFind))
                return false;
            if (!StringNullCompare(value.Name, Name))
                return false;
            if (!StringNullCompare(value.NameFull, NameFull))
                return false;
            if (!StringNullCompare(value.Memo, Memo))
                return false;
            if (!StringNullCompare(value.FlagString, FlagString))
                return false;
            if (value.StateId!=StateId)
                return false;
            return true;
        }



        #region ICloneable Members
        /// <summary>Копия объекта</summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {            
            return Clone();
        }
        /// <summary>Копия объекта</summary>
        /// <returns></returns>
        public T Clone() 
        {
            return Clone(true);
        }
        /// <summary>Копия объекта</summary>
        /// <param name="endInit">Завершить инициализацию</param>
        /// <returns></returns>
        protected virtual T Clone(bool endInit)
        {
            T def = Activator.CreateInstance<T>();
            BaseCore<T> cloneObj = def as BaseCore<T>;
            if (cloneObj != null)
            {
                cloneObj.Workarea = Workarea;
                cloneObj.OnBeginInit();
                cloneObj.Id = Id;
                cloneObj.Guid = Guid;
                cloneObj.Name = Name;
                cloneObj.Memo = Memo;
                cloneObj.KindValue = KindValue;
                cloneObj.DatabaseId = DatabaseId;
                cloneObj.Code = Code;
                // ???
                //cloneObj.SetDbEntityId(EntityId);
                cloneObj.FlagsValue = FlagsValue;
                cloneObj.DbSourceId = DbSourceId;
                cloneObj.StateId = StateId;
                cloneObj.KindId = KindId;
                cloneObj.KindValue = KindValue;
                cloneObj.TemplateId = TemplateId;
                cloneObj.NameFull = NameFull;
                cloneObj.CodeFind = CodeFind;
                cloneObj.FlagString = FlagString;
                if(endInit)
                    cloneObj.OnEndInit();
            }
            return def;
        }

        void ICopyValue<T>.CopyValue(T template)
        {
            CopyValue(template);
        }
        /// <summary>
        /// Копирование данных в текущий объект
        /// </summary>
        /// <remarks>Копируются данные о наименовании, полном наименовании, примечании, коде поиска, 
        /// пользовательском флаге.</remarks>
        /// <param name="template">Объект из которого проводится копирование</param>
        protected virtual void CopyValue(T template)
        {
            Name = (template as BaseCore<T>).Name;
            NameFull = (template as BaseCore<T>).NameFull;
            Memo = (template as BaseCore<T>).Memo;
            CodeFind = (template as BaseCore<T>).CodeFind;
            Code = (template as BaseCore<T>).Code;
            FlagString = (template as BaseCore<T>).FlagString;
        }
        #endregion
        #region Конструктор
        /// <summary>Конструктор</summary>
        protected BaseCore()
            : base()
        {

        }
        /// <summary>Конструктор</summary>
        /// <param name="dbEntityId">Идентификатор системного типа</param>
        protected BaseCore(short dbEntityId)
            : this()
        {
            IsStateInit = true;
            EntityId = dbEntityId;
            IsStateInit = false;
        } 
        #endregion

        protected override void OnReadXml(XmlReader reader, string elementName)
        {
            //if (elementName == GlobalPropertyNames.Code)
            //{
            //    _code = reader.ReadString();
            //}
            //else if (elementName == GlobalPropertyNames.FlagString)
            //{
            //    _flagString = reader.ReadString();
            //}
            //else if (elementName == GlobalPropertyNames.KindId)
            //{
            //    _kindId = Convert.ToInt32(reader.ReadString());
            //}
            //else if (elementName == GlobalPropertyNames.KindValue)
            //{
            //    _kindValue = Convert.ToInt16(reader.ReadString());
            //}
            //else if (elementName == GlobalPropertyNames.Memo)
            //{
            //    _memo = reader.ReadString();
            //}
            //else if (elementName == GlobalPropertyNames.Name)
            //{
            //    _name = reader.ReadString();
            //}
            //else if (elementName == GlobalPropertyNames.TemplateId)
            //{
            //    _templateId = Convert.ToInt32(reader.ReadString());
            //}
            base.OnReadXml(reader, elementName);
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            KindId = XmlConvert.ToInt32(reader[GlobalPropertyNames.KindId]);
            KindValue = XmlConvert.ToInt16(reader[GlobalPropertyNames.KindValue]);

            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                Code = reader.GetAttribute(GlobalPropertyNames.Code);

            if (reader.GetAttribute(GlobalPropertyNames.CodeFind) != null)
                CodeFind = reader.GetAttribute(GlobalPropertyNames.CodeFind);

            if (reader.GetAttribute(GlobalPropertyNames.FlagString) != null)
                FlagString = reader.GetAttribute(GlobalPropertyNames.FlagString);

            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                Memo = reader.GetAttribute(GlobalPropertyNames.Memo);

            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                Name = reader.GetAttribute(GlobalPropertyNames.Name);

            if (reader.GetAttribute(GlobalPropertyNames.NameFull) != null)
                NameFull = reader.GetAttribute(GlobalPropertyNames.NameFull);

            if (reader.GetAttribute(GlobalPropertyNames.TemplateId) != null)
                TemplateId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TemplateId));

        }

        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!String.IsNullOrEmpty(Code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, Code);
            if (!String.IsNullOrEmpty(Code))
                writer.WriteAttributeString(GlobalPropertyNames.CodeFind, CodeFind);
            if (!String.IsNullOrEmpty(FlagString))
                writer.WriteAttributeString(GlobalPropertyNames.FlagString, FlagString);
            writer.WriteAttributeString(GlobalPropertyNames.KindId, KindId.ToString());
            writer.WriteAttributeString(GlobalPropertyNames.KindValue, KindValue.ToString());
            if (!String.IsNullOrEmpty(Memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, Memo);
            if (!String.IsNullOrEmpty(Name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, Name);
            if (!String.IsNullOrEmpty(NameFull))
                writer.WriteAttributeString(GlobalPropertyNames.NameFull, NameFull);

            writer.WriteAttributeString(GlobalPropertyNames.TemplateId, TemplateId.ToString());

        }
        #region Свойства

        /*
declare @CommaList varchar(max)
set @CommaList='ABC333'+replicate(cast(',ABC333' as varchar(max)),4999)
select x.i.value('.','varchar(50)')
from (select XMLList=cast('<i>'+replace(@CommaList,',','</i><i>')+'</i>' as xml).query('.')) a
cross apply XMLList.nodes('i') x(i)
         */
        private string _flagString;
        /// <summary>Дополнительный строковый флаг</summary>
        /// <remarks>Данный флаг используется для дополнительной произвольной группировки в списках объектов, в качестве разделителя используется "|".</remarks>
        [DataMember(EmitDefaultValue = false)]
        public string FlagString
        {
            get { return _flagString; }
            set
            {
                if (value == _flagString) return;
                OnPropertyChanging(GlobalPropertyNames.FlagString);
                _flagString = value;
                OnPropertyChanged(GlobalPropertyNames.FlagString);
            }
        }
        
        private int _templateId;
        /// <summary>Идентификатор шаблона</summary>
        /// <remarks>Идентификатор объекта на основе которого создан текущий</remarks>
        [Description("Идентификатор шаблона"),
        DataMember(EmitDefaultValue = false)]
        public int TemplateId
        {
            get 
            { 
                return _templateId; 
            }
            set
            {
                if (_templateId == value) return;
                OnPropertyChanging(GlobalPropertyNames.TemplateId);
                _templateId = value;
                OnPropertyChanged(GlobalPropertyNames.TemplateId);
            }
        }
        /// <summary>
        /// Шаблон объекта
        /// </summary>
        /// <returns></returns>
        public T GetTemplate()
        {
            return Workarea.Cashe.GetCasheData<T>().Item(_templateId);
        }

        private string _memo;
        /// <summary>Примечание к экземпляру объекта</summary>
        [Description("Примечание"),
        DataMember(EmitDefaultValue = false)]
        public string Memo
        {
            get { return _memo; }
            set
            {
                if (_memo == value) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }
        /// <summary>Краткое примечание к экземпляру объекта</summary>
        /// <remarks>Содержит первые 100 символов свойства <see cref="Memo">"Примечание"</see> если примечание более 100 символов 
        /// или само примечание если его длина менее 100 символов. Никогда не возвращает null: вместо null возвращается пустая строка.</remarks>
        public string DisplayMemo
        {
            get
            {
                if (Memo != null && Memo.Length > 100)
                    return Memo.Substring(0, 100) + "...";
                return Memo ?? string.Empty;
            }
        }
        private string _code;
        /// <summary>Код</summary>
        [Description("Код"),
        DataMember(EmitDefaultValue = false)]
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code == value) return;
                OnPropertyChanging(GlobalPropertyNames.Code);
                _code = value;
                OnPropertyChanged(GlobalPropertyNames.Code);
            }
        }
        

        private string _name;
        /// <summary>Наименование</summary>
        [Description("Наименование"),
        DataMember(EmitDefaultValue = false)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
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
        
        
        #endregion
        private short _kindValue;
        /// <summary>Идентификатор подтипа элемента</summary>
        [DataMember(EmitDefaultValue = false)]
        public short KindValue
        {
            get { return _kindValue; }
            set
            {
                if (value == _kindValue) return;
                OnPropertyChanging(GlobalPropertyNames.KindValue);
                _kindValue = value;
                if (!IsStateInit)
                {
                    _kindId = BaseKind.CreateId(EntityId, _kindValue);
                }
                OnPropertyChanged(GlobalPropertyNames.KindValue);
            }
        }
        private int _kindId;
        /// <summary>Подтип</summary>
        /// <remarks>В зависимости от системного типа подтип принимает соответствующие значения. Для корреспондентов: 4 - Сотрудник; 2 - Предприятие; 8 - Моя фирма</remarks>
        [Description("Подтип"),
        DataMember(EmitDefaultValue = false)]
        public virtual int KindId
        {
            get { return _kindId; }
            set
            {
                if (_kindId == value) return;
                OnPropertyChanging(GlobalPropertyNames.KindId);
                _kindId = value;
                _kindValue = EntityKind.ExtractSubKind(_kindId);
                OnPropertyChanged(GlobalPropertyNames.KindId);
                
            }
        }
        #region Состояния
        BaseStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStruct
                                  {
                                      Code = _code,
                                      Memo = _memo,
                                      Name = _name,
                                      FlagString = _flagString,
                                      TemplateId = _templateId,
                                      KindId = _kindId,
                                      KindValue = _kindValue,
                                      CodeFind = _codeFind,
                                      NameFull = _nameFull
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
            Code = _baseStruct.Code;
            Memo = _baseStruct.Memo;
            Name = _baseStruct.Name;
            FlagString = _baseStruct.FlagString;
            TemplateId = _baseStruct.TemplateId;            
            KindId = _baseStruct.KindId;
            KindValue = _baseStruct.KindValue;
            CodeFind = _baseStruct.CodeFind;
            NameFull = _baseStruct.NameFull;
            IsChanged = false;
        } 
        #endregion

        #region IEditableObject Members
        void IEditableObject.BeginEdit()
        {
            SaveState(false);
        }

        void IEditableObject.CancelEdit()
        {
            RestoreState();
        }

        void IEditableObject.EndEdit()
        {
            _baseStruct = new BaseStruct();
        }

        #endregion

        /// <summary>Проверка соответствия объекта бизнес правилам</summary>
        /// <remarks>Метод выполняет проверку наименования объекта <see cref="Name"/> на предмет null, <see cref="string.Empty"/> и максимальную длину не более 255 символов</remarks>
        /// <returns><c>true</c> - если объект соответствует бизнес правилам, <c>false</c> в противном случае</returns>
        /// <exception cref="ValidateException">Если объект не соответствует текущим правилам</exception>
        public override void Validate()
        {
            base.Validate();
            CancelEventArgs e = new CancelEventArgs();
            if (e.Cancel)
                return;
            if (string.IsNullOrEmpty(_name))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_NAMEISEMPTY,1049));
            if (!string.IsNullOrEmpty(_code) && _code.Length > 100)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_NAMELENTH", 1049));
            OnValidated();
        }
        
        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255)
                      {
                          IsNullable = false,
                          Value = _name
                      };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.NameFull, SqlDbType.NVarChar, -1) { IsNullable = true };
            if (string.IsNullOrEmpty(NameFull))
                prm.Value = DBNull.Value;
            else
                prm.Value = NameFull;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.KindId, SqlDbType.Int) {IsNullable = false, Value = _kindId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100) {IsNullable = true};
            if (string.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CodeFind, SqlDbType.NVarChar, 100) { IsNullable = true };
            if (string.IsNullOrEmpty(_codeFind))
                prm.Value = DBNull.Value;
            else
                prm.Value = _codeFind;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar) {IsNullable = true};
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _memo.Length;
                prm.Value = _memo;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.FlagString, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_flagString))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _flagString.Length;
                prm.Value = _flagString;
            }
            sqlCmd.Parameters.Add(prm);

            

            prm = new SqlParameter(GlobalSqlParamNames.TemplateId, SqlDbType.Int) {IsNullable = true};
            if (_templateId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _templateId;
            sqlCmd.Parameters.Add(prm);
        }

        /// <summary>Имя вида</summary>
        [XmlIgnore]
        public string KindName
        {
            get
            {
                EntityKind obj = Workarea.CollectionEntityKinds.FirstOrDefault(f => f.Id == KindId);
                return obj != null ? obj.Name : "Неизвестный тип";
            }
        }
        /// <summary>Сохранение объекта в базе данных</summary>
        /// <remarks>Метод является универсальным - если объект является новым 
        /// <see cref="BaseCoreObject.IsNew"/> - создается в базе данных с использованием метода 
        /// <see cref="Create()"/>, если не является новым - обновляется 
        /// методом <see cref="Update(bool)"/></remarks>
        protected override void Save(bool endSave=true)
        {
            base.Save(false);

            _baseStruct = new BaseStruct();
            if (endSave)
                OnSaved();
        }
        protected override void OnLoadEnd()
        {
            base.OnLoadEnd();
            if (Workarea != null && Id != 0)
            {
                if (Workarea.Cashe.GetCasheData<T>().Exists(Id))
                {
                    Workarea.Cashe.UpdateCasheData<T>(this as T);
                }
            }

        }
        protected override void OnSaved()
        {
            base.OnSaved();

            if (Workarea != null && Id != 0)
            {
                if (Workarea.Cashe.GetCasheData<T>().Exists(Id))
                {
                    Workarea.Cashe.UpdateCasheData<T>(this as T);
                }
            }
        }
        protected override void OnCreated()
        {
            base.OnCreated();
            this.Workarea.OnCreatedObject<T>(this as T);
        }
        public IAsyncResult BeginSave(AsyncCallback callback, object state)
        {
            AsyncResultNoResult ar = new AsyncResultNoResult(callback, state);
            ThreadPool.QueueUserWorkItem(SaveHelper, ar);
            return ar;
        }
        public void EndSave(IAsyncResult asyncResult)
        {
            AsyncResultNoResult ar = (AsyncResultNoResult)asyncResult;
            ar.EndInvoke();
        }
        private void SaveHelper(object asyncResult)
        {
            AsyncResultNoResult ar = (AsyncResultNoResult)asyncResult;
            try
            {
                Save();
                ar.SetAsComplited(null, false);
            }
            catch (Exception e)
            {
                ar.SetAsComplited(e, false);
            }
        }

        void ICoreObject.Load(SqlDataReader reader, bool endInit)
        {
            Load(reader, endInit);
        }
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _name = reader.GetString(9);
                _nameFull = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                _kindId = reader.GetInt32(11);
                _kindValue = BaseKind.ExtractSubKind(_kindId);
                _code = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                _codeFind = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                _memo = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                _flagString = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                _templateId = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                foreach (CustomProperty prop in CustomPropertyValues)
                {
                    prop.Load(reader);
                }
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Представление объекта в виде строки</summary>
        /// <rereturns>Соответствует наименованию объекта <see cref="Name"/></rereturns>
        public override string ToString()
        {
            return _name ?? string.Empty;
        }

        /// <summary>
        /// Представление сущности в виде форматированной строки
        /// </summary>
        /// <remarks>
        /// Допустимая маска : 
        /// <list type="table">
        /// <listheader>
        /// <term>Маска</term>
        /// <description>Значение</description></listheader>
        /// <item>
        /// <term>%id%</term>
        /// <description><see
        /// cref="P:BusinessObjects.BaseCoreObject.Id">Идентификатор</see></description></item>
        /// <item>
        /// <term>%name%</term>
        /// <description><see
        /// cref="P:BusinessObjects.BaseCore`1.Name">Наименование</see></description></item>
        /// <item>
        /// <term>%code%</term>
        /// <description><see
        /// cref="P:BusinessObjects.BaseCore`1.Code">Признак</see></description></item>
        /// <item>
        /// <term>%memo%</term>
        /// <description><see
        /// cref="P:BusinessObjects.BaseCore`1.Memo">Примечание</see></description></item></list>
        /// </remarks>
        /// <param name="mask"></param>
        /// <returns>
        /// Строковое значение соответствующее маске
        /// </returns>
        /// <seealso cref="M:BusinessObjects.BaseCore`1.ToString">Преобразовать в
        /// строку</seealso>
        public override string ToString(string mask)
        {
             string res = base.ToString(mask);
            
            // Макроподстановка для названия
            if (_name != null) res = res.Replace("%name%", _name);
            // Макроподстановка для признака
            if (_code != null) res = res.Replace("%code%", _code);
            // Макроподстановка для примечания
            if (_memo != null) res = res.Replace("%memo%", _memo);
            // Макроподстановка для идентификатора шаблона
            res = res.Replace("%templateid%", _templateId.ToString());
            return res;
        }


        #region ICustomPropertySupport Members

        void ICustomPropertySupport.RegisterProperty(CustomPropertyDescriptor descriptor)
        {
            descriptor.Save();
        }

        void ICustomPropertySupport.UnRegisterProperty(CustomPropertyDescriptor descriptor)
        {
            descriptor.Delete();
        }

        private List<CustomPropertyDescriptor> _collCustomPropertyDesctiptor;
        public List<CustomPropertyDescriptor> PropertyDescriptors
        {
            get {
                return _collCustomPropertyDesctiptor ??
                       (_collCustomPropertyDesctiptor =
                        Workarea.CustomPropertyDescriptors().Where(f => f.DbTypeSource == EntityId).ToList());
            }
        }

        private List<CustomProperty> _customPropertyValues;
        protected List<CustomProperty> CustomPropertyValues
        {
            get
            {
                if (_customPropertyValues == null)
                    _customPropertyValues = new List<CustomProperty>();
                foreach (CustomPropertyDescriptor item in PropertyDescriptors)
                {
                    CustomProperty v = new CustomProperty {Descriptor = item};
                }
                return _customPropertyValues;
            }
        }

        #endregion

        /// <summary>
        /// Поиск объектов
        /// </summary>
        /// <param name="hierarchyId">Идентификатор иеархии в которой необъодимо вести поиск</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="flags">Флаг</param>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <param name="name">Наименование</param>
        /// <param name="kindId">Подтип</param>
        /// <param name="code">Код</param>
        /// <param name="memo">Примечание</param>
        /// <param name="flagString">Пользовательский флаг</param>
        /// <param name="templateId">Идентификатор шаблона</param>
        /// <param name="count">Количество записей, по умолчанию 100</param>
        /// <param name="filter">Дополнительный фильтр</param>
        /// <param name="useAndFilter">Проверка на И</param>
        /// <returns>Коллекция объектов удовлтворяющая условим поиска</returns>
        public List<T> FindBy(int? hierarchyId=null, string userName=null, int? flags=null, 
            int? stateId=null, string name=null, int kindId= 0, string code = null, 
            string memo=null, string flagString=null, int templateId=0,
            int count = 100, Predicate<T> filter = null)
        {
            T item = new T { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<T> collection = new List<T>();
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
                        if (hierarchyId != null && hierarchyId.Value!=0)
                            cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;
                        if (userName != null && !string.IsNullOrEmpty(userName))
                            cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar,128).Value = userName;
                        if (flags.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.Flags, SqlDbType.Int).Value = flags;
                        if (stateId.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateId;
                        if (!string.IsNullOrWhiteSpace(name))
                            cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = name;
                        if (kindId!=0)
                            cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindId;
                        if (!string.IsNullOrWhiteSpace(code))
                            cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;
                        if (!string.IsNullOrWhiteSpace(memo))
                            cmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255).Value = memo;
                        if (!string.IsNullOrWhiteSpace(flagString))
                            cmd.Parameters.Add(GlobalSqlParamNames.FlagString, SqlDbType.NVarChar, 50).Value = flagString;
                        if (templateId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TemplateId, SqlDbType.Int).Value = templateId;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new T { Workarea = Workarea };
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
        /// <summary>
        /// Строка всех пользовательских кодов для объекта
        /// </summary>
        /// <returns></returns>
        public string GetFlagStringAll()
        {
            string res = string.Empty;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return res;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindProcedure(GlobalMethodAlias.GetFlagStringAll);
                        object val = cmd.ExecuteScalar();
                        if (val != null)
                            res = val.ToString();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }

            return res;
        }

        
    }
}
