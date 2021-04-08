using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utilities.ClassMsgBox;
using UI.Configs;
using SPACE.TJG.Barcode.UI.Execute;

namespace SPACE.TJG.Barcode.UI.Activates
{
    public partial class frmActivateLicense : Form
    {
        #region Constuctor

        public frmActivateLicense()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        private void SubmitLicence()
        {
            if (string.IsNullOrEmpty(this.txtActivateKeys.Text.Trim()))
            {
                ClassMsg.DialogWarning("Please enter lincense keys");
                this.txtActivateKeys.Focus();
                this.txtActivateKeys.SelectAll();
                return;
            }
            else if (!string.IsNullOrEmpty(this.txtActivateKeys.Text.Trim()))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string _licence_value = Executing.Instance.ValidationLicence(this.txtActivateKeys.Text.Trim());
                    if (_licence_value.Trim() == "TRUE")
                    {
                        ClassMsg.DialogInfomation("Lincense Accept. Please start program");
                        Application.Exit();
                        //Application.Restart();
                    }
                    else if (_licence_value.Trim().IndexOf("|") >= 0)
                    {
                        string[] array = _licence_value.Trim().Split('|');
                        ClassMsg.DialogInfomation("Lincense Accept. Expiry Date " + array[1].ToString().Trim() + " Please start program");
                        Application.Exit();
                        //Application.Restart();
                    }
                    else if (_licence_value.Trim() == "TRUE_EXPIRED")
                    {
                        if (MessageActivate.DialogQuestion("Lincense expire. Do you want register lincense again?")
                                        == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.txtActivateKeys.Focus();
                            this.txtActivateKeys.SelectAll();
                            return;
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                    else if (_licence_value.Trim() == "FALSE")
                    {
                        if (MessageActivate.DialogQuestion("Lincense incorrect Do you want register lincense again?")
                                        == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.txtActivateKeys.Focus();
                            this.txtActivateKeys.SelectAll();
                            return;
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Write Log
                    Executing.Instance.Insert_Log("ERR_SUBMITKEY", ex.Message.ToString(), "Program.cs", "Method SubmitLicence()");

                    // Message Dialog
                    if (MessageActivate.DialogQuestion("Lincense incorrect Do you want register lincense again?")
                            == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.txtActivateKeys.Focus();
                        this.txtActivateKeys.SelectAll();
                        return;
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        #endregion

        #region Event

        private void frmActivateLicense_Load(object sender, EventArgs e)
        {
            this.txtActivateKeys.Focus();
            this.txtActivateKeys.SelectAll();
        }
        private void frmActivateLicense_KeyUp(object sender, KeyEventArgs e)
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

        private void txtActivateKeys_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SubmitLicence();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitLicence();
        }

        private void lklLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}