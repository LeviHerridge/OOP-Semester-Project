namespace OOP_Project_Bakery_POS_System
{
    partial class Dashboard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            titleLabel = new Label();
            buttonOrder = new Button();
            buttonCustomer = new Button();
            buttonPickup = new Button();
            buttonAdmin = new Button();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(26, 22);
            titleLabel.Margin = new Padding(6, 0, 6, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(582, 37);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Test Version 1 of Olive Branch Bakes POS system";
            // 
            // buttonOrder
            // 
            buttonOrder.Location = new Point(41, 145);
            buttonOrder.Name = "buttonOrder";
            buttonOrder.Size = new Size(214, 107);
            buttonOrder.TabIndex = 1;
            buttonOrder.Text = "New Order";
            buttonOrder.UseVisualStyleBackColor = true;
            buttonOrder.Click += buttonOrder_Click;
            // 
            // buttonCustomer
            // 
            buttonCustomer.Location = new Point(261, 145);
            buttonCustomer.Name = "buttonCustomer";
            buttonCustomer.Size = new Size(209, 107);
            buttonCustomer.TabIndex = 2;
            buttonCustomer.Text = "Lookup Customer";
            buttonCustomer.UseVisualStyleBackColor = true;
            buttonCustomer.Click += buttonCustomer_Click;
            // 
            // buttonPickup
            // 
            buttonPickup.Location = new Point(476, 145);
            buttonPickup.Name = "buttonPickup";
            buttonPickup.Size = new Size(215, 107);
            buttonPickup.TabIndex = 3;
            buttonPickup.Text = "Pickup List";
            buttonPickup.UseVisualStyleBackColor = true;
            buttonPickup.Click += buttonPickup_Click;
            // 
            // buttonAdmin
            // 
            buttonAdmin.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonAdmin.Location = new Point(535, 405);
            buttonAdmin.Name = "buttonAdmin";
            buttonAdmin.Size = new Size(171, 56);
            buttonAdmin.TabIndex = 4;
            buttonAdmin.Text = "Admin Login";
            buttonAdmin.UseVisualStyleBackColor = true;
            buttonAdmin.Click += buttonAdmin_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(718, 473);
            Controls.Add(buttonAdmin);
            Controls.Add(buttonPickup);
            Controls.Add(buttonCustomer);
            Controls.Add(buttonOrder);
            Controls.Add(titleLabel);
            Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(6, 7, 6, 7);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bakery Dashboard";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label titleLabel;
        private Button buttonOrder;
        private Button buttonCustomer;
        private Button buttonPickup;
        private Button buttonAdmin;
    }
}
