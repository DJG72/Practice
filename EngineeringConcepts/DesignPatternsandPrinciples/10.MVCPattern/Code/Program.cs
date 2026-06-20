// Exercise 10: Implementing the MVC Pattern
// Name: Nilanjan Pradhan
// Assignment: Cognizant Digital Nurture 5.0 - Week 1 (Design Patterns & Principles)

using System;

namespace MVCPatternExample
{
    // Model
    public class Student
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Grade { get; set; }

        public Student(string name, int id, string grade)
        {
            Name = name;
            Id = id;
            Grade = grade;
        }
    }

    // View
    public class StudentView
    {
        public void DisplayStudentDetails(string name, int id, string grade)
        {
            Console.WriteLine("Student Details");
            Console.WriteLine("----------------");
            Console.WriteLine($"Name  : {name}");
            Console.WriteLine($"ID    : {id}");
            Console.WriteLine($"Grade : {grade}");
        }
    }

    // Controller
    public class StudentController
    {
        private Student model;
        private StudentView view;

        public StudentController(Student model, StudentView view)
        {
            this.model = model;
            this.view = view;
        }

        public void SetStudentName(string name)
        {
            model.Name = name;
        }

        public string GetStudentName()
        {
            return model.Name;
        }

        public void SetStudentId(int id)
        {
            model.Id = id;
        }

        public int GetStudentId()
        {
            return model.Id;
        }

        public void SetStudentGrade(string grade)
        {
            model.Grade = grade;
        }

        public string GetStudentGrade()
        {
            return model.Grade;
        }

        public void UpdateView()
        {
            view.DisplayStudentDetails(
                model.Name,
                model.Id,
                model.Grade
            );
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MVC Pattern - Student Management System\n");

            Student student = new Student(
                "Rahul Sharma",
                101,
                "A"
            );

            StudentView view = new StudentView();

            StudentController controller =
                new StudentController(student, view);

            Console.WriteLine("Initial Student Record:");
            controller.UpdateView();

            Console.WriteLine("\nUpdating Student Details...\n");

            controller.SetStudentName("Priya Das");
            controller.SetStudentId(102);
            controller.SetStudentGrade("A+");

            Console.WriteLine("Updated Student Record:");
            controller.UpdateView();
        }
    }
}