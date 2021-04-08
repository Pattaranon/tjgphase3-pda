using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace EntitiesTemp.OtherScan
{
    public class OtherScanEntity
    {
        //public string guid { get; set; }
        public string scan_number { get; set; }
        public string item_code { get; set; }
        public string notes { get; set; }
        public string create_by { get; set; }
        public DateTime create_date { get; set; }
    }
}
