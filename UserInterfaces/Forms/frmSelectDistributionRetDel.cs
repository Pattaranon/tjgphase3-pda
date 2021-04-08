using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using ServiceFunction.Function;
using System.Windows.Forms;
using MsgBox.ClassMsgBox;
using UserInterfaces.Execute;

namespace UserInterfaces.Forms
{
    public partial class frmSelectDistributionRetDel : Form
    {
        #region Member

        DataTable dt_output = new DataTable();
        int No = 0;

        #endregion

        #region Constructor

        public frmSelectDistributionRetDel()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmSelectDistributionRetDel_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.lblMessage.Hide();
                this.dt_output = Executing.Instance.getDistributionRetForDel();
                if (!this.dt_output.IsNullOrNoRows())
                {
                    this.txtCustomer.Text = this.dt_output.Rows[0]["cust_id"].ToString();
                    this.txtDoc.Text = this.dt_output.Rows[0]["doc_no"].ToString();
                    this.txtVehicle.Text = this.dt_output.Rows[0]["vehicle"].ToString();
                    this.txtSelect.Text = this.dt_output.Rows[0]["empty_or_full"].ToString();
                    this.txtInputText.Text = this.dt_output.Rows[0]["serial_number"].ToString();
                    this.lblCountData.Text = this.dt_output.Rows.Count.ToString() + "/" + this.dt_output.Rows.Count.ToString() + "/0";
                }
                else
                {
                    this.lblMessage.Show();
                    this.lblMessage.Text = "NOT FOUND DATA";
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ClassMsg.DialogErrorTryCatch("System Error save : ", ex.InnerException);
                }
                else
                {
                    ClassMsg.DialogErrorTryCatch("System Error save : ", ex);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void frmSelectDistributionRetDel_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Left:
                    lnkPre_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Right:
                    lnkNext_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lnkExit_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Down:
                    lnkDel_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void lnkExit_Click(object sender, EventArgs e)
        {
            frmDistributionRet frmDistributionRet = new frmDistributionRet();
            frmDistributionRet.Show();
            this.Close();
        }
        private void lnkPre_Click(object sender, EventArgs e)
        {
            if (this.dt_output.Rows.Count != 1)
            {
                if (this.No == 1)
                {
                    this.txtCustomer.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["cust_id"].ToString();
                    this.txtDoc.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["doc_no"].ToString();
                    this.txtVehicle.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["vehicle"].ToString();
                    this.txtSelect.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["empty_or_full"].ToString();
                    this.txtInputText.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["serial_number"].ToString();
                    this.lblCountData.Text = string.Concat(new object[]
					{
						this.No,
						"/",
						this.dt_output.Rows.Count.ToString(),
						"/0"
					});
                }
                else
                {
                    if (this.No == 0)
                    {
                        this.No = this.dt_output.Rows.Count - 1;
                    }
                    else if (this.No != this.dt_output.Rows.Count)
                    {
                        this.No--;
                    }
                    else if (this.No == this.dt_output.Rows.Count)
                    {
                        this.No--;
                    }
                    this.txtCustomer.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["cust_id"].ToString();
                    this.txtDoc.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["doc_no"].ToString();
                    this.txtVehicle.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["vehicle"].ToString();
                    this.txtSelect.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["empty_or_full"].ToString();
                    this.txtInputText.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["serial_number"].ToString();
                    this.lblCountData.Text = string.Concat(new object[]
					{
						this.No,
						"/",
						this.dt_output.Rows.Count.ToString(),
						"/0"
					});
                }
            }
        }
        private void lnkNext_Click(object sender, EventArgs e)
        {
            if (this.No != 0)
            {
                if (this.No != this.dt_output.Rows.Count)
                {
                    this.No++;
                }
                this.txtCustomer.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["cust_id"].ToString();
                this.txtDoc.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["doc_no"].ToString();
                this.txtVehicle.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["vehicle"].ToString();
                this.txtSelect.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["empty_or_full"].ToString();
                this.txtInputText.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["serial_number"].ToString();
                this.lblCountData.Text = string.Concat(new object[]
				{
					this.No,
					"/",
					this.dt_output.Rows.Count.ToString(),
					"/0"
				});
            }
        }
        private void lnkDel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCustomer.Text.Trim()) && 
                !string.IsNullOrEmpty(this.txtDoc.Text.Trim()) && 
                !string.IsNullOrEmpty(this.txtVehicle.Text.Trim()) && 
                !string.IsNullOrEmpty(this.txtSelect.Text.Trim()) && 
                !string.IsNullOrEmpty(this.txtInputText.Text.Trim()))
            {
                frmMenuDelete frmMenuDelete = new frmMenuDelete("DEL", "DistributionRet", this.txtCustomer.Text.Trim(),
                    this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim(), this.txtSelect.Text.Trim(), this.txtInputText.Text.Trim());
                frmMenuDelete.Show();
                this.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Interval = 3000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}