﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ch_nhac_cu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class NhaCungCap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaCungCap()
        {
            this.NhacCus = new HashSet<NhacCu>();
        }
        [DisplayName("Mã nhà cung cấp")]
        public string MaNCC { get; set; }
        [DisplayName("Tên nhà cung cấp")]
        public string TenNCC { get; set; }
        [DisplayName("Số điện thoại")]
        public string SDT { get; set; }
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhacCu> NhacCus { get; set; }
    }
}
