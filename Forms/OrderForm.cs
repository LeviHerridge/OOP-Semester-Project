using OOP_Project_Bakery_POS_System.Assets;
using OOP_Project_Bakery_POS_System.Models;

namespace OliveBranchBakes;

// One ordering form used for both guests (account == null) and account holders.
public class OrderForm : Form
{
    readonly AccountCustomer? account;
    readonly List<OrderItem> cart = new();

    readonly TextBox txtFirst, txtLast, txtPhone, txtEmail, txtAllergy, txtInstructions, txtGift;
    readonly ComboBox cboContact, cboFulfill, cboHeard;
    readonly DateTimePicker dtDate, dtTime;
    readonly ListBox lstMenu, lstCart;
    readonly NumericUpDown numQty;
    readonly Label lblDetails, lblTotal, lblError;
    readonly PictureBox pic;

    public OrderForm(AccountCustomer? account = null, Order? reorderFrom = null)
    {
        this.account = account;
        Theme.Setup(this, account == null ? "Guest Order - Olive Branch Bakes" : "Account Order - Olive Branch Bakes", 1000, 760);
        AutoScroll = true;

        Theme.Lbl(this, "Place an Order", 20, 10, 450, 50, title: true);
        Theme.Lbl(this, account == null ? "Ordering as a guest" : $"Account: {account.FullName}", 480, 25, 490);

        // ----- Customer details -----
        Theme.Lbl(this, "First name", 20, 70, 130);   txtFirst = Theme.Txt(this, 155, 67, 290);
        Theme.Lbl(this, "Last name", 20, 110, 130);   txtLast = Theme.Txt(this, 155, 107, 290);
        Theme.Lbl(this, "Phone", 20, 150, 130);       txtPhone = Theme.Txt(this, 155, 147, 290);
        Theme.Lbl(this, "Email", 20, 190, 130);       txtEmail = Theme.Txt(this, 155, 187, 290);
        Theme.Lbl(this, "Contact me by", 20, 230, 130); cboContact = Theme.EnumCombo<ContactMethod>(this, 155, 227, 290);

        // ----- Order details -----
        Theme.Lbl(this, "Requested date", 20, 280, 130);
        dtDate = new DateTimePicker { Left = 155, Top = 277, Width = 290, Format = DateTimePickerFormat.Long, MinDate = DateTime.Today, Value = DateTime.Today.AddDays(2) };
        Controls.Add(dtDate);
        Theme.Lbl(this, "Requested time", 20, 320, 130);
        dtTime = new DateTimePicker { Left = 155, Top = 317, Width = 290, Format = DateTimePickerFormat.Time, ShowUpDown = true, Value = DateTime.Today.AddHours(12) };
        Controls.Add(dtTime);
        Theme.Lbl(this, "Pickup or delivery", 20, 360, 130, 28);
        cboFulfill = Theme.EnumCombo<FulfillmentChoice>(this, 155, 357, 290);
        Theme.Lbl(this, "Allergy notes", 20, 400, 130);   txtAllergy = Theme.Txt(this, 155, 397, 290, 60, true);
        Theme.Lbl(this, "Special instructions", 20, 470, 130, 50); txtInstructions = Theme.Txt(this, 155, 467, 290, 60, true);
        Theme.Lbl(this, "Event or gift note (optional)", 20, 540, 130, 50); txtGift = Theme.Txt(this, 155, 537, 290, 60, true);
        Theme.Lbl(this, "How did you hear about us?", 20, 610, 130, 50);
        cboHeard = Theme.Combo(this, 155, 607, 290, "Church", "Friend or family", "Social media", "Flyer or poster", "Other");

        // ----- Menu -----
        Theme.Lbl(this, "Menu (sold out items cannot be added)", 480, 70, 490);
        lstMenu = new ListBox { Left = 480, Top = 100, Width = 490, Height = 170 };
        foreach (var p in AppData.Menu.Products.OrderBy(p => p.Category))
            lstMenu.Items.Add(p);
        Controls.Add(lstMenu);

        lblDetails = Theme.Lbl(this, "Select a product to see its details.", 480, 275, 330, 115);
        pic = new PictureBox { Left = 820, Top = 275, Width = 150, Height = 115, SizeMode = PictureBoxSizeMode.Zoom };
        Controls.Add(pic);

        Theme.Lbl(this, "Quantity", 480, 400, 80);
        numQty = new NumericUpDown { Left = 565, Top = 397, Width = 70, Minimum = 1, Maximum = 99, Value = 1 };
        Controls.Add(numQty);
        Theme.Btn(this, "Add to order", 645, 392, 150, 42, Theme.Olive, (s, e) => AddSelected());
        Theme.Btn(this, "Remove selected", 805, 392, 165, 42, Theme.Brown, (s, e) => RemoveSelected());

        lstCart = new ListBox { Left = 480, Top = 445, Width = 490, Height = 105 };
        Controls.Add(lstCart);
        lblTotal = Theme.Lbl(this, "Estimated subtotal: $0.00", 480, 555, 490);

        Theme.Lbl(this, AppData.HelpMessage, 480, 590, 490, 50);
        Theme.Lbl(this, AppData.AllergyStatement, 480, 640, 490, 100);

        lblError = Theme.Lbl(this, "", 20, 665, 450, 30);
        lblError.ForeColor = Theme.Error;

        var submit = Theme.Btn(this, "Submit Order Request", 20, 700, 250, 50, Theme.Olive, (s, e) => Submit());
        Theme.Btn(this, "Cancel", 285, 700, 160, 50, Theme.Brown, (s, e) => Close());
        AcceptButton = submit;

        lstMenu.SelectedIndexChanged += (s, e) => ShowDetails();

        // Prefill saved contact info for account holders.
        if (account != null)
        {
            txtFirst.Text = account.FirstName;
            txtLast.Text = account.LastName;
            txtPhone.Text = account.Phone;
            txtEmail.Text = account.Email;
            cboContact.SelectedItem = account.PreferredContact;
        }

        // Reorder: copy only items that are still available.
        if (reorderFrom != null)
        {
            var skipped = new List<string>();
            foreach (var item in reorderFrom.Items)
            {
                if (item.Product.IsAvailable) AddToCart(item.Product, item.Quantity);
                else skipped.Add(item.Product.Name);
            }
            if (skipped.Count > 0)
                Shown += (s, e) => MessageBox.Show(
                    "These items are sold out or unavailable and were not added:\n\n" + string.Join("\n", skipped),
                    "Some items unavailable");
        }
        RefreshCart();
    }

    void ShowError(string message) => lblError.Text = message;

    void ShowDetails()
    {
        if (lstMenu.SelectedItem is not BakeryProduct p) return;
        lblDetails.Text =
            $"{p.Category}  |  {p.ServingSize}\n{p.Description}\nAllergens: {p.Allergens}\n" +
            (p.IsAvailable ? "Available" : "SOLD OUT");
        pic.Image?.Dispose();
        pic.Image = !string.IsNullOrWhiteSpace(p.ImagePath) && File.Exists(p.ImagePath)
            ? Image.FromFile(p.ImagePath) : null;
    }

    void AddSelected()
    {
        ShowError("");
        if (lstMenu.SelectedItem is not BakeryProduct p) { ShowError("Please select a product first."); return; }
        if (!p.IsAvailable) { ShowError($"{p.Name} is sold out and cannot be added."); return; }
        AddToCart(p, (int)numQty.Value);
    }

    void AddToCart(BakeryProduct p, int qty)
    {
        var existing = cart.FirstOrDefault(i => i.Product == p);
        if (existing != null) existing.Quantity += qty;
        else cart.Add(new OrderItem(p, qty));
        RefreshCart();
    }

    void RemoveSelected()
    {
        if (lstCart.SelectedIndex < 0) { ShowError("Select an item in your order to remove."); return; }
        cart.RemoveAt(lstCart.SelectedIndex);
        RefreshCart();
    }

    void RefreshCart()
    {
        lstCart.Items.Clear();
        foreach (var item in cart) lstCart.Items.Add(item);
        lblTotal.Text = $"Estimated subtotal: {cart.Sum(i => i.LineTotal):C}";
    }

    void Submit()
    {
        if (string.IsNullOrWhiteSpace(txtFirst.Text) || string.IsNullOrWhiteSpace(txtLast.Text))
        { ShowError("Please enter your first and last name."); return; }
        if (string.IsNullOrWhiteSpace(txtPhone.Text))
        { ShowError("Please enter a phone number."); return; }
        if (!txtEmail.Text.Contains('@'))
        { ShowError("Please enter a valid email address."); return; }
        if (cart.Count == 0)
        { ShowError("Please add at least one item to your order."); return; }
        if (dtDate.Value.Date == DateTime.Today && dtTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay)
        { ShowError("Please choose a requested time that has not passed."); return; }
        if (cboHeard.SelectedIndex < 0)
        { ShowError("Please tell us how you heard about Olive Branch Bakes."); return; }

        Customer customer = account ?? (Customer)new GuestCustomer();
        customer.FirstName = txtFirst.Text.Trim();
        customer.LastName = txtLast.Text.Trim();
        customer.Phone = txtPhone.Text.Trim();
        customer.Email = txtEmail.Text.Trim();
        customer.PreferredContact = (ContactMethod)cboContact.SelectedItem!;

        var order = new Order(customer, cart)
        {
            RequestedDate = dtDate.Value.Date,
            RequestedTime = dtTime.Value.TimeOfDay,
            Fulfillment = (FulfillmentChoice)cboFulfill.SelectedItem!,
            AllergyNotes = txtAllergy.Text.Trim(),
            SpecialInstructions = txtInstructions.Text.Trim(),
            GiftNote = txtGift.Text.Trim(),
            HowHeard = cboHeard.SelectedItem!.ToString()!
        };

        AppData.Orders.Add(order);
        account?.History.Add(order);

        ShowConfirmation(order);
        Close();
    }

    void ShowConfirmation(Order order)
    {
        using var f = new Form();
        Theme.Setup(f, "Order Request Submitted", 700, 660);
        var nl = Environment.NewLine;
        var box = Theme.Txt(f, 20, 20, 660, 520, multi: true);
        box.ReadOnly = true;
        box.Text = AppData.ThankYouMessage + nl + nl + order.Summary() + nl +
                   AppData.AllergyStatement + nl + nl + AppData.HelpMessage;
        var close = Theme.Btn(f, "Close", 20, 560, 200, 55, Theme.Olive, (s, e) => f.Close());
        f.AcceptButton = close;
        f.ShowDialog(this);
    }
}
