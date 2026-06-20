﻿/*
 * Exercise 7: Financial Forecasting
 * Author: Nilanjan Pradhan
 * Description: Recursive algorithm to forecast future values based on past growth rates.
 */

using System;

namespace FinancialForecasting
{
    class Program
    {
        // Recursive method to forecast future value
        static double ForecastValue(double currentValue, double growthRate, int years)
        {
            if (years == 0)
                return currentValue;

            return ForecastValue(currentValue * (1 + growthRate), growthRate, years - 1);
        }

        // Optimized version using tail-recursion pattern (though C# doesn't optimize tail calls natively)
        static double ForecastTail(double currentValue, double growthRate, int years)
        {
            double result = currentValue;
            for (int i = 0; i < years; i++)
            {
                result *= (1 + growthRate);
            }
            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== Financial Forecast Tool ===");

            Console.Write("Enter current value (e.g., revenue): ");
            double currentValue = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter annual growth rate (e.g., 0.05 for 5%): ");
            double growthRate = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter number of years to forecast: ");
            int years = Convert.ToInt32(Console.ReadLine());

            double futureValue = ForecastValue(currentValue, growthRate, years);
            double optimizedFutureValue = ForecastTail(currentValue, growthRate, years);

            Console.WriteLine($"\nForecasted value (Recursive): ₹{futureValue:F2}");
            Console.WriteLine($"Forecasted value (Optimized Loop): ₹{optimizedFutureValue:F2}");
        }
    }
}