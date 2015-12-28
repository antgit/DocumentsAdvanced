With Dups as 
(
    select *, row_number() over (partition by Product_Code order by Product_Code) as RowNum 
    from #prod
)


Delete from Dups where rownum > 1;

--Note duplicate record 345 Product_3 has been removed.
SELECT *
FROM #prod;