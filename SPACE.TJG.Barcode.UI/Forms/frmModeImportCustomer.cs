using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SPACE.TJG.Barcode.UI.Configs;
using MsgBox.ClassMsgBox;
using ServiceFunction.Cursor;
using System.Net;
using ServiceFunction.Function;
using ServiceFunction.Function.Extension;
using SPACE.TJG.Barcode.UI.Execute;
using System.IO;
using SPACE.TJG.Barcode.UI.Settings;
using EntitiesTemp.Customer;
using SPACE.TJG.Barcode.UI.Main;
using System.Media;
using System.Threading;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmModeImportCustomer : Form
    {
        #region Member

        DataTable dtRead = new DataTable();
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmModeImportCustomer()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        private void frmModeImportCustomer_Load(object sender, EventArgs e)
        {
            button1.Focus();
            this.lblMessage.Hide();
        }
        private void frmModeImportCustomer_KeyUp(object sender, KeyEventArgs e)
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
            // OFFLINE
            frmImportCustomer frmImportCustomer = new frmImportCustomer();
            frmImportCustomer.Show();
            this.Close();
        }

        private void lnkYES_Click(object sender, EventArgs e)
        {
            try
            {
                UICursor.CursorWait();
                if (iConfig.WS.IsOnline().ToUpper().Trim() == "ONLINE")
                {
                    #region MODE ONLINE

                    dtRead = iConfig.WS.DoLoadCustomer();
                    if (dtRead.Rows.Count > 0 && dtRead != null)
                    {
                        #region DOWNLOAD CUSTOMER

                        if (!Executing.Instance.DeleteCustomerInSqlCeOnPDA())
                        {
                            this.lblMessage.Show();
                            this.lblMessage.Text = "Can't import customer.";
                            return;
                        }
                        else
                        {
                            #region Import Customer

                            List<CustomerEntity> _list = new List<CustomerEntity>();
                            for (int i = 0; i < this.dtRead.Rows.Count; i++)
                            {
                                _list.Add(new CustomerEntity
                                {
                                    cust_id = this.dtRead.Rows[i]["customer_code"].ToString().Trim(),
                                    cust_name = this.dtRead.Rows[i]["customer_name"].ToString().Trim()
                                });
                            }
                            bool flag = Executing.Instance.SaveImportCustomer(_list);
                            if (flag)
                            {
                                frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
                                frmMainMenu.Show();
                                this.Close();
                            }
                            else
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    this.lblMessage.Show();
                                    this.lblMessage.Text = "CAN'T IMPORT CUSTOMER FILE";
                                }
                            }

                            #endregion
                        }

                        #endregion
                    }

                    #endregion
                }
                else
                {
                    #region MODE OFFLINE

                    ClassMsg.DialogWarning("This mode offline. Can't upload data. Please connect WiFi");
                    return;

                    #endregion
                }
            }
            catch (System.Net.WebException WebEx)
            {
                // Messgae alert.
                if (WebEx.Status.ToString().Trim() == "ConnectFailure")
                {
                    ClassMsg.DialogWarning("This mode offline. Can't upload data. Please connect WiFi : " + WebEx.Message.ToString());
                }
                else
                {
                    if (((HttpWebResponse)WebEx.Response) != null)
                    {
                        string error_code = string.IsNullOrEmpty(((HttpWebResponse)WebEx.Response).StatusCode.ToString()) ? string.Empty : ((HttpWebResponse)WebEx.Response).StatusCode.ToString().Trim();
                        if (error_code == "NotFound")
                            ClassMsg.DialogWarning("Host server (404) NotFound : " + WebEx.Message.ToString());
                        else
                            ClassMsg.DialogWarning("This mode offline. Can't upload data. Please connect WiFi : " + WebEx.Message.ToString());
                    }
                    else
                    {
                        if (WebEx.Status.ToString().Trim() == "ReceiveFailure")
                        {
                            Executing.Instance.Insert_Log("ERR-504", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmModeImportCustomer", "lnkYES_Click");
                            if (MessageBox.Show("Please contact IT for setup IPAddress ?" + string.Empty + " ", "Question",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                frmModeConfigWebService fSetting = new frmModeConfigWebService();
                                fSetting.Show();
                                this.Close();
                            }
                        }
                        else
                        {
                            ClassMsg.DialogWarning("IP Config fail : " + WebEx.Message.ToString());
                            Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmModeImportCustomer", "lnkYES_Click");
                        }
                    }
                }

                // Write log.
                if (WebEx.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)WebEx.Response).StatusCode.ToString().Trim() == "NotFound")
                    {
                        Executing.Instance.Insert_Log("(404) " + ((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmModeImportCustomer", "lnkYES_Click");
                    }
                    else
                    {
                        if (((HttpWebResponse)WebEx.Response) != null)
                            Executing.Instance.Insert_Log(((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmModeImportCustomer", "lnkYES_Click");
                        else
                            Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmModeImportCustomer", "lnkYES_Click");
                    }
                }
                else if (WebEx.Status.ToString().Trim() == "ConnectFailure")
                {
                    Executing.Instance.Insert_Log("PDA Mode OFFLINE", WebEx.Message.ToString(), "frmModeImportCustomer", "lnkYES_Click");
                }
            }
            catch (Exception ex)
            {
                ClassMsg.DialogWarning("This mode offline. Can't upload data. Please connect WiFi : " + ex.Message.ToString());
                Executing.Instance.Insert_Log("CatchException", ex.Message.ToString(), "frmModeImportCustomer", "lnkYES_Click");
            }
            finally
            {
                UICursor.CursorStop();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 3000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}