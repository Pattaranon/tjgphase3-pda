using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SPACE.TJG.Barcode.UI.Execute;
using MsgBox.ClassMsgBox;
using ServiceFunction.Cursor;
using System.Media;
using System.Threading;
using PrintCENET;
using EntitiesTemp.GIDelivery;
using SPACE.TJG.Barcode.UI.Configs;
using ServiceFunction.Function;
using System.Net;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmGIRefDelivery : Form
    {
        #region Member

        public bool status_del { get; set; }
        private int mCurLang = 0;

        private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmGIRefDelivery()
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

        #region Print

        private void Print(string invoice_no, string serial_number, string count_serial)
        {
            PrintASCII printASCII = null;
            try
            {
                printASCII = new PrintASCII();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            printASCII.Init("75F3-AF9B-C429-1141");
            printASCII.Language = this.mCurLang;
            if (printASCII.Connect(ASCII_PORT.ASCII_COM4, null))
            {
                printASCII.SendString(string.Concat(new string[]
				{
					string.Empty,
					Environment.NewLine,
					"  DATE: ",
					DateTime.Now.ToString("dd/MM/yy"),
					Environment.NewLine,
					"  INV: ",
					invoice_no,
					Environment.NewLine,
					"  S/N:",
					Environment.NewLine,
					serial_number,
					Environment.NewLine,
					"  .................................................",
					Environment.NewLine,
					"  Total: ",
					count_serial,
					Environment.NewLine,
					"  ...............................................",
					Environment.NewLine,
					"  Thai Japan Gas Co., Ltd    Accepted By Customer",
					Environment.NewLine,
					string.Empty,
					Environment.NewLine,
					string.Empty,
					Environment.NewLine,
					"  .......................    ....................",
					Environment.NewLine,
					string.Empty,
					Environment.NewLine,
					string.Empty,
					Environment.NewLine,
					string.Empty,
					Environment.NewLine
				}));
                printASCII.Disconnect();
            }
            printASCII.UnInit();
        }

        #endregion

        #region Save

        private bool DoSave()
        {
            bool result;
            try
            {
                UICursor.CursorWait();
                GIDeliveryEntity gIDeliveryEntity = new GIDeliveryEntity();
                gIDeliveryEntity.do_no = this.txtDO.Text.Trim();
                gIDeliveryEntity.serial_number = this.txtSerialNumber.Text.Trim();
                gIDeliveryEntity.create_date = Executing.Instance.GetDateServer();
                if (Executing.Instance.SaveGIDelivery(gIDeliveryEntity))
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

        //Form
        private void frmGIRefDelivery_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (this.status_del)
                {
                    this.lblMessage.Show();
                    this.lblMessage.Text = "Delete 1 Record";
                    this.lblCountByDO.Text = Executing.Instance.CountGIALL();
                }
                else
                {
                    this.lblMessage.Hide();
                    this._Action = Action.New;
                    this.lblCountByDO.Text = Executing.Instance.CountGIALL();
                    this.txtDO.Focus();
                    this.txtDO.SelectAll();
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
        private void frmGIRefDelivery_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Left:
                    lnkNEW_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Right:
                    lnkDelete_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Up:
                    lnkExit_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Down:
                    lnkPrint_Click(sender, e);
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
                    this._Action = Action.Edit;
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
                        // check match delivery and serialnumber
                        if (!Executing.Instance.IsMatchDeliveryNoAndSerialNumber(this.txtDO.Text.Trim(), this.txtSerialNumber.Text.Trim()))
                        {
                            // Not Match data
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "S/N Not Match Data";
                                this.txtSerialNumber.Text = string.Empty;
                                this.txtSerialNumber.Focus();
                                this.txtSerialNumber.SelectAll();
                            }
                        }
                        else
                        {
                            // Match data -> Save Data
                            if (Executing.Instance.checkGIDeliverySerialDuplicate(this.txtSerialNumber.Text.Trim()))
                            {
                                //If has in system.
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

                                    //Count By DO.
                                    this.lblCountByDO.Text = Executing.Instance.CountGIByDO(this.txtDO.Text.Trim());
                                    this.txtSerialNumber.Text = string.Empty;
                                    this.txtSerialNumber.Focus();
                                    this.txtSerialNumber.SelectAll();
                                }
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
        //Link Label
        private void lnkExit_Click(object sender, EventArgs e)
        {
            frmMenuDistributionSale frmMDistributionSale = new frmMenuDistributionSale();
            frmMDistributionSale.Show();
            this.Close();
        }
        private void lnkNEW_Click(object sender, EventArgs e)
        {
            if (this._Action == Action.New)
            {
                this.txtDO.Text = string.Empty;

                this.txtDO.Focus();
                this.txtDO.SelectAll();
                return;
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
        private void lnkDelete_Click(object sender, EventArgs e)
        {
            //Check : ว่ามีอยู่ใน Table หรือไม่ ?
            if (Executing.Instance.CountGIALL() == "0")
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);

                    this.lblMessage.Show();
                    this.lblMessage.Text = "NOT FOUND DATA";
                }
            }
            else
            {
                frmSelectGIDeliveryDel frmSelectGIDeliveryDel = new frmSelectGIDeliveryDel();
                frmSelectGIDeliveryDel.Show();
                this.Close();
            }
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.txtDO.Enabled)
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "No Data to Print";
                    this.txtDO.Text = string.Empty;
                    this.txtDO.Enabled = true;
                }
            }
            else if (!this.txtDO.Enabled)
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
                    DataTable invoiceDO = Executing.Instance.getInvoiceDO(this.txtDO.Text.Trim());
                    if (!invoiceDO.IsNullOrNoRows())
                    {
                        var list = Enumerable.ToList(Enumerable.Select(DataTableExtensions.AsEnumerable(invoiceDO), (DataRow _list) => new
                        {
                            create_date = _list["create_date"],
                            serial_number = _list["serial_number"].ToString(),
                            do_no = _list["do_no"].ToString()
                        }));
                        string text = string.Empty;
                        for (int i = 1; i <= Enumerable.Count(list); i++)
                        {
                            if (i % 2 == 1)
                            {
                                text = text + "    " + list[i - 1].serial_number;
                                for (int j = 0; j < 27 - list[i - 1].serial_number.Length; j++)
                                {
                                    text += " ";
                                }
                            }
                            else
                            {
                                text = text + list[i - 1].serial_number + Environment.NewLine;
                            }
                        }
                        this.Print(this.txtDO.Text.Trim(), text, Enumerable.Count(list).ToString());
                    }
                    else
                    {
                        using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                        {
                            soundPlayer.Play();
                            Thread.Sleep(1000);
                            this.lblMessage.Show();
                            this.lblMessage.Text = "No Data to Print";
                        }
                    }
                }
            }
        }
        //Timer
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
                    DataTable dtUpload = Executing.Instance.getDataGIDelivery();
                    countAll = dtUpload.Rows.Count;
                    if (!dtUpload.IsNullOrNoRows())
                    {
                        if (MessageBox.Show("Do you want upload data to server yes or no" + string.Empty + " ", "Question",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            foreach (DataRow item in dtUpload.Rows)
                            {
                                if (iConfig.WS.DoInsUpdGIDelivery(string.IsNullOrEmpty(item["serial_number"].ToString()) ? string.Empty : item["serial_number"].ToString().Trim(),
                                                                  string.IsNullOrEmpty(item["do_no"].ToString()) ? string.Empty : item["do_no"].ToString().Trim(),
                                                                  string.IsNullOrEmpty(item["create_date"].ToString()) ? string.Empty : item["create_date"].ToString().Trim()) == "0")
                                {
                                    insertRow++;
                                }
                            }

                            if (countAll == insertRow)
                            {
                                #region SUCCESS

                                if (Executing.Instance.DeleteGIDeliveryAfterUpload())
                                {
                                    // Delete success.
                                    this.lblMessage.Hide();
                                    this._Action = Action.New;

                                    this.txtDO.Text = string.Empty;
                                    this.txtDO.Enabled = true;
                                    this.txtSerialNumber.Enabled = true;
                                    this.txtSerialNumber.Text = string.Empty;

                                    this.lblCountByDO.Text = "0 ";
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
                        Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmGORefDelivery", "btnUploadData_Click");
                    }
                }

                // Write log.
                if (WebEx.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)WebEx.Response).StatusCode.ToString().Trim() == "NotFound")
                    {
                        Executing.Instance.Insert_Log("(404) " + ((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmGORefDelivery", "btnUploadData_Click");
                    }
                    else
                    {
                        if (((HttpWebResponse)WebEx.Response) != null)
                            Executing.Instance.Insert_Log(((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmGORefDelivery", "btnUploadData_Click");
                        else
                            Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmGORefDelivery", "btnUploadData_Click");
                    }
                }
                else if (WebEx.Status.ToString().Trim() == "ConnectFailure")
                {
                    Executing.Instance.Insert_Log("PDA Mode OFFLINE", WebEx.Message.ToString(), "frmGORefDelivery", "btnUploadData_Click");
                }
            }
            catch (Exception ex)
            {
                ClassMsg.DialogWarning("This mode offline. Can't upload data. Please connect WiFi : " + ex.Message.ToString());
                Executing.Instance.Insert_Log("CatchException", ex.Message.ToString(), "frmGORefDelivery", "btnUploadData_Click");
            }
            finally
            {
                UICursor.CursorStop();
            }
        }

        #endregion
    }
}