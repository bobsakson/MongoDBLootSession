// Load the RecipeLibrary.js file
load("c:\\users\\bobsakson\\documents\\visual studio 2013\\Projects\\MongoDBLootSession\\MongoDBLootSession\\MongoDBScripts\\RecipeLibrary.js")

// Create first recipe and the Recipes collection
var recipe = {
    "name": "My First Recipe",
    "dateAdded": new Date(2014, 4, 1),
    "tags": [
        "fish",
        "entree",
        "American"
    ]
}

db.Recipes.insert(recipe)

// Bring back the first recipe with findOne
db.Recipes.findOne({ name: "My First Recipe" })

// Bring back a cursor with recipes that match name "My First Recipe"
db.Recipes.find({ name: "My First Recipe" })

// Shows how limit(1) is the same as findOne
db.Recipes.find({ name: "My First Recipe" }).limit(1)

// Save a recipe - save inserts a record if _id is not provided, updates a document if provided
db.Recipes.save({
    "_id": ObjectId(""),
    "name": "My First Recipe",
    "dateAdded": ISODate("2014-05-01T05:00:00.000Z"),
    "tags": [
        "beef",
        "entree",
        "American"
    ]
})

// Attempts to update a document, if none found, inserts via the upsert attribute
db.Recipes.update({ name: "My First Recipe", "tags.0": "fish" }, recipe, { upsert: true })

// Updates only a specific field, not the whole document, with the $set command
db.Recipes.update({ name: "My First Recipe", "tags.0": "beef" }, { $set: { "tags.2": "French" } })

// Build out the recipe database
buildRecipes(10)
db.Recipes.ensureIndex({ "dateAdded": 1 })

// Finds document with either beef or French as a tag
db.Recipes.find({ tags: { $in: ["beef", "French"] } })

// Finds documents with beef as first tag, French as second tag, and a serving size greater than 10
db.Recipes.find({ "tags.0": "beef", "tags.2": "French", servingSize: { $gt: 10 } }).count()

// Finds documents with either beef or French and a serving size greater than 10
db.Recipes.find({ $or: [{ "tags.0": "beef" }, { "tags.2": "French" }], servingSize: { $gt: 10 } }).count()

// Projection, _id is always returned unless intentionally left off
db.Recipes.find({ 'steps.2': 'broil' }, { name: 1, _id: 0 })

// Show the query plan without an index
db.Recipes.find({ 'steps.2': 'broil' }).explain()

// Show the query plan with an index
db.Recipes.find({ dateAdded: { $gt: ISODate("2012-04-01T12:03:57.231Z") } }).explain()

// Count the number of steps in a recipe
db.Recipes.aggregate([
    { $match: { "tags.0": "beef" } },
    { $unwind: "$steps" },
    { $group: { _id: "$name", number: { $sum: 1 } } }
])