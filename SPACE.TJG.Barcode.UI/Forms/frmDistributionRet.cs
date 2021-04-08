using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using PrintCENET;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using SPACE.TJG.Barcode.UI.Execute;
using System.Media;
using EntitiesTemp.DistributionRet;
using ServiceFunction.Cursor;
using SPACE.TJG.Barcode.UI.Configs;
using Utilities.ClassMsgBox;
using ServiceFunction.Function;
using System.Net;

namespace SPACE.TJG.Barcode.UI.Forms
{
    public partial class frmDistributionRet : Form
    {
        #region Member

        private int mCurLang = 0;
        private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";
        public bool status_del { get; set; }
        public bool print_complete { get; set; }
        public bool print_error { get; set; }

        #endregion

        #region Properties

        public Action _Action { get; set; }

        #endregion

        #region Enum

        public enum Action
        {
            New,
            Edit_Cust,
            Edit_Doc,
            Edit_Vehicle,
            Edit_Select
        }

        #endregion

        #region Constructor

        public frmDistributionRet()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        private bool DoSave()
        {
            bool result;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DistributionRetEntity distributionRetEntity = new DistributionRetEntity();
                distributionRetEntity.cust_id = this.txtCustomer.Text.Trim();
                distributionRetEntity.doc_no = this.txtDoc.Text.Trim();
                distributionRetEntity.vehicle = this.txtVehicle.Text.Trim();
                distributionRetEntity.empty_or_full = this.txtSelect.Text.Trim();
                distributionRetEntity.serial_number = this.txtInputText.Text.Trim();
                distributionRetEntity.create_date = Executing.Instance.GetDateServer();
                bool flag = Executing.Instance.SaveDistributionRet(distributionRetEntity);
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
                Cursor.Current = Cursors.Default;
            }
            return result;
        }
        private bool BeginSave()
        {
            if (string.IsNullOrEmpty(this.txtCustomer.Text.Trim()))
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "Please key customer";
                    return false;
                }
            }
            else if (string.IsNullOrEmpty(this.txtDoc.Text.Trim()))
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "Please key doc";
                    return false;
                }
            }
            else if (string.IsNullOrEmpty(this.txtVehicle.Text.Trim()))
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "Please key vehicle";
                    return false;
                }
            }
            else if (string.IsNullOrEmpty(this.txtSelect.Text.Trim()))
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "Please select Empty/Full";
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Method Print

        private void Print(string cust_code, string cust_name, string doc_number, string vehicle, string empty_or_full, string serial_number, string count_serial)
        {
            if (empty_or_full.Trim() == "0")
            {
                #region empty_or_full = 0

                PrintASCII printASCII = null;
                try
                {
                    printASCII = new PrintASCII();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "empty_or_full = 0", "Method_Print()");
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
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    Thread.Sleep(200);
                    printASCII.SendString(string.Concat(new string[]
					{
						string.Empty,
						Environment.NewLine,
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ---------------------- Copy ----------------------",
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    this.print_error = true;
                    printASCII.Disconnect();
                }
                else
                {
                    this.print_error = false;
                }
                printASCII.UnInit();

                #endregion
            }
            else if (empty_or_full.Trim() == "9")
            {
                #region empty_or_full = 9

                PrintASCII printASCII = null;
                try
                {
                    printASCII = new PrintASCII();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "empty_or_full = 9", "Method_Print()");
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
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cy1",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cy1",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    Thread.Sleep(200);
                    printASCII.SendString(string.Concat(new string[]
					{
						string.Empty,
						Environment.NewLine,
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cy1",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cy1",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ---------------------- Copy ----------------------",
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    this.print_error = true;
                    printASCII.Disconnect();
                }
                else
                {
                    this.print_error = false;
                }
                printASCII.UnInit();

                #endregion
            }
        }
        private void PrintSlipTT(string cust_code, string cust_name, string doc_number, string vehicle, string empty_or_full1, string empty_or_full2, string serial_number_zero, string serial_number_nine, string count_serial)
        {
            if (!string.IsNullOrEmpty(empty_or_full1) && !string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF1

                PrintASCII printASCII = null;
                try
                {
                    printASCII = new PrintASCII();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF1", "Method_PrintSlipTT()");
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
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_zero,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_nine,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ......................................................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    Thread.Sleep(200);
                    printASCII.SendString(string.Concat(new string[]
					{
						string.Empty,
						Environment.NewLine,
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_zero,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_nine,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ---------------------- Copy ----------------------",
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    this.print_error = true;
                    printASCII.Disconnect();
                }
                else
                {
                    this.print_error = false;
                }
                printASCII.UnInit();

                #endregion
            }
            else if (string.IsNullOrEmpty(empty_or_full1) && !string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF2

                if (empty_or_full2.Trim() == "9" || empty_or_full1.Trim() == "9")
                {
                    PrintASCII printASCII = null;
                    try
                    {
                        printASCII = new PrintASCII();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF2", "Method_PrintSlipTT()");
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
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_nine,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ......................................................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        Thread.Sleep(200);
                        printASCII.SendString(string.Concat(new string[]
						{
							string.Empty,
							Environment.NewLine,
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_nine,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ---------------------- Copy ----------------------",
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        this.print_error = true;
                        printASCII.Disconnect();
                    }
                    else
                    {
                        this.print_error = false;
                    }
                    printASCII.UnInit();
                }

                #endregion
            }
            else if (!string.IsNullOrEmpty(empty_or_full1) && string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF3

                if (empty_or_full1.Trim() == "0" || empty_or_full2.Trim() == "0")
                {
                    PrintASCII printASCII = null;
                    try
                    {
                        printASCII = new PrintASCII();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF3", "Method_PrintSlipTT()");
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
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_zero,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							" Cyl",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ......................................................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        Thread.Sleep(200);
                        printASCII.SendString(string.Concat(new string[]
						{
							string.Empty,
							Environment.NewLine,
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_zero,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							" Cyl",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ---------------------- Copy ----------------------",
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        this.print_error = true;
                        printASCII.Disconnect();
                    }
                    else
                    {
                        this.print_error = false;
                    }
                    printASCII.UnInit();
                }

                #endregion
            }
            else if (string.IsNullOrEmpty(empty_or_full1) && string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF4

                PrintASCII printASCII = null;
                try
                {
                    printASCII = new PrintASCII();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF4", "Method_PrintSlipTT()");
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
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ......................................................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    Thread.Sleep(200);
                    printASCII.SendString(string.Concat(new string[]
					{
						string.Empty,
						Environment.NewLine,
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ---------------------- Copy ----------------------",
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    this.print_error = true;
                    printASCII.Disconnect();
                }
                else
                {
                    this.print_error = false;
                }
                printASCII.UnInit();
            }

                #endregion
        }
        private void PrintSlipTF(string cust_code, string cust_name, string doc_number, string vehicle, string empty_or_full1, string empty_or_full2, string serial_number_zero, string count_serial)
        {
            if (!string.IsNullOrEmpty(empty_or_full1) && !string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF1

                PrintASCII printASCII = null;
                try
                {
                    printASCII = new PrintASCII();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF1", "Method_PrintSlipTF()");
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
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_zero,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_zero,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ......................................................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    Thread.Sleep(200);
                    printASCII.SendString(string.Concat(new string[]
					{
						string.Empty,
						Environment.NewLine,
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_zero,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_zero,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ---------------------- Copy ----------------------",
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    this.print_error = true;
                    printASCII.Disconnect();
                }
                else
                {
                    this.print_error = false;
                }
                printASCII.UnInit();

                #endregion
            }
            else if (string.IsNullOrEmpty(empty_or_full1) && !string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF2

                if (empty_or_full2.Trim() == "9" || empty_or_full1.Trim() == "9")
                {
                    PrintASCII printASCII = null;
                    try
                    {
                        printASCII = new PrintASCII();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF2", "Method_PrintSlipTF()");
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
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_zero,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ......................................................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        Thread.Sleep(200);
                        printASCII.SendString(string.Concat(new string[]
						{
							string.Empty,
							Environment.NewLine,
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_zero,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ---------------------- Copy ----------------------",
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        this.print_error = true;
                        printASCII.Disconnect();
                    }
                    else
                    {
                        this.print_error = false;
                    }
                    printASCII.UnInit();
                }

                #endregion
            }
            else if (!string.IsNullOrEmpty(empty_or_full1) && string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF3

                if (empty_or_full1.Trim() == "0" || empty_or_full2.Trim() == "0")
                {
                    PrintASCII printASCII = null;
                    try
                    {
                        printASCII = new PrintASCII();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF3", "Method_PrintSlipTF()");
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
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_zero,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							" Cyl",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ......................................................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        Thread.Sleep(200);
                        printASCII.SendString(string.Concat(new string[]
						{
							string.Empty,
							Environment.NewLine,
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_zero,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							" Cyl",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ---------------------- Copy ----------------------",
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        this.print_error = true;
                        printASCII.Disconnect();
                    }
                    else
                    {
                        this.print_error = false;
                    }
                    printASCII.UnInit();
                }

                #endregion
            }
            else if (string.IsNullOrEmpty(empty_or_full1) && string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF4

                PrintASCII printASCII = null;
                try
                {
                    printASCII = new PrintASCII();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF4", "Method_PrintSlipTF()");
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
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ......................................................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    Thread.Sleep(200);
                    printASCII.SendString(string.Concat(new string[]
					{
						string.Empty,
						Environment.NewLine,
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ---------------------- Copy ----------------------",
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    this.print_error = true;
                    printASCII.Disconnect();
                }
                else
                {
                    this.print_error = false;
                }
                printASCII.UnInit();

                #endregion
            }
        }
        private void PrintSlipFT(string cust_code, string cust_name, string doc_number, string vehicle, string empty_or_full1, string empty_or_full2, string serial_number_nine, string count_serial)
        {
            if (!string.IsNullOrEmpty(empty_or_full1) && !string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF1

                PrintASCII printASCII = null;
                try
                {
                    printASCII = new PrintASCII();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF1", "Method_PrintSlipFT()");
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
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_nine,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_nine,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ......................................................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    Thread.Sleep(200);
                    printASCII.SendString(string.Concat(new string[]
					{
						string.Empty,
						Environment.NewLine,
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_nine,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number_nine,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ---------------------- Copy ----------------------",
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    this.print_error = true;
                    printASCII.Disconnect();
                }
                else
                {
                    this.print_error = false;
                }
                printASCII.UnInit();

                #endregion
            }
            else if (string.IsNullOrEmpty(empty_or_full1) && !string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF2

                if (empty_or_full2.Trim() == "9" || empty_or_full1.Trim() == "9")
                {
                    PrintASCII printASCII = null;
                    try
                    {
                        printASCII = new PrintASCII();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF2", "Method_PrintSlipFT()");
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
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_nine,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ......................................................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        Thread.Sleep(200);
                        printASCII.SendString(string.Concat(new string[]
						{
							string.Empty,
							Environment.NewLine,
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_nine,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ---------------------- Copy ----------------------",
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        this.print_error = true;
                        printASCII.Disconnect();
                    }
                    else
                    {
                        this.print_error = false;
                    }
                    printASCII.UnInit();
                }

                #endregion
            }
            else if (!string.IsNullOrEmpty(empty_or_full1) && string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF3

                if (empty_or_full1.Trim() == "0" || empty_or_full2.Trim() == "0")
                {
                    PrintASCII printASCII = null;
                    try
                    {
                        printASCII = new PrintASCII();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF3", "Method_PrintSlipFT()");
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
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_nine,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							" Cyl",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ......................................................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        Thread.Sleep(200);
                        printASCII.SendString(string.Concat(new string[]
						{
							string.Empty,
							Environment.NewLine,
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number_nine,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							" Cyl",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ---------------------- Copy ----------------------",
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        this.print_error = true;
                        printASCII.Disconnect();
                    }
                    else
                    {
                        this.print_error = false;
                    }
                    printASCII.UnInit();
                }

                #endregion
            }
            else if (string.IsNullOrEmpty(empty_or_full1) && string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF4

                PrintASCII printASCII = null;
                try
                {
                    printASCII = new PrintASCII();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF4", "Method_PrintSlipFT()");
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
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ......................................................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    Thread.Sleep(200);
                    printASCII.SendString(string.Concat(new string[]
					{
						string.Empty,
						Environment.NewLine,
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ---------------------- Copy ----------------------",
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    this.print_error = true;
                    printASCII.Disconnect();
                }
                else
                {
                    this.print_error = false;
                }
                printASCII.UnInit();

                #endregion
            }
        }
        private void PrintSlipFF(string cust_code, string cust_name, string doc_number, string vehicle, string empty_or_full1, string empty_or_full2, string serial_number, string count_serial)
        {
            if (!string.IsNullOrEmpty(empty_or_full1) && !string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF1

                PrintASCII printASCII = null;
                try
                {
                    printASCII = new PrintASCII();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF1", "Method_PrintSlipFF()");
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
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ......................................................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    Thread.Sleep(200);
                    printASCII.SendString(string.Concat(new string[]
					{
						string.Empty,
						Environment.NewLine,
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						serial_number,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ---------------------- Copy ----------------------",
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    this.print_error = true;
                    printASCII.Disconnect();
                }
                else
                {
                    this.print_error = false;
                }
                printASCII.UnInit();

                #endregion
            }
            else if (string.IsNullOrEmpty(empty_or_full1) && !string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF2

                if (empty_or_full2.Trim() == "9" || empty_or_full1.Trim() == "9")
                {
                    PrintASCII printASCII = null;
                    try
                    {
                        printASCII = new PrintASCII();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF2", "Method_PrintSlipFF()");
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
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ......................................................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        Thread.Sleep(200);
                        printASCII.SendString(string.Concat(new string[]
						{
							string.Empty,
							Environment.NewLine,
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cy1",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ---------------------- Copy ----------------------",
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        this.print_error = true;
                        printASCII.Disconnect();
                    }
                    else
                    {
                        this.print_error = false;
                    }
                    printASCII.UnInit();
                }

                #endregion
            }
            else if (!string.IsNullOrEmpty(empty_or_full1) && string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF3

                if (empty_or_full1.Trim() == "0" || empty_or_full2.Trim() == "0")
                {
                    PrintASCII printASCII = null;
                    try
                    {
                        printASCII = new PrintASCII();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF3", "Method_PrintSlipFF()");
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
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							" Cyl",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ......................................................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        Thread.Sleep(200);
                        printASCII.SendString(string.Concat(new string[]
						{
							string.Empty,
							Environment.NewLine,
							"                  Thai Japan Gas Co.,Ltd",
							Environment.NewLine,
							"             1/1 Rojana Industrial Park Moo5",
							Environment.NewLine,
							"            Khan Harm U-Thai Ayutthaya 13210",
							Environment.NewLine,
							"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"                 Cylinder Receving Form",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Customer Code: ",
							cust_code,
							Environment.NewLine,
							"          Customer Name: ",
							cust_name,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          DATE: ",
							DateTime.Now.ToString("dd/MM/yy HH:mm"),
							Environment.NewLine,
							"          Doc No: ",
							doc_number,
							Environment.NewLine,
							"          Vehicle No: ",
							vehicle,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          0:Empty Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							serial_number,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          9:Full Cyl",
							Environment.NewLine,
							"          S/N:",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Total: ",
							count_serial,
							" Cyl",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          Receiver:.................   Customer.................",
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							"          ---------------------- Copy ----------------------",
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine,
							string.Empty,
							Environment.NewLine
						}));
                        this.print_error = true;
                        printASCII.Disconnect();
                    }
                    else
                    {
                        this.print_error = false;
                    }
                    printASCII.UnInit();
                }

                #endregion
            }
            else if (string.IsNullOrEmpty(empty_or_full1) && string.IsNullOrEmpty(empty_or_full2))
            {
                #region IF4

                PrintASCII printASCII = null;
                try
                {
                    printASCII = new PrintASCII();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    Executing.Instance.Insert_Log("PrintingError", ex.Message.ToString(), "IF4", "Method_PrintSlipFF()");
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
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ......................................................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    Thread.Sleep(200);
                    printASCII.SendString(string.Concat(new string[]
					{
						string.Empty,
						Environment.NewLine,
						"                  Thai Japan Gas Co.,Ltd",
						Environment.NewLine,
						"             1/1 Rojana Industrial Park Moo5",
						Environment.NewLine,
						"            Khan Harm U-Thai Ayutthaya 13210",
						Environment.NewLine,
						"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"                 Cylinder Receving Form",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Customer Code: ",
						cust_code,
						Environment.NewLine,
						"          Customer Name: ",
						cust_name,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          DATE: ",
						DateTime.Now.ToString("dd/MM/yy HH:mm"),
						Environment.NewLine,
						"          Doc No: ",
						doc_number,
						Environment.NewLine,
						"          Vehicle No: ",
						vehicle,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          0:Empty Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          9:Full Cyl",
						Environment.NewLine,
						"          S/N:",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Total: ",
						count_serial,
						" Cyl",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          Receiver:.................   Customer.................",
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						"          ---------------------- Copy ----------------------",
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine,
						string.Empty,
						Environment.NewLine
					}));
                    this.print_error = true;
                    printASCII.Disconnect();
                }
                else
                {
                    this.print_error = false;
                }
                printASCII.UnInit();

                #endregion
            }
        }

        #endregion

        #region Event

        private void frmDistributionRet_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.lblMessage.Hide();

                this.txtCustomer.Focus();
                this.txtCustomer.SelectAll();

                if (this.status_del)
                {
                    this.lblMessage.Show();
                    this.lblMessage.Text = "Delete 1 Records";
                    this.lblCountByCustomer.Text = Executing.Instance.CountDistributionRetALL();
                }
                else
                {
                    this.lblMessage.Hide();
                    this._Action = Action.New;
                    this.lblCountByCustomer.Text = Executing.Instance.CountDistributionRetALL();
                    this.txtCustomer.Focus();
                    this.txtCustomer.SelectAll();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void frmDistributionRet_KeyUp(object sender, KeyEventArgs e)
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
                            this._Action = Action.Edit_Cust;
                            this.txtDoc.Enabled = true;
                            this.txtDoc.Focus();
                            this.txtDoc.SelectAll();
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
                                this.lblMessage.Text = "Customer not Found";
                                this.txtCustomer.Focus();
                                this.txtCustomer.SelectAll();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }
        private void txtDoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtDoc.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else if (string.IsNullOrEmpty(this.txtCustomer.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key customer";

                        this.txtDoc.Text = string.Empty;
                        this.txtCustomer.Focus();
                        this.txtCustomer.SelectAll();
                    }
                }
                else if (this.txtCustomer.Text.Length != 7)
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                        this.lblMessage.Show();
                        this.lblMessage.Text = "Input 7 Digit";

                        this.txtDoc.Text = string.Empty;
                        this.txtCustomer.Focus();
                        this.txtCustomer.SelectAll();
                    }
                }
                else if (this.txtCustomer.Text.Length == 7)
                {
                    this._Action = Action.Edit_Doc;
                    this.txtDoc.Enabled = false;
                    this.txtVehicle.Enabled = true;
                    this.txtVehicle.Focus();
                    this.txtVehicle.SelectAll();
                }
            }
        }
        private void txtVehicle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtVehicle.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else if (string.IsNullOrEmpty(this.txtDoc.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key doc";

                        this.txtVehicle.Text = string.Empty;
                        this.txtDoc.Focus();
                        this.txtDoc.SelectAll();
                    }
                }
                else
                {
                    this._Action = Action.Edit_Vehicle;
                    this.txtVehicle.Enabled = false;
                    this.txtSelect.Enabled = true;
                    this.txtSelect.Focus();
                    this.txtSelect.SelectAll();
                }
            }
        }
        private void txtSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtSelect.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else if (string.IsNullOrEmpty(this.txtVehicle.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key vehicle";

                        this.txtSelect.Text = string.Empty;
                        this.txtVehicle.Focus();
                        this.txtVehicle.SelectAll();
                    }
                }
                else if (this.txtSelect.Text.Trim() != "0" && this.txtSelect.Text.Trim() != "9")
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                        this.txtSelect.Text = string.Empty;
                        this.txtSelect.Enabled = true;
                        this.lblMessage.Show();
                        this.lblMessage.Text = "0 or 9 Only";
                        this.txtSelect.Focus();
                        this.txtSelect.SelectAll();
                    }
                }
                else
                {
                    this.txtSelect.Enabled = false;
                    this.txtInputText.Text = string.Empty;
                    this.txtInputText.Enabled = true;
                    this.txtInputText.Focus();
                    this.txtInputText.SelectAll();
                }
            }
        }
        private void txtInputText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtInputText.Text.Trim()))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    if (BeginSave())
                    {
                        bool flag = Executing.Instance.checkDistributionRetSerialDuplicate(this.txtInputText.Text.Trim());
                        if (flag)
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Duplicate Data";
                                this.txtInputText.Text = string.Empty;
                                this.txtInputText.Focus();
                                this.txtInputText.SelectAll();
                            }
                        }
                        else if (DoSave())
                        {
                            this.lblMessage.Show();
                            this.lblMessage.Text = "SAVE DATA";
                            this.lblCountByCustomer.Text = Executing.Instance.DistributionRetByCustomer(this.txtCustomer.Text.Trim());
                            this.txtInputText.Text = string.Empty;
                            this.txtInputText.Focus();
                            this.txtInputText.SelectAll();
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

        private void lnkExit_Click(object sender, EventArgs e)
        {
            if (this.txtCustomer.Enabled)
            {
                frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                frmMenuScanBarcode.Show();
                this.Close();
            }
            else if (!this.txtCustomer.Enabled)
            {
                if (this.txtDoc.Enabled)
                {
                    frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                    frmMenuScanBarcode.Show();
                    this.Close();
                }
                else if (!this.txtDoc.Enabled)
                {
                    if (this.txtVehicle.Enabled)
                    {
                        frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                        frmMenuScanBarcode.Show();
                        this.Close();
                    }
                    else if (!this.txtVehicle.Enabled)
                    {
                        if (this.txtSelect.Enabled)
                        {
                            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                            frmMenuScanBarcode.Show();
                            this.Close();
                        }
                        else if (!this.txtSelect.Enabled)
                        {
                            if (this.print_complete)
                            {
                                frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                                frmMenuScanBarcode.Show();
                                this.Close();
                            }
                            else
                            {
                                DataTable detailCustomer = Executing.Instance.getDetailCustomer(this.txtCustomer.Text.Trim(), this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim());
                                if (detailCustomer.Rows.Count > 0)
                                {
                                    var list = Enumerable.ToList(Enumerable.Select(DataTableExtensions.AsEnumerable(detailCustomer), (DataRow _list) => new
                                    {
                                        create_date = _list["create_date"],
                                        empty_or_full = _list["empty_or_full"].ToString(),
                                        doc_no = _list["doc_no"].ToString(),
                                        serial_number = _list["serial_number"].ToString(),
                                        vehicle = _list["vehicle"].ToString()
                                    }));
                                    string text = string.Empty;
                                    string _sub1 = string.Empty;
                                    string _sub2 = string.Empty;
                                    var list2 = Enumerable.ToList(
                                                Enumerable.Distinct(
                                                Enumerable.Select(
                                                Enumerable.Where(
                                                list, rows => rows.empty_or_full != null), rows => new
                                    {
                                        rows.empty_or_full
                                    })));
                                    using (var enumerator = Enumerable.Distinct(list2).GetEnumerator())
                                    {
                                        while (enumerator.MoveNext())
                                        {
                                            var current = enumerator.Current;
                                            if (!string.IsNullOrEmpty(current.empty_or_full))
                                            {
                                                text += current.empty_or_full;
                                            }
                                        }
                                    }
                                    if (text.Length == 2)
                                    {
                                        _sub1 = text.Substring(0, 1);
                                        if (_sub1.Trim() == "0")
                                        {
                                            _sub1 = "0";
                                        }
                                        else if (_sub1.Trim() == "9")
                                        {
                                            _sub1 = "0";
                                        }
                                        _sub2 = text.Substring(1, 1);
                                        if (_sub2.Trim() == "0")
                                        {
                                            _sub2 = "9";
                                        }
                                        else if (_sub1.Trim() == "9")
                                        {
                                            _sub2 = "9";
                                        }
                                    }
                                    else if (text.Length == 1)
                                    {
                                        if (text.Trim() == "0")
                                        {
                                            _sub1 = text.Trim();
                                        }
                                        else if (text.Trim() == "9")
                                        {
                                            _sub2 = text.Trim();
                                        }
                                        else
                                        {
                                            _sub1 = string.Empty;
                                            _sub2 = string.Empty;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(_sub1) && !string.IsNullOrEmpty(_sub2))
                                    {
                                        var list3 = Enumerable.ToList(Enumerable.Distinct(Enumerable.Where(list, it => it.empty_or_full == _sub1)));
                                        var list4 = Enumerable.ToList(Enumerable.Distinct(Enumerable.Where(list, it => it.empty_or_full == _sub2)));
                                        string text2 = string.Empty;
                                        string text3 = string.Empty;
                                        for (int i = 1; i <= Enumerable.Count(list3); i++)
                                        {
                                            if (i % 2 == 1)
                                            {
                                                text2 = text2 + "          " + list3[i - 1].serial_number;
                                                for (int j = 0; j < 27 - list3[i - 1].serial_number.Length; j++)
                                                {
                                                    text2 += " ";
                                                }
                                            }
                                            else
                                            {
                                                text2 = text2 + list3[i - 1].serial_number + Environment.NewLine;
                                            }
                                        }
                                        for (int i = 1; i <= Enumerable.Count(list4); i++)
                                        {
                                            if (i % 2 == 1)
                                            {
                                                text3 = text3 + "          " + list4[i - 1].serial_number;
                                                for (int j = 0; j < 27 - list4[i - 1].serial_number.Length; j++)
                                                {
                                                    text3 += " ";
                                                }
                                            }
                                            else
                                            {
                                                text3 = text3 + list4[i - 1].serial_number + Environment.NewLine;
                                            }
                                        }
                                        this.PrintSlipTT(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text2, text3, Enumerable.Count(list).ToString());
                                        if (!this.print_error)
                                        {
                                            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                                            frmMenuScanBarcode.Show();
                                            this.Close();
                                        }
                                        else
                                        {
                                            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                                            frmMenuScanBarcode.Show();
                                            this.Close();
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(_sub1) && string.IsNullOrEmpty(_sub2))
                                    {
                                        var list3 = Enumerable.ToList(Enumerable.Distinct(Enumerable.Where(list, it => it.empty_or_full == _sub1)));
                                        string text2 = string.Empty;
                                        for (int i = 1; i <= Enumerable.Count(list3); i++)
                                        {
                                            if (i % 2 == 1)
                                            {
                                                text2 = text2 + "          " + list3[i - 1].serial_number;
                                                for (int j = 0; j < 27 - list3[i - 1].serial_number.Length; j++)
                                                {
                                                    text2 += " ";
                                                }
                                            }
                                            else
                                            {
                                                text2 = text2 + list3[i - 1].serial_number + Environment.NewLine;
                                            }
                                        }
                                        this.PrintSlipTF(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text2, Enumerable.Count(list).ToString());
                                        if (!this.print_error)
                                        {
                                            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                                            frmMenuScanBarcode.Show();
                                            this.Close();
                                        }
                                        else
                                        {
                                            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                                            frmMenuScanBarcode.Show();
                                            this.Close();
                                        }
                                    }
                                    else if (string.IsNullOrEmpty(_sub1) && !string.IsNullOrEmpty(_sub2))
                                    {
                                        var list4 = Enumerable.ToList(Enumerable.Distinct(Enumerable.Where(list, it => it.empty_or_full == _sub2)));
                                        string text3 = string.Empty;
                                        for (int i = 1; i <= Enumerable.Count(list4); i++)
                                        {
                                            if (i % 2 == 1)
                                            {
                                                text3 = text3 + "          " + list4[i - 1].serial_number;
                                                for (int j = 0; j < 27 - list4[i - 1].serial_number.Length; j++)
                                                {
                                                    text3 += " ";
                                                }
                                            }
                                            else
                                            {
                                                text3 = text3 + list4[i - 1].serial_number + Environment.NewLine;
                                            }
                                        }
                                        this.PrintSlipFT(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text3, Enumerable.Count(list).ToString());
                                        if (!this.print_error)
                                        {
                                            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                                            frmMenuScanBarcode.Show();
                                            this.Close();
                                        }
                                        else
                                        {
                                            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                                            frmMenuScanBarcode.Show();
                                            this.Close();
                                        }
                                    }
                                    else if (string.IsNullOrEmpty(_sub1) && string.IsNullOrEmpty(_sub2))
                                    {
                                        string text4 = string.Empty;
                                        for (int i = 1; i <= Enumerable.Count(list); i++)
                                        {
                                            if (i % 2 == 1)
                                            {
                                                text4 = text4 + "          " + list[i - 1].serial_number;
                                                for (int j = 0; j < 27 - list[i - 1].serial_number.Length; j++)
                                                {
                                                    text4 += " ";
                                                }
                                            }
                                            else
                                            {
                                                text4 = text4 + list[i - 1].serial_number + Environment.NewLine;
                                            }
                                        }
                                        this.PrintSlipFF(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text4, Enumerable.Count(list).ToString());
                                        if (!this.print_error)
                                        {
                                            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                                            frmMenuScanBarcode.Show();
                                            this.Close();
                                        }
                                        else
                                        {
                                            frmMenuScanBarcode frmMenuScanBarcode = new frmMenuScanBarcode();
                                            frmMenuScanBarcode.Show();
                                            this.Close();
                                        }
                                    }
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
                }
            }
        }
        private void lnkNEW_Click(object sender, EventArgs e)
        {
            if (this._Action == Action.New)
            {
                this.txtCustomer.Text = string.Empty;
                this.txtCustomer.Enabled = true;
                this.lblCustName.Text = string.Empty;
                this.txtDoc.Text = string.Empty;
                this.txtDoc.Enabled = false;
                this.txtVehicle.Text = string.Empty;
                this.txtVehicle.Enabled = false;
                this.txtSelect.Text = string.Empty;
                this.txtSelect.Enabled = false;
                this.txtInputText.Text = string.Empty;
                this.txtInputText.Enabled = false;
                this.txtCustomer.Focus();
                this.txtCustomer.SelectAll();
            }
            else if (this._Action == Action.Edit_Cust)
            {
                // แก้ไขข้อมูล Document ได้
                this.txtDoc.Text = string.Empty;
                this.txtDoc.Enabled = true;
                this.txtVehicle.Text = string.Empty;
                this.txtVehicle.Enabled = false;
                this.txtSelect.Text = string.Empty;
                this.txtSelect.Enabled = false;
                this.txtInputText.Text = string.Empty;
                this.txtInputText.Enabled = false;
                this.txtDoc.Focus();
                this.txtDoc.SelectAll();
            }
            else if (this._Action == Action.Edit_Doc)
            {
                // แก้ไขข้อมูล Vehicle ได้
                this.txtVehicle.Text = string.Empty;
                this.txtVehicle.Enabled = true;
                this.txtSelect.Text = string.Empty;
                this.txtSelect.Enabled = false;
                this.txtInputText.Text = string.Empty;
                this.txtInputText.Enabled = false;
                this.txtVehicle.Focus();
                this.txtVehicle.SelectAll();
            }
            else if (this._Action == Action.Edit_Vehicle)
            {
                this.txtCustomer.Text = string.Empty;
                this.txtCustomer.Enabled = true;
                this.lblCustName.Text = string.Empty;
                this.txtDoc.Text = string.Empty;
                this.txtDoc.Enabled = false;
                this.txtVehicle.Text = string.Empty;
                this.txtVehicle.Enabled = false;
                this.txtSelect.Text = string.Empty;
                this.txtSelect.Enabled = false;
                this.txtInputText.Text = string.Empty;
                this.txtInputText.Enabled = false;
                this.lblCountByCustomer.Text = "0";
                this.txtCustomer.Focus();
                this.txtCustomer.SelectAll();
            }
            else if (this._Action == Action.Edit_Select)
            {
                this.txtCustomer.Text = string.Empty;
                this.txtCustomer.Enabled = true;
                this.lblCustName.Text = string.Empty;
                this.txtDoc.Text = string.Empty;
                this.txtDoc.Enabled = false;
                this.txtVehicle.Text = string.Empty;
                this.txtVehicle.Enabled = false;
                this.txtSelect.Text = string.Empty;
                this.txtSelect.Enabled = false;
                this.txtInputText.Text = string.Empty;
                this.txtInputText.Enabled = false;
                this.lblResult.Visible = false;
                this.lblCountByCustomer.Visible = false;
                this.txtCustomer.Focus();
                this.txtCustomer.SelectAll();
            }
        }
        private void lnkDelete_Click(object sender, EventArgs e)
        {
            if (Executing.Instance.CountDistributionRetALL() == "0")
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "NO Data to Del";
                }
            }
            else
            {
                frmSelectDistributionRetDel frmSelectDistributionRetDel = new frmSelectDistributionRetDel();
                frmSelectDistributionRetDel.Show();
                this.Close();
            }
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.txtCustomer.Enabled)
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "No Data to Print";
                    this.txtCustomer.Text = string.Empty;
                    this.txtCustomer.Enabled = true;
                }
            }
            else if (!this.txtCustomer.Enabled)
            {
                if (this.txtDoc.Enabled)
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                        this.lblMessage.Show();
                        this.lblMessage.Text = "No Data to Print";
                        this.txtDoc.Text = string.Empty;
                        this.txtDoc.Enabled = true;
                    }
                }
                else if (!this.txtDoc.Enabled)
                {
                    if (this.txtVehicle.Enabled)
                    {
                        using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                        {
                            soundPlayer.Play();
                            Thread.Sleep(1000);
                            this.lblMessage.Show();
                            this.lblMessage.Text = "No Data to Print";
                            this.txtVehicle.Text = string.Empty;
                            this.txtVehicle.Enabled = true;
                        }
                    }
                    else if (!this.txtVehicle.Enabled)
                    {
                        if (this.txtSelect.Enabled)
                        {
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "No Data to Print";
                                this.txtSelect.Text = string.Empty;
                                this.txtSelect.Enabled = true;
                            }
                        }
                        else if (!this.txtSelect.Enabled)
                        {
                            DataTable detailCustomer = Executing.Instance.getDetailCustomer(this.txtCustomer.Text.Trim(), this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim());
                            if (detailCustomer.Rows.Count > 0)
                            {
                                var list = Enumerable.ToList(Enumerable.Select(DataTableExtensions.AsEnumerable(detailCustomer), (DataRow _list) => new
                                {
                                    create_date = _list["create_date"],
                                    empty_or_full = _list["empty_or_full"].ToString(),
                                    doc_no = _list["doc_no"].ToString(),
                                    serial_number = _list["serial_number"].ToString(),
                                    vehicle = _list["vehicle"].ToString()
                                }));
                                string text = string.Empty;
                                string _sub1 = string.Empty;
                                string _sub2 = string.Empty;
                                var list2 = Enumerable.ToList(Enumerable.Distinct(Enumerable.Select(Enumerable.Where(list, rows => rows.empty_or_full != null), rows => new
                                {
                                    rows.empty_or_full
                                })));
                                using (var enumerator = Enumerable.Distinct(list2).GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        var current = enumerator.Current;
                                        if (!string.IsNullOrEmpty(current.empty_or_full))
                                        {
                                            text += current.empty_or_full;
                                        }
                                    }
                                }
                                if (text.Length == 2)
                                {
                                    _sub1 = text.Substring(0, 1);
                                    if (_sub1.Trim() == "0")
                                    {
                                        _sub1 = "0";
                                    }
                                    else if (_sub1.Trim() == "9")
                                    {
                                        _sub1 = "0";
                                    }
                                    _sub2 = text.Substring(1, 1);
                                    if (_sub2.Trim() == "0")
                                    {
                                        _sub2 = "9";
                                    }
                                    else if (_sub1.Trim() == "9")
                                    {
                                        _sub2 = "9";
                                    }
                                }
                                else if (text.Length == 1)
                                {
                                    if (text.Trim() == "0")
                                    {
                                        _sub1 = text.Trim();
                                    }
                                    else if (text.Trim() == "9")
                                    {
                                        _sub2 = text.Trim();
                                    }
                                    else
                                    {
                                        _sub1 = string.Empty;
                                        _sub2 = string.Empty;
                                    }
                                }
                                if (!string.IsNullOrEmpty(_sub1) && !string.IsNullOrEmpty(_sub2))
                                {
                                    var list3 = Enumerable.ToList(Enumerable.Distinct(Enumerable.Where(list, it => it.empty_or_full == _sub1)));
                                    var list4 = Enumerable.ToList(Enumerable.Distinct(Enumerable.Where(list, it => it.empty_or_full == _sub2)));
                                    string text2 = string.Empty;
                                    string text3 = string.Empty;
                                    for (int i = 1; i <= Enumerable.Count(list3); i++)
                                    {
                                        if (i % 2 == 1)
                                        {
                                            text2 = text2 + "          " + list3[i - 1].serial_number;
                                            for (int j = 0; j < 27 - list3[i - 1].serial_number.Length; j++)
                                            {
                                                text2 += " ";
                                            }
                                        }
                                        else
                                        {
                                            text2 = text2 + list3[i - 1].serial_number + Environment.NewLine;
                                        }
                                    }
                                    for (int i = 1; i <= Enumerable.Count(list4); i++)
                                    {
                                        if (i % 2 == 1)
                                        {
                                            text3 = text3 + "          " + list4[i - 1].serial_number;
                                            for (int j = 0; j < 27 - list4[i - 1].serial_number.Length; j++)
                                            {
                                                text3 += " ";
                                            }
                                        }
                                        else
                                        {
                                            text3 = text3 + list4[i - 1].serial_number + Environment.NewLine;
                                        }
                                    }
                                    this.PrintSlipTT(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text2, text3, Enumerable.Count(list).ToString());
                                    if (this.print_error)
                                    {
                                        this.print_complete = true;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(_sub1) && string.IsNullOrEmpty(_sub2))
                                {
                                    var list3 = Enumerable.ToList(Enumerable.Distinct(Enumerable.Where(list, it => it.empty_or_full == _sub1)));
                                    string text2 = string.Empty;
                                    for (int i = 1; i <= Enumerable.Count(list3); i++)
                                    {
                                        if (i % 2 == 1)
                                        {
                                            text2 = text2 + "          " + list3[i - 1].serial_number;
                                            for (int j = 0; j < 27 - list3[i - 1].serial_number.Length; j++)
                                            {
                                                text2 += " ";
                                            }
                                        }
                                        else
                                        {
                                            text2 = text2 + list3[i - 1].serial_number + Environment.NewLine;
                                        }
                                    }
                                    this.PrintSlipTF(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text2, Enumerable.Count(list).ToString());
                                    if (this.print_error)
                                    {
                                        this.print_complete = true;
                                    }
                                }
                                else if (string.IsNullOrEmpty(_sub1) && !string.IsNullOrEmpty(_sub2))
                                {
                                    var list4 = Enumerable.ToList(Enumerable.Distinct(Enumerable.Where(list, it => it.empty_or_full == _sub2)));
                                    string text3 = string.Empty;
                                    for (int i = 1; i <= Enumerable.Count(list4); i++)
                                    {
                                        if (i % 2 == 1)
                                        {
                                            text3 = text3 + "          " + list4[i - 1].serial_number;
                                            for (int j = 0; j < 27 - list4[i - 1].serial_number.Length; j++)
                                            {
                                                text3 += " ";
                                            }
                                        }
                                        else
                                        {
                                            text3 = text3 + list4[i - 1].serial_number + Environment.NewLine;
                                        }
                                    }
                                    this.PrintSlipFT(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text3, Enumerable.Count(list).ToString());
                                    if (this.print_error)
                                    {
                                        this.print_complete = true;
                                    }
                                }
                                else if (string.IsNullOrEmpty(_sub1) && string.IsNullOrEmpty(_sub2))
                                {
                                    string text4 = string.Empty;
                                    for (int i = 1; i <= Enumerable.Count(list); i++)
                                    {
                                        if (i % 2 == 1)
                                        {
                                            text4 = text4 + "          " + list[i - 1].serial_number;
                                            for (int j = 0; j < 27 - list[i - 1].serial_number.Length; j++)
                                            {
                                                text4 += " ";
                                            }
                                        }
                                        else
                                        {
                                            text4 = text4 + list[i - 1].serial_number + Environment.NewLine;
                                        }
                                    }
                                    this.PrintSlipFF(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.txtDoc.Text.Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text4, Enumerable.Count(list).ToString());
                                    if (this.print_error)
                                    {
                                        this.print_complete = true;
                                    }
                                }
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
                    DataTable dtUpload = Executing.Instance.getDataDistributionRet();
                    countAll = dtUpload.Rows.Count;
                    if (!dtUpload.IsNullOrNoRows())
                    {
                        if (MessageBox.Show("Do you want upload data to server yes or no" + string.Empty + " ", "Question",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            foreach (DataRow item in dtUpload.Rows)
                            {
                                if (iConfig.WS.DoInsUpdDistributionRet(string.IsNullOrEmpty(item["serial_number"].ToString()) ? string.Empty : item["serial_number"].ToString().Trim(),
                                                                       string.IsNullOrEmpty(item["cust_id"].ToString()) ? string.Empty : item["cust_id"].ToString().Trim(),
                                                                       string.IsNullOrEmpty(item["doc_no"].ToString()) ? string.Empty : item["doc_no"].ToString().Trim(),
                                                                       string.IsNullOrEmpty(item["vehicle"].ToString()) ? string.Empty : item["vehicle"].ToString().Trim(),
                                                                       string.IsNullOrEmpty(item["empty_or_full"].ToString()) ? string.Empty : item["empty_or_full"].ToString().Trim(),
                                                                       string.IsNullOrEmpty(item["create_date"].ToString()) ? string.Empty : item["create_date"].ToString().Trim()) == "0")
                                {
                                    insertRow++;
                                }
                            }

                            if (countAll == insertRow)
                            {
                                #region SUCCESS

                                if (Executing.Instance.DeleteDistributionRetAfterUpload())
                                {
                                    // Delete success.
                                    this.lblMessage.Hide();
                                    this._Action = Action.New;
                                    this.lblCountByCustomer.Text = Executing.Instance.CountDistributionRetALL();

                                    this.txtCustomer.Text = string.Empty;
                                    this.txtCustomer.Enabled = true;
                                    this.lblCustName.Text = string.Empty;
                                    this.txtDoc.Text = string.Empty;
                                    this.txtDoc.Enabled = true;
                                    this.txtVehicle.Text = string.Empty;
                                    this.txtVehicle.Enabled = true;
                                    this.txtSelect.Text = string.Empty;
                                    this.txtSelect.Enabled = true;
                                    this.txtInputText.Text = string.Empty;
                                    this.txtInputText.Enabled = true;
                                    this.lblResult.Text = "RecT : ";
                                    this.txtCustomer.Focus();
                                    this.txtCustomer.SelectAll();
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
                        Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmDistributionRet", "btnUploadData_Click");
                    }
                }

                // Write log.
                if (WebEx.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)WebEx.Response).StatusCode.ToString().Trim() == "NotFound")
                    {
                        Executing.Instance.Insert_Log("(404) " + ((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmDistributionRet", "btnUploadData_Click");
                    }
                    else
                    {
                        if (((HttpWebResponse)WebEx.Response) != null)
                            Executing.Instance.Insert_Log(((HttpWebResponse)WebEx.Response).StatusCode.ToString(), WebEx.Message.ToString(), "frmDistributionRet", "btnUploadData_Click");
                        else
                            Executing.Instance.Insert_Log("WebResponse NULL", "Click OFFLINE mainmenu : " + WebEx.Message.ToString(), "frmDistributionRet", "btnUploadData_Click");
                    }
                }
                else if (WebEx.Status.ToString().Trim() == "ConnectFailure")
                {
                    Executing.Instance.Insert_Log("PDA Mode OFFLINE", WebEx.Message.ToString(), "frmDistributionRet", "btnUploadData_Click");
                }
            }
            catch (Exception ex)
            {
                ClassMsg.DialogWarning("This mode offline. Can't upload data. Please connect WiFi : " + ex.Message.ToString());
                Executing.Instance.Insert_Log("CatchException", ex.Message.ToString(), "frmDistributionRet", "btnUploadData_Click");
            }
            finally
            {
                UICursor.CursorStop();
            }
        }

        #endregion
    }
}