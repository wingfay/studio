using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZP.Bill.Models;

namespace ZP.Bill.Controllers
{
    public class HomeController : Controller
    {
        private billEntities db = new billEntities();

        public ActionResult Index()
        {
            var Goods = from goods in db.goods
                        join preorder in db.preorders
                            on goods.ID equals preorder.GoodsID into shownPostsInForum
                        select new GoodsCallModel
                        {
                            Goods = goods,
                            // Select the number of shown posts within the forum     
                            CallNumber = shownPostsInForum.Count()
                        };
            return View(Goods);
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login";
            return View();
        }


        public ActionResult LoginForm(string UserName, string Password, UserType userType)
        {
            if (userType == UserType.Buyer)
            {
                var buyer = db.buyers.Where(d => d.UserName == UserName && d.Password == Password).FirstOrDefault();
                if (buyer == null)
                {
                    ViewBag.ErrorMessage = "登录错误。";
                }
                else
                {
                    Session["User"] = buyer;

                    return RedirectToAction("Index", "Goods");
                }

            }

            if (userType == UserType.Seller)
            {
                var seller = db.sellers.Where(d => d.UserName == UserName && d.Password == Password).FirstOrDefault();
                if (seller == null)
                {
                    ViewBag.ErrorMessage = "登录错误。";
                }
                else
                {

                    Session["User"] = seller;

                    return RedirectToAction("Index", "Goods");
                }

            }

            return View("Login", ViewBag);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Register";

            return View();
        }

        public ActionResult ForgetPassword()
        {
            ViewBag.Message = "ForgetPassword";

            return View();
        }
    }
}