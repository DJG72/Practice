// -----------------------------------------------------------------------------
// Developer      : Nilanjan Pradhan
// Assignment     : Cognizant Digital Nurture 5.0
// Exercise Title : E-commerce Platform Search Function
// Language       : C# (.NET 9.0 Console App)
// Description    : Implements Linear and Binary Search on a product catalog
// -----------------------------------------------------------------------------

using System;

namespace EcommerceSearchSystem
{
    // Represents a product in the e-commerce platform
    class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }

        public Product(int productId, string productName, string category)
        {
            ProductId = productId;
            ProductName = productName;
            Category = category;
        }
    }

    // Search logic for linear and binary search
    class SearchEngine
    {
        // Linear Search: O(n)
        public static int LinearSearch(Product[] products, string targetName)
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].ProductName.Equals(targetName, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        // Binary Search: O(log n)
        public static int BinarySearch(Product[] sortedProducts, string targetName)
        {
            int left = 0;
            int right = sortedProducts.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                int comparison = string.Compare(sortedProducts[mid].ProductName, targetName, StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                    return mid;
                else if (comparison < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Initialize product list
            Product[] products = new Product[]
            {
                new Product(101, "Laptop", "Electronics"),
                new Product(102, "Headphones", "Electronics"),
                new Product(103, "Shoes", "Fashion"),
                new Product(104, "Coffee Mug", "Home"),
                new Product(105, "T-shirt", "Fashion")
            };

            // Sort the array by product name (required for binary search)
            Array.Sort(products, (p1, p2) => p1.ProductName.CompareTo(p2.ProductName));

            string searchQuery = "Shoes";

            Console.WriteLine("====Performing Linear Search====");
            int indexLinear = SearchEngine.LinearSearch(products, searchQuery);
            PrintResult(indexLinear, products);

            Console.WriteLine("\n====Performing Binary Search====");
            int indexBinary = SearchEngine.BinarySearch(products, searchQuery);
            PrintResult(indexBinary, products);
        }

        // Utility to print search results
        static void PrintResult(int index, Product[] products)
        {
            if (index != -1)
            {
                Console.WriteLine($"Product found at index {index}:");
                Console.WriteLine($"ID       : {products[index].ProductId}");
                Console.WriteLine($"Name     : {products[index].ProductName}");
                Console.WriteLine($"Category : {products[index].Category}");
            }
            else
            {
                Console.WriteLine(" Product not found.");
            }
        }
    }
}