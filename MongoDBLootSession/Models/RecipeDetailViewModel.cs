using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBLootSession.Models
{
    public class RecipeDetailViewModel
    {
        public RecipeDetailViewModel()
        {
            Tags = new List<string>();
        }

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public List<string> Tags { get; set; }
        public double ServingSize { get; set; }
        public List<string> Steps { get; set; }
        public ObjectId PhotoId { get; set; }
        public HttpPostedFileBase Picture { get; set; }
        public string Image { get; set; }
    }
}