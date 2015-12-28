using System;
using System.Activities;

namespace BusinessObjects.Workflows.System
{
    /// <summary>
    /// Cоздание метаданных системного объекта о связанных файлах
    /// </summary>
    public sealed class ActivityEntityTypeMetaFiles : CodeActivity
    {
        public ActivityEntityTypeMetaFiles()
            : base()
        {
            DisplayName = "Cоздание метаданных системного объекта о связанных файлах";
        }
        // Define an activity input argument of type string
        [RequiredArgument]
        public InArgument<EntityType> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.Text);
            Workarea wa = CurrentObject.Get(context).Workarea;
            EntityType systemType = CurrentObject.Get(context);
            string sourceTypeName = systemType.GetType().Name;
            ProcedureMap newObject = new ProcedureMap
                                         {
                                             TypeId = 0,
                                             Workarea = wa,
                                             EntityId = ((Int16)systemType.Id),
                                             Name = "Создание (обновление) данных о связанном файле",
                                             Schema = systemType.NameSchema,
                                             Procedure = systemType.CodeClass + "FileDataChainInsertUpdate",
                                             Method = sourceTypeName + "FileDataChainInsertUpdate"
                                         };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Загрузка данных о связаных файлах",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "FileDataChainLoadSources",
                                Method = "LoadFiles"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Представления значений о связанных файлах",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "FilesLoadView",
                                Method = "FileDataGetView"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Список данных о связях",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "FilesLoadList",
                                Method = "FileDataLoadList"
                            };
            newObject.Save();


            systemType.RefreshMethods();
        }
    }
}