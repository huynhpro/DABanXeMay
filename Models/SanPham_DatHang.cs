//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopXeMay.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham_DatHang
    {
        public long idDonHang { get; set; }
        public int idSanPham { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public double GiaBan { get; set; }
    
        public virtual DonHang DonHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
