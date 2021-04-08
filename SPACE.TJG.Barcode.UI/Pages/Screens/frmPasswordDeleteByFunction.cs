using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SPACE.TJG.Barcode.UI.Execute;
using ServiceFunction.Cursor;
using SPACE.TJG.Barcode.UI.HomeScreen;
using System.Media;
using System.Threading;
using SPACE.TJG.Barcode.UI.Pages.Menus;

namespace SPACE.TJG.Barcode.UI.Pages.Screens
{
    public partial class frmPasswordDeleteByFunction : Form
    {
        #region Member

        public string status_del { get; set; }
        string _str_menu = string.Empty;
        string _menu_name = string.Empty;

        string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmPasswordDeleteByFunction(string _pram_menu, string _menu)
        {
            InitializeComponent();
            this._str_menu = _pram_menu.Trim();
            this._menu_name = _menu.Trim();
        }

        #endregion

        #region Event

        private void frmPasswordDeleteByFunction_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_str_menu))
            {
                if (_str_menu == "DEL")
                {
                    this.lblMenu.Text = "-- ENTER PASSWORD --";
                }
                else if (_str_menu == "DEL_ALL")
                {
                    this.lblMenu.Text = "-- 5.1 DELETE ALL DATA --";
                }
                else if (_str_menu == "DEL_WH")
                {
                    this.lblMenu.Text = "-- 5.2 DELETE W/H DATA --";
                }
                else if (_str_menu == "DEL_DELIVERY")
                {
                    this.lblMenu.Text = "-- 5.3 DELETE DELIVERY DATA --";
                    this.lblMenu.Font = new Font("Tahoma", 9, FontStyle.Bold);
                }
                else if (_str_menu == "DEL_OTHERSCAN")
                {
                    this.lblMenu.Text = "-- 5.4 DELETE NEW BARCODE DATA --";
                    this.lblMenu.Font = new Font("Tahoma", 9, FontStyle.Bold);
                }
            }

            this.lblMessage.Hide();

            this.txtPassword.Text = string.Empty;
            this.txtPassword.Focus();
            this.txtPassword.SelectAll();
        }
        private void frmPasswordDeleteByFunction_KeyUp(object sender, KeyEventArgs e)
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
                string _function = string.Empty;
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
                    else if (this._menu_name.Trim() == "MENU_DEL_ALL")
                    {
                        #region Menu MENU_DEL_ALL

                        //GlobalPass._password = this.txtPassword.Text.Trim();
                        if (this.txtPassword.Text.Trim() == Executing.Instance.getPassword("DELETE_ALL_DATA"))
                        {
                            _function = "DEL_ALL_SC5";
                            if (Executing.Instance.DoDeleteAllInSqlCeOnPDA())
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    new frmMenuDeleteByFunctions
                                    {
                                        status_del = true
                                    }.Show();
                                    this.Close();

                                    /*
                                    frmMenuDeleteByFunctions frmMenuDelete = new frmMenuDeleteByFunctions();
                                    frmMenuDelete.Show();
                                    this.Close();
                                    */
                                }
                            }
                            else
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    this.lblMessage.Show();
                                    this.lblMessage.Text = "Delete don't success!";
                                    this.txtPassword.Text = string.Empty;
                                    this.txtPassword.Focus();
                                    this.txtPassword.SelectAll();
                                }
                            }
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
                    else if (this._menu_name.Trim() == "MENU_DEL_WH")
                    {
                        #region Menu DEL W/H

                        if (this.txtPassword.Text.Trim() == Executing.Instance.getPassword("DELETE_WH_DATA"))
                        {
                            _function = "DEL_WH_SC5";
                            if (Executing.Instance.DeleteWarehouse())
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    new frmMenuDeleteByFunctions
                                    {
                                        status_del = true
                                    }.Show();
                                    this.Close();

                                    /*
                                    frmMenuDeleteByFunctions frmMenuDelete = new frmMenuDeleteByFunctions();
                                    frmMenuDelete.Show();
                                    this.Close();
                                    */
                                }
                            }
                            else
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    this.lblMessage.Show();
                                    this.lblMessage.Text = "Delete don't success!";
                                    this.txtPassword.Text = string.Empty;
                                    this.txtPassword.Focus();
                                    this.txtPassword.SelectAll();
                                }
                            }
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
                    else if (this._menu_name.Trim() == "MENU_DEL_DELIVERY")
                    {
                        #region Menu DEL DELIVERY

                        if (this.txtPassword.Text.Trim() == Executing.Instance.getPassword("DELETE_DELIVERY_DATA"))
                        {
                            _function = "DEL_DELIVERY_SC5";
                            if (Executing.Instance.DeleteDelivery())
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    new frmMenuDeleteByFunctions
                                    {
                                        status_del = true
                                    }.Show();
                                    this.Close();

                                    /*
                                    frmMenuDeleteByFunctions frmMenuDelete = new frmMenuDeleteByFunctions();
                                    frmMenuDelete.Show();
                                    this.Close();
                                    */
                                }
                            }
                            else
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    this.lblMessage.Show();
                                    this.lblMessage.Text = "Delete don't success!";
                                    this.txtPassword.Text = string.Empty;
                                    this.txtPassword.Focus();
                                    this.txtPassword.SelectAll();
                                }
                            }
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
                    else if (this._menu_name.Trim() == "MENU_DEL_OTHERSCAN")
                    {
                        #region Menu DEL OTHER SCAN

                        if (this.txtPassword.Text.Trim() == Executing.Instance.getPassword("DELETE_OTHERSCAN_DATA"))
                        {
                            _function = "DEL_NEWBARCODE_SC5";
                            if (Executing.Instance.DeleteOtherScans())
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    new frmMenuDeleteByFunctions
                                    {
                                        status_del = true
                                    }.Show();
                                    this.Close();

                                    /*
                                    frmMenuDeleteByFunctions frmMenuDelete = new frmMenuDeleteByFunctions();
                                    frmMenuDelete.Show();
                                    this.Close();
                                    */
                                }
                            }
                            else
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    this.lblMessage.Show();
                                    this.lblMessage.Text = "Delete don't success!";
                                    this.txtPassword.Text = string.Empty;
                                    this.txtPassword.Focus();
                                    this.txtPassword.SelectAll();
                                }
                            }
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
                    Executing.Instance.Insert_Log(_function, ex.Message.ToString(), "frmPasswordDeleteByFunction", "txtPassword_KeyDown");
                }
            }
        }
        private void lklLogout_Click(object sender, EventArgs e)
        {
            try
            {
                UICursor.CursorWait();
                frmMenuDeleteByFunctions frmMainMenu = new frmMenuDeleteByFunctions();
                frmMainMenu.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                Executing.Instance.Insert_Log("ERR999", "Click Logout MenuDelete : " + ex.Message.ToString(), "frmPasswordDeleteByFunction", "lklLogout_Click");
            }
            finally
            {
                UICursor.CursorStop();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Interval = 5000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}