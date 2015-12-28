using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using BusinessObjects.Developer;

namespace BusinessObjects.ProcedureGenerator
{
    internal class GeneratorProcedureLoadAll : DataObjectGenerator, IDataObjectGenerator2
    {
        public GeneratorProcedureLoadAll()
        {
            OptionDrop = false;
            OptionCreate = false;
            Name = "ХП загрузки всех записей";
            Memo = "Хранимая процедура загрузки всех записей";
            Template = "SET ANSI_NULLS ON \n"
                       + "GO \n"
                       + "SET QUOTED_IDENTIFIER ON \n"
                       + "GO \n"
                       + "%action% PROCEDURE %procedurename% \n"
                       + "@Kind INT=null, \n"
                       + "@StateId int=null \n"
                       + "AS \n"
                       + "SET NOCOUNT ON \n"
                       + "BEGIN TRY \n"
                       + "if(@Kind is null and @StateId is null) \n"            
                       + "SELECT %selectcolumnlist% \n"
                       + "      FROM  %schema%.%table% WHERE IsTemplate = 0 \n"
                       + "else if(@Kind is null and @StateId is not null) \n"
                       + "SELECT %selectcolumnlist% \n"
                       + "      FROM  %schema%.%table% \n"
                       + "      WHERE StateId = @StateId and IsTemplate = 0\n"
                       + "else if(@Kind is not null and @StateId is null) \n"
                       + "SELECT %selectcolumnlist% \n"
                       + "      FROM  %schema%.%table%, Core.IntFlagsValue(@Kind) \n"
                       + "      WHERE (KindId & Value) = Value and IsTemplate = 0\n"
                       + "else if(@Kind is not null and @StateId is not null) \n"
                       + "SELECT %selectcolumnlist% \n"
                       + "      FROM  %schema%.%table%, Core.IntFlagsValue(@Kind) \n"
                       + "      WHERE (KindId & Value) = Value and StateId = @StateId and IsTemplate = 0 \n"
                       + " \n"
                       + "return 0 \n"
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
            //if (tblNameShort.EndsWith("s"))
            //    tblNameShort = tblNameShort.Remove(tblNameShort.Length - 1);
            string procname = "[" + Schema + "].[" + tblNameShort + "LoadAll" + "]";
            string currentaction = (OptionCreate || OptionDrop) ? "CREATE" : "ALTER";


            string deleteProc = ("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'%procedurename%') AND type in (N'P', N'PC')) \n"
                                 + "DROP PROCEDURE %procedurename% \n"
                                 + "GO \n").Replace("%procedurename%", procname);
            GetData();
            GenerateCustomParamString();
            string res = Template.Replace("%action%", currentaction).Replace("%schema%", Schema).Replace("%table%", TableName).Replace("%procedurename%", procname).Replace("%paramdef%", _paramdef).Replace("%tablecolumnsdef%", tablecolumnsdef).Replace("%insertcolumnlist%", insertcolumnlist).Replace("%outputinserted%", outputinserted).Replace("%paramlist%", paramlist).Replace("%selectcolumnlist%", selectcolumnlist).Replace("%_updatelist%", _updatelist);
            if (OptionDrop)
                res = deleteProc + res;

            return res;
        }
        #region Дополнительные методы

        private string _updatelist = string.Empty;
        private string _paramdef = string.Empty;
        private string tablecolumnsdef = string.Empty;
        private string insertcolumnlist = string.Empty;
        private string outputinserted = string.Empty;
        private string paramlist = string.Empty;
        private string selectcolumnlist = string.Empty;
        private void GenerateCustomParamString()
        {
            _updatelist = string.Empty;
            _paramdef = string.Empty;
            tablecolumnsdef = string.Empty;
            insertcolumnlist = string.Empty;
            outputinserted = string.Empty;
            paramlist = string.Empty;
            selectcolumnlist = string.Empty;
            string res = string.Empty;
            foreach (DbObjectChild column in Enumerable.Where<DbObjectChild>(_columns, s => s.IsFormula == false))
            {
                if (_updatelist.Length > 0 && !_updatelist.EndsWith(", ")) _updatelist += ", ";
                if (column.Name != GlobalPropertyNames.Id && column.Name != "Guid" && column.Name != "Version")
                    _updatelist += column.Name + " = @" + column.Name;

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

                if (tablecolumnsdef.Length > 0) tablecolumnsdef += ", ";
                tablecolumnsdef += column.Name + " " + column.TypeNameSql;
                if (column.TypeNameSql == "nvarchar")
                {
                    tablecolumnsdef += string.Format("({0})", column.TypeLen);
                }

                if (_paramdef.Length > 0) _paramdef += ", ";
                _paramdef += "@" + column.Name + " " + column.TypeNameSql;
                if (column.TypeNameSql == "nvarchar")
                {
                    _paramdef += string.Format("({0})", column.TypeLen);
                }
                if (column.AllowNull)
                {
                    _paramdef += "=null";
                }
                if (column.Name == GlobalPropertyNames.Id || column.Name == "Guid" || column.Name == "Version")
                {
                    _paramdef += "=null";
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