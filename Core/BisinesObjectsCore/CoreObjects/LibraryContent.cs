using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// �������� ���������� ����������
    /// </summary>
    public sealed class LibraryContent : BaseCoreObject
    {
        public LibraryContent()
            : base()
        {
            EntityId = (short) WhellKnownDbEntity.LibraryContent;
        }
        #region ��������
	
        private int _libraryId;
        /// <summary>
        /// ������������� ����������
        /// </summary>
        public int LibraryId
        {
            get { return _libraryId; }
            set
            {
                if (_libraryId == value) return;
                OnPropertyChanging(GlobalPropertyNames.LibraryId);
                _libraryId = value;
                OnPropertyChanged(GlobalPropertyNames.LibraryId);
            }
        }

        private string _typeName;
        /// <summary>
        /// ��� ���� ��� ������
        /// </summary>
        public string TypeName
        {
            get { return _typeName; }
            set
            {
                if (_typeName == value) return;
                OnPropertyChanging(GlobalPropertyNames.TypeName);
                _typeName = value;
                OnPropertyChanged(GlobalPropertyNames.TypeName);
            }
        }

        private string _kindCode;
        /// <summary>
        /// ��������� ����������� ����
        /// </summary>
        public string KindCode
        {
            get { return _kindCode; }
            set
            {
                if (_kindCode == value) return;
                OnPropertyChanging(GlobalPropertyNames.KindCode);
                _kindCode = value;
                OnPropertyChanged(GlobalPropertyNames.KindCode);
            }
        }

        private bool _isGeneric;
        /// <summary>
        /// ���������
        /// </summary>
        public bool IsGeneric
        {
            get { return _isGeneric; }
            set
            {
                if (_isGeneric == value) return;
                OnPropertyChanging(GlobalPropertyNames.IsGeneric);
                _isGeneric = value;
                OnPropertyChanged(GlobalPropertyNames.IsGeneric);
            }
        }
        private string _fullTypeName;
        /// <summary>
        /// ������ ��� ����
        /// </summary>
        public string FullTypeName
        {
            get { return _fullTypeName; }
            set
            {
                if (_fullTypeName == value) return;
                OnPropertyChanging(GlobalPropertyNames.FullTypeName);
                _fullTypeName = value;
                OnPropertyChanged(GlobalPropertyNames.FullTypeName);
            }
        }
	
        #endregion

        /// <summary>
        /// �������� ������������ ������� ��������� �����������
        /// </summary>
        public override void Validate()
        {
            base.Validate(); 
            if (_libraryId == 0)
                throw new ValidateException("�� ������ ������������� ����������");
            if (string.IsNullOrEmpty(_typeName))
                throw new ValidateException("�� ������� ��� ����");
            if (string.IsNullOrEmpty(_kindCode))
                throw new ValidateException("�� ������� ���");
            if (string.IsNullOrEmpty(_fullTypeName))
                throw new ValidateException("�� ������� ������ ��� ����");
        }
        ///// <summary>
        ///// ���������
        ///// </summary>
        //public void Save()
        //{
        //    Validate();
        //    if (IsNew)
        //        Create(Workarea.Empty<Library>().Entity.FindMethod("ContentInsert").FullName);
        //    else
        //        Update(Workarea.Empty<Library>().Entity.FindMethod("ContentUpdate").FullName, true);
        //}
        ///// <summary>
        ///// �������
        ///// </summary>
        //public void Delete()
        //{
        //    Workarea.DeleteById(Id, Workarea.Empty<Library>().Entity.FindMethod("LibraryContentDelete").FullName);
        //}

        #region ���� ������
        ///// <summary>
        ///// ��������� ������ �� ���� ������ �� ��������������
        ///// </summary>
        ///// <param name="value">�������������</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.Empty<Library>().Entity.FindMethod("LibraryContentLoad").FullName);
        //}
        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="reader">������ <see cref="SqlDataReader"/> ������ ������</param>
        /// <param name="endInit">��������� �������������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _typeName = reader.GetString(9);
                _libraryId = reader.GetInt32(10);
                _kindCode = reader.GetString(11);
                _isGeneric = reader.GetInt32(12) != 0;
                _fullTypeName = reader.GetString(13);
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
        /// <param name="insertCommand">�������� �� �������� ��������� ��������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.TypeName, SqlDbType.NVarChar, 255) { IsNullable = false, Value = _typeName };
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.LibraryId, SqlDbType.Int) { IsNullable = false, Value = _libraryId };
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.KindCode, SqlDbType.NVarChar, 255) { IsNullable = false, Value = _kindCode };
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.IsGeneric, SqlDbType.Int)
                      {
                          IsNullable = false,
                          Value = _isGeneric ? 1 : 0
                      };
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.FullTypeName, SqlDbType.NVarChar, 255) { IsNullable = false, Value = _fullTypeName };
            sqlCmd.Parameters.Add(prm);
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

            if (_libraryId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.LibraryId, XmlConvert.ToString(_libraryId));
            if (!string.IsNullOrEmpty(_typeName))
                writer.WriteAttributeString(GlobalPropertyNames.TypeName, _typeName);
            if (!string.IsNullOrEmpty(_kindCode))
                writer.WriteAttributeString(GlobalPropertyNames.KindCode, _kindCode);
            if (_isGeneric)
                writer.WriteAttributeString(GlobalPropertyNames.IsGeneric, XmlConvert.ToString(_isGeneric));
            if (!string.IsNullOrEmpty(_fullTypeName))
                writer.WriteAttributeString(GlobalPropertyNames.FullTypeName, _fullTypeName);
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.LibraryId) != null)
                _libraryId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.LibraryId));
            if (reader.GetAttribute(GlobalPropertyNames.TypeName) != null)
                _typeName = reader[GlobalPropertyNames.TypeName];
            if (reader.GetAttribute(GlobalPropertyNames.KindCode) != null)
                _kindCode = reader[GlobalPropertyNames.KindCode];
            if (reader.GetAttribute(GlobalPropertyNames.IsGeneric) != null)
                _isGeneric = XmlConvert.ToBoolean(reader[GlobalPropertyNames.IsGeneric]);
            if (reader.GetAttribute(GlobalPropertyNames.FullTypeName) != null)
                _fullTypeName = reader[GlobalPropertyNames.FullTypeName];
        }
        #endregion

        public override string ToString()
        {
            return FullTypeName;
        }
    }
}