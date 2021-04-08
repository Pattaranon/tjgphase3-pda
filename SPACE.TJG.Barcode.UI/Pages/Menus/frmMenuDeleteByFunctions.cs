using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SPACE.TJG.Barcode.UI.Pages.Screens;
using SPACE.TJG.Barcode.UI.HomeScreen;

namespace SPACE.TJG.Barcode.UI.Pages.Menus
{
    public partial class frmMenuDeleteByFunctions : Form
    {
        #region Member

        public bool status_del { get; set; }

        #endregion

        #region Constructor

        public frmMenuDeleteByFunctions()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmMenuDeleteByFunctions_Load(object sender, EventArgs e)
        {
            this.lblMessage.Hide();

            if (this.status_del)
            {
                this.lblMessage.Show();
                this.lblMessage.Text = "Delete Successful";
            }
        }
        private void frmMenuDeleteByFunctions_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    llbDeleteAll_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D2:
                    llbDeleteWarehouse_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D3:
                    llbDeleteDelivery_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D4:
                    llbDeleteOtherScan_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void llbDeleteAll_Click(object sender, EventArgs e)
        {
            // Delete All
            frmPasswordDeleteByFunction frmMenuDelete = new frmPasswordDeleteByFunction("DEL_ALL", "MENU_DEL_ALL");
            frmMenuDelete.Show();
            this.Close();
        }
        private void llbDeleteWarehouse_Click(object sender, EventArgs e)
        {
            // Delete W/H
            frmPasswordDeleteByFunction frmMenuDelete = new frmPasswordDeleteByFunction("DEL_WH", "MENU_DEL_WH");
            frmMenuDelete.Show();
            this.Close();
        }
        private void llbDeleteDelivery_Click(object sender, EventArgs e)
        {
            // Delete Delivery
            frmPasswordDeleteByFunction frmMenuDelete = new frmPasswordDeleteByFunction("DEL_DELIVERY", "MENU_DEL_DELIVERY");
            frmMenuDelete.Show();
            this.Close();
        }
        private void llbDeleteOtherScan_Click(object sender, EventArgs e)
        {
            // Delete other scan
            frmPasswordDeleteByFunction frmMenuDelete = new frmPasswordDeleteByFunction("DEL_OTHERSCAN", "MENU_DEL_OTHERSCAN");
            frmMenuDelete.Show();
            this.Close();
        }
        private void lklLogout_Click(object sender, EventArgs e)
        {
            frmHomeScreen frmMainMenu = new frmHomeScreen("LIFETIME");
            frmMainMenu.Show();
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 4000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}