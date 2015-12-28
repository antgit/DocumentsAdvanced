using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace BusinessObjects
{
    /// <summary>Вид системного объекта</summary>
    [Serializable]
    public sealed class EntityKind : BaseKind
    {
        /// <summary>Конструктор</summary>
        public EntityKind(): base()
        {
        }
        #region Сериализация XML
        //http://devolutions.net/articles/dot-net/Net-Serialization-FAQ.aspx
        //http://www.albahari.com/nutshell/ch15.aspx
        //http://insideaspnet.com/index/custom-serialization/
        // The following constructor is for deserialization
        private EntityKind(SerializationInfo info, StreamingContext context)
        {
            //productId = info.GetInt32("Product ID");
            //price = info.GetDecimal("Price");
            //quantity = info.GetInt32("Quantity");
            //total = price * quantity;
        }
        // The following method is called during serialization
        [SecurityPermission(SecurityAction.Demand,SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(GlobalPropertyNames.DatabaseId, DatabaseId);
            
        }

        #endregion
        public override void Delete(bool checkVersion = true)
        {
            CanDelete();
            Delete(Workarea.FindMethod("Core.EntityKindDelete").FullName, checkVersion);
            // TODO: Добавить процедуры CANDELETE
            //if (CanDeleteFromDataBase())
            //    Delete(FindProcedure(GlobalMethodAlias.Delete), checkVersion);
            //else
            //    throw new ValidateException("Объект не может быть удален на основе проверки целосности данных в базе данных!");
        }

        /// <summary>Создание объекта в базе данных</summary>
        /// <remarks>Перед созданием объекта не выполняется проверка <see cref="BaseCoreObject.Validate"/>.
        /// Метод использует хранимую процедуру "Core.EntityKindInsert".
        /// </remarks>
        /// <seealso cref="BaseCoreObject.Load(int)"/>
        /// <seealso cref="BaseCoreObject.Validate"/>
        /// <seealso cref="BaseCoreObject.Update(bool)"/>
        protected override void Create()
        {
            CancelEventArgs e = new CancelEventArgs();
            OnCreating(e);
            if (e.Cancel)
                return;
            Create("Core.EntityKindInsert");
            OnCreated();
        }

        /// <summary>Обновление объекта в базе данных</summary>
        /// <remarks>Метод использует хранимую процедуру "Core.EntityKindUpdate"</remarks>
        /// <seealso cref="BaseCoreObject.Create()"/>
        /// <seealso cref="BaseCoreObject.Load(int)"/>
        /// <seealso cref="BaseCoreObject.Validate"/>
        protected override void Update(bool versionControl = true)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnUpdating(e);
            if (e.Cancel)
                return;
            Update("Core.EntityKindUpdate", true);
            OnUpdated();
        }

        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру "Core.EntityKindLoad"</remarks>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnLoading(e);
            if (e.Cancel)
                return;
            Load(value, "Core.EntityKindLoad");
        }

    }
}
