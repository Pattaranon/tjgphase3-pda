using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MsgBox.ClassMsgBox;
using UserInterfaces.Execute;
using ServiceFunction.Cursor;
using System.Media;
using System.Threading;
using System.IO;
using EntitiesTemp.SAP;

namespace UserInterfaces.Forms
{
    public partial class frmImportSAP : Form
    {
        #region Member

        DataTable dtRead = new DataTable();
        public delegate void _progess(int _value);

        //private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmImportSAP()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        public void _setProgess(int _value)
        {
            if (this.pgbStartImport.InvokeRequired)
            {
                frmImportSAP._progess progess = new frmImportSAP._progess(this._setProgess);
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

        private void frmImportSAP_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.dtRead.Columns.Add("Posting_Date");
                this.dtRead.Columns.Add("Transaction_Type");
                this.dtRead.Columns.Add("Delivery_No");
                this.dtRead.Columns.Add("Invoice_No");
                this.dtRead.Columns.Add("Customer");
                this.dtRead.Columns.Add("Customer_Name");
                this.dtRead.Columns.Add("Material");
                this.dtRead.Columns.Add("Material_Description");
                this.dtRead.Columns.Add("Quantity");
                this.dtRead.Columns.Add("Serial_Number");
                this.dtRead.Columns.Add("Batch");
                this.dtRead.Columns.Add("Plant");
                this.dtRead.Columns.Add("Plant_Name");
                this.dtRead.Columns.Add("Car_ID");
                this.dtRead.Columns.Add("Customer_Purchase_Order_Number");

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
        private void frmImportSAP_KeyUp(object sender, KeyEventArgs e)
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
                        this.lblMessage.Text = "Please select file SAP";
                    }
                }
                else
                {
                    UICursor.CursorWait();
                    if (Executing.Instance.getImportSAP())
                    {
                        #region Have SAP

                        this.dtRead = new DataTable();

                        // Check column in datatable.
                        if (!this.dtRead.Columns.Contains("Posting_Date"))
                            this.dtRead.Columns.Add("Posting_Date");
                        if (!this.dtRead.Columns.Contains("Transaction_Type"))
                            this.dtRead.Columns.Add("Transaction_Type");
                        if (!this.dtRead.Columns.Contains("Delivery_No"))
                            this.dtRead.Columns.Add("Delivery_No");
                        if (!this.dtRead.Columns.Contains("Invoice_No"))
                            this.dtRead.Columns.Add("Invoice_No");
                        if (!this.dtRead.Columns.Contains("Customer"))
                            this.dtRead.Columns.Add("Customer");
                        if (!this.dtRead.Columns.Contains("Customer_Name"))
                            this.dtRead.Columns.Add("Customer_Name");
                        if (!this.dtRead.Columns.Contains("Material"))
                            this.dtRead.Columns.Add("Material");
                        if (!this.dtRead.Columns.Contains("Material_Description"))
                            this.dtRead.Columns.Add("Material_Description");
                        if (!this.dtRead.Columns.Contains("Quantity"))
                            this.dtRead.Columns.Add("Quantity");
                        if (!this.dtRead.Columns.Contains("Serial_Number"))
                            this.dtRead.Columns.Add("Serial_Number");
                        if (!this.dtRead.Columns.Contains("Batch"))
                            this.dtRead.Columns.Add("Batch");
                        if (!this.dtRead.Columns.Contains("Batch"))
                            this.dtRead.Columns.Add("Batch");
                        if (!this.dtRead.Columns.Contains("Plant"))
                            this.dtRead.Columns.Add("Plant");
                        if (!this.dtRead.Columns.Contains("Batch"))
                            this.dtRead.Columns.Add("Batch");
                        if (!this.dtRead.Columns.Contains("Plant_Name"))
                            this.dtRead.Columns.Add("Plant_Name");
                        if (!this.dtRead.Columns.Contains("Car_ID"))
                            this.dtRead.Columns.Add("Car_ID");
                        if (!this.dtRead.Columns.Contains("Customer_Purchase_Order_Number"))
                            this.dtRead.Columns.Add("Customer_Purchase_Order_Number");

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
									Convert.ToChar('|')
								});

                                        if (array2.Length == 18)
                                        {
                                            this.lblMessage.Show();
                                            this.lblMessage.Text = "Select file not type for SAP.";
                                            return;
                                        }

                                        this.dtRead.Rows.Add(new object[0]);
                                        int num = this.dtRead.Rows.Count - 1;

                                        this.dtRead.Rows[num]["Posting_Date"] = array2[1].Trim();
                                        this.dtRead.Rows[num]["Transaction_Type"] = array2[2].Trim();
                                        this.dtRead.Rows[num]["Delivery_No"] = array2[3].Trim();
                                        this.dtRead.Rows[num]["Invoice_No"] = array2[4].Trim();
                                        this.dtRead.Rows[num]["Customer"] = array2[5].Trim();
                                        this.dtRead.Rows[num]["Customer_Name"] = array2[6].Trim();
                                        this.dtRead.Rows[num]["Material"] = array2[7].Trim();
                                        this.dtRead.Rows[num]["Material_Description"] = array2[8].Trim();
                                        this.dtRead.Rows[num]["Quantity"] = array2[9].Trim();
                                        this.dtRead.Rows[num]["Serial_Number"] = array2[10].Trim();
                                        this.dtRead.Rows[num]["Batch"] = array2[11].Trim();
                                        this.dtRead.Rows[num]["Plant"] = array2[12].Trim();
                                        this.dtRead.Rows[num]["Plant_Name"] = array2[13].Trim();
                                        this.dtRead.Rows[num]["Car_ID"] = array2[14].Trim();
                                        this.dtRead.Rows[num]["Customer_Purchase_Order_Number"] = array2[15].Trim();
                                    }
                                }
                            }
                            streamReader.Dispose();
                        }
                        this.btnOpenFile.Enabled = false;
                        this.btnStartImport.Enabled = false;
                        if (this.dtRead.Rows.Count > 0)
                        {
                            if (!this.chkContinuous.Checked)
                            {
                                if (!Executing.Instance.DeleteSAPInSqlCeOnPDA())
                                {
                                    this.lblMessage.Show();
                                    this.lblMessage.Text = "Can't import SAP.";
                                    return;
                                }
                                else
                                {
                                    #region Import SAP

                                    List<SAPEntity> _list = new List<SAPEntity>();
                                    int num2 = 1;
                                    for (int i = 0; i < this.dtRead.Rows.Count; i++)
                                    {
                                        _list.Add(new SAPEntity
                                        {
                                            Posting_Date = this.dtRead.Rows[i]["Posting_Date"].ToString().Trim(),
                                            Transaction_Type = this.dtRead.Rows[i]["Transaction_Type"].ToString().Trim(),
                                            Delivery_No = this.dtRead.Rows[i]["Delivery_No"].ToString().Trim(),
                                            Invoice_No = this.dtRead.Rows[i]["Invoice_No"].ToString().Trim(),
                                            Customer = this.dtRead.Rows[i]["Customer"].ToString().Trim(),
                                            Customer_Name = this.dtRead.Rows[i]["Customer_Name"].ToString().Trim(),
                                            Material = this.dtRead.Rows[i]["Material"].ToString().Trim(),
                                            Material_Description = this.dtRead.Rows[i]["Material_Description"].ToString().Trim(),
                                            Quantity = this.dtRead.Rows[i]["Quantity"].ToString().Trim(),
                                            Serial_Number = this.dtRead.Rows[i]["Serial_Number"].ToString().Trim(),
                                            Batch = this.dtRead.Rows[i]["Batch"].ToString().Trim(),
                                            Plant = this.dtRead.Rows[i]["Plant"].ToString().Trim(),
                                            Plant_Name = this.dtRead.Rows[i]["Plant_Name"].ToString().Trim(),
                                            Car_ID = this.dtRead.Rows[i]["Car_ID"].ToString().Trim(),
                                            Customer_Purchase_Order_Number = this.dtRead.Rows[i]["Customer_Purchase_Order_Number"].ToString().Trim()
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
                                    bool flag = Executing.Instance.SaveImportSAP(_list);
                                    if (flag)
                                    {
                                        this.dtRead = new DataTable();
                                        this.dtRead.Columns.Add("Posting_Date");
                                        this.dtRead.Columns.Add("Transaction_Type");
                                        this.dtRead.Columns.Add("Delivery_No");
                                        this.dtRead.Columns.Add("Invoice_No");
                                        this.dtRead.Columns.Add("Customer");
                                        this.dtRead.Columns.Add("Customer_Name");
                                        this.dtRead.Columns.Add("Material");
                                        this.dtRead.Columns.Add("Material_Description");
                                        this.dtRead.Columns.Add("Quantity");
                                        this.dtRead.Columns.Add("Serial_Number");
                                        this.dtRead.Columns.Add("Batch");
                                        this.dtRead.Columns.Add("Plant");
                                        this.dtRead.Columns.Add("Plant_Name");
                                        this.dtRead.Columns.Add("Car_ID");
                                        this.dtRead.Columns.Add("Customer_Purchase_Order_Number");
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
                                            this.lblMessage.Text = "CAN'T IMPORT SAP FILE";
                                            this.btnOpenFile.Enabled = true;
                                            this.btnStartImport.Enabled = true;
                                        }
                                    }

                                    #endregion
                                }
                            }
                            else
                            {
                                #region Import SAP Continuous

                                List<SAPEntity> _list = new List<SAPEntity>();
                                int num2 = 1;
                                for (int i = 0; i < this.dtRead.Rows.Count; i++)
                                {
                                    _list.Add(new SAPEntity
                                    {
                                        Posting_Date = this.dtRead.Rows[i]["Posting_Date"].ToString().Trim(),
                                        Transaction_Type = this.dtRead.Rows[i]["Transaction_Type"].ToString().Trim(),
                                        Delivery_No = this.dtRead.Rows[i]["Delivery_No"].ToString().Trim(),
                                        Invoice_No = this.dtRead.Rows[i]["Invoice_No"].ToString().Trim(),
                                        Customer = this.dtRead.Rows[i]["Customer"].ToString().Trim(),
                                        Customer_Name = this.dtRead.Rows[i]["Customer_Name"].ToString().Trim(),
                                        Material = this.dtRead.Rows[i]["Material"].ToString().Trim(),
                                        Material_Description = this.dtRead.Rows[i]["Material_Description"].ToString().Trim(),
                                        Quantity = this.dtRead.Rows[i]["Quantity"].ToString().Trim(),
                                        Serial_Number = this.dtRead.Rows[i]["Serial_Number"].ToString().Trim(),
                                        Batch = this.dtRead.Rows[i]["Batch"].ToString().Trim(),
                                        Plant = this.dtRead.Rows[i]["Plant"].ToString().Trim(),
                                        Plant_Name = this.dtRead.Rows[i]["Plant_Name"].ToString().Trim(),
                                        Car_ID = this.dtRead.Rows[i]["Car_ID"].ToString().Trim(),
                                        Customer_Purchase_Order_Number = this.dtRead.Rows[i]["Customer_Purchase_Order_Number"].ToString().Trim()
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
                                bool flag = Executing.Instance.SaveImportSAP(_list);
                                if (flag)
                                {
                                    this.dtRead = new DataTable();
                                    this.dtRead.Columns.Add("Posting_Date");
                                    this.dtRead.Columns.Add("Transaction_Type");
                                    this.dtRead.Columns.Add("Delivery_No");
                                    this.dtRead.Columns.Add("Invoice_No");
                                    this.dtRead.Columns.Add("Customer");
                                    this.dtRead.Columns.Add("Customer_Name");
                                    this.dtRead.Columns.Add("Material");
                                    this.dtRead.Columns.Add("Material_Description");
                                    this.dtRead.Columns.Add("Quantity");
                                    this.dtRead.Columns.Add("Serial_Number");
                                    this.dtRead.Columns.Add("Batch");
                                    this.dtRead.Columns.Add("Plant");
                                    this.dtRead.Columns.Add("Plant_Name");
                                    this.dtRead.Columns.Add("Car_ID");
                                    this.dtRead.Columns.Add("Customer_Purchase_Order_Number");
                                    HomeScreen.frmHomeScreen frmMainMenu = new HomeScreen.frmHomeScreen("LIFETIME");
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
                                        this.lblMessage.Text = "CAN'T IMPORT SAP FILE";
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
                        #region Not have SAP

                        this.dtRead = new DataTable();

                        // Check column in datatable.
                        if (!this.dtRead.Columns.Contains("Posting_Date"))
                            this.dtRead.Columns.Add("Posting_Date");
                        if (!this.dtRead.Columns.Contains("Transaction_Type"))
                            this.dtRead.Columns.Add("Transaction_Type");
                        if (!this.dtRead.Columns.Contains("Delivery_No"))
                            this.dtRead.Columns.Add("Delivery_No");
                        if (!this.dtRead.Columns.Contains("Invoice_No"))
                            this.dtRead.Columns.Add("Invoice_No");
                        if (!this.dtRead.Columns.Contains("Customer"))
                            this.dtRead.Columns.Add("Customer");
                        if (!this.dtRead.Columns.Contains("Customer_Name"))
                            this.dtRead.Columns.Add("Customer_Name");
                        if (!this.dtRead.Columns.Contains("Material"))
                            this.dtRead.Columns.Add("Material");
                        if (!this.dtRead.Columns.Contains("Material_Description"))
                            this.dtRead.Columns.Add("Material_Description");
                        if (!this.dtRead.Columns.Contains("Quantity"))
                            this.dtRead.Columns.Add("Quantity");
                        if (!this.dtRead.Columns.Contains("Serial_Number"))
                            this.dtRead.Columns.Add("Serial_Number");
                        if (!this.dtRead.Columns.Contains("Batch"))
                            this.dtRead.Columns.Add("Batch");
                        if (!this.dtRead.Columns.Contains("Batch"))
                            this.dtRead.Columns.Add("Batch");
                        if (!this.dtRead.Columns.Contains("Plant"))
                            this.dtRead.Columns.Add("Plant");
                        if (!this.dtRead.Columns.Contains("Batch"))
                            this.dtRead.Columns.Add("Batch");
                        if (!this.dtRead.Columns.Contains("Plant_Name"))
                            this.dtRead.Columns.Add("Plant_Name");
                        if (!this.dtRead.Columns.Contains("Car_ID"))
                            this.dtRead.Columns.Add("Car_ID");
                        if (!this.dtRead.Columns.Contains("Customer_Purchase_Order_Number"))
                            this.dtRead.Columns.Add("Customer_Purchase_Order_Number");

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
									Convert.ToChar('|')
								});

                                        if (array2.Length == 18)
                                        {
                                            this.lblMessage.Show();
                                            this.lblMessage.Text = "Select file not type for SAP.";
                                            return;
                                        }

                                        this.dtRead.Rows.Add(new object[0]);
                                        int num = this.dtRead.Rows.Count - 1;

                                        this.dtRead.Rows[num]["Posting_Date"] = array2[1].Trim();
                                        this.dtRead.Rows[num]["Transaction_Type"] = array2[2].Trim();
                                        this.dtRead.Rows[num]["Delivery_No"] = array2[3].Trim();
                                        this.dtRead.Rows[num]["Invoice_No"] = array2[4].Trim();
                                        this.dtRead.Rows[num]["Customer"] = array2[5].Trim();
                                        this.dtRead.Rows[num]["Customer_Name"] = array2[6].Trim();
                                        this.dtRead.Rows[num]["Material"] = array2[7].Trim();
                                        this.dtRead.Rows[num]["Material_Description"] = array2[8].Trim();
                                        this.dtRead.Rows[num]["Quantity"] = array2[9].Trim();
                                        this.dtRead.Rows[num]["Serial_Number"] = array2[10].Trim();
                                        this.dtRead.Rows[num]["Batch"] = array2[11].Trim();
                                        this.dtRead.Rows[num]["Plant"] = array2[12].Trim();
                                        this.dtRead.Rows[num]["Plant_Name"] = array2[13].Trim();
                                        this.dtRead.Rows[num]["Car_ID"] = array2[14].Trim();
                                        this.dtRead.Rows[num]["Customer_Purchase_Order_Number"] = array2[15].Trim();
                                    }
                                }
                            }
                            streamReader.Dispose();
                        }
                        this.btnOpenFile.Enabled = false;
                        this.btnStartImport.Enabled = false;
                        if (this.dtRead.Rows.Count > 0)
                        {
                            List<SAPEntity> _list = new List<SAPEntity>();
                            int num2 = 1;
                            for (int i = 0; i < this.dtRead.Rows.Count; i++)
                            {
                                _list.Add(new SAPEntity
                                {
                                    Posting_Date = this.dtRead.Rows[i]["Posting_Date"].ToString().Trim(),
                                    Transaction_Type = this.dtRead.Rows[i]["Transaction_Type"].ToString().Trim(),
                                    Delivery_No = this.dtRead.Rows[i]["Delivery_No"].ToString().Trim(),
                                    Invoice_No = this.dtRead.Rows[i]["Invoice_No"].ToString().Trim(),
                                    Customer = this.dtRead.Rows[i]["Customer"].ToString().Trim(),
                                    Customer_Name = this.dtRead.Rows[i]["Customer_Name"].ToString().Trim(),
                                    Material = this.dtRead.Rows[i]["Material"].ToString().Trim(),
                                    Material_Description = this.dtRead.Rows[i]["Material_Description"].ToString().Trim(),
                                    Quantity = this.dtRead.Rows[i]["Quantity"].ToString().Trim(),
                                    Serial_Number = this.dtRead.Rows[i]["Serial_Number"].ToString().Trim(),
                                    Batch = this.dtRead.Rows[i]["Batch"].ToString().Trim(),
                                    Plant = this.dtRead.Rows[i]["Plant"].ToString().Trim(),
                                    Plant_Name = this.dtRead.Rows[i]["Plant_Name"].ToString().Trim(),
                                    Car_ID = this.dtRead.Rows[i]["Car_ID"].ToString().Trim(),
                                    Customer_Purchase_Order_Number = this.dtRead.Rows[i]["Customer_Purchase_Order_Number"].ToString().Trim()
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
                            bool flag = Executing.Instance.SaveImportSAP(_list);
                            if (flag)
                            {
                                this.dtRead = new DataTable();
                                this.dtRead.Columns.Add("Posting_Date");
                                this.dtRead.Columns.Add("Transaction_Type");
                                this.dtRead.Columns.Add("Delivery_No");
                                this.dtRead.Columns.Add("Invoice_No");
                                this.dtRead.Columns.Add("Customer");
                                this.dtRead.Columns.Add("Customer_Name");
                                this.dtRead.Columns.Add("Material");
                                this.dtRead.Columns.Add("Material_Description");
                                this.dtRead.Columns.Add("Quantity");
                                this.dtRead.Columns.Add("Serial_Number");
                                this.dtRead.Columns.Add("Batch");
                                this.dtRead.Columns.Add("Plant");
                                this.dtRead.Columns.Add("Plant_Name");
                                this.dtRead.Columns.Add("Car_ID");
                                this.dtRead.Columns.Add("Customer_Purchase_Order_Number");
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
                                    this.lblMessage.Text = "CAN'T IMPORT SAP FILE";
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
                    Executing.Instance.Insert_Log("IMPORTSAP_SC7", ex.Message.ToString(), "frmImportSAP", "btnStartImport_Click");
                    this.btnOpenFile.Enabled = true;
                    this.btnStartImport.Enabled = false;
                }
                else
                {
                    ClassMsg.DialogWarning(ex.Message);
                    Executing.Instance.Insert_Log("IMPORTSAP_SC7", ex.Message.ToString(), "frmImportSAP", "btnStartImport_Click");
                    this.btnOpenFile.Enabled = true;
                    this.btnStartImport.Enabled = false;
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