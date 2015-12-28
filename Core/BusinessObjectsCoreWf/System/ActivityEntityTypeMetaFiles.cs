using System;
using System.Activities;

namespace BusinessObjects.Workflows.System
{
    /// <summary>
    /// C������� ���������� ���������� ������� � ��������� ������
    /// </summary>
    public sealed class ActivityEntityTypeMetaFiles : CodeActivity
    {
        public ActivityEntityTypeMetaFiles()
            : base()
        {
            DisplayName = "C������� ���������� ���������� ������� � ��������� ������";
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
                                             Name = "�������� (����������) ������ � ��������� �����",
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
                                Name = "�������� ������ � �������� ������",
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
                                Name = "������������� �������� � ��������� ������",
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
                                Name = "������ ������ � ������",
                                Schema = systemType.NameSchema,
                                Procedure = systemType.CodeClass + "FilesLoadList",
                                Method = "FileDataLoadList"
                            };
            newObject.Save();


            systemType.RefreshMethods();
        }
    }
}