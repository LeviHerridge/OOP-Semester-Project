using System.Text;

namespace OOP_Project_Bakery_POS_System.Models;

// ---------- Enumerations ----------
public enum ProductCategory { Cookies, Brownies, Cakes, Seasonal }
public enum Availability { Available, SoldOut }
public enum ContactMethod { Phone, Text, Email }
public enum FulfillmentChoice { Pickup, Delivery }
public enum CustomerSource { Guest, Account }
public enum OrderStatus { Pending, Confirmed, Ready, Completed, Cancelled }

// ---------- Products ----------
public class BakeryProduct
{
    public string Name { get; set; } = "";
    public ProductCategory Category { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public string ServingSize { get; set; } = "";
    public string Allergens { get; set; } = "";
    public Availability Availability { get; set; } = Availability.Available;
    public string ImagePath { get; set; } = "";

    public bool IsAvailable => Availability == Availability.Available;

    public override string ToString() =>
        $"{Name} - {Price:C}" + (IsAvailable ? "" : "  (SOLD OUT)");
}

public class BakeryMenu
{
    public List<BakeryProduct> Products { get; } = new();

    public void Add(BakeryProduct product) => Products.Add(product);

    // Sold out products stay in Products (visible) but are not selectable.
    public IEnumerable<BakeryProduct> Selectable => Products.Where(p => p.IsAvailable);
}

// ---------- Customers ----------
public abstract class Customer
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public ContactMethod PreferredContact { get; set; }

    public string FullName => $"{FirstName} {LastName}";
    public abstract CustomerSource Source { get; }
}

public class GuestCustomer : Customer
{
    public override CustomerSource Source => CustomerSource.Guest;
}

public class AccountCustomer : Customer
{
    public string Id { get; set; } = "";
    public string Password { get; set; } = "";   // demo placeholder only
    public string SavedPreferences { get; set; } = "";
    public List<Order> History { get; } = new();

    public override CustomerSource Source => CustomerSource.Account;
}

// ---------- Orders ----------
public class OrderItem
{
    public BakeryProduct Product { get; }
    public int Quantity { get; set; }
    public decimal LineTotal => Product.Price * Quantity;

    public OrderItem(BakeryProduct product, int quantity)
    {
        if (!product.IsAvailable)
            throw new InvalidOperationException($"{product.Name} is sold out.");
        if (quantity < 1)
            throw new ArgumentException("Quantity must be at least 1.");
        Product = product;
        Quantity = quantity;
    }

    public override string ToString() => $"{Quantity} x {Product.Name}  =  {LineTotal:C}";
}

public class Order
{
    static int nextNumber = 1001;

    public int Number { get; } = nextNumber++;
    public DateTime Created { get; } = DateTime.Now;
    public Customer Customer { get; }
    public List<OrderItem> Items { get; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;   // every order starts Pending

    public DateTime RequestedDate { get; set; }
    public TimeSpan RequestedTime { get; set; }
    public FulfillmentChoice Fulfillment { get; set; }
    public string AllergyNotes { get; set; } = "";
    public string SpecialInstructions { get; set; } = "";
    public string GiftNote { get; set; } = "";
    public string HowHeard { get; set; } = "";

    public decimal Total => Items.Sum(i => i.LineTotal);

    public Order(Customer customer, IEnumerable<OrderItem> items)
    {
        Customer = customer;
        Items = items.ToList();
        if (Items.Count == 0)
            throw new InvalidOperationException("An order needs at least one item.");
    }

    static string Or(string s) => string.IsNullOrWhiteSpace(s) ? "None" : s;

    public string Summary()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Order #{Number}");
        sb.AppendLine($"Status: {Status}");
        sb.AppendLine($"Customer: {Customer.FullName} ({Customer.Source})");
        sb.AppendLine($"Phone: {Customer.Phone}");
        sb.AppendLine($"Email: {Customer.Email}");
        sb.AppendLine($"Preferred contact: {Customer.PreferredContact}");
        sb.AppendLine($"Requested: {RequestedDate:D} at {DateTime.Today.Add(RequestedTime):t}");
        sb.AppendLine($"Fulfillment: {Fulfillment}");
        sb.AppendLine();
        sb.AppendLine("Items:");
        foreach (var item in Items) sb.AppendLine("   " + item);
        sb.AppendLine($"Estimated subtotal: {Total:C}");
        sb.AppendLine();
        sb.AppendLine($"Allergy notes: {Or(AllergyNotes)}");
        sb.AppendLine($"Special instructions: {Or(SpecialInstructions)}");
        sb.AppendLine($"Event or gift note: {Or(GiftNote)}");
        sb.AppendLine($"Heard about us: {Or(HowHeard)}");
        return sb.ToString();
    }

    public override string ToString() => $"#{Number}   {Created:g}   {Status}   {Total:C}";
}

// ---------- Administrator ----------
public class Administrator
{
    // Demonstration credentials only.
    const string DemoUser = "admin";
    const string DemoPassword = "olive123";

    public bool Validate(string user, string password) =>
        user == DemoUser && password == DemoPassword;
}
