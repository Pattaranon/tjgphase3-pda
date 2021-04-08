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
    public partial class frmMenuSendDataDistributionSale : Form
    {
        #region Constructor

        public frmMenuSendDataDistributionSale()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmMenuSendDataDistributionSale_Load(object sender, EventArgs e)
        {

        }
        private void frmMenuSendDataDistributionSale_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    llbGIRefDelivery_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D2:
                    //llbDistributionRet_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void llbGIRefInvoice_Click(object sender, EventArgs e)
        {

        }
        private void llbGIRefDelivery_Click(object sender, EventArgs e)
        {

        }
        private void lklLogout_Click(object sender, EventArgs e)
        {
            frmMenuSendData frmMenuSendData = new frmMenuSendData();
            frmMenuSendData.Show();
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