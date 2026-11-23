using OOP_Project_Bakery_POS_System.Assets;
using OOP_Project_Bakery_POS_System.Models;

namespace OliveBranchBakes;

// Baker console: manage products/availability and order status.
public class AdminForm : Form
{
    // Products tab
    readonly ListBox lstProducts;
    readonly TextBox txtName, txtDesc, txtServing, txtAllergens, txtImage;
    readonly ComboBox cboCategory, cboAvail;
    readonly NumericUpDown numPrice;
    readonly Label lblProductMsg;
    BakeryProduct? current;

    // Orders tab
    readonly ListBox lstOrders;
    readonly TextBox txtDetails;
    readonly ComboBox cboStatus;

    public AdminForm()
    {
        Theme.Setup(this, "Baker Console - Olive Branch Bakes", 1000, 680);

        var tabs = new TabControl { Dock = DockStyle.Fill, Font = Theme.Body, ItemSize = new Size(200, 40), SizeMode = TabSizeMode.Fixed };
        var tabProducts = new TabPage("Products & Menu") { BackColor = Theme.Cream };
        var tabOrders = new TabPage("Orders") { BackColor = Theme.Cream };
        tabs.TabPages.Add(tabProducts);
        tabs.TabPages.Add(tabOrders);
        Controls.Add(tabs);

        // ----- Products tab -----
        lstProducts = new ListBox { Left = 10, Top = 10, Width = 340, Height = 520 };
        tabProducts.Controls.Add(lstProducts);

        Theme.Lbl(tabProducts, "Name", 370, 15, 140);          txtName = Theme.Txt(tabProducts, 520, 12, 440);
        Theme.Lbl(tabProducts, "Category", 370, 60, 140);      cboCategory = Theme.EnumCombo<ProductCategory>(tabProducts, 520, 57, 440);
        Theme.Lbl(tabProducts, "Description", 370, 105, 140);  txtDesc = Theme.Txt(tabProducts, 520, 102, 440, 70, true);
        Theme.Lbl(tabProducts, "Price ($)", 370, 190, 140);
        numPrice = new NumericUpDown { Left = 520, Top = 187, Width = 150, DecimalPlaces = 2, Maximum = 10000, Increment = 0.5m };
        tabProducts.Controls.Add(numPrice);
        Theme.Lbl(tabProducts, "Serving size", 370, 235, 140); txtServing = Theme.Txt(tabProducts, 520, 232, 440);
        Theme.Lbl(tabProducts, "Allergens", 370, 280, 140);    txtAllergens = Theme.Txt(tabProducts, 520, 277, 440);
        Theme.Lbl(tabProducts, "Availability", 370, 325, 140); cboAvail = Theme.EnumCombo<Availability>(tabProducts, 520, 322, 440);
        Theme.Lbl(tabProducts, "Image path (optional)", 370, 370, 150, 50); txtImage = Theme.Txt(tabProducts, 520, 367, 440);

        Theme.Btn(tabProducts, "New Product", 370, 440, 190, 55, Theme.Brown, (s, e) => NewProduct());
        Theme.Btn(tabProducts, "Save Product", 575, 440, 190, 55, Theme.Olive, (s, e) => SaveProduct());
        lblProductMsg = Theme.Lbl(tabProducts, "", 370, 505, 590);

        lstProducts.SelectedIndexChanged += (s, e) =>
        {
            if (lstProducts.SelectedItem is BakeryProduct p) LoadProduct(p);
        };
        ReloadProducts();

        // ----- Orders tab -----
        lstOrders = new ListBox { Left = 10, Top = 10, Width = 400, Height = 520, FormattingEnabled = true };
        lstOrders.Format += (s, e) =>
        {
            if (e.ListItem is Order o)
                e.Value = $"#{o.Number}  {o.Customer.FullName}  [{o.Customer.Source}]  {o.Status}";
        };
        tabOrders.Controls.Add(lstOrders);

        txtDetails = Theme.Txt(tabOrders, 430, 10, 530, 430, multi: true);
        txtDetails.ReadOnly = true;
        Theme.Lbl(tabOrders, "Status", 430, 462, 70);
        cboStatus = Theme.EnumCombo<OrderStatus>(tabOrders, 510, 459, 200);
        Theme.Btn(tabOrders, "Update Status", 730, 450, 230, 55, Theme.Olive, (s, e) => UpdateStatus());

        lstOrders.SelectedIndexChanged += (s, e) =>
        {
            if (lstOrders.SelectedItem is Order o)
            {
                txtDetails.Text = o.Summary();
                cboStatus.SelectedItem = o.Status;
            }
        };
        tabs.SelectedIndexChanged += (s, e) => ReloadOrders();
        ReloadOrders();
    }

    // ----- Products -----
    void ReloadProducts()
    {
        lstProducts.Items.Clear();
        foreach (var p in AppData.Menu.Products) lstProducts.Items.Add(p);
    }

    void LoadProduct(BakeryProduct p)
    {
        current = p;
        txtName.Text = p.Name;
        cboCategory.SelectedItem = p.Category;
        txtDesc.Text = p.Description;
        numPrice.Value = p.Price;
        txtServing.Text = p.ServingSize;
        txtAllergens.Text = p.Allergens;
        cboAvail.SelectedItem = p.Availability;
        txtImage.Text = p.ImagePath;
        lblProductMsg.Text = "";
    }

    void NewProduct()
    {
        current = null;
        lstProducts.ClearSelected();
        txtName.Clear(); txtDesc.Clear(); txtServing.Clear(); txtAllergens.Clear(); txtImage.Clear();
        numPrice.Value = 0;
        cboCategory.SelectedIndex = 0;
        cboAvail.SelectedIndex = 0;
        lblProductMsg.Text = "Enter the new product's details, then click Save Product.";
        lblProductMsg.ForeColor = Theme.Brown;
        txtName.Focus();
    }

    void SaveProduct()
    {
        if (string.IsNullOrWhiteSpace(txtName.Text) || numPrice.Value <= 0)
        {
            lblProductMsg.ForeColor = Theme.Error;
            lblProductMsg.Text = "A name and a price greater than zero are required.";
            return;
        }

        var p = current ?? new BakeryProduct();
        p.Name = txtName.Text.Trim();
        p.Category = (ProductCategory)cboCategory.SelectedItem!;
        p.Description = txtDesc.Text.Trim();
        p.Price = numPrice.Value;
        p.ServingSize = txtServing.Text.Trim();
        p.Allergens = txtAllergens.Text.Trim();
        p.Availability = (Availability)cboAvail.SelectedItem!;
        p.ImagePath = txtImage.Text.Trim();
        if (current == null) AppData.Menu.Add(p);

        ReloadProducts();
        lstProducts.SelectedItem = p;
        lblProductMsg.ForeColor = Theme.Olive;
        lblProductMsg.Text = "Product saved.";
    }

    // ----- Orders -----
    void ReloadOrders()
    {
        var selected = lstOrders.SelectedItem;
        lstOrders.Items.Clear();
        foreach (var o in AppData.Orders.OrderByDescending(o => o.Created)) lstOrders.Items.Add(o);
        if (selected != null && lstOrders.Items.Contains(selected)) lstOrders.SelectedItem = selected;
    }

    void UpdateStatus()
    {
        if (lstOrders.SelectedItem is not Order o)
        {
            MessageBox.Show("Select an order first.");
            return;
        }
        o.Status = (OrderStatus)cboStatus.SelectedItem!;
        ReloadOrders();
        lstOrders.SelectedItem = o;
        txtDetails.Text = o.Summary();
    }
}
