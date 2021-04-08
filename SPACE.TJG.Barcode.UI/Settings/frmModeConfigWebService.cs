using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UI.Configs;
using MsgBox.ClassMsgBox;
using SPACE.TJG.Barcode.UI.Configs;
using SPACE.TJG.Barcode.UI.Main;
using SPACE.TJG.Barcode.UI.Forms;

namespace SPACE.TJG.Barcode.UI.Settings
{
    public partial class frmModeConfigWebService : Form
    {
        #region "Constructor"

        public frmModeConfigWebService()
        {
            InitializeComponent();
        }

        #endregion

        #region "VeriflyConfigWebservice"

        private bool VeriflyData()
        {
            if (string.IsNullOrEmpty(txtURLWebService.Text.Trim()))
            {
                ClassMsg.DialogWarning("Please input URL Web service or IPServername !");
                txtURLWebService.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region "Method FormLoad"

        private void InitializeFormLoad()
        {
            //แสดง IP ที่ได้เก็บไว้ที่ textfile
            Cursor.Current = Cursors.Default;
            txtURLWebService.Text = iConfig.ReadConfig().Trim();

            this.btnSave.Visible = false;
        }

        #endregion

        #region "SaveConfig"

        private void SaveConfig()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor; 
                iConfig.SaveConfig(this.txtURLWebService.Text.Trim());
                ClassMsg.DialogInfomation("Save Server Name/IP success");
                iConfig.IPAddress = txtURLWebService.Text.Trim();

                frmModeImportCustomer fModeImport = new frmModeImportCustomer();
                fModeImport.Show();
                this.Close();
            }
            catch (System.Net.WebException WebEx) 
            { 
                Utilities.ClassMsgBox.ClassMsg.DialogError("Can't connect to server : " + WebEx.Message); 
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("System warning" + ex.Message); 
                this.txtURLWebService.Focus(); 
                this.txtURLWebService.SelectAll(); 
                return; 
            }
            finally 
            { 
                Cursor.Current = Cursors.Default; 
            }
        }

        #endregion

        #region "FormLoad"

        private void frmConfigWebService_Load(object sender, EventArgs e)
        {
            try 
            { 
                Cursor.Current = Cursors.WaitCursor; 
                InitializeFormLoad(); 
            }
            catch (System.Net.WebException WebEx) 
            { 
                Utilities.ClassMsgBox.ClassMsg.DialogError("Error form load of web : " + WebEx.Message); 
            }
            catch (Exception ex) 
            {
                Utilities.ClassMsgBox.ClassMsg.DialogError("Error form load : " + ex.Message); 
            }
            finally 
            { 
                Cursor.Current = Cursors.Default; 
            }
        }

        #endregion

        #region "btntestConnection"

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!VeriflyData())
                {
                    return;
                }
                using (UserInterfaces.wsTJG.Service wsTest = new UserInterfaces.wsTJG.Service())
                {
                    wsTest.Url = "http://" + txtURLWebService.Text.Trim() + "/TJGService/Service.svc";
                    bool OutputResult; bool OutputSpacial;
                    wsTest.ConnectDB(out OutputResult, out OutputSpacial);
                    if (OutputResult)
                    {
                        Cursor.Current = Cursors.Default;
                        ClassMsg.DialogInfomation("Connection server complete");
                        this.btnSave.Visible = true;
                    }
                    else
                    {
                        this.btnSave.Visible = false;
                    }
                }
            }
            catch (System.Net.WebException WebEx)
            {
                Utilities.ClassMsgBox.ClassMsg.DialogError("Error test connection of web : " + WebEx.Message);
                this.btnSave.Visible = false;
                this.txtURLWebService.Focus();
                this.txtURLWebService.SelectAll();
            }
            catch (Exception ex)
            {
                Utilities.ClassMsgBox.ClassMsg.DialogError("Error test connection : " + ex.Message);
                this.btnSave.Visible = false;
                this.txtURLWebService.Focus();
                this.txtURLWebService.SelectAll();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        #region "btnSave"

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtURLWebService.Text.Trim().Length == 0 || this.txtURLWebService.Text.Trim() == string.Empty
                || this.txtURLWebService.Text.Trim() == "")
            {
                ClassMsg.DialogWarning("Please insert Server Name/IP"); txtURLWebService.Focus(); return;
            }
            else 
            { 
                SaveConfig(); 
            }
        }

        #endregion

        #region "txtURL"

        private void txtURLWebService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            { 
                btnTestConnection_Click(null, new EventArgs()); 
            }
        }

        #endregion

        #region "lklLogout_Click"

        private void lklLogout_Click(object sender, EventArgs e)
        {
            frmModeImportCustomer fModeImport = new frmModeImportCustomer();
            fModeImport.Show();
            this.Close();
        }

        #endregion
    }
}