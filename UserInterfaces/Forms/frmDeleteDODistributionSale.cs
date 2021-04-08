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
using UserInterfaces.Execute;

namespace UserInterfaces.Forms
{
    public partial class frmDeleteDODistributionSale : Form
    {
        #region Memberm

        private string _str_menu_name = string.Empty;
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmDeleteDODistributionSale(string _menu_name)
        {
            InitializeComponent();
            this._str_menu_name = _menu_name.Trim();
        }

        #endregion

        #region Event

        private void lklLogout_Click(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Interval = 3000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        private void frmDeleteDODistributionSale_Load(object sender, EventArgs e)
        {
            this.lblMessage.Hide();
            if (this._str_menu_name.Trim() == "1.2")
            {
                this.lblDO.Text = "Invoice/DO :";
                this.txtInvoiceDO.MaxLength = 15;
            }
            else if (this._str_menu_name.Trim() == "2")
            {
                this.lblDO.Text = "Cust Code :";
                this.txtInvoiceDO.MaxLength = 7;
            }

            this.txtInvoiceDO.Focus();
            this.txtInvoiceDO.SelectAll();
        }
        private void frmDeleteDODistributionSale_KeyUp(object sender, KeyEventArgs e)
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

        private void txtInvoiceDO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtInvoiceDO.Text.Trim()))
                {
                    #region Empty

                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                        frmMenuDeleteDataForDistributionSale frmMenuDeleteDataForDistributionSale = new frmMenuDeleteDataForDistributionSale("1.2");
                        frmMenuDeleteDataForDistributionSale.Show();
                        this.Close();
                    }

                    #endregion
                }
                else if (this._str_menu_name.Trim() == "1.2")
                {
                    #region Menu Delete DO 1.2

                    if (Executing.Instance.CountRowDeleteGIByDO(this.txtInvoiceDO.Text.Trim()) == "0")
                    {
                        using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                        {
                            soundPlayer.Play();
                            Thread.Sleep(1000);
                            this.lblMessage.Show();
                            this.lblMessage.Text = "No Invoice/Do Found";
                            frmMenuDeleteDataForDistributionSale frmMenuDeleteDataForDistributionSale = new frmMenuDeleteDataForDistributionSale("1.2");
                            frmMenuDeleteDataForDistributionSale.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        if (Executing.Instance.DeleteGIDeliveryByDO(this.txtInvoiceDO.Text.Trim()))
                        {
                            this.lblMessage.Show();
                            this.lblMessage.Text = "Delete " + Executing.Instance.CountRowDeleteGIByDO(this.txtInvoiceDO.Text.Trim()) + " Records";
                            frmMenuDeleteDataForDistributionSale frmMenuDeleteDataForDistributionSale = new frmMenuDeleteDataForDistributionSale("1.2");
                            frmMenuDeleteDataForDistributionSale.Show();
                            this.Close();
                        }
                    }

                    #endregion
                }
                else if (this._str_menu_name.Trim() == "2")
                {
                    if (Executing.Instance.CountRowDeleteDistributionRetByCustCode(this.txtInvoiceDO.Text.Trim()) == "0")
                    {
                        using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                        {
                            soundPlayer.Play();
                            Thread.Sleep(1000);
                            this.lblMessage.Show();
                            this.lblMessage.Text = "No Invoice/Do Found";
                            frmMenuDeleteDataForDistributionSale frmMenuDeleteDataForDistributionSale = new frmMenuDeleteDataForDistributionSale("2");
                            frmMenuDeleteDataForDistributionSale.Show();
                            base.Close();
                        }
                    }
                    else
                    {
                        if (Executing.Instance.DeleteDistributionRetByCustCode(this.txtInvoiceDO.Text.Trim()))
                        {
                            this.lblMessage.Show();
                            this.lblMessage.Text = "Delete " + Executing.Instance.CountRowDeleteDistributionRetByCustCode(this.txtInvoiceDO.Text.Trim()) + " Records";
                            frmMenuDeleteDataForDistributionSale frmMenuDeleteDataForDistributionSale = new frmMenuDeleteDataForDistributionSale("2");
                            frmMenuDeleteDataForDistributionSale.Show();
                            this.Close();
                        }
                    }
                }
            }
        }

        #endregion
    }
}