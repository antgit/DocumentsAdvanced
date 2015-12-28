using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Набор правил"</summary>
    internal struct StorageCellStruct
    {
        /// <summary>Значение</summary>
        public int UnitId;
        /// <summary>Идентификатор библиотеки</summary>
        public decimal Qty;
        /// <summary>Имя типа</summary>
        public int StoreId;
    }

    /// <summary>
    /// Ячейка хранения товара
    /// </summary>
    /// <remarks>
    /// Ячейка хранения товара представляет собой элемент складской логистики.
    /// <para>При наличии лгистического управления ячейками хранения достигается:</para>
    /// <list type="bullet">
    /// <item>
    /// <description>Автоматическое снятие в программе остатков в ячейках при продаже
    /// товара </description></item></list>
    /// <para>порядок списания товара с ячеек должен проходить с учетом топологии склада
    /// – высвобождаются первыми нижние и ближайшие к зоне отгрузки места (в случае если
    /// товар размещен на нескольких этажах складских стеллаже</para>
    /// <list type="bullet">
    /// <item>
    /// <description>Контроль состояния загруженности мест хранения на складе в любой
    /// момент времени </description></item>
    /// <item>
    /// <description>Частичное проведение инвентаризации товара на складе без
    /// приостановки работы склада (с возможностью проведения инвентаризации как в
    /// разрезе товара, так и в разрезе мест хранения) </description></item>
    /// <item>
    /// <description>Контроль за перемещениями товара между ячейками размещения
    /// </description></item></list>
    /// <para>Ячейки хранения представляют собой элемент имеющий приявязку по следующим
    /// свойствам: собственно склад которому принадлежит ячейка хранения, единица
    /// измерения ячейки хранения, вместимость в текущей единице измерения. Текущая
    /// информация о загрузке ячеек осуществляется через объект StorageCellTurn -
    /// представляющий собой обороты по ячейке хранения.</para>
    /// <para>Основная работа с ячейками хранения осуществляется в документе
    /// &quot;Приход товара&quot; - в этом документе выполняется распределение по
    /// ячейкам хранения. В расходных документах выполняется снятие товара с ячеек
    /// хранения. В документе &quot;Внутренее перемещение&quot; выполняется перемещение
    /// товара по ячейкам хранения.</para>
    /// </remarks>
    public sealed class StorageCell : BaseCore<StorageCell>, IEquatable<StorageCell>,
        IChainsAdvancedList<StorageCell, Note>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Ячейка хранения, соответствует значению 1</summary>
        public const int KINDVALUE_STORAGECELL = 1;

        /// <summary>Ячейка хранения, соответствует значению 2555905</summary>
        public const int KINDID_STORAGECELL = 2555905;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<StorageCell>.Equals(StorageCell other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public StorageCell()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.StorageCell;
        }
        protected override void CopyValue(StorageCell template)
        {
            base.CopyValue(template);
            Qty = template.Qty;
            StoreId = template.StoreId;
            UnitId = template.UnitId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override StorageCell Clone(bool endInit)
        {
            StorageCell obj = base.Clone(false);
            obj.Qty = Qty;
            obj.StoreId = StoreId;
            obj.UnitId = UnitId;

            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства

        private int _unitId;
        /// <summary>
        /// Идентификатор единицы измерения
        /// </summary>
        /// <remarks>
        /// Принятие товара в ячейку осуществляется на основании текущей единицы измерения
        /// товара и производной единицы измерения товара - таким образом возможно
        /// полноценное заполнение ячейки только соответствующими товарами. <i>Например:
        /// </i>
        /// <para><i>   Основной единицей измерения товара является &quot;штука&quot;, и
        /// производной единицей является &quot;ящик&quot;, содержащий 20 штук - в ячейке
        /// хранения №1 имеющей единицу измерения 18 &quot;штук&quot; можно разместить
        /// только штучный товар, но не ящик, в ячейке хранения №2 имеющей вместимость 10
        /// ящиков можно разместить как ящики так и штуки. </i></para>
        /// <para><i>   При приходе товара и распределении по ячейкам выполняется
        /// автоматический подбор наиболее подходящей ячейки </i></para>
        /// <para><i>- если количество товара не превышает 18 штук - товар будет распределен
        /// в ячейку №1, </i></para>
        /// <para><i>-если количество товара превышает 1 ящик и поступило в ящике -
        /// распределение выполняется в ячейку №2.</i></para>
        /// <para><i>-если количество превышает 20 штук и поступило в штуках (22 штуки) : 18
        /// штук распределяются в ячейку №1 и 2 в ячейку №2</i></para>
        /// <para><i>- если количество превышает 20 штук и поступило как в ящиках так и в
        /// штуках распределение происходит как в ячейку №1 - только товар поступивший в
        /// штуках, так и в ячейку №2 - только товар поступивший в яшиках.</i></para>
        /// </remarks>
        public int UnitId
        {
            get { return _unitId; }
            set
            {
                if (value == _unitId) return;
                OnPropertyChanging(GlobalPropertyNames.UnitId);
                _unitId = value;
                OnPropertyChanged(GlobalPropertyNames.UnitId);
            }
        }


        private Unit _unit;
        /// <summary>
        /// Единица измерения
        /// </summary>
        public Unit Unit
        {
            get
            {
                if (_unitId == 0)
                    return null;
                if (_unit == null)
                    _unit = Workarea.Cashe.GetCasheData<Unit>().Item(_unitId);
                else if (_unit.Id != _unitId)
                    _unit = Workarea.Cashe.GetCasheData<Unit>().Item(_unitId);
                return _unit;
            }
            set
            {
                if (_unit == value) return;
                OnPropertyChanging(GlobalPropertyNames.Unit);
                _unit = value;
                _unitId = _unit == null ? 0 : _unit.Id;
                OnPropertyChanged(GlobalPropertyNames.Unit);
            }
        }
        

        private decimal _qty;
        /// <summary>
        /// Вместимость в текущей единице измерения
        /// </summary>
        public decimal Qty
        {
            get { return _qty; }
            set
            {
                if (value == _qty) return;
                OnPropertyChanging(GlobalPropertyNames.Qty);
                _qty = value;
                OnPropertyChanged(GlobalPropertyNames.Qty);
            }
        }

        private int _storeId;
        /// <summary>
        /// Идентификатор склада
        /// </summary>
        public int StoreId
        {
            get { return _storeId; }
            set
            {
                if (value == _storeId) return;
                OnPropertyChanging(GlobalPropertyNames.StoreId);
                _storeId = value;
                OnPropertyChanged(GlobalPropertyNames.StoreId);
            }
        }


        private Agent _store;
        /// <summary>
        /// Склад
        /// </summary>
        public Agent Store
        {
            get
            {
                if (_storeId == 0)
                    return null;
                if (_store == null)
                    _store = Workarea.Cashe.GetCasheData<Agent>().Item(_storeId);
                else if (_store.Id != _storeId)
                    _store = Workarea.Cashe.GetCasheData<Agent>().Item(_storeId);
                return _store;
            }
            set
            {
                if (_store == value) return;
                OnPropertyChanging(GlobalPropertyNames.Store);
                _store = value;
                _storeId = _store == null ? 0 : _store.Id;
                OnPropertyChanged(GlobalPropertyNames.Store);
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

            if (_unitId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UnitId, XmlConvert.ToString(_unitId));
            if (_qty != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Qty, XmlConvert.ToString(_qty));
            if (_storeId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.StoreId, XmlConvert.ToString(_storeId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.UnitId) != null)
                _unitId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UnitId));
            if (reader.GetAttribute(GlobalPropertyNames.Qty) != null)
                _qty = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Qty));
            if (reader.GetAttribute(GlobalPropertyNames.StoreId) != null)
                _storeId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.StoreId));
        }
        #endregion

        #region Состояние
        StorageCellStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new StorageCellStruct
                {
                    UnitId = _unitId,
                    Qty = _qty,
                    StoreId = _storeId
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            UnitId = _baseStruct.UnitId;
            Qty = _baseStruct.Qty;
            StoreId = _baseStruct.StoreId;

            IsChanged = false;
        }
        #endregion

        #region IChainsAdvancedList<StorageCell,Note> Members

        List<IChainAdvanced<StorageCell, Note>> IChainsAdvancedList<StorageCell, Note>.GetLinks()
        {
            return ChainAdvanced<StorageCell, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<StorageCell, Note>> IChainsAdvancedList<StorageCell, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<StorageCell, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<StorageCell, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<StorageCell, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<StorageCell, Note>.GetChainView()
        {
            return ChainValueView.GetView<StorageCell, Note>(this);
        }
        #endregion

        public List<Country> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Country> filter = null,
            int? unitId = null, decimal? qty=null, int? storeId = null,
            bool useAndFilter = false)
        {
            Country item = new Country { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Country> collection = new List<Country>();
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
                       //int? unitId = null, decimal? qty=null, int? storeId = null, string activityName = null,

                        if (unitId.HasValue && unitId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.UnitId, SqlDbType.Int).Value = unitId.Value;
                        if (qty.HasValue && qty != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.Qty, SqlDbType.Money).Value = qty.Value;
                        if (storeId.HasValue && storeId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.StoreId, SqlDbType.Int).Value = storeId.Value;
        
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Country { Workarea = Workarea };
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
    /// <summary>
    /// Движение товара по ячейкам хранения
    /// </summary>
    public sealed class StoraceCellTurn : BaseCoreObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public StoraceCellTurn():base()
        {
            EntityId = (short)WhellKnownDbEntity.StorageCellTurn;
        }
        #region Свойства

        private int _rowId;
        /// <summary>
        /// Идентификатор строки документа в разделе "Управление товарными запасами"
        /// </summary>
        public int RowId
        {
            get { return _rowId; }
            set
            {
                if (value == _rowId) return;
                OnPropertyChanging(GlobalPropertyNames.RowId);
                _rowId = value;
                OnPropertyChanged(GlobalPropertyNames.RowId);
            }
        }


        private int _cellId;
        /// <summary>
        /// Идентификатор ячейки хранения
        /// </summary>
        public int CellId
        {
            get { return _cellId; }
            set
            {
                if (value == _cellId) return;
                OnPropertyChanging(GlobalPropertyNames.CellId);
                _cellId = value;
                OnPropertyChanged(GlobalPropertyNames.CellId);
            }
        }


        private decimal _qty;
        /// <summary>
        /// Количество
        /// </summary>
        /// <remarks>Количество имеет только положительное значение, собственно текущая операция означает знак операции: 
        /// - приход товара означает "+"
        /// - расход или возврат товара означает "-"
        /// - внутренее перемещение означает "+" для предприятия получателя и "-" для предприятия отправителя
        /// </remarks>
        public decimal Qty
        {
            get { return _qty; }
            set
            {
                if (value == _qty) return;
                OnPropertyChanging(GlobalPropertyNames.Qty);
                _qty = value;
                OnPropertyChanged(GlobalPropertyNames.Qty);
            }
        }
        private int _ownId;
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public int OwnId
        {
            get { return _ownId; }
            set
            {
                if (value == _ownId) return;
                OnPropertyChanging(GlobalPropertyNames.OwnId);
                _ownId = value;
                OnPropertyChanged(GlobalPropertyNames.OwnId);
            }
        }
        
        
        #endregion
    }
}