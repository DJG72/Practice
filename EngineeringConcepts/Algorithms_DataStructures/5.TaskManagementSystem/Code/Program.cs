/*
 * Exercise 5: Task Management System
 * Author: Nilanjan Pradhan
 * Description: Manage tasks using a Singly Linked List.
 */

using System;

namespace TaskManagementSystem
{
    class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }

        public Task(int taskId, string taskName, string status)
        {
            TaskId = taskId;
            TaskName = taskName;
            Status = status;
        }
    }

    class Node
    {
        public Task Data;
        public Node Next;

        public Node(Task task)
        {
            Data = task;
            Next = null;
        }
    }

    class SinglyLinkedList
    {
        Node head;

        public void AddTask(Task task)
        {
            Node newNode = new Node(task);

            if (head == null)
            {
                head = newNode;
                return;
            }

            Node temp = head;

            while (temp.Next != null)
            {
                temp = temp.Next;
            }

            temp.Next = newNode;
        }

        public void SearchTask(int taskId)
        {
            Node temp = head;

            while (temp != null)
            {
                if (temp.Data.TaskId == taskId)
                {
                    Console.WriteLine("\nTask Found:");
                    Console.WriteLine($"{temp.Data.TaskId}\t{temp.Data.TaskName}\t{temp.Data.Status}");
                    return;
                }

                temp = temp.Next;
            }

            Console.WriteLine("Task not found.");
        }

        public void TraverseTasks()
        {
            Node temp = head;

            Console.WriteLine("\nTask Records");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("ID\tTask Name\tStatus");

            while (temp != null)
            {
                Console.WriteLine($"{temp.Data.TaskId}\t{temp.Data.TaskName}\t{temp.Data.Status}");
                temp = temp.Next;
            }
        }

        public void DeleteTask(int taskId)
        {
            if (head == null)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            if (head.Data.TaskId == taskId)
            {
                head = head.Next;
                Console.WriteLine("Task deleted successfully.");
                return;
            }

            Node temp = head;

            while (temp.Next != null && temp.Next.Data.TaskId != taskId)
            {
                temp = temp.Next;
            }

            if (temp.Next == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            temp.Next = temp.Next.Next;
            Console.WriteLine("Task deleted successfully.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SinglyLinkedList taskList = new SinglyLinkedList();

            Console.WriteLine("=== Task Management System ===");

            taskList.AddTask(new Task(101, "Design UI", "Pending"));
            taskList.AddTask(new Task(102, "Develop Backend", "In Progress"));
            taskList.AddTask(new Task(103, "Testing", "Pending"));
            taskList.AddTask(new Task(104, "Deployment", "Completed"));

            taskList.TraverseTasks();

            Console.WriteLine("\nSearching Task ID 102");
            taskList.SearchTask(102);

            Console.WriteLine("\nDeleting Task ID 103");
            taskList.DeleteTask(103);

            taskList.TraverseTasks();
        }
    }
}