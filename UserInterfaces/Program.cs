using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using UserInterfaces.Configs;
using UserInterfaces.Execute;
using UI.Configs;
using UserInterfaces.Activates;

namespace UserInterfaces
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            try
            {
                // Check table
                if (!Executing.Instance.checkTableExits("SAP_Import"))
                {
                    // Create Table
                    Executing.Instance.CreateTableSAPImport();
                }

                // Check password
                if (!Executing.Instance.checkPasswordExits("DOWNLOAD_SAP"))
                {
                    // Get Max Id Password
                    int IdMax = 0;
                    string tempMaxPassword = Executing.Instance.getMaxIdPassword();
                    if (!string.IsNullOrEmpty(tempMaxPassword))
                    {
                        IdMax = Convert.ToInt32(tempMaxPassword) + 1;
                        // Insert new password
                        Executing.Instance.Insert_Password(IdMax.ToString(), "DOWNLOAD_SAP", "1008");
                    }
                }

                if (Executing.Instance.CheckExpireSoftware().Trim() == "TRUE")
                {
                    Executing.Instance.IsSignIn();
                    Application.Run(new HomeScreen.frmHomeScreen("TRUE"));
                }
                else if (Executing.Instance.CheckExpireSoftware().Trim() == "LIFETIME")
                {
                    Executing.Instance.IsSignIn();
                    Application.Run(new HomeScreen.frmHomeScreen("LIFETIME"));
                }
                else if (Executing.Instance.CheckExpireSoftware().Trim() == "EXPIRE")
                {
                    if (MessageActivate.DialogQuestion("Software expire. Please contact IT for register license?")
                            == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (frmActivateLicense fAvtivate = new frmActivateLicense())
                        {
                            fAvtivate.ShowDialog();
                        }
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else if (Executing.Instance.CheckExpireSoftware().Trim() == "FALSE")
                {
                    MsgBox.ClassMsgBox.ClassMsg.DialogWarning("License incorrect Can't open program. Please contact IT");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Executing.Instance.Insert_Log("ERR00_MAIN", ex.Message.ToString(), "Program", "Method Main()");
            }
        }
    }
}