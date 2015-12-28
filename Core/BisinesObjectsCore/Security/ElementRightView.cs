using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Security
{

    public sealed class EntityRightView
    {
        private class StorageRirhts
        {
            public StorageRirhts(int elementId, string code, int? value)
            {
                _elementId = elementId;
                _code = code;
                _value = value;
            }
            private readonly int _elementId;

            public int ElementId
            {
                get { return _elementId; }
            }

            private readonly string _code;

            public string Code
            {
                get { return _code; }
            }
            private readonly int? _value;

            public int? Value
            {
                get { return _value; }
            }


        }

        readonly List<StorageRirhts> _rightsList = new List<StorageRirhts>();
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        public EntityRightView(IWorkarea wa)
        {

            _workarea = wa;
            _defaltValue = _workarea.Access.RightCommon.AdminEnterprize;
            Refresh();
        }
        private readonly IWorkarea _workarea;
        /// <summary>
        /// Рабочая область
        /// </summary>
        public IWorkarea Workarea
        {
            get { return _workarea; }
        }
        private readonly bool _defaltValue;
        /// <summary>
        /// Значение прав по умолчанию
        /// </summary>
        public bool DefaltValue
        {
            get { return _defaltValue; }
        }
        /// <summary>
        /// Обновить
        /// </summary>
        public void Refresh()
        {
            _rightsList.Clear();
            LoadRights();
        }
        /// <summary>
        /// Загрузить данные из базы данных
        /// </summary>
        void LoadRights()
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("RightByUserForElementGroup").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = Workarea.CurrentUser.Name;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            StorageRirhts r = new StorageRirhts(reader.GetInt16(0), reader.GetString(1), reader.IsDBNull(2) ? new int?() : reader.GetInt32(2));
                            _rightsList.Add(r);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /// <summary>
        /// Проверка, установлено ли разрешение явно
        /// </summary>
        /// <param name="code">Ключ наименования правила</param>
        /// <param name="elementId">Идентификатор записи</param>
        /// <returns></returns>
        public bool IsSet(string code, int elementId)
        {
            return (_rightsList.Where(f => f.Code == code && f.ElementId == elementId).Count() > 0);
        }
        /// <summary>
        /// Разрешено ли правило
        /// </summary>
        /// <remarks>Если правило имеет явное разрешение возвращается <c>true</c>, 
        /// если правило явно не указано возвращается значение по умолчанию <see cref="EntityRightView.DefaltValue"/></remarks>
        /// <param name="code">Ключ наименования правила</param>
        /// <param name="elementId">Идентификатор записи</param>
        /// <returns></returns>
        public bool IsAllow(string code, int elementId)
        {
            if (IsSet(code, elementId))
            {
                return _rightsList.First(f => f.Code == code && f.ElementId == elementId).Value == 1;
            }
            return _defaltValue;
        }

        /// <summary>
        /// Список идентификатор записей имеющих явные разрешения 
        /// </summary>
        /// <param name="code">Ключ наименования правила</param>
        /// <returns></returns>
        public List<int> GetAllowed(string code)
        {
            return _rightsList.Where(f => f.Code == code && f.Value == 1).Select(f => f.ElementId).ToList();
        }
    }

    public class ElementRightView
    {
        private class StorageRirhts
        {
            public StorageRirhts(int elementId, string code, int? value, string elementCode)
            {
                _elementId = elementId;
                _code = code;
                _value = value;
                ElementCode = elementCode;
            }
            private int _elementId;

            public string ElementCode { get; set; }
            public int ElementId
            {
                get { return _elementId; }
                set { _elementId = value; }
            }

            private string _code;

            public string Code
            {
                get { return _code; }
                set { _code = value; }
            }
            private int? _value;

            public int? Value
            {
                get { return _value; }
                set { _value = value; }
            }
	
	
        }
        private readonly short _kind;
        readonly List<StorageRirhts> _rightsList = new List<StorageRirhts>();

        public ElementRightView()
        {
            
        }
        /// <summary>Коснтруктор</summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="kind">Идентификатор типа <see cref="EntityType.Id"/> </param>
        /// <param name="currentUser">Имя пользователя</param>
        public ElementRightView(IWorkarea wa, short kind, string currentUser=null)
        {

            _workarea = wa;
            _defaltValue = (!_workarea.IsWebApplication && _workarea.Access.RightCommon.AdminEnterprize);
            _kind = kind;
            CurrentUser = currentUser;
            if (CurrentUser == null)
                CurrentUser = Workarea.CurrentUser.Name;
            Refresh();
        }

        private int _count;
        /// <summary>
        /// Количество записей о разрешениях
        /// </summary>
        public int Count
        {
            get
            {
                if (_rightsList == null)
                    Refresh();
                return _rightsList.Count;
            }
        }

        private readonly string CurrentUser;
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
        /// <summary>Обновить</summary>
        public void Refresh()
        {
            _rightsList.Clear();
            LoadRights();
        }
        /// <summary>Загрузить разрешения</summary>
        protected virtual void LoadRights()
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("ElementRightsListByUser").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = CurrentUser;
                        
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = _kind;
                        if((!_workarea.IsWebApplication && _workarea.Access.RightCommon.AdminEnterprize))
                            cmd.Parameters.Add(GlobalSqlParamNames.UseMin, SqlDbType.Bit).Value = 0;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            StorageRirhts r = new StorageRirhts(reader.GetInt32(0), reader.GetString(1), reader.IsDBNull(2) ? new int?() : reader.GetInt32(2), reader.IsDBNull(4)? string.Empty: reader.GetString(4));
                            _rightsList.Add(r);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        /// <summary>Проверка, установлено ли разрешение явно</summary>
        /// <param name="code">Ключ наименования правила</param>
        /// <param name="elementId">Идентификатор записи</param>
        /// <returns></returns>
        public bool IsSet(string code, int elementId)
        {
            return (_rightsList.Where(f => f.Code == code && f.ElementId == elementId).Count() > 0);
        }
        public bool IsSet(string code, string elementCode)
        {
            return (_rightsList.Where(f => f.Code == code && f.ElementCode == elementCode).Count() > 0);
        }
        /// <summary>Разрешено ли правило</summary>
        /// <remarks>Если правило имеет явное разрешение возвращается <c>true</c>, 
        /// если правило явно не указано возвращается значение по умолчанию <see cref="EntityRightView.DefaltValue"/></remarks>
        /// <param name="code">Ключ наименования правила</param>
        /// <param name="elementId">Идентификатор записи</param>
        /// <returns></returns>
        public bool IsAllow(string code, int elementId)
        {
            if (IsSet(code, elementId))
            {
                return _rightsList.First(f => f.Code == code && f.ElementId == elementId).Value == 1;
            }
            return _defaltValue;
        }

        public bool IsAllow(string code, string elementCode)
        {
            if (IsSet(code, elementCode))
            {
                return _rightsList.First(f => f.Code == code && f.ElementCode == elementCode).Value == 1;
            }
            return _defaltValue;
        }
        /// <summary>Список идентификаторов явно разрешенных записей</summary>
        /// <param name="code">Ключ наименования правила</param>
        /// <returns></returns>
        public List<int> GetAllowed(string code)
        {
            return _rightsList.Where(f => f.Code == code && f.Value == 1).Select(f => f.ElementId).ToList();            
        }
        /// <summary>Список идентификаторов явно запрещенных записей</summary>
        /// <param name="code">Ключ наименования правила</param>
        /// <returns></returns>
        public List<int> GetDeny(string code)
        {
            return _rightsList.Where(f => f.Code == code && f.Value == 0).Select(f => f.ElementId).ToList();
        }
    }
    /*
    /// <summary>
    /// Общие права в диалоге дерева
    /// </summary>
    class CommonTreeRight: RightByKey
    {
        public CommonTreeRight(Workarea wa): base(wa)
        {
            DefaltValue = false;
        }
        public void Refresh()
        {
        }

        private bool _AllowDelete;
        /// <summary>
        /// Разрешить удаление
        /// </summary>
        public bool AllowDelete
        {
            get { return _AllowDelete; }
            set { _AllowDelete = value; }
        }

        private bool _AllowMove;
        /// <summary>
        /// Разрешить перемещение
        /// </summary>
        public bool AllowMove
        {
            get { return _AllowMove; }
            set { _AllowMove = value; }
        }

        private bool _AllowInclude;
        /// <summary>
        /// Разрешить добавление в иерархии
        /// </summary>
        public bool AllowInclude
        {
            get { return _AllowInclude; }
            set { _AllowInclude = value; }
        }

        private bool _AllowExclude;
        /// <summary>
        /// Разрешить исключение из иерархии
        /// </summary>
        public bool AllowExclude
        {
            get { return _AllowExclude; }
            set { _AllowExclude = value; }
        }

        private bool _AllowPropertiesView;
        /// <summary>
        /// Рвазрешить просмотр свойств
        /// </summary>
        public bool AllowPropertiesView
        {
            get { return _AllowPropertiesView; }
            set { _AllowPropertiesView = value; }
        }
    }
    /// <summary>
    /// Общие права в интерфейсе
    /// </summary>
    class CommonUiRight : RightByKey
    {
        public CommonUiRight(Workarea wa): base(wa)
        {
            DefaltValue = false;
            Refresh();
            //wa.Database.UserName
        }
        public void Refresh()
        {
            _Delete = IsAllow("DELETE");
            _View = IsAllow("VIEW");
            _Create = IsAllow("CREATE");
        }
        private bool _Delete;
        /// <summary>
        /// Удаление
        /// </summary>
        public bool Delete
        {
            get { return _Delete; }
            set { _Delete = value; }
        }
        private bool _Create;
        /// <summary>
        /// Создание
        /// </summary>
        public bool Create
        {
            get { return _Create; }
            set { _Create = value; }
        }
        private bool _View;
        /// <summary>
        /// Просмотр свойств
        /// </summary>
        public bool View
        {
            get { return _View; }
            set { _View = value; }
        }
	
	
	
    }
    */
}
