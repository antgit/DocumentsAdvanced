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
    internal struct BaseStructDocumentDetailStore
    {
        /// <summary>������������� ���� ������ ������</summary>
        public Int32 Kind;
        /// <summary>������������� ������</summary>
        public int ProductId;
        /// <summary>������������� ������� ���������</summary>
        public int UnitId;
        /// <summary>������������� ������� ���������</summary>
        public int FUnitId;
        /// <summary>����</summary>
        public decimal Price;
        /// <summary>�����</summary>
        public decimal Summa;
        /// <summary>����������</summary>
        public decimal Qty;
        /// <summary>����������</summary>
        public decimal FQty;
        /// <summary>����������</summary>
        public string Memo;
        /// <summary>������������� ������ ������</summary>
        public int SeriesId;
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
    public class DocumentDetailStore : DocumentBaseDetail, IEditableObject, IChainsAdvancedList<DocumentDetailStore, FileData>
    {
        /// <summary>
        /// �����������
        /// </summary>
        public DocumentDetailStore()
            : base()
        {
            _entityId = 1;

        }
        #region ��������
        /// <summary>
        /// �������� ��������
        /// </summary>
        public DocumentStore Document { get; set; }

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

        private int _funitId;
        /// <summary>
        /// ������������� ������� ���������
        /// </summary>
        public int FUnitId
        {
            get { return _funitId; }
            set
            {
                if (value == _funitId) return;
                OnPropertyChanging(GlobalPropertyNames.FUnitId);
                _funitId = value;
                OnPropertyChanged(GlobalPropertyNames.FUnitId);
            }
        }

        private Unit _funit;
        /// <summary>
        /// ������� ��������� 
        /// </summary>
        public Unit FUnit
        {
            get
            {
                if (_funitId == 0)
                    return null;
                if (_funit == null)
                    _funit = Workarea.Cashe.GetCasheData<Unit>().Item(_funitId);
                else if (_funit.Id != _funitId)
                    _funit = Workarea.Cashe.GetCasheData<Unit>().Item(_funitId);
                return _funit;
            }
            set
            {
                if (_funit == value) return;
                OnPropertyChanging(GlobalPropertyNames.FUnit);
                _funit = value;
                _funitId = _funit == null ? 0 : _funit.Id;
                OnPropertyChanged(GlobalPropertyNames.FUnit);
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

        private decimal _fqty;
        /// <summary>
        /// ����������
        /// </summary>
        public decimal FQty
        {
            get { return _fqty; }
            set
            {
                if (value == _fqty) return;
                OnPropertyChanging(GlobalPropertyNames.FQty);
                _fqty = value;
                OnPropertyChanged(GlobalPropertyNames.FQty);
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

        private int _seriesId;
        /// <summary>
        /// ������������� ������ ������
        /// </summary>
        public int SeriesId
        {
            get { return _seriesId; }
            set
            {
                if (_seriesId == value) return;
                OnPropertyChanging(GlobalPropertyNames.SeriesId);
                _seriesId = value;
                OnPropertyChanged(GlobalPropertyNames.SeriesId);
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
            if (_funitId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FUnitId, XmlConvert.ToString(_funitId));
            if (_price != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Price, XmlConvert.ToString(_price));
            if (_summa != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Summa, XmlConvert.ToString(_summa));
            if (_qty != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Qty, XmlConvert.ToString(_qty));
            if (_fqty != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FQty, XmlConvert.ToString(_fqty));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (_seriesId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SeriesId, XmlConvert.ToString(_seriesId));
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
            if (reader.GetAttribute(GlobalPropertyNames.FUnitId) != null)
                _funitId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FUnitId));
            if (reader.GetAttribute(GlobalPropertyNames.Price) != null)
                _price = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Price));
            if (reader.GetAttribute(GlobalPropertyNames.Summa) != null)
                _summa = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Summa));
            if (reader.GetAttribute(GlobalPropertyNames.Qty) != null)
                _qty = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Qty));
            if (reader.GetAttribute(GlobalPropertyNames.FQty) != null)
                _fqty = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.FQty));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.SeriesId) != null)
                _seriesId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SeriesId));
            if (reader.GetAttribute(GlobalPropertyNames.QtyFact) != null)
                _qtyFact = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.QtyFact));
            if (reader.GetAttribute(GlobalPropertyNames.SummaFact) != null)
                _summaFact = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaFact));
            if (reader.GetAttribute(GlobalPropertyNames.QtyBase) != null)
                _qtyBase = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.QtyBase));
        }
        #endregion

        #region ���������
        BaseStructDocumentDetailStore _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentDetailStore
                {
                    Kind = Kind,
                    ProductId = _productId,
                    UnitId = _unitId,
                    FUnitId = _funitId,
                    Price = _price,
                    Summa = _summa,
                    Qty = _qty,
                    FQty = _fqty,
                    Memo = _memo,
                    SeriesId = _seriesId,
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
            ProductId = _baseStruct.ProductId;
            UnitId = _baseStruct.UnitId;
            FUnitId = _baseStruct.FUnitId;
            Price = _baseStruct.Price;
            Summa = _baseStruct.Summa;
            Qty = _baseStruct.Qty;
            FQty = _baseStruct.FQty;
            Memo = _baseStruct.Memo;
            SeriesId = _baseStruct.SeriesId;
            QtyFact = _baseStruct.QtyFact;
            SummaFact = _baseStruct.SummaFact; 
            QtyBase = _baseStruct.QtyBase;
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
            if (StateId != State.STATEDELETED)
            {
                StateId = Document.StateId;
            }
            Date = Document.Date;
            if (Kind == 0)
                Kind = Document.Kind;
            if (Kind == 0)
            {
                throw new ValidateException("�� ������ ��� ������ ���������");
            }
            if (_productId == 0)
                throw new ValidateException("�� ������ �����");

            if (Id == 0)
                _mGuid = Guid.NewGuid();
            else
                _mGuid = Guid;
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
                _seriesId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _funitId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _fqty = reader.GetDecimal(20);
                _qtyFact = reader.GetDecimal(21);
                _summaFact = reader.GetDecimal(22);
                _qtyBase = reader.GetDecimal(23);
                _mGuid = reader.IsDBNull(24) ? Guid.Empty : reader.GetGuid(24);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            OnEndInit();
        }

        internal class TpvCollection : List<DocumentDetailStore>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.FlagsValue, SqlDbType.Int),
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
                    new SqlMetaData(GlobalPropertyNames.SeriesId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.FUnitId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.FQty, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.QtyFact, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.SummaFact, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.QtyBase, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.MGuid, SqlDbType.UniqueIdentifier)
                );

                foreach (DocumentDetailStore doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailStore doc)
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
            sdr.SetInt32(13, doc.UnitId);
            sdr.SetDecimal(14, doc.Qty);
            sdr.SetDecimal(15, doc.Price);
            sdr.SetDecimal(16, doc.Summa);
            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetString(17, doc.Memo);

            if (doc.SeriesId == 0)
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetInt32(18, doc.SeriesId);

            if (doc.FUnitId == 0)
                sdr.SetValue(19, DBNull.Value);
            else
                sdr.SetInt32(19, doc.FUnitId);
            sdr.SetDecimal(20, doc.FQty);

            sdr.SetDecimal(21, doc.QtyFact);
            sdr.SetDecimal(22, doc.SummaFact);
            sdr.SetDecimal(23, doc.QtyBase);
            sdr.SetGuid(24, doc.MGuid);
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
            _baseStruct = new BaseStructDocumentDetailStore();
        }

        #endregion

        /// <summary>��������� ��������� �� ���� ������ �� ��� ��������������</summary>
        /// <remarks>����� ���������� �������� ��������� ��������� � ������� ������� �� ����� "Load"</remarks>
        /// <param name="value">�������������</param>
        public override void Load(int value)
        {
            //throw new NotImplementedException();
        }

        #region IChainsAdvancedList<DocumentDetailStore,FileData> Members

        List<IChainAdvanced<DocumentDetailStore, FileData>> IChainsAdvancedList<DocumentDetailStore, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<DocumentDetailStore, FileData>)this).GetLinks(51);
        }

        List<IChainAdvanced<DocumentDetailStore, FileData>> IChainsAdvancedList<DocumentDetailStore, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<DocumentDetailStore, FileData>.GetChainView()
        {
            return null; //ChainValueView.GetView<Agent, FileData>(this);
        }
        public List<IChainAdvanced<DocumentDetailStore, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<DocumentDetailStore, FileData>> collection = new List<IChainAdvanced<DocumentDetailStore, FileData>>();
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
                                ChainAdvanced<DocumentDetailStore, FileData> item = new ChainAdvanced<DocumentDetailStore, FileData> { Workarea = Workarea, Left = this };
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