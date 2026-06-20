
using System;

interface IPayment
{
    void ProcessPayment(double amount);
}

class CreditCardPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Credit Card Payment of Rs.{amount} processed successfully.");
    }
}

class UpiPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"UPI Payment of Rs.{amount} processed successfully.");
    }
}

class NetBankingPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Net Banking Payment of Rs.{amount} processed successfully.");
    }
}

class PaymentFactory
{
    public static IPayment CreatePayment(int choice)
    {
        switch (choice)
        {
            case 1: return new CreditCardPayment();
            case 2: return new UpiPayment();
            case 3: return new NetBankingPayment();
            default:
                throw new ArgumentException("Invalid payment type");
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
            Console.WriteLine("\n===== Payment Processing System =====");
            Console.WriteLine("1. Pay using Credit Card");
            Console.WriteLine("2. Pay using UPI");
            Console.WriteLine("3. Pay using Net Banking");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
                continue;

            if (choice >= 1 && choice <= 3)
            {
                Console.Write("Enter amount: ");
                double amount = Convert.ToDouble(Console.ReadLine());

                IPayment payment = PaymentFactory.CreatePayment(choice);
                payment.ProcessPayment(amount);
            }
        } while (choice != 4);
    }
}
