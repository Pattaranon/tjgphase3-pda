using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MsgBox.ClassMsgBox;
using SPACE.TJG.Barcode.UI.Main;
using SPACE.TJG.Barcode.UI.Execute;
using System.IO;
using EntitiesTemp.Customer;
using System.Media;
using System.Threading;
using ServiceFunction.Cursor;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmImportCustomer : Form
    {
        #region Member

        DataTable dtRead = new DataTable();
        public delegate void _progess(int _value);

        //private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmImportCustomer()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        public void _setProgess(int _value)
        {
            if (this.pgbStartImport.InvokeRequired)
            {
                frmImportCustomer._progess progess = new frmImportCustomer._progess(this._setProgess);
                this.pgbStartImport.Invoke(progess, new object[]
				{
					_value
				});
            }
            else
            {
                this.pgbStartImport.Value = _value;
            }
        }

        #endregion

        #region Event

        private void frmImportCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.dtRead.Columns.Add("cust_id");
                this.dtRead.Columns.Add("cust_name");

                this.txtFileName.Enabled = false;
                this.lblMessage.Hide();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ClassMsg.DialogWarning(ex.InnerException.Message);
                }
                else
                {
                    ClassMsg.DialogWarning(ex.Message);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void frmImportCustomer_KeyUp(object sender, KeyEventArgs e)
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

        private void lklLogout_Click(object sender, EventArgs e)
        {
            HomeScreen.frmHomeScreen frmMainMenu = new HomeScreen.frmHomeScreen(string.Empty);
            frmMainMenu.Show();
            this.Close();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "All files (*.*)|*.*|txt files (*.txt)|*.txt|CSV files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 2;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.txtFileName.Text = openFileDialog.FileName.ToString().Trim();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        ClassMsg.DialogWarning(ex.InnerException.Message);
                    }
                    else
                    {
                        ClassMsg.DialogWarning(ex.Message);
                    }
                }
            }
        }
        private void btnStartImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtFileName.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please select file customer";
                    }
                }
                else
                {
                    UICursor.CursorWait();
                    if (Executing.Instance.getImportCustomer())
                    {
                        #region Have Customer

                        // Check column in datatable.
                        if (!this.dtRead.Columns.Contains("cust_id"))
                            this.dtRead.Columns.Add("cust_id");
                        else if (!this.dtRead.Columns.Contains("cust_name"))
                            this.dtRead.Columns.Add("cust_name");

                        using (StreamReader streamReader = new StreamReader(this.txtFileName.Text.Trim(), Encoding.GetEncoding("windows-874")))
                        {
                            string text = streamReader.ReadToEnd().Replace("\n", string.Empty);
                            string[] array = text.Split(new char[]
					                                {
						                                Convert.ToChar("\r")
					                                });

                            for (int i = 0; i < Enumerable.Count<string>(array); i++)
                            {
                                if (i >= 0)
                                {
                                    if (!string.IsNullOrEmpty(array[i]))
                                    {
                                        string[] array2 = array[i].Split(new char[]
								{
									Convert.ToChar('\t')
								});

                                        if (array2.Length == 3)
                                        {
                                            this.lblMessage.Show();
                                            this.lblMessage.Text = "Select file not type for customer.";
                                            return;
                                        }

                                        this.dtRead.Rows.Add(new object[0]);
                                        int num = this.dtRead.Rows.Count - 1;

                                        this.dtRead.Rows[num]["cust_id"] = array2[0];
                                        this.dtRead.Rows[num]["cust_name"] = array2[1];
                                    }
                                }
                            }
                            streamReader.Dispose();
                        }
                        this.btnOpenFile.Enabled = false;
                        this.btnStartImport.Enabled = false;
                        if (this.dtRead.Rows.Count > 0)
                        {
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
                                int num2 = 1;
                                for (int i = 0; i < this.dtRead.Rows.Count; i++)
                                {
                                    _list.Add(new CustomerEntity
                                    {
                                        cust_id = this.dtRead.Rows[i]["cust_id"].ToString().Trim(),
                                        cust_name = this.dtRead.Rows[i]["cust_name"].ToString().Trim()
                                    });

                                    int count = this.dtRead.Rows.Count;
                                    float num3 = (float)(num2 * 100 / count);
                                    if (num3 < 0f)
                                    {
                                        num3 = 0f;
                                    }
                                    if (num3 > 100f)
                                    {
                                        num3 = 100f;
                                    }
                                    this._setProgess(Convert.ToInt32(num3));
                                    num2++;
                                }
                                bool flag = Executing.Instance.SaveImportCustomer(_list);
                                if (flag)
                                {
                                    this.dtRead = new DataTable();
                                    this.dtRead.Columns.Add("cust_id");
                                    this.dtRead.Columns.Add("cust_name");
                                    HomeScreen.frmHomeScreen frmMainMenu = new HomeScreen.frmHomeScreen("LIFETIME");
                                    frmMainMenu.Show();
                                    this.Close();
                                    /*
                                    frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
                                    frmMainMenu.Show();
                                    this.Close();
                                    */
                                }
                                else
                                {
                                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                                    {
                                        soundPlayer.Play();
                                        Thread.Sleep(1000);
                                        this.lblMessage.Show();
                                        this.lblMessage.Text = "CAN'T IMPORT CUSTOMER FILE";
                                        this.btnOpenFile.Enabled = true;
                                        this.btnStartImport.Enabled = true;
                                    }
                                }

                                #endregion
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region Not have customer

                        // Check column in datatable.
                        if (!this.dtRead.Columns.Contains("cust_id"))
                            this.dtRead.Columns.Add("cust_id");
                        else if (!this.dtRead.Columns.Contains("cust_name"))
                            this.dtRead.Columns.Add("cust_name");

                        using (StreamReader streamReader = new StreamReader(this.txtFileName.Text.Trim(), Encoding.GetEncoding("windows-874")))
                        {
                            string text = streamReader.ReadToEnd().Replace("\n", string.Empty);
                            string[] array = text.Split(new char[]
					                                {
						                                Convert.ToChar("\r")
					                                });

                            for (int i = 0; i < Enumerable.Count<string>(array); i++)
                            {
                                if (i >= 0)
                                {
                                    if (!string.IsNullOrEmpty(array[i]))
                                    {
                                        string[] array2 = array[i].Split(new char[]
								{
									Convert.ToChar('\t')
								});

                                        if (array2.Length == 3)
                                        {
                                            this.lblMessage.Show();
                                            this.lblMessage.Text = "Select file not type for customer.";
                                            return;
                                        }

                                        this.dtRead.Rows.Add(new object[0]);
                                        int num = this.dtRead.Rows.Count - 1;

                                        this.dtRead.Rows[num]["cust_id"] = array2[0];
                                        this.dtRead.Rows[num]["cust_name"] = array2[1];
                                    }
                                }
                            }
                            streamReader.Dispose();
                        }
                        this.btnOpenFile.Enabled = false;
                        this.btnStartImport.Enabled = false;
                        if (this.dtRead.Rows.Count > 0)
                        {
                            List<CustomerEntity> _list = new List<CustomerEntity>();
                            int num2 = 1;
                            for (int i = 0; i < this.dtRead.Rows.Count; i++)
                            {
                                _list.Add(new CustomerEntity
                                {
                                    cust_id = this.dtRead.Rows[i]["cust_id"].ToString().Trim(),
                                    cust_name = this.dtRead.Rows[i]["cust_name"].ToString().Trim()
                                });

                                int count = this.dtRead.Rows.Count;
                                float num3 = (float)(num2 * 100 / count);
                                if (num3 < 0f)
                                {
                                    num3 = 0f;
                                }
                                if (num3 > 100f)
                                {
                                    num3 = 100f;
                                }
                                this._setProgess(Convert.ToInt32(num3));
                                num2++;
                            }
                            bool flag = Executing.Instance.SaveImportCustomer(_list);
                            if (flag)
                            {
                                this.dtRead = new DataTable();
                                this.dtRead.Columns.Add("cust_id");
                                this.dtRead.Columns.Add("cust_name");
                                HomeScreen.frmHomeScreen frmMainMenu = new HomeScreen.frmHomeScreen(string.Empty);
                                frmMainMenu.Show();
                                this.Close();
                                /*
                                frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
                                frmMainMenu.Show();
                                this.Close();
                                */
                            }
                            else
                            {
                                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(1000);
                                    this.lblMessage.Show();
                                    this.lblMessage.Text = "CAN'T IMPORT CUSTOMER FILE";
                                    this.btnOpenFile.Enabled = true;
                                    this.btnStartImport.Enabled = true;
                                }
                            }
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ClassMsg.DialogWarning(ex.InnerException.Message);
                    Executing.Instance.Insert_Log("IMPORTCUST_SC6", ex.Message.ToString(), "frmImportCustomer", "btnStartImport_Click");
                    this.btnOpenFile.Enabled = true;
                    this.btnStartImport.Enabled = false;
                }
                else
                {
                    ClassMsg.DialogWarning(ex.Message);
                    Executing.Instance.Insert_Log("IMPORTCUST_SC6", ex.Message.ToString(), "frmImportCustomer", "btnStartImport_Click");
                }
            }
            finally
            {
                UICursor.CursorStop();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Interval = 100;
            this.lblMessage.Hide();

            this.batt.UpdateBatteryLife();
            this.batt.Refresh();
        }

        #endregion
    }
}