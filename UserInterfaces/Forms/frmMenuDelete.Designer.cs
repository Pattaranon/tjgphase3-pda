namespace UserInterfaces.Forms
{
    partial class frmMenuDelete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuDelete));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.button2 = new System.Windows.Forms.Button();
            this.palMenu = new System.Windows.Forms.Panel();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lklLogout = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer();
            this.palMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.palMenu.Controls.Add(this.txtPassword);
            this.palMenu.Controls.Add(this.lblPassword);
            this.palMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palMenu.Location = new System.Drawing.Point(0, 36);
            this.palMenu.Name = "palMenu";
            this.palMenu.Size = new System.Drawing.Size(240, 232);
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(9, 211);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 98;
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
            this.lblMessage.Location = new System.Drawing.Point(6, 176);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(229, 27);
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(90, 48);
            this.txtPassword.MaxLength = 7;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(130, 21);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(9, 50);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(75, 16);
            this.lblPassword.Text = "Password :";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblMenu
            // 
            this.lblMenu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(3, 6);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(234, 21);
            this.lblMenu.Text = "--- Menu Delete ---";
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.lklLogout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 268);
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
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // frmMenuDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.palMenu);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "frmMenuDelete";
            this.Text = "frmMenuDelete";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMenuDelete_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMenuDelete_KeyUp);
            this.palMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel palMenu;
        private System.Windows.Forms.Label lblPassword;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lklLogout;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblMessage;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Timer timer;
    }
}