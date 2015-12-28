using System;
using System.Data.SqlClient;

namespace BusinessObjects
{
    /// <summary>Протокол ошибок базы данных</summary>
    public sealed class ErrorLog
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        internal ErrorLog()
        {

        }

        /// <summary>Идентификатор</summary>
        public int Id { get; private set; }

        private Guid _guid;
        /// <summary>Глобальный идентификатор</summary>
        public Guid Guid
        {
            get { return _guid; }
        }
        private DateTime _errorTime;
        /// <summary>Дата ошибки</summary>
        public DateTime ErrorTime
        {
            get { return _errorTime; }
        }
        private string _userName;
        /// <summary>Пользователь</summary>
        public string UserName
        {
            get { return _userName; }
        }
        private int _number;
        /// <summary>Номер ошибки</summary>
        public int Number
        {
            get { return _number; }
        }
        private int _severity;
        /// <summary>Уровень</summary>
        public int Severity
        {
            get { return _severity; }
        }
        private int _state;
        /// <summary>Состояние</summary>
        public int State
        {
            get { return _state; }
        }
        private string _procedure;
        /// <summary>Процедура</summary>
        public string Procedure
        {
            get { return _procedure; }
        }
        private int _line;
        /// <summary>Строка</summary>
        public int Line
        {
            get { return _line; }
        }
        private string _message;
        /// <summary>Сообщение</summary>
        public string Message
        {
            get { return _message; }
        }
        /// <summary>Загрузка</summary>
        /// <param name="reader">Объект чтения данных</param>
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
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
        }
	
    }
}