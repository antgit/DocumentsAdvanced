using System;
using System.Data.SqlClient;

namespace BusinessObjects
{
    /// <summary>�������� ������ ���� ������</summary>
    public sealed class ErrorLog
    {
        /// <summary>
        /// �����������
        /// </summary>
        internal ErrorLog()
        {

        }

        /// <summary>�������������</summary>
        public int Id { get; private set; }

        private Guid _guid;
        /// <summary>���������� �������������</summary>
        public Guid Guid
        {
            get { return _guid; }
        }
        private DateTime _errorTime;
        /// <summary>���� ������</summary>
        public DateTime ErrorTime
        {
            get { return _errorTime; }
        }
        private string _userName;
        /// <summary>������������</summary>
        public string UserName
        {
            get { return _userName; }
        }
        private int _number;
        /// <summary>����� ������</summary>
        public int Number
        {
            get { return _number; }
        }
        private int _severity;
        /// <summary>�������</summary>
        public int Severity
        {
            get { return _severity; }
        }
        private int _state;
        /// <summary>���������</summary>
        public int State
        {
            get { return _state; }
        }
        private string _procedure;
        /// <summary>���������</summary>
        public string Procedure
        {
            get { return _procedure; }
        }
        private int _line;
        /// <summary>������</summary>
        public int Line
        {
            get { return _line; }
        }
        private string _message;
        /// <summary>���������</summary>
        public string Message
        {
            get { return _message; }
        }
        /// <summary>��������</summary>
        /// <param name="reader">������ ������ ������</param>
        internal void Load(SqlDataReader reader)
        {
            try
            {
                Id = reader.GetInt32(0);
                _guid = reader.GetGuid(1);
                _errorTime = reader.GetDateTime(2);
                _userName = reader.GetString(3);
                _number = reader.GetInt32(4);
                _severity = reader.GetInt32(5);
                _state = reader.GetInt32(6);
                _procedure = reader.GetString(7);
                _line = reader.GetInt32(8);
                _message = reader.GetString(9);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
        }
	
    }
}