using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EntitiesTemp.Factory;
using Utilities.ComboBoxList;
using ServiceFunction.Function;
using System.Media;
using System.Threading;
using SPACE.TJG.Barcode.UI.Execute;
using EntitiesTemp.Delivery;
using PrintCENET;

namespace SPACE.TJG.Barcode.UI.Pages.Screens
{
    public partial class frmDelivery : Form
    {
        #region Member

        private int mCurLang = 0;
        private string pathSoundAlert1 = "\\My Documents\\Sound\\beep-04.wav";
        private string pathSoundAlert2 = "\\My Documents\\Sound\\beep-05.wav";
        public bool status_del { get; set; }
        public bool print_complete { get; set; }
        public bool print_error { get; set; }

        #endregion

        #region Constrcutor

        public frmDelivery()
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
            Edit_Cust,
            Edit_Vehicle,
            Edit_Select
        }

        #endregion

        #region Binding

        private void getFactoryToComboBox()
        {
            DataTable _listFactory = new DataTable();
            _listFactory = Execute.Executing.Instance.getFactory();
            if (!_listFactory.IsNullOrNoRows())
            {
                ComboBoxBinding.BindDataTableToCombobox(_listFactory, this.cbbFactory, "factory_display", "factory_value", "-Select Factory-");
            }
            else
            {
                this.cbbFactory.DataSource = null;
            }
        }

        #endregion

        #region Method

        private bool DoSave()
        {
            bool result;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DeliveryEntity _deliveryEntity = new DeliveryEntity();
                _deliveryEntity.factory = this.cbbFactory.SelectedValue.ToString();
                _deliveryEntity.cust_id = this.txtCustomer.Text.Trim();
                _deliveryEntity.vehicle = this.txtVehicle.Text.Trim();
                _deliveryEntity.item_code = this.txtInputText.Text.Trim().Substring(0, 5);
                _deliveryEntity.empty_or_full = this.txtSelect.Text.Trim();
                _deliveryEntity.cylinder_sn = this.txtInputText.Text.Trim();
                _deliveryEntity.create_by = "USER_DELIVERY";
                _deliveryEntity.create_date = Executing.Instance.GetDateServer();
                bool flag = Executing.Instance.SaveDelivery(_deliveryEntity);
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
            bool _result = false;
            if (this.cbbFactory.SelectedIndex == 0)
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "Please key factory";
                    _result = false;
                }
            }
            if (string.IsNullOrEmpty(this.txtCustomer.Text.Trim()))
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                {
                    soundPlayer.Play();
                    Thread.Sleep(1000);
                    this.lblMessage.Show();
                    this.lblMessage.Text = "Please key customer";
                    _result = false;
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
                    _result = false;
                }
            }
            else if (!string.IsNullOrEmpty(this.txtCustomer.Text.Trim()))
            {
                #region Customer profile

                string _error_code = string.Empty;
                string _customer_name = string.Empty;
                
                //Check Cust Code
                if (Executing.Instance.checkCustomerCode(this.txtCustomer.Text.Trim(), ref _error_code, ref _customer_name))
                {
                    //Has
                    _result = true;
                }
                else if (_error_code.Trim() == "002")
                {
                    #region Customer not Found

                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert1))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);
                        this.txtCustomer.Text = string.Empty;
                        this.txtCustomer.Enabled = true;
                        this.lblMessage.Show();
                        this.lblMessage.Text = "Customer not Found";
                        _result = false;
                    }

                    #endregion
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
                        _result = false;
                    }
                }

                #endregion
            }
            else
            {
                _result = true;
            }

            return _result;
        }

        #endregion

        #region Method Print

        private void Print(string cust_code, string cust_name, string factory_no, string vehicle, string empty_or_full, string cylinder_sn, string count_serial)
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
						Environment.NewLine,
						"               1-5-5, E Gate, Lebuh Tunku Kudin 2",
						Environment.NewLine,
						"              11700 Gelugor, Pulau Pinang, Malaysia",
						Environment.NewLine,
						"            Tel: +60 4-659 2177 ",
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
						"          Factory Code: ",
						factory_no,
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
						cylinder_sn,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
						Environment.NewLine,
						"               1-5-5, E Gate, Lebuh Tunku Kudin 2",
						Environment.NewLine,
						"              11700 Gelugor, Pulau Pinang, Malaysia",
						Environment.NewLine,
						"            Tel: +60 4-659 2177 ",
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
						"          Factory Code: ",
						factory_no,
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
						cylinder_sn,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
						Environment.NewLine,
						"               1-5-5, E Gate, Lebuh Tunku Kudin 2",
						Environment.NewLine,
						"              11700 Gelugor, Pulau Pinang, Malaysia",
						Environment.NewLine,
						"            Tel: +60 4-659 2177 ",
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
						"          Factory Code: ",
						factory_no,
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
						cylinder_sn,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
						Environment.NewLine,
						"               1-5-5, E Gate, Lebuh Tunku Kudin 2",
						Environment.NewLine,
						"              11700 Gelugor, Pulau Pinang, Malaysia",
						Environment.NewLine,
						"            Tel: +60 4-659 2177 ",
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
						"          Factory Code: ",
						factory_no,
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
						cylinder_sn,
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
        private void PrintSlipTT(string cust_code, string cust_name, string factory_no, string vehicle, string empty_or_full1, string empty_or_full2, string serial_number_zero, string serial_number_nine, string count_serial)
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
						Environment.NewLine,
						"               1-5-5, E Gate, Lebuh Tunku Kudin 2",
						Environment.NewLine,
						"              11700 Gelugor, Pulau Pinang, Malaysia",
						Environment.NewLine,
						"            Tel: +60 4-659 2177 ",
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
						"          Factory Code: ",
						factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
						Environment.NewLine,
						"               1-5-5, E Gate, Lebuh Tunku Kudin 2",
						Environment.NewLine,
						"              11700 Gelugor, Pulau Pinang, Malaysia",
						Environment.NewLine,
						"            Tel: +60 4-659 2177 ",
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
						"          Factory Code: ",
						factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
						    Environment.NewLine,
						    "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
						    Environment.NewLine,
						    "              11700 Gelugor, Pulau Pinang, Malaysia",
						    Environment.NewLine,
						    "            Tel: +60 4-659 2177 ",
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
							"          Factory Code: ",
							factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
						    Environment.NewLine,
						    "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
						    Environment.NewLine,
						    "              11700 Gelugor, Pulau Pinang, Malaysia",
						    Environment.NewLine,
						    "            Tel: +60 4-659 2177 ",
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
							"          Factory Code: ",
							factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
						    Environment.NewLine,
						    "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
						    Environment.NewLine,
						    "              11700 Gelugor, Pulau Pinang, Malaysia",
						    Environment.NewLine,
						    "            Tel: +60 4-659 2177 ",
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
							"          Factory Code: ",
							factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
						    Environment.NewLine,
						    "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
						    Environment.NewLine,
						    "              11700 Gelugor, Pulau Pinang, Malaysia",
						    Environment.NewLine,
						    "            Tel: +60 4-659 2177 ",
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
							"          Factory Code: ",
							factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
					    Environment.NewLine,
					    "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
					    Environment.NewLine,
					    "              11700 Gelugor, Pulau Pinang, Malaysia",
					    Environment.NewLine,
					    "            Tel: +60 4-659 2177 ",
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
						"          Factory Code: ",
						factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
					    Environment.NewLine,
					    "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
					    Environment.NewLine,
					    "              11700 Gelugor, Pulau Pinang, Malaysia",
					    Environment.NewLine,
					    "            Tel: +60 4-659 2177 ",
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
						"          Factory Code: ",
						factory_no,
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
        private void PrintSlipTF(string cust_code, string cust_name, string factory_no, string vehicle, string empty_or_full1, string empty_or_full2, string serial_number_zero, string count_serial)
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
					    Environment.NewLine,
					    "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
					    Environment.NewLine,
					    "              11700 Gelugor, Pulau Pinang, Malaysia",
					    Environment.NewLine,
					    "            Tel: +60 4-659 2177 ",
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
						"          Factory Code: ",
						factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
					    Environment.NewLine,
					    "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
					    Environment.NewLine,
					    "              11700 Gelugor, Pulau Pinang, Malaysia",
					    Environment.NewLine,
					    "            Tel: +60 4-659 2177 ",
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
						"          Factory Code: ",
						factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
					        Environment.NewLine,
					        "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
					        Environment.NewLine,
					        "              11700 Gelugor, Pulau Pinang, Malaysia",
					        Environment.NewLine,
					        "            Tel: +60 4-659 2177 ",
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
						    "          Factory Code: ",
						    factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
					        Environment.NewLine,
					        "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
					        Environment.NewLine,
					        "              11700 Gelugor, Pulau Pinang, Malaysia",
					        Environment.NewLine,
					        "            Tel: +60 4-659 2177 ",
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
						    "          Factory Code: ",
						    factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
					        Environment.NewLine,
					        "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
					        Environment.NewLine,
					        "              11700 Gelugor, Pulau Pinang, Malaysia",
					        Environment.NewLine,
					        "            Tel: +60 4-659 2177 ",
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
						    "          Factory Code: ",
						    factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
					        Environment.NewLine,
					        "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
					        Environment.NewLine,
					        "              11700 Gelugor, Pulau Pinang, Malaysia",
					        Environment.NewLine,
					        "            Tel: +60 4-659 2177 ",
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
						    "          Factory Code: ",
						    factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
				        Environment.NewLine,
				        "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
				        Environment.NewLine,
				        "              11700 Gelugor, Pulau Pinang, Malaysia",
				        Environment.NewLine,
				        "            Tel: +60 4-659 2177 ",
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
					    "          Factory Code: ",
					    factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
				        Environment.NewLine,
				        "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
				        Environment.NewLine,
				        "              11700 Gelugor, Pulau Pinang, Malaysia",
				        Environment.NewLine,
				        "            Tel: +60 4-659 2177 ",
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
					    "          Factory Code: ",
					    factory_no,
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
        private void PrintSlipFT(string cust_code, string cust_name, string factory_no, string vehicle, string empty_or_full1, string empty_or_full2, string serial_number_nine, string count_serial)
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
				        Environment.NewLine,
				        "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
				        Environment.NewLine,
				        "              11700 Gelugor, Pulau Pinang, Malaysia",
				        Environment.NewLine,
				        "            Tel: +60 4-659 2177 ",
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
					    "          Factory Code: ",
					    factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
				        Environment.NewLine,
				        "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
				        Environment.NewLine,
				        "              11700 Gelugor, Pulau Pinang, Malaysia",
				        Environment.NewLine,
				        "            Tel: +60 4-659 2177 ",
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
					    "          Factory Code: ",
					    factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
				            Environment.NewLine,
				            "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
				            Environment.NewLine,
				            "              11700 Gelugor, Pulau Pinang, Malaysia",
				            Environment.NewLine,
				            "            Tel: +60 4-659 2177 ",
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
					        "          Factory Code: ",
					        factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
				            Environment.NewLine,
				            "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
				            Environment.NewLine,
				            "              11700 Gelugor, Pulau Pinang, Malaysia",
				            Environment.NewLine,
				            "            Tel: +60 4-659 2177 ",
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
					        "          Factory Code: ",
					        factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
				            Environment.NewLine,
				            "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
				            Environment.NewLine,
				            "              11700 Gelugor, Pulau Pinang, Malaysia",
				            Environment.NewLine,
				            "            Tel: +60 4-659 2177 ",
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
					        "          Factory Code: ",
					        factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
				            Environment.NewLine,
				            "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
				            Environment.NewLine,
				            "              11700 Gelugor, Pulau Pinang, Malaysia",
				            Environment.NewLine,
				            "            Tel: +60 4-659 2177 ",
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
					        "          Factory Code: ",
					        factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
			            Environment.NewLine,
			            "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
			            Environment.NewLine,
			            "              11700 Gelugor, Pulau Pinang, Malaysia",
			            Environment.NewLine,
			            "            Tel: +60 4-659 2177 ",
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
					    "          Factory Code: ",
					    factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
			            Environment.NewLine,
			            "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
			            Environment.NewLine,
			            "              11700 Gelugor, Pulau Pinang, Malaysia",
			            Environment.NewLine,
			            "            Tel: +60 4-659 2177 ",
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
					    "          Factory Code: ",
					    factory_no,
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
        private void PrintSlipFF(string cust_code, string cust_name, string factory_no, string vehicle, string empty_or_full1, string empty_or_full2, string serial_number, string count_serial)
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
			            Environment.NewLine,
			            "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
			            Environment.NewLine,
			            "              11700 Gelugor, Pulau Pinang, Malaysia",
			            Environment.NewLine,
			            "            Tel: +60 4-659 2177 ",
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
					    "          Factory Code: ",
					    factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
			            Environment.NewLine,
			            "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
			            Environment.NewLine,
			            "              11700 Gelugor, Pulau Pinang, Malaysia",
			            Environment.NewLine,
			            "            Tel: +60 4-659 2177 ",
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
					    "          Factory Code: ",
					    factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
			                Environment.NewLine,
			                "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
			                Environment.NewLine,
			                "              11700 Gelugor, Pulau Pinang, Malaysia",
			                Environment.NewLine,
			                "            Tel: +60 4-659 2177 ",
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
					        "          Factory Code: ",
					        factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
			                Environment.NewLine,
			                "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
			                Environment.NewLine,
			                "              11700 Gelugor, Pulau Pinang, Malaysia",
			                Environment.NewLine,
			                "            Tel: +60 4-659 2177 ",
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
					        "          Factory Code: ",
					        factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
			                Environment.NewLine,
			                "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
			                Environment.NewLine,
			                "              11700 Gelugor, Pulau Pinang, Malaysia",
			                Environment.NewLine,
			                "            Tel: +60 4-659 2177 ",
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
					        "          Factory Code: ",
					        factory_no,
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
                            //"                  Thai Japan Gas Co.,Ltd",
                            //Environment.NewLine,
                            //"             1/1 Rojana Industrial Park Moo5",
                            //Environment.NewLine,
                            //"            Khan Harm U-Thai Ayutthaya 13210",
                            //Environment.NewLine,
                            //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                            "                    TOMOE MALAYSIA SDN BHD",
			                Environment.NewLine,
			                "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
			                Environment.NewLine,
			                "              11700 Gelugor, Pulau Pinang, Malaysia",
			                Environment.NewLine,
			                "            Tel: +60 4-659 2177 ",
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
					        "          Factory Code: ",
					        factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
		                Environment.NewLine,
		                "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
		                Environment.NewLine,
		                "              11700 Gelugor, Pulau Pinang, Malaysia",
		                Environment.NewLine,
		                "            Tel: +60 4-659 2177 ",
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
				        "          Factory Code: ",
				        factory_no,
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
                        //"                  Thai Japan Gas Co.,Ltd",
                        //Environment.NewLine,
                        //"             1/1 Rojana Industrial Park Moo5",
                        //Environment.NewLine,
                        //"            Khan Harm U-Thai Ayutthaya 13210",
                        //Environment.NewLine,
                        //"          Tel:66(0)3533-0040-3 Fax:66(0)3533-0039",
                        "                    TOMOE MALAYSIA SDN BHD",
		                Environment.NewLine,
		                "               1-5-5, E Gate, Lebuh Tunku Kudin 2",
		                Environment.NewLine,
		                "              11700 Gelugor, Pulau Pinang, Malaysia",
		                Environment.NewLine,
		                "            Tel: +60 4-659 2177 ",
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
				        "          Factory Code: ",
				        factory_no,
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

        private void frmDelivery_Load(object sender, EventArgs e)
        {
            this.lblMessage.Hide();

            // Binding dropdownlist
            getFactoryToComboBox();

            this.txtCustomer.Focus();
            this.txtCustomer.SelectAll();

            if (this.status_del)
            {
                this.lblMessage.Show();
                this.lblMessage.Text = "Delete 1 Records";
                this.lblCountByCustomer.Text = Executing.Instance.CountDeliveryALL();
            }
            else
            {
                // On form load
                this.lblMessage.Hide();
                this._Action = Action.New;
                this.lblCountByCustomer.Text = Executing.Instance.CountDeliveryALL();
                this.txtCustomer.Focus();
                this.txtCustomer.SelectAll();
            }
        }
        private void frmDelivery_KeyUp(object sender, KeyEventArgs e)
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

        private void cbbFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbbFactory.SelectedIndex > 0)
            {
                this.cbbFactory.Enabled = false;
				if(this.txtCustomer.Text.Trim() == string.Empty)
				{
					this.txtCustomer.Enabled = true;
					this.txtCustomer.Focus();
					this.txtCustomer.SelectAll();
				}
				else
				{
					this.txtCustomer.Focus();
					this.txtCustomer.SelectAll();
				}
				
				/*
                this.txtCustomer.Text = string.Empty;

                this.txtCustomer.Enabled = true;
                this.txtCustomer.Focus();
                this.txtCustomer.SelectAll();
				*/
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

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key customer";
                    }
                }
                else
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (this.cbbFactory.SelectedIndex == 0)
                        {
                            #region Please select factory

                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Please select factory";
                            }

                            #endregion
                        }
                        else
                        {
                            #region Customer profile

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

                                this.txtVehicle.Enabled = true;
                                this.txtVehicle.Focus();
                                this.txtVehicle.SelectAll();
                            }
                            /*
                            else if (_error_code.Trim() == "001")
                            {
                                #region Format cusomer ไม่ถูกต้อง

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

                                #endregion
                            }
                            */
                            else if (_error_code.Trim() == "002")
                            {
                                #region Customer not Found

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

                                #endregion
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

                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        Executing.Instance.Insert_Log("ERRCUST_SC2", ex.Message.ToString(), "frmDelivery", "txtCustomer_KeyDown");
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }

                /*
                else if (this.txtCustomer.Text.Length != 7)
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                    {
                        soundPlayer.Play();
                        Thread.Sleep(1000);

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Format customer incorrect";
                    }
                }
                else if (this.txtCustomer.Text.Length == 7)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (this.cbbFactory.SelectedIndex == 0)
                        {
                            #region Please select factory

                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Please select factory";
                            }

                            #endregion
                        }
                        else
                        {
                            #region Customer profile

                            string _error_code = string.Empty;
                            string _customer_name = string.Empty;
                            string _sub_cust = this.txtCustomer.Text.Substring(0, 3);
                            //Check Cust Code

                            if (Executing.Instance.checkCustCode(this.txtCustomer.Text.Substring(0, 3), this.txtCustomer.Text.Trim(), ref _error_code, ref _customer_name))
                            {
                                //Has
                                this.txtCustomer.Enabled = false;
                                this.lblCustName.Text = _customer_name.Trim();

                                this.txtVehicle.Enabled = true;
                                this.txtVehicle.Focus();
                                this.txtVehicle.SelectAll();
                            }
                            else if (_error_code.Trim() == "001")
                            {
                                #region Format cusomer ไม่ถูกต้อง

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

                                #endregion
                            }
                            else if (_error_code.Trim() == "002")
                            {
                                #region Customer not Found

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

                                #endregion
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

                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        Executing.Instance.Insert_Log("ERRCUST_SC2", ex.Message.ToString(), "frmDelivery", "txtCustomer_KeyDown");
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
                */
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

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Please key vehicle";
                    }
                }
                else
                {
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

                        this.lblMessage.Show();
                        this.lblMessage.Text = "Select empty or full";
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
        private void lnkClean_Click(object sender, EventArgs e)
        {
            this.txtSelect.Enabled = true;
            this.txtSelect.Text = string.Empty;

            this.txtInputText.Enabled = true;
            this.txtInputText.Text = string.Empty;

            this.txtSelect.Focus();
            this.txtSelect.SelectAll();
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
                    try
                    {
                        if (this.txtInputText.Text.Length >= 5)
                        {
                            if (BeginSave())
                            {
                                bool flag = Executing.Instance.checkCylinderDeliveryDuplicate(this.txtInputText.Text.Trim());
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
                                    this.lblCountByCustomer.Text = Executing.Instance.DeliveryByCustomer(this.txtCustomer.Text.Trim());
                                    this.txtInputText.Text = string.Empty;
                                    this.txtInputText.Focus();
                                    this.txtInputText.SelectAll();
                                }
                            }
                        }
                        else
                        {
                            // Cylinder ไม่ถูกต้อง
                            using (SoundPlayer soundPlayer = new SoundPlayer(this.pathSoundAlert2))
                            {
                                soundPlayer.Play();
                                Thread.Sleep(1000);
                                this.lblMessage.Show();
                                this.lblMessage.Text = "Cylinder S/N format incorrect";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Executing.Instance.Insert_Log("ERRSAVE_SC2", ex.Message.ToString(), "frmDelivery", "txtInputText_KeyDown");
                    }
                }
            }
        }

        private void lnkNEW_Click(object sender, EventArgs e)
        {
            if (this._Action == Action.New)
            {
                this.cbbFactory.SelectedIndex = 0;
                this.cbbFactory.Enabled = true;
                this.txtCustomer.Text = string.Empty;
                this.txtCustomer.Enabled = true;
                this.lblCustName.Text = string.Empty;
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
                this.txtVehicle.Text = string.Empty;
                this.txtVehicle.Enabled = false;
                this.txtSelect.Text = string.Empty;
                this.txtSelect.Enabled = false;
                this.txtInputText.Text = string.Empty;
                this.txtInputText.Enabled = false;
                this.txtSelect.Focus();
                this.txtSelect.SelectAll();
            }
            else if (this._Action == Action.Edit_Vehicle)
            {
                this.txtCustomer.Text = string.Empty;
                this.txtCustomer.Enabled = true;
                this.lblCustName.Text = string.Empty;
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
                this.cbbFactory.SelectedIndex = 0;
                this.cbbFactory.Enabled = true;
                this.txtCustomer.Text = string.Empty;
                this.txtCustomer.Enabled = true;
                this.lblCustName.Text = string.Empty;
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
            if (Executing.Instance.CountDeliveryALL() == "0")
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
                frmSelectDeliveryDel frmSDeliveryDel = new frmSelectDeliveryDel();
                frmSDeliveryDel.ShowDialog();
                //this.Close();
            }
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.cbbFactory.SelectedIndex == 0)
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
                if (!this.txtVehicle.Enabled)
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
                        try
                        {
                            DataTable detailCustomer = Executing.Instance.GetCustomer(this.txtCustomer.Text.Trim(), this.cbbFactory.SelectedValue.ToString().Trim(), this.txtVehicle.Text.Trim());
                            if (detailCustomer.Rows.Count > 0)
                            {
                                var list = Enumerable.ToList(Enumerable.Select(DataTableExtensions.AsEnumerable(detailCustomer), (DataRow _list) => new
                                {
                                    create_date = _list["create_date"],
                                    empty_or_full = _list["empty_or_full"].ToString(),
                                    factory_code = _list["factory_code"].ToString(),
                                    serial_number = _list["cylinder_sn"].ToString(),
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
                                    this.PrintSlipTT(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.cbbFactory.SelectedValue.ToString().Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text2, text3, Enumerable.Count(list).ToString());
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
                                    this.PrintSlipTF(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.cbbFactory.SelectedValue.ToString().Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text2, Enumerable.Count(list).ToString());
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
                                    this.PrintSlipFT(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.cbbFactory.SelectedValue.ToString().Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text3, Enumerable.Count(list).ToString());
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
                                    this.PrintSlipFF(this.txtCustomer.Text.Trim(), this.lblCustName.Text.Trim(), this.cbbFactory.SelectedValue.ToString().Trim(), this.txtVehicle.Text.Trim(), _sub1, _sub2, text4, Enumerable.Count(list).ToString());
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
                        catch (Exception ex)
                        {
                            Executing.Instance.Insert_Log("ERRPRINT_SC2", ex.Message.ToString(), "frmDelivery", "lnkPrint_Click");
                        }
                    }
                }
            }
        }

        private void lnkExit_Click(object sender, EventArgs e)
        {
            HomeScreen.frmHomeScreen fHome = new SPACE.TJG.Barcode.UI.HomeScreen.frmHomeScreen("LIFETIME");
            fHome.Show();
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