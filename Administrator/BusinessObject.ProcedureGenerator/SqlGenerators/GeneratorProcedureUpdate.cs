using System.Collections.Generic;
using BusinessObjects;
using BusinessObjects.Developer;
using Enumerable=System.Linq.Enumerable;

namespace BusinessObjects.ProcedureGenerator
{
    internal class GeneratorProcedureUpdate : DataObjectGenerator, IDataObjectGenerator2
    {
        public GeneratorProcedureUpdate()
        {
            /*
             
             */
            OptionDrop = false;
            OptionCreate = false;
            Name = "ХП обновления записи";
            Memo = "Хранимая процедура обновления записи";
            Template = "SET ANSI_NULLS ON \n"
                       + "GO \n"
                       + "SET QUOTED_IDENTIFIER ON \n"
                       + "GO \n"
                       + "%action% PROCEDURE %procedurename% \n"
                       + "%paramdef% \n"
                       + "AS \n"
                       + "SET NOCOUNT ON \n"
                       + "BEGIN TRY \n"
                       + "declare @count int \n"
                       + "declare @table table(%tablecolumnsdef%) \n"
                       + "if(@Version is null) \n"
                       + "update  %schema%.%table% set %updatelist% \n"
                       + "output %outputinserted% into @table \n"
                       + "where Id = @Id \n"
                       + "else \n"
                       + "update  %schema%.%table% set %updatelist% \n"
                       + "output %outputinserted% into @table \n"
                       + "where Id = @Id and Version=@Version \n"
                       + " \n"
                       + "select @count = @@rowcount \n"
                       + "if(@count=0) -- нет обновлённых записей? \n"
                       + "begin \n"
                       + "if not exists(select Id from %schema%.%table% where Id = @Id) \n"
                       + "   RAISERROR (N'Запись в таблице  с идентификатором %d отсутствует',16, 1, @Id); \n"
                       + "if (select [Version] from %schema%.%table% where Id = @id) <> @Version \n"
                       + "   RAISERROR (N'Запись в таблице с идентификатором %d была изменена, конфликт версий',16, 1, @Id); \n"
                       + "end \n"
                       + " \n"
                       + "select %selectcolumnlist% from @table \n"
                       + "return 0; \n"
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
                       + "GO \n"; ;

        }
        public Workarea Workarea { get; set; }
        public override string Generate()
        {
            string tblNameShort = TableName;
            if (tblNameShort.EndsWith("s"))
                tblNameShort = tblNameShort.Remove(tblNameShort.Length - 1);
            string procname = "[" + Schema + "].[" + tblNameShort + "Update" + "]";
            string currentaction = (OptionCreate || OptionDrop) ? "CREATE" : "ALTER";


            string deleteProc = ("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'%procedurename%') AND type in (N'P', N'PC')) \n"
                                + "DROP PROCEDURE %procedurename% \n"
                                + "GO \n").Replace("%procedurename%", procname);
            GetData();
            GenerateCustomParamString();
            string res = Template.Replace("%action%", currentaction).Replace("%schema%", Schema).Replace("%table%", TableName).Replace("%procedurename%", procname).Replace("%paramdef%", paramdef).Replace("%tablecolumnsdef%", tablecolumnsdef).Replace("%insertcolumnlist%", insertcolumnlist).Replace("%outputinserted%", outputinserted).Replace("%paramlist%", paramlist).Replace("%selectcolumnlist%", selectcolumnlist).Replace("%updatelist%", updatelist);
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
            foreach (DbObjectChild column in Enumerable.Where<DbObjectChild>(_columns, s => s.IsFormula == false))
            {
                string maxLen = string.Empty;
                if (updatelist.Length > 0 && !updatelist.EndsWith(", ")) updatelist += ", ";
                if (column.Name != GlobalPropertyNames.Id && column.Name != "Guid" && column.Name != "Version")
                    updatelist += column.Name + " = @" + column.Name;
                
                if (selectcolumnlist.Length > 0) selectcolumnlist += ", ";
                selectcolumnlist += column.Name;

                if (outputinserted.Length > 0) outputinserted += ", ";
                outputinserted += "INSERTED." + column.Name;

                if (paramlist.Length > 0) paramlist += ",";
                if (column.Name != GlobalPropertyNames.Id && column.Name != "Guid")
                    paramlist += "@" + column.Name;

                if (insertcolumnlist.Length > 0) insertcolumnlist += ", ";
                if (column.Name != GlobalPropertyNames.Id && column.Name != "Guid")
                    insertcolumnlist += column.Name;

                //ColumnInfo.TableColumnSystemInfo sysInfo = Enumerable.First<ColumnInfo.TableColumnSystemInfo>(_columnSysinfo, s => s.Id == column.Id);

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

                if (paramdef.Length > 0) paramdef += ", ";
                paramdef += "@" + column.Name + " " + datatype;
                if (column.TypeNameSql == "nvarchar" || column.TypeNameSql == "timestamp")
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