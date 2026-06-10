using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBooking.Models
{
    public class Phim
    {
        [Key]
        public int MaPhim { get; set; }
        public string TenPhim { get; set; }
        public string HinhAnh { get; set; }
        public string DaoDien { get; set; }
        public int ThoiLuong { get; set; }

        public int MaTheLoai { get; set; }
        [ForeignKey("MaTheLoai")]
        public virtual TheLoai TheLoai { get; set; }

        public virtual ICollection<SuatChieu> SuatChieus { get; set; }
    }
}