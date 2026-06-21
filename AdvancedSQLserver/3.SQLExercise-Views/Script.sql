/* =========================================================
   SQL SERVER VIEWS EXERCISES
   ========================================================= */


/* =========================================================
   CLEANUP SECTION
   Allows repeated execution
   ========================================================= */

IF OBJECT_ID('vw_EmployeeReport','V') IS NOT NULL
DROP VIEW vw_EmployeeReport;
GO

IF OBJECT_ID('vw_EmployeeAnnualSalary','V') IS NOT NULL
DROP VIEW vw_EmployeeAnnualSalary;
GO

IF OBJECT_ID('vw_EmployeeFullName','V') IS NOT NULL
DROP VIEW vw_EmployeeFullName;
GO

IF OBJECT_ID('vw_EmployeeBasicInfo','V') IS NOT NULL
DROP VIEW vw_EmployeeBasicInfo;
GO

IF OBJECT_ID('Employees','U') IS NOT NULL
DROP TABLE Employees;
GO

IF OBJECT_ID('Departments','U') IS NOT NULL
DROP TABLE Departments;
GO


/* =========================================================
   DATABASE SCHEMA
   ========================================================= */

CREATE TABLE Departments
(
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100)
);
GO

CREATE TABLE Employees
(
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DepartmentID INT,
    Salary DECIMAL(10,2),
    JoinDate DATE,

    FOREIGN KEY (DepartmentID)
    REFERENCES Departments(DepartmentID)
);
GO


/* =========================================================
   SAMPLE DATA
   ========================================================= */

INSERT INTO Departments VALUES
(1,'Human Resources'),
(2,'Finance'),
(3,'Information Technology'),
(4,'Marketing');
GO

INSERT INTO Employees VALUES
(101,'Alice','Johnson',1,45000,'2021-01-15'),
(102,'Bob','Smith',2,55000,'2020-05-10'),
(103,'Charlie','Brown',3,65000,'2019-03-20'),
(104,'David','Wilson',4,50000,'2022-07-01');
GO


/* =========================================================
   EXERCISE 1
   CREATE SIMPLE VIEW
   ========================================================= */

CREATE VIEW vw_EmployeeBasicInfo
AS
SELECT
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    d.DepartmentName
FROM Employees e
JOIN Departments d
ON e.DepartmentID = d.DepartmentID;
GO

SELECT *
FROM vw_EmployeeBasicInfo;
GO


/* =========================================================
   EXERCISE 2
   COMPUTED COLUMN - FULL NAME
   ========================================================= */

CREATE VIEW vw_EmployeeFullName
AS
SELECT
    EmployeeID,
    FirstName,
    LastName,
    FirstName + ' ' + LastName AS FullName
FROM Employees;
GO

SELECT *
FROM vw_EmployeeFullName;
GO


/* =========================================================
   EXERCISE 3
   COMPUTED COLUMN - ANNUAL SALARY
   ========================================================= */

CREATE VIEW vw_EmployeeAnnualSalary
AS
SELECT
    EmployeeID,
    FirstName,
    LastName,
    Salary,
    Salary * 12 AS AnnualSalary
FROM Employees;
GO

SELECT *
FROM vw_EmployeeAnnualSalary;
GO


/* =========================================================
   EXERCISE 4
   MULTIPLE COMPUTED COLUMNS
   ========================================================= */

CREATE VIEW vw_EmployeeReport
AS
SELECT
    e.EmployeeID,
    e.FirstName + ' ' + e.LastName AS FullName,
    d.DepartmentName,
    e.Salary * 12 AS AnnualSalary,
    (e.Salary * 12) * 0.10 AS Bonus
FROM Employees e
JOIN Departments d
ON e.DepartmentID = d.DepartmentID;
GO

SELECT *
FROM vw_EmployeeReport;
GO


/* =========================================================
   VERIFY CREATED VIEWS
   ========================================================= */

SELECT
    TABLE_NAME
FROM INFORMATION_SCHEMA.VIEWS;
GO


/* =========================================================
   END OF SCRIPT
   ========================================================= */