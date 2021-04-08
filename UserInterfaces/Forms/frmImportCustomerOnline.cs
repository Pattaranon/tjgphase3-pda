using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MsgBox.ClassMsgBox;
using UserInterfaces.Main;
using UserInterfaces.Execute;
using System.IO;
using EntitiesTemp.Customer;
using System.Media;
using System.Threading;

namespace UserInterfaces.Forms
{
    public partial class frmImportCustomerOnline : Form
    {
        #region Member

        DataTable dtRead = new DataTable();
        public delegate void _progess(int _value);

        //private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";

        #endregion

        #region Constructor

        public frmImportCustomerOnline()
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
            frmMainMenu frmMainMenu = new frmMainMenu(string.Empty);
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
                bool importCustomer = Executing.Instance.getImportCustomer();
                if (importCustomer)
                {
                    if (!Executing.Instance.DeleteCustomerInSqlCeOnPDA())
                    {
                        this.lblMessage.Show();
                        this.lblMessage.Text = "Can't import customer.";
                        return;
                    }
                }
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

                                //this.dtRead.Rows.Add([num] (0, array2[0]);
                                //this.dtRead.Rows.get_Item(num).set_Item(1, array2[1]);
                            }
                        }
                    }
                    streamReader.Dispose();
                }
                this.btnOpenFile.Enabled = false;
                this.btnStartImport.Enabled = false;
                if (this.dtRead.Rows.Count > 0)
                {
                    List<CustomerEntity> list = new List<CustomerEntity>();
                    int num2 = 1;
                    for (int i = 0; i < this.dtRead.Rows.Count; i++)
                    {
                        list.Add(new CustomerEntity
                        {
                            cust_id = this.dtRead.Rows[i]["cust_id"].ToString().Trim(),
                            cust_name = this.dtRead.Rows[i]["cust_id"].ToString().Trim()
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
                    bool flag = Executing.Instance.SaveImportCustomer(list);
                    if (flag)
                    {
                        this.dtRead = new DataTable();
                        this.dtRead.Columns.Add("cust_id");
                        this.dtRead.Columns.Add("cust_name");
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
                            this.btnOpenFile.Enabled = true;
                            this.btnStartImport.Enabled = true;
                        }
                    }
                }
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

        #endregion
    }
}