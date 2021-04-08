using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmMenuGRProduction : Form
    {
        #region Constructor

        public frmMenuGRProduction()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmMenuGRProduction_Load(object sender, EventArgs e)
        {
            this.button1.Focus();
        }
        private void frmMenuGRProduction_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    llbGRFromProduction_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D2:
                    llbGRFromChange_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void llbGRFromProduction_Click(object sender, EventArgs e)
        {
            frmGRFromProduction frmGRFromProduction = new frmGRFromProduction();
            frmGRFromProduction.Show();
            this.Close();
        }
        private void llbGRFromChange_Click(object sender, EventArgs e)
        {

        }
        private void lklLogout_Click(object sender, EventArgs e)
        {
            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
            frmMenuScanBarcode.Show();
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