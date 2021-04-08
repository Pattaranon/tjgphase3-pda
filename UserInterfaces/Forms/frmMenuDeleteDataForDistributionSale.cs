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
using UserInterfaces.Execute;

namespace UserInterfaces.Forms
{
    public partial class frmMenuDeleteDataForDistributionSale : Form
    {
        #region Member

        string _str_menu_name = string.Empty;
        public string result_message { get; set; }

        #endregion

        #region Constructor

        public frmMenuDeleteDataForDistributionSale(string _menu_name)
        {
            InitializeComponent();
            this._str_menu_name = _menu_name.Trim();
        }

        #endregion

        #region Event

        private void frmMenuDeleteDataForDistributionSale_Load(object sender, EventArgs e)
        {
            this.result_message = ((this.result_message == null) ? string.Empty : this.result_message.Trim());
            if (this.result_message == string.Empty)
            {
                this.lblMessage.Hide();
            }
            else
            {
                this.lblMessage.Show();
                this.lblMessage.Text = this.result_message.Trim();
            }
        }
        private void frmMenuDeleteDataForDistributionSale_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    llbDeleteAllData_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D2:
                    lblDeleteByInvoice_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Down:
                    //lnkDel_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void llbDeleteAllData_Click(object sender, EventArgs e)
        {
            if (this._str_menu_name.Trim() == "1.2")
            {
                if (Executing.Instance.DeleteDistributionSale())
                {
                    frmMenuDeleteData frmMenuDeleteData = new frmMenuDeleteData(GlobalPass._password);
                    frmMenuDeleteData.Show();
                    this.Close();
                }
            }
            else if (this._str_menu_name.Trim() == "2")
            {
                if (Executing.Instance.DeleteDistributionRet())
                {
                    frmMenuDeleteData frmMenuDeleteData = new frmMenuDeleteData(GlobalPass._password);
                    frmMenuDeleteData.Show();
                    this.Close();
                }
            }
        }
        private void lblDeleteByInvoice_Click(object sender, EventArgs e)
        {
            if (this._str_menu_name.Trim() == "1.2")
            {
                frmDeleteDODistributionSale frmDeleteDODistributionSale = new frmDeleteDODistributionSale("1.2");
                frmDeleteDODistributionSale.Show();
                base.Close();
            }
            else if (!(this._str_menu_name.Trim() == "1.1"))
            {
                if (this._str_menu_name.Trim() == "2")
                {
                    frmDeleteDODistributionSale frmDeleteDODistributionSale = new frmDeleteDODistributionSale("2");
                    frmDeleteDODistributionSale.Show();
                    base.Close();
                }
            }
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
            this.timer.Interval = 6000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}