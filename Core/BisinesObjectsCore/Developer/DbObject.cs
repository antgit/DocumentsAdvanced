using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects.Developer
{
    /// <summary>��������� ������� "������ ���� ������"</summary>
    internal struct DbObjectStruct
    {
        /// <summary>��������</summary>
        public string Description;
        /// <summary>��������� �������� ������</summary>
        public string ProcedureExport;
        /// <summary>��������� ������� ������</summary>
        public string ProcedureImport;
        /// <summary>����� �������</summary>
        public string Schema;
    }
    /// <summary>
    /// ������ ���� ������
    /// </summary>
    /// <remarks>�������� Name �������� ������������ ������� ���� ������</remarks>
    public sealed class DbObject : BaseCore<DbObject>
    {
        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>������� ���� ������, ������������� �������� 1</summary>
        public const int KINDVALUE_TABLE = 1;
        /// <summary>�������������, ������������� �������� 2</summary>
        public const int KINDVALUE_VIEW = 2;
        /// <summary>�������� ���������, ������������� �������� 3</summary>
        public const int KINDVALUE_STOREDPROC = 3;
        /// <summary>��������� �������, ������������� �������� 4</summary>
        public const int KINDVALUE_FUNC = 4;
        /// <summary>��������� �������, ������������� �������� 5</summary>
        public const int KINDVALUE_TABLEFUNC = 5;
        /// <summary>����� ������, ������������� �������� 6</summary>
        public const int KINDVALUE_SCHEMA = 6;

        /// <summary>������� ���� ������, ������������� �������� 2686977</summary>
        public const int KINDID_TABLE = 2686977;
        /// <summary>�������������, ������������� �������� 2686978</summary>
        public const int KINDID_VIEW = 2686978;
        /// <summary>�������� ���������, ������������� �������� 2686979</summary>
        public const int KINDID_STOREDPROC = 2686979;
        /// <summary>��������� �������, ������������� �������� 2686980</summary>
        public const int KINDID_FUNC = 2686980;
        /// <summary>��������� �������, ������������� �������� 2686981</summary>
        public const int KINDID_TABLEFUNC = 2686981;
        /// <summary>����� ������, ������������� �������� 2686982</summary>
        public const int KINDID_SCHEMA = 2686982;
        // ReSharper restore InconsistentNaming
        #endregion
        /// <summary>
        /// �����������
        /// </summary>
        public DbObject()
            : base((short)WhellKnownDbEntity.DbObject)
        {
            
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
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
        #region ��������

        private string _schema;
        /// <summary>
        /// ����� �������
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
        /// ���������� ������ ������������ � ������ �����
        /// </summary>
        /// <remarks>������ ������������ �������������, ��������: Core.Units</remarks>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}.{1}", _schema, Name); 
        }

        private string _description;
        /// <summary>
        /// ��������
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
        /// ��������� ������� ������
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
        /// ��������� �������� ������
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
        /// ������� �������
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

        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
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
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
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

        #region ���������
        DbObjectStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
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

        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
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

        #region ���� ������

        /// <summary>�������� ������</summary>
        /// <param name="reader">������ SqlDataReader</param>
        /// <param name="endInit">������� ��������� ��������</param>
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
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>���������� �������� ���������� ��� �������� ��������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ����������</param>
        /// <param name="validateVersion">��������� �� �������� ������</param>
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