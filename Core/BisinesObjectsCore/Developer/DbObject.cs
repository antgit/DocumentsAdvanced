using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects.Developer
{
    /// <summary>Структура объекта "Объект базы данных"</summary>
    internal struct DbObjectStruct
    {
        /// <summary>Описание</summary>
        public string Description;
        /// <summary>Процедура экспорта данных</summary>
        public string ProcedureExport;
        /// <summary>Процедура импорта данных</summary>
        public string ProcedureImport;
        /// <summary>Схема таблицы</summary>
        public string Schema;
    }
    /// <summary>
    /// Объект базы данных
    /// </summary>
    /// <remarks>Свойство Name содержит наименование объекта базы данных</remarks>
    public sealed class DbObject : BaseCore<DbObject>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Таблица базы данных, соответствует значению 1</summary>
        public const int KINDVALUE_TABLE = 1;
        /// <summary>Представление, соответствует значению 2</summary>
        public const int KINDVALUE_VIEW = 2;
        /// <summary>Хранимая процедура, соответствует значению 3</summary>
        public const int KINDVALUE_STOREDPROC = 3;
        /// <summary>Скалярная функция, соответствует значению 4</summary>
        public const int KINDVALUE_FUNC = 4;
        /// <summary>Табличная функция, соответствует значению 5</summary>
        public const int KINDVALUE_TABLEFUNC = 5;
        /// <summary>Схема данных, соответствует значению 6</summary>
        public const int KINDVALUE_SCHEMA = 6;

        /// <summary>Таблица базы данных, соответствует значению 2686977</summary>
        public const int KINDID_TABLE = 2686977;
        /// <summary>Представление, соответствует значению 2686978</summary>
        public const int KINDID_VIEW = 2686978;
        /// <summary>Хранимая процедура, соответствует значению 2686979</summary>
        public const int KINDID_STOREDPROC = 2686979;
        /// <summary>Скалярная функция, соответствует значению 2686980</summary>
        public const int KINDID_FUNC = 2686980;
        /// <summary>Табличная функция, соответствует значению 2686981</summary>
        public const int KINDID_TABLEFUNC = 2686981;
        /// <summary>Схема данных, соответствует значению 2686982</summary>
        public const int KINDID_SCHEMA = 2686982;
        // ReSharper restore InconsistentNaming
        #endregion
        /// <summary>
        /// Конструктор
        /// </summary>
        public DbObject()
            : base((short)WhellKnownDbEntity.DbObject)
        {
            
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override DbObject Clone(bool endInit)
        {
            DbObject obj = base.Clone(endInit);
            obj.Description = Description;
            obj.ProcedureExport = ProcedureExport;
            obj.ProcedureImport = ProcedureImport;
            obj.Schema = Schema;
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства

        private string _schema;
        /// <summary>
        /// Схема таблицы
        /// </summary>
        public string Schema
        {
            get { return _schema; }
            set
            {
                if (value == _schema) return;
                OnPropertyChanging(GlobalPropertyNames.Schema);
                _schema = value;
                OnPropertyChanged(GlobalPropertyNames.Schema);
            }
        }
        /// <summary>
        /// Возвращает полное наименование с учетом схемы
        /// </summary>
        /// <remarks>Полное наименование соответствует, например: Core.Units</remarks>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}.{1}", _schema, Name); 
        }

        private string _description;
        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                OnPropertyChanging(GlobalPropertyNames.Description);
                _description = value;
                OnPropertyChanged(GlobalPropertyNames.Description);
            }
        }


        private string _procedureImport;
        /// <summary>
        /// Процедура импорта данных
        /// </summary>
        public string ProcedureImport
        {
            get { return _procedureImport; }
            set
            {
                if (value == _procedureImport) return;
                OnPropertyChanging(GlobalPropertyNames.ProcedureImport);
                _procedureImport = value;
                OnPropertyChanged(GlobalPropertyNames.ProcedureImport);
            }
        }

        private string _procedureExport;
        /// <summary>
        /// Процедура экспорта данных
        /// </summary>
        public string ProcedureExport
        {
            get { return _procedureExport; }
            set
            {
                if (value == _procedureExport) return;
                OnPropertyChanging(GlobalPropertyNames.ProcedureExport);
                _procedureExport = value;
                OnPropertyChanged(GlobalPropertyNames.ProcedureExport);
            }
        }
        
        private List<DbObjectChild> _columns;
        /// <summary>
        /// Столбцы таблицы
        /// </summary>
        public List<DbObjectChild> ColumnInfos
        {
            get
            {
                if (_columns==null)
                    _columns = new List<DbObjectChild>();
                if (Workarea != null && _columns.Count==0)
                    _columns = DbObjectChild.GetCollection(Workarea, Schema, Name);
                return _columns;
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

            if (!string.IsNullOrEmpty(_description))
                writer.WriteAttributeString(GlobalPropertyNames.Description, _description);
            if (!string.IsNullOrEmpty(_procedureExport))
                writer.WriteAttributeString(GlobalPropertyNames.ProcedureExport, _procedureExport);
            if (!string.IsNullOrEmpty(_procedureImport))
                writer.WriteAttributeString(GlobalPropertyNames.ProcedureImport, _procedureImport);
            if (!string.IsNullOrEmpty(_schema))
                writer.WriteAttributeString(GlobalPropertyNames.Schema, _schema);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Description) != null)
                _description = reader.GetAttribute(GlobalPropertyNames.Description);
            if (reader.GetAttribute(GlobalPropertyNames.ProcedureExport) != null)
                _procedureExport = reader.GetAttribute(GlobalPropertyNames.ProcedureExport);
            if (reader.GetAttribute(GlobalPropertyNames.ProcedureImport) != null)
                _procedureImport = reader.GetAttribute(GlobalPropertyNames.ProcedureImport);
            if (reader.GetAttribute(GlobalPropertyNames.Schema) != null)
                _schema = reader.GetAttribute(GlobalPropertyNames.Schema);
        }
        #endregion

        #region Состояние
        DbObjectStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new DbObjectStruct
                {
                    Description = _description,
                    ProcedureExport = _procedureExport,
                    ProcedureImport = _procedureImport,
                    Schema = _schema
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
            Description = _baseStruct.Description;
            ProcedureExport = _baseStruct.ProcedureExport;
            ProcedureImport = _baseStruct.ProcedureImport;
            Schema = _baseStruct.Schema;

            IsChanged = false;
        }
        #endregion

        #region База данных

        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _schema = reader.GetString(17);
                _description = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                _procedureImport = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                _procedureExport = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Schema, SqlDbType.NVarChar, 128)
            {
                IsNullable = false,
                Value = _schema
            };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Description, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_description))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _description.Length;
                prm.Value = _description;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ProcedureImport, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_procedureImport))
                prm.Value = DBNull.Value;
            else
                prm.Value = _procedureImport;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ProcedureExport, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_procedureExport))
                prm.Value = DBNull.Value;
            else
                prm.Value = _procedureExport;
            sqlCmd.Parameters.Add(prm);
        } 
        #endregion
    }
}