using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ServiceFunction.Cursor;
using MsgBox.ClassMsgBox;
using UserInterfaces.Main;

namespace UserInterfaces.Forms
{
    public partial class frmMenuDistributionSale : Form
    {
        #region Constructor

        public frmMenuDistributionSale()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmMenuDistributionSale_Load(object sender, EventArgs e)
        {
            this.button1.Focus();
        }
        private void frmMenuDistributionSale_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    //llbDistributionSale_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D2:
                    llbGIRefDelivery_Click(sender, e);
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
            frmGIRefDelivery frmGIRefDelivery = new frmGIRefDelivery();
            frmGIRefDelivery.Show();
            this.Close();
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