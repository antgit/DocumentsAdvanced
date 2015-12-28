using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>��������� ������� "����������� ������� ���������"</summary>
    internal struct ProductUnitStruct
    {
        /// <summary>��������</summary>
        public decimal Divider;
        /// <summary>���������</summary>
        public decimal Multiplier;
        /// <summary>������������� ������</summary>
        public int ProductId;
        /// <summary>����������</summary>
        public int SortOrder;
        /// <summary>������������� ������� ���������</summary>
        public int UnitId;
    }

    /// <summary>
    /// ����������� ������� ��������� ������
    /// </summary>
    public sealed class ProductUnit : BaseCore<ProductUnit>, IEquatable<ProductUnit>
    {
        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>����������� �������, ������������� �������� 1</summary>
        public const int KINDVALUE_UNIT = 1;

        /// <summary>����������� �������, ������������� �������� 2097153</summary>
        public const int KINDID_UNIT = 2097153;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<ProductUnit>.Equals(ProductUnit other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>�����������</summary>
        public ProductUnit():base()
        {
            EntityId = (short)WhellKnownDbEntity.ProductUnit;
        }
        protected override void CopyValue(ProductUnit template)
        {
            base.CopyValue(template);
            Divider = template.Divider;
            Multiplier = template.Multiplier;
            ProductId = template.ProductId;
            SortOrder = template.SortOrder;
            UnitId = template.UnitId;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override ProductUnit Clone(bool endInit)
        {
            ProductUnit obj = base.Clone(false);
            obj.Divider = Divider;
            obj.Multiplier = Multiplier;
            obj.ProductId = ProductId;
            obj.SortOrder = SortOrder;
            obj.UnitId = UnitId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region ��������
        private int _productId;
        /// <summary>
        /// ������������� ������
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

        private Product _product;
        /// <summary>
        /// �����
        /// </summary>
        public Product Product
        {
            get
            {
                if (_productId == 0)
                    return null;
                if (_product == null)
                    _product = Workarea.Cashe.GetCasheData<Product>().Item(_productId);
                else if (_product.Id != _productId)
                    _product = Workarea.Cashe.GetCasheData<Product>().Item(_productId);
                return _product;
            }
            set
            {
                if (_product == value) return;
                OnPropertyChanging(GlobalPropertyNames.Product);
                _product = value;
                _productId = _product == null ? 0 : _product.Id;
                OnPropertyChanged(GlobalPropertyNames.Product);
            }
        }


        private int _unitId;
        /// <summary>
        /// ������������� ������� ���������
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

        private Unit _unit;
        /// <summary>
        /// ������� ���������
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

        private int _sortOrder;
        /// <summary>
        /// ����������
        /// </summary>
        public int SortOrder
        {
            get { return _sortOrder; }
            set
            {
                if (value == _sortOrder) return;
                OnPropertyChanging(GlobalPropertyNames.SortOrder);
                _sortOrder = value;
                OnPropertyChanged(GlobalPropertyNames.SortOrder);
            }
        }

        private decimal _divider;
        /// <summary>
        /// ��������
        /// </summary>
        public decimal Divider
        {
            get { return _divider; }
            set
            {
                if (value == _divider) return;
                OnPropertyChanging(GlobalPropertyNames.Divider);
                _divider = value;
                OnPropertyChanged(GlobalPropertyNames.Divider);
            }
        }
        private decimal _multiplier;
        /// <summary>
        /// ���������
        /// </summary>
        public decimal Multiplier
        {
            get { return _multiplier; }
            set
            {
                if (value == _multiplier) return;
                OnPropertyChanging(GlobalPropertyNames.Multiplier);
                _multiplier = value;
                OnPropertyChanged(GlobalPropertyNames.Multiplier);
            }
        } 
        #endregion

        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_divider != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Divider, XmlConvert.ToString(_divider));
            if (_multiplier != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Multiplier, XmlConvert.ToString(_multiplier));
            if (_productId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProductId, XmlConvert.ToString(_productId));
            if (_sortOrder != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SortOrder, XmlConvert.ToString(_sortOrder));
            if (_unitId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UnitId, XmlConvert.ToString(_unitId));
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Divider) != null)
                _divider = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Divider));
            if (reader.GetAttribute(GlobalPropertyNames.Multiplier) != null)
                _multiplier = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Multiplier));
            if (reader.GetAttribute(GlobalPropertyNames.ProductId) != null)
                _productId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProductId));
            if (reader.GetAttribute(GlobalPropertyNames.SortOrder) != null)
                _sortOrder = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SortOrder));
            if (reader.GetAttribute(GlobalPropertyNames.UnitId) != null)
                _unitId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UnitId));
        }
        #endregion

        #region ���������
        ProductUnitStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new ProductUnitStruct
                                  {
                                      Divider = _divider,
                                      Multiplier = _multiplier,
                                      ProductId = _productId,
                                      SortOrder = _sortOrder,
                                      UnitId = _unitId
                                  };
                return true;
            }
            return false;
        }

        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            Divider = _baseStruct.Divider;
            Multiplier = _baseStruct.Multiplier;
            ProductId = _baseStruct.ProductId;
            SortOrder = _baseStruct.SortOrder;
            UnitId = _baseStruct.UnitId;
            IsChanged = false;
        }
        #endregion
        /// <summary>�������� ������</summary>
        /// <param name="reader">������ SqlDataReader</param>
        /// <param name="endInit">������� ��������� ��������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _productId = reader.GetInt32(17);
                _unitId = reader.GetInt32(18);
                _sortOrder = reader.GetInt32(19);
                _divider = reader.GetDecimal(20);
                _multiplier = reader.GetDecimal(21);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>���������� �������� ���������� ��� �������� �������� ��� ����������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ����������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ProductId, SqlDbType.Int) {IsNullable = false, Value = _productId};
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.UnitId, SqlDbType.Int) {IsNullable = false, Value = _unitId};
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.Int) {IsNullable = false, Value = _sortOrder};
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.Divider, SqlDbType.Money) {IsNullable = false, Value = _divider};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Multiplier, SqlDbType.Money) { IsNullable = false, Value = _multiplier };
            sqlCmd.Parameters.Add(prm);

        }
        /// <summary>
        /// �������� ������������ ������� ��������� �����������
        /// </summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                Name = "����������� ������� ���������";
            if (_unitId == 0)
                throw new ValidateException("�� ������� ������� ���������");
            if (_productId == 0)
                throw new ValidateException("�� ������ �����");
            if (DatabaseId == 0)
                DatabaseId = Workarea.MyBranche.Id;
        }
    }
}
