/* =========================================================
   SQL SERVER STORED PROCEDURE EXERCISES
   ========================================================= */


/* =========================================================
   CLEANUP SECTION
   Allows repeated execution
   ========================================================= */

IF OBJECT_ID('sp_GetEmployeesByDepartment','P') IS NOT NULL
DROP PROCEDURE sp_GetEmployeesByDepartment;
GO

IF OBJECT_ID('sp_InsertEmployee','P') IS NOT NULL
DROP PROCEDURE sp_InsertEmployee;
GO

IF OBJECT_ID('sp_GetEmployeeCountByDepartment','P') IS NOT NULL
DROP PROCEDURE sp_GetEmployeeCountByDepartment;
GO

IF OBJECT_ID('sp_GetDepartmentSalary','P') IS NOT NULL
DROP PROCEDURE sp_GetDepartmentSalary;
GO

IF OBJECT_ID('sp_UpdateEmployeeSalary','P') IS NOT NULL
DROP PROCEDURE sp_UpdateEmployeeSalary;
GO

IF OBJECT_ID('sp_GiveBonus','P') IS NOT NULL
DROP PROCEDURE sp_GiveBonus;
GO

IF OBJECT_ID('sp_UpdateSalaryTransaction','P') IS NOT NULL
DROP PROCEDURE sp_UpdateSalaryTransaction;
GO

IF OBJECT_ID('sp_GetEmployeesDynamic','P') IS NOT NULL
DROP PROCEDURE sp_GetEmployeesDynamic;
GO

IF OBJECT_ID('sp_UpdateSalaryWithErrorHandling','P') IS NOT NULL
DROP PROCEDURE sp_UpdateSalaryWithErrorHandling;
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
(2,'Finance'),
(3,'IT'),
(4,'Marketing');
GO

INSERT INTO Employees VALUES
(1,'John','Doe',1,5000.00,'2020-01-15'),
(2,'Jane','Smith',2,6000.00,'2019-03-22'),
(3,'Michael','Johnson',3,7000.00,'2018-07-30'),
(4,'Emily','Davis',4,5500.00,'2021-11-05');
GO

SELECT * FROM Departments;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 1
   CREATE STORED PROCEDURE
   ========================================================= */

CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT
        EmployeeID,
        FirstName,
        LastName
    FROM Employees
    WHERE DepartmentID=@DepartmentID;
END;
GO

EXEC sp_GetEmployeesByDepartment 1;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 2
   MODIFY STORED PROCEDURE
   ========================================================= */

ALTER PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT
        EmployeeID,
        FirstName,
        LastName,
        Salary
    FROM Employees
    WHERE DepartmentID=@DepartmentID;
END;
GO

EXEC sp_GetEmployeesByDepartment 1;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 3
   CREATE INSERT PROCEDURE
   ========================================================= */

CREATE PROCEDURE sp_InsertEmployee
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @DepartmentID INT,
    @Salary DECIMAL(10,2),
    @JoinDate DATE
AS
BEGIN
    INSERT INTO Employees
    (
        EmployeeID,
        FirstName,
        LastName,
        DepartmentID,
        Salary,
        JoinDate
    )
    VALUES
    (
        @EmployeeID,
        @FirstName,
        @LastName,
        @DepartmentID,
        @Salary,
        @JoinDate
    );
END;
GO

EXEC sp_InsertEmployee
5,'Robert','Wilson',1,6500.00,'2022-05-10';
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 4
   EXECUTE STORED PROCEDURE
   ========================================================= */

EXEC sp_GetEmployeesByDepartment 1;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 5
   RETURN EMPLOYEE COUNT
   ========================================================= */

CREATE PROCEDURE sp_GetEmployeeCountByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT
        COUNT(*) AS TotalEmployees
    FROM Employees
    WHERE DepartmentID=@DepartmentID;
END;
GO

EXEC sp_GetEmployeeCountByDepartment 1;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 6
   OUTPUT PARAMETER
   ========================================================= */

CREATE PROCEDURE sp_GetDepartmentSalary
    @DepartmentID INT,
    @TotalSalary DECIMAL(12,2) OUTPUT
AS
BEGIN
    SELECT
        @TotalSalary = SUM(Salary)
    FROM Employees
    WHERE DepartmentID=@DepartmentID;
END;
GO

DECLARE @SalaryTotal DECIMAL(12,2);

EXEC sp_GetDepartmentSalary
    1,
    @SalaryTotal OUTPUT;

SELECT @SalaryTotal AS TotalSalary;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 7
   UPDATE EMPLOYEE SALARY
   ========================================================= */

CREATE PROCEDURE sp_UpdateEmployeeSalary
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    UPDATE Employees
    SET Salary=@NewSalary
    WHERE EmployeeID=@EmployeeID;
END;
GO

EXEC sp_UpdateEmployeeSalary
1,5500.00;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 8
   CONDITIONAL LOGIC - BONUS
   ========================================================= */

CREATE PROCEDURE sp_GiveBonus
    @DepartmentID INT,
    @BonusAmount DECIMAL(10,2)
AS
BEGIN
    UPDATE Employees
    SET Salary = Salary + @BonusAmount
    WHERE DepartmentID=@DepartmentID;
END;
GO

EXEC sp_GiveBonus
1,500.00;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 9
   TRANSACTION
   ========================================================= */

CREATE PROCEDURE sp_UpdateSalaryTransaction
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    BEGIN TRANSACTION;

    UPDATE Employees
    SET Salary=@NewSalary
    WHERE EmployeeID=@EmployeeID;

    COMMIT TRANSACTION;
END;
GO

EXEC sp_UpdateSalaryTransaction
2,7000.00;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 10
   DYNAMIC SQL
   ========================================================= */

CREATE PROCEDURE sp_GetEmployeesDynamic
    @ColumnName VARCHAR(50),
    @Value VARCHAR(100)
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);

    SET @SQL =
    'SELECT * FROM Employees WHERE ' +
    @ColumnName +
    ' = ''' +
    @Value +
    '''';

    EXEC(@SQL);
END;
GO

EXEC sp_GetEmployeesDynamic
'FirstName',
'John';
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 11
   ERROR HANDLING
   ========================================================= */

CREATE PROCEDURE sp_UpdateSalaryWithErrorHandling
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    BEGIN TRY

        UPDATE Employees
        SET Salary=@NewSalary
        WHERE EmployeeID=@EmployeeID;

        PRINT 'Salary Updated Successfully';

    END TRY

    BEGIN CATCH

        PRINT 'Error Occurred While Updating Salary';

    END CATCH
END;
GO

EXEC sp_UpdateSalaryWithErrorHandling
1,8000.00;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   VERIFY STORED PROCEDURES
   ========================================================= */

SELECT
    name AS ProcedureName
FROM sys.procedures
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