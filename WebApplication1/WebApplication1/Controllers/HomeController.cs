using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAO;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Login()
        {
            return Redirect("~/Acount/Login");
        }
        public ActionResult Index()
        {

            if (User.IsInRole("Administrator"))
            {
                return Redirect("~/Admin/UserList");
            }
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Account/Login");
            }
            else
            {
                return Redirect("~/Report/List");
            }
        }
        [Authorize(Roles = "School_Stuff")]
        public ActionResult About()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Account/Login");
            }
            else
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }
        }

        public ActionResult Contact()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Account/Login");
            }
            else
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
        }
        public ActionResult stringError(String str)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Account/Login");
            }
            else
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
        }

        public PartialViewResult Menu()
        {
            if (User.IsInRole("Administrator"))
            {
                return PartialView("~/Shared/AdminPanel");
            }
            else
            {
                if (User.IsInRole("SuperVisor"))
                {
                    return PartialView("~/Shared/AdminPanel");
                }
                else
                {
                    return PartialView("~/Shared/AdminPanel");
                }
            }
        }
    }
}