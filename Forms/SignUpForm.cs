using OOP_Project_Bakery_POS_System.Assets;
using OOP_Project_Bakery_POS_System.Models;

namespace OliveBranchBakes;

// Creates an optional account. Demonstration only: do not enter a real password.
public class SignUpForm : Form
{
    readonly TextBox txtFirst, txtLast, txtPhone, txtEmail, txtId, txtPass, txtConfirm, txtPrefs;
    readonly ComboBox cboContact;
    readonly Label lblError;

    public AccountCustomer? CreatedAccount { get; private set; }

    public SignUpForm()
    {
        Theme.Setup(this, "Create an Account - Olive Branch Bakes", 500, 640);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        Theme.Lbl(this, "First name", 20, 20, 160);  txtFirst = Theme.Txt(this, 190, 17, 290);
        Theme.Lbl(this, "Last name", 20, 65, 160);   txtLast = Theme.Txt(this, 190, 62, 290);
        Theme.Lbl(this, "Phone", 20, 110, 160);      txtPhone = Theme.Txt(this, 190, 107, 290);
        Theme.Lbl(this, "Email", 20, 155, 160);      txtEmail = Theme.Txt(this, 190, 152, 290);
        Theme.Lbl(this, "Contact me by", 20, 200, 160); cboContact = Theme.EnumCombo<ContactMethod>(this, 190, 197, 290);
        Theme.Lbl(this, "Account ID", 20, 245, 160); txtId = Theme.Txt(this, 190, 242, 290);
        Theme.Lbl(this, "Password", 20, 290, 160);   txtPass = Theme.Txt(this, 190, 287, 290);
        Theme.Lbl(this, "Confirm password", 20, 335, 160); txtConfirm = Theme.Txt(this, 190, 332, 290);
        txtPass.UseSystemPasswordChar = true;
        txtConfirm.UseSystemPasswordChar = true;
        Theme.Lbl(this, "Saved preferences (optional)", 20, 380, 160, 55);
        txtPrefs = Theme.Txt(this, 190, 377, 290, 70, true);

        Theme.Lbl(this, "Demo only: please do not use a real password.", 20, 460, 460);
        lblError = Theme.Lbl(this, "", 20, 495, 460, 30);
        lblError.ForeColor = Theme.Error;

        var create = Theme.Btn(this, "Create Account", 20, 540, 220, 55, Theme.Olive, (s, e) => Create());
        var cancel = Theme.Btn(this, "Cancel", 255, 540, 225, 55, Theme.Brown, (s, e) => DialogResult = DialogResult.Cancel);
        AcceptButton = create;
        CancelButton = cancel;
    }

    void Create()
    {
        if (string.IsNullOrWhiteSpace(txtFirst.Text) || string.IsNullOrWhiteSpace(txtLast.Text))
        { lblError.Text = "Please enter your first and last name."; return; }
        if (string.IsNullOrWhiteSpace(txtPhone.Text))
        { lblError.Text = "Please enter a phone number."; return; }
        if (!txtEmail.Text.Contains('@'))
        { lblError.Text = "Please enter a valid email address."; return; }
        if (string.IsNullOrWhiteSpace(txtId.Text))
        { lblError.Text = "Please choose an account ID."; return; }
        if (AppData.Accounts.Any(a => a.Id.Equals(txtId.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
        { lblError.Text = "That account ID is already taken."; return; }
        if (txtPass.Text.Length < 6)
        { lblError.Text = "Password must be at least 6 characters."; return; }
        if (txtPass.Text != txtConfirm.Text)
        { lblError.Text = "Passwords do not match."; return; }

        CreatedAccount = new AccountCustomer
        {
            Id = txtId.Text.Trim(),
            Password = txtPass.Text,
            FirstName = txtFirst.Text.Trim(),
            LastName = txtLast.Text.Trim(),
            Phone = txtPhone.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            PreferredContact = (ContactMethod)cboContact.SelectedItem!,
            SavedPreferences = txtPrefs.Text.Trim()
        };
        AppData.Accounts.Add(CreatedAccount);
        DialogResult = DialogResult.OK;
    }
}
