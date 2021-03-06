namespace UserInterfaces.Pages.Screens
{
    partial class frmDelivery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDelivery));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.txtInputText = new System.Windows.Forms.TextBox();
            this.lblCustName = new System.Windows.Forms.Label();
            this.lblCountByCustomer = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtSelect = new System.Windows.Forms.TextBox();
            this.lblSelect = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lnkPrint = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lnkDelete = new System.Windows.Forms.LinkLabel();
            this.lnkNEW = new System.Windows.Forms.LinkLabel();
            this.lnkExit = new System.Windows.Forms.LinkLabel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbFactory = new System.Windows.Forms.ComboBox();
            this.txtVehicle = new System.Windows.Forms.TextBox();
            this.lblVehicle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lnkClean = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(187, 272);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 76;
            this.batt.TabStop = false;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(186, 270);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(49, 19);
            // 
            // txtInputText
            // 
            this.txtInputText.Enabled = false;
            this.txtInputText.Location = new System.Drawing.Point(91, 212);
            this.txtInputText.Name = "txtInputText";
            this.txtInputText.Size = new System.Drawing.Size(144, 21);
            this.txtInputText.TabIndex = 75;
            this.txtInputText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInputText_KeyDown);
            // 
            // lblCustName
            // 
            this.lblCustName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustName.Location = new System.Drawing.Point(9, 99);
            this.lblCustName.Name = "lblCustName";
            this.lblCustName.Size = new System.Drawing.Size(223, 57);
            // 
            // lblCountByCustomer
            // 
            this.lblCountByCustomer.Location = new System.Drawing.Point(48, 273);
            this.lblCountByCustomer.Name = "lblCountByCustomer";
            this.lblCountByCustomer.Size = new System.Drawing.Size(25, 15);
            this.lblCountByCustomer.Text = "0";
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(9, 272);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(85, 15);
            this.lblResult.Text = "RecT :";
            // 
            // txtSelect
            // 
            this.txtSelect.Enabled = false;
            this.txtSelect.Location = new System.Drawing.Point(127, 187);
            this.txtSelect.MaxLength = 7;
            this.txtSelect.Name = "txtSelect";
            this.txtSelect.Size = new System.Drawing.Size(26, 21);
            this.txtSelect.TabIndex = 74;
            this.txtSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSelect_KeyDown);
            // 
            // lblSelect
            // 
            this.lblSelect.Location = new System.Drawing.Point(9, 190);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(112, 15);
            this.lblSelect.Text = "Empty[0] / Full[9] :";
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pnlLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLine.Location = new System.Drawing.Point(0, 33);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(240, 3);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 6000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblMenu
            // 
            this.lblMenu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(3, 6);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(234, 21);
            this.lblMenu.Text = "--- 2. Delivery Screen ---";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.lblMenu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 33);
            // 
            // lnkPrint
            // 
            this.lnkPrint.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkPrint.ForeColor = System.Drawing.Color.White;
            this.lnkPrint.Location = new System.Drawing.Point(193, 3);
            this.lnkPrint.Name = "lnkPrint";
            this.lnkPrint.Size = new System.Drawing.Size(42, 20);
            this.lnkPrint.TabIndex = 4;
            this.lnkPrint.Text = "Print";
            this.lnkPrint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkPrint.Click += new System.EventHandler(this.lnkPrint_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.lnkPrint);
            this.panel2.Controls.Add(this.lnkDelete);
            this.panel2.Controls.Add(this.lnkNEW);
            this.panel2.Controls.Add(this.lnkExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 294);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 26);
            // 
            // lnkDelete
            // 
            this.lnkDelete.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkDelete.ForeColor = System.Drawing.Color.White;
            this.lnkDelete.Location = new System.Drawing.Point(135, 3);
            this.lnkDelete.Name = "lnkDelete";
            this.lnkDelete.Size = new System.Drawing.Size(40, 20);
            this.lnkDelete.TabIndex = 5;
            this.lnkDelete.Text = "DEL";
            this.lnkDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkDelete.Click += new System.EventHandler(this.lnkDelete_Click);
            // 
            // lnkNEW
            // 
            this.lnkNEW.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkNEW.ForeColor = System.Drawing.Color.White;
            this.lnkNEW.Location = new System.Drawing.Point(64, 3);
            this.lnkNEW.Name = "lnkNEW";
            this.lnkNEW.Size = new System.Drawing.Size(44, 20);
            this.lnkNEW.TabIndex = 2;
            this.lnkNEW.Text = "NEW";
            this.lnkNEW.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkNEW.Click += new System.EventHandler(this.lnkNEW_Click);
            // 
            // lnkExit
            // 
            this.lnkExit.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkExit.ForeColor = System.Drawing.Color.White;
            this.lnkExit.Location = new System.Drawing.Point(5, 3);
            this.lnkExit.Name = "lnkExit";
            this.lnkExit.Size = new System.Drawing.Size(45, 20);
            this.lnkExit.TabIndex = 3;
            this.lnkExit.Text = "Exit";
            this.lnkExit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkExit.Click += new System.EventHandler(this.lnkExit_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMessage.BackColor = System.Drawing.Color.Red;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(6, 239);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(229, 27);
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtCustomer.Location = new System.Drawing.Point(84, 68);
            this.txtCustomer.MaxLength = 7;
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(115, 26);
            this.txtCustomer.TabIndex = 71;
            this.txtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomer_KeyDown);
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(9, 74);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(71, 15);
            this.lblCustomer.Text = "Cust Code :";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.Text = "Factory :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbbFactory
            // 
            this.cbbFactory.Location = new System.Drawing.Point(84, 42);
            this.cbbFactory.Name = "cbbFactory";
            this.cbbFactory.Size = new System.Drawing.Size(147, 22);
            this.cbbFactory.TabIndex = 90;
            this.cbbFactory.SelectedIndexChanged += new System.EventHandler(this.cbbFactory_SelectedIndexChanged);
            // 
            // txtVehicle
            // 
            this.txtVehicle.Enabled = false;
            this.txtVehicle.Location = new System.Drawing.Point(65, 162);
            this.txtVehicle.MaxLength = 7;
            this.txtVehicle.Name = "txtVehicle";
            this.txtVehicle.Size = new System.Drawing.Size(139, 21);
            this.txtVehicle.TabIndex = 103;
            this.txtVehicle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVehicle_KeyDown);
            // 
            // lblVehicle
            // 
            this.lblVehicle.Location = new System.Drawing.Point(9, 164);
            this.lblVehicle.Name = "lblVehicle";
            this.lblVehicle.Size = new System.Drawing.Size(51, 15);
            this.lblVehicle.Text = "Vehicle :";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.Text = "Cylinder S/N :";
            // 
            // lnkClean
            // 
            this.lnkClean.ForeColor = System.Drawing.Color.Black;
            this.lnkClean.Location = new System.Drawing.Point(157, 190);
            this.lnkClean.Name = "lnkClean";
            this.lnkClean.Size = new System.Drawing.Size(48, 16);
            this.lnkClean.TabIndex = 116;
            this.lnkClean.Text = "Clean";
            this.lnkClean.Click += new System.EventHandler(this.lnkClean_Click);
            // 
            // frmDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.lnkClean);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVehicle);
            this.Controls.Add(this.lblVehicle);
            this.Controls.Add(this.cbbFactory);
            this.Controls.Add(this.batt);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.txtInputText);
            this.Controls.Add(this.lblCustName);
            this.Controls.Add(this.lblCountByCustomer);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtSelect);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCustomer);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmDelivery";
            this.Text = "frmWarehouse";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDelivery_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmDelivery_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.TextBox txtInputText;
        private System.Windows.Forms.Label lblCustName;
        private System.Windows.Forms.Label lblCountByCustomer;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtSelect;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Timer timer;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lnkPrint;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel lnkDelete;
        private System.Windows.Forms.LinkLabel lnkNEW;
        private System.Windows.Forms.LinkLabel lnkExit;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbFactory;
        private System.Windows.Forms.TextBox txtVehicle;
        private System.Windows.Forms.Label lblVehicle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkClean;
    }
}