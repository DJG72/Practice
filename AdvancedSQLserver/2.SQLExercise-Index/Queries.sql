/* =========================================================
   SQL SERVER INDEXING EXERCISES
   ========================================================= */


/* =========================================================
   CLEANUP SECTION
   Allows repeated execution of the script
   ========================================================= */

IF EXISTS (SELECT * FROM sys.indexes WHERE name='IX_ProductName')
DROP INDEX IX_ProductName ON Products;

IF EXISTS (SELECT * FROM sys.indexes WHERE name='IX_OrderDate')
DROP INDEX IX_OrderDate ON Orders;

IF EXISTS (SELECT * FROM sys.indexes WHERE name='IX_CustomerID_OrderDate')
DROP INDEX IX_CustomerID_OrderDate ON Orders;

IF OBJECT_ID('OrderDetails','U') IS NOT NULL
DROP TABLE OrderDetails;

IF OBJECT_ID('Orders','U') IS NOT NULL
DROP TABLE Orders;

IF OBJECT_ID('Products','U') IS NOT NULL
DROP TABLE Products;

IF OBJECT_ID('Customers','U') IS NOT NULL
DROP TABLE Customers;


/* =========================================================
   DATABASE SCHEMA
   ========================================================= */

CREATE TABLE Customers
(
    CustomerID INT PRIMARY KEY,
    Name VARCHAR(100),
    Region VARCHAR(50)
);

CREATE TABLE Products
(
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10,2)
);

CREATE TABLE Orders
(
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,

    FOREIGN KEY(CustomerID)
    REFERENCES Customers(CustomerID)
);

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
   SAMPLE DATA
   ========================================================= */

INSERT INTO Customers VALUES
(1,'Alice','North'),
(2,'Bob','South'),
(3,'Charlie','East'),
(4,'David','West');

INSERT INTO Products VALUES
(1,'Laptop','Electronics',1200.00),
(2,'Smartphone','Electronics',800.00),
(3,'Tablet','Electronics',600.00),
(4,'Headphones','Accessories',150.00);

INSERT INTO Orders VALUES
(1,1,'2023-01-15'),
(2,2,'2023-02-20'),
(3,3,'2023-03-25'),
(4,4,'2023-04-30');

INSERT INTO OrderDetails VALUES
(1,1,1,1),
(2,2,2,2),
(3,3,3,1),
(4,4,4,3);


/* =========================================================
   EXERCISE 1
   NON-CLUSTERED INDEX
   ========================================================= */

-- Query before index creation

SELECT *
FROM Products
WHERE ProductName='Laptop';

-- Create Non-Clustered Index

CREATE NONCLUSTERED INDEX IX_ProductName
ON Products(ProductName);

-- Query after index creation

SELECT *
FROM Products
WHERE ProductName='Laptop';


/* =========================================================
   EXERCISE 2
   INDEX ON ORDERDATE
   NOTE:
   OrderID Primary Key already has a clustered index.
   Therefore a Non-Clustered Index is created on OrderDate.
   ========================================================= */

-- Query before index creation

SELECT *
FROM Orders
WHERE OrderDate='2023-01-15';

-- Create Non-Clustered Index

CREATE NONCLUSTERED INDEX IX_OrderDate
ON Orders(OrderDate);

-- Query after index creation

SELECT *
FROM Orders
WHERE OrderDate='2023-01-15';


/* =========================================================
   EXERCISE 3
   COMPOSITE INDEX
   ========================================================= */

-- Query before index creation

SELECT *
FROM Orders
WHERE CustomerID=1
AND OrderDate='2023-01-15';

-- Create Composite Index

CREATE NONCLUSTERED INDEX IX_CustomerID_OrderDate
ON Orders(CustomerID,OrderDate);

-- Query after index creation

SELECT *
FROM Orders
WHERE CustomerID=1
AND OrderDate='2023-01-15';


/* =========================================================
   VERIFY CREATED INDEXES
   ========================================================= */

SELECT
    name AS IndexName,
    type_desc AS IndexType
FROM sys.indexes
WHERE object_id = OBJECT_ID('Products');

SELECT
    name AS IndexName,
    type_desc AS IndexType
FROM sys.indexes
WHERE object_id = OBJECT_ID('Orders');


/* =========================================================
   END OF SCRIPT
   ========================================================= */