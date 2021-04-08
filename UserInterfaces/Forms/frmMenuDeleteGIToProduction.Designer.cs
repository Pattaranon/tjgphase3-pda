namespace UserInterfaces.Forms
{
    partial class frmMenuDeleteGIToProduction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuDeleteGIToProduction));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lklLogout = new System.Windows.Forms.LinkLabel();
            this.llbGIRefIOEmpty = new System.Windows.Forms.LinkLabel();
            this.llbGIRefReserEmpty = new System.Windows.Forms.LinkLabel();
            this.llbGIRefReserFull = new System.Windows.Forms.LinkLabel();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer();
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
            this.lblMenu.Text = "--- GI To Production ---";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.lklLogout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 268);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 26);
            // 
            // lklLogout
            // 
            this.lklLogout.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lklLogout.ForeColor = System.Drawing.Color.White;
            this.lklLogout.Location = new System.Drawing.Point(3, 3);
            this.lklLogout.Name = "lklLogout";
            this.lklLogout.Size = new System.Drawing.Size(106, 20);
            this.lklLogout.TabIndex = 2;
            this.lklLogout.Text = "Exit";
            this.lklLogout.Click += new System.EventHandler(this.lklLogout_Click);
            // 
            // llbGIRefIOEmpty
            // 
            this.llbGIRefIOEmpty.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbGIRefIOEmpty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbGIRefIOEmpty.Location = new System.Drawing.Point(21, 133);
            this.llbGIRefIOEmpty.Name = "llbGIRefIOEmpty";
            this.llbGIRefIOEmpty.Size = new System.Drawing.Size(199, 20);
            this.llbGIRefIOEmpty.TabIndex = 35;
            this.llbGIRefIOEmpty.Text = "3. GI Ref. IO Empty";
            this.llbGIRefIOEmpty.Click += new System.EventHandler(this.llbGIRefIOEmpty_Click);
            // 
            // llbGIRefReserEmpty
            // 
            this.llbGIRefReserEmpty.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbGIRefReserEmpty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbGIRefReserEmpty.Location = new System.Drawing.Point(21, 64);
            this.llbGIRefReserEmpty.Name = "llbGIRefReserEmpty";
            this.llbGIRefReserEmpty.Size = new System.Drawing.Size(199, 20);
            this.llbGIRefReserEmpty.TabIndex = 33;
            this.llbGIRefReserEmpty.Text = "1. GI Ref. Reser Empty";
            this.llbGIRefReserEmpty.Click += new System.EventHandler(this.llbGIRefReserEmpty_Click);
            // 
            // llbGIRefReserFull
            // 
            this.llbGIRefReserFull.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbGIRefReserFull.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbGIRefReserFull.Location = new System.Drawing.Point(21, 99);
            this.llbGIRefReserFull.Name = "llbGIRefReserFull";
            this.llbGIRefReserFull.Size = new System.Drawing.Size(199, 20);
            this.llbGIRefReserFull.TabIndex = 34;
            this.llbGIRefReserFull.Text = "2. GI Ref. Reser Full";
            this.llbGIRefReserFull.Click += new System.EventHandler(this.llbGIRefReserFull_Click);
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(9, 248);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 101;
            this.batt.TabStop = false;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(8, 246);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(49, 19);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // frmMenuDeleteGIToProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.batt);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.llbGIRefIOEmpty);
            this.Controls.Add(this.llbGIRefReserEmpty);
            this.Controls.Add(this.llbGIRefReserFull);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "frmMenuDeleteGIToProduction";
            this.Text = "frmMenuDeleteGIToProduction";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMenuDeleteGIToProduction_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMenuDeleteGIToProduction_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel lklLogout;
        internal System.Windows.Forms.LinkLabel llbGIRefIOEmpty;
        internal System.Windows.Forms.LinkLabel llbGIRefReserEmpty;
        internal System.Windows.Forms.LinkLabel llbGIRefReserFull;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Timer timer;
    }
}