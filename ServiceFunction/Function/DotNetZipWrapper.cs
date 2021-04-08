using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.IO;
using System.Data;

namespace ServiceFunction.Function
{
    public static class DotNetZipWrapper
    {
        public static byte[] Compress(Stream inputStream)
        {
            byte[] compressedBytes = null;

            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddEntry(Guid.NewGuid().ToString(), inputStream);
                    zip.Save(zipStream);
                }

                compressedBytes = zipStream.ToArray();

                zipStream.Close();
            }

            return compressedBytes;
        }
        public static byte[] Compress(Stream inputStream, string password)
        {
            byte[] compressedBytes = null;

            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {                   
                    zip.Password = password;
                    zip.AddEntry(Guid.NewGuid().ToString(), inputStream);
                    zip.Save(zipStream);
                }

                compressedBytes = zipStream.ToArray();

                zipStream.Close();
            }

            return compressedBytes;
        }
        public static byte[] Compress(byte[] inputBytes)
        {
            byte[] compressedBytes = null;

            using(MemoryStream zipStream = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddEntry(Guid.NewGuid().ToString(), inputBytes);
                    zip.Save(zipStream);
                }

                compressedBytes = zipStream.ToArray();

                zipStream.Close();
            }

            return compressedBytes;
        }
        public static byte[] Compress(byte[] inputBytes, string password)
        {
            byte[] compressedBytes = null;

            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = password;
                    zip.AddEntry(Guid.NewGuid().ToString(), inputBytes);
                    zip.Save(zipStream);
                }

                compressedBytes = zipStream.ToArray();

                zipStream.Close();
            }

            return compressedBytes;
        }

        public static byte[] DeCompress(byte[] inputBytes)
        {
            byte[] decompressedBytes = null;

            using(MemoryStream zipStream = new MemoryStream())
            {
                using (ZipFile zip = ZipFile.Read(inputBytes))
                {
                    zip.Entries.First().Extract(zipStream);
                }

                decompressedBytes = zipStream.ToArray();

                zipStream.Close();
            }

            return decompressedBytes;
        }
        public static byte[] DeCompress(byte[] inputBytes, string password)
        {
            byte[] decompressedBytes = null;

            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipFile zip = ZipFile.Read(inputBytes))
                {
                    zip.Password = password;
                    zip.Entries.First().Extract(zipStream);
                }

                decompressedBytes = zipStream.ToArray();

                zipStream.Close();
            }

            return decompressedBytes;
        }

        public static byte[] CompressByDotNetZip(this Stream instance)
        {
            if (instance == null)
                return null;

            return DotNetZipWrapper.Compress(instance);
        }
        public static byte[] CompressByDotNetZip(this Stream instance, string password)
        {
            if (instance == null)
                return null;

            return DotNetZipWrapper.Compress(instance, password);
        }
        public static byte[] CompressByDotNetZip(this byte[] instance)
        {
            if (instance == null)
                return null;

            return DotNetZipWrapper.Compress(instance);
        }
        public static byte[] CompressByDotNetZip(this byte[] instance, string password)
        {
            if (instance == null)
                return null;

            return DotNetZipWrapper.Compress(instance, password);
        }

        public static byte[] DeCompressByDotNetZip(this byte[] instance)
        {
            if (instance == null)
                return null;

            return DotNetZipWrapper.DeCompress(instance);
        }
        public static byte[] DeCompressByDotNetZip(this byte[] instance, string password)
        {
            if (instance == null)
                return null;

            return DotNetZipWrapper.DeCompress(instance, password);
        }

        public static byte[] CompressItems(Dictionary<string,byte[]> dataItems)
        {
            byte[] compressedBytes = null;

            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    foreach (var item in dataItems)
                    {
                        zip.AddEntry(item.Key, item.Value);
                    }

                    zip.Save(zipStream);
                }

                compressedBytes = zipStream.ToArray();

                zipStream.Close();
            }

            return compressedBytes;
        }

        public static Dictionary<string, byte[]> DecompressItems(byte[] compressBytes)
        {
            Dictionary<string, byte[]> dataItems = new Dictionary<string, byte[]>();

            using (ZipFile zip = ZipFile.Read(compressBytes))
            {
                foreach (ZipEntry entry in zip.Entries)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        entry.Extract(ms);

                        dataItems.Add(entry.FileName, ms.ToArray());

                        ms.Close();
                    }
                }
            }

            return dataItems;
        }
    }
}