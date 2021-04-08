namespace SPACE.TJG.Barcode.UI.Forms
{
    partial class frmGIRefDelivery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGIRefDelivery));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lblResult = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtDO = new System.Windows.Forms.TextBox();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.lblDO = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lnkPrint = new System.Windows.Forms.LinkLabel();
            this.lnkDelete = new System.Windows.Forms.LinkLabel();
            this.lnkNEW = new System.Windows.Forms.LinkLabel();
            this.lnkExit = new System.Windows.Forms.LinkLabel();
            this.lblU = new System.Windows.Forms.Label();
            this.lblCountByDO = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.btnUploadData = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(9, 245);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(40, 15);
            this.lblResult.Text = "RecT :";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMessage.BackColor = System.Drawing.Color.Red;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(6, 213);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(229, 27);
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtDO
            // 
            this.txtDO.Location = new System.Drawing.Point(9, 63);
            this.txtDO.MaxLength = 15;
            this.txtDO.Name = "txtDO";
            this.txtDO.Size = new System.Drawing.Size(139, 21);
            this.txtDO.TabIndex = 41;
            this.txtDO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDO_KeyDown);
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
            this.lblMenu.Text = "--- 1.1.2 GI Ref. Delivery ---";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDO
            // 
            this.lblDO.Location = new System.Drawing.Point(9, 45);
            this.lblDO.Name = "lblDO";
            this.lblDO.Size = new System.Drawing.Size(34, 15);
            this.lblDO.Text = "DO :";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.lnkPrint);
            this.panel2.Controls.Add(this.lnkDelete);
            this.panel2.Controls.Add(this.lnkNEW);
            this.panel2.Controls.Add(this.lnkExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 268);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 26);
            // 
            // lnkPrint
            // 
            this.lnkPrint.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkPrint.ForeColor = System.Drawing.Color.White;
            this.lnkPrint.Location = new System.Drawing.Point(192, 2);
            this.lnkPrint.Name = "lnkPrint";
            this.lnkPrint.Size = new System.Drawing.Size(42, 20);
            this.lnkPrint.TabIndex = 1;
            this.lnkPrint.Text = "Print";
            this.lnkPrint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkPrint.Click += new System.EventHandler(this.lnkPrint_Click);
            // 
            // lnkDelete
            // 
            this.lnkDelete.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkDelete.ForeColor = System.Drawing.Color.White;
            this.lnkDelete.Location = new System.Drawing.Point(134, 2);
            this.lnkDelete.Name = "lnkDelete";
            this.lnkDelete.Size = new System.Drawing.Size(40, 20);
            this.lnkDelete.TabIndex = 1;
            this.lnkDelete.Text = "DEL";
            this.lnkDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkDelete.Click += new System.EventHandler(this.lnkDelete_Click);
            // 
            // lnkNEW
            // 
            this.lnkNEW.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkNEW.ForeColor = System.Drawing.Color.White;
            this.lnkNEW.Location = new System.Drawing.Point(63, 2);
            this.lnkNEW.Name = "lnkNEW";
            this.lnkNEW.Size = new System.Drawing.Size(44, 20);
            this.lnkNEW.TabIndex = 1;
            this.lnkNEW.Text = "NEW";
            this.lnkNEW.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkNEW.Click += new System.EventHandler(this.lnkNEW_Click);
            // 
            // lnkExit
            // 
            this.lnkExit.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkExit.ForeColor = System.Drawing.Color.White;
            this.lnkExit.Location = new System.Drawing.Point(4, 2);
            this.lnkExit.Name = "lnkExit";
            this.lnkExit.Size = new System.Drawing.Size(45, 20);
            this.lnkExit.TabIndex = 1;
            this.lnkExit.Text = "Exit";
            this.lnkExit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkExit.Click += new System.EventHandler(this.lnkExit_Click);
            // 
            // lblU
            // 
            this.lblU.Location = new System.Drawing.Point(79, 245);
            this.lblU.Name = "lblU";
            this.lblU.Size = new System.Drawing.Size(30, 15);
            this.lblU.Text = "U :";
            // 
            // lblCountByDO
            // 
            this.lblCountByDO.Location = new System.Drawing.Point(55, 245);
            this.lblCountByDO.Name = "lblCountByDO";
            this.lblCountByDO.Size = new System.Drawing.Size(25, 15);
            this.lblCountByDO.Text = "0";
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Location = new System.Drawing.Point(9, 116);
            this.txtSerialNumber.MaxLength = 18;
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(139, 21);
            this.txtSerialNumber.TabIndex = 58;
            this.txtSerialNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialNumber_KeyDown);
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.Location = new System.Drawing.Point(9, 98);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(34, 15);
            this.lblSerialNumber.Text = "S/N :";
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(187, 246);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 69;
            this.batt.TabStop = false;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(186, 244);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(49, 19);
            // 
            // btnUploadData
            // 
            this.btnUploadData.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUploadData.ForeColor = System.Drawing.Color.White;
            this.btnUploadData.Location = new System.Drawing.Point(128, 244);
            this.btnUploadData.Name = "btnUploadData";
            this.btnUploadData.Size = new System.Drawing.Size(54, 19);
            this.btnUploadData.TabIndex = 80;
            this.btnUploadData.Text = "Upload";
            this.btnUploadData.Visible = false;
            this.btnUploadData.Click += new System.EventHandler(this.btnUploadData_Click);
            // 
            // frmGIRefDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.btnUploadData);
            this.Controls.Add(this.batt);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.lblSerialNumber);
            this.Controls.Add(this.lblCountByDO);
            this.Controls.Add(this.lblU);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtDO);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblDO);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "frmGIRefDelivery";
            this.Text = "frmGIRefDelivery";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmGIRefDelivery_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmGIRefDelivery_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtDO;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Label lblDO;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblU;
        private System.Windows.Forms.Label lblCountByDO;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.LinkLabel lnkPrint;
        private System.Windows.Forms.LinkLabel lnkDelete;
        private System.Windows.Forms.LinkLabel lnkNEW;
        private System.Windows.Forms.LinkLabel lnkExit;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btnUploadData;
    }
}