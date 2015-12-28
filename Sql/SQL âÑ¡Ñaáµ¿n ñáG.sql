SET LANGUAGE Russian;
INSERT INTO Report.Dates(Date, Y, M, YStr, MStr, MohthName, DayOfMonthStr,
            DayOfYearStr, DayOfWeekStr)
SELECT [date]AS [Date],
YEAR([date]) AS Y,
MONTH([date]) AS M,
DATENAME(year, [date]) AS YStr,
cast(MONTH([date]) AS NVARCHAR(2)) AS MStr,
DATENAME(month, [date]) as MohthName,
DATENAME(day, [date]) AS DayOfMonthStr,
DATENAME(dayofyear, [date]) DayOfYearStr,
DATENAME(weekday, [date]) AS DayOfWeekStr

FROM Report.[DatesTable]('20000101', '20490101')