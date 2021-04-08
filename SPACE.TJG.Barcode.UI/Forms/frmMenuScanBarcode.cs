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
    public partial class frmMenuScanBarcode : Form
    {
        #region Constructor
        public frmMenuScanBarcode()
        {
            InitializeComponent();
        }
        #endregion

        #region Event

        private void frmMenuScanBarcode_Load(object sender, EventArgs e)
        {

        }
        private void frmMenuScanBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    llbDistributionSale_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D2:
                    llbDistributionRet_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D3:
                    //llbRecordSerialLot_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D4:
                    llbGRProduction_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D5:
                    llbGItoProduction_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D6:
                    //llbGI_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D7:
                    //llbGI_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void llbDistributionSale_Click(object sender, EventArgs e)
        {
            frmMenuDistributionSale frmMenuDistributionSale = new frmMenuDistributionSale();
            frmMenuDistributionSale.Show();
            this.Close();
        }
        private void llbDistributionRet_Click(object sender, EventArgs e)
        {
            frmDistributionRet frmDistributionRet = new frmDistributionRet();
            frmDistributionRet.Show();
            this.Close();
        }
        private void llbGRRefPO_Click(object sender, EventArgs e)
        {

        }
        private void llbGRProduction_Click(object sender, EventArgs e)
        {
            frmMenuGRProduction frmMenuGRProduction = new frmMenuGRProduction();
            frmMenuGRProduction.Show();
            this.Close();
        }
        private void llbGItoProduction_Click(object sender, EventArgs e)
        {
            frmMenuGIProduction frmMenuGIProduction = new frmMenuGIProduction();
            frmMenuGIProduction.Show();
            this.Close();
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