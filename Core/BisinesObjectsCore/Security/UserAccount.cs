using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects.Security
{
    /// <summary>Аккаунт пользователя</summary>
    public sealed class UserAccount : BaseCore<UserAccount>, ICodes<UserAccount>
    {

        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Почтовый аккаунт, соответствует значению 1</summary>
        public const int KINDVALUE_MAIL = 1;
        /// <summary>Live ID, соответствует значению 2</summary>
        public const int KINDVALUE_LIVEID = 2;
        /// <summary>Open ID, соответствует значению 3</summary>
        public const int KINDVALUE_OPENID = 3;
        /// <summary>Skype, соответствует значению 4</summary>
        public const int KINDVALUE_SKYPE = 4;
        /// <summary>OOVO, соответствует значению 5</summary>
        public const int KINDVALUE_OOVO = 5;
        /// <summary>Google, соответствует значению 6</summary>
        public const int KINDVALUE_GOOGLE = 6;
        /// <summary>Социальные сети, соответствует значению 7</summary>
        public const int KINDVALUE_SOCIALNET = 7;
        /// <summary>Домен, соответствует значению 8</summary>
        public const int KINDVALUE_DOMAIN = 8;

        /// <summary>Почтовый аккаунт, соответствует значению 1</summary>
        public const int KINDID_MAIL = 6356993;
        /// <summary>Live ID, соответствует значению 2</summary>
        public const int KINDID_LIVEID = 6356994;
        /// <summary>Open ID, соответствует значению 3</summary>
        public const int KINDID_OPENID = 6356995;
        /// <summary>Skype, соответствует значению 4</summary>
        public const int KINDID_SKYPE = 6356996;
        /// <summary>OOVO, соответствует значению 5</summary>
        public const int KINDID_OOVO = 6356997;
        /// <summary>Google, соответствует значению 6</summary>
        public const int KINDID_GOOGLE = 6356998;
        /// <summary>Социальные сети, соответствует значению 7</summary>
        public const int KINDID_SOCIALNET = 6356999;
        /// <summary>Домен, соответствует значению 8</summary>
        public const int KINDID_DOMAIN = 6357000;
        // ReSharper restore InconsistentNaming
        #endregion

        /// <summary>Конструктор</summary>
        public UserAccount()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.UserAccount;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override UserAccount Clone(bool endInit)
        {
            UserAccount obj = base.Clone(endInit);
            obj.UserId = UserId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства


        private int _userId;
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId
        {
            get { return _userId; }
            set
            {
                if (value == _userId) return;
                OnPropertyChanging(GlobalPropertyNames.UserId);
                _userId = value;
                OnPropertyChanged(GlobalPropertyNames.UserId);
            }
        }
        

        
        

        private Uid _uid;
        /// <summary>
        /// Пользователь 
        /// </summary>
        public Uid Uid
        {
            get
            {
                if (_userId == 0)
                    return null;
                if (_uid == null)
                    _uid = Workarea.Cashe.GetCasheData<Uid>().Item(_userId);
                else if (_uid.Id != _userId)
                    _uid = Workarea.Cashe.GetCasheData<Uid>().Item(_userId);
                return _uid;
            }
            set
            {
                if (_uid == value) return;
                OnPropertyChanging(GlobalPropertyNames.Uid);
                _uid = value;
                _userId = _uid == null ? 0 : _uid.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent);
            }
        }



        private string _email;
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                if (value == _email) return;
                OnPropertyChanging(GlobalPropertyNames.Email);
                _email = value;
                OnPropertyChanged(GlobalPropertyNames.Email);
            }
        }



        private string _password;
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                OnPropertyChanging(GlobalPropertyNames.Password);
                _password = value;
                OnPropertyChanged(GlobalPropertyNames.Password);
            }
        }
        

        private bool _isActivated;
        /// <summary>
        /// Активен
        /// </summary>
        public bool IsActivated
        {
            get { return IsStateAllow; }
            set
            {
                if (value)
                    StateId = State.STATEACTIVE;
                else
                    StateId = State.STATEDENY;
            }
        }


        private bool _isLockedOut;
        /// <summary>
        /// Заблокирован
        /// </summary>
        public bool IsLockedOut
        {
            get { return IsStateDeny; }
            set
            {
                if (!value)
                    StateId = State.STATEACTIVE;
                else
                    StateId = State.STATEDENY;
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
            if (_userId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserId, XmlConvert.ToString(_userId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.UserId) != null)
                _userId = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.UserId));
        }
        #endregion
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();

            if (_userId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_USERACCOUNTUSERID"));
        }
        /// <summary>Загрузить данные из базы данных</summary>
        /// <param name="reader">Объект чтения данных<see cref="System.Data.SqlClient.SqlDataReader"/></param>
        /// <param name="endInit">Выполнять действия окончания инициализации</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _userId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _password = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                _email = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);

            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, true, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.UserId, SqlDbType.Int) { IsNullable = false};
            prm.Value = _userId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Password, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_password))
                prm.Value = DBNull.Value;
            else
                prm.Value = _password;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Email, SqlDbType.NVarChar, 255) { IsNullable = true };
            if (string.IsNullOrEmpty(_email))
                prm.Value = DBNull.Value;
            else
                prm.Value = _email;
            sqlCmd.Parameters.Add(prm);
        }

        #region ICodes
        public List<CodeValue<UserAccount>> GetValues(bool allKinds)
        {
            return CodeHelper<UserAccount>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<UserAccount>.GetView(this, true);
        }
        #endregion
    }
}