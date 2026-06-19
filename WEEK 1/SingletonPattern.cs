
using System;
using System.Collections.Generic;

/// <summary>
/// Singleton Task Manager Class
/// </summary>
class TaskManager
{
    private static readonly TaskManager instance = new TaskManager();
    private List<string> tasks;
    private string currentTask;

    private TaskManager()
    {
        tasks = new List<string>();
        currentTask = null;
    }

    public static TaskManager GetInstance()
    {
        return instance;
    }

    public void AddTask(string task)
    {
        tasks.Add(task);
        Console.WriteLine($"'{task}' added successfully.");
    }

    public void StartTask(string task)
    {
        if (tasks.Contains(task))
        {
            currentTask = task;
            Console.WriteLine($"Current Task: {task}");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }

    public void CompleteTask()
    {
        if (currentTask != null)
        {
            Console.WriteLine($"'{currentTask}' completed.");
            currentTask = null;
        }
        else
        {
            Console.WriteLine("No active task.");
        }
    }

    public void ShowTasks()
    {
        Console.WriteLine("\n===== TASK LIST =====");
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        foreach (string task in tasks)
            Console.WriteLine("- " + task);
    }

    public void ShowCurrentTask()
    {
        Console.WriteLine("\n===== CURRENT TASK =====");
        Console.WriteLine(currentTask ?? "No active task.");
    }
}

class SingletonPattern
{
    static void Main()
    {
        TaskManager manager1 = TaskManager.GetInstance();
        TaskManager manager2 = TaskManager.GetInstance();

        Console.WriteLine("Singleton Task Manager Demonstration");
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Same Object: {Object.ReferenceEquals(manager1, manager2)}");

        int choice;

        do
        {
            Console.WriteLine("\n===== TASK MANAGER MENU =====");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Start Task");
            Console.WriteLine("3. Complete Task");
            Console.WriteLine("4. Show Tasks");
            Console.WriteLine("5. Show Current Task");
            Console.WriteLine("6. Exit");
            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
                continue;

            switch(choice)
            {
                case 1:
                    Console.Write("Enter task: ");
                    string t = Console.ReadLine();
                    if(!string.IsNullOrWhiteSpace(t)) manager1.AddTask(t);
                    break;
                case 2:
                    Console.Write("Enter task to start: ");
                    string s = Console.ReadLine();
                    if(!string.IsNullOrWhiteSpace(s)) manager2.StartTask(s);
                    break;
                case 3:
                    manager1.CompleteTask();
                    break;
                case 4:
                    manager2.ShowTasks();
                    break;
                case 5:
                    manager1.ShowCurrentTask();
                    break;
                case 6:
                    Console.WriteLine("Exiting...");
                    break;
            }
        } while(choice != 6);
    }
}
