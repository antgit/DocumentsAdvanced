using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Xml;
using BusinessObjects.Documents;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>Структура используемая для сохранения объекта</summary>
    internal struct ChainDocumentStruct
    {
        /// <summary>Дата</summary>
        public DateTime? DateValue;
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Summ;
    }
    /// <summary>Цепочка</summary>
    /// <typeparam name="T">Тип</typeparam>
    public class Chain<T> : BaseCoreObject, IComparable, IChain<T> where T : class, IBase, new()
    {
        /// <summary>Конструктор</summary>
        public Chain():base()
        {
            EntityId = (short) WhellKnownDbEntity.Chain;
        }
        /// <summary>
        /// Сравнение выполняется по идентификатору
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Chain<T> otherPerson = (Chain<T>)obj;
            return Id.CompareTo(otherPerson.Id);
        }
        /// <summary>Конструктор</summary>
        /// <param name="left">Источник</param>
        public Chain(T left): this()
        {
            _left = left;
            _leftId = left.Id;
            Workarea = left.Workarea;
        }
        private T _left;
        /// <summary>Источник, объект слева</summary>
        public T Left
        {
            get
            {
                if (_leftId == 0)
                    return default(T);
                if (_left == null)
                    _left = Workarea.Cashe.GetCasheData<T>().Item(_leftId);
                else if (_left.Id != _leftId)
                    _left = Workarea.Cashe.GetCasheData<T>().Item(_leftId);
                return _left;
            }
            set
            {
                _left = value;
                _leftId = _left != null ? _left.Id : 0;
            }
        }

        private T _right;
        /// <summary>Назначение, объект справа</summary>
        public T Right
        {
            get 
            {
                if (_rightId == 0)
                    return default (T);
                if (_right == null)
                    _right = Workarea.Cashe.GetCasheData<T>().Item(_rightId);
                else if (_right.Id != _rightId)
                    _right = Workarea.Cashe.GetCasheData<T>().Item(_rightId);
                return _right; 
            }
            set
            {
                _right = value;
                _rightId = _right != null ? _right.Id : 0;
            }
        }

        private int _userOwnerId;
        /// <summary>
        /// Идентификатор пользователя владельца
        /// </summary>
        public int UserOwnerId
        {
            get { return _userOwnerId; }
            set
            {
                if (value == _userOwnerId) return;
                OnPropertyChanging(GlobalPropertyNames.UserOwnerId);
                _userOwnerId = value;
                OnPropertyChanged(GlobalPropertyNames.UserOwnerId);
            }
        }

        private Uid _userOwner;
        /// <summary>
        /// Пользователь-владелец
        /// </summary>
        public Uid UserOwner
        {
            get
            {
                if (_userOwnerId == 0)
                    return null;
                if (_userOwner == null)
                    _userOwner = Workarea.Cashe.GetCasheData<Uid>().Item(_userOwnerId);
                else if (_userOwner.Id != _userOwnerId)
                    _userOwner = Workarea.Cashe.GetCasheData<Uid>().Item(_userOwnerId);
                return _userOwner;
            }
            set
            {
                if (_userOwner == value) return;
                OnPropertyChanging(GlobalPropertyNames.UserOwner);
                _userOwner = value;
                _userOwnerId = _userOwner == null ? 0 : _userOwner.Id;
                OnPropertyChanged(GlobalPropertyNames.UserOwner);
            }
        }


        /// <summary>
        /// Имя пользователя-владельца
        /// </summary>
        public string UserOwnerName
        {
            get
            {
                return UserOwner == null ? string.Empty : UserOwner.Name;
            }

        }
        /// <summary>
        /// Наименоване сотрудника-владельца
        /// </summary>
        public string WorkerName
        {
            get
            {
                if (UserOwner != null && UserOwner.AgentId != 0)
                    return UserOwner.Agent.Name;
                return string.Empty;
            }

        }

        #region ILink<T> Members
        private int _leftId;
        /// <summary>Идентификатор источника</summary>
        public int LeftId
        {
            get { return _leftId; }
            set
            {
                if (value == _leftId) return;
                OnPropertyChanging(GlobalPropertyNames.LeftId);
                _leftId = value;
                OnPropertyChanged(GlobalPropertyNames.LeftId);
            }
        }
        private int _rightId;
        /// <summary>Идентификатор назначения</summary>
        public int RightId
        {
            get { return _rightId; }
            set
            {
                if (value == _rightId) return;
                OnPropertyChanging(GlobalPropertyNames.RightId);
                _rightId = value;
                OnPropertyChanged(GlobalPropertyNames.RightId);
            }
        }
	
        private int _kindId;
        /// <summary>Идентификатор вида цепочки</summary>
        public int KindId
        {
            get { return _kindId; }
            set
            {
                if (value == _kindId) return;
                OnPropertyChanging(GlobalPropertyNames.KindValue);
                _kindId = value;
                OnPropertyChanged(GlobalPropertyNames.KindValue);
            }
        }


        private string _code;
        /// <summary>Код</summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (value == _code) return;
                OnPropertyChanging(GlobalPropertyNames.Code);
                _code = value;
                OnPropertyChanged(GlobalPropertyNames.Code);
            }
        }
        
        private ChainKind _kind;
        /// <summary>Тип связи</summary>
        public ChainKind Kind
        {
            get
            {
                if (_kind == null)
                    _kind = Workarea.CollectionChainKinds.FirstOrDefault(f => f.Id == _kindId);
                else if (_kind.Id != _kindId)
                    _kind = ((ICoreObject) this).Workarea.CollectionChainKinds.FirstOrDefault(f => f.Id == _kindId);
                return _kind;
            }
        }

        private int _orderNo;
        /// <summary>Глобальный номер в списке</summary>
        public int OrderNo
        {
            get { return _orderNo; }
            set
            {
                if (value == _orderNo) return;
                OnPropertyChanging(GlobalPropertyNames.OrderNo);
                _orderNo = value;
                OnPropertyChanged(GlobalPropertyNames.OrderNo);
            }
        }
        private string _memo;
        /// <summary>
        /// Примечание
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set
            {
                if (value == _memo) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }
        #endregion

        #region Состояние
        ChainAdvancedStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new ChainAdvancedStruct
                {
                    Code=_code,
                    KindId=_kindId,
                    LeftId=_leftId,
                    OrderNo=_orderNo,
                    RightId=_rightId,
                    Memo = _memo,
                    UserOwnerId = _userOwnerId
                };
                return true;
            }
            return false;
        }
        /// <summary>
        /// Востановить исходное состояние
        /// </summary>
        public override void RestoreState()
        {
            base.RestoreState();
            Code = _baseStruct.Code;
            KindId = _baseStruct.KindId;
            LeftId = _baseStruct.LeftId;
            OrderNo = _baseStruct.OrderNo;
            RightId = _baseStruct.RightId;
            Memo = _baseStruct.Memo;
            UserOwnerId = _baseStruct.UserOwnerId;
            IsChanged = false;
        }
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, _code);
            if (_kindId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.KindId, XmlConvert.ToString(_kindId));
            if (_leftId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.LeftId, XmlConvert.ToString(_leftId));
            if (_orderNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_orderNo));
            if (_rightId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.RightId, XmlConvert.ToString(_rightId));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                _code = reader.GetAttribute(GlobalPropertyNames.Code);
            if (reader.GetAttribute(GlobalPropertyNames.KindId) != null)
                _kindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.KindId));
            if (reader.GetAttribute(GlobalPropertyNames.LeftId) != null)
                _leftId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.LeftId));
            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _orderNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OrderNo));
            if (reader.GetAttribute(GlobalPropertyNames.RightId) != null)
                _rightId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RightId));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
        }
        #endregion

        #region Методы
        /// <summary>
        /// Изменить состояние объекта
        /// </summary>
        /// <param name="stateValue">Идентификатор нового состояния</param>
        public override void ChangeState(int stateValue)
        {
            if (stateValue < 0 && stateValue > 4)
                throw new ArgumentOutOfRangeException("stateValue", "Новое состояние объекта не может быть меньше 0 или больше 4");
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {

                        string methotAlias = typeof (T).Name;
                        string procedureName = Left.Entity.FindMethod(methotAlias + "ChainChangeState").FullName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateValue;
                        cmd.ExecuteNonQuery();
                        StateId = stateValue;
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Завершить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _leftId = reader.GetInt32(9);
                _rightId = reader.GetInt32(10);
                _kindId = reader.GetInt32(11);
                _orderNo = reader.GetInt32(12);
                _code = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                _memo = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                _userOwnerId = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>
        /// Создание объекта в базе данных
        /// </summary>
        protected override void Create()
        {
            CancelEventArgs e = new CancelEventArgs();
            OnCreating(e);
            if (e.Cancel)
                return;
            Create(Left.Entity.FindMethod("ChainInsert").FullName);
            OnCreated();
        }
        /// <summary>
        /// Обновление обюъекта в базе данных
        /// </summary>
        /// <param name="versionControl"></param>
        protected override void Update(bool versionControl = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnUpdating(e);
            if (e.Cancel)
                return;
            Update(Left.Entity.FindMethod("ChainUpdate").FullName, versionControl);
            OnUpdated();
        }
        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.LeftId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _leftId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.RightId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _rightId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _kindId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.OrderNo, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _orderNo;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, -1);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Value = _memo;
            }

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.UserOwnerId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            if (_userOwnerId == 0)
                prm.Value = Workarea.CurrentUser.Id;
            else
                prm.Value = _userOwnerId;
        }
        /// <summary>Удаление объекта из базы данных</summary>
        public override void Delete(bool checkVersion = true)
        {
            Delete(Left.Entity.FindMethod("ChainDelete").FullName, checkVersion);
        }
        /// <summary>Загрузить данные объекта из базы данных по идентификатору</summary>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnLoading(e);
            if (e.Cancel)
                return;
            Load(value, Left.Entity.FindMethod("ChainLoad").FullName);
        }
        /// <summary>Коллекция цепоцек назначения</summary>
        /// <param name="kind">Числовое представление</param>
        /// <returns></returns>
        public virtual List<IChain<T>> CollectionDestinations(int? kind)
        {
            List<IChain<T>> collection = new List<IChain<T>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Left.Entity.FindMethod("ChainLoadDestinations").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.RightId, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = Id;
                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    if(kind.HasValue)
                        prm.Value = kind.Value;
                    else
                        prm.Value = DBNull.Value;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
       
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Chain<T> item = new Chain<T> {Workarea = Workarea};
                                item.Load(reader);
                                collection.Add(item);
                            }
                            reader.Close();
                        }
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
        /// <summary>Коллекция цепоцек</summary>
        /// <param name="kind">Числовое представление</param>
        /// <returns></returns>
        public virtual List<IChain<T>> CollectionSource(int? kind)
        {
            List<IChain<T>> collection = new List<IChain<T>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Left.Entity.FindMethod("ChainLoadSources").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.RightId, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = Id;
                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    if (kind.HasValue)
                        prm.Value = kind.Value;
                    else
                        prm.Value = DBNull.Value;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Chain<T> item = new Chain<T> { Workarea = Workarea };
                                item.Load(reader);
                                collection.Add(item);
                            }
                            reader.Close();
                        }
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
        protected override void OnSaved()
        {
            base.OnSaved();
            if (Left.Workarea.Cashe.GetChainCasheData<T>().Exists(Left.Id, KindId) && StateId==State.STATEACTIVE )
            {
                Left.Workarea.Cashe.GetChainCasheData<T>().AddElement(Left.Id, KindId, Right);
            }
        }
        /// <summary>
        /// Коллекция объектов назначения (справа)
        /// </summary>
        /// <remarks>
        /// При получении данных используется глобальный кеш данных рабочей области - если объект был ранее загружен он обновляется, 
        /// в противном случае добавляется в глобальный кеш.
        /// </remarks>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="left">Значение объекта слева</param>
        /// <param name="chainKindId">Идентификатор типа связи</param>
        /// <param name="stateId">Состояние, по умолчанию (-1) - все связи, допустимые значения в соотвествии с возможными состояниями</param>
        /// <param name="refresh">Обновлять данные о коллекции из базы данных</param>
        /// <returns></returns>
        public static List<T> GetChainSourceList<T>(T left, int chainKindId, int stateId = -1, bool refresh=false) where T : class, ICoreObject, new()
        {
            T item;
            List<T> collectionChainSourceList = new List<T>();
            // используем кеширование только для stateId == 1 !!!
            if (!refresh && stateId == State.STATEACTIVE)
            {
                if(left.Workarea.Cashe.GetChainCasheData<T>().Exists(left.Id, chainKindId))
                {
                    return left.Workarea.Cashe.GetChainCasheData<T>().Get(left.Id, chainKindId);
                }
            }
            using (SqlConnection cnn = left.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collectionChainSourceList;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = left.Entity.FindMethod("LoadChainSourceList").FullName; 
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = left.Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = chainKindId;
                        if (stateId != 1)
                            cmd.Parameters.Add(GlobalSqlParamNames.ChainStateId, SqlDbType.Int).Value = stateId;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            if (left.Workarea.Cashe.GetCasheData<T>().Exists(id))
                            {
                                item = left.Workarea.Cashe.GetCasheData<T>().Item(id);
                                item.Load(reader);
                            }
                            else
                            {
                                item = new T { Workarea = left.Workarea };
                                item.Load(reader);
                                left.Workarea.Cashe.SetCasheData(item);
                            }
                            collectionChainSourceList.Add(item);
                            
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            if (stateId == State.STATEACTIVE)
            {
                left.Workarea.Cashe.GetChainCasheData<T>().Add(left.Id, chainKindId, collectionChainSourceList);

            }
            return collectionChainSourceList;
        }
        /// <summary>
        /// Коллекция объектов ссылающихся на текущий (слева)
        /// </summary>
        /// <remarks>
        /// При получении данных используется глобальный кеш данных рабочей области - если объект был ранее загружен он обновляется, 
        /// в противном случае добавляется в глобальный кеш.
        /// </remarks>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="left">Значение объекта слева</param>
        /// <param name="chainKindId">Идентификатор типа связи</param>
        /// <param name="stateId">Состояние, по умолчанию (-1) - все связи, допустимые значения в соотвествии с возможными состояниями</param>
        /// <returns></returns>
        public static List<T> DestinationList<T>(T left, int chainKindId, int stateId = -1) where T : class, ICoreObject, new()
        {
            T item;
            List<T> collection = new List<T>();
            using (SqlConnection cnn = left.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = left.Entity.FindMethod("LoadChainDestinationList").FullName; //"[Contractor].[AgentsLoadByChainsId]";//
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = left.Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = chainKindId;
                        if (stateId != 1)
                            cmd.Parameters.Add(GlobalSqlParamNames.ChainStateId, SqlDbType.Int).Value = stateId;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            if (left.Workarea.Cashe.GetCasheData<T>().Exists(id))
                            {
                                item = left.Workarea.Cashe.GetCasheData<T>().Item(id);
                                item.Load(reader);
                            }
                            else
                            {
                                item = new T { Workarea = left.Workarea };
                                item.Load(reader);
                                left.Workarea.Cashe.SetCasheData(item);
                            }
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
        #endregion
    }
    /// <summary>
    /// Связи документов
    /// </summary>
    public class ChainDocument: Chain<Document>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ChainDocument()
        {
            KindId = 20;
        }
        private decimal _summ;
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Summ
        {
            get { return _summ; }
            set
            {
                if (value == _summ) return;
                OnPropertyChanging(GlobalPropertyNames.Summ);
                _summ = value;
                OnPropertyChanged(GlobalPropertyNames.Summ);
            }
        }

        private DateTime? _dateValue;
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime? DateValue
        {
            get { return _dateValue; }
            set
            {
                if (value == _dateValue) return;
                OnPropertyChanging(GlobalPropertyNames.DateValue);
                _dateValue = value;
                OnPropertyChanged(GlobalPropertyNames.DateValue);
            }
        }

        #region Состояние
        ChainDocumentStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new ChainDocumentStruct
                {
                    DateValue = _dateValue,
                    Summ = _summ
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            DateValue = _baseStruct.DateValue;
            Summ = _baseStruct.Summ;

            IsChanged = false;
        }
        #endregion

        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Завершить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _summ = reader.IsDBNull(16) ? 0 : reader.GetDecimal(16);
                if (reader.IsDBNull(17))
                    _dateValue = null;
                else
                    _dateValue = reader.GetDateTime(17);
            }
            catch (Exception ex)
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
            SqlParameter prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Summ, SqlDbType.Money);
            prm.Direction = ParameterDirection.Input;
            if (_summ == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _summ;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.DateValue, SqlDbType.Date);
            prm.Direction = ParameterDirection.Input;
            if(_dateValue.HasValue)
                prm.Value = _dateValue;
            else
                prm.Value = DBNull.Value;

        }
        
    }
}
