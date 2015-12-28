using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects.Security
{
    /// <summary>Разрешение пользователя или группы для записи</summary>
    public class UserRightElement : UserRightBase, IUserRightElement
    {
        /// <summary>Конструктор</summary>
        public UserRightElement()
            : base()
        {
            EntityId = (short) WhellKnownDbEntity.UserRightElement;
        }

        #region Свойства
        private int _elementId;
        /// <summary>Идентификатор элемента</summary>
        public int ElementId
        {
            get { return _elementId; }
            set
            {
                if (value == _elementId) return;
                OnPropertyChanging(GlobalPropertyNames.ElementId);
                _elementId = value;
                OnPropertyChanged(GlobalPropertyNames.ElementId);
            }
        }

        private short _dbEntityId;
        /// <summary>Идентификатор системного типа</summary>
        public short DbEntityId
        {
            get { return _dbEntityId; }
            set
            {
                if (value == _dbEntityId) return;
                OnPropertyChanging(GlobalPropertyNames.EntityId);
                _dbEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.EntityId);
            }
        }
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_dbEntityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DbEntityId, XmlConvert.ToString(_dbEntityId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.DbEntityId) != null)
                _dbEntityId = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.DbEntityId));
        }
        #endregion

        /// <summary>Проверка объекта</summary>
        public override void Validate()
        {
            base.Validate();
            if (_elementId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_IDISEMPTY", 1049));
            if (_dbEntityId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_TYPEISEMPTY", 1049));
        }
        /// <summary>
        /// Установить значения параметров для комманды создания
        /// </summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Sql комманда создания или обновления данных</param>
        /// <param name="validateVersion">Выполнять проверку версии. Параметр используется
        /// только для комманды обновления данных.</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ElementId, SqlDbType.Int)
            {
                IsNullable = false,
                Value = _elementId
            };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt) { IsNullable = false, Value = _dbEntityId };
            sqlCmd.Parameters.Add(prm);
        }

        public override void Load(int value)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnLoading(e);
            if (e.Cancel)
                return;
            Load(value, FindProcedure(GlobalMethodAlias.Load));
        }
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                ElementId = reader.GetInt32(12);
                _dbEntityId = reader.GetInt16(13);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();

        }
        //public override void Load(int value)
        //{
            
        //}
        ///// <summary>Сохранить</summary>
        //public override void Save()
        //{
        //    Validate();
        //    if (IsNew)
        //        Create("Secure.ElementRightsInsert");
        //    else
        //        Update("Secure.ElementRightsUpdate", true);
        //}
        ///// <summary>Удалить</summary>
        ///// <returns></returns>
        //public override int Delete()
        //{
        //    return base.Delete("Secure.ElementRightsDelete");
        //}

        // ReSharper disable InconsistentNaming
        /// <summary> Добавление элементов в иерархию </summary>
        public const string UIADDELEMENTS="UIADDELEMENTS";
        /// <summary> Создание новой группы </summary>
        public const string UINEWHIERARCHY = "UINEWHIERARCHY";
        /// <summary> Создание нового элемента </summary>
        public const string UICREATE = "UICREATE";

        /// <summary> Редактирование </summary>
        public const string UIEDIT = "UIEDIT";
        /// <summary> Обновление данных </summary>
        public const string UIREFRESH = "UIREFRESH";
        /// <summary> Перемещение элементов внутри группы </summary>
        public const string UIHIERARCHYMOVEUPDOWN = "UIHIERARCHYMOVEUPDOWN";
        /// <summary> Перемещение в дереве групп </summary>
        public const string UIMOVEHIERARCHYTREE = "UIMOVEHIERARCHYTREE";
        /// <summary> Поиск </summary>
        public const string UIFIND = "UIFIND";
        /// <summary> Поиск группы </summary>
        public const string UIFINDGROUP = "UIFINDGROUP";
        /// <summary> Панель действий </summary>
        public const string UIVIEWPANELACTION = "UIVIEWPANELACTION";
        /// <summary> Панель свойств </summary>
        public const string UIVIEWPANELPROP = "UIVIEWPANELPROP";
        /// <summary> Видимость строки данных или свойства </summary>
        public const string VIEW = "VIEW";
        /// <summary> Связанные модули </summary>
        public const string UILINKEDMODULES = "UILINKEDMODULES";

        /// <summary> Удаление в корзину </summary>
        public const string UITRASH = "UITRASH";
        /// <summary> Удаление </summary>
        public const string UIDELETE = "UIDELETE";
        /// <summary> Исключение элементов из иерархии </summary>
        public const string UIEXCLUDEELEMENTS = "UIEXCLUDEELEMENTS";
        /// <summary> Запретить использовать </summary>
        public const string UISTATEDENY = "UISTATEDENY";

        /// <summary> Просмотр в виде списка </summary>
        public const string UIVIEWLIST = "UIVIEWLIST";
        /// <summary> Просмотр в виде дерева </summary>
        public const string UIVIEWTREE = "UIVIEWTREE";
        /// <summary> Просмотр в виде групп </summary>
        public const string UITREELIST = "UITREELIST";

        /// <summary> Построить отчет </summary>
        public const string UIREPORTBUILD = "UIREPORTBUILD";
        /// <summary> Изменить отчет в построителе </summary>
        public const string UIEDITREPORT = "UIEDITREPORT";
        /// <summary> Обновление списка отчетов с сервера отчетов </summary>
        public const string UIREPORTLISTFILL = "UIREPORTLISTFILL";

        /// <summary> Обновление файлов </summary>
        public const string UIFILEDATAUPDATE = "UIFILEDATAUPDATE";
        /// <summary> Импорт файла </summary>
        public const string UIFILEDATAIMPORT = "UIFILEDATAIMPORT";

        /// <summary> Действия с документами </summary>
        public const string UIDOCUMENTACTION = "UIDOCUMENTACTION";
        /// <summary> Действия с товарами </summary>
        public const string UIPRODUCTACTION = "UIPRODUCTACTION";
        /// <summary> Действия с корреспондентами </summary>
        public const string UIAGENTACTION = "UIAGENTACTION";
        // ReSharper restore InconsistentNaming        
    }
}
