using Kiemtra_VoThanhThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kiemtra_VoThanhThong.Controllers
{
    public class DangKyController : Controller
    {
        // GET: DangKy

        MyDataDataContext data = new MyDataDataContext();
        public List<HocPhan> LayHocPhan()
        {
            List<HocPhan> LayHocPhan = Session["HocPhan"] as List<HocPhan>;
            if (LayHocPhan == null)
            {
                LayHocPhan = new List<HocPhan>();
                Session["HocPhan"] = LayHocPhan;
            }

            return LayHocPhan;
        }


        // GET: GioHang
        public ActionResult ThemHocPhan(string id, string strUrl)
        {
            List<HocPhan> lstHocPhan = LayHocPhan();
            HocPhan hp = lstHocPhan.Find(n => n.MaHP == id);
            if (hp == null)
            {
                hp = new HocPhan(id);
                lstHocPhan.Add(hp);
                return Redirect(strUrl);
            }
            else
            {
                hp.iSoluong++;
                return Redirect(strUrl);
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<DangKy> lstDangKy = Session["DangKy"] as List<DangKy>;
            if (lstDangKy != null)
            {
                tsl = lstDangKy.Sum(n => n.iSoluong);
            }
            return tsl;
        }

        private int TongSoLuongHocPhan()
        {
            int tsl = 0;
            List<HocPhan> lstHocPhan = Session["HocPhan"] as List<HocPhan>;
            if (lstHocPhan != null)
            {
                tsl = lstHocPhan.Count;
            }
            return tsl;
        }

        public ActionResult DangKy()
        {
            List<HocPhan> lstHocPhan = LayHocPhan();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongsoluongsanpham = TongSoLuongHocPhan();
            return View(lstHocPhan);
        }

        public ActionResult XoaDangKy(string id)
        {
            List<HocPhan> lstHocPhan = LayHocPhan();
            HocPhan hp = lstHocPhan.SingleOrDefault(n => n.MaHP == id);
            if (hp != null)
            {
                lstHocPhan.RemoveAll(n => n.MaHP == id);
                return RedirectToAction("HocPhan");
            }
            return RedirectToAction("HocPhan");
        }
       
    }
}
