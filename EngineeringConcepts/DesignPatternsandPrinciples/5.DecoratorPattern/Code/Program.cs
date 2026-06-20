// Exercise 5: Implementing the Decorator Pattern
// Name: Nilanjan Pradhan
// Assignment: Cognizant Digital Nurture 5.0 - Week 1 (Design Patterns & Principles)

using System;

namespace DecoratorPatternExample
{
    // Component Interface
    public interface INotifier
    {
        void Send(string message);
    }

    // Concrete Component
    public class EmailNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"Email Notification: {message}");
        }
    }

    // Abstract Decorator
    public abstract class NotifierDecorator : INotifier
    {
        protected INotifier notifier;

        public NotifierDecorator(INotifier notifier)
        {
            this.notifier = notifier;
        }

        public virtual void Send(string message)
        {
            notifier.Send(message);
        }
    }

    // Concrete Decorator - SMS
    public class SMSNotifierDecorator : NotifierDecorator
    {
        public SMSNotifierDecorator(INotifier notifier)
            : base(notifier)
        {
        }

        public override void Send(string message)
        {
            base.Send(message);
            Console.WriteLine($"SMS Notification: {message}");
        }
    }

    // Concrete Decorator - Slack
    public class SlackNotifierDecorator : NotifierDecorator
    {
        public SlackNotifierDecorator(INotifier notifier)
            : base(notifier)
        {
        }

        public override void Send(string message)
        {
            base.Send(message);
            Console.WriteLine($"Slack Notification: {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Decorator Pattern - Notification System\n");

            INotifier emailNotifier = new EmailNotifier();

            Console.WriteLine("Sending Email Notification:");
            emailNotifier.Send("System Maintenance Scheduled");

            Console.WriteLine("\nSending Email + SMS Notification:");
            INotifier smsNotifier =
                new SMSNotifierDecorator(new EmailNotifier());

            smsNotifier.Send("System Maintenance Scheduled");

            Console.WriteLine("\nSending Email + SMS + Slack Notification:");
            INotifier multiChannelNotifier =
                new SlackNotifierDecorator(
                    new SMSNotifierDecorator(
                        new EmailNotifier()));

            multiChannelNotifier.Send("System Maintenance Scheduled");
        }
    }
}