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
using EntitiesTemp.GlobalPassword;
using SPACE.TJG.Barcode.UI.Main;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmMenuDeleteAllGRProduction : Form
    {
        #region Member

        private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmMenuDeleteAllGRProduction()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void lklLogout_Click(object sender, EventArgs e)
        {
            frmMenuDeleteGRProduction frmMenuDeleteGRProduction = new frmMenuDeleteGRProduction();
            frmMenuDeleteGRProduction.Show();
            this.Close();
        }

        private void llbDeleteAllData_Click(object sender, EventArgs e)
        {
            if (Executing.Instance.DeleteGRFromProduction())
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
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
                        base.Close();
                    }
                    else if (string.IsNullOrEmpty(GlobalPass._password))
                    {
                        base.Close();
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

        private void frmMenuDeleteAllGRProduction_Load(object sender, EventArgs e)
        {

        }
        private void frmMenuDeleteAllGRProduction_KeyUp(object sender, KeyEventArgs e)
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

        private void timer_Tick(object sender, EventArgs e)
        {
            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}