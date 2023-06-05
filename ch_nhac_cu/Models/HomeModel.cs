using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ch_nhac_cu.Models
{
    public class HomeModel
    {
        public IEnumerable<NhacCu> NhacCus { get; set; }
        public IEnumerable<LoaiNhacCu> LoaiNhacCus { get; set; }
        public IEnumerable<CTHoaDon> CTHoaDons { get; set; }
    }
}