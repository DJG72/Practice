// Exercise 8: Implementing the Strategy Pattern
// Name: Nilanjan Pradhan
// Assignment: Cognizant Digital Nurture 5.0 - Week 1 (Design Patterns & Principles)

using System;

namespace StrategyPatternExample
{
    // Strategy Interface
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }

    // Concrete Strategy - Credit Card
    public class CreditCardPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine($"Payment of ₹{amount} made using Credit Card.");
        }
    }

    // Concrete Strategy - PayPal
    public class PayPalPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine($"Payment of ₹{amount} made using PayPal.");
        }
    }

    // Context Class
    public class PaymentContext
    {
        private IPaymentStrategy paymentStrategy;

        public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
        {
            this.paymentStrategy = paymentStrategy;
        }

        public void ExecutePayment(double amount)
        {
            if (paymentStrategy == null)
            {
                Console.WriteLine("Please select a payment method.");
                return;
            }

            paymentStrategy.Pay(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Strategy Pattern - Payment Processing System\n");

            PaymentContext paymentContext = new PaymentContext();

            Console.WriteLine("Using Credit Card Payment:");
            paymentContext.SetPaymentStrategy(new CreditCardPayment());
            paymentContext.ExecutePayment(5000);

            Console.WriteLine("\nUsing PayPal Payment:");
            paymentContext.SetPaymentStrategy(new PayPalPayment());
            paymentContext.ExecutePayment(7500);
        }
    }
}