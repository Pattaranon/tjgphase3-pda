namespace UserInterfaces.Pages.Views
{
    partial class frmWarehouseViewData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWarehouseViewData));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lklLogout = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lnkSearchWarehouse = new System.Windows.Forms.LinkLabel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer();
            this.batt = new OpenNETCF.Windows.Forms.BatteryLife();
            this.pic = new System.Windows.Forms.PictureBox();
            this.lblTotalRecord = new System.Windows.Forms.Label();
            this.dgvWarehouse = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.cNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cSellOrderNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cCust_id = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cItemCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cCylinder = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cCreateDate = new System.Windows.Forms.DataGridTextBoxColumn();
            this.chkBarcodeORSerial = new System.Windows.Forms.CheckBox();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.panel2.Controls.Add(this.lnkSearchWarehouse);
            this.panel2.Controls.Add(this.lklLogout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 294);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 26);
            // 
            // lnkSearchWarehouse
            // 
            this.lnkSearchWarehouse.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lnkSearchWarehouse.ForeColor = System.Drawing.Color.White;
            this.lnkSearchWarehouse.Location = new System.Drawing.Point(130, 5);
            this.lnkSearchWarehouse.Name = "lnkSearchWarehouse";
            this.lnkSearchWarehouse.Size = new System.Drawing.Size(106, 17);
            this.lnkSearchWarehouse.TabIndex = 2;
            this.lnkSearchWarehouse.Text = "Search";
            this.lnkSearchWarehouse.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lnkSearchWarehouse.Click += new System.EventHandler(this.lnkSearchWarehouse_Click);
            // 
            // lblMenu
            // 
            this.lblMenu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(3, 6);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(234, 21);
            this.lblMenu.Text = "--- 4.1 Warehouse Data ---";
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
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // batt
            // 
            this.batt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.batt.Location = new System.Drawing.Point(9, 272);
            this.batt.Name = "batt";
            this.batt.PercentageBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.batt.Size = new System.Drawing.Size(39, 13);
            this.batt.TabIndex = 100;
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
            this.lblTotalRecord.Location = new System.Drawing.Point(137, 271);
            this.lblTotalRecord.Name = "lblTotalRecord";
            this.lblTotalRecord.Size = new System.Drawing.Size(100, 16);
            this.lblTotalRecord.Text = "Total : 0";
            this.lblTotalRecord.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dgvWarehouse
            // 
            this.dgvWarehouse.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgvWarehouse.Location = new System.Drawing.Point(3, 67);
            this.dgvWarehouse.Name = "dgvWarehouse";
            this.dgvWarehouse.Size = new System.Drawing.Size(236, 199);
            this.dgvWarehouse.TabIndex = 186;
            this.dgvWarehouse.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cNo);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cSellOrderNo);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cCust_id);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cItemCode);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cCylinder);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cCreateDate);
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
            // cSellOrderNo
            // 
            this.cSellOrderNo.Format = "";
            this.cSellOrderNo.FormatInfo = null;
            this.cSellOrderNo.HeaderText = "Sale Order No";
            this.cSellOrderNo.MappingName = "sell_order_no";
            this.cSellOrderNo.Width = 80;
            // 
            // cCust_id
            // 
            this.cCust_id.Format = "";
            this.cCust_id.FormatInfo = null;
            this.cCust_id.HeaderText = "Customer Code";
            this.cCust_id.MappingName = "cust_id";
            this.cCust_id.Width = 70;
            // 
            // cItemCode
            // 
            this.cItemCode.Format = "";
            this.cItemCode.FormatInfo = null;
            this.cItemCode.HeaderText = "Item Code";
            this.cItemCode.MappingName = "item_code";
            this.cItemCode.Width = 80;
            // 
            // cCylinder
            // 
            this.cCylinder.Format = "";
            this.cCylinder.FormatInfo = null;
            this.cCylinder.HeaderText = "Cylinder S/N";
            this.cCylinder.MappingName = "cylinder_sn";
            this.cCylinder.Width = 120;
            // 
            // cCreateDate
            // 
            this.cCreateDate.Format = "dd-MM-yyyy HH:mm";
            this.cCreateDate.FormatInfo = null;
            this.cCreateDate.HeaderText = "Create Date";
            this.cCreateDate.MappingName = "create_date";
            this.cCreateDate.Width = 120;
            // 
            // chkBarcodeORSerial
            // 
            this.chkBarcodeORSerial.Checked = true;
            this.chkBarcodeORSerial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBarcodeORSerial.Location = new System.Drawing.Point(112, 42);
            this.chkBarcodeORSerial.Name = "chkBarcodeORSerial";
            this.chkBarcodeORSerial.Size = new System.Drawing.Size(19, 20);
            this.chkBarcodeORSerial.TabIndex = 220;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblSerialNumber.Location = new System.Drawing.Point(3, 44);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(110, 14);
            this.lblSerialNumber.Text = "Cylinder S/N or Item";
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.BackColor = System.Drawing.Color.Black;
            this.txtSerialNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtSerialNumber.ForeColor = System.Drawing.Color.White;
            this.txtSerialNumber.Location = new System.Drawing.Point(136, 41);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(102, 21);
            this.txtSerialNumber.TabIndex = 219;
            // 
            // frmWarehouseViewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.chkBarcodeORSerial);
            this.Controls.Add(this.lblSerialNumber);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.lblTotalRecord);
            this.Controls.Add(this.dgvWarehouse);
            this.Controls.Add(this.batt);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmWarehouseViewData";
            this.Text = "frmWarehouseViewData";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmWarehouseViewData_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmWarehouseViewData_KeyUp);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel lklLogout;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer;
        internal OpenNETCF.Windows.Forms.BatteryLife batt;
        internal System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label lblTotalRecord;
        private System.Windows.Forms.DataGrid dgvWarehouse;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn cNo;
        private System.Windows.Forms.DataGridTextBoxColumn cSellOrderNo;
        private System.Windows.Forms.DataGridTextBoxColumn cCust_id;
        private System.Windows.Forms.DataGridTextBoxColumn cItemCode;
        private System.Windows.Forms.DataGridTextBoxColumn cCylinder;
        private System.Windows.Forms.DataGridTextBoxColumn cCreateDate;
        private System.Windows.Forms.CheckBox chkBarcodeORSerial;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.LinkLabel lnkSearchWarehouse;
    }
}