using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBLootSession.Models
{
    public class RecipeListViewModel
    {
        public RecipeListViewModel()
        {
            Recipes = new List<RecipeDetailViewModel>();
        }

        public List<RecipeDetailViewModel> Recipes { get; set; }
    }
}