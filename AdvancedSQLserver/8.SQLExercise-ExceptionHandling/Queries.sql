/* =========================================================
   SQL SERVER EXCEPTION HANDLING EXERCISES
   TRY...CATCH, THROW, RAISERROR, TRANSACTIONS
   ========================================================= */


/* =========================================================
   CLEANUP SECTION
   ========================================================= */

IF OBJECT_ID('BatchInsertEmployees','P') IS NOT NULL
DROP PROCEDURE BatchInsertEmployees;
GO

IF OBJECT_ID('TransferEmployee','P') IS NOT NULL
DROP PROCEDURE TransferEmployee;
GO

IF OBJECT_ID('AddEmployee','P') IS NOT NULL
DROP PROCEDURE AddEmployee;
GO

IF OBJECT_ID('AuditLog','U') IS NOT NULL
DROP TABLE AuditLog;
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
    DepartmentName VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Employees
(
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100) UNIQUE,
    Salary DECIMAL(10,2),
    DepartmentID INT,

    FOREIGN KEY(DepartmentID)
    REFERENCES Departments(DepartmentID)
);
GO

CREATE TABLE AuditLog
(
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    Action VARCHAR(100),
    ErrorMessage VARCHAR(4000),
    ActionDate DATETIME DEFAULT GETDATE()
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
(1,'John','Doe','john@company.com',5000,1),
(2,'Jane','Smith','jane@company.com',6000,2),
(3,'Bob','Johnson','bob@company.com',5500,3);
GO

SELECT * FROM Departments;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   QUESTION 1
   BASIC TRY...CATCH FOR ERROR LOGGING
   ========================================================= */

CREATE PROCEDURE AddEmployee
(
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
)
AS
BEGIN

    BEGIN TRY

        INSERT INTO Employees
        VALUES
        (
            @EmployeeID,
            @FirstName,
            @LastName,
            @Email,
            @Salary,
            @DepartmentID
        );

    END TRY

    BEGIN CATCH

        INSERT INTO AuditLog
        (
            Action,
            ErrorMessage
        )
        VALUES
        (
            'AddEmployee',
            ERROR_MESSAGE()
        );

    END CATCH

END;
GO

EXEC AddEmployee
4,'Alice','Brown',
'alice@company.com',
4500,
1;
GO

SELECT * FROM Employees;
GO


/************************************************************
 QUESTION 2
 THROW TO RE-RAISE ERRORS
 ************************************************************/

ALTER PROCEDURE AddEmployee
(
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
)
AS
BEGIN

    BEGIN TRY

        INSERT INTO Employees
        VALUES
        (
            @EmployeeID,
            @FirstName,
            @LastName,
            @Email,
            @Salary,
            @DepartmentID
        );

    END TRY

    BEGIN CATCH

        INSERT INTO AuditLog
        (
            Action,
            ErrorMessage
        )
        VALUES
        (
            'AddEmployee',
            ERROR_MESSAGE()
        );

        THROW;

    END CATCH

END;
GO

BEGIN TRY

    EXEC AddEmployee
    5,
    'Duplicate',
    'Email',
    'john@company.com',
    4000,
    1;

END TRY

BEGIN CATCH

    PRINT ERROR_MESSAGE();

END CATCH
GO

SELECT * FROM AuditLog;
GO


/************************************************************
 QUESTION 3
 CUSTOM ERROR USING RAISERROR
 ************************************************************/

ALTER PROCEDURE AddEmployee
(
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
)
AS
BEGIN

    IF @Salary <= 0
    BEGIN

        RAISERROR
        (
            'Salary must be greater than zero.',
            16,
            1
        );

        RETURN;

    END

    BEGIN TRY

        INSERT INTO Employees
        VALUES
        (
            @EmployeeID,
            @FirstName,
            @LastName,
            @Email,
            @Salary,
            @DepartmentID
        );

    END TRY

    BEGIN CATCH

        INSERT INTO AuditLog
        (
            Action,
            ErrorMessage
        )
        VALUES
        (
            'AddEmployee',
            ERROR_MESSAGE()
        );

        THROW;

    END CATCH

END;
GO

BEGIN TRY

EXEC AddEmployee
6,
'Invalid',
'Salary',
'invalid@company.com',
-100,
1;

END TRY

BEGIN CATCH

PRINT ERROR_MESSAGE();

END CATCH
GO


/* =========================================================
   QUESTION 4
   NESTED TRY...CATCH WITH RAISERROR
   ========================================================= */

CREATE PROCEDURE TransferEmployee
(
    @EmployeeID INT,
    @DepartmentID INT
)
AS
BEGIN

    BEGIN TRY

        BEGIN TRY

            IF NOT EXISTS
            (
                SELECT *
                FROM Departments
                WHERE DepartmentID=@DepartmentID
            )
            BEGIN

                RAISERROR
                (
                    'Department does not exist.',
                    16,
                    1
                );

            END

            UPDATE Employees
            SET DepartmentID=@DepartmentID
            WHERE EmployeeID=@EmployeeID;

        END TRY

        BEGIN CATCH

            INSERT INTO AuditLog
            (
                Action,
                ErrorMessage
            )
            VALUES
            (
                'TransferEmployee',
                ERROR_MESSAGE()
            );

            THROW;

        END CATCH

    END TRY

    BEGIN CATCH

        PRINT ERROR_MESSAGE();

    END CATCH

END;
GO

EXEC TransferEmployee
1,
99;
GO

SELECT * FROM AuditLog;
GO


/* =========================================================
   QUESTION 5
   TRANSACTION WITH ERROR LOGGING
   ========================================================= */

CREATE PROCEDURE BatchInsertEmployees
AS
BEGIN

    BEGIN TRY

        BEGIN TRANSACTION;

        INSERT INTO Employees
        VALUES
        (
            7,
            'Tom',
            'Wilson',
            'tom@company.com',
            5000,
            1
        );

        INSERT INTO Employees
        VALUES
        (
            8,
            'Jerry',
            'Thomas',
            'john@company.com',
            5500,
            2
        );

        COMMIT TRANSACTION;

    END TRY

    BEGIN CATCH

        ROLLBACK TRANSACTION;

        INSERT INTO AuditLog
        (
            Action,
            ErrorMessage
        )
        VALUES
        (
            'BatchInsertEmployees',
            ERROR_MESSAGE()
        );

    END CATCH

END;
GO

EXEC BatchInsertEmployees;
GO

SELECT * FROM Employees;
GO

SELECT * FROM AuditLog;
GO


/* =========================================================
   QUESTION 6
   DYNAMIC RAISERROR SEVERITY
   ========================================================= */

ALTER PROCEDURE AddEmployee
(
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
)
AS
BEGIN

    IF @Salary < 0
    BEGIN

        RAISERROR
        (
            'Salary cannot be negative.',
            16,
            1
        );

        RETURN;

    END

    IF @Salary < 1000
    BEGIN

        RAISERROR
        (
            'Salary is unusually low.',
            10,
            1
        );

    END

    INSERT INTO Employees
    VALUES
    (
        @EmployeeID,
        @FirstName,
        @LastName,
        @Email,
        @Salary,
        @DepartmentID
    );

END;
GO

EXEC AddEmployee
9,
'Warning',
'Case',
'warning@company.com',
500,
1;
GO

SELECT * FROM Employees;
GO


/* =========================================================
   FINAL OUTPUTS
   ========================================================= */

SELECT * FROM Departments;
GO

SELECT * FROM Employees;
GO

SELECT * FROM AuditLog;
GO


/* =========================================================
   END OF SCRIPT
   ========================================================= */