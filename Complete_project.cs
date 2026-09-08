using System;
using System.Collections.Generic;

namespace FirstProject
{
    class Complete_project
    {
        static List<MenuItem> menus = new List<MenuItem>();
        static List<Order> allOrders = new List<Order>();
        static int nextOrderId = 1;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("---------Welcome To Tasty Food Corner-------- ");
                Console.WriteLine("1 - Add Menu Item");
                Console.WriteLine("2 - View Menu");
                Console.WriteLine("3 - Place Order");
                Console.WriteLine("4 - View Order");
                Console.WriteLine("5 - Total Cost");
                Console.WriteLine("6 - Remove Item");
                Console.WriteLine("7 - Exit The system");
                Console.Write("Enter the Choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddMenuItem();
                        break;
                    case "2":
                        ViewItem();
                        break;
                    case "3":
                        PlaceOrder();
                        break;
                    case "4":
                        ViewOrder();
                        break;
                    case "5":
                        TotalCost();
                        break;
                    case "6":
                        RemoveItem();
                        break;
                    case "7":
                        Console.WriteLine("Exit the App");
                        return;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
        }

        static void AddMenuItem()
        {
            Console.WriteLine("Enter the item name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the description: ");
            string desc = Console.ReadLine();
            Console.WriteLine("Enter the price: ");
            int price = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Is the item available right now? (yes/no)");
            string availInput = Console.ReadLine();
            bool isAvailable = availInput.ToLower() == "yes";

            Console.Write("Enter the Item Type No (1-Food, 2-Beverage, 3-Sweet): ");
            string item = Console.ReadLine();
            switch (item)
            {
                case "1":
                    Console.Write("Enter the cuisine (e.g. Italian, Fast Food): ");
                    string cuisine = Console.ReadLine();
                    Console.WriteLine("Enter the Calories: ");
                    int cuical = Convert.ToInt32(Console.ReadLine());
                    menus.Add(new FoodItem(name, desc, price, cuisine, cuical, isAvailable));
                    break;
                case "2":
                    Console.Write("Enter the drink type (e.g. Soft Drink, Juice): ");
                    string drinks = Console.ReadLine();
                    Console.WriteLine("Enter the Calories: ");
                    int bevcal = Convert.ToInt32(Console.ReadLine());
                    menus.Add(new Beverage(name, desc, price, drinks, bevcal, isAvailable));
                    break;
                case "3":
                    Console.Write("Enter the dessert type: ");
                    string dessert = Console.ReadLine();
                    Console.WriteLine("Enter the Calories: ");
                    int sweetcal = Convert.ToInt32(Console.ReadLine());
                    menus.Add(new Sweet(name, desc, price, dessert, sweetcal, isAvailable));
                    break;
                default:
                    Console.WriteLine("Invalid item type");
                    break;
            }
        }

        static void ViewItem()
        {
            if (menus.Count == 0)
            {
                Console.WriteLine("No items in menu.");
                return;
            }
            foreach (var item in menus)
            {
                Console.WriteLine(item.GetDescription());
            }
        }

        static void PlaceOrder()
        {
            Order neworder = new Order(nextOrderId);
            nextOrderId++;
            while (true)
            {
                Console.WriteLine("Enter the name of item you want to order: ");
                string orderItemName = Console.ReadLine();
                bool found = false;
                foreach (var item in menus)
                {
                    if (item.name == orderItemName)
                    {
                        found = true;
                        if (!item.IsAvailable)
                        {
                            Console.WriteLine("Sorry, this item is currently sold out.");
                        }
                        else
                        {
                            neworder.AddItem(item);
                        }
                        break;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Item not found");
                }

                Console.WriteLine("Do you want to add more items? (yes/no)");
                string more = Console.ReadLine();
                if (more.ToLower() != "yes")
                {
                    break;
                }
            }

            Console.WriteLine("How do you want to pay? (Online/Cash)");
            string pay = Console.ReadLine();
            neworder.PlaceOrder(pay);
            neworder.DeliveryTime = "30 Minutes";
            neworder.DeliveryCost = 250;
            allOrders.Add(neworder);
            Console.WriteLine($"Order Placed! Your order ID is {neworder.orderID}");
        }

        static void ViewOrder()
        {
            Console.Write("Enter the ID of your order: ");
            int id = Convert.ToInt32(Console.ReadLine());
            bool found = false;
            foreach (var order in allOrders)
            {
                if (order.orderID == id)
                {
                    found = true;
                    Console.WriteLine($"Placed: {order.IsPlaced}, Payment: {order.paymentMethod}, Delivery: {order.DeliveryCost} ({order.DeliveryTime}), Final Amount: {order.GetFinalAmount()}");
                    Console.WriteLine("Items:");
                    foreach (var menuItem in order.items)
                    {
                        Console.WriteLine(menuItem.GetDescription());
                    }
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Order not found");
            }
        }

        static void TotalCost()
        {
            Console.Write("Enter the ID of your Order: ");
            int id = Convert.ToInt32(Console.ReadLine());
            bool found = false;
            foreach (var order in allOrders)
            {
                if (order.orderID == id)
                {
                    found = true;
                    Console.WriteLine($"Total cost of the order is: {order.GetFinalAmount()}");
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Order not found");
            }
        }

        static void RemoveItem()
        {
            Console.Write("Enter the ID of your Order: ");
            int id = Convert.ToInt32(Console.ReadLine());
            bool orderFound = false;

            foreach (var order in allOrders)
            {
                if (order.orderID == id)
                {
                    orderFound = true;
                    Console.WriteLine("Items in this order:");
                    foreach (var menuItem in order.items)
                    {
                        Console.WriteLine(menuItem.GetDescription());
                    }

                    Console.WriteLine("What item do you want to remove (enter exact name):");
                    string itemName = Console.ReadLine();

                    bool itemFound = false;
                    foreach (var menuItem in order.items)
                    {
                        if (menuItem.name == itemName)
                        {
                            itemFound = true;
                            order.RemoveItem(menuItem);
                            Console.WriteLine("Item removed successfully");
                            break;
                        }
                    }
                    if (!itemFound)
                    {
                        Console.WriteLine("Item not found in this order");
                    }
                    break;
                }
            }
            if (!orderFound)
            {
                Console.WriteLine("Order not found");
            }
        }

        abstract class MenuItem
        {
            public string name;
            public string description;
            public double price;
            public bool IsAvailable;
            public virtual string GetDescription()
            {
                return $"Title: {name}, Description: {description}, Price: ${price}, Available: {IsAvailable}";
            }
        }

        class FoodItem : MenuItem
        {
            public string cuisine;
            private int calories;
            public int Calories
            {
                get { return calories; }
                set
                {
                    if (value < 150 || value > 750)
                        Console.WriteLine("Calories must be in range 150-750");
                    else
                        calories = value;
                }
            }
            public FoodItem(string n, string d, double p, string c, int ca, bool isAvailable)
            {
                name = n;
                description = d;
                price = p;
                cuisine = c;
                Calories = ca;
                IsAvailable = isAvailable;
            }
            public override string GetDescription()
            {
                return base.GetDescription() + $", Cuisine: {cuisine}, Calories: {calories}";
            }
        }

        class Beverage : MenuItem
        {
            public string drinks;
            private int calories;
            public int Calories
            {
                get { return calories; }
                set
                {
                    if (value < 100 || value > 500)
                        Console.WriteLine("Calories must be in range 100-500");
                    else
                        calories = value;
                }
            }
            public Beverage(string n, string d, double p, string dr, int ca, bool isAvailable)
            {
                name = n;
                description = d;
                price = p;
                drinks = dr;
                Calories = ca;
                IsAvailable = isAvailable;
            }
            public override string GetDescription()
            {
                return base.GetDescription() + $", Drinks: {drinks}, Calories: {calories}";
            }
        }

        class Sweet : MenuItem
        {
            public string dessert;
            private int calories;
            public int Calories
            {
                get { return calories; }
                set
                {
                    if (value < 250 || value > 750)
                        Console.WriteLine("Calories must be in range 250-750");
                    else
                        calories = value;
                }
            }
            public Sweet(string n, string d, double p, string de, int ca, bool isAvailable)
            {
                name = n;
                description = d;
                price = p;
                dessert = de;
                Calories = ca;
                IsAvailable = isAvailable;
            }
            public override string GetDescription()
            {
                return base.GetDescription() + $", Dessert: {dessert}, Calories: {calories}";
            }
        }

        class Order
        {
            public int orderID;
            public List<MenuItem> items = new List<MenuItem>();
            public bool IsPlaced;
            public string DeliveryTime;
            public double DeliveryCost;
            public string paymentMethod;

            public Order(int id)
            {
                orderID = id;
                IsPlaced = false;
            }

            public void AddItem(MenuItem item)
            {
                if (!IsPlaced)
                {
                    items.Add(item);
                }
                else
                { 
                    Console.WriteLine("Cannot modify placed order");
                }
            }

            public void RemoveItem(MenuItem item)
            {
                if (!IsPlaced)
                { 
                    items.Remove(item);
                }
                else
                {
                    Console.WriteLine("Item cannot be removed");
                }
            }

            public double CalulateTotal()
            {
                double total = 0;
                foreach (var item in items) 
                {
                    total += item.price;
                }
                return total;
            }

            public double CalculateDiscount(double totalAmount)
            {
                if (totalAmount >= 5000)
                {
                    return totalAmount * 0.20;
                }
                else if (totalAmount >= 2000) 
                {
                    return totalAmount * 0.10;
                }
                else if (totalAmount >= 1000) 
                {
                    return totalAmount * 0.05;
                }
                else 
                {
                    return 0;
                }
            }

            public double GetFinalAmount()
            {
                double total = CalulateTotal();
                double discount = CalculateDiscount(total);
                return total - discount + DeliveryCost;
            }

            public void PlaceOrder(string payment)
            {
                IsPlaced = true;
                paymentMethod = payment;
            }
        }
    }
}