using OOP_Project_Bakery_POS_System.Assets;
using OOP_Project_Bakery_POS_System.Models;

namespace OliveBranchBakes;

// Shows only the logged-in customer's own orders.
public class AccountForm : Form
{
    readonly AccountCustomer account;
    readonly ListBox lstOrders;
    readonly TextBox txtDetails;

    public AccountForm(AccountCustomer account)
    {
        this.account = account;
        Theme.Setup(this, "My Account - Olive Branch Bakes", 960, 640);

        Theme.Lbl(this, $"Welcome, {account.FirstName}", 20, 15, 600, 45, title: true);
        Theme.Lbl(this, "My orders", 20, 70, 300);
        lstOrders = new ListBox { Left = 20, Top = 100, Width = 400, Height = 400 };
        Controls.Add(lstOrders);

        Theme.Lbl(this, "Order details and status", 440, 70, 400);
        txtDetails = Theme.Txt(this, 440, 100, 500, 400, multi: true);
        txtDetails.ReadOnly = true;

        Theme.Btn(this, "New Order", 20, 520, 190, 60, Theme.Olive, (s, e) =>
        {
            using var f = new OrderForm(account);
            f.ShowDialog(this);
            Reload();
        });
        Theme.Btn(this, "Reorder Selected", 220, 520, 200, 60, Theme.Honey, (s, e) =>
        {
            if (lstOrders.SelectedItem is not Order o) { MessageBox.Show("Select an order to reorder."); return; }
            using var f = new OrderForm(account, o);
            f.ShowDialog(this);
            Reload();
        });
        Theme.Btn(this, "Close", 750, 520, 190, 60, Theme.Brown, (s, e) => Close());

        lstOrders.SelectedIndexChanged += (s, e) =>
            txtDetails.Text = lstOrders.SelectedItem is Order o ? o.Summary() : "";
        Reload();
    }

    void Reload()
    {
        lstOrders.Items.Clear();
        foreach (var o in account.History.OrderByDescending(o => o.Created))
            lstOrders.Items.Add(o);
        txtDetails.Text = "";
    }
}
