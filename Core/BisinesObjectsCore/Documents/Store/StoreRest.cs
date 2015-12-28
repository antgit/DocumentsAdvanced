/*
using System;

namespace BusinessObjects
{
    // TODO: Хранимые процедуры
    // TODO: Методы 
    /// <summary>
    /// Строка данных хранимых остатков
    /// </summary>
    public class StoreRest : BaseCoreObject, ICoreObject
    {
        public StoreRest():base()
        {
        }
        #region Свойства

        private int _storeId;
        /// <summary>
        /// Идентификатор склада
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
        /// Дата
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
        /// Идентификатор товара
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
        /// Количество
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
        /// Сумма
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
        /// Идентификатор документа
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
        /// <summary>Загрузить</summary>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            // TODO: срочно!!! правильное имя процедуры 
            Load(value, Workarea.FindMethod("").FullName);
        }
	
    }
}
*/