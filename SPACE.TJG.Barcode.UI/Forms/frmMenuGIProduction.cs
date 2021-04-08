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
    public partial class frmMenuGIProduction : Form
    {
        #region Constructor

        public frmMenuGIProduction()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmMenuGIProduction_Load(object sender, EventArgs e)
        {
            button1.Focus();
        }
        private void frmMenuGIProduction_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    llbGIRefReserEmpty_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D2:
                    llbGIRefReserFull_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D3:
                    llbGIRefIOEmpty_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void lklLogout_Click(object sender, EventArgs e)
        {
            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
            frmMenuScanBarcode.Show();
            this.Close();
        }
        private void llbGIRefReserEmpty_Click(object sender, EventArgs e)
        {

        }
        private void llbGIRefReserFull_Click(object sender, EventArgs e)
        {

        }
        private void llbGIRefIOEmpty_Click(object sender, EventArgs e)
        {
            frmGIRefIOEmpty frmGIRefIOEmpty = new frmGIRefIOEmpty();
            frmGIRefIOEmpty.Show();
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