using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace BusinessObjects.Documents
{
    internal struct BaseStructDocumentDetailSale
    {
        /// <summary>������������� ���� ������ ������</summary>
        public Int32 Kind;
        /// <summary>������������� ������</summary>
        public int ProductId;
        /// <summary>������������� ������� ���������</summary>
        public int UnitId;
        /// <summary>����</summary>
        public decimal Price;
        /// <summary>����������</summary>
        public decimal Qty;
        /// <summary>����������</summary>
        public string Memo;
        /// <summary>������������� ����������� ������� ���������</summary>
        public int FUnitId;
        /// <summary>���������� � ����������� ������� ���������</summary>
        public decimal FQty;
        /// <summary>������������� ������ ������</summary>
        public int TaxAnaliticId;
        /// <summary>����� ���</summary>
        public decimal SummaTax;
        /// <summary>����� ��������</summary>
        public decimal SummaExp;
        /// <summary>������������� ����� ��������</summary>
        public decimal SummaIn;
        /// <summary>����� �������� �� ������ ���������� �������</summary>
        public decimal SummaLastIn;
        /// <summary>������ � % ���������</summary>
        public decimal Discount;
        /// <summary>����������� ����������</summary>
        public decimal QtyFact;
        /// <summary>����������� �����</summary>
        public decimal SummaFact;
        /// <summary>���������� � ������� ������� ���������, ��� ������������� ����������� �������</summary>
        public decimal QtyBase;

    }
    
    /// <summary>
    /// ����������� ��������� � ������� "���������� ��������� ��������"
    /// </summary>
    public class DocumentDetailSale : DocumentBaseDetail, IEditableObject, IChainsAdvancedList<DocumentDetailSale, FileData>
    {
        /// <summary>
        /// �����������
        /// </summary>
        public DocumentDetailSale()
            : base()
        {
            _entityId = 2;

        }
        #region ��������
        /// <summary>
        /// �������� ��������
        /// </summary>
        public DocumentSales Document { get; set; }


        private decimal _qtyBase;
        /// <summary>
        /// ���������� � ������� ������� ���������, ��� ������������� ����������� �������
        /// </summary>
        public decimal QtyBase
        {
            get { return _qtyBase; }
            set
            {
                if (value == _qtyBase) return;
                OnPropertyChanging(GlobalPropertyNames.QtyBase);
                _qtyBase = value;
                OnPropertyChanged(GlobalPropertyNames.QtyBase);
            }
        }

        private decimal _qtyFact;
        /// <summary>
        /// ����������� ����������
        /// </summary>
        /// <remarks>������������ � ���������� ��������������</remarks>
        public decimal QtyFact
        {
            get { return _qtyFact; }
            set
            {
                if (value == _qtyFact) return;
                OnPropertyChanging(GlobalPropertyNames.QtyFact);
                _qtyFact = value;
                OnPropertyChanged(GlobalPropertyNames.QtyFact);
            }
        }

        private decimal _summaFact;
        /// <summary>
        /// ����������� �����
        /// </summary>
        /// <remarks>������������ � ���������� ��������������</remarks>
        public decimal SummaFact
        {
            get { return _summaFact; }
            set
            {
                if (value == _summaFact) return;
                OnPropertyChanging(GlobalPropertyNames.SummaFact);
                _summaFact = value;
                OnPropertyChanged(GlobalPropertyNames.SummaFact);
            }
        }
        
        //private int _kind;
        ///// <summary>
        ///// ��� �������� ��������
        ///// </summary>
        ///// <remarks>������� ���� �������� ��������: 
        ///// 0-������, 
        ///// 1-������, 
        ///// 2-�����������, 
        ///// 3-������� ������ ����������,
        ///// 4-������� ������ �� ����������</remarks>
        //public int Kind
        //{
        //    get { return _kind; }
        //    set
        //    {
        //        if (value != _kind)
        //        {
        //            OnPropertyChanging("KindValue");
        //            _kind = value;
        //            OnPropertyChanged("KindValue");
        //        }
        //    }
        //}

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

        private decimal _price;
        /// <summary>
        /// ����
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price == value) return;
                OnPropertyChanging(GlobalPropertyNames.Price);
                _price = value;
                OnPropertyChanged(GlobalPropertyNames.Price);
            }
        }

        private decimal _summa;
        /// <summary>
        /// �����
        /// </summary>
        public decimal Summa
        {
            get { return _summa; }
            set
            {
                if (value == _summa) return;
                OnPropertyChanging(GlobalPropertyNames.Summa);
                _summa = value;
                OnPropertyChanged(GlobalPropertyNames.Summa);
            }
        }

        private decimal _qty;
        /// <summary>
        /// ����������
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

        private string _memo;
        /// <summary>
        /// ����������
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

        private int _taxAnaliticId;
        /// <summary>
        /// ������������� ������ ������
        /// </summary>
        public int TaxAnaliticId
        {
            get { return _taxAnaliticId; }
            set
            {
                if (_taxAnaliticId == value) return;
                OnPropertyChanging(GlobalPropertyNames.TaxAnaliticId);
                _taxAnaliticId = value;
                OnPropertyChanged(GlobalPropertyNames.TaxAnaliticId);
            }
        }

        private decimal _fQty;
        /// <summary>
        /// ���������� � ����������� ������� ���������
        /// </summary>
        public decimal FQty
        {
            get { return _fQty; }
            set
            {
                if (value == _fQty) return;
                OnPropertyChanging(GlobalPropertyNames.FQty);
                _fQty = value;
                OnPropertyChanged(GlobalPropertyNames.FQty);
            }
        }

        private int _fUnitId;
        /// <summary>
        /// ������������� ����������� ������� ���������
        /// </summary>
        public int FUnitId
        {
            get { return _fUnitId; }
            set
            {
                if (value == _fUnitId) return;
                OnPropertyChanging(GlobalPropertyNames.FUnitId);
                _fUnitId = value;
                OnPropertyChanged(GlobalPropertyNames.FUnitId);
            }
        }

        private decimal _summaTax;
        /// <summary>
        /// ����� ���
        /// </summary>
        public decimal SummaTax
        {
            get { return _summaTax; }
            set
            {
                if (value == _summaTax) return;
                OnPropertyChanging(GlobalPropertyNames.SummaTax);
                _summaTax = value;
                OnPropertyChanged(GlobalPropertyNames.SummaTax);
            }
        }


        private decimal _summaExp;
        /// <summary>
        /// ����� ��������
        /// </summary>
        public decimal SummaExp
        {
            get { return _summaExp; }
            set
            {
                if (value == _summaExp) return;
                OnPropertyChanging(GlobalPropertyNames.SummaExp);
                _summaExp = value;
                OnPropertyChanged(GlobalPropertyNames.SummaExp);
            }
        }


        private decimal _summaIn;
        /// <summary>
        /// ������������� ����� ��������
        /// </summary>
        public decimal SummaIn
        {
            get { return _summaIn; }
            set
            {
                if (value == _summaIn) return;
                OnPropertyChanging(GlobalPropertyNames.SummaIn);
                _summaIn = value;
                OnPropertyChanged(GlobalPropertyNames.SummaIn);
            }
        }


        private decimal _summaLastIn;
        /// <summary>
        /// ����� �������� �� ������ ���������� �������
        /// </summary>
        public decimal SummaLastIn
        {
            get { return _summaLastIn; }
            set
            {
                if (value == _summaLastIn) return;
                OnPropertyChanging(GlobalPropertyNames.SummaLastIn);
                _summaLastIn = value;
                OnPropertyChanged(GlobalPropertyNames.SummaLastIn);
            }
        }


        private decimal _discount;
        /// <summary>
        /// ������ � % ���������
        /// </summary>
        public decimal Discount
        {
            get { return _discount; }
            set
            {
                if (value == _discount) return;
                OnPropertyChanging(GlobalPropertyNames.Discount);
                _discount = value;
                OnPropertyChanged(GlobalPropertyNames.Discount);
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

            /*if (_kind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Kind, XmlConvert.ToString(_kind));*/
            if (_productId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProductId, XmlConvert.ToString(_productId));
            if (_unitId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UnitId, XmlConvert.ToString(_unitId));
            if (_price != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Price, XmlConvert.ToString(_price));
            if (_qty != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Qty, XmlConvert.ToString(_qty));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (_fUnitId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FUnitId, XmlConvert.ToString(_fUnitId));
            if (_fQty != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FQty, XmlConvert.ToString(_fQty));
            if (_taxAnaliticId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TaxAnaliticId, XmlConvert.ToString(_taxAnaliticId));
            if (_summaTax != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SummaTax, XmlConvert.ToString(_summaTax));
            if (_summaExp != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SummaExp, XmlConvert.ToString(_summaExp));
            if (_summaIn != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SummaIn, XmlConvert.ToString(_summaIn));
            if (_summaLastIn != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SummaLastIn, XmlConvert.ToString(_summaLastIn));
            if (_discount != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Discount, XmlConvert.ToString(_discount));
            if (_qtyFact != 0)
                writer.WriteAttributeString(GlobalPropertyNames.QtyFact, XmlConvert.ToString(_qtyFact));
            if (_summaFact != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SummaFact, XmlConvert.ToString(_summaFact));
            if (_qtyBase != 0)
                writer.WriteAttributeString(GlobalPropertyNames.QtyBase, XmlConvert.ToString(_qtyBase));
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            /*if (reader.GetAttribute(GlobalPropertyNames.Kind) != null)
                _kind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Kind));*/
            if (reader.GetAttribute(GlobalPropertyNames.ProductId) != null)
                _productId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProductId));
            if (reader.GetAttribute(GlobalPropertyNames.UnitId) != null)
                _unitId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UnitId));
            if (reader.GetAttribute(GlobalPropertyNames.Price) != null)
                _price = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Price));
            if (reader.GetAttribute(GlobalPropertyNames.Qty) != null)
                _qty = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Qty));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo =reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.FUnitId) != null)
                _fUnitId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FUnitId));
            if (reader.GetAttribute(GlobalPropertyNames.FQty) != null)
                _fQty = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.FQty));
            if (reader.GetAttribute(GlobalPropertyNames.TaxAnaliticId) != null)
                _taxAnaliticId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TaxAnaliticId));
            if (reader.GetAttribute(GlobalPropertyNames.SummaTax) != null)
                _summaTax = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaTax));
            if (reader.GetAttribute(GlobalPropertyNames.SummaExp) != null)
                _summaExp = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaExp));
            if (reader.GetAttribute(GlobalPropertyNames.SummaIn) != null)
                _summaIn = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaIn));
            if (reader.GetAttribute(GlobalPropertyNames.SummaLastIn) != null)
                _summaLastIn = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaLastIn));
            if (reader.GetAttribute(GlobalPropertyNames.Discount) != null)
                _discount = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Discount));
            if (reader.GetAttribute(GlobalPropertyNames.QtyFact) != null)
                _qtyFact = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.QtyFact));
            if (reader.GetAttribute(GlobalPropertyNames.SummaFact) != null)
                _summaFact = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaFact));
            if (reader.GetAttribute(GlobalPropertyNames.QtyBase) != null)
                _qtyBase = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.QtyBase));
        }
        #endregion

        #region ���������
        BaseStructDocumentDetailSale _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentDetailSale
                {
                    Kind = Kind,
                    Memo = _memo,
                    Price = _summa,
                    ProductId = _productId,
                    Qty = _qty,
                    UnitId = _unitId,
                    FQty = _fQty,
                    FUnitId = _fUnitId,
                    TaxAnaliticId = _taxAnaliticId,
                    SummaTax = _summaTax,
                    SummaExp = _summaExp,
                    SummaIn = _summaIn,
                    SummaLastIn = _summaLastIn,
                    Discount = _discount,
                    QtyFact = _qtyFact,
                    SummaFact = _summaFact,
                    QtyBase = _qtyBase

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
            Kind = _baseStruct.Kind;
            _memo = _baseStruct.Memo;
            _summa = _baseStruct.Price;
            _productId = _baseStruct.ProductId;
            _qty = _baseStruct.Qty;
            _unitId = _baseStruct.UnitId;
            _fQty = _baseStruct.FQty;
            _fUnitId = _baseStruct.FUnitId;
            _taxAnaliticId = _baseStruct.TaxAnaliticId;
            _summaTax = _baseStruct.SummaTax;
            _summaExp = _baseStruct.SummaExp;
            _summaIn = _baseStruct.SummaIn;
            _summaLastIn = _baseStruct.SummaLastIn;
            _discount = _baseStruct.Discount;
            _qtyFact = _baseStruct.QtyFact;
            _summaFact = _baseStruct.SummaFact;
            _qtyBase = _baseStruct.QtyBase;
            IsChanged = false;
        }
        #endregion
        /// <summary>
        /// �������� ������������ ������� ��������� �����������
        /// </summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();
            if(StateId!= State.STATEDELETED)
            {
                StateId = Document.StateId;
            }
            Date = Document.Date;
            if (Kind == 0)
                Kind = Document.Kind;
            // TODO: ��������� ��������� �������������� 131088, 16
            if (Kind != 131088)
            {
                if (_summaFact == 0)
                    _summaFact = Summa;
                if (_qtyFact == 0)
                    _qtyFact = Qty;
            }
            if (FUnitId == 0)
                QtyBase = 0;
            
            if(Kind==0)
            {
                throw new ValidateException("�� ������ ��� ������ ���������");
            }
            if (_productId == 0)
                throw new ValidateException("�� ������ �����");
        }
        #region ���� ������
        /// <summary>�������� ������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">������� ��������� ��������</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _productId = reader.GetInt32(12);
                _unitId = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _qty = reader.GetDecimal(14);
                _price = reader.GetDecimal(15);
                _summa = reader.GetDecimal(16);
                _memo = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                _fUnitId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _fQty = reader.GetDecimal(19);
                _taxAnaliticId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _summaTax = reader.GetDecimal(21);
                _summaExp = reader.GetDecimal(22);
                _summaIn = reader.GetDecimal(23);
                _summaLastIn = reader.GetDecimal(24);
                _discount = reader.GetDecimal(25);
                _qtyFact = reader.GetDecimal(26);
                _summaFact = reader.GetDecimal(27);
                _qtyBase = reader.GetDecimal(28);
                _mGuid = reader.IsDBNull(29) ? Guid.Empty : reader.GetGuid(29);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            OnEndInit();
        }

        internal class TpvCollection : List<DocumentDetailSale>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {

                var sdr = new SqlDataRecord
                (
                    new SqlMetaData(GlobalPropertyNames.Id, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Guid, SqlDbType.UniqueIdentifier),
                    new SqlMetaData(GlobalPropertyNames.DatabaseId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.DbSourceId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Version, SqlDbType.Binary, 8),
                    new SqlMetaData(GlobalPropertyNames.UserName, SqlDbType.NVarChar, 50),
                    new SqlMetaData(GlobalPropertyNames.DateModified, SqlDbType.DateTime),
                    new SqlMetaData(GlobalPropertyNames.Flags, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StateId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Date, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.OwnId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.KindId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ProductId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.UnitId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Qty, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Price, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Summa, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.FUnitId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.FQty, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.TaxAnaliticId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.SummaTax, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.SummaExp, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.SummaIn, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.SummaLastIn, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Discount, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.QtyFact, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.SummaFact, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.QtyBase, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.MGuid, SqlDbType.UniqueIdentifier)
                );

                foreach (DocumentDetailSale doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailSale doc)
        {
            sdr.SetInt32(0, doc.Id);
            sdr.SetGuid(1, doc.Guid);
            sdr.SetInt32(2, doc.DatabaseId);
            if (doc.DbSourceId == 0)
                sdr.SetValue(3, DBNull.Value);
            else
                sdr.SetInt32(3, doc.DbSourceId);
            if (doc.ObjectVersion == null || doc.ObjectVersion.All(v => v == 0))
                sdr.SetValue(4, DBNull.Value);
            else
                sdr.SetValue(4, doc.ObjectVersion);

            if (string.IsNullOrEmpty(doc.UserName))
                sdr.SetValue(5, DBNull.Value);
            else
                sdr.SetString(5, doc.UserName);

            if (doc.DateModified.HasValue)
                sdr.SetDateTime(6, doc.DateModified.Value);
            else
                sdr.SetValue(6, DBNull.Value);

            sdr.SetInt32(7, doc.FlagsValue);
            sdr.SetInt32(8, doc.StateId);
            sdr.SetDateTime(9, doc.Date);
            sdr.SetInt32(10, doc.Document.Id);
            sdr.SetInt32(11, doc.Kind);          
            sdr.SetInt32(12, doc.ProductId);
            
            if (doc.UnitId == 0)
                sdr.SetValue(13, DBNull.Value);
            else
                sdr.SetInt32(13, doc.UnitId);

            
            sdr.SetDecimal(14, doc.Qty);
            sdr.SetDecimal(15, doc.Price);
            sdr.SetDecimal(16, doc.Summa);
            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetString(17, doc.Memo);

            if (doc.FUnitId == 0)
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetInt32(18, doc.FUnitId);
            sdr.SetDecimal(19, doc.FQty);
            if (doc.TaxAnaliticId == 0)
                sdr.SetValue(20, DBNull.Value);
            else
                sdr.SetInt32(20, doc.TaxAnaliticId);

            sdr.SetDecimal(21, doc.SummaTax);
            sdr.SetDecimal(22, doc.SummaExp);
            sdr.SetDecimal(23, doc.SummaIn);
            sdr.SetDecimal(24, doc.SummaLastIn);
            sdr.SetDecimal(25, doc.Discount);
            sdr.SetDecimal(26, doc.QtyFact);
            sdr.SetDecimal(27, doc.SummaFact);
            sdr.SetDecimal(28, doc.QtyBase);
            sdr.SetGuid(29, doc.MGuid);
            return sdr;
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
            _baseStruct = new BaseStructDocumentDetailSale();
        }

        #endregion

        /// <summary>��������� ��������� �� ���� ������ �� ��� ��������������</summary>
        /// <remarks>����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "Load"</remarks>
        /// <param name="value">�������������</param>
        public override void Load(int value)
        {
            //throw new NotImplementedException();
        }

        #region IChainsAdvancedList<DocumentDetailSale,FileData> Members

        List<IChainAdvanced<DocumentDetailSale, FileData>> IChainsAdvancedList<DocumentDetailSale, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<DocumentDetailSale, FileData>)this).GetLinks(50);
        }

        List<IChainAdvanced<DocumentDetailSale, FileData>> IChainsAdvancedList<DocumentDetailSale, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<DocumentDetailSale, FileData>.GetChainView()
        {
            return null; //ChainValueView.GetView<Agent, FileData>(this);
        }
        public List<IChainAdvanced<DocumentDetailSale, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<DocumentDetailSale, FileData>> collection = new List<IChainAdvanced<DocumentDetailSale, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Library>().Entity.FindMethod("LoadFiles").FullName;
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
                                ChainAdvanced<DocumentDetailSale, FileData> item = new ChainAdvanced<DocumentDetailSale, FileData> { Workarea = Workarea, Left = this };
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
    }
}