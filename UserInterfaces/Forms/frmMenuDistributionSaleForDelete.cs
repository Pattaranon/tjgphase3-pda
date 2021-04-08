using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EntitiesTemp.GlobalPassword;
using UserInterfaces.Main;

namespace UserInterfaces.Forms
{
    public partial class frmMenuDistributionSaleForDelete : Form
    {
        #region Constructor

        public frmMenuDistributionSaleForDelete()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmMenuDistributionSaleForDelete_Load(object sender, EventArgs e)
        {

        }
        private void frmMenuDistributionSaleForDelete_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    llbGIRefInvoice_Click(sender, e);
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
            frmMenuDeleteDataForDistributionSale frmMenuDeleteDataForDistributionSale = new frmMenuDeleteDataForDistributionSale("1.2");
            frmMenuDeleteDataForDistributionSale.Show();
            this.Close();
        }

        private void lklLogout_Click(object sender, EventArgs e)
        {
            if (GlobalPass._password.Trim() == "5416")
            {
                frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
                frmMainMenu.Show();
                this.Close();
            }
            else if (GlobalPass._password.Trim() == "3792" || GlobalPass._password.Trim() == "3018")
            {
                frmMenuDeleteData frmMenuDeleteData = new frmMenuDeleteData(GlobalPass._password);
                frmMenuDeleteData.Show();
                this.Close();
            }
            else if (string.IsNullOrEmpty(GlobalPass._password))
            {
                this.Close();
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}