namespace SPACE.TJG.Barcode.UI.Pages.Menus
{
    partial class frmMenuDeleteByFunctions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuDeleteByFunctions));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.llbDeleteDelivery = new System.Windows.Forms.LinkLabel();
            this.lklLogout = new System.Windows.Forms.LinkLabel();
            this.llbDeleteAll = new System.Windows.Forms.LinkLabel();
            this.llbDeleteOtherScan = new System.Windows.Forms.LinkLabel();
            this.llbDeleteWarehouse = new System.Windows.Forms.LinkLabel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(9, 270);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 0;
            this.batt.TabStop = false;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(8, 268);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(49, 19);
            // 
            // llbDeleteDelivery
            // 
            this.llbDeleteDelivery.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbDeleteDelivery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbDeleteDelivery.Location = new System.Drawing.Point(21, 100);
            this.llbDeleteDelivery.Name = "llbDeleteDelivery";
            this.llbDeleteDelivery.Size = new System.Drawing.Size(199, 20);
            this.llbDeleteDelivery.TabIndex = 3;
            this.llbDeleteDelivery.Text = "3. Delete Delivery";
            this.llbDeleteDelivery.Click += new System.EventHandler(this.llbDeleteDelivery_Click);
            // 
            // lklLogout
            // 
            this.lklLogout.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lklLogout.ForeColor = System.Drawing.Color.White;
            this.lklLogout.Location = new System.Drawing.Point(3, 3);
            this.lklLogout.Name = "lklLogout";
            this.lklLogout.Size = new System.Drawing.Size(106, 20);
            this.lklLogout.TabIndex = 0;
            this.lklLogout.Text = "Exit";
            this.lklLogout.Click += new System.EventHandler(this.lklLogout_Click);
            // 
            // llbDeleteAll
            // 
            this.llbDeleteAll.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbDeleteAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbDeleteAll.Location = new System.Drawing.Point(21, 45);
            this.llbDeleteAll.Name = "llbDeleteAll";
            this.llbDeleteAll.Size = new System.Drawing.Size(199, 20);
            this.llbDeleteAll.TabIndex = 1;
            this.llbDeleteAll.Text = "1. Delete All";
            this.llbDeleteAll.Click += new System.EventHandler(this.llbDeleteAll_Click);
            // 
            // llbDeleteOtherScan
            // 
            this.llbDeleteOtherScan.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbDeleteOtherScan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbDeleteOtherScan.Location = new System.Drawing.Point(21, 128);
            this.llbDeleteOtherScan.Name = "llbDeleteOtherScan";
            this.llbDeleteOtherScan.Size = new System.Drawing.Size(199, 20);
            this.llbDeleteOtherScan.TabIndex = 4;
            this.llbDeleteOtherScan.Text = "4. Delete New Barcode";
            this.llbDeleteOtherScan.Click += new System.EventHandler(this.llbDeleteOtherScan_Click);
            // 
            // llbDeleteWarehouse
            // 
            this.llbDeleteWarehouse.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbDeleteWarehouse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbDeleteWarehouse.Location = new System.Drawing.Point(21, 72);
            this.llbDeleteWarehouse.Name = "llbDeleteWarehouse";
            this.llbDeleteWarehouse.Size = new System.Drawing.Size(199, 20);
            this.llbDeleteWarehouse.TabIndex = 2;
            this.llbDeleteWarehouse.Text = "2. Delete W/H";
            this.llbDeleteWarehouse.Click += new System.EventHandler(this.llbDeleteWarehouse_Click);
            // 
            // lblMenu
            // 
            this.lblMenu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(3, 6);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(234, 21);
            this.lblMenu.Text = "--- 5. Delete Data ---";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.lklLogout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 294);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 26);
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
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 4000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMessage.BackColor = System.Drawing.Color.Red;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(6, 229);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(229, 27);
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmMenuDeleteByFunctions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.batt);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.llbDeleteDelivery);
            this.Controls.Add(this.llbDeleteAll);
            this.Controls.Add(this.llbDeleteOtherScan);
            this.Controls.Add(this.llbDeleteWarehouse);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmMenuDeleteByFunctions";
            this.Text = "frmMenuDeleteByFunctions";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMenuDeleteByFunctions_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMenuDeleteByFunctions_KeyUp);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
        internal System.Windows.Forms.LinkLabel llbDeleteDelivery;
        private System.Windows.Forms.LinkLabel lklLogout;
        internal System.Windows.Forms.LinkLabel llbDeleteAll;
        internal System.Windows.Forms.LinkLabel llbDeleteOtherScan;
        internal System.Windows.Forms.LinkLabel llbDeleteWarehouse;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblMessage;
    }
}