using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MsgBox.ClassMsgBox;
using UserInterfaces.Execute;
using ServiceFunction.Function;

namespace UserInterfaces.Pages.Screens
{
    public partial class frmSelectDeliveryDel : Form
    {
        #region Member

        DataTable dt_output = new DataTable();
        int No = 0;

        #endregion

        #region Constructor

        public frmSelectDeliveryDel()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmSelectDeliveryDel_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.lblMessage.Hide();
                this.dt_output = Executing.Instance.getDeliveryForDel();
                if (!this.dt_output.IsNullOrNoRows())
                {
                    this.txtFactory.Text = this.dt_output.Rows[0]["factory_code"].ToString();
                    this.txtCustomer.Text = this.dt_output.Rows[0]["cust_id"].ToString();
                    this.lblCustName.Text = this.dt_output.Rows[0]["cust_name"].ToString();
                    this.txtItemCode.Text = this.dt_output.Rows[0]["item_code"].ToString();
                    this.txtSelect.Text = this.dt_output.Rows[0]["empty_or_full"].ToString();
                    this.txtInputText.Text = this.dt_output.Rows[0]["cylinder_sn"].ToString();
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
                    Executing.Instance.Insert_Log("ERRLOAD_SC2_1", ex.Message.ToString(), "frmSelectDeliveryDel", "frmSelectDeliveryDel_Load");
                }
                else
                {
                    ClassMsg.DialogErrorTryCatch("System Error save : ", ex);
                    Executing.Instance.Insert_Log("ERRLOAD_SC2_1", ex.Message.ToString(), "frmSelectDeliveryDel", "frmSelectDeliveryDel_Load");
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void frmSelectDeliveryDel_KeyUp(object sender, KeyEventArgs e)
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

        private void lnkPre_Click(object sender, EventArgs e)
        {
            if (this.dt_output.Rows.Count != 1)
            {
                if (this.No == 1)
                {
                    this.txtCustomer.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["cust_id"].ToString();
                    this.lblCustName.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["cust_name"].ToString();
                    this.txtItemCode.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["item_code"].ToString();
                    this.txtSelect.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["empty_or_full"].ToString();
                    this.txtInputText.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["cylinder_sn"].ToString();
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
                    this.lblCustName.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["cust_name"].ToString();
                    this.txtItemCode.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["item_code"].ToString();
                    this.txtSelect.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["empty_or_full"].ToString();
                    this.txtInputText.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["cylinder_sn"].ToString();
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
                this.lblCustName.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["cust_name"].ToString();
                this.txtItemCode.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["item_code"].ToString();
                this.txtSelect.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["empty_or_full"].ToString();
                this.txtInputText.Text = this.dt_output.Rows[this.dt_output.Rows.Count - this.No]["cylinder_sn"].ToString();
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
                !string.IsNullOrEmpty(this.txtFactory.Text.Trim()) &&
                !string.IsNullOrEmpty(this.txtItemCode.Text.Trim()) &&
                !string.IsNullOrEmpty(this.txtSelect.Text.Trim()) &&
                !string.IsNullOrEmpty(this.txtInputText.Text.Trim()))
            {
                frmPasswordDeleteByRecord frmMenuDelete = new frmPasswordDeleteByRecord("DEL_DELIVERY_BY_RECORD",
                                                                                        "MENU_DEL_DELIVERY_BY_RECORD", 
                                                                                        this.txtFactory.Text.Trim(),
                                                                                        this.txtCustomer.Text.Trim(), 
                                                                                        this.txtItemCode.Text.Trim(), 
                                                                                        this.txtSelect.Text.Trim(), 
                                                                                        this.txtInputText.Text.Trim());
                frmMenuDelete.Show();
                this.Close();
            }
        }
        private void lnkExit_Click(object sender, EventArgs e)
        {
            //frmDelivery frmDelivery = new frmDelivery();
            //frmDelivery.Show();
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 3000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}