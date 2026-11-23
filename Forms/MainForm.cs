using OOP_Project_Bakery_POS_System.Assets;

namespace OliveBranchBakes;

public class MainForm : Form
{
    public MainForm()
    {
        Theme.Setup(this, "Olive Branch Bakes", 560, 600);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Theme.Lbl(this, "Olive Branch Bakes", 40, 30, 480, 50, title: true);
        Theme.Lbl(this, "Fresh baked goods, made with love.", 40, 85, 480);

        Theme.Btn(this, "Order as a Guest", 40, 130, 480, 70, Theme.Olive, (s, e) =>
        {
            using var f = new OrderForm();
            f.ShowDialog(this);
        });

        Theme.Btn(this, "Account Login (order history, reorder)", 40, 215, 480, 70, Theme.Brown, (s, e) => AccountLogin());

        Theme.Btn(this, "Create an Account", 40, 300, 480, 70, Theme.Olive, (s, e) => SignUp());

        Theme.Btn(this, "Baker Login", 40, 385, 480, 70, Theme.Honey, (s, e) => AdminLogin());

        Theme.Lbl(this, "Demo logins - account: demo / demo123, baker: admin / olive123", 40, 480, 480, 50);
    }

    void SignUp()
    {
        using var signUp = new SignUpForm();
        if (signUp.ShowDialog(this) != DialogResult.OK || signUp.CreatedAccount == null) return;

        using var f = new AccountForm(signUp.CreatedAccount);
        f.ShowDialog(this);
    }

    void AccountLogin()
    {
        using var login = new LoginForm("Account Login");
        if (login.ShowDialog(this) != DialogResult.OK) return;

        var account = AppData.FindAccount(login.UserName, login.Password);
        if (account == null)
        {
            MessageBox.Show("Account ID or password is incorrect.", "Login failed");
            return;
        }
        using var f = new AccountForm(account);
        f.ShowDialog(this);
    }

    void AdminLogin()
    {
        using var login = new LoginForm("Baker Login");
        if (login.ShowDialog(this) != DialogResult.OK) return;

        if (!AppData.Admin.Validate(login.UserName, login.Password))
        {
            MessageBox.Show("Username or password is incorrect.", "Login failed");
            return;
        }
        using var f = new AdminForm();
        f.ShowDialog(this);
    }
}
