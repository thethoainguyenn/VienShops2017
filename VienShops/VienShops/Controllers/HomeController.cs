using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VienShops.Models;

namespace VienShops.Controllers
{
    public class HomeController : Controller
    {
        DBVienShopsDataContext Db = new DBVienShopsDataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        // sản phẩm mới 
        public ActionResult NewProduct()
        {
            var newProduct = Db.SANPHAMs.OrderBy(n => n.MASP).Take(4).ToList();
            return PartialView(newProduct);
        }
        public ActionResult FeaturedProduct()
        {
            var featuredProduct = Db.SANPHAMs.OrderBy(n => n.GIA).Take(4).ToList();
            return PartialView(featuredProduct);
        }
        public ActionResult Products()
        {
            var products = Db.LOAISANPHAMs.ToList();
            return PartialView(products);
        }
        // Show products list
        public ActionResult ShowProductList(string id)
        {
            //var tenLoai = Db.LOAISANPHAMs.SingleOrDefault(n => n.MALOAISP == id);
            var productList = Db.SANPHAMs.Where(n => n.MALOAISP == id).ToList();
            // Gán mã, khi load form lưu id
            ViewBag.ID = id;
            return View(productList);
        }
        // Show products list giao diện 2
        public ActionResult ShowProductListTheme2(string id)
        {
            var productList = Db.SANPHAMs.Where(n => n.MALOAISP == id).ToList();
            return View(productList);
        }
        // Menu Trái của Loại sản phẩm 
        public PartialViewResult MenuLeft()
        {
            return PartialView();
        }
        // Liên hệ 
        public ActionResult Contact()
        {
            return View();
        }
        // Giỏ hàng 
        public ActionResult Cart()
        {
            return View();
        }
        // Chi tiết sản phẩm 
        public ActionResult Detail(string id)
        {
            var detail = Db.SANPHAMs.Where(n => n.MASP == id).ToList();
            return View(detail);
        }

    }
}