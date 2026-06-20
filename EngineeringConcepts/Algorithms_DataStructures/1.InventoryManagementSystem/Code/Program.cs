// Assignment: Exercise 1 - Inventory Management System
// Submitted by: Nilanjan Pradhan
// Cognizant Digital Nurture 5.0

using System;
using System.Collections.Generic;

// This class is used to define the product blueprint
class Product
{
    public int id;
    public string name;
    public int quantity;
    public double price;

    public Product(int id, string name, int quantity, double price)
    {
        this.id = id;
        this.name = name;
        this.quantity = quantity;
        this.price = price;
    }

    public void Show()
    {
        Console.WriteLine($"ID: {id}, Name: {name}, Quantity: {quantity}, Price: ₹{price}");
    }
}

class MyInventory
{
    Dictionary<int, Product> inventory = new Dictionary<int, Product>();

    public void Add(Product p)
    {
        if (!inventory.ContainsKey(p.id))
        {
            inventory[p.id] = p;
            Console.WriteLine("Product added successfully.");
        }
        else
        {
            Console.WriteLine("Product already exists.");
        }
    }

    public void ChangeQuantity(int id, int qty)
    {
        if (inventory.ContainsKey(id))
        {
            inventory[id].quantity = qty;
            Console.WriteLine("Product quantity updated.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    public void Delete(int id)
    {
        if (inventory.ContainsKey(id))
        {
            inventory.Remove(id);
            Console.WriteLine("Product removed.");
        }
        else
        {
            Console.WriteLine("No such product.");
        }
    }

    // This function displays the current inventory
    public void ShowAllProducts()
    {
        Console.WriteLine("\n==== PRODUCT DETAILS ====");
        if (inventory.Count == 0)
        {
            Console.WriteLine("No products in inventory.");
        }
        else
        {
            foreach (var p in inventory.Values)
            {
                p.Show();
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyInventory invSys = new MyInventory();

        Product p1 = new Product(101, "Pen Drive", 20, 499.00);
        Product p2 = new Product(102, "Charger", 10, 899.99);

        invSys.Add(p1);
        invSys.Add(p2);

        invSys.ShowAllProducts();

        invSys.ChangeQuantity(101, 25);
        invSys.Delete(102);

        invSys.ShowAllProducts();
    }
}