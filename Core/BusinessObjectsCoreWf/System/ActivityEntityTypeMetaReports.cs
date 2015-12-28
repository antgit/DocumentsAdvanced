using System;
using System.Activities;

namespace BusinessObjects.Workflows.System
{
    /// <summary>
    /// Cоздание метаданных системного объекта о отчетах
    /// </summary>
    public sealed class ActivityEntityTypeMetaReports : CodeActivity
    {
        public ActivityEntityTypeMetaReports()
            : base()
        {
            DisplayName = "Cоздание метаданных системного объекта о отчетах";
        }
        // Define an activity input argument of type string
        [RequiredArgument]
        public InArgument<EntityType> CurrentObject { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            
            Workarea wa = CurrentObject.Get(context).Workarea;
            EntityType systemType = CurrentObject.Get(context);
            ProcedureMap newObject;
            if (!systemType.Methods.Exists(s => s.Method == "ReportChainByElement"))
            {
                newObject = new ProcedureMap
                                             {
                                                 TypeId = 0,
                                                 Workarea = wa,
                                                 EntityId = ((Int16) systemType.Id),
                                                 Name = "Связанные отчеты - список отчетов",
                                                 Schema = "Reports",
                                                 Procedure =
                                                     string.Format("Reports{0}LoadByElement", systemType.CodeClass),
                                                 Method = "ReportChainByElement"
                                             };
                newObject.Save();
            }

            if (!systemType.Methods.Exists(s => s.Method == "ReportChainChangeState"))
            {
                newObject = new ProcedureMap
                                {
                                    TypeId = 0,
                                    Workarea = wa,
                                    EntityId = ((Int16) systemType.Id),
                                    Name = "Связанные отчеты - изменение состояния",
                                    Schema = "Reports",
                                    Procedure = string.Format("Reports{0}ChangeState", systemType.CodeClass),
                                    Method = "ReportChainChangeState"
                                };
                newObject.Save();
            }
            if (!systemType.Methods.Exists(s => s.Method == "ReportChainDelete"))
            {
                newObject = new ProcedureMap
                                {
                                    TypeId = 0,
                                    Workarea = wa,
                                    EntityId = ((Int16) systemType.Id),
                                    Name = "Связанные отчеты - удаление",
                                    Schema = "Report",
                                    Procedure = string.Format("Reports{0}sDelete", systemType.CodeClass),
                                    Method = "ReportChainDelete"
                                };
                newObject.Save();
            }
            if (!systemType.Methods.Exists(s => s.Method == "ReportChainExistsGuids"))
            {
                newObject = new ProcedureMap
                                {
                                    TypeId = 0,
                                    Workarea = wa,
                                    EntityId = ((Int16) systemType.Id),
                                    Name = "Связанные отчеты - проверка по глобальному идентификатору",
                                    Schema = "Report",
                                    Procedure = string.Format("Reports{0}ExistsGuids", systemType.CodeClass),
                                    Method = "ReportChainExistsGuids"
                                };
                newObject.Save();
            }
            if (!systemType.Methods.Exists(s => s.Method == "ReportChainExistsId"))
            {
                newObject = new ProcedureMap
                                {
                                    TypeId = 0,
                                    Workarea = wa,
                                    EntityId = ((Int16) systemType.Id),
                                    Name = "Связанные отчеты - проверка по идентификатору",
                                    Schema = "Report",
                                    Procedure = string.Format("Reports{0}ExistsId", systemType.CodeClass),
                                    Method = "ReportChainExistsId"
                                };
                newObject.Save();
            }
            if (!systemType.Methods.Exists(s => s.Method == "ReportChainFindIdByGuid"))
            {
                newObject = new ProcedureMap
                                {
                                    TypeId = 0,
                                    Workarea = wa,
                                    EntityId = ((Int16) systemType.Id),
                                    Name = "Связанные отчеты - поиск идентификатора по глобальному идентификатору",
                                    Schema = "Report",
                                    Procedure = string.Format("Reports{0}FindIdByGuid", systemType.CodeClass),
                                    Method = "ReportChainFindIdByGuid"
                                };
                newObject.Save();
            }
            if (!systemType.Methods.Exists(s => s.Method == "ReportChainInsert"))
            {
                newObject = new ProcedureMap
                                {
                                    TypeId = 0,
                                    Workarea = wa,
                                    EntityId = ((Int16) systemType.Id),
                                    Name = "Связанные отчеты - создание",
                                    Schema = "Report",
                                    Procedure = string.Format("Reports{0}InsertUpdate", systemType.CodeClass),
                                    Method = "ReportChainInsert"
                                };
                newObject.Save();
            }
            if (!systemType.Methods.Exists(s => s.Method == "ReportChainLoad"))
            {
                newObject = new ProcedureMap
                                {
                                    TypeId = 0,
                                    Workarea = wa,
                                    EntityId = ((Int16) systemType.Id),
                                    Name = "Связанные отчеты - загрузка по идентификатору",
                                    Schema = "Report",
                                    Procedure = string.Format("Reports{0}Load", systemType.CodeClass),
                                    Method = "ReportChainLoad"
                                };
                newObject.Save();
            }
            if (!systemType.Methods.Exists(s => s.Method == "ReportChainUpdate"))
            {
                newObject = new ProcedureMap
                                {
                                    TypeId = 0,
                                    Workarea = wa,
                                    EntityId = ((Int16) systemType.Id),
                                    Name = "Связанные отчеты - обновление",
                                    Schema = "Report",
                                    Procedure = string.Format("Reports{0}InsertUpdate", systemType.CodeClass),
                                    Method = "ReportChainUpdate"
                                };
                newObject.Save();
            }
            if (!systemType.Methods.Exists(s => s.Method == "ReportLoadAll"))
            {
                newObject = new ProcedureMap
                                {
                                    TypeId = 0,
                                    Workarea = wa,
                                    EntityId = ((Int16) systemType.Id),
                                    Name = "Связанные отчеты - загрузить все",
                                    Schema = "Report",
                                    Procedure = string.Format("Reports{0}LoadAll", systemType.CodeClass),
                                    Method = "ReportLoadAll"
                                };
                newObject.Save();
            }
        }
    }
}