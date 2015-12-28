-- http://www.sqlservercentral.com/articles/FULL+JOIN/71136/
SELECT 'Src-->Dest' AS Label, * FROM 
(
        SELECT * FROM SourceTable
        EXCEPT 
        SELECT * FROM DestinationTable
) LEFT_DIFFS
UNION
SELECT 'Dest-->Src' AS Label, * FROM 
(
        SELECT * FROM DestinationTable
        EXCEPT 
        SELECT * FROM SourceTable
) RIGHT_DIFFS