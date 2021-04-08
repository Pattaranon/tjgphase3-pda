using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace EntitiesTemp.DistributionRet
{
    public class DistributionRetEntity
    {
        public string serial_number { get; set; }
        public string cust_id { get; set; }
        public string doc_no { get; set; }
        public string vehicle { get; set; }
        public string empty_or_full { get; set; }
        public DateTime create_date { get; set; }
    }
}
