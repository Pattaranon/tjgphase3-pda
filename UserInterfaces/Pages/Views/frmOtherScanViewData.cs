using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UserInterfaces.Execute;
using ServiceFunction.Function;

namespace UserInterfaces.Pages.Views
{
    public partial class frmOtherScanViewData : Form
    {
        #region Constructor

        public frmOtherScanViewData()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        private void Display()
        {
            DataTable dt_output_all = Executing.Instance.getOtherScan();
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

                /*
                for (int i = 0; i < dt_output_all.Rows.Count; i++)
                {
                    //int[] number = new int[10]; //กำหนดขนาดของ Array
                    dgvOtherScan.PreferredRowHeight = dt_output_all.Rows[i]["notes"].ToString().Length + 15;
                }

                List<int> number = new List<int>();
                for (int i = 0; i < dt_output_all.Rows.Count; i++)
                {
                    number.Add(dt_output_all.Rows[i]["notes"].ToString().Length); //กำหนดขนาดของ Array
                }

                int maxValue = number.Max();
                if (maxValue > 100)
                    dgvOtherScan.PreferredRowHeight = (maxValue / 2) + 20;
                else if (maxValue < 50)
                    dgvOtherScan.PreferredRowHeight = maxValue + 20;
                else if (maxValue > 50)
                    dgvOtherScan.PreferredRowHeight = maxValue;
                else
                    dgvOtherScan.PreferredRowHeight = 50;
                */

                dgvOtherScan.DataSource = dt_output_all;
                this.lblTotalRecord.Text = "Total : " + dt_output_all.Rows.Count;
            }
        }

        #endregion

        #region Event

        private void frmOtherScanViewData_Load(object sender, EventArgs e)
        {
            try
            {
                // Display
                Display();
            }
            catch (Exception ex)
            {
                Executing.Instance.Insert_Log("ERRLOAD_NEWBARCODE_SC4", ex.Message.ToString(), "frmOtherScanViewData", "frmOtherScanViewData_Load");
            }
        }
        private void frmOtherScanViewData_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void lklLogout_Click(object sender, EventArgs e)
        {
            Pages.Menus.frmMenuViewByFunctions fView = new UserInterfaces.Pages.Menus.frmMenuViewByFunctions();
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