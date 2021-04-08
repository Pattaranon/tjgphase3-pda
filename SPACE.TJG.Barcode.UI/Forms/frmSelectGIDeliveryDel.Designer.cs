namespace SPACE.TJG.Barcode.UI.Forms
{
    partial class frmSelectGIDeliveryDel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectGIDeliveryDel));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lnkDel = new System.Windows.Forms.LinkLabel();
            this.lnkNext = new System.Windows.Forms.LinkLabel();
            this.lnkPre = new System.Windows.Forms.LinkLabel();
            this.lnkExit = new System.Windows.Forms.LinkLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.palMenu = new System.Windows.Forms.Panel();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblCountData = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.txtDO = new System.Windows.Forms.TextBox();
            this.lblDO = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.palMenu.SuspendLayout();
            this.SuspendLayout();
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
            this.lblMenu.Text = "-- 1.1.2 GI Ref. Delivery --";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.lnkDel);
            this.panel2.Controls.Add(this.lnkNext);
            this.panel2.Controls.Add(this.lnkPre);
            this.panel2.Controls.Add(this.lnkExit);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 268);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 26);
            // 
            // lnkDel
            // 
            this.lnkDel.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkDel.ForeColor = System.Drawing.Color.White;
            this.lnkDel.Location = new System.Drawing.Point(193, 3);
            this.lnkDel.Name = "lnkDel";
            this.lnkDel.Size = new System.Drawing.Size(42, 20);
            this.lnkDel.TabIndex = 5;
            this.lnkDel.Text = "Del";
            this.lnkDel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkDel.Click += new System.EventHandler(this.lnkDel_Click);
            // 
            // lnkNext
            // 
            this.lnkNext.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkNext.ForeColor = System.Drawing.Color.White;
            this.lnkNext.Location = new System.Drawing.Point(114, 3);
            this.lnkNext.Name = "lnkNext";
            this.lnkNext.Size = new System.Drawing.Size(61, 20);
            this.lnkNext.TabIndex = 6;
            this.lnkNext.Text = "Next";
            this.lnkNext.Click += new System.EventHandler(this.lnkNext_Click);
            // 
            // lnkPre
            // 
            this.lnkPre.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkPre.ForeColor = System.Drawing.Color.White;
            this.lnkPre.Location = new System.Drawing.Point(64, 3);
            this.lnkPre.Name = "lnkPre";
            this.lnkPre.Size = new System.Drawing.Size(44, 20);
            this.lnkPre.TabIndex = 3;
            this.lnkPre.Text = "Pre";
            this.lnkPre.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lnkPre.Click += new System.EventHandler(this.lnkPre_Click);
            // 
            // lnkExit
            // 
            this.lnkExit.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkExit.ForeColor = System.Drawing.Color.White;
            this.lnkExit.Location = new System.Drawing.Point(5, 3);
            this.lnkExit.Name = "lnkExit";
            this.lnkExit.Size = new System.Drawing.Size(45, 20);
            this.lnkExit.TabIndex = 4;
            this.lnkExit.Text = "Exit";
            this.lnkExit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkExit.Click += new System.EventHandler(this.lnkExit_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(132, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(0, 0);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.Visible = false;
            // 
            // palMenu
            // 
            this.palMenu.BackColor = System.Drawing.Color.Transparent;
            this.palMenu.Controls.Add(this.batt);
            this.palMenu.Controls.Add(this.pic);
            this.palMenu.Controls.Add(this.lblMessage);
            this.palMenu.Controls.Add(this.lblCountData);
            this.palMenu.Controls.Add(this.txtSerialNumber);
            this.palMenu.Controls.Add(this.lblSerialNumber);
            this.palMenu.Controls.Add(this.txtDO);
            this.palMenu.Controls.Add(this.lblDO);
            this.palMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palMenu.Location = new System.Drawing.Point(0, 36);
            this.palMenu.Name = "palMenu";
            this.palMenu.Size = new System.Drawing.Size(240, 258);
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(186, 209);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 66;
            this.batt.TabStop = false;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(185, 207);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(49, 19);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMessage.BackColor = System.Drawing.Color.Red;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(6, 174);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(229, 27);
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCountData
            // 
            this.lblCountData.Location = new System.Drawing.Point(13, 208);
            this.lblCountData.Name = "lblCountData";
            this.lblCountData.Size = new System.Drawing.Size(162, 15);
            this.lblCountData.Text = "0/0/0";
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Location = new System.Drawing.Point(13, 82);
            this.txtSerialNumber.MaxLength = 18;
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(139, 21);
            this.txtSerialNumber.TabIndex = 62;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.Location = new System.Drawing.Point(13, 64);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(34, 15);
            this.lblSerialNumber.Text = "S/N :";
            // 
            // txtDO
            // 
            this.txtDO.Location = new System.Drawing.Point(13, 29);
            this.txtDO.MaxLength = 15;
            this.txtDO.Name = "txtDO";
            this.txtDO.Size = new System.Drawing.Size(139, 21);
            this.txtDO.TabIndex = 61;
            // 
            // lblDO
            // 
            this.lblDO.Location = new System.Drawing.Point(13, 11);
            this.lblDO.Name = "lblDO";
            this.lblDO.Size = new System.Drawing.Size(34, 15);
            this.lblDO.Text = "DO :";
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
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // frmSelectGIDeliveryDel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.palMenu);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "frmSelectGIDeliveryDel";
            this.Text = "frmSelectGIDeliveryDel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSelectGIDeliveryDel_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmSelectGIDeliveryDel_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.palMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel palMenu;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.TextBox txtDO;
        private System.Windows.Forms.Label lblDO;
        private System.Windows.Forms.LinkLabel lnkDel;
        private System.Windows.Forms.LinkLabel lnkNext;
        private System.Windows.Forms.LinkLabel lnkPre;
        private System.Windows.Forms.LinkLabel lnkExit;
        private System.Windows.Forms.Label lblCountData;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Timer timer;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
    }
}