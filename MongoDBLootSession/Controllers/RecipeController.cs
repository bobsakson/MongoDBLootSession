using MongoDB.Driver;
using MongoDBLootSession.Model;
using MongoDBLootSession.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Builders;

namespace MongoDBLootSession.Controllers
{
    public class RecipeController : Controller
    {
        private MongoDatabase GetDB()
        {
            var client = new MongoClient("mongodb://localhost");
            var server = client.GetServer();
            return server.GetDatabase("RecipeDB");
        }

        private Recipe GetRecipe(ObjectId id)
        {
            var database = GetDB();

            var qd = new QueryDocument("_id", id);

            return database.GetCollection<Recipe>("Recipes").Find(qd).SetLimit(1).First();
        }

        //
        // GET: /Recipe/
        public ActionResult Index()
        {
            var database = GetDB();
            var recipeList = new RecipeListViewModel();

            database.GetCollection<Recipe>("Recipes").FindAll().ToList().ForEach(x => recipeList.Recipes.Add(new RecipeDetailViewModel() { Name = x.Name, Id = x.Id }));

            return View("Index", recipeList);
        }

        //
        // GET: /Recipe/Details/5
        public ActionResult Details(string id)
        {
            var objId = ObjectId.Parse(id);
            var client = new MongoClient("mongodb://localhost");
            var server = client.GetServer();
            var database = server.GetDatabase("RecipeDB");
            var settings = new MongoGridFSSettings();
            var gfs = new MongoGridFS(server, "RecipeDB", settings);            
            var qd = new QueryDocument("_id", objId);

            var model = database.GetCollection<Recipe>("Recipes").Find(qd).SetLimit(1).First();

            var recipe = new RecipeDetailViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                DateAdded = model.DateAdded,
                ServingSize = model.ServingSize,
                Steps = model.Steps,
                Tags = model.Tags,
                PhotoId = model.PhotoId
            };

            if(model.PhotoId != ObjectId.Empty)
            {
                var imageQD = new QueryDocument("_id", model.PhotoId);
                var stream = new System.IO.MemoryStream();
                gfs.Download(stream, imageQD);

                recipe.Image = System.Convert.ToBase64String(stream.ToArray());
            }
            
            return View("Details", recipe);
        }

        //
        // GET: /Recipe/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Recipe/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Recipe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Edit(RecipeDetailViewModel recipeDetailViewModel)
        {
            try
            {
                var client = new MongoClient("mongodb://localhost");
                var server = client.GetServer();
                var database = server.GetDatabase("RecipeDB");
                var settings = new MongoGridFSSettings();
                var gfs = new MongoGridFS(server, "RecipeDB", settings);

                var model = GetRecipe(recipeDetailViewModel.Id);

                var fileInfo = gfs.Upload(recipeDetailViewModel.Picture.InputStream, recipeDetailViewModel.Picture.FileName);

                model.PhotoId = fileInfo.Id.AsObjectId;

                var qd = new QueryDocument("_id", recipeDetailViewModel.Id);
                var ud = Update.Replace(model);
                database.GetCollection<Recipe>("Recipes").Update(qd, ud);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        //
        // GET: /Recipe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Recipe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
