// Exercise 4: Implementing the Adapter Pattern
// Name: Nilanjan Pradhan
// Assignment: Cognizant Digital Nurture 5.0 - Week 1 (Design Patterns & Principles

using System;

namespace AdapterPatternExample
{
    // Target Interface
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }

    // Adaptee 1
    public class PayPalGateway
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine($"Processing ₹{amount} through PayPal Gateway...");
        }
    }

    // Adaptee 2
    public class StripeGateway
    {
        public void Pay(double amount)
        {
            Console.WriteLine($"Processing ₹{amount} through Stripe Gateway...");
        }
    }

    // Adaptee 3
    public class RazorpayGateway
    {
        public void SendPayment(double amount)
        {
            Console.WriteLine($"Processing ₹{amount} through Razorpay Gateway...");
        }
    }

    // Adapter for PayPal
    public class PayPalAdapter : IPaymentProcessor
    {
        private PayPalGateway paypal;

        public PayPalAdapter(PayPalGateway paypal)
        {
            this.paypal = paypal;
        }

        public void ProcessPayment(double amount)
        {
            paypal.MakePayment(amount);
        }
    }

    // Adapter for Stripe
    public class StripeAdapter : IPaymentProcessor
    {
        private StripeGateway stripe;

        public StripeAdapter(StripeGateway stripe)
        {
            this.stripe = stripe;
        }

        public void ProcessPayment(double amount)
        {
            stripe.Pay(amount);
        }
    }

    // Adapter for Razorpay
    public class RazorpayAdapter : IPaymentProcessor
    {
        private RazorpayGateway razorpay;

        public RazorpayAdapter(RazorpayGateway razorpay)
        {
            this.razorpay = razorpay;
        }

        public void ProcessPayment(double amount)
        {
            razorpay.SendPayment(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Adapter Pattern - Payment Processing System\n");

            IPaymentProcessor paypal =
                new PayPalAdapter(new PayPalGateway());

            IPaymentProcessor stripe =
                new StripeAdapter(new StripeGateway());

            IPaymentProcessor razorpay =
                new RazorpayAdapter(new RazorpayGateway());

            paypal.ProcessPayment(2500);
            stripe.ProcessPayment(5000);
            razorpay.ProcessPayment(7500);
        }
    }
}