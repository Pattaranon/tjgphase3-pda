using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UserInterfaces.Main;
using System.Threading;
using System.Media;
using UserInterfaces.HomeScreen;
using UserInterfaces.Execute;

namespace UserInterfaces.Forms
{
    public partial class frmLogout : Form
    {
        #region Member

        //private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmLogout()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmLogout_Load(object sender, EventArgs e)
        {
            this.lblMessage.Hide();
            this.txtPassword.Focus();
            this.txtPassword.SelectAll();
        }
        private void frmLogout_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                    this.lklLogout_Click(sender, e);
                    break;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtPassword.Text.Trim()))
                {
                    #region Password Empty

                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }

                    #endregion
                }
                else if (this.txtPassword.Text.Trim() == Executing.Instance.getPassword("LOGOUT_SYSTEM"))
                {
                    #region Password Accept

                    Executing.Instance.IsSignOut();
                    Application.Exit();

                    #endregion
                }
                else
                {
                    #region Wrong Password

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

                    #endregion
                }
            }
        }
        private void lklLogout_Click(object sender, EventArgs e)
        {
            /*
            frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
            frmMainMenu.Show();
            this.Close();
            */

            frmHomeScreen frmMainMenu = new frmHomeScreen(string.Empty);
            frmMainMenu.Show();
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