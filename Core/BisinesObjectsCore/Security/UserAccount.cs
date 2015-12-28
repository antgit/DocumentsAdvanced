using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects.Security
{
    /// <summary>������� ������������</summary>
    public sealed class UserAccount : BaseCore<UserAccount>, ICodes<UserAccount>
    {

        #region ��������� �������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>�������� �������, ������������� �������� 1</summary>
        public const int KINDVALUE_MAIL = 1;
        /// <summary>Live ID, ������������� �������� 2</summary>
        public const int KINDVALUE_LIVEID = 2;
        /// <summary>Open ID, ������������� �������� 3</summary>
        public const int KINDVALUE_OPENID = 3;
        /// <summary>Skype, ������������� �������� 4</summary>
        public const int KINDVALUE_SKYPE = 4;
        /// <summary>OOVO, ������������� �������� 5</summary>
        public const int KINDVALUE_OOVO = 5;
        /// <summary>Google, ������������� �������� 6</summary>
        public const int KINDVALUE_GOOGLE = 6;
        /// <summary>���������� ����, ������������� �������� 7</summary>
        public const int KINDVALUE_SOCIALNET = 7;
        /// <summary>�����, ������������� �������� 8</summary>
        public const int KINDVALUE_DOMAIN = 8;

        /// <summary>�������� �������, ������������� �������� 1</summary>
        public const int KINDID_MAIL = 6356993;
        /// <summary>Live ID, ������������� �������� 2</summary>
        public const int KINDID_LIVEID = 6356994;
        /// <summary>Open ID, ������������� �������� 3</summary>
        public const int KINDID_OPENID = 6356995;
        /// <summary>Skype, ������������� �������� 4</summary>
        public const int KINDID_SKYPE = 6356996;
        /// <summary>OOVO, ������������� �������� 5</summary>
        public const int KINDID_OOVO = 6356997;
        /// <summary>Google, ������������� �������� 6</summary>
        public const int KINDID_GOOGLE = 6356998;
        /// <summary>���������� ����, ������������� �������� 7</summary>
        public const int KINDID_SOCIALNET = 6356999;
        /// <summary>�����, ������������� �������� 8</summary>
        public const int KINDID_DOMAIN = 6357000;
        // ReSharper restore InconsistentNaming
        #endregion

        /// <summary>�����������</summary>
        public UserAccount()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.UserAccount;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override UserAccount Clone(bool endInit)
        {
            UserAccount obj = base.Clone(endInit);
            obj.UserId = UserId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region ��������


        private int _userId;
        /// <summary>
        /// ������������� ������������
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
        /// ������������ 
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
        /// ����������� �����
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
        /// ������
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
        /// �������
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
        /// ������������
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

        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);
            if (_userId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserId, XmlConvert.ToString(_userId));
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.UserId) != null)
                _userId = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.UserId));
        }
        #endregion
        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();

            if (_userId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_USERACCOUNTUSERID"));
        }
        /// <summary>��������� ������ �� ���� ������</summary>
        /// <param name="reader">������ ������ ������<see cref="System.Data.SqlClient.SqlDataReader"/></param>
        /// <param name="endInit">��������� �������� ��������� �������������</param>
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