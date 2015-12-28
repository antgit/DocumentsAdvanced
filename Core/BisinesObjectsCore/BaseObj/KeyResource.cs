using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// ������� ����� ��������
    /// </summary>
    /// <remarks>
    /// � ������� ����������� ����������� ���������� �������������� ������ <see
    /// cref="Load">Load</see> � <see
    /// cref="SetParametersToInsertUpdate">SetParametersToInsertUpdate</see>
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public abstract class KeyResource<T>: BaseCoreObject
    {
        protected KeyResource():base()
        {
            
        }
        #region ��������
        private int _kindId;
        public int KindId
        {
            get
            {
                return _kindId;
            }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.KindId);
                _kindId = value;
                OnPropertyChanged(GlobalPropertyNames.KindId);
            }
        }
        private string _code;
        /// <summary>
        /// ���
        /// </summary>
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.Code);
                _code = value;
                OnPropertyChanged(GlobalPropertyNames.Code);
            }
        }
        private T _value;
        /// <summary>
        /// ��������
        /// </summary>
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.Value);
                _value = value;
                OnPropertyChanged(GlobalPropertyNames.Value);
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

            if (_kindId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.KindId, XmlConvert.ToString(_kindId));
            if (!string.IsNullOrEmpty(_code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, _code);
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.KindId) != null)
                _kindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.KindId));
            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                _code = reader.GetAttribute(GlobalPropertyNames.Code);
        }
        #endregion

        #region ���� ������
        /// <summary>���������</summary>
        /// <param name="reader">������ SqlDataReader</param>
        /// <param name="endInit">��������� ������������� �������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _kindId = reader.GetInt32(9);
                _code = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
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
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.KindId, SqlDbType.Int) {IsNullable = false, Value = _kindId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 255) {IsNullable = true};
            if (string.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion
    }
}