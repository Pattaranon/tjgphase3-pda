using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using EntitiesTemp.GlobalPassword;
using SPACE.TJG.Barcode.UI.Execute;
using SPACE.TJG.Barcode.UI.Main;
using ServiceFunction.Cursor;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmMenuDelete : Form
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

        #region Constrcutor

        public frmMenuDelete(string _pram_menu, string _menu, string _str_param1, string _str_param2, string _str_param3, string _str_param4, string _str_param5)
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

        private void frmMenuDelete_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_str_menu))
            {
                if (_str_menu == "DEL")
                {
                    this.lblMenu.Text = "--- ENTER PASSWORD ---";
                }
                else if (_str_menu == "DEL_ALL")
                {
                    this.lblMenu.Text = "--- DELETE ALL DATA ---";
                }
            }

            this.lblMessage.Hide();
        }
        private void frmMenuDelete_KeyUp(object sender, KeyEventArgs e)
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
                else if (this._menu_name.Trim() == "GIRefDelivery")
                {
                    #region Menu GIRefDelivery

                    GlobalPass._password = this.txtPassword.Text.Trim();
                    if (this.txtPassword.Text.Trim() == "3792")
                    {
                        if (Executing.Instance.DeleteGIDelivery(this._param1, this._param2))
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Delete 1 Records";
                                new frmGIRefDelivery
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
                else if (this._menu_name.Trim() == "MENU_DEL")
                {
                    #region Menu MENU_DEL

                    GlobalPass._password = this.txtPassword.Text.Trim();
                    if (this.txtPassword.Text.Trim() == "3792")
                    {
                        frmMenuDeleteData frmMenuDeleteData = new frmMenuDeleteData("3792");
                        frmMenuDeleteData.Show();
                        this.Close();
                    }
                    else if (this.txtPassword.Text.Trim() == "3018")
                    {
                        frmMenuDeleteData frmMenuDeleteData = new frmMenuDeleteData("3018");
                        frmMenuDeleteData.Show();
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
                else if (this._menu_name.Trim() == "MENU_DEL_ALL")
                {
                    #region Menu MENU_DEL_ALL

                    GlobalPass._password = this.txtPassword.Text.Trim();
                    if (this.txtPassword.Text.Trim() == "5416")
                    {
                        if (Executing.Instance.DeleteAllInSqlCeOnPDA())
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
                                frmMainMenu.Show();
                                this.Close();
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
                else if (this._menu_name.Trim() == "1.2_DEL_ALL")
                {
                    #region Menu 1.2_DEL_ALL

                    GlobalPass._password = this.txtPassword.Text.Trim();
                    if (this.txtPassword.Text.Trim() == "5416")
                    {
                        if (Executing.Instance.DeleteAllInSqlCeOnPDA())
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                frmMenuDeleteDataForDistributionSale frmMenuDeleteDataForDistributionSale = new frmMenuDeleteDataForDistributionSale("1.2");
                                frmMenuDeleteDataForDistributionSale.Show();
                                this.Close();
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
                else if (this._menu_name.Trim() == "4.1_DEL_ALL")
                {
                    #region Menu 4.1_DEL_ALL

                    GlobalPass._password = this.txtPassword.Text.Trim();
                    if (this.txtPassword.Text.Trim() == "5416")
                    {
                        if (Executing.Instance.DeleteAllInSqlCeOnPDA())
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                frmMenuDeleteAllGRProduction frmMenuDeleteAllGRProduction = new frmMenuDeleteAllGRProduction();
                                frmMenuDeleteAllGRProduction.Show();
                                this.Close();
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
                else if (this._menu_name.Trim() == "5.3_DEL_ALL")
                {
                    #region Menu 5.3_DEL_ALL

                    GlobalPass._password = this.txtPassword.Text.Trim();
                    if (this.txtPassword.Text.Trim() == "5416")
                    {
                        if (Executing.Instance.DeleteAllInSqlCeOnPDA())
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                frmMenuDeleteAllGIRefEmpty frmMenuDeleteAllGIRefEmpty = new frmMenuDeleteAllGIRefEmpty();
                                frmMenuDeleteAllGIRefEmpty.Show();
                                this.Close();
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
                else if (this._menu_name.Trim() == "DistributionRet")
                {
                    #region Menu DistributionRet

                    GlobalPass._password = this.txtPassword.Text.Trim();
                    if (this.txtPassword.Text.Trim() == "3792")
                    {
                        if (Executing.Instance.DeleteDistributionRet(this._param1, this._param2, this._param3, this._param4, this._param5))
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Delete 1 Records";
                                new frmDistributionRet
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
            }
        }

        private void lklLogout_Click(object sender, EventArgs e)
        {
            try
            {
                UICursor.CursorWait();
                if (this._menu_name.Trim() == "MENU_DEL")
                {
                    frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
                    frmMainMenu.Show();
                    this.Close();
                }
                else if (this._menu_name.Trim() == "MENU_DEL_ALL")
                {
                    frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
                    frmMainMenu.Show();
                    this.Close();
                }
                else if (this._str_menu.Trim() == "DEL" && this._menu_name == "GIRefDelivery")
                {
                    frmSelectGIDeliveryDel frmSelectGIDeliveryDel = new frmSelectGIDeliveryDel();
                    frmSelectGIDeliveryDel.Show();
                    this.Close();
                }
                else if (this._str_menu.Trim() == "DEL" && this._menu_name.Trim() == "DistributionRet")
                {
                    frmSelectDistributionRetDel frmSelectDistributionRetDel = new frmSelectDistributionRetDel();
                    frmSelectDistributionRetDel.Show();
                    this.Close();
                }
                else if (this._str_menu.Trim() == "DEL_ALL" && this._menu_name.Trim() == "1.2_DEL_ALL")
                {
                    frmMenuDeleteDataForDistributionSale frmMenuDeleteDataForDistributionSale = new frmMenuDeleteDataForDistributionSale("1.2");
                    frmMenuDeleteDataForDistributionSale.Show();
                    this.Close();
                }
                else
                {
                    frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
                    frmMainMenu.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Executing.Instance.Insert_Log("ERR999", "Click Logout MenuDelete : " + ex.Message.ToString(), "frmMenuDelete", "lklLogout_Click");
            }
            finally
            {
                UICursor.CursorStop();
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