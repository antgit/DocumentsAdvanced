using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml;

namespace BusinessObjects
{

//    AutoGosNumber	nvarchar(50)	Checked
//AutoUsedFuelId	int	Checked
//AutoPassportNumber	nvarchar(50)	Checked
//AutoChassis	nvarchar(50)	Checked
//AutoMotor	nvarchar(50)	Checked
//AutoRateFuelConsumption	money	Checked
//AutoDateRelease	datetime	Checked
//AutoGrossVehicleWeight	money	Checked
//AutoUnladenWeight	money	Checked
//AutoVolumeVehicleEngine	money	Checked
//AutoLicenseNumber	nvarchar(50)	Checked
//AutoLicenseDateStart	datetime	Checked
//AutoLicenseDateExpiration	datetime	Checked
//AutoInsurance	nvarchar(50)	Checked
//AutoInsuranceDateStart	datetime	Checked
//AutoInsuranceDateExpiration	datetime	Checked
//AutoSanitary	nvarchar(50)	Checked
//AutoSanitaryDateStart	datetime	Checked
//AutoSanitaryDateExpiration	datetime	Checked
//AutoTonnageKindId	int	Checked
//AutoTypeId	int	Checked
//TrailerUsedAutoId	int	Checked
//TrailerInterceptedVolumesLeft	money	Checked
//TrailerInterceptedVolumesRight	money	Checked
//TrailerPallet	int	Checked
//AutoAdditionalFuelTank	bit	Checked
	internal struct AutoStruct
	{
		/// <summary>Гос. №</summary>
		public string GosNumber;
		/// <summary>Используемое топлива</summary>
		public int UsedFuelId;
		/// <summary>Номер тех. паспорта</summary>
		public string AutoPassportNumber;
		/// <summary>Номер шасси</summary>
		public string AutoChassis;
		/// <summary>Номер двигателя</summary>
		public string AutoMotor;
		/// <summary>Норма расхода топлива</summary>
		public decimal RateFuelConsumption;
		/// <summary>Год выпуска</summary>
		public DateTime? DateOn;
		/// <summary>Полная масса</summary>
		public decimal GrossVehicleWeight;
		/// <summary>Собственная масса</summary>
		public decimal UnladenWeight;
		/// <summary>Объем двигнателя</summary>
		public decimal VolumeVehicleEngine;
		/// <summary>Номер лицензии</summary>
		public string LicenseNumber;
		/// <summary>Дата начала лицензии</summary>
		public DateTime? LicenseDateStart;
		/// <summary>Дата окончания лицензии</summary>
		public DateTime? LicenseDateExpiration;
		/// <summary>Номер страховки</summary>
		public string InsuranceNumber;
		/// <summary>Дата начала страховки</summary>
		public DateTime? InsuranceDateStart;
		/// <summary>Дата окончания страховки</summary>
		public DateTime? InsuranceDateExpiration;
		/// <summary>Номер санитарного пасспорта</summary>
		public string SanitaryNumber;
		/// <summary>Дата начала санитарного пасспорта</summary>
		public DateTime? SanitaryDateStart;
		/// <summary>Дата окончания санитарного пасспорта</summary>
		public DateTime? SanitaryDateExpiration;
		/// <summary>Вид ТС по тоннажу</summary>
		public int TonnageKindId;
		/// <summary>Закрепленный за прицепом автомобиль</summary>
		public int TrailerUsedAutoId;
		/// <summary>Отсекаемый объем слева</summary>
		public decimal InterceptedVolumesLeft;
		/// <summary>Отсекаемый объем справа</summary>
		public decimal InterceptedVolumesRight;
		/// <summary>Паллетоместа</summary>
		public int Pallet;
		/// <summary>Дополнительный бак</summary>
		public bool AdditionalFuelTank;
		/// <summary>Id Модели авто</summary>
		public int EquipmentId;
	}
    /// <summary>
    /// Автомобиль
    /// </summary>
    public class Auto : BaseCoreObject, IRelationSingle
    {
        /// <summary>
        /// Конструктор
        /// </summary>
		public Auto()
			: base()
        {
            EntityId = (short) WhellKnownDbEntity.Auto;
        }

        #region Свойства
		private string _gosNumber;
		/// <summary>
		/// Государственный номер
		/// </summary>
		public string GosNumber
		{
			get { return _gosNumber; }
			set
			{
				if (value == _gosNumber) return;
				OnPropertyChanging(GlobalPropertyNames.GosNumber);
				_gosNumber = value;
				OnPropertyChanged(GlobalPropertyNames.GosNumber);
			}
		}

		private int _usedFuelId;
		/// <summary>
		/// Id Используемого топлива
		/// </summary>
		public int UsedFuelId
		{
			get { return _usedFuelId; }
			set
			{
				if (value == _usedFuelId) return;
				OnPropertyChanging(GlobalPropertyNames.UsedFuelId);
				_usedFuelId = value;
				OnPropertyChanged(GlobalPropertyNames.UsedFuelId);
			}
		}

		private Analitic _usedFuel;
		/// <summary>
		/// Используемое топлива
		/// </summary>
		public Analitic UsedFuel
		{
			get
			{
				if (_usedFuelId == 0)
					return null;
				if (_usedFuel == null)
					_usedFuel = Workarea.Cashe.GetCasheData<Analitic>().Item(_usedFuelId);
				else if (_usedFuel.Id != _usedFuelId)
					_usedFuel = Workarea.Cashe.GetCasheData<Analitic>().Item(_usedFuelId);
				return _usedFuel;
			}
			set
			{
				if (_usedFuel == value) return;
				OnPropertyChanging(GlobalPropertyNames.UsedFuel);
				_usedFuel = value;
				_usedFuelId = _usedFuel == null ? 0 : _usedFuel.Id;
				OnPropertyChanged(GlobalPropertyNames.UsedFuel);
			}
		}

		private string _autoPassportNumber;
		/// <summary>
		/// Номер тех. паспорта
		/// </summary>
		public string AutoPassportNumber
		{
			get { return _autoPassportNumber; }
			set
			{
				if (value == _autoPassportNumber) return;
				OnPropertyChanging(GlobalPropertyNames.AutoPassportNumber);
				_autoPassportNumber = value;
				OnPropertyChanged(GlobalPropertyNames.AutoPassportNumber);
			}
		}

		private string _autoChassis;
		/// <summary>
		/// Номер шасси
		/// </summary>
		public string AutoChassis
		{
			get { return _autoChassis; }
			set
			{
				if (value == _autoChassis) return;
				OnPropertyChanging(GlobalPropertyNames.AutoChassis);
				_autoChassis = value;
				OnPropertyChanged(GlobalPropertyNames.AutoChassis);
			}
		}

		private string _autoMotor;
		/// <summary>
		/// Номер двигателя
		/// </summary>
		public string AutoMotor
		{
			get { return _autoMotor; }
			set
			{
				if (value == _autoMotor) return;
				OnPropertyChanging(GlobalPropertyNames.AutoMotor);
				_autoMotor = value;
				OnPropertyChanged(GlobalPropertyNames.AutoMotor);
			}
		}

		private decimal _rateFuelConsumption;
		/// <summary>
		/// Норма расхода топлива
		/// </summary>
		public decimal RateFuelConsumption
		{
			get { return _rateFuelConsumption; }
			set
			{
				if (value == _rateFuelConsumption) return;
				OnPropertyChanging(GlobalPropertyNames.RateFuelConsumption);
				_rateFuelConsumption = value;
				OnPropertyChanged(GlobalPropertyNames.RateFuelConsumption);
			}
		}

		private DateTime? _dateOn;
		/// <summary>
		/// Год выпуска
		/// </summary>
		public DateTime? DateOn
		{
			get { return _dateOn; }
			set
			{
				if (value == _dateOn) return;
				OnPropertyChanging(GlobalPropertyNames.DateOn);
				_dateOn = value;
				OnPropertyChanged(GlobalPropertyNames.DateOn);
			}
		}

		private decimal _grossVehicleWeight;
		/// <summary>
		/// Полная масса
		/// </summary>
		public decimal GrossVehicleWeight
		{
			get { return _grossVehicleWeight; }
			set
			{
				if (value == _grossVehicleWeight) return;
				OnPropertyChanging(GlobalPropertyNames.GrossVehicleWeight);
				_grossVehicleWeight = value;
				OnPropertyChanged(GlobalPropertyNames.GrossVehicleWeight);
			}
		}

		private decimal _unladenWeight;
		/// <summary>
		/// Собственная масса
		/// </summary>
		public decimal UnladenWeight
		{
			get { return _unladenWeight; }
			set
			{
				if (value == _unladenWeight) return;
				OnPropertyChanging(GlobalPropertyNames.UnladenWeight);
				_unladenWeight = value;
				OnPropertyChanged(GlobalPropertyNames.UnladenWeight);
			}
		}

		private decimal _volumeVehicleEngine;
		/// <summary>
		/// Объем двигнателя
		/// </summary>
		public decimal VolumeVehicleEngine
		{
			get { return _volumeVehicleEngine; }
			set
			{
				if (value == _volumeVehicleEngine) return;
				OnPropertyChanging(GlobalPropertyNames.VolumeVehicleEngine);
				_volumeVehicleEngine = value;
				OnPropertyChanged(GlobalPropertyNames.VolumeVehicleEngine);
			}
		}

		private string _licenseNumber;
		/// <summary>
		/// Номер лицензии
		/// </summary>
		public string LicenseNumber
		{
			get { return _licenseNumber; }
			set
			{
				if (value == _licenseNumber) return;
				OnPropertyChanging(GlobalPropertyNames.LicenseNumber);
				_licenseNumber = value;
				OnPropertyChanged(GlobalPropertyNames.LicenseNumber);
			}
		}

		private DateTime? _licenseDateStart;
		/// <summary>
		/// Дата начала лицензии
		/// </summary>
		public DateTime? LicenseDateStart
		{
			get { return _licenseDateStart; }
			set
			{
				if (value == _licenseDateStart) return;
				OnPropertyChanging(GlobalPropertyNames.LicenseDateStart);
				_licenseDateStart = value;
				OnPropertyChanged(GlobalPropertyNames.LicenseDateStart);
			}
		}

		private DateTime? _licenseDateExpiration;
		/// <summary>
		/// Дата окончания лицензии
		/// </summary>
		public DateTime? LicenseDateExpiration
		{
			get { return _licenseDateExpiration; }
			set
			{
				if (value == _licenseDateExpiration) return;
				OnPropertyChanging(GlobalPropertyNames.LicenseDateExpiration);
				_licenseDateExpiration = value;
				OnPropertyChanged(GlobalPropertyNames.LicenseDateExpiration);
			}
		}

		private string _insuranceNumber;
		/// <summary>
		/// Номер страховки
		/// </summary>
		public string InsuranceNumber
		{
			get { return _insuranceNumber; }
			set
			{
				if (value == _insuranceNumber) return;
				OnPropertyChanging(GlobalPropertyNames.InsuranceNumber);
				_insuranceNumber = value;
				OnPropertyChanged(GlobalPropertyNames.InsuranceNumber);
			}
		}

		private DateTime? _insuranceDateStart;
		/// <summary>
		/// Дата начала страховки
		/// </summary>
		public DateTime? InsuranceDateStart
		{
			get { return _insuranceDateStart; }
			set
			{
				if (value == _insuranceDateStart) return;
				OnPropertyChanging(GlobalPropertyNames.InsuranceDateStart);
				_insuranceDateStart = value;
				OnPropertyChanged(GlobalPropertyNames.InsuranceDateStart);
			}
		}

		private DateTime? _insuranceDateExpiration;
		/// <summary>
		/// Дата окончания страховки
		/// </summary>
		public DateTime? InsuranceDateExpiration
		{
			get { return _insuranceDateExpiration; }
			set
			{
				if (value == _insuranceDateExpiration) return;
				OnPropertyChanging(GlobalPropertyNames.InsuranceDateExpiration);
				_insuranceDateExpiration = value;
				OnPropertyChanged(GlobalPropertyNames.InsuranceDateExpiration);
			}
		}

		private string _sanitaryNumber;
		/// <summary>
		/// Номер санитарного пасспорта
		/// </summary>
		public string SanitaryNumber
		{
			get { return _sanitaryNumber; }
			set
			{
				if (value == _sanitaryNumber) return;
				OnPropertyChanging(GlobalPropertyNames.SanitaryNumber);
				_sanitaryNumber = value;
				OnPropertyChanged(GlobalPropertyNames.SanitaryNumber);
			}
		}

		private DateTime? _sanitaryDateStart;
		/// <summary>
		/// Дата начала санитарного пасспорта
		/// </summary>
		public DateTime? SanitaryDateStart
		{
			get { return _sanitaryDateStart; }
			set
			{
				if (value == _sanitaryDateStart) return;
				OnPropertyChanging(GlobalPropertyNames.SanitaryDateStart);
				_sanitaryDateStart = value;
				OnPropertyChanged(GlobalPropertyNames.SanitaryDateStart);
			}
		}

		private DateTime? _sanitaryDateExpiration;
		/// <summary>
		/// Дата окончания санитарного пасспорта
		/// </summary>
		public DateTime? SanitaryDateExpiration
		{
			get { return _sanitaryDateExpiration; }
			set
			{
				if (value == _sanitaryDateExpiration) return;
				OnPropertyChanging(GlobalPropertyNames.SanitaryDateExpiration);
				_sanitaryDateExpiration = value;
				OnPropertyChanged(GlobalPropertyNames.SanitaryDateExpiration);
			}
		}

		private int _tonnageKindId;
		/// <summary>
		/// Id Вида ТС по тоннажу
		/// </summary>
		public int TonnageKindId
		{
			get { return _tonnageKindId; }
			set
			{
				if (value == _tonnageKindId) return;
				OnPropertyChanging(GlobalPropertyNames.TonnageKindId);
				_tonnageKindId = value;
				OnPropertyChanged(GlobalPropertyNames.TonnageKindId);
			}
		}

		private Analitic _tonnageKind;
		/// <summary>
		/// Вид ТС по тоннажу
		/// </summary>
		public Analitic TonnageKind
		{
			get
			{
				if (_tonnageKindId == 0)
					return null;
				if (_tonnageKind == null)
					_tonnageKind = Workarea.Cashe.GetCasheData<Analitic>().Item(_tonnageKindId);
				else if (_tonnageKind.Id != _tonnageKindId)
					_tonnageKind = Workarea.Cashe.GetCasheData<Analitic>().Item(_tonnageKindId);
				return _tonnageKind;
			}
			set
			{
				if (_tonnageKind == value) return;
				OnPropertyChanging(GlobalPropertyNames.TonnageKind);
				_tonnageKind = value;
				_tonnageKindId = _tonnageKind == null ? 0 : _tonnageKind.Id;
				OnPropertyChanged(GlobalPropertyNames.TonnageKind);
			}
		}

		private int _trailerUsedAutoId;
		/// <summary>
		/// Id Закрепленного за прицепом автомобиля
		/// </summary>
		public int TrailerUsedAutoId
		{
			get { return _trailerUsedAutoId; }
			set
			{
				if (value == _trailerUsedAutoId) return;
				OnPropertyChanging(GlobalPropertyNames.TrailerUsedAutoId);
				_trailerUsedAutoId = value;
				OnPropertyChanged(GlobalPropertyNames.TrailerUsedAutoId);
			}
		}

		private Auto _trailerUsedAuto;
		/// <summary>
		/// Закрепленный за прицепом автомобиль
		/// </summary>
		public Auto TrailerUsedAuto
		{
			get
			{
				if (_trailerUsedAutoId == 0)
					return null;
				if (_trailerUsedAuto == null)
					_trailerUsedAuto = Workarea.Cashe.GetCasheData<Auto>().Item(_trailerUsedAutoId);
				else if (_trailerUsedAuto.Id != _trailerUsedAutoId)
					_trailerUsedAuto = Workarea.Cashe.GetCasheData<Auto>().Item(_trailerUsedAutoId);
				return _trailerUsedAuto;
			}
			set
			{
				if (_trailerUsedAuto == value) return;
				OnPropertyChanging(GlobalPropertyNames.TrailerUsedAuto);
				_trailerUsedAuto = value;
				_trailerUsedAutoId = _trailerUsedAuto == null ? 0 : _trailerUsedAuto.Id;
				OnPropertyChanged(GlobalPropertyNames.TrailerUsedAuto);
			}
		}

		private decimal _interceptedVolumesLeft;
		/// <summary>
		/// Отсекаемый объем слева
		/// </summary>
		public decimal InterceptedVolumesLeft
		{
			get { return _interceptedVolumesLeft; }
			set
			{
				if (value == _interceptedVolumesLeft) return;
				OnPropertyChanging(GlobalPropertyNames.InterceptedVolumesLeft);
				_interceptedVolumesLeft = value;
				OnPropertyChanged(GlobalPropertyNames.InterceptedVolumesLeft);
			}
		}

		private decimal _interceptedVolumesRight;
		/// <summary>
		/// Отсекаемый объем справа
		/// </summary>
		public decimal InterceptedVolumesRight
		{
			get { return _interceptedVolumesRight; }
			set
			{
				if (value == _interceptedVolumesRight) return;
				OnPropertyChanging(GlobalPropertyNames.InterceptedVolumesRight);
				_interceptedVolumesRight = value;
				OnPropertyChanged(GlobalPropertyNames.InterceptedVolumesRight);
			}
		}

		private int _pallet;
		/// <summary>
		/// Паллетоместа
		/// </summary>
		public int Pallet
		{
			get { return _pallet; }
			set
			{
				if (value == _pallet) return;
				OnPropertyChanging(GlobalPropertyNames.Pallet);
				_pallet = value;
				OnPropertyChanged(GlobalPropertyNames.Pallet);
			}
		}

		private bool _additionalFuelTank;
		/// <summary>
		/// Дополнительный бак
		/// </summary>
		public bool AdditionalFuelTank
		{
			get { return _additionalFuelTank; }
			set
			{
				if (value == _additionalFuelTank) return;
				OnPropertyChanging(GlobalPropertyNames.AdditionalFuelTank);
				_additionalFuelTank = value;
				OnPropertyChanged(GlobalPropertyNames.AdditionalFuelTank);
			}
		}

		internal Product _owner;
		/// <summary>
		/// Product(родитель)
		/// </summary>
		public Product Owner
		{
			get { return _owner; }
		}

		private int _equipmentId;
		/// <summary>
		/// Id Закрепленного за прицепом автомобиля
		/// </summary>
		public int EquipmentId
		{
			get { return _equipmentId; }
			set
			{
				if (value == _equipmentId) return;
				OnPropertyChanging(GlobalPropertyNames.EquipmentId);
				_equipmentId = value;
				OnPropertyChanged(GlobalPropertyNames.EquipmentId);
			}
		}

		private Equipment _equipment;
		/// <summary>
		/// Вид ТС по тоннажу
		/// </summary>
		public Equipment Equipment
		{
			get
			{
				if (_equipmentId == 0)
					return null;
				if (_equipment == null)
					_equipment = Workarea.Cashe.GetCasheData<Equipment>().Item(_equipmentId);
				else if (_tonnageKind.Id != _tonnageKindId)
					_equipment = Workarea.Cashe.GetCasheData<Equipment>().Item(_equipmentId);
				return _equipment;
			}
			set
			{
				if (_equipment == value) return;
				OnPropertyChanging(GlobalPropertyNames.Equipment);
				_equipment = value;
				_equipmentId = _equipment == null ? 0 : _equipment.Id;
				OnPropertyChanged(GlobalPropertyNames.Equipment);
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

			writer.WriteAttributeString(GlobalPropertyNames.GosNumber, _gosNumber);
			writer.WriteAttributeString(GlobalPropertyNames.UsedFuelId, _usedFuelId.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString(GlobalPropertyNames.AutoPassportNumber, _autoPassportNumber);
			writer.WriteAttributeString(GlobalPropertyNames.AutoChassis, _autoChassis);
			writer.WriteAttributeString(GlobalPropertyNames.AutoMotor, _autoMotor);
			writer.WriteAttributeString(GlobalPropertyNames.RateFuelConsumption, _rateFuelConsumption.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString(GlobalPropertyNames.DateOn, _dateOn.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.GrossVehicleWeight, _grossVehicleWeight.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString(GlobalPropertyNames.UnladenWeight, _unladenWeight.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString(GlobalPropertyNames.VolumeVehicleEngine, _volumeVehicleEngine.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString(GlobalPropertyNames.LicenseNumber, _licenseNumber);
			writer.WriteAttributeString(GlobalPropertyNames.LicenseDateStart, _licenseDateStart.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.LicenseDateExpiration, _licenseDateExpiration.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.InsuranceNumber, _insuranceNumber);
			writer.WriteAttributeString(GlobalPropertyNames.InsuranceDateStart, _insuranceDateStart.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.InsuranceDateExpiration, _insuranceDateExpiration.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.SanitaryNumber, _sanitaryNumber);
			writer.WriteAttributeString(GlobalPropertyNames.SanitaryDateStart, _sanitaryDateStart.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.SanitaryDateExpiration, _sanitaryDateExpiration.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.TonnageKindId, _tonnageKindId.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString(GlobalPropertyNames.TrailerUsedAutoId, _trailerUsedAutoId.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString(GlobalPropertyNames.InterceptedVolumesLeft, _interceptedVolumesLeft.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString(GlobalPropertyNames.InterceptedVolumesRight, _interceptedVolumesRight.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString(GlobalPropertyNames.Pallet, _pallet.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString(GlobalPropertyNames.AdditionalFuelTank, _additionalFuelTank.ToString());
			writer.WriteAttributeString(GlobalPropertyNames.EquipmentId, _equipmentId.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

			if (reader.GetAttribute(GlobalPropertyNames.GosNumber) != null) _gosNumber = reader[GlobalPropertyNames.GosNumber];
			if (reader.GetAttribute(GlobalPropertyNames.UsedFuelId) != null) _usedFuelId = Int32.Parse(reader[GlobalPropertyNames.UsedFuelId]);
			if (reader.GetAttribute(GlobalPropertyNames.AutoPassportNumber) != null) _autoPassportNumber = reader[GlobalPropertyNames.AutoPassportNumber];
			if (reader.GetAttribute(GlobalPropertyNames.AutoChassis) != null) _autoChassis = reader[GlobalPropertyNames.AutoChassis];
			if (reader.GetAttribute(GlobalPropertyNames.AutoMotor) != null) _autoMotor = reader[GlobalPropertyNames.AutoMotor];
			if (reader.GetAttribute(GlobalPropertyNames.RateFuelConsumption) != null) _rateFuelConsumption = Decimal.Parse(reader[GlobalPropertyNames.RateFuelConsumption]);
			if (reader.GetAttribute(GlobalPropertyNames.DateOn) != null) _dateOn = DateTime.Parse(reader[GlobalPropertyNames.DateOn]);
			if (reader.GetAttribute(GlobalPropertyNames.GrossVehicleWeight) != null) _grossVehicleWeight = Decimal.Parse(reader[GlobalPropertyNames.GrossVehicleWeight]);
			if (reader.GetAttribute(GlobalPropertyNames.UnladenWeight) != null) _unladenWeight = Decimal.Parse(reader[GlobalPropertyNames.UnladenWeight]);
			if (reader.GetAttribute(GlobalPropertyNames.VolumeVehicleEngine) != null) _volumeVehicleEngine = Decimal.Parse(reader[GlobalPropertyNames.VolumeVehicleEngine]);
			if (reader.GetAttribute(GlobalPropertyNames.LicenseNumber) != null) _licenseNumber = reader[GlobalPropertyNames.LicenseNumber];
			if (reader.GetAttribute(GlobalPropertyNames.LicenseDateStart) != null) _licenseDateStart = DateTime.Parse(reader[GlobalPropertyNames.LicenseDateStart]);
			if (reader.GetAttribute(GlobalPropertyNames.LicenseDateExpiration) != null) _licenseDateExpiration = DateTime.Parse(reader[GlobalPropertyNames.LicenseDateExpiration]);
			if (reader.GetAttribute(GlobalPropertyNames.InsuranceNumber) != null) _insuranceNumber = reader[GlobalPropertyNames.InsuranceNumber];
			if (reader.GetAttribute(GlobalPropertyNames.InsuranceDateStart) != null) _insuranceDateStart = DateTime.Parse(reader[GlobalPropertyNames.InsuranceDateStart]);
			if (reader.GetAttribute(GlobalPropertyNames.InsuranceDateExpiration) != null) _insuranceDateExpiration = DateTime.Parse(reader[GlobalPropertyNames.InsuranceDateExpiration]);
			if (reader.GetAttribute(GlobalPropertyNames.SanitaryNumber) != null) _sanitaryNumber = reader[GlobalPropertyNames.SanitaryNumber];
			if (reader.GetAttribute(GlobalPropertyNames.SanitaryDateStart) != null) _sanitaryDateStart = DateTime.Parse(reader[GlobalPropertyNames.SanitaryDateStart]);
			if (reader.GetAttribute(GlobalPropertyNames.SanitaryDateExpiration) != null) _sanitaryDateExpiration = DateTime.Parse(reader[GlobalPropertyNames.SanitaryDateExpiration]);
			if (reader.GetAttribute(GlobalPropertyNames.TonnageKindId) != null) _tonnageKindId = Int32.Parse(reader[GlobalPropertyNames.TonnageKindId]);
			if (reader.GetAttribute(GlobalPropertyNames.TrailerUsedAutoId) != null) _trailerUsedAutoId = Int32.Parse(reader[GlobalPropertyNames.TrailerUsedAutoId]);
			if (reader.GetAttribute(GlobalPropertyNames.InterceptedVolumesLeft) != null) _interceptedVolumesLeft = Decimal.Parse(reader[GlobalPropertyNames.InterceptedVolumesLeft]);
			if (reader.GetAttribute(GlobalPropertyNames.InterceptedVolumesRight) != null) _interceptedVolumesRight = Decimal.Parse(reader[GlobalPropertyNames.InterceptedVolumesRight]);
			if (reader.GetAttribute(GlobalPropertyNames.Pallet) != null) _pallet = Int32.Parse(reader[GlobalPropertyNames.Pallet]);
			if (reader.GetAttribute(GlobalPropertyNames.AdditionalFuelTank) != null) _additionalFuelTank = Boolean.Parse(reader[GlobalPropertyNames.AdditionalFuelTank]);
			if (reader.GetAttribute(GlobalPropertyNames.EquipmentId) != null) _equipmentId = Int32.Parse(reader[GlobalPropertyNames.EquipmentId]);
        }
        #endregion

        ///// <summary>Загрузить</summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, "Contractor.StoresLoad");
        //}
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
				_gosNumber = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
				_usedFuelId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
				_autoPassportNumber = reader.IsDBNull(12) ? string.Empty : reader.GetString(13);
				_autoChassis = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
				_autoMotor = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
				_rateFuelConsumption = reader.IsDBNull(15) ? 0 : reader.GetDecimal(15);
				_dateOn = reader.IsDBNull(16) ? null : (DateTime?)reader.GetDateTime(16);
				_grossVehicleWeight = reader.IsDBNull(17) ? 0 : reader.GetDecimal(17);
				_unladenWeight = reader.IsDBNull(18) ? 0 : reader.GetDecimal(18);
				_volumeVehicleEngine = reader.IsDBNull(19) ? 0 : reader.GetDecimal(19);
				_licenseNumber = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
				_licenseDateStart = reader.IsDBNull(21) ? null : (DateTime?)reader.GetDateTime(21);
				_licenseDateExpiration = reader.IsDBNull(22) ? null : (DateTime?)reader.GetDateTime(22);
				_insuranceNumber = reader.IsDBNull(23) ? string.Empty : reader.GetString(23);
				_insuranceDateStart = reader.IsDBNull(24) ? null : (DateTime?)reader.GetDateTime(24);
				_insuranceDateExpiration = reader.IsDBNull(25) ? null : (DateTime?)reader.GetDateTime(25);
				_sanitaryNumber = reader.IsDBNull(26) ? string.Empty : reader.GetString(26);
				_sanitaryDateStart = reader.IsDBNull(27) ? null : (DateTime?)reader.GetDateTime(27);
				_sanitaryDateExpiration = reader.IsDBNull(28) ? null : (DateTime?)reader.GetDateTime(28);
				_tonnageKindId = reader.IsDBNull(29) ? 0 : reader.GetInt32(29);
				_trailerUsedAutoId = reader.IsDBNull(30) ? 0 : reader.GetInt32(30);
				_interceptedVolumesLeft = reader.IsDBNull(31) ? 0 : reader.GetDecimal(31);
				_interceptedVolumesRight = reader.IsDBNull(32) ? 0 : reader.GetDecimal(32);
				_pallet = reader.IsDBNull(33) ? 0 : reader.GetInt32(33);
				_additionalFuelTank = reader.IsDBNull(34) ? false : reader.GetBoolean(34);
				_equipmentId = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (endInit)
                OnEndInit();
        }

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

			SqlParameter prm = new SqlParameter(GlobalSqlParamNames.GosNumber, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_gosNumber))
				prm.Value = DBNull.Value;
			else
				prm.Value = _gosNumber;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.UsedFuelId, SqlDbType.Int) { IsNullable = true };
			if (_usedFuelId == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _usedFuelId;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.AutoPassportNumber, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_autoPassportNumber))
				prm.Value = DBNull.Value;
			else
				prm.Value = _autoPassportNumber;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.AutoChassis, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_autoChassis))
				prm.Value = DBNull.Value;
			else
				prm.Value = _autoChassis;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.AutoMotor, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_autoMotor))
				prm.Value = DBNull.Value;
			else
				prm.Value = _autoMotor;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.RateFuelConsumption, SqlDbType.Money) { IsNullable = true };
			if (_rateFuelConsumption == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _rateFuelConsumption;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.DateOn, SqlDbType.Date) { IsNullable = true };
			if (_dateOn == null)
				prm.Value = DBNull.Value;
			else
				prm.Value = _dateOn;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.GrossVehicleWeight, SqlDbType.Money) { IsNullable = true };
			if (_grossVehicleWeight == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _grossVehicleWeight;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.UnladenWeight, SqlDbType.Money) { IsNullable = true };
			if (_unladenWeight == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _unladenWeight;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.VolumeVehicleEngine, SqlDbType.Money) { IsNullable = true };
			if (_volumeVehicleEngine == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _volumeVehicleEngine;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.LicenseNumber, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_licenseNumber))
				prm.Value = DBNull.Value;
			else
				prm.Value = _licenseNumber;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.LicenseDateStart, SqlDbType.Date) { IsNullable = true };
			if (_licenseDateStart == null)
				prm.Value = DBNull.Value;
			else
				prm.Value = _licenseDateStart;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.LicenseDateExpiration, SqlDbType.Date) { IsNullable = true };
			if (_licenseDateExpiration == null)
				prm.Value = DBNull.Value;
			else
				prm.Value = _licenseDateExpiration;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.InsuranceNumber, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_insuranceNumber))
				prm.Value = DBNull.Value;
			else
				prm.Value = _insuranceNumber;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.InsuranceDateStart, SqlDbType.Date) { IsNullable = true };
			if (_insuranceDateStart == null)
				prm.Value = DBNull.Value;
			else
				prm.Value = _insuranceDateStart;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.InsuranceDateExpiration, SqlDbType.Date) { IsNullable = true };
			if (_insuranceDateExpiration == null)
				prm.Value = DBNull.Value;
			else
				prm.Value = _insuranceDateExpiration;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.SanitaryNumber, SqlDbType.NVarChar, 50) { IsNullable = true };
			if (string.IsNullOrEmpty(_sanitaryNumber))
				prm.Value = DBNull.Value;
			else
				prm.Value = _sanitaryNumber;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.SanitaryDateStart, SqlDbType.Date) { IsNullable = true };
			if (_sanitaryDateStart == null)
				prm.Value = DBNull.Value;
			else
				prm.Value = _sanitaryDateStart;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.SanitaryDateExpiration, SqlDbType.Date) { IsNullable = true };
			if (_sanitaryDateExpiration == null)
				prm.Value = DBNull.Value;
			else
				prm.Value = _sanitaryDateExpiration;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.TonnageKindId, SqlDbType.Int) { IsNullable = true };
			if (_tonnageKindId == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _tonnageKindId;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.TrailerUsedAutoId, SqlDbType.Int) { IsNullable = true };
			if (_trailerUsedAutoId == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _trailerUsedAutoId;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.InterceptedVolumesLeft, SqlDbType.Money) { IsNullable = true };
			if (_interceptedVolumesLeft == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _interceptedVolumesLeft;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.InterceptedVolumesRight, SqlDbType.Money) { IsNullable = true };
			if (_interceptedVolumesRight == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _interceptedVolumesRight;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.Pallet, SqlDbType.Int) { IsNullable = true };
			if (_pallet == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _pallet;
			sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.AdditionalFuelTank, SqlDbType.Bit)
			    {IsNullable = true, Value = _additionalFuelTank ? 1 : 0};
            sqlCmd.Parameters.Add(prm);

			prm = new SqlParameter(GlobalSqlParamNames.EquipmentId, SqlDbType.Int) { IsNullable = true };
			if (_equipmentId == 0)
				prm.Value = DBNull.Value;
			else
				prm.Value = _equipmentId;
			sqlCmd.Parameters.Add(prm);
        }

        #region IRelationSingle Members

        string IRelationSingle.Schema
        {
            get { return GlobalSchemaNames.Product; }
        }

        #endregion
    }
}
