using System.Collections.Generic;
using BusinessObjects;
using System.Linq;
using BusinessObjects.Developer;

namespace BusinessObjects.ProcedureGenerator
{
    internal class GeneratorProcedureInsertUpdate : DataObjectGenerator, IDataObjectGenerator2
    {
        public GeneratorProcedureInsertUpdate()
        {
            OptionDrop = false;
            OptionCreate = false;
            Name = "ХП создания записи";
            Memo = "Хранимая процедура создания записи";
            Template = "SET ANSI_NULLS ON \n"
                       + "GO \n"
                       + "SET QUOTED_IDENTIFIER ON \n"
                       + "GO \n"
                       + "%action% PROCEDURE %procedurename% \n"
                       + "%paramdef% \n"
                       + "AS \n"
                       + "set nocount on \n"
                       + "BEGIN TRY \n"
                       + "	declare @count INT \n"
                       + "	IF @Version IS NULL \n"
                       + "	begin \n"
                       + "	MERGE %schema%.%table% AS T  \n"
                       + "      USING (SELECT %usingselectlist%) AS S  \n"
                       + "            (%usingselectcolumnnameslist%)   \n"
                       + "      ON T.Id = S.Id  \n"
                       + "      WHEN NOT MATCHED BY TARGET  \n"
                       + "         THEN INSERT(%insertcolumnlist%)  \n"
                       + "         VALUES(%paramlist%)  \n"
                       + "      WHEN MATCHED  \n"
                       + "         THEN UPDATE SET %updatelist% \n"
                       + "      output %outputinserted%; \n"
                       + "	END \n"
                       + "	ELSE IF @Version IS NOT NULL \n"
                       + "	BEGIN \n"
                       + "	MERGE %schema%.%table% AS T  \n"
                       + "      USING (SELECT %usingselectlist%) AS S  \n"
                       + "            (%usingselectcolumnnameslist%)   \n"
                       + "      ON T.Id = S.Id  \n"
                       + "      WHEN NOT MATCHED BY TARGET  \n"
                       + "        THEN INSERT(%insertcolumnlist%)  \n"
                       + "         VALUES(%paramlist%)  \n"
                       + "      WHEN MATCHED AND T.Version=@Version \n"
                       + "         THEN UPDATE SET  %updatelist% \n"
                       + "         output %outputinserted%; \n"
                       + "	END \n"
                       + "	select @count = @@rowcount \n"
                       + "	if(@count=0) -- нет обновлённых записей? \n"
                       + "	begin \n"
                       + "	if not exists(select Id from %schema%.%table% where Id = @Id) \n"
                       + "		RAISERROR (N'Запись в таблице  с идентификатором %d отсутствует',16, 1, @Id); \n"
                       + "    if (select [Version] from %schema%.%table% where Id = @id) <> @Version \n"
                       +
                       "      RAISERROR (N'Запись в таблице с идентификатором %d была изменена, конфликт версий',16, 1, @Id); \n"
                       + "	end \n"
                       + "	RETURN 0; \n"
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
                       + "GO \n";
        }
        public Workarea Workarea { get; set; }
        public override string Generate()
        {
            string tblNameShort = TableName;
            if (tblNameShort.EndsWith("s"))
                tblNameShort = tblNameShort.Remove(tblNameShort.Length - 1);
            string procname = "[" + Schema + "].[" + tblNameShort + "InsertUpdate" + "]";
            string currentaction = (OptionCreate || OptionDrop) ? "CREATE" : "ALTER";


            string deleteProc = ("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'%procedurename%') AND type in (N'P', N'PC')) \n"
                                + "DROP PROCEDURE %procedurename% \n"
                                + "GO \n").Replace("%procedurename%", procname);
            GetData();
            GenerateCustomParamString();
            string res = Template.Replace("%action%", currentaction).Replace("%schema%", Schema).Replace("%table%", TableName).Replace("%procedurename%", procname).Replace("%paramdef%", paramdef).Replace("%tablecolumnsdef%", tablecolumnsdef).Replace("%insertcolumnlist%", insertcolumnlist).Replace("%updatelist%",updatelist).Replace("%outputinserted%", outputinserted).Replace("%paramlist%", paramlist).Replace("%selectcolumnlist%", selectcolumnlist)
                .Replace("%usingselectlist%", usingselectlist).Replace("%usingselectcolumnnameslist%", usingselectcolumnnameslist);
            if (OptionDrop)
                res = deleteProc + res;

            return res;
        }

        #region Дополнительные методы
        private string updatelist = string.Empty;
        private string paramdef = string.Empty;
        private string tablecolumnsdef = string.Empty;
        private string insertcolumnlist = string.Empty;
        private string outputinserted = string.Empty;
        private string paramlist = string.Empty;
        private string selectcolumnlist = string.Empty;
        private string usingselectlist = string.Empty;
        private string usingselectcolumnnameslist = string.Empty;
        private void GenerateCustomParamString()
        {
            updatelist = string.Empty;
            paramdef = string.Empty;
            tablecolumnsdef = string.Empty;
            insertcolumnlist = string.Empty;
            outputinserted = string.Empty;
            paramlist = string.Empty;
            selectcolumnlist = string.Empty;
            string res = string.Empty;
            foreach (DbObjectChild column in _columns.Where(s => s.IsFormula == false))
            {
                string maxLen = string.Empty;
                if (selectcolumnlist.Length > 0) selectcolumnlist += ", ";
                selectcolumnlist += column.Name;

                if (updatelist.Length > 0 && !updatelist.EndsWith(", ")) updatelist += ", ";
                if (column.Name != GlobalPropertyNames.Id && column.Name != "Guid" && column.Name != "Version")
                    updatelist += column.Name + " = @" + column.Name;

                if (outputinserted.Length > 0) outputinserted += ", ";
                outputinserted += "INSERTED." + column.Name;

                if (paramlist.Length > 0 && !paramlist.EndsWith(",")) paramlist += ",";
                if (column.Name != GlobalPropertyNames.Id && column.Name != "Guid" && column.Name != "Version")
                    paramlist += "@" + column.Name;

                if (insertcolumnlist.Length > 0 && !insertcolumnlist.EndsWith(", ")) insertcolumnlist += ", ";
                if (column.Name != GlobalPropertyNames.Id && column.Name != "Guid" && column.Name != "Version")
                    insertcolumnlist += column.Name;

                //ColumnInfo.TableColumnSystemInfo sysInfo = _columnSysinfo.First(s => s.Id == column.Id);

                if (tablecolumnsdef.Length > 0 && !tablecolumnsdef.EndsWith(", ")) tablecolumnsdef += ", ";
                string datatype = column.TypeNameSql == "timestamp" ? "binary" : column.TypeNameSql;
                tablecolumnsdef += column.Name + " " + datatype;

                if (column.TypeNameSql == "timestamp")
                {
                    tablecolumnsdef += string.Format("({0})", column.TypeLen);
                }

                if (column.TypeNameSql == "nvarchar")
                {
                    tablecolumnsdef += string.Format("({0})", column.TypeLen);
                }

                if (usingselectlist.Length > 0 && !usingselectlist.EndsWith(", ")) { usingselectlist += ", ";
                    usingselectcolumnnameslist += ", ";
                }
                switch(column.Name)
                {
                    case "Version":
                        break;
                    case "UserName":
                        usingselectlist += "isnull(@UserName,suser_sname())";
                        usingselectcolumnnameslist += "UserName";
                        break;
                    case "DateModified":
                        usingselectlist += "isnull(@DateModified, GETDATE())";
                        usingselectcolumnnameslist += "DateModified";
                        break;
                    default:
                        usingselectlist += "@"+column.Name;
                        usingselectcolumnnameslist += column.Name;
                        break;
                }
                

                if (paramdef.Length > 0 && !paramdef.EndsWith(", ")) paramdef += ", ";
                paramdef += "@" + column.Name + " " + datatype;
                if (column.TypeNameSql == "nvarchar")
                {

                    paramdef += string.Format("({0})", column.TypeLen);
                }
                if (column.AllowNull)
                {
                    paramdef += "=null";
                }
                if (column.Name == GlobalPropertyNames.Id || column.Name == "Guid" || column.Name == "Version")
                {
                    paramdef += "=null";
                }

            }
        }

        private List<DbObjectChild> _columns;
        //private List<ColumnInfo.TableColumnSystemInfo> _columnSysinfo;
        void GetData()
        {
            //_columnSysinfo = ColumnInfo.GetCollectionSystemInfo(Workarea, Schema, TableName);
            _columns = DbObjectChild.GetCollection(Workarea, Schema, TableName);
        }
        #endregion
    }
}