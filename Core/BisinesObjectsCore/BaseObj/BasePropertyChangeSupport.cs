using System;
using System.ComponentModel;

namespace BusinessObjects
{
    /// <summary>Абстрактный класс реализущий поддержку интерфейсов INotifyPropertyChanged, INotifyPropertyChanging</summary>
    public abstract class BasePropertyChangeSupport : INotifyPropertyChanged, INotifyPropertyChanging, ISupportInitialize
    {
        /// <summary>Конструктор</summary>
        protected BasePropertyChangeSupport()
        {
        }
        /// <summary>
        /// Сравнение строк с поддержкой NULL зачений
        /// </summary>
        /// <param name="a">Строка</param>
        /// <param name="b">Строка</param>
        /// <returns></returns>
        protected bool StringNullCompare(string a, string b)
        {
            if (string.Equals(a, b))
            {
                return true;
            }
            else
            {
                a = a == null ? string.Empty : a;
                b = b == null ? string.Empty : b;
                return string.Equals(a, b);
            }
        }
        #region INotifyPropertyChanging
        [NonSerialized]
        private PropertyChangingEventHandler _serializablePropertyChangingHandlers;
        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging
        {
            add
            {
                _serializablePropertyChangingHandlers = (PropertyChangingEventHandler)
                      Delegate.Combine(_serializablePropertyChangingHandlers, value);
            }
            remove
            {
                _serializablePropertyChangingHandlers = (PropertyChangingEventHandler)
                      Delegate.Remove(_nonSerializableHandlers, value);
            }
        }
        /// <summary>
        /// Начало изменения свойства
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanging(string propertyName)
        {
            if (IsStateInit) return;
            SaveState(false);
            if (_serializablePropertyChangingHandlers != null)
                _serializablePropertyChangingHandlers.Invoke(this,
                                                             new PropertyChangingEventArgs(propertyName));
        }
        #endregion
        #region INotifyPropertyChanged
        [NonSerialized]
        private PropertyChangedEventHandler _nonSerializableHandlers;
        private PropertyChangedEventHandler _serializableHandlers;
        /// <summary>Событие подтверждающее изменение свойства</summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (value.Method.IsPublic &&
                   (value.Method.DeclaringType.IsSerializable ||
                    value.Method.IsStatic))
                    _serializableHandlers = (PropertyChangedEventHandler)
                      Delegate.Combine(_serializableHandlers, value);
                else
                    _nonSerializableHandlers = (PropertyChangedEventHandler)
                      Delegate.Combine(_nonSerializableHandlers, value);
            }
            remove
            {
                if (value.Method.IsPublic &&
                   (value.Method.DeclaringType.IsSerializable ||
                    value.Method.IsStatic))
                    _serializableHandlers = (PropertyChangedEventHandler)
                      Delegate.Remove(_serializableHandlers, value);
                else
                    _nonSerializableHandlers = (PropertyChangedEventHandler)
                      Delegate.Remove(_nonSerializableHandlers, value);
            }
        }

        /// <summary>Подтверждение изменение свойства</summary>
        /// <param name="propertyName"></param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (IsStateInit) return;
            if (_nonSerializableHandlers != null)
                _nonSerializableHandlers.Invoke(this,
                                                new PropertyChangedEventArgs(propertyName));
            if (_serializableHandlers != null)
                _serializableHandlers.Invoke(this,
                                             new PropertyChangedEventArgs(propertyName));
            _isChanged = true;
        }
        public virtual void InvokePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }


        #region События изменения
        [NonSerialized]
        private EventHandler _changedHandlers;
        /// <summary>Событие начала сохранения</summary>
        public event EventHandler Changed
        {
            add
            {
                _changedHandlers = (EventHandler)
                      Delegate.Combine(_changedHandlers, value);
            }
            remove
            {
                _changedHandlers = (EventHandler)
                      Delegate.Remove(_changedHandlers, value);
            }
        }
        protected virtual void OnChanged()
        {
            if (_changedHandlers != null)
                _changedHandlers.Invoke(this, EventArgs.Empty);
        }
        #endregion
        #endregion
        private bool _isChanged;
        /// <summary>Является ли объект измененным</summary>
        [Description("Является ли объект измененным"), Browsable(false)]
        public bool IsChanged
        {
            get { return _isChanged; }
            set 
            { 
                _isChanged = value;
                OnChanged();
            }
        }

        /// <summary>Сохранение состояния</summary>
        /// <param name="overwrite">Переписать текущее состояние</param>
        public abstract bool SaveState(bool overwrite);

        /// <summary>Востановить состояние</summary>
        public abstract void RestoreState();
        #region ISupportInitialize Members

        /// <summary>Объект находится в состоянии загрузки</summary>
        protected bool IsStateInit { get; set; }

        void ISupportInitialize.BeginInit()
        {
            OnBeginInit();
        }
        /// <summary>Начало инициализации</summary>
        protected void OnBeginInit()
        {
            if (!IsStateInit)
                IsStateInit = true;
        }
        void ISupportInitialize.EndInit()
        {
            OnEndInit();
        }
        /// <summary>Окончание инициализации</summary>
        protected virtual void OnEndInit()
        {
            if (IsStateInit)
                IsStateInit = false;
            IsChanged = false;
        }
        #endregion
    }
}
