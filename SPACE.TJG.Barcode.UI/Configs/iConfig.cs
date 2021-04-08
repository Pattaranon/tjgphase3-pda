using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace SPACE.TJG.Barcode.UI.Configs
{
    public static class iConfig
    {
        #region Set Defult IPAddress

        public static int _UserID = 0;
        private static string _IPAddress = "192.168.1.34";
        public static string FILE_CONFIG_PATH = System.IO.Path.GetDirectoryName
            (System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "/config.txt";

        #endregion

        #region Call Object WebService

        public static System.Drawing.Color Color;

        public static int M_GroupID = 0;
        private static UserInterfaces.wsTJG.Service _WS =
            new UserInterfaces.wsTJG.Service();

        public static decimal _ReceiveID = 0;
        public static string _InvoiceID;

        #endregion

        #region URL WebService

        public static string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
        public static UserInterfaces.wsTJG.Service WS
        {
            get
            {
                _WS.Url = "http://" + _IPAddress + "/TJGService/Service.svc";
                //-- TimeOut Add 2011-01-25 --
                _WS.Timeout = 60000;
                return _WS;
            }
        }

        #endregion

        #region ReadFile And SaveFile

        public static string ReadConfig()
        {
            try
            {
                if (!System.IO.File.Exists(FILE_CONFIG_PATH))
                {
                    SaveConfig(_IPAddress);
                }
                string result;
                System.IO.StreamReader sr = new StreamReader(FILE_CONFIG_PATH);
                result = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SaveConfig(string ip)
        {
            try
            {
                System.IO.StreamWriter sw = new StreamWriter(FILE_CONFIG_PATH);
                sw.Write(ip);
                sw.Flush();
                sw.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}