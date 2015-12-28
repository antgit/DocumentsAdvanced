using System;
using System.Data.SqlClient;

namespace BusinessObjects
{
    /// <summary>Дополнительные свойства базовых объектов</summary>
    /// <remarks>Соответсвует паре значений "Ключ-Значение"</remarks>
    public class CustomProperty : BasePropertyChangeSupport 
    {
        /// <summary>Конструктор</summary>
        public CustomProperty()
        {
            
        }
        
        /// <summary>Загрузить данные</summary>
        /// <param name="reader">Объект чтения данных</param>
        public void Load(SqlDataReader reader)
        {
            try
            {
                int idx = reader.GetOrdinal(Descriptor.DataField);
                var value = reader.IsDBNull(idx) ? null : reader.GetValue(idx);
                _value = value != null ? value.ToString() : string.Empty;
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            IsChanged = false;
        }
        public CustomPropertyDescriptor Descriptor { get; set; }
        private string _value;
        /// <summary>Значение</summary>
        public string Value
        {
            get { return _value; }
            set
            {
                if (!_value.Equals(value)) return;
                OnPropertyChanging(GlobalPropertyNames.Value);
                _value = value;
                OnPropertyChanged(GlobalPropertyNames.Value);
            }
        }

        /// <summary>Сохранение состояния</summary>
        /// <param name="overwrite">Переписать текущее состояние</param>
        public override bool SaveState(bool overwrite)
        {
            // TODO: Реализация
            return true;
        }

        /// <summary>Востановить состояние</summary>
        public override void RestoreState()
        {
            // TODO: Реализация
        }

    }
}
