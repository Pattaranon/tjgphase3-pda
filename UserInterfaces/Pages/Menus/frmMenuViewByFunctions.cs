using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UserInterfaces.Pages.Screens;
using UserInterfaces.HomeScreen;

namespace UserInterfaces.Pages.Menus
{
    public partial class frmMenuViewByFunctions : Form
    {
        #region Constrcutor

        public frmMenuViewByFunctions()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmMenuViewByFunctions_Load(object sender, EventArgs e)
        {

        }
        private void frmMenuViewByFunctions_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    llbWarehouseData_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D2:
                    llbDeliveryData_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D3:
                    llbOtherScanData_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void llbWarehouseData_Click(object sender, EventArgs e)
        {
            frmPasswordViewByFunction frmViewWH = new frmPasswordViewByFunction("Warehouse", "MENU_VIEW_WAREHOUSE");
            frmViewWH.Show();
            this.Close();
        }
        private void llbDeliveryData_Click(object sender, EventArgs e)
        {
            frmPasswordViewByFunction frmViewWH = new frmPasswordViewByFunction("Delivery", "MENU_VIEW_DELIVERY");
            frmViewWH.Show();
            this.Close();
        }
        private void llbOtherScanData_Click(object sender, EventArgs e)
        {
            frmPasswordViewByFunction frmViewWH = new frmPasswordViewByFunction("OtherScan", "MENU_VIEW_OTHERSCAN");
            frmViewWH.Show();
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
            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}