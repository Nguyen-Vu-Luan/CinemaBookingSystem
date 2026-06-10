using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models
{
    public class TheLoai
    {
        [Key]
        public int MaTheLoai { get; set; }
        public string TenTheLoai { get; set; }
        public virtual ICollection<Phim> Phims { get; set; }
    }
}