using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IngredientChecklist.Models;

namespace IngredientChecklist.Controllers
{
    public class ChecklistController : Controller
    {

        // GET: Cooking
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Index", "Login", null);

            using (IngredientChecklistEntities db = new IngredientChecklistEntities())
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                var recipes = db.Recipes.Where(x => x.UserId == userID).ToList();
                List<SelectListItem> RecipesList = recipes.Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() }).ToList();
                ViewBag.Recipes = RecipesList;
                var ingredients = new List<Ingredient>();
                return View(ingredients);
            }
        }

        public PartialViewResult GetItem(int id)
        {
            using (IngredientChecklistEntities db = new IngredientChecklistEntities())
            {
                var charts = db.Ingredients.ToList();
                var model = charts.Where(x => x.RecipeId == id).ToList();
                return PartialView("_GetItem", model);
            }
        }

        [HttpPost]
        public ActionResult UpdateIngredient(Ingredient ingredient)
        {
            if (ingredient.Id != 0)
            {
                using (IngredientChecklistEntities entities = new IngredientChecklistEntities())
                {
                    Ingredient updatedIngredient = (from c in entities.Ingredients
                                                    where c.Id == ingredient.Id
                                                    select c).FirstOrDefault();
                    updatedIngredient.IsChecked = ingredient.IsChecked;
                    entities.SaveChanges();
                }

            }
            return Json(new { success = true, responseText = "Updated sucessfully!" }, JsonRequestBehavior.AllowGet);
        }

    }
}