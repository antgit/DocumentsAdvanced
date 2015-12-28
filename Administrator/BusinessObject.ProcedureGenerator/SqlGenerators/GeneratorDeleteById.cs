namespace BusinessObjects.ProcedureGenerator
{
    internal class GeneratorDeleteById: DataObjectGenerator    
    {
        public GeneratorDeleteById()
        {
            OptionDrop = false;
            OptionCreate = false;
            Name = "ХП удаления по идентификатору";
            Memo = "Хранимая процедура удаления по идентификатору";
            Template = "SET ANSI_NULLS ON \n"
                       + "GO \n"
                       + "SET QUOTED_IDENTIFIER ON \n"
                       + "GO \n"
                       + "%action% PROCEDURE %procedurename% \n"
                       + "@id INT, @Version BINARY (8)=null \n"
                       + "AS \n"
                       + "BEGIN \n"
                       + "SET NOCOUNT ON; \n"
                       + "BEGIN TRY \n"
                       + "	declare @count int \n"
                       + "	if(@Version is null) \n"
                       + "		delete [%schema%].[%table%] where [Id] = @id \n"
                       + "	else \n"
                       + "		delete [%schema%].[%table%] where [Id] = @id and [Version]=@Version \n"
                       + "	select @count = @@rowcount \n"
                       + "	if(@count=0) -- нет удаленных записей? \n"
                       + "	begin \n"
                       + "	if not exists(select [Id] from [%schema%].[%table%] where [Id] = @Id) \n"
                       + "		RAISERROR (N'Запись в таблице  с идентификатором %d отсутствует',16, 1, @Id); \n"
                       + "	if (select [Version] from [%schema%].[%table%] where [Id] = @id) <> @Version \n"
                       + "		RAISERROR (N'Запись в таблице с идентификатором %d была изменена, конфликт версий',16, 1, @Id); \n"
                       + "	end \n"
                       + "	RETURN 0 \n"
                       + "END TRY \n"
                       + "BEGIN CATCH \n"
                       + " -- Откат и вставка информации в таблицу ErrorLog \n"
                       + "	IF @@TRANCOUNT > 0 \n"
                       + "	BEGIN \n"
                       + "		ROLLBACK TRANSACTION; \n"
                       + "	END \n"
                       + " declare @rc int \n"
                       + " EXECUTE [dbo].[uspLogError] @rc OUTPUT; \n"
                       + " RETURN @rc \n"
                       + "END CATCH; \n"
                       + "END \n"
                       + "GO";
            
        }
        public override string Generate()
        {
            string tblNameShort = TableName;
            if(tblNameShort.EndsWith("s"))
                tblNameShort = tblNameShort.Remove(tblNameShort.Length - 1);
            string procname = "[" + Schema + "].[" + tblNameShort + "Delete" +"]";
            string currentaction = (OptionCreate || OptionDrop) ? "CREATE" : "ALTER";

            string deleteProc = ("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'%procedurename%') AND type in (N'P', N'PC')) \n"
           + "DROP PROCEDURE %procedurename% \n"
           +"GO \n").Replace("%procedurename%", procname);

            string res = Template.Replace("%action%",currentaction).Replace("%schema%", Schema).Replace("%table%", TableName).Replace("%procedurename%", procname);
            if (OptionDrop)
                res = deleteProc + res;

            return res;
        }
    }
    internal class GeneratorFindIdByGuid : DataObjectGenerator
    {
        public GeneratorFindIdByGuid()
        {
            OptionDrop = false;
            OptionCreate = false;
            Name = "ХП поиска идентификатора по глобальному идентификатору идентификатору";
            Memo = "ХП поиска идентификатора по глобальному идентификатору идентификатору";
            Template = "SET ANSI_NULLS ON \n"
           + "GO \n"
           + "SET QUOTED_IDENTIFIER ON \n"
           + "GO \n"
           + "%action% PROCEDURE %procedurename% \n"
           + "@Guid UNIQUEIDENTIFIER \n"
           + "AS \n"
           + "set nocount on \n"
           + "BEGIN TRY \n"
           + "DECLARE @Id INT \n"
           + "SELECT @Id = Id from  [%schema%].[%table%]  where Guid=@Guid  \n"
           + "SELECT isnull(@Id,0) \n"
           + "RETURN 0; \n"
           + "END TRY \n"
           + "BEGIN CATCH \n"
           + "	IF @@TRANCOUNT > 0 \n"
           + "	BEGIN \n"
           + "		ROLLBACK TRANSACTION; \n"
           + "	END \n"
           + " declare @rc int \n"
           + " EXECUTE [dbo].[uspLogError] @rc OUTPUT; \n"
           + " RETURN @rc \n"
           + "END CATCH \n"
           + "GO";
           

        }
        public override string Generate()
        {
            string tblNameShort = TableName;
            if (tblNameShort.EndsWith("s"))
                tblNameShort = tblNameShort.Remove(tblNameShort.Length - 1);
            string procname = "[" + Schema + "].[" + tblNameShort + "FindIdByGuid" + "]";
            string currentaction = (OptionCreate || OptionDrop) ? "CREATE" : "ALTER";

            string deleteProc = ("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'%procedurename%') AND type in (N'P', N'PC')) \n"
           + "DROP PROCEDURE %procedurename% \n"
           + "GO \n").Replace("%procedurename%", procname);

            string res = Template.Replace("%action%", currentaction).Replace("%schema%", Schema).Replace("%table%", TableName).Replace("%procedurename%", procname);
            if (OptionDrop)
                res = deleteProc + res;

            return res;
        }
    }
}