using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UserInterfaces.Execute;
using ServiceFunction.Cursor;
using UserInterfaces.HomeScreen;
using System.Media;
using System.Threading;
using UserInterfaces.Pages.Menus;

namespace UserInterfaces.Pages.Screens
{
    public partial class frmPasswordViewByFunction : Form
    {
        #region Member

        string _str_menu = string.Empty;
        string _menu_name = string.Empty;

        //string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmPasswordViewByFunction(string _pram_menu, string _menu)
        {
            InitializeComponent();
            this._str_menu = _pram_menu.Trim();
            this._menu_name = _menu.Trim();
        }

        #endregion

        #region Event

        private void frmPasswordViewByFunction_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_str_menu))
            {
                if (_str_menu == "DEL")
                {
                    this.lblMenu.Text = "-- ENTER PASSWORD --";
                }
                else if (_str_menu == "Warehouse")
                {
                    this.lblMenu.Text = "-- ENTER PASSWORD W/H --";
                }
                else if (_str_menu == "Delivery")
                {
                    this.lblMenu.Text = "-- ENTER PASSWORD DELIVERY --";
                    this.lblMenu.Font = new Font("Tahoma", 9, FontStyle.Bold);
                }
                else if (_str_menu == "OtherScan")
                {
                    this.lblMenu.Text = "-- ENTER PASSWORD BARCODE --";
                    this.lblMenu.Font = new Font("Tahoma", 9, FontStyle.Bold);
                }
            }

            this.lblMessage.Hide();

            this.txtPassword.Focus();
            this.txtPassword.SelectAll();
        }
        private void frmPasswordViewByFunction_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(this.txtPassword.Text.Trim()))
                    {
                        #region Text Empty

                        using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                        {
                            soundPlayer.Play();
                            Thread.Sleep(1000);
                        }

                        #endregion
                    }
                    else if (this._menu_name.Trim() == "MENU_VIEW_WAREHOUSE")
                    {
                        #region Menu View Warehouse

                        //GlobalPass._password = this.txtPassword.Text.Trim();
                        if (this.txtPassword.Text.Trim() == Executing.Instance.getPassword("VIEW_WH_DATA"))
                        {
                            Views.frmWarehouseViewData fViewWarehouse = new UserInterfaces.Pages.Views.frmWarehouseViewData();
                            fViewWarehouse.Show();
                            this.Close();
                        }
                        else
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Wrong Password!";
                                this.txtPassword.Text = string.Empty;
                                this.txtPassword.Focus();
                                this.txtPassword.SelectAll();
                            }
                        }

                        #endregion
                    }
                    else if (this._menu_name.Trim() == "MENU_VIEW_DELIVERY")
                    {
                        #region Menu View Delivery

                        if (this.txtPassword.Text.Trim() == Executing.Instance.getPassword("VIEW_DELIVERY_DATA"))
                        {
                            Views.frmDeliveryViewData fViewDelivery = new UserInterfaces.Pages.Views.frmDeliveryViewData();
                            fViewDelivery.Show();
                            this.Close();
                        }
                        else
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Wrong Password!";
                                this.txtPassword.Text = string.Empty;
                                this.txtPassword.Focus();
                                this.txtPassword.SelectAll();
                            }
                        }

                        #endregion
                    }
                    else if (this._menu_name.Trim() == "MENU_VIEW_OTHERSCAN")
                    {
                        #region Menu View Other Scan

                        if (this.txtPassword.Text.Trim() == Executing.Instance.getPassword("VIEW_OTHERSCAN_DATA"))
                        {
                            Views.frmOtherScanViewData fOtherScan = new UserInterfaces.Pages.Views.frmOtherScanViewData();
                            fOtherScan.Show();
                            this.Close();
                        }
                        else
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Wrong Password!";
                                this.txtPassword.Text = string.Empty;
                                this.txtPassword.Focus();
                                this.txtPassword.SelectAll();
                            }
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Executing.Instance.Insert_Log("VIEW_SC4", ex.Message.ToString(), "frmPasswordViewByFunction", "txtPassword_KeyDown");
                }
            }
        }
        private void lklLogout_Click(object sender, EventArgs e)
        {
            try
            {
                frmMenuViewByFunctions frmMainMenu = new frmMenuViewByFunctions();
                frmMainMenu.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                Executing.Instance.Insert_Log("ERR999", "Click Logout MenuDelete : " + ex.Message.ToString(), "frmMenuViewByFunctions", "lklLogout_Click");
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Interval = 3000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}