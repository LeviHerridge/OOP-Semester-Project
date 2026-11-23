using OOP_Project_Bakery_POS_System.Assets;

namespace OliveBranchBakes;

public class LoginForm : Form
{
    readonly TextBox txtUser, txtPass;

    public string UserName => txtUser.Text.Trim();
    public string Password => txtPass.Text;

    public LoginForm(string title)
    {
        Theme.Setup(this, title, 440, 240);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        Theme.Lbl(this, "User ID", 20, 25, 120);
        txtUser = Theme.Txt(this, 150, 22, 260);
        Theme.Lbl(this, "Password", 20, 75, 120);
        txtPass = Theme.Txt(this, 150, 72, 260);
        txtPass.UseSystemPasswordChar = true;

        var ok = Theme.Btn(this, "Log In", 20, 140, 190, 55, Theme.Olive, (s, e) => DialogResult = DialogResult.OK);
        var cancel = Theme.Btn(this, "Cancel", 220, 140, 190, 55, Theme.Brown, (s, e) => DialogResult = DialogResult.Cancel);
        AcceptButton = ok;
        CancelButton = cancel;
    }
}
