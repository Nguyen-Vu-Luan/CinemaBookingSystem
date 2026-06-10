using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBooking.Models
{
    public class SuatChieu
    {
        [Key]
        public int MaSuatChieu { get; set; }
        public int MaPhim { get; set; }
        [ForeignKey("MaPhim")]
        public virtual Phim Phim { get; set; }
        public DateTime ThoiGianChieu { get; set; }
        public string PhongChieu { get; set; }
        public decimal GiaVe { get; set; }
        public virtual ICollection<GheNgoi> GheNgois { get; set; }
    }
}