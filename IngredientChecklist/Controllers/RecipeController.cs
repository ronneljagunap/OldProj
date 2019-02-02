using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IngredientChecklist.Models;

namespace IngredientChecklist.Controllers
{
    public class RecipeController : Controller
    {
        // GET: Recipe
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Index", "Login", null);
            return View();
        }

        public ActionResult Recipe()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login", null);
            using (IngredientChecklistEntities db = new IngredientChecklistEntities())
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                var recipe = db.Recipes.Where(x => x.UserId == userID).ToList();
                return View(recipe);
            }
        }
        public ActionResult Create()
        {
            List<Ingredient> Ingredients = new List<Ingredient>();
            Recipe recipe = new Recipe();
            recipe.Ingredients = Ingredients;
            return View(new Recipe());
        }

        [HttpPost]
        public ActionResult Create(Recipe recipes)
        {
            using (IngredientChecklistEntities db = new IngredientChecklistEntities())
            {
                Recipe recipe1 = new Recipe();
                recipe1.Name = recipes.Name;
                recipe1.UserId = Convert.ToInt32(Session["UserID"]);
                db.Recipes.Add(recipe1);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult Edit(int id)
        {
            using (IngredientChecklistEntities db = new IngredientChecklistEntities())
            {
                var recipe = db.Recipes.SingleOrDefault(x => x.Id == id);
                return View(recipe);
            }
        }

        [HttpPost]
        public ActionResult Edit(Recipe recipes)
        {
            using (IngredientChecklistEntities db = new IngredientChecklistEntities())
            {
                recipes.UserId = Convert.ToInt32(Session["UserID"]);
                db.Entry(recipes).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult Ingredients(int id, string name)
        {
            Session["RecipeID"] = id;
            Session["RecipeName"] = name;
            return RedirectToAction("Index", "Ingredients", null);
        }

        public ActionResult Cook(int id, string name)
        {
            Session["RecipeID"] = id;
            Session["RecipeName"] = name;
            //return RedirectToAction("Index", "Cook", null);
            return RedirectToAction("Index", "Cooking", null);
        }

        public ActionResult ModalPopUp()
        {
            return View();
        }

    }
}