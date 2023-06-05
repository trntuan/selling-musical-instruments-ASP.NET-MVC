using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ch_nhac_cu.Models
{
    public class NhacCuKhachHangModel
    {
        public IEnumerable<NhacCu> NhacCus { get; set; }
        public IEnumerable<KhachHang> KhachHangs { get; set; }
      
    }
}