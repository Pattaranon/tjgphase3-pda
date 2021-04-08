using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Globalization;
using System.Data.Common;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;

namespace ServiceFunction.Function.Extension
{
    public static class StreamExtensions
    {
        public static byte[] ToBytes(this Stream instance, long dataLength)
        {
            if (instance == null)
                return null;

            byte[] gzipCompressedBytes = null;

            int bufferSize = 4096;
            long index = 0;

            byte[] bufferBytes = new byte[bufferSize];

            using (MemoryStream ms = new MemoryStream())
            {
            
                BinaryReader reader = new BinaryReader(instance);

                while (index < dataLength)
                {
                    bufferBytes = reader.ReadBytes(bufferSize);

                    ms.Write(bufferBytes, 0, bufferBytes.Length);

                    index += bufferSize;
                }

                reader.Close();                    
                

                gzipCompressedBytes = ms.ToArray();

                ms.Close();
            }


            instance.Close();
            instance.Dispose();


            return gzipCompressedBytes;
        }
    }

    public static class ADONetClassesExtensions
    {
        public static T GetValue<T>(this DbDataReader instance, string fieldName)
        {
            object o = instance[fieldName];
            if (o == DBNull.Value || o == null)
                return default(T);
            else
                return (T)o;
        }

        public static T GetValue<T>(this DbDataReader instance, string fieldName, T valueIfDbNull)
        {
            object o = instance[fieldName];
            if (o == DBNull.Value || o == null)
                return valueIfDbNull;
            else
                return (T)o;
        }

        public static T GetValue<T>(this DbDataReader instance, string fieldName, T valueIfDbNull, Func<object, T> converter)
        {
            object o = instance[fieldName];
            if (o == DBNull.Value || o == null)
                return valueIfDbNull;
            else
                return converter(o);
        }

        public static TReturn GetValue<TData, TReturn>(this DbDataReader instance, string columnName, TData valueIfDbNull, Func<TData, TReturn> converter)
        {
            object o = instance[columnName];
            TData data = default(TData);

            if (o == DBNull.Value || o == null)
                data = valueIfDbNull;
            else
                data = (TData)o;

            return converter(data);
        }

        public static IEnumerable<T> GetValues<T>(this DbDataReader instance, string[] columnNames)
        {
            foreach (string c in columnNames)
            {
                object o = instance[c];
                if (o != DBNull.Value && o != null)
                    yield return (T)o;
            }
        }

        public static IEnumerable<T> GetValues<T>(this DbDataReader instance, string[] columnNames, Func<T, bool> cond)
        {
            foreach (string c in columnNames)
            {
                object o = instance[c];
                if (o != DBNull.Value 
                    && o != null
                    && cond((T)o))
                {
                    yield return (T)o;
                }
            }
        }

        public static T GetValue<T>(this DataRow instance, string columnName)
        {
            object o = instance[columnName];
            if (o == DBNull.Value || o == null)
                return default(T);
            else
                return (T)o;
        }

        public static T GetValue<T>(this DataRow instance, string columnName, T valueIfDbNull)
        {
            object o = instance[columnName];
            if (o == DBNull.Value || o == null)
                return valueIfDbNull;
            else
                return (T)o;
        }

        public static TReturn GetValue<TData, TReturn>(this DataRow instance, string columnName, TData valueIfDbNull, Func<TData, TReturn> converter)
        {
            object o = instance[columnName];
            TData data = default(TData);

            if (o == DBNull.Value || o == null)
                data = valueIfDbNull;
            else
                data = (TData)o;

            return converter(data);
        }

        public static T GetValue<T>(this DataRow instance, string columnName, T valueIfDbNull, Func<object, T> converter)
        {
            object o = instance[columnName];
            if (o == DBNull.Value || o == null)
                return valueIfDbNull;
            else
                return converter(o);
        }

        public static IEnumerable<T> GetValues<T>(this DataRow instance, string[] columnNames)
        {
            foreach (string c in columnNames)
            {
                object o = instance[c];
                if (o != DBNull.Value)
                    yield return (T)o;
            }
        }

        public static IEnumerable<T> GetValues<T>(this DataRow instance, string[] columnNames, Func<T, bool> cond)
        {
            foreach (string c in columnNames)
            {
                object o = instance[c];
                if (o != DBNull.Value
                    && o != null 
                    && cond((T)o))
                {
                    yield return (T)o;
                }
            }
        }
    }

    public static class ArrayAndListExtensions
    {

        public static TValue GetValueByKey<TKey, TValue>(this KeyValuePair<TKey, TValue>[] instance, TKey key) where TKey : IComparable<TKey>
        {
            var result = (from item in instance
                          where item.Key.Equals(key)
                          select item.Value).FirstOrDefault();

            return result;
        }

        public static void SetItem<TKey, TValue>(this KeyValuePair<TKey, TValue>[] instance, int index, TKey key, TValue value)
        {
            instance[index] = new KeyValuePair<TKey, TValue>(key, value);
        }


        public static TValue GetValueByKey<TKey, TValue>(this List<KeyValuePair<TKey, TValue>> instance, TKey key) where TKey : IComparable<TKey>
        {
            var result = (from item in instance
                          where item.Key.Equals(key)
                          select item.Value).FirstOrDefault();

            return result;
        }

        public static void SetItem<TKey, TValue>(this List<KeyValuePair<TKey, TValue>> instance, TKey key, TValue value)
        {
            instance.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

    }

    public static class DataSetExtensions
    {
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

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    using (XmlTextWriter w = new XmlTextWriter(ms, Encoding.UTF8))
            //    {
            //        instance.WriteXml(w, XmlWriteMode.WriteSchema);
            //    }

            //    byte[] datasetBytes = ms.ToArray();
            //    datasetPartLength = datasetBytes.Length;

            //    dataBytes = new byte[8 + datasetPartLength];

            //    Buffer.BlockCopy(BitConverter.GetBytes(formatFlag), 0, dataBytes, 0, 8);
            //    Buffer.BlockCopy(datasetBytes, 0, dataBytes, 8, (int)datasetPartLength);
            //}
            
            return dataBytes;
        }
    }

    public static class DataTableExtensions
    {
        public static void LoadFromReader(this DataTable instance, DbDataReader reader)
        {
            int fieldsCount = 0;
            
            //create columns
            fieldsCount = reader.FieldCount;

            for (int i = 0; i < fieldsCount; i++)
            {
                instance.Columns.Add(new DataColumn(reader.GetName(i), reader.GetFieldType(i)));
            }      

            while (reader.Read())
            {                

                DataRow row = instance.NewRow();

                for (int i = 0; i < fieldsCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }

                instance.Rows.Add(row);
            }

            reader.Close();
        }
    }

    public static class DateTimeExtensions
    {
        private static CultureInfo thCulture = new CultureInfo("th-TH");
        private static CultureInfo enCulture = new CultureInfo("en-US");

        public static string ToThString(this DateTime instance, string format)
        {
            return instance.ToString(format, thCulture);
        }

        public static string ToEnString(this DateTime instance, string format)
        {
            return instance.ToString(format, enCulture);
        }

        public static string ToThString(this DateTime? instance, string format)
        {
            if (instance.HasValue)
                return instance.Value.ToString(format, thCulture);
            else
                return "";
        }

        public static string ToEnString(this DateTime? instance, string format)
        {
            if (instance.HasValue)
                return instance.Value.ToString(format, enCulture);
            else
                return "";
        }

        public static DateTime StartOfDay(this DateTime instance)
        {
            return new DateTime(instance.Year, instance.Month, instance.Day, 0, 0, 0);
        }

        public static DateTime EndOfDay(this DateTime instance)
        {
            return new DateTime(instance.Year, instance.Month, instance.Day, 23, 59, 59);
        }
    }

    public static class ExceptionExtensions
    {
        public static string GetShortDescription(this Exception instance)
        {
            return string.Format(
                @"{0} : {1}", instance.GetType().ToString(), instance.Message);

        }
    }

    public static class LINQToXmlExtensions
    {
        public static string GetStringValue(this XAttribute instance, string valueIfNotExist)
        {
            if (instance == null)
                return valueIfNotExist;
            else
                return instance.Value;
        }

        public static int GetIntValue(this XAttribute instance, int valueIfNotExist)
        {
            if (instance == null)
                return valueIfNotExist;
            else
                return Convert.ToInt32(instance.Value);
        }
    }

    public static class StringExtensions
    {
        public static string AutoTrim(this string instance)
        {
            if (instance == null)
                return null;
            else
                return instance.Trim();
            
        }

        public static string ToSafeSearchString(this string instance)
        {
            if (instance == null)
                return null;
            else
                return instance.Replace("'", "''");
        }

        public static bool IsNullOrBlank(this string instance)
        {
            return (instance == null || instance.Trim().Length == 0);
        }

        public static bool IsNull(this string instance)
        {
            return (instance == null);
        }

        public static int ToInt(this string instance)
        {
            try
            {
                return Convert.ToInt32(instance);
            }
            catch
            {
                return 0;
            }
        }

        public static bool IsNull(this decimal? instance)
        {
            return (instance == null);
        }

        public static bool IsNumericDecimal(this char instance)
        {
            ////เดิมเป็นแบบนี้ 20100404
            //return !char.IsDigit(instance) && instance != '\b' && instance != '.' && instance != ',';

            //ใหม่ 20100405
            return !char.IsDigit(instance) && instance != '\b' && instance != '.';
        }

        public static bool IsNumericInt(this char instance)
        {
            return !char.IsDigit(instance) && instance != '\b';
        }

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

        public static string ToYandNFlag(this bool b)
        {
            if (b)
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }

        public static string ToNandYFlag(this bool b)
        {
            if (b)
            {
                return "N";
            }
            else
            {
                return "Y";
            }
        }

        public static string ToYandNull(this bool b)
        {
            if (b)
            {
                return "Y";
            }
            else
            {
                return null;
            }
        }

        public static string To0Or1Flag(this bool b)
        {
            if (b)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
    }
    public enum HashAlgorithm
    {
        SHA256
    }
}