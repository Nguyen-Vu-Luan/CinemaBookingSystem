using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models
{
    public class NguoiDung
    {
        [Key]
        public int MaND { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string VaiTro { get; set; } // "Admin" hoặc "KhachHang"
    }
}