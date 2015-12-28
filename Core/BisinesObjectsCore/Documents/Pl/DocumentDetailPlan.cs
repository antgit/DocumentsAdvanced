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

    internal struct BaseStructDocumentDetailPlan
    {
        /// <summary>Идентификатор типа строки данных</summary>
        public Int32 Kind;
        /// <summary>Идентификатор товара</summary>
        public int ProductId;
        /// <summary>Идентификатор товара</summary>
        public int ProductId2;
        /// <summary>Идентификатор товара</summary>
        public int ProductId3;
        /// <summary>Идентификатор товара</summary>
        public int ProductId4;
        /// <summary>Идентификатор товара</summary>
        public int ProductId5;
        /// <summary>Цена</summary>
        public decimal Price;
        /// <summary>Цена</summary>
        public decimal Price2;
        /// <summary>Цена</summary>
        public decimal Price3;
        /// <summary>Цена</summary>
        public decimal Price4;
        /// <summary>Цена</summary>
        public decimal Price5;
        /// <summary>Количество</summary>
        public decimal Qty;
        /// <summary>Количество</summary>
        public decimal Qty2;
        /// <summary>Количество</summary>
        public decimal Qty3;
        /// <summary>Количество</summary>
        public decimal Qty4;
        /// <summary>Количество</summary>
        public decimal Qty5;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>сумма</summary>
        public decimal Summa;
        /// <summary>сумма</summary>
        public decimal Summa2;
        /// <summary>сумма</summary>
        public decimal Summa3;
        /// <summary>сумма</summary>
        public decimal Summa4;
        /// <summary>сумма</summary>
        public decimal Summa5;
        /// <summary>Идентификатор аналитики</summary>
        public int AnaliticId;
        /// <summary>Идентификатор аналитики №2</summary>
        public int AnaliticId2;
        /// <summary>Идентификатор аналитики №3</summary>
        public int AnaliticId3;
        /// <summary>Идентификатор аналитики №4</summary>
        public int AnaliticId4;
        /// <summary>Идентификатор аналитики №5</summary>
        public int AnaliticId5;
        /// <summary>Строковое значение №1</summary>
        public string StringValue1;
        /// <summary>Строковое значение №2</summary>
        public string StringValue2;
        /// <summary>Строковое значение №3</summary>
        public string StringValue3;
        /// <summary>Строковое значение №4</summary>
        public string StringValue4;
        /// <summary>Строковое значение №5</summary>
        public string StringValue5;
        /// <summary>Идентификатор корреспондента</summary>
        public int AgentId;
        /// <summary>Идентификатор корреспондента</summary>
        public int AgentId2;
        /// <summary>Идентификатор корреспондента</summary>
        public int AgentId3;
        /// <summary>Идентификатор корреспондента</summary>
        public int AgentId4;
        /// <summary>Идентификатор корреспондента</summary>
        public int AgentId5;
        
    }
    /// <summary>
    /// Детализация документа в разделе "Договора и документы"
    /// </summary>
    public class DocumentDetailPlan : DocumentBaseDetail, IEditableObject, IChainsAdvancedList<DocumentDetailPlan, FileData>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailPlan()
            : base()
        {
            _entityId = 12;

        }
        #region Свойства
        /// <summary>
        /// Основной документ
        /// </summary>
        public DocumentPlan Document { get; set; }

        //private int _kind;
        ///// <summary>
        ///// Тип товарной операции
        ///// </summary>
        ///// <remarks>Текущие типы товарной операции: 
        ///// 0-приход, 
        ///// 1-Расход, 
        ///// 2-перемещение, 
        ///// 3-возврат товара поставщику,
        ///// 4-возврат товара от покупателя</remarks>
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

        private Product _product;
        /// <summary>Товар</summary>
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
        
        private int _productId2;
        /// <summary>Идентификатор товара №2</summary>
        public int ProductId2
        {
            get { return _productId2; }
            set
            {
                if (value == _productId2) return;
                OnPropertyChanging(GlobalPropertyNames.ProductId2);
                _productId2 = value;
                OnPropertyChanged(GlobalPropertyNames.ProductId2);
            }
        }
        
        private Product _product2;
        /// <summary>Товар №2</summary>
        public Product Product2
        {
            get
            {
                if (_productId2 == 0)
                    return null;
                if (_product2 == null)
                    _product2 = Workarea.Cashe.GetCasheData<Product>().Item(_productId2);
                else if (_product2.Id != _productId2)
                    _product2 = Workarea.Cashe.GetCasheData<Product>().Item(_productId2);
                return _product2;
            }
            set
            {
                if (_product2 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Product2);
                _product2 = value;
                _productId2 = _product2 == null ? 0 : _product2.Id;
                OnPropertyChanged(GlobalPropertyNames.Product2);
            }
        }
        
        private int _productId3;
        /// <summary>Идентификатор товара №3</summary>
        public int ProductId3
        {
            get { return _productId3; }
            set
            {
                if (value == _productId3) return;
                OnPropertyChanging(GlobalPropertyNames.ProductId3);
                _productId3 = value;
                OnPropertyChanged(GlobalPropertyNames.ProductId3);
            }
        }

        private Product _product3;
        /// <summary>Товар №3</summary>
        public Product Product3
        {
            get
            {
                if (_productId3 == 0)
                    return null;
                if (_product3 == null)
                    _product3 = Workarea.Cashe.GetCasheData<Product>().Item(_productId3);
                else if (_product3.Id != _productId3)
                    _product3 = Workarea.Cashe.GetCasheData<Product>().Item(_productId3);
                return _product3;
            }
            set
            {
                if (_product3 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Product3);
                _product3 = value;
                _productId3 = _product3 == null ? 0 : _product3.Id;
                OnPropertyChanged(GlobalPropertyNames.Product3);
            }
        }
        
        private int _productId4;
        /// <summary>Идентификатор товара №4</summary>
        public int ProductId4
        {
            get { return _productId4; }
            set
            {
                if (value == _productId4) return;
                OnPropertyChanging(GlobalPropertyNames.ProductId4);
                _productId4 = value;
                OnPropertyChanged(GlobalPropertyNames.ProductId4);
            }
        }
        
        private Product _product4;
        /// <summary>Товар №4</summary>
        public Product Product4
        {
            get
            {
                if (_productId4 == 0)
                    return null;
                if (_product4 == null)
                    _product4 = Workarea.Cashe.GetCasheData<Product>().Item(_productId4);
                else if (_product4.Id != _productId4)
                    _product4 = Workarea.Cashe.GetCasheData<Product>().Item(_productId4);
                return _product4;
            }
            set
            {
                if (_product4 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Product4);
                _product4 = value;
                _productId4 = _product4 == null ? 0 : _product4.Id;
                OnPropertyChanged(GlobalPropertyNames.Product4);
            }
        }
        
        private int _productId5;
        /// <summary>Идентификатор товара №5</summary>
        public int ProductId5
        {
            get { return _productId5; }
            set
            {
                if (value == _productId5) return;
                OnPropertyChanging(GlobalPropertyNames.ProductId5);
                _productId5 = value;
                OnPropertyChanged(GlobalPropertyNames.ProductId5);
            }
        }

        private Product _product5;
        /// <summary>Товар №5</summary>
        public Product Product5
        {
            get
            {
                if (_productId5 == 0)
                    return null;
                if (_product5 == null)
                    _product5 = Workarea.Cashe.GetCasheData<Product>().Item(_productId5);
                else if (_product5.Id != _productId5)
                    _product5 = Workarea.Cashe.GetCasheData<Product>().Item(_productId5);
                return _product5;
            }
            set
            {
                if (_product5 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Product5);
                _product5 = value;
                _productId5 = _product5 == null ? 0 : _product5.Id;
                OnPropertyChanged(GlobalPropertyNames.Product5);
            }
        }
        
        private decimal _price;
        /// <summary>Цена</summary>
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value == _price) return;
                OnPropertyChanging(GlobalPropertyNames.Price);
                _price = value;
                OnPropertyChanged(GlobalPropertyNames.Price);
            }
        }
        
        private decimal _price2;
        /// <summary>Цена №2</summary>
        public decimal Price2
        {
            get { return _price2; }
            set
            {
                if (value == _price2) return;
                OnPropertyChanging(GlobalPropertyNames.Price2);
                _price2 = value;
                OnPropertyChanged(GlobalPropertyNames.Price2);
            }
        }
        
        private decimal _price3;
        /// <summary>Цена №3</summary>
        public decimal Price3
        {
            get { return _price3; }
            set
            {
                if (value == _price3) return;
                OnPropertyChanging(GlobalPropertyNames.Price3);
                _price3 = value;
                OnPropertyChanged(GlobalPropertyNames.Price3);
            }
        }

        private decimal _price4;
        /// <summary>Цена №4</summary>
        public decimal Price4
        {
            get { return _price4; }
            set
            {
                if (value == _price4) return;
                OnPropertyChanging(GlobalPropertyNames.Price4);
                _price4 = value;
                OnPropertyChanged(GlobalPropertyNames.Price4);
            }
        }

        private decimal _price5;
        /// <summary>Цена №5</summary>
        public decimal Price5
        {
            get { return _price5; }
            set
            {
                if (value == _price5) return;
                OnPropertyChanging(GlobalPropertyNames.Price5);
                _price5 = value;
                OnPropertyChanged(GlobalPropertyNames.Price5);
            }
        }
        
        private decimal _summa;
        /// <summary>Сумма</summary>
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
        
        private decimal _summa2;
        /// <summary>Сумма №2</summary>
        public decimal Summa2
        {
            get { return _summa2; }
            set
            {
                if (value == _summa2) return;
                OnPropertyChanging(GlobalPropertyNames.Summa2);
                _summa2 = value;
                OnPropertyChanged(GlobalPropertyNames.Summa2);
            }
        }

        private decimal _summa3;
        /// <summary>Сумма №3</summary>
        public decimal Summa3
        {
            get { return _summa3; }
            set
            {
                if (value == _summa3) return;
                OnPropertyChanging(GlobalPropertyNames.Summa3);
                _summa3 = value;
                OnPropertyChanged(GlobalPropertyNames.Summa3);
            }
        }

        private decimal _summa4;
        /// <summary>Сумма №4</summary>
        public decimal Summa4
        {
            get { return _summa4; }
            set
            {
                if (value == _summa4) return;
                OnPropertyChanging(GlobalPropertyNames.Summa4);
                _summa4 = value;
                OnPropertyChanged(GlobalPropertyNames.Summa4);
            }
        }

        private decimal _summa5;
        /// <summary>Сумма №5</summary>
        public decimal Summa5
        {
            get { return _summa5; }
            set
            {
                if (value == _summa5) return;
                OnPropertyChanging(GlobalPropertyNames.Summa5);
                _summa5 = value;
                OnPropertyChanged(GlobalPropertyNames.Summa5);
            }
        }
        
        private decimal _qty;
        /// <summary>Количество</summary>
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
        
        private decimal _qty2;
        /// <summary>Количество №2</summary>
        public decimal Qty2
        {
            get { return _qty2; }
            set
            {
                if (value == _qty2) return;
                OnPropertyChanging(GlobalPropertyNames.Qty2);
                _qty2 = value;
                OnPropertyChanged(GlobalPropertyNames.Qty2);
            }
        }

        private decimal _qty3;
        /// <summary>Количество №3</summary>
        public decimal Qty3
        {
            get { return _qty3; }
            set
            {
                if (value == _qty3) return;
                OnPropertyChanging(GlobalPropertyNames.Qty3);
                _qty3 = value;
                OnPropertyChanged(GlobalPropertyNames.Qty3);
            }
        }

        private decimal _qty4;
        /// <summary>Количество №4</summary>
        public decimal Qty4
        {
            get { return _qty4; }
            set
            {
                if (value == _qty4) return;
                OnPropertyChanging(GlobalPropertyNames.Qty4);
                _qty4 = value;
                OnPropertyChanged(GlobalPropertyNames.Qty4);
            }
        }

        private decimal _qty5;
        /// <summary>Количество №5</summary>
        public decimal Qty5
        {
            get { return _qty5; }
            set
            {
                if (value == _qty5) return;
                OnPropertyChanging(GlobalPropertyNames.Qty5);
                _qty5 = value;
                OnPropertyChanged(GlobalPropertyNames.Qty5);
            }
        }
        
        private string _memo;
        /// <summary>
        /// Примечание
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

        private int _analiticId;
        /// <summary>Идентификатор аналитики</summary>
        public int AnaliticId
        {
            get { return _analiticId; }
            set
            {
                if (value == _analiticId) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId);
                _analiticId = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId);
            }
        }

        private Analitic _analitic;
        /// <summary>Аналитика</summary>
        public Analitic Analitic
        {
            get
            {
                if (_analiticId == 0)
                    return null;
                if (_analitic == null)
                    _analitic = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId);
                else if (_analitic.Id != _analiticId)
                    _analitic = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId);
                return _analitic;
            }
            set
            {
                if (_analitic == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic);
                _analitic = value;
                _analiticId = _analitic == null ? 0 : _analitic.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic);
            }
        }

        private int _analiticId2;
        /// <summary>Идентификатор аналитики №2</summary>
        public int AnaliticId2
        {
            get { return _analiticId2; }
            set
            {
                if (value == _analiticId2) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId2);
                _analiticId2 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId2);
            }
        }

        private Analitic _analitic2;
        /// <summary>Аналитика №2</summary>
        public Analitic Analitic2
        {
            get
            {
                if (_analiticId2 == 0)
                    return null;
                if (_analitic2 == null)
                    _analitic2 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId2);
                else if (_analitic2.Id != _analiticId2)
                    _analitic2 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId2);
                return _analitic2;
            }
            set
            {
                if (_analitic2 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic2);
                _analitic2 = value;
                _analiticId2 = _analitic2 == null ? 0 : _analitic2.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic2);
            }
        }

        private int _analiticId3;
        /// <summary>Идентификатор аналитики №3</summary>
        public int AnaliticId3
        {
            get { return _analiticId3; }
            set
            {
                if (value == _analiticId3) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId3);
                _analiticId3 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId3);
            }
        }

        private Analitic _analitic3;
        /// <summary>Аналитика №3</summary>
        public Analitic Analitic3
        {
            get
            {
                if (_analiticId3 == 0)
                    return null;
                if (_analitic3 == null)
                    _analitic3 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId3);
                else if (_analitic3.Id != _analiticId3)
                    _analitic3 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId3);
                return _analitic3;
            }
            set
            {
                if (_analitic3 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic3);
                _analitic3 = value;
                _analiticId3 = _analitic3 == null ? 0 : _analitic3.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic3);
            }
        }

        private int _analiticId4;
        /// <summary>Идентификатор аналитики №4</summary>
        public int AnaliticId4
        {
            get { return _analiticId4; }
            set
            {
                if (value == _analiticId4) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId4);
                _analiticId4 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId4);
            }
        }

        private Analitic _analitic4;
        /// <summary>Аналитика №4</summary>
        public Analitic Analitic4
        {
            get
            {
                if (_analiticId4 == 0)
                    return null;
                if (_analitic4 == null)
                    _analitic4 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId4);
                else if (_analitic4.Id != _analiticId4)
                    _analitic4 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId4);
                return _analitic4;
            }
            set
            {
                if (_analitic4 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic4);
                _analitic4 = value;
                _analiticId4 = _analitic4 == null ? 0 : _analitic4.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic4);
            }
        }

        private int _analiticId5;
        /// <summary>Идентификатор аналитики №5</summary>
        public int AnaliticId5
        {
            get { return _analiticId5; }
            set
            {
                if (value == _analiticId5) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId5);
                _analiticId5 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId5);
            }
        }

        private Analitic _analitic5;
        /// <summary>Аналитика №5</summary>
        public Analitic Analitic5
        {
            get
            {
                if (_analiticId5 == 0)
                    return null;
                if (_analitic5 == null)
                    _analitic5 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId5);
                else if (_analitic5.Id != _analiticId5)
                    _analitic5 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId5);
                return _analitic5;
            }
            set
            {
                if (_analitic5 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic5);
                _analitic5 = value;
                _analiticId5 = _analitic5 == null ? 0 : _analitic5.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic5);
            }
        }

        private string _stringValue1;
        /// <summary>Строковое значение №1</summary>
        public string StringValue1
        {
            get { return _stringValue1; }
            set
            {
                if (value == _stringValue1) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue1);
                _stringValue1 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue1);
            }
        }

        private string _stringValue2;
        /// <summary>Строковое значение №2</summary>
        public string StringValue2
        {
            get { return _stringValue2; }
            set
            {
                if (value == _stringValue2) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue2);
                _stringValue2 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue2);
            }
        }

        private string _stringValue3;
        /// <summary>Строковое значение №3</summary>
        public string StringValue3
        {
            get { return _stringValue3; }
            set
            {
                if (value == _stringValue3) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue3);
                _stringValue3 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue3);
            }
        }

        private string _stringValue4;
        /// <summary>Строковое значение №4</summary>
        public string StringValue4
        {
            get { return _stringValue4; }
            set
            {
                if (value == _stringValue4) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue4);
                _stringValue4 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue4);
            }
        }

        private string _stringValue5;
        /// <summary>Строковое значение №5</summary>
        public string StringValue5
        {
            get { return _stringValue5; }
            set
            {
                if (value == _stringValue5) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue5);
                _stringValue5 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue5);
            }
        }
        
        private int _agentId;
        /// <summary>Идентификатор корреспондента</summary>
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
        
        private Agent _agent;
        /// <summary>Корреспондент №1</summary>
        public Agent Agent
        {
            get
            {
                if (_agentId == 0)
                    return null;
                if (_agent == null)
                    _agent = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId);
                else if (_agent.Id != _agentId)
                    _agent = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId);
                return _agent;
            }
            set
            {
                if (_agent == value) return;
                OnPropertyChanging(GlobalPropertyNames.Agent);
                _agent = value;
                _agentId = _agent == null ? 0 : _agent.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent);
            }
        }
        
        private int _agentId2;
        /// <summary>Идентификатор корреспондента №2</summary>
        public int AgentId2
        {
            get { return _agentId2; }
            set
            {
                if (value == _agentId2) return;
                OnPropertyChanging(GlobalPropertyNames.AgentId2);
                _agentId2 = value;
                OnPropertyChanged(GlobalPropertyNames.AgentId2);
            }
        }
        
        private Agent _agent2;
        /// <summary>Корреспондент №2</summary>
        public Agent Agent2
        {
            get
            {
                if (_agentId2 == 0)
                    return null;
                if (_agent2 == null)
                    _agent2 = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId2);
                else if (_agent2.Id != _agentId2)
                    _agent2 = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId2);
                return _agent2;
            }
            set
            {
                if (_agent2 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Agent2);
                _agent2 = value;
                _agentId2 = _agent2 == null ? 0 : _agent2.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent2);
            }
        }
        
        private int _agentId3;
        /// <summary>Идентификатор корреспондента №3</summary>
        public int AgentId3
        {
            get { return _agentId3; }
            set
            {
                if (value == _agentId3) return;
                OnPropertyChanging(GlobalPropertyNames.AgentId3);
                _agentId3 = value;
                OnPropertyChanged(GlobalPropertyNames.AgentId3);
            }
        }
        
        private Agent _agent3;
        /// <summary>Корреспондент №3</summary>
        public Agent Agent3
        {
            get
            {
                if (_agentId3 == 0)
                    return null;
                if (_agent3 == null)
                    _agent3 = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId3);
                else if (_agent3.Id != _agentId3)
                    _agent3 = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId3);
                return _agent3;
            }
            set
            {
                if (_agent3 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Agent2);
                _agent3 = value;
                _agentId3 = _agent3 == null ? 0 : _agent3.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent2);
            }
        }
        
        private int _agentId4;
        /// <summary>Идентификатор корреспондента №4</summary>
        public int AgentId4
        {
            get { return _agentId4; }
            set
            {
                if (value == _agentId4) return;
                OnPropertyChanging(GlobalPropertyNames.AgentId4);
                _agentId4 = value;
                OnPropertyChanged(GlobalPropertyNames.AgentId4);
            }
        }
        
        private Agent _agent4;
        /// <summary>Корреспондент №4</summary>
        public Agent Agent4
        {
            get
            {
                if (_agentId4 == 0)
                    return null;
                if (_agent4 == null)
                    _agent4 = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId4);
                else if (_agent4.Id != _agentId4)
                    _agent4 = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId4);
                return _agent4;
            }
            set
            {
                if (_agent4 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Agent4);
                _agent4 = value;
                _agentId4 = _agent4 == null ? 0 : _agent4.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent4);
            }
        }
        
        private int _agentId5;
        /// <summary>Идентификатор корреспондента №5</summary>
        public int AgentId5
        {
            get { return _agentId5; }
            set
            {
                if (value == _agentId5) return;
                OnPropertyChanging(GlobalPropertyNames.AgentId5);
                _agentId5 = value;
                OnPropertyChanged(GlobalPropertyNames.AgentId5);
            }
        }
        
        private Agent _agent5;
        /// <summary>Корреспондент №5</summary>
        public Agent Agent5
        {
            get
            {
                if (_agentId5 == 0)
                    return null;
                if (_agent5 == null)
                    _agent5 = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId5);
                else if (_agent5.Id != _agentId5)
                    _agent5 = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId5);
                return _agent5;
            }
            set
            {
                if (_agent5 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Agent5);
                _agent5 = value;
                _agentId5 = _agent5 == null ? 0 : _agent5.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent5);
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
            
            if (_productId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProductId, XmlConvert.ToString(_productId));
            
            if (_price != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Price, XmlConvert.ToString(_price));
            if (_qty != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Qty, XmlConvert.ToString(_qty));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            /*if (reader.GetAttribute(GlobalPropertyNames.Kind) != null)
                _kind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Kind));*/
            if (reader.GetAttribute(GlobalPropertyNames.ProductId) != null)
                _productId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProductId));
            
            if (reader.GetAttribute(GlobalPropertyNames.Price) != null)
                _price = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Price));
            if (reader.GetAttribute(GlobalPropertyNames.Qty) != null)
                _qty = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Qty));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            
        }
        #endregion

        #region Состояния
        BaseStructDocumentDetailPlan _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentDetailPlan
                                  {
                                      Kind = Kind,
                                      Memo = _memo,
                                      Summa = _summa,
                                      Summa2 = _summa2,
                                      Summa3 = _summa3,
                                      Summa4 = _summa4,
                                      Summa5 = _summa5,
                                      Price = _price,
                                      Price2 = _price2,
                                      Price3 = _price3,
                                      Price4 = _price4,
                                      Price5 = _price5,
                                      ProductId = _productId,
                                      ProductId2 = _productId2,
                                      ProductId3 = _productId3,
                                      ProductId4 = _productId4,
                                      ProductId5 = _productId5,
                                      Qty = _qty,
                                      Qty2 = _qty2,
                                      Qty3 = _qty3,
                                      Qty4 = _qty4,
                                      Qty5 = _qty5,
                                      AnaliticId = _analiticId,
                                      AnaliticId2 = _analiticId2,
                                      AnaliticId3 = _analiticId3,
                                      AnaliticId4 = _analiticId4,
                                      AnaliticId5 = _analiticId5,
                                      StringValue1 = _stringValue1,
                                      StringValue2 = _stringValue2,
                                      StringValue3 = _stringValue3,
                                      StringValue4 = _stringValue4,
                                      StringValue5 = _stringValue5,
                                      AgentId = _agentId,
                                      AgentId2 = _agentId2,
                                      AgentId3 = _agentId3,
                                      AgentId4 = _agentId4,
                                      AgentId5 = _agentId5,
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
            Kind = _baseStruct.Kind;
            _memo = _baseStruct.Memo;
            _price = _baseStruct.Price;
            _price2 = _baseStruct.Price2;
            _price3 = _baseStruct.Price3;
            _price4 = _baseStruct.Price4;
            _price5 = _baseStruct.Price5;
            _summa = _baseStruct.Summa;
            _summa2 = _baseStruct.Summa2;
            _summa3 = _baseStruct.Summa3;
            _summa4 = _baseStruct.Summa4;
            _summa5 = _baseStruct.Summa5;
            _productId = _baseStruct.ProductId;
            _productId2 = _baseStruct.ProductId2;
            _productId3 = _baseStruct.ProductId3;
            _productId4 = _baseStruct.ProductId4;
            _productId5 = _baseStruct.ProductId5;
            _qty = _baseStruct.Qty;
            _qty2 = _baseStruct.Qty2;
            _qty3 = _baseStruct.Qty3;
            _qty4 = _baseStruct.Qty4;
            _qty5 = _baseStruct.Qty5;
            _analiticId = _baseStruct.AnaliticId;
            _analiticId2 = _baseStruct.AnaliticId2;
            _analiticId3 = _baseStruct.AnaliticId3;
            _analiticId4 = _baseStruct.AnaliticId4;
            _analiticId5 = _baseStruct.AnaliticId5;
            _stringValue1 = _baseStruct.StringValue1;
            _stringValue2 = _baseStruct.StringValue2;
            _stringValue3 = _baseStruct.StringValue3;
            _stringValue4 = _baseStruct.StringValue4;
            _stringValue5 = _baseStruct.StringValue5;
            _agentId = _baseStruct.AgentId;
            _agentId2 = _baseStruct.AgentId2;
            _agentId3 = _baseStruct.AgentId3;
            _agentId4 = _baseStruct.AgentId4;
            _agentId5 = _baseStruct.AgentId5;
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
            if (StateId != State.STATEDELETED)
            {
                StateId = Document.StateId;
            }
            Date = Document.Date;
            if (Kind == 0)
                Kind = Document.Kind;
            if (_productId == 0)
                throw new ValidateException("Не указан товар");

            if (Kind == 0)
            {
                throw new ValidateException("Не указан тип строки документа");
            }

            if (Id == 0)
                _mGuid = Guid.NewGuid();
            else
                _mGuid = Guid;
        }
        #region База данных
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _productId = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                _productId2 = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _productId3 = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                _productId4 = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
                _productId5 = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);

                _analiticId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _analiticId2 = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _analiticId3 = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _analiticId4 = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _analiticId5 = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);

                _stringValue1 = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                _stringValue2 = reader.IsDBNull(23) ? string.Empty : reader.GetString(23);
                _stringValue3 = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                _stringValue4 = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                _stringValue5 = reader.IsDBNull(26) ? string.Empty : reader.GetString(26);

                _agentId = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                _agentId2 = reader.IsDBNull(28) ? 0 : reader.GetInt32(28);
                _agentId3 = reader.IsDBNull(29) ? 0 : reader.GetInt32(29);
                _agentId4 = reader.IsDBNull(30) ? 0 : reader.GetInt32(30);
                _agentId5 = reader.IsDBNull(31) ? 0 : reader.GetInt32(31);

                _qty = reader.GetDecimal(32);
                _qty2 = reader.GetDecimal(33);
                _qty3 = reader.GetDecimal(34);
                _qty4 = reader.GetDecimal(35);
                _qty5 = reader.GetDecimal(36);

                _price = reader.GetDecimal(37);
                _price2 = reader.GetDecimal(38);
                _price3 = reader.GetDecimal(39);
                _price4 = reader.GetDecimal(40);
                _price5 = reader.GetDecimal(41);

                _summa = reader.GetDecimal(42);
                _summa2 = reader.GetDecimal(43);
                _summa3 = reader.GetDecimal(44);
                _summa4 = reader.GetDecimal(45);
                _summa5 = reader.GetDecimal(46);

                _memo = reader.IsDBNull(47) ? string.Empty : reader.GetString(47);
                _mGuid = reader.IsDBNull(48) ? Guid.Empty : reader.GetGuid(48);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }

        internal class TpvCollection : List<DocumentDetailPlan>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.KindId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.DocId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ProductId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ProductId2, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ProductId3, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ProductId4, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ProductId5, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId2, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId3, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId4, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId5, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StringValue1, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.StringValue2, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.StringValue3, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.StringValue4, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.StringValue5, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.AgentId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AgentId2, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AgentId3, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AgentId4, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AgentId5, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Qty, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Qty2, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Qty3, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Qty4, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Qty5, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Price, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Price2, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Price3, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Price4, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Price5, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Summa, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Summa2, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Summa3, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Summa4, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Summa5, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.MGuid, SqlDbType.UniqueIdentifier)
                    );

                foreach (DocumentDetailPlan doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailPlan doc)
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
            sdr.SetInt32(10, doc.Kind);

            sdr.SetInt32(11, doc.Document.Id);

            if (doc.ProductId != 0)
                sdr.SetInt32(12, doc.ProductId);
            else
                sdr.SetValue(12, DBNull.Value);

            if (doc.ProductId2 != 0)
                sdr.SetInt32(13, doc.ProductId2);
            else
                sdr.SetValue(13, DBNull.Value);

            if (doc.ProductId3 != 0)
                sdr.SetInt32(14, doc.ProductId3);
            else
                sdr.SetValue(14, DBNull.Value);

            if (doc.ProductId4 != 0)
                sdr.SetInt32(15, doc.ProductId4);
            else
                sdr.SetValue(15, DBNull.Value);

            if (doc.ProductId5 != 0)
                sdr.SetInt32(16, doc.ProductId5);
            else
                sdr.SetValue(16, DBNull.Value);

            if (doc.AnaliticId == 0)
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetInt32(17, doc.AnaliticId);

            if (doc.AnaliticId2 == 0)
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetInt32(18, doc.AnaliticId2);

            if (doc.AnaliticId3 == 0)
                sdr.SetValue(19, DBNull.Value);
            else
                sdr.SetInt32(19, doc.AnaliticId3);
            if (doc.AnaliticId4 == 0)
                sdr.SetValue(20, DBNull.Value);
            else
                sdr.SetInt32(20, doc.AnaliticId4);
            if (doc.AnaliticId5 == 0)
                sdr.SetValue(21, DBNull.Value);
            else
                sdr.SetInt32(21, doc.AnaliticId5);


            if (string.IsNullOrEmpty(doc.StringValue1))
                sdr.SetValue(22, DBNull.Value);
            else
                sdr.SetString(22, doc.StringValue1);

            if (string.IsNullOrEmpty(doc.StringValue2))
                sdr.SetValue(23, DBNull.Value);
            else
                sdr.SetString(23, doc.StringValue2);

            if (string.IsNullOrEmpty(doc.StringValue3))
                sdr.SetValue(24, DBNull.Value);
            else
                sdr.SetString(24, doc.StringValue3);

            if (string.IsNullOrEmpty(doc.StringValue4))
                sdr.SetValue(25, DBNull.Value);
            else
                sdr.SetString(25, doc.StringValue4);

            if (string.IsNullOrEmpty(doc.StringValue5))
                sdr.SetValue(26, DBNull.Value);
            else
                sdr.SetString(26, doc.StringValue5);

            if (doc.AgentId == 0)
                sdr.SetValue(27, DBNull.Value);
            else
                sdr.SetInt32(27, doc.AgentId);
            if (doc.AgentId2 == 0)
                sdr.SetValue(28, DBNull.Value);
            else
                sdr.SetInt32(28, doc.AgentId2);
            if (doc.AgentId3 == 0)
                sdr.SetValue(29, DBNull.Value);
            else
                sdr.SetInt32(29, doc.AgentId3);
            if (doc.AgentId4 == 0)
                sdr.SetValue(30, DBNull.Value);
            else
                sdr.SetInt32(30, doc.AgentId4);
            if (doc.AgentId5 == 0)
                sdr.SetValue(31, DBNull.Value);
            else
                sdr.SetInt32(31, doc.AgentId5);

            sdr.SetDecimal(32, doc.Qty);
            sdr.SetDecimal(33, doc.Qty2);
            sdr.SetDecimal(34, doc.Qty3);
            sdr.SetDecimal(35, doc.Qty4);
            sdr.SetDecimal(36, doc.Qty5);

            sdr.SetDecimal(37, doc.Price);
            sdr.SetDecimal(38, doc.Price2);
            sdr.SetDecimal(39, doc.Price3);
            sdr.SetDecimal(40, doc.Price4);
            sdr.SetDecimal(41, doc.Price5);

            sdr.SetDecimal(42, doc.Summa);
            sdr.SetDecimal(43, doc.Summa2);
            sdr.SetDecimal(44, doc.Summa3);
            sdr.SetDecimal(45, doc.Summa4);
            sdr.SetDecimal(46, doc.Summa5);

            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(47, DBNull.Value);
            else
                sdr.SetString(47, doc.Memo);

            sdr.SetGuid(48, doc.MGuid);
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
            _baseStruct = new BaseStructDocumentDetailPlan();
        }

        #endregion

        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "Load"</remarks>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            //throw new NotImplementedException();
        }

        #region IChainsAdvancedList<DocumentDetailPlan,FileData> Members

        List<IChainAdvanced<DocumentDetailPlan, FileData>> IChainsAdvancedList<DocumentDetailPlan, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<DocumentDetailPlan, FileData>)this).GetLinks(56);
        }

        List<IChainAdvanced<DocumentDetailPlan, FileData>> IChainsAdvancedList<DocumentDetailPlan, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<DocumentDetailPlan, FileData>.GetChainView()
        {
            // TODO: 
            return null; //ChainValueView.GetView<DocumentDetailPlan, FileData>(this);
        }
        public List<IChainAdvanced<DocumentDetailPlan, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<DocumentDetailPlan, FileData>> collection = new List<IChainAdvanced<DocumentDetailPlan, FileData>>();
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
                                ChainAdvanced<DocumentDetailPlan, FileData> item = new ChainAdvanced<DocumentDetailPlan, FileData> { Workarea = Workarea, Left = this };
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