using System.Collections.Generic;
using BusinessObjects;
using System.Linq;
using BusinessObjects.Developer;

namespace BusinessObjects.ProcedureGenerator
{
    internal class GeneratorProcedureInsert : DataObjectGenerator, IDataObjectGenerator2
    {
        public GeneratorProcedureInsert()
        {
            OptionDrop = false;
            OptionCreate = false;
            Name = "�� �������� ������";
            Memo = "�������� ��������� �������� ������";
            Template = "SET ANSI_NULLS ON \n"
                       + "GO \n"
                       + "SET QUOTED_IDENTIFIER ON \n"
                       + "GO \n"
                       + "%action% PROCEDURE %procedurename% \n"
                       + "%paramdef% \n"
                       + "AS \n"
                       +"SET NOCOUNT ON \n"
                       + "BEGIN TRY \n"
                       + "declare @table table(%tablecolumnsdef%) \n"
                       + "insert %schema%.%table%(%insertcolumnlist%)  \n"
                       + "output %outputinserted% into @table \n"
                       + "values(%paramlist%) \n"
                       + "select %selectcolumnlist% from @table \n"
                       + "END TRY \n"
                       + "BEGIN CATCH \n"
                       + " -- ����� � ������� ���������� � ������� ErrorLog \n"
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
            string procname = "[" + Schema + "].[" + tblNameShort + "Insert" + "]";
            string currentaction = (OptionCreate || OptionDrop) ? "CREATE" : "ALTER";


            string deleteProc = ("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'%procedurename%') AND type in (N'P', N'PC')) \n"
                                + "DROP PROCEDURE %procedurename% \n"
                                + "GO \n").Replace("%procedurename%", procname);
            GetData();
            GenerateCustomParamString();
            string res = Template.Replace("%action%", currentaction).Replace("%schema%", Schema).Replace("%table%", TableName).Replace("%procedurename%", procname).Replace("%paramdef%", paramdef).Replace("%tablecolumnsdef%", tablecolumnsdef).Replace("%insertcolumnlist%", insertcolumnlist).Replace("%outputinserted%", outputinserted).Replace("%paramlist%", paramlist).Replace("%selectcolumnlist%", selectcolumnlist);
            if (OptionDrop)
                res = deleteProc + res;

            return res;
        }

        #region �������������� ������
        private string paramdef= string.Empty;
        private string tablecolumnsdef = string.Empty;
        private string insertcolumnlist = string.Empty;
        private string outputinserted = string.Empty;
        private string paramlist = string.Empty;
        private string selectcolumnlist = string.Empty;
        private void GenerateCustomParamString()
        {
            paramdef= string.Empty;
            tablecolumnsdef = string.Empty;
            insertcolumnlist = string.Empty;
            outputinserted = string.Empty;
            paramlist = string.Empty;
            selectcolumnlist = string.Empty;
            string res = string.Empty;
            foreach (DbObjectChild column in _columns.Where(s=>s.IsFormula==false))
            {
                string maxLen = string.Empty;
                if (selectcolumnlist.Length > 0) selectcolumnlist += ", ";
                selectcolumnlist += column.Name;

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
                    tablecolumnsdef += string.Format("({0})",column.TypeLen);
                }

                if (column.TypeNameSql == "nvarchar")
                {
                    tablecolumnsdef += string.Format("({0})", column.TypeLen);
                }
            
                if (column.Name != "Version")
                {
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