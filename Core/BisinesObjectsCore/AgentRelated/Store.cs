using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Расширение данных о коррекспонденте касающихся склада
    /// </summary>
    public class Store : BaseCoreObject, IRelationSingle
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Store(): base()
        {
            EntityId = (short) WhellKnownDbEntity.Store;
        }

        #region Свойства
        internal Agent _owner;
        /// <summary>
        /// Объект владелец (корреспондент)
        /// </summary>
        public Agent Owner
        {
            get { return _owner; }
        }
        private int _storekeeperId;
        /// <summary>
        /// Идентификатор заведующего складом
        /// </summary>
        public int StorekeeperId
        {
            get { return _storekeeperId; }
            set
            {
                if (_storekeeperId == value) return;
                OnPropertyChanging(GlobalPropertyNames.StorekeeperId);
                _storekeeperId = value;
                OnPropertyChanged(GlobalPropertyNames.StorekeeperId);
            }
        }
        private Employer _storekeeper;
        /// <summary>
        /// Заведующий складом
        /// </summary>
        public Employer Storekeeper
        {
            get
            {
                if (_storekeeperId != 0 && _storekeeper==null)
                {
                    Agent ag = Workarea.Cashe.GetCasheData<Agent>().Item(_storekeeperId);
                    if(ag!=null)
                    {
                        if(ag.IsPeople && ag.People.Employer!=null)
                        {
                            _storekeeper = ag.People.Employer;
                        }
                    }
                }
                else if (_storekeeperId != 0 && _storekeeper != null && _storekeeper.Id != _storekeeperId)
                {
                    Agent ag = Workarea.Cashe.GetCasheData<Agent>().Item(_storekeeperId);
                    if (ag != null)
                    {
                        if (ag.IsPeople && ag.People.Employer != null)
                        {
                            _storekeeper = ag.People.Employer;
                        }
                    }
                }
                return _storekeeper;
            }
            set
            {
                if (_storekeeper == value) return;
                OnPropertyChanging(GlobalPropertyNames.Storekeeper);
                _storekeeper = value;
                if (_storekeeper==null)
                {
                    _storekeeperId = 0;
                }
                else
                {
                    _storekeeperId = _storekeeper.Id;
                }
                OnPropertyChanged(GlobalPropertyNames.Storekeeper);
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

            if (_storekeeperId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Storekeeper, XmlConvert.ToString(_storekeeperId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Storekeeper) != null)
                _storekeeperId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Storekeeper));
        }
        #endregion

        ///// <summary>Загрузить</summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, "Contractor.StoresLoad");
        //}
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _storekeeperId = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (endInit)
                OnEndInit();
        }

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            sqlCmd.Parameters[GlobalSqlParamNames.Id].Value = Id;

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.StorekeeperId, SqlDbType.Int);
            if(_storekeeperId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _storekeeperId;
            
            sqlCmd.Parameters.Add(prm);
        }

        #region IRelationSingle Members

        string IRelationSingle.Schema
        {
            get { return GlobalSchemaNames.Contractor; }
        }

        #endregion
    }
}
