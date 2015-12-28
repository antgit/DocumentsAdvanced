using System;
using System.Activities;

namespace BusinessObjects.Workflows.System
{
    /// <summary>
    /// Cоздание метаданных системного объекта о связях
    /// </summary>
    public sealed class ActivityEntityTypeMetaChains : CodeActivity
    {
        public ActivityEntityTypeMetaChains()
            : base()
        {
            DisplayName = "Cоздание метаданных системного объекта о связях";
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
            ProcedureMap newObject = new ProcedureMap
                                         {
                                             TypeId = 0,
                                             Workarea = wa,
                                             EntityId = ((Int16)systemType.Id),
                                             Name = "Связи - удаление",
                                             Schema = systemType.NameSchema,
                                             Procedure = systemType.CodeClass + "ChainDelete",
                                             Method = "ChainDelete"
                                         };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Связи - представление",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ChainGetView",
                                Method = "ChainGetView"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Связи - создание",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ChainInsert",
                                Method = "ChainInsert"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Связи - Загрузка данных о связи объекта (тип IChains<T>) по идентификатору связи",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ChainLoad",
                                Method = "ChainLoad"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Связи - данные по источнику",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ChainLoadSources",
                                Method = "ChainLoadSource"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Связи - данные по назначению",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ChainLoadDestinations",
                                Method = "ChainsLoadDestination"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Связи - Загрузка данных о связях объекта (тип IChains<T>)",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ChainLoadList",
                                Method = "ChainsLoadList"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Связи - перемещение",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ChainSwap",
                                Method = "ChainSwap"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Связи - обновление",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ChainUpdate",
                                Method = "ChainUpdate"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "Связи - LoadChainSourceList",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "LoadChainId",
                                Method = "LoadChainSourceList"
                            };
            newObject.Save();
            systemType.RefreshMethods();
        }
    }
}