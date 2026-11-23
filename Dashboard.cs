using System.Reflection.Emit;

namespace OOP_Project_Bakery_POS_System
{
    public partial class Dashboard : Form
    {
        enum ThemeColor
        {
            Primary,
            Secondary,
            Tertiary,
            Text
        }

        Dictionary<ThemeColor, Color> Nature = new Dictionary<ThemeColor, Color>();

        public Dashboard()
        {
            InitializeComponent();
            Nature[ThemeColor.Primary] = Color.DarkSeaGreen;
            Nature[ThemeColor.Secondary] = Color.DarkOliveGreen;
            Nature[ThemeColor.Tertiary] = Color.Tan;
            Nature[ThemeColor.Text] = Color.Honeydew;
            ChangeTheme(Nature[ThemeColor.Primary], Nature[ThemeColor.Secondary], Nature[ThemeColor.Tertiary]);
            ChangeTextColor(Nature[ThemeColor.Text]);
        }

        private void ChangeTheme(Color primaryColor, Color secondaryColor, Color tertiaryColor)
        {
            // Change background color of button.
            buttonOrder.BackColor = primaryColor;
            buttonCustomer.BackColor = secondaryColor;
            buttonPickup.BackColor = secondaryColor;
            buttonAdmin.BackColor = primaryColor;
            this.BackColor = tertiaryColor;
        }

        private void ChangeTextColor(Color textColor)
        {
            // Change color of text
            buttonOrder.ForeColor = textColor;
            buttonCustomer.ForeColor = textColor;
            buttonPickup.ForeColor = textColor;
            buttonAdmin.ForeColor = textColor;
            titleLabel.ForeColor = textColor;
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {

        }

        private void buttonCustomer_Click(object sender, EventArgs e)
        {

        }

        private void buttonPickup_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdmin_Click(object sender, EventArgs e)
        {

        }
    }
}
