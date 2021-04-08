﻿namespace SPACE.TJG.Barcode.UI.Forms
{
    partial class frmMenuDeleteData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuDeleteData));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lklLogout = new System.Windows.Forms.LinkLabel();
            this.llbPhysicalCount = new System.Windows.Forms.LinkLabel();
            this.llbGRRefPO = new System.Windows.Forms.LinkLabel();
            this.llbDistributionSale = new System.Windows.Forms.LinkLabel();
            this.llbGItoProduction = new System.Windows.Forms.LinkLabel();
            this.llbGRProduction = new System.Windows.Forms.LinkLabel();
            this.llbTransfer = new System.Windows.Forms.LinkLabel();
            this.llbDistributionRet = new System.Windows.Forms.LinkLabel();
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
            this.lblMenu.Text = "--- Delete Data ---";
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
            // llbPhysicalCount
            // 
            this.llbPhysicalCount.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbPhysicalCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbPhysicalCount.Location = new System.Drawing.Point(21, 215);
            this.llbPhysicalCount.Name = "llbPhysicalCount";
            this.llbPhysicalCount.Size = new System.Drawing.Size(199, 20);
            this.llbPhysicalCount.TabIndex = 43;
            this.llbPhysicalCount.Text = "7. Physical Count";
            this.llbPhysicalCount.Click += new System.EventHandler(this.llbPhysicalCount_Click);
            // 
            // llbGRRefPO
            // 
            this.llbGRRefPO.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbGRRefPO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbGRRefPO.Location = new System.Drawing.Point(21, 100);
            this.llbGRRefPO.Name = "llbGRRefPO";
            this.llbGRRefPO.Size = new System.Drawing.Size(199, 20);
            this.llbGRRefPO.TabIndex = 39;
            this.llbGRRefPO.Text = "3. GR Ref .PO";
            this.llbGRRefPO.Click += new System.EventHandler(this.llbGRRefPO_Click);
            // 
            // llbDistributionSale
            // 
            this.llbDistributionSale.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbDistributionSale.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbDistributionSale.Location = new System.Drawing.Point(21, 45);
            this.llbDistributionSale.Name = "llbDistributionSale";
            this.llbDistributionSale.Size = new System.Drawing.Size(199, 20);
            this.llbDistributionSale.TabIndex = 37;
            this.llbDistributionSale.Text = "1. Distribution Sale";
            this.llbDistributionSale.Click += new System.EventHandler(this.llbDistributionSale_Click);
            // 
            // llbGItoProduction
            // 
            this.llbGItoProduction.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbGItoProduction.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbGItoProduction.Location = new System.Drawing.Point(21, 156);
            this.llbGItoProduction.Name = "llbGItoProduction";
            this.llbGItoProduction.Size = new System.Drawing.Size(199, 20);
            this.llbGItoProduction.TabIndex = 41;
            this.llbGItoProduction.Text = "5. GI to Production";
            this.llbGItoProduction.Click += new System.EventHandler(this.llbGItoProduction_Click);
            // 
            // llbGRProduction
            // 
            this.llbGRProduction.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbGRProduction.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbGRProduction.Location = new System.Drawing.Point(21, 128);
            this.llbGRProduction.Name = "llbGRProduction";
            this.llbGRProduction.Size = new System.Drawing.Size(199, 20);
            this.llbGRProduction.TabIndex = 40;
            this.llbGRProduction.Text = "4. GR Production";
            this.llbGRProduction.Click += new System.EventHandler(this.llbGRProduction_Click);
            // 
            // llbTransfer
            // 
            this.llbTransfer.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbTransfer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbTransfer.Location = new System.Drawing.Point(21, 184);
            this.llbTransfer.Name = "llbTransfer";
            this.llbTransfer.Size = new System.Drawing.Size(199, 20);
            this.llbTransfer.TabIndex = 42;
            this.llbTransfer.Text = "6. Transfer";
            this.llbTransfer.Click += new System.EventHandler(this.llbTransfer_Click);
            // 
            // llbDistributionRet
            // 
            this.llbDistributionRet.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.llbDistributionRet.ForeColor = System.Drawing.SystemColors.ControlText;
            this.llbDistributionRet.Location = new System.Drawing.Point(21, 72);
            this.llbDistributionRet.Name = "llbDistributionRet";
            this.llbDistributionRet.Size = new System.Drawing.Size(199, 20);
            this.llbDistributionRet.TabIndex = 38;
            this.llbDistributionRet.Text = "2. Distribution Ret";
            this.llbDistributionRet.Click += new System.EventHandler(this.llbDistributionRet_Click);
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(9, 248);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 96;
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
            // frmMenuDeleteData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.batt);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.llbPhysicalCount);
            this.Controls.Add(this.llbGRRefPO);
            this.Controls.Add(this.llbDistributionSale);
            this.Controls.Add(this.llbGItoProduction);
            this.Controls.Add(this.llbGRProduction);
            this.Controls.Add(this.llbTransfer);
            this.Controls.Add(this.llbDistributionRet);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "frmMenuDeleteData";
            this.Text = "frmMenuDeleteData";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMenuDeleteData_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMenuDeleteData_KeyUp);
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
        internal System.Windows.Forms.LinkLabel llbPhysicalCount;
        internal System.Windows.Forms.LinkLabel llbGRRefPO;
        internal System.Windows.Forms.LinkLabel llbDistributionSale;
        internal System.Windows.Forms.LinkLabel llbGItoProduction;
        internal System.Windows.Forms.LinkLabel llbGRProduction;
        internal System.Windows.Forms.LinkLabel llbTransfer;
        internal System.Windows.Forms.LinkLabel llbDistributionRet;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Timer timer;
    }
}