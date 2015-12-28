using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace BusinessObjects.Documents
{

    internal struct DocumentDetailCoordinatesStruct
    {
        /// <summary>Идентификатор документа</summary>
        public int OwnId;
        /// <summary>X</summary>
        public decimal X;
        /// <summary>Y</summary>
        public decimal Y;
        /// <summary>Z</summary>
        public decimal Z;
        /// <summary>Порядок следования</summary>
        public int OrderNo;
        /// <summary>Строка1</summary>
        public string StringValue1;
        /// <summary>Строка2</summary>
        public string StringValue2;
        /// <summary>Строка3</summary>
        public string StringValue3;
    }
   
    /// <summary>
    /// Списки координат для строк детализации
    /// </summary>
    public sealed class DocumentDetailCoordinates : BaseCoreObject, ICopyValue<DocumentDetailCoordinates>, IEquatable<DocumentDetailCoordinates>, IComparable
    {
        /// <summary>
        /// Список документов которые возможно использовать как шаблоны для подписания
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="kind">Идентификатор типа документа</param>
        /// <returns></returns>
        /*public static List<Document> GetCollectionDocumentDetailCoordinatesTemplates(Workarea wa, int kind)
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Document>().Entity.FindMethod("Document.GetSignaturesDocuments").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = kind;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
                            item.Load(reader);
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
        }*/

        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Первая сторона, соответствует значению 1</summary>
        //public const int KINDVALUE_FIRST = 1;
        /// <summary>Вторая сторона, соответствует значению 2</summary>
        //public const int KINDVALUE_SECOND = 2;
        /// <summary>Третья сторона, соответствует значению 3</summary>
        //public const int KINDVALUE_THIRD = 3;

        /// <summary>Первая сторона, соответствует значению 3407873</summary>
        //public const int KINDID_FIRST = 3407873;
        /// <summary>Вторая сторона, соответствует значению 3407874</summary>
        //public const int KINDID_SECOND = 3407874;
        /// <summary>Третья сторона, соответствует значению 3407875</summary>
        //public const int KINDID_THIRD = 3407875;
        // ReSharper restore InconsistentNaming
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailCoordinates(): base()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentDetailCoordinates;
        }

        void ICopyValue<DocumentDetailCoordinates>.CopyValue(DocumentDetailCoordinates template)
        {
            CopyValue(template);
        }

        public void CopyValue(DocumentDetailCoordinates template)
        {
            OwnId = template.OwnId;
            Workarea = Workarea;
            StateId = State.STATEACTIVE;
            X = template.X;
            Y = template.Y;
            Z = template.Z;
            OrderNo = template.OrderNo;
            StringValue1 = template.StringValue1;
            StringValue2 = template.StringValue2;
            StringValue3 = template.StringValue3;
            DatabaseId = Workarea.MyBranche.Id;
        }

        bool IEquatable<DocumentDetailCoordinates>.Equals(DocumentDetailCoordinates other)
        {
            return Workarea == other.Workarea & Id == other.Id & DbSourceId == other.DbSourceId & Entity == other.Entity;
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            DocumentDetailCoordinates otherObj = (DocumentDetailCoordinates)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(DocumentDetailCoordinates other)
        {
            return Id.CompareTo(other.Id);
        }
        
        #region Свойства
        private int _ownId;
        /// <summary>
        /// Идентификатор строки документа
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
        
        private decimal _X;
        /// <summary>X</summary>
        public decimal X
        {
            get { return _X; }
            set
            {
                if (value == _X) return;
                OnPropertyChanging(GlobalPropertyNames.X);
                _X = value;
                OnPropertyChanged(GlobalPropertyNames.X);
            }
        }
        
        public decimal _Y;
        /// <summary>Y</summary>
        public decimal Y
        {
            get { return _Y; }
            set
            {
                if (value == _Y) return;
                OnPropertyChanging(GlobalPropertyNames.Y);
                _Y = value;
                OnPropertyChanged(GlobalPropertyNames.Y);
            }
        }
        
        public decimal _Z;
        /// <summary>Z</summary>
        public decimal Z
        {
            get { return _Z; }
            set
            {
                if (value == _Z) return;
                OnPropertyChanging(GlobalPropertyNames.Z);
                _Z = value;
                OnPropertyChanged(GlobalPropertyNames.Z);
            }
        }
        
        public int _OrderNo;
        /// <summary>Порядок следования</summary>
        public int OrderNo
        {
            get { return _OrderNo; }
            set
            {
                if (value == _OrderNo) return;
                OnPropertyChanging(GlobalPropertyNames.OrderNo);
                _OrderNo = value;
                OnPropertyChanged(GlobalPropertyNames.OrderNo);
            }
        }

        public string _StringValue1;
        /// <summary>Строка1</summary>
        public string StringValue1
        {
            get { return _StringValue1; }
            set
            {
                if (value == _StringValue1) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue1);
                _StringValue1 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue1);
            }
        }
        
        public string _StringValue2;
        /// <summary>Строка2</summary>
        public string StringValue2
        {
            get { return _StringValue2; }
            set
            {
                if (value == _StringValue2) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue2);
                _StringValue2 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue2);
            }
        }

        public string _StringValue3;
        /// <summary>Строка3</summary>
        public string StringValue3
        {
            get { return _StringValue3; }
            set
            {
                if (value == _StringValue3) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue3);
                _StringValue3 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue3);
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

            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));

            writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_OrderNo));

            if (_X != 0)
                writer.WriteAttributeString(GlobalPropertyNames.X, XmlConvert.ToString(_X));
            if (_Y != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Y, XmlConvert.ToString(_Y));
            if (_Z != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Z, XmlConvert.ToString(_Z));

            if (!string.IsNullOrEmpty(_StringValue1))
                writer.WriteAttributeString(GlobalPropertyNames.StringValue1, _StringValue1);
            if (!string.IsNullOrEmpty(_StringValue2))
                writer.WriteAttributeString(GlobalPropertyNames.StringValue2, _StringValue2);
            if (!string.IsNullOrEmpty(_StringValue3))
                writer.WriteAttributeString(GlobalPropertyNames.StringValue3, _StringValue3);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnId));

            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _OrderNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OrderNo));

            if (reader.GetAttribute(GlobalPropertyNames.X) != null)
                _X = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.X));
            if (reader.GetAttribute(GlobalPropertyNames.Y) != null)
                _Y = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Y));
            if (reader.GetAttribute(GlobalPropertyNames.Z) != null)
                _Z = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Z));

            if (reader.GetAttribute(GlobalPropertyNames.StringValue1) != null)
                _StringValue1 = reader.GetAttribute(GlobalPropertyNames.StringValue1);
            if (reader.GetAttribute(GlobalPropertyNames.StringValue2) != null)
                _StringValue2 = reader.GetAttribute(GlobalPropertyNames.StringValue2);
            if (reader.GetAttribute(GlobalPropertyNames.StringValue3) != null)
                _StringValue3 = reader.GetAttribute(GlobalPropertyNames.StringValue3);
        }
        #endregion

        #region Методы
        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            if (_ownId == 0)
                throw new ValidateException("Не указана строка детализации документа");
            if (_X == 0)
                throw new ValidateException("Не указан X");
            if (_Y == 0)
                throw new ValidateException("Не указан Y");
        }
        
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.GetInt32(9);

                _X = reader.GetDecimal(10);
                _Y = reader.GetDecimal(11);
                _Z = reader.GetDecimal(12);

                _OrderNo = reader.GetInt32(13);

                _StringValue1 = reader.GetString(14);
                _StringValue2 = reader.GetString(15);
                _StringValue3 = reader.GetString(16);
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
            SqlParameter prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.OwnId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _ownId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.X, SqlDbType.Decimal);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _X;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Y, SqlDbType.Decimal);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _Y;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Z, SqlDbType.Decimal);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _Z;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.OrderNo, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _OrderNo;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.StringValue1, SqlDbType.NVarChar, 255);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_StringValue1))
                prm.Value = DBNull.Value;
            else
                prm.Value = _StringValue1;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.StringValue2, SqlDbType.NVarChar, 255);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_StringValue2))
                prm.Value = DBNull.Value;
            else
                prm.Value = _StringValue2;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.StringValue3, SqlDbType.NVarChar, 255);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_StringValue3))
                prm.Value = DBNull.Value;
            else
                prm.Value = _StringValue3;
        }

        ///// <summary>
        ///// Удаление объекта из базы данных
        ///// </summary>
        //public void Delete()
        //{
        //    Workarea.DeleteById(Id, Workarea.Empty<Document>().Entity.FindMethod("SignatureDelete").FullName);    
        //}
        ///// <summary>
        ///// Загрузить данные объекта из базы данных по идентификатору
        ///// </summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.Empty<Document>().Entity.FindMethod("SignatureLoad").FullName);
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

        private DocumentDetailCoordinatesStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new DocumentDetailCoordinatesStruct
                {
                    OwnId = OwnId,
                    X = X,
                    Y = Y,
                    Z = Z,
                    OrderNo = OrderNo,
                    StringValue1 = StringValue1,
                    StringValue2 = StringValue2,
                    StringValue3 = StringValue3
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
            OrderNo = _baseStruct.OrderNo;
            X = _baseStruct.X;
            Y = _baseStruct.Y;
            Z = _baseStruct.Z;
            OrderNo = _baseStruct.OrderNo;
            StringValue1 = _baseStruct.StringValue1;
            StringValue2 = _baseStruct.StringValue2;
            StringValue3 = _baseStruct.StringValue3;
            IsChanged = false;
        }

        internal class TpvCollection : List<DocumentDetailCoordinates>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.OwnId, SqlDbType.Int),

                    new SqlMetaData(GlobalPropertyNames.X, SqlDbType.Decimal),
                    new SqlMetaData(GlobalPropertyNames.Y, SqlDbType.Decimal),
                    new SqlMetaData(GlobalPropertyNames.Z, SqlDbType.Decimal),

                    new SqlMetaData(GlobalPropertyNames.OrderNo, SqlDbType.Int),

                    new SqlMetaData(GlobalPropertyNames.StringValue1, SqlDbType.VarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.StringValue2, SqlDbType.VarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.StringValue3, SqlDbType.VarChar, 255)
                );

                foreach (DocumentDetailCoordinates doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }

        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailCoordinates doc)
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
            sdr.SetInt32(9, doc.OwnId);
            sdr.SetDecimal(10, doc.X);
            sdr.SetDecimal(11, doc.Y);
            sdr.SetDecimal(12, doc.Z);
            sdr.SetInt32(13, doc.OrderNo);

            if (doc.StringValue1.Length != 0)
                sdr.SetString(14, doc.StringValue1);
            else
                sdr.SetValue(14, DBNull.Value);

            if (doc.StringValue2.Length != 0)
                sdr.SetString(15, doc.StringValue2);
            else
                sdr.SetValue(15, DBNull.Value);

            if (doc.StringValue3.Length != 0)
                sdr.SetString(16, doc.StringValue3);
            else
                sdr.SetValue(16, DBNull.Value);

            return sdr;
        }
    }
}
