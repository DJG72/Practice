/*
 * Exercise 3: Sorting Customer Orders
 * Author: Nilanjan Pradhan
 * Description: Sort customer orders based on total price using Bubble Sort and Quick Sort.
 */

using System;

namespace SortingCustomerOrders
{
    class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public double TotalPrice { get; set; }

        public Order(int orderId, string customerName, double totalPrice)
        {
            OrderId = orderId;
            CustomerName = customerName;
            TotalPrice = totalPrice;
        }

        public void Display()
        {
            Console.WriteLine($"{OrderId}\t{CustomerName}\t₹{TotalPrice}");
        }
    }

    class Program
    {
        static void BubbleSort(Order[] orders)
        {
            int n = orders.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (orders[j].TotalPrice > orders[j + 1].TotalPrice)
                    {
                        Order temp = orders[j];
                        orders[j] = orders[j + 1];
                        orders[j + 1] = temp;
                    }
                }
            }
        }

        static int Partition(Order[] orders, int low, int high)
        {
            double pivot = orders[high].TotalPrice;
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (orders[j].TotalPrice < pivot)
                {
                    i++;

                    Order temp = orders[i];
                    orders[i] = orders[j];
                    orders[j] = temp;
                }
            }

            Order swap = orders[i + 1];
            orders[i + 1] = orders[high];
            orders[high] = swap;

            return i + 1;
        }

        static void QuickSort(Order[] orders, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(orders, low, high);

                QuickSort(orders, low, pivotIndex - 1);
                QuickSort(orders, pivotIndex + 1, high);
            }
        }

        static void DisplayOrders(Order[] orders)
        {
            Console.WriteLine("\nOrderID\tCustomer\tTotal Price");
            Console.WriteLine("-----------------------------------");

            foreach (Order order in orders)
            {
                order.Display();
            }
        }

        static void Main(string[] args)
        {
            Order[] orders =
            {
                new Order(101,"Amit",4500),
                new Order(102,"Riya",1200),
                new Order(103,"Sourav",7800),
                new Order(104,"Priya",2500),
                new Order(105,"Rahul",6000)
            };

            Console.WriteLine("=== Original Orders ===");
            DisplayOrders(orders);

            Order[] bubbleOrders = (Order[])orders.Clone();

            BubbleSort(bubbleOrders);

            Console.WriteLine("\n=== Orders Sorted using Bubble Sort ===");
            DisplayOrders(bubbleOrders);

            Order[] quickOrders = (Order[])orders.Clone();

            QuickSort(quickOrders, 0, quickOrders.Length - 1);

            Console.WriteLine("\n=== Orders Sorted using Quick Sort ===");
            DisplayOrders(quickOrders);
        }
    }
}