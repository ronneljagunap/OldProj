using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IngredientChecklist.Models;

namespace IngredientChecklist.Controllers
{
    public class IngredientsController : Controller
    {
        // GET: Ingredients
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Index", "Login", null);
            using (IngredientChecklistEntities db = new IngredientChecklistEntities())
            {
                int RecipeID = Convert.ToInt32(Session["RecipeID"]);
                List<Ingredient> ingredients = db.Ingredients.Where(x => x.RecipeId == RecipeID).ToList();
                ingredients.Insert(0, new Ingredient());
                return View(ingredients);
            }
        }

        [HttpPost]
        public JsonResult InsertIngredient(Ingredient ingredient)
        {
            using (IngredientChecklistEntities entities = new IngredientChecklistEntities())
            {
                int RecipeID = Convert.ToInt32(Session["RecipeID"]);
                ingredient.RecipeId = RecipeID;
                ingredient.IsChecked = false;
                entities.Ingredients.Add(ingredient);
                entities.SaveChanges();
            }
            return Json(ingredient);
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
                    updatedIngredient.Name = ingredient.Name;
                    entities.SaveChanges();
                }
            }
            return Json(new { success = true, responseText = "Updated sucessfully!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteIngredient(int id)
        {
            using (IngredientChecklistEntities entities = new IngredientChecklistEntities())
            {
                Ingredient updatedCustomer = (from c in entities.Ingredients
                                              where c.Id == id
                                              select c).FirstOrDefault();
                entities.Ingredients.Remove(updatedCustomer);
                entities.SaveChanges();
            }
            return Json(new { success = true, responseText = "Delete sucessfully!" }, JsonRequestBehavior.AllowGet);
        }
    }
}