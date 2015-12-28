using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// ���������� ��������� XML ������ ���������
    /// </summary>
    internal struct LinkedXmlDataStruct
    {
        /// <summary>������������� ���������</summary>
        public int OwnId;
        /// <summary>XML</summary>
        public string Xml;
        /// <summary>����� ������</summary>
        public int GroupNo;
    }
    /// <summary>Xml ������ ��������� � ��������</summary>
    /// <remarks>������ � ���� ����� ����� ������������ �� ��������� OwnId � GroupNo</remarks>
    public class LinkedXmlData<T> : BaseCoreObject where T : class, ICoreObject, new()
    {
        /// <summary>
        /// �����������
        /// </summary>
        protected LinkedXmlData()
            : base()
        {
            //EntityId = (short)WhellKnownDbEntity.DocumentXml;
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="owner">������ ��������</param>
        public LinkedXmlData(T owner): this()
        {
            Owner = owner;
            Workarea = owner.Workarea;
        }

        #region ��������

        private int _ownId;
        /// <summary>������������� ���������</summary>
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



        private T _own;
        /// <summary>
        /// ��������
        /// </summary>
        public T Owner
        {
            get
            {
                if (_ownId == 0)
                    return null;
                if (_own == null)
                    _own = Workarea.Cashe.GetCasheData<T>().Item(_ownId);
                else if (_own.Id != _ownId)
                    _own = Workarea.Cashe.GetCasheData<T>().Item(_ownId);
                return _own;
            }
            set
            {
                _own = value;
                _ownId = _own == null ? 0 : _own.Id;
            }
            
        }
        
        
        private string _xml;
        /// <summary>XML</summary>
        public string Xml
        {
            get { return _xml; }
            set
            {
                if (value == _xml) return;
                OnPropertyChanging(GlobalPropertyNames.Xml);
                _xml = value;
                OnPropertyChanged(GlobalPropertyNames.Xml);
            }
        }


        private int _groupNo;
        /// <summary>
        /// ����� ������
        /// </summary>
        public int GroupNo
        {
            get { return _groupNo; }
            set
            {
                if (value == _groupNo) return;
                OnPropertyChanging(GlobalPropertyNames.GroupNo);
                _groupNo = value;
                OnPropertyChanged(GlobalPropertyNames.GroupNo);
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

            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));
            if (!string.IsNullOrEmpty(_xml))
                writer.WriteAttributeString(GlobalPropertyNames.Xml, _xml);
            if (_groupNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.GroupNo, XmlConvert.ToString(_groupNo));
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnId));
            if (reader.GetAttribute(GlobalPropertyNames.Xml) != null)
                _xml = reader.GetAttribute(GlobalPropertyNames.Xml);
            if (reader.GetAttribute(GlobalPropertyNames.GroupNo) != null)
                _groupNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.GroupNo));

        }
        #endregion

        #region ������
        /// <summary>�������� ������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">������� ��������� ��������</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.GetInt32(9);
                _xml = reader.GetString(10);
                _groupNo = reader.GetInt32(11);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        

        /// <summary>���������� �������� ���������� ��� �������� �������� ��� ����������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ����������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.OwnId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _ownId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Xml, SqlDbType.Xml);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _xml;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.GroupNo, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _groupNo;

        }

        
        /// <summary>
        /// ��������� XML ������
        /// </summary>
        /// <param name="owner">��������</param>
        /// <returns></returns>
        /// <exception cref="SqlReturnException"></exception>
        /// <exception cref="DatabaseException"></exception>
        public static List<LinkedXmlData<T>> GetCollection(T owner)
        {
            List<LinkedXmlData<T>> collection = new List<LinkedXmlData<T>>();
            using (SqlConnection cnn = owner.Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = owner.FindProcedure(GlobalMethodAlias.LoadXmlByOwnerId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = owner.Id;
                    
                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                LinkedXmlData<T> item = new LinkedXmlData<T>(owner);
                                item.Load(reader);
                                collection.Add(item);
                            }

                        }
                        reader.Close();
                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(owner.Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(owner.Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

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

        #region ���������
        private LinkedXmlDataStruct _baseDataStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseDataStruct = new LinkedXmlDataStruct
                                      {
                                          OwnId = _ownId,
                                          Xml = _xml,
                                          GroupNo = _groupNo
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
            OwnId = _baseDataStruct.OwnId;
            Xml = _baseDataStruct.Xml;
            GroupNo = _baseDataStruct.GroupNo;
            IsChanged = false;
        }
        #endregion
    }
}