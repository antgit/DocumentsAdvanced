using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BusinessObjects.Models
{
    /// <summary>
    /// Интерфейс модели данных
    /// </summary>
    public interface IModelData
    {
        /// <summary>Собственный идентификатор модели</summary>
        string ModelId { get; set; }
        /// <summary>Идентификатор пользователя</summary>
        int ModelUserOwnerId { get; set; }
    }
    /// <summary>
    /// Модель 1 уровня
    /// </summary>
    public class CoreModel : IModelData
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public CoreModel()
        {
            ModelId = Guid.NewGuid().ToString();
        }
        private Dictionary<string, string> _errors = new Dictionary<string, string>();

        /// <summary>
        /// Ошибки
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, string> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }
        /// <summary>
        /// Очистить ошибки
        /// </summary>
        public void ClearErrors()
        {
            _errors.Clear();
        }
        /// <summary>Выполнять повторное заполнение данных</summary>
        public bool ReloadOnEdit { get; set; }
        /// <summary>Идентификатор модели</summary>
        public string ModelId { get; set; }
        /// <summary>Идентификатор пользователя</summary>
        public int ModelUserOwnerId { get; set; }
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }
        /// <summary>Глобальный идентификатор</summary>
        public Guid Guid { get; set; }
        /// <summary>Идентификатор в базе источнике</summary>
        public int DbSourceId { get; set; }
        /// <summary>Идентификатор владельца</summary>
        public int DatabaseId { get; set; }
        /// <summary>Идентификатор состояния</summary>
        public int StateId { get; set; }
        /// <summary>Наименование состояния</summary>
        public string StateName { get; set; }
        /// <summary>Дата создания или последнего изменения записи</summary>
        public DateTime? DateModified { get; set; }
        /// <summary>Идентификатор системного типа</summary>
        public short EntityId { get; set; }
        /// <summary>Набор флагов</summary>
        public int FlagsValue { get; set; }
        /// <summary>Только для чтения</summary>
        public bool IsReadOnly { get; set; }
        /// <summary>Флаг "Системный"</summary>
        public bool IsSystem { get; set; }
        /// <summary>Флаг "Активен"</summary>
        public bool IsActive { get; set; }
        /// <summary>Имя пользователя создавшего или изменившего запись</summary>
        public string UserName { get; set; }

        public virtual void GetData(ICoreObject value)
        {
            Id = value.Id;
            Guid = value.Guid;
            UserName = value.UserName;
            DbSourceId = value.DbSourceId;
            DatabaseId = value.DatabaseId;
            StateId = value.StateId;
            StateName = value.State.Name;
            DateModified = value.DateModified;
            EntityId = value.EntityId;
            FlagsValue = value.FlagsValue;
            IsReadOnly = value.IsReadOnly;
            IsSystem = value.IsSystem;
            IsActive = value.IsStateActive;
        }
    }
}