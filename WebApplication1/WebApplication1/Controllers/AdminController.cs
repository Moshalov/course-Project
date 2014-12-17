using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAO;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {


        [Authorize(Roles = "Administrator")]
        public ActionResult CreateUser()
        {
            return View(new Models.RegisterViewModel());
        }


        public ActionResult SetRole(String user_id)
        {
            return View(new AspNetUserRoles() { UserId = user_id });
        }

        [HttpPost]
        public ActionResult SetRole(AspNetUserRoles roles)
        {
            Database db = new Database();
            db.GetTable<AspNetUserRoles>().InsertOnSubmit(roles);
            db.SubmitChanges();
            return Redirect("~/Admin/UserList");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult CreateUser(Models.RegisterViewModel acc)
        {
            AccountController ac = new AccountController();
            ac.Register(acc);
            Database db = new Database();
            string id = db.GetTable<AspNetUsers>().Single(obj => obj.UserName == acc.UserName).Id;
            db.GetTable<AspNetUserRoles>().InsertOnSubmit(new AspNetUserRoles() { RoleId = acc.Role_Id.ToString(), UserId = id });
            db.SubmitChanges();
            if (acc.Role_Id.ToString() == new Database().GetTable<AspNetRoles>().SingleOrDefault(x => x.Name.ToLower() == "School_Stuff".ToLower()).Id)
            {      
                return RedirectToAction("SelectSchoolForUser",id);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        public PartialViewResult AdminPanel()
        {
            if (User.IsInRole("Administrator"))
            {
                return PartialView();
            }
            else
            {
                return null;
            }
        }

          [Authorize(Roles = "Administrator")]
        public ActionResult Register()
        {
            return View();
        }

          [Authorize(Roles = "Administrator")]
        public ActionResult CreateSchool()
        {
            return View(new DAO.School());
        }

          [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult CreateSchool(DAO.School sc)
        {
            Database db = new Database();
            db.GetTable<School>().InsertOnSubmit(sc);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

          [Authorize(Roles = "Administrator")]
        public ActionResult ShcoolList()
        {
            Database db = new Database();
            return View(db.GetTable<School>().ToArray());
        }

          [Authorize(Roles = "Administrator")]
        public ActionResult UserList()
        {
            return View(new DAO.DAO().getUserList());
        }

          [Authorize(Roles = "Administrator")]
        public ActionResult SelectSchoolForUser(String user_id){
            return View(new DAO.AspUserInSchool());
        }

          [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult SelectSchoolForUser(DAO.AspUserInSchool us)
        {
            Database db = new DAO.Database();
            db.GetTable<AspUserInSchool>().InsertOnSubmit(us);
            db.SubmitChanges();
            return Redirect("~/Admin/UserList");
        }

          [Authorize(Roles = "Administrator")]
        public ActionResult AddSchool()
        {
            return View(new School());
        }

          [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult AddSchool(School sc)
        {
            Database db = new Database();
            db.GetTable<School>().InsertOnSubmit(sc);
            db.SubmitChanges();
            return RedirectToAction("ShcoolList", "Admin");

        }

	}
}