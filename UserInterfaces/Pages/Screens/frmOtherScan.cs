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
using ServiceFunction.Cursor;
using EntitiesTemp.OtherScan;
using ServiceFunction.Function;
using PrintCENET;

namespace UserInterfaces.Pages.Screens
{
    public partial class frmOtherScan : Form
    {
        #region Member

        private int mCurLang = 0;

        private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmOtherScan()
        {
            InitializeComponent();
        }

        #endregion

        #region Save

        private bool DoSave()
        {
            bool result;
            try
            {
                UICursor.CursorWait();
                OtherScanEntity enOtherScanEntity = new OtherScanEntity();
                enOtherScanEntity.scan_number = this.txtScanNumber.Text.Trim();
                enOtherScanEntity.item_code = this.txtScanNumber.Text.Substring(0, 5);
                enOtherScanEntity.notes = this.listNotes.Text.Trim();
                enOtherScanEntity.create_by = "USER_OTHERSCAN";
                enOtherScanEntity.create_date = Executing.Instance.GetDateServer();

                bool flag = Executing.Instance.DoSaveOtherScan(enOtherScanEntity);
                if (flag)
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
					"  Lot Number: ",
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
                    "  Recorder",
					Environment.NewLine,
					string.Empty,
					Environment.NewLine,
					string.Empty,
					Environment.NewLine,
					"  ....................",
                    //"  Thai Japan Gas Co., Ltd    Accepted By Customer",
                    //Environment.NewLine,
                    //string.Empty,
                    //Environment.NewLine,
                    //string.Empty,
                    //Environment.NewLine,
                    //"  .......................    ....................",
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

        #region Event

        private void frmOtherScan_Load(object sender, EventArgs e)
        {
            this.lblMessage.Hide();

            this.lblTotalSO.Text = "Lot Number : " + Executing.Instance.CountAllLotNumber();
            this.listNotes.Focus();
            this.listNotes.SelectAll();
        }
        private void frmOtherScan_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                    lklLogout_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Left:
                    lnkNEW_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Down:
                    lnkPrint_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void listNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.listNotes.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key lot number";
                    }
                }
                else
                {
                    this.txtScanNumber.Text = string.Empty;
                    this.listNotes.Enabled = false;
                    this.txtScanNumber.Enabled = true;

                    this.lblTotalSO.Text = "Lot Number : " + Executing.Instance.CountAllLotNumber();
                    this.txtScanNumber.Focus();
                    this.txtScanNumber.SelectAll();
                }
            }
        }
        private void txtScanNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.listNotes.Text))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key lot number";
                    }
                }
                else if (string.IsNullOrEmpty(this.txtScanNumber.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key cylinder S/N";
                    }
                }
                else
                {
                    try
                    {
                        if (this.txtScanNumber.Text.Length >= 5)
                        {
                            bool flag = Executing.Instance.checkScanNumberDuplicate(this.txtScanNumber.Text.Trim());
                            if (flag)
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    this.lblMessage.Show();
                                    this.lblMessage.Text = "Duplicate Data";
                                    this.txtScanNumber.Text = string.Empty;
                                    this.txtScanNumber.Focus();
                                    this.txtScanNumber.SelectAll();
                                }
                            }
                            else if (DoSave())
                            {
                                this.lblMessage.Show();
                                this.lblMessage.Text = "SAVE DATA";
                                this.lblCountByPreOrder.Text = Executing.Instance.CountByLotNumber(this.listNotes.Text.Trim());
                                this.lblTotalSO.Text = "Lot Number : " + Executing.Instance.CountAllLotNumber();
                                this.txtScanNumber.Text = string.Empty;
                                this.txtScanNumber.Focus();
                                this.txtScanNumber.SelectAll();
                            }
                        }
                        else
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                // Cylinder ไม่ถูกต้อง
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Cylinder S/N incorrect";
                            }
                        }

                        /*
                        bool flag = Executing.Instance.checkScanNumberDuplicate(this.txtScanNumber.Text.Trim());
                        if (flag)
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Duplicate Data";
                                this.txtScanNumber.Text = string.Empty;
                                this.txtScanNumber.Focus();
                                this.txtScanNumber.SelectAll();
                            }
                        }
                        else if (DoSave())
                        {
                            this.lblMessage.Show();
                            this.lblMessage.Text = "SAVE DATA";
                            this.txtScanNumber.Text = string.Empty;
                            this.txtScanNumber.Focus();
                            this.txtScanNumber.SelectAll();
                        }
                        */
                    }
                    catch (Exception ex)
                    {
                        Executing.Instance.Insert_Log("ERRSAVE_SC3", ex.Message.ToString(), "frmOtherScan", "txtScanNumber_KeyDown");
                    }
                }
            }
        }

        private void lnkNEW_Click(object sender, EventArgs e)
        {
            this.listNotes.Text = string.Empty;
            this.txtScanNumber.Text = string.Empty;
            this.txtScanNumber.Enabled = false;
            this.listNotes.Enabled = true;

            this.listNotes.Focus();
            this.listNotes.SelectAll();
        }
        private void lklLogout_Click(object sender, EventArgs e)
        {
            HomeScreen.frmHomeScreen fHome = new UserInterfaces.HomeScreen.frmHomeScreen("LIFETIME");
            fHome.Show();
            this.Close();
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.listNotes.Enabled)
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "No Data to Print";
                    this.listNotes.Text = string.Empty;
                    this.listNotes.Enabled = true;
                }
            }
            else if (!this.listNotes.Enabled)
            {
                DataTable invoiceDO = Executing.Instance.getLotBarcode(this.listNotes.Text.Trim());
                if (!invoiceDO.IsNullOrNoRows())
                {
                    var list = Enumerable.ToList(Enumerable.Select(DataTableExtensions.AsEnumerable(invoiceDO), (DataRow _list) => new
                    {
                        create_date = _list["create_date"],
                        serial_number = _list["scan_number"].ToString(),
                        do_no = _list["notes"].ToString()
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
                    this.Print(this.listNotes.Text.Trim(), text, Enumerable.Count(list).ToString());
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