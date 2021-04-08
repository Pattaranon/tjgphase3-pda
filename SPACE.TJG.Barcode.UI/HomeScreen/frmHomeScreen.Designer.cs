namespace SPACE.TJG.Barcode.UI.HomeScreen
{
    partial class frmHomeScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHomeScreen));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lblMenu = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.lklLogout = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblActivateLicences = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblSetting = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer();
            this.lblTimeRun = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.llbOtherScan = new System.Windows.Forms.LinkLabel();
            this.llbScanBarcode = new System.Windows.Forms.LinkLabel();
            this.llbViewData = new System.Windows.Forms.LinkLabel();
            this.llbDownloadCustomer = new System.Windows.Forms.LinkLabel();
            this.llbDelivery = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.llbDeleteData = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.llbDownloadSAP = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMenu
            // 
            this.lblMenu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(3, 6);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(234, 21);
            this.lblMenu.Text = "--- Home Screen ---";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pnlLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLine.Location = new System.Drawing.Point(0, 33);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(240, 3);
            // 
            // lklLogout
            // 
            this.lklLogout.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lklLogout.ForeColor = System.Drawing.Color.White;
            this.lklLogout.Location = new System.Drawing.Point(178, 5);
            this.lklLogout.Name = "lklLogout";
            this.lklLogout.Size = new System.Drawing.Size(59, 17);
            this.lklLogout.TabIndex = 0;
            this.lklLogout.Text = "Logout";
            this.lklLogout.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lklLogout.Click += new System.EventHandler(this.lklLogout_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.lblActivateLicences);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.lblSetting);
            this.panel2.Controls.Add(this.lklLogout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 294);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 26);
            // 
            // lblActivateLicences
            // 
            this.lblActivateLicences.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblActivateLicences.ForeColor = System.Drawing.Color.White;
            this.lblActivateLicences.Location = new System.Drawing.Point(3, 5);
            this.lblActivateLicences.Name = "lblActivateLicences";
            this.lblActivateLicences.Size = new System.Drawing.Size(114, 17);
            this.lblActivateLicences.TabIndex = 48;
            this.lblActivateLicences.Text = "Activate Licences";
            this.lblActivateLicences.Click += new System.EventHandler(this.lblActivateLicences_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(0, 0);
            this.button1.TabIndex = 47;
            this.button1.Text = "button1";
            this.button1.Visible = false;
            // 
            // lblSetting
            // 
            this.lblSetting.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblSetting.ForeColor = System.Drawing.Color.White;
            this.lblSetting.Location = new System.Drawing.Point(3, 5);
            this.lblSetting.Name = "lblSetting";
            this.lblSetting.Size = new System.Drawing.Size(106, 17);
            this.lblSetting.TabIndex = 0;
            this.lblSetting.Text = "Settings";
            this.lblSetting.Visible = false;
            this.lblSetting.Click += new System.EventHandler(this.lblSetting_Click);
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
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblTimeRun
            // 
            this.lblTimeRun.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lblTimeRun.Location = new System.Drawing.Point(98, 233);
            this.lblTimeRun.Name = "lblTimeRun";
            this.lblTimeRun.Size = new System.Drawing.Size(76, 17);
            this.lblTimeRun.Text = "HH:mm:ss";
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lblDate.Location = new System.Drawing.Point(16, 232);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(76, 18);
            this.lblDate.Text = "dd/MM/yy";
            // 
            // llbOtherScan
            // 
            this.llbOtherScan.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.llbOtherScan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbOtherScan.Location = new System.Drawing.Point(14, 98);
            this.llbOtherScan.Name = "llbOtherScan";
            this.llbOtherScan.Size = new System.Drawing.Size(144, 20);
            this.llbOtherScan.TabIndex = 2;
            this.llbOtherScan.Text = "3. New Barcode";
            this.llbOtherScan.Click += new System.EventHandler(this.llbOtherScan_Click);
            // 
            // llbScanBarcode
            // 
            this.llbScanBarcode.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.llbScanBarcode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbScanBarcode.Location = new System.Drawing.Point(14, 44);
            this.llbScanBarcode.Name = "llbScanBarcode";
            this.llbScanBarcode.Size = new System.Drawing.Size(136, 21);
            this.llbScanBarcode.TabIndex = 0;
            this.llbScanBarcode.Text = "1. Warehouse";
            this.llbScanBarcode.Click += new System.EventHandler(this.llbScanBarcode_Click);
            // 
            // llbViewData
            // 
            this.llbViewData.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.llbViewData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbViewData.Location = new System.Drawing.Point(14, 124);
            this.llbViewData.Name = "llbViewData";
            this.llbViewData.Size = new System.Drawing.Size(136, 20);
            this.llbViewData.TabIndex = 3;
            this.llbViewData.Text = "4. View Data";
            this.llbViewData.Click += new System.EventHandler(this.llbViewData_Click);
            // 
            // llbDownloadCustomer
            // 
            this.llbDownloadCustomer.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.llbDownloadCustomer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbDownloadCustomer.Location = new System.Drawing.Point(14, 176);
            this.llbDownloadCustomer.Name = "llbDownloadCustomer";
            this.llbDownloadCustomer.Size = new System.Drawing.Size(206, 20);
            this.llbDownloadCustomer.TabIndex = 5;
            this.llbDownloadCustomer.Text = "6. Download Customer";
            this.llbDownloadCustomer.Click += new System.EventHandler(this.llbDownloadCustomer_Click);
            // 
            // llbDelivery
            // 
            this.llbDelivery.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.llbDelivery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbDelivery.Location = new System.Drawing.Point(14, 71);
            this.llbDelivery.Name = "llbDelivery";
            this.llbDelivery.Size = new System.Drawing.Size(119, 21);
            this.llbDelivery.TabIndex = 1;
            this.llbDelivery.Text = "2. Deliveries";
            this.llbDelivery.Click += new System.EventHandler(this.llbDelivery_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblVersion.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblVersion.Location = new System.Drawing.Point(150, 254);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(89, 13);
            this.lblVersion.Text = "Version : x.x.x.x";
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(5, 273);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(49, 19);
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(6, 275);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 31;
            this.batt.TabStop = false;
            // 
            // llbDeleteData
            // 
            this.llbDeleteData.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.llbDeleteData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbDeleteData.Location = new System.Drawing.Point(14, 150);
            this.llbDeleteData.Name = "llbDeleteData";
            this.llbDeleteData.Size = new System.Drawing.Size(144, 20);
            this.llbDeleteData.TabIndex = 4;
            this.llbDeleteData.Text = "5. Delete Data";
            this.llbDeleteData.Click += new System.EventHandler(this.llbDeleteData_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(61, 280);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 11);
            this.label1.Text = "copyright©2016. Thai-Japan Gas Co.,Ltd.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // llbDownloadSAP
            // 
            this.llbDownloadSAP.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.llbDownloadSAP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbDownloadSAP.Location = new System.Drawing.Point(14, 202);
            this.llbDownloadSAP.Name = "llbDownloadSAP";
            this.llbDownloadSAP.Size = new System.Drawing.Size(206, 21);
            this.llbDownloadSAP.TabIndex = 39;
            this.llbDownloadSAP.Text = "7. Download SAP";
            this.llbDownloadSAP.Click += new System.EventHandler(this.llbDownloadSAP_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(61, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 11);
            this.label2.Text = "Developed by Space";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmHomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.llbDownloadSAP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.batt);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblTimeRun);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.llbOtherScan);
            this.Controls.Add(this.llbScanBarcode);
            this.Controls.Add(this.llbDeleteData);
            this.Controls.Add(this.llbViewData);
            this.Controls.Add(this.llbDownloadCustomer);
            this.Controls.Add(this.llbDelivery);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmHomeScreen";
            this.Text = "frmMainMenu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmHomeScreen_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmHomeScreen_KeyUp);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.LinkLabel lklLogout;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblTimeRun;
        private System.Windows.Forms.Label lblDate;
        internal System.Windows.Forms.LinkLabel llbOtherScan;
        internal System.Windows.Forms.LinkLabel llbScanBarcode;
        internal System.Windows.Forms.LinkLabel llbViewData;
        internal System.Windows.Forms.LinkLabel llbDownloadCustomer;
        internal System.Windows.Forms.LinkLabel llbDelivery;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblVersion;
        internal System.Windows.Forms.PictureBox pic;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        private System.Windows.Forms.LinkLabel lblSetting;
        private System.Windows.Forms.LinkLabel lblActivateLicences;
        internal System.Windows.Forms.LinkLabel llbDeleteData;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.LinkLabel llbDownloadSAP;
        private System.Windows.Forms.Label label2;
    }
}