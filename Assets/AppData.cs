using OOP_Project_Bakery_POS_System.Models;

namespace OOP_Project_Bakery_POS_System.Assets;

// In-memory data shared by all forms.
public static class AppData
{
    public const string ThankYouMessage =
        "Thank you for your order request! Your order is not confirmed until Olive Branch Bakes responds with availability, total price, and pickup or delivery details.";

    public const string HelpMessage =
        "Need help placing an order? Message Olive Branch Bakes directly and I’ll help you.";

    public const string AllergyStatement =
        "Allergy notice: Our products are made in a kitchen that handles wheat, milk, eggs, tree nuts, peanuts, and soy. We cannot guarantee any item is free of these allergens. Please list any allergies in your order notes.";

    public static BakeryMenu Menu { get; } = new();
    public static List<Order> Orders { get; } = new();
    public static List<AccountCustomer> Accounts { get; } = new();
    public static Administrator Admin { get; } = new();

    static AppData()
    {
        
        Menu.Add(new BakeryProduct { Name = "Chocolate Chip Cookies", Category = ProductCategory.Cookies, Description = "Classic soft cookies with chocolate chips.", Price = 18m, ServingSize = "Dozen", Allergens = "Wheat, milk, eggs, soy" });
        Menu.Add(new BakeryProduct { Name = "Oatmeal Raisin Cookies", Category = ProductCategory.Cookies, Description = "Chewy oatmeal cookies with raisins.", Price = 18m, ServingSize = "Dozen", Allergens = "Wheat, milk, eggs" });
        Menu.Add(new BakeryProduct { Name = "Fudge Brownies", Category = ProductCategory.Brownies, Description = "Rich, fudgy chocolate brownies.", Price = 20m, ServingSize = "Pan of 12", Allergens = "Wheat, milk, eggs, soy" });
        Menu.Add(new BakeryProduct { Name = "Vanilla Layer Cake", Category = ProductCategory.Cakes, Description = "Two-layer vanilla cake with buttercream.", Price = 45m, ServingSize = "8-inch, serves 10-12", Allergens = "Wheat, milk, eggs" });
        Menu.Add(new BakeryProduct { Name = "Pumpkin Spice Loaf", Category = ProductCategory.Seasonal, Description = "Seasonal spiced pumpkin loaf.", Price = 14m, ServingSize = "1 loaf", Allergens = "Wheat, eggs, tree nuts", Availability = Availability.SoldOut });
        Menu.Add(new BakeryProduct { Name = "Custom Pie 10 inches", Category = ProductCategory.Cakes, Description = "Custom 10-inch pie.", Price = 30m, ServingSize = "10-inch", Allergens = "Wheat, milk, eggs" });
        Menu.Add(new BakeryProduct { Name = "Custom Pie 12 inches", Category = ProductCategory.Cakes, Description = "Custom 12-inch pie.", Price = 40m, ServingSize = "12-inch", Allergens = "Wheat, milk, eggs" });
        Menu.Add(new BakeryProduct { Name = "Custom Cake 8 inches", Category = ProductCategory.Cakes, Description = "Custom 8-inch cake.", Price = 45m, ServingSize = "8-inch", Allergens = "Wheat, milk, eggs" });
        Menu.Add(new BakeryProduct { Name = "Custom Cake 10 inches", Category = ProductCategory.Cakes, Description = "Custom 10-inch cake.", Price = 60m, ServingSize = "10-inch", Allergens = "Wheat, milk, eggs" });
        Menu.Add(new BakeryProduct { Name = "Custom Cake 12 inches", Category = ProductCategory.Cakes, Description = "Custom 12-inch cake.", Price = 75m, ServingSize = "12-inch", Allergens = "Wheat, milk, eggs" });

        // Demonstration account only. Never use real passwords.
        Accounts.Add(new AccountCustomer
        {
            Id = "demo", Password = "demo123",
            FirstName = "Demo", LastName = "Customer",
            Phone = "555-0100", Email = "demo@example.com",
            PreferredContact = ContactMethod.Email
        });
    }

    public static AccountCustomer? FindAccount(string id, string password) =>
        Accounts.FirstOrDefault(a => a.Id == id && a.Password == password);
}
