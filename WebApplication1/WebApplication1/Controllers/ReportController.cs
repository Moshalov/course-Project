using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAO;

namespace WebApplication1.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
         [Authorize(Roles = "School_Stuff")]
        public ActionResult Create()
        {
           // if(DAO.Database())
            return View(new Report());
        }

        [Authorize(Roles = "School_Stuff")]
        [HttpPost]
        public ActionResult Create(Report rep)
        {
            DAO.Database db = new DAO.Database();
            rep.UserName = User.Identity.Name;
            Table<Report> reports = db.GetTable<Report>();
            rep.Date = System.DateTime.Now;
            rep.Status_id = 2;
            String user_id = db.GetTable<AspNetUsers>().Single(x => x.UserName == User.Identity.Name).Id;
            Table<AspUserInSchool> tab = db.GetTable<AspUserInSchool>();
            AspUserInSchool s = tab.SingleOrDefault(x => x.User_id == user_id);
            rep.School = s.School1;
            reports.InsertOnSubmit(rep);
            db.SubmitChanges();
            return RedirectToAction( "List","Report");
        }


        [Authorize(Roles = "School_Stuff,Supervisor")]
        public ActionResult List()
        {
            DAO.Database db = new DAO.Database();
            if (User.IsInRole("Supervisor"))
            {
                Table<Report> reports = db.GetTable<Report>();
                var test = from c in reports
                           select c;
                return View(test.ToList());
            }
            else
            {

                Table<Report> reports = db.GetTable<Report>();
                var test = from c in reports
                           where c.UserName == User.Identity.Name
                           select c;
                return View(test.ToList());
            }
        }

         [Authorize(Roles = "School_Stuff")]
        public ActionResult Edit(int id)
        {
            Table<Report> reports = new DAO.Database().GetTable<Report>();
            Report rep = reports.SingleOrDefault(item => item.Id == id);
            if (rep.UserName == User.Identity.Name)
            {
                return View(rep);
            }
            else
            {
                ViewBag.Message = "Нет доступа";
                return RedirectToAction("list");
            }
        }
         [Authorize(Roles = "School_Stuff")]
        [HttpPost]
        public ActionResult Edit(Report rep)
        {
            DAO.Database db = new DAO.Database();
          Report reps = db.GetTable<Report>().Single(x => x.Id == rep.Id);
                 reps.Id = rep.Id;
                 reps.Name = rep.Name;
                 reps.Text = rep.Text;
                 reps.School_id = rep.School_id;
                 reps.Date = rep.Date;
                 reps.Status_id = 2;
                 reps.Type_id = rep.Type_id;
                 reps.UserName = rep.UserName;
             db.SubmitChanges();
             return RedirectToAction("list", "Report");
        }

         [Authorize(Roles = "Supervisor")]
        public ActionResult Allow(int id)
        {
            Database db = new Database();
            Report rep = db.GetTable<Report>().SingleOrDefault(item => item.Id == id);
            return View(rep);
        }
        [Authorize(Roles = "Supervisor")]
        [HttpPost]
        public ActionResult Allow(Report rep)
        {
            DAO.Database db = new DAO.Database();
            rep.UserName = User.Identity.Name;
            Table<Report> reports = db.GetTable<Report>();
            var test = from c in reports
                       where c.Id == rep.Id
                       select c;
            foreach (Report r in test)
            {
                r.Status_id = rep.Status_id;
            }

            db.SubmitChanges();
            return RedirectToAction("List","Report");
        }

        [Authorize(Roles = "School_Stuff, Supervisor")]
        public PartialViewResult Comments(int id)
        {
            DAO.Database db = new Database();
            var test = from c in db.GetTable<DAO.Comments>()
                       where c.Report_id == id
                       select c;
            return PartialView(test.ToArray());
        }

        [Authorize(Roles = "School_Stuff, Supervisor")]
        public ActionResult AddCommentView(int id)
        {
            Comments com = new Comments();
            com.Report_id = id;
            com.UserName = User.Identity.Name;
            return View(com);
        }

        [Authorize(Roles = "School_Stuff, Supervisor")]
        [HttpPost]
        public ActionResult AddCommentView(DAO.Comments com)
        {
                Database db = new Database();
                com.Report_id = com.Id;
                com.UserName = User.Identity.Name;
                com.Date_time = DateTime.Now;    
                db.GetTable<Comments>().InsertOnSubmit(com);
                db.SubmitChanges();
                return RedirectToAction("Details","Report", new { id = com.Report_id});
                
        }

        public ActionResult Details(int id)
        {
            Database db = new Database();
            Report rep = db.GetTable<Report>().SingleOrDefault(item => item.Id == id);
            return View(rep);
        }

        public ActionResult Index()
        {
            return Redirect("~Route/List");
        }
	}
}
