using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Внутренняя структура объекта "Оборудования"</summary>
    internal struct EquipmentStruct
    {
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
		/// <summary>Тип транспортного средства</summary>
		public int AutoTypeId;
		/// <summary>Id создавшего пользователя</summary>
		public int UserOwnerId;
		/// <summary>Номер чертежа</summary>
		public string DrawingNumber;
		/// <summary>Номер сборочного чертежа</summary>
		public string DrawingAssemblyNumber;
		/// <summary>Масса</summary>
		public decimal Weight;
    }
    /// <summary>Оборудование</summary>
    public sealed class Equipment : BaseCore<Equipment>, IChains<Equipment>, IReportChainSupport, IEquatable<Equipment>,
                                    IComparable, IComparable<Equipment>,
                                    IFacts<Equipment>,
									IChainsAdvancedList<Equipment, EquipmentNode>,
									IChainsAdvancedList<Equipment, Knowledge>,
                                    IChainsAdvancedList<Equipment, Note>,
                                    IChainsAdvancedList<Equipment, FileData>,
                                    ICodes<Equipment>, IHierarchySupport, ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>Автомобиль 1</summary>
        public const int KINDVALUE_EQUIPMENTAUTO = 1;
        /// <summary>Единица оборудования 2</summary>
        public const int KINDVALUE_EQUIPMENTUNIT = 2;
		/// <summary>Папка 3</summary>
		public const int KINDVALUE_FOLDER = 3;

		/// <summary>Автомобиль 7733249</summary>
        public const int KINDID_EQUIPMENTAUTO = 7733249;
        /// <summary>Единица оборудования 7733250</summary>
        public const int KINDID_EQUIPMENTUNIT = 7733250;
		/// <summary>Папка 7733251</summary>
		public const int KINDID_FOLDER = 7733251;
        #endregion
        bool IEquatable<Equipment>.Equals(Equipment other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Equipment otherObj = (Equipment)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(Equipment other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>Конструктор</summary>
        public Equipment()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Equipment;
        }
        protected override void CopyValue(Equipment template)
        {
            base.CopyValue(template);
            MyCompanyId = template.MyCompanyId;
			AutoTypeId = template.AutoTypeId;
			UserOwnerId = template.UserOwnerId;
			DrawingNumber = template.DrawingNumber;
			DrawingAssemblyNumber = template.DrawingAssemblyNumber;
			Weight = template.Weight;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Equipment Clone(bool endInit)
        {
            Equipment obj = base.Clone(false);
            obj.MyCompanyId = MyCompanyId;
			obj.AutoTypeId = AutoTypeId;
			obj.UserOwnerId = UserOwnerId;
			obj.DrawingNumber = DrawingNumber;
			obj.DrawingAssemblyNumber = DrawingAssemblyNumber;
			obj.Weight = Weight;
            
            if (endInit)
                OnEndInit();
            return obj;
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


		private int _AutoTypeId;
		/// <summary>
		/// Id Типа транспортного средства
		/// </summary>
		public int AutoTypeId
		{
			get { return _AutoTypeId; }
			set
			{
				if (value == _AutoTypeId) return;
				OnPropertyChanging(GlobalPropertyNames.AutoTypeId);
				_AutoTypeId = value;
				OnPropertyChanged(GlobalPropertyNames.AutoTypeId);
			}
		}

		private Analitic _AutoType;
		/// <summary>
		/// Тип транспортного средства
		/// </summary>
		public Analitic AutoType
		{
			get
			{
				if (_AutoTypeId == 0)
					return null;
				if (_AutoType == null)
					_AutoType = Workarea.Cashe.GetCasheData<Analitic>().Item(_AutoTypeId);
				else if (_AutoType.Id != _AutoTypeId)
					_AutoType = Workarea.Cashe.GetCasheData<Analitic>().Item(_AutoTypeId);
				return _AutoType;
			}
			set
			{
				if (_AutoType == value) return;
				OnPropertyChanging(GlobalPropertyNames.AutoType);
				_AutoType = value;
				_AutoTypeId = _AutoType == null ? 0 : _AutoType.Id;
				OnPropertyChanged(GlobalPropertyNames.AutoType);
			}
		}

		private int _userOwnerId;
		/// <summary>
		/// Идентификатор пользователя, который создал элемент образа оборудования
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

		private Agent _userOwner;
		/// <summary>
		/// Пользователь, который создал элемент образа оборудования
		/// </summary>
		public Agent UserOwner
		{
			get
			{
				if (_userOwnerId == 0)
					return null;
				if (_userOwner == null)
					_userOwner = Workarea.Cashe.GetCasheData<Agent>().Item(_userOwnerId);
				else if (_userOwner.Id != _userOwnerId)
					_userOwner = Workarea.Cashe.GetCasheData<Agent>().Item(_userOwnerId);
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

		private string _drawingNumber;
		/// <summary>
		/// Номер чертежа для элемента образа оборудования
		/// </summary>
		public string DrawingNumber
		{
			get { return _drawingNumber; }
			set
			{
				if (value == _drawingNumber) return;
				OnPropertyChanging(GlobalPropertyNames.DrawingNumber);
				_drawingNumber = value;
				OnPropertyChanged(GlobalPropertyNames.DrawingNumber);
			}
		}

		private string _drawingAssemblyNumber;
		/// <summary>
		/// Номер сборочного чертежа для элемента образа оборудования
		/// </summary>
		public string DrawingAssemblyNumber
		{
			get { return _drawingAssemblyNumber; }
			set
			{
				if (value == _drawingAssemblyNumber) return;
				OnPropertyChanging(GlobalPropertyNames.DrawingAssemblyNumber);
				_drawingAssemblyNumber = value;
				OnPropertyChanged(GlobalPropertyNames.DrawingAssemblyNumber);
			}
		}

		private decimal _weight;
		/// <summary>
		/// Масса элемента образа оборудования
		/// </summary>
		public decimal Weight
		{
			get { return _weight; }
			set
			{
				if (value == _weight) return;
				OnPropertyChanging(GlobalPropertyNames.Weight);
				_weight = value;
				OnPropertyChanged(GlobalPropertyNames.Weight);
			}
		}

 

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();

            if (_myCompanyId == 0)
                throw new ValidateException("Не указана компания владелец");
            //throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_CURRENCYINTCODE", 1049));
            //if (string.IsNullOrEmpty(Code))
            //throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_CURRENCYCODE", 1049));
        }
        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, _myCompanyId.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.AutoTypeId, _AutoTypeId.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.UserOwnerId, _userOwnerId.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.DrawingNumber, _drawingNumber.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.DrawingAssemblyNumber, _drawingAssemblyNumber.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.Weight, _weight.ToString());
		}

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null) _myCompanyId = Int32.Parse(reader[GlobalPropertyNames.MyCompanyId]);
			if (reader.GetAttribute(GlobalPropertyNames.UserOwnerId) != null) _userOwnerId = Int32.Parse(reader[GlobalPropertyNames.UserOwnerId]);
			if (reader.GetAttribute(GlobalPropertyNames.DrawingNumber) != null) _drawingNumber = reader[GlobalPropertyNames.DrawingNumber];
			if (reader.GetAttribute(GlobalPropertyNames.DrawingAssemblyNumber) != null) _drawingAssemblyNumber = reader[GlobalPropertyNames.DrawingAssemblyNumber];
			if (reader.GetAttribute(GlobalPropertyNames.Weight) != null) _weight = Decimal.Parse(reader[GlobalPropertyNames.Weight]);
			if (reader.GetAttribute(GlobalPropertyNames.AutoTypeId) != null) _AutoTypeId = Int32.Parse(reader[GlobalPropertyNames.AutoTypeId]);
		}
        #endregion

        #region Состояние
        EquipmentStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new EquipmentStruct
                {
                    MyCompanyId = _myCompanyId,
					AutoTypeId = _AutoTypeId,
					UserOwnerId = _userOwnerId,
					DrawingNumber = _drawingNumber,
					DrawingAssemblyNumber = _drawingAssemblyNumber,
					Weight = _weight
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
            MyCompanyId = _baseStruct.MyCompanyId;
			AutoTypeId = _baseStruct.AutoTypeId;
			UserOwnerId = _baseStruct.UserOwnerId;
			DrawingNumber = _baseStruct.DrawingNumber;
			DrawingAssemblyNumber = _baseStruct.DrawingAssemblyNumber;
			Weight = _baseStruct.Weight;
            IsChanged = false;
        }
        #endregion
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
                _myCompanyId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
				_AutoTypeId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
				_userOwnerId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
				_drawingNumber = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
				_drawingAssemblyNumber = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
				_weight = reader.IsDBNull(22) ? 0 : reader.GetDecimal(22);
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
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.AutoTypeId, SqlDbType.Int) { IsNullable = true };
			if (_AutoTypeId == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _AutoTypeId;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.UserOwnerId, SqlDbType.Int) { IsNullable = true };
			if (_userOwnerId == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _userOwnerId;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.DrawingNumber, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_drawingNumber))
				prm.Value = DBNull.Value;
			else
				prm.Value = _drawingNumber;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.DrawingAssemblyNumber, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_drawingAssemblyNumber))
				prm.Value = DBNull.Value;
			else
				prm.Value = _drawingAssemblyNumber;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.Weight, SqlDbType.Money) { IsNullable = true };
			prm.Value = _weight;
			sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Equipment> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<Equipment>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Equipment>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Equipment> IChains<Equipment>.SourceList(int chainKindId)
        {
            return Chain<Equipment>.GetChainSourceList(this, chainKindId);
        }
        List<Equipment> IChains<Equipment>.DestinationList(int chainKindId)
        {
            return Chain<Equipment>.DestinationList(this, chainKindId);

        }
        #endregion

        #region IChainsAdvancedList<Agent,Knowledge> Members

        List<IChainAdvanced<Equipment, Knowledge>> IChainsAdvancedList<Equipment, Knowledge>.GetLinks()
        {
            return ((IChainsAdvancedList<Equipment, Knowledge>)this).GetLinks(59);
        }

        List<IChainAdvanced<Equipment, Knowledge>> IChainsAdvancedList<Equipment, Knowledge>.GetLinks(int? kind)
        {
            return GetLinkedKnowledges();
        }
        public List<IChainAdvanced<Equipment, Knowledge>> GetLinkedKnowledges()
        {
            List<IChainAdvanced<Equipment, Knowledge>> collection = new List<IChainAdvanced<Equipment, Knowledge>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Equipment>().Entity.FindMethod("LoadKnowledges").FullName;
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
                                ChainAdvanced<Equipment, Knowledge> item = new ChainAdvanced<Equipment, Knowledge> { Workarea = Workarea, Left = this };
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
        List<ChainValueView> IChainsAdvancedList<Equipment, Knowledge>.GetChainView()
        {
            return ChainValueView.GetView<Equipment, Knowledge>(this);
        }
        #endregion
        #region IChainsAdvancedList<Equipment,Note> Members

        List<IChainAdvanced<Equipment, Note>> IChainsAdvancedList<Equipment, Note>.GetLinks()
        {
            return ChainAdvanced<Equipment, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Equipment, Note>> IChainsAdvancedList<Equipment, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Equipment, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Equipment, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Equipment, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Equipment, Note>.GetChainView()
        {
            return ChainValueView.GetView<Equipment, Note>(this);
        }
        #endregion
		#region IChainsAdvancedList<Equipment,EquipmentNode> Members

		List<IChainAdvanced<Equipment, EquipmentNode>> IChainsAdvancedList<Equipment, EquipmentNode>.GetLinks()
		{
			return ((IChainsAdvancedList<Equipment, EquipmentNode>)this).GetLinks(112);
		}

		List<IChainAdvanced<Equipment, EquipmentNode>> IChainsAdvancedList<Equipment, EquipmentNode>.GetLinks(int? kind)
		{
			return GetLinkedEquipmentDetail();
		}
		public List<IChainAdvanced<Equipment, EquipmentNode>> GetLinkedEquipmentDetail()
		{
			List<IChainAdvanced<Equipment, EquipmentNode>> collection = new List<IChainAdvanced<Equipment, EquipmentNode>>();
			using (SqlConnection cnn = Workarea.GetDatabaseConnection())
			{
				using (SqlCommand cmd = cnn.CreateCommand())
				{
					cmd.CommandText = Workarea.Empty<Equipment>().Entity.FindMethod("LoadEquipmentDetails").FullName;
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
								ChainAdvanced<Equipment, EquipmentNode> item = new ChainAdvanced<Equipment, EquipmentNode> { Workarea = Workarea, Left = this };
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
		List<ChainValueView> IChainsAdvancedList<Equipment, EquipmentNode>.GetChainView()
		{
			return ChainValueView.GetView<Equipment, EquipmentNode>(this);
		}
		#endregion
		#region IChainsAdvancedList<Equipment,FileData> Members

        List<IChainAdvanced<Equipment, FileData>> IChainsAdvancedList<Equipment, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<Equipment, FileData>)this).GetLinks(70);
        }

        List<IChainAdvanced<Equipment, FileData>> IChainsAdvancedList<Equipment, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<Equipment, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Equipment, FileData>(this);
        }
        public List<IChainAdvanced<Equipment, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<Equipment, FileData>> collection = new List<IChainAdvanced<Equipment, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Equipment>().Entity.FindMethod("LoadFiles").FullName;
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
                                ChainAdvanced<Equipment, FileData> item = new ChainAdvanced<Equipment, FileData> { Workarea = Workarea, Left = this };
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
        #region ICodes
        public List<CodeValue<Equipment>> GetValues(bool allKinds)
        {
            return CodeHelper<Equipment>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Equipment>.GetView(this, true);
        }
        #endregion

        #region IFacts

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

        private int? _firstHierarchy;
        /// <summary>
        /// Первая группа в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            return FirstHierarchy(false);
        }
        /// <summary>
        /// Первая группа в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy(bool refresh)
        {
            if (!refresh && (LastLoadPartial.HasValue && LastLoadPartial.Value.AddMinutes(Workarea.Cashe.DefaultPartalReloadTime) > DateTime.Now))
            {
                if (!_firstHierarchy.HasValue) return null;
                return Workarea.Cashe.GetCasheData<Hierarchy>().Item(_firstHierarchy.Value);
            }
            _firstHierarchy = Hierarchy.FirstHierarchy<Equipment>(this);
            LastLoadPartial = DateTime.Now;
            if (!_firstHierarchy.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(_firstHierarchy.Value);
        }
        /// <summary>
        /// Поиск объекта
        /// </summary>
        /// <param name="hierarchyId">Идентификатор иерархии в которой осуществлять поиск</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="flags">Флаг</param>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <param name="name">Наименование</param>
        /// <param name="kindId">Идентификатор типа</param>
        /// <param name="code">Признак</param>
        /// <param name="memo">Наименование</param>
        /// <param name="flagString">Пользовательский флаг</param>
        /// <param name="templateId">Идентификатор шаблона</param>
        /// <param name="count">Количество, по умолчанию 100</param>
        /// <param name="filter">Дополнительный фильтр</param>
        /// <param name="useAndFilter">Использовать фильтр И</param>
        /// <returns></returns>
        public List<Equipment> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
                                     int? stateId = null, string name = null, int kindId = 0, string code = null,
                                     string memo = null, string flagString = null, int templateId = 0,
                                     int count = 100, Predicate<Equipment> filter = null, bool useAndFilter = false)
        {
            Equipment item = new Equipment { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Equipment> collection = new List<Equipment>();
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



                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Equipment { Workarea = Workarea };
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