SET LANGUAGE Russian
DECLARE @ds DATE = '20050101'
DECLARE @de DATE = '20150101'

WHILE @ds<=@de
BEGIN
INSERT INTO Report.DimTime(Date, DayNumberOfWeek, DayNameOfWeek, DayNumberOfMonth,
            DayNumberOfYear, WeekNumberOfYear, MonthName, MonthNumberOfYear,
            CalendarQuarter, CalendarYear, CalendarSemester)
SELECT @ds AS Date,
DATEPART(weekday, @ds) AS DayNumberOfWeek,
DATENAME(Weekday, @ds) AS DayNameOfWeek,
DATEPART(weekday, @ds) AS DayNumberOfWeek,
DATEPART(dayofyear, @ds) AS DayNumberOfYear,
DATEPART(week , @ds) AS WeekNumberOfYear,
DATENAME(MONTH, @ds) AS MonthName,
DATEPART(MONTH, @ds) AS MonthNumberOfYear, 
DATEPART(quarter, @ds) AS CalendarQuarter,
DATEPART(YEAR, @ds) AS CalendarYear,
CASE WHEN DATEPART(MONTH, @ds)<6 AND DATEPART(weekday, @ds)<=31 THEN 1
     ELSE 2 END AS CalendarSemester;

SELECT @ds = DATEADD(DAY,1,@ds);
end
SELECT * FROM Report.DimTime

SELECT DISTINCT Date FROM Report.DimTime

--TRUNCATE TABLE Report.DimTime