using System.Linq;
using System.Web.Mvc;
using CinemaBooking.Models;
using System.Data.Entity;

namespace CinemaBooking.Controllers
{
    public class HomeController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        public ActionResult Index()
        {
            // Lấy toàn bộ danh sách phim đưa ra trang chủ cho Khách hàng xem
            var danhSachPhim = db.Phims.ToList();
            return View(danhSachPhim);
        }

        public ActionResult ChiTiet(int maPhim)
        {
            // Lấy thông tin phim kèm theo toàn bộ Suất chiếu của phim đó
            var phim = db.Phims.Include(p => p.SuatChieus).FirstOrDefault(p => p.MaPhim == maPhim);
            if (phim == null) return HttpNotFound();

            return View(phim);
        }
    }
}