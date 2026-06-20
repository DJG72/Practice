// Exercise 2: Factory Method Pattern
// Name: Nilanjan Pradhan
// Assignment: Cognizant Digital Nurture 5.0 - Week 1 (Design Patterns & Principles)

using System;

namespace FactoryMethodPattern
{
    // Product Interface
    public interface IDocument
    {
        void Open();
    }

    // Concrete Products
    public class WordDocument : IDocument
    {
        public void Open() => Console.WriteLine("Opening a Word document...");
    }

    public class PdfDocument : IDocument
    {
        public void Open() => Console.WriteLine("Opening a PDF document...");
    }

    public class ExcelDocument : IDocument
    {
        public void Open() => Console.WriteLine("Opening an Excel document...");
    }

    // Abstract Factory
    public abstract class DocumentFactory
    {
        public abstract IDocument CreateDocument();
    }

    // Concrete Factories
    public class WordDocumentFactory : DocumentFactory
    {
        public override IDocument CreateDocument() => new WordDocument();
    }

    public class PdfDocumentFactory : DocumentFactory
    {
        public override IDocument CreateDocument() => new PdfDocument();
    }

    public class ExcelDocumentFactory : DocumentFactory
    {
        public override IDocument CreateDocument() => new ExcelDocument();
    }

    // Client code
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Factory Method Pattern - Document Management System\n");

            DocumentFactory factory;

            factory = new WordDocumentFactory();
            IDocument wordDoc = factory.CreateDocument();
            wordDoc.Open();

            factory = new PdfDocumentFactory();
            IDocument pdfDoc = factory.CreateDocument();
            pdfDoc.Open();

            factory = new ExcelDocumentFactory();
            IDocument excelDoc = factory.CreateDocument();
            excelDoc.Open();
        }
    }
}