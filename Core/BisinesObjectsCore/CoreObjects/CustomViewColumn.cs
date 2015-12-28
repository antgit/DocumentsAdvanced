using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Колонка списка"</summary>
    internal struct CustomViewColumnStruct
    {
        /// <summary>Выравнивание в колонке</summary>
        public int Alignment;
        /// <summary>Авто размер</summary>
        public int AutoSizeMode;
        /// <summary>Идентификатор списка, которому принадлежит колонка</summary>
        public int CustomViewListId;
        /// <summary>Наименование столбца данных</summary>
        public string DataProperty;
        /// <summary>Тип данных</summary>
        public string DataType;
        /// <summary>Отображать текст заголовка</summary>
        public bool DisplayHeader;
        /// <summary>Возможность редактирования</summary>
        public bool EditAble;
        /// <summary>Форматирование</summary>
        public string Format;
        /// <summary>Закрепление</summary>
        public bool Frozen;
        /// <summary>Индекс группировки</summary>
        public int GroupIndex;
        /// <summary>Номер колонки в списке по умолчанию</summary>
        public Int16 OrderNo;
        /// <summary>Видимость колонки</summary>
        public bool Visible;
        /// <summary>Ширина</summary>
        public int With;
        /// <summary>Формула для вычисляемой колонки</summary>
        public string Formula;
    }
    /// <summary>Колонка списка</summary>
    /// <remarks>Значение свойства KindValue: 1-колонка, 2-вычисляемая колонка</remarks>
    public sealed class CustomViewColumn : BaseCore<CustomViewColumn>, IEquatable<CustomViewColumn>
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Колонка, соответствует значению 1</summary>
        public const int KINDVALUE_COLUMN = 1;
        /// <summary>Вычисляемая колонка, соответствует значению 2</summary>
        public const int KINDVALUE_COMPUTED = 2;

        /// <summary>Колонка, соответствует значению 1441793</summary>
        public const int KINDID_COLUMN = 1441793;
        /// <summary>Вычисляемая колонка, соответствует значению 1441794</summary>
        public const int KINDID_COMPUTED = 1441794;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<CustomViewColumn>.Equals(CustomViewColumn other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public CustomViewColumn()
            : base()
        {
            EntityId = 22;
        }
        protected override void CopyValue(CustomViewColumn template)
        {
            base.CopyValue(template);
            Alignment = template.Alignment;
            AutoSizeMode = template.AutoSizeMode;
            CustomViewListId = template.CustomViewListId;
            DataProperty = template.DataProperty;
            DataType = template.DataType;
            DisplayHeader = template.DisplayHeader;
            EditAble = template.EditAble;
            Format = template.Format;
            Formula = template.Formula;
            Frozen = template.Frozen;
            GroupIndex = template.GroupIndex;
            OrderNo = template.OrderNo;
            Visible = template.Visible;
            With = template.With;

        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override CustomViewColumn Clone(bool endInit)
        {
            CustomViewColumn obj = base.Clone(false);
            obj.Alignment = Alignment;
            obj.AutoSizeMode = AutoSizeMode;
            obj.CustomViewListId = CustomViewListId;
            obj.DataProperty = DataProperty;
            obj.DataType = DataType;
            obj.DisplayHeader = DisplayHeader;
            obj.EditAble = EditAble;
            obj.Format = Format;
            obj.Formula = Formula;
            obj.Frozen = Frozen;
            obj.GroupIndex = GroupIndex;
            obj.OrderNo = OrderNo;
            obj.Visible = Visible;
            obj.With = With;

            if (endInit)
                OnEndInit();
            return obj;
        }
        /// <summary>
        /// Сравнение объекта для службы обмена данными
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange(CustomViewColumn value)
        {
            if (!base.CompareExchange(value))
            {
                return false;
            }

            if (value.Alignment != Alignment)
                return false;
            if (value.AutoSizeMode != AutoSizeMode)
                return false;
            if (!StringNullCompare(value.DataProperty, DataProperty))
                return false;
            if (!StringNullCompare(value.DataType, DataType))
                return false;
            if (value.DisplayHeader != DisplayHeader)
                return false;
            if (value.EditAble != EditAble)
                return false;
            if (!StringNullCompare(value.Format, Format))
                return false;
            if (!StringNullCompare(value.Formula, Formula))
                return false;
            if (value.Frozen != Frozen)
                return false;
            if (value.GroupIndex != GroupIndex)
                return false;
            if (value.OrderNo != OrderNo)
                return false;
            if (value.Visible != Visible)
                return false;
            if (value.With != With)
                return false;

            return true;
        }

        #region Свойства

        private string _formula;
        /// <summary>
        /// Формула для вычисляемой колонки
        /// </summary>
        public string Formula
        {
            get { return _formula; }
            set
            {
                if (value == _formula) return;
                OnPropertyChanging(GlobalPropertyNames.Formula);
                _formula = value;
                OnPropertyChanged(GlobalPropertyNames.Formula);
            }
        }
        
        private string _dataProperty;
        /// <summary>Наименование столбца данных</summary>
        public string DataProperty
        {
            get { return _dataProperty; }
            set
            {
                if (value == _dataProperty) return;
                OnPropertyChanging(GlobalPropertyNames.DataProperty);
                _dataProperty = value;
                OnPropertyChanged(GlobalPropertyNames.DataProperty);
            }
        }
        private int _customViewListId;
        /// <summary>Идентификатор списка, которому принадлежит колонка</summary>
        public int CustomViewListId
        {
            get { return _customViewListId; }
            set
            {
                if (value == _customViewListId) return;
                OnPropertyChanging(GlobalPropertyNames.CustomViewListId);
                _customViewListId = value;
                OnPropertyChanged(GlobalPropertyNames.CustomViewListId);
            }
        }
        private CustomViewList _customViewList;
        /// <summary>Список которому принадлежит колонка</summary>
        public CustomViewList CustomViewList
        {
            get
            {
                if (_customViewListId == 0)
                    return null;
                if (_customViewList == null)
                    _customViewList = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_customViewListId);
                else if (_customViewList.Id != _customViewListId)
                    _customViewList = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_customViewListId);
                return _customViewList;
            }
            set
            {
                if (_customViewList == value) return;
                OnPropertyChanging(GlobalPropertyNames.CustomViewList);
                _customViewList = value;
                _customViewListId = _customViewList == null ? 0 : _customViewList.Id;
                OnPropertyChanged(GlobalPropertyNames.CustomViewList);
            }
        }

        private string _dataType;
        /// <summary>Тип данных</summary>
        public string DataType
        {
            get { return _dataType; }
            set
            {
                if (value == _dataType) return;
                OnPropertyChanging(GlobalPropertyNames.DataType);
                _dataType = value;
                OnPropertyChanged(GlobalPropertyNames.DataType);
            }
        }
        private int _with;
        /// <summary>Ширина</summary>
        public int With
        {
            get { return _with; }
            set
            {
                if (value == _with) return;
                OnPropertyChanging(GlobalPropertyNames.With);
                _with = value;
                OnPropertyChanged(GlobalPropertyNames.With);
            }
        }

        private bool _visible;
        /// <summary>Видимость колонки</summary>
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (value == _visible) return;
                OnPropertyChanging(GlobalPropertyNames.Visible);
                _visible = value;
                OnPropertyChanged(GlobalPropertyNames.Visible);
            }
        }
        private string _format;
        /// <summary>Форматирование</summary>
        public string Format
        {
            get { return _format; }
            set
            {
                if (value == _format) return;
                OnPropertyChanging(GlobalPropertyNames.Format);
                _format = value;
                OnPropertyChanged(GlobalPropertyNames.Format);
            }
        }

        private Int16 _orderNo;
        /// <summary>
        /// Номер колонки в списке по умолчанию 
        /// </summary>
        public Int16 OrderNo
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

        private bool _frozen;
        /// <summary>
        /// Закрепление
        /// </summary>
        public bool Frozen
        {
            get { return _frozen; }
            set
            {
                if (_frozen == value) return;
                OnPropertyChanging(GlobalPropertyNames.Frozen);
                _frozen = value;
                OnPropertyChanged(GlobalPropertyNames.Frozen);
            }
        }

        private int _autoSizeMode;
        /// <summary>
        /// Авто размер
        /// </summary>
        public int AutoSizeMode
        {
            get { return _autoSizeMode; }
            set
            {
                if (_autoSizeMode == value) return;
                OnPropertyChanging(GlobalPropertyNames.AutoSizeMode);
                _autoSizeMode = value;
                OnPropertyChanged(GlobalPropertyNames.AutoSizeMode);
            }
        }

        private bool _displayHeader;
        /// <summary>
        /// Отображать текст заголовка
        /// </summary>
        public bool DisplayHeader
        {
            get { return _displayHeader; }
            set
            {
                if (_displayHeader == value) return;
                OnPropertyChanging(GlobalPropertyNames.DisplayHeader);
                _displayHeader = value;
                OnPropertyChanged(GlobalPropertyNames.DisplayHeader);
            }
        }

        private int _alignment;
        /// <summary>
        /// Выравнивание в колонке
        /// </summary>
        /// <remarks>"По умолчанию" = 0, "Влево" = 16, "По центру" = 32, "Вправо" = 64</remarks>
        public int Alignment
        {
            get { return _alignment; }
            set
            {
                if (value == _alignment) return;
                OnPropertyChanging(GlobalPropertyNames.Alignment);
                _alignment = value;
                OnPropertyChanged(GlobalPropertyNames.Alignment);
            }
        }


        private int _groupIndex=-1;
        /// <summary>
        /// Индекс группировки
        /// </summary>
        public int GroupIndex
        {
            get { return _groupIndex; }
            set
            {
                if (value == _groupIndex) return;
                OnPropertyChanging(GlobalPropertyNames.GroupIndex);
                _groupIndex = value;
                OnPropertyChanged(GlobalPropertyNames.GroupIndex);
            }
        }

        private bool _editAble;
        /// <summary>
        /// Возможность редактирования
        /// </summary>
        public bool EditAble
        {
            get { return _editAble; }
            set
            {
                if (_editAble == value) return;
                OnPropertyChanging(GlobalPropertyNames.EditAble);
                _editAble = value;
                OnPropertyChanged(GlobalPropertyNames.EditAble);
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

            if (_alignment != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Alignment, XmlConvert.ToString(_alignment));
            if (_autoSizeMode!=0)
                writer.WriteAttributeString(GlobalPropertyNames.AutoSizeMode, XmlConvert.ToString(_autoSizeMode));
            if (_customViewListId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CustomViewListId, XmlConvert.ToString(_customViewListId));
            if (!string.IsNullOrEmpty(_dataProperty))
                writer.WriteAttributeString(GlobalPropertyNames.DataProperty, _dataProperty);
            if (!string.IsNullOrEmpty(_dataType))
                writer.WriteAttributeString(GlobalPropertyNames.DataType, _dataType);
            if (_displayHeader)
                writer.WriteAttributeString(GlobalPropertyNames.DisplayHeader, XmlConvert.ToString(_displayHeader));
            if (_editAble)
                writer.WriteAttributeString(GlobalPropertyNames.EditAble, XmlConvert.ToString(_editAble));
            if (!string.IsNullOrEmpty(_format))
                writer.WriteAttributeString(GlobalPropertyNames.Format, _format);
            if (_frozen)
                writer.WriteAttributeString(GlobalPropertyNames.Frozen, XmlConvert.ToString(_frozen));
            if (_groupIndex!=0)
                writer.WriteAttributeString(GlobalPropertyNames.GroupIndex, XmlConvert.ToString(_groupIndex));
            if (_orderNo!=0)
                writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_orderNo));
            if (_visible)
                writer.WriteAttributeString(GlobalPropertyNames.Visible, XmlConvert.ToString(_visible));
            if (_with!=0)
                writer.WriteAttributeString(GlobalPropertyNames.With, XmlConvert.ToString(_with));
            if (!string.IsNullOrEmpty(Formula))
                writer.WriteAttributeString(GlobalPropertyNames.Formula, _formula);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Alignment) != null)
                _alignment = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Alignment));
            if (reader.GetAttribute(GlobalPropertyNames.AutoSizeMode) != null)
                _autoSizeMode = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AutoSizeMode));
            if (reader.GetAttribute(GlobalPropertyNames.CustomViewListId) != null)
                _customViewListId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CustomViewListId));
            if (reader.GetAttribute(GlobalPropertyNames.DataProperty) != null)
                _dataProperty = reader[GlobalPropertyNames.DataProperty];
            if (reader.GetAttribute(GlobalPropertyNames.DataType) != null)
                _dataType = reader[GlobalPropertyNames.DataType];
            if (reader.GetAttribute(GlobalPropertyNames.DisplayHeader) != null)
                _displayHeader = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.DisplayHeader));
            if (reader.GetAttribute(GlobalPropertyNames.EditAble) != null)
                _editAble = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.EditAble));
            if (reader.GetAttribute(GlobalPropertyNames.Format) != null)
                _format = reader[GlobalPropertyNames.Format];
            if (reader.GetAttribute(GlobalPropertyNames.Frozen) != null)
                _frozen = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Frozen));
            if (reader.GetAttribute(GlobalPropertyNames.GroupIndex) != null)
                _groupIndex = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.GroupIndex));
            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _orderNo = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.OrderNo));
            if (reader.GetAttribute(GlobalPropertyNames.Visible) != null)
                _visible = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Visible));
            if (reader.GetAttribute(GlobalPropertyNames.With) != null)
                _with = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.With));
            if (reader.GetAttribute(GlobalPropertyNames.Formula) != null)
                _formula = reader[GlobalPropertyNames.Formula];
        }
        #endregion

        #region Состояние
        CustomViewColumnStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new CustomViewColumnStruct
                {
                    Alignment = _alignment,
                    AutoSizeMode = _autoSizeMode,
                    CustomViewListId = _customViewListId,
                    DataProperty = _dataProperty,
                    DataType = _dataType,
                    DisplayHeader = _displayHeader,
                    EditAble = _editAble,
                    Format = _format,
                    Frozen = _frozen,
                    GroupIndex = _groupIndex,
                    OrderNo = _orderNo,
                    Visible = _visible,
                    With = _with,
                    Formula = _formula
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
            Alignment = _baseStruct.Alignment;
            AutoSizeMode = _baseStruct.AutoSizeMode;
            CustomViewListId = _baseStruct.CustomViewListId;
            DataProperty = _baseStruct.DataProperty;
            DataType = _baseStruct.DataType;
            DisplayHeader = _baseStruct.DisplayHeader;
            EditAble = _baseStruct.EditAble;
            Format = _baseStruct.Format;
            Frozen = _baseStruct.Frozen;
            GroupIndex = _baseStruct.GroupIndex;
            OrderNo = _baseStruct.OrderNo;
            Visible = _baseStruct.Visible;
            With = _baseStruct.With;
            Formula = _baseStruct.Formula;
            IsChanged = false;
        }
        #endregion

        /// <summary>Проверка соответствия объекта бизнес правилам</summary>
        /// <remarks>Метод выполняет проверку наименования объекта <see cref="BaseCore{T}.Name"/> на предмет null, <see cref="string.Empty"/> и максимальную длину не более 255 символов.
        /// Объект требует наличия свойства DataProperty, DataType и СustomViewListId отличное от 0.
        /// </remarks>
        /// <returns><c>true</c> - если объект соответствует бизнес правилам, <c>false</c> в противном случае</returns>
        /// <exception cref="ValidateException">Если объект не соответствует текущим правилам</exception>
        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(_dataProperty))
                throw new ValidateException("Не указано свойство или поле данных");
            if (string.IsNullOrEmpty(_dataType))
                throw new ValidateException("Не указан тип данных");
            if (_customViewListId == 0)
                throw new ValidateException("Не указан список");
        }
        #region База данных
        /// <summary>Загрузить экземпляр из базы данных</summary>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _customViewListId = reader.GetInt32(17);
                _dataProperty = reader.GetString(18);
                _dataType = reader.GetString(19);
                _with = reader.GetInt32(20);
                _visible = reader.GetBoolean(21);
                _format = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                _orderNo = reader.GetInt16(23);
                _frozen = reader.GetBoolean(24);
                _autoSizeMode = reader.GetInt32(25);
                _displayHeader = reader.GetBoolean(26);
                _alignment = reader.GetInt32(27);
                _groupIndex = reader.GetInt32(28);
                _editAble = reader.GetBoolean(29);
                _formula = reader.IsDBNull(30) ? string.Empty : reader.GetString(30);
                if (CustomViewList != null && CustomViewList._columns != null)
                {
                    if (CustomViewList._columns.Contains(this))
                    {
                        int idx = CustomViewList._columns.IndexOf(this);
                        CustomViewList._columns[idx] = this;
                    }
                }
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
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.DataProperty, SqlDbType.NVarChar, 128) {IsNullable = false, Value = _dataProperty};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.CustomViewListId, SqlDbType.Int) {IsNullable = false, Value = _customViewListId};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.DataType, SqlDbType.NVarChar, 128) {IsNullable = false, Value = _dataType};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.With, SqlDbType.Int) {IsNullable = false, Value = _with};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Visible, SqlDbType.Bit) {IsNullable = false, Value = _visible};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Format, SqlDbType.NVarChar, 128) {IsNullable = false};
            if (!string.IsNullOrEmpty(_format))
                prm.Value = _format;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.SmallInt) {IsNullable = false, Value = _orderNo};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Frozen, SqlDbType.Bit) {IsNullable = false, Value = _frozen};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AutoSizeMode, SqlDbType.Int) {IsNullable = false, Value = _autoSizeMode};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DisplayHeader, SqlDbType.Bit) {IsNullable = false, Value = _displayHeader};
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Alignment, SqlDbType.Int) { IsNullable = false, Value = _alignment };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.GroupIndex, SqlDbType.Int) { IsNullable = false, Value = _groupIndex };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EditAble, SqlDbType.Bit) { IsNullable = false, Value = _editAble };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Formula, SqlDbType.NVarChar, 255) { IsNullable = false };
            if (!string.IsNullOrEmpty(_formula))
                prm.Value = _formula;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion
    }
}
