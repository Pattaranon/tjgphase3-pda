using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using UserInterfaces.Forms;
using UserInterfaces.Configs;
using UserInterfaces.Execute;
using System.Net;
using System.IO;
using UserInterfaces.Activates;

namespace UserInterfaces.Main
{
    public partial class frmMainMenu : Form
    {
        #region Member

        string _isOnline = string.Empty;
        public string LicencesType { get; set; }

        #endregion

        #region Constructor

        public frmMainMenu(string _licencesType)
        {
            InitializeComponent();
            LicencesType = _licencesType.Trim();
        }

        #endregion

        #region Method

        private string RenderHtml(string urlAddress)
        {
            string data = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding("TIS-620"));

                    data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
            }
            catch (Exception e)
            {
                data = e.Message.ToString().Trim();
            }

            return data;
        }

        #endregion

        #region Event

        //Form
        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            try
            {
                this.button1.Focus();

                #region Comment Display connection

                /* Display connection
                iConfig.IPAddress = iConfig.ReadConfig();
                _isOnline = iConfig.WS.IsOnline().ToString().Trim();
                if (!string.IsNullOrEmpty(_isOnline))
                {
                    if (_isOnline.ToUpper().Trim() == "ONLINE")
                        this.lblSetting.Text = "ONLINE";
                    else if (_isOnline.ToUpper().Trim() == "OFFLINE")
                        this.lblSetting.Text = "OFFLINE";
                }
                */

                #endregion

                if (LicencesType.Trim() == "TRUE")
                {
                    this.lblActivateLicences.Visible = true;
                }
                else if (LicencesType.Trim() == "LIFETIME")
                {
                    this.lblActivateLicences.Visible = false;
                }

                this.lblVersion.Text = "Version : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Trim();
            }
            catch (System.Net.WebException WebEx)
            {
                #region WebException

                if (WebEx.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)WebEx.Response) != null)
                    {
                        if (((HttpWebResponse)WebEx.Response).StatusCode.ToString().Trim() == "NotFound")
                            Executing.Instance.Insert_Log("(404) " + ((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmMainMenu", "frmMainMenu_Load");
                        else
                            Executing.Instance.Insert_Log(((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmMainMenu", "frmMainMenu_Load");
                    }
                    else
                    {
                        Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmMainMenu", "frmMainMenu_Load");
                    }
                }
                else if (WebEx.Status.ToString().Trim() == "ConnectFailure")
                {
                    Executing.Instance.Insert_Log("PDA Mode OFFLINE", WebEx.Message.ToString(), "frmMainMenu", "frmMainMenu_Load");
                }
                
                this.lblSetting.Text = "OFFLINE";
                this.lblVersion.Text = "Version : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Trim();

                #endregion
            }
            catch (Exception ex)
            {
                #region Exception

                Executing.Instance.Insert_Log("CatchException", ex.Message.ToString(), "frmMainMenu", "frmMainMenu_Load");

                this.lblSetting.Text = "OFFLINE";
                this.lblVersion.Text = "Version : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Trim();

                #endregion
            }
        }
        private void frmMainMenu_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.D1:
                    llbScanBarcode_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D2:
                    //llbRecFG_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D3:
                    llbSendData_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D4:
                    llbDeleteData_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D5:
                    llbDeleteAllData_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.D6:
                    llbDownloadCustomer_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lblSetting_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Down:
                    lklLogout_Click(sender, e);
                    break;
                default:
                    break;
            }
        }
        //Timer
        private void timer_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dd/MM/yy").Trim();
            lblTimeRun.Text = DateTime.Now.ToString("HH:mm:ss").Trim();

            #region Comment Display connection

            /* Display connection
            _isOnline = iConfig.WS.IsOnline().ToString().Trim();
            if (!string.IsNullOrEmpty(_isOnline))
            {
                if (_isOnline.ToUpper().Trim() == "ONLINE")
                    this.lblSetting.Text = "ONLINE";
                else if (_isOnline.ToUpper().Trim() == "OFFLINE")
                    this.lblSetting.Text = "OFFLINE";
            }
            */

            #endregion

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }
        //Link Label
        private void llbScanBarcode_Click(object sender, EventArgs e)
        {
            Forms.frmMenuScanBarcode fMscanBarcode = new UserInterfaces.Forms.frmMenuScanBarcode();
            fMscanBarcode.Show();
        }
        private void llbViewData_Click(object sender, EventArgs e)
        {

        }
        private void llbSendData_Click(object sender, EventArgs e)
        {
            /*
            Forms.frmMenuSendData fSendData = new UserInterfaces.Forms.frmMenuSendData();
            fSendData.Show();
            */
        }
        private void llbDeleteData_Click(object sender, EventArgs e)
        {
            frmMenuDelete frmMenuDelete = new frmMenuDelete("DEL", "MENU_DEL", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            frmMenuDelete.Show();
        }
        private void llbDeleteAllData_Click(object sender, EventArgs e)
        {
            frmMenuDelete frmMenuDelete = new frmMenuDelete("DEL_ALL", "MENU_DEL_ALL", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            frmMenuDelete.Show();
        }
        private void llbDownloadCustomer_Click(object sender, EventArgs e)
        {
            Forms.frmMenuDownloadCustomer fDownloadCustomer = new UserInterfaces.Forms.frmMenuDownloadCustomer();
            fDownloadCustomer.Show();
        }
        private void lblSetting_Click(object sender, EventArgs e)
        {
            /* Check connection
            if (this.lblSetting.Text.ToUpper().Trim() == "OFFLINE")
            {
                Settings.frmConfigWebService fSetting = new UserInterfaces.Settings.frmConfigWebService();
                fSetting.Show();
            }
            */
        }
        private void lblActivateLicences_Click(object sender, EventArgs e)
        {
            using (frmActivateLicense fAvtivate = new frmActivateLicense())
            {
                fAvtivate.ShowDialog();
            }
        }
        private void lklLogout_Click(object sender, EventArgs e)
        {
            frmLogout frmLogout = new frmLogout();
            frmLogout.Show();
        }

        #endregion
    }
}