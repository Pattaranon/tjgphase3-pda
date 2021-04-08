namespace SPACE.TJG.Barcode.UI.Pages.Views
{
    partial class frmOtherScanViewData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOtherScanViewData));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lklLogout = new System.Windows.Forms.LinkLabel();
            this.timer = new System.Windows.Forms.Timer();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.lblTotalRecord = new System.Windows.Forms.Label();
            this.dgvOtherScan = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.cNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cNotes = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cItemCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cscan_number = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cCreate_date = new System.Windows.Forms.DataGridTextBoxColumn();
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
            this.lblMenu.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(3, 6);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(234, 21);
            this.lblMenu.Text = "--- 4.3 New Barcode Data ---";
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
            // timer
            // 
            this.timer.Enabled = true;
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(9, 272);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 102;
            this.batt.TabStop = false;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(8, 270);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(49, 19);
            // 
            // lblTotalRecord
            // 
            this.lblTotalRecord.Location = new System.Drawing.Point(133, 271);
            this.lblTotalRecord.Name = "lblTotalRecord";
            this.lblTotalRecord.Size = new System.Drawing.Size(100, 16);
            this.lblTotalRecord.Text = "Total : 0";
            this.lblTotalRecord.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dgvOtherScan
            // 
            this.dgvOtherScan.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgvOtherScan.Location = new System.Drawing.Point(2, 37);
            this.dgvOtherScan.Name = "dgvOtherScan";
            this.dgvOtherScan.PreferredRowHeight = 30;
            this.dgvOtherScan.Size = new System.Drawing.Size(236, 227);
            this.dgvOtherScan.TabIndex = 186;
            this.dgvOtherScan.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cNo);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cNotes);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cItemCode);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cscan_number);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cCreate_date);
            this.dataGridTableStyle1.MappingName = "Data";
            // 
            // cNo
            // 
            this.cNo.Format = "";
            this.cNo.FormatInfo = null;
            this.cNo.HeaderText = "No";
            this.cNo.MappingName = "No";
            this.cNo.Width = 30;
            // 
            // cNotes
            // 
            this.cNotes.Format = "";
            this.cNotes.FormatInfo = null;
            this.cNotes.HeaderText = "Lot Number";
            this.cNotes.MappingName = "notes";
            this.cNotes.Width = 250;
            // 
            // cItemCode
            // 
            this.cItemCode.Format = "";
            this.cItemCode.FormatInfo = null;
            this.cItemCode.HeaderText = "Item Code";
            this.cItemCode.MappingName = "item_code";
            this.cItemCode.Width = 120;
            // 
            // cscan_number
            // 
            this.cscan_number.Format = "";
            this.cscan_number.FormatInfo = null;
            this.cscan_number.HeaderText = "Cylinder S/N";
            this.cscan_number.MappingName = "scan_number";
            this.cscan_number.Width = 140;
            // 
            // cCreate_date
            // 
            this.cCreate_date.Format = "dd/MM/yyyy HH:mm:ss";
            this.cCreate_date.FormatInfo = null;
            this.cCreate_date.HeaderText = "Create Date";
            this.cCreate_date.MappingName = "create_date";
            this.cCreate_date.Width = 120;
            // 
            // frmOtherScanViewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.lblTotalRecord);
            this.Controls.Add(this.dgvOtherScan);
            this.Controls.Add(this.batt);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmOtherScanViewData";
            this.Text = "frmOtherScanViewData";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmOtherScanViewData_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOtherScanViewData_KeyUp);
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
        private System.Windows.Forms.Timer timer;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label lblTotalRecord;
        private System.Windows.Forms.DataGrid dgvOtherScan;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn cNo;
        private System.Windows.Forms.DataGridTextBoxColumn cNotes;
        private System.Windows.Forms.DataGridTextBoxColumn cscan_number;
        private System.Windows.Forms.DataGridTextBoxColumn cCreate_date;
        private System.Windows.Forms.DataGridTextBoxColumn cItemCode;
    }
}