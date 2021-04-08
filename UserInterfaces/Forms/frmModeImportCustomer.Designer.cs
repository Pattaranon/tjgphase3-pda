namespace UserInterfaces.Forms
{
    partial class frmModeImportCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModeImportCustomer));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.palMenu = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkYES = new System.Windows.Forms.LinkLabel();
            this.lklNO = new System.Windows.Forms.LinkLabel();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer();
            this.palMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // palMenu
            // 
            this.palMenu.BackColor = System.Drawing.Color.Transparent;
            this.palMenu.Controls.Add(this.label7);
            this.palMenu.Controls.Add(this.label6);
            this.palMenu.Controls.Add(this.label4);
            this.palMenu.Controls.Add(this.label5);
            this.palMenu.Controls.Add(this.label3);
            this.palMenu.Controls.Add(this.label2);
            this.palMenu.Controls.Add(this.batt);
            this.palMenu.Controls.Add(this.pic);
            this.palMenu.Controls.Add(this.lblMessage);
            this.palMenu.Controls.Add(this.label1);
            this.palMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palMenu.Location = new System.Drawing.Point(0, 36);
            this.palMenu.Name = "palMenu";
            this.palMenu.Size = new System.Drawing.Size(240, 232);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(190, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 21);
            this.label7.Text = "]";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(25, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 21);
            this.label6.Text = "[";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Lime;
            this.label4.Location = new System.Drawing.Point(129, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 21);
            this.label4.Text = "ONLINE";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(110, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 21);
            this.label5.Text = "/";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(31, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 21);
            this.label3.Text = "OFFLINE";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 29);
            this.label2.Text = "PLEASE SELECT MODE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(9, 211);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 103;
            this.batt.TabStop = false;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(8, 209);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(49, 19);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMessage.BackColor = System.Drawing.Color.Red;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(6, 175);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(229, 27);
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(5, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 28);
            this.label1.Text = "DOWNLOAD CUSTOMER?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lnkYES
            // 
            this.lnkYES.Font = new System.Drawing.Font("Tahoma", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkYES.ForeColor = System.Drawing.Color.White;
            this.lnkYES.Location = new System.Drawing.Point(130, 4);
            this.lnkYES.Name = "lnkYES";
            this.lnkYES.Size = new System.Drawing.Size(106, 17);
            this.lnkYES.TabIndex = 3;
            this.lnkYES.Text = "ONLINE";
            this.lnkYES.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lnkYES.Click += new System.EventHandler(this.lnkYES_Click);
            // 
            // lklNO
            // 
            this.lklNO.Font = new System.Drawing.Font("Tahoma", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lklNO.ForeColor = System.Drawing.Color.White;
            this.lklNO.Location = new System.Drawing.Point(3, 4);
            this.lklNO.Name = "lklNO";
            this.lklNO.Size = new System.Drawing.Size(106, 17);
            this.lklNO.TabIndex = 0;
            this.lklNO.Text = "OFFLINE";
            this.lklNO.Click += new System.EventHandler(this.lklNO_Click);
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pnlLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLine.Location = new System.Drawing.Point(0, 33);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(240, 3);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.lnkYES);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.lklNO);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 268);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 26);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(191, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(0, 0);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.Visible = false;
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
            this.lblMenu.Text = "Mode Download Customer";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // frmModeImportCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.palMenu);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "frmModeImportCustomer";
            this.Text = "frmModeImportCustomer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmModeImportCustomer_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmModeImportCustomer_KeyUp);
            this.palMenu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palMenu;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkYES;
        private System.Windows.Forms.LinkLabel lklNO;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}