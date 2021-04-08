using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UserInterfaces.Execute;
using ObjectManager.ConvertObject;
using ServiceFunction.Cursor;
using ServiceFunction.Function;
using MsgBox.ClassMsgBox;

namespace UserInterfaces.Forms
{
    public partial class frmSelectGIDeliveryDel : Form
    {
        #region Constructor

        public frmSelectGIDeliveryDel()
        {
            InitializeComponent();
        }

        #endregion

        #region Member

        DataTable dt_output = new DataTable();
        //int No_left = 0;
        //int No_Right = 0;
        int No = 0;
        //int Row = 0;

        #endregion

        #region Event

        private void frmSelectGIDeliveryDel_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                this.lblMessage.Hide();
                this.txtDO.Enabled = false;
                this.txtSerialNumber.Enabled = false;

                dt_output = (DataTable)Executing.Instance.getGIDeliveryForDel();
                if (!dt_output.IsNullOrNoRows())
                {
                    //Has Data.
                    this.txtDO.Text = dt_output.Rows[0][0].ToString();
                    this.txtSerialNumber.Text = dt_output.Rows[0][1].ToString();

                    this.lblCountData.Text = dt_output.Rows.Count.ToString() + "/" + dt_output.Rows.Count.ToString() + "/" + "0";
                }
                else
                {
                    //Not found data.
                    this.lblMessage.Show();
                    lblMessage.Text = "NOT FOUND DATA";
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ClassMsg.DialogErrorTryCatch("System Error save : ", ex.InnerException);
                else
                    ClassMsg.DialogErrorTryCatch("System Error save : ", ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void frmSelectGIDeliveryDel_KeyUp(object sender, KeyEventArgs e)
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

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 3000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        private void lnkExit_Click(object sender, EventArgs e)
        {
            frmGIRefDelivery frmGIRefDelivery = new frmGIRefDelivery();
            frmGIRefDelivery.Show();
            this.Close();
        }
        private void lnkPre_Click(object sender, EventArgs e)
        {
            if (No == 1)
            {
                this.txtDO.Text = dt_output.Rows[dt_output.Rows.Count - No]["do_no"].ToString();
                this.txtSerialNumber.Text = dt_output.Rows[dt_output.Rows.Count - No]["serial_number"].ToString();

                this.lblCountData.Text = No + "/" + dt_output.Rows.Count.ToString() + "/" + "0";
                return;
            }
            if (No == 0)
            {
                No = dt_output.Rows.Count - 1;
            }
            else if (No != dt_output.Rows.Count)
            {
                No -= 1;
            }
            else if (No == dt_output.Rows.Count)
            {
                No -= 1;
            }

            this.txtDO.Text = dt_output.Rows[dt_output.Rows.Count - No]["do_no"].ToString();
            this.txtSerialNumber.Text = dt_output.Rows[dt_output.Rows.Count - No]["serial_number"].ToString();

            this.lblCountData.Text = No + "/" + dt_output.Rows.Count.ToString() + "/" + "0";
        }
        private void lnkNext_Click(object sender, EventArgs e)
        {
            if (No == 0)
            {
                return;
            }
            if (No != dt_output.Rows.Count)
            {
                No += 1;
            }

            this.txtDO.Text = dt_output.Rows[dt_output.Rows.Count - No]["do_no"].ToString();
            this.txtSerialNumber.Text = dt_output.Rows[dt_output.Rows.Count - No]["serial_number"].ToString();

            this.lblCountData.Text = No + "/" + dt_output.Rows.Count.ToString() + "/" + "0";
        }
        private void lnkDel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtDO.Text.Trim()) &&
                !string.IsNullOrEmpty(this.txtSerialNumber.Text.Trim()))
            {
                frmMenuDelete frmMenuDelete = new frmMenuDelete("DEL", "GIRefDelivery", this.txtDO.Text.Trim(), this.txtSerialNumber.Text.Trim(), string.Empty, string.Empty, string.Empty);
                frmMenuDelete.Show();
                this.Close();
            }
        }

        #endregion
    }
}