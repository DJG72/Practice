/*
 * Exercise 6: Library Management System
 * Author: Nilanjan Pradhan
 * Description: Search books by title using Linear Search and Binary Search.
 */

using System;

namespace LibraryManagementSystem
{
    class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public Book(int bookId, string title, string author)
        {
            BookId = bookId;
            Title = title;
            Author = author;
        }

        public void Display()
        {
            Console.WriteLine($"{BookId}\t{Title}\t{Author}");
        }
    }

    class Program
    {
        static int LinearSearch(Book[] books, string title)
        {
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

        static int BinarySearch(Book[] books, string title)
        {
            int low = 0;
            int high = books.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;

                int result = string.Compare(
                    books[mid].Title,
                    title,
                    StringComparison.OrdinalIgnoreCase
                );

                if (result == 0)
                {
                    return mid;
                }
                else if (result < 0)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return -1;
        }

        static void Main(string[] args)
        {
            Book[] books =
            {
                new Book(101,"Algorithms","Thomas Cormen"),
                new Book(102,"Computer Networks","Andrew Tanenbaum"),
                new Book(103,"Data Structures","Seymour Lipschutz"),
                new Book(104,"Database Systems","Elmasri"),
                new Book(105,"Operating Systems","Galvin")
            };

            Console.WriteLine("=== Library Management System ===");

            Console.WriteLine("\nAvailable Books");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("ID\tTitle\t\t\tAuthor");

            foreach (Book book in books)
            {
                book.Display();
            }

            string searchTitle = "Data Structures";

            Console.WriteLine($"\nSearching using Linear Search: {searchTitle}");

            int linearResult = LinearSearch(books, searchTitle);

            if (linearResult != -1)
            {
                books[linearResult].Display();
            }
            else
            {
                Console.WriteLine("Book not found.");
            }

            Console.WriteLine($"\nSearching using Binary Search: {searchTitle}");

            int binaryResult = BinarySearch(books, searchTitle);

            if (binaryResult != -1)
            {
                books[binaryResult].Display();
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
    }
}