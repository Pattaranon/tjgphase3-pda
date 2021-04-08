using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EntitiesTemp.GlobalPassword;
using SPACE.TJG.Barcode.UI.Main;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmMenuDeleteGIToProduction : Form
    {
        #region Constructor

        public frmMenuDeleteGIToProduction()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmMenuDeleteGIToProduction_Load(object sender, EventArgs e)
        {

        }
        private void frmMenuDeleteGIToProduction_KeyUp(object sender, KeyEventArgs e)
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

        private void llbGIRefIOEmpty_Click(object sender, EventArgs e)
        {
            frmMenuDeleteAllGIRefEmpty frmMenuDeleteAllGIRefEmpty = new frmMenuDeleteAllGIRefEmpty();
            frmMenuDeleteAllGIRefEmpty.Show();
            this.Close();
        }
        private void llbGIRefReserEmpty_Click(object sender, EventArgs e)
        {

        }
        private void llbGIRefReserFull_Click(object sender, EventArgs e)
        {

        }
        private void lklLogout_Click(object sender, EventArgs e)
        {
            if (GlobalPass._password.Trim() == "5416")
            {
                frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
                frmMainMenu.Show();
                base.Close();
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