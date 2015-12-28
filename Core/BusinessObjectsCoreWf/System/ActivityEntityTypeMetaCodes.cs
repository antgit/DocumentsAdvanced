using System;
using System.Activities;

namespace BusinessObjects.Workflows.System
{
    /// <summary>
    /// C������� ���������� ���������� ������� � ���������������� �����
    /// </summary>
    public sealed class ActivityEntityTypeMetaCodes : CodeActivity
    {
        public ActivityEntityTypeMetaCodes()
            : base()
        {
            DisplayName = "C������� ���������� ���������� ������� � ���������������� �����";
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
                                             Name = "�������� �������� ����������������� ����",
                                             Schema = systemType.NameSchema,
                                             Procedure = systemType.CodeClass + "CodeDelete",
                                             Method = "CodeDelete"
                                         };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "�������� ����������������� ����",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "CodeGetValues",
                                Method = "CodeGetValues"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "������������� �������� ����������������� ����",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "CodeGetView",
                                Method = "CodeGetView"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "�������� (����������) �������� ����������������� ����",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "CodeInsertUpdate",
                                Method = "CodeInsertUpdate"
                            };
            newObject.Save();

            newObject = new ProcedureMap
                            {
                                TypeId = 0,
                                Workarea = wa,
                                EntityId = ((Int16)systemType.Id),
                                Name = "�������� ������ �������� ����������������� ����",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "CodeLoad",
                                Method = "CodeLoad"
                            };
            newObject.Save();

            systemType.RefreshMethods();
        }
    }
}