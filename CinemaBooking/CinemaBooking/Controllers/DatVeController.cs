using System;
using System.Linq;
using System.Web.Mvc;
using CinemaBooking.Models;
using System.Data.Entity;

namespace CinemaBooking.Controllers
{
    public class DatVeController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // 1. Hiển thị sơ đồ ghế
        public ActionResult ChonGhe(int maSuatChieu)
        {
            // Bắt buộc phải đăng nhập mới được chọn ghế
            if (Session["TaiKhoan"] == null) return RedirectToAction("DangNhap", "Nguoidung");

            var suatChieu = db.SuatChieus.Include(s => s.GheNgois).FirstOrDefault(s => s.MaSuatChieu == maSuatChieu);
            if (suatChieu == null) return HttpNotFound();

            return View(suatChieu);
        }

        // 2. Xử lý đặt vé với TRANSACTION (Chống trùng ghế)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XacNhanDat(int maGhe, int maSuatChieu)
        {
            if (Session["TaiKhoan"] == null) return RedirectToAction("DangNhap", "Nguoidung");

            int maND = (int)Session["MaND"];

            // BẮT ĐẦU TRANSACTION: Khóa dữ liệu mức cao nhất (Serializable) để chống trùng ghế
            using (var transaction = db.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    var ghe = db.GheNgois.FirstOrDefault(g => g.MaGhe == maGhe);
                    var suatChieu = db.SuatChieus.FirstOrDefault(s => s.MaSuatChieu == maSuatChieu);

                    // Kiểm tra TrangThai == false (ghế còn trống)
                    if (ghe != null && ghe.TrangThai == false)
                    {
                        ghe.TrangThai = true; // Đổi trạng thái thành đã đặt (màu đỏ)

                        Ve veMoi = new Ve()
                        {
                            NgayDat = DateTime.Now,
                            TongTien = suatChieu.GiaVe,
                            MaND = maND,
                            MaSuatChieu = maSuatChieu,
                            MaGhe = maGhe
                        };
                        db.Ves.Add(veMoi);
                        db.SaveChanges(); // Lưu vào CSDL

                        transaction.Commit(); // Xác nhận giao dịch thành công
                        TempData["Message"] = "🎉 Đặt vé thành công! Vị trí ghế: " + ghe.ViTriGhe;
                    }
                    else
                    {
                        transaction.Rollback(); // Hoàn tác nếu có người khác vừa nhanh tay đặt trước
                        TempData["Error"] = "⚠️ Ghế này vừa có người đặt. Vui lòng chọn ghế khác!";
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback(); // Hoàn tác nếu lỗi CSDL
                    TempData["Error"] = "⚠️ Lỗi hệ thống khi xử lý vé.";
                }
            }
            return RedirectToAction("ChonGhe", new { maSuatChieu = maSuatChieu });
        }

        // 3. Hiển thị lịch sử đặt vé của khách hàng
        public ActionResult LichSu()
        {
            // Kiểm tra đăng nhập
            if (Session["TaiKhoan"] == null) return RedirectToAction("DangNhap", "Nguoidung");

            int maND = (int)Session["MaND"];

            // Lấy danh sách vé của người dùng hiện tại, sắp xếp vé mới nhất lên đầu
            // Dùng Include để kết nối lấy thông tin Phim, Suất Chiếu và Ghế
            var danhSachVe = db.Ves
                               .Include(v => v.SuatChieu.Phim)
                               .Include(v => v.GheNgoi)
                               .Where(v => v.MaND == maND)
                               .OrderByDescending(v => v.NgayDat)
                               .ToList();

            return View(danhSachVe);
        }
    }
}