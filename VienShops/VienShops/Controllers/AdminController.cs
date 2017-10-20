using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VienShops.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
namespace VienShops.Controllers
{
    public class AdminController : Controller
    {
        DBVienShopsDataContext Db = new DBVienShopsDataContext();
        // GET: Admin
        public ActionResult AdminHome(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(Db.SANPHAMs.ToList().OrderBy(n=>n.TENSP).ToPagedList(pageNumber,pageSize));
        }
        // Thêm mới sản phẩm
        [HttpGet]
        public ActionResult AddProductNew()
        {
            // Đưa vào dropdown

            ViewBag.MaLoaiSP = new SelectList(Db.LOAISANPHAMs.ToList(),"MaLoaiSP","TenLoai");
            return View();
        }
        // Up load file ảnh
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProductNew(SANPHAM sanpham, HttpPostedFileBase fileUpload)
        {
           
            ViewBag.MaLoaiSP = new SelectList(Db.LOAISANPHAMs.ToList(), "MaLoaiSP", "TenLoai");
            if(fileUpload == null)
            {
                ViewBag.Notification = "Mời bạn chọn hình ảnh";
                return View();
            }
            // Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                // Lưu tên file 
                var fileName = Path.GetFileName(fileUpload.FileName);
                // Lưu đường dẫn file
                var path = Path.Combine(Server.MapPath("~/images/products/large"), fileName);
                // Kiểm tra hình ảnh đã tồn tại
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Notification = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sanpham.URL = fileUpload.FileName;
                Db.SANPHAMs.InsertOnSubmit(sanpham);
                Db.SubmitChanges();
            }
            return View();
        }
    }
}