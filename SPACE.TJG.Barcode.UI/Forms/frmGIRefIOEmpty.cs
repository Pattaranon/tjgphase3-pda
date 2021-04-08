using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ServiceFunction.Cursor;
using SPACE.TJG.Barcode.UI.Execute;
using MsgBox.ClassMsgBox;
using System.Media;
using System.Threading;
using EntitiesTemp.GIRefIOEmpty;
using SPACE.TJG.Barcode.UI.Configs;
using ServiceFunction.Function;
using System.Net;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmGIRefIOEmpty : Form
    {
        #region Member

        private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmGIRefIOEmpty()
        {
            InitializeComponent();
        }

        #endregion

        #region Enum property

        public Action _Action { get; set; }

        #endregion

        #region Enum

        public enum Action
        {
            New,
            Edit
        }

        #endregion

        #region Save

        private bool DoSave()
        {
            bool result;
            try
            {
                UICursor.CursorWait();
                GIRefIOEmptyEntity gIRefIOEmptyEntity = new GIRefIOEmptyEntity();
                gIRefIOEmptyEntity.do_no = this.txtDO.Text.Trim();
                gIRefIOEmptyEntity.serial_number = this.txtSerialNumber.Text.Trim();
                gIRefIOEmptyEntity.create_date = Executing.Instance.GetDateServer();
                bool flag = Executing.Instance.SaveGIRefIOEmpty(gIRefIOEmptyEntity);
                if (flag)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                UICursor.CursorStop();
            }
            return result;
        }

        #endregion

        #region Event

        private void frmGIRefIOEmpty_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.lblMessage.Hide();
                this._Action = Action.New;
                //Count
                this.lblCountByDO.Text = Executing.Instance.CountGIRefIOALL();

                this.txtDO.Focus();
                this.txtDO.SelectAll();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ClassMsg.DialogErrorTryCatch("System Error save : ", ex.InnerException);
                else
                    ClassMsg.DialogErrorTryCatch("System Error save : ", ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void frmGIRefIOEmpty_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Left:
                    lnkNEW_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lnkExit_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        //TextBox
        private void txtDO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtDO.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    this._Action = frmGIRefIOEmpty.Action.Edit;
                    this.txtDO.Enabled = false;
                    this.txtSerialNumber.Enabled = true;
                    this.txtSerialNumber.Focus();
                    this.txtSerialNumber.SelectAll();
                }
            }
        }
        private void txtSerialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtSerialNumber.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else if (string.IsNullOrEmpty(this.txtDO.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key DO";

                        this.txtSerialNumber.Text = string.Empty;
                        this.txtDO.Focus();
                        this.txtDO.SelectAll();
                    }
                }
                else
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        //Save Data
                        bool _chk_serial = (bool)Executing.Instance.checkGIRefIOEmptySerialDuplicate(this.txtSerialNumber.Text.Trim());
                        if (_chk_serial)
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Duplicate Data";
                                this.txtSerialNumber.Text = string.Empty;
                                this.txtSerialNumber.Focus();
                                this.txtSerialNumber.SelectAll();
                            }
                        }
                        else
                        {
                            //Save
                            if (DoSave())
                            {
                                this.lblMessage.Show();
                                this.lblMessage.Text = "SAVE DATA";

                                this.lblCountByDO.Text = Executing.Instance.CountGIEmptyByDO(this.txtDO.Text.Trim());
                                this.txtSerialNumber.Text = string.Empty;
                                this.txtSerialNumber.Focus();
                                this.txtSerialNumber.SelectAll();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                            ClassMsg.DialogErrorTryCatch("System Error save : ", ex.InnerException);
                        else
                            ClassMsg.DialogErrorTryCatch("System Error save : ", ex);
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

        private void lnkNEW_Click(object sender, EventArgs e)
        {
            if (this._Action == Action.New)
            {
                this.txtDO.Text = string.Empty;
                this.txtDO.Focus();
                this.txtDO.SelectAll();
            }
            else if (this._Action == Action.Edit)
            {
                this.txtDO.Text = string.Empty;
                this.txtDO.Enabled = true;
                this.txtSerialNumber.Enabled = false;
                this.txtSerialNumber.Text = string.Empty;
                this.lblCountByDO.Text = "0";
                this.txtDO.Focus();
                this.txtDO.SelectAll();
            }
        }
        private void lnkExit_Click(object sender, EventArgs e)
        {
            frmMenuGIProduction frmMenuGIProduction = new frmMenuGIProduction();
            frmMenuGIProduction.Show();
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 3000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        private void btnUploadData_Click(object sender, EventArgs e)
        {
            try
            {
                UICursor.CursorWait();
                if (iConfig.WS.IsOnline().ToUpper().Trim() == "ONLINE")
                {
                    #region MODE ONLINE

                    int countAll = 0;
                    int insertRow = 0;
                    DataTable dtUpload = Executing.Instance.getDataGIRefIOEmpty();
                    countAll = dtUpload.Rows.Count;
                    if (!dtUpload.IsNullOrNoRows())
                    {
                        if (MessageBox.Show("Do you want upload data to server yes or no" + string.Empty + " ", "Question",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            foreach (DataRow item in dtUpload.Rows)
                            {
                                if (iConfig.WS.DoInsGIRefIOEmpty(string.IsNullOrEmpty(item["serial_number"].ToString()) ? string.Empty : item["serial_number"].ToString().Trim(),
                                                                 string.IsNullOrEmpty(item["do_no"].ToString()) ? string.Empty : item["do_no"].ToString().Trim(),
                                                                 string.IsNullOrEmpty(item["create_date"].ToString()) ? string.Empty : item["create_date"].ToString().Trim()) == "0")
                                {
                                    insertRow++;
                                }
                            }

                            if (countAll == insertRow)
                            {
                                #region SUCCESS

                                // Success
                                if (Executing.Instance.DeleteGIRefIOEmptyAfterUpload())
                                {
                                    // Delete success.
                                    this.lblMessage.Hide();
                                    this._Action = Action.New;
                                    //Count
                                    this.lblCountByDO.Text = Executing.Instance.CountGIRefIOALL();
                                    this.txtDO.Text = string.Empty;
                                    this.txtDO.Enabled = true;
                                    this.txtSerialNumber.Enabled = false;
                                    this.txtSerialNumber.Text = string.Empty;
                                    this.lblCountByDO.Text = "0";
                                    this.txtDO.Focus();
                                    this.txtDO.SelectAll();
                                }

                                #endregion
                            }
                        }
                    }
                    else
                    {
                        ClassMsg.DialogWarning("Not found data for upload.");
                        return;
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
                        ClassMsg.DialogWarning("IP Config fail : " + WebEx.Message.ToString());
                        Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmGIRefIOEmpty", "btnUploadData_Click");
                    }
                }

                // Write log.
                if (WebEx.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)WebEx.Response).StatusCode.ToString().Trim() == "NotFound")
                    {
                        Executing.Instance.Insert_Log("(404) " + ((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmGIRefIOEmpty", "btnUploadData_Click");
                    }
                    else
                    {
                        if (((HttpWebResponse)WebEx.Response) != null)
                            Executing.Instance.Insert_Log(((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmGIRefIOEmpty", "btnUploadData_Click");
                        else
                            Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmGIRefIOEmpty", "btnUploadData_Click");
                    }
                }
                else if (WebEx.Status.ToString().Trim() == "ConnectFailure")
                {
                    Executing.Instance.Insert_Log("PDA Mode OFFLINE", WebEx.Message.ToString(), "frmGIRefIOEmpty", "btnUploadData_Click");
                }
            }
            catch (Exception ex)
            {
                ClassMsg.DialogWarning("This mode offline. Can't upload data. Please connect WiFi : " + ex.Message.ToString());
                Executing.Instance.Insert_Log("CatchException", ex.Message.ToString(), "frmGIRefIOEmpty", "btnUploadData_Click");
            }
            finally
            {
                UICursor.CursorStop();
            }
        }

        #endregion
    }
}