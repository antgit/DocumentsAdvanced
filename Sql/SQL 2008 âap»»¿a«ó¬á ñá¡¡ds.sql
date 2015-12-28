CREATE TABLE Customer
(CustomerId int,YEAR INT, Sales MONEY)

INSERT INTO Customer(CustomerId,[YEAR],Sales)
VALUES(1,2005,12000)
INSERT INTO Customer(CustomerId,[YEAR],Sales)
VALUES(1,2006,18000)
INSERT INTO Customer(CustomerId,[YEAR],Sales)
VALUES(1,2007,25000)
INSERT INTO Customer(CustomerId,[YEAR],Sales)
VALUES(2,2005,15000)
INSERT INTO Customer(CustomerId,[YEAR],Sales)
VALUES(2,2006,6000)
INSERT INTO Customer(CustomerId,[YEAR],Sales)
VALUES(3,2006,20000)
INSERT INTO Customer(CustomerId,[YEAR],Sales)
VALUES(3,2007,24000)


SELECT CustomerId, NULL AS YEAR, SUM(Sales) FROM Customer
GROUP BY CustomerId
UNION ALL
SELECT NULL as CustomerId, YEAR, SUM(Sales) FROM Customer
GROUP BY YEAR
UNION ALL
SELECT NULL AS CustomerId, NULL AS YEAR, SUM(Sales) FROM Customer

SELECT CustomerId, [YEAR], SUM(Sales) FROM Customer
GROUP BY GROUPING sets((CustomerId), (Year), () )