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
    
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            this.SanPham_DatHang = new HashSet<SanPham_DatHang>();
        }
    
        public long ID_DonHang { get; set; }
        public Nullable<int> idTaiKhoan { get; set; }
        public Nullable<bool> DaThanhToan { get; set; }
        public Nullable<int> TrangThaiGiaoHang { get; set; }
        public Nullable<System.DateTime> NgayDat { get; set; }
        public Nullable<System.DateTime> NgayGiao { get; set; }
        public string DiaChiGiao { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public Nullable<int> idHinhThucThanhToan { get; set; }
    
        public virtual GiaoHang GiaoHang { get; set; }
        public virtual HinhThucThanhToan HinhThucThanhToan { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_DatHang> SanPham_DatHang { get; set; }
    }
}