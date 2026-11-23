namespace OOP_Project_Bakery_POS_System.Assets;

// Shared colors and control helpers so every form looks the same.
static class Theme
{
    public static readonly Color Cream = Color.FromArgb(255, 248, 231);
    public static readonly Color Olive = Color.FromArgb(98, 117, 52);
    public static readonly Color Brown = Color.FromArgb(111, 78, 55);
    public static readonly Color Honey = Color.FromArgb(227, 168, 44);
    public static readonly Color Error = Color.FromArgb(176, 42, 42);

    public static readonly Font Body = new("Segoe UI", 12f);
    public static readonly Font Title = new("Segoe UI", 22f, FontStyle.Bold);

    public static void Setup(Form f, string title, int width, int height)
    {
        f.Text = title;
        f.ClientSize = new Size(width, height);
        f.BackColor = Cream;
        f.ForeColor = Brown;
        f.Font = Body;
        f.StartPosition = FormStartPosition.CenterScreen;
    }

    public static Label Lbl(Control parent, string text, int x, int y, int w = 200, int h = 28, bool title = false)
    {
        var l = new Label { Text = text, Left = x, Top = y, Width = w, Height = h, AutoSize = false };
        if (title) { l.Font = Title; l.ForeColor = Olive; }
        parent.Controls.Add(l);
        return l;
    }

    public static TextBox Txt(Control parent, int x, int y, int w = 290, int h = 32, bool multi = false)
    {
        var t = new TextBox { Left = x, Top = y, Width = w, Height = h, Multiline = multi, BackColor = Color.White };
        if (multi) t.ScrollBars = ScrollBars.Vertical;
        parent.Controls.Add(t);
        return t;
    }

    public static ComboBox Combo(Control parent, int x, int y, int w, params object[] items)
    {
        var c = new ComboBox { Left = x, Top = y, Width = w, DropDownStyle = ComboBoxStyle.DropDownList };
        c.Items.AddRange(items);
        parent.Controls.Add(c);
        return c;
    }

    public static ComboBox EnumCombo<T>(Control parent, int x, int y, int w) where T : struct, Enum
    {
        var c = Combo(parent, x, y, w, Enum.GetValues<T>().Cast<object>().ToArray());
        c.SelectedIndex = 0;
        return c;
    }

    public static Button Btn(Control parent, string text, int x, int y, int w, int h, Color back, EventHandler click)
    {
        var b = new Button
        {
            Text = text, Left = x, Top = y, Width = w, Height = h,
            BackColor = back, ForeColor = back == Honey ? Brown : Color.White,
            FlatStyle = FlatStyle.Flat, Font = new Font(Body, FontStyle.Bold)
        };
        b.FlatAppearance.BorderSize = 0;
        b.Click += click;
        parent.Controls.Add(b);
        return b;
    }
}
