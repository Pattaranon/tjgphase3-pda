using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace EntitiesTemp.Warehouse
{
    public class WarehouseEntity
    {
        public string sell_order_no { get; set; }
        public string cust_id { get; set; }
        public string item_code { get; set; }
        public string cylinder_sn { get; set; }
        public string create_by { get; set; }
        public DateTime create_date { get; set; }
    }
}