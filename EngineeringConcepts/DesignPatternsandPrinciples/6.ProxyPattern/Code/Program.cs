// Exercise 6: Implementing the Proxy Pattern
// Name: Nilanjan Pradhan
// Assignment: Cognizant Digital Nurture 5.0 - Week 1 (Design Patterns & Principles)

using System;

namespace ProxyPatternExample
{
    // Subject Interface
    public interface IImage
    {
        void Display();
    }

    // Real Subject
    public class RealImage : IImage
    {
        private string fileName;

        public RealImage(string fileName)
        {
            this.fileName = fileName;
            LoadFromServer();
        }

        private void LoadFromServer()
        {
            Console.WriteLine($"Loading image '{fileName}' from remote server...");
        }

        public void Display()
        {
            Console.WriteLine($"Displaying image '{fileName}'");
        }
    }

    // Proxy Class
    public class ProxyImage : IImage
    {
        private RealImage realImage;
        private string fileName;

        public ProxyImage(string fileName)
        {
            this.fileName = fileName;
        }

        public void Display()
        {
            if (realImage == null)
            {
                realImage = new RealImage(fileName);
            }

            realImage.Display();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Proxy Pattern - Image Viewer Application\n");

            IImage image1 = new ProxyImage("Nature.jpg");

            Console.WriteLine("First Display Call:");
            image1.Display();

            Console.WriteLine("\nSecond Display Call:");
            image1.Display();

            Console.WriteLine("\nLoading Another Image:");

            IImage image2 = new ProxyImage("Mountain.jpg");
            image2.Display();
        }
    }
}