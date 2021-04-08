using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace ObjectManager.ConvertObject
{
    public class ConvertObject
    {
        public static DataTable ToTable<T>(IEnumerable<T> data)
        {
            var table = new System.Data.DataTable();

            try
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

                object[] values = new object[props.Count];

                foreach (T item in data)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item) ?? DBNull.Value;
                    }
                    table.Rows.Add(values);
                }
            }
            catch
            {
                throw;
            }

            return table;
        }
    }
}
