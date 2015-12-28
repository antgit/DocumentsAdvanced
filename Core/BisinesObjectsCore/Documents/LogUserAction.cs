using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// Протокол действий в документе
    /// </summary>
    /// <remarks>Под действиями пользователя подразумеваются действия открытия документа, сохранения, печати, изменения товарных позиций и т.д.</remarks>
    public sealed class LogUserAction : BaseCoreObject
    {
        private int _ownId;
        private string _memo;
        private string _name;
        /// <summary>
        /// Конструктор
        /// </summary>
        public LogUserAction()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.LogUserAction;
        }

        #region Свойства
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public int OwnId
        {
            get { return _ownId; }
            set { _ownId = value; }
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
        #endregion

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, _name);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnId));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                _name = reader.GetAttribute(GlobalPropertyNames.Name);
        }
        #endregion

        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if (String.IsNullOrEmpty(_name))
                throw new ValidateException(Workarea.Cashe.ResourceString(ResourceString.MSG_VAL_NAMEISEMPTY, 1049));
        }
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.GetInt32(9);
                _name = reader.GetString(10);
                _memo = reader.IsDBNull(11) ? String.Empty : reader.GetString(11);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }

        /// <summary>
        /// Создать запись в протоколе документа о сохранении
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="id">Идентификатор документа</param>
        /// <param name="memo">Описание</param>
        public static void CreateActionSave(Workarea wa, int id, string memo)
        {
            CreateAction(wa, id, "SAVE", memo);
        }
        /// <summary>
        /// Создать запись в протоколе документа о предварительном просмотре
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="id">Идентификатор документа</param>
        /// <param name="memo">Описание</param>
        public static void CreateActionPreview(Workarea wa, int id, string memo)
        {
            CreateAction(wa, id, "PREVIEW", memo);
        }
        public static void CreateActionPageView(Workarea wa, int id, string memo)
        {
            CreateAction(wa, id, "PAGEVIEW", memo);
        }
        public static void CreateActionStateChanged(Workarea wa, int id, string memo)
        {
            CreateAction(wa, id, "STATECHANGED", memo);
        }
        public static void CreateActionOpenDocument(Workarea wa, int id, string memo)
        {
            CreateAction(wa, id, "OPENDOCUMENT", memo);
        }
        /// <summary>
        /// Создать запись в протоколе документа о печати
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="id">Идентификатор документа</param>
        /// <param name="memo">Описание</param>
        public static void CreateActionPrint(Workarea wa, int id, string memo)
        {
            CreateAction(wa, id, "PRINT", memo);
        }
        /// <summary>
        /// Создать запись в протоколе документа о действии
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="id">Идентификатор документа</param>
        /// <param name="name">Наименование действия</param>
        /// <param name="memo">Описание</param>
        /// <returns></returns>
        public static void CreateAction(Workarea wa, int id, string name, string memo)
        {
            if (id == 0) return;
            LogUserAction logAction = new LogUserAction
                                          {
                                              Workarea = wa, 
                                              StateId = State.STATEACTIVE, 
                                              Name = name, 
                                              Memo = memo, 
                                              OwnId = id
                                          };
            Action del = logAction.Save;
            del.Invoke();
            //logAction.Save();
        }
        /// <summary>Загрузить список действий выполненвх пользователями в документе</summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="value">Идентификатор документа</param>
        public static List<LogUserAction> GetCollection(Workarea wa, int value)
        {
            List<LogUserAction> collection = new List<LogUserAction>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.FindMethod("Document.LogUserActionLoadByDocId").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            LogUserAction item = new LogUserAction { Workarea = wa };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar) { IsNullable = true };
            if (String.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _memo.Length;
                prm.Value = _memo;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalPropertyNames.OwnId, SqlDbType.Int) { IsNullable = false, Value = _ownId };
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

    }
}