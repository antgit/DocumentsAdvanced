using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class RouteEvent: BaseCoreObject
    {
        /// <summary>Конструктор</summary>
        public RouteEvent()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.RouteEvent;
        }

        private int _RouteMemberId;
        /// <summary>
        /// Объект слежения
        /// </summary>
        public int RouteMemberId
        {
            get { return _RouteMemberId; }
            set
            {
                if (value == _RouteMemberId) return;
                OnPropertyChanging(GlobalPropertyNames.RouteMemberId);
                _RouteMemberId = value;
                OnPropertyChanged(GlobalPropertyNames.RouteMemberId);
            }
        }

        private int _DeviceId;
        /// <summary>
        /// Устройство слежения
        /// </summary>
        public int DeviceId
        {
            get { return _DeviceId; }
            set
            {
                if (value == _DeviceId) return;
                OnPropertyChanging(GlobalPropertyNames.DeviceId);
                _DeviceId = value;
                OnPropertyChanged(GlobalPropertyNames.DeviceId);
            }
        }

        private decimal _X;
        /// <summary>
        /// Координата X
        /// </summary>
        public decimal X
        {
            get { return _X; }
            set
            {
                if (value == _X) return;
                OnPropertyChanging(GlobalPropertyNames.X);
                _X = value;
                OnPropertyChanged(GlobalPropertyNames.X);
            }
        }

        private decimal _Y;
        /// <summary>
        /// Координата Y
        /// </summary>
        public decimal Y
        {
            get { return _Y; }
            set
            {
                if (value == _Y) return;
                OnPropertyChanging(GlobalPropertyNames.Y);
                _Y = value;
                OnPropertyChanged(GlobalPropertyNames.Y);
            }
        }

        private decimal _Speed;
        /// <summary>
        /// Скорость движения
        /// </summary>
        public decimal Speed
        {
            get { return _Speed; }
            set
            {
                if (value == _Speed) return;
                OnPropertyChanging(GlobalPropertyNames.Speed);
                _Speed = value;
                OnPropertyChanged(GlobalPropertyNames.Speed);
            }
        }

        private DateTime? _Date;
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime? Date
        {
            get { return _Date; }
            set
            {
                if (value == _Date) return;
                OnPropertyChanging(GlobalPropertyNames.Date);
                _Date = value;
                OnPropertyChanged(GlobalPropertyNames.Date);
            }
        }

        private TimeSpan? _Time;
        /// <summary>
        /// Время
        /// </summary>
        public TimeSpan? Time
        {
            get { return _Time; }
            set
            {
                if (value == _Time) return;
                OnPropertyChanging(GlobalPropertyNames.Time);
                _Time = value;
                OnPropertyChanged(GlobalPropertyNames.Time);
            }
        }

        private decimal _Distance;
        /// <summary>
        /// Расстояние до предыдущей точки
        /// </summary>
        public decimal Distance
        {
            get { return _Distance; }
            set
            {
                if (value == _Distance) return;
                OnPropertyChanging(GlobalPropertyNames.Distance);
                _Distance = value;
                OnPropertyChanged(GlobalPropertyNames.Distance);
            }
        }

        private decimal _Odometr;
        /// <summary>
        /// Расстояние до предыдущей точки
        /// </summary>
        public decimal Odometr
        {
            get { return _Odometr; }
            set
            {
                if (value == _Odometr) return;
                OnPropertyChanging(GlobalPropertyNames.Odometer);
                _Odometr = value;
                OnPropertyChanged(GlobalPropertyNames.Odometer);
            }
        }

        private int _AgentId;
        /// <summary>
        /// Идентификатор корреспондента в зону которого попал объект наблюдения
        /// </summary>
        public int AgentId
        {
            get { return _AgentId; }
            set
            {
                if (value == _AgentId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentId);
                _AgentId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentId);
            }
        }

        private int _AddressId;
        /// <summary>
        /// Идентификатор адреса корреспондента в зону которого попал объект наблюдения
        /// </summary>
        public int AddressId
        {
            get { return _AddressId; }
            set
            {
                if (value == _AddressId) return;
                OnPropertyChanging(GlobalPropertyNames.AddressId);
                _AddressId = value;
                OnPropertyChanged(GlobalPropertyNames.AddressId);
            }
        }

        private int _StatusId;
        /// <summary>
        /// Идентификатор статуса
        /// </summary>
        public int StatusId
        {
            get { return _StatusId; }
            set
            {
                if (value == _StatusId) return;
                OnPropertyChanging(GlobalPropertyNames.StatusId);
                _StatusId = value;
                OnPropertyChanged(GlobalPropertyNames.StatusId);
            }
        }

        private string _StatusCode;
        /// <summary>
        /// Код статуса
        /// </summary>
        public string StatusCode
        {
            get { return _StatusCode; }
            set
            {
                if (value == _StatusCode) return;
                OnPropertyChanging(GlobalPropertyNames.StatusCode);
                _StatusCode = value;
                OnPropertyChanged(GlobalPropertyNames.StatusCode);
            }
        }

        private string _Memo;
        /// <summary>
        /// Примечание
        /// </summary>
        public string Memo
        {
            get { return _Memo; }
            set
            {
                if (value == _Memo) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _Memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }

        private int _MessageId;
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public int MessageId
        {
            get { return _MessageId; }
            set
            {
                if (value == _MessageId) return;
                OnPropertyChanging(GlobalPropertyNames.MessageId);
                _MessageId = value;
                OnPropertyChanged(GlobalPropertyNames.MessageId);
            }
        }

        private int _Altitude;
        /// <summary>
        /// Высота
        /// </summary>
        public int Altitude
        {
            get { return _Altitude; }
            set
            {
                if (value == _Altitude) return;
                OnPropertyChanging(GlobalPropertyNames.Altitude);
                _Altitude = value;
                OnPropertyChanged(GlobalPropertyNames.Altitude);
            }
        }

        private decimal _Direction;
        /// <summary>
        /// Направление движения
        /// </summary>
        public decimal Direction
        {
            get { return _Direction; }
            set
            {
                if (value == _Direction) return;
                OnPropertyChanging(GlobalPropertyNames.Direction);
                _Direction = value;
                OnPropertyChanged(GlobalPropertyNames.Direction);
            }
        }

        private int _HDOP;
        /// <summary>
        /// Точность замеров
        /// </summary>
        public int HDOP
        {
            get { return _HDOP; }
            set
            {
                if (value == _HDOP) return;
                OnPropertyChanging(GlobalPropertyNames.HDOP);
                _HDOP = value;
                OnPropertyChanged(GlobalPropertyNames.HDOP);
            }
        }

        private int _Satellites;
        /// <summary>
        /// Число спутников
        /// </summary>
        public int Satellites
        {
            get { return _Satellites; }
            set
            {
                if (value == _Satellites) return;
                OnPropertyChanging(GlobalPropertyNames.Satellites);
                _Satellites = value;
                OnPropertyChanged(GlobalPropertyNames.Satellites);
            }
        }

        private int _IOStatus;
        /// <summary>
        /// Статус устройства
        /// </summary>
        public int IOStatus
        {
            get { return _IOStatus; }
            set
            {
                if (value == _IOStatus) return;
                OnPropertyChanging(GlobalPropertyNames.IOStatus);
                _IOStatus = value;
                OnPropertyChanged(GlobalPropertyNames.IOStatus);
            }
        }

        private int _Input1;
        /// <summary>
        /// Значение с первого аналогового входа
        /// </summary>
        public int Input1
        {
            get { return _Input1; }
            set
            {
                if (value == _Input1) return;
                OnPropertyChanging(GlobalPropertyNames.Input1);
                _Input1 = value;
                OnPropertyChanged(GlobalPropertyNames.Input1);
            }
        }

        private int _Input2;
        /// <summary>
        /// Значение со второго аналогового входа
        /// </summary>
        public int Input2
        {
            get { return _Input2; }
            set
            {
                if (value == _Input2) return;
                OnPropertyChanging(GlobalPropertyNames.Input2);
                _Input2 = value;
                OnPropertyChanged(GlobalPropertyNames.Input2);
            }
        }

        private DateTime? _RtcDate;
        /// <summary>
        /// Дата RTC
        /// </summary>
        public DateTime? RtcDate
        {
            get { return _RtcDate; }
            set
            {
                if (value == _RtcDate) return;
                OnPropertyChanging(GlobalPropertyNames.RtcDate);
                _RtcDate = value;
                OnPropertyChanged(GlobalPropertyNames.RtcDate);
            }
        }

        private TimeSpan? _RtcTime;
        /// <summary>
        /// Время RTC
        /// </summary>
        public TimeSpan? RtcTime
        {
            get { return _RtcTime; }
            set
            {
                if (value == _RtcTime) return;
                OnPropertyChanging(GlobalPropertyNames.RtcTime);
                _RtcTime = value;
                OnPropertyChanged(GlobalPropertyNames.RtcTime);
            }
        }

        private DateTime? _PosDate;
        /// <summary>
        /// Дата POS
        /// </summary>
        public DateTime? PosDate
        {
            get { return _PosDate; }
            set
            {
                if (value == _PosDate) return;
                OnPropertyChanging(GlobalPropertyNames.PosDate);
                _PosDate = value;
                OnPropertyChanged(GlobalPropertyNames.PosDate);
            }
        }

        private TimeSpan? _PosTime;
        /// <summary>
        /// Время POS
        /// </summary>
        public TimeSpan? PosTime
        {
            get { return _PosTime; }
            set
            {
                if (value == _PosTime) return;
                OnPropertyChanging(GlobalPropertyNames.PosTime);
                _PosTime = value;
                OnPropertyChanged(GlobalPropertyNames.PosTime);
            }
        }

        /// <summary>Загрузить экземпляр из базы данных</summary>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _RouteMemberId = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                _DeviceId = !reader.IsDBNull(10) ? reader.GetInt32(10) : 0;
                _X = !reader.IsDBNull(11) ? reader.GetDecimal(11) : 0;
                _Y = !reader.IsDBNull(12) ? reader.GetDecimal(12) : 0;
                _Speed = !reader.IsDBNull(13) ? reader.GetDecimal(13) : 0;
                _Date = reader.IsDBNull(14) ? (DateTime?)null : reader.GetDateTime(14);
                _Time = reader.IsDBNull(15) ? (TimeSpan?)null : reader.GetTimeSpan(15);
                _Distance = !reader.IsDBNull(16) ? reader.GetDecimal(16) : 0;
                _Odometr = !reader.IsDBNull(17) ? reader.GetDecimal(17) : 0;
                _AgentId = !reader.IsDBNull(18) ? reader.GetInt32(18) : 0;
                _AddressId = !reader.IsDBNull(19) ? reader.GetInt32(19) : 0;
                _StatusId = !reader.IsDBNull(20) ? reader.GetInt32(20) : 0;
                _StatusCode = !reader.IsDBNull(21) ? reader.GetString(21) : "";
                _Memo = !reader.IsDBNull(22) ? reader.GetString(22) : "";
                _MessageId = !reader.IsDBNull(23) ? reader.GetInt32(23) : 0;
                _Altitude = !reader.IsDBNull(24) ? reader.GetInt32(24) : 0;
                _Direction = !reader.IsDBNull(25) ? reader.GetDecimal(25) : 0;
                _HDOP = !reader.IsDBNull(26) ? reader.GetInt32(26) : 0;
                _Satellites = !reader.IsDBNull(27) ? reader.GetInt32(27) : 0;
                _IOStatus = !reader.IsDBNull(28) ? reader.GetInt32(28) : 0;
                _Input1 = !reader.IsDBNull(29) ? reader.GetInt32(29) : 0;
                _Input2 = !reader.IsDBNull(30) ? reader.GetInt32(30) : 0;
                _RtcDate = reader.IsDBNull(31) ? (DateTime?)null : reader.GetDateTime(31);
                _RtcTime = reader.IsDBNull(32) ? (TimeSpan?)null : reader.GetTimeSpan(32);
                _PosDate = reader.IsDBNull(33) ? (DateTime?)null : reader.GetDateTime(33);
                _PosTime = reader.IsDBNull(34) ? (TimeSpan?)null : reader.GetTimeSpan(34);
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

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.RouteMemberId, SqlDbType.Int) { IsNullable = true, Value = _RouteMemberId == 0 ? DBNull.Value : (object)_RouteMemberId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.DeliveryId, SqlDbType.Int) { IsNullable = true, Value = _DeviceId == 0 ? DBNull.Value : (object)_DeviceId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.X, SqlDbType.Decimal) { IsNullable = true, Value = _X };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Y, SqlDbType.Decimal) { IsNullable = true, Value = _Y };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Speed, SqlDbType.Decimal) { IsNullable = true, Value = _Speed };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Date, SqlDbType.Date) { IsNullable = true, Value = _Date };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Time, SqlDbType.Time) { IsNullable = true, Value = _Time };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Distance, SqlDbType.Decimal) { IsNullable = true, Value = _Distance };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Odometer, SqlDbType.Decimal) { IsNullable = true, Value = _Odometr };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.AgentId, SqlDbType.Int) { IsNullable = true, Value = _AgentId == 0 ? DBNull.Value : (object)_AgentId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.AddressId, SqlDbType.Int) { IsNullable = true, Value = _AddressId == 0 ? DBNull.Value : (object)_AddressId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.StatusId, SqlDbType.Int) { IsNullable = true, Value = _StatusId == 0 ? DBNull.Value : (object)_StatusId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.StatusCode, SqlDbType.VarChar, 100) { IsNullable = true, Value = _StatusCode };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.VarChar) { IsNullable = true, Value = _Memo };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.MessageId, SqlDbType.Int) { IsNullable = true, Value = _MessageId == 0 ? DBNull.Value : (object)_MessageId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Altitude, SqlDbType.Int) { IsNullable = true, Value = _Altitude == 0 ? DBNull.Value : (object)_Altitude };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Direction, SqlDbType.Decimal) { IsNullable = true, Value = _Direction == 0 ? DBNull.Value : (object)_Direction };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.HDOP, SqlDbType.Int) { IsNullable = true, Value = _HDOP == 0 ? DBNull.Value : (object)_HDOP };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Satellites, SqlDbType.Int) { IsNullable = true, Value = _Satellites == 0 ? DBNull.Value : (object)_Satellites };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.IOStatus, SqlDbType.Int) { IsNullable = true, Value = _IOStatus == 0 ? DBNull.Value : (object)_IOStatus };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Input1, SqlDbType.Int) { IsNullable = true, Value = _Input1 == 0 ? DBNull.Value : (object)_Input1 };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Input2, SqlDbType.Int) { IsNullable = true, Value = _Input2 == 0 ? DBNull.Value : (object)_Input2 };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.RtcDate, SqlDbType.Date) { IsNullable = true, Value = _RtcDate };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.RtcTime, SqlDbType.Time) { IsNullable = true, Value = _RtcTime };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.PosDate, SqlDbType.Date) { IsNullable = true, Value = _PosDate };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.PosTime, SqlDbType.Time) { IsNullable = true, Value = _PosTime };
            sqlCmd.Parameters.Add(prm);
        }
    }
}
