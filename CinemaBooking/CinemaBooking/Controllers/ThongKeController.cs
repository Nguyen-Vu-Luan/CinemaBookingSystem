
using System.Linq;
using System.Web.Mvc;
using CinemaBooking.Models;

namespace CinemaBooking.Controllers
{
    public class ThongKeController : Controller
    {

        private CinemaDbContext db = new CinemaDbContext();
        // GET: ThongKe
        public ActionResult Index()
        {
            // Truy vấn doanh thu theo phim bằng LINQ
            var data = db.Ves
                         .GroupBy(v => v.SuatChieu.Phim.TenPhim)
                         .Select(g => new {
                             TenPhim = g.Key,
                             DoanhThu = g.Sum(v => v.TongTien)
                         })
                         .OrderByDescending(x => x.DoanhThu)
                         .Take(5) // Lấy top 5 phim
                         .ToList();

            // Chuyển dữ liệu sang dạng mảng để JS dễ dùng
            ViewBag.Labels = Newtonsoft.Json.JsonConvert.SerializeObject(data.Select(x => x.TenPhim).ToList());
            ViewBag.Data = Newtonsoft.Json.JsonConvert.SerializeObject(data.Select(x => x.DoanhThu).ToList());

            return View();
        }
    }
}