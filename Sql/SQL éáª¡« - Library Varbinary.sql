--sp_tableoption 'Core.Libraries', 'large value types out of row',1
select OBJECTPROPERTY(OBJECT_ID('Core.Libraries'),'TableTextInRowLimit') 
SELECT     name, large_value_types_out_of_row
FROM         sys.tables


