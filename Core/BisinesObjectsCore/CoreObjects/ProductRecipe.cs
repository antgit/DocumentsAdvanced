using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;


namespace BusinessObjects
{
    /// <summary>Структура объекта "Состав объект учета"</summary>
    internal struct ProductRecipeStruct
    {
        /// <summary>Идентификатор корреспондента</summary>
        public int AgentId;
        /// <summary>Номер в списке</summary>
        public int OrderNo;
        /// <summary>Процент</summary>
        public decimal Percent;
        /// <summary>Идентификатор товара</summary>
        public int ProductId;
        /// <summary>Количество</summary>
        public decimal Qty;
        /// <summary>Идентификатор состава</summary>
        public int RecipeId;
        /// <summary>Идентификатор единицы измерения</summary>
        public int UnitId;
    }
    /// <summary>
    /// "Состав объект учета" - рецепта для товара, комплектация
    /// </summary>
    public sealed class ProductRecipe : BaseCore<ProductRecipe>, IEquatable<ProductRecipe>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Производная единица, соответствует значению 1</summary>
        public const int KINDVALUE_PRODUCTRECIPE = 1;

        /// <summary>Производная единица, соответствует значению 786433</summary>
        public const int KINDID_PRODUCTRECIPE = 786433;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<ProductRecipe>.Equals(ProductRecipe other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public ProductRecipe():base()
        {
            EntityId = (short)WhellKnownDbEntity.ProductRecipeItem;
        }
        protected override void CopyValue(ProductRecipe template)
        {
            base.CopyValue(template);
            AgentId = template.AgentId;
            OrderNo = template.OrderNo;
            Percent = template.Percent;
            ProductId = template.ProductId;
            Qty = template.Qty;
            RecipeId = template.RecipeId;
            UnitId = template.UnitId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override ProductRecipe Clone(bool endInit)
        {
            ProductRecipe obj = base.Clone(false);
            obj.AgentId = AgentId;
            obj.OrderNo = OrderNo;
            obj.Percent = Percent;
            obj.ProductId = ProductId;
            obj.Qty = Qty;
            obj.RecipeId = RecipeId;
            obj.UnitId = UnitId;
            
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства
        private int _recipeId;
        /// <summary>
        /// Идентификатор состава
        /// </summary>
        public int RecipeId
        {
            get { return _recipeId; }
            set
            {
                if (value == _recipeId) return;
                OnPropertyChanging(GlobalPropertyNames.RecipeId);
                _recipeId = value;
                OnPropertyChanged(GlobalPropertyNames.RecipeId);
            }
        }
        private int _productId;
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int ProductId
        {
            get { return _productId; }
            set
            {
                if (value == _productId) return;
                OnPropertyChanging(GlobalPropertyNames.ProductId);
                _productId = value;
                OnPropertyChanged(GlobalPropertyNames.ProductId);
            }
        }

        private int _unitId;
        /// <summary>
        /// Идентификатор единицы измерения
        /// </summary>
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
        private decimal _qty;
        /// <summary>
        /// Количество
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

        private int _orderNo;
        /// <summary>
        /// Номер в списке
        /// </summary>
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

        private int _agentId;
        /// <summary>
        /// Идентификатор корреспондента
        /// </summary>
        public int AgentId
        {
            get { return _agentId; }
            set
            {
                if (value == _agentId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentId);
                _agentId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentId);
            }
        }

        private decimal _percent;
        /// <summary>
        /// Процент
        /// </summary>
        public decimal Percent
        {
            get { return _percent; }
            set
            {
                if (value == _percent) return;
                OnPropertyChanging(GlobalPropertyNames.Percent);
                _percent = value;
                OnPropertyChanged(GlobalPropertyNames.Percent);
            }
        } 
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_agentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentId, XmlConvert.ToString(_agentId));
            if (_orderNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_orderNo));
            if (_percent != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Percent, XmlConvert.ToString(_percent));
            if (_productId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProductId, XmlConvert.ToString(_productId));
            if (_qty != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Qty, XmlConvert.ToString(_qty));
            if (_unitId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UnitId, XmlConvert.ToString(_unitId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.AgentId) != null)
                _agentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentId));
            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _orderNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OrderNo));
            if (reader.GetAttribute(GlobalPropertyNames.Percent) != null)
                _percent = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Percent));
            if (reader.GetAttribute(GlobalPropertyNames.ProductId) != null)
                _productId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProductId));
            if (reader.GetAttribute(GlobalPropertyNames.Qty) != null)
                _qty = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Qty));
            if (reader.GetAttribute(GlobalPropertyNames.UnitId) != null)
                _unitId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UnitId));
        }
        #endregion

        #region Состояние
        ProductRecipeStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new ProductRecipeStruct
                                  {
                                      AgentId = _agentId,
                                      OrderNo = _orderNo,
                                      Percent = _percent,
                                      ProductId = _productId,
                                      Qty = _qty,
                                      RecipeId = _recipeId,
                                      UnitId = _recipeId
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
            AgentId = _baseStruct.AgentId;
            OrderNo = _baseStruct.OrderNo;
            Percent = _baseStruct.Percent;
            ProductId = _baseStruct.ProductId;
            Qty = _baseStruct.Qty;
            RecipeId = _baseStruct.RecipeId;
            UnitId = _baseStruct.UnitId;
            IsChanged = false;
        }
        #endregion
        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if (_unitId == 0)
                throw new ValidateException("Не указана единица измерения");
            if (_recipeId == 0)
                throw new ValidateException("Не указана рецептура");
            if (_productId == 0)
                throw new ValidateException("Не указан товар");
            
        }
        #region База данных
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _recipeId = reader.GetInt32(17);
                _productId = reader.GetInt32(18);
                _unitId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _qty = reader.GetDecimal(20);
                _orderNo = reader.GetInt32(21);
                _agentId = reader.IsDBNull(22) ? 0 : reader.GetInt32(22);
                _percent = reader.IsDBNull(22) ? 0 : reader.GetDecimal(22);
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
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.RecipeId, SqlDbType.Int) {IsNullable = false, Value = _recipeId};
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ProductId, SqlDbType.Int) {IsNullable = false, Value = _productId};
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.UnitId, SqlDbType.Int) {IsNullable = true};
            if (_unitId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _unitId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.Int) {IsNullable = false, Value = _orderNo};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.AgentId, SqlDbType.Int) { IsNullable = true };
            if (_agentId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _agentId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Percent, SqlDbType.Money) {IsNullable = true};
            if (_percent == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _percent;
            sqlCmd.Parameters.Add(prm);
        }
    	#endregion    
    }
}
