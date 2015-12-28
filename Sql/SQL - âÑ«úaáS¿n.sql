http://www.stevetrefethen.com/blog/UsingGoogleMapsforGeocodinginC.aspx
http://www.stevetrefethen.com/blog/UsingGoogleMapsForGeocodingInCPart2DeserializingToAClass.aspx
http://www.microsoftbible.com/tag/sqlgeometry
http://www.sqlservercentral.com/scripts/T-SQL/65739/

SELECT NAME,Location.STX,Location.STY,
'http://maps.google.com/maps?ll=' +cast(Location.STX AS VARCHAR(20)) +','+cast(Location.STY AS VARCHAR(20)) + '&z=5&t=m&hl=ru'
FROM Territory.Countries c WHERE c.Location IS NOT NULL

SELECT NAME,Location.STX,Location.STY,
'http://maps.google.com/maps?ll=' +cast(Location.STX AS VARCHAR(20)) +','+cast(Location.STY AS VARCHAR(20)) + '&z=10&t=m&hl=ru'
FROM Territory.Territories c WHERE c.Location IS NOT NULL


SELECT NAME,Location.STX,Location.STY,
'http://maps.google.com/maps?ll=' +cast(Location.STX AS VARCHAR(20)) +','+cast(Location.STY AS VARCHAR(20)) + '&z=12&t=m&hl=ru'
FROM Territory.Towns c WHERE c.Location IS NOT NULL


SELECT t.id, t3.Memo + ' ' + t2.Memo + ' район ' + 
CASE WHEN LEFT(t.memo,3) ='смт' THEN RIGHT(t.Memo, LEN(t.Memo)-4)
 WHEN LEFT(t.memo,2) ='с ' THEN RIGHT(t.Memo, LEN(t.Memo)-2)
 WHEN LEFT(t.memo,2) ='м ' THEN RIGHT(t.Memo, LEN(t.Memo)-2)
ELSE
	t.Memo END 
AS Code, 
'http://maps.google.com/maps?ll=' +cast(t.Location.STX AS VARCHAR(20)) +','+cast(t.Location.STY AS VARCHAR(20)) + '&z=12&t=m&hl=ru'
FROM Territory.Towns t
INNER JOIN Territory.TownsTerritory c ON c.RightId = t.Id
INNER JOIN Territory.Territories t2 ON t2.Id = c.LeftId 
INNER JOIN Territory.TerritoryChains tc ON t2.Id = tc.RightId
INNER JOIN Territory.Territories t3 ON tc.LeftId = t3.id
WHERE t.Flags<>1 AND t.Location IS NOT NULL 


SELECT *
FROM Territory.Towns  c
WHERE c.Location IS NULL

DECLARE @gm geometry;
set @gm = geometry::STGeomFromText('POINT(55.7557860 37.6176330 )',0)

SELECT @gm.STX, @gm.STY
--//http://maps.google.com/maps?ll=&z=10&t=m&hl=ru&z=18
SELECT * FROM Territory.Countries c WHERE c.[Name] LIKE '%Рос%'
UPDATE Territory.Countries
SET Location = @gm
WHERE Id = 217
-----------------------------------

/*
DECLARE @g geometry;
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 0);
SELECT @g.STPointN(2).ToString();
*/
/*
UPDATE [dbo].[Landmark]
SET [GeoLocation] = geography::Point([Latitude], [Longitude], 4326)
*/



DECLARE @g geography;
SET @g = geography::Parse('POINT(' + CAST('48.036917' AS VARCHAR(20)) + ' ' + 
                    CAST('37.779011' AS VARCHAR(20)) + ')')
SELECT @g.Long
SELECT @g.Lat
--47.954553, 37.774097 -мол завод
--http://maps.google.com/maps?ll=48.036917,37.779011&z=10&t=m&hl=ru&z=18
--45.5242,-122.622
--http://www.bing.com/maps/default.aspx?v=2&cp=45.5242~-122.622&lvl=1000  
http://www.bing.com/maps/default.aspx?cp=45.5242~-122.622&style=o&lvl=5&tilt=-90&dir=0&alt=-1000
--http://www.bing.com/maps/default.aspx?cp=45.5242~-122.622&alt=1000000&style=o
http://www.bing.com/maps/default.aspx?cp=48.037384~37.779043&style=o&lvl=5&tilt=-90&dir=0&alt=-1000

SELECT City, SpatialLocation.Lat,SpatialLocation.Long,
'http://maps.google.com/maps?ll=' +cast(SpatialLocation.Lat AS VARCHAR(20)) +','+cast(SpatialLocation.Long AS VARCHAR(20)) +
'&z=10&t=m&hl=ru&z=18'
FROM Person.Address
/*
DECLARE @g geography;
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);
SELECT @g.STPointN(2)
*/
