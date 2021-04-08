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

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmPermissionDownloadCustomer : Form
    {
        #region Member

        private string _str_menu = string.Empty;
        private string _menu_name = string.Empty;
        private string _param1 = string.Empty;
        private string _param2 = string.Empty;
        private string _param3 = string.Empty;
        private string _param4 = string.Empty;
        private string _param5 = string.Empty;

        //private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmPermissionDownloadCustomer()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmPermissionDownloadCustomer_Load(object sender, EventArgs e)
        {
            this.lblMessage.Hide();

            this.txtPassword.Focus();
            this.txtPassword.SelectAll();
        }
        private void frmPermissionDownloadCustomer_KeyUp(object sender, KeyEventArgs e)
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
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else if (this.txtPassword.Text.Trim() == Execute.Executing.Instance.getPassword("DOWNLOAD_CUSTOMER"))
                {
                    /* Use connection online
                    frmModeImportCustomer frmMode = new frmModeImportCustomer();
                    frmMode.Show();
                    this.Close();
                    */

                    frmImportCustomer frmImportCustomer = new frmImportCustomer();
                    frmImportCustomer.Show();
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
            }
        }

        private void lklLogout_Click(object sender, EventArgs e)
        {
            frmMenuDownloadCustomer fDownloadCustomer = new frmMenuDownloadCustomer();
            fDownloadCustomer.Show();
            this.Close();
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