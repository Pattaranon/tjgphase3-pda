namespace SPACE.TJG.Barcode.UI.Forms
{
    partial class frmImportCustomerOnline
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.lklLogout = new System.Windows.Forms.LinkLabel();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.pgbStartImport = new System.Windows.Forms.ProgressBar();
            this.btnStartImport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
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
            this.lblMenu.Text = "--- Import Customer ---";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(132, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(0, 0);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.Visible = false;
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
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(8, 75);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(223, 44);
            this.btnOpenFile.TabIndex = 6;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(8, 43);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(223, 21);
            this.txtFileName.TabIndex = 7;
            // 
            // pgbStartImport
            // 
            this.pgbStartImport.Location = new System.Drawing.Point(8, 139);
            this.pgbStartImport.Name = "pgbStartImport";
            this.pgbStartImport.Size = new System.Drawing.Size(223, 20);
            // 
            // btnStartImport
            // 
            this.btnStartImport.Location = new System.Drawing.Point(8, 170);
            this.btnStartImport.Name = "btnStartImport";
            this.btnStartImport.Size = new System.Drawing.Size(223, 44);
            this.btnStartImport.TabIndex = 6;
            this.btnStartImport.Text = "Start Import File";
            this.btnStartImport.Click += new System.EventHandler(this.btnStartImport_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 1);
            this.label1.Text = "label1";
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Red;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(6, 231);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(229, 27);
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmImportCustomerOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pgbStartImport);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnStartImport);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "frmImportCustomerOnline";
            this.Text = "frmImportCustomer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmImportCustomer_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmImportCustomer_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.LinkLabel lklLogout;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.ProgressBar pgbStartImport;
        private System.Windows.Forms.Button btnStartImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMessage;
    }
}