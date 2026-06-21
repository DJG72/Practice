/* =========================================================
   SQL SERVER LAB SCRIPT
   Topics:
   1. Ranking Functions
   2. GROUPING SETS, ROLLUP, CUBE
   3. Recursive CTE
   4. PIVOT
   5. CTE
   ========================================================= */


/* =========================================================
   STEP 1: CREATE TABLES
   ========================================================= */

-- Products Table
CREATE TABLE Products
(
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(50),
    Category VARCHAR(50),
    Price DECIMAL(10,2)
);

-- Customers Table
CREATE TABLE Customers
(
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(50),
    Region VARCHAR(50)
);

-- Orders Table
CREATE TABLE Orders
(
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,

    FOREIGN KEY(CustomerID)
    REFERENCES Customers(CustomerID)
);

-- Order Details Table
CREATE TABLE OrderDetails
(
    OrderDetailID INT PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT,

    FOREIGN KEY(OrderID)
    REFERENCES Orders(OrderID),

    FOREIGN KEY(ProductID)
    REFERENCES Products(ProductID)
);


/* =========================================================
   STEP 2: INSERT SAMPLE DATA
   ========================================================= */

-- Products
INSERT INTO Products
VALUES
(1,'Laptop','Electronics',50000),
(2,'Mobile','Electronics',30000),
(3,'Tablet','Electronics',30000),
(4,'Chair','Furniture',5000),
(5,'Desk','Furniture',8000);

-- Customers
INSERT INTO Customers
VALUES
(1,'Rahul','East'),
(2,'Amit','West'),
(3,'Priya','North');

-- Orders
INSERT INTO Orders
VALUES
(101,1,'2025-01-01'),
(102,2,'2025-01-02'),
(103,1,'2025-01-05'),
(104,3,'2025-01-08');

-- Order Details
INSERT INTO OrderDetails
VALUES
(1,101,1,2),
(2,101,4,1),
(3,102,2,1),
(4,103,5,2),
(5,104,3,4);


/* =========================================================
   EXERCISE 1
   ROW_NUMBER(), RANK(), DENSE_RANK()
   ========================================================= */

SELECT
    ProductID,
    ProductName,
    Category,
    Price,

    ROW_NUMBER() OVER
    (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS RowNumber,

    RANK() OVER
    (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS RankNumber,

    DENSE_RANK() OVER
    (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS DenseRankNumber

FROM Products;


/* =========================================================
   EXERCISE 2A
   GROUPING SETS
   ========================================================= */

SELECT
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantity

FROM Orders o
JOIN Customers c
ON o.CustomerID = c.CustomerID

JOIN OrderDetails od
ON o.OrderID = od.OrderID

JOIN Products p
ON od.ProductID = p.ProductID

GROUP BY GROUPING SETS
(
    (c.Region),
    (p.Category),
    (c.Region,p.Category)
);


/* =========================================================
   EXERCISE 2B
   ROLLUP
   ========================================================= */

SELECT
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantity

FROM Orders o
JOIN Customers c
ON o.CustomerID = c.CustomerID

JOIN OrderDetails od
ON o.OrderID = od.OrderID

JOIN Products p
ON od.ProductID = p.ProductID

GROUP BY ROLLUP
(
    c.Region,
    p.Category
);


/* =========================================================
   EXERCISE 2C
   CUBE
   ========================================================= */

SELECT
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantity

FROM Orders o
JOIN Customers c
ON o.CustomerID = c.CustomerID

JOIN OrderDetails od
ON o.OrderID = od.OrderID

JOIN Products p
ON od.ProductID = p.ProductID

GROUP BY CUBE
(
    c.Region,
    p.Category
);


/* =========================================================
   EXERCISE 3
   RECURSIVE CTE
   GENERATE CALENDAR DATES
   ========================================================= */

WITH CalendarCTE AS
(
    SELECT CAST('2025-01-01' AS DATE) AS CurrentDate

    UNION ALL

    SELECT DATEADD(DAY,1,CurrentDate)

    FROM CalendarCTE

    WHERE CurrentDate < '2025-01-31'
)

SELECT *
FROM CalendarCTE
OPTION(MAXRECURSION 31);


/* =========================================================
   EXERCISE 4
   PIVOT TABLE
   MONTH-WISE PRODUCT SALES
   ========================================================= */

SELECT *
FROM
(
    SELECT
        ProductID,
        MONTH(OrderDate) AS OrderMonth,
        Quantity

    FROM Orders o
    JOIN OrderDetails od
    ON o.OrderID = od.OrderID

) AS SourceTable

PIVOT
(
    SUM(Quantity)

    FOR OrderMonth IN
    (
        [1],[2],[3],[4],[5],[6],
        [7],[8],[9],[10],[11],[12]
    )

) AS PivotTable;


/* =========================================================
   EXERCISE 5
   COMMON TABLE EXPRESSION (CTE)
   FIND CUSTOMERS HAVING MORE THAN 1 ORDER
   ========================================================= */

WITH CustomerOrderCount AS
(
    SELECT
        CustomerID,
        COUNT(OrderID) AS TotalOrders

    FROM Orders

    GROUP BY CustomerID
)

SELECT *
FROM CustomerOrderCount

WHERE TotalOrders > 1;


/* =========================================================
   END OF SCRIPT
   ========================================================= */