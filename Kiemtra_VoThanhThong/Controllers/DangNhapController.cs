using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiemtra_VoThanhThong.Models;

namespace Kiemtra_VoThanhThong.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var MaSV = collection["MaSV"];
            SinhVien sv = data.SinhViens.SingleOrDefault(n => n.MaSV == MaSV);
            if (sv != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["TaiKhoan"] = sv;
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return RedirectToAction("Index", "SinhVien");
        }
    }
}