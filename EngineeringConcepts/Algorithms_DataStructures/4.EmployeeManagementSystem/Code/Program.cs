/*
 * Exercise 4: Employee Management System
 * Author: Nilanjan Pradhan
 * Description: Manage employee records using array operations such as add,
 * search, traverse, and delete.
 */

using System;

namespace EmployeeManagementSystem
{
    class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }

        public Employee(int employeeId, string name, string position, double salary)
        {
            EmployeeId = employeeId;
            Name = name;
            Position = position;
            Salary = salary;
        }

        public void Display()
        {
            Console.WriteLine($"{EmployeeId}\t{Name}\t{Position}\t₹{Salary}");
        }
    }

    class Program
    {
        static Employee[] employees = new Employee[10];
        static int count = 0;

        static void AddEmployee(Employee employee)
        {
            if (count < employees.Length)
            {
                employees[count] = employee;
                count++;
                Console.WriteLine("Employee added successfully.");
            }
            else
            {
                Console.WriteLine("Array is full. Cannot add more employees.");
            }
        }

        static void SearchEmployee(int employeeId)
        {
            for (int i = 0; i < count; i++)
            {
                if (employees[i].EmployeeId == employeeId)
                {
                    Console.WriteLine("\nEmployee Found:");
                    employees[i].Display();
                    return;
                }
            }

            Console.WriteLine("Employee not found.");
        }

        static void TraverseEmployees()
        {
            Console.WriteLine("\nEmployee Records");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("ID\tName\tPosition\tSalary");

            for (int i = 0; i < count; i++)
            {
                employees[i].Display();
            }
        }

        static void DeleteEmployee(int employeeId)
        {
            int index = -1;

            for (int i = 0; i < count; i++)
            {
                if (employees[i].EmployeeId == employeeId)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            for (int i = index; i < count - 1; i++)
            {
                employees[i] = employees[i + 1];
            }

            employees[count - 1] = null;
            count--;

            Console.WriteLine("Employee deleted successfully.");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== Employee Management System ===");

            AddEmployee(new Employee(101, "Amit", "Manager", 75000));
            AddEmployee(new Employee(102, "Riya", "Developer", 55000));
            AddEmployee(new Employee(103, "Sourav", "Analyst", 50000));
            AddEmployee(new Employee(104, "Priya", "Tester", 45000));

            TraverseEmployees();

            Console.WriteLine("\nSearching Employee ID 102");
            SearchEmployee(102);

            Console.WriteLine("\nDeleting Employee ID 103");
            DeleteEmployee(103);

            TraverseEmployees();
        }
    }
}