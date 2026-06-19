using System;

/// <summary>
/// Product Interface
/// All document types must implement this.
/// </summary>
interface IDocument
{
    void GenerateDocument(string title);
}

/// <summary>
/// Concrete Product - PDF Document
/// </summary>
class PDFDocument : IDocument
{
    public void GenerateDocument(string title)
    {
        Console.WriteLine($"PDF Document '{title}' generated successfully.");
    }
}

/// <summary>
/// Concrete Product - Word Document
/// </summary>
class WordDocument : IDocument
{
    public void GenerateDocument(string title)
    {
        Console.WriteLine($"Word Document '{title}' generated successfully.");
    }
}

/// <summary>
/// Concrete Product - Excel Document
/// </summary>
class ExcelDocument : IDocument
{
    public void GenerateDocument(string title)
    {
        Console.WriteLine($"Excel Document '{title}' generated successfully.");
    }
}

/// <summary>
/// Factory Class
/// Responsible for creating document objects.
/// </summary>
class DocumentFactory
{
    public static IDocument CreateDocument(int choice)
    {
        switch (choice)
        {
            case 1:
                return new PDFDocument();

            case 2:
                return new WordDocument();

            case 3:
                return new ExcelDocument();

            default:
                return null;
        }
    }
}

class FactoryMethodPattern
{
    static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== Document Generator =====");
            Console.WriteLine("1. Generate PDF");
            Console.WriteLine("2. Generate Word Document");
            Console.WriteLine("3. Generate Excel Sheet");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            if (choice >= 1 && choice <= 3)
            {
                Console.Write("Enter document title: ");
                string title = Console.ReadLine();

                IDocument document =
                    DocumentFactory.CreateDocument(choice);

                document.GenerateDocument(title);
            }
            else if (choice == 4)
            {
                Console.WriteLine("Exiting Document Generator...");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

        } while (choice != 4);
    }
}