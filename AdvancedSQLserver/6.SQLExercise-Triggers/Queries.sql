/* =========================================================
   SQL SERVER TRIGGERS EXERCISES
   ========================================================= */


/* =========================================================
   CLEANUP SECTION
   ========================================================= */

IF OBJECT_ID('tr_UpdateAnnualSalary','TR') IS NOT NULL
DROP TRIGGER tr_UpdateAnnualSalary;
GO

IF OBJECT_ID('tr_PreventEmployeeDelete','TR') IS NOT NULL
DROP TRIGGER tr_PreventEmployeeDelete;
GO

IF OBJECT_ID('tr_LogSalaryChanges','TR') IS NOT NULL
DROP TRIGGER tr_LogSalaryChanges;
GO

IF OBJECT_ID('EmployeeChanges','U') IS NOT NULL
DROP TABLE EmployeeChanges;
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

INSERT INTO Departments
VALUES
(1,'HR'),
(2,'Finance'),
(3,'IT'),
(4,'Marketing');
GO

INSERT INTO Employees
VALUES
(1,'John','Doe',1,5000.00,'2022-01-15'),
(2,'Jane','Smith',2,6000.00,'2021-03-22'),
(3,'Michael','Johnson',3,7000.00,'2020-07-30'),
(4,'Emily','Davis',4,5500.00,'2019-11-05');
GO

SELECT * FROM Departments;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 1
   CREATE AFTER TRIGGER
   ========================================================= */

CREATE TABLE EmployeeChanges
(
    ChangeID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT,
    OldSalary DECIMAL(10,2),
    NewSalary DECIMAL(10,2),
    ChangeDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TRIGGER tr_LogSalaryChanges
ON Employees
AFTER UPDATE
AS
BEGIN

    INSERT INTO EmployeeChanges
    (
        EmployeeID,
        OldSalary,
        NewSalary
    )
    SELECT
        d.EmployeeID,
        d.Salary,
        i.Salary
    FROM deleted d
    JOIN inserted i
    ON d.EmployeeID=i.EmployeeID
    WHERE d.Salary<>i.Salary;

END;
GO

UPDATE Employees
SET Salary=5500
WHERE EmployeeID=1;
GO

SELECT * FROM EmployeeChanges;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 2
   CREATE INSTEAD OF DELETE TRIGGER
   ========================================================= */

CREATE TRIGGER tr_PreventEmployeeDelete
ON Employees
INSTEAD OF DELETE
AS
BEGIN

    RAISERROR
    (
        'Employee records cannot be deleted.',
        16,
        1
    );

END;
GO

BEGIN TRY

    DELETE FROM Employees
    WHERE EmployeeID=1;

END TRY

BEGIN CATCH

    PRINT ERROR_MESSAGE();

END CATCH
GO

SELECT * FROM Employees;
GO


/* =========================================================
   EXERCISE 3
   LOGON TRIGGER DEMONSTRATION
   ========================================================= */

/*
CREATE TRIGGER tr_RestrictLogin
ON ALL SERVER
FOR LOGON
AS
BEGIN

    IF DATEPART(HOUR,GETDATE()) BETWEEN 2 AND 2
    BEGIN
        ROLLBACK;
    END

END;
GO
*/

PRINT 'Logon Trigger Example Created As Demonstration Only';
GO


/* =========================================================
   EXERCISE 4
   MODIFY EXISTING TRIGGER
   ========================================================= */

ALTER TRIGGER tr_LogSalaryChanges
ON Employees
AFTER UPDATE
AS
BEGIN

    INSERT INTO EmployeeChanges
    (
        EmployeeID,
        OldSalary,
        NewSalary
    )
    SELECT
        d.EmployeeID,
        d.Salary,
        i.Salary
    FROM deleted d
    JOIN inserted i
    ON d.EmployeeID=i.EmployeeID;

END;
GO

UPDATE Employees
SET Salary=6500
WHERE EmployeeID=2;
GO

SELECT * FROM EmployeeChanges;
GO


/* =========================================================
   EXERCISE 5
   DELETE TRIGGER
   ========================================================= */

DROP TRIGGER tr_PreventEmployeeDelete;
GO

SELECT
    name AS TriggerName
FROM sys.triggers
ORDER BY name;
GO


/* =========================================================
   EXERCISE 6
   TRIGGER TO UPDATE ANNUAL SALARY
   ========================================================= */

ALTER TABLE Employees
ADD AnnualSalary DECIMAL(12,2);
GO

UPDATE Employees
SET AnnualSalary=Salary*12;
GO

CREATE TRIGGER tr_UpdateAnnualSalary
ON Employees
AFTER UPDATE
AS
BEGIN

    UPDATE Employees
    SET AnnualSalary=Salary*12
    WHERE EmployeeID IN
    (
        SELECT EmployeeID
        FROM inserted
    );

END;
GO

UPDATE Employees
SET Salary=8000
WHERE EmployeeID=3;
GO

SELECT *
FROM Employees;
GO


/* =========================================================
   VERIFY TRIGGERS
   ========================================================= */

SELECT
    name AS TriggerName
FROM sys.triggers
ORDER BY name;
GO


/* =========================================================
   FINAL OUTPUTS
   ========================================================= */

SELECT *
FROM Employees;
GO

SELECT *
FROM EmployeeChanges;
GO


/* =========================================================
   END OF SCRIPT
   ========================================================= */