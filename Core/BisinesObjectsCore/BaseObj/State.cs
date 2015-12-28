using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Состояние</summary>
    public sealed class State : BaseCoreObject
    {
        #region Константы
        // ReSharper disable InconsistentNaming
        /// <summary>Неопределен, соответствует значению 0</summary>
        public const int STATEDEFAULT = 0;
        /// <summary>Активен, соответствует значению 1</summary>
        public const int STATEACTIVE = 1;
        /// <summary>Требуется корректировка, соответствует значению 2</summary>
        public const int STATENOTDONE = 2;
        /// <summary>Запрешен к использованию, соответствует значению 4</summary>
        public const int STATEDENY = 4;
        /// <summary>Удален, соответствует значению 5</summary>
        public const int STATEDELETED = 5;
        // ReSharper restore InconsistentNaming 
        #endregion
        /// <summary>Конструктор</summary>
        public State():base()
        {
            EntityId = (short)WhellKnownDbEntity.State;
        }
        #region Свойства

        private string _name;
        /// <summary>Наименование</summary>
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
        /// Примечание
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set
            {
                if (_memo == value) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
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

            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
        }
        #endregion

        /// <summary>Проверка объекта</summary>
        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(_name))
                throw new ValidateException("Не указано наименование.");
        }

        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                Name = reader.GetString(9);
                Memo = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
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
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255) {IsNullable = false, Value = Name};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar) { IsNullable = false};
            if (string.IsNullOrEmpty(Memo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = Memo.Length;
                prm.Value = Memo;
            }
            sqlCmd.Parameters.Add(prm);
        }

        /// <summary>Представление объекта в виде строки</summary>
        /// <rereturns>Соответствует наименованию объекта <see cref="BusinessObjects.State.Name"/></rereturns>
        public override string ToString()
        {
            return _name;
        }
        /// <summary>Представление объекта в виде строки</summary>
        /// <remarks>Допустимая маска :
        /// <list type="bullet">
        /// <item><term>%name%</term>
        /// <description>Наименование <see cref="BusinessObjects.State.Name"/></description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <param name="mask">Маска</param>
        /// <returns></returns>
        public override string ToString(string mask)
        {
            if (string.IsNullOrEmpty(mask))
                return ToString();
            base.ToString(mask);
            string res = base.ToString(mask);
            //Макроподстановка для названия
            res = res.Replace("%name%", _name);

            return res;
        }
    }
}
