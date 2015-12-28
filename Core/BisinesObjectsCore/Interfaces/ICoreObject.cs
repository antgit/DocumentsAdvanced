using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс базового объекта
    /// </summary>
    public interface ICoreObject
    {
        /// <summary>
        /// Событие после сохранения
        /// </summary>
        event EventHandler Saved;
        /// <summary>
        /// Событие перед сохранением
        /// </summary>
        event CancelEventHandler Saving;

        /// <summary>
        /// Системный объект
        /// </summary>
        EntityType Entity { get; }
        /// <summary>
        /// Идентификатор типа
        /// </summary>
        Int16 EntityId { get; }
        /// <summary>Идентификатор</summary>
        int Id { get; set; }
        /// <summary>Глобальный идентификатор</summary>
        Guid Guid { get; set; }
        /// <summary>Рабочая область</summary>
        Workarea Workarea { get; set; }
        /// <summary>Идентификатор источника</summary>
        int DbSourceId { get; set; }
        /// <summary>Идентификатор владельца</summary>
        int DatabaseId { get; set; }
        /// <summary>Имя пользователя создавшего или изменившего данные</summary>
        string UserName { get; set; }
        /// <summary>Дата создания или изменения данных</summary>
        DateTime? DateModified { get; set; }
        /// <summary>
        /// Флаг
        /// </summary>
        int FlagsValue { get; set; }
        /// <summary>
        /// Идентификатор текущего состояния
        /// </summary>
        int StateId { get; set; }
        /// <summary>
        /// Состояние
        /// </summary>
        State State { get; set; }
        /// <summary>
        /// Версия объекта
        /// </summary>
        /// <returns></returns>
        byte[] GetVersion();
        /// <summary>
        /// Загузка данных по идентификатору
        /// </summary>
        /// <param name="procedureName">Хранимая процедура</param>
        void Load(string procedureName);
        /// <summary>
        /// Загузка данных
        /// </summary>
        /// <param name="reader">Объект System.Data.SqlClient.SqlDataReader содержащий данные</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        void Load(System.Data.SqlClient.SqlDataReader reader, bool endInit=true);
        /// <summary>
        /// Загрузка данных по идентификатору
        /// </summary>
        /// <param name="value"></param>
        void Load(int value);
        /// <summary>
        /// Загрузка данных по глобальному идентификатору
        /// </summary>
        /// <param name="value"></param>
        void Load(Guid value);

        void Refresh(bool all=true);
        void Delete(bool checkVersion = true);
        void Remove();
        void Save();
        void Save(SqlTransaction trans);
        Dictionary<string, string> Errors{get;}

        /// <summary>Состояние по умолчанию</summary>
        bool IsStateDefault { get; }

        /// <summary>Состояние "Активное"</summary>
        bool IsStateActive { get; }

        /// <summary>Состояние "Удален"</summary>
        bool IsStateDeleted { get; }

        /// <summary>Состояние "Запрещен"</summary>
        bool IsStateDeny { get; }

        /// <summary>Состояние "Разрешен"</summary>
        bool IsStateAllow { get; }

        /// <summary>Флаг "Только чтение"</summary>
        bool IsReadOnly { get; }

        /// <summary>Флаг "Скрытый"</summary>
        bool IsHiden { get; }

        /// <summary>Флаг "Системный"</summary>
        bool IsSystem { get; }

        /// <summary>Флаг "Шаблон"</summary>
        bool IsTemplate { get; }

        string FindProcedure(string metodAliasName);

    }
}
