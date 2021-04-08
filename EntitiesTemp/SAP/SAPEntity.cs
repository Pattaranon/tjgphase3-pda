using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace EntitiesTemp.SAP
{
    public class SAPEntity
    {
        public string Posting_Date { get; set; }
        public string Transaction_Type { get; set; }
        public string Delivery_No { get; set; }
        public string Invoice_No { get; set; }
        public string Customer { get; set; }
        public string Customer_Name { get; set; }
        public string Material { get; set; }
        public string Material_Description { get; set; }
        public string Quantity { get; set; }
        public string Serial_Number { get; set; }
        public string Batch { get; set; }
        public string Plant { get; set; }
        public string Plant_Name { get; set; }
        public string Car_ID { get; set; }
        public string Customer_Purchase_Order_Number { get; set; }
    }
}
