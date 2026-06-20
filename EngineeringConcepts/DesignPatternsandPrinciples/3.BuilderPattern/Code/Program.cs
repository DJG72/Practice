// Exercise 3: Implementing the Builder Pattern
// Name: Nilanjan Pradhan
// Assignment: Cognizant Digital Nurture 5.0 - Week 1 (Design Patterns & Principles)

using System;

namespace BuilderPatternExample
{
    // Product Class
    public class Computer
    {
        public string CPU { get; private set; }
        public string RAM { get; private set; }
        public string Storage { get; private set; }
        public string GPU { get; private set; }
        public string OperatingSystem { get; private set; }

        // Private Constructor
        private Computer(Builder builder)
        {
            CPU = builder.CPU;
            RAM = builder.RAM;
            Storage = builder.Storage;
            GPU = builder.GPU;
            OperatingSystem = builder.OperatingSystem;
        }

        public void Display()
        {
            Console.WriteLine("Computer Configuration");
            Console.WriteLine("----------------------");
            Console.WriteLine($"CPU: {CPU}");
            Console.WriteLine($"RAM: {RAM}");
            Console.WriteLine($"Storage: {Storage}");
            Console.WriteLine($"GPU: {GPU}");
            Console.WriteLine($"Operating System: {OperatingSystem}");
            Console.WriteLine();
        }

        // Nested Builder Class
        public class Builder
        {
            public string CPU { get; private set; }
            public string RAM { get; private set; }
            public string Storage { get; private set; }
            public string GPU { get; private set; }
            public string OperatingSystem { get; private set; }

            public Builder SetCPU(string cpu)
            {
                CPU = cpu;
                return this;
            }

            public Builder SetRAM(string ram)
            {
                RAM = ram;
                return this;
            }

            public Builder SetStorage(string storage)
            {
                Storage = storage;
                return this;
            }

            public Builder SetGPU(string gpu)
            {
                GPU = gpu;
                return this;
            }

            public Builder SetOperatingSystem(string operatingSystem)
            {
                OperatingSystem = operatingSystem;
                return this;
            }

            public Computer Build()
            {
                return new Computer(this);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Builder Pattern - Computer Configuration System\n");

            Computer gamingPC = new Computer.Builder()
                .SetCPU("Intel Core i9")
                .SetRAM("32 GB")
                .SetStorage("1 TB SSD")
                .SetGPU("NVIDIA RTX 4080")
                .SetOperatingSystem("Windows 11")
                .Build();

            Computer officePC = new Computer.Builder()
                .SetCPU("Intel Core i5")
                .SetRAM("16 GB")
                .SetStorage("512 GB SSD")
                .SetOperatingSystem("Windows 11")
                .Build();

            Computer serverPC = new Computer.Builder()
                .SetCPU("AMD EPYC")
                .SetRAM("64 GB")
                .SetStorage("4 TB SSD")
                .SetOperatingSystem("Linux")
                .Build();

            gamingPC.Display();
            officePC.Display();
            serverPC.Display();
        }
    }
}