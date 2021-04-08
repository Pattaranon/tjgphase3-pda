using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SPACE.TJG.Barcode.UI.Execute;
using ServiceFunction.Function;

namespace SPACE.TJG.Barcode.UI.Pages.Views
{
    public partial class frmWarehouseViewData : Form
    {
        #region Constructor

        public frmWarehouseViewData()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        private void SearchAll()
        {
            DataTable dt_output_all = Executing.Instance.getWarehouse();
            dt_output_all.TableName = "Data";
            dt_output_all.Columns.Add("No", typeof(string));

            if (!dt_output_all.IsNullOrNoRows())
            {
                dt_output_all.TableName = "Data";

                #region "Loop Number"

                int No = 0;
                for (int i = 0; i < dt_output_all.Rows.Count; i++)
                {
                    No++;
                    dt_output_all.Rows[i]["No"] = No;
                }

                #endregion

                dgvWarehouse.DataSource = dt_output_all;
                this.lblTotalRecord.Text = "Total : " + dt_output_all.Rows.Count;
                this.txtSerialNumber.Focus();
                this.txtSerialNumber.SelectAll();
            }
        }
        private void SearchByCondition(string condition_type, string condition_value)
        {
            DataTable dt_output_all = Executing.Instance.getWarehouseByCondition(condition_type, condition_value);
            dt_output_all.TableName = "Data";
            dt_output_all.Columns.Add("No", typeof(string));

            if (!dt_output_all.IsNullOrNoRows())
            {
                dt_output_all.TableName = "Data";

                #region "Loop Number"

                int No = 0;
                for (int i = 0; i < dt_output_all.Rows.Count; i++)
                {
                    No++;
                    dt_output_all.Rows[i]["No"] = No;
                }

                #endregion

                dgvWarehouse.DataSource = dt_output_all;
                this.lblTotalRecord.Text = "Total : " + dt_output_all.Rows.Count;
                this.txtSerialNumber.Focus();
                this.txtSerialNumber.SelectAll();
            }
            else
            {
                dgvWarehouse.DataSource = dt_output_all;
                this.lblTotalRecord.Text = "Total : " + dt_output_all.Rows.Count;
                this.txtSerialNumber.Focus();
                this.txtSerialNumber.SelectAll();
            }
        }

        #endregion

        #region Event

        private void frmWarehouseViewData_Load(object sender, EventArgs e)
        {
            try
            {
                // Search
                this.chkBarcodeORSerial.Checked = true;
                SearchAll();
            }
            catch (Exception ex)
            {
                Executing.Instance.Insert_Log("ERRLOAD_VIEW_WH_SC4", ex.Message.ToString(), "frmWarehouseViewData", "frmWarehouseViewData_Load");
            }
        }
        private void frmWarehouseViewData_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Down:
                    lnkSearchWarehouse_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void lnkSearchWarehouse_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chkBarcodeORSerial.Checked)
                {
                    // Click = search cylinder.
                    if (string.IsNullOrEmpty(this.txtSerialNumber.Text.Trim()))
                    {
                        // Search All
                        SearchAll();
                    }
                    else
                    {
                        // Search by cylinder or item code
                        SearchByCondition("CYLINDER", this.txtSerialNumber.Text.Trim());
                    }
                }
                else if (!this.chkBarcodeORSerial.Checked)
                {
                    // Click = search item code.
                    if (string.IsNullOrEmpty(this.txtSerialNumber.Text.Trim()))
                    {
                        // Search All
                        SearchAll();
                    }
                    else
                    {
                        // Search by cylinder or item code
                        SearchByCondition("ITEM_CODE", this.txtSerialNumber.Text.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                Executing.Instance.Insert_Log("ERRSEARCH_WH_SC4", ex.Message.ToString(), "frmWarehouseViewData", "lnkSearchWarehouse_Click");
            }
        }

        private void lklLogout_Click(object sender, EventArgs e)
        {
            Pages.Menus.frmMenuViewByFunctions fView = new SPACE.TJG.Barcode.UI.Pages.Menus.frmMenuViewByFunctions();
            fView.Show();
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}