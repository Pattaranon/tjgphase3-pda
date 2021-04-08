using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace EntitiesTemp.Delivery
{
    public class DeliveryEntity
    {
        public string factory { get; set; }
        public string cust_id { get; set; }
        public string vehicle { get; set; }
        public string item_code { get; set; }
        public string empty_or_full { get; set; }
        public string cylinder_sn { get; set; }
        public string create_by { get; set; }
        public DateTime create_date { get; set; }
    }
}
