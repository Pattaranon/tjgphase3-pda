using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SPACE.TJG.Barcode.UI.Main;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmMenuDeleteData : Form
    {
        #region Member

        string _str_pass_code = string.Empty;

        #endregion

        #region Constructor

        public frmMenuDeleteData(string _pass_code)
        {
            InitializeComponent();
            this._str_pass_code = _pass_code.Trim();
        }

        #endregion

        #region Event

        private void frmMenuDeleteData_Load(object sender, EventArgs e)
        {
        }
        private void frmMenuDeleteData_KeyUp(object sender, KeyEventArgs e)
        {
            if (this._str_pass_code.Trim() == "3792")
            {
                switch (e.KeyCode)
                {
                    case System.Windows.Forms.Keys.D1:
                        llbDistributionSale_Click(sender, e);
                        break;
                    case System.Windows.Forms.Keys.D2:
                        llbDistributionRet_Click(sender, e);
                        break;
                    case System.Windows.Forms.Keys.Up:
                        lklLogout_Click(sender, e);
                        break;
                    default:
                        break;
                }
            }
            else if (this._str_pass_code.Trim() == "3018")
            {
                switch (e.KeyCode)
                {
                    case System.Windows.Forms.Keys.D3:
                        //llbRecordSerialLot_Click(sender, e);
                        break;
                    case System.Windows.Forms.Keys.D4:
                        llbGRProduction_Click(sender, e);
                        break;
                    case System.Windows.Forms.Keys.D5:
                        llbGItoProduction_Click(sender, e);
                        break;
                    case System.Windows.Forms.Keys.Up:
                        lklLogout_Click(sender, e);
                        break;
                    default:
                        break;
                }
            }
            else if (this._str_pass_code.Trim() == "5416")
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
        }

        private void llbGRRefPO_Click(object sender, EventArgs e)
        {

        }
        private void llbDistributionSale_Click(object sender, EventArgs e)
        {
            if (this._str_pass_code.Trim() == "3792")
            {
                frmMenuDistributionSaleForDelete frmMenuDistributionSaleForDelete = new frmMenuDistributionSaleForDelete();
                frmMenuDistributionSaleForDelete.Show();
                this.Close();
            }
        }
        private void llbDistributionRet_Click(object sender, EventArgs e)
        {
            if (this._str_pass_code.Trim() == "3792")
            {
                frmMenuDeleteDataForDistributionSale frmMenuDeleteDataForDistributionSale = new frmMenuDeleteDataForDistributionSale("2");
                frmMenuDeleteDataForDistributionSale.Show();
                this.Close();
            }
        }
        private void llbGRProduction_Click(object sender, EventArgs e)
        {
            if (this._str_pass_code.Trim() == "3018")
            {
                frmMenuDeleteGRProduction frmMenuDeleteGRProduction = new frmMenuDeleteGRProduction();
                frmMenuDeleteGRProduction.Show();
                this.Close();
            }
        }
        private void llbGItoProduction_Click(object sender, EventArgs e)
        {
            if (this._str_pass_code.Trim() == "3018")
            {
                frmMenuDeleteGIToProduction frmMenuDeleteGIToProduction = new frmMenuDeleteGIToProduction();
                frmMenuDeleteGIToProduction.Show();
                this.Close();
            }
        }
        private void llbTransfer_Click(object sender, EventArgs e)
        {
        }
        private void llbPhysicalCount_Click(object sender, EventArgs e)
        {
        }

        private void lklLogout_Click(object sender, EventArgs e)
        {
            frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
            frmMainMenu.Show();
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