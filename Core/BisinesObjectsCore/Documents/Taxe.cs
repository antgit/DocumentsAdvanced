using System;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace BusinessObjects.Documents
{

    internal struct DocumentTaxStruct
    {
        /// <summary>Идентификатор документа</summary>
        public int DocumentId;
        /// <summary>Идентификатор налога</summary>
        public int TaxId;
        /// <summary>Сумма</summary>
        public decimal Summa;
    }
    /// <summary>
    /// Налог документа
    /// </summary>
    public sealed class Taxe : BaseCoreObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Taxe()
            : base()
        {
            EntityId = (short) WhellKnownDbEntity.Taxe;
        }

        #region Свойства
        
        private int _documentId;
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public int DocumentId
        {
            get { return _documentId; }
            set
            {
                if (value == _documentId) return;
                OnPropertyChanging(GlobalPropertyNames.DocumentId);
                _documentId = value;
                OnPropertyChanged(GlobalPropertyNames.DocumentId);
            }
        }

        private int _taxId;
        /// <summary>
        /// Идентификатор налога
        /// </summary>
        public int TaxId
        {
            get { return _taxId; }
            set
            {
                if (value == _taxId) return;
                OnPropertyChanging(GlobalPropertyNames.TaxId);
                _taxId = value;
                OnPropertyChanged(GlobalPropertyNames.TaxId);
            }
        }

        private Analitic _tax;
        /// <summary>
        /// Налог
        /// </summary>
        public Analitic Tax
        {
            get
            {
                if (_taxId == 0)
                    return default(Analitic);
                if (_tax == null)
                    _tax = Workarea.Cashe.GetCasheData<Analitic>().Item(_taxId);
                else if (_tax.Id != _taxId)
                    _tax = Workarea.Cashe.GetCasheData<Analitic>().Item(_taxId);
                return _tax;
            }
            set
            {
                if (_tax == value) return;
                OnPropertyChanging(GlobalPropertyNames.Tax);
                _tax = value;
                _taxId = _tax == null ? 0 : Tax.Id;
                OnPropertyChanged(GlobalPropertyNames.Tax);
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
                if (value == _summa) return;
                OnPropertyChanging(GlobalPropertyNames.Summa);
                _summa = value;
                OnPropertyChanged(GlobalPropertyNames.Summa);
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

            if (_documentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DocumentId, XmlConvert.ToString(_documentId));
            if (_taxId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TaxId, XmlConvert.ToString(_taxId));
            if (_summa != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Summa, XmlConvert.ToString(_summa));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.DocumentId) != null)
                _documentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DocumentId));
            if (reader.GetAttribute(GlobalPropertyNames.TaxId) != null)
                _taxId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TaxId));
            if (reader.GetAttribute(GlobalPropertyNames.Summa) != null)
                _summa = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Summa));
        }
        #endregion

        #region Методы
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _documentId = reader.GetInt32(9);
                _taxId = reader.GetInt32(10);
                _summa = reader.GetDecimal(11);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        ///// <summary>
        ///// Сохранить объект в базе данных
        ///// </summary>
        ///// <remarks>Если идентификатор объекта <see cref="Id"/> равен 0 
        ///// выполняется создание в противном случае обновление объекта</remarks>
        //public void Save()
        //{
        //    Validate();
        //    if (IsNew)
        //        Create(Workarea.Empty<Document>().Entity.FindMethod("TaxInsert").FullName);
        //    else
        //        Update(Workarea.Empty<Document>().Entity.FindMethod("TaxUpdate").FullName, true);
        //}

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.DocId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _documentId;
            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.TaxId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _taxId;
            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Summa, SqlDbType.Money);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _summa;
        }

        ///// <summary>
        ///// Удаление объекта из базы данных
        ///// </summary>
        //public void Delete()
        //{
        //    Workarea.DeleteById(Id, Workarea.Empty<Document>().Entity.FindMethod("TaxDelete").FullName);
        //}

        ///// <summary>
        ///// Загрузить данные объекта из базы данных по идентификатору
        ///// </summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.Empty<Document>().Entity.FindMethod("TaxLoad").FullName);
        //}

        ///// <summary>
        ///// Загрузить текущий объект
        ///// </summary>
        ///// <remarks>Загрузка возможна только для объекта существующего в базе данных, чей идентификатор не равен 0</remarks>
        //public void Load()
        //{
        //    Load(Id);
        //}
        #endregion

        #region Состояния
        private DocumentTaxStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new DocumentTaxStruct
                {
                    DocumentId = DocumentId,
                    TaxId = TaxId,
                    Summa = Summa
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
            DocumentId = _baseStruct.DocumentId;
            TaxId = _baseStruct.TaxId;
            Summa = _baseStruct.Summa;
            IsChanged = false;
        }
        #endregion
    }
}
