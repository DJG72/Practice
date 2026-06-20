// Exercise 7: Implementing the Observer Pattern
// Name: Nilanjan Pradhan
// Assignment: Cognizant Digital Nurture 5.0 - Week 1 (Design Patterns & Principles)

using System;
using System.Collections.Generic;

namespace ObserverPatternExample
{
    // Observer Interface
    public interface IObserver
    {
        void Update(string stockName, double stockPrice);
    }

    // Subject Interface
    public interface IStock
    {
        void RegisterObserver(IObserver observer);
        void DeregisterObserver(IObserver observer);
        void NotifyObservers();
    }

    // Concrete Subject
    public class StockMarket : IStock
    {
        private List<IObserver> observers = new List<IObserver>();

        private string stockName;
        private double stockPrice;

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void DeregisterObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update(stockName, stockPrice);
            }
        }

        public void SetStock(string stockName, double stockPrice)
        {
            this.stockName = stockName;
            this.stockPrice = stockPrice;

            Console.WriteLine($"\nStock Updated: {stockName} = ₹{stockPrice}");

            NotifyObservers();
        }
    }

    // Concrete Observer - Mobile App
    public class MobileApp : IObserver
    {
        private string userName;

        public MobileApp(string userName)
        {
            this.userName = userName;
        }

        public void Update(string stockName, double stockPrice)
        {
            Console.WriteLine($"Mobile App [{userName}] Notification: {stockName} price changed to ₹{stockPrice}");
        }
    }

    // Concrete Observer - Web App
    public class WebApp : IObserver
    {
        private string userName;

        public WebApp(string userName)
        {
            this.userName = userName;
        }

        public void Update(string stockName, double stockPrice)
        {
            Console.WriteLine($"Web App [{userName}] Notification: {stockName} price changed to ₹{stockPrice}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Observer Pattern - Stock Market Monitoring System");

            StockMarket stockMarket = new StockMarket();

            IObserver mobileUser = new MobileApp("Rahul");
            IObserver webUser = new WebApp("Priya");

            stockMarket.RegisterObserver(mobileUser);
            stockMarket.RegisterObserver(webUser);

            stockMarket.SetStock("TCS", 3850.50);

            stockMarket.SetStock("Infosys", 1625.75);

            Console.WriteLine("\nRemoving Web App Observer...\n");

            stockMarket.DeregisterObserver(webUser);

            stockMarket.SetStock("Wipro", 540.25);
        }
    }
}