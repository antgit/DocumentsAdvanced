using System;
using System.Activities;

namespace BusinessObjects.Workflows.System
{
    /// <summary>
    /// C������� ���������� ���������� ������� �� �������� ���������
    /// </summary>
    public sealed class ActivityEntityTypeMetaMain : CodeActivity
    {
        public ActivityEntityTypeMetaMain()
            : base()
        {
            DisplayName = "C������� ���������� ���������� ������� �� �������� ���������";
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
                                             Name = "����������",
                                             Schema = systemType.NameSchema,
                                             Procedure = systemType.CodeClass + "InsertUpdate",
                                             Method = "Update"
                                         };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "��������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "InsertUpdate",
                                Method = "Create"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "��������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "Delete",
                                Method = "Delete"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "��������  �� ��������������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "Load",
                                Method = "Load"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "��� ������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "LoadAll",
                                Method = "LoadAll"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "�������� �� ����������� ��������������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "LoadGuid",
                                Method = "LoadGuid"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "�������� �� ������ ���������������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "LoadList",
                                Method = "LoadList"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "�������� �� ��������������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ExistsId",
                                Method = "ExistsId"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "����� �������������� �� ���������� ���������������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ExistsGuids",
                                Method = "ExistsGuids"
                            };
            newObject.Save();

            
            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "��������� ���������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "ChangeState",
                                Method = "ChangeState"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "�������� ��������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "LoadTemplates",
                                Method = "LoadTemplates"
                            };
            newObject.Save();
            


            systemType.RefreshMethods();
        }
    }
}