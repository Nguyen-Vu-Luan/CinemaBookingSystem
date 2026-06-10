using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBooking.Models
{
    public class GheNgoi
    {
        [Key]
        public int MaGhe { get; set; }
        public int MaSuatChieu { get; set; }
        [ForeignKey("MaSuatChieu")]
        public virtual SuatChieu SuatChieu { get; set; }
        public string ViTriGhe { get; set; }
        // False: Còn trống | True: Đã có người đặt
        public bool TrangThai { get; set; }
    }
}