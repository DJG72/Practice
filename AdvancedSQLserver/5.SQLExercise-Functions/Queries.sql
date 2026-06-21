/* =========================================================
   SQL SERVER FUNCTIONS EXERCISES
   ========================================================= */


/* =========================================================
   CLEANUP SECTION
   Allows repeated execution
   ========================================================= */

IF OBJECT_ID('fn_CalculateTotalCompensation','FN') IS NOT NULL
DROP FUNCTION fn_CalculateTotalCompensation;
GO

IF OBJECT_ID('fn_CalculateBonus','FN') IS NOT NULL
DROP FUNCTION fn_CalculateBonus;
GO

IF OBJECT_ID('fn_GetEmployeesByDepartment','IF') IS NOT NULL
DROP FUNCTION fn_GetEmployeesByDepartment;
GO

IF OBJECT_ID('fn_CalculateAnnualSalary','FN') IS NOT NULL
DROP FUNCTION fn_CalculateAnnualSalary;
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

    FOREIGN KEY(DepartmentID)
    REFERENCES Departments(DepartmentID)
);
GO


/* =========================================================
   SAMPLE DATA
   ========================================================= */

INSERT INTO Departments VALUES
(1,'HR'),
(2,'IT'),
(3,'Finance');
GO

INSERT INTO Employees VALUES
(1,'John','Doe',1,5000.00,'2020-01-15'),
(2,'Jane','Smith',2,6000.00,'2019-03-22'),
(3,'Bob','Johnson',3,5500.00,'2021-07-01');
GO

SELECT * FROM Departments;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 1
   SCALAR FUNCTION
   ========================================================= */

CREATE FUNCTION fn_CalculateAnnualSalary
(
    @Salary DECIMAL(10,2)
)
RETURNS DECIMAL(12,2)
AS
BEGIN

    RETURN @Salary * 12;

END;
GO

SELECT
    EmployeeID,
    FirstName,
    Salary,
    dbo.fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees;
GO


/* =========================================================
   EXERCISE 2
   TABLE VALUED FUNCTION
   ========================================================= */

CREATE FUNCTION fn_GetEmployeesByDepartment
(
    @DepartmentID INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Employees
    WHERE DepartmentID=@DepartmentID
);
GO

SELECT *
FROM dbo.fn_GetEmployeesByDepartment(2);
GO


/* =========================================================
   EXERCISE 3
   USER DEFINED FUNCTION
   ========================================================= */

CREATE FUNCTION fn_CalculateBonus
(
    @Salary DECIMAL(10,2)
)
RETURNS DECIMAL(12,2)
AS
BEGIN

    RETURN @Salary * 0.10;

END;
GO

SELECT
    EmployeeID,
    FirstName,
    Salary,
    dbo.fn_CalculateBonus(Salary) AS Bonus
FROM Employees;
GO


/* =========================================================
   EXERCISE 4
   MODIFY FUNCTION
   ========================================================= */

ALTER FUNCTION fn_CalculateBonus
(
    @Salary DECIMAL(10,2)
)
RETURNS DECIMAL(12,2)
AS
BEGIN

    RETURN @Salary * 0.15;

END;
GO

SELECT
    EmployeeID,
    FirstName,
    Salary,
    dbo.fn_CalculateBonus(Salary) AS ModifiedBonus
FROM Employees;
GO


/* =========================================================
   EXERCISE 5
   DELETE FUNCTION
   ========================================================= */

DROP FUNCTION fn_CalculateBonus;
GO

SELECT
    name
FROM sys.objects
WHERE type='FN';
GO


/* =========================================================
   RECREATE BONUS FUNCTION
   Required for later exercises
   ========================================================= */

CREATE FUNCTION fn_CalculateBonus
(
    @Salary DECIMAL(10,2)
)
RETURNS DECIMAL(12,2)
AS
BEGIN

    RETURN @Salary * 0.15;

END;
GO


/* =========================================================
   EXERCISE 6
   EXECUTE SCALAR FUNCTION
   ========================================================= */

SELECT
    EmployeeID,
    FirstName,
    dbo.fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees;
GO


/* =========================================================
   EXERCISE 7
   RETURN DATA FROM SCALAR FUNCTION
   ========================================================= */

SELECT
    EmployeeID,
    FirstName,
    dbo.fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees
WHERE EmployeeID=1;
GO


/* =========================================================
   EXERCISE 8
   RETURN DATA FROM TABLE FUNCTION
   ========================================================= */

SELECT *
FROM dbo.fn_GetEmployeesByDepartment(3);
GO


/* =========================================================
   EXERCISE 9
   NESTED USER DEFINED FUNCTION
   ========================================================= */

CREATE FUNCTION fn_CalculateTotalCompensation
(
    @Salary DECIMAL(10,2)
)
RETURNS DECIMAL(12,2)
AS
BEGIN

    RETURN
    dbo.fn_CalculateAnnualSalary(@Salary)
    +
    dbo.fn_CalculateBonus(@Salary);

END;
GO

SELECT
    EmployeeID,
    FirstName,
    Salary,
    dbo.fn_CalculateTotalCompensation(Salary)
    AS TotalCompensation
FROM Employees;
GO


/* =========================================================
   EXERCISE 10
   MODIFY NESTED FUNCTION
   ========================================================= */

ALTER FUNCTION fn_CalculateTotalCompensation
(
    @Salary DECIMAL(10,2)
)
RETURNS DECIMAL(12,2)
AS
BEGIN

    RETURN
    dbo.fn_CalculateAnnualSalary(@Salary)
    +
    (dbo.fn_CalculateBonus(@Salary) * 2);

END;
GO

SELECT
    EmployeeID,
    FirstName,
    Salary,
    dbo.fn_CalculateTotalCompensation(Salary)
    AS ModifiedTotalCompensation
FROM Employees;
GO


/* =========================================================
   VERIFY FUNCTIONS
   ========================================================= */

SELECT
    name AS FunctionName
FROM sys.objects
WHERE type IN ('FN','IF','TF')
ORDER BY name;
GO


/* =========================================================
   FINAL EMPLOYEE TABLE
   ========================================================= */

SELECT *
FROM Employees;
GO


/* =========================================================
   END OF SCRIPT
   ========================================================= */