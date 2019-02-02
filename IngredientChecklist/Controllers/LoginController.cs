using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IngredientChecklist.Models;

namespace IngredientChecklist.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Session["UserID"] = null;
            Session["FullName"] = null;

            return View(new User());
        }

        [HttpPost]
        public ActionResult Index(User user)
        {

            using (IngredientChecklistEntities db = new IngredientChecklistEntities())
            {
                var users = db.Users.SingleOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
                if (users != null)
                {
                    Session["UserID"] = users.Id.ToString();
                    Session["FullName"] = users.FullName.ToString();
                    return RedirectToAction("Index", "Home", null);
                }
                else
                {
                    return RedirectToAction("Index", "Login", null);
                }
            }

        }
    }
}