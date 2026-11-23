This is the dump of my local files for the OOP project so that it can exist on GitHub. 


A C# Windows Forms (.NET 10) application that lets customers submit bakery order requests and lets the baker manage the menu and orders. Data is kept in memory, so it resets when the app closes.

Running the app

Open OOP-Project-Bakery-POS-System.sln in Visual Studio and press F5, or run dotnet run in the project folder. To build a Windows release:

dotnet publish -c Release -r win-x64 --self-contained
Demo logins
Role	ID	Password
Customer account	demo	demo123
Baker (admin)	admin	olive123

These are demonstration credentials only. Do not enter real passwords or private data.

Features

Customers

Order as a guest, or create an optional account.
Browse the menu (Cookies, Brownies, Cakes, Seasonal) with price, serving size, allergens, and an optional image.
Sold out items stay visible but cannot be added to an order.
Add multiple products with quantities, then enter the requested date and time, pickup or delivery, allergy notes, special instructions, an optional event or gift note, and how they heard about the bakery.
After submitting, see the required confirmation message, a full order summary, the allergy statement, and the help message. Every new order starts as Pending.

Account holders

View their own order history and current status.
Reorder a past order. Items that are sold out or unavailable are skipped and the customer is told which ones.
Contact details are prefilled on new orders.

Baker

Add products and edit prices, descriptions, allergens, and availability, including seasonal items and the weekly menu.
View all orders, including whether each came from a guest or an account holder.
Update order status: Pending, Confirmed, Ready, Completed, or Cancelled.
Project structure
File	Purpose
Models.cs	Enums and domain classes: BakeryProduct, BakeryMenu, Customer, GuestCustomer, AccountCustomer, Order, OrderItem, Administrator
AppData.cs	In-memory menu, orders, accounts, and the required customer messages
Theme.cs	Shared colors (cream, olive, brown, honey) and control helpers
MainForm.cs	Home screen
OrderForm.cs	Ordering form for guests and account holders
SignUpForm.cs	Account creation
LoginForm.cs	Login dialog for accounts and the baker
AccountForm.cs	Order history, status, and reorder
AdminForm.cs	Baker console for products and orders

Business rules (such as blocking sold out items and starting orders as Pending) live in the domain classes. The forms handle interaction and presentation.
