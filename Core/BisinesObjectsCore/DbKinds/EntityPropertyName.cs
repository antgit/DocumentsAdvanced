using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// ������������ ��������
    /// </summary>
    /// <remarks>
    /// ������������ �������� ����������� ��� ��������� ������� ��������� �������� �
    /// ������� <see cref="T:BusinessObjects.EntityProperty">&quot;��������
    /// �������&quot;</see>
    /// </remarks>
    public sealed class EntityPropertyName: BaseCoreObject
    {
        /// <summary>
        /// �����������
        /// </summary>
        public EntityPropertyName():base()
        {

        }
        #region ��������

        private string _name;
        /// <summary>
        /// ������������
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                OnPropertyChanging(GlobalPropertyNames.Name);
                _name = value;
                OnPropertyChanged(GlobalPropertyNames.Name);
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

        private string _displayName;
        /// <summary>
        /// ������������ ���
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                if (value == _displayName) return;
                OnPropertyChanging(GlobalPropertyNames.DisplayName);
                _displayName = value;
                OnPropertyChanged(GlobalPropertyNames.DisplayName);
            }
        }

        private int _cultureId;
        /// <summary>
        /// ������������� �����
        /// </summary>
        public int CultureId
        {
            get { return _cultureId; }
            set
            {
                if (value == _cultureId) return;
                OnPropertyChanging(GlobalPropertyNames.CultureId);
                _cultureId = value;
                OnPropertyChanged(GlobalPropertyNames.CultureId);
            }
        }

        private CultureInfo _cultureInfo;
        /// <summary>
        /// ����
        /// </summary>
        public CultureInfo CultureInfo
        {
            get
            {
                if (_cultureId == 0)
                    return null;
                if (_cultureInfo == null)
                    _cultureInfo = CultureInfo.GetCultureInfo(_cultureId);
                else if (_cultureInfo.LCID != _cultureId)
                    _cultureInfo = CultureInfo.GetCultureInfo(_cultureId);
                return _cultureInfo;
            }
            set
            {
                if (_cultureInfo == value) return;
                OnPropertyChanging(GlobalPropertyNames.CultureInfo);
                _cultureInfo = value;
                _cultureId = _cultureInfo == null ? 0 : _cultureInfo.LCID;
                OnPropertyChanged(GlobalPropertyNames.CultureInfo);
            }
        }
        
        #endregion

        #region ������������
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (!string.IsNullOrEmpty(_displayName))
                writer.WriteAttributeString(GlobalPropertyNames.DisplayName, _displayName);
            if (_cultureId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.CultureId, XmlConvert.ToString(_cultureId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.DisplayName) != null)
                _displayName = reader.GetAttribute(GlobalPropertyNames.DisplayName);
            if (reader.GetAttribute(GlobalPropertyNames.CultureId) != null)
                _cultureId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.CultureId));
        }
        #endregion

        /// <summary>��������� ������ � ���� ������</summary>
        /// <remarks>� ����������� �� ��������� ������� <see cref="EntityKind.IsNew"/> 
        /// ����������� �������� ��� ���������� �������</remarks>
        public void Save()
        {
            Validate();
            if (IsNew)
                Create(Workarea.FindMethod("EntityPropertyNameInsertUpdate").FullName);
            else
                Update(Workarea.FindMethod("EntityPropertyNameInsertUpdate").FullName, true);
        }

        public override void Load(int value)
        {
            Load(value, Workarea.FindMethod("EntityPropertyNameLoad").FullName);
        }
        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();
            if (_cultureId == 0)
                // TODO:
                throw new ValidateException("�� ������ ������������� �����");
            if (string.IsNullOrEmpty(_name))
                // TODO:
                throw new ValidateException("�� ������� ������������");
            if (string.IsNullOrEmpty(_displayName))
                // TODO:
                throw new ValidateException("�� ������� ������������ ������������");
        }
        /// <summary>���������</summary>
        /// <param name="reader">������ SqlDataReader</param>
        /// <param name="endInit">��������� ������������� �������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _name = reader.GetString(9);
                _memo = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                _displayName = reader.GetString(11);
                _cultureId = reader.GetInt32(12);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar,255) { IsNullable = false, Value = _name};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar,255) { IsNullable = true};
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
                prm.Value = _memo;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DisplayName, SqlDbType.NVarChar,255) { IsNullable = false, Value = _displayName };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.CultureId, SqlDbType.Int) { IsNullable = false, Value = _cultureId};
            sqlCmd.Parameters.Add(prm);
        }

        /// <summary>
        /// ��������� ������������� �������
        /// </summary>
        /// <returns>
        /// ��������� ������������� ������� ��������������� <see
        /// cref="P:BusinessObjects.EntityPropertyName.Name">������������</see> �������
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.IsNullOrEmpty(_displayName) ? _name : _displayName;
        }
        /// <summary>
        /// ������������� �������� � ���� ��������������� ������
        /// </summary>
        /// <remarks>
        /// ���������� �����: 
        /// <para> </para>
        /// <list type="table">
        /// <listheader>
        /// <term>�����</term>
        /// <description>��������</description></listheader>
        /// <item>
        /// <term>%name%</term>
        /// <description><see
        /// cref="P:BusinessObjects.EntityPropertyName.Name">������������</see></description></item>
        /// <item>
        /// <term>%CultureId%</term>
        /// <description><see
        /// cref="P:BusinessObjects.EntityPropertyName.CultureId">�������������
        /// �����</see></description></item>
        /// <item>
        /// <term>%Culture%</term>
        /// <description><see
        /// cref="P:BusinessObjects.EntityPropertyName.CultureInfo">����</see></description></item>
        /// <item>
        /// <term> %DisplayName%</term>
        /// <description><see
        /// cref="P:BusinessObjects.EntityPropertyName.DisplayName">������������
        /// ������������</see></description></item></list>
        /// </remarks>
        /// <param name="mask"></param>
        /// <returns>
        /// ��������� ������������� ��������������� �����
        /// </returns>
        public override string ToString(string mask)
        {
            string res = base.ToString(mask);

            // ���������������� ��� ��������
            if (_name != null) res = res.Replace("%name%", _name);
            // ���������������� ��� ��������
            res = res.Replace("%CultureId%", _cultureId.ToString());
            // ���������������� ��� ����������
            if (_memo != null) res = res.Replace("%memo%", _memo);
            // ���������������� ��� ������
            res = res.Replace("%CultureInfo%", CultureInfo.ToString());
            // ���������������� ��� �������������� �������
            res = res.Replace("%DisplayName%", _displayName);
            return res;
        }
    }
}