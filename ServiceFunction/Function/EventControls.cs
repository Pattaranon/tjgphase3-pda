using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization;
using System.Data;
using System.IO;

namespace ServiceFunction.Function
{
    public static class EventControls
    {
        #region Event TextBox

        public static void txtNumber_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.' || e.KeyChar == ControlKeysChars.Back))
            {
                e.Handled = true;
            }
        }

        public static void txtNumberDat_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '-' || e.KeyChar == ControlKeysChars.Back))
            {
                e.Handled = true;
            }
        }

        public static void txtNumInt_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == ControlKeysChars.Back))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region DataTable

        public static bool IsNullOrNoRows(this DataTable dt)
        {
            bool result = true;

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    result = false;
                }
            }
            return result;
        }

        public static bool HasData(this DataSet instance)
        {
            if (instance == null)
                return false;

            foreach (DataTable table in instance.Tables)
            {
                if (table.Rows.Count > 0)
                    return true;
            }

            return false;
        }
        public static byte[] ConvertToByteArray(this DataSet instance)
        {
            if (instance == null)
                return null;

            byte[] dataBytes = null;
            long formatFlag = 0;    //no binary data
            long datasetPartLength = 0;

            using (MemoryStream ms = new MemoryStream())
            {
                instance.WriteXml(ms, XmlWriteMode.WriteSchema);

                byte[] datasetBytes = ms.ToArray();
                datasetPartLength = datasetBytes.Length;

                dataBytes = new byte[8 + datasetPartLength];

                Buffer.BlockCopy(BitConverter.GetBytes(formatFlag), 0, dataBytes, 0, 8);
                Buffer.BlockCopy(datasetBytes, 0, dataBytes, 8, (int)datasetPartLength);
            }

            return dataBytes;
        }

        #endregion

        #region CompressByDotNetZip

        public static byte[] CompressByDotNetZip(this byte[] instance, string password)
        {
            if (instance == null)
                return null;

            return DotNetZipWrapper.Compress(instance, password);
        }

        #endregion
    }
}
