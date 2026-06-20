// Exercise 9: Implementing the Command Pattern
// Name: Nilanjan Pradhan
// Assignment: Cognizant Digital Nurture 5.0 - Week 1 (Design Patterns & Principles)

using System;

namespace CommandPatternExample
{
    // Command Interface
    public interface ICommand
    {
        void Execute();
    }

    // Receiver Class
    public class Light
    {
        public void TurnOn()
        {
            Console.WriteLine("Light is ON");
        }

        public void TurnOff()
        {
            Console.WriteLine("Light is OFF");
        }
    }

    // Concrete Command - Turn On
    public class LightOnCommand : ICommand
    {
        private Light light;

        public LightOnCommand(Light light)
        {
            this.light = light;
        }

        public void Execute()
        {
            light.TurnOn();
        }
    }

    // Concrete Command - Turn Off
    public class LightOffCommand : ICommand
    {
        private Light light;

        public LightOffCommand(Light light)
        {
            this.light = light;
        }

        public void Execute()
        {
            light.TurnOff();
        }
    }

    // Invoker Class
    public class RemoteControl
    {
        private ICommand command;

        public void SetCommand(ICommand command)
        {
            this.command = command;
        }

        public void PressButton()
        {
            command.Execute();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Command Pattern - Home Automation System\n");

            Light livingRoomLight = new Light();

            ICommand lightOn = new LightOnCommand(livingRoomLight);
            ICommand lightOff = new LightOffCommand(livingRoomLight);

            RemoteControl remote = new RemoteControl();

            Console.WriteLine("Turning Light ON:");
            remote.SetCommand(lightOn);
            remote.PressButton();

            Console.WriteLine("\nTurning Light OFF:");
            remote.SetCommand(lightOff);
            remote.PressButton();
        }
    }
}