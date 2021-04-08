namespace SPACE.TJG.Barcode.UI.Forms
{
    partial class frmSelectDistributionRetDel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectDistributionRetDel));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lblCountData = new System.Windows.Forms.Label();
            this.lnkPre = new System.Windows.Forms.LinkLabel();
            this.palMenu = new System.Windows.Forms.Panel();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.txtInputText = new System.Windows.Forms.TextBox();
            this.txtSelect = new System.Windows.Forms.TextBox();
            this.lblSelect = new System.Windows.Forms.Label();
            this.txtVehicle = new System.Windows.Forms.TextBox();
            this.lblVehicle = new System.Windows.Forms.Label();
            this.txtDoc = new System.Windows.Forms.TextBox();
            this.lblDoc = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lnkNext = new System.Windows.Forms.LinkLabel();
            this.lnkDel = new System.Windows.Forms.LinkLabel();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lnkExit = new System.Windows.Forms.LinkLabel();
            this.timer = new System.Windows.Forms.Timer();
            this.palMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCountData
            // 
            this.lblCountData.Location = new System.Drawing.Point(13, 208);
            this.lblCountData.Name = "lblCountData";
            this.lblCountData.Size = new System.Drawing.Size(162, 15);
            this.lblCountData.Text = "0/0/0";
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
            // palMenu
            // 
            this.palMenu.BackColor = System.Drawing.Color.Transparent;
            this.palMenu.Controls.Add(this.batt);
            this.palMenu.Controls.Add(this.pic);
            this.palMenu.Controls.Add(this.txtInputText);
            this.palMenu.Controls.Add(this.txtSelect);
            this.palMenu.Controls.Add(this.lblSelect);
            this.palMenu.Controls.Add(this.txtVehicle);
            this.palMenu.Controls.Add(this.lblVehicle);
            this.palMenu.Controls.Add(this.txtDoc);
            this.palMenu.Controls.Add(this.lblDoc);
            this.palMenu.Controls.Add(this.txtCustomer);
            this.palMenu.Controls.Add(this.lblCustomer);
            this.palMenu.Controls.Add(this.lblMessage);
            this.palMenu.Controls.Add(this.lblCountData);
            this.palMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palMenu.Location = new System.Drawing.Point(0, 36);
            this.palMenu.Name = "palMenu";
            this.palMenu.Size = new System.Drawing.Size(240, 232);
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(185, 209);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 54;
            this.batt.TabStop = false;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(184, 207);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(49, 19);
            // 
            // txtInputText
            // 
            this.txtInputText.Enabled = false;
            this.txtInputText.Location = new System.Drawing.Point(9, 131);
            this.txtInputText.Name = "txtInputText";
            this.txtInputText.Size = new System.Drawing.Size(223, 21);
            this.txtInputText.TabIndex = 46;
            // 
            // txtSelect
            // 
            this.txtSelect.Enabled = false;
            this.txtSelect.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.txtSelect.Location = new System.Drawing.Point(153, 101);
            this.txtSelect.MaxLength = 7;
            this.txtSelect.Name = "txtSelect";
            this.txtSelect.Size = new System.Drawing.Size(26, 24);
            this.txtSelect.TabIndex = 39;
            // 
            // lblSelect
            // 
            this.lblSelect.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lblSelect.Location = new System.Drawing.Point(10, 104);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(136, 20);
            this.lblSelect.Text = "Empty[0] / Full[9] :";
            // 
            // txtVehicle
            // 
            this.txtVehicle.Enabled = false;
            this.txtVehicle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.txtVehicle.Location = new System.Drawing.Point(77, 71);
            this.txtVehicle.MaxLength = 7;
            this.txtVehicle.Name = "txtVehicle";
            this.txtVehicle.Size = new System.Drawing.Size(139, 24);
            this.txtVehicle.TabIndex = 38;
            // 
            // lblVehicle
            // 
            this.lblVehicle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lblVehicle.Location = new System.Drawing.Point(10, 73);
            this.lblVehicle.Name = "lblVehicle";
            this.lblVehicle.Size = new System.Drawing.Size(63, 19);
            this.lblVehicle.Text = "Vehicle :";
            // 
            // txtDoc
            // 
            this.txtDoc.Enabled = false;
            this.txtDoc.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.txtDoc.Location = new System.Drawing.Point(58, 41);
            this.txtDoc.MaxLength = 7;
            this.txtDoc.Name = "txtDoc";
            this.txtDoc.Size = new System.Drawing.Size(139, 24);
            this.txtDoc.TabIndex = 37;
            // 
            // lblDoc
            // 
            this.lblDoc.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lblDoc.Location = new System.Drawing.Point(10, 43);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(43, 20);
            this.lblDoc.Text = "Doc :";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Enabled = false;
            this.txtCustomer.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.txtCustomer.Location = new System.Drawing.Point(96, 11);
            this.txtCustomer.MaxLength = 7;
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(115, 24);
            this.txtCustomer.TabIndex = 36;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lblCustomer.Location = new System.Drawing.Point(10, 13);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(83, 19);
            this.lblCustomer.Text = "Cust Code :";
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
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pnlLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLine.Location = new System.Drawing.Point(0, 33);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(240, 3);
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
            this.lblMenu.Text = "--- 1.2 View Data ---";
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
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // frmSelectDistributionRetDel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.palMenu);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "frmSelectDistributionRetDel";
            this.Text = "frmSelectDistributionRetDel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSelectDistributionRetDel_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmSelectDistributionRetDel_KeyUp);
            this.palMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCountData;
        private System.Windows.Forms.LinkLabel lnkPre;
        private System.Windows.Forms.Panel palMenu;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.LinkLabel lnkNext;
        private System.Windows.Forms.LinkLabel lnkDel;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel lnkExit;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox txtSelect;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.TextBox txtVehicle;
        private System.Windows.Forms.Label lblVehicle;
        private System.Windows.Forms.TextBox txtDoc;
        private System.Windows.Forms.Label lblDoc;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtInputText;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
    }
}