using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SPACE.TJG.Barcode.UI.Execute;
using ServiceFunction.Cursor;
using MsgBox.ClassMsgBox;
using EntitiesTemp.GRProduction;
using System.Media;
using System.Threading;
using SPACE.TJG.Barcode.UI.Configs;
using ServiceFunction.Function;
using System.Net;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmGRFromProduction : Form
    {
        #region Member

        string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmGRFromProduction()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

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
                GRProductionEntity gRProductionEntity = new GRProductionEntity();
                gRProductionEntity.pre_order = this.txtPreOrder.Text.Trim();
                gRProductionEntity.batch = this.txtBatch.Text.Trim();
                gRProductionEntity.serial_number = this.txtSerialNumber.Text.Trim();
                gRProductionEntity.create_date = Executing.Instance.GetDateServer();

                if (Executing.Instance.SaveGRProduction(gRProductionEntity))
                    result = true;
                else
                    result = false;
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

        private void frmGRFromProduction_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.lblMessage.Hide();
                this._Action = Action.New;
                this.lblCountByPreOrder.Text = Executing.Instance.CountGRProductionALL();
                this.txtPreOrder.Focus();
                this.txtPreOrder.SelectAll();
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
        private void frmGRFromProduction_KeyUp(object sender, KeyEventArgs e)
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

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 3000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        private void lnkExit_Click(object sender, EventArgs e)
        {
            frmMenuGRProduction frmMenuGRProduction = new frmMenuGRProduction();
            frmMenuGRProduction.Show();
            this.Close();
        }
        private void lnkNEW_Click(object sender, EventArgs e)
        {
            if (this._Action == Action.New)
            {
                this.txtPreOrder.Text = string.Empty;
                this.txtPreOrder.Focus();
                this.txtPreOrder.SelectAll();
            }
            else if (this._Action == Action.Edit)
            {
                this.txtPreOrder.Text = string.Empty;
                this.txtPreOrder.Enabled = true;
                this.txtBatch.Text = string.Empty;
                this.txtBatch.Enabled = false;
                this.txtSerialNumber.Text = string.Empty;
                this.txtSerialNumber.Enabled = false;
                this.lblCountByPreOrder.Text = "0";
                this.txtPreOrder.Focus();
                this.txtPreOrder.SelectAll();
            }
        }

        private void txtPreOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtPreOrder.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    this._Action = Action.Edit;
                    this.txtPreOrder.Enabled = false;
                    this.txtBatch.Enabled = true;
                    this.txtBatch.Focus();
                    this.txtBatch.SelectAll();
                }
            }
        }
        private void txtBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtBatch.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else if (string.IsNullOrEmpty(this.txtPreOrder.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key pre order";
                    }
                }
                else
                {
                    this.txtBatch.Enabled = false;
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
                else if (string.IsNullOrEmpty(this.txtPreOrder.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key pre order";
                    }
                }
                else if (string.IsNullOrEmpty(this.txtBatch.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key batch";
                    }
                }
                else
                {
                    bool flag = Executing.Instance.checkGRProductionSerialDuplicate(this.txtSerialNumber.Text.Trim());
                    if (flag)
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
                    else if (DoSave())
                    {
                        this.lblMessage.Show();
                        this.lblMessage.Text = "SAVE DATA";
                        this.lblCountByPreOrder.Text = Executing.Instance.CountByPreOrderGRProduction(this.txtPreOrder.Text.Trim());
                        this.txtSerialNumber.Text = string.Empty;
                        this.txtSerialNumber.Focus();
                        this.txtSerialNumber.SelectAll();
                    }
                }
            }
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
                    //DataSet dsTable = new DataSet();
                    DataTable dtUpload = Executing.Instance.getDataGRProduction();
                    countAll = dtUpload.Rows.Count;
                    //dtUpload.TableName = "Distribution_Ret";
                    if (!dtUpload.IsNullOrNoRows())
                    {
                        if (MessageBox.Show("Do you want upload data to server yes or no" + string.Empty + " ", "Question",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            //dsTable.Tables.Add(dtUpload);
                            foreach (DataRow item in dtUpload.Rows)
                            {
                                if (iConfig.WS.DoInsGRProduction(string.IsNullOrEmpty(item["serial_number"].ToString()) ? string.Empty : item["serial_number"].ToString().Trim(),
                                                                 string.IsNullOrEmpty(item["pre_order"].ToString()) ? string.Empty : item["pre_order"].ToString().Trim(),
                                                                 string.IsNullOrEmpty(item["batch"].ToString()) ? string.Empty : item["batch"].ToString().Trim(),
                                                                 string.IsNullOrEmpty(item["create_date"].ToString()) ? string.Empty : item["create_date"].ToString().Trim()) == "0")
                                {
                                    insertRow++;
                                }
                            }

                            if (countAll == insertRow)
                            {
                                #region SUCCESS

                                // Success
                                if (Executing.Instance.DeleteGRProductionAfterUpload())
                                {
                                    // Delete success.
                                    this.lblMessage.Hide();
                                    this._Action = Action.New;

                                    this.txtPreOrder.Text = string.Empty;
                                    this.txtPreOrder.Enabled = true;
                                    this.txtBatch.Text = string.Empty;
                                    this.txtBatch.Enabled = false;
                                    this.txtSerialNumber.Text = string.Empty;
                                    this.txtSerialNumber.Enabled = false;
                                    this.lblCountByPreOrder.Text = "0";
                                    this.txtPreOrder.Focus();
                                    this.txtPreOrder.SelectAll(); ;
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
                        Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmGRFromProduction", "btnUploadData_Click");
                    }
                }

                // Write log.
                if (WebEx.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)WebEx.Response).StatusCode.ToString().Trim() == "NotFound")
                    {
                        Executing.Instance.Insert_Log("(404) " + ((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmGRFromProduction", "btnUploadData_Click");
                    }
                    else
                    {
                        if (((HttpWebResponse)WebEx.Response) != null)
                            Executing.Instance.Insert_Log(((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmGRFromProduction", "btnUploadData_Click");
                        else
                            Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmGRFromProduction", "btnUploadData_Click");
                    }
                }
                else if (WebEx.Status.ToString().Trim() == "ConnectFailure")
                {
                    Executing.Instance.Insert_Log("PDA Mode OFFLINE", WebEx.Message.ToString(), "frmGRFromProduction", "btnUploadData_Click");
                }
            }
            catch (Exception ex)
            {
                ClassMsg.DialogWarning("This mode offline. Can't upload data. Please connect WiFi : " + ex.Message.ToString());
                Executing.Instance.Insert_Log("CatchException", ex.Message.ToString(), "frmGRFromProduction", "btnUploadData_Click");
            }
            finally
            {
                UICursor.CursorStop();
            }
        }

        #endregion
    }
}