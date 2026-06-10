using System.Linq;
using System.Web.Mvc;
using CinemaBooking.Models;

namespace CinemaBooking.Controllers
{
    public class NguoidungController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // 1. GET: Hiển thị form Đăng Ký
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        // 1. POST: Xử lý Đăng Ký
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(NguoiDung nguoiDung)
        {
            // Bỏ qua kiểm tra cột VaiTro vì trên form không có ô nhập này
            ModelState.Remove("VaiTro");

            if (ModelState.IsValid)
            {
                var checkUser = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == nguoiDung.TaiKhoan);
                if (checkUser == null)
                {
                    nguoiDung.VaiTro = "KhachHang"; // Mặc định tài khoản mới là Khách hàng
                    db.NguoiDungs.Add(nguoiDung);
                    db.SaveChanges();

                    return RedirectToAction("DangNhap");
                }
                else
                {
                    ViewBag.ErrorRegister = "Tài khoản đã tồn tại. Vui lòng chọn tên khác!";
                }
            }
            return View(nguoiDung);
        }

        // 2. GET: Hiển thị form Đăng Nhập
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        // 2. POST: Xử lý Đăng Nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string TaiKhoan, string MatKhau)
        {
            if (ModelState.IsValid)
            {
                var data = db.NguoiDungs.FirstOrDefault(s => s.TaiKhoan == TaiKhoan && s.MatKhau == MatKhau);

                if (data != null)
                {
                    // Đăng nhập thành công -> Lưu Session
                    Session["TaiKhoan"] = data.TaiKhoan;
                    Session["VaiTro"] = data.VaiTro.Trim().ToUpper(); // Chuẩn hóa chữ in hoa để tránh lỗi khoảng trắng
                    Session["MaND"] = data.MaND;

                    // Phân luồng: Admin vào trang Quản lý Phim, Khách hàng ra Trang chủ
                    if (Session["VaiTro"].ToString() == "ADMIN")
                    {
                        return RedirectToAction("Index", "Phims");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.ErrorLogin = "Tài khoản hoặc mật khẩu không chính xác!";
                }
            }
            return View();
        }

        // 3. ĐĂNG XUẤT
        public ActionResult DangXuat()
        {
            Session.Clear(); // Xóa sạch Session
            return RedirectToAction("Index", "Home");
        }
    }
}