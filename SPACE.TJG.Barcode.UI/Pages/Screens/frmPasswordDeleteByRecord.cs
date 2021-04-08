using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SPACE.TJG.Barcode.UI.Execute;
using System.Media;
using System.Threading;
using ServiceFunction.Cursor;

namespace SPACE.TJG.Barcode.UI.Pages.Screens
{
    public partial class frmPasswordDeleteByRecord : Form
    {
        #region Member

        string _str_menu = string.Empty;
        string _menu_name = string.Empty;
        string _param1 = string.Empty;
        string _param2 = string.Empty;
        string _param3 = string.Empty;
        string _param4 = string.Empty;
        string _param5 = string.Empty;

        string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmPasswordDeleteByRecord(string _pram_menu, string _menu, string _str_param1, string _str_param2, string _str_param3, string _str_param4, string _str_param5)
        {
            InitializeComponent();
            this._str_menu = _pram_menu.Trim();
            this._menu_name = _menu;
            this._param1 = _str_param1;
            this._param2 = _str_param2;
            this._param3 = _str_param3;
            this._param4 = _str_param4;
            this._param5 = _str_param5;
        }

        #endregion

        #region Event

        private void frmPasswordDeleteByRecord_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_str_menu))
            {
                if (_str_menu == "DEL_DELIVERY_BY_RECORD")
                {
                    this.lblMenu.Text = "--- DELETE DELIVERY ---";
                }
                else if (_str_menu == "DEL_WAREHOUSE_BY_RECORD")
                {
                    this.lblMenu.Text = "--- DELETE WAREHOUSE ---";
                }
                else 
                {
                    this.lblMenu.Text = "--- ENTER PASSWORD ---";
                }
            }

            this.lblMessage.Hide();

            this.txtPassword.Focus();
            this.txtPassword.SelectAll();
        }
        private void frmPasswordDeleteByRecord_KeyUp(object sender, KeyEventArgs e)
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
                    if (this._menu_name.Trim() == "MENU_DEL_DELIVERY_BY_RECORD")
                    {
                        #region Menu Delete by record delivery

                        if (this.txtPassword.Text.Trim() == Executing.Instance.getPassword("DELETE_DELIVERY_DATA_BY_RECORD"))
                        {
                            _function = "DEL_DELIVERY_SC6";
                            if (Executing.Instance.DeleteDelivery(this._param1, this._param2, this._param3, this._param4, this._param5))
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    this.lblMessage.Show();
                                    this.lblMessage.Text = "Delete 1 Records";
                                    new frmDelivery
                                    {
                                        status_del = true
                                    }.Show();
                                    this.Close();
                                }
                            }
                            else
                            {
                                this.lblMessage.Show();
                                this.lblMessage.Text = "DELETE NOT DATA";
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
                    else if (this._menu_name.Trim() == "MENU_DEL_WAREHOUSE_BY_RECORD")
                    {
                        #region Menu Delete by record warehouse

                        if (this.txtPassword.Text.Trim() == Executing.Instance.getPassword("DELETE_WAREHOUSE_DATA_BY_RECORD"))
                        {
                            _function = "DEL_WH_SC6";
                            if (Executing.Instance.DoDeleteDelivery(this._param1, this._param2, this._param3, this._param4))
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    this.lblMessage.Show();
                                    this.lblMessage.Text = "Delete 1 Records";
                                    frmWarehouse fWarehouse = new frmWarehouse();
                                    fWarehouse.Show();
                                    this.Close();
                                }
                            }
                            else
                            {
                                this.lblMessage.Show();
                                this.lblMessage.Text = "DELETE NOT DATA";
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
                    Executing.Instance.Insert_Log(_function, ex.Message.ToString(), "frmPasswordDeleteByRecord", "txtPassword_KeyDown");
                }
            }
        }

        private void lklLogout_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._str_menu.Trim() == "DEL_DELIVERY_BY_RECORD" && this._menu_name.Trim() == "MENU_DEL_DELIVERY_BY_RECORD")
                {
                    frmSelectDeliveryDel frmSelectDeliveryDel = new frmSelectDeliveryDel();
                    frmSelectDeliveryDel.Show();
                    this.Close();
                }
                else if (this._str_menu.Trim() == "DEL_WAREHOUSE_BY_RECORD" && this._menu_name.Trim() == "MENU_DEL_WAREHOUSE_BY_RECORD")
                {
                    frmSelectWarehouseDel frmSWarehouseDel = new frmSelectWarehouseDel();
                    frmSWarehouseDel.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Executing.Instance.Insert_Log("ERRLOGOUT_SC2_1", ex.Message.ToString(), "frmPasswordDeleteByRecord", "lklLogout_Click");
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