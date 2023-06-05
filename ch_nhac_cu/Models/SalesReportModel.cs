using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ch_nhac_cu.Models
{
    public class SalesReportModel
    {
        public string TenNC { get; set; }
        public string MaNC { get; set; }
        public string TenNCC { get; set; }
        public string TenLoaiNC { get; set; }
        public string TenHSX { get; set; }
        public int DonGia { get; set; }
        public int TON { get; set; }
     
        public int SoLuongBan { get; set; }
        public int Gia { get; set; }
    }
}