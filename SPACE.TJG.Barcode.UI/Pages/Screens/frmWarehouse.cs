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
using SPACE.TJG.Barcode.UI.Execute;
using ServiceFunction.Cursor;
using EntitiesTemp.Warehouse;
using ServiceFunction.Function;
using PrintCENET;

namespace SPACE.TJG.Barcode.UI.Pages.Screens
{
    public partial class frmWarehouse : Form
    {
        #region Member

        private int mCurLang = 0;

        private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmWarehouse()
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
                WarehouseEntity enWarehouseEntity = new WarehouseEntity();
                enWarehouseEntity.sell_order_no = this.txtSellOrderNo.Text.Trim();
                enWarehouseEntity.cust_id = this.txtCustomer.Text.Trim();
                enWarehouseEntity.item_code = this.txtSerialNumber.Text.Substring(0, 5);
                enWarehouseEntity.cylinder_sn = this.txtSerialNumber.Text.Trim();
                enWarehouseEntity.create_by = "USER_WAREHOUSE";
                enWarehouseEntity.create_date = Executing.Instance.GetDateServer();
                bool flag = Executing.Instance.DoSaveWarehouse(enWarehouseEntity);
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
					"  Sale Order No: ",
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

        private void frmWarehouse_Load(object sender, EventArgs e)
        {
            this.lblMessage.Hide();

            this.lblTotalSO.Text = "SO : " + Executing.Instance.CountAllSellOrderNo();
            this.txtSellOrderNo.Focus();
            this.txtSellOrderNo.SelectAll();
        }
        private void frmWarehouse_KeyUp(object sender, KeyEventArgs e)
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
                    lklLogout_Click(sender, e);
                    break;
                case System.Windows.Forms.Keys.Down:
                    lnkPrint_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void txtSellOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtSellOrderNo.Enabled = false;
                this.txtCustomer.Enabled = true;
                this.lblTotalSO.Text = "SO : " + Executing.Instance.CountAllSellOrderNo();
                this.txtCustomer.Focus();
                this.txtCustomer.SelectAll();
            }
        }
        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtCustomer.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    #region Customer profile

                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        string _error_code = string.Empty;
                        string _customer_name = string.Empty;
                        string _sub_cust = this.txtCustomer.Text.Substring(0, 3);
                        //Check Cust Code

                        //if (Executing.Instance.checkCustCode(this.txtCustomer.Text.Substring(0, 3), this.txtCustomer.Text.Trim(), ref _error_code, ref _customer_name))
                        if (Executing.Instance.checkCustomerCode(this.txtCustomer.Text.Trim(), ref _error_code, ref _customer_name))
                        {
                            //Has
                            this.txtCustomer.Enabled = false;
                            this.lblCustName.Text = _customer_name.Trim();
                            this.txtSerialNumber.Enabled = true;
                            this.txtSerialNumber.Focus();
                            this.txtSerialNumber.SelectAll();
                        }
                        /*
                        else if (_error_code.Trim() == "001")
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.txtCustomer.Text = string.Empty;
                                this.txtCustomer.Enabled = true;
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Cust :101XXXX";
                                this.txtCustomer.Focus();
                                this.txtCustomer.SelectAll();
                            }
                        }
                        */
                        else if (_error_code.Trim() == "002")
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.txtCustomer.Text = string.Empty;
                                this.txtCustomer.Enabled = true;
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Customer not found";
                                this.txtCustomer.Focus();
                                this.txtCustomer.SelectAll();
                            }
                        }
                        else if (_error_code.Trim() == "003")
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.txtCustomer.Text = string.Empty;
                                this.txtCustomer.Enabled = true;
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Customer empty";
                                this.txtCustomer.Focus();
                                this.txtCustomer.SelectAll();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Executing.Instance.Insert_Log("ERRCUST_SC1", "Enter Save other scan : " + ex.Message.ToString(), "frmWarehouse", "txtCustomer_KeyDown");
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }

                    #endregion
                }

                /*
                else if (this.txtCustomer.Text.Length != 7)
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else if (this.txtCustomer.Text.Length == 7)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        string _error_code = string.Empty;
                        string _customer_name = string.Empty;
                        string _sub_cust = this.txtCustomer.Text.Substring(0, 3);
                        //Check Cust Code

                        if (Executing.Instance.checkCustCode(this.txtCustomer.Text.Substring(0, 3), this.txtCustomer.Text.Trim(), ref _error_code, ref _customer_name))
                        {
                            //Has
                            this.txtCustomer.Enabled = false;
                            this.lblCustName.Text = _customer_name.Trim();
                            this.txtSerialNumber.Enabled = true;
                            this.txtSerialNumber.Focus();
                            this.txtSerialNumber.SelectAll();
                        }
                        else if (_error_code.Trim() == "001")
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.txtCustomer.Text = string.Empty;
                                this.txtCustomer.Enabled = true;
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Cust :101XXXX";
                                this.txtCustomer.Focus();
                                this.txtCustomer.SelectAll();
                            }
                        }
                        else if (_error_code.Trim() == "002")
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.txtCustomer.Text = string.Empty;
                                this.txtCustomer.Enabled = true;
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Customer not found";
                                this.txtCustomer.Focus();
                                this.txtCustomer.SelectAll();
                            }
                        }
                        else if (_error_code.Trim() == "003")
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.txtCustomer.Text = string.Empty;
                                this.txtCustomer.Enabled = true;
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Customer empty";
                                this.txtCustomer.Focus();
                                this.txtCustomer.SelectAll();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Executing.Instance.Insert_Log("ERRCUST_SC1", "Enter Save other scan : " + ex.Message.ToString(), "frmWarehouse", "txtCustomer_KeyDown");
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
                */
            }
        }
        private void txtSerialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtSerialNumber.Text.Length >= 5)
                {
                    if (string.IsNullOrEmpty(this.txtSerialNumber.Text.Trim()))
                    {
                        using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                        {
                            soundPlayer.Play();
                            Thread.Sleep(1000);
                        }
                    }
                    else if (string.IsNullOrEmpty(this.txtSellOrderNo.Text.Trim()))
                    {
                        using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                        {
                            soundPlayer.Play();
                            Thread.Sleep(1000);

                            this.lblMessage.Show();
                            this.lblMessage.Text = "Please key sell order no";
                        }
                    }
                    else if (string.IsNullOrEmpty(this.txtCustomer.Text.Trim()))
                    {
                        using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                        {
                            soundPlayer.Play();
                            Thread.Sleep(1000);

                            this.lblMessage.Show();
                            this.lblMessage.Text = "Please key customer code";
                        }
                    }
                    else
                    {
                        try
                        {
                            bool flag = Executing.Instance.checkCylinderDuplicate(this.txtSerialNumber.Text.Trim());
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
                                this.lblCountByPreOrder.Text = Executing.Instance.CountBySellOrderNo(this.txtSellOrderNo.Text.Trim());
                                this.lblTotalSO.Text = "SO : " + Executing.Instance.CountAllSellOrderNo();
                                this.txtSerialNumber.Text = string.Empty;
                                this.txtSerialNumber.Focus();
                                this.txtSerialNumber.SelectAll();
                            }
                        }
                        catch (Exception ex)
                        {
                            Executing.Instance.Insert_Log("ERRSAVE_SC1", ex.Message.ToString(), "frmWarehouse", "txtSerialNumber_KeyDown");
                        }
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
            }
        }

        private void lnkNEW_Click(object sender, EventArgs e)
        {
            this.txtSellOrderNo.Text = string.Empty;
            this.txtSellOrderNo.Enabled = true;

            this.txtCustomer.Text = string.Empty;
            this.txtCustomer.Enabled = false;
            this.lblCustName.Text = string.Empty;

            this.txtSerialNumber.Text = string.Empty;
            this.txtSerialNumber.Enabled = false;

            this.lblCountByPreOrder.Text = "0";
            this.lblTotalSO.Text = "SO : " + Executing.Instance.CountAllSellOrderNo();

            this.txtSellOrderNo.Focus();
            this.txtSellOrderNo.SelectAll();
        }
        private void lnkDelete_Click(object sender, EventArgs e)
        {
            if (Executing.Instance.CountWarehouseALL() == "0")
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "No Data to Del";
                }
            }
            else
            {
                frmSelectWarehouseDel frmSWarehouseDel = new frmSelectWarehouseDel();
                frmSWarehouseDel.ShowDialog();
                //this.Close();
            }
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.txtSellOrderNo.Enabled)
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "No Data to Print";
                    this.txtSellOrderNo.Text = string.Empty;
                    this.txtSellOrderNo.Enabled = true;
                }
            }
            else if (!this.txtSellOrderNo.Enabled)
            {
                if (string.IsNullOrEmpty(this.txtSellOrderNo.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    DataTable invoiceDO = Executing.Instance.getSaleOrderNo(this.txtSellOrderNo.Text.Trim());
                    if (!invoiceDO.IsNullOrNoRows())
                    {
                        var list = Enumerable.ToList(Enumerable.Select(DataTableExtensions.AsEnumerable(invoiceDO), (DataRow _list) => new
                        {
                            create_date = _list["create_date"],
                            serial_number = _list["cylinder_sn"].ToString(),
                            do_no = _list["sell_order_no"].ToString()
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
                        this.Print(this.txtSellOrderNo.Text.Trim(), text, Enumerable.Count(list).ToString());
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
        private void lklLogout_Click(object sender, EventArgs e)
        {
            HomeScreen.frmHomeScreen fMain = new SPACE.TJG.Barcode.UI.HomeScreen.frmHomeScreen("LIFETIME");
            fMain.Show();
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 6000;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}