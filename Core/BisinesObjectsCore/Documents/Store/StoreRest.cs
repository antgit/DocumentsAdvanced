/*
using System;

namespace BusinessObjects
{
    // TODO: �������� ���������
    // TODO: ������ 
    /// <summary>
    /// ������ ������ �������� ��������
    /// </summary>
    public class StoreRest : BaseCoreObject, ICoreObject
    {
        public StoreRest():base()
        {
        }
        #region ��������

        private int _storeId;
        /// <summary>
        /// ������������� ������
        /// </summary>
        public int StoreId
        {
            get { return _storeId; }
            set
            {
                if (value != _storeId)
                {
                    OnPropertyChanging("StoreId");
                    _storeId = value;
                    OnPropertyChanged("StoreId");
                }
            }
        }

        private DateTime _date;

        /// <summary>
        /// ����
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value != _date)
                {
                    OnPropertyChanging("Date");
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        private int _productId;

        /// <summary>
        /// ������������� ������
        /// </summary>
        public int ProductId
        {
            get { return _productId; }
            set
            {
                if (value != _productId)
                {
                    OnPropertyChanging("ProductId");
                    _productId = value;
                    OnPropertyChanged("ProductId");
                }
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
                if (value != _qty)
                {
                    OnPropertyChanging("Qty");
                    _qty = value;
                    OnPropertyChanged("Qty");
                }
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
                if (value != _summa)
                {
                    OnPropertyChanging("Summa");
                    _summa = value;
                    OnPropertyChanged("Summa");
                }
            }
        }

        private int _documentId;

        /// <summary>
        /// ������������� ���������
        /// </summary>
        public int DocumentId
        {
            get { return _documentId; }
            set
            {
                if (value != _documentId)
                {
                    OnPropertyChanging("DocumentId");
                    _documentId = value;
                    OnPropertyChanged("DocumentId");
                }
            }
        } 
        #endregion
        /// <summary>���������</summary>
        /// <param name="value">�������������</param>
        public override void Load(int value)
        {
            // TODO: ������!!! ���������� ��� ��������� 
            Load(value, Workarea.FindMethod("").FullName);
        }
	
    }
}
*/