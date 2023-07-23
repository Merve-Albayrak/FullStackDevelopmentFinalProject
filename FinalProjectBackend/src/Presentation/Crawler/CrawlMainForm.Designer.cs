namespace Crawler
{
    partial class CrawlMainForm
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
            txtProductCount = new TextBox();
            lblSelectProduct = new Label();
            lblEnterTheNumber = new Label();
            cmbBoxProductType = new ComboBox();
            btnStart = new Button();
            lblWelcome = new Label();
            cmbBoxAllOrCount = new ComboBox();
            lblAllOrCount = new Label();
            lblError = new Label();
            lblEr = new Label();
            SuspendLayout();
            // 
            // txtProductCount
            // 
            txtProductCount.Location = new Point(282, 186);
            txtProductCount.Margin = new Padding(3, 2, 3, 2);
            txtProductCount.Name = "txtProductCount";
            txtProductCount.Size = new Size(133, 23);
            txtProductCount.TabIndex = 1;
            txtProductCount.TextChanged += txtProductCount_TextChanged;
            // 
            // lblSelectProduct
            // 
            lblSelectProduct.AutoSize = true;
            lblSelectProduct.ForeColor = SystemColors.ControlLightLight;
            lblSelectProduct.Location = new Point(185, 119);
            lblSelectProduct.Name = "lblSelectProduct";
            lblSelectProduct.Size = new Size(91, 15);
            lblSelectProduct.TabIndex = 2;
            lblSelectProduct.Text = "Select Products:";
            // 
            // lblEnterTheNumber
            // 
            lblEnterTheNumber.AutoSize = true;
            lblEnterTheNumber.ForeColor = SystemColors.ControlLightLight;
            lblEnterTheNumber.Location = new Point(172, 189);
            lblEnterTheNumber.Name = "lblEnterTheNumber";
            lblEnterTheNumber.Size = new Size(104, 15);
            lblEnterTheNumber.TabIndex = 3;
            lblEnterTheNumber.Text = "Enter the Number:";
            lblEnterTheNumber.Click += label2_Click;
            // 
            // cmbBoxProductType
            // 
            cmbBoxProductType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBoxProductType.FormattingEnabled = true;
            cmbBoxProductType.Items.AddRange(new object[] { "On Sale", "All", "Regular Prices" });
            cmbBoxProductType.Location = new Point(282, 116);
            cmbBoxProductType.Margin = new Padding(3, 2, 3, 2);
            cmbBoxProductType.Name = "cmbBoxProductType";
            cmbBoxProductType.Size = new Size(133, 23);
            cmbBoxProductType.TabIndex = 4;
            cmbBoxProductType.SelectedIndexChanged += cmbBoxProductType_SelectedIndexChanged;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.DarkTurquoise;
            btnStart.ForeColor = Color.DimGray;
            btnStart.Location = new Point(282, 226);
            btnStart.Margin = new Padding(3, 2, 3, 2);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(124, 33);
            btnStart.TabIndex = 5;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblWelcome.ForeColor = SystemColors.ControlLightLight;
            lblWelcome.Location = new Point(220, 43);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(229, 25);
            lblWelcome.TabIndex = 6;
            lblWelcome.Text = "Welcome To The Crawler";
            // 
            // cmbBoxAllOrCount
            // 
            cmbBoxAllOrCount.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBoxAllOrCount.FormattingEnabled = true;
            cmbBoxAllOrCount.Items.AddRange(new object[] { "All", "Enter the Number" });
            cmbBoxAllOrCount.Location = new Point(282, 151);
            cmbBoxAllOrCount.Margin = new Padding(3, 2, 3, 2);
            cmbBoxAllOrCount.Name = "cmbBoxAllOrCount";
            cmbBoxAllOrCount.Size = new Size(133, 23);
            cmbBoxAllOrCount.TabIndex = 7;
            cmbBoxAllOrCount.SelectedIndexChanged += cmbBoxAllOrCount_SelectedIndexChanged;
            // 
            // lblAllOrCount
            // 
            lblAllOrCount.AutoSize = true;
            lblAllOrCount.ForeColor = SystemColors.ControlLightLight;
            lblAllOrCount.Location = new Point(147, 154);
            lblAllOrCount.Name = "lblAllOrCount";
            lblAllOrCount.Size = new Size(129, 15);
            lblAllOrCount.TabIndex = 8;
            lblAllOrCount.Text = "All or Enter the Count?:";
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Location = new Point(282, 281);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 9;
            // 
            // lblEr
            // 
            lblEr.AutoSize = true;
            lblEr.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblEr.ForeColor = Color.OrangeRed;
            lblEr.Location = new Point(239, 279);
            lblEr.Name = "lblEr";
            lblEr.Size = new Size(43, 17);
            lblEr.TabIndex = 10;
            lblEr.Text = "Error:";
            // 
            // CrawlMainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkCyan;
            ClientSize = new Size(700, 338);
            Controls.Add(lblEr);
            Controls.Add(lblError);
            Controls.Add(lblAllOrCount);
            Controls.Add(cmbBoxAllOrCount);
            Controls.Add(lblWelcome);
            Controls.Add(btnStart);
            Controls.Add(cmbBoxProductType);
            Controls.Add(lblEnterTheNumber);
            Controls.Add(lblSelectProduct);
            Controls.Add(txtProductCount);
            Margin = new Padding(3, 2, 3, 2);
            Name = "CrawlMainForm";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtProductCount;
        private Label lblSelectProduct;
        private Label lblEnterTheNumber;
        private ComboBox cmbBoxProductType;
        private Button btnStart;
        private Label lblWelcome;
        private ComboBox cmbBoxAllOrCount;
        private Label lblAllOrCount;
        private Label lblError;
        private Label lblEr;
    }
}