using System.Collections.Generic;

namespace BusinessObjects.Security
{
    /// <summary>Именованое разрешение</summary>
    public abstract class NamedRight
    {
        protected internal Dictionary<string, int?> Acl;

        protected NamedRight ()
        {
            Acl = new Dictionary<string, int?>();
        }
        /// <summary>Конструктор</summary>
        protected NamedRight(IWorkarea wa): this()
        {
            _defaltValue = true;
            _workarea = wa;
        }
        /// <summary>Обновить список разрешений</summary>
        public virtual void Refresh()
        {
            if (Acl == null)
                Acl = new Dictionary<string, int?>();
            else if (Acl.Count > 0)
                Acl.Clear();
            LoadRights();
        }
        /// <summary>Загрузка данных из базы данных</summary>
        protected abstract void LoadRights();

        private IWorkarea _workarea;
        /// <summary>Рабочая область</summary>
        public IWorkarea Workarea
        {
            get { return _workarea; }
            protected set { _workarea = value; }
        }
        private bool _defaltValue;
        /// <summary>Значение прав по умолчанию</summary>
        public bool DefaltValue
        {
            get { return _defaltValue; }
            protected set { _defaltValue = value; }
        }
	
        /// <summary>Разрешено ли правило</summary>
        /// <remarks>Если правило имеет явное разрешение возвращается <c>true</c>, 
        /// если правило явно не указано возвращается значение по умолчанию <see cref="BusinessObjects.Security.NamedRight.DefaltValue"/></remarks>
        /// <param name="key">Ключ наименования правила</param>
        /// <returns></returns>
        public virtual bool IsAllow(string key)
        {
            return IsSet(key) ? OnIsAllow(key) : _defaltValue;
        }

        /// <summary>Проверка, установлено ли разрешение явно</summary>
        /// <param name="key">Ключ наименования правила</param>
        /// <returns></returns>
        public virtual bool IsSet(string key)
        {
            return OnIsSet(key);
        }
        /// <summary>Определение соответствующего разрешения</summary>
        /// <param name="key">Ключ наименования правила</param>
        /// <returns></returns>
        protected virtual bool OnIsAllow(string key)
        {
            return Acl[key] == 1 || _defaltValue;
        }

        /// <summary>Поиск соответствующего разрешения</summary>
        /// <param name="key">Ключ наименования правила</param>
        /// <returns></returns>
        protected virtual bool OnIsSet(string key)
        {
            return (Acl.ContainsKey(key) && Acl[key].HasValue);
        }
    }
}
