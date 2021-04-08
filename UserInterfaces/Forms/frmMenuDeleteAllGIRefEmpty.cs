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
using UserInterfaces.Main;
using UserInterfaces.Execute;

namespace UserInterfaces.Forms
{
    public partial class frmMenuDeleteAllGIRefEmpty : Form
    {
        #region Member

        private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmMenuDeleteAllGIRefEmpty()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmMenuDeleteAllGIRefEmpty_Load(object sender, EventArgs e)
        {

        }
        private void frmMenuDeleteAllGIRefEmpty_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    llbDeleteAllData_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void lklLogout_Click(object sender, EventArgs e)
        {
            frmMenuDeleteGIToProduction frmMenuDeleteGIToProduction = new frmMenuDeleteGIToProduction();
            frmMenuDeleteGIToProduction.Show();
            this.Close();
        }

        private void llbDeleteAllData_Click(object sender, EventArgs e)
        {
            if (Executing.Instance.DeleteGIRefIOEmpty())
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
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
            }
            else
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                }
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