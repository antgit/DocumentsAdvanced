using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using BusinessObjects.Developer;

namespace BusinessObjects.ProcedureGenerator
{
    internal class GeneratorProcedureFindBy : DataObjectGenerator, IDataObjectGenerator2
    {
        public GeneratorProcedureFindBy()
        {
            OptionDrop = false;
            OptionCreate = false;
            Name = "ХП загрузки записи";
            Memo = "Хранимая процедура загрузки записи по идентификатору";
            Template =   "SET ANSI_NULLS ON \n"
                       + "GO \n"
                       + "SET QUOTED_IDENTIFIER ON \n"
                       + "GO \n"
                       + " \n"
                       + "%action% PROCEDURE %procedurename% \n"
                       + "%paramdef% \n"
                       + "AS \n"
                       + "set nocount ON \n"
                       + "DECLARE @valuesId KeyListId \n"
                       + " \n"
                       + "BEGIN TRY \n"
                       + "%searchsection%"
                       + "EXEC %schema%.%table%LoadList @valuesId \n"
                       + " \n"
                       + "RETURN 0; \n"
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
            string procname = "[" + Schema + "].[" + TableName + "FindBy" + "]";
            string currentaction = (OptionCreate || OptionDrop) ? "CREATE" : "ALTER";


            string deleteProc = ("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'%procedurename%') AND type in (N'P', N'PC')) \n"
                                 + "DROP PROCEDURE %procedurename% \n"
                                 + "GO \n").Replace("%procedurename%", procname);
            GetData();
            GenerateCustomParamString();
            string res =
                Template.Replace("%action%", currentaction).Replace("%schema%", Schema).Replace("%table%", TableName).
                    Replace("%procedurename%", procname).Replace("%paramdef%", paramdef).Replace("%searchsection%",searchsection);
            if (OptionDrop)
                res = deleteProc + res;

            return res;
        }
        #region Дополнительные методы
        private string paramdef = string.Empty;
        private string searchsection = string.Empty;
        private void GenerateCustomParamString()
        {
            paramdef = string.Empty;
            searchsection = string.Empty;
            foreach (DbObjectChild column in _columns.Where(s => s.IsFormula == false))
            {
                string datatype = column.TypeNameSql == "timestamp" ? "binary" : column.TypeNameSql;
                switch(column.Name)
                {
                    case GlobalPropertyNames.Id:
                    case GlobalPropertyNames.Guid:
                    case GlobalPropertyNames.DatabaseId:
                    case GlobalPropertyNames.SourceId:
                    case GlobalPropertyNames.Version:
                        break;
                    default:
                        //paramdef
                        if (paramdef.Length > 0 && !paramdef.EndsWith(", ")) paramdef += ", \n";
                        paramdef += "@" + column.Name + " " + datatype;
                        if (column.TypeNameSql == "nvarchar")
                        {

                            paramdef += string.Format("({0})", column.TypeLen);
                        }
                        //if (column.AllowNull)
                        //{
                            paramdef += "=NULL";
                        //}
                        //%searchsection%
                        string sign;
                        switch(column.TypeNameSql.ToUpper())
                        {
                            case "NVARCHAR":
                            case "VARCHAR":
                                sign = "LIKE";
                                break;
                            case "INT":
                            case "BIT":
                            case "SMALLINT":
                            case "XML":
                            case "MONEY":
                            case "NUMERIC":
                            case "FLOAT":
                            case "DECIMAL":
                            case "IMAGE":
                            case "GEOGRAPHY":
                            case "VARBINARY":
                            case "UNIQUEIDENTIFIER":
                                sign = "=";
                                break;
                            case "DATETIME":
                            case "DATE":
                            case "TIME":
                            case "TIMESTAMP":
                                sign = ">=";
                                break;
                            default:
                                throw new System.Exception(string.Format("Не задана операция сравнения для типа данных '{0}'", column.TypeNameSql));
                        }
                        searchsection +=
                            "IF(@" + column.Name + " IS NOT NULL) \n" +
                            "INSERT @valuesId(Id) \n" +
                            "SELECT top (@Count) v.[Id] \n" +
                            "      FROM " + Schema + "." + TableName + " v LEFT JOIN @valuesId s ON v.Id=s.Id   \n" +
                            "WHERE v." + column.Name + " " + sign + " @" + column.Name + " AND s.Id IS NULL \n\n";
                        break;
                }
            }
            paramdef += ", \n@Count INT=100";
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