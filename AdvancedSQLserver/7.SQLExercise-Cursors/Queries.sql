/* =========================================================
   SQL SERVER CURSOR EXERCISES
   ========================================================= */


/* =========================================================
   CLEANUP SECTION
   Allows repeated execution
   ========================================================= */

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

INSERT INTO Departments
VALUES
(1,'HR'),
(2,'IT'),
(3,'Finance');
GO

INSERT INTO Employees
VALUES
(1,'John','Doe',1,5000.00,'2020-01-15'),
(2,'Jane','Smith',2,6000.00,'2019-03-22'),
(3,'Bob','Johnson',3,5500.00,'2021-07-30');
GO

SELECT * FROM Departments;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 1
   CREATE A CURSOR
   ========================================================= */

DECLARE
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @DepartmentID INT,
    @Salary DECIMAL(10,2),
    @JoinDate DATE;

DECLARE EmployeeCursor CURSOR
FOR
SELECT
    EmployeeID,
    FirstName,
    LastName,
    DepartmentID,
    Salary,
    JoinDate
FROM Employees;

OPEN EmployeeCursor;

FETCH NEXT FROM EmployeeCursor
INTO
    @EmployeeID,
    @FirstName,
    @LastName,
    @DepartmentID,
    @Salary,
    @JoinDate;

WHILE @@FETCH_STATUS = 0
BEGIN

    PRINT
    'EmployeeID = ' + CAST(@EmployeeID AS VARCHAR)
    + ', Name = ' + @FirstName + ' ' + @LastName
    + ', DepartmentID = ' + CAST(@DepartmentID AS VARCHAR)
    + ', Salary = ' + CAST(@Salary AS VARCHAR);

    FETCH NEXT FROM EmployeeCursor
    INTO
        @EmployeeID,
        @FirstName,
        @LastName,
        @DepartmentID,
        @Salary,
        @JoinDate;

END;

CLOSE EmployeeCursor;
DEALLOCATE EmployeeCursor;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 2A
   STATIC CURSOR
   ========================================================= */

DECLARE
    @EmpID1 INT,
    @EmpName1 VARCHAR(50);

DECLARE StaticCursor CURSOR STATIC
FOR
SELECT EmployeeID, FirstName
FROM Employees;

OPEN StaticCursor;

FETCH NEXT FROM StaticCursor
INTO @EmpID1,@EmpName1;

WHILE @@FETCH_STATUS = 0
BEGIN

    PRINT 'STATIC CURSOR -> '
    + CAST(@EmpID1 AS VARCHAR)
    + ' '
    + @EmpName1;

    FETCH NEXT FROM StaticCursor
    INTO @EmpID1,@EmpName1;

END;

CLOSE StaticCursor;
DEALLOCATE StaticCursor;
GO


/* =========================================================
   EXERCISE 2B
   DYNAMIC CURSOR
   ========================================================= */

DECLARE
    @EmpID2 INT,
    @EmpName2 VARCHAR(50);

DECLARE DynamicCursor CURSOR DYNAMIC
FOR
SELECT EmployeeID, FirstName
FROM Employees;

OPEN DynamicCursor;

FETCH NEXT FROM DynamicCursor
INTO @EmpID2,@EmpName2;

WHILE @@FETCH_STATUS = 0
BEGIN

    PRINT 'DYNAMIC CURSOR -> '
    + CAST(@EmpID2 AS VARCHAR)
    + ' '
    + @EmpName2;

    FETCH NEXT FROM DynamicCursor
    INTO @EmpID2,@EmpName2;

END;

CLOSE DynamicCursor;
DEALLOCATE DynamicCursor;
GO


/* =========================================================
   EXERCISE 2C
   FORWARD ONLY CURSOR
   ========================================================= */

DECLARE
    @EmpID3 INT,
    @EmpName3 VARCHAR(50);

DECLARE ForwardCursor CURSOR FORWARD_ONLY
FOR
SELECT EmployeeID, FirstName
FROM Employees;

OPEN ForwardCursor;

FETCH NEXT FROM ForwardCursor
INTO @EmpID3,@EmpName3;

WHILE @@FETCH_STATUS = 0
BEGIN

    PRINT 'FORWARD ONLY CURSOR -> '
    + CAST(@EmpID3 AS VARCHAR)
    + ' '
    + @EmpName3;

    FETCH NEXT FROM ForwardCursor
    INTO @EmpID3,@EmpName3;

END;

CLOSE ForwardCursor;
DEALLOCATE ForwardCursor;
GO


/* =========================================================
   EXERCISE 2D
   KEYSET CURSOR
   ========================================================= */

DECLARE
    @EmpID4 INT,
    @EmpName4 VARCHAR(50);

DECLARE KeysetCursor CURSOR KEYSET
FOR
SELECT EmployeeID, FirstName
FROM Employees;

OPEN KeysetCursor;

FETCH NEXT FROM KeysetCursor
INTO @EmpID4,@EmpName4;

WHILE @@FETCH_STATUS = 0
BEGIN

    PRINT 'KEYSET CURSOR -> '
    + CAST(@EmpID4 AS VARCHAR)
    + ' '
    + @EmpName4;

    FETCH NEXT FROM KeysetCursor
    INTO @EmpID4,@EmpName4;

END;

CLOSE KeysetCursor;
DEALLOCATE KeysetCursor;
GO


/* =========================================================
   CURSOR COMPARISON OUTPUT
   ========================================================= */

SELECT
    'STATIC CURSOR' AS CursorType,
    'Snapshot of data' AS Behavior

UNION ALL

SELECT
    'DYNAMIC CURSOR',
    'Reflects all changes'

UNION ALL

SELECT
    'FORWARD ONLY CURSOR',
    'Fastest sequential access'

UNION ALL

SELECT
    'KEYSET CURSOR',
    'Keys fixed, values can change';
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