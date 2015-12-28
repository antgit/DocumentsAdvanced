using System.Data;
using System.Data.SqlClient;
namespace BusinessObjects.Security
{
    /// <summary>
    /// Эффективные общие права
    /// </summary>
    public class CommonRightView : NamedRight, ICommonRights
    {

// ReSharper disable InconsistentNaming
        /// <summary>
        /// Просмотр только собственных документов
        /// </summary>
        public const string VIEWONLYMYDOCUMENTS = "VIEWONLYMYDOCUMENTS";
        /// <summary>
        /// Только чтение
        /// </summary>
        public const string READONLY = "READONLY";
        /// <summary>
        /// Системный администратор
        /// </summary>
        public const string ENTERPRIZEADMIN = "ENTERPRIZEADMIN";
        /// <summary>
        /// Администратор
        /// </summary>
        public const string ADMIN = "ADMIN";
        /// <summary>
        /// Администратор интерфейса
        /// </summary>
        public const string ADMINUI = "ADMINUI";
        /// <summary>
        /// Администратор Web приложения
        /// </summary>
        public const string WEBADMIN = "WEBADMIN";
// ReSharper restore InconsistentNaming

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        public CommonRightView(IWorkarea wa)
            : base(wa)
        {
            DefaltValue = false;
        }
        /// <summary>
        /// Загрузка данных из базы данных
        /// </summary>
        protected override void LoadRights()
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Uid>().Entity.FindMethod("RightByUser").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = Workarea.CurrentUser.Name;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Acl.Add(reader.GetString(0), reader.IsDBNull(1)? new int?(): reader.GetInt32(1));
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
        /// Идентификатор пользователя
        /// </summary>
        public int UidId
        {
            get { return Workarea.CurrentUser.Id; }
        }
	
        /// <summary>
        /// Соединение
        /// </summary>
        /// <remarks>Данное правило является определяемым на уровне текущего состояния пользователя: 
        /// если текущее состояние не соответствует состоянию "Активен", 
        /// то пользователь не имеет права на вход в систему.</remarks>
        public bool Connect
        {
            get { return Workarea.CurrentUser.StateId==1; }
        }
        /// <summary>
        /// Только чтение
        /// </summary>
        /// <remarks>Используются общиее права по наименованию прав READONLY. 
        /// Наличие данного разрешения дает доступ только на чтение, без возможности внесения изменений.</remarks>
        public bool ReadOnly
        {
            get { return IsAllow(READONLY); }
        }
        /// <summary>
        /// Системный администратор
        /// </summary>
        /// <remarks>Используются общиее права по наименованию прав ENTERPRIZEADMIN. 
        /// Наличие данного разрешения предоставляет абсолютные права в программе.</remarks>
        public bool AdminEnterprize
        {
            get { return IsAllow(ENTERPRIZEADMIN); }
        }
        /// <summary>
        /// Администратор
        /// </summary>
        /// <remarks>Используются общиее права по наименованию прав ADMIN. 
        /// Наличие данного разрешения позволяет выполнять базовые 
        /// административные функции в программе.</remarks>
        public bool Admin
        {
            get { return IsAllow(ADMIN); }
        }
        /// <summary>
        /// Администратор интерфейса
        /// </summary>
        /// <remarks>Используются общиее права по наименованию прав ADMINUI. 
        /// Данное правило дает возможность доступа к изненению пользовательского интерфейса, 
        /// как например изменение видимости колонок в списке объектов, 
        /// изменение внешнего вида основного окна программы и т.д.</remarks>
        public bool AdminUserInterface
        {
            get { return IsAllow(ADMINUI); }
        }
        /// <summary>
        /// Ограничение на просмотр собственных документов
        /// </summary>
        public bool ViewOnlyMyDocyments
        {
            get { return IsAllow(VIEWONLYMYDOCUMENTS); }
        }

        /// <summary>
        /// Администратор Web приложения
        /// </summary>
        public bool AdminWeb
        {
            get { return IsAllow(WEBADMIN); }
        }
    }
}