# Food-ordering-System-Console-App
Tasty Food Corner — Console Food Ordering System

A console-based food ordering application built in C#, simulating a restaurant menu and order management system. Built as a capstone project to practice core Object-Oriented Programming concepts (inheritance, polymorphism, abstraction) in C#.

# Features
Menu Management — add new menu items across three categories (Food, Beverage, Sweet), each with its own attributes and calorie-range validation
View Menu — displays all menu items with category-specific details (cuisine, drink type, or dessert type) via polymorphic descriptions
Place Order — build an order by selecting multiple items from the menu, with availability checks to prevent ordering sold-out items
View Order — look up a placed order by its unique ID to see full details: items, payment method, delivery info, and final cost
Total Cost Calculator — calculates the final order amount including a threshold-based discount and delivery charge
Remove Item — remove an item from an order (only permitted before the order has been placed)
Tiered Discounts — automatic discount based on order subtotal:
Rs 1,000+ → 5% off
Rs 2,000+ → 10% off
Rs 5,000+ → 20% off
# Tech Stack
C# (.NET)
Console Application
In-memory data storage using List<T> (no external database)
Architecture / OOP Concepts Used
Abstraction — MenuItem is an abstract base class defining shared structure (name, description, price, IsAvailable) and a polymorphic GetDescription() method
Inheritance — FoodItem, Beverage, and Sweet all inherit from MenuItem, each adding its own category-specific field (cuisine, drink type, dessert type) and a validated Calories property with a category-specific acceptable range
Polymorphism — each derived class overrides GetDescription() to return a description formatted for its own category; a single foreach loop over a mixed list of menu items correctly calls the right version for each item
Encapsulation — calorie values are validated through private fields with public properties (rejecting out-of-range input) rather than exposed as plain public fields
Composition — the Order class holds a List<MenuItem> representing everything a customer has ordered, rather than inheriting from MenuItem (an order is not a menu item — it has menu items)
# Project Structure
Complete_project.cs
├── Main()                  — menu-driven console loop
├── AddMenuItem()            — add a new Food/Beverage/Sweet item
├── ViewItem()                — display all menu items
├── PlaceOrder()              — build and finalize a new order
├── ViewOrder()                — look up an order by ID
├── TotalCost()                 — display final cost for an order
├── RemoveItem()                 — remove an item from an unplaced order
│
├── MenuItem (abstract class)
│   ├── FoodItem : MenuItem
│   ├── Beverage : MenuItem
│   └── Sweet : MenuItem
│
└── Order
    ├── AddItem() / RemoveItem()
    ├── CalulateTotal()
    ├── CalculateDiscount()
    ├── GetFinalAmount()
    └── PlaceOrder()
# How to Run
Clone the repository
Open the project in Visual Studio / VS Code
Run dotnet run or launch via the IDE
Follow the on-screen menu to add items, place orders, and manage the order lifecycle
Sample Menu Flow
1 - Add Menu Item
2 - View Menu
3 - Place Order
4 - View Order
5 - Total Cost
6 - Remove Item
7 - Exit The system
What I Learned

This project ties together arrays/collections, methods, classes, and the full set of core OOP pillars (inheritance, polymorphism, abstraction, encapsulation) into a single working console application, along with practical patterns like list-based search-by-ID (bool found + foreach + break), sentinel-style validation in setters, and tiered conditional business logic (discount calculation).
