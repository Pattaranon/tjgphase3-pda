namespace UserInterfaces.Pages.Screens
{
    partial class frmWarehouse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWarehouse));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lnkDelete = new System.Windows.Forms.LinkLabel();
            this.lnkNEW = new System.Windows.Forms.LinkLabel();
            this.lnkExit = new System.Windows.Forms.LinkLabel();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.lblBatch = new System.Windows.Forms.Label();
            this.txtSellOrderNo = new System.Windows.Forms.TextBox();
            this.lblPreOrder = new System.Windows.Forms.Label();
            this.lblCustName = new System.Windows.Forms.Label();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblCountByPreOrder = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer();
            this.lblTotalSO = new System.Windows.Forms.Label();
            this.lnkPrint = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pnlLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLine.Location = new System.Drawing.Point(0, 33);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(240, 3);
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
            // lblMenu
            // 
            this.lblMenu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(3, 6);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(234, 21);
            this.lblMenu.Text = "--- 1. Warehouse ---";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.lnkDelete.TabIndex = 8;
            this.lnkDelete.Text = "DEL";
            this.lnkDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkDelete.Click += new System.EventHandler(this.lnkDelete_Click);
            // 
            // lnkNEW
            // 
            this.lnkNEW.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkNEW.ForeColor = System.Drawing.Color.White;
            this.lnkNEW.Location = new System.Drawing.Point(63, 3);
            this.lnkNEW.Name = "lnkNEW";
            this.lnkNEW.Size = new System.Drawing.Size(44, 20);
            this.lnkNEW.TabIndex = 7;
            this.lnkNEW.Text = "NEW";
            this.lnkNEW.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkNEW.Click += new System.EventHandler(this.lnkNEW_Click);
            // 
            // lnkExit
            // 
            this.lnkExit.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkExit.ForeColor = System.Drawing.Color.White;
            this.lnkExit.Location = new System.Drawing.Point(6, 3);
            this.lnkExit.Name = "lnkExit";
            this.lnkExit.Size = new System.Drawing.Size(53, 20);
            this.lnkExit.TabIndex = 6;
            this.lnkExit.Text = "Exit";
            this.lnkExit.Click += new System.EventHandler(this.lklLogout_Click);
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Enabled = false;
            this.txtSerialNumber.Location = new System.Drawing.Point(6, 207);
            this.txtSerialNumber.MaxLength = 18;
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(227, 21);
            this.txtSerialNumber.TabIndex = 86;
            this.txtSerialNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialNumber_KeyDown);
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.Location = new System.Drawing.Point(6, 189);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(138, 15);
            this.lblSerialNumber.Text = "Cylinder S/N :";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Enabled = false;
            this.txtCustomer.Location = new System.Drawing.Point(6, 102);
            this.txtCustomer.MaxLength = 7;
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(227, 21);
            this.txtCustomer.TabIndex = 85;
            this.txtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomer_KeyDown);
            // 
            // lblBatch
            // 
            this.lblBatch.Location = new System.Drawing.Point(6, 84);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(110, 15);
            this.lblBatch.Text = "Customer Code :";
            // 
            // txtSellOrderNo
            // 
            this.txtSellOrderNo.Location = new System.Drawing.Point(6, 59);
            this.txtSellOrderNo.MaxLength = 15;
            this.txtSellOrderNo.Name = "txtSellOrderNo";
            this.txtSellOrderNo.Size = new System.Drawing.Size(227, 21);
            this.txtSellOrderNo.TabIndex = 84;
            this.txtSellOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSellOrderNo_KeyDown);
            // 
            // lblPreOrder
            // 
            this.lblPreOrder.Location = new System.Drawing.Point(6, 41);
            this.lblPreOrder.Name = "lblPreOrder";
            this.lblPreOrder.Size = new System.Drawing.Size(110, 15);
            this.lblPreOrder.Text = "Sale Order No :";
            // 
            // lblCustName
            // 
            this.lblCustName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustName.Location = new System.Drawing.Point(9, 131);
            this.lblCustName.Name = "lblCustName";
            this.lblCustName.Size = new System.Drawing.Size(223, 53);
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(186, 273);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 97;
            this.batt.TabStop = false;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(185, 271);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(49, 19);
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(7, 273);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(40, 15);
            this.lblResult.Text = "RecT :";
            // 
            // lblCountByPreOrder
            // 
            this.lblCountByPreOrder.Location = new System.Drawing.Point(53, 273);
            this.lblCountByPreOrder.Name = "lblCountByPreOrder";
            this.lblCountByPreOrder.Size = new System.Drawing.Size(25, 15);
            this.lblCountByPreOrder.Text = "0";
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMessage.BackColor = System.Drawing.Color.Red;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(4, 234);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(229, 27);
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 6000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblTotalSO
            // 
            this.lblTotalSO.Location = new System.Drawing.Point(85, 273);
            this.lblTotalSO.Name = "lblTotalSO";
            this.lblTotalSO.Size = new System.Drawing.Size(40, 15);
            this.lblTotalSO.Text = "SO :";
            // 
            // lnkPrint
            // 
            this.lnkPrint.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkPrint.ForeColor = System.Drawing.Color.White;
            this.lnkPrint.Location = new System.Drawing.Point(192, 2);
            this.lnkPrint.Name = "lnkPrint";
            this.lnkPrint.Size = new System.Drawing.Size(42, 20);
            this.lnkPrint.TabIndex = 9;
            this.lnkPrint.Text = "Print";
            this.lnkPrint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkPrint.Click += new System.EventHandler(this.lnkPrint_Click);
            // 
            // frmWarehouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.lblTotalSO);
            this.Controls.Add(this.batt);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblCountByPreOrder);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblCustName);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.lblSerialNumber);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.lblBatch);
            this.Controls.Add(this.txtSellOrderNo);
            this.Controls.Add(this.lblPreOrder);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmWarehouse";
            this.Text = "frmWarehouse";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmWarehouse_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmWarehouse_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel lnkNEW;
        private System.Windows.Forms.LinkLabel lnkExit;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.TextBox txtSellOrderNo;
        private System.Windows.Forms.Label lblPreOrder;
        private System.Windows.Forms.Label lblCustName;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblCountByPreOrder;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblTotalSO;
        private System.Windows.Forms.LinkLabel lnkDelete;
        private System.Windows.Forms.LinkLabel lnkPrint;
    }
}