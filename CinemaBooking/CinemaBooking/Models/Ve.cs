using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBooking.Models
{
    public class Ve
    {
        [Key]
        public int MaVe { get; set; }
        public int MaND { get; set; }
        [ForeignKey("MaND")]
        public virtual NguoiDung NguoiDung { get; set; }
        public int MaSuatChieu { get; set; }
        [ForeignKey("MaSuatChieu")]
        public virtual SuatChieu SuatChieu { get; set; }
        public int MaGhe { get; set; }
        [ForeignKey("MaGhe")]
        public virtual GheNgoi GheNgoi { get; set; }
        public DateTime NgayDat { get; set; }
        public decimal TongTien { get; set; }
    }
}