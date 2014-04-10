using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLootSession.Model
{
    public class Recipe
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("dateAdded")]
        public DateTime DateAdded { get; set; }

        [BsonElement("servingSize")]
        public double ServingSize { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; }

        [BsonElement("steps")]
        public List<string> Steps { get; set; }

        [BsonElement("photoId")]
        public ObjectId PhotoId { get; set; }
    }
}
