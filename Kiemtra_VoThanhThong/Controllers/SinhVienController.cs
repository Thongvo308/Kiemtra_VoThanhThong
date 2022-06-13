using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiemtra_VoThanhThong.Models;

namespace Kiemtra_VoThanhThong.Controllers
{
    public class SinhVienController : Controller
    {
        // GET: SinhVien
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_SinhVien = from tt in data.SinhViens select tt;
            return View(all_SinhVien);
        }

        //---------Detail------------   
        public ActionResult Detail(string id)
        {
            var D_SinhVien = data.SinhViens.Where(m => m.MaSV == id).First();
            return View(D_SinhVien);
        }

        //---------create------------
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien sv)
        {
            var E_Hoten = collection["HoTen"];
            var E_Hinh = collection["Hinh"];
            var E_MaNganh = collection["MaNganh"];
            if (string.IsNullOrEmpty(E_Hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sv.HoTen = E_Hoten.ToString();
                sv.Hinh = E_Hinh.ToString();
                sv.MaNganh = E_MaNganh;
                data.SinhViens.InsertOnSubmit(sv);
                data.SubmitChanges();
                return RedirectToAction("ListSach");
            }
            return this.Create();
        }

    //---------Edit------------
        public ActionResult Edit(string id)
        {
            var E_category = data.SinhViens.First(m => m.MaSV == id);
            return View(E_category);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_SinhVien = data.SinhViens.First(m => m.MaSV == id);
            var E_HoTen = collection["HoTen"];
            var E_Hinh = collection["Hinh"];
            var E_MaNganh = collection["MaNganh"];
            E_SinhVien.MaSV = id;
            if (string.IsNullOrEmpty(E_HoTen))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_SinhVien.HoTen = E_HoTen;
                E_SinhVien.Hinh = E_Hinh;
                E_SinhVien.MaNganh = E_MaNganh;
                UpdateModel(E_SinhVien);
                data.SubmitChanges();
                return RedirectToAction("SinhVien");
            }
            return this.Edit(id);
        }
        //-------------Delete-------------------

        public ActionResult Delete(string id)
        {
            var D_SinhVien = data.SinhViens.First(m => m.MaSV == id);
            return View(D_SinhVien);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_SinhVien = data.SinhViens.Where(m => m.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(D_SinhVien);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}
 