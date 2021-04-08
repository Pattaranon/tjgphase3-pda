using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UserInterfaces.Forms
{
    public partial class frmMenuDownloadCustomer : Form
    {
        #region Memberm

        bool _close_form = false;

        #endregion

        #region Constructor

        public frmMenuDownloadCustomer()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmMenuDownloadCustomer_Load(object sender, EventArgs e)
        {
            button1.Focus();
            this.lblMessage.Hide();
        }
        private void frmMenuDownloadCustomer_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                    lklNO_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Down:
                    lnkYES_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void lklNO_Click(object sender, EventArgs e)
        {
            this._close_form = true;
            this.lblMessage.Show();
            this.lblMessage.Text = "No Download";
        }
        private void lnkYES_Click(object sender, EventArgs e)
        {
            frmPermissionDownloadCustomer frmPermissionDownloadCustomer = new frmPermissionDownloadCustomer();
            frmPermissionDownloadCustomer.Show();
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this._close_form)
            {
                this.timer.Interval = 6000;
                this.Close();
            }
            else
            {
                this.timer.Interval = 6000;
                this.lblMessage.Hide();

                this.batt.UpdateBatteryLife();
                this.batt.Refresh();
            }
        }

        #endregion
    }
}