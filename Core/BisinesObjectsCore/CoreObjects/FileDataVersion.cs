using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Версия файловых данных
    /// </summary>
    public sealed class FileDataVersion: BaseCoreObject
    {
        /// <summary>Конструктор</summary>
        public FileDataVersion()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.FileDataVersion;
        }

        #region Свойства
        private int _ownId;
        /// <summary>
        /// Идентификатор владельца
        /// </summary>
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


        private int _versionCode;
        /// <summary>Код версии</summary>
        public int VersionCode
        {
            get { return _versionCode; }
            set
            {
                if (value == _versionCode) return;
                OnPropertyChanging(GlobalPropertyNames.VersionCode);
                _versionCode = value;
                OnPropertyChanged(GlobalPropertyNames.VersionCode);
            }
        }
        

        private byte[] _streamData;

        /// <summary>
        /// Данные
        /// </summary>
        public byte[] StreamData
        {
            get
            {
                if (!_refreshDode)
                    RefreshSteamData();
                return _streamData;
            }
            set
            {
                //if (value == _streamData) return;
                OnPropertyChanging(GlobalPropertyNames.StreamData);
                _streamData = value;
                _refreshDode = true;
                OnPropertyChanged(GlobalPropertyNames.StreamData);
            }
        }
        bool IsStreamDataNull
        {
            get
            {
                return (_streamData == null || _streamData.All(v => v == 0));
            }
        }
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));
            if (_streamData != null)
                writer.WriteAttributeString(GlobalPropertyNames.StreamData, Convert.ToBase64String(_streamData));
            if (_versionCode != 0)
                writer.WriteAttributeString(GlobalPropertyNames.VersionCode, XmlConvert.ToString(_versionCode));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader[GlobalPropertyNames.OwnId]);
            if (reader.GetAttribute(GlobalPropertyNames.VersionCode) != null)
                _versionCode = XmlConvert.ToInt32(reader[GlobalPropertyNames.VersionCode]);
            if (reader.GetAttribute(GlobalPropertyNames.StreamData) != null)
                _streamData = Convert.FromBase64String(reader[GlobalPropertyNames.StreamData]);
        }
        #endregion

        private bool _refreshDode;
        public void RefreshSteamData()
        {
            _streamData = new byte[] { };
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = Id;
                        cmd.CommandText = "FileData.FileVersionLoadStreamData";
                        // TODO: использовать глобальный метод
                        //FindMethod("Core.ErrorLogLoad").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            _streamData = !reader.IsDBNull(0) ? reader.GetSqlBinary(0).Value : null;
                        }
                        reader.Close();
                    }
                    _refreshDode = true;
                }
                finally
                {
                    cnn.Close();

                }
            }
        }
        /// <summary>Экспорт данных в файл</summary>
        /// <param name="filename">Полный путь к файлу</param>
        public void ExportToFile(string filename)
        {
            if (IsStreamDataNull && !_refreshDode)
                RefreshSteamData();
            if (!IsStreamDataNull)
                System.IO.File.WriteAllBytes(filename, _streamData);
        }
        /// <summary>Загрузить экземпляр из базы данных</summary>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.GetInt32(9);
                //_streamData = !reader.IsDBNull(16) ? reader.GetSqlBinary(16).Value : null;
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
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.StreamData, SqlDbType.VarBinary) { IsNullable = true };
            if (_streamData == null || _streamData.All(v => v == 0))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _streamData.Length;
                prm.Value = _streamData;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OwnId, SqlDbType.Int) { IsNullable = false, Value = _ownId};
            sqlCmd.Parameters.Add(prm);
        }
        
    }
}