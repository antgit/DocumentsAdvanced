using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct BaseKindStruct
    {
        /// <summary>Признак</summary>
        public string Code;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>Наименование</summary>
        public string Name;
        /// <summary>Числовое представление элемента</summary>
        public Int16 SubKind;
    }
    /// <summary>
    /// Подтипы
    /// </summary>
    public abstract class BaseKind : BaseCoreObject
    {
        private string _code;
        private string _memo;
        private string _name;
        //private Int16 _entityId;
        private Int16 _subKind;

        /// <summary>Конструктор</summary>
        protected BaseKind(): base()
        {
        }

        #region Свойства
        /// <summary>Признак</summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (value == _code) return;
                OnPropertyChanging(GlobalPropertyNames.Code);
                _code = value;
                OnPropertyChanged(GlobalPropertyNames.Code);
            }
        }

        /// <summary>Примечание</summary>
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

        /// <summary>Наименование</summary>
        public string Name
        {
            get { return _name; }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.Name);
                _name = value;
                OnPropertyChanged(GlobalPropertyNames.Name);
            }
        }

        ///// <summary>Идентификатор типа</summary>
        //public new Int16 EntityId
        //{
        //    get { return _entityId; }
        //    set
        //    {
        //        _entityId = value;
        //    }
        //}

        /// <summary>Числовое представление элемента</summary>
        public Int16 SubKind
        {
            get { return _subKind; }
            set { _subKind = value; }
        }
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (!string.IsNullOrEmpty(_code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, _code);
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
            if (_subKind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SubKind, XmlConvert.ToString(_subKind));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                _code = reader.GetAttribute(GlobalPropertyNames.Code);
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
            if (reader.GetAttribute(GlobalPropertyNames.SubKind) != null)
                _subKind = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.SubKind));
        }
        #endregion

        protected override void OnReadXml(XmlReader reader, string elementName)
        {
            if (elementName == GlobalPropertyNames.Code)
            {
                _code = reader.ReadString();
            }
            else if (elementName == GlobalPropertyNames.SubKind)
            {
                _subKind = Convert.ToInt16(reader.ReadString());
            }
            else if (elementName == GlobalPropertyNames.Memo)
            {
                _memo = reader.ReadString();
            }
            else if (elementName == GlobalPropertyNames.Name)
            {
                _name = reader.ReadString();
            }
            base.OnReadXml(reader, elementName);
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            if (!String.IsNullOrEmpty(Code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, Code);
            writer.WriteAttributeString(GlobalPropertyNames.SubKind, SubKind.ToString());
            if (!String.IsNullOrEmpty(Memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, Memo);
            if (!String.IsNullOrEmpty(Name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, Name);
        }

        /// <summary>Получение вида из значения идентификатора</summary>
        /// <param name="value">Значение идентификатора</param>
        /// <returns></returns>
        public static Int16 ExtractSubKind(int value)
        {
            //byte[] aBytes = BitConverter.GetBytes(value);
            //byte[] dValue = new byte[] { aBytes[0], aBytes[1] };
            //return BitConverter.ToInt16(dValue, 0);
            return (Int16)(value & 0xffff);
        }

        /// <summary>Генератор идентификатора</summary>
        /// <returns></returns>
        private int CreateId()
        {
            //return CreateId(_entityId, SubKind);
            return CreateId(EntityId, SubKind);
        }

        /// <summary>Генератор идентификатора</summary>
        /// <param name="dbEntityKindValue">Значение вида</param>
        /// <param name="subKindValue">Значение типа</param>
        /// <returns></returns>
        public static int CreateId(Int16 dbEntityKindValue, Int16 subKindValue)
        {
            byte[] aBytes = BitConverter.GetBytes(dbEntityKindValue);
            byte[] bBytes = BitConverter.GetBytes(subKindValue);
            byte[] dValue = new[] { bBytes[0], bBytes[1], aBytes[0], aBytes[1] };
            return BitConverter.ToInt32(dValue, 0);
        }

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if (String.IsNullOrEmpty(_name))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_NAMEISEMPTY, 1049));
            if (_subKind == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_SUBKINDISEMPTY", 1049));
            if (EntityId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_DOCTYPEIDISEMPTY", 1049));
            Id = CreateId();
        }

        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _code = reader.IsDBNull(9) ? String.Empty : reader.GetString(9);
                _memo = reader.IsDBNull(10) ? String.Empty : reader.GetString(10);
                _name = reader.GetString(11);
                EntityId = reader.GetInt16(12);
                _subKind = reader.GetInt16(13);
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
            if (insertCommand)
                sqlCmd.Parameters[GlobalSqlParamNames.Id].Value = CreateId();
            else
            {
                SqlParameter prmNewId = new SqlParameter(GlobalSqlParamNames.NewId, SqlDbType.Int) { Value = CreateId() };
                sqlCmd.Parameters.Add(prmNewId);
            }
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100) { IsNullable = true };
            if (String.IsNullOrEmpty(_code))
                prm.Value = DBNull.Value;
            else
                prm.Value = _code;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar) {IsNullable = true};
            if (String.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _memo.Length;
                prm.Value = _memo;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt) {IsNullable = false, Value = EntityId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.KindId, SqlDbType.SmallInt) {IsNullable = false, Value = _subKind};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255)
                      {
                          IsNullable = false,
                          Value = _name
                      };
            sqlCmd.Parameters.Add(prm);
        }
        /// <summary>Представление объекта в виде строки</summary>
        /// <rereturns>Соответствует наименованию объекта <see cref="EntityKind.Name"/></rereturns>
        public override string ToString()
        {
            return _name;
        }

        /// <summary>Получение типа из начения идентификатора</summary>
        /// <param name="value">Значение идентификатора</param>
        /// <returns></returns>
        public static Int16 ExtractEntityKind(int value)
        {
            //byte[] aBytes = BitConverter.GetBytes(value);
            //byte[] dValue = new byte[] { aBytes[2], aBytes[3]};
            //return BitConverter.ToInt16(dValue, 0);
            return (Int16)((value / 0xffff) & 0x0000ffff);
        }
        #region Состояние
        BaseKindStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseKindStruct
                {
                    Code = _code,
                    Memo = _memo,
                    Name = _name,
                    SubKind = _subKind
                };
                return true;
            }
            return false;
        }
        /// <summary>
        /// Востановить исходное состояние
        /// </summary>
        public override void RestoreState()
        {
            base.RestoreState();
            Code = _baseStruct.Code;
            Memo = _baseStruct.Memo;
            Name = _baseStruct.Name;
            SubKind = _baseStruct.SubKind;

            IsChanged = false;
        }
        #endregion
    }
}